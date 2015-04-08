package com.ctfo.analy.dao;

import java.sql.Clob;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
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
import com.ctfo.analy.beans.OrgParentInfo;
import com.ctfo.analy.beans.OverspeedAlarmCfgBean;
import com.ctfo.analy.beans.RoadAlarmBean;
import com.ctfo.analy.beans.TbLineStationBean;
import com.ctfo.analy.connpool.OracleConnectionPool;
import com.ctfo.analy.util.CDate;
import com.ctfo.analy.util.ExceptionUtil;
import com.lingtu.xmlconf.XmlConf;

/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： KCPTDataAnaly		</li><br>
 * <li>文件名称：com.ctfo.analy.dao </li><br>
 * <li>时        间：2013-5-8  下午12:43:49	</li><br>
 * <li>描        述：车辆报警设置缓存缓存更新数据库操作类			</li><br>
 * </ul>
 *****************************************/
public class CacheUpdateDBAdapter {
	private static final Logger log = Logger.getLogger(CacheUpdateDBAdapter.class);
	
	/** 车车辆报警设置缓存更新SQL */
	private String vehicleAlarmUpdateSql;
	/** 车辆报警设置缓存批量增加SQL */
	private String vehicleAlarmBlukAddSql;
	/** 道路等级缓存更新SQL */
	private String roadAlarmUpdateSql;
	/** 区域报警SQL */
	private String areaAlarmQuerySQL;
	/** 线路报警SQL */
	private String lineAlarmQuerySQL;
	/** 非法运营报警SQL */
	private String illeOptQuerySQL;
	
	String queryOrgAlarmNoticeSQL;
	
	String queryLineAlarmSQL;
	String queryLineSectionWithAlarmSQL;
	
	String queryOverspeedCfgSQL;
	String queryFatigueCfgSQL;
	
	String queryTrVehicleStationSQL;
	
	String queryOrgParentInfoSQL;
	
	/*****************************************
	 * <li>描       述： 车辆报警设置缓存管理类初始化		</li><br>
	 * <li>参        数：@param config		</li><br>
	 *****************************************/
	public void initCacheUpdateDBAdapter(XmlConf config){
		vehicleAlarmUpdateSql 		= config.getStringValue("database|sqlstatement|vehicleAlarmUpdateSql");
		vehicleAlarmBlukAddSql 		= config.getStringValue("database|sqlstatement|vehicleAlarmBlukAddSql");
		roadAlarmUpdateSql 			= config.getStringValue("database|sqlstatement|roadAlarmUpdateSql");
		areaAlarmQuerySQL 			= config.getStringValue("database|sqlstatement|areaAlarmQuerySQL");
		lineAlarmQuerySQL 			= config.getStringValue("database|sqlstatement|lineAlarmQuerySQL");
		illeOptQuerySQL 			= config.getStringValue("database|sqlstatement|illeOptQuerySQL");
		queryOrgAlarmNoticeSQL 		= config.getStringValue("database|sqlstatement|sql_queryAlarmNotice");
		
		// VID对应线路报警设置Map
		queryLineAlarmSQL = config.getStringValue("database|sqlstatement|sql_queryLineAlarm");
		// 设置过告警参数的线路段
		queryLineSectionWithAlarmSQL = config.getStringValue("database|sqlstatement|sql_queryLineAlarmNode");
		
		// VID对应非法运营软报警设置Map
		queryOverspeedCfgSQL = config.getStringValue("database|sqlstatement|sql_queryOverspeedAlarmCfg");

		// VID对应非法运营软报警设置Map
		queryFatigueCfgSQL = config.getStringValue("database|sqlstatement|sql_queryFatigueAlarmCfg");
		
		// VID对应站点设置Map
		queryTrVehicleStationSQL = config.getStringValue("database|sqlstatement|sql_queryTrVehicleStationInfo");
		//企业对应所有父企业ID信息
		queryOrgParentInfoSQL = config.getStringValue("database|sqlstatement|sql_orgParentInfo");
	}
	
