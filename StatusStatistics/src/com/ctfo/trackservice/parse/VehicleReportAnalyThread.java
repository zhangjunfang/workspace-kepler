package com.ctfo.trackservice.parse;

import java.io.File;
import java.io.IOException;
import java.sql.SQLException;
import java.util.Iterator;
import java.util.List;
import java.util.Set;
import java.util.TreeMap;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.dao.ThreadPool;
import com.ctfo.trackservice.io.LoadFile;
import com.ctfo.trackservice.model.VehicleMessageBean;
import com.ctfo.trackservice.service.VechicleReportService;
import com.ctfo.trackservice.util.DateTools;
import com.ctfo.trackservice.util.Tools;

public class VehicleReportAnalyThread extends AbstractThread{
	private final static Logger logger = LoggerFactory.getLogger(VehicleReportAnalyThread.class);

	private ArrayBlockingQueue<File> queue = new ArrayBlockingQueue<File>(5000);
	//private VehicleRunningUpdateThread vehicleRunningUpdateThread;
	//private static VehicleRunningAnalyserService vehicleRunningAnalyserService;
	private long utc;
	//private static String vid;
	private long threadId ;
	
	private long endTime = System.currentTimeMillis();
	
	private final long time = 60 * 1000;
	private Boolean flag = true;
	public VehicleReportAnalyThread(int threadId,int threadNum,long u){
		setName("VehicleReportAnanly-thread-"+threadId);
		this.threadId = threadId;
		utc = u;
	}
	
