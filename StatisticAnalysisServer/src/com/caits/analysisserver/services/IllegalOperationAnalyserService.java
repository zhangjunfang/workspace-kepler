package com.caits.analysisserver.services;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.TreeMap;
import java.util.UUID;
import java.util.Vector;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;
import oracle.jdbc.OracleResultSet;

import com.caits.analysisserver.bean.AlarmCacheBean;
import com.caits.analysisserver.bean.DataBean;
import com.caits.analysisserver.bean.ExcConstants;
import com.caits.analysisserver.bean.IllegalOptionsAlarmBean;
import com.caits.analysisserver.bean.VehicleAlarm;
import com.caits.analysisserver.bean.VehicleAlarmEvent;
import com.caits.analysisserver.bean.VehicleInfo;
import com.caits.analysisserver.bean.VehicleMessageBean;
import com.caits.analysisserver.bean.VehicleStatus;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.DBAdapter;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.OracleDBAdapter;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.MathUtils;
import com.caits.analysisserver.utils.Utils;
import com.ctfo.generator.pk.GeneratorPK;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： AlarmAnalyserService <br>
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
 * @since JDK1.6 @ Description: 用于统计车辆报警信息
 */
@SuppressWarnings("unused")
public class IllegalOperationAnalyserService {
	private static final Logger logger = LoggerFactory.getLogger(IllegalOperationAnalyserService.class);

	private Vector<AlarmCacheBean> alarmList = new Vector<AlarmCacheBean>();

	// 报警map 缓存 key=vId_areaId
	private Map<String, AlarmCacheBean> alarmMap = new ConcurrentHashMap<String, AlarmCacheBean>();

	private OracleConnection dbCon = null;

	private String vid = ""; // 当前车辆编号

	private long utc = 0;

	private VehicleMessageBean lastLocBean = null;

	private IllegalOptionsAlarmBean ioabean = null;
	
	private String queryIllegalOptionsSQL;

	public IllegalOperationAnalyserService(OracleConnection dbCon, String vid, long utc) {
		this.vid = vid;
		this.utc = utc;
		this.dbCon = dbCon;
		initAnalyser();
	}

	/**
	 * 初始化报警统计线程
	 * 
	 * @param nodeName
	 * @throws Exception
	 */
	public void initAnalyser() {
		//查询车辆非法运营配置信息
		queryIllegalOptionsSQL = SQLPool.getinstance().getSql(
				"sql_queryIllegealOperationsAlarm");

		// 查询车辆非法营运 配置信息
		ioabean = queryIllegalOptionsAlarm(this.vid);
	}

	public void executeAnalyser(VehicleMessageBean trackBean, boolean isLastRow) {
		try {
			analyseIllegalOperations(trackBean,isLastRow);
			lastLocBean = trackBean;
		} catch (Exception ex) {
			logger.debug("VID:" + vid + " 夜间非法运营分析过程中出错！", ex);
		}
	}

	/**
	 * 返回非法营运告警列表
	 * @return
	 */
	public Vector<AlarmCacheBean> getAlarmList() {
		return alarmList;
	}

