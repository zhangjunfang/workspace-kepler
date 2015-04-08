package com.caits.analysisserver.repair;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.sql.Connection;
import java.sql.SQLException;
import java.util.Calendar;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;
import java.util.TreeMap;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.IllegalOptionsAlarmBean;
import com.caits.analysisserver.bean.IllegalOptionsCacheBean;
import com.caits.analysisserver.bean.VehicleMessageBean;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.TmpOracleDBAdapter;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.MathUtils;
/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： AnalysisStatusThread <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2011-10-18</td>
 * <td>刘志伟</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author 刘志伟
 * @since JDK1.6
 * @ Description: 用于统计车辆相关信息
 */
@SuppressWarnings("unused")
public class IllegalOperationsRepairThread extends Thread {
	
	private static final Logger logger = LoggerFactory.getLogger(IllegalOperationsRepairThread.class);

	private String vid ;
	
	private Connection dbCon = null;
	
	private File trackFile = null;
	
	// 报警map 缓存 key=vId_areaId
	private Map<String, IllegalOptionsCacheBean> alarmMap = new ConcurrentHashMap<String, IllegalOptionsCacheBean>();
	
	public IllegalOperationsRepairThread(File f){
		this.trackFile = f;
		initAnalyser();
	}
	
	public void initAnalyser(){
		
	}
	
	public void run(){
			try {
				logger.info( "夜间非法运营信息补录:" + trackFile.getName());
				vid = trackFile.getName().replaceAll("\\.txt", "");
				
				//从连接池获取连接
				dbCon = OracleConnectionPool.getConnection();
				
				statisticStatus(); //开始统计
			} catch (Exception e) {
				logger.error("夜间非法运营信息补录出错：VID："+vid,e);
			}finally{
				if(dbCon != null){
					try {
						dbCon.close();
					} catch (SQLException e) {
						logger.error("连接放回连接池出错.",e);
					}
				}
			}
	}

	private void statisticStatus() throws SQLException, IOException{
		if(TmpOracleDBAdapter.illeOptAlarmMap.get(vid) == null){ // 找不到该车对应的非法运营信息
			return;
		}
		long start = System.currentTimeMillis();

		readTrackFile();

		logger.info("VID = " + vid +  "; 非法运营数据补录用时 : " + (System.currentTimeMillis() - start)/1000);
	}

	/**
	 * 读取轨迹文件
	 * @param file
	 * @throws SQLException 
	 * @throws IOException 
	 */

	private void readTrackFile() throws IOException{

		BufferedReader buf = null;
		TreeMap<Long, String> statusMap = new TreeMap<Long, String>();
		int rowCount = 0;
		int currentRow =0;
		VehicleMessageBean messageBean = null;
		try{
			buf = new BufferedReader(new FileReader(this.trackFile));
			statusMap = getTrackMap(buf);
			rowCount = statusMap.size();
			Set<Long> key = statusMap.keySet();
			Long keys = null;		
			String[] cols = null;
			
			for (Iterator<Long> it = key.iterator(); it.hasNext();) {
				keys = (Long) it.next();
				try{
						cols = statusMap.get(keys).split(":");
						//分析轨迹文件生成起步停车数据和行车数据
						currentRow++;
						messageBean = null;//changTxtVMB(cols);

						analyseIllegalOperations(messageBean,currentRow==rowCount);

				}catch(Exception ex){
					ex.printStackTrace();
					logger.error("文件:" + trackFile.getAbsolutePath() ,ex);
				}
			}// End for

		}finally{
	
			closeAllAlarm(messageBean);
			messageBean = null;
			if(statusMap != null && statusMap.size() > 0){
				statusMap.clear();
			}
			if(buf != null){
				buf.close();
			}
		}
	}

	/**
	 * 根据gps时间将读取的轨迹文件数据进行排序
	 */
	private TreeMap<Long, String> getTrackMap(BufferedReader buf) {
		
		TreeMap<Long, String> returnTrackMap = new TreeMap<Long, String>();
		String readLine = null;
		String gpsdate = null;
		String[] track = null;
		Long gpstime = null;
		try {
			
			while ((readLine = buf.readLine()) != null) {
				
				// 轨迹文件每行的数据分割
				track = readLine.split(":");
				
				if(track.length >1){
					
					gpsdate = track[2];
					// 将gpsdate转换成utc格式
					gpstime = CDate.stringConvertUtc(gpsdate);
					//只填充凌晨2--5点之间的数据
					Calendar cal = Calendar.getInstance();
					cal.setTimeInMillis(gpstime);
					int hour = cal.get(Calendar.HOUR_OF_DAY);
					
					//if (hour>=2 && hour <5){
						returnTrackMap.put(gpstime, readLine);	
					//}
				}		
			}// End while		
		
		} catch (Exception e) {
			logger.error("读取轨迹文件信息出错",e);
		}finally{
			
			if(buf != null){
				try {
					buf.close();
				} catch (IOException e) {
					logger.error(e.getMessage(), e);
				}
			}
		}

		return returnTrackMap;
		
	}

