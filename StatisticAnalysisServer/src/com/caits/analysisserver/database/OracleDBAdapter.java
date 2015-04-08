package com.caits.analysisserver.database;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.UUID;
import java.util.Vector;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.AirTempertureBean;
import com.caits.analysisserver.bean.AlarmCacheBean;
import com.caits.analysisserver.bean.CoolLiquidtemBean;
import com.caits.analysisserver.bean.DriverDetailBean;
import com.caits.analysisserver.bean.ExcConstants;
import com.caits.analysisserver.bean.GasPressureBean;
import com.caits.analysisserver.bean.OilMonitorBean2;
import com.caits.analysisserver.bean.OilPressureBean;
import com.caits.analysisserver.bean.OilmassChangedDetail;
import com.caits.analysisserver.bean.RotateSpeedDay;
import com.caits.analysisserver.bean.RunningBean;
import com.caits.analysisserver.bean.SpeeddistDay;
import com.caits.analysisserver.bean.StopstartBean;
import com.caits.analysisserver.bean.ThVehicleSpeedAnomalous;
import com.caits.analysisserver.bean.VehicleInfo;
import com.caits.analysisserver.bean.VehicleMessageBean;
import com.caits.analysisserver.bean.VehicleStatus;
import com.caits.analysisserver.bean.VoltagedistDay;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.ExceptionUtil;
import com.caits.analysisserver.utils.GetAddressUtil;
import com.ctfo.generator.pk.GeneratorPK;
/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： OracleDBAdapter <br>
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
 * <td>2014-01-15</td>
 * <td>yujch</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author yujch
 * @since JDK1.6
 * @ Description: 日统计数据库类
 */
@SuppressWarnings("unused")
public class OracleDBAdapter  {
	
	private static final Logger logger = LoggerFactory.getLogger(OracleDBAdapter.class);
	
	public void initAnalyser() throws SQLException{
	
	}
	
	/**
	 * 保存当前车辆状态事件
	 * @param file
	 * @throws IOException
	 * @throws NumberFormatException
	 */
	public static void saveStateEventInfo(String vid,Vector<AlarmCacheBean> stateEventList) {
		OracleConnection dbCon = null;
		PreparedStatement stSaveDriverEventInfo = null;
		String tmpkey = "";
		try{
			if (stateEventList != null&&stateEventList.size()>0) {
				logger.info("-----save state event start----get dbcon start ");
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				logger.info("-----save state event start----get dbcon end "+dbCon);
				if (dbCon!=null){
				stSaveDriverEventInfo = (OraclePreparedStatement)dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveStateEventInfo"));
				
				VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
				
				String vehicle_no = info.getVehicleNo();
				String corp_id = info.getEntId();
				String corp_name = info.getEntName();
				String team_id = info.getTeamId();
				String team_name = info.getTeamName();
				String vline_id = info.getVlineId();
				String vline_name = info.getLineName();
				
				Iterator<AlarmCacheBean> eventList = stateEventList.iterator();
				int count = 0;
				while(eventList.hasNext()){
					AlarmCacheBean alarmCacheBean = eventList.next();

						VehicleMessageBean beginVmb = alarmCacheBean.getBeginVmb();
						VehicleMessageBean endVmb = alarmCacheBean.getEndVmb();
						String id = GeneratorPK.instance().getPKString();
						tmpkey += ":" +id;
						stSaveDriverEventInfo.setString(1, id);
						stSaveDriverEventInfo.setString(2, vid);
						stSaveDriverEventInfo.setString(3, vehicle_no);
						stSaveDriverEventInfo.setString(4, corp_id);
						stSaveDriverEventInfo.setString(5, corp_name);
						
						stSaveDriverEventInfo.setString(6, team_id);
						stSaveDriverEventInfo.setString(7, team_name);
						
						if(vline_id != null && !"".equals(vline_id)){
							stSaveDriverEventInfo.setString(8, vline_id);
						}else{
							stSaveDriverEventInfo.setNull(8, Types.VARCHAR);
						}
						
						stSaveDriverEventInfo.setString(9, vline_name);
						
						stSaveDriverEventInfo.setString(10, alarmCacheBean.getAlarmcode());
						stSaveDriverEventInfo.setLong(11, beginVmb.getUtc());
						stSaveDriverEventInfo.setLong(12, beginVmb.getLat());
						stSaveDriverEventInfo.setLong(13, beginVmb.getLon());
						stSaveDriverEventInfo.setLong(14, beginVmb.getMaplon());
						stSaveDriverEventInfo.setLong(15, beginVmb.getMaplat());
						stSaveDriverEventInfo.setLong(16, beginVmb.getElevation());
						stSaveDriverEventInfo.setLong(17, beginVmb.getDir());
						stSaveDriverEventInfo.setLong(18, beginVmb.getSpeed());
						
						stSaveDriverEventInfo.setLong(19, endVmb.getUtc());
						stSaveDriverEventInfo.setLong(20, endVmb.getLat());
						stSaveDriverEventInfo.setLong(21, endVmb.getLon());
						stSaveDriverEventInfo.setLong(22, endVmb.getMaplon());
						stSaveDriverEventInfo.setLong(23, endVmb.getMaplat());
						stSaveDriverEventInfo.setLong(24, endVmb.getElevation());
						stSaveDriverEventInfo.setLong(25, endVmb.getDir());
						stSaveDriverEventInfo.setLong(26, endVmb.getSpeed());
						
						long use_time = (endVmb.getUtc() - beginVmb.getUtc())/1000;
						
						if (use_time>0){
							stSaveDriverEventInfo.setLong(27, use_time);
						}else{
							stSaveDriverEventInfo.setNull(27, Types.INTEGER);
						}
						
						if (alarmCacheBean.getMaxSpeed()>0){
							stSaveDriverEventInfo.setLong(28, alarmCacheBean.getMaxSpeed());
						}else{
							stSaveDriverEventInfo.setNull(28, Types.INTEGER);
						}
						
						if (alarmCacheBean.getMileage()>0){
							stSaveDriverEventInfo.setLong(29, alarmCacheBean.getMileage());
						}else{
							stSaveDriverEventInfo.setNull(29, Types.INTEGER);
						}
						
						if (alarmCacheBean.getOil()>0){
							stSaveDriverEventInfo.setLong(30, alarmCacheBean.getOil());
						}else{
							stSaveDriverEventInfo.setNull(30, Types.INTEGER);
						}
						
						stSaveDriverEventInfo.addBatch();
						
						count++;
						if (count>=50){
							stSaveDriverEventInfo.executeBatch();
							count=0;
							tmpkey = "";
						}
				}
				if (count>0){
					stSaveDriverEventInfo.executeBatch();
				}
			}
			}
		}catch(Exception e){
			logger.error(vid + " 存储车辆状态事件信息出错.",e);
			logger.debug(vid + "stateEventList size:"+stateEventList.size()+" keys==="+tmpkey);
		}finally{
			try{
				if (stSaveDriverEventInfo!=null){
					stSaveDriverEventInfo.close();
				}
				if (dbCon!=null){
					dbCon.close();
				}
			}catch(Exception e){
				logger.error("将连接放回连接池出错：",e);
			}
		}
	}
	
	/**
	 * 存储起步停车及行车数据
	 * @throws SQLException
	 */

