package com.ctfo.trackservice.task;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Iterator;
import java.util.List;
import java.util.Set;
import java.util.TreeMap;
import java.util.Vector;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.dao.ThreadPool;
import com.ctfo.trackservice.io.LoadFile;
import com.ctfo.trackservice.model.OilMonitorBean;
import com.ctfo.trackservice.model.OilmassChangedDetail;
import com.ctfo.trackservice.parse.AbstractThread;
import com.ctfo.trackservice.parse.OilWearAnalyThread;
import com.ctfo.trackservice.parse.OilWearUpdateThread;
import com.ctfo.trackservice.service.OracleService;
import com.ctfo.trackservice.util.DateTools;
import com.ctfo.trackservice.util.Tools;
import com.ctfo.trackservice.util.Utils;

/**
 * 文件名：OilWearTask.java
 * 功能：油箱油量监控统计任务
 *
 * @author huangjincheng
 * 2014-9-24下午5:10:26
 * 
 */
public class OilWearTask  extends TaskAdapter{
	private final static Logger logger = LoggerFactory.getLogger(OilWearTask.class);
	/**油耗文件路径*/
	private String oilFilePath ="";
	/**轨迹文件路径*/
	private String trackFilePath ="";
	/**当日12点utc*/
	//private long utc = -1l;
	/**写油箱油量监控文件数*/
	private static long oilFileCount = 0l;
	/**写油箱油量监控文件数*/
	private static long buildOilFileCount = 0l;
	private OracleService oracleService = new OracleService();
	private Boolean flag ;
	@Override
	public void init() {
		this.oilFilePath = config.get("oilFilePath");
		this.trackFilePath = config.get("trackFilePath");
		//this.utc = DateTools.getYesDayYearMonthDay();
	}

	@Override
	public void execute() throws Exception {
		
		if("restore".equals(this.type)){
			String restoreTime = "";
			System.out.print("--------------------【油箱油量监控】输入您需要补跑的日期(yyyy/mm/dd)：");
			while(true){
				BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
				restoreTime = br.readLine();
				if (!restoreTime.matches("\\d{4}/\\d{2}/\\d{2}")) {
					System.out.print("--------------------【油箱油量监控】输入错误,请重新选择输入：");
					continue;
				}else break;
			}
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd hh/mm/ss");
			Date date = sdf.parse(restoreTime+" 00/00/00");
			utc = date.getTime();
			logger.info("--------------------正在进行补录数据删除，请稍后...");
			oracleService.deleteOilInfo(utc+12*60*60*1000);	
		}else if("start".equals(this.type)){
			utc = DateTools.getYesDayYearMonthDay();
//			6.加载油箱油量监控车辆列表
			oracleService.loadOilMonitorVehicleListing();
//			7.加载车辆静态信息列表
			oracleService.loadVehicleInfo();
		}else if("autoRestore".equals(this.type)){
			logger.info("--------------------【油箱油量监控】自动补跑启动,时间:{}",DateTools.getStringDate(utc));
			logger.info("--------------------正在进行补录数据删除，请稍后...");
			oracleService.deleteOilInfo(utc+12*60*60*1000);	
		}
		
		
		logger.info("---------------------------【油箱油量监控】任务开始！-------------------------");
		flag = true;
		//ThreadPool.init();
		long startTime = System.currentTimeMillis();
		Vector<File> fl = LoadFile.findFile(trackFilePath, utc);
		for(int emp = 0;emp<fl.size();emp++){
			String vid = fl.get(emp).getName().replaceAll("\\.txt", "");
			String goalPath = fl.get(emp).getPath().replace("track", "oilUrl");
			// 判断是否油箱液位传感器
			judgeSensor(vid,fl.get(emp).getPath(),goalPath);
		}
		logger.info("--------------------传感器油箱油量文件生成！油箱配置车辆数：{}，实际生成文件数：{}(油耗为0的轨迹不生成油量文件)",oilFileCount,buildOilFileCount);		
		for(int i = 0;i<this.threadNum;i++){
			OilWearAnalyThread oilWearAnalyThread = new OilWearAnalyThread(i+1,this.threadNum,utc);
			oilWearAnalyThread.start();
			ThreadPool.addOilWearAnalyPool(i, oilWearAnalyThread);
		}
		OilWearUpdateThread oilWearUpdateThread = new OilWearUpdateThread(1);
		ThreadPool.addOilWearUpdatePool(1, oilWearUpdateThread);
		oilWearUpdateThread.start();	
		Vector<File> fileList = LoadFile.findFile(oilFilePath, utc);
		
		logger.info("--------------------油量文件加载完成！文件数：[{}]",fileList.size());
		ThreadPool.setOilSize(fileList.size());
		ThreadPool.setCurrentOilsize(0);
		ThreadPool.oilFileMap.clear();
		for(int e=0;e<fileList.size();e++){
			//设置油耗文件缓存，为查询传感器油耗准备		
			ThreadPool.oilFileMap.put(fileList.get(e).getName().replaceAll("\\.txt", ""), fileList.get(e));
			//平均分配文件装载线程
			ThreadPool.getOilWearAnalyPool().get(e % ThreadPool.getOilWearAnalyPool().size()).addPacket(fileList.get(e));
		}
		fileList.clear();
		while(flag){
			long fileSize = ThreadPool.getCurrentOilsize();
			logger.info("-------------------oilSize : {} , currentOilSize : {}",ThreadPool.getOilSize(),fileSize);
			if(ThreadPool.getOilSize() == fileSize){
				flag = false;
				closeThread();
				logger.info("---------------车辆油箱油量日统计完成！日期：【{}】,耗时： 【{}】s",Tools.getDate(utc),(System.currentTimeMillis()-startTime)/1000);			
				logger.info("---------------开始加载油箱油量传感器油耗缓存！");
				oracleService.loadingOilMap(utc);
				ThreadPool.isOilOver = true;
			}else{
				try {
					Thread.sleep(30 * 1000); 
				} catch (InterruptedException e) {
					logger.error("车辆统计出错.",e);
				}
			}
		}
		
	}
	/**
	 * 判断传感器油箱油量监控
	 * @param vid
	 * @param path
	 */
	private void judgeSensor(String vid, String path, String goalPath) {
		try{
			File f = new File(goalPath);
			//03是金旅，02是宇通
			//ThreadPool.oilMonitorMap.size();
			
			if(null !=ThreadPool.oilMonitorMap.get(vid) && ("000100060003".equals(ThreadPool.oilMonitorMap.get(vid))) || "000100060002".equals(ThreadPool.oilMonitorMap.get(vid))){
				if(!f.exists()){
					oilFileCount++;				
					OilMonitorBean oilMonitorBean = new OilMonitorBean();
					oilMonitorBean = readTrackFile(new File(path));
					writeOilChangedMassFile(vid,f,oilMonitorBean);
				}
			}
		}catch (Exception e) {
			logger.debug("判断传感器油箱油量监控出错！");
		}
		
	}
	