	public void analyseIllegalOperations(VehicleMessageBean vehicleMessage,boolean lastrow) {
		IllegalOptionsCacheBean illeOptCacheBean = null;// 非法运营缓存
		
		IllegalOptionsAlarmBean ioabean = TmpOracleDBAdapter.illeOptAlarmMap.get(vid);// 根据vid查询车辆当前的非法运营软报警配置
		if (ioabean != null) {//当此车有配置信息时才判断非法运营
				vehicleMessage.setVid(vid);
				long currentTime = vehicleMessage.getUtc();
				
				//判断当前记录的时间是否在非法营运判定时间之内
				if (checkTime(currentTime,ioabean.getStartTime(),ioabean.getEndTime())){
					String binaryStr = MathUtils.getBinaryString(vehicleMessage.getBaseStatus());
					
					if ((MathUtils.check("0", binaryStr)&&vehicleMessage.getSpeed()>=50)||lastrow){ //符合非法运营状态条件
						illeOptCacheBean = alarmMap.get(""+vid);//取得车辆对应缓存对象
						if (illeOptCacheBean != null){
							illeOptCacheBean.setCount(0);
							//判断是否符合非法运营时间条件，初始时间到当前时间大于等于报警判断时间
							if (!illeOptCacheBean.getIsTriggerAlarm()&&(currentTime - illeOptCacheBean.getInitTime()) >= ioabean.getDeferred()*60*1000){
								//符合报警触发条件，保存实时报警数据
								illeOptCacheBean.setBegintime(currentTime);
								dealSaveIlleOptalarm(vehicleMessage,illeOptCacheBean);
								illeOptCacheBean.setIsTriggerAlarm(true);
							}else{
								//不符合触发报警条件，继续进行计算
								if (currentTime > illeOptCacheBean.getUtc()){
									illeOptCacheBean.setUtc(currentTime);
								}
								logger.debug("【非法运营】缓存非法运营车辆对象--更新"+vehicleMessage.getCommanddr());
							}
							
							//缓存结束时数据对象
							setMileageOil(vehicleMessage,illeOptCacheBean);
	
						}else{
							illeOptCacheBean=new IllegalOptionsCacheBean();
							illeOptCacheBean.setUtc(currentTime);
							illeOptCacheBean.setInitTime(currentTime);
							illeOptCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
							illeOptCacheBean.setBeginVmb(vehicleMessage);
							illeOptCacheBean.setEndVmb(vehicleMessage);
							illeOptCacheBean.setAlarmcode("110");
							illeOptCacheBean.setAlarmlevel("A001");
							illeOptCacheBean.setAlarmSrc(2);
							alarmMap.put(""+vid,illeOptCacheBean);
							logger.debug("【非法运营】缓存非法运营车辆对象--添加"+vehicleMessage.getCommanddr());
						}
					}else{
						//不符合非法运营状态条件，则结束原非法运营，清除缓存对象
 						illeOptCacheBean = alarmMap.get(""+vid);//取得车辆对应缓存对象
						if (illeOptCacheBean!=null){
							if (illeOptCacheBean.getCount()==0){
								illeOptCacheBean.setUtc(currentTime);
								
								long endTime = getEndTime(currentTime,ioabean.getStartTime(),ioabean.getEndTime());
								if (currentTime>endTime&&endTime>0){
									illeOptCacheBean.setEndTime(endTime);
								}else{
									illeOptCacheBean.setEndTime(currentTime);
								}
								
								//缓存结束时数据对象
								setMileageOil(vehicleMessage,illeOptCacheBean);
							}
							
							if (illeOptCacheBean.getCount()>=2){
								//更新非法运营软报警时长等信息，清除此车的非法运营信息
								dealUpdateIlleOptalarm(vehicleMessage,illeOptCacheBean);
								alarmMap.remove(""+vid);
								illeOptCacheBean.setCount(0);
							}else{
								illeOptCacheBean.setCount(illeOptCacheBean.getCount()+1);
							}
						}
					}
				}else{
					//logger.info("不符合时间区间条件 --"+vehicleMessage.getCommanddr());
					//不符合非法运营状态条件，则结束原非法运营，清除缓存对象
					illeOptCacheBean = alarmMap.get(""+vid);//取得车辆对应缓存对象
					if (illeOptCacheBean!=null){
						illeOptCacheBean.setUtc(currentTime);
						
						long endTime = getEndTime(currentTime,ioabean.getStartTime(),ioabean.getEndTime());
						if (currentTime>endTime&&endTime>0){
							illeOptCacheBean.setEndTime(endTime);
						}else{
							illeOptCacheBean.setEndTime(currentTime);
						}

						//更新非法运营软报警时长等信息，清楚此车的非法运营信息
						dealUpdateIlleOptalarm(vehicleMessage,illeOptCacheBean);
						alarmMap.remove(""+vid);
					}
				}

					
		} 
		}
	