	@Override
	public void init() {
		// TODO Auto-generated method stub
		
	}
	/**
	 * 执行主方法
	 */
	public void run() {
		while(flag){
			try {
				File file = queue.poll();				
				if(file != null){
					long  startTime=System.currentTimeMillis();
					statisticStatus(file); //开始统计
					endTime=System.currentTimeMillis();
					logger.info("----单车报告分析线程Thread Id : {},处理文件名称:{},处理时长：【{}】ms",this.threadId,file.getName(),(endTime-startTime));
				}
				else{
					if((System.currentTimeMillis()-endTime)>time){
						//1分钟秒没文件读取，自动关闭分析线程
						close();
					}
				}
				
			} catch (Exception e) {
				logger.debug("单车报告分析异常！",e);
			
			}
		}
		logger.info("----单车报告分析线程Thread Id : {} 已关闭！",this.threadId);
	}
	
	
	/**
	 * 文件分析方法
	 * @param file
	 * @throws Exception 
	 */
	private void statisticStatus(File file) throws Exception {	
		VechicleReportService vechicleReportService = new VechicleReportService(utc, file.getName().replaceAll("\\.txt", ""));
		readTrackFile(file,vechicleReportService);
	}
	
	
	/**
	 * 读取轨迹文件
	 * @param file
	 * @throws SQLException 
	 * @throws IOException 
	 */
	private void readTrackFile(File file,VechicleReportService vechicleReportService) throws Exception{

		TreeMap<Long, String> statusMap = new TreeMap<Long, String>();
		int rowCount = 0;
		int currentRow =0;
		//long start = System.currentTimeMillis();
		statusMap = getTrackMap(file);
		//logger.info("----IO读取并排序文件耗时：【{}】ms,文件名: {}",(System.currentTimeMillis()-start),file.getName());
		rowCount = statusMap.size();
		Set<Long> key = statusMap.keySet();
		Long keys = null;		
		String[] cols = null;

		for (Iterator<Long> it = key.iterator(); it.hasNext();) {
			keys = (Long) it.next();
			try{
					String trackStr = statusMap.get(keys);
					cols = trackStr.split(":");
					
					boolean isLastRow = false;
					currentRow++;
					if (rowCount==currentRow){
						isLastRow = true;
					}
					VehicleMessageBean trackBean = this.changTxtVMB(cols);
					trackBean.setTrackStr(trackStr);
					vechicleReportService.executeAnalyser(trackBean, isLastRow);		
					
			}catch (Exception e) {
				logger.debug("----------轨迹文件分析异常!"+e.getMessage());
			}
		}
		//加入提交线程
		ThreadPool.getVehicleReportUpdatePool().get(1).addPacket(vechicleReportService.getCoolLiquidtemBean());// 冷却液温度
		ThreadPool.getVehicleReportUpdatePool().get(2).addPacket(vechicleReportService.getGsPressureBean());// 进气压力
		ThreadPool.getVehicleReportUpdatePool().get(3).addPacket(vechicleReportService.getOilPressureBean());// 燃油压力
		ThreadPool.getVehicleReportUpdatePool().get(4).addPacket(vechicleReportService.getRotateSpeedDay());//转速
		ThreadPool.getVehicleReportUpdatePool().get(5).addPacket(vechicleReportService.getSpeeddistDay());//速度	
		ThreadPool.getVehicleReportUpdatePool().get(6).addPacket(vechicleReportService.getAirTempertureBean());// 进气温度
		ThreadPool.getVehicleReportUpdatePool().get(7).addPacket(vechicleReportService.getVoltagedistDay());// 蓄电池电压
	}
			
			
	/**
	 * 
	 * 根据gps时间将读取的轨迹文件数据进行排序
	 * @param file
	 * @return
	 */
	private TreeMap<Long, String> getTrackMap(File file) {	
		TreeMap<Long, String> returnTrackMap = new TreeMap<Long, String>();
		String gpsdate = null;
		String[] track = null;
		Long gpstime = null;
		try {
			List<String> list = LoadFile.readLines(file, null);
			for(String str : list){
				// 轨迹文件每行的数据分割
				track = StringUtils.splitPreserveAllTokens(str, ":");
				if(track.length > 35){
//							速度来源是行驶记录仪
					int speed = 0;
					if(StringUtils.isNumeric(track[19]) && StringUtils.isNumeric(track[24]) && track[24].equals("0")){ // 解析速度
						speed = Integer.parseInt(track[19]);
					}else{
//							速度来源是GPS
						if(StringUtils.isNumeric(track[3]) ){
							speed = Integer.parseInt(track[3]);
						} else{
							speed = -1;
						}
					}
					if(speed < 0 && speed >= 1400 ){ // 非法速度数据过滤
						continue;
					}
					gpsdate = track[2];
					// 将gpsdate转换成utc格式
					gpstime = DateTools.stringConvertUtc(gpsdate);

					returnTrackMap.put(gpstime, str);	
				}

			}// End while		
			list.clear();
		} catch (Exception e) {
			logger.error("Thread Id : "+ this.threadId + ",读取轨迹文件信息出错",e);
		}
		return returnTrackMap;
	}
	
		
	/**
	 * 
	 * 轨迹文件字段解析
	 * @param track
	 * @return
	 */
	private VehicleMessageBean changTxtVMB(String[] track){
		
		VehicleMessageBean po = new VehicleMessageBean();
		
		po.setCommanddr("");
		po.setMaplon((Long)formatValueByType(track[0],"0",'L'));
		po.setMaplat((Long)formatValueByType(track[1],"0",'L'));
		po.setDateString(track[2]);
		long utc = DateTools.stringConvertUtc(track[2]);
		po.setUtc(utc);
		String speedForm = (String)formatValueByType(track[24],"1",'S');
//			Long spd = 0L;
		Long gpsSpeed = (Long)formatValueByType(track[3],"0",'L');
		po.setGpsSpeed(gpsSpeed);
		Long vssSpeed = (Long)formatValueByType(track[19],"0",'L');
		po.setVssSpeed(vssSpeed);
		
		if ("0".equals(speedForm)){
			if(vssSpeed<0){
				po.setSpeed(0L);
			}else{
				po.setSpeed(vssSpeed);
				}
		}else{
			if(gpsSpeed<0){
				po.setSpeed(0L); // 1：来自GPS
			}else{
				po.setSpeed(gpsSpeed);
			}
		}
		po.setSpeedFrom(speedForm);
		po.setDir((Integer)formatValueByType(track[4],"0",'I'));
		po.setAlarmcode((String)formatValueByType(track[6],"",'S'));
		po.setLon((Long)formatValueByType(track[7],"0",'L'));
		po.setLat((Long)formatValueByType(track[8],"0",'L'));
		po.setElevation((Integer)formatValueByType(track[9],"0",'I'));
		
		
		po.setMileage((Long)formatValueByType(track[10],"-1",'L'));
		po.setOil((Long)formatValueByType(track[11],"-1",'L'));
		po.setRpm((Long)formatValueByType(track[13],"-1",'L'));
		String baseStatus = (String)formatValueByType(track[14],"0",'S');
		po.setBaseStatus(baseStatus);
		String number = Tools.getBinaryString(baseStatus);
		po.setAccState(Tools.check("0", number));
		po.setGpsState(Tools.check("1", number));
		po.setAreaAlarmAdditional((String)formatValueByType(track[15],"",'S'));
		po.setCoolLiquidtem((Long)formatValueByType(track[16],"-1",'L'));
		po.setVolgate((Long)formatValueByType(track[17],"-1",'L'));
		po.setOilPres((Long)formatValueByType(track[20],"-1",'L'));
		po.setGsPres((Long)formatValueByType(track[21],"-1",'L'));
		po.setTorque((String)formatValueByType(track[22],"",'S'));
		po.setExtendStatus((String)formatValueByType(track[23],"0",'S'));
		po.setOverspeedAlarmAdditional((String)formatValueByType(track[26],"",'S'));
		po.setAlarmAdditional((String)formatValueByType(track[27],"",'S'));
		
		po.setAirTemperture((Long)formatValueByType(track[32],"-1",'L'));
		po.setOpendoorState((String)formatValueByType(track[33],"",'S'));
		po.setMetOil((Long)formatValueByType(track[35],"-1",'L'));
		
	/*	if (track.length>=39){
			String driverId = (String)formatValueByType(track[36],"",'S');
			if (driverId!=null&&!"".equals(driverId)){
				po.setDriverId(driverId);
				po.setDriverName(getCurrectDriverName(driverId));
				po.setDriverSrc((String)formatValueByType(track[37],"",'S'));
			}else{
				//如果当前轨迹没有匹配驾驶员信息则需要从刷卡记录中按时间段进行匹配
				String driverinfo = getCurrectDriver(utc);
				if (driverinfo!=null&&!"".equals(driverinfo)){
					String driver[] = driverinfo.split(";");
					po.setDriverId(driver[0]);
					po.setDriverName(driver[1]);
					po.setDriverSrc(driver[2]);
				}
			}
		}*/
		
		return po;
	}
	
	private Object formatValueByType(String str,String defaultVal,char type){
		Object obj = null;
		switch (type)	{
			case 'S': obj=((str==null || "".equals(str)|| "null".equals(str))?defaultVal:str.trim());break;
			case 'L': obj=Long.parseLong((str==null || "".equals(str)|| "null".equals(str))?defaultVal:str.trim());break;
			case 'I': obj=Integer.parseInt((str==null || "".equals(str)|| "null".equals(str))?defaultVal:str.trim());break;
		}
		return obj;
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */

	@Override
	public void addPacket(Object o) {
		try {
			queue.put((File)o);
		} catch (InterruptedException e) {
			logger.error("插入数据到队列异常!"); 
		}
		
	}

	@Override
	public void close() {
		this.flag = false;
		
	}

	@Override
	public void setThreadId(int threadId) {
		this.threadId = threadId;
		
	}

	@Override
	public void setTime(long utc) {
		// TODO Auto-generated method stub
		
	}

	


}