	private OilMonitorBean readTrackFile(File file) throws Exception{
		OilMonitorBean oilMonitorBean = new OilMonitorBean();
		TreeMap<Long, String> statusMap = new TreeMap<Long, String>();
		int count = 0;
		try{
			statusMap = getTrackMap(file);
			Set<Long> key = statusMap.keySet();//按时间倒序
			Long keys = null;		
			String[] cols = null;
			for (Iterator<Long> it = key.iterator(); it.hasNext();) {
				keys = (Long) it.next();
				String trackStr = statusMap.get(keys);
				cols = trackStr.split(":");
				count++;
				analyseOilmassData(cols,oilMonitorBean);
			}
		}catch (Exception e) {
			logger.debug("----------------分析轨迹文件中油耗数据错误！文件名：,行数："+file.getName(),count,e);
		}
		return oilMonitorBean;
	}
	
	/**
	 * 分析油量数据，产生油量监控记录
	 * @param cols
	 */
	public int analyseOilmassData(String[] cols,OilMonitorBean oilMonitorBean){
		int ret = 0;
		try{
			if(cols.length >35){
				String spdFrom = cols[24];	//车速来源
				int speed = Utils.getSpeed(spdFrom,cols);
				int oilBox = Integer.parseInt(Utils.checkEmpty(cols[25]) ? "0":cols[25]);
				//logger.info("oilBox:"+oilBox);
				String charGpsTime = cols[2];
				long gpsTime = DateTools.stringConvertUtc(charGpsTime);
				Long lon =  Long.parseLong(cols[7]==null||"".equals(cols[7])?"0":cols[7]);
				Long lat = Long.parseLong(cols[8]==null||"".equals(cols[8])?"0":cols[8]);
				Long mapLon =  Long.parseLong(cols[0]==null||"".equals(cols[0])?"0":cols[0]);
				Long mapLat = Long.parseLong(cols[1]==null||"".equals(cols[1])?"0":cols[1]);
				
				Long rpm = -1L;
				if(!Utils.checkEmpty(cols[13])){ 
					rpm = Long.parseLong(cols[13]==null||"".equals(cols[13])?"0":cols[13]);
				}
				
				String binaryStus = "";
				if(!Utils.checkEmpty(cols[14])){
					binaryStus = Long.toBinaryString(Long.parseLong(cols[14]));
				}
				
			
				
				
				String currStatus = "";
				//判断当前是否行车 车速大于5 点火状态为1 转速大于800
				if (speed>5*10&&binaryStus.endsWith("1")&&checkRpm(rpm)){
					//行车状态
					currStatus = "R";
				}else{
					//停车状态
					currStatus = "S";
				}
				
				//拼装油量监控点对象
				OilmassChangedDetail oil = new OilmassChangedDetail();
				oil.setDirection(Integer.parseInt(Utils.checkEmpty(cols[4]) ? "0":cols[4]));
				oil.setElevation(Integer.parseInt(Utils.checkEmpty(cols[9]) ? "0":cols[9]));
				oil.setGps_speed(speed);
				oil.setUtc(gpsTime);
				oil.setLat(lat);
				oil.setLon(lon);
				oil.setMapLat(mapLat);
				oil.setMapLon(mapLon);
				oil.setChangeType("00");
				oil.setCurr_oilmass(oilBox);
				oil.setGpsTime(charGpsTime);
				oil.setStatus(currStatus);
				
				//金旅建议：当前油量为20L以下的点过滤掉
				//4月17日文件：过滤掉0值、负值、小于10升的值、大于1200升的值、FF值
				if (/*"S".equals(currStatus)||("R".equals(currStatus)&&*/oilBox>=100 && oilBox <= 12000 /*&& oilBox != 32760*/){
					//oilMonitorBean2.addTrackPoint2(currStatus,oil);
					//需求调整：要求只把结果油耗数据展示即可，不进行算法过滤，直接展示原始点
					oilMonitorBean.addTrackPoint3(currStatus,oil);
				}
			}
		}catch (Exception e) {
			ret =-1;
			logger.debug("----------------油耗数据分析错误！");
		}
		
		return ret;

	}
	