	/*****************************************
	 * <li>描       述： 车辆报警设置缓存添加		</li><br>
	 * <li>参        数：@param vid		</li><br>
	 *****************************************/
	public void vehicleAlarmUpdate(String name,String value){
		// 从连接池获得连接
		OracleConnection conn = null;
		OraclePreparedStatement ops = null;
		OracleResultSet rs = null;
		try {
			StringBuffer sb = new StringBuffer(vehicleAlarmUpdateSql);
			sb.append(" AND ").append(name).append(" = ?");
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			ops = (OraclePreparedStatement) conn.prepareStatement(sb.toString());
			ops.setString(1, value);
			rs = (OracleResultSet) ops.executeQuery();
			while (rs.next()) {
				AlarmBaseBean alarmVehicleBean = new AlarmBaseBean();
				alarmVehicleBean.setCommaddr( rs.getString("commaddr"));
				alarmVehicleBean.setVid(rs.getString("vid"));
				alarmVehicleBean.setVehicleno(rs.getString("VEHICLENO"));
				alarmVehicleBean.setCorpId(rs.getString("PENT_ID"));
				alarmVehicleBean.setCorpName(rs.getString("PENT_NAME"));
				alarmVehicleBean.setTeamId(rs.getString("ENT_ID"));
				alarmVehicleBean.setTeamName(rs.getString("ENT_NAME"));
				alarmVehicleBean.setVinCode(rs.getString("VIN_CODE"));
				alarmVehicleBean.setInnerCode(rs.getString("INNER_CODE"));
				alarmVehicleBean.setPlateColor(rs.getString("PLATE_COLOR"));
				alarmVehicleBean.setUpdatetime(System.currentTimeMillis());
				
//				log.debug("activemq---手机号对应车辆报警设置缓存----手机号:---"+rs.getString("commaddr"));
				TempMemory.setAlarmVehicleMap(rs.getString("commaddr"), alarmVehicleBean);
			} 
		} catch (SQLException e) {
			log.error("activemq手机号对应车辆报警设置缓存添加 - 异常:",e);
		} finally {
			try {
				if(rs != null) {
					rs.close();
				}
				if(ops != null) {
					ops.close();
				}
				if(conn != null) {
					conn.close();
				}
			} catch (Exception ex) {
				log.error("关闭连接异常:",ex);
			}
		}
	}
	
