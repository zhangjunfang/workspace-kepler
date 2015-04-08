package com.ctfo.analy.dao;

import java.sql.Clob;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;
import oracle.jdbc.driver.OracleResultSet;

import org.apache.log4j.Logger;

import com.ctfo.analy.TempMemory;
import com.ctfo.analy.beans.AlarmBaseBean;
import com.ctfo.analy.beans.AlarmNotice;
import com.ctfo.analy.beans.AlarmTacticsSetBean;
import com.ctfo.analy.beans.AreaAlarmBean;
import com.ctfo.analy.beans.FatigueAlarmCfgBean;
import com.ctfo.analy.beans.IllegalOptionsAlarmBean;
import com.ctfo.analy.beans.LineAlarmBean;
import com.ctfo.analy.beans.OrgAlarmConfBean;
import com.ctfo.analy.beans.OrgParentInfo;
import com.ctfo.analy.beans.OverspeedAlarmCfgBean;
import com.ctfo.analy.beans.RoadAlarmBean;
import com.ctfo.analy.beans.TbLineStationBean;
import com.ctfo.analy.beans.VehicleInfo;
import com.ctfo.analy.connpool.OracleConnectionPool;
import com.ctfo.analy.util.CDate;
import com.ctfo.analy.util.ExceptionUtil;
import com.lingtu.xmlconf.XmlConf;

/**
 * 数据库访问类
 */
public class MonitorDBAdapter {

	private static final Logger logger = Logger.getLogger(MonitorDBAdapter.class);

	String queryVehicleInfoSQL;
	String queryAlarmVehicleSQL;
	String queryAreaAlarmSQL;
	String queryLineAlarmSQL;
	String queryRoadAlarmSQL;
	String queryIllegalOptionsSQL;
	String queryOrgAlarmConfSQL;
	String queryOrgAlarmNoticeSQL;
	String queryLineSectionWithAlarmSQL;
	String queryOverspeedCfgSQL;
	String queryFatigueCfgSQL;
	String queryTrVehicleStationSQL;
	String queryOrgParentInfoSQL;
	
	/**
	 * 初始化缓存
	 */
	public void initDBAdapter(XmlConf config) throws Exception {
		
		// 全部注册车辆信息
		queryVehicleInfoSQL = config.getStringValue("database|sqlstatement|sql_queryVehicleInfo");

		// 手机号对应报警设置Map
		queryAlarmVehicleSQL = config.getStringValue("database|sqlstatement|sql_queryAlarmVehicle");

		// VID对应围栏报警设置Map
		queryAreaAlarmSQL = config.getStringValue("database|sqlstatement|sql_queryAreaAlarm");

		// VID对应线路报警设置Map
		queryLineAlarmSQL = config.getStringValue("database|sqlstatement|sql_queryLineAlarm");
		
		// 设置过告警参数的线路段
		queryLineSectionWithAlarmSQL = config.getStringValue("database|sqlstatement|sql_queryLineAlarmNode");
		
		// VID对应道路等级报警设置Map
		queryRoadAlarmSQL = config.getStringValue("database|sqlstatement|sql_queryRoadAlarm");
		
		// VID对应非法运营软报警设置Map
		queryIllegalOptionsSQL = config.getStringValue("database|sqlstatement|sql_queryIllegealOperationsAlarm");

		//企业告警等级设置Map
		queryOrgAlarmConfSQL = config.getStringValue("database|sqlstatement|sql_queryOrgAlarmConf");

		//查询企业车辆告警下发消息内容
		queryOrgAlarmNoticeSQL = config.getStringValue("database|sqlstatement|sql_queryAlarmNotice");
		
		// VID对应非法运营软报警设置Map
		queryOverspeedCfgSQL = config.getStringValue("database|sqlstatement|sql_queryOverspeedAlarmCfg");

		// VID对应非法运营软报警设置Map
		queryFatigueCfgSQL = config.getStringValue("database|sqlstatement|sql_queryFatigueAlarmCfg");
		
		// VID对应站点设置Map
		queryTrVehicleStationSQL = config.getStringValue("database|sqlstatement|sql_queryTrVehicleStationInfo");
		
		//企业对应所有父企业ID信息
		queryOrgParentInfoSQL = config.getStringValue("database|sqlstatement|sql_orgParentInfo");
		// 初始化同步所有车辆信息
		queryAlarmVehicle();
	}