	public static void saveStopstartInfo(String vid,List<StopstartBean> stopstartlist) throws SQLException{
		if(stopstartlist.size() < 1){ // 无起步停车信息
			return;
		}
		OracleConnection dbCon = null;
		PreparedStatement stopstartSt = null;
		PreparedStatement runningSt = null;
		try {
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			//提取当前车的配置方案信息
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			String cfgFlag = "0";
			if (info!=null){
				cfgFlag = info.getCfgFlag();
			}
			
			stopstartSt = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveStopstartInfo"));
			//最后一个对象冗余，因此stopstartlist.size()-2
			for (int i=0;i<stopstartlist.size();i++){
				try {
					StopstartBean ssb = stopstartlist.get(i);
					
//					long subStartMileage = -1;
//					long subEndMileage = -1;
//					long subStartOil = -1;
//					long subEndOil = -1;
					long subStartTime = -1;
					long subEndTime = -1;
					
					double ecuRunningOilSubTotal = 0;
					double metRunningOilSubTotal = 0;
					//long runningMileage = 0;
					long runningTime = 0;
					
//					long id  = CDate.getNow().getTime();
					String mainId = GeneratorPK.instance().getPKString();
					List<RunningBean> runninglist = ssb.getRunninglist();
					//排除疑似漂移的行车数据：无ECU油耗和精准油耗、无里程、有短暂车速、每个起步停车里只包含一个行车过程
					if (runninglist!=null&&runninglist.size()==1){
						RunningBean rb = runninglist.get(0);
						long ecuRunningOil = rb.getEcuRunningOil();
						long metRunningOil = rb.getMetRunningOil();
						long runningMileage = rb.getRunningMileage();

						if (ecuRunningOil<=0&&metRunningOil<=0&&runningMileage<=0){
							continue;
						}
					}
					
					if (runninglist!=null&&runninglist.size()>0){
						
						runningSt = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveRunningInfo"));
						for (int j=0;j<runninglist.size();j++){
							RunningBean rb = runninglist.get(j);
							String detailId  = GeneratorPK.instance().getPKString();
							runningSt.setString(1, detailId+""+j);
							runningSt.setString(2, mainId);
							runningSt.setString(3, vid);
							runningSt.setString(4, info.getVehicleNo());
							/*runningSt.setLong(5, AnalysisDBAdapter.queryVechileInfo(vid).getEntId());
							runningSt.setString(6, AnalysisDBAdapter.queryVechileInfo(vid).getEntName());
							runningSt.setLong(7, AnalysisDBAdapter.queryVechileInfo(vid).getTeamId());
							runningSt.setString(8, AnalysisDBAdapter.queryVechileInfo(vid).getTeamName());
							runningSt.setLong(5, AnalysisDBAdapter.queryVechileInfo(vid).getDriverId());
							runningSt.setString(6, AnalysisDBAdapter.queryVechileInfo(vid).getDriverName());
							runningSt.setLong(5, AnalysisDBAdapter.queryVechileInfo(vid).getVlineId());
							runningSt.setString(6, AnalysisDBAdapter.queryVechileInfo(vid).getEntName());*/
							long startTime = CDate.stringConvertUtc(rb.getStartTime());
							long endTime = CDate.stringConvertUtc(rb.getStopTime());
							
							runningTime +=(endTime-startTime);
							
							runningSt.setLong(5, startTime);
							runningSt.setLong(6, endTime);
							runningSt.setLong(7, rb.getBeginLon());
							runningSt.setLong(8, rb.getBeginLat());
							runningSt.setLong(9, rb.getEndLon());
							runningSt.setLong(10, rb.getEndLat());
//							long startRunningMileage = rb.getStartRunningMileage();
//							long stopRunningMileage = rb.getStopRunningMileage();
//							if (startRunningMileage<0){
//								startRunningMileage = stopRunningMileage;
//							}
//							long subTotalMileage = stopRunningMileage-startRunningMileage;
							
							runningSt.setLong(11, rb.getStartRunningMileage());
							runningSt.setLong(12, rb.getStopRunningMileage());
							runningSt.setLong(13, rb.getRunningMileage());
//							long startRunningOil = rb.getStartRunningOil();
//							long stopRunningOil = rb.getStopRunningOil();
//							if (startRunningOil<0){
//								startRunningOil = stopRunningOil;
//							}
//							long subTotalOil = stopRunningOil-startRunningOil;
							
							
							runningSt.setLong(14, rb.getStartRunningOil());
							runningSt.setLong(15, rb.getStopRunningOil());
							
							metRunningOilSubTotal +=  rb.getMetRunningOil();
							ecuRunningOilSubTotal += rb.getEcuRunningOil();
							
							if ("1".equals(cfgFlag)){
								//精准油耗时要进行单位换算 由 0.01换算为0.5  
								runningSt.setDouble(16, rb.getMetRunningOil()*0.02);
							}else{
								runningSt.setLong(16, rb.getEcuRunningOil());
							}
							
							
							runningSt.setLong(17, rb.getMaxRotateSpeed());
							runningSt.setLong(18, rb.getMaxSpeed());
							runningSt.setString(19, info.getEntId());
							runningSt.setString(20, info.getEntName());
							runningSt.setString(21, info.getTeamId());
							runningSt.setString(22, info.getTeamName());
							if(info.getVinCode() != null){
								runningSt.setString(23, info.getVinCode());
							}else{
								runningSt.setString(23, null);
							}
							
							if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
								runningSt.setString(24, info.getVlineId());
							}else{
								runningSt.setNull(24, Types.INTEGER);
							}
							
							if(info.getLineName() != null){
								runningSt.setString(25, info.getLineName());
							}else{
								runningSt.setString(25, null);
							}
							
							runningSt.setLong(26, rb.getEcuRunningOil());
							runningSt.setLong(27, rb.getMetRunningOil());
							runningSt.setString(28, cfgFlag);
							
							if (j==0){
//								subStartMileage = startRunningMileage;
//								subStartOil = startRunningOil;
								subStartTime = startTime;
							}
							if (j==runninglist.size()-1){
//								subEndMileage = stopRunningMileage;
//								subEndOil = stopRunningOil;
								subEndTime = endTime;
							}
		//不保存行车数据
							//runningSt.addBatch();
						}// Inner of end for
						//runningSt.executeBatch();
						
						if(runningSt != null){
							runningSt.close();
						}
					}
					stopstartSt.setString(1, mainId);
					stopstartSt.setString(2,vid);
					stopstartSt.setString(3, info.getVehicleNo());
					
					long launchTime = 0;
					if (ssb.getLaunchTime()==null||"".equals(ssb.getLaunchTime())){
						stopstartSt.setNull(4, Types.NULL);
					}else{
						launchTime = CDate.stringConvertUtc(ssb.getLaunchTime());
						stopstartSt.setLong(4, launchTime);
					}
					
					if (subStartTime!=-1){
						stopstartSt.setLong(5, subStartTime);
					}else{
						stopstartSt.setNull(5, Types.INTEGER);
					}
					
					if (subEndTime!=-1){
						stopstartSt.setLong(6, subEndTime);
					}else{
						stopstartSt.setNull(6, Types.INTEGER);
					}
					
					long fireoffTime = 0;
					if (ssb.getFireoffTime()==null||"".equals(ssb.getFireoffTime())){
						stopstartSt.setNull(7, Types.NULL);
					}else{
						fireoffTime = CDate.stringConvertUtc(ssb.getFireoffTime());
						stopstartSt.setLong(7, fireoffTime);
					}
					
					stopstartSt.setLong(8, ssb.getBeginLon());
					stopstartSt.setLong(9, ssb.getBeginLat());
					stopstartSt.setLong(10, ssb.getEndLon());
					stopstartSt.setLong(11, ssb.getEndLat());
//					long startMileage = ssb.getStartMileage();
//					long stopMileage = ssb.getEndMileage();
//					long totalMileage = 0;
//					if (ssb.getLaunchTime()==null||"".equals(ssb.getLaunchTime())){
//						totalMileage = runningMileage;
//					}else{
//						totalMileage=stopMileage-startMileage;
//					}
//
//					if( totalMileage< 0){
//						totalMileage = 0;
//					}
					
					stopstartSt.setLong(12, ssb.getStartMileage());
					stopstartSt.setLong(13, ssb.getEndMileage());
					stopstartSt.setLong(14, ssb.getMileage());
//					long startOil = ssb.getStartOil();
//					long stopOil = ssb.getEndOil();
//	
//					long totalOil = 0;
//					if (ssb.getLaunchTime()==null||"".equals(ssb.getLaunchTime())){
//						totalOil = 0;
//					}else{
//						if(stopOil > 0 && startOil > 0)
//						totalOil = stopOil-startOil;
//					}
//
//					if( totalOil< 0){
//						totalOil = 0;
//					}
					
					
					stopstartSt.setLong(15, ssb.getStartOil());
					stopstartSt.setLong(16, ssb.getEndOil());
					
					if ("1".equals(cfgFlag)){
						stopstartSt.setDouble(17, ssb.getMetOilWear()*0.02);
					}else{
						stopstartSt.setLong(17, ssb.getEcuOilWear());
					}
					
					stopstartSt.setLong(18, ssb.getMaxRotateSpeed());
					stopstartSt.setLong(19, ssb.getMaxSpeed());
					stopstartSt.setLong(20, runninglist.size());
					if ("1".equals(cfgFlag)){
						stopstartSt.setDouble(21, (ssb.getMetOilWear()-metRunningOilSubTotal)*0.02);
						stopstartSt.setDouble(22, metRunningOilSubTotal*0.02);
					}else{
						stopstartSt.setDouble(21, ssb.getEcuOilWear()-ecuRunningOilSubTotal);
						stopstartSt.setDouble(22, ecuRunningOilSubTotal);
					}
					
					
					//子查询语句条件
					long fromTime=launchTime;
					if (launchTime==0){
						fromTime = subStartTime;
					}
					long endTime=fireoffTime;
					if (fireoffTime==0){
						endTime=subEndTime;
					}

	/*				stopstartSt.setString(23,vid);
					stopstartSt.setLong(24, fromTime);
					stopstartSt.setLong(25, endTime);
					stopstartSt.setLong(26, fromTime);
					stopstartSt.setLong(27, endTime);
					
					stopstartSt.setString(28,vid);
					stopstartSt.setLong(29, fromTime);
					stopstartSt.setLong(30, endTime);
					stopstartSt.setLong(31, fromTime);
					stopstartSt.setLong(32, endTime);
					
					
					stopstartSt.setString(33,vid);
					stopstartSt.setLong(34, fromTime);
					stopstartSt.setLong(35, endTime);
					stopstartSt.setLong(36, fromTime);
					stopstartSt.setLong(37, endTime);
					
					stopstartSt.setString(38,vid);
					stopstartSt.setLong(39, fromTime);
					stopstartSt.setLong(40, endTime);
					stopstartSt.setLong(41, fromTime);
					stopstartSt.setLong(42, endTime);
					
					stopstartSt.setString(43,vid);
					stopstartSt.setLong(44, fromTime);
					stopstartSt.setLong(45, endTime);
					stopstartSt.setLong(46, fromTime);
					stopstartSt.setLong(47, endTime);
					
					stopstartSt.setString(48,vid);
					stopstartSt.setLong(49, fromTime);
					stopstartSt.setLong(50, endTime);
					stopstartSt.setLong(51, fromTime);
					stopstartSt.setLong(52, endTime);
					
					
					stopstartSt.setString(53,vid);
					stopstartSt.setLong(54, fromTime);
					stopstartSt.setLong(55, endTime);
					stopstartSt.setLong(56, fromTime);
					stopstartSt.setLong(57, endTime);
					
					stopstartSt.setString(58,vid);
					stopstartSt.setLong(59, fromTime);
					stopstartSt.setLong(60, endTime);
					stopstartSt.setLong(61, fromTime);
					stopstartSt.setLong(62, endTime);
					
					stopstartSt.setLong(63,(fireoffTime-launchTime)/1000);//发动机工作时长 unit:s
					stopstartSt.setLong(64,runningTime/1000);//行车时长 unit:s
					
					stopstartSt.setString(65, AnalysisDBAdapter.queryVechileInfo(vid).getEntId());
					stopstartSt.setString(66, AnalysisDBAdapter.queryVechileInfo(vid).getEntName());
					stopstartSt.setString(67, AnalysisDBAdapter.queryVechileInfo(vid).getTeamId());
					stopstartSt.setString(68, AnalysisDBAdapter.queryVechileInfo(vid).getTeamName());
					if(AnalysisDBAdapter.queryVechileInfo(vid).getVinCode() != null){
						stopstartSt.setString(69, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode());
					}else{
						stopstartSt.setString(69, null);
					}
					
					if(AnalysisDBAdapter.queryVechileInfo(vid).getVlineId() != null && !"".equals(AnalysisDBAdapter.queryVechileInfo(vid).getVlineId())){
						stopstartSt.setString(70, AnalysisDBAdapter.queryVechileInfo(vid).getVlineId());
					}else{
						stopstartSt.setNull(70, Types.VARCHAR);
					}
					
					if(AnalysisDBAdapter.queryVechileInfo(vid).getLineName() != null){
						stopstartSt.setString(71, AnalysisDBAdapter.queryVechileInfo(vid).getLineName());
					}else{
						stopstartSt.setString(71, null);
					}
					
					stopstartSt.setLong(72, ssb.getStatDate());// 统计日期UTC
					
					stopstartSt.setLong(73, ssb.getEcuOilWear());
					stopstartSt.setDouble(74, ecuRunningOilSubTotal);
					stopstartSt.setDouble(75, ssb.getEcuOilWear()-ecuRunningOilSubTotal);
					stopstartSt.setLong(76, ssb.getMetOilWear());
					stopstartSt.setDouble(77, metRunningOilSubTotal);
					stopstartSt.setDouble(78, ssb.getMetOilWear()-metRunningOilSubTotal);
					stopstartSt.setString(79, cfgFlag);*/
					
					
					
					stopstartSt.setLong(23,(fireoffTime-launchTime)/1000);//发动机工作时长 unit:s
					stopstartSt.setLong(24,runningTime/1000);//行车时长 unit:s
					
					stopstartSt.setString(25, info.getEntId());
					stopstartSt.setString(26, info.getEntName());
					stopstartSt.setString(27, info.getTeamId());
					stopstartSt.setString(28, info.getTeamName());
					
					if(info.getVinCode() != null){
						stopstartSt.setString(29, info.getVinCode());
					}else{
						stopstartSt.setString(29, null);
					}
					
					if(info.getVlineId() != null && !"".equals(info.getVlineId())){
						stopstartSt.setString(30, info.getVlineId());
					}else{
						stopstartSt.setNull(30, Types.VARCHAR);
					}
					
					if(info.getLineName() != null){
						stopstartSt.setString(31, info.getLineName());
					}else{
						stopstartSt.setString(31, null);
					}
					
					if (ssb.getStatDate()==0||ssb.getStatDate()==0L){
						logger.debug("statDate==0");
					}
					
					stopstartSt.setLong(32, ssb.getStatDate());// 统计日期UTC
					
					stopstartSt.setLong(33, ssb.getEcuOilWear());
					stopstartSt.setDouble(34, ecuRunningOilSubTotal);
					stopstartSt.setDouble(35, ssb.getEcuOilWear()-ecuRunningOilSubTotal);
					stopstartSt.setLong(36, ssb.getMetOilWear());
					stopstartSt.setDouble(37, metRunningOilSubTotal);
					stopstartSt.setDouble(38, ssb.getMetOilWear()-metRunningOilSubTotal);
					stopstartSt.setString(39, cfgFlag);
					
					stopstartSt.setLong(40, ssb.getHeaterWorkingTime());
					stopstartSt.setLong(41, ssb.getAircWorkingTime());
					stopstartSt.setLong(42, ssb.getDoor1OpenNum());
					stopstartSt.setLong(43, ssb.getDoor2OpenNum());
					stopstartSt.setLong(44, ssb.getBrakingNum());
					stopstartSt.setLong(45, ssb.getHornWorkingNum());
					stopstartSt.setLong(46, ssb.getRetarderWorkNum());
					stopstartSt.setLong(47, ssb.getAbsWorkingNum());
					
					stopstartSt.addBatch();
			}catch(SQLException e){
				logger.error("存储起步停车数据出错：VID="+vid+" ", e);
				dbCon.rollback();
			}finally{
				if(runningSt != null){
					runningSt.close();
				}
			}
			}
			stopstartSt.executeBatch();
			
		} catch (Exception e) {
			logger.error("存储起步停车数据出错：VID="+vid+" ", e);
			dbCon.rollback();
		}finally{
			/*if(runningSt != null){
				runningSt.close();
			}*/
			if(stopstartSt != null){
				stopstartSt.close();
			}
			if(stopstartlist != null){
				stopstartlist.clear();
			}
			if (dbCon!=null){
				dbCon.close();
			}
			stopstartlist = null;
		}
	}
	
	/**
	 * 存储起步停车及行车数据
	 * @throws SQLException
	 */
	public static void saveStopstartInfo(OracleConnection dbCon,String vid,List<StopstartBean> stopstartlist) throws SQLException{
		if(stopstartlist.size() < 1){ // 无起步停车信息
			return;
		}
		PreparedStatement stopstartSt = null;
		PreparedStatement runningSt = null;
		try {
			//提取当前车的配置方案信息
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			String cfgFlag = "0";
			if (info!=null){
				cfgFlag = info.getCfgFlag();
			}
			
			stopstartSt = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveStopstartInfo"));
			//最后一个对象冗余，因此stopstartlist.size()-2
			for (int i=0;i<stopstartlist.size();i++){
				try {
					StopstartBean ssb = stopstartlist.get(i);
					
//					long subStartMileage = -1;
//					long subEndMileage = -1;
//					long subStartOil = -1;
//					long subEndOil = -1;
					long subStartTime = -1;
					long subEndTime = -1;
					
					double ecuRunningOilSubTotal = 0;
					double metRunningOilSubTotal = 0;
					//long runningMileage = 0;
					long runningTime = 0;
					
//					long id  = CDate.getNow().getTime();
					String mainId = GeneratorPK.instance().getPKString();
					List<RunningBean> runninglist = ssb.getRunninglist();
					//排除疑似漂移的行车数据：无ECU油耗和精准油耗、无里程、有短暂车速、每个起步停车里只包含一个行车过程
					if (runninglist!=null&&runninglist.size()==1){
						RunningBean rb = runninglist.get(0);
						long ecuRunningOil = rb.getEcuRunningOil();
						long metRunningOil = rb.getMetRunningOil();
						long runningMileage = rb.getRunningMileage();

						if (ecuRunningOil<=0&&metRunningOil<=0&&runningMileage<=0){
							continue;
						}
					}
					
					if (runninglist!=null&&runninglist.size()>0){
						
						runningSt = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveRunningInfo"));
						for (int j=0;j<runninglist.size();j++){
							RunningBean rb = runninglist.get(j);
							String detailId  = GeneratorPK.instance().getPKString();
							runningSt.setString(1, detailId+""+j);
							runningSt.setString(2, mainId);
							runningSt.setString(3, vid);
							runningSt.setString(4, info.getVehicleNo());
							/*runningSt.setLong(5, AnalysisDBAdapter.queryVechileInfo(vid).getEntId());
							runningSt.setString(6, AnalysisDBAdapter.queryVechileInfo(vid).getEntName());
							runningSt.setLong(7, AnalysisDBAdapter.queryVechileInfo(vid).getTeamId());
							runningSt.setString(8, AnalysisDBAdapter.queryVechileInfo(vid).getTeamName());
							runningSt.setLong(5, AnalysisDBAdapter.queryVechileInfo(vid).getDriverId());
							runningSt.setString(6, AnalysisDBAdapter.queryVechileInfo(vid).getDriverName());
							runningSt.setLong(5, AnalysisDBAdapter.queryVechileInfo(vid).getVlineId());
							runningSt.setString(6, AnalysisDBAdapter.queryVechileInfo(vid).getEntName());*/
							long startTime = CDate.stringConvertUtc(rb.getStartTime());
							long endTime = CDate.stringConvertUtc(rb.getStopTime());
							
							runningTime +=(endTime-startTime);
							
							runningSt.setLong(5, startTime);
							runningSt.setLong(6, endTime);
							runningSt.setLong(7, rb.getBeginLon());
							runningSt.setLong(8, rb.getBeginLat());
							runningSt.setLong(9, rb.getEndLon());
							runningSt.setLong(10, rb.getEndLat());
//							long startRunningMileage = rb.getStartRunningMileage();
//							long stopRunningMileage = rb.getStopRunningMileage();
//							if (startRunningMileage<0){
//								startRunningMileage = stopRunningMileage;
//							}
//							long subTotalMileage = stopRunningMileage-startRunningMileage;
							
							runningSt.setLong(11, rb.getStartRunningMileage());
							runningSt.setLong(12, rb.getStopRunningMileage());
							runningSt.setLong(13, rb.getRunningMileage());
//							long startRunningOil = rb.getStartRunningOil();
//							long stopRunningOil = rb.getStopRunningOil();
//							if (startRunningOil<0){
//								startRunningOil = stopRunningOil;
//							}
//							long subTotalOil = stopRunningOil-startRunningOil;
							
							
							runningSt.setLong(14, rb.getStartRunningOil());
							runningSt.setLong(15, rb.getStopRunningOil());
							
							metRunningOilSubTotal +=  rb.getMetRunningOil();
							ecuRunningOilSubTotal += rb.getEcuRunningOil();
							
							if ("1".equals(cfgFlag)){
								//精准油耗时要进行单位换算 由 0.01换算为0.5  
								runningSt.setDouble(16, rb.getMetRunningOil()*0.02);
							}else{
								runningSt.setLong(16, rb.getEcuRunningOil());
							}
							
							
							runningSt.setLong(17, rb.getMaxRotateSpeed());
							runningSt.setLong(18, rb.getMaxSpeed());
							runningSt.setString(19, info.getEntId());
							runningSt.setString(20, info.getEntName());
							runningSt.setString(21, info.getTeamId());
							runningSt.setString(22, info.getTeamName());
							if(info.getVinCode() != null){
								runningSt.setString(23, info.getVinCode());
							}else{
								runningSt.setString(23, null);
							}
							
							if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
								runningSt.setString(24, info.getVlineId());
							}else{
								runningSt.setNull(24, Types.INTEGER);
							}
							
							if(info.getLineName() != null){
								runningSt.setString(25, info.getLineName());
							}else{
								runningSt.setString(25, null);
							}
							
							runningSt.setLong(26, rb.getEcuRunningOil());
							runningSt.setLong(27, rb.getMetRunningOil());
							runningSt.setString(28, cfgFlag);
							
							if (j==0){
//								subStartMileage = startRunningMileage;
//								subStartOil = startRunningOil;
								subStartTime = startTime;
							}
							if (j==runninglist.size()-1){
//								subEndMileage = stopRunningMileage;
//								subEndOil = stopRunningOil;
								subEndTime = endTime;
							}
		//不保存行车数据
							//runningSt.addBatch();
						}// Inner of end for
						//runningSt.executeBatch();
						
						if(runningSt != null){
							runningSt.close();
						}
					}
					stopstartSt.setString(1, mainId);
					stopstartSt.setString(2,vid);
					stopstartSt.setString(3, info.getVehicleNo());
					
					long launchTime = 0;
					if (ssb.getLaunchTime()==null||"".equals(ssb.getLaunchTime())){
						stopstartSt.setNull(4, Types.NULL);
					}else{
						launchTime = CDate.stringConvertUtc(ssb.getLaunchTime());
						stopstartSt.setLong(4, launchTime);
					}
					
					if (subStartTime!=-1){
						stopstartSt.setLong(5, subStartTime);
					}else{
						stopstartSt.setNull(5, Types.INTEGER);
					}
					
					if (subEndTime!=-1){
						stopstartSt.setLong(6, subEndTime);
					}else{
						stopstartSt.setNull(6, Types.INTEGER);
					}
					
					long fireoffTime = 0;
					if (ssb.getFireoffTime()==null||"".equals(ssb.getFireoffTime())){
						stopstartSt.setNull(7, Types.NULL);
					}else{
						fireoffTime = CDate.stringConvertUtc(ssb.getFireoffTime());
						stopstartSt.setLong(7, fireoffTime);
					}
					
					stopstartSt.setLong(8, ssb.getBeginLon());
					stopstartSt.setLong(9, ssb.getBeginLat());
					stopstartSt.setLong(10, ssb.getEndLon());
					stopstartSt.setLong(11, ssb.getEndLat());
//					long startMileage = ssb.getStartMileage();
//					long stopMileage = ssb.getEndMileage();
//					long totalMileage = 0;
//					if (ssb.getLaunchTime()==null||"".equals(ssb.getLaunchTime())){
//						totalMileage = runningMileage;
//					}else{
//						totalMileage=stopMileage-startMileage;
//					}
//
//					if( totalMileage< 0){
//						totalMileage = 0;
//					}
					
					stopstartSt.setLong(12, ssb.getStartMileage());
					stopstartSt.setLong(13, ssb.getEndMileage());
					stopstartSt.setLong(14, ssb.getMileage());
//					long startOil = ssb.getStartOil();
//					long stopOil = ssb.getEndOil();
//	
//					long totalOil = 0;
//					if (ssb.getLaunchTime()==null||"".equals(ssb.getLaunchTime())){
//						totalOil = 0;
//					}else{
//						if(stopOil > 0 && startOil > 0)
//						totalOil = stopOil-startOil;
//					}
//
//					if( totalOil< 0){
//						totalOil = 0;
//					}
					
					
					stopstartSt.setLong(15, ssb.getStartOil());
					stopstartSt.setLong(16, ssb.getEndOil());
					
					if ("1".equals(cfgFlag)){
						stopstartSt.setDouble(17, ssb.getMetOilWear()*0.02);
					}else{
						stopstartSt.setLong(17, ssb.getEcuOilWear());
					}
					
					stopstartSt.setLong(18, ssb.getMaxRotateSpeed());
					stopstartSt.setLong(19, ssb.getMaxSpeed());
					stopstartSt.setLong(20, runninglist.size());
					if ("1".equals(cfgFlag)){
						stopstartSt.setDouble(21, (ssb.getMetOilWear()-metRunningOilSubTotal)*0.02);
						stopstartSt.setDouble(22, metRunningOilSubTotal*0.02);
					}else{
						stopstartSt.setDouble(21, ssb.getEcuOilWear()-ecuRunningOilSubTotal);
						stopstartSt.setDouble(22, ecuRunningOilSubTotal);
					}
					
					
					//子查询语句条件
					long fromTime=launchTime;
					if (launchTime==0){
						fromTime = subStartTime;
					}
					long endTime=fireoffTime;
					if (fireoffTime==0){
						endTime=subEndTime;
					}

	/*				stopstartSt.setString(23,vid);
					stopstartSt.setLong(24, fromTime);
					stopstartSt.setLong(25, endTime);
					stopstartSt.setLong(26, fromTime);
					stopstartSt.setLong(27, endTime);
					
					stopstartSt.setString(28,vid);
					stopstartSt.setLong(29, fromTime);
					stopstartSt.setLong(30, endTime);
					stopstartSt.setLong(31, fromTime);
					stopstartSt.setLong(32, endTime);
					
					
					stopstartSt.setString(33,vid);
					stopstartSt.setLong(34, fromTime);
					stopstartSt.setLong(35, endTime);
					stopstartSt.setLong(36, fromTime);
					stopstartSt.setLong(37, endTime);
					
					stopstartSt.setString(38,vid);
					stopstartSt.setLong(39, fromTime);
					stopstartSt.setLong(40, endTime);
					stopstartSt.setLong(41, fromTime);
					stopstartSt.setLong(42, endTime);
					
					stopstartSt.setString(43,vid);
					stopstartSt.setLong(44, fromTime);
					stopstartSt.setLong(45, endTime);
					stopstartSt.setLong(46, fromTime);
					stopstartSt.setLong(47, endTime);
					
					stopstartSt.setString(48,vid);
					stopstartSt.setLong(49, fromTime);
					stopstartSt.setLong(50, endTime);
					stopstartSt.setLong(51, fromTime);
					stopstartSt.setLong(52, endTime);
					
					
					stopstartSt.setString(53,vid);
					stopstartSt.setLong(54, fromTime);
					stopstartSt.setLong(55, endTime);
					stopstartSt.setLong(56, fromTime);
					stopstartSt.setLong(57, endTime);
					
					stopstartSt.setString(58,vid);
					stopstartSt.setLong(59, fromTime);
					stopstartSt.setLong(60, endTime);
					stopstartSt.setLong(61, fromTime);
					stopstartSt.setLong(62, endTime);
					
					stopstartSt.setLong(63,(fireoffTime-launchTime)/1000);//发动机工作时长 unit:s
					stopstartSt.setLong(64,runningTime/1000);//行车时长 unit:s
					
					stopstartSt.setString(65, AnalysisDBAdapter.queryVechileInfo(vid).getEntId());
					stopstartSt.setString(66, AnalysisDBAdapter.queryVechileInfo(vid).getEntName());
					stopstartSt.setString(67, AnalysisDBAdapter.queryVechileInfo(vid).getTeamId());
					stopstartSt.setString(68, AnalysisDBAdapter.queryVechileInfo(vid).getTeamName());
					if(AnalysisDBAdapter.queryVechileInfo(vid).getVinCode() != null){
						stopstartSt.setString(69, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode());
					}else{
						stopstartSt.setString(69, null);
					}
					
					if(AnalysisDBAdapter.queryVechileInfo(vid).getVlineId() != null && !"".equals(AnalysisDBAdapter.queryVechileInfo(vid).getVlineId())){
						stopstartSt.setString(70, AnalysisDBAdapter.queryVechileInfo(vid).getVlineId());
					}else{
						stopstartSt.setNull(70, Types.VARCHAR);
					}
					
					if(AnalysisDBAdapter.queryVechileInfo(vid).getLineName() != null){
						stopstartSt.setString(71, AnalysisDBAdapter.queryVechileInfo(vid).getLineName());
					}else{
						stopstartSt.setString(71, null);
					}
					
					stopstartSt.setLong(72, ssb.getStatDate());// 统计日期UTC
					
					stopstartSt.setLong(73, ssb.getEcuOilWear());
					stopstartSt.setDouble(74, ecuRunningOilSubTotal);
					stopstartSt.setDouble(75, ssb.getEcuOilWear()-ecuRunningOilSubTotal);
					stopstartSt.setLong(76, ssb.getMetOilWear());
					stopstartSt.setDouble(77, metRunningOilSubTotal);
					stopstartSt.setDouble(78, ssb.getMetOilWear()-metRunningOilSubTotal);
					stopstartSt.setString(79, cfgFlag);*/
					
					
					
					stopstartSt.setLong(23,(fireoffTime-launchTime)/1000);//发动机工作时长 unit:s
					stopstartSt.setLong(24,runningTime/1000);//行车时长 unit:s
					
					stopstartSt.setString(25, info.getEntId());
					stopstartSt.setString(26, info.getEntName());
					stopstartSt.setString(27, info.getTeamId());
					stopstartSt.setString(28, info.getTeamName());
					
					if(info.getVinCode() != null){
						stopstartSt.setString(29, info.getVinCode());
					}else{
						stopstartSt.setString(29, null);
					}
					
					if(info.getVlineId() != null && !"".equals(info.getVlineId())){
						stopstartSt.setString(30, info.getVlineId());
					}else{
						stopstartSt.setNull(30, Types.VARCHAR);
					}
					
					if(info.getLineName() != null){
						stopstartSt.setString(31, info.getLineName());
					}else{
						stopstartSt.setString(31, null);
					}
					
					if (ssb.getStatDate()==0||ssb.getStatDate()==0L){
						logger.debug("statDate==0");
					}
					
					stopstartSt.setLong(32, ssb.getStatDate());// 统计日期UTC
					
					stopstartSt.setLong(33, ssb.getEcuOilWear());
					stopstartSt.setDouble(34, ecuRunningOilSubTotal);
					stopstartSt.setDouble(35, ssb.getEcuOilWear()-ecuRunningOilSubTotal);
					stopstartSt.setLong(36, ssb.getMetOilWear());
					stopstartSt.setDouble(37, metRunningOilSubTotal);
					stopstartSt.setDouble(38, ssb.getMetOilWear()-metRunningOilSubTotal);
					stopstartSt.setString(39, cfgFlag);
					
					stopstartSt.setLong(40, ssb.getHeaterWorkingTime());
					stopstartSt.setLong(41, ssb.getAircWorkingTime());
					stopstartSt.setLong(42, ssb.getDoor1OpenNum());
					stopstartSt.setLong(43, ssb.getDoor2OpenNum());
					stopstartSt.setLong(44, ssb.getBrakingNum());
					stopstartSt.setLong(45, ssb.getHornWorkingNum());
					stopstartSt.setLong(46, ssb.getRetarderWorkNum());
					stopstartSt.setLong(47, ssb.getAbsWorkingNum());
					
					stopstartSt.setString(48, ssb.getDriverId());
					stopstartSt.setString(49, ssb.getDriverName());
					stopstartSt.setString(50, ssb.getDriverSrc());
					
					stopstartSt.addBatch();
			}catch(SQLException e){
				logger.error("存储起步停车数据出错：VID="+vid+" ", e);
				dbCon.rollback();
			}finally{
				if(runningSt != null){
					runningSt.close();
				}
			}
			}
			stopstartSt.executeBatch();
			
		} catch (Exception e) {
			logger.error("存储起步停车数据出错：VID="+vid+" ", e);
			dbCon.rollback();
		}finally{
			/*if(runningSt != null){
				runningSt.close();
			}*/
			if(stopstartSt != null){
				stopstartSt.close();
			}
			if(stopstartlist != null){
				stopstartlist.clear();
			}
			stopstartlist = null;
		}
	}
	
	/***
	 *  存储车辆日统计信息
	 * @throws SQLException
	 */
	public static void saveStaDayInfo(String vid,VehicleStatus vehicleStatus) throws SQLException{
		OracleConnection dbCon = null;
		PreparedStatement stSaveDayStInfo = null;
		try{
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			String cfgFlag = info.getCfgFlag();
			stSaveDayStInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveDayServiceStInfo"));
			stSaveDayStInfo.setLong(1, vehicleStatus.getStatDate()); // 日期UTC
			stSaveDayStInfo.setString(2, vid); // 车辆编号
			if(info != null){
				stSaveDayStInfo.setString(3, info.getVehicleNo()); // 车牌号码
				stSaveDayStInfo.setString(4, info.getVinCode()); // 车架号(VIN)
				stSaveDayStInfo.setString(5, info.getEntId()); // 企业ID
				stSaveDayStInfo.setString(6, info.getEntName()); // 企业名称
				stSaveDayStInfo.setString(7, info.getTeamId()); // 车队ID
				stSaveDayStInfo.setString(8, info.getTeamName()); // 车队名称
			} else {
				stSaveDayStInfo.setString(3, null); // 车牌号码
				stSaveDayStInfo.setString(4, null); // 车架号(VIN)
				stSaveDayStInfo.setString(5, null); // 企业ID
				stSaveDayStInfo.setString(6, null); // 企业名称
				stSaveDayStInfo.setString(7, null); // 车队ID
				stSaveDayStInfo.setString(8, null); // 车队名称
			}
			//stSaveDayStInfo.setLong(9, info.getCheckNum()); // 车辆上线次数(终端成功鉴权次数)
			stSaveDayStInfo.setLong(9, vehicleStatus.getOnOffLine()); // 车辆在线时长
			stSaveDayStInfo.setLong(10, vehicleStatus.getEngineTime()); // 当日发动机运行时间
			
			stSaveDayStInfo.setLong(11, vehicleStatus.getMaxSpeed()); // 本日最大车速
			stSaveDayStInfo.setLong(12, vehicleStatus.getMaxRpm()); // 本日最大发动机转速
			stSaveDayStInfo.setLong(13, vehicleStatus.getCountGPSValid()); // 定位有效数量
			stSaveDayStInfo.setLong(14, vehicleStatus.getCountGPSInvalid()); // 定位无效数量
			stSaveDayStInfo.setLong(15, vehicleStatus.getGpsTimeInvild()); // GPS时间无效数量
			stSaveDayStInfo.setLong(16, vehicleStatus.getCountLatLonInvalid()); // 经纬度无效数量
		
			
			stSaveDayStInfo.setLong(17, vehicleStatus.getAccCount()); // ACC开次数
			stSaveDayStInfo.setLong(18, vehicleStatus.getAccTime()); // ACC开时长
			
			if( info.getVlineId() != null && !"".equals(info.getVlineId())){ // 线路ID
				stSaveDayStInfo.setString(19,  info.getVlineId()); 
			}else{
				stSaveDayStInfo.setNull(19,  Types.VARCHAR);
			}
			
			if( info.getLineName() != null){ // 线路ID
				stSaveDayStInfo.setString(20,  info.getLineName()); 
			}else{
				stSaveDayStInfo.setString(20, null);
			}

			stSaveDayStInfo.setLong(21, vehicleStatus.getPoint_milege()); // 最后一个点减第一个点计算里程
			stSaveDayStInfo.setLong(22, vehicleStatus.getPoint_oil()); // 最后一个点减第一个点计算油耗
			stSaveDayStInfo.setLong(23, vehicleStatus.getGis_milege()); // GIS计算里程
			
			stSaveDayStInfo.setLong(24,vehicleStatus.getIdlingTime());//当日行车时长
			stSaveDayStInfo.setString(25, cfgFlag);//是否使用精准油耗 1 使用 0不使用
			
			stSaveDayStInfo.setLong(26, vehicleStatus.getMileage());//里程
			if ("1".equals(cfgFlag)){
				stSaveDayStInfo.setDouble(27, vehicleStatus.getPrecise_oil()*0.02);//油耗
				stSaveDayStInfo.setDouble(28, vehicleStatus.getMetRunningOil()*0.02);//行车油耗
			}else{
				stSaveDayStInfo.setDouble(27, vehicleStatus.getEcuOil());
				stSaveDayStInfo.setDouble(28, vehicleStatus.getEcuRunningOil());
			}
			
			stSaveDayStInfo.setLong(29, vehicleStatus.getPrecise_oil());//精准油耗
			stSaveDayStInfo.setLong(30, vehicleStatus.getMetRunningOil());//精准行车油耗
			stSaveDayStInfo.setLong(31, (vehicleStatus.getPrecise_oil() - vehicleStatus.getMetRunningOil()));//精准怠速油耗
			
			stSaveDayStInfo.setLong(32, vehicleStatus.getEcuOil());//ecu油耗
			stSaveDayStInfo.setLong(33, vehicleStatus.getEcuRunningOil());//ecu行车油耗
			stSaveDayStInfo.setLong(34, (vehicleStatus.getEcuOil() - vehicleStatus.getEcuRunningOil()));//ecu怠速油耗
			
			stSaveDayStInfo.executeUpdate();
		}catch(SQLException e){
			logger.error("存储车辆日统计出错：VID="+vid+" ",e);
		}finally{
			if(stSaveDayStInfo != null){
				stSaveDayStInfo.close();
			}
			if (dbCon!=null){
				dbCon.close();
			}
		}
	}
	
	/***
	 *  存储车辆日统计信息
	 * @throws SQLException
	 */
	public static void saveStaDayInfo(OracleConnection dbCon,String vid,VehicleStatus vehicleStatus) throws Exception{
		PreparedStatement stSaveDayStInfo = null;
		try{
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			String cfgFlag = "0";
			if (info!=null){
				cfgFlag = info.getCfgFlag();
			}
			//String cfgFlag = info.getCfgFlag();
			stSaveDayStInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveDayServiceStInfo"));
			stSaveDayStInfo.setLong(1, vehicleStatus.getStatDate()); // 日期UTC
			stSaveDayStInfo.setString(2, vid); // 车辆编号
			if(info != null){
				stSaveDayStInfo.setString(3, info.getVehicleNo()); // 车牌号码
				stSaveDayStInfo.setString(4, info.getVinCode()); // 车架号(VIN)
				stSaveDayStInfo.setString(5, info.getEntId()); // 企业ID
				stSaveDayStInfo.setString(6, info.getEntName()); // 企业名称
				stSaveDayStInfo.setString(7, info.getTeamId()); // 车队ID
				stSaveDayStInfo.setString(8, info.getTeamName()); // 车队名称
			} else {
				stSaveDayStInfo.setString(3, null); // 车牌号码
				stSaveDayStInfo.setString(4, null); // 车架号(VIN)
				stSaveDayStInfo.setString(5, null); // 企业ID
				stSaveDayStInfo.setString(6, null); // 企业名称
				stSaveDayStInfo.setString(7, null); // 车队ID
				stSaveDayStInfo.setString(8, null); // 车队名称
			}
			//stSaveDayStInfo.setLong(9, info.getCheckNum()); // 车辆上线次数(终端成功鉴权次数)
			stSaveDayStInfo.setLong(9, vehicleStatus.getOnOffLine()); // 车辆在线时长
			stSaveDayStInfo.setLong(10, vehicleStatus.getEngineTime()); // 当日发动机运行时间
			
			stSaveDayStInfo.setLong(11, vehicleStatus.getMaxSpeed()); // 本日最大车速
			stSaveDayStInfo.setLong(12, vehicleStatus.getMaxRpm()); // 本日最大发动机转速
			stSaveDayStInfo.setLong(13, vehicleStatus.getCountGPSValid()); // 定位有效数量
			stSaveDayStInfo.setLong(14, vehicleStatus.getCountGPSInvalid()); // 定位无效数量
			stSaveDayStInfo.setLong(15, vehicleStatus.getGpsTimeInvild()); // GPS时间无效数量
			stSaveDayStInfo.setLong(16, vehicleStatus.getCountLatLonInvalid()); // 经纬度无效数量
		
			
			stSaveDayStInfo.setLong(17, vehicleStatus.getAccCount()); // ACC开次数
			stSaveDayStInfo.setLong(18, vehicleStatus.getAccTime()); // ACC开时长
			
			if( info.getVlineId() != null && !"".equals(info.getVlineId())){ // 线路ID
				stSaveDayStInfo.setString(19,  info.getVlineId()); 
			}else{
				stSaveDayStInfo.setNull(19,  Types.VARCHAR);
			}
			
			if( info.getLineName() != null){ // 线路ID
				stSaveDayStInfo.setString(20,  info.getLineName()); 
			}else{
				stSaveDayStInfo.setString(20, null);
			}

			stSaveDayStInfo.setLong(21, vehicleStatus.getPoint_milege()); // 最后一个点减第一个点计算里程
			stSaveDayStInfo.setLong(22, vehicleStatus.getPoint_oil()); // 最后一个点减第一个点计算油耗
			stSaveDayStInfo.setLong(23, vehicleStatus.getGis_milege()); // GIS计算里程
			
			stSaveDayStInfo.setLong(24,vehicleStatus.getIdlingTime());//当日行车时长
			stSaveDayStInfo.setString(25, cfgFlag);//是否使用精准油耗 1 使用 0不使用
			
			stSaveDayStInfo.setLong(26, vehicleStatus.getMileage());//里程
			if ("1".equals(cfgFlag)){
				stSaveDayStInfo.setDouble(27, vehicleStatus.getPrecise_oil()*0.02);//油耗
				stSaveDayStInfo.setDouble(28, vehicleStatus.getMetRunningOil()*0.02);//行车油耗
			}else{
				stSaveDayStInfo.setDouble(27, vehicleStatus.getEcuOil());
				stSaveDayStInfo.setDouble(28, vehicleStatus.getEcuRunningOil());
			}
			
			stSaveDayStInfo.setLong(29, vehicleStatus.getPrecise_oil());//精准油耗
			stSaveDayStInfo.setLong(30, vehicleStatus.getMetRunningOil());//精准行车油耗
			stSaveDayStInfo.setLong(31, (vehicleStatus.getPrecise_oil() - vehicleStatus.getMetRunningOil()));//精准怠速油耗
			
			stSaveDayStInfo.setLong(32, vehicleStatus.getEcuOil());//ecu油耗
			stSaveDayStInfo.setLong(33, vehicleStatus.getEcuRunningOil());//ecu行车油耗
			stSaveDayStInfo.setLong(34, (vehicleStatus.getEcuOil() - vehicleStatus.getEcuRunningOil()));//ecu怠速油耗
			
			stSaveDayStInfo.executeUpdate();
		}catch(SQLException e){
			logger.error("存储车辆日统计出错：VID="+vid+" ",e);
		}finally{
			if(stSaveDayStInfo != null){
				stSaveDayStInfo.close();
			}
		}
	}
	
	/****
	 * 存储开门详细信息
	 * @throws SQLException 
	 */
	public static void saveOpenningDoorDetail(String vid,Vector<AlarmCacheBean> openingDoorList) throws SQLException{
		OracleConnection dbCon = null;
		PreparedStatement stSaveOpenningDoor = null;
		try{
			if (openingDoorList!=null&&openingDoorList.size()>0){
				VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
				
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stSaveOpenningDoor = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_insertOpenDoorDetails"));
				Iterator<AlarmCacheBean> openningDoorIt = openingDoorList.iterator();
				int count =0;
				while(openningDoorIt.hasNext()){
					AlarmCacheBean event = openningDoorIt.next();
					String alarmCode =  event.getAlarmcode();
					count++;
					stSaveOpenningDoor.setString(1, GeneratorPK.instance().getPKString());
					stSaveOpenningDoor.setString(2, vid);
					stSaveOpenningDoor.setString(3, info.getVehicleNo());
					stSaveOpenningDoor.setString(4, info.getVinCode());
					stSaveOpenningDoor.setString(5, info.getInnerCode());
					stSaveOpenningDoor.setString(6, info.getEntId());
					stSaveOpenningDoor.setString(7, info.getEntName());
					stSaveOpenningDoor.setString(8, info.getTeamId());
					stSaveOpenningDoor.setString(9, info.getTeamName());
					if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
						stSaveOpenningDoor.setString(10, info.getVlineId());
					}else{
						stSaveOpenningDoor.setNull(10, Types.VARCHAR);
					}
					if(info.getLineName() != null){
						stSaveOpenningDoor.setString(11, info.getLineName());
					}else{
						stSaveOpenningDoor.setString(11, null);
					}
		
					stSaveOpenningDoor.setNull(12, Types.INTEGER);
					stSaveOpenningDoor.setString(13, null);
					if(event.getAreaId() != null && !"".equals(event.getAreaId())){
						stSaveOpenningDoor.setString(14, event.getAreaId());
					}else{
						stSaveOpenningDoor.setNull(14, Types.VARCHAR);
					}
					
					if(event.getMtypeCode() != null){
						stSaveOpenningDoor.setString(15, event.getMtypeCode());
					}else{
						stSaveOpenningDoor.setString(15, null);
					}
					
					if(event.getMediaUrl() != null){
						stSaveOpenningDoor.setString(16, event.getMediaUrl());
					}else{
						stSaveOpenningDoor.setString(16, null);
					}
					
					if("BS0013".equals(alarmCode)){
						stSaveOpenningDoor.setString(17, "1");
					}else if("BS0014".equals(alarmCode)){
						stSaveOpenningDoor.setString(17, "2");
					}else if("BS0015".equals(alarmCode)){
						stSaveOpenningDoor.setString(17, "3");
					}else if("BS0016".equals(alarmCode)||"BS0017".equals(alarmCode)){
						stSaveOpenningDoor.setString(17, "4");
					}else{
						stSaveOpenningDoor.setNull(17, Types.VARCHAR);
					}
					
					VehicleMessageBean beginBean  = event.getBeginVmb();
					String opendoorType = beginBean.getOpendoorState();
					if (opendoorType==null||"".equals(opendoorType)){
						opendoorType = "1";//为空时默认为正常开门
					}
					stSaveOpenningDoor.setString(18, opendoorType);
					
					if(beginBean.getUtc() != -1){
						stSaveOpenningDoor.setLong(19, beginBean.getUtc());
					}else{
						stSaveOpenningDoor.setLong(19, 0);
					}
					
					if(beginBean.getLat() != -1){
						stSaveOpenningDoor.setLong(20,beginBean.getLat());
					}else{
						stSaveOpenningDoor.setLong(20,0);
					}
					
					if( beginBean.getLon() != -1){
						stSaveOpenningDoor.setLong(21, beginBean.getLon());
					}else{
						stSaveOpenningDoor.setLong(21, 0);
					}
					
					if(beginBean.getMaplon() != -1){
						stSaveOpenningDoor.setLong(22, beginBean.getMaplon());
					}else{
						stSaveOpenningDoor.setLong(22, 0);
					}
					
					if(beginBean.getMaplat() != -1){
						stSaveOpenningDoor.setLong(23, beginBean.getMaplat());
					}else{
						stSaveOpenningDoor.setLong(23, 0);
					}
					if(beginBean.getElevation() != -1){
						stSaveOpenningDoor.setInt(24, beginBean.getElevation());
					}else{
						stSaveOpenningDoor.setInt(24, 0);
					}
					
					if(beginBean.getDir() != -1){
						stSaveOpenningDoor.setInt(25, beginBean.getDir());
					}else{
						stSaveOpenningDoor.setInt(25, 0);
					}
					
					if(beginBean.getSpeed() != -1){
						stSaveOpenningDoor.setLong(26, beginBean.getSpeed());
					}else{
						stSaveOpenningDoor.setLong(26, 0);
					}
					
					VehicleMessageBean endBean = event.getEndVmb();
					if( endBean.getUtc() != -1){
						stSaveOpenningDoor.setLong(27, endBean.getUtc());
					}else{
						stSaveOpenningDoor.setLong(27, 0);
					}
					
					if(endBean.getLat() != -1){
						stSaveOpenningDoor.setLong(28,endBean.getLat());
					}else{
						stSaveOpenningDoor.setLong(28,0);
					}
					
					if(endBean.getLon() != -1){
						stSaveOpenningDoor.setLong(29, endBean.getLon());
					}else{
						stSaveOpenningDoor.setLong(29, 0);
					}
	
					if(endBean.getMaplon() != -1){
						stSaveOpenningDoor.setLong(30, endBean.getMaplon());
					}else{
						stSaveOpenningDoor.setLong(30, 0);
					}
					
					if(endBean.getMaplat() != -1){
						stSaveOpenningDoor.setLong(31, endBean.getMaplat());
					}else{
						stSaveOpenningDoor.setLong(31, 0);
					}
					
					if(endBean.getElevation() != -1){
						stSaveOpenningDoor.setInt(32, endBean.getElevation());
					}else{
						stSaveOpenningDoor.setInt(32, 0);
					}
					
					if( endBean.getDir() != -1){
						stSaveOpenningDoor.setInt(33, endBean.getDir());
					}else{
						stSaveOpenningDoor.setInt(33, 0);
					}
					
					if(endBean.getSpeed() != -1){
						stSaveOpenningDoor.setLong(34, endBean.getSpeed());
					}else{
						stSaveOpenningDoor.setLong(34, 0);
					}
					long diffTime = (endBean.getUtc() - beginBean.getUtc())/1000;
					if (diffTime>0){
						stSaveOpenningDoor.setDouble(35, diffTime);
					}else{
						stSaveOpenningDoor.setDouble(35, 0);
					}
					
					if(event.getMaxSpeed() != -1){
						stSaveOpenningDoor.setLong(36, event.getMaxSpeed());
					}else{
						stSaveOpenningDoor.setInt(36, 0);
					}
					
					stSaveOpenningDoor.setLong(37, event.getMileage());
					
					stSaveOpenningDoor.setLong(38, event.getOil());
					
					stSaveOpenningDoor.addBatch();
					if(count % 100 == 0){
						stSaveOpenningDoor.executeBatch();
						stSaveOpenningDoor.clearBatch();
						count = 0;
					}
				}// End while
				if(count > 0 ){
					stSaveOpenningDoor.executeBatch();
				}
			}
		}catch(SQLException e){
			logger.error(",",e);
		}finally{
			if(stSaveOpenningDoor != null){
				stSaveOpenningDoor.close();
			}
			if(openingDoorList.size() > 0){
				openingDoorList.clear();
			}
			
			if (dbCon!=null){
				dbCon.close();
			}
		}
	}
	
	
	/****
	 * 存储开门详细信息
	 * @throws SQLException 
	 */
	public static void saveOpenningDoorDetail(OracleConnection dbCon,String vid,Vector<AlarmCacheBean> openingDoorList) throws Exception{
		PreparedStatement stSaveOpenningDoor = null;
		try{
			if (openingDoorList!=null&&openingDoorList.size()>0){
				VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);

				stSaveOpenningDoor = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_insertOpenDoorDetails"));
				Iterator<AlarmCacheBean> openningDoorIt = openingDoorList.iterator();
				int count =0;
				while(openningDoorIt.hasNext()){
					AlarmCacheBean event = openningDoorIt.next();
					String alarmCode =  event.getAlarmcode();
					count++;
					stSaveOpenningDoor.setString(1, GeneratorPK.instance().getPKString());
					stSaveOpenningDoor.setString(2, vid);
					stSaveOpenningDoor.setString(3, info.getVehicleNo());
					stSaveOpenningDoor.setString(4, info.getVinCode());
					stSaveOpenningDoor.setString(5, info.getInnerCode());
					stSaveOpenningDoor.setString(6, info.getEntId());
					stSaveOpenningDoor.setString(7, info.getEntName());
					stSaveOpenningDoor.setString(8, info.getTeamId());
					stSaveOpenningDoor.setString(9, info.getTeamName());
					if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
						stSaveOpenningDoor.setString(10, info.getVlineId());
					}else{
						stSaveOpenningDoor.setNull(10, Types.VARCHAR);
					}
					if(info.getLineName() != null){
						stSaveOpenningDoor.setString(11, info.getLineName());
					}else{
						stSaveOpenningDoor.setString(11, null);
					}
					
					if(event.getAreaId() != null && !"".equals(event.getAreaId())){
						stSaveOpenningDoor.setString(12, event.getAreaId());
					}else{
						stSaveOpenningDoor.setNull(12, Types.VARCHAR);
					}
					
					if(event.getMtypeCode() != null){
						stSaveOpenningDoor.setString(13, event.getMtypeCode());
					}else{
						stSaveOpenningDoor.setString(13, null);
					}
					
					if(event.getMediaUrl() != null){
						stSaveOpenningDoor.setString(14, event.getMediaUrl());
					}else{
						stSaveOpenningDoor.setString(14, null);
					}
					
					if("BS0013".equals(alarmCode)){
						stSaveOpenningDoor.setString(15, "1");
					}else if("BS0014".equals(alarmCode)){
						stSaveOpenningDoor.setString(15, "2");
					}else if("BS0015".equals(alarmCode)){
						stSaveOpenningDoor.setString(15, "3");
					}else if("BS0016".equals(alarmCode)||"BS0017".equals(alarmCode)){
						stSaveOpenningDoor.setString(15, "4");
					}else{
						stSaveOpenningDoor.setNull(15, Types.VARCHAR);
					}
					
					VehicleMessageBean beginBean  = event.getBeginVmb();
					String opendoorType = beginBean.getOpendoorState();
					if (opendoorType==null||"".equals(opendoorType)){
						opendoorType = "1";//为空时默认为正常开门
					}
					stSaveOpenningDoor.setString(16, opendoorType);
					
					if(beginBean.getUtc() != -1){
						stSaveOpenningDoor.setLong(17, beginBean.getUtc());
					}else{
						stSaveOpenningDoor.setLong(17, 0);
					}
					
					if(beginBean.getLat() != -1){
						stSaveOpenningDoor.setLong(18,beginBean.getLat());
					}else{
						stSaveOpenningDoor.setLong(18,0);
					}
					
					if( beginBean.getLon() != -1){
						stSaveOpenningDoor.setLong(19, beginBean.getLon());
					}else{
						stSaveOpenningDoor.setLong(19, 0);
					}
					
					if(beginBean.getMaplon() != -1){
						stSaveOpenningDoor.setLong(20, beginBean.getMaplon());
					}else{
						stSaveOpenningDoor.setLong(20, 0);
					}
					
					if(beginBean.getMaplat() != -1){
						stSaveOpenningDoor.setLong(21, beginBean.getMaplat());
					}else{
						stSaveOpenningDoor.setLong(21, 0);
					}
					if(beginBean.getElevation() != -1){
						stSaveOpenningDoor.setInt(22, beginBean.getElevation());
					}else{
						stSaveOpenningDoor.setInt(22, 0);
					}
					
					if(beginBean.getDir() != -1){
						stSaveOpenningDoor.setInt(23, beginBean.getDir());
					}else{
						stSaveOpenningDoor.setInt(23, 0);
					}
					
					if(beginBean.getSpeed() != -1){
						stSaveOpenningDoor.setLong(24, beginBean.getSpeed());
					}else{
						stSaveOpenningDoor.setLong(24, 0);
					}
					
					VehicleMessageBean endBean = event.getEndVmb();
					if( endBean.getUtc() != -1){
						stSaveOpenningDoor.setLong(25, endBean.getUtc());
					}else{
						stSaveOpenningDoor.setLong(25, 0);
					}
					
					if(endBean.getLat() != -1){
						stSaveOpenningDoor.setLong(26,endBean.getLat());
					}else{
						stSaveOpenningDoor.setLong(26,0);
					}
					
					if(endBean.getLon() != -1){
						stSaveOpenningDoor.setLong(27, endBean.getLon());
					}else{
						stSaveOpenningDoor.setLong(27, 0);
					}
	
					if(endBean.getMaplon() != -1){
						stSaveOpenningDoor.setLong(28, endBean.getMaplon());
					}else{
						stSaveOpenningDoor.setLong(28, 0);
					}
					
					if(endBean.getMaplat() != -1){
						stSaveOpenningDoor.setLong(29, endBean.getMaplat());
					}else{
						stSaveOpenningDoor.setLong(29, 0);
					}
					
					if(endBean.getElevation() != -1){
						stSaveOpenningDoor.setInt(30, endBean.getElevation());
					}else{
						stSaveOpenningDoor.setInt(30, 0);
					}
					
					if( endBean.getDir() != -1){
						stSaveOpenningDoor.setInt(31, endBean.getDir());
					}else{
						stSaveOpenningDoor.setInt(31, 0);
					}
					
					if(endBean.getSpeed() != -1){
						stSaveOpenningDoor.setLong(32, endBean.getSpeed());
					}else{
						stSaveOpenningDoor.setLong(32, 0);
					}
					long diffTime = (endBean.getUtc() - beginBean.getUtc())/1000;
					if (diffTime>0){
						stSaveOpenningDoor.setDouble(33, diffTime);
					}else{
						stSaveOpenningDoor.setDouble(33, 0);
					}
					
					if(event.getMaxSpeed() != -1){
						stSaveOpenningDoor.setLong(34, event.getMaxSpeed());
					}else{
						stSaveOpenningDoor.setInt(34, 0);
					}
					
					stSaveOpenningDoor.setLong(35, event.getMileage());
					
					stSaveOpenningDoor.setLong(36, event.getOil());
					
					stSaveOpenningDoor.setString(37, beginBean.getDriverId());
					stSaveOpenningDoor.setString(38, beginBean.getDriverName());
					stSaveOpenningDoor.setString(39, beginBean.getDriverSrc());
					
					stSaveOpenningDoor.addBatch();
					if(count % 100 == 0){
						stSaveOpenningDoor.executeBatch();
						stSaveOpenningDoor.clearBatch();
						count = 0;
					}
				}// End while
				if(count > 0 ){
					stSaveOpenningDoor.executeBatch();
				}
			}
		}catch(SQLException e){
			logger.error(",",e);
		}finally{
			if(stSaveOpenningDoor != null){
				stSaveOpenningDoor.close();
			}
			if(openingDoorList.size() > 0){
				openingDoorList.clear();
			}
		}
	}
	
	/****
	 * 存储车辆开门运营违规日统计
	 * utc 当日0点对应utc时间
	 * @throws SQLException 
	 */
	
	public static void saveOpenningDoorDay(String vid,long utc){
		OracleConnection dbCon = null;
		PreparedStatement stSaveOpenningDoor = null;
		PreparedStatement stSelectOpenningDoorDay = null;
		ResultSet rs = null;
		
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveOpenningDoor = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOpenningDoorDay"));
			stSelectOpenningDoorDay = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_selectOpenningDoorDay"));
			
			stSelectOpenningDoorDay.setString(1, vid);
			stSelectOpenningDoorDay.setLong(2, utc);
			stSelectOpenningDoorDay.setLong(3, utc + 24 * 60 * 60 * 1000);
			rs = stSelectOpenningDoorDay.executeQuery();
			int count = 0;
			while(rs.next()){
				stSaveOpenningDoor.setString(1, GeneratorPK.instance().getPKString());
				stSaveOpenningDoor.setLong(2, utc + 12 * 60 * 60 * 1000);
				stSaveOpenningDoor.setString(3,vid);
				stSaveOpenningDoor.setString(4,info.getEntId());
				stSaveOpenningDoor.setString(5, info.getEntName());
				stSaveOpenningDoor.setString(6, info.getTeamId());
				stSaveOpenningDoor.setString(7, info.getTeamName());
				stSaveOpenningDoor.setString(8, info.getVehicleNo());
				stSaveOpenningDoor.setString(9, info.getVinCode());
				stSaveOpenningDoor.setInt(10,rs.getInt("NUM"));
				stSaveOpenningDoor.setInt(11,rs.getInt("TIME"));
				String openType = rs.getString("OPENDOOR_TYPE");
				if("2".equals(openType)){ //带速开门 
					stSaveOpenningDoor.setString(12,AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.IDELINGOPENINGDOOR));
					stSaveOpenningDoor.setString(13,ExcConstants.IDELINGOPENINGDOOR);
				}else if("3".equals(openType)){ // 区域内开门
					stSaveOpenningDoor.setString(12,AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.OPENINGDOOR));
					stSaveOpenningDoor.setString(13,ExcConstants.OPENINGDOOR);
				}else if("4".equals(openType)){ // 区域外开门
					stSaveOpenningDoor.setString(12,AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.OUTOPENINGDOOR));
					stSaveOpenningDoor.setString(13,ExcConstants.OUTOPENINGDOOR);
				}else{
					stSaveOpenningDoor.setString(12,null);
					stSaveOpenningDoor.setString(13,null);
				}
				stSaveOpenningDoor.setInt(14,rs.getInt("MILEAGE"));
				stSaveOpenningDoor.setInt(15,rs.getInt("OIL_WEAR"));
				if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
					stSaveOpenningDoor.setString(16, info.getVlineId());
				}else{
					stSaveOpenningDoor.setNull(16, Types.VARCHAR);
				}
				
				if(info.getLineName() != null){
					stSaveOpenningDoor.setString(17, info.getLineName());
				}else{
					stSaveOpenningDoor.setString(17, null);
				}
				
				stSaveOpenningDoor.addBatch();
				count++;
			}//End while
			
			if(count > 0){
				stSaveOpenningDoor.executeBatch();
			}
		} catch (SQLException e) {
			logger.error("存储车辆开门运营违规日统计",e);
		}finally{
			try {
				if(rs != null){
					rs.close();
				}
				
				if(stSelectOpenningDoorDay != null){
					stSelectOpenningDoorDay.close();
				}
				
				if(stSaveOpenningDoor != null){
					stSaveOpenningDoor.close();
				}
				
				if (dbCon!=null){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error(e.getMessage(),e);
			}
		}
	}
	
	/****
	 * 存储车辆开门运营违规日统计
	 * utc 当日0点对应utc时间
	 * @throws SQLException 
	 */
	
	public static void saveOpenningDoorDay(OracleConnection dbCon,String vid,long utc) throws Exception{
		PreparedStatement stSaveOpenningDoor = null;
		PreparedStatement stSelectOpenningDoorDay = null;
		ResultSet rs = null;
		
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);

			stSaveOpenningDoor = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOpenningDoorDay"));
			stSelectOpenningDoorDay = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_selectOpenningDoorDay"));
			
			stSelectOpenningDoorDay.setString(1, vid);
			stSelectOpenningDoorDay.setLong(2, utc);
			stSelectOpenningDoorDay.setLong(3, utc + 24 * 60 * 60 * 1000);
			rs = stSelectOpenningDoorDay.executeQuery();
			int count = 0;
			while(rs.next()){
				stSaveOpenningDoor.setString(1, GeneratorPK.instance().getPKString());
				stSaveOpenningDoor.setLong(2, utc + 12 * 60 * 60 * 1000);
				stSaveOpenningDoor.setString(3,vid);
				stSaveOpenningDoor.setString(4,info.getEntId());
				stSaveOpenningDoor.setString(5, info.getEntName());
				stSaveOpenningDoor.setString(6, info.getTeamId());
				stSaveOpenningDoor.setString(7, info.getTeamName());
				stSaveOpenningDoor.setString(8, info.getVehicleNo());
				stSaveOpenningDoor.setString(9, info.getVinCode());
				stSaveOpenningDoor.setInt(10,rs.getInt("NUM"));
				stSaveOpenningDoor.setInt(11,rs.getInt("TIME"));
				String openType = rs.getString("OPENDOOR_TYPE");
				if("2".equals(openType)){ //带速开门 
					stSaveOpenningDoor.setString(12,AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.IDELINGOPENINGDOOR));
					stSaveOpenningDoor.setString(13,ExcConstants.IDELINGOPENINGDOOR);
				}else if("3".equals(openType)){ // 区域内开门
					stSaveOpenningDoor.setString(12,AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.OPENINGDOOR));
					stSaveOpenningDoor.setString(13,ExcConstants.OPENINGDOOR);
				}else if("4".equals(openType)){ // 区域外开门
					stSaveOpenningDoor.setString(12,AnalysisDBAdapter.alarmTypeMap.get(ExcConstants.OUTOPENINGDOOR));
					stSaveOpenningDoor.setString(13,ExcConstants.OUTOPENINGDOOR);
				}else{
					stSaveOpenningDoor.setString(12,null);
					stSaveOpenningDoor.setString(13,null);
				}
				stSaveOpenningDoor.setInt(14,rs.getInt("MILEAGE"));
				stSaveOpenningDoor.setInt(15,rs.getInt("OIL_WEAR"));
				if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
					stSaveOpenningDoor.setString(16, info.getVlineId());
				}else{
					stSaveOpenningDoor.setNull(16, Types.VARCHAR);
				}
				
				if(info.getLineName() != null){
					stSaveOpenningDoor.setString(17, info.getLineName());
				}else{
					stSaveOpenningDoor.setString(17, null);
				}
				
				stSaveOpenningDoor.addBatch();
				count++;
			}//End while
			
			if(count > 0){
				stSaveOpenningDoor.executeBatch();
			}
		} catch (SQLException e) {
			logger.error("存储车辆开门运营违规日统计",e);
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(stSelectOpenningDoorDay != null){
				stSelectOpenningDoorDay.close();
			}
			
			if(stSaveOpenningDoor != null){
				stSaveOpenningDoor.close();
			}
		}
	}
	

	/*****
	 * 存储数据库油量变化记录
	 */
	public static void saveOilChangedMass(String vid,OilMonitorBean2 oilMonitorBean2){
		
		if(oilMonitorBean2!=null&&oilMonitorBean2.getOilMonitor_ls().size()>0){
			OracleConnection dbCon = null;
			PreparedStatement stSaveOilChangedMass = null;
			try {
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stSaveOilChangedMass = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOilChanged"));
				List<OilmassChangedDetail> oilIt = oilMonitorBean2.getOilMonitor_ls();
				for (int i=0;i<oilIt.size();i++){
					OilmassChangedDetail oil = oilIt.get(i);
					if (!"00".equals(oil.getChangeType())){
						stSaveOilChangedMass.setString(1, GeneratorPK.instance().getPKString());
						stSaveOilChangedMass.setString(2, oil.getChangeType());
						stSaveOilChangedMass.setString(3, vid);
						stSaveOilChangedMass.setLong(4, CDate.stringConvertUtc(oil.getGpsTime()));
						stSaveOilChangedMass.setLong(5, oil.getLat());
						stSaveOilChangedMass.setLong(6, oil.getLon());
						stSaveOilChangedMass.setLong(7, oil.getMapLon());
						stSaveOilChangedMass.setLong(8, oil.getMapLat());
						stSaveOilChangedMass.setInt(9, oil.getElevation());
						stSaveOilChangedMass.setInt(10, oil.getDirection());
						stSaveOilChangedMass.setInt(11, oil.getGps_speed());
						stSaveOilChangedMass.setLong(12, System.currentTimeMillis());
						stSaveOilChangedMass.setInt(13, Types.NULL);
						stSaveOilChangedMass.setDouble(14, Math.round(oil.getCurr_oilmass()*2));//进行单位换算 0.1 = 0.05 *2
						stSaveOilChangedMass.setDouble(15, Math.round(oil.getChange_oilmass()*2));//进行单位换算
						stSaveOilChangedMass.addBatch();
					}
				}// End while
				stSaveOilChangedMass.executeBatch();
			} catch (Exception e) {
				logger.error("存储数据库油量变化记录VID=" + vid,e );
			}finally{
				try {
					if(null != stSaveOilChangedMass ){
							stSaveOilChangedMass.close();
					}
					if (dbCon!=null){
						dbCon.close();
					}
				} catch (SQLException e) {
					logger.error(e.getMessage(),e);
				}
			}
			
		}

	}
	
	/****
	 * 存储文件油量变化记录
	 */
	public static void writeOilChangedMassFile(String vid,File oilFile,OilMonitorBean2 oilMonitorBean2){
		if(oilMonitorBean2!=null&&oilMonitorBean2.getOilMonitor_ls().size()>0 ){
			FileWriter fw = null;
			StringBuffer buf = new StringBuffer();
			try {
				fw = new FileWriter(oilFile);
				List<OilmassChangedDetail> ls = oilMonitorBean2.getOilMonitor_ls();
				long lastOil = 0;
				for (int i=0;i<ls.size();i++){
					OilmassChangedDetail oil = ls.get(i);
					//" + oil.getChangeType() + "
					long tmpOil = Math.round(oil.getCurr_oilmass()*2);
					//if (tmpOil!=lastOil){
					buf.append(oil.getLat() + ":" + oil.getLon() + ":" + oil.getElevation() + ":" + oil.getGps_speed() + ":" + oil.getDirection() + ":" + oil.getGpsTime().replaceAll("/", "").substring(2) + ":"+oil.getChangeType()+"::" + Math.round(oil.getChange_oilmass()*2) + ":" + tmpOil + "\r\n");
					//}
					lastOil = tmpOil;
				}// End while
				fw.write(buf.toString());
				fw.flush();
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
	
	/*****
	 * GPS巡检记录
	 */
	public static void saveGpsInspection(String vid,List<VehicleMessageBean> inspectionRecorder){
		if(inspectionRecorder!=null&&inspectionRecorder.size()>0){
			OracleConnection dbCon = null;
			PreparedStatement stGPSInspection = null;
			try {
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stGPSInspection = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveGpsInspection"));
				for (int i=0;i<inspectionRecorder.size();i++){
					VehicleMessageBean po = inspectionRecorder.get(i);

					stGPSInspection.setString(1, UUID.randomUUID().toString().replace("-", ""));
					stGPSInspection.setString(2, vid);
					stGPSInspection.setLong(3, po.getUtc());
					stGPSInspection.setLong(4,po.getSpeed());
					stGPSInspection.setInt(5,po.isGpsState()?1:0);
					stGPSInspection.setLong(6, po.getLat());
					stGPSInspection.setLong(7, po.getLon());
					stGPSInspection.setLong(8, po.getMaplon());
					stGPSInspection.setLong(9, po.getMaplat());
					stGPSInspection.setString(10, GetAddressUtil.getAddress((po.getMaplon()/600000.0)+"" , (po.getMaplat()/600000.0) + ""));
					stGPSInspection.addBatch();

				}// End while
				stGPSInspection.executeBatch();
			} catch (Exception e) {
				logger.error("存储GPS巡检记录出错：VID=" + vid+" ",e );
			}finally{
				try {
					if(null != stGPSInspection ){
							stGPSInspection.close();
					}
					if (dbCon!=null){
							dbCon.close();
					}
				} catch (SQLException e) {
					logger.error(e.getMessage(),e);
				}
			}
			
		}
		
		
	}
	
	
	/**
	 * @description:删除数据（已生成的统计数据）
	 * @param:
	 * @author: cuis
	 * @creatTime:  2013-9-26下午02:35:37
	 * @modifyInformation：
	 */
	public static void deleteStatisticsDatas(Long utc){
		CallableStatement dbCstmt = null;
		Connection dbConnection = null;
		try{
			   dbConnection = OracleConnectionPool.getConnection();
			  if (dbConnection!=null){
				dbCstmt = dbConnection.prepareCall(SQLPool.getinstance().getSql("sql_procDeleteStatisticDatas"));
				dbCstmt.setLong(1,utc); 
				dbCstmt.setLong(2,utc + 12 * 60 * 60 * 1000); 
				dbCstmt.setLong(3,utc + 24 * 60 * 60 * 1000); 
				dbCstmt.registerOutParameter(4, Types.INTEGER); 
				dbCstmt.execute();
			  }
			  int successTag = dbCstmt.getInt(4); 
				
				if (successTag==1){
					logger.debug("删除数据（已生成的统计数据）信息成功！");
				}else{
					logger.debug("删除数据（已生成的统计数据）信息出错!");
				}
     		}catch(SQLException e){
				logger.error(" 删除数据（已生成的统计数据）信息出错!",e);
			}finally{
				try {
					if(dbCstmt != null){
						dbCstmt.close();
					}
					if(dbConnection != null){
						dbConnection.close();
					}
				} catch (SQLException e) {
					logger.error("连接放回连接池出错.",e);
				}
				
			}
	}
	
	/**
	 * 存储报警事件表
	 * @param vehicleMessage
	 * @param alarmCacheBean
	 * @throws SQLException
	 */
	public static void saveVehicleAlarmEvent(String vid,Vector<AlarmCacheBean> alarmList) throws SQLException {
		OracleConnection dbCon = null;
		OraclePreparedStatement stSaveVehicleAlarmEvent=null;
		try {
			if (alarmList != null&&alarmList.size()>0) {
				VehicleInfo vehicleMessage = AnalysisDBAdapter.queryVechileInfo(vid);
				// 从连接池获得连接
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stSaveVehicleAlarmEvent = (OraclePreparedStatement) dbCon
				.prepareStatement(SQLPool.getinstance().getSql("sql_saveVehicleAlarmEventInfo"));
				
				Iterator<AlarmCacheBean> eventList = alarmList.iterator();
				int count = 0;
				while(eventList.hasNext()){
					AlarmCacheBean alarmCacheBean = eventList.next();
					
					String alarmCode = alarmCacheBean.getAlarmcode();

					stSaveVehicleAlarmEvent.setString(1, GeneratorPK.instance().getPKString());
					stSaveVehicleAlarmEvent.setString(2, vid);
					stSaveVehicleAlarmEvent.setString(3,vehicleMessage.getCommaddr());
					stSaveVehicleAlarmEvent.setString(4,alarmCode);

					if (alarmCacheBean.getAreaId() != null && !"".equals(alarmCacheBean.getAreaId())) {
						stSaveVehicleAlarmEvent.setString(5,
								alarmCacheBean.getAreaId());
					} else {
						stSaveVehicleAlarmEvent.setNull(5, Types.VARCHAR);
					}

					if (alarmCacheBean.getMtypeCode() != null) {
						stSaveVehicleAlarmEvent.setString(6,
								alarmCacheBean.getMtypeCode());
					} else {
						stSaveVehicleAlarmEvent.setString(6, null);
					}

					if (alarmCacheBean.getMediaUrl() != null) {
						stSaveVehicleAlarmEvent.setString(7,
								alarmCacheBean.getMediaUrl());
					} else {
						stSaveVehicleAlarmEvent.setString(7, null);
					}

					VehicleMessageBean beginVmb = alarmCacheBean.getBeginVmb();

					stSaveVehicleAlarmEvent.setLong(8, beginVmb.getUtc());
					stSaveVehicleAlarmEvent.setLong(9, beginVmb.getLat());
					stSaveVehicleAlarmEvent.setLong(10, beginVmb.getLon());
					stSaveVehicleAlarmEvent.setLong(11, beginVmb.getMaplat());
					stSaveVehicleAlarmEvent.setLong(12, beginVmb.getMaplon());
					stSaveVehicleAlarmEvent.setInt(13, beginVmb.getElevation());
					if (beginVmb.getDir() != null) {
						stSaveVehicleAlarmEvent.setInt(14, beginVmb.getDir());
					} else {
						stSaveVehicleAlarmEvent.setNull(14, Types.INTEGER);
					}
					stSaveVehicleAlarmEvent.setLong(15, beginVmb.getSpeed());

					VehicleMessageBean endVmb = alarmCacheBean.getEndVmb();

					stSaveVehicleAlarmEvent.setLong(16, endVmb.getUtc());

					if (endVmb.getLat() != null) {
						stSaveVehicleAlarmEvent.setLong(17, endVmb.getLat());
					} else {
						stSaveVehicleAlarmEvent.setNull(17, Types.INTEGER);
					}

					if (endVmb.getLon() != null) {
						stSaveVehicleAlarmEvent.setLong(18, endVmb.getLon());
					} else {
						stSaveVehicleAlarmEvent.setNull(18, Types.INTEGER);
					}
					stSaveVehicleAlarmEvent.setLong(19, endVmb.getMaplat());
					stSaveVehicleAlarmEvent.setLong(20, endVmb.getMaplon());
					stSaveVehicleAlarmEvent.setInt(21, endVmb.getElevation());
					if (endVmb.getDir() != null) {
						stSaveVehicleAlarmEvent.setInt(22, endVmb.getDir());
					} else {
						stSaveVehicleAlarmEvent.setNull(22, Types.INTEGER);
					}
					if (endVmb.getSpeed() != null) {
						stSaveVehicleAlarmEvent.setLong(23, endVmb.getSpeed());
					} else {
						stSaveVehicleAlarmEvent.setNull(23, Types.INTEGER);
					}

					stSaveVehicleAlarmEvent.setDouble(24,
							(endVmb.getUtc() - beginVmb.getUtc()) / 1000);

					if (alarmCacheBean.getMaxSpeed() != -1) {
						stSaveVehicleAlarmEvent.setLong(25,
								alarmCacheBean.getMaxSpeed());
					} else {
						stSaveVehicleAlarmEvent.setNull(25, Types.INTEGER);
					}

					if (alarmCacheBean.getVlineId() != null && !"".equals(alarmCacheBean.getVlineId())) {
						stSaveVehicleAlarmEvent.setString(26,
								alarmCacheBean.getVlineId());
					} else {
						stSaveVehicleAlarmEvent.setNull(26, Types.VARCHAR);
					}
					stSaveVehicleAlarmEvent.setString(27,
							vehicleMessage.getInnerCode());
					stSaveVehicleAlarmEvent.setString(28,
							vehicleMessage.getVehicleNo());

					if (beginVmb.getMileage() >= 0
							&& endVmb.getMileage() >= beginVmb.getMileage()) {
						stSaveVehicleAlarmEvent.setLong(29, endVmb.getMileage()
								- beginVmb.getMileage());
					} else {
						stSaveVehicleAlarmEvent.setNull(29, Types.INTEGER);
					}

					if (endVmb.getOil() >= 0
							&& beginVmb.getOil() >= endVmb.getOil()) {
						stSaveVehicleAlarmEvent.setLong(30, beginVmb.getOil()
								- endVmb.getOil());
					} else {
						stSaveVehicleAlarmEvent.setNull(30, Types.INTEGER);
					}

					stSaveVehicleAlarmEvent.setString(31,
							vehicleMessage.getVinCode());

					if (alarmCacheBean.getLineName() != null) {
						stSaveVehicleAlarmEvent.setString(32,
								alarmCacheBean.getLineName());
					} else {
						stSaveVehicleAlarmEvent.setString(32, null);
					}

					stSaveVehicleAlarmEvent.setString(33,
							vehicleMessage.getEntId());
					stSaveVehicleAlarmEvent.setString(34,
							vehicleMessage.getEntName());
					stSaveVehicleAlarmEvent.setString(35,
							vehicleMessage.getTeamId());
					stSaveVehicleAlarmEvent.setString(36,
							vehicleMessage.getTeamName());
					stSaveVehicleAlarmEvent
							.setInt(37, alarmCacheBean.getAlarmSrc());
					
					if (alarmCacheBean.getSpeedThreshold() > 0) {
						stSaveVehicleAlarmEvent.setDouble(38,
								alarmCacheBean.getSpeedThreshold());
					} else {
						stSaveVehicleAlarmEvent.setNull(38, Types.INTEGER);
					}
					
					if (alarmCacheBean.getAvgSpeed()> 0){
						stSaveVehicleAlarmEvent.setLong(39, alarmCacheBean.getAvgSpeed());
					}else{
						stSaveVehicleAlarmEvent.setNull(39, Types.INTEGER);
					}
					
					stSaveVehicleAlarmEvent.addBatch();
					
				}

				stSaveVehicleAlarmEvent.executeBatch();
			}
		} catch (Exception ex) {
			logger.error("保存告警事件信息出错（ORA）："
					+ ExceptionUtil.getErrorStack(ex, 0));
		} finally {
			if (stSaveVehicleAlarmEvent != null) {
				stSaveVehicleAlarmEvent.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}
	
	/**
	 * 存储报警事件表
	 * @param vehicleMessage
	 * @param alarmCacheBean
	 * @throws SQLException
	 */
	public static void saveVehicleAlarmEvent(OracleConnection dbCon,String vid,Vector<AlarmCacheBean> alarmList) throws SQLException {
		OraclePreparedStatement stSaveVehicleAlarmEvent=null;
		try {
			if (alarmList != null&&alarmList.size()>0) {
				VehicleInfo vehicleMessage = AnalysisDBAdapter.queryVechileInfo(vid);

				stSaveVehicleAlarmEvent = (OraclePreparedStatement) dbCon
				.prepareStatement(SQLPool.getinstance().getSql("sql_saveVehicleAlarmEventInfo"));
				
				Iterator<AlarmCacheBean> eventList = alarmList.iterator();
				int count = 0;
				while(eventList.hasNext()){
					AlarmCacheBean alarmCacheBean = eventList.next();
					
					String alarmCode = alarmCacheBean.getAlarmcode();

					stSaveVehicleAlarmEvent.setString(1, GeneratorPK.instance().getPKString());
					stSaveVehicleAlarmEvent.setString(2, vid);
					stSaveVehicleAlarmEvent.setString(3,vehicleMessage.getCommaddr());
					stSaveVehicleAlarmEvent.setString(4,alarmCode);

					if (alarmCacheBean.getAreaId() != null && !"".equals(alarmCacheBean.getAreaId())) {
						stSaveVehicleAlarmEvent.setString(5,
								alarmCacheBean.getAreaId());
					} else {
						stSaveVehicleAlarmEvent.setNull(5, Types.VARCHAR);
					}

					if (alarmCacheBean.getMtypeCode() != null) {
						stSaveVehicleAlarmEvent.setString(6,
								alarmCacheBean.getMtypeCode());
					} else {
						stSaveVehicleAlarmEvent.setString(6, null);
					}

					if (alarmCacheBean.getMediaUrl() != null) {
						stSaveVehicleAlarmEvent.setString(7,
								alarmCacheBean.getMediaUrl());
					} else {
						stSaveVehicleAlarmEvent.setString(7, null);
					}

					VehicleMessageBean beginVmb = alarmCacheBean.getBeginVmb();

					stSaveVehicleAlarmEvent.setLong(8, beginVmb.getUtc());
					stSaveVehicleAlarmEvent.setLong(9, beginVmb.getLat());
					stSaveVehicleAlarmEvent.setLong(10, beginVmb.getLon());
					stSaveVehicleAlarmEvent.setLong(11, beginVmb.getMaplat());
					stSaveVehicleAlarmEvent.setLong(12, beginVmb.getMaplon());
					stSaveVehicleAlarmEvent.setInt(13, beginVmb.getElevation());
					if (beginVmb.getDir() != null) {
						stSaveVehicleAlarmEvent.setInt(14, beginVmb.getDir());
					} else {
						stSaveVehicleAlarmEvent.setNull(14, Types.INTEGER);
					}
					stSaveVehicleAlarmEvent.setLong(15, beginVmb.getSpeed());

					VehicleMessageBean endVmb = alarmCacheBean.getEndVmb();

					stSaveVehicleAlarmEvent.setLong(16, endVmb.getUtc());

					if (endVmb.getLat() != null) {
						stSaveVehicleAlarmEvent.setLong(17, endVmb.getLat());
					} else {
						stSaveVehicleAlarmEvent.setNull(17, Types.INTEGER);
					}

					if (endVmb.getLon() != null) {
						stSaveVehicleAlarmEvent.setLong(18, endVmb.getLon());
					} else {
						stSaveVehicleAlarmEvent.setNull(18, Types.INTEGER);
					}
					stSaveVehicleAlarmEvent.setLong(19, endVmb.getMaplat());
					stSaveVehicleAlarmEvent.setLong(20, endVmb.getMaplon());
					stSaveVehicleAlarmEvent.setInt(21, endVmb.getElevation());
					if (endVmb.getDir() != null) {
						stSaveVehicleAlarmEvent.setInt(22, endVmb.getDir());
					} else {
						stSaveVehicleAlarmEvent.setNull(22, Types.INTEGER);
					}
					if (endVmb.getSpeed() != null) {
						stSaveVehicleAlarmEvent.setLong(23, endVmb.getSpeed());
					} else {
						stSaveVehicleAlarmEvent.setNull(23, Types.INTEGER);
					}

					stSaveVehicleAlarmEvent.setDouble(24,
							(endVmb.getUtc() - beginVmb.getUtc()) / 1000);

					if (alarmCacheBean.getMaxSpeed() != -1) {
						stSaveVehicleAlarmEvent.setLong(25,
								alarmCacheBean.getMaxSpeed());
					} else {
						stSaveVehicleAlarmEvent.setNull(25, Types.INTEGER);
					}

					if (alarmCacheBean.getVlineId() != null && !"".equals(alarmCacheBean.getVlineId())) {
						stSaveVehicleAlarmEvent.setString(26,
								alarmCacheBean.getVlineId());
					} else {
						stSaveVehicleAlarmEvent.setNull(26, Types.VARCHAR);
					}
					stSaveVehicleAlarmEvent.setString(27,
							vehicleMessage.getInnerCode());
					stSaveVehicleAlarmEvent.setString(28,
							vehicleMessage.getVehicleNo());

					if (beginVmb.getMileage() >= 0
							&& endVmb.getMileage() >= beginVmb.getMileage()) {
						stSaveVehicleAlarmEvent.setLong(29, endVmb.getMileage()
								- beginVmb.getMileage());
					} else {
						stSaveVehicleAlarmEvent.setNull(29, Types.INTEGER);
					}

					if (endVmb.getOil() >= 0
							&& beginVmb.getOil() >= endVmb.getOil()) {
						stSaveVehicleAlarmEvent.setLong(30, beginVmb.getOil()
								- endVmb.getOil());
					} else {
						stSaveVehicleAlarmEvent.setNull(30, Types.INTEGER);
					}

					stSaveVehicleAlarmEvent.setString(31,
							vehicleMessage.getVinCode());

					if (alarmCacheBean.getLineName() != null) {
						stSaveVehicleAlarmEvent.setString(32,
								alarmCacheBean.getLineName());
					} else {
						stSaveVehicleAlarmEvent.setString(32, null);
					}

					stSaveVehicleAlarmEvent.setString(33,
							vehicleMessage.getEntId());
					stSaveVehicleAlarmEvent.setString(34,
							vehicleMessage.getEntName());
					stSaveVehicleAlarmEvent.setString(35,
							vehicleMessage.getTeamId());
					stSaveVehicleAlarmEvent.setString(36,
							vehicleMessage.getTeamName());
					stSaveVehicleAlarmEvent
							.setInt(37, alarmCacheBean.getAlarmSrc());
					
					if (alarmCacheBean.getSpeedThreshold() > 0) {
						stSaveVehicleAlarmEvent.setDouble(38,
								alarmCacheBean.getSpeedThreshold());
					} else {
						stSaveVehicleAlarmEvent.setNull(38, Types.INTEGER);
					}
					
					if (alarmCacheBean.getAvgSpeed()> 0){
						stSaveVehicleAlarmEvent.setLong(39, alarmCacheBean.getAvgSpeed());
					}else{
						stSaveVehicleAlarmEvent.setNull(39, Types.INTEGER);
					}
					
					stSaveVehicleAlarmEvent.setString(40, beginVmb.getDriverId());
					stSaveVehicleAlarmEvent.setString(41, beginVmb.getDriverName());
					stSaveVehicleAlarmEvent.setString(42, beginVmb.getDriverSrc());
					
					stSaveVehicleAlarmEvent.addBatch();
					
				}

				stSaveVehicleAlarmEvent.executeBatch();
			}
		} catch (Exception ex) {
			logger.error("保存告警事件信息出错（ORA）："
					+ ExceptionUtil.getErrorStack(ex, 0));
		} finally {
			if (stSaveVehicleAlarmEvent != null) {
				stSaveVehicleAlarmEvent.close();
			}
		}
	}
	
	
	/*****
	 * 保存速度异常数据记录
	 * INSERT INTO TH_VEHICLE_SPEED_ANOMALOUS
	 * (STAT_DATE,VID,VEHICLE_NO,VSS_SPEED_AVG,GPS_SPEED_AVG,SPEED_DIFFERENCE,SPEED_FROM)
	 * VALUES ( ?, ?, ?, ?, ?, ?, ?)
	 */
	public static void  saveSpeedAnomalous(ThVehicleSpeedAnomalous thVehicleSpeedAnomalous){
		if (thVehicleSpeedAnomalous!=null&&thVehicleSpeedAnomalous.getCount()>0){
			OracleConnection dbCon = null;
			PreparedStatement stSpeedAnomalous = null;
			try {
				//判断vss、gps差值是否符合要求
				long vss = thVehicleSpeedAnomalous.getVssSpeedAvg();
				long gps = thVehicleSpeedAnomalous.getGpsSpeedAvg();
				long diff = Math.abs(gps - vss);
				if (diff>100){
					String vid = thVehicleSpeedAnomalous.getVid();
					VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
					dbCon = (OracleConnection) OracleConnectionPool.getConnection();
					
					stSpeedAnomalous = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_vehicleSpeedAnomalous"));
					
					stSpeedAnomalous.setLong(1, thVehicleSpeedAnomalous.getStatDate());
					stSpeedAnomalous.setString(2, vid);
					
					if(info != null){
						stSpeedAnomalous.setString(3, info.getEntId()); // 
						stSpeedAnomalous.setString(4, info.getVehicleNo()); // 车牌号
					} else {
						stSpeedAnomalous.setNull(3, Types.NULL); // 
						stSpeedAnomalous.setNull(4, Types.NULL); // 车牌号
					}

					stSpeedAnomalous.setLong(5, vss);
					stSpeedAnomalous.setLong(6,gps);
					stSpeedAnomalous.setLong(7,diff);
					stSpeedAnomalous.setString(8, thVehicleSpeedAnomalous.getSpeedForm());
					stSpeedAnomalous.executeUpdate();
				}

			} catch (Exception e) {
				logger.error("存储车辆速度异常记录出错：VID=" + thVehicleSpeedAnomalous.getVid() +" ",e );
			}finally{
				try {
					if(null != stSpeedAnomalous ){
						stSpeedAnomalous.close();
					}
					if(null != dbCon ){
						dbCon.close();
					}
				} catch (SQLException e) {
					logger.error("将连接放回连接池出错：",e);
				}
			}
		}
	}
	
	/*****
	 * 保存速度异常数据记录
	 * INSERT INTO TH_VEHICLE_SPEED_ANOMALOUS
	 * (STAT_DATE,VID,VEHICLE_NO,VSS_SPEED_AVG,GPS_SPEED_AVG,SPEED_DIFFERENCE,SPEED_FROM)
	 * VALUES ( ?, ?, ?, ?, ?, ?, ?)
	 */
	public static void  saveSpeedAnomalous(OracleConnection dbCon,ThVehicleSpeedAnomalous thVehicleSpeedAnomalous)throws Exception{
		if (thVehicleSpeedAnomalous!=null&&thVehicleSpeedAnomalous.getCount()>0){
			PreparedStatement stSpeedAnomalous = null;
			try {
				//判断vss、gps差值是否符合要求
				long vss = thVehicleSpeedAnomalous.getVssSpeedAvg();
				long gps = thVehicleSpeedAnomalous.getGpsSpeedAvg();
				long diff = Math.abs(gps - vss);
				if (diff>100){
					String vid = thVehicleSpeedAnomalous.getVid();
					VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
					
					stSpeedAnomalous = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_vehicleSpeedAnomalous"));
					
					stSpeedAnomalous.setLong(1, thVehicleSpeedAnomalous.getStatDate());
					stSpeedAnomalous.setString(2, vid);
					
					if(info != null){
						stSpeedAnomalous.setString(3, info.getEntId()); // 
						stSpeedAnomalous.setString(4, info.getVehicleNo()); // 车牌号
					} else {
						stSpeedAnomalous.setNull(3, Types.NULL); // 
						stSpeedAnomalous.setNull(4, Types.NULL); // 车牌号
					}

					stSpeedAnomalous.setLong(5, vss);
					stSpeedAnomalous.setLong(6,gps);
					stSpeedAnomalous.setLong(7,diff);
					stSpeedAnomalous.setString(8, thVehicleSpeedAnomalous.getSpeedForm());
					stSpeedAnomalous.executeUpdate();
				}

			} catch (Exception e) {
				logger.error("存储车辆速度异常记录出错：VID=" + thVehicleSpeedAnomalous.getVid() +" ",e );
			}finally{

				if(null != stSpeedAnomalous ){
					stSpeedAnomalous.close();
				}
			}
		}
	}
	
	/**
	 * 存储非法营运事件统计信息
	 * @param alarmEventList
	 * @throws SQLException
	 */
	public static void saveOutLineEventInfo(String vid,Vector<AlarmCacheBean> vAlarmEventList) throws SQLException{
		OracleConnection dbCon = null;
		PreparedStatement stSaveVehicleOutLineEventInfo = null;
		VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
		try{
			if (vAlarmEventList!=null&&vAlarmEventList.size()>0){
				
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveVehicleOutLineEventInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOutLineEventInfo"));
			
			Iterator<AlarmCacheBean> alarmEventIt = vAlarmEventList.iterator();
			
			while(alarmEventIt.hasNext()){
				AlarmCacheBean event = alarmEventIt.next();
				String alarmCode =  event.getAlarmcode();
				
				if(!AnalysisDBAdapter.alarmTypeMap.containsKey(alarmCode)){
					continue;
				}
				stSaveVehicleOutLineEventInfo.setString(1, GeneratorPK.instance().getPKString());
				stSaveVehicleOutLineEventInfo.setString(2, vid);
				stSaveVehicleOutLineEventInfo.setString(3, info.getCommaddr());
				stSaveVehicleOutLineEventInfo.setString(4, event.getAlarmcode());
				
				if(event.getAreaId() != null && !"".equals(event.getAreaId())){
					stSaveVehicleOutLineEventInfo.setString(5, event.getAreaId());
				}else{
					stSaveVehicleOutLineEventInfo.setNull(5, Types.VARCHAR);
				}
				
				if(event.getMtypeCode() != null){
					stSaveVehicleOutLineEventInfo.setString(6, event.getMtypeCode());
				}else{
					stSaveVehicleOutLineEventInfo.setString(6, null);
				}
				
				if(event.getMediaUrl() != null){
					stSaveVehicleOutLineEventInfo.setString(7, event.getMediaUrl());
				}else{
					stSaveVehicleOutLineEventInfo.setString(7, null);
				}
				
				VehicleMessageBean beginBean =event.getBeginVmb();
				
				if(beginBean.getUtc() != -1){
					stSaveVehicleOutLineEventInfo.setLong(8, beginBean.getUtc());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(8, 0);
				}
				
				if(beginBean.getLat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(9,beginBean.getLat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(9,0);
				}
				
				if( beginBean.getLon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(10, beginBean.getLon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(10, 0);
				}
				
				if(beginBean.getMaplat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(11, beginBean.getMaplat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(11, 0);
				}
				
				if(beginBean.getMaplon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(12, beginBean.getMaplon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(12, 0);
				}
				
				if(beginBean.getElevation() != -1){
					stSaveVehicleOutLineEventInfo.setInt(13, beginBean.getElevation());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(13, 0);
				}
				
				if(beginBean.getDir() != -1){
					stSaveVehicleOutLineEventInfo.setInt(14, beginBean.getDir());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(14, 0);
				}
				
				if(beginBean.getSpeed() != -1){
					stSaveVehicleOutLineEventInfo.setLong(15, beginBean.getSpeed());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(15, 0);
				}
				
				VehicleMessageBean endBean = event.getEndVmb();
				
				if( endBean.getUtc() != -1){
					stSaveVehicleOutLineEventInfo.setLong(16, endBean.getUtc());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(16, 0);
				}
				
				if(endBean.getLat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(17,endBean.getLat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(17,0);
				}
				
				if(endBean.getLon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(18, endBean.getLon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(18, 0);
				}
				
				if(endBean.getMaplat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(19, endBean.getMaplat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(19, 0);
				}
				
				if(endBean.getMaplon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(20, endBean.getMaplon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(20, 0);
				}
				
				if(endBean.getElevation() != -1){
					stSaveVehicleOutLineEventInfo.setInt(21, endBean.getElevation());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(21, 0);
				}
				
				if( endBean.getDir() != -1){
					stSaveVehicleOutLineEventInfo.setInt(22, endBean.getDir());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(22, 0);
				}
				
				if(endBean.getSpeed() != -1){
					stSaveVehicleOutLineEventInfo.setLong(23, endBean.getSpeed());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(23, 0);
				}
				long diffTime = (endBean.getUtc() - beginBean.getUtc())/1000;
				if(diffTime > -1){
					stSaveVehicleOutLineEventInfo.setDouble(24, diffTime);
				}else{
					stSaveVehicleOutLineEventInfo.setNull(24, Types.INTEGER);
				}
				
				if(event.getMaxSpeed() != -1){
					stSaveVehicleOutLineEventInfo.setLong(25, event.getMaxSpeed());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(25, 0);
				}
				
				if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
					stSaveVehicleOutLineEventInfo.setString(26, info.getVlineId());
				}else{
					stSaveVehicleOutLineEventInfo.setNull(26, Types.VARCHAR);
				}
				stSaveVehicleOutLineEventInfo.setString(27, info.getInnerCode());
				stSaveVehicleOutLineEventInfo.setString(28, info.getVehicleNo());
				stSaveVehicleOutLineEventInfo.setString(29, info.getVinCode());
				
				if(info.getLineName() != null){
					stSaveVehicleOutLineEventInfo.setString(30, info.getLineName());
				}else{
					stSaveVehicleOutLineEventInfo.setString(30, null);
				}
				
				stSaveVehicleOutLineEventInfo.setString(31, info.getEntId());
				stSaveVehicleOutLineEventInfo.setString(32, info.getEntName());
				stSaveVehicleOutLineEventInfo.setString(33, info.getTeamId());
				stSaveVehicleOutLineEventInfo.setString(34, info.getTeamName());
				stSaveVehicleOutLineEventInfo.executeUpdate();
			}// End while
			
			//stSaveVehicleOutLineEventInfo.executeBatch();
			}	
		}catch(SQLException e){
			logger.error("存储非法营运事件统计信息出错:VID="+vid+" ",e);
		}finally{
			try{
				vAlarmEventList.clear();
				if(stSaveVehicleOutLineEventInfo != null){
					stSaveVehicleOutLineEventInfo.close();
				}
				if(null != dbCon ){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("将连接放回连接池出错：",e);
			}
		}
	}
	
	/**
	 * 存储非法营运事件统计信息
	 * @param alarmEventList
	 * @throws SQLException
	 */
	public static void saveOutLineEventInfo(OracleConnection dbCon,String vid,Vector<AlarmCacheBean> vAlarmEventList) throws Exception{
		PreparedStatement stSaveVehicleOutLineEventInfo = null;
		VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
		try{
			if (vAlarmEventList!=null&&vAlarmEventList.size()>0){

			stSaveVehicleOutLineEventInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOutLineEventInfo"));
			
			Iterator<AlarmCacheBean> alarmEventIt = vAlarmEventList.iterator();
			
			while(alarmEventIt.hasNext()){
				AlarmCacheBean event = alarmEventIt.next();
				String alarmCode =  event.getAlarmcode();
				
				if(!AnalysisDBAdapter.alarmTypeMap.containsKey(alarmCode)){
					continue;
				}
				stSaveVehicleOutLineEventInfo.setString(1, GeneratorPK.instance().getPKString());
				stSaveVehicleOutLineEventInfo.setString(2, vid);
				stSaveVehicleOutLineEventInfo.setString(3, info.getCommaddr());
				stSaveVehicleOutLineEventInfo.setString(4, event.getAlarmcode());
				
				if(event.getAreaId() != null && !"".equals(event.getAreaId())){
					stSaveVehicleOutLineEventInfo.setString(5, event.getAreaId());
				}else{
					stSaveVehicleOutLineEventInfo.setNull(5, Types.VARCHAR);
				}
				
				if(event.getMtypeCode() != null){
					stSaveVehicleOutLineEventInfo.setString(6, event.getMtypeCode());
				}else{
					stSaveVehicleOutLineEventInfo.setString(6, null);
				}
				
				if(event.getMediaUrl() != null){
					stSaveVehicleOutLineEventInfo.setString(7, event.getMediaUrl());
				}else{
					stSaveVehicleOutLineEventInfo.setString(7, null);
				}
				
				VehicleMessageBean beginBean =event.getBeginVmb();
				
				if(beginBean.getUtc() != -1){
					stSaveVehicleOutLineEventInfo.setLong(8, beginBean.getUtc());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(8, 0);
				}
				
				if(beginBean.getLat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(9,beginBean.getLat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(9,0);
				}
				
				if( beginBean.getLon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(10, beginBean.getLon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(10, 0);
				}
				
				if(beginBean.getMaplat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(11, beginBean.getMaplat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(11, 0);
				}
				
				if(beginBean.getMaplon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(12, beginBean.getMaplon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(12, 0);
				}
				
				if(beginBean.getElevation() != -1){
					stSaveVehicleOutLineEventInfo.setInt(13, beginBean.getElevation());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(13, 0);
				}
				
				if(beginBean.getDir() != -1){
					stSaveVehicleOutLineEventInfo.setInt(14, beginBean.getDir());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(14, 0);
				}
				
				if(beginBean.getSpeed() != -1){
					stSaveVehicleOutLineEventInfo.setLong(15, beginBean.getSpeed());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(15, 0);
				}
				
				VehicleMessageBean endBean = event.getEndVmb();
				
				if( endBean.getUtc() != -1){
					stSaveVehicleOutLineEventInfo.setLong(16, endBean.getUtc());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(16, 0);
				}
				
				if(endBean.getLat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(17,endBean.getLat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(17,0);
				}
				
				if(endBean.getLon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(18, endBean.getLon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(18, 0);
				}
				
				if(endBean.getMaplat() != -1){
					stSaveVehicleOutLineEventInfo.setLong(19, endBean.getMaplat());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(19, 0);
				}
				
				if(endBean.getMaplon() != -1){
					stSaveVehicleOutLineEventInfo.setLong(20, endBean.getMaplon());
				}else{
					stSaveVehicleOutLineEventInfo.setLong(20, 0);
				}
				
				if(endBean.getElevation() != -1){
					stSaveVehicleOutLineEventInfo.setInt(21, endBean.getElevation());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(21, 0);
				}
				
				if( endBean.getDir() != -1){
					stSaveVehicleOutLineEventInfo.setInt(22, endBean.getDir());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(22, 0);
				}
				
				if(endBean.getSpeed() != -1){
					stSaveVehicleOutLineEventInfo.setLong(23, endBean.getSpeed());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(23, 0);
				}
				long diffTime = (endBean.getUtc() - beginBean.getUtc())/1000;
				if(diffTime > -1){
					stSaveVehicleOutLineEventInfo.setDouble(24, diffTime);
				}else{
					stSaveVehicleOutLineEventInfo.setNull(24, Types.INTEGER);
				}
				
				if(event.getMaxSpeed() != -1){
					stSaveVehicleOutLineEventInfo.setLong(25, event.getMaxSpeed());
				}else{
					stSaveVehicleOutLineEventInfo.setInt(25, 0);
				}
				
				if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
					stSaveVehicleOutLineEventInfo.setString(26, info.getVlineId());
				}else{
					stSaveVehicleOutLineEventInfo.setNull(26, Types.VARCHAR);
				}
				stSaveVehicleOutLineEventInfo.setString(27, info.getInnerCode());
				stSaveVehicleOutLineEventInfo.setString(28, info.getVehicleNo());
				stSaveVehicleOutLineEventInfo.setString(29, info.getVinCode());
				
				if(info.getLineName() != null){
					stSaveVehicleOutLineEventInfo.setString(30, info.getLineName());
				}else{
					stSaveVehicleOutLineEventInfo.setString(30, null);
				}
				
				stSaveVehicleOutLineEventInfo.setString(31, info.getEntId());
				stSaveVehicleOutLineEventInfo.setString(32, info.getEntName());
				stSaveVehicleOutLineEventInfo.setString(33, info.getTeamId());
				stSaveVehicleOutLineEventInfo.setString(34, info.getTeamName());
				
				stSaveVehicleOutLineEventInfo.executeUpdate();
			}// End while
			
			//stSaveVehicleOutLineEventInfo.executeBatch();
			}	
		}catch(SQLException e){
			logger.error("存储非法营运事件统计信息出错:VID="+vid+" ",e);
		}finally{
				vAlarmEventList.clear();
				if(stSaveVehicleOutLineEventInfo != null){
					stSaveVehicleOutLineEventInfo.close();
				}
		}
	}
	
	/*****
	 * 汇总统计超员
	 * utc 当日0点对应utc时间
	 * @throws SQLException
	 */
	public static void saveVehicleIsOverLoad(String vid,long utc) throws SQLException{
		OracleConnection dbCon = null;
		PreparedStatement stSaveVehicleOutLineEventInfo = null;
		PreparedStatement stQueryVehicleMedia = null;
		ResultSet rs = null;
		try{
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryVehicleMedia = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_queryVehicleIsOverLoad"));
			stQueryVehicleMedia.setString(1, vid);
			stQueryVehicleMedia.setLong(2, utc);
			stQueryVehicleMedia.setLong(3, utc + 24 * 60 * 60 * 1000);
			rs = stQueryVehicleMedia.executeQuery();
			stSaveVehicleOutLineEventInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOutLineEventInfo"));
			while(rs.next()){
				stSaveVehicleOutLineEventInfo.setString(1, GeneratorPK.instance().getPKString());
				stSaveVehicleOutLineEventInfo.setString(2, vid);
				stSaveVehicleOutLineEventInfo.setString(3, info.getCommaddr());
				stSaveVehicleOutLineEventInfo.setString(4, ExcConstants.OVERLOAD_CODE);
				
				if(rs.getString("MULT_MEDIA_ID") != null){
					stSaveVehicleOutLineEventInfo.setString(5, rs.getString("MULT_MEDIA_ID") );
				}else{
					stSaveVehicleOutLineEventInfo.setString(5, null);
				}
				
				if(rs.getString("MTYPE_CODE") != null){
					stSaveVehicleOutLineEventInfo.setString(6, rs.getString("MTYPE_CODE"));
				}else{
					stSaveVehicleOutLineEventInfo.setString(6, null);
				}
				
				if(rs.getString("MEDIA_URI") != null){
					stSaveVehicleOutLineEventInfo.setString(7, rs.getString("MEDIA_URI"));
				}else{
					stSaveVehicleOutLineEventInfo.setString(7, null);
				}
				
				stSaveVehicleOutLineEventInfo.setLong(8, rs.getLong("UTC"));
				stSaveVehicleOutLineEventInfo.setLong(9,rs.getLong("LAT"));
				stSaveVehicleOutLineEventInfo.setLong(10, rs.getLong("LON"));
				stSaveVehicleOutLineEventInfo.setLong(11, rs.getLong("MAPLAT"));
				stSaveVehicleOutLineEventInfo.setLong(12, rs.getLong("MAPLON"));
				stSaveVehicleOutLineEventInfo.setInt(13, rs.getInt("ELEVATION"));
				stSaveVehicleOutLineEventInfo.setInt(14, rs.getInt("DIRECTION"));
				stSaveVehicleOutLineEventInfo.setInt(15, rs.getInt("GPS_SPEED"));
				stSaveVehicleOutLineEventInfo.setLong(16, rs.getLong("UTC"));
				stSaveVehicleOutLineEventInfo.setLong(17,rs.getLong("LAT"));
				stSaveVehicleOutLineEventInfo.setLong(18, rs.getLong("LON"));
				stSaveVehicleOutLineEventInfo.setLong(19, rs.getLong("MAPLAT"));
				stSaveVehicleOutLineEventInfo.setLong(20, rs.getLong("MAPLON"));
				stSaveVehicleOutLineEventInfo.setInt(21, rs.getInt("ELEVATION"));
				stSaveVehicleOutLineEventInfo.setInt(22, rs.getInt("DIRECTION"));
				stSaveVehicleOutLineEventInfo.setInt(23, rs.getInt("GPS_SPEED"));
				stSaveVehicleOutLineEventInfo.setDouble(24, 0);
				stSaveVehicleOutLineEventInfo.setInt(25, rs.getInt("GPS_SPEED"));
				
				if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
					stSaveVehicleOutLineEventInfo.setString(26, info.getVlineId());
				}else{
					stSaveVehicleOutLineEventInfo.setNull(26, Types.VARCHAR);
				}
				stSaveVehicleOutLineEventInfo.setString(27, info.getInnerCode());
				stSaveVehicleOutLineEventInfo.setString(28, info.getVehicleNo());
				stSaveVehicleOutLineEventInfo.setString(29, info.getVinCode());
				
				if(info.getLineName() != null){
					stSaveVehicleOutLineEventInfo.setString(30, info.getLineName());
				}else{
					stSaveVehicleOutLineEventInfo.setString(30, null);
				}
				
				stSaveVehicleOutLineEventInfo.setString(31, info.getEntId());
				stSaveVehicleOutLineEventInfo.setString(32, info.getEntName());
				stSaveVehicleOutLineEventInfo.setString(33, info.getTeamId());
				stSaveVehicleOutLineEventInfo.setString(34, info.getTeamName());
				stSaveVehicleOutLineEventInfo.addBatch();
			}
			stSaveVehicleOutLineEventInfo.executeBatch();
			
		}catch(SQLException e){
			logger.error("存储日非法营运统计信息出错：VID="+vid+" ",e);
		}finally{
			try{
				if(rs != null){
					rs.close();
				}
				
				if(stQueryVehicleMedia != null){
					stQueryVehicleMedia.close();
				}
				
				if(stSaveVehicleOutLineEventInfo != null){
					stSaveVehicleOutLineEventInfo.close();
				}
				if(null != dbCon ){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("将连接放回连接池出错：",e);
			}
		}
	}
	
	/*****
	 * 汇总统计超员
	 * utc 当日0点对应utc时间
	 * @throws SQLException
	 */
	public static void saveVehicleIsOverLoad(OracleConnection dbCon,String vid,long utc) throws Exception{
		PreparedStatement stSaveVehicleOutLineEventInfo = null;
		PreparedStatement stQueryVehicleMedia = null;
		ResultSet rs = null;
		try{
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stQueryVehicleMedia = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_queryVehicleIsOverLoad"));
			stQueryVehicleMedia.setString(1, vid);
			stQueryVehicleMedia.setLong(2, utc);
			stQueryVehicleMedia.setLong(3, utc + 24 * 60 * 60 * 1000);
			rs = stQueryVehicleMedia.executeQuery();
			stSaveVehicleOutLineEventInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOutLineEventInfo"));
			while(rs.next()){
				stSaveVehicleOutLineEventInfo.setString(1, GeneratorPK.instance().getPKString());
				stSaveVehicleOutLineEventInfo.setString(2, vid);
				stSaveVehicleOutLineEventInfo.setString(3, info.getCommaddr());
				stSaveVehicleOutLineEventInfo.setString(4, ExcConstants.OVERLOAD_CODE);
				
				if(rs.getString("MULT_MEDIA_ID") != null){
					stSaveVehicleOutLineEventInfo.setString(5, rs.getString("MULT_MEDIA_ID") );
				}else{
					stSaveVehicleOutLineEventInfo.setString(5, null);
				}
				
				if(rs.getString("MTYPE_CODE") != null){
					stSaveVehicleOutLineEventInfo.setString(6, rs.getString("MTYPE_CODE"));
				}else{
					stSaveVehicleOutLineEventInfo.setString(6, null);
				}
				
				if(rs.getString("MEDIA_URI") != null){
					stSaveVehicleOutLineEventInfo.setString(7, rs.getString("MEDIA_URI"));
				}else{
					stSaveVehicleOutLineEventInfo.setString(7, null);
				}
				
				stSaveVehicleOutLineEventInfo.setLong(8, rs.getLong("UTC"));
				stSaveVehicleOutLineEventInfo.setLong(9,rs.getLong("LAT"));
				stSaveVehicleOutLineEventInfo.setLong(10, rs.getLong("LON"));
				stSaveVehicleOutLineEventInfo.setLong(11, rs.getLong("MAPLAT"));
				stSaveVehicleOutLineEventInfo.setLong(12, rs.getLong("MAPLON"));
				stSaveVehicleOutLineEventInfo.setInt(13, rs.getInt("ELEVATION"));
				stSaveVehicleOutLineEventInfo.setInt(14, rs.getInt("DIRECTION"));
				stSaveVehicleOutLineEventInfo.setInt(15, rs.getInt("GPS_SPEED"));
				stSaveVehicleOutLineEventInfo.setLong(16, rs.getLong("UTC"));
				stSaveVehicleOutLineEventInfo.setLong(17,rs.getLong("LAT"));
				stSaveVehicleOutLineEventInfo.setLong(18, rs.getLong("LON"));
				stSaveVehicleOutLineEventInfo.setLong(19, rs.getLong("MAPLAT"));
				stSaveVehicleOutLineEventInfo.setLong(20, rs.getLong("MAPLON"));
				stSaveVehicleOutLineEventInfo.setInt(21, rs.getInt("ELEVATION"));
				stSaveVehicleOutLineEventInfo.setInt(22, rs.getInt("DIRECTION"));
				stSaveVehicleOutLineEventInfo.setInt(23, rs.getInt("GPS_SPEED"));
				stSaveVehicleOutLineEventInfo.setDouble(24, 0);
				stSaveVehicleOutLineEventInfo.setInt(25, rs.getInt("GPS_SPEED"));
				
				if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
					stSaveVehicleOutLineEventInfo.setString(26, info.getVlineId());
				}else{
					stSaveVehicleOutLineEventInfo.setNull(26, Types.VARCHAR);
				}
				stSaveVehicleOutLineEventInfo.setString(27, info.getInnerCode());
				stSaveVehicleOutLineEventInfo.setString(28, info.getVehicleNo());
				stSaveVehicleOutLineEventInfo.setString(29, info.getVinCode());
				
				if(info.getLineName() != null){
					stSaveVehicleOutLineEventInfo.setString(30, info.getLineName());
				}else{
					stSaveVehicleOutLineEventInfo.setString(30, null);
				}
				
				stSaveVehicleOutLineEventInfo.setString(31, info.getEntId());
				stSaveVehicleOutLineEventInfo.setString(32, info.getEntName());
				stSaveVehicleOutLineEventInfo.setString(33, info.getTeamId());
				stSaveVehicleOutLineEventInfo.setString(34, info.getTeamName());
				
				stSaveVehicleOutLineEventInfo.addBatch();
			}
			stSaveVehicleOutLineEventInfo.executeBatch();
			
		}catch(SQLException e){
			logger.error("存储日非法营运统计信息出错：VID="+vid+" ",e);
		}finally{
				if(rs != null){
					rs.close();
				}
				
				if(stQueryVehicleMedia != null){
					stQueryVehicleMedia.close();
				}
				
				if(stSaveVehicleOutLineEventInfo != null){
					stSaveVehicleOutLineEventInfo.close();
				}
		}
	}
	
	/**
	 * 存储日统计非法营运信息
	 * @param info
	 * @param alarmMap
	 * @param vid
	 * @throws SQLException
	 */
	/**
	 * @throws SQLException
	 */
	public static void saveOutLineInfo(String vid,long utc) throws SQLException{
		OracleConnection dbCon = null;
		PreparedStatement stSaveVehicleOutLineInfo = null;
		PreparedStatement stSelectOutlineEvent = null;
		
		ResultSet rs = null;
		
		VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
		try{
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveVehicleOutLineInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOutLineInfo"));
			stSelectOutlineEvent = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_selectOutLineEvent"));
			stSelectOutlineEvent.setString(1, vid);
			stSelectOutlineEvent.setLong(2, utc);
			stSelectOutlineEvent.setLong(3, utc + 24*60*60*1000);
			rs = stSelectOutlineEvent.executeQuery();
			while(rs.next()){
				stSaveVehicleOutLineInfo.setString(1, GeneratorPK.instance().getPKString());
				stSaveVehicleOutLineInfo.setLong(2, utc + 12 * 60 * 60 * 1000);
				stSaveVehicleOutLineInfo.setString(3, vid);
				stSaveVehicleOutLineInfo.setString(4, info.getEntId());
				stSaveVehicleOutLineInfo.setString(5, info.getEntName());
				stSaveVehicleOutLineInfo.setString(6, info.getTeamId());
				stSaveVehicleOutLineInfo.setString(7, info.getTeamName());
				stSaveVehicleOutLineInfo.setString(8, info.getVehicleNo());
				stSaveVehicleOutLineInfo.setString(9,info.getVinCode());
				String outLine_code = rs.getString("OUTLINE_CODE");
				stSaveVehicleOutLineInfo.setString(10,outLine_code);
				stSaveVehicleOutLineInfo.setInt(11, rs.getInt("NUM"));
				stSaveVehicleOutLineInfo.setString(12, AnalysisDBAdapter.alarmTypeMap.get(outLine_code));
				stSaveVehicleOutLineInfo.setLong(13, rs.getLong("TIME"));
				if(info.getVlineId() != null && !"".equals(info.getVlineId())){
					stSaveVehicleOutLineInfo.setString(14, info.getVlineId());
				}else{
					stSaveVehicleOutLineInfo.setNull(14, Types.VARCHAR);
				}
				
				if(info.getLineName() != null){
					stSaveVehicleOutLineInfo.setString(15, info.getLineName());
				}else{
					stSaveVehicleOutLineInfo.setString(15, null);
				}
				stSaveVehicleOutLineInfo.addBatch();
			}
		
			stSaveVehicleOutLineInfo.executeBatch();
			
		}catch(SQLException e){
			logger.error("存储日非法营运统计信息出错:VID="+vid +" " ,e);
		}finally{
			try{
				if(rs != null){
					rs.close();
				}
				
				if(stSelectOutlineEvent != null){
					stSelectOutlineEvent.close();
				}
				
				if(stSaveVehicleOutLineInfo != null){
					stSaveVehicleOutLineInfo.close();
				}
				if(null != dbCon ){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("将连接放回连接池出错：",e);
			}
		}
	}
	
	/**
	 * 存储日统计非法营运信息
	 * @param info
	 * @param alarmMap
	 * @param vid
	 * @throws SQLException
	 */
	/**
	 * @throws SQLException
	 */
	public static void saveOutLineInfo(OracleConnection dbCon,String vid,long utc) throws Exception{
		PreparedStatement stSaveVehicleOutLineInfo = null;
		PreparedStatement stSelectOutlineEvent = null;
		
		ResultSet rs = null;
		
		VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
		try{
			stSaveVehicleOutLineInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOutLineInfo"));
			stSelectOutlineEvent = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_selectOutLineEvent"));
			stSelectOutlineEvent.setString(1, vid);
			stSelectOutlineEvent.setLong(2, utc);
			stSelectOutlineEvent.setLong(3, utc + 24*60*60*1000);
			rs = stSelectOutlineEvent.executeQuery();
			while(rs.next()){
				stSaveVehicleOutLineInfo.setString(1, GeneratorPK.instance().getPKString());
				stSaveVehicleOutLineInfo.setLong(2, utc + 12 * 60 * 60 * 1000);
				stSaveVehicleOutLineInfo.setString(3, vid);
				stSaveVehicleOutLineInfo.setString(4, info.getEntId());
				stSaveVehicleOutLineInfo.setString(5, info.getEntName());
				stSaveVehicleOutLineInfo.setString(6, info.getTeamId());
				stSaveVehicleOutLineInfo.setString(7, info.getTeamName());
				stSaveVehicleOutLineInfo.setString(8, info.getVehicleNo());
				stSaveVehicleOutLineInfo.setString(9,info.getVinCode());
				String outLine_code = rs.getString("OUTLINE_CODE");
				stSaveVehicleOutLineInfo.setString(10,outLine_code);
				stSaveVehicleOutLineInfo.setInt(11, rs.getInt("NUM"));
				stSaveVehicleOutLineInfo.setString(12, AnalysisDBAdapter.alarmTypeMap.get(outLine_code));
				stSaveVehicleOutLineInfo.setLong(13, rs.getLong("TIME"));
				if(info.getVlineId() != null && !"".equals(info.getVlineId())){
					stSaveVehicleOutLineInfo.setString(14, info.getVlineId());
				}else{
					stSaveVehicleOutLineInfo.setNull(14, Types.VARCHAR);
				}
				
				if(info.getLineName() != null){
					stSaveVehicleOutLineInfo.setString(15, info.getLineName());
				}else{
					stSaveVehicleOutLineInfo.setString(15, null);
				}
				stSaveVehicleOutLineInfo.addBatch();
			}
		
			stSaveVehicleOutLineInfo.executeBatch();
			
		}catch(SQLException e){
			logger.error("存储日非法营运统计信息出错:VID="+vid +" " ,e);
		}finally{
				if(rs != null){
					rs.close();
				}
				
				if(stSelectOutlineEvent != null){
					stSelectOutlineEvent.close();
				}
				
				if(stSaveVehicleOutLineInfo != null){
					stSaveVehicleOutLineInfo.close();
				}
		}
	}
	
	public static boolean delDriverEvent(String vid){
		OracleConnection dbCon = null;
		PreparedStatement stDeleteDriverEventInfo = null;
		try{
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
		//读入之前先清空原有数据
			stDeleteDriverEventInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_deleteDriverEventInfo"));
			stDeleteDriverEventInfo.setString(1, vid);
			stDeleteDriverEventInfo.executeUpdate();
			return true;
		}catch(Exception ex){
			logger.error("删除数据库中驾驶行为事件出错：VID="+vid+" ",ex);
			return false;
		}finally{
			try{
				if (stDeleteDriverEventInfo!=null){
					stDeleteDriverEventInfo.close();
				}
				if (dbCon!=null){
					dbCon.close();
				}
			}catch(Exception e){
				logger.error("将连接放回连接池出错：",e);
			}
		}
	}
	
	/**
	 * 保存当前车辆驾驶行为事件并信息
	 * @param file
	 * @throws IOException
	 * @throws NumberFormatException
	 */
	public static void saveDriverEvent(String vid,Vector<AlarmCacheBean> alarmList) {
		OracleConnection dbCon = null;
		PreparedStatement stSaveDriverEventInfo = null;
		try{
			if (alarmList != null&&alarmList.size()>0) {
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stSaveDriverEventInfo = (OraclePreparedStatement)dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveDriverEventInfo"));
				
				Iterator<AlarmCacheBean> eventList = alarmList.iterator();
				int count = 0;
				while(eventList.hasNext()){
					AlarmCacheBean alarmCacheBean = eventList.next();
				
					if("EVENT".equals(alarmCacheBean.getAlarmType())){
						VehicleMessageBean beginVmb = alarmCacheBean.getBeginVmb();
						VehicleMessageBean endVmb = alarmCacheBean.getEndVmb();
						stSaveDriverEventInfo.setString(1, GeneratorPK.instance().getPKString());
						stSaveDriverEventInfo.setString(2, vid);
						stSaveDriverEventInfo.setString(3, "");
						stSaveDriverEventInfo.setString(4, alarmCacheBean.getAlarmcode());
						
						stSaveDriverEventInfo.setLong(5, beginVmb.getUtc());
						stSaveDriverEventInfo.setLong(6, beginVmb.getLat());
						stSaveDriverEventInfo.setLong(7, beginVmb.getLon());
						stSaveDriverEventInfo.setNull(8, Types.INTEGER);
						stSaveDriverEventInfo.setNull(9, Types.INTEGER);
						stSaveDriverEventInfo.setLong(10, beginVmb.getElevation());
						stSaveDriverEventInfo.setLong(11, beginVmb.getDir());
						stSaveDriverEventInfo.setLong(12, beginVmb.getSpeed());
						stSaveDriverEventInfo.setLong(13, endVmb.getUtc());
						stSaveDriverEventInfo.setLong(14, endVmb.getLat());
						stSaveDriverEventInfo.setLong(15, endVmb.getLon());
						stSaveDriverEventInfo.setNull(16, Types.INTEGER);
						stSaveDriverEventInfo.setNull(17, Types.INTEGER);
						stSaveDriverEventInfo.setLong(18, endVmb.getElevation());
						stSaveDriverEventInfo.setLong(19, endVmb.getDir());
						stSaveDriverEventInfo.setLong(20, endVmb.getSpeed());
						
						stSaveDriverEventInfo.addBatch();
					}
				}
				stSaveDriverEventInfo.executeBatch();
			}
		}catch(Exception e){
			logger.error(vid + " 存储驾驶行为事件信息出错.",e);
		}finally{
			try{
				if (stSaveDriverEventInfo!=null){
					stSaveDriverEventInfo.close();
				}
				if (dbCon!=null){
					dbCon.close();
				}
			}catch(Exception e){
				logger.error("将连接放回连接池出错：",e);
			}
		}
	}
	
	/**
	 * 存储机油压力
	 * 
	 * @throws SQLException
	 * @throws SQLException
	 */
	public static void saveOilPressureDay(String vid,OilPressureBean oilPressureBean) throws SQLException {
		OracleConnection dbCon = null;
		PreparedStatement stSaveOilPressureDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveOilPressureDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveOilPressureDayStat"));
			stSaveOilPressureDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveOilPressureDayStat.setString(2, vid);
			if ( info!= null) {
				stSaveOilPressureDayStat.setString(3, info.getVehicleNo());
				stSaveOilPressureDayStat.setString(4, info.getVinCode());
			} else {
				stSaveOilPressureDayStat.setString(3, null);
				stSaveOilPressureDayStat.setString(4, null);
			}
			stSaveOilPressureDayStat.setLong(5, oilPressureBean.getStatDate());

			stSaveOilPressureDayStat.setInt(6, oilPressureBean.getPressure_0()
					.getNum());
			stSaveOilPressureDayStat.setInt(7, oilPressureBean.getPressure_0()
					.getTime());
			stSaveOilPressureDayStat.setInt(8, oilPressureBean
					.getPressure_0_50().getNum());
			stSaveOilPressureDayStat.setInt(9, oilPressureBean
					.getPressure_0_50().getTime());

			stSaveOilPressureDayStat.setInt(10, oilPressureBean
					.getPressure_50_100().getNum());
			stSaveOilPressureDayStat.setInt(11, oilPressureBean
					.getPressure_50_100().getTime());

			stSaveOilPressureDayStat.setInt(12, oilPressureBean
					.getPressure_100_150().getNum());
			stSaveOilPressureDayStat.setInt(13, oilPressureBean
					.getPressure_100_150().getTime());

			stSaveOilPressureDayStat.setInt(14, oilPressureBean
					.getPressure_150_200().getNum());
			stSaveOilPressureDayStat.setInt(15, oilPressureBean
					.getPressure_150_200().getTime());

			stSaveOilPressureDayStat.setInt(16, oilPressureBean
					.getPressure_200_250().getNum());
			stSaveOilPressureDayStat.setInt(17, oilPressureBean
					.getPressure_200_250().getTime());
			stSaveOilPressureDayStat.setInt(18, oilPressureBean
					.getPressure_250_300().getNum());
			stSaveOilPressureDayStat.setInt(19, oilPressureBean
					.getPressure_250_300().getTime());

			stSaveOilPressureDayStat.setInt(20, oilPressureBean
					.getPressure_300_350().getNum());
			stSaveOilPressureDayStat.setInt(21, oilPressureBean
					.getPressure_300_350().getTime());

			stSaveOilPressureDayStat.setInt(22, oilPressureBean
					.getPressure_350_400().getNum());
			stSaveOilPressureDayStat.setInt(23, oilPressureBean
					.getPressure_350_400().getTime());

			stSaveOilPressureDayStat.setInt(24, oilPressureBean
					.getPressure_400_450().getNum());
			stSaveOilPressureDayStat.setInt(25, oilPressureBean
					.getPressure_400_450().getTime());

			stSaveOilPressureDayStat.setInt(26, oilPressureBean
					.getPressure_450_500().getNum());
			stSaveOilPressureDayStat.setInt(27, oilPressureBean
					.getPressure_450_500().getTime());

			stSaveOilPressureDayStat.setInt(28, oilPressureBean
					.getPressure_500_550().getNum());
			stSaveOilPressureDayStat.setInt(29, oilPressureBean
					.getPressure_500_550().getTime());

			stSaveOilPressureDayStat.setInt(30, oilPressureBean
					.getPressure_550_600().getNum());
			stSaveOilPressureDayStat.setInt(31, oilPressureBean
					.getPressure_550_600().getTime());

			stSaveOilPressureDayStat.setInt(32, oilPressureBean
					.getPressure_600_650().getNum());
			stSaveOilPressureDayStat.setInt(33, oilPressureBean
					.getPressure_600_650().getTime());

			stSaveOilPressureDayStat.setInt(34, oilPressureBean
					.getPressure_650_700().getNum());
			stSaveOilPressureDayStat.setInt(35, oilPressureBean
					.getPressure_650_700().getTime());

			stSaveOilPressureDayStat.setInt(36, oilPressureBean
					.getPressure_700_750().getNum());
			stSaveOilPressureDayStat.setInt(37, oilPressureBean
					.getPressure_700_750().getTime());

			stSaveOilPressureDayStat.setInt(38, oilPressureBean
					.getPressure_750_800().getNum());
			stSaveOilPressureDayStat.setInt(39, oilPressureBean
					.getPressure_750_800().getTime());

			stSaveOilPressureDayStat.setInt(40, oilPressureBean
					.getPressure_800().getNum());
			stSaveOilPressureDayStat.setInt(41, oilPressureBean
					.getPressure_800().getTime());

			stSaveOilPressureDayStat.setLong(42, oilPressureBean.getMax());
			stSaveOilPressureDayStat.setLong(43, oilPressureBean.getMin());

			stSaveOilPressureDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计车辆" + vid + "机油压力出错", e);
		} finally {
			try {
				if(null != stSaveOilPressureDayStat ){
					stSaveOilPressureDayStat.close();
				}
				if(null != dbCon ){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("将连接放回连接池出错：",e);
			}
		}

	}
	
	/**
	 * 存储机油压力
	 * 
	 * @throws SQLException
	 * @throws SQLException
	 */
	public static void saveOilPressureDay(OracleConnection dbCon,String vid,OilPressureBean oilPressureBean) throws Exception {
		PreparedStatement stSaveOilPressureDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stSaveOilPressureDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveOilPressureDayStat"));
			stSaveOilPressureDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveOilPressureDayStat.setString(2, vid);
			if ( info!= null) {
				stSaveOilPressureDayStat.setString(3, info.getVehicleNo());
				stSaveOilPressureDayStat.setString(4, info.getVinCode());
			} else {
				stSaveOilPressureDayStat.setString(3, null);
				stSaveOilPressureDayStat.setString(4, null);
			}
			stSaveOilPressureDayStat.setLong(5, oilPressureBean.getStatDate());

			stSaveOilPressureDayStat.setInt(6, oilPressureBean.getPressure_0()
					.getNum());
			stSaveOilPressureDayStat.setInt(7, oilPressureBean.getPressure_0()
					.getTime());
			stSaveOilPressureDayStat.setInt(8, oilPressureBean
					.getPressure_0_50().getNum());
			stSaveOilPressureDayStat.setInt(9, oilPressureBean
					.getPressure_0_50().getTime());

			stSaveOilPressureDayStat.setInt(10, oilPressureBean
					.getPressure_50_100().getNum());
			stSaveOilPressureDayStat.setInt(11, oilPressureBean
					.getPressure_50_100().getTime());

			stSaveOilPressureDayStat.setInt(12, oilPressureBean
					.getPressure_100_150().getNum());
			stSaveOilPressureDayStat.setInt(13, oilPressureBean
					.getPressure_100_150().getTime());

			stSaveOilPressureDayStat.setInt(14, oilPressureBean
					.getPressure_150_200().getNum());
			stSaveOilPressureDayStat.setInt(15, oilPressureBean
					.getPressure_150_200().getTime());

			stSaveOilPressureDayStat.setInt(16, oilPressureBean
					.getPressure_200_250().getNum());
			stSaveOilPressureDayStat.setInt(17, oilPressureBean
					.getPressure_200_250().getTime());
			stSaveOilPressureDayStat.setInt(18, oilPressureBean
					.getPressure_250_300().getNum());
			stSaveOilPressureDayStat.setInt(19, oilPressureBean
					.getPressure_250_300().getTime());

			stSaveOilPressureDayStat.setInt(20, oilPressureBean
					.getPressure_300_350().getNum());
			stSaveOilPressureDayStat.setInt(21, oilPressureBean
					.getPressure_300_350().getTime());

			stSaveOilPressureDayStat.setInt(22, oilPressureBean
					.getPressure_350_400().getNum());
			stSaveOilPressureDayStat.setInt(23, oilPressureBean
					.getPressure_350_400().getTime());

			stSaveOilPressureDayStat.setInt(24, oilPressureBean
					.getPressure_400_450().getNum());
			stSaveOilPressureDayStat.setInt(25, oilPressureBean
					.getPressure_400_450().getTime());

			stSaveOilPressureDayStat.setInt(26, oilPressureBean
					.getPressure_450_500().getNum());
			stSaveOilPressureDayStat.setInt(27, oilPressureBean
					.getPressure_450_500().getTime());

			stSaveOilPressureDayStat.setInt(28, oilPressureBean
					.getPressure_500_550().getNum());
			stSaveOilPressureDayStat.setInt(29, oilPressureBean
					.getPressure_500_550().getTime());

			stSaveOilPressureDayStat.setInt(30, oilPressureBean
					.getPressure_550_600().getNum());
			stSaveOilPressureDayStat.setInt(31, oilPressureBean
					.getPressure_550_600().getTime());

			stSaveOilPressureDayStat.setInt(32, oilPressureBean
					.getPressure_600_650().getNum());
			stSaveOilPressureDayStat.setInt(33, oilPressureBean
					.getPressure_600_650().getTime());

			stSaveOilPressureDayStat.setInt(34, oilPressureBean
					.getPressure_650_700().getNum());
			stSaveOilPressureDayStat.setInt(35, oilPressureBean
					.getPressure_650_700().getTime());

			stSaveOilPressureDayStat.setInt(36, oilPressureBean
					.getPressure_700_750().getNum());
			stSaveOilPressureDayStat.setInt(37, oilPressureBean
					.getPressure_700_750().getTime());

			stSaveOilPressureDayStat.setInt(38, oilPressureBean
					.getPressure_750_800().getNum());
			stSaveOilPressureDayStat.setInt(39, oilPressureBean
					.getPressure_750_800().getTime());

			stSaveOilPressureDayStat.setInt(40, oilPressureBean
					.getPressure_800().getNum());
			stSaveOilPressureDayStat.setInt(41, oilPressureBean
					.getPressure_800().getTime());

			stSaveOilPressureDayStat.setLong(42, oilPressureBean.getMax());
			stSaveOilPressureDayStat.setLong(43, oilPressureBean.getMin());

			stSaveOilPressureDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计车辆" + vid + "机油压力出错", e);
		} finally {
			if(null != stSaveOilPressureDayStat ){
				stSaveOilPressureDayStat.close();
			}
		}

	}

	/***
	 * 存储进气压力
	 * 
	 * @param vid
	 * @throws SQLException
	 */
	public static void saveGasPressure(String vid,GasPressureBean gsPressureBean) throws SQLException {
		OracleConnection dbCon = null;
		PreparedStatement stSaveGasPressureDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveGasPressureDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveGasPressureDayStat"));
			stSaveGasPressureDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveGasPressureDayStat.setString(2, vid);
			if ( info!= null) {
				stSaveGasPressureDayStat.setString(3, info.getVehicleNo());
				stSaveGasPressureDayStat.setString(4, info.getVinCode());
			} else {
				stSaveGasPressureDayStat.setString(3, null);
				stSaveGasPressureDayStat.setString(4, null);
			}
			stSaveGasPressureDayStat.setLong(5, gsPressureBean.getStatDate());

			stSaveGasPressureDayStat.setInt(6, gsPressureBean.getGsPressure_0()
					.getNum());
			stSaveGasPressureDayStat.setInt(7, gsPressureBean.getGsPressure_0()
					.getTime());

			stSaveGasPressureDayStat.setInt(8, gsPressureBean
					.getGsPressure_0_50().getNum());
			stSaveGasPressureDayStat.setInt(9, gsPressureBean
					.getGsPressure_0_50().getTime());

			stSaveGasPressureDayStat.setInt(10, gsPressureBean
					.getGsPressure_50_55().getNum());
			stSaveGasPressureDayStat.setInt(11, gsPressureBean
					.getGsPressure_50_55().getTime());

			stSaveGasPressureDayStat.setInt(12, gsPressureBean
					.getGsPressure_55_60().getNum());
			stSaveGasPressureDayStat.setInt(13, gsPressureBean
					.getGsPressure_55_60().getTime());

			stSaveGasPressureDayStat.setInt(14, gsPressureBean
					.getGsPressure_60_65().getNum());
			stSaveGasPressureDayStat.setInt(15, gsPressureBean
					.getGsPressure_60_65().getTime());

			stSaveGasPressureDayStat.setInt(16, gsPressureBean
					.getGsPressure_65_70().getNum());
			stSaveGasPressureDayStat.setInt(17, gsPressureBean
					.getGsPressure_65_70().getTime());

			stSaveGasPressureDayStat.setInt(18, gsPressureBean
					.getGsPressure_70_75().getNum());
			stSaveGasPressureDayStat.setInt(19, gsPressureBean
					.getGsPressure_70_75().getTime());

			stSaveGasPressureDayStat.setInt(20, gsPressureBean
					.getGsPressure_75_80().getNum());
			stSaveGasPressureDayStat.setInt(21, gsPressureBean
					.getGsPressure_75_80().getTime());

			stSaveGasPressureDayStat.setInt(22, gsPressureBean
					.getGsPressure_80_85().getNum());
			stSaveGasPressureDayStat.setInt(23, gsPressureBean
					.getGsPressure_80_85().getTime());

			stSaveGasPressureDayStat.setInt(24, gsPressureBean
					.getGsPressure_85_90().getNum());
			stSaveGasPressureDayStat.setInt(25, gsPressureBean
					.getGsPressure_85_90().getTime());

			stSaveGasPressureDayStat.setInt(26, gsPressureBean
					.getGsPressure_90_95().getNum());
			stSaveGasPressureDayStat.setInt(27, gsPressureBean
					.getGsPressure_90_95().getTime());

			stSaveGasPressureDayStat.setInt(28, gsPressureBean
					.getGsPressure_95_100().getNum());
			stSaveGasPressureDayStat.setInt(29, gsPressureBean
					.getGsPressure_95_100().getTime());

			stSaveGasPressureDayStat.setInt(30, gsPressureBean
					.getGsPressure_100_105().getNum());
			stSaveGasPressureDayStat.setInt(31, gsPressureBean
					.getGsPressure_100_105().getTime());

			stSaveGasPressureDayStat.setInt(32, gsPressureBean
					.getGsPressure_105_110().getNum());
			stSaveGasPressureDayStat.setInt(33, gsPressureBean
					.getGsPressure_105_110().getTime());

			stSaveGasPressureDayStat.setInt(34, gsPressureBean
					.getGsPressure_110_115().getNum());
			stSaveGasPressureDayStat.setInt(35, gsPressureBean
					.getGsPressure_110_115().getTime());

			stSaveGasPressureDayStat.setInt(36, gsPressureBean
					.getGsPressure_115_120().getNum());
			stSaveGasPressureDayStat.setInt(37, gsPressureBean
					.getGsPressure_115_120().getTime());

			stSaveGasPressureDayStat.setInt(38, gsPressureBean
					.getGsPressure_120().getNum());
			stSaveGasPressureDayStat.setInt(39, gsPressureBean
					.getGsPressure_120().getTime());

			stSaveGasPressureDayStat.setDouble(40, gsPressureBean.getMax());
			stSaveGasPressureDayStat.setDouble(41, gsPressureBean.getMin());

			stSaveGasPressureDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计" + vid + " 进气压力出错.", e);
		} finally {
			try {
				if(null != stSaveGasPressureDayStat ){
					stSaveGasPressureDayStat.close();
				}
				if(null != dbCon ){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("将连接放回连接池出错：",e);
			}
		}
	}
	
	/***
	 * 存储进气压力
	 * 
	 * @param vid
	 * @throws SQLException
	 */
	public static void saveGasPressure(OracleConnection dbCon,String vid,GasPressureBean gsPressureBean) throws SQLException {
		PreparedStatement stSaveGasPressureDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stSaveGasPressureDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveGasPressureDayStat"));
			stSaveGasPressureDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveGasPressureDayStat.setString(2, vid);
			if ( info!= null) {
				stSaveGasPressureDayStat.setString(3, info.getVehicleNo());
				stSaveGasPressureDayStat.setString(4, info.getVinCode());
			} else {
				stSaveGasPressureDayStat.setString(3, null);
				stSaveGasPressureDayStat.setString(4, null);
			}
			stSaveGasPressureDayStat.setLong(5, gsPressureBean.getStatDate());

			stSaveGasPressureDayStat.setInt(6, gsPressureBean.getGsPressure_0()
					.getNum());
			stSaveGasPressureDayStat.setInt(7, gsPressureBean.getGsPressure_0()
					.getTime());

			stSaveGasPressureDayStat.setInt(8, gsPressureBean
					.getGsPressure_0_50().getNum());
			stSaveGasPressureDayStat.setInt(9, gsPressureBean
					.getGsPressure_0_50().getTime());

			stSaveGasPressureDayStat.setInt(10, gsPressureBean
					.getGsPressure_50_55().getNum());
			stSaveGasPressureDayStat.setInt(11, gsPressureBean
					.getGsPressure_50_55().getTime());

			stSaveGasPressureDayStat.setInt(12, gsPressureBean
					.getGsPressure_55_60().getNum());
			stSaveGasPressureDayStat.setInt(13, gsPressureBean
					.getGsPressure_55_60().getTime());

			stSaveGasPressureDayStat.setInt(14, gsPressureBean
					.getGsPressure_60_65().getNum());
			stSaveGasPressureDayStat.setInt(15, gsPressureBean
					.getGsPressure_60_65().getTime());

			stSaveGasPressureDayStat.setInt(16, gsPressureBean
					.getGsPressure_65_70().getNum());
			stSaveGasPressureDayStat.setInt(17, gsPressureBean
					.getGsPressure_65_70().getTime());

			stSaveGasPressureDayStat.setInt(18, gsPressureBean
					.getGsPressure_70_75().getNum());
			stSaveGasPressureDayStat.setInt(19, gsPressureBean
					.getGsPressure_70_75().getTime());

			stSaveGasPressureDayStat.setInt(20, gsPressureBean
					.getGsPressure_75_80().getNum());
			stSaveGasPressureDayStat.setInt(21, gsPressureBean
					.getGsPressure_75_80().getTime());

			stSaveGasPressureDayStat.setInt(22, gsPressureBean
					.getGsPressure_80_85().getNum());
			stSaveGasPressureDayStat.setInt(23, gsPressureBean
					.getGsPressure_80_85().getTime());

			stSaveGasPressureDayStat.setInt(24, gsPressureBean
					.getGsPressure_85_90().getNum());
			stSaveGasPressureDayStat.setInt(25, gsPressureBean
					.getGsPressure_85_90().getTime());

			stSaveGasPressureDayStat.setInt(26, gsPressureBean
					.getGsPressure_90_95().getNum());
			stSaveGasPressureDayStat.setInt(27, gsPressureBean
					.getGsPressure_90_95().getTime());

			stSaveGasPressureDayStat.setInt(28, gsPressureBean
					.getGsPressure_95_100().getNum());
			stSaveGasPressureDayStat.setInt(29, gsPressureBean
					.getGsPressure_95_100().getTime());

			stSaveGasPressureDayStat.setInt(30, gsPressureBean
					.getGsPressure_100_105().getNum());
			stSaveGasPressureDayStat.setInt(31, gsPressureBean
					.getGsPressure_100_105().getTime());

			stSaveGasPressureDayStat.setInt(32, gsPressureBean
					.getGsPressure_105_110().getNum());
			stSaveGasPressureDayStat.setInt(33, gsPressureBean
					.getGsPressure_105_110().getTime());

			stSaveGasPressureDayStat.setInt(34, gsPressureBean
					.getGsPressure_110_115().getNum());
			stSaveGasPressureDayStat.setInt(35, gsPressureBean
					.getGsPressure_110_115().getTime());

			stSaveGasPressureDayStat.setInt(36, gsPressureBean
					.getGsPressure_115_120().getNum());
			stSaveGasPressureDayStat.setInt(37, gsPressureBean
					.getGsPressure_115_120().getTime());

			stSaveGasPressureDayStat.setInt(38, gsPressureBean
					.getGsPressure_120().getNum());
			stSaveGasPressureDayStat.setInt(39, gsPressureBean
					.getGsPressure_120().getTime());

			stSaveGasPressureDayStat.setDouble(40, gsPressureBean.getMax());
			stSaveGasPressureDayStat.setDouble(41, gsPressureBean.getMin());

			stSaveGasPressureDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计" + vid + " 进气压力出错.", e);
		} finally {
			if(null != stSaveGasPressureDayStat ){
				stSaveGasPressureDayStat.close();
			}
		}
	}

	/**
	 * 存储冷却液温度
	 * 
	 * @param vid
	 * @throws SQLException
	 */
	public static void saveCoolLiquidtemDayStat(String vid,CoolLiquidtemBean coolLiquidtemBean) throws SQLException {
		OracleConnection dbCon = null;
		PreparedStatement stSaveCoolLiquidtemDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveCoolLiquidtemDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveCoolLiquidtemDayStat"));
			stSaveCoolLiquidtemDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveCoolLiquidtemDayStat.setString(2, vid);
			if (info != null) {
				stSaveCoolLiquidtemDayStat.setString(3, info.getVehicleNo());
				stSaveCoolLiquidtemDayStat.setString(4, info.getVinCode());
			} else {
				stSaveCoolLiquidtemDayStat.setString(3, null);
				stSaveCoolLiquidtemDayStat.setString(4, null);
			}
			stSaveCoolLiquidtemDayStat.setLong(5, coolLiquidtemBean.getStatDate());
			stSaveCoolLiquidtemDayStat.setInt(6, coolLiquidtemBean
					.getCollLiquidtem_0().getNum());
			stSaveCoolLiquidtemDayStat.setInt(7, coolLiquidtemBean
					.getCollLiquidtem_0().getTime());

			stSaveCoolLiquidtemDayStat.setInt(8, coolLiquidtemBean
					.getCollLiquidtem_0_5().getNum());
			stSaveCoolLiquidtemDayStat.setInt(9, coolLiquidtemBean
					.getCollLiquidtem_0_5().getTime());

			stSaveCoolLiquidtemDayStat.setInt(10, coolLiquidtemBean
					.getCollLiquidtem_5_10().getNum());
			stSaveCoolLiquidtemDayStat.setInt(11, coolLiquidtemBean
					.getCollLiquidtem_5_10().getTime());

			stSaveCoolLiquidtemDayStat.setInt(12, coolLiquidtemBean
					.getCollLiquidtem_10_15().getNum());
			stSaveCoolLiquidtemDayStat.setInt(13, coolLiquidtemBean
					.getCollLiquidtem_10_15().getTime());

			stSaveCoolLiquidtemDayStat.setInt(14, coolLiquidtemBean
					.getCollLiquidtem_15_20().getNum());
			stSaveCoolLiquidtemDayStat.setInt(15, coolLiquidtemBean
					.getCollLiquidtem_15_20().getTime());

			stSaveCoolLiquidtemDayStat.setInt(16, coolLiquidtemBean
					.getCollLiquidtem_20_25().getNum());
			stSaveCoolLiquidtemDayStat.setInt(17, coolLiquidtemBean
					.getCollLiquidtem_20_25().getTime());

			stSaveCoolLiquidtemDayStat.setInt(18, coolLiquidtemBean
					.getCollLiquidtem_25_30().getNum());
			stSaveCoolLiquidtemDayStat.setInt(19, coolLiquidtemBean
					.getCollLiquidtem_25_30().getTime());

			stSaveCoolLiquidtemDayStat.setInt(20, coolLiquidtemBean
					.getCollLiquidtem_30_35().getNum());
			stSaveCoolLiquidtemDayStat.setInt(21, coolLiquidtemBean
					.getCollLiquidtem_30_35().getTime());

			stSaveCoolLiquidtemDayStat.setInt(22, coolLiquidtemBean
					.getCollLiquidtem_35_40().getNum());
			stSaveCoolLiquidtemDayStat.setInt(23, coolLiquidtemBean
					.getCollLiquidtem_35_40().getTime());

			stSaveCoolLiquidtemDayStat.setInt(24, coolLiquidtemBean
					.getCollLiquidtem_40_45().getNum());
			stSaveCoolLiquidtemDayStat.setInt(25, coolLiquidtemBean
					.getCollLiquidtem_40_45().getTime());

			stSaveCoolLiquidtemDayStat.setInt(26, coolLiquidtemBean
					.getCollLiquidtem_45_50().getNum());
			stSaveCoolLiquidtemDayStat.setInt(27, coolLiquidtemBean
					.getCollLiquidtem_45_50().getTime());

			stSaveCoolLiquidtemDayStat.setInt(28, coolLiquidtemBean
					.getCollLiquidtem_50_55().getNum());
			stSaveCoolLiquidtemDayStat.setInt(29, coolLiquidtemBean
					.getCollLiquidtem_50_55().getTime());

			stSaveCoolLiquidtemDayStat.setInt(30, coolLiquidtemBean
					.getCollLiquidtem_55_60().getNum());
			stSaveCoolLiquidtemDayStat.setInt(31, coolLiquidtemBean
					.getCollLiquidtem_55_60().getTime());

			stSaveCoolLiquidtemDayStat.setInt(32, coolLiquidtemBean
					.getCollLiquidtem_60_65().getNum());
			stSaveCoolLiquidtemDayStat.setInt(33, coolLiquidtemBean
					.getCollLiquidtem_60_65().getTime());

			stSaveCoolLiquidtemDayStat.setInt(34, coolLiquidtemBean
					.getCollLiquidtem_65_70().getNum());
			stSaveCoolLiquidtemDayStat.setInt(35, coolLiquidtemBean
					.getCollLiquidtem_65_70().getTime());

			stSaveCoolLiquidtemDayStat.setInt(36, coolLiquidtemBean
					.getCollLiquidtem_70_75().getNum());
			stSaveCoolLiquidtemDayStat.setInt(37, coolLiquidtemBean
					.getCollLiquidtem_70_75().getTime());

			stSaveCoolLiquidtemDayStat.setInt(38, coolLiquidtemBean
					.getCollLiquidtem_75_80().getNum());
			stSaveCoolLiquidtemDayStat.setInt(39, coolLiquidtemBean
					.getCollLiquidtem_75_80().getTime());

			stSaveCoolLiquidtemDayStat.setInt(40, coolLiquidtemBean
					.getCollLiquidtem_80_85().getNum());
			stSaveCoolLiquidtemDayStat.setInt(41, coolLiquidtemBean
					.getCollLiquidtem_80_85().getTime());

			stSaveCoolLiquidtemDayStat.setInt(42, coolLiquidtemBean
					.getCollLiquidtem_85_90().getNum());
			stSaveCoolLiquidtemDayStat.setInt(43, coolLiquidtemBean
					.getCollLiquidtem_85_90().getTime());

			stSaveCoolLiquidtemDayStat.setInt(44, coolLiquidtemBean
					.getCollLiquidtem_90_95().getNum());
			stSaveCoolLiquidtemDayStat.setInt(45, coolLiquidtemBean
					.getCollLiquidtem_90_95().getTime());

			stSaveCoolLiquidtemDayStat.setInt(46, coolLiquidtemBean
					.getCollLiquidtem_95_100().getNum());
			stSaveCoolLiquidtemDayStat.setInt(47, coolLiquidtemBean
					.getCollLiquidtem_95_100().getTime());

			stSaveCoolLiquidtemDayStat.setInt(48, coolLiquidtemBean
					.getCollLiquidtem_100_105().getNum());
			stSaveCoolLiquidtemDayStat.setInt(49, coolLiquidtemBean
					.getCollLiquidtem_100_105().getTime());

			stSaveCoolLiquidtemDayStat.setInt(50, coolLiquidtemBean
					.getCollLiquidtem_105_110().getNum());
			stSaveCoolLiquidtemDayStat.setInt(51, coolLiquidtemBean
					.getCollLiquidtem_105_110().getTime());

			stSaveCoolLiquidtemDayStat.setInt(52, coolLiquidtemBean
					.getCollLiquidtem_110_115().getNum());
			stSaveCoolLiquidtemDayStat.setInt(53, coolLiquidtemBean
					.getCollLiquidtem_110_115().getTime());

			stSaveCoolLiquidtemDayStat.setInt(54, coolLiquidtemBean
					.getCollLiquidtem_115_120().getNum());
			stSaveCoolLiquidtemDayStat.setInt(55, coolLiquidtemBean
					.getCollLiquidtem_115_120().getTime());

			stSaveCoolLiquidtemDayStat.setInt(56, coolLiquidtemBean
					.getCollLiquidtem_120().getNum());
			stSaveCoolLiquidtemDayStat.setInt(57, coolLiquidtemBean
					.getCollLiquidtem_120().getTime());

			stSaveCoolLiquidtemDayStat.setLong(58, coolLiquidtemBean.getMax());
			stSaveCoolLiquidtemDayStat.setLong(59, coolLiquidtemBean.getMin());

			stSaveCoolLiquidtemDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计 " + vid + " 冷却液温度出错.", e);
		} finally {
			try {
				if(null != stSaveCoolLiquidtemDayStat ){
					stSaveCoolLiquidtemDayStat.close();
				}
				if(null != dbCon ){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("将连接放回连接池出错：",e);
			}
			
		}
	}
	
	/**
	 * 存储冷却液温度
	 * 
	 * @param vid
	 * @throws SQLException
	 */
	public static void saveCoolLiquidtemDayStat(OracleConnection dbCon,String vid,CoolLiquidtemBean coolLiquidtemBean) throws Exception {
		PreparedStatement stSaveCoolLiquidtemDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stSaveCoolLiquidtemDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveCoolLiquidtemDayStat"));
			stSaveCoolLiquidtemDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveCoolLiquidtemDayStat.setString(2, vid);
			if (info != null) {
				stSaveCoolLiquidtemDayStat.setString(3, info.getVehicleNo());
				stSaveCoolLiquidtemDayStat.setString(4, info.getVinCode());
			} else {
				stSaveCoolLiquidtemDayStat.setString(3, null);
				stSaveCoolLiquidtemDayStat.setString(4, null);
			}
			stSaveCoolLiquidtemDayStat.setLong(5, coolLiquidtemBean.getStatDate());
			stSaveCoolLiquidtemDayStat.setInt(6, coolLiquidtemBean
					.getCollLiquidtem_0().getNum());
			stSaveCoolLiquidtemDayStat.setInt(7, coolLiquidtemBean
					.getCollLiquidtem_0().getTime());

			stSaveCoolLiquidtemDayStat.setInt(8, coolLiquidtemBean
					.getCollLiquidtem_0_5().getNum());
			stSaveCoolLiquidtemDayStat.setInt(9, coolLiquidtemBean
					.getCollLiquidtem_0_5().getTime());

			stSaveCoolLiquidtemDayStat.setInt(10, coolLiquidtemBean
					.getCollLiquidtem_5_10().getNum());
			stSaveCoolLiquidtemDayStat.setInt(11, coolLiquidtemBean
					.getCollLiquidtem_5_10().getTime());

			stSaveCoolLiquidtemDayStat.setInt(12, coolLiquidtemBean
					.getCollLiquidtem_10_15().getNum());
			stSaveCoolLiquidtemDayStat.setInt(13, coolLiquidtemBean
					.getCollLiquidtem_10_15().getTime());

			stSaveCoolLiquidtemDayStat.setInt(14, coolLiquidtemBean
					.getCollLiquidtem_15_20().getNum());
			stSaveCoolLiquidtemDayStat.setInt(15, coolLiquidtemBean
					.getCollLiquidtem_15_20().getTime());

			stSaveCoolLiquidtemDayStat.setInt(16, coolLiquidtemBean
					.getCollLiquidtem_20_25().getNum());
			stSaveCoolLiquidtemDayStat.setInt(17, coolLiquidtemBean
					.getCollLiquidtem_20_25().getTime());

			stSaveCoolLiquidtemDayStat.setInt(18, coolLiquidtemBean
					.getCollLiquidtem_25_30().getNum());
			stSaveCoolLiquidtemDayStat.setInt(19, coolLiquidtemBean
					.getCollLiquidtem_25_30().getTime());

			stSaveCoolLiquidtemDayStat.setInt(20, coolLiquidtemBean
					.getCollLiquidtem_30_35().getNum());
			stSaveCoolLiquidtemDayStat.setInt(21, coolLiquidtemBean
					.getCollLiquidtem_30_35().getTime());

			stSaveCoolLiquidtemDayStat.setInt(22, coolLiquidtemBean
					.getCollLiquidtem_35_40().getNum());
			stSaveCoolLiquidtemDayStat.setInt(23, coolLiquidtemBean
					.getCollLiquidtem_35_40().getTime());

			stSaveCoolLiquidtemDayStat.setInt(24, coolLiquidtemBean
					.getCollLiquidtem_40_45().getNum());
			stSaveCoolLiquidtemDayStat.setInt(25, coolLiquidtemBean
					.getCollLiquidtem_40_45().getTime());

			stSaveCoolLiquidtemDayStat.setInt(26, coolLiquidtemBean
					.getCollLiquidtem_45_50().getNum());
			stSaveCoolLiquidtemDayStat.setInt(27, coolLiquidtemBean
					.getCollLiquidtem_45_50().getTime());

			stSaveCoolLiquidtemDayStat.setInt(28, coolLiquidtemBean
					.getCollLiquidtem_50_55().getNum());
			stSaveCoolLiquidtemDayStat.setInt(29, coolLiquidtemBean
					.getCollLiquidtem_50_55().getTime());

			stSaveCoolLiquidtemDayStat.setInt(30, coolLiquidtemBean
					.getCollLiquidtem_55_60().getNum());
			stSaveCoolLiquidtemDayStat.setInt(31, coolLiquidtemBean
					.getCollLiquidtem_55_60().getTime());

			stSaveCoolLiquidtemDayStat.setInt(32, coolLiquidtemBean
					.getCollLiquidtem_60_65().getNum());
			stSaveCoolLiquidtemDayStat.setInt(33, coolLiquidtemBean
					.getCollLiquidtem_60_65().getTime());

			stSaveCoolLiquidtemDayStat.setInt(34, coolLiquidtemBean
					.getCollLiquidtem_65_70().getNum());
			stSaveCoolLiquidtemDayStat.setInt(35, coolLiquidtemBean
					.getCollLiquidtem_65_70().getTime());

			stSaveCoolLiquidtemDayStat.setInt(36, coolLiquidtemBean
					.getCollLiquidtem_70_75().getNum());
			stSaveCoolLiquidtemDayStat.setInt(37, coolLiquidtemBean
					.getCollLiquidtem_70_75().getTime());

			stSaveCoolLiquidtemDayStat.setInt(38, coolLiquidtemBean
					.getCollLiquidtem_75_80().getNum());
			stSaveCoolLiquidtemDayStat.setInt(39, coolLiquidtemBean
					.getCollLiquidtem_75_80().getTime());

			stSaveCoolLiquidtemDayStat.setInt(40, coolLiquidtemBean
					.getCollLiquidtem_80_85().getNum());
			stSaveCoolLiquidtemDayStat.setInt(41, coolLiquidtemBean
					.getCollLiquidtem_80_85().getTime());

			stSaveCoolLiquidtemDayStat.setInt(42, coolLiquidtemBean
					.getCollLiquidtem_85_90().getNum());
			stSaveCoolLiquidtemDayStat.setInt(43, coolLiquidtemBean
					.getCollLiquidtem_85_90().getTime());

			stSaveCoolLiquidtemDayStat.setInt(44, coolLiquidtemBean
					.getCollLiquidtem_90_95().getNum());
			stSaveCoolLiquidtemDayStat.setInt(45, coolLiquidtemBean
					.getCollLiquidtem_90_95().getTime());

			stSaveCoolLiquidtemDayStat.setInt(46, coolLiquidtemBean
					.getCollLiquidtem_95_100().getNum());
			stSaveCoolLiquidtemDayStat.setInt(47, coolLiquidtemBean
					.getCollLiquidtem_95_100().getTime());

			stSaveCoolLiquidtemDayStat.setInt(48, coolLiquidtemBean
					.getCollLiquidtem_100_105().getNum());
			stSaveCoolLiquidtemDayStat.setInt(49, coolLiquidtemBean
					.getCollLiquidtem_100_105().getTime());

			stSaveCoolLiquidtemDayStat.setInt(50, coolLiquidtemBean
					.getCollLiquidtem_105_110().getNum());
			stSaveCoolLiquidtemDayStat.setInt(51, coolLiquidtemBean
					.getCollLiquidtem_105_110().getTime());

			stSaveCoolLiquidtemDayStat.setInt(52, coolLiquidtemBean
					.getCollLiquidtem_110_115().getNum());
			stSaveCoolLiquidtemDayStat.setInt(53, coolLiquidtemBean
					.getCollLiquidtem_110_115().getTime());

			stSaveCoolLiquidtemDayStat.setInt(54, coolLiquidtemBean
					.getCollLiquidtem_115_120().getNum());
			stSaveCoolLiquidtemDayStat.setInt(55, coolLiquidtemBean
					.getCollLiquidtem_115_120().getTime());

			stSaveCoolLiquidtemDayStat.setInt(56, coolLiquidtemBean
					.getCollLiquidtem_120().getNum());
			stSaveCoolLiquidtemDayStat.setInt(57, coolLiquidtemBean
					.getCollLiquidtem_120().getTime());

			stSaveCoolLiquidtemDayStat.setLong(58, coolLiquidtemBean.getMax());
			stSaveCoolLiquidtemDayStat.setLong(59, coolLiquidtemBean.getMin());

			stSaveCoolLiquidtemDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计 " + vid + " 冷却液温度出错.", e);
		} finally {
			if(null != stSaveCoolLiquidtemDayStat ){
				stSaveCoolLiquidtemDayStat.close();
			}
		}
	}

	/**
	 * 存储车速分析表
	 * 
	 * @throws SQLException
	 */
	public static void saveSpeeddistDay(String vid,SpeeddistDay speeddistDay) throws SQLException {
		OracleConnection dbCon = null;
		PreparedStatement stSaveSpeeddistDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveSpeeddistDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveSpeeddistDayStat"));
			stSaveSpeeddistDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveSpeeddistDayStat.setString(2, vid);
			if (info != null) {
				stSaveSpeeddistDayStat.setString(3, info.getVehicleNo());
				stSaveSpeeddistDayStat.setString(4, info.getVinCode());
			} else {
				stSaveSpeeddistDayStat.setString(3, null);
				stSaveSpeeddistDayStat.setString(4, null);
			}
			stSaveSpeeddistDayStat.setLong(5, speeddistDay.getStatDate());

			stSaveSpeeddistDayStat.setLong(6, speeddistDay.getSpeed0());
			stSaveSpeeddistDayStat.setLong(7, speeddistDay.getSpeed0Time());

			stSaveSpeeddistDayStat.setLong(8, speeddistDay.getSpeed010());
			stSaveSpeeddistDayStat.setLong(9, speeddistDay.getSpeed010Time());

			stSaveSpeeddistDayStat.setLong(10, speeddistDay.getSpeed1020());
			stSaveSpeeddistDayStat.setLong(11, speeddistDay.getSpeed1020Time());

			stSaveSpeeddistDayStat.setLong(12, speeddistDay.getSpeed2030());
			stSaveSpeeddistDayStat.setLong(13, speeddistDay.getSpeed2030Time());

			stSaveSpeeddistDayStat.setLong(14, speeddistDay.getSpeed3040());
			stSaveSpeeddistDayStat.setLong(15, speeddistDay.getSpeed3040Time());

			stSaveSpeeddistDayStat.setLong(16, speeddistDay.getSpeed4050());
			stSaveSpeeddistDayStat.setLong(17, speeddistDay.getSpeed4050Time());

			stSaveSpeeddistDayStat.setLong(18, speeddistDay.getSpeed5060());
			stSaveSpeeddistDayStat.setLong(19, speeddistDay.getSpeed5060Time());

			stSaveSpeeddistDayStat.setLong(20, speeddistDay.getSpeed6070());
			stSaveSpeeddistDayStat.setLong(21, speeddistDay.getSpeed6070Time());

			stSaveSpeeddistDayStat.setLong(22, speeddistDay.getSpeed7080());
			stSaveSpeeddistDayStat.setLong(23, speeddistDay.getSpeed7080Time());

			stSaveSpeeddistDayStat.setLong(24, speeddistDay.getSpeed8090());
			stSaveSpeeddistDayStat.setLong(25, speeddistDay.getSpeed8090Time());

			stSaveSpeeddistDayStat.setLong(26, speeddistDay.getSpeed90100());
			stSaveSpeeddistDayStat
					.setLong(27, speeddistDay.getSpeed90100Time());

			stSaveSpeeddistDayStat.setLong(28, speeddistDay.getSpeed100110());
			stSaveSpeeddistDayStat.setLong(29,
					speeddistDay.getSpeed100110Time());

			stSaveSpeeddistDayStat.setLong(30, speeddistDay.getSpeed110120());
			stSaveSpeeddistDayStat.setLong(31,
					speeddistDay.getSpeed110120Time());

			stSaveSpeeddistDayStat.setLong(32, speeddistDay.getSpeed120130());
			stSaveSpeeddistDayStat.setLong(33,
					speeddistDay.getSpeed120130Time());

			stSaveSpeeddistDayStat.setLong(34, speeddistDay.getSpeed130140());
			stSaveSpeeddistDayStat.setLong(35,
					speeddistDay.getSpeed130140Time());

			stSaveSpeeddistDayStat.setLong(36, speeddistDay.getSpeed140150());
			stSaveSpeeddistDayStat.setLong(37,
					speeddistDay.getSpeed140150Time());

			stSaveSpeeddistDayStat.setLong(38, speeddistDay.getSpeed150160());
			stSaveSpeeddistDayStat.setLong(39,
					speeddistDay.getSpeed150160Time());

			stSaveSpeeddistDayStat.setLong(40, speeddistDay.getSpeed160170());
			stSaveSpeeddistDayStat.setLong(41,
					speeddistDay.getSpeed160170Time());

			stSaveSpeeddistDayStat.setLong(42, speeddistDay.getSpeed170180());
			stSaveSpeeddistDayStat.setLong(43,
					speeddistDay.getSpeed170180Time());

			stSaveSpeeddistDayStat.setLong(44, speeddistDay.getSpeed180190());
			stSaveSpeeddistDayStat.setLong(45,
					speeddistDay.getSpeed180190Time());

			stSaveSpeeddistDayStat.setLong(46, speeddistDay.getSpeed190200());
			stSaveSpeeddistDayStat.setLong(47,
					speeddistDay.getSpeed190200Time());

			stSaveSpeeddistDayStat.setLong(48, speeddistDay.getSpeedMax());
			stSaveSpeeddistDayStat.setLong(49, speeddistDay.getSpeedMaxTime());

			stSaveSpeeddistDayStat.setLong(50, speeddistDay.getMaxSpeed());
			stSaveSpeeddistDayStat.setLong(51, speeddistDay.getMinSpeed());

			stSaveSpeeddistDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计 " + vid + " 车速分析表出错.", e);
		} finally {
			try {
				if(null != stSaveSpeeddistDayStat ){
					stSaveSpeeddistDayStat.close();
				}
				if(null != dbCon ){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("将连接放回连接池出错：",e);
			}
		}
	}
	
	/**
	 * 存储车速分析表
	 * 
	 * @throws SQLException
	 */
	public static void saveSpeeddistDay(OracleConnection dbCon,String vid,SpeeddistDay speeddistDay) throws Exception {
		PreparedStatement stSaveSpeeddistDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stSaveSpeeddistDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveSpeeddistDayStat"));
			stSaveSpeeddistDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveSpeeddistDayStat.setString(2, vid);
			if (info != null) {
				stSaveSpeeddistDayStat.setString(3, info.getVehicleNo());
				stSaveSpeeddistDayStat.setString(4, info.getVinCode());
			} else {
				stSaveSpeeddistDayStat.setString(3, null);
				stSaveSpeeddistDayStat.setString(4, null);
			}
			stSaveSpeeddistDayStat.setLong(5, speeddistDay.getStatDate());

			stSaveSpeeddistDayStat.setLong(6, speeddistDay.getSpeed0());
			stSaveSpeeddistDayStat.setLong(7, speeddistDay.getSpeed0Time());

			stSaveSpeeddistDayStat.setLong(8, speeddistDay.getSpeed010());
			stSaveSpeeddistDayStat.setLong(9, speeddistDay.getSpeed010Time());

			stSaveSpeeddistDayStat.setLong(10, speeddistDay.getSpeed1020());
			stSaveSpeeddistDayStat.setLong(11, speeddistDay.getSpeed1020Time());

			stSaveSpeeddistDayStat.setLong(12, speeddistDay.getSpeed2030());
			stSaveSpeeddistDayStat.setLong(13, speeddistDay.getSpeed2030Time());

			stSaveSpeeddistDayStat.setLong(14, speeddistDay.getSpeed3040());
			stSaveSpeeddistDayStat.setLong(15, speeddistDay.getSpeed3040Time());

			stSaveSpeeddistDayStat.setLong(16, speeddistDay.getSpeed4050());
			stSaveSpeeddistDayStat.setLong(17, speeddistDay.getSpeed4050Time());

			stSaveSpeeddistDayStat.setLong(18, speeddistDay.getSpeed5060());
			stSaveSpeeddistDayStat.setLong(19, speeddistDay.getSpeed5060Time());

			stSaveSpeeddistDayStat.setLong(20, speeddistDay.getSpeed6070());
			stSaveSpeeddistDayStat.setLong(21, speeddistDay.getSpeed6070Time());

			stSaveSpeeddistDayStat.setLong(22, speeddistDay.getSpeed7080());
			stSaveSpeeddistDayStat.setLong(23, speeddistDay.getSpeed7080Time());

			stSaveSpeeddistDayStat.setLong(24, speeddistDay.getSpeed8090());
			stSaveSpeeddistDayStat.setLong(25, speeddistDay.getSpeed8090Time());

			stSaveSpeeddistDayStat.setLong(26, speeddistDay.getSpeed90100());
			stSaveSpeeddistDayStat
					.setLong(27, speeddistDay.getSpeed90100Time());

			stSaveSpeeddistDayStat.setLong(28, speeddistDay.getSpeed100110());
			stSaveSpeeddistDayStat.setLong(29,
					speeddistDay.getSpeed100110Time());

			stSaveSpeeddistDayStat.setLong(30, speeddistDay.getSpeed110120());
			stSaveSpeeddistDayStat.setLong(31,
					speeddistDay.getSpeed110120Time());

			stSaveSpeeddistDayStat.setLong(32, speeddistDay.getSpeed120130());
			stSaveSpeeddistDayStat.setLong(33,
					speeddistDay.getSpeed120130Time());

			stSaveSpeeddistDayStat.setLong(34, speeddistDay.getSpeed130140());
			stSaveSpeeddistDayStat.setLong(35,
					speeddistDay.getSpeed130140Time());

			stSaveSpeeddistDayStat.setLong(36, speeddistDay.getSpeed140150());
			stSaveSpeeddistDayStat.setLong(37,
					speeddistDay.getSpeed140150Time());

			stSaveSpeeddistDayStat.setLong(38, speeddistDay.getSpeed150160());
			stSaveSpeeddistDayStat.setLong(39,
					speeddistDay.getSpeed150160Time());

			stSaveSpeeddistDayStat.setLong(40, speeddistDay.getSpeed160170());
			stSaveSpeeddistDayStat.setLong(41,
					speeddistDay.getSpeed160170Time());

			stSaveSpeeddistDayStat.setLong(42, speeddistDay.getSpeed170180());
			stSaveSpeeddistDayStat.setLong(43,
					speeddistDay.getSpeed170180Time());

			stSaveSpeeddistDayStat.setLong(44, speeddistDay.getSpeed180190());
			stSaveSpeeddistDayStat.setLong(45,
					speeddistDay.getSpeed180190Time());

			stSaveSpeeddistDayStat.setLong(46, speeddistDay.getSpeed190200());
			stSaveSpeeddistDayStat.setLong(47,
					speeddistDay.getSpeed190200Time());

			stSaveSpeeddistDayStat.setLong(48, speeddistDay.getSpeedMax());
			stSaveSpeeddistDayStat.setLong(49, speeddistDay.getSpeedMaxTime());

			stSaveSpeeddistDayStat.setLong(50, speeddistDay.getMaxSpeed());
			stSaveSpeeddistDayStat.setLong(51, speeddistDay.getMinSpeed());

			stSaveSpeeddistDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计 " + vid + " 车速分析表出错.", e);
		} finally {
			if(null != stSaveSpeeddistDayStat ){
				stSaveSpeeddistDayStat.close();
			}
		}
	}

	/**
	 * 存储转速分析表
	 * 
	 * @throws SQLException
	 */
	public static void saveRotatedistDay(String vid,RotateSpeedDay rotateSpeedDay) throws SQLException {
		OracleConnection dbCon = null;
		PreparedStatement stSaveRoatedistDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveRoatedistDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveRotateDayStat"));
			stSaveRoatedistDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveRoatedistDayStat.setString(2, vid);
			if (info != null) {
				stSaveRoatedistDayStat.setString(3, info.getVehicleNo());
				stSaveRoatedistDayStat.setString(4, info.getVinCode());
			} else {
				stSaveRoatedistDayStat.setString(3, null);
				stSaveRoatedistDayStat.setString(4, null);
			}
			stSaveRoatedistDayStat.setLong(5, rotateSpeedDay.getStatDate());
			stSaveRoatedistDayStat.setLong(6, rotateSpeedDay.getStatDate());

			stSaveRoatedistDayStat.setLong(7, rotateSpeedDay.getRotateSpeed0());
			stSaveRoatedistDayStat.setLong(8,
					rotateSpeedDay.getRotateSpeed0Time());

			stSaveRoatedistDayStat.setLong(9,
					rotateSpeedDay.getRotateSpeed0100());
			stSaveRoatedistDayStat.setLong(10,
					rotateSpeedDay.getRotateSpeed0100Time());

			stSaveRoatedistDayStat.setLong(11,
					rotateSpeedDay.getRotateSpeed100200());
			stSaveRoatedistDayStat.setLong(12,
					rotateSpeedDay.getRotateSpeed100200Time());

			stSaveRoatedistDayStat.setLong(13,
					rotateSpeedDay.getRotateSpeed200300());
			stSaveRoatedistDayStat.setLong(14,
					rotateSpeedDay.getRotateSpeed200300Time());

			stSaveRoatedistDayStat.setLong(15,
					rotateSpeedDay.getRotateSpeed300400());
			stSaveRoatedistDayStat.setLong(16,
					rotateSpeedDay.getRotateSpeed300400Time());

			stSaveRoatedistDayStat.setLong(17,
					rotateSpeedDay.getRotateSpeed400500());
			stSaveRoatedistDayStat.setLong(18,
					rotateSpeedDay.getRotateSpeed400500Time());

			stSaveRoatedistDayStat.setLong(19,
					rotateSpeedDay.getRotateSpeed500600());
			stSaveRoatedistDayStat.setLong(20,
					rotateSpeedDay.getRotateSpeed500600Time());

			stSaveRoatedistDayStat.setLong(21,
					rotateSpeedDay.getRotateSpeed600700());
			stSaveRoatedistDayStat.setLong(22,
					rotateSpeedDay.getRotateSpeed600700Time());

			stSaveRoatedistDayStat.setLong(23,
					rotateSpeedDay.getRotateSpeed700800());
			stSaveRoatedistDayStat.setLong(24,
					rotateSpeedDay.getRotateSpeed700800Time());

			stSaveRoatedistDayStat.setLong(25,
					rotateSpeedDay.getRotateSpeed800900());
			stSaveRoatedistDayStat.setLong(26,
					rotateSpeedDay.getRotateSpeed800900Time());

			stSaveRoatedistDayStat.setLong(27,
					rotateSpeedDay.getRotateSpeed9001000());
			stSaveRoatedistDayStat.setLong(28,
					rotateSpeedDay.getRotateSpeed9001000Time());

			stSaveRoatedistDayStat.setLong(29,
					rotateSpeedDay.getRotateSpeed10001100());
			stSaveRoatedistDayStat.setLong(30,
					rotateSpeedDay.getRotateSpeed10001100Time());

			stSaveRoatedistDayStat.setLong(31,
					rotateSpeedDay.getRotateSpeed11001200());
			stSaveRoatedistDayStat.setLong(32,
					rotateSpeedDay.getRotateSpeed11001200Time());

			stSaveRoatedistDayStat.setLong(33,
					rotateSpeedDay.getRotateSpeed12001300());
			stSaveRoatedistDayStat.setLong(34,
					rotateSpeedDay.getRotateSpeed12001300Time());

			stSaveRoatedistDayStat.setLong(35,
					rotateSpeedDay.getRotateSpeed13001400());
			stSaveRoatedistDayStat.setLong(36,
					rotateSpeedDay.getRotateSpeed13001400Time());

			stSaveRoatedistDayStat.setLong(37,
					rotateSpeedDay.getRotateSpeed14001500());
			stSaveRoatedistDayStat.setLong(38,
					rotateSpeedDay.getRotateSpeed14001500Time());

			stSaveRoatedistDayStat.setLong(39,
					rotateSpeedDay.getRotateSpeed15001600());
			stSaveRoatedistDayStat.setLong(40,
					rotateSpeedDay.getRotateSpeed15001600Time());

			stSaveRoatedistDayStat.setLong(41,
					rotateSpeedDay.getRotateSpeed16001700());
			stSaveRoatedistDayStat.setLong(42,
					rotateSpeedDay.getRotateSpeed16001700Time());

			stSaveRoatedistDayStat.setLong(43,
					rotateSpeedDay.getRotateSpeed17001800());
			stSaveRoatedistDayStat.setLong(44,
					rotateSpeedDay.getRotateSpeed17001800Time());

			stSaveRoatedistDayStat.setLong(45,
					rotateSpeedDay.getRotateSpeed18001900());
			stSaveRoatedistDayStat.setLong(46,
					rotateSpeedDay.getRotateSpeed18001900Time());

			stSaveRoatedistDayStat.setLong(47,
					rotateSpeedDay.getRotateSpeed19002000());
			stSaveRoatedistDayStat.setLong(48,
					rotateSpeedDay.getRotateSpeed19002000Time());

			stSaveRoatedistDayStat.setLong(49,
					rotateSpeedDay.getRotateSpeed20002100());
			stSaveRoatedistDayStat.setLong(50,
					rotateSpeedDay.getRotateSpeed20002100Time());

			stSaveRoatedistDayStat.setLong(51,
					rotateSpeedDay.getRotateSpeed21002200());
			stSaveRoatedistDayStat.setLong(52,
					rotateSpeedDay.getRotateSpeed21002200Time());

			stSaveRoatedistDayStat.setLong(53,
					rotateSpeedDay.getRotateSpeed22002300());
			stSaveRoatedistDayStat.setLong(54,
					rotateSpeedDay.getRotateSpeed22002300Time());

			stSaveRoatedistDayStat.setLong(55,
					rotateSpeedDay.getRotateSpeed23002400());
			stSaveRoatedistDayStat.setLong(56,
					rotateSpeedDay.getRotateSpeed23002400Time());

			stSaveRoatedistDayStat.setLong(57,
					rotateSpeedDay.getRotateSpeed24002500());
			stSaveRoatedistDayStat.setLong(58,
					rotateSpeedDay.getRotateSpeed24002500Time());

			stSaveRoatedistDayStat.setLong(59,
					rotateSpeedDay.getRotateSpeed25002600());
			stSaveRoatedistDayStat.setLong(60,
					rotateSpeedDay.getRotateSpeed25002600Time());

			stSaveRoatedistDayStat.setLong(61,
					rotateSpeedDay.getRotateSpeed26002700());
			stSaveRoatedistDayStat.setLong(62,
					rotateSpeedDay.getRotateSpeed26002700Time());

			stSaveRoatedistDayStat.setLong(63,
					rotateSpeedDay.getRotateSpeed27002800());
			stSaveRoatedistDayStat.setLong(64,
					rotateSpeedDay.getRotateSpeed27002800Time());

			stSaveRoatedistDayStat.setLong(65,
					rotateSpeedDay.getRotateSpeed28002900());
			stSaveRoatedistDayStat.setLong(66,
					rotateSpeedDay.getRotateSpeed28002900Time());

			stSaveRoatedistDayStat.setLong(67,
					rotateSpeedDay.getRotateSpeed29003000());
			stSaveRoatedistDayStat.setLong(68,
					rotateSpeedDay.getRotateSpeed29003000Time());

			stSaveRoatedistDayStat.setLong(69,
					rotateSpeedDay.getRotateSpeedMax());
			stSaveRoatedistDayStat.setLong(70,
					rotateSpeedDay.getRotateSpeedMaxTime());

			stSaveRoatedistDayStat.setLong(71,
					rotateSpeedDay.getPercent6080Fuhelv());

			stSaveRoatedistDayStat.setLong(72,
					rotateSpeedDay.getMinRotateSpeed());
			stSaveRoatedistDayStat.setLong(73,
					rotateSpeedDay.getMaxRotateSpeed());

			stSaveRoatedistDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计 " + vid + " 转速分析表出错。", e);
		} finally {
			try {
				if(null != stSaveRoatedistDayStat ){
					stSaveRoatedistDayStat.close();
				}
				if(null != dbCon ){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("将连接放回连接池出错：",e);
			}
			
		}
	}
	
	/**
	 * 存储转速分析表
	 * 
	 * @throws SQLException
	 */
	public static void saveRotatedistDay(OracleConnection dbCon,String vid,RotateSpeedDay rotateSpeedDay) throws Exception {
		PreparedStatement stSaveRoatedistDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);

			stSaveRoatedistDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveRotateDayStat"));
			stSaveRoatedistDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveRoatedistDayStat.setString(2, vid);
			if (info != null) {
				stSaveRoatedistDayStat.setString(3, info.getVehicleNo());
				stSaveRoatedistDayStat.setString(4, info.getVinCode());
			} else {
				stSaveRoatedistDayStat.setString(3, null);
				stSaveRoatedistDayStat.setString(4, null);
			}
			stSaveRoatedistDayStat.setLong(5, rotateSpeedDay.getStatDate());
			stSaveRoatedistDayStat.setLong(6, rotateSpeedDay.getStatDate());

			stSaveRoatedistDayStat.setLong(7, rotateSpeedDay.getRotateSpeed0());
			stSaveRoatedistDayStat.setLong(8,
					rotateSpeedDay.getRotateSpeed0Time());

			stSaveRoatedistDayStat.setLong(9,
					rotateSpeedDay.getRotateSpeed0100());
			stSaveRoatedistDayStat.setLong(10,
					rotateSpeedDay.getRotateSpeed0100Time());

			stSaveRoatedistDayStat.setLong(11,
					rotateSpeedDay.getRotateSpeed100200());
			stSaveRoatedistDayStat.setLong(12,
					rotateSpeedDay.getRotateSpeed100200Time());

			stSaveRoatedistDayStat.setLong(13,
					rotateSpeedDay.getRotateSpeed200300());
			stSaveRoatedistDayStat.setLong(14,
					rotateSpeedDay.getRotateSpeed200300Time());

			stSaveRoatedistDayStat.setLong(15,
					rotateSpeedDay.getRotateSpeed300400());
			stSaveRoatedistDayStat.setLong(16,
					rotateSpeedDay.getRotateSpeed300400Time());

			stSaveRoatedistDayStat.setLong(17,
					rotateSpeedDay.getRotateSpeed400500());
			stSaveRoatedistDayStat.setLong(18,
					rotateSpeedDay.getRotateSpeed400500Time());

			stSaveRoatedistDayStat.setLong(19,
					rotateSpeedDay.getRotateSpeed500600());
			stSaveRoatedistDayStat.setLong(20,
					rotateSpeedDay.getRotateSpeed500600Time());

			stSaveRoatedistDayStat.setLong(21,
					rotateSpeedDay.getRotateSpeed600700());
			stSaveRoatedistDayStat.setLong(22,
					rotateSpeedDay.getRotateSpeed600700Time());

			stSaveRoatedistDayStat.setLong(23,
					rotateSpeedDay.getRotateSpeed700800());
			stSaveRoatedistDayStat.setLong(24,
					rotateSpeedDay.getRotateSpeed700800Time());

			stSaveRoatedistDayStat.setLong(25,
					rotateSpeedDay.getRotateSpeed800900());
			stSaveRoatedistDayStat.setLong(26,
					rotateSpeedDay.getRotateSpeed800900Time());

			stSaveRoatedistDayStat.setLong(27,
					rotateSpeedDay.getRotateSpeed9001000());
			stSaveRoatedistDayStat.setLong(28,
					rotateSpeedDay.getRotateSpeed9001000Time());

			stSaveRoatedistDayStat.setLong(29,
					rotateSpeedDay.getRotateSpeed10001100());
			stSaveRoatedistDayStat.setLong(30,
					rotateSpeedDay.getRotateSpeed10001100Time());

			stSaveRoatedistDayStat.setLong(31,
					rotateSpeedDay.getRotateSpeed11001200());
			stSaveRoatedistDayStat.setLong(32,
					rotateSpeedDay.getRotateSpeed11001200Time());

			stSaveRoatedistDayStat.setLong(33,
					rotateSpeedDay.getRotateSpeed12001300());
			stSaveRoatedistDayStat.setLong(34,
					rotateSpeedDay.getRotateSpeed12001300Time());

			stSaveRoatedistDayStat.setLong(35,
					rotateSpeedDay.getRotateSpeed13001400());
			stSaveRoatedistDayStat.setLong(36,
					rotateSpeedDay.getRotateSpeed13001400Time());

			stSaveRoatedistDayStat.setLong(37,
					rotateSpeedDay.getRotateSpeed14001500());
			stSaveRoatedistDayStat.setLong(38,
					rotateSpeedDay.getRotateSpeed14001500Time());

			stSaveRoatedistDayStat.setLong(39,
					rotateSpeedDay.getRotateSpeed15001600());
			stSaveRoatedistDayStat.setLong(40,
					rotateSpeedDay.getRotateSpeed15001600Time());

			stSaveRoatedistDayStat.setLong(41,
					rotateSpeedDay.getRotateSpeed16001700());
			stSaveRoatedistDayStat.setLong(42,
					rotateSpeedDay.getRotateSpeed16001700Time());

			stSaveRoatedistDayStat.setLong(43,
					rotateSpeedDay.getRotateSpeed17001800());
			stSaveRoatedistDayStat.setLong(44,
					rotateSpeedDay.getRotateSpeed17001800Time());

			stSaveRoatedistDayStat.setLong(45,
					rotateSpeedDay.getRotateSpeed18001900());
			stSaveRoatedistDayStat.setLong(46,
					rotateSpeedDay.getRotateSpeed18001900Time());

			stSaveRoatedistDayStat.setLong(47,
					rotateSpeedDay.getRotateSpeed19002000());
			stSaveRoatedistDayStat.setLong(48,
					rotateSpeedDay.getRotateSpeed19002000Time());

			stSaveRoatedistDayStat.setLong(49,
					rotateSpeedDay.getRotateSpeed20002100());
			stSaveRoatedistDayStat.setLong(50,
					rotateSpeedDay.getRotateSpeed20002100Time());

			stSaveRoatedistDayStat.setLong(51,
					rotateSpeedDay.getRotateSpeed21002200());
			stSaveRoatedistDayStat.setLong(52,
					rotateSpeedDay.getRotateSpeed21002200Time());

			stSaveRoatedistDayStat.setLong(53,
					rotateSpeedDay.getRotateSpeed22002300());
			stSaveRoatedistDayStat.setLong(54,
					rotateSpeedDay.getRotateSpeed22002300Time());

			stSaveRoatedistDayStat.setLong(55,
					rotateSpeedDay.getRotateSpeed23002400());
			stSaveRoatedistDayStat.setLong(56,
					rotateSpeedDay.getRotateSpeed23002400Time());

			stSaveRoatedistDayStat.setLong(57,
					rotateSpeedDay.getRotateSpeed24002500());
			stSaveRoatedistDayStat.setLong(58,
					rotateSpeedDay.getRotateSpeed24002500Time());

			stSaveRoatedistDayStat.setLong(59,
					rotateSpeedDay.getRotateSpeed25002600());
			stSaveRoatedistDayStat.setLong(60,
					rotateSpeedDay.getRotateSpeed25002600Time());

			stSaveRoatedistDayStat.setLong(61,
					rotateSpeedDay.getRotateSpeed26002700());
			stSaveRoatedistDayStat.setLong(62,
					rotateSpeedDay.getRotateSpeed26002700Time());

			stSaveRoatedistDayStat.setLong(63,
					rotateSpeedDay.getRotateSpeed27002800());
			stSaveRoatedistDayStat.setLong(64,
					rotateSpeedDay.getRotateSpeed27002800Time());

			stSaveRoatedistDayStat.setLong(65,
					rotateSpeedDay.getRotateSpeed28002900());
			stSaveRoatedistDayStat.setLong(66,
					rotateSpeedDay.getRotateSpeed28002900Time());

			stSaveRoatedistDayStat.setLong(67,
					rotateSpeedDay.getRotateSpeed29003000());
			stSaveRoatedistDayStat.setLong(68,
					rotateSpeedDay.getRotateSpeed29003000Time());

			stSaveRoatedistDayStat.setLong(69,
					rotateSpeedDay.getRotateSpeedMax());
			stSaveRoatedistDayStat.setLong(70,
					rotateSpeedDay.getRotateSpeedMaxTime());

			stSaveRoatedistDayStat.setLong(71,
					rotateSpeedDay.getPercent6080Fuhelv());

			stSaveRoatedistDayStat.setLong(72,
					rotateSpeedDay.getMinRotateSpeed());
			stSaveRoatedistDayStat.setLong(73,
					rotateSpeedDay.getMaxRotateSpeed());

			stSaveRoatedistDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计 " + vid + " 转速分析表出错。", e);
		} finally {
			if(null != stSaveRoatedistDayStat ){
				stSaveRoatedistDayStat.close();
			}
		}
	}

	/**
	 * //蓄电池电压分布存储
	 * 
	 * @throws SQLException
	 */
	public static void saveVoltageDayStat(String vid,VoltagedistDay voltagedistDay) throws SQLException {
		OracleConnection dbCon = null;
		PreparedStatement stSaveVoltageDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveVoltageDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveVoltageDayStat"));
			stSaveVoltageDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveVoltageDayStat.setString(2, vid);
			if (info != null) {
				stSaveVoltageDayStat.setString(3, info.getVehicleNo());
				stSaveVoltageDayStat.setString(4, info.getVinCode());
			} else {
				stSaveVoltageDayStat.setString(3, null);
				stSaveVoltageDayStat.setString(4, null);
			}
			stSaveVoltageDayStat.setLong(5, voltagedistDay.getStatDate());

			stSaveVoltageDayStat.setLong(6, voltagedistDay.getVoltage0());
			stSaveVoltageDayStat.setLong(7, voltagedistDay.getVoltage0Time());

			stSaveVoltageDayStat.setLong(8, voltagedistDay.getVoltage020());
			stSaveVoltageDayStat.setLong(9, voltagedistDay.getVoltage020Time());

			stSaveVoltageDayStat.setLong(10, voltagedistDay.getVoltage20211());
			stSaveVoltageDayStat.setLong(11,
					voltagedistDay.getVoltage20211Time());

			stSaveVoltageDayStat.setLong(12, voltagedistDay.getVoltage20212());
			stSaveVoltageDayStat.setLong(13,
					voltagedistDay.getVoltage20212Time());

			stSaveVoltageDayStat.setLong(14, voltagedistDay.getVoltage21221());
			stSaveVoltageDayStat.setLong(15,
					voltagedistDay.getVoltage21221Time());

			stSaveVoltageDayStat.setLong(16, voltagedistDay.getVoltage21222());
			stSaveVoltageDayStat.setLong(17,
					voltagedistDay.getVoltage21222Time());

			stSaveVoltageDayStat.setLong(18, voltagedistDay.getVoltage22231());
			stSaveVoltageDayStat.setLong(19,
					voltagedistDay.getVoltage22231Time());

			stSaveVoltageDayStat.setLong(20, voltagedistDay.getVoltage22232());
			stSaveVoltageDayStat.setLong(21,
					voltagedistDay.getVoltage22232Time());

			stSaveVoltageDayStat.setLong(22, voltagedistDay.getVoltage23241());
			stSaveVoltageDayStat.setLong(23,
					voltagedistDay.getVoltage23241Time());

			stSaveVoltageDayStat.setLong(24, voltagedistDay.getVoltage23242());
			stSaveVoltageDayStat.setLong(25,
					voltagedistDay.getVoltage23242Time());

			stSaveVoltageDayStat.setLong(26, voltagedistDay.getVoltage24251());
			stSaveVoltageDayStat.setLong(27,
					voltagedistDay.getVoltage24251Time());

			stSaveVoltageDayStat.setLong(28, voltagedistDay.getVoltage24252());
			stSaveVoltageDayStat.setLong(29,
					voltagedistDay.getVoltage24252Time());

			stSaveVoltageDayStat.setLong(30, voltagedistDay.getVoltage25261());
			stSaveVoltageDayStat.setLong(31,
					voltagedistDay.getVoltage25261Time());

			stSaveVoltageDayStat.setLong(32, voltagedistDay.getVoltage25262());
			stSaveVoltageDayStat.setLong(33,
					voltagedistDay.getVoltage25262Time());

			stSaveVoltageDayStat.setLong(34, voltagedistDay.getVoltage26271());
			stSaveVoltageDayStat.setLong(35,
					voltagedistDay.getVoltage26271Time());

			stSaveVoltageDayStat.setLong(36, voltagedistDay.getVoltage26272());
			stSaveVoltageDayStat.setLong(37,
					voltagedistDay.getVoltage26272Time());

			stSaveVoltageDayStat.setLong(38, voltagedistDay.getVoltage27281());
			stSaveVoltageDayStat.setLong(39,
					voltagedistDay.getVoltage27281Time());

			stSaveVoltageDayStat.setLong(40, voltagedistDay.getVoltage27282());
			stSaveVoltageDayStat.setLong(41,
					voltagedistDay.getVoltage27282Time());

			stSaveVoltageDayStat.setLong(42, voltagedistDay.getVoltage28291());
			stSaveVoltageDayStat.setLong(43,
					voltagedistDay.getVoltage28291Time());

			stSaveVoltageDayStat.setLong(44, voltagedistDay.getVoltage28292());
			stSaveVoltageDayStat.setLong(45,
					voltagedistDay.getVoltage28292Time());

			stSaveVoltageDayStat.setLong(46, voltagedistDay.getVoltage29Max());
			stSaveVoltageDayStat.setLong(47,
					voltagedistDay.getVoltage29MaxTime());

			stSaveVoltageDayStat.setLong(48, voltagedistDay.getMax());
			stSaveVoltageDayStat.setLong(49, voltagedistDay.getMin());
			stSaveVoltageDayStat.setLong(50, voltagedistDay.getSumtime());
			stSaveVoltageDayStat.setLong(51, voltagedistDay.getSumcount());

			stSaveVoltageDayStat.setLong(52, voltagedistDay.getVoltage0121());
			stSaveVoltageDayStat.setLong(53,
					voltagedistDay.getVoltage0121Time());

			stSaveVoltageDayStat.setLong(54, voltagedistDay.getVoltage0122());
			stSaveVoltageDayStat.setLong(55,
					voltagedistDay.getVoltage0122Time());

			stSaveVoltageDayStat.setLong(56, voltagedistDay.getVoltage12131());
			stSaveVoltageDayStat.setLong(57,
					voltagedistDay.getVoltage12131Time());

			stSaveVoltageDayStat.setLong(58, voltagedistDay.getVoltage12132());
			stSaveVoltageDayStat.setLong(59,
					voltagedistDay.getVoltage12132Time());

			stSaveVoltageDayStat.setLong(60, voltagedistDay.getVoltage13141());
			stSaveVoltageDayStat.setLong(61,
					voltagedistDay.getVoltage13141Time());

			stSaveVoltageDayStat.setLong(62, voltagedistDay.getVoltage13142());
			stSaveVoltageDayStat.setLong(63,
					voltagedistDay.getVoltage13142Time());

			stSaveVoltageDayStat.setLong(64, voltagedistDay.getVoltage141());
			stSaveVoltageDayStat
					.setLong(65, voltagedistDay.getVoltage141Time());

			stSaveVoltageDayStat.setLong(66, voltagedistDay.getVoltage14Max());
			stSaveVoltageDayStat.setLong(67,
					voltagedistDay.getVoltage14MaxTime());

			stSaveVoltageDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计 " + vid + "蓄电池电压出错.", e);
		} finally {
			try {
				if(null != stSaveVoltageDayStat ){
					stSaveVoltageDayStat.close();
				}
				if(null != dbCon ){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("将连接放回连接池出错：",e);
			}
		}
	}
	
	/**
	 * //蓄电池电压分布存储
	 * 
	 * @throws SQLException
	 */
	public static void saveVoltageDayStat(OracleConnection dbCon,String vid,VoltagedistDay voltagedistDay) throws SQLException {
		PreparedStatement stSaveVoltageDayStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stSaveVoltageDayStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveVoltageDayStat"));
			stSaveVoltageDayStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveVoltageDayStat.setString(2, vid);
			if (info != null) {
				stSaveVoltageDayStat.setString(3, info.getVehicleNo());
				stSaveVoltageDayStat.setString(4, info.getVinCode());
			} else {
				stSaveVoltageDayStat.setString(3, null);
				stSaveVoltageDayStat.setString(4, null);
			}
			stSaveVoltageDayStat.setLong(5, voltagedistDay.getStatDate());

			stSaveVoltageDayStat.setLong(6, voltagedistDay.getVoltage0());
			stSaveVoltageDayStat.setLong(7, voltagedistDay.getVoltage0Time());

			stSaveVoltageDayStat.setLong(8, voltagedistDay.getVoltage020());
			stSaveVoltageDayStat.setLong(9, voltagedistDay.getVoltage020Time());

			stSaveVoltageDayStat.setLong(10, voltagedistDay.getVoltage20211());
			stSaveVoltageDayStat.setLong(11,
					voltagedistDay.getVoltage20211Time());

			stSaveVoltageDayStat.setLong(12, voltagedistDay.getVoltage20212());
			stSaveVoltageDayStat.setLong(13,
					voltagedistDay.getVoltage20212Time());

			stSaveVoltageDayStat.setLong(14, voltagedistDay.getVoltage21221());
			stSaveVoltageDayStat.setLong(15,
					voltagedistDay.getVoltage21221Time());

			stSaveVoltageDayStat.setLong(16, voltagedistDay.getVoltage21222());
			stSaveVoltageDayStat.setLong(17,
					voltagedistDay.getVoltage21222Time());

			stSaveVoltageDayStat.setLong(18, voltagedistDay.getVoltage22231());
			stSaveVoltageDayStat.setLong(19,
					voltagedistDay.getVoltage22231Time());

			stSaveVoltageDayStat.setLong(20, voltagedistDay.getVoltage22232());
			stSaveVoltageDayStat.setLong(21,
					voltagedistDay.getVoltage22232Time());

			stSaveVoltageDayStat.setLong(22, voltagedistDay.getVoltage23241());
			stSaveVoltageDayStat.setLong(23,
					voltagedistDay.getVoltage23241Time());

			stSaveVoltageDayStat.setLong(24, voltagedistDay.getVoltage23242());
			stSaveVoltageDayStat.setLong(25,
					voltagedistDay.getVoltage23242Time());

			stSaveVoltageDayStat.setLong(26, voltagedistDay.getVoltage24251());
			stSaveVoltageDayStat.setLong(27,
					voltagedistDay.getVoltage24251Time());

			stSaveVoltageDayStat.setLong(28, voltagedistDay.getVoltage24252());
			stSaveVoltageDayStat.setLong(29,
					voltagedistDay.getVoltage24252Time());

			stSaveVoltageDayStat.setLong(30, voltagedistDay.getVoltage25261());
			stSaveVoltageDayStat.setLong(31,
					voltagedistDay.getVoltage25261Time());

			stSaveVoltageDayStat.setLong(32, voltagedistDay.getVoltage25262());
			stSaveVoltageDayStat.setLong(33,
					voltagedistDay.getVoltage25262Time());

			stSaveVoltageDayStat.setLong(34, voltagedistDay.getVoltage26271());
			stSaveVoltageDayStat.setLong(35,
					voltagedistDay.getVoltage26271Time());

			stSaveVoltageDayStat.setLong(36, voltagedistDay.getVoltage26272());
			stSaveVoltageDayStat.setLong(37,
					voltagedistDay.getVoltage26272Time());

			stSaveVoltageDayStat.setLong(38, voltagedistDay.getVoltage27281());
			stSaveVoltageDayStat.setLong(39,
					voltagedistDay.getVoltage27281Time());

			stSaveVoltageDayStat.setLong(40, voltagedistDay.getVoltage27282());
			stSaveVoltageDayStat.setLong(41,
					voltagedistDay.getVoltage27282Time());

			stSaveVoltageDayStat.setLong(42, voltagedistDay.getVoltage28291());
			stSaveVoltageDayStat.setLong(43,
					voltagedistDay.getVoltage28291Time());

			stSaveVoltageDayStat.setLong(44, voltagedistDay.getVoltage28292());
			stSaveVoltageDayStat.setLong(45,
					voltagedistDay.getVoltage28292Time());

			stSaveVoltageDayStat.setLong(46, voltagedistDay.getVoltage29Max());
			stSaveVoltageDayStat.setLong(47,
					voltagedistDay.getVoltage29MaxTime());

			stSaveVoltageDayStat.setLong(48, voltagedistDay.getMax());
			stSaveVoltageDayStat.setLong(49, voltagedistDay.getMin());
			stSaveVoltageDayStat.setLong(50, voltagedistDay.getSumtime());
			stSaveVoltageDayStat.setLong(51, voltagedistDay.getSumcount());

			stSaveVoltageDayStat.setLong(52, voltagedistDay.getVoltage0121());
			stSaveVoltageDayStat.setLong(53,
					voltagedistDay.getVoltage0121Time());

			stSaveVoltageDayStat.setLong(54, voltagedistDay.getVoltage0122());
			stSaveVoltageDayStat.setLong(55,
					voltagedistDay.getVoltage0122Time());

			stSaveVoltageDayStat.setLong(56, voltagedistDay.getVoltage12131());
			stSaveVoltageDayStat.setLong(57,
					voltagedistDay.getVoltage12131Time());

			stSaveVoltageDayStat.setLong(58, voltagedistDay.getVoltage12132());
			stSaveVoltageDayStat.setLong(59,
					voltagedistDay.getVoltage12132Time());

			stSaveVoltageDayStat.setLong(60, voltagedistDay.getVoltage13141());
			stSaveVoltageDayStat.setLong(61,
					voltagedistDay.getVoltage13141Time());

			stSaveVoltageDayStat.setLong(62, voltagedistDay.getVoltage13142());
			stSaveVoltageDayStat.setLong(63,
					voltagedistDay.getVoltage13142Time());

			stSaveVoltageDayStat.setLong(64, voltagedistDay.getVoltage141());
			stSaveVoltageDayStat
					.setLong(65, voltagedistDay.getVoltage141Time());

			stSaveVoltageDayStat.setLong(66, voltagedistDay.getVoltage14Max());
			stSaveVoltageDayStat.setLong(67,
					voltagedistDay.getVoltage14MaxTime());

			stSaveVoltageDayStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计 " + vid + "蓄电池电压出错.", e);
		} finally {
			if(null != stSaveVoltageDayStat ){
				stSaveVoltageDayStat.close();
			}
		}
	}

	/*****
	 * 存储进气温度
	 * 
	 * @param vid
	 */
	public static void saveAirTemperture(String vid,AirTempertureBean airTempertureBean) {
		OracleConnection dbCon = null;
		PreparedStatement stSaveAirTempertureStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveAirTempertureStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveAirTemperture"));
			stSaveAirTempertureStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveAirTempertureStat.setString(2, vid);
			if (info != null) {
				stSaveAirTempertureStat.setString(3, info.getVehicleNo());
				stSaveAirTempertureStat.setString(4, info.getVinCode());
			} else {
				stSaveAirTempertureStat.setString(3, null);
				stSaveAirTempertureStat.setString(4, null);
			}
			stSaveAirTempertureStat.setLong(5, airTempertureBean.getStatDate());
			stSaveAirTempertureStat.setLong(6, airTempertureBean
					.getTemperature_0().getNum());
			stSaveAirTempertureStat.setLong(7, airTempertureBean
					.getTemperature_0().getTime());
			stSaveAirTempertureStat.setLong(8, airTempertureBean
					.getTemperature_0_10().getNum());
			stSaveAirTempertureStat.setLong(9, airTempertureBean
					.getTemperature_0_10().getTime());
			stSaveAirTempertureStat.setLong(10, airTempertureBean
					.getTemperature_10_20().getNum());
			stSaveAirTempertureStat.setLong(11, airTempertureBean
					.getTemperature_10_20().getTime());
			stSaveAirTempertureStat.setLong(12, airTempertureBean
					.getTemperature_20_25().getNum());
			stSaveAirTempertureStat.setLong(13, airTempertureBean
					.getTemperature_20_25().getTime());
			stSaveAirTempertureStat.setLong(14, airTempertureBean
					.getTemperature_25_30().getNum());
			stSaveAirTempertureStat.setLong(15, airTempertureBean
					.getTemperature_25_30().getTime());
			stSaveAirTempertureStat.setLong(16, airTempertureBean
					.getTemperature_30_35().getNum());
			stSaveAirTempertureStat.setLong(17, airTempertureBean
					.getTemperature_30_35().getTime());
			stSaveAirTempertureStat.setLong(18, airTempertureBean
					.getTemperature_35_40().getNum());
			stSaveAirTempertureStat.setLong(19, airTempertureBean
					.getTemperature_35_40().getTime());
			stSaveAirTempertureStat.setLong(20, airTempertureBean
					.getTemperature_40_45().getNum());
			stSaveAirTempertureStat.setLong(21, airTempertureBean
					.getTemperature_40_45().getTime());
			stSaveAirTempertureStat.setLong(22, airTempertureBean
					.getTemperature_45_50().getNum());
			stSaveAirTempertureStat.setLong(23, airTempertureBean
					.getTemperature_45_50().getTime());
			stSaveAirTempertureStat.setLong(24, airTempertureBean
					.getTemperature_50_60().getNum());
			stSaveAirTempertureStat.setLong(25, airTempertureBean
					.getTemperature_50_60().getTime());
			stSaveAirTempertureStat.setLong(26, airTempertureBean
					.getTemperature_60_70().getNum());
			stSaveAirTempertureStat.setLong(27, airTempertureBean
					.getTemperature_60_70().getTime());
			stSaveAirTempertureStat.setLong(28, airTempertureBean
					.getTemperature_70().getNum());
			stSaveAirTempertureStat.setLong(29, airTempertureBean
					.getTemperature_70().getTime());
			stSaveAirTempertureStat.setLong(30, airTempertureBean.getMax());
			stSaveAirTempertureStat.setLong(31, airTempertureBean.getMin());
			stSaveAirTempertureStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计进气温度出错." + vid, e);
		} finally {
			try {
				if(null != stSaveAirTempertureStat ){
					stSaveAirTempertureStat.close();
				}
				if(null != dbCon ){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("将连接放回连接池出错：",e);
			}
		}
	}
	
	/*****
	 * 存储进气温度
	 * 
	 * @param vid
	 */
	public static void saveAirTemperture(OracleConnection dbCon,String vid,AirTempertureBean airTempertureBean) throws Exception{
		PreparedStatement stSaveAirTempertureStat = null;
		try {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stSaveAirTempertureStat = dbCon
					.prepareStatement(SQLPool.getinstance().getSql(
					"sql_saveAirTemperture"));
			stSaveAirTempertureStat.setString(1, GeneratorPK.instance().getPKString());
			stSaveAirTempertureStat.setString(2, vid);
			if (info != null) {
				stSaveAirTempertureStat.setString(3, info.getVehicleNo());
				stSaveAirTempertureStat.setString(4, info.getVinCode());
			} else {
				stSaveAirTempertureStat.setString(3, null);
				stSaveAirTempertureStat.setString(4, null);
			}
			stSaveAirTempertureStat.setLong(5, airTempertureBean.getStatDate());
			stSaveAirTempertureStat.setLong(6, airTempertureBean
					.getTemperature_0().getNum());
			stSaveAirTempertureStat.setLong(7, airTempertureBean
					.getTemperature_0().getTime());
			stSaveAirTempertureStat.setLong(8, airTempertureBean
					.getTemperature_0_10().getNum());
			stSaveAirTempertureStat.setLong(9, airTempertureBean
					.getTemperature_0_10().getTime());
			stSaveAirTempertureStat.setLong(10, airTempertureBean
					.getTemperature_10_20().getNum());
			stSaveAirTempertureStat.setLong(11, airTempertureBean
					.getTemperature_10_20().getTime());
			stSaveAirTempertureStat.setLong(12, airTempertureBean
					.getTemperature_20_25().getNum());
			stSaveAirTempertureStat.setLong(13, airTempertureBean
					.getTemperature_20_25().getTime());
			stSaveAirTempertureStat.setLong(14, airTempertureBean
					.getTemperature_25_30().getNum());
			stSaveAirTempertureStat.setLong(15, airTempertureBean
					.getTemperature_25_30().getTime());
			stSaveAirTempertureStat.setLong(16, airTempertureBean
					.getTemperature_30_35().getNum());
			stSaveAirTempertureStat.setLong(17, airTempertureBean
					.getTemperature_30_35().getTime());
			stSaveAirTempertureStat.setLong(18, airTempertureBean
					.getTemperature_35_40().getNum());
			stSaveAirTempertureStat.setLong(19, airTempertureBean
					.getTemperature_35_40().getTime());
			stSaveAirTempertureStat.setLong(20, airTempertureBean
					.getTemperature_40_45().getNum());
			stSaveAirTempertureStat.setLong(21, airTempertureBean
					.getTemperature_40_45().getTime());
			stSaveAirTempertureStat.setLong(22, airTempertureBean
					.getTemperature_45_50().getNum());
			stSaveAirTempertureStat.setLong(23, airTempertureBean
					.getTemperature_45_50().getTime());
			stSaveAirTempertureStat.setLong(24, airTempertureBean
					.getTemperature_50_60().getNum());
			stSaveAirTempertureStat.setLong(25, airTempertureBean
					.getTemperature_50_60().getTime());
			stSaveAirTempertureStat.setLong(26, airTempertureBean
					.getTemperature_60_70().getNum());
			stSaveAirTempertureStat.setLong(27, airTempertureBean
					.getTemperature_60_70().getTime());
			stSaveAirTempertureStat.setLong(28, airTempertureBean
					.getTemperature_70().getNum());
			stSaveAirTempertureStat.setLong(29, airTempertureBean
					.getTemperature_70().getTime());
			stSaveAirTempertureStat.setLong(30, airTempertureBean.getMax());
			stSaveAirTempertureStat.setLong(31, airTempertureBean.getMin());
			stSaveAirTempertureStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计进气温度出错." + vid, e);
		} finally {
			if(null != stSaveAirTempertureStat ){
				stSaveAirTempertureStat.close();
			}
		}
	}
	
	/*****
	 * 查询前一天报警
	 * @throws SQLException 
	 */
	public static ResultSet searchAlarmDays(String vid,long beginUtc,long endUtc){
		OracleConnection dbCon = null;
		PreparedStatement stSearchAlarmDays = null;
		ResultSet rs = null;
		try {
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSearchAlarmDays = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_searchAlarmDays"));
			stSearchAlarmDays.setString(1, vid);
			stSearchAlarmDays.setLong(2, beginUtc); // 指定时间凌晨UTC时间
			stSearchAlarmDays.setLong(3, endUtc); // 指定时间24小时后时间
			
			rs = stSearchAlarmDays.executeQuery();
			return rs;
		} catch (SQLException e) {
			logger.error("查询前一天报警出错." + vid,e);
		}finally{
			try{
				if(rs != null){
					rs.close();
				}
				if(stSearchAlarmDays != null){
					stSearchAlarmDays.close();
				}
				if(null != dbCon ){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("将连接放回连接池出错：",e);
			}
		}
		return null;
	}
	
	public static List<String> selectOpenningDoorPic(String vid,long beginUtc,long endUtc) throws SQLException{
		OracleConnection dbCon = null;
		PreparedStatement stSelectOpenningDoorPic = null;
		ResultSet rs = null;
		List<String> ls = new ArrayList<String>();
		 try {
			 dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			 stSelectOpenningDoorPic = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_queryOpenningDoorPicture"));
			 stSelectOpenningDoorPic.setString(1, vid);
			 stSelectOpenningDoorPic.setLong(2, beginUtc);
			 stSelectOpenningDoorPic.setLong(3, endUtc);
			 rs = stSelectOpenningDoorPic.executeQuery();
			 while(rs.next()){
				 ls.add(rs.getLong("UTC") + ";" +  rs.getString("MEDIA_URI") + ";" + rs.getString("MTYPE_CODE"));
			 }
		} catch (SQLException e) {
			logger.error(e.getMessage(),e);
		}finally{
			if(rs != null){
				rs.close();
			}
			if(stSelectOpenningDoorPic != null){
				stSelectOpenningDoorPic.close();
			}
			if(null != dbCon ){
				dbCon.close();
			}
		}
		return ls;
	}
	

	public static List<String> selectOpenningDoorPic(OracleConnection dbCon,String vid,long beginUtc,long endUtc) throws Exception{
		PreparedStatement stSelectOpenningDoorPic = null;
		ResultSet rs = null;
		List<String> ls = new ArrayList<String>();
		 try {
			 stSelectOpenningDoorPic = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_queryOpenningDoorPicture"));
			 stSelectOpenningDoorPic.setString(1, vid);
			 stSelectOpenningDoorPic.setLong(2, beginUtc);
			 stSelectOpenningDoorPic.setLong(3, endUtc);
			 rs = stSelectOpenningDoorPic.executeQuery();
			 while(rs.next()){
				 ls.add(rs.getLong("UTC") + ";" +  rs.getString("MEDIA_URI") + ";" + rs.getString("MTYPE_CODE"));
			 }
		} catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			if(rs != null){
				rs.close();
			}
			if(stSelectOpenningDoorPic != null){
				stSelectOpenningDoorPic.close();
			}
		}
		return ls;
	}
	
	/**
	 * 保存当前车辆状态事件
	 * @param file
	 * @throws IOException
	 * @throws NumberFormatException
	 */
	public static void saveStateEventInfo(OracleConnection dbCon,String vid,Vector<AlarmCacheBean> stateEventList) {
		PreparedStatement stSaveDriverEventInfo = null;
		String tmpkey = "";
		try{
			if (stateEventList != null&&stateEventList.size()>0) {
				if (dbCon!=null){
				stSaveDriverEventInfo = (OraclePreparedStatement)dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveStateEventInfo"));
				
				VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
				
				String vehicle_no = info.getVehicleNo();
				String corp_id = info.getEntId();
				String corp_name = info.getEntName();
				String team_id = info.getTeamId();
				String team_name = info.getTeamName();
				String vline_id = info.getVlineId();
				String vline_name = info.getLineName();
				
				Iterator<AlarmCacheBean> eventList = stateEventList.iterator();
				int count = 0;
				while(eventList.hasNext()){
					AlarmCacheBean alarmCacheBean = eventList.next();

						VehicleMessageBean beginVmb = alarmCacheBean.getBeginVmb();
						VehicleMessageBean endVmb = alarmCacheBean.getEndVmb();
						String id = UUID.randomUUID().toString().replace("-", "");
						tmpkey += ":" +id;
						stSaveDriverEventInfo.setString(1, id);
						stSaveDriverEventInfo.setString(2, vid);
						stSaveDriverEventInfo.setString(3, vehicle_no);
						stSaveDriverEventInfo.setString(4, corp_id);
						stSaveDriverEventInfo.setString(5, corp_name);
						
						stSaveDriverEventInfo.setString(6, team_id);
						stSaveDriverEventInfo.setString(7, team_name);
						
						if(vline_id != null && !"".equals(vline_id)){
							stSaveDriverEventInfo.setString(8, vline_id);
						}else{
							stSaveDriverEventInfo.setNull(8, Types.VARCHAR);
						}
						
						stSaveDriverEventInfo.setString(9, vline_name);
						
						stSaveDriverEventInfo.setString(10, alarmCacheBean.getAlarmcode());
						stSaveDriverEventInfo.setLong(11, beginVmb.getUtc());
						stSaveDriverEventInfo.setLong(12, beginVmb.getLat());
						stSaveDriverEventInfo.setLong(13, beginVmb.getLon());
						stSaveDriverEventInfo.setLong(14, beginVmb.getMaplon());
						stSaveDriverEventInfo.setLong(15, beginVmb.getMaplat());
						stSaveDriverEventInfo.setLong(16, beginVmb.getElevation());
						stSaveDriverEventInfo.setLong(17, beginVmb.getDir());
						stSaveDriverEventInfo.setLong(18, beginVmb.getSpeed());
						
						stSaveDriverEventInfo.setLong(19, endVmb.getUtc());
						stSaveDriverEventInfo.setLong(20, endVmb.getLat());
						stSaveDriverEventInfo.setLong(21, endVmb.getLon());
						stSaveDriverEventInfo.setLong(22, endVmb.getMaplon());
						stSaveDriverEventInfo.setLong(23, endVmb.getMaplat());
						stSaveDriverEventInfo.setLong(24, endVmb.getElevation());
						stSaveDriverEventInfo.setLong(25, endVmb.getDir());
						stSaveDriverEventInfo.setLong(26, endVmb.getSpeed());
						
						long use_time = (endVmb.getUtc() - beginVmb.getUtc())/1000;
						
						if (use_time>0){
							stSaveDriverEventInfo.setLong(27, use_time);
						}else{
							stSaveDriverEventInfo.setNull(27, Types.INTEGER);
						}
						
						if (alarmCacheBean.getMaxSpeed()>0){
							stSaveDriverEventInfo.setLong(28, alarmCacheBean.getMaxSpeed());
						}else{
							stSaveDriverEventInfo.setNull(28, Types.INTEGER);
						}
						
						if (alarmCacheBean.getMileage()>0){
							stSaveDriverEventInfo.setLong(29, alarmCacheBean.getMileage());
						}else{
							stSaveDriverEventInfo.setNull(29, Types.INTEGER);
						}
						
						if (alarmCacheBean.getOil()>0){
							stSaveDriverEventInfo.setLong(30, alarmCacheBean.getOil());
						}else{
							stSaveDriverEventInfo.setNull(30, Types.INTEGER);
						}
						
						stSaveDriverEventInfo.setString(31, beginVmb.getDriverId());
						stSaveDriverEventInfo.setString(32, beginVmb.getDriverName());
						stSaveDriverEventInfo.setString(33, beginVmb.getDriverSrc());
						
						stSaveDriverEventInfo.addBatch();
						
						count++;
						if (count>=50){
							stSaveDriverEventInfo.executeBatch();
							count=0;
							tmpkey = "";
						}
				}
				if (count>0){
					stSaveDriverEventInfo.executeBatch();
				}
			}
			}
		}catch(Exception e){
			logger.error(vid + " 存储车辆状态事件信息出错.",e);
			logger.debug(vid + "stateEventList size:"+stateEventList.size()+" keys==="+tmpkey);
		}finally{
			try{
				if (stSaveDriverEventInfo!=null){
					stSaveDriverEventInfo.close();
				}
			}catch(Exception e){
				logger.error("关闭statement失败：",e);
			}
		}
	}
	
			 
	public static void saveVehicleAlarm(OracleConnection dbCon,String vid,Vector<AlarmCacheBean> alarmList)
	throws SQLException {
	
	OraclePreparedStatement stSaveVehicleAlarm =null;
	try {
		if (alarmList != null&&alarmList.size()>0) {
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stSaveVehicleAlarm = (OraclePreparedStatement) dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveVehicleAlarm"));
			
			Iterator<AlarmCacheBean> eventList = alarmList.iterator();
			int count = 0;
			while(eventList.hasNext()){
				AlarmCacheBean alarmCacheBean = eventList.next();
				
				stSaveVehicleAlarm.setString(1, alarmCacheBean.getAlarmId());
				stSaveVehicleAlarm.setString(2, vid);
				stSaveVehicleAlarm.setString(3, info.getVehicleNo());
				stSaveVehicleAlarm.setString(4, info.getTeamId());
				stSaveVehicleAlarm.setString(5, info.getTeamName());
				
				stSaveVehicleAlarm.setString(6, info.getEntId());
				stSaveVehicleAlarm.setString(7, info.getEntName());
				stSaveVehicleAlarm.setString(8, alarmCacheBean.getAlarmcode());
				stSaveVehicleAlarm.setInt(9, alarmCacheBean.getAlarmSrc());
				stSaveVehicleAlarm.setString(10, alarmCacheBean.getAlarmlevel());
				
				VehicleMessageBean beginVmb = alarmCacheBean.getBeginVmb();
				
				stSaveVehicleAlarm.setLong(11, beginVmb.getUtc());
				stSaveVehicleAlarm.setLong(12, beginVmb.getLat());
				stSaveVehicleAlarm.setLong(13, beginVmb.getLon());
				stSaveVehicleAlarm.setLong(14, beginVmb.getMaplat());
				stSaveVehicleAlarm.setLong(15, beginVmb.getMaplon());
				
				stSaveVehicleAlarm.setLong(16, beginVmb.getElevation());
				stSaveVehicleAlarm.setInt(17, beginVmb.getDir());
				stSaveVehicleAlarm.setLong(18, beginVmb.getSpeed());
				stSaveVehicleAlarm.setLong(19, beginVmb.getMileage());
				stSaveVehicleAlarm.setLong(20, beginVmb.getOil());
				
				stSaveVehicleAlarm.setLong(21, beginVmb.getUtc());
				
				VehicleMessageBean endVmb = alarmCacheBean.getEndVmb();
				
				stSaveVehicleAlarm.setLong(22, endVmb.getUtc());
				if (endVmb.getLat()>0){
					stSaveVehicleAlarm.setLong(23, endVmb.getLat());
				}else{
					stSaveVehicleAlarm.setNull(23, Types.INTEGER);
				}
				if (endVmb.getLon()>0){
					stSaveVehicleAlarm.setLong(24, endVmb.getLon());
				}else{
					stSaveVehicleAlarm.setNull(24, Types.INTEGER);
				}
				if (endVmb.getMaplon()>0){
					stSaveVehicleAlarm.setLong(25, endVmb.getMaplon());
				}else{
					stSaveVehicleAlarm.setNull(25, Types.INTEGER);
				}
				
				if (endVmb.getMaplat()>0){
					stSaveVehicleAlarm.setLong(26, endVmb.getMaplat());
				}else{
					stSaveVehicleAlarm.setNull(26, Types.INTEGER);
				}
				if (endVmb.getElevation()>0){
					stSaveVehicleAlarm.setLong(27, endVmb.getElevation());
				}else{
					stSaveVehicleAlarm.setNull(27, Types.INTEGER);
				}
				if (endVmb.getDir()>0){
					stSaveVehicleAlarm.setInt(28, endVmb.getDir());
				}else{
					stSaveVehicleAlarm.setNull(28, Types.INTEGER);
				}
				if (endVmb.getSpeed()>=0){
					stSaveVehicleAlarm.setLong(29, endVmb.getSpeed());
				}else{
					stSaveVehicleAlarm.setNull(29, Types.INTEGER);
				}
				if (endVmb.getMileage()>0){
					stSaveVehicleAlarm.setLong(30, endVmb.getMileage());
				}else{
					stSaveVehicleAlarm.setNull(30, Types.INTEGER);
				}
				
				if (endVmb.getOil()>0){
					stSaveVehicleAlarm.setLong(31, endVmb.getOil());
				}else{
					stSaveVehicleAlarm.setNull(31, Types.INTEGER);
				}
				
				stSaveVehicleAlarm.setString(32, alarmCacheBean.getAlarmadd());
				if (alarmCacheBean.getSpeedThreshold() > 0) {
					stSaveVehicleAlarm.setDouble(33,
							alarmCacheBean.getSpeedThreshold());
				} else {
					stSaveVehicleAlarm.setNull(33, Types.INTEGER);
				}
				
				if (alarmCacheBean.getMaxSpeed()> 0){
					stSaveVehicleAlarm.setLong(34, alarmCacheBean.getMaxSpeed());
				}else{
					stSaveVehicleAlarm.setNull(34, Types.INTEGER);
				}
				
				if (alarmCacheBean.getAvgSpeed()> 0){
					stSaveVehicleAlarm.setLong(35, alarmCacheBean.getAvgSpeed());
				}else{
					stSaveVehicleAlarm.setNull(35, Types.INTEGER);
				}
				
				stSaveVehicleAlarm.setLong(36, System.currentTimeMillis());
				stSaveVehicleAlarm.setString(37, "");
				
				stSaveVehicleAlarm.addBatch();
				
				count++;
				if (count>=50){
					stSaveVehicleAlarm.executeBatch();
					count=0;
				}
			}

			if (count>0){
				stSaveVehicleAlarm.executeBatch();
			}
		}
	}catch(Exception ex){
		logger.error("保存告警信息出错（ORA）：",ex);
	} finally {
		if (stSaveVehicleAlarm != null) {
			stSaveVehicleAlarm.close();
		}
	}

}
	
	/**
	 * 查询驾驶员在给定时段内的打卡明细
	 * @param dbCon
	 * @param vid
	 * @param beginUtc
	 * @param endUtc
	 * @return
	 * @throws Exception
	 */
	public static List<String> selectDriverClockinDetail(OracleConnection dbCon,String vid,long beginUtc,long endUtc) throws Exception{
		PreparedStatement stSelectClockin = null;
		ResultSet rs = null;
		List<String> ls = new ArrayList<String>();
		 try {
			 stSelectClockin = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_queryDriverClockinDetail"));
			 stSelectClockin.setString(1, vid);
			 stSelectClockin.setLong(2, beginUtc);
			 stSelectClockin.setLong(3, endUtc);
			 stSelectClockin.setLong(4, beginUtc);
			 stSelectClockin.setLong(5, endUtc);
			 stSelectClockin.setString(6, vid);
			 
			 rs = stSelectClockin.executeQuery();
			 while(rs.next()){
				 ls.add(rs.getString("DEVICE_ID") + ";" + rs.getString("DRIVER_NAME") + ";"+ rs.getString("DRIVER_SRC") + ";" + rs.getLong("ON_LINE_TIME") + ";" +  rs.getLong("OFF_LINE_TIME"));
			 }
		} catch (SQLException e) {
			logger.error(e.getMessage(), e);
		}finally{
			if(rs != null){
				rs.close();
			}
			if(stSelectClockin != null){
				stSelectClockin.close();
			}
		}
		return ls;
	}
	
	/***
	 *  存储驾驶员驾驶明细数据
	 * @throws SQLException
	 */
	public static void saveDriverDetail(OracleConnection dbCon,String vid,List<DriverDetailBean> driverDetaillist) throws Exception{
		if (driverDetaillist.size()==0){
			return ;
		}
		PreparedStatement stSaveDriverDetailInfo = null;
		int count = 0;
		try{
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			String cfgFlag = "0";
			if (info!=null){
				cfgFlag = info.getCfgFlag();
			}
			
			stSaveDriverDetailInfo = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveDriverDetailInfo"));
			
			for (int i = 0;i < driverDetaillist.size(); i++){
				DriverDetailBean ddb = driverDetaillist.get(i);
				stSaveDriverDetailInfo.setString(1, ddb.getDetailId());
				stSaveDriverDetailInfo.setLong(2, ddb.getStatDate()); // 日期UTC
				stSaveDriverDetailInfo.setString(3, vid); // 车辆编号
				if(info != null){
					stSaveDriverDetailInfo.setString(4, info.getVehicleNo()); // 车牌号码
					stSaveDriverDetailInfo.setString(5, info.getEntId()); // 企业ID
					stSaveDriverDetailInfo.setString(6, info.getEntName()); // 企业名称
					stSaveDriverDetailInfo.setString(7, info.getTeamId()); // 车队ID
					stSaveDriverDetailInfo.setString(8, info.getTeamName()); // 车队名称
				} else {
					stSaveDriverDetailInfo.setString(3, null); // 车牌号码
					stSaveDriverDetailInfo.setString(5, null); // 企业ID
					stSaveDriverDetailInfo.setString(6, null); // 企业名称
					stSaveDriverDetailInfo.setString(7, null); // 车队ID
					stSaveDriverDetailInfo.setString(8, null); // 车队名称
				}

				VehicleMessageBean beginVmb = ddb.getBeginVmb();
				
				stSaveDriverDetailInfo.setString(9, beginVmb.getDriverId()); // 驾驶员编号
				stSaveDriverDetailInfo.setString(10, beginVmb.getDriverName()); // 驾驶员名称
				stSaveDriverDetailInfo.setString(11, beginVmb.getDriverSrc()); // 驾驶员信息来源
				
				stSaveDriverDetailInfo.setLong(12, beginVmb.getUtc()); // 开始时间
				stSaveDriverDetailInfo.setLong(13, beginVmb.getLon()); // 经度
				stSaveDriverDetailInfo.setLong(14, beginVmb.getLat()); // 维度
				stSaveDriverDetailInfo.setLong(15, beginVmb.getMaplon()); // 地图经度
				stSaveDriverDetailInfo.setLong(16, beginVmb.getMaplat()); // 地图维度
				stSaveDriverDetailInfo.setLong(17, beginVmb.getElevation()); // 高程
				stSaveDriverDetailInfo.setLong(18, beginVmb.getDir()); // 方向
				stSaveDriverDetailInfo.setLong(19, beginVmb.getSpeed()); // 速度
				stSaveDriverDetailInfo.setLong(20, beginVmb.getMileage()); //里程
				stSaveDriverDetailInfo.setLong(21, beginVmb.getOil()); // 油耗
				
				VehicleMessageBean endVmb = ddb.getEndVmb();
				stSaveDriverDetailInfo.setLong(22, endVmb.getUtc()); // 开始时间
				stSaveDriverDetailInfo.setLong(23, endVmb.getLon()); // 经度
				stSaveDriverDetailInfo.setLong(24, endVmb.getLat()); // 维度
				stSaveDriverDetailInfo.setLong(25, endVmb.getMaplon()); // 地图经度
				stSaveDriverDetailInfo.setLong(26, endVmb.getMaplat()); // 地图维度
				stSaveDriverDetailInfo.setLong(27, endVmb.getElevation()); // 高程
				stSaveDriverDetailInfo.setLong(28, endVmb.getDir()); // 方向
				stSaveDriverDetailInfo.setLong(29, endVmb.getSpeed()); // 速度
				stSaveDriverDetailInfo.setLong(30, endVmb.getMileage()); //里程
				stSaveDriverDetailInfo.setLong(31, endVmb.getOil()); // 油耗
				
				stSaveDriverDetailInfo.setLong(32, ddb.getMileage()); //驾驶员驾驶里程
				if ("1".equals(cfgFlag)){
					stSaveDriverDetailInfo.setDouble(33, ddb.getMetOilWear()*0.02);//油耗
					stSaveDriverDetailInfo.setDouble(34, ddb.getMetRunningOilWear()*0.02);//行车油耗
				}else{
					stSaveDriverDetailInfo.setDouble(33, ddb.getEcuOilWear());
					stSaveDriverDetailInfo.setDouble(34, ddb.getEcuRunningOilWear());
				}
				stSaveDriverDetailInfo.setString(35, cfgFlag); //油耗标记
				
				stSaveDriverDetailInfo.setLong(36, (endVmb.getUtc() - beginVmb.getUtc())/1000); //驾驶时长
				stSaveDriverDetailInfo.setLong(37, ddb.getEngineRotateTime()); //发动机工作时长
				stSaveDriverDetailInfo.setLong(38, ddb.getRunningTime()); //行车时长
				stSaveDriverDetailInfo.setLong(39, ddb.getEcuOilWear()); //ecu油耗
				stSaveDriverDetailInfo.setLong(40, ddb.getEcuRunningOilWear()); //ecu行车油耗
				
				stSaveDriverDetailInfo.setLong(41, ddb.getEcuOilWear() - ddb.getEcuRunningOilWear()); //ecu怠速油耗
				stSaveDriverDetailInfo.setLong(42, ddb.getMetOilWear()); //met油耗
				stSaveDriverDetailInfo.setLong(43, ddb.getMetRunningOilWear()); //met行车油耗
				stSaveDriverDetailInfo.setLong(44, ddb.getMetOilWear() - ddb.getMetRunningOilWear()); //met怠速油耗
				stSaveDriverDetailInfo.setLong(45, ddb.getAccCloseNum()); //acc开次数
				stSaveDriverDetailInfo.setLong(46, ddb.getAccCloseTime()); //acc开时长
				
				stSaveDriverDetailInfo.setLong(47, ddb.getDoorLockNum()); //车门加锁次数
				stSaveDriverDetailInfo.setLong(48, ddb.getDoorLockTime()); //车门加锁时长
				
				stSaveDriverDetailInfo.setLong(49, ddb.getOverspeedAlarm()); //超速次数
				stSaveDriverDetailInfo.setLong(50, ddb.getOverspeedTime()); //超速时长
				
				stSaveDriverDetailInfo.setLong(51, ddb.getFatigueAlarm()); //疲劳驾驶次数
				stSaveDriverDetailInfo.setLong(52, ddb.getFatigueTime()); //疲劳驾驶时长
				
				stSaveDriverDetailInfo.setLong(53, ddb.getDriverTimeoutTime()); //驾驶超时时长
				stSaveDriverDetailInfo.setLong(54, ddb.getStopTimoutNum()); //超时停车次数
				stSaveDriverDetailInfo.setLong(55, ddb.getStopTimoutTime()); //超时停车时长
				
				stSaveDriverDetailInfo.setLong(56, ddb.getInareaAlarm()); //进区告警次数
				stSaveDriverDetailInfo.setLong(57, ddb.getOutareaAlarm()); //出区告警次数
				stSaveDriverDetailInfo.setLong(58, ddb.getInRouteNum()); //进区告警次数
				stSaveDriverDetailInfo.setLong(59, ddb.getOutRouteNum()); //出区告警次数
				stSaveDriverDetailInfo.setLong(60, ddb.getRouteRunDiffNum()); //出区告警次数
				
				stSaveDriverDetailInfo.setLong(61, ddb.getDeviateRouteAlarm()); //
				stSaveDriverDetailInfo.setLong(62, ddb.getDeviateRouteTime()); //
				stSaveDriverDetailInfo.setLong(63, ddb.getIllegalFireNum()); //
				stSaveDriverDetailInfo.setLong(64, ddb.getIllegalMoveNum()); //
				stSaveDriverDetailInfo.setLong(65, ddb.getCashAlarmNum()); //
				
				stSaveDriverDetailInfo.setLong(66, ddb.getCashAlarmTime()); //
				stSaveDriverDetailInfo.setLong(67, ddb.getOverrpmAlarm()); //
				stSaveDriverDetailInfo.setLong(68, ddb.getOverrpmTime()); //
				stSaveDriverDetailInfo.setLong(69, ddb.getGearWrongNum()); //
				stSaveDriverDetailInfo.setLong(70, ddb.getGearWrongTime()); //
				
				stSaveDriverDetailInfo.setLong(71, ddb.getGearGlideNum()); //
				stSaveDriverDetailInfo.setLong(72, ddb.getGearGlideTime()); //
				stSaveDriverDetailInfo.setLong(73, ddb.getUrgentSpeedNum()); //
				stSaveDriverDetailInfo.setLong(74, ddb.getUrgentSpeedTime()); //
				stSaveDriverDetailInfo.setLong(75, ddb.getUrgentLowdownNum()); //
				
				stSaveDriverDetailInfo.setLong(76, ddb.getUrgentLowdownTime()); //
				stSaveDriverDetailInfo.setLong(77, ddb.getLongIdleNum()); //超长怠速次数
				stSaveDriverDetailInfo.setLong(78, ddb.getLongIdleTime()); //超长怠速时长
				stSaveDriverDetailInfo.setLong(79, ddb.getIdlingAirNum()); //怠速空调次数
				stSaveDriverDetailInfo.setLong(80, ddb.getIdlingAirTime()); //怠速空调时长
				
				stSaveDriverDetailInfo.setLong(81, ddb.getEconomicRunTime()); //超经济区运行时长
				stSaveDriverDetailInfo.setLong(82, ddb.getAreaOverspeedAlarm()); //区域内超速次数
				stSaveDriverDetailInfo.setLong(83, ddb.getAreaOverspeedTime()); //区域内超速时长
				stSaveDriverDetailInfo.setLong(84, ddb.getHeatUpNum()); //
				
				stSaveDriverDetailInfo.setLong(85, ddb.getHeatUpTime()); //
				stSaveDriverDetailInfo.setLong(86, ddb.getAreaOpendoorNum()); //
				stSaveDriverDetailInfo.setLong(87, ddb.getAreaOpendoorTime()); //
				stSaveDriverDetailInfo.setLong(88, ddb.getOverloadNum()); //
				stSaveDriverDetailInfo.setLong(89, ddb.getIllegalStopNum()); //
				
				stSaveDriverDetailInfo.setLong(90, ddb.getIllegalStopTime()); //
				stSaveDriverDetailInfo.setLong(91, ddb.getDoor1OpenNum()); //
				stSaveDriverDetailInfo.setLong(92, ddb.getDoor2OpenNum()); //
				stSaveDriverDetailInfo.setLong(93, ddb.getDoor3OpenNum()); //
				stSaveDriverDetailInfo.setLong(94, ddb.getDoor4OpenNum()); //
				
				stSaveDriverDetailInfo.setLong(95, ddb.getGearImproper()); //
				stSaveDriverDetailInfo.setLong(96, ddb.getGearTime()); //
				stSaveDriverDetailInfo.setLong(97, ddb.getRouteRunNum()); //
				stSaveDriverDetailInfo.setLong(98, ddb.getDoorOpenNum()); //
				stSaveDriverDetailInfo.setLong(99, ddb.getOvermanNum()); //
				
				stSaveDriverDetailInfo.setLong(100, ddb.getRetarderWorkNum()); //
				stSaveDriverDetailInfo.setLong(101, ddb.getRetarderWorkTime()); //
				stSaveDriverDetailInfo.setLong(102, ddb.getBrakeNum()); //
				stSaveDriverDetailInfo.setLong(103, ddb.getBrakeTime()); //
				stSaveDriverDetailInfo.setLong(104, ddb.getReverseGearNum()); //
				
				stSaveDriverDetailInfo.setLong(105, ddb.getReverseGearTime()); //
				stSaveDriverDetailInfo.setLong(106, ddb.getLowerBeamNum()); //
				stSaveDriverDetailInfo.setLong(107, ddb.getLowerBeamTime()); //
				stSaveDriverDetailInfo.setLong(108, ddb.getHighBeamNum()); //
				stSaveDriverDetailInfo.setLong(109, ddb.getHighBeamTime()); //
				
				stSaveDriverDetailInfo.setLong(110, ddb.getLeftTurningSignalNum()); //
				stSaveDriverDetailInfo.setLong(111, ddb.getLeftTurningSignalTime()); //
				stSaveDriverDetailInfo.setLong(112, ddb.getRightTurningSignalNum()); //
				stSaveDriverDetailInfo.setLong(113, ddb.getRightTurningSignalTime()); //
				stSaveDriverDetailInfo.setLong(114, ddb.getOutlineLampNum()); //
				
				stSaveDriverDetailInfo.setLong(115, ddb.getOutlineLampTime()); //
				stSaveDriverDetailInfo.setLong(116, ddb.getTrumpetNum()); //
				stSaveDriverDetailInfo.setLong(117, ddb.getTrumpetTime()); //
				stSaveDriverDetailInfo.setLong(118, ddb.getFreePositionNum()); //
				stSaveDriverDetailInfo.setLong(119, ddb.getFreePositionTime()); //
				
				stSaveDriverDetailInfo.setLong(120, ddb.getAbsWorkNum()); //
				stSaveDriverDetailInfo.setLong(121, ddb.getAbsWorkTime()); //
				stSaveDriverDetailInfo.setLong(122, ddb.getClutchNum()); //
				stSaveDriverDetailInfo.setLong(123, ddb.getClutchTime()); //
				stSaveDriverDetailInfo.setLong(124, ddb.getFoglightNum()); //
				
				stSaveDriverDetailInfo.setLong(125, ddb.getFoglightTime()); //
				stSaveDriverDetailInfo.setLong(126, ddb.getAirconditionNum()); //
				stSaveDriverDetailInfo.setLong(127, ddb.getAirconditionTime()); //
				
				stSaveDriverDetailInfo.setLong(128, ddb.getHeadCollideNum()); // 前向碰撞
				stSaveDriverDetailInfo.setLong(129, ddb.getVehicleDeviateNum()); //车道偏离
				
				stSaveDriverDetailInfo.addBatch();
				
				count++;
				if (count>=50){
					stSaveDriverDetailInfo.executeBatch();
					count = 0;
				}
			}
			if (count>0){
				stSaveDriverDetailInfo.executeBatch();
				count = 0;
			}
		}catch(SQLException e){
			logger.error("存储驾驶员驾驶明细信息出错：VID="+vid+" ",e);
		}finally{
			if(stSaveDriverDetailInfo != null){
				stSaveDriverDetailInfo.close();
			}
		}
	}

}