	/**
	 * 根据gps时间将读取的轨迹文件数据进行排序
	 */
	private TreeMap<Long, String> getTrackMap(File file) {
		
		TreeMap<Long, String> returnTrackMap = new TreeMap<Long, String>();
//				String readLine = null;
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
					long speed = 0;
					if(StringUtils.isNumeric(track[19]) && StringUtils.isNumeric(track[24]) && track[24].equals("0")){ // 解析速度
						speed = Long.parseLong(track[19]);
					}else{
//							速度来源是GPS
						if(StringUtils.isNumeric(track[3]) ){
							speed = Long.parseLong(track[3]);
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
		
		} catch (Exception e) {
			logger.error("油箱油量监控任务，读取轨迹文件信息出错!"+file.getName()+":"+track,e);
		}


		return returnTrackMap;
		
	}
	
	/****
	 * 存储文件油量变化记录
	 */
	public static void writeOilChangedMassFile(String vid,File file,OilMonitorBean oilMonitorBean){
		if(oilMonitorBean!=null&&oilMonitorBean.getOilMonitor_ls().size()>0 ){
			//logger.info("需要生成文件名："+vid+".txt---------"+oilMonitorBean.getOilMonitor_ls().size());
			FileWriter fw = null;
			StringBuffer buf = new StringBuffer();
			try {
				fw = new FileWriter(file);
				List<OilmassChangedDetail> ls = oilMonitorBean.getOilMonitor_ls();
				for (int i=0;i<ls.size();i++){
					OilmassChangedDetail oil = ls.get(i);
					//" + oil.getChangeType() + "
					long tmpOil = Math.round(oil.getCurr_oilmass()*2);
					//if (tmpOil!=lastOil){
					buf.append(oil.getLat() + ":" + oil.getLon() + ":" + oil.getElevation() + ":" + oil.getGps_speed() + ":" + oil.getDirection() + ":" + oil.getGpsTime().replaceAll("/", "").substring(2) + ":"+oil.getChangeType()+"::" + Math.round(oil.getChange_oilmass()*2) + ":" + tmpOil + "\r\n");
					//}
				}// End while
				fw.write(buf.toString());
				fw.flush();
				buildOilFileCount++;
				logger.info("[轨迹文件]-------------->>[油量文件]成功！文件名:[{}],行数:[{}]",file.getName(),oilMonitorBean.getOilMonitor_ls().size());
			} catch (IOException e) {
				logger.error("存储文件油量变化记录VID=" + vid,e);
			}finally{
				if(null != fw){
					try {
						fw.close();
					} catch (IOException e) {
						logger.error(e.getMessage(),e);
					}
				}
			}
			
		}
	}
	/**
	 * 判断车辆转速是否处于行车状态
	 * @param rpm
	 * @return
	 */
	public boolean checkRpm(Long rpm){
		if (rpm < 0){
			return true;
		}else if (rpm * 0.125 > 800 ){
			return true;
		}else{
			return false;
		}
	}
	private void closeThread() {	
		for(AbstractThread t : ThreadPool.getOilWearUpdatePool().values()){
			t.close();
		}
		for(AbstractThread t : ThreadPool.getOilWearAnalyPool().values()){
			t.close();
		}
	}

	@Override
	public void isTimeRun() throws Exception {
		// TODO Auto-generated method stub
		
	}

	
}