	private void closeAllAlarm(VehicleMessageBean vehicleMessage){
		try{
			if (alarmMap.size()>0){
				Set<String> key = alarmMap.keySet();
				String keys = null;
				for (Iterator<String> it = key.iterator(); it.hasNext();) {
					keys = (String) it.next();
					IllegalOptionsCacheBean illeOptCacheBean = alarmMap.get(keys);
					if (illeOptCacheBean!=null){
						illeOptCacheBean.setEndTime(vehicleMessage.getUtc());
						setMileageOil(vehicleMessage,illeOptCacheBean);
						dealUpdateIlleOptalarm(vehicleMessage,illeOptCacheBean);
					}
				
				}
				alarmMap.clear();
			}
		}catch(Exception ex){
			logger.error("结束剩余告警出错！vid="+vehicleMessage.getVid(),ex);
		}
	}
	
	private long getEndTime(long currentTime,String beginTime,String endTime){
		
		String currDay = CDate.getStringDateShort();
		
		long fromTime = CDate.setOnedayHMS(currentTime,0,0,0)+TimeToUTC(beginTime)*1000;
		long toTime = CDate.setOnedayHMS(currentTime,0,0,0)+TimeToUTC(endTime)*1000;

		return toTime;
	}
	
	private void setMileageOil(VehicleMessageBean vehicleMessage,IllegalOptionsCacheBean illeOptCacheBean){
		//缓存最大速度
		if (illeOptCacheBean.getMaxSpeed()<vehicleMessage.getSpeed()){
			illeOptCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
		}
		//计算里程、油耗信息
		if (vehicleMessage.getOil()>0&&illeOptCacheBean.getEndVmb().getOil()>0){
			long costOil = vehicleMessage.getOil() - illeOptCacheBean.getEndVmb().getOil();
			if (costOil>0){
				illeOptCacheBean.setOil(illeOptCacheBean.getOil() + costOil);
			}
		}
		if (vehicleMessage.getMetOil()>0&&illeOptCacheBean.getEndVmb().getMetOil()>0){
			long costMetOil = vehicleMessage.getMetOil() -illeOptCacheBean.getEndVmb().getMetOil();
			if (costMetOil>0){
				illeOptCacheBean.setMetOil(illeOptCacheBean.getMetOil()+costMetOil);
			}
		}
		
		if (vehicleMessage.getMileage()>0&&illeOptCacheBean.getEndVmb().getMileage()>0){
			long costMileage = vehicleMessage.getMileage()-illeOptCacheBean.getEndVmb().getMileage();
			if (costMileage>0){
				illeOptCacheBean.setMileage(illeOptCacheBean.getMileage()+costMileage);
			}
		}
		
		if (illeOptCacheBean.getEndTime()>=vehicleMessage.getUtc()){
			illeOptCacheBean.setEndVmb(vehicleMessage);
		}
		
	}
	