	/**
	 * 非法运营软报警分析处理
	 * 
	 * @param vehicleMessage
	 * 
	 */
	public void analyseIllegalOperations(VehicleMessageBean vehicleMessage,boolean lastrow) {
			AlarmCacheBean illeOptCacheBean = null;// 非法运营缓存
			String vid = vehicleMessage.getVid();
			String cacheKey = vid + "_FFYY";
			
			if (ioabean != null) {//当此车有配置信息时才判断非法运营
					long currentTime = vehicleMessage.getUtc();
				//非法运营报警：服务器时间在判定时间范围内，点火状态为开，当前车速大于5Km/h,原始车速大于等于50
				if (checkTime(currentTime,ioabean.getStartTime(),ioabean.getEndTime())&&!lastrow){//符合非法运营时间区间条件
					
						String binaryStr = MathUtils.getBinaryString(vehicleMessage.getBaseStatus());
						
						if (MathUtils.check("0", binaryStr)&&vehicleMessage.getSpeed()>=50){ //符合非法运营状态条件
							illeOptCacheBean = alarmMap.get(cacheKey);//取得车辆对应缓存对象
							if (illeOptCacheBean != null){
								illeOptCacheBean.setCount(0);
								//判断是否符合非法运营时间条件，初始时间到当前时间大于等于报警判断时间
								if (!illeOptCacheBean.isSaved()&&(currentTime - illeOptCacheBean.getBegintime()) >= ioabean.getDeferred()*60*1000){
									//符合报警触发条件，保存实时报警数据
									illeOptCacheBean.setBegintime(currentTime);
									illeOptCacheBean.setBeginVmb(vehicleMessage);
									illeOptCacheBean.setSaved(true);
								}else{
									//不符合触发报警条件，继续进行计算
									if (currentTime > illeOptCacheBean.getUtc()){
										illeOptCacheBean.setUtc(currentTime);
									}
								}
								//缓存结束时数据对象
								setMileageOil(vehicleMessage,illeOptCacheBean);
								
							}else{
								illeOptCacheBean=new AlarmCacheBean();
								illeOptCacheBean.setUtc(currentTime);
								illeOptCacheBean.setBegintime(currentTime);
								illeOptCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
								illeOptCacheBean.setAvgSpeed(vehicleMessage.getSpeed());
								illeOptCacheBean.setBeginVmb(vehicleMessage);
								illeOptCacheBean.setAlarmcode("110");
								illeOptCacheBean.setAlarmlevel("A001");
								illeOptCacheBean.setAlarmSrc(2);
								illeOptCacheBean.setEndVmb(vehicleMessage);
								illeOptCacheBean.setAlarmId(UUID.randomUUID().toString().replace("-", ""));
								alarmMap.put(cacheKey,illeOptCacheBean);
								//logger.debug("【非法运营】缓存非法运营车辆对象--添加"+vehicleMessage.getCommanddr());
							}
						}else{
							//不符合非法运营状态条件，则结束原非法运营，清除缓存对象
							illeOptCacheBean = alarmMap.get(cacheKey);//取得车辆对应缓存对象
							if (illeOptCacheBean!=null){
								if (illeOptCacheBean.getCount()==0){
									illeOptCacheBean.setUtc(currentTime);
									
									long endTime = getEndTime(currentTime,ioabean.getStartTime(),ioabean.getEndTime());
									if (currentTime>endTime&&endTime>0){
										illeOptCacheBean.setEndTime(endTime);
										vehicleMessage.setUtc(endTime);
									}else{
										illeOptCacheBean.setEndTime(currentTime);
									}
									//illeOptCacheBean.setEndTime2(currentTime);
									//缓存结束时数据对象
									setMileageOil(vehicleMessage,illeOptCacheBean);
								}
								
								if (illeOptCacheBean.getCount()>=2&&illeOptCacheBean.isSaved()){
									//更新非法运营软报警时长等信息，清楚此车的非法运营信息
									alarmList.add(illeOptCacheBean);
									alarmMap.remove(cacheKey);
									
									illeOptCacheBean.setCount(0);
								}else{
									illeOptCacheBean.setCount(illeOptCacheBean.getCount()+1);
								}
							}
						}
					}else{
						//不符合非法运营状态条件，则结束原非法运营，清除缓存对象
						illeOptCacheBean = alarmMap.get(cacheKey);//取得车辆对应缓存对象
						if (illeOptCacheBean!=null&&illeOptCacheBean.isSaved()){
							illeOptCacheBean.setUtc(currentTime);
							
							long endTime = getEndTime(currentTime,ioabean.getStartTime(),ioabean.getEndTime());
							if (currentTime>endTime&&endTime>0){
								illeOptCacheBean.setEndTime(endTime);
								vehicleMessage.setUtc(endTime);
							}else{
								illeOptCacheBean.setEndTime(currentTime);
							}

							//更新非法运营软报警时长等信息，清楚此车的非法运营信息
							setMileageOil(vehicleMessage,illeOptCacheBean);
							
							alarmList.add(illeOptCacheBean);
							alarmMap.remove(cacheKey);
						}
						
						alarmMap.remove(cacheKey);
					}
				} 
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
	 
		private long getEndTime(long currentTime,String beginTime,String endTime){
			
			String currDay = CDate.getStringDateShort();
			
			long fromTime = CDate.getCurrentDayYearMonthDay()+CDate.TimeToUTC(beginTime)*1000;
			long toTime = CDate.getCurrentDayYearMonthDay()+CDate.TimeToUTC(endTime)*1000;

			return toTime;
		}
	 
		private void setMileageOil(VehicleMessageBean vehicleMessage,AlarmCacheBean illeOptCacheBean){
			//缓存最大速度
			if (illeOptCacheBean.getMaxSpeed()<vehicleMessage.getSpeed()){
				illeOptCacheBean.setMaxSpeed(vehicleMessage.getSpeed());
			}
			
			//缓存平均车速
			illeOptCacheBean.setAvgSpeed(vehicleMessage.getSpeed());
			
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
			
			//if (illeOptCacheBean.getEndTime()>=vehicleMessage.getUtc()){
				illeOptCacheBean.setEndVmb(vehicleMessage);
			//}
			
		}
		
		/**
		 * 查询非法运营配置信息
		 */
		private IllegalOptionsAlarmBean queryIllegalOptionsAlarm(String tmpvid) {
			OraclePreparedStatement stQueryIllegalOptionsAlarm= null;
			OracleResultSet rs = null;
			IllegalOptionsAlarmBean illegalOptAlarmBean = null;
			try {
				stQueryIllegalOptionsAlarm = (OraclePreparedStatement) dbCon.prepareStatement(queryIllegalOptionsSQL);
				stQueryIllegalOptionsAlarm.setString(1, tmpvid);
				rs = (OracleResultSet) stQueryIllegalOptionsAlarm.executeQuery();
				while (rs.next()) {
					String vid=rs.getString("VID");
					illegalOptAlarmBean = new IllegalOptionsAlarmBean();
					illegalOptAlarmBean.setVid(vid);//车辆ID
					illegalOptAlarmBean.setEntId(rs.getString("ENT_ID"));//所属企业
					illegalOptAlarmBean.setStartTime(rs.getString("START_TIME"));//开始时间
					illegalOptAlarmBean.setEndTime(rs.getString("END_TIME"));//结束时间
					illegalOptAlarmBean.setDeferred(rs.getLong("DEFERRED"));//持续时间
					illegalOptAlarmBean.setIsDefault(rs.getString("ISDEFAULT"));//是否默认配置（1、是）	
				}// End while
			} catch (Exception e) {
				logger.error("查询到非法运营软报警配置车辆总数-ERROR-数据库异常"+e.getMessage(),e);
				e.printStackTrace();
				return null;
			} finally {
				try{
				if (rs != null) {
					rs.close();
				}
				if (stQueryIllegalOptionsAlarm != null) {
					stQueryIllegalOptionsAlarm.close();
				}
				}catch(Exception ex){
					logger.error("关闭Statement出错！");
				}
			}
			return illegalOptAlarmBean;
		}

}