	/*****************************************
	 * <li>描       述： 车辆报警设置缓存批量添加		</li><br>
	 * <li>参        数：@param name
	 * <li>参        数：@param value		</li><br>
	 *****************************************/
	public void vehicleAlarmBlukAdd(){
		// 从连接池获得连接
		OracleConnection conn = null;
		OraclePreparedStatement ops = null;
		OracleResultSet rs = null;
		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			ops = (OraclePreparedStatement) conn.prepareStatement(vehicleAlarmBlukAddSql);
			rs = (OracleResultSet) ops.executeQuery();
			while (rs.next()) {
				AlarmBaseBean alarmVehicleBean = new AlarmBaseBean();
				alarmVehicleBean.setCommaddr( rs.getString("COMMADDR"));
				alarmVehicleBean.setVid(rs.getString("VID"));
				alarmVehicleBean.setVehicleno(rs.getString("VEHICLE_NO"));
				alarmVehicleBean.setCorpId(rs.getString("PENT_ID"));
				alarmVehicleBean.setCorpName(rs.getString("PENT_NAME"));
				alarmVehicleBean.setTeamId(rs.getString("ENT_ID"));
				alarmVehicleBean.setTeamName(rs.getString("ENT_NAME"));
				alarmVehicleBean.setVinCode(rs.getString("VIN_CODE"));
				alarmVehicleBean.setInnerCode(rs.getString("INNER_CODE"));
				alarmVehicleBean.setPlateColor(rs.getString("PLATE_COLOR"));
				alarmVehicleBean.setUpdatetime(System.currentTimeMillis());

//				log.debug("activemq---手机号对应车辆报警设置缓存----手机号:---"+rs.getString("commaddr"));
				TempMemory.setAlarmVehicleMap(rs.getString("COMMADDR"), alarmVehicleBean);
			} 
		} catch (SQLException e) {
			log.error("activemq车辆报警缓存批量添加 - 异常:",e);
		} finally {
			try {
				if(rs != null) {
					rs.close();
				}
				if(ops != null) {
					ops.close();
				}
				if(conn != null) {
					conn.close();
				}
			} catch (Exception ex) {
				log.error("关闭连接异常:",ex);
			}
		}
	}
	
	/*****************************************
	 * <li>描       述： 非法运营软报警缓存删除		</li><br>
	 *  
	 * <li>参        数：@param value		</li><br>
	 *****************************************/
	public void illeOptAlarmDelete(String vid) {
		TempMemory.deleteIlleOptAlarmMapByVid(vid);
	}
	/*****************************************
	 * <li>描       述： 车辆区间设置缓存更新		</li><br>
	 *  
	 * <li>参        数：@param name
	 * <li>参        数：@param value		</li><br>
	 *****************************************/
	public void roadAlarmUpdate(String name, String value) {
		/*int roadAlarmMapNo = TempMemory.getRoadAlarmMapNo();
		int roadAlarmMapNoNew = roadAlarmMapNo ==1?2:1; */
		// 从连接池获得连接
		OracleConnection conn = null;
		OraclePreparedStatement ops = null;
		OracleResultSet rs = null;
		try {
			StringBuffer sb = new StringBuffer(roadAlarmUpdateSql);
			sb.append(" AND ").append(name).append(" = ?");
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			ops = (OraclePreparedStatement) conn.prepareStatement(sb.toString());
			ops.setString(1, value);
			rs = (OracleResultSet) ops.executeQuery();
			while (rs.next()) {
				String vid=rs.getString("vid");
				String config_id = rs.getString("config_id");
				RoadAlarmBean roadAlarmBean = new RoadAlarmBean();
				roadAlarmBean.setVid(vid);//车辆ID
				roadAlarmBean.setConfig_id(config_id);//config_id
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
				
//				log.debug("activemq---config_id:---"+rs.getLong("config_id"));
				
				if(TempMemory.getRoadAlarmMap(vid)==null||TempMemory.getRoadAlarmMap(vid).size()<=0){
					List<RoadAlarmBean> list = new ArrayList<RoadAlarmBean>();
					list.add(roadAlarmBean);
					TempMemory.setRoadAlarmMap(vid, list);
				}else{
					boolean  flag = false;
					List<RoadAlarmBean> list = TempMemory.getRoadAlarmMap(vid);
					if (list!=null&&list.size()>0){
						for (int i=0;i<list.size();i++){
							RoadAlarmBean bean= list.get(i);
							if (bean.getVid()==vid&&bean.getConfig_id() == config_id){
								list.set(i, bean);
								flag = true;
								break;
							}
						}
					}
					if (!flag){
						List<RoadAlarmBean> roadAlarmList = TempMemory.getRoadAlarmMap(vid);
						roadAlarmList.clear();
						roadAlarmList.add(roadAlarmBean);
					}
				}
			} 
		} catch (SQLException e) {
			log.error("activemq车辆区间设置缓存更新 - 异常:",e);
		} finally {
			try {
				if(rs != null) {
					rs.close();
				}
				if(ops != null) {
					ops.close();
				}
				if(conn != null) {
					conn.close();
				}
			} catch (Exception ex) {
				log.error("关闭连接异常:",ex);
			}
		}
	}

	/*****************************************
	 * <li>描       述： 车辆区间设置缓存批量更新		</li><br>
	 *  
	 * <li>参        数：		</li><br>
	 *****************************************/
	public void roadAlarmBlukAdd() {
		TempMemory.clearRoadAlarmMap();
		int roadAlarmMapNo = TempMemory.getRoadAlarmMapNo();
		int roadAlarmMapNoNew = roadAlarmMapNo ==1?2:1; 
		// 从连接池获得连接
		OracleConnection conn = null;
		OraclePreparedStatement ops = null;
		OracleResultSet rs = null;
		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			ops = (OraclePreparedStatement) conn.prepareStatement(roadAlarmUpdateSql);
			rs = (OracleResultSet) ops.executeQuery();
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
				
//				log.debug("activemq---config_id:---"+rs.getLong("config_id"));
				
				if(TempMemory.getRoadAlarmMap(vid,roadAlarmMapNoNew)==null||TempMemory.getRoadAlarmMap(vid,roadAlarmMapNoNew).size()<=0){
					List<RoadAlarmBean> list = new ArrayList<RoadAlarmBean>();
					list.add(roadAlarmBean);
					TempMemory.setRoadAlarmMap(vid, list,roadAlarmMapNoNew);
				}else{
					List<RoadAlarmBean> list = TempMemory.getRoadAlarmMap(vid,roadAlarmMapNoNew);
					list.clear();
					list.add(roadAlarmBean);
				}
			} 
			TempMemory.setRoadAlarmMapNo(roadAlarmMapNoNew);
		} catch (SQLException e) {
			log.error("activemq车辆区间设置缓存更新 - 异常:",e);
		} finally {
			try {
				if(rs != null) {
					rs.close();
				}
				if(ops != null) {
					ops.close();
				}
				if(conn != null) {
					conn.close();
				}
			} catch (Exception ex) {
				log.error("关闭连接异常:",ex);
			}
		}
	}

	
	
	
	
	


	/*****************************************
	 * <li>描       述： 区域报警缓存更新		</li><br>
	 *  
	 * <li>参        数：@param name
	 * <li>参        数：@param value		</li><br>
	 *****************************************/
	public void areaAlarmUpdate(String name, String value) {
		TempMemory.clearAreaAlarmMap();
		TempMemory.copyAreaAlarmMap();
		int areaAlarmMapNo = TempMemory.getAreaAlarmMapNo();
		int areaAlarmMapNoNew = areaAlarmMapNo ==1?2:1; 
		
		// 从连接池获得连接
		OracleConnection conn = null;
		OraclePreparedStatement ops = null;
		OracleResultSet rs = null;
		try {
			StringBuffer sb = new StringBuffer(areaAlarmQuerySQL);
			sb.append(" AND ").append(name).append(" = ? ");
			sb.append(" ORDER BY TR.VID ");
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			ops = (OraclePreparedStatement) conn.prepareStatement(sb.toString());
			ops.setString(1, value);
			rs = (OracleResultSet) ops.executeQuery();
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
					String areaId = rs.getString("areaId");
					areaAlarmBean.setAreaid(areaId);//围栏ID
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
//					log.debug("activemq---vid:---"+vid);
					//key：车vid,value:该车所在的各个围栏集合
					if(TempMemory.getAreaAlarmMap(vid,areaAlarmMapNoNew)==null||TempMemory.getAreaAlarmMap(vid,areaAlarmMapNoNew).size()<=0){
						List<AreaAlarmBean> list = new ArrayList<AreaAlarmBean>();
						list.add(areaAlarmBean);
						TempMemory.setAreaAlarmMap(vid, list,areaAlarmMapNoNew);
					}else{
						boolean  flag = false;
						List<AreaAlarmBean> list = TempMemory.getAreaAlarmMap(vid,areaAlarmMapNoNew);
						if (list!=null&&list.size()>0){
							for (int i=0;i<list.size();i++){
								AreaAlarmBean bean= list.get(i);
								if (bean.getVid()==vid&&bean.getAreaid()==areaId){
									list.set(i, areaAlarmBean);
									flag = true;
									break;
								}
							}
						}
						if (!flag){
							TempMemory.getAreaAlarmMap(vid,areaAlarmMapNoNew).add(areaAlarmBean);
						}
					}

				} catch (Exception e) {
					log.error("activemq车辆区间设置缓存更新异常:",e);
				}
			}
			
			TempMemory.setAreaAlarmMapNo(areaAlarmMapNoNew);//切换配置
			TempMemory.setAreaTree();
		} catch (SQLException e) {
			log.error("车辆区间设置缓存更新 - 异常:",e);
		} finally {
			try {
				if(rs != null) {
					rs.close();
				}
				if(ops != null) {
					ops.close();
				}
				if(conn != null) {
					conn.close();
				}
			} catch (Exception ex) {
				log.error("关闭连接异常:",ex);
			}
		}
		
	}

	/*****************************************
	 * <li>描       述： 区域报警缓存全量更新		</li><br>
	 *  
	 * <li>参        数：		</li><br>
	 *****************************************/
	public void areaAlarmBlukAdd() {
		TempMemory.clearAreaAlarmMap();
		int areaAlarmMapNo = TempMemory.getAreaAlarmMapNo();
		int areaAlarmMapNoNew = areaAlarmMapNo ==1?2:1; 
		// 从连接池获得连接
		OracleConnection conn = null;
		OraclePreparedStatement ops = null;
		OracleResultSet rs = null;
		try {
			StringBuffer sb = new StringBuffer(areaAlarmQuerySQL);
			sb.append(" ORDER BY TR.VID ");
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			ops = (OraclePreparedStatement) conn.prepareStatement(sb.toString());
			rs = (OracleResultSet) ops.executeQuery();
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
					log.error("activemq车辆区间设置缓存更新异常:",e);
				}

			}

			TempMemory.setAreaAlarmMapNo(areaAlarmMapNoNew);//切换配置
			TempMemory.setAreaTree();
		} catch (SQLException e) {
			log.error("车辆区间设置缓存更新 - 异常:",e);
		} finally {
			try {
				if(rs != null) {
					rs.close();
				}
				if(ops != null) {
					ops.close();
				}
				if(conn != null) {
					conn.close();
				}
			} catch (Exception ex) {
				log.error("关闭连接异常:",ex);
			}
		}
	}

	/*****************************************
	 * <li>描       述： 线路报警缓存全量更新		</li><br>
	 *  
	 * <li>参        数：		</li><br>
	 *****************************************/
	public void lineAlarmUpdate1() {
		//清空缓存
		TempMemory.clearLineAlarmMap();
		int lineAlarmMapNo = TempMemory.getLineAlarmMapNo();
		int lineAlarmMapNoNew = lineAlarmMapNo ==1?2:1; 
		// 从连接池获得连接
		OracleConnection conn = null;
		OraclePreparedStatement ops = null;
		OracleResultSet rs = null;
		try {
			StringBuffer sb = new StringBuffer(lineAlarmQuerySQL);
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			ops = (OraclePreparedStatement) conn.prepareStatement(sb.toString());
			rs = (OracleResultSet) ops.executeQuery();
			long currentday = CDate.getCurrentDayYearMonthDay();
			while (rs.next()) {
				try {
					String vid = rs.getString("vid");
					LineAlarmBean lineAlarmBean = new LineAlarmBean();
					lineAlarmBean.setLineid(rs.getString("lineid"));
					String lonlat =rs.getString("lonlat");                 
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
					lineAlarmBean.setBeginTime(currentday+ (CDate.timeConvertSec(rs.getString("periodbegintime"))* 1000));
					lineAlarmBean.setEndTime(currentday+ (CDate.timeConvertSec(rs.getString("periodendtime"))* 1000));

					lineAlarmBean.setLonlats(points);
					lineAlarmBean.setUsetype(rs.getString("usetype").split(","));
//					log.debug("activemq----------vid------"+vid);
					//key：车vid,value:该车所在的各个线路集合
					if(TempMemory.getLineAlarmMap(vid,lineAlarmMapNoNew)==null||TempMemory.getLineAlarmMap(vid,lineAlarmMapNoNew).size()<=0){
						List<LineAlarmBean> list = new ArrayList<LineAlarmBean>();
						list.add(lineAlarmBean);
						TempMemory.setLineAlarmMap(vid, list,lineAlarmMapNoNew);
					}else{
						TempMemory.getLineAlarmMap(vid,lineAlarmMapNoNew).add(lineAlarmBean);
					}

				} catch (Exception e) {
					log.error("activemq线路报警缓存更新错误",e);
				}
			}
			
			TempMemory.setLineAlarmMapNo(lineAlarmMapNoNew);//切换配置
			TempMemory.setLineTree();
		} catch (SQLException e) {
			log.error("线路报警缓存更新 - 异常:",e);
		} finally {
			try {
				if(rs != null) {
					rs.close();
				}
				if(ops != null) {
					ops.close();
				}
				if(conn != null) {
					conn.close();
				}
			} catch (Exception ex) {
				log.error("关闭连接异常:",ex);
			}
		}
	}
	
	public void lineAlarmUpdate() {
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
					lineAlarmBean.setUsetype(rs.getString("usetype").split(","));

					//key：车vid,value:该车所在的各个线路集合
					if(TempMemory.getLineAlarmMap(vid,lineAlarmMapNoNew)==null||TempMemory.getLineAlarmMap(vid,lineAlarmMapNoNew).size()<=0){
						List<LineAlarmBean> list = new ArrayList<LineAlarmBean>();
						list.add(lineAlarmBean);
						TempMemory.setLineAlarmMap(vid, list,lineAlarmMapNoNew);
					}else{
						TempMemory.getLineAlarmMap(vid,lineAlarmMapNoNew).add(lineAlarmBean);
					}

				} catch (Exception e) {
					log.error("同步绑定线路报警车辆错误" + ExceptionUtil.getErrorStack(e, 0));
					e.printStackTrace();
				}
			}
			
			TempMemory.setLineAlarmMapNo(lineAlarmMapNoNew);//切换配置
			TempMemory.setLineTree();