	/**
	 * 判断存储非法运营报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	public void dealSaveIlleOptalarm(VehicleMessageBean vehicleMessage,
			 IllegalOptionsCacheBean illeOptAlarmCacheBean) {
			logger.debug("【非法运营】保存实时告警信息:"+vehicleMessage.getCommanddr());
			saveIlleOptAlarm(vehicleMessage, illeOptAlarmCacheBean);
	}

	/**
	 * 判断更新非法运营报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmBean
	 * @param areaAlarmCacheBean
	 * 
	 */
	public void dealUpdateIlleOptalarm(VehicleMessageBean vehicleMessage,
			 IllegalOptionsCacheBean illeOptAlarmCacheBean) {
			logger.debug("【非法运营】更新实时告警信息并保存告警事件:"+vehicleMessage.getCommanddr()+" 结束时间："+illeOptAlarmCacheBean.getEndTime());
			updateIlleOptAlarm(vehicleMessage, illeOptAlarmCacheBean);
			saveIlleOptAlarmEvent(vehicleMessage, illeOptAlarmCacheBean);
	}
	
	
	/**
	 * 存储非法运营软报警
	 * 
	 * @param vehicleMessage
	 * 
	 */
	private void saveIlleOptAlarm(VehicleMessageBean vehicleMessage,
			IllegalOptionsCacheBean illeOPptAlarmBean) {
		
			vehicleMessage.setAlarmid("500000" + vehicleMessage.getVid()
					+ "110"
					+ illeOPptAlarmBean.getBegintime());
			vehicleMessage.setUtc(illeOPptAlarmBean.getBegintime());
			vehicleMessage.setAlarmcode("110");
			vehicleMessage.setBglevel("A001");
			vehicleMessage.setAlarmadd("2");
			vehicleMessage.setAlarmAddInfo("非法运营报警");
			try {
				TmpOracleDBAdapter.saveVehicleAlarm(vehicleMessage);
				logger.debug("【非法运营软报警】【存储】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ vehicleMessage.getAlarmid() + ";AlarmAddInfo:"
						+ vehicleMessage.getAlarmAddInfo());
			} catch (Exception e) {
				logger.error("名称：非法运营软报警---数据库异常", e);
			}
	}

	/**
	 * 更新非法运营软报警
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void updateIlleOptAlarm(VehicleMessageBean vehicleMessage,
			IllegalOptionsCacheBean illeOptAlarmCache) {
			try {
				vehicleMessage.setAlarmid("500000"
						+ vehicleMessage.getVid() 
						+ "110" + illeOptAlarmCache.getBegintime());
				if (illeOptAlarmCache.getEndTime().equals(new Long(0))) {
					System.out.println("aaaa");
				}
				vehicleMessage.setUtc(illeOptAlarmCache.getEndTime());
				TmpOracleDBAdapter.updateVehicleAlarm(vehicleMessage);
				logger.debug("【非法运营软报警】【更新】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ vehicleMessage.getAlarmid());
			} catch (Exception e) {
				logger.error("Alarmid:" + vehicleMessage.getAlarmid()
						+ "---更新非法运营软报警-数据库异常", e);
			}
		}
	
	/**
	 * 存储驾驶行为事件数据
	 * 
	 * @param vehicleMessage
	 * @param areaAlarmCache
	 * 
	 */
	private void saveIlleOptAlarmEvent(VehicleMessageBean vehicleMessage,
			IllegalOptionsCacheBean illeOptAlarmCache) {
			try {
				vehicleMessage.setAlarmid("500000"
						+ vehicleMessage.getVid() 
						+ "110" + illeOptAlarmCache.getBegintime());
				vehicleMessage.setUtc(illeOptAlarmCache.getEndTime());
				TmpOracleDBAdapter.saveVehicleAlarmEvent(vehicleMessage,illeOptAlarmCache);
				logger.debug("【非法运营软报警驾驶行为事件】【添加】成功["
						+ vehicleMessage.getCommanddr() + "]Alarmid:"
						+ vehicleMessage.getAlarmid());
			} catch (Exception e) {
				logger.error("Alarmid:" + vehicleMessage.getAlarmid()
						+ "---添加非法运营软报警驾驶行为事件-数据库异常", e);
			}
		}
	
	public long TimeToUTC(String time){
    	String[] s=time.split(":");
    	long utc=0;
    	if (s.length==1){
    		utc=Integer.parseInt(s[0])*3600;
    	}else if (s.length==2){
    		utc=Integer.parseInt(s[0])*3600+Integer.parseInt(s[1])*60;
    	}else if (s.length==3){
    		utc=Integer.parseInt(s[0])*3600+Integer.parseInt(s[1])*60+Integer.parseInt(s[2]);
    	}

    	return utc;
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
	 * 判断当前时间是否在非法运营时间段内
	 * 时间段肯定在一天之内，不存在跨天情况
	 * @param currentTime
	 * @param beginTime
	 * @param endTime
	 * @return
	 */
	 private boolean checkTime(long currentTime,String beginTime,String endTime){
			boolean flag = false;
			
			String currTime = CDate.utc2Str(currentTime, "HH:mm:ss");
			
			if (java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(endTime))&&java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(beginTime))){
				flag=true;
			}

			return flag;
		}
}