	/**
	 * 初始化手机号对应报警设置Map
	 * 
	 * @param time
	 */
	private void queryAlarmVehicle() {
		OraclePreparedStatement stQueryAlarmVehicle = null;
		OracleResultSet rs = null;
		String commaddr;
		String vid;
		//String alarmtype;
		String vehicleno;
		String corpId;
		String corpName;
		String teamId;
		String teamName;
		String innerCode;
		String vinCode;
		String plateColor;
		// 从连接池获得连接
		OracleConnection conn = null;

		try {
			long t = System.currentTimeMillis();
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryAlarmVehicle = (OraclePreparedStatement) conn.prepareStatement(queryAlarmVehicleSQL);
			rs = (OracleResultSet) stQueryAlarmVehicle.executeQuery();
			while (rs.next()) {

				commaddr = rs.getString("commaddr");// 手机号码
				vid = rs.getString("vid");// 车辆标识号
				//alarmtype = rs.getString("alarmtype");// 报警类型
				vehicleno = rs.getString("vehicle_no");// 车牌号
				corpId =rs.getString("PENT_ID");
				corpName=rs.getString("PENT_NAME");
				teamId=rs.getString("ENT_ID");
				teamName=rs.getString("ENT_NAME");
				innerCode=rs.getString("INNER_CODE");
				vinCode=rs.getString("VIN_CODE");
				plateColor = rs.getString("PLATE_COLOR");
				

				AlarmBaseBean alarmVehicleBean = new AlarmBaseBean();
				alarmVehicleBean.setCommaddr(commaddr);
				alarmVehicleBean.setVid(vid);
				//alarmVehicleBean.setAlarmtype(alarmtype);
				alarmVehicleBean.setVehicleno(vehicleno);
				alarmVehicleBean.setCorpId(corpId);
				alarmVehicleBean.setCorpName(corpName);
				alarmVehicleBean.setTeamId(teamId);
				alarmVehicleBean.setTeamName(teamName);
				alarmVehicleBean.setVinCode(vinCode);
				alarmVehicleBean.setInnerCode(innerCode);
				alarmVehicleBean.setPlateColor(plateColor);
				alarmVehicleBean.setUpdatetime(t);

				TempMemory.setAlarmVehicleMap(commaddr, alarmVehicleBean);
			}// End while

//			List<String> list = new ArrayList<String>();
//			Map<String, AlarmBaseBean> map = TempMemory.getAlarmVehicleMapAll();
//			for (Map.Entry<String, AlarmBaseBean> m : map.entrySet()) {
//				if (m.getValue().getUpdatetime() != t) {
//					list.add(m.getKey());
//				}
//			}
//			for (String s : list) {
//				TempMemory.deleteAlarmVehicleMap(s);
//				logger.debug("删除绑定平台判断报警车辆" + s);
//			}

//			logger.info("查询到绑定平台判断报警车辆总数["+ TempMemory.getAlarmVehicleMapSize() + "]");
		} catch (SQLException e) {
			logger.error("查询到绑定平台判断报警车辆总数" + e.getMessage(),e);
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryAlarmVehicle != null) {
					stQueryAlarmVehicle.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询到绑定平台判断报警车辆总数关闭连接-" + e.getMessage(),e);
				e.printStackTrace();
			}
		}
	}

	/*****
	 * 同步增量车辆
	 */
	public void synNewVehicle(long datetime){
		OraclePreparedStatement stQueryAlarmVehicle = null;
		OracleResultSet rs = null;
		String commaddr;
		String vid;
		String vehicleno;
		String corpId;
		String corpName;
		String teamId;
		String teamName;
		String innerCode;
		String vinCode;
		String plateColor;
		// 从连接池获得连接
		OracleConnection conn = null;
		try{
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryAlarmVehicle = (OraclePreparedStatement) conn.prepareStatement(SQLPool.getinstance().getSql("sql_queryNewVehicle"));
			stQueryAlarmVehicle.setLong(1, datetime);
			stQueryAlarmVehicle.setLong(2, datetime);
			stQueryAlarmVehicle.setLong(3, datetime);
			stQueryAlarmVehicle.setLong(4, datetime);
			stQueryAlarmVehicle.setLong(5, datetime);
			stQueryAlarmVehicle.setLong(6, datetime);
			rs = (OracleResultSet) stQueryAlarmVehicle.executeQuery();
			while (rs.next()) {
				commaddr = rs.getString("COMMADDR");// 手机号码
				vid = rs.getString("VID");// 车辆标识号
				//alarmtype = rs.getString("alarmtype");// 报警类型
				vehicleno = rs.getString("VEHICLENO");// 车牌号
				corpId =rs.getString("PENT_ID");
				corpName=rs.getString("PENT_NAME");
				teamId=rs.getString("ENT_ID");
				teamName=rs.getString("ENT_NAME");
				innerCode=rs.getString("INNER_CODE");
				vinCode=rs.getString("VIN_CODE");
				plateColor = rs.getString("PLATE_COLOR");
				
				AlarmBaseBean alarmVehicleBean = new AlarmBaseBean();
				alarmVehicleBean.setCommaddr(commaddr);
				alarmVehicleBean.setVid(vid);
				//alarmVehicleBean.setAlarmtype(alarmtype);
				alarmVehicleBean.setVehicleno(vehicleno);
				alarmVehicleBean.setCorpId(corpId);
				alarmVehicleBean.setCorpName(corpName);
				alarmVehicleBean.setTeamId(teamId);
				alarmVehicleBean.setTeamName(teamName);
				alarmVehicleBean.setVinCode(vinCode);
				alarmVehicleBean.setInnerCode(innerCode);
				alarmVehicleBean.setPlateColor(plateColor);
				TempMemory.setAlarmVehicleMap(commaddr, alarmVehicleBean);
				logger.info("Syn of adding vehicle :" + commaddr);
			}// End while
		}catch(Exception e){
			logger.error("同步增量车辆",e);
		}finally{
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryAlarmVehicle != null) {
					stQueryAlarmVehicle.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException ex) {
				logger.error("同步增量车辆关闭连接-",ex);
			}
		}
	}
	
	/*****
	 * 删除车辆、注销车辆
	 */
	public void synDelVehicle(long datetime){
		OraclePreparedStatement stQueryAlarmVehicle = null;
		OracleResultSet rs = null;
		String commaddr;
		// 从连接池获得连接
		OracleConnection conn = null;
		try{
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryAlarmVehicle = (OraclePreparedStatement) conn.prepareStatement(SQLPool.getinstance().getSql("sql_queryDelVehicle"));
			stQueryAlarmVehicle.setLong(1, datetime);
			stQueryAlarmVehicle.setLong(2, datetime);
			rs = (OracleResultSet) stQueryAlarmVehicle.executeQuery();
			while (rs.next()) {
				commaddr = rs.getString("COMMADDR");// 手机号码
				TempMemory.deleteAlarmVehicleMap(commaddr);
				logger.info("Syn of deleting vehicle :" + commaddr);
			}// End while
		}catch(Exception e){
			logger.error("同步删除车辆、注销车辆",e);
		}finally{
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryAlarmVehicle != null) {
					stQueryAlarmVehicle.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException ex) {
				logger.error("同步删除车辆、注销车辆关闭连接-",ex);
			}
		}
	}
	/**
	 * 初始化VID对应区域报警设置Map
	 * 
	 * @param time
	 */
	public void queryAreaAlarm() {
		int areaAlarmMapNo = TempMemory.getAreaAlarmMapNo();//当期可以使用的缓存编号
		int areaAlarmMapNoNew = areaAlarmMapNo==1?2:1;
		TempMemory.clearAreaAlarmMap();//清空线路缓存，重新同步载入。
		OraclePreparedStatement stQueryAreaAlarm = null;
		OracleResultSet rs = null;
		// 从连接池获得连接
		OracleConnection conn = null;
		
		String outAreaAlarmType = "";
		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryAreaAlarm = (OraclePreparedStatement) conn.prepareStatement(queryAreaAlarmSQL);
			rs = (OracleResultSet) stQueryAreaAlarm.executeQuery();
			long currentday = CDate.getCurrentDayYearMonthDay();
			while (rs.next()) {
				try {
					String vid = rs.getString("vid");
					AreaAlarmBean areaAlarmBean = new AreaAlarmBean();
					Clob clob = rs.getClob("lonlat");
					String lonlat ="";                
					if(clob!=null){         
						lonlat = clob.getSubString((long)1, (int)clob.length());
					}
					String[] ll = lonlat.split("#");
					
					List<String> points = new ArrayList<String>();
					for (int i = 0; i < ll.length; i++) {
						points.add(ll[i]);
					}
					areaAlarmBean.setAreaid(rs.getString("areaId"));//围栏ID
					areaAlarmBean.setAreaName(rs.getString("areaName"));//围栏名称
					areaAlarmBean.setVid(vid);
					areaAlarmBean.setBeginTime(rs.getLong("areabegintime"));
					areaAlarmBean.setEndTime(rs.getLong("areaendtime"));
					
					areaAlarmBean.setAreamaxspeed(rs.getLong("areaMaxSpeed"));
					areaAlarmBean.setSuperspeedtimes(rs.getLong("superSpeedTimes"));
					areaAlarmBean.setArealowspeed(rs.getLong("areaLowSpeed"));
					areaAlarmBean.setLowspeedtimes(rs.getLong("lowSpeedTimes"));

					areaAlarmBean.setAreasharp(rs.getString("areashape"));
					areaAlarmBean.setLonlats(points);
					areaAlarmBean.setUsetype(rs.getString("usetype").split(","));
					if (rs.getString("message_value")!=null){
						areaAlarmBean.setMessageValue(rs.getString("message_value").split("#"));
					}
					if (rs.getString("vehicle_door_type")!=null){
						String[] type = rs.getString("vehicle_door_type").split(",");						
						areaAlarmBean.setVehicleDoorType(type);
					}

					//key：车vid,value:该车所在的各个围栏集合
					if(TempMemory.getAreaAlarmMap(vid,areaAlarmMapNoNew)==null||TempMemory.getAreaAlarmMap(vid,areaAlarmMapNoNew).size()<=0){
						List<AreaAlarmBean> list = new ArrayList<AreaAlarmBean>();
						list.add(areaAlarmBean);
						TempMemory.setAreaAlarmMap(vid, list,areaAlarmMapNoNew);
					}else{
						TempMemory.getAreaAlarmMap(vid,areaAlarmMapNoNew).add(areaAlarmBean);
					}
					
				} catch (Exception e) {
					logger.error("同步绑定区域报警车辆错误" + e.getMessage());
					e.printStackTrace();
				}

			}// End while
			
			TempMemory.setAreaAlarmMapNo(areaAlarmMapNoNew);//切换配置
			TempMemory.setAreaTree();

		} catch (SQLException e) {
			logger.error("查询到绑定区域报警车辆错误" + e.getMessage(),e);
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryAreaAlarm != null) {
					stQueryAreaAlarm.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询到绑定平台判断区域报警车辆总数关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}

	/**
	 * 初始化VID对应线路报警设置Map
	 * 
	 * @param time
	 */
	public void queryLineAlarm() {
		int lineAlarmMapNo = TempMemory.getLineAlarmMapNo();//当期所使用的编号
		int lineAlarmMapNoNew = lineAlarmMapNo == 1?2:1;
		TempMemory.clearLineAlarmMap();//清空线路缓存，重新同步载入。
		OraclePreparedStatement stQueryLineAlarm = null;
		OracleResultSet rs = null;
		// 从连接池获得连接
		OracleConnection conn = null;
		long currentday = CDate.getCurrentDayYearMonthDay();
		try {
			//清除告警路段缓存
			TempMemory.clearLineSectionWithAlarmMap();
			
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryLineAlarm = (OraclePreparedStatement) conn.prepareStatement(queryLineAlarmSQL);
			rs = (OracleResultSet) stQueryLineAlarm.executeQuery();
			while (rs.next()) {
				try {
					String vid = rs.getString("vid");
					LineAlarmBean lineAlarmBean = new LineAlarmBean();
					String lineId = rs.getString("lineid");
					lineAlarmBean.setLineid(lineId);
//					Clob clob = rs.getClob("lonlat");
//					String lonlat ="";                
//					if(clob!=null){                    
//						lonlat = clob.getSubString((long)1, (int)clob.length());
//					}
//					
//					String[] ll = lonlat.split(",");
					long startStationId = rs.getLong("START_STATION_ID");
					long endStationId = rs.getLong("END_STATION_ID");
					String lonlat = queryLineSectionWithAlarm(lineId,startStationId,endStationId);
					if (lonlat==null||lonlat.trim().length()==0){
						continue;
					}
					String[] ll = lonlat.split(",");
					List<String> points = new ArrayList<String>();
					for (int i = 0; i < ll.length; i += 2) {
						points.add(ll[i] + "," + ll[i + 1]);
					}
					lineAlarmBean.setPid(rs.getString("pid"));
					lineAlarmBean.setSpeedthreshold(rs.getLong("speedthreshold"));//最大速度
					lineAlarmBean.setSpeedtimethreshold(rs.getLong("speedtimethreshold"));//最大速度持续时间

					lineAlarmBean.setLineName(rs.getString("linename"));//线路名称
					
					lineAlarmBean.setVid(vid);
					lineAlarmBean.setRoadwight(rs.getLong("roadwight"));
					lineAlarmBean.setBeginTime(currentday+ timeConvertSec(rs.getString("periodbegintime"))* 1000);
					lineAlarmBean.setEndTime(currentday+ timeConvertSec(rs.getString("periodendtime"))* 1000);

					lineAlarmBean.setLonlats(points);
					String usetype = rs.getString("usetype");
					if (usetype!=null&&!"".equals(usetype)){
						lineAlarmBean.setUsetype(usetype.split(","));
					}else{
						continue;
					}
					

					//key：车vid,value:该车所在的各个线路集合
					if(TempMemory.getLineAlarmMap(vid,lineAlarmMapNoNew)==null||TempMemory.getLineAlarmMap(vid,lineAlarmMapNoNew).size()<=0){
						List<LineAlarmBean> list = new ArrayList<LineAlarmBean>();
						list.add(lineAlarmBean);
						TempMemory.setLineAlarmMap(vid, list,lineAlarmMapNoNew);
					}else{
						TempMemory.getLineAlarmMap(vid,lineAlarmMapNoNew).add(lineAlarmBean);
					}

				} catch (Exception e) {
					logger.error("同步绑定线路报警车辆错误" + ExceptionUtil.getErrorStack(e, 0));
					e.printStackTrace();
				}
			}
			
			TempMemory.setLineAlarmMapNo(lineAlarmMapNoNew);//切换配置
			TempMemory.setLineTree();
//			logger.info("查询到绑定线路报警线段总数[" + TempMemory.getLineAlarmMapSize()+ "]");
		} catch (SQLException e) {
			logger.error("查询到绑定线路报警线段总数" + e.getMessage(),e);
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryLineAlarm != null) {
					stQueryLineAlarm.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询到绑定平台判断线路报警车辆总数关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}

	/**
	 * 初始化VID对应道路等级报警设置Map
	 * 
	 * @param time
	 */
	public void queryRoadAlarm () {
		int roadAlarmMapNo = TempMemory.getRoadAlarmMapNo();//获取当前配置所使用的编号
		int roadAlarmMapNoNew = roadAlarmMapNo ==1?2:1; 
		TempMemory.clearRoadAlarmMap();//清除就缓存，准备载入新配置
		OraclePreparedStatement stQueryRoadAlarm= null;
		OracleResultSet rs = null;
 
		// 从连接池获得连接
		OracleConnection conn = null;

		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryRoadAlarm = (OraclePreparedStatement) conn.prepareStatement(queryRoadAlarmSQL);
			rs = (OracleResultSet) stQueryRoadAlarm.executeQuery();
			while (rs.next()) {
				String vid=rs.getString("vid");
				RoadAlarmBean roadAlarmBean = new RoadAlarmBean();
				roadAlarmBean.setVid(vid);//车辆ID
				roadAlarmBean.setConfig_id(rs.getString("config_id"));//config_id
				roadAlarmBean.setConfig_name(rs.getString("config_name"));//配置名称
				roadAlarmBean.setEnt_id(rs.getString("ent_id"));//所属企业
				roadAlarmBean.setCreate_by(rs.getString("create_by"));//创建人
				roadAlarmBean.setCreate_time(rs.getLong("create_time"));//创建时间
				roadAlarmBean.setUpdate_by(rs.getString("update_by"));//修改人
				roadAlarmBean.setUpdate_time(rs.getLong("update_time"));//修改时间
				roadAlarmBean.setEnable_flag(rs.getString("enable_flag"));//有效标记（1、有效 0、无效）
				roadAlarmBean.setIs_default(rs.getString("is_default"));//是否默认配置（1、是）
				roadAlarmBean.setEw_speed_limit(rs.getLong("ew_speed_limit"));//高速速度限制(Km/h)
				roadAlarmBean.setEw_continue_limit(rs.getLong("ew_continue_limit"));//高速超速持续时间(s)
				roadAlarmBean.setNr_speed_limit(rs.getLong("nr_speed_limit"));//国道速度限制(Km/h)
				roadAlarmBean.setNr_continue_limit(rs.getLong("nr_continue_limit"));//国道超速持续时间(s)
				roadAlarmBean.setPr_speed_limit(rs.getLong("pr_speed_limit"));//省道速度限制(Km/h)
				roadAlarmBean.setPr_continue_limit(rs.getLong("pr_continue_limit"));//省道超速持续时间(s)
				roadAlarmBean.setCr_speed_limit(rs.getLong("cr_speed_limit"));//县道速度限制(Km/h)
				roadAlarmBean.setCr_continue_limit(rs.getLong("cr_continue_limit"));//县道超速持续时间(s)
				roadAlarmBean.setOr_speed_limit(rs.getLong("or_speed_limit"));//其他道路速度限制(Km/h)
				roadAlarmBean.setOr_continue_limit(rs.getLong("or_continue_limit"));//其他道路超速持续时间(s)
				
				//key：车vid,value:该车所在的各个道路等级配置
				if(TempMemory.getRoadAlarmMap(vid,roadAlarmMapNoNew)==null||TempMemory.getRoadAlarmMap(vid,roadAlarmMapNoNew).size()<=0){
					List<RoadAlarmBean> list = new ArrayList<RoadAlarmBean>();
					list.add(roadAlarmBean);
					TempMemory.setRoadAlarmMap(vid, list,roadAlarmMapNoNew);
				}else{
					TempMemory.getRoadAlarmMap(vid,roadAlarmMapNoNew).add(roadAlarmBean);
				}
			}// End while
			TempMemory.setRoadAlarmMapNo(roadAlarmMapNoNew);//切换配置
		} catch (SQLException e) {
			logger.error("查询到绑定平台道路等级报警车辆总数-ERROR-数据库异常"+e.getMessage(),e);
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}
				if (stQueryRoadAlarm != null) {
					stQueryRoadAlarm.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询到绑定平台判断道路等级报警车辆关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}
	
	/**
	 * 初始化VID对应非法运营软报警设置Map
	 * 
	 * @param time
	 */
	public void queryIllegalOptionsAlarm () {
		int illeOptAlarmMapNo = TempMemory.getIlleOptAlarmMapNo();//获取当前配置所使用的编号
		int illeOptAlarmMapNoNew = illeOptAlarmMapNo==1?2:1;
		TempMemory.clearIlleOptAlarmMap();//清除就缓存，准备载入新配置
		OraclePreparedStatement stQueryIllegalOptionsAlarm= null;
		OracleResultSet rs = null;
 
		// 从连接池获得连接
		OracleConnection conn = null;

		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryIllegalOptionsAlarm = (OraclePreparedStatement) conn.prepareStatement(queryIllegalOptionsSQL);
			rs = (OracleResultSet) stQueryIllegalOptionsAlarm.executeQuery();
			while (rs.next()) {
				String vid=rs.getString("VID");
				String entId = rs.getString("ENT_ID");
				String startTime = rs.getString("START_TIME");
				String endTime = rs.getString("END_TIME");
				Long deferred = rs.getLong("DEFERRED");
				String isDefault = rs.getString("ISDEFAULT");
				
				AlarmTacticsSetBean atsBean = new AlarmTacticsSetBean();
				atsBean.setStartTime(startTime);
				atsBean.setEndTime(endTime);
				atsBean.setDeferred(deferred);
				
				IllegalOptionsAlarmBean illegalOptAlarmBean = new IllegalOptionsAlarmBean();
				illegalOptAlarmBean.setVid(vid);//车辆ID
				illegalOptAlarmBean.setEntId(entId);//所属企业
				illegalOptAlarmBean.setStartTime(startTime);//开始时间
				illegalOptAlarmBean.setEndTime(endTime);//结束时间
				illegalOptAlarmBean.setDeferred(deferred);//持续时间
				illegalOptAlarmBean.setIsDefault(isDefault);//是否默认配置（1、是）
				
				//key：车vid,value:该车对应的非法运营软报警配置
				IllegalOptionsAlarmBean oldBean = TempMemory.getIlleOptAlarmMap(vid, illeOptAlarmMapNoNew);
				if (oldBean==null){
					TempMemory.setIlleOptAlarmMap(vid, illegalOptAlarmBean,illeOptAlarmMapNoNew);
					TempMemory.getIlleOptAlarmMap(vid, illeOptAlarmMapNoNew).getSetList().add(atsBean);
				}else{
					//判断起始时间和结束时间，如果在时间之内就更新
					String currTime = CDate.getTimeShort();
					if (!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(atsBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(atsBean.getEndTime())))){
						TempMemory.getIlleOptAlarmMap(vid, illeOptAlarmMapNoNew).setStartTime(startTime);
						TempMemory.getIlleOptAlarmMap(vid, illeOptAlarmMapNoNew).setEndTime(endTime);
						TempMemory.getIlleOptAlarmMap(vid, illeOptAlarmMapNoNew).setDeferred(deferred);
						TempMemory.getIlleOptAlarmMap(vid, illeOptAlarmMapNoNew).setIsDefault(isDefault);
					}
					
					TempMemory.getIlleOptAlarmMap(vid, illeOptAlarmMapNoNew).getSetList().add(atsBean);
				}

			}// End while
			TempMemory.setIlleOptAlarmMapNo(illeOptAlarmMapNoNew);//切换配置
		} catch (SQLException e) {
			logger.error("查询到非法运营软报警配置车辆总数-ERROR-数据库异常"+e.getMessage(),e);
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}
				if (stQueryIllegalOptionsAlarm != null) {
					stQueryIllegalOptionsAlarm.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询到非法运营软报警配置车辆出错，关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}
	
	/**
	 * 定时更新非法营运软报警时间设置
	 * 缓存刷新
	 * @param time
	 */
	public void updateIllegalOptionsAlarm() {
		int illeOptAlarmMapNo = TempMemory.getIlleOptAlarmMapNo();//获取当前配置所使用的编号
		//int illeOptAlarmMapNoNew = illeOptAlarmMapNo==1?2:1;
		//TempMemory.clearIlleOptAlarmMap();//清除就缓存，准备载入新配置

		try {
			Map<String, IllegalOptionsAlarmBean> map = TempMemory.getIlleOptAlarmMap(illeOptAlarmMapNo);
			Set tmpSet = map.keySet();
			Iterator itr = tmpSet.iterator();
			while(itr.hasNext()){
				IllegalOptionsAlarmBean entry=(IllegalOptionsAlarmBean) map.get((String)itr.next());  
				//根据当前时间判断，复制当前所处时段的非法营运设置
				String currTime = CDate.getTimeShort();
				if (entry==null) continue;
				List<AlarmTacticsSetBean> ls = entry.getSetList();
				for (int i=0;i<ls.size();i++){
					AlarmTacticsSetBean tmpBean = ls.get(i);
					if (tmpBean!=null&&!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(tmpBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(tmpBean.getEndTime())))){
						entry.setStartTime(tmpBean.getStartTime());
						entry.setEndTime(tmpBean.getEndTime());
						entry.setDeferred(tmpBean.getDeferred());
					}
				}
			}
		} catch (Exception e) {
			logger.error("定时更新非法运营软报警配置车辆总数-ERROR："+e.getMessage(),e);
			e.printStackTrace();
		} 
	}
	

	
	/***
	 * 将HH:MM:SS 转成秒格式
	 * 
	 * @param time
	 * @return
	 */
	private static long timeConvertSec(String time) {
		if("".equals(time)||null==time){
			return 0;
		}
		String[] arrays = time.split(":");
		if (arrays.length == 3) {
			return Integer.parseInt(arrays[0]) * 60 * 60
					+ Integer.parseInt(arrays[1]) * 60
					+ Integer.parseInt(arrays[2]);
		} else {
			logger.error("设置时间格式不正确。" + time);
		}
		return 0;
	}

	/******
	 * 查询GPS巡检车辆信息
	 * @param orgId
	 * @return
	 */
	public static List<VehicleInfo> getGpsInsVehicleList(String orgId){
		OraclePreparedStatement stGpsInsVehicle= null;
		OracleResultSet rs = null;
		List<VehicleInfo> vList = new ArrayList<VehicleInfo>();
		// 从连接池获得连接
		OracleConnection conn = null;
		VehicleInfo  vInfo = null;
		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stGpsInsVehicle = (OraclePreparedStatement) conn.prepareStatement(SQLPool.getinstance().getSql("sql_gpsInsVehicle"));
			stGpsInsVehicle.setString(1, orgId);
			rs = (OracleResultSet) stGpsInsVehicle.executeQuery();
			
			while(rs.next()){
				vInfo = new VehicleInfo();
				vInfo.setVid(rs.getString("VID"));
				vInfo.setCommaddr(rs.getString("COMMADDR"));
				vInfo.setVehicleNo(rs.getString("VEHICLE_NO"));
				vList.add(vInfo);
			}// End while
			
		}catch(Exception e){
			logger.error("查询GPS巡检车辆出错.",e);
		}finally{
			try{
				if(null != rs){
					rs.close();
				}
				
				if(null != stGpsInsVehicle){
					stGpsInsVehicle.close();
				}
				
				if(null != conn){
					conn.close();
				}
			}catch(Exception e){
				logger.error(e);
			}
		}
		
		return vList;
	}
	
	/**
	 * 初始化企业告警等级设置Map
	 * 
	 * @param time
	 */
	public void queryOrgAlarmConf() {
		int orgAlarmConfMapNo = TempMemory.getOrgAlarmConfMapNo();//当期所使用的编号
		int orgAlarmConfMapNoNew = orgAlarmConfMapNo == 1?2:1;
		TempMemory.clearOrgAlarmConfMap();//清空线路缓存，重新同步载入。
		OraclePreparedStatement stQueryAlarmConf = null;
		OracleResultSet rs = null;
		// 从连接池获得连接
		OracleConnection conn = null;
		long currentday = CDate.getCurrentDayYearMonthDay();
		try {
			Map<String,String> map = new ConcurrentHashMap<String,String>();
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryAlarmConf = (OraclePreparedStatement) conn.prepareStatement(queryOrgAlarmConfSQL);
			rs = (OracleResultSet) stQueryAlarmConf.executeQuery();
			while (rs.next()) {
				try {
					
					String entId = rs.getString("ENT_ID");
					String alarmCode = rs.getString("ALARM_CODE");
					
					if (map.get(entId)!=null){
						alarmCode = map.get(entId)+","+alarmCode;
					}
					map.put(entId, alarmCode);

				} catch (Exception e) {
					logger.error("同步企业告警等级设置失败：" + e.getMessage(),e);
					e.printStackTrace();
				}
			}
			
			Set tmpSet = map.keySet();
			Iterator itr = tmpSet.iterator();
			while(itr.hasNext()){
				String id = (String)itr.next();
				String alarmCode =(String) map.get(id);
				
				OrgAlarmConfBean orgAlarmConfBean = new OrgAlarmConfBean();
				orgAlarmConfBean.setEntId(id);
				orgAlarmConfBean.setAlarmCode(alarmCode);
				
				TempMemory.setOrgAlarmConfMap(id, orgAlarmConfBean,orgAlarmConfMapNoNew);
			}
			
			TempMemory.setOrgAlarmConfMapNo(orgAlarmConfMapNoNew);//切换配置

		} catch (SQLException e) {
			logger.error("查询企业告警等级设置信息失败：" + e.getMessage(),e);
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}
				if (stQueryAlarmConf != null) {
					stQueryAlarmConf.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询企业告警等级设置信息过程中关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}
	
	/**
	 * 企业告警下发消息缓存
	 * 
	 */
	public void queryOrgAlarmNotice() {
		TempMemory.clearOrgAlarmNoticeMap();//清空缓存，重新载入。
		OraclePreparedStatement stQueryOrgAlarmNotice= null;
		OracleResultSet rs = null;
		// 从连接池获得连接
		OracleConnection conn = null;
 		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryOrgAlarmNotice = (OraclePreparedStatement) conn.prepareStatement(queryOrgAlarmNoticeSQL);
			rs = (OracleResultSet) stQueryOrgAlarmNotice.executeQuery();
			while (rs.next()) {//
				try {
					String entId = rs.getString("ENT_ID");
					String alarmCode = rs.getString("ALARM_CODE");
					
					AlarmNotice alarmNotice = new AlarmNotice();
					alarmNotice.setEntId(rs.getString("ENT_ID"));
					alarmNotice.setAlarmClass(rs.getString("ALARM_CLASS"));
					alarmNotice.setAlarmCode(rs.getString("ALARM_CODE"));
//					alarmNotice.setCommaddr(rs.getString("COMMADDR"));
					alarmNotice.setDisplayFlag(rs.getShort("DISPLAY_FLAG"));
 					alarmNotice.setMsg(rs.getString("MSG"));
					alarmNotice.setTtsFlag(rs.getShort("TTS_FLAG"));
					
					if (entId!=null){
						if(TempMemory.getOrgAlarmNoticeMap(entId)==null||TempMemory.getOrgAlarmNoticeMap(entId).size()<=0){
							ConcurrentHashMap<String,AlarmNotice> map = new ConcurrentHashMap<String,AlarmNotice>();
							map.put(alarmCode, alarmNotice);
							TempMemory.setOrgAlarmNoticeMap(entId, map);
						}else{
							TempMemory.getOrgAlarmNoticeMap(entId).put(alarmCode,alarmNotice);
						}
					}
				 } catch (Exception e) {
					logger.error("查询到企业告警自动下发消息内容缓存1:" + ExceptionUtil.getErrorStack(e, 0));
					e.printStackTrace();
				}
			}
 		} catch (Exception e) {
			logger.error("查询到企业告警自动下发消息内容缓存2:" + ExceptionUtil.getErrorStack(e, 0));
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryOrgAlarmNotice != null) {
					stQueryOrgAlarmNotice.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询到企业告警自动下发消息内容缓存查询关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}
	
	/**
	 * 初始化VID对应线路报警设置Map
	 * 
	 * @param time
	 */
	public String queryLineSectionWithAlarm(String lineId,long startStationId,long endStationId) {
		OraclePreparedStatement stQueryLineAlarm = null;
		OracleResultSet rs = null;
		// 从连接池获得连接
		OracleConnection conn = null;

		String key = lineId + "_" + startStationId + "_" + endStationId;
		try {
			if (!TempMemory.containsKeyOfLineSectionWithAlarmMap(key)){
				conn = (OracleConnection) OracleConnectionPool.getConnection();
				stQueryLineAlarm = (OraclePreparedStatement) conn.prepareStatement(queryLineSectionWithAlarmSQL);
				stQueryLineAlarm.setString(1, lineId);
				stQueryLineAlarm.setLong(2, startStationId);
				stQueryLineAlarm.setLong(3, endStationId);
				rs = (OracleResultSet) stQueryLineAlarm.executeQuery();
				while (rs.next()) {
					String lonlat = rs.getString("LONLAT");
					TempMemory.setLineSectionWithAlarmMap(key, lonlat);
				}
			}
			
			return TempMemory.getLineSectionWithAlarmMap(key);
			
		} catch (SQLException e) {
			logger.error("查询设置报警的路线段出错：[key="+key+"]" + e.getMessage(),e);
			return "";
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryLineAlarm != null) {
					stQueryLineAlarm.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询设置报警的路线段关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}
	
	
	/**
	 * 初始化VID对应超速报警设置Map
	 * 
	 * @param time
	 */
	public void queryOverspeedAlarmCfg () {
		int overspeedAlarmCfgMapNo = TempMemory.getOverspeedAlarmCfgMapNo();//获取当前配置所使用的编号
		int overspeedAlarmCfgMapNoNew = overspeedAlarmCfgMapNo==1?2:1;
		TempMemory.clearOverspeedAlarmCfgMap();//清除就缓存，准备载入新配置
		OraclePreparedStatement stQueryOverspeedAlarmCfg= null;
		OracleResultSet rs = null;
 
		// 从连接池获得连接
		OracleConnection conn = null;

		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryOverspeedAlarmCfg = (OraclePreparedStatement) conn.prepareStatement(queryOverspeedCfgSQL);
			rs = (OracleResultSet) stQueryOverspeedAlarmCfg.executeQuery();
			while (rs.next()) {
				String vid=rs.getString("VID");
				String entId = rs.getString("ENT_ID");
				String startTime = rs.getString("START_TIME");
				String endTime = rs.getString("END_TIME");
				Long speedScale = rs.getLong("SPEED_SCALE");
				
				AlarmTacticsSetBean atsBean = new AlarmTacticsSetBean();
				atsBean.setStartTime(startTime);
				atsBean.setEndTime(endTime);
				atsBean.setSpeedScale(speedScale);
				
				OverspeedAlarmCfgBean overspeedAlarmCfgBean = new OverspeedAlarmCfgBean();
				overspeedAlarmCfgBean.setVid(vid);//车辆ID
				overspeedAlarmCfgBean.setEntId(entId);//所属企业
				overspeedAlarmCfgBean.setStartTime(startTime);//开始时间
				overspeedAlarmCfgBean.setEndTime(endTime);//结束时间
				overspeedAlarmCfgBean.setSpeedScale(speedScale);//持续时间

				
				//key：车vid,value:该车对应的超速报警配置
				OverspeedAlarmCfgBean oldBean = TempMemory.getOverspeedAlarmCfgMap(vid, overspeedAlarmCfgMapNoNew);
				if (oldBean==null){
					TempMemory.setOverspeedAlarmCfgMap(vid, overspeedAlarmCfgBean,overspeedAlarmCfgMapNoNew);
					TempMemory.getOverspeedAlarmCfgMap(vid, overspeedAlarmCfgMapNoNew).getSetList().add(atsBean);
				}else{
					//判断起始时间和结束时间，如果在时间之内就更新
					String currTime = CDate.getTimeShort();
					//logger.info("currTime: "+currTime +" atsBean.getStartTime():"+atsBean.getStartTime() +" atsBean.getEndTime()"+atsBean.getEndTime());
					if (!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(atsBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(atsBean.getEndTime())))){
						TempMemory.getOverspeedAlarmCfgMap(vid, overspeedAlarmCfgMapNoNew).setStartTime(startTime);
						TempMemory.getOverspeedAlarmCfgMap(vid, overspeedAlarmCfgMapNoNew).setEndTime(endTime);
						TempMemory.getOverspeedAlarmCfgMap(vid, overspeedAlarmCfgMapNoNew).setSpeedScale(speedScale);
					}
					
					TempMemory.getOverspeedAlarmCfgMap(vid, overspeedAlarmCfgMapNoNew).getSetList().add(atsBean);
				}

			}// End while
			TempMemory.setOverspeedAlarmCfgMapNo(overspeedAlarmCfgMapNoNew);//切换配置
		} catch (SQLException e) {
			logger.error("查询到车辆超速报警信息出错-ERROR-数据库异常"+e.getMessage(),e);
			e.printStackTrace();
		} catch (Exception e) {
			logger.error("查询车辆超速报警过程出错-ERROR-其他异常"+e.getMessage(),e);
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}
				if (stQueryOverspeedAlarmCfg != null) {
					stQueryOverspeedAlarmCfg.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询到车辆超速报警信息出错，关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}
	
	/**
	 * 定时更新超速报警时间设置
	 * 缓存刷新
	 * @param time
	 */
	public void updateOverspeedAlarmCfg() {
		int overspeedAlarmCfgMapNo = TempMemory.getOverspeedAlarmCfgMapNo();//获取当前配置所使用的编号
		//int illeOptAlarmMapNoNew = illeOptAlarmMapNo==1?2:1;
		//TempMemory.clearIlleOptAlarmMap();//清除就缓存，准备载入新配置

		try {
			Map<String, OverspeedAlarmCfgBean> map = TempMemory.getOverspeedAlarmCfgMap(overspeedAlarmCfgMapNo);
			Set tmpSet = map.keySet();
			Iterator itr = tmpSet.iterator();
			while(itr.hasNext()){
				OverspeedAlarmCfgBean entry=(OverspeedAlarmCfgBean) map.get((String)itr.next());
				//根据当前时间判断，复制当前所处时段的非法营运设置
				String currTime = CDate.getTimeShort();
				if (entry==null) continue;
				List<AlarmTacticsSetBean> ls = entry.getSetList();
				for (int i=0;i<ls.size();i++){
					AlarmTacticsSetBean tmpBean = ls.get(i);
					if (tmpBean!=null&&!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(tmpBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(tmpBean.getEndTime())))){
						entry.setStartTime(tmpBean.getStartTime());
						entry.setEndTime(tmpBean.getEndTime());
						entry.setSpeedScale(tmpBean.getSpeedScale());
					}
				}
			}
		} catch (Exception e) {
			logger.error("定时更新车辆超速报警配置-ERROR："+e.getMessage(),e);
			e.printStackTrace();
		} 
	}
	
	
	/**
	 * 初始化VID对应疲劳驾驶报警设置Map
	 * 
	 * @param time
	 */
	public void queryFatigueAlarmCfg () {
		int fatigueAlarmCfgMapNo = TempMemory.getFatigueAlarmCfgMapNo();//获取当前配置所使用的编号
		int fatigueAlarmCfgMapNoNew = fatigueAlarmCfgMapNo==1?2:1;
		TempMemory.clearFatigueAlarmCfgMap();//清除就缓存，准备载入新配置
		OraclePreparedStatement stQueryFatigueAlarmCfg= null;
		OracleResultSet rs = null;
 
		// 从连接池获得连接
		OracleConnection conn = null;

		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryFatigueAlarmCfg = (OraclePreparedStatement) conn.prepareStatement(queryFatigueCfgSQL);
			rs = (OracleResultSet) stQueryFatigueAlarmCfg.executeQuery();
			while (rs.next()) {
				String vid=rs.getString("VID");
				String entId = rs.getString("ENT_ID");
				String startTime = rs.getString("START_TIME");
				String endTime = rs.getString("END_TIME");
				Long deferred = rs.getLong("DEFERRED");
				
				AlarmTacticsSetBean atsBean = new AlarmTacticsSetBean();
				atsBean.setStartTime(startTime);
				atsBean.setEndTime(endTime);
				atsBean.setDeferred(deferred);
				
				FatigueAlarmCfgBean fatigueAlarmCfgBean = new FatigueAlarmCfgBean();
				fatigueAlarmCfgBean.setVid(vid);//车辆ID
				fatigueAlarmCfgBean.setEntId(entId);//所属企业
				fatigueAlarmCfgBean.setStartTime(startTime);//开始时间
				fatigueAlarmCfgBean.setEndTime(endTime);//结束时间
				fatigueAlarmCfgBean.setDeferred(deferred);//持续时间

				
				//key：车vid,value:该车对应的超速报警配置
				FatigueAlarmCfgBean oldBean = TempMemory.getFatigueAlarmCfgMap(vid, fatigueAlarmCfgMapNoNew);
				if (oldBean==null){
					TempMemory.setFatigueAlarmCfgMap(vid, fatigueAlarmCfgBean,fatigueAlarmCfgMapNoNew);
					TempMemory.getFatigueAlarmCfgMap(vid, fatigueAlarmCfgMapNoNew).getSetList().add(atsBean);
				}else{
					//判断起始时间和结束时间，如果在时间之内就更新
					String currTime = CDate.getTimeShort();
					if (!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(atsBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(atsBean.getEndTime())))){
						TempMemory.getFatigueAlarmCfgMap(vid, fatigueAlarmCfgMapNoNew).setStartTime(startTime);
						TempMemory.getFatigueAlarmCfgMap(vid, fatigueAlarmCfgMapNoNew).setEndTime(endTime);
						TempMemory.getFatigueAlarmCfgMap(vid, fatigueAlarmCfgMapNoNew).setDeferred(deferred);
					}
					
					TempMemory.getFatigueAlarmCfgMap(vid, fatigueAlarmCfgMapNoNew).getSetList().add(atsBean);
				}

			}// End while
			TempMemory.setFatigueAlarmCfgMapNo(fatigueAlarmCfgMapNoNew);//切换配置
		} catch (SQLException e) {
			logger.error("查询到车辆疲劳驾驶报警信息出错-ERROR-数据库异常"+e.getMessage(),e);
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}
				if (stQueryFatigueAlarmCfg != null) {
					stQueryFatigueAlarmCfg.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询到疲劳驾驶报警信息出错，关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}
	
	/**
	 * 定时更新超速报警时间设置
	 * 缓存刷新
	 * @param time
	 */
	public void updateFatigueAlarmCfg() {
		int fatigueAlarmCfgMapNo = TempMemory.getFatigueAlarmCfgMapNo();//获取当前配置所使用的编号
		//int illeOptAlarmMapNoNew = illeOptAlarmMapNo==1?2:1;
		//TempMemory.clearIlleOptAlarmMap();//清除就缓存，准备载入新配置

		try {
			Map<String, FatigueAlarmCfgBean> map = TempMemory.getFatigueAlarmCfgMap(fatigueAlarmCfgMapNo);
			Set tmpSet = map.keySet();
			Iterator itr = tmpSet.iterator();
			while(itr.hasNext()){
				FatigueAlarmCfgBean entry=(FatigueAlarmCfgBean) map.get((String)itr.next());
				//根据当前时间判断，复制当前所处时段的非法营运设置
				String currTime = CDate.getTimeShort();
				if (entry==null) continue;
				List<AlarmTacticsSetBean> ls = entry.getSetList();
				for (int i=0;i<ls.size();i++){
					AlarmTacticsSetBean tmpBean = ls.get(i);
					if (tmpBean!=null&&!(java.sql.Time.valueOf(currTime).before(java.sql.Time.valueOf(tmpBean.getStartTime()))||java.sql.Time.valueOf(currTime).after(java.sql.Time.valueOf(tmpBean.getEndTime())))){
						entry.setStartTime(tmpBean.getStartTime());
						entry.setEndTime(tmpBean.getEndTime());
						entry.setDeferred(tmpBean.getDeferred());
					}
				}
			}
		} catch (Exception e) {
			logger.error("定时更新疲劳驾驶报警配置-ERROR："+e.getMessage(),e);
			e.printStackTrace();
		} 
	}
	
	/**
	 * 初始化VID对应站点设置Map
	 * 
	 * @param time
	 */
	public void queryLineStationCfg() {
		int stationMapNo = TempMemory.getStationMapNo();//当期可以使用的缓存编号
		int stationMapNoNew = stationMapNo==1?2:1;
		TempMemory.clearStationMap();//清空线路缓存，重新同步载入。
		OraclePreparedStatement stQueryStation = null;
		OracleResultSet rs = null;
		// 从连接池获得连接
		OracleConnection conn = null;
		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryStation = (OraclePreparedStatement) conn.prepareStatement(queryTrVehicleStationSQL);
			rs = (OracleResultSet) stQueryStation.executeQuery();

			while (rs.next()) {
				try {
					String vid = rs.getString("VID");
					
					TbLineStationBean bean = new TbLineStationBean();
					
					bean.setStationId(rs.getString("STATION_ID"));//围栏ID
					bean.setStationCode(rs.getString("STATION_CODE"));
					bean.setStationName(rs.getString("STATION_NAME"));//围栏名称
					bean.setStationRadius(rs.getLong("STATION_RADIUS"));
					bean.setStationNumber(rs.getLong("STATION_NUMBER"));
					bean.setStationNum(rs.getLong("STATION_NUM"));
					bean.setLineId(rs.getString("LINE_ID"));
					bean.setMapLat(rs.getLong("MAPLAT"));
					bean.setMapLon(rs.getLong("MAPLON"));

					//key：车vid,value:该车所在的各个围栏集合
					if(TempMemory.getStationMap(vid,stationMapNoNew)==null||TempMemory.getStationMap(vid,stationMapNoNew).size()<=0){
						List<TbLineStationBean> list = new ArrayList<TbLineStationBean>();
						list.add(bean);
						TempMemory.setStationMap(vid, list,stationMapNoNew);
					}else{
						TempMemory.getStationMap(vid,stationMapNoNew).add(bean);
					}
					
				} catch (Exception e) {
					logger.error("查询车辆站点设置信息错误" + e.getMessage());
					e.printStackTrace();
				}

			}// End while
			
			TempMemory.setStationMapNo(stationMapNoNew);//切换配置
			logger.info("同步车辆站点设置信息完成！");
		} catch (SQLException e) {
			logger.error("查询车辆站点设置信息错误" + e.getMessage(),e);
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryStation != null) {
					stQueryStation.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询车辆站点设置信息错误，关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}
	
	/**
	 * 初始化企业对应父ID串信息
	 * 
	 * @param time
	 */
	public void queryOrgParentInfo() {
		int orgParentMapNo = TempMemory.getOrgParentMapNo();//当期可以使用的缓存编号
		int orgParentMapNoNew = orgParentMapNo==1?2:1;
		TempMemory.clearOrgParentMap();//清空线路缓存，重新同步载入。
		OraclePreparedStatement stQueryOrgParent = null;
		OracleResultSet rs = null;
		// 从连接池获得连接
		OracleConnection conn = null;
		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryOrgParent = (OraclePreparedStatement) conn.prepareStatement(queryOrgParentInfoSQL);
			rs = (OracleResultSet) stQueryOrgParent.executeQuery();

			while (rs.next()) {
				try {
					String entId = rs.getString("MOTORCADE");
					String entName = rs.getString("ENT_NAME");
					String parentIds = rs.getString("PARENT_ID");
					
					OrgParentInfo bean = new OrgParentInfo();
					
					bean.setEntId(entId);//围栏ID
					bean.setEntName(entName);//围栏名称
					bean.setParent(parentIds);
					
					TempMemory.setOrgParentMap(entId,bean,orgParentMapNoNew);
					
				} catch (Exception e) {
					logger.error("查询车辆站点设置信息错误" + e.getMessage());
					e.printStackTrace();
				}

			}// End while
			
			TempMemory.setOrgParentMapNo(orgParentMapNoNew);//切换配置
			logger.info("同步企业所属父ID串信息完成！");
		} catch (SQLException e) {
			logger.error("同步企业所属父ID串信息错误" + e.getMessage(),e);
			e.printStackTrace();
		} finally {
			try {
				if (rs != null) {
					rs.close();
				}

				if (stQueryOrgParent != null) {
					stQueryOrgParent.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("查询车辆站点设置信息错误，关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}

	
	public static void main(String[] args) {

	}
}