//			logger.info("查询到绑定线路报警线段总数[" + TempMemory.getLineAlarmMapSize()+ "]");
		} catch (SQLException e) {
			log.error("查询到绑定线路报警线段总数" + e.getMessage(),e);
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
				log.error("查询到绑定平台判断线路报警车辆总数关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}

	/*****************************************
	 * <li>描       述： 非法运营频道缓存更新		</li><br>
	 *  
	 * <li>参        数：@param name
	 * <li>参        数：@param value		</li><br>
	 *****************************************/
	public void illeOptUpdate() {
		int illeOptAlarmMapNo = TempMemory.getIlleOptAlarmMapNo();//获取当前配置所使用的编号
		int illeOptAlarmMapNoNew = illeOptAlarmMapNo==1?2:1;
		TempMemory.clearIlleOptAlarmMap();//清除就缓存，准备载入新配置
		OraclePreparedStatement stQueryIllegalOptionsAlarm= null;
		OracleResultSet rs = null;
		// 从连接池获得连接
		OracleConnection conn = null;
		try {
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			stQueryIllegalOptionsAlarm = (OraclePreparedStatement) conn.prepareStatement(illeOptQuerySQL);
			rs = (OracleResultSet) stQueryIllegalOptionsAlarm.executeQuery();
			while (rs.next()) {
				String vid=rs.getString("VID");
				IllegalOptionsAlarmBean illegalOptAlarmBean = new IllegalOptionsAlarmBean();
				illegalOptAlarmBean.setVid(vid);//车辆ID
				illegalOptAlarmBean.setEntId(rs.getString("ENT_ID"));//所属企业
				illegalOptAlarmBean.setStartTime(rs.getString("START_TIME"));//开始时间
				illegalOptAlarmBean.setEndTime(rs.getString("END_TIME"));//结束时间
				illegalOptAlarmBean.setDeferred(rs.getLong("DEFERRED"));//持续时间
				illegalOptAlarmBean.setIsDefault(rs.getString("ISDEFAULT"));//是否默认配置（1、是）
				
//				log.debug("activemq-------vid-------"+vid);
				//key：车vid,value:该车对应的非法运营软报警配置
				TempMemory.setIlleOptAlarmMap(vid, illegalOptAlarmBean,illeOptAlarmMapNoNew);
				
			}
			TempMemory.setIlleOptAlarmMapNo(illeOptAlarmMapNoNew);//切换配置
		} catch (SQLException e) {
			log.error("activemq查询到非法运营软报警配置车辆总数-ERROR-数据库异常"+e.getMessage(),e);
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
				log.error("activemq查询到非法运营软报警配置车辆出错，关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}
	
//	/*****************************************
//	 * <ul>
//	 * <li>描       述： main		</li><br>
//	 * <li>参        数：@param args		</li><br>
//	 * </ul>
//	 * @throws Exception 
//	 *****************************************/
//	public static void main(String[] args) throws Exception {
//		String str = Thread.currentThread().getContextClassLoader().getResource("DataAnaly.xml").toString(); 
//		System.out.println(str.substring(5));
//		// 读取配置文件
//		XmlConf conf = new XmlConf(str.substring(5));
//		JDCConnectionInit j = new JDCConnectionInit(conf);
//		j.init();
//		
//		CacheUpdateDBAdapter c = new CacheUpdateDBAdapter();
//		c.initCacheUpdateDBAdapter(conf);
////		c.vehicleAlarmUpdate(" SIM.COMMADDR ","15286843641"); 
////		c.vehicleAlarmBlukAdd();
////		c.vehicleAlarmDelete(" v.vid ","236001");
////		c.vehicleAlarmDelete("sim.sid", "239442");
////		c.vehicleAlarmUpdate("sim.sid", "387");
////		c.vehicleAlarmUpdate(" VINFO.ent_id ", "10221");
////		c.vehicleAlarmUpdate(" TS.SUID ", "33388");
////		c.vehicleAlarmDelete(" TS.SUID ", "25091349");
////		c.sectioncfgUpdate(" tvs.auto_id ", "55");
////		c.sectioncfgBlukAdd();
////		c.roadAlarmBlukAdd();
////		c.roadAlarmUpdate("tss.config_id", "18386845");
////		c.areaAlarmUpdate("tr.id", "30590348");
////		c.areaAlarmBlukAdd();
////		c.areaAlarmUpdate("TRB.BID", "31f8d76994af428284616ae9f331f5c3");
////		c.areaAlarmUpdate("TRA.Id", "30590347");
////		c.areaAlarmUpdate("a.area_id", "30590346");
////		c.lineAlarmBlukAdd();
//		c.lineAlarmUpdate();
////		c.illeOptUpdate();
////		c.vehicleAlarmUpdate("TT.TID", "10910");
//		System.exit(0);
//	}
	
	/**
	 * 企业告警下发消息缓存
	 * 
	 */
	public void queryOrgAlarmNotice() {
		//TempMemory.clearOrgAlarmNoticeMap();清空缓存，重新载入。
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
					log.error("查询到企业告警自动下发消息内容缓存1:" + ExceptionUtil.getErrorStack(e, 0));
					e.printStackTrace();
				}
			}
 		} catch (Exception e) {
			log.error("查询到企业告警自动下发消息内容缓存2:" + ExceptionUtil.getErrorStack(e, 0));
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
				log.error("查询到企业告警自动下发消息内容缓存查询关闭连接" + e.getMessage());
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
			log.error("查询设置报警的路线段出错：[key="+key+"]" + e.getMessage(),e);
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
				log.error("查询设置报警的路线段关闭连接" + e.getMessage());
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
				
//				log.error(vid+":"+entId+":"+speedScale);
				
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
			log.error("查询到车辆超速报警信息出错-ERROR-数据库异常"+e.getMessage(),e);
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
				log.error("查询到车辆超速报警信息出错，关闭连接" + e.getMessage());
				e.printStackTrace();
			}
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
			log.error("查询到车辆疲劳驾驶报警信息出错-ERROR-数据库异常"+e.getMessage(),e);
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
				log.error("查询到疲劳驾驶报警信息出错，关闭连接" + e.getMessage());
				e.printStackTrace();
			}
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
					log.error("查询车辆站点设置信息错误" + e.getMessage());
					e.printStackTrace();
				}

			}// End while
			
			TempMemory.setStationMapNo(stationMapNoNew);//切换配置
			log.info("同步车辆站点设置信息完成！");
		} catch (SQLException e) {
			log.error("查询车辆站点设置信息错误" + e.getMessage(),e);
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
				log.error("查询车辆站点设置信息错误，关闭连接" + e.getMessage());
				e.printStackTrace();
			}
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
			log.error("设置时间格式不正确。" + time);
		}
		return 0;
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
					log.error("查询车辆站点设置信息错误" + e.getMessage());
					e.printStackTrace();
				}

			}// End while
			
			TempMemory.setOrgParentMapNo(orgParentMapNoNew);//切换配置
			log.info("同步企业所属父ID串信息完成！");
		} catch (SQLException e) {
			log.error("同步企业所属父ID串信息错误" + e.getMessage(),e);
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
				log.error("查询车辆站点设置信息错误，关闭连接" + e.getMessage());
				e.printStackTrace();
			}
		}
	}
}
