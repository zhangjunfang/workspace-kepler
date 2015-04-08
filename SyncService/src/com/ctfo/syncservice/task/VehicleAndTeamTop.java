package com.ctfo.syncservice.task;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collections;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.redis.util.RedisJsonUtil;
import com.ctfo.syncservice.model.GradeMonthstat;
import com.ctfo.syncservice.model.Organization;
import com.ctfo.syncservice.model.TsGradeMonthstat;
import com.ctfo.syncservice.util.Constant;
import com.ctfo.syncservice.util.TaskAdapter;
import com.ctfo.syncservice.util.Tools;

/*****************************************
 * <li>描        述：车辆、车队排行榜同步服务		
 * 
 *****************************************/
public class VehicleAndTeamTop  extends TaskAdapter {
	public static Logger logger = LoggerFactory.getLogger(VehicleAndTeamTop.class);
	/** 排行榜统计日期	  */
	private static String top_minute = "05";
	/** 排行榜统计时间	  */
	private static String top_hours = "20";
	
	/*****************************************
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		setName("VehicleAndTeamTop");
		top_minute = conf.get("top_minute");
		top_hours = conf.get("top_hours");
	}
	/*****************************************
	 * <li>描 述：同步信息 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void execute() {
		long start = System.currentTimeMillis();
		try {
			logger.info("--syncTop--同步车辆、车队排行榜 - 检查 - checkTime");
//			判断是否在同步中
			if(Tools.syncTopStatus){
				logger.info("--syncTop--同步车辆、车队排行榜 - 正在同步");
				return ;
			}
	//		每天运行
			String hoursStr = new SimpleDateFormat("HH").format(new Date());
			if(!hoursStr.equals(top_hours)){
				return;
			}
			String minuteStr = new SimpleDateFormat("mm").format(new Date());
			if(!minuteStr.startsWith(top_minute)){
				return;
			}
//			标记同步开始
			Tools.syncTopStatus = true;
			logger.info("--syncTop--同步车辆、车队排行榜任务开始...");
			syncTop(Constant.VEHICLE_TOP, Constant.M_VEHICLETOP, "同步车辆排行");
			syncTop(Constant.TEAM_TOP, Constant.M_VEHICLETEAMTOP, "同步车队排行");
			logger.info("--syncTop--同步车辆、车队排行榜任务结束, 总耗时:[{}]ms", (System.currentTimeMillis() - start) );
			
//			休眠10分钟, 防止同一时段重复同步
			Thread.sleep(1000 * 60 * 10);
			
		} catch (Exception e) {
			logger.error("--syncTop--同步车辆、车队排行榜任务异常:" + e.getMessage(), e);
		}finally {
			Tools.syncTopStatus = false;
		}
	}
	
	/*****************************************
	 * <li>描        述：同步排行榜 		</li><br>
	 * <li>时        间：2013-10-17  上午11:32:54	</li><br>
	 * <li>参数： @param type	
	 * <li>参数： @param topType
	 * <li>参数： @param desc			</li><br>
	 * 
	 *****************************************/
	private void syncTop(String type, String topType, String desc) {
		long start = System.currentTimeMillis();
		Map<String, List<GradeMonthstat>> mapClient = new HashMap<String, List<GradeMonthstat>>();
		Jedis jedis = null;
		try {
			mapClient = synMemTop(type, desc);
			jedis = this.redis.getJedisConnection();
			jedis.select(5);
			jedis.set(topType, RedisJsonUtil.objectToJson(mapClient));
			logger.info("--syncTop--{}结束, 处理数据({})条, 耗时:({})ms", desc, mapClient.size(), System.currentTimeMillis() - start );
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("--syncTop--" + desc + "异常:" + e.getMessage(), e);
		} finally {
			if(jedis != null){
				this.redis.returnJedisConnection(jedis);
			}
			if(mapClient != null && mapClient.size() > 0){
				mapClient.clear();
			}
		}
		
	}

	/**
	 * 查询车辆、车队排行榜
	 * @param String
	 * @return Map
	 */
	private Map<String, List<GradeMonthstat>> synMemTop(String type, String desc) {
		Connection connection = null;	
		Statement oraclestmt = null;
		PreparedStatement oracleps = null;
		// 企业用户结果集
		ResultSet oraclestmtrs = null;
		// 车辆排行结果集
		ResultSet oraclers = null;
		List<Organization> orgList = new ArrayList<Organization>();
		Map<String, List<GradeMonthstat>> mapClient = new HashMap<String, List<GradeMonthstat>>();
		try {		
			connection = this.oracle.getConnection();
			this.oracle.getConnection();
			oraclestmt = connection.createStatement();
			oraclestmtrs = oraclestmt.executeQuery(conf.get("sql_top_org"));
			while (oraclestmtrs.next()) {
				Organization tbOrganization = new Organization();
				tbOrganization.setEntId(oraclestmtrs.getString("ENT_ID"));
				tbOrganization.setEntName(oraclestmtrs.getString("ENT_NAME"));
				orgList.add(tbOrganization);
			}// End while
			
			if(type != null && !type.equals("")) {
				// 车辆排行
				if(type.equals(Constant.VEHICLE_TOP)) {
					oracleps = connection.prepareStatement(conf.get("sql_vehicleTop"));
					mapClient = getMemVehicleTopMap(oracleps, oraclers, orgList);
				}
				
				// 车队排行
				if(type.equals(Constant.TEAM_TOP)) {
					oracleps = connection.prepareStatement(conf.get("sql_vehicleTeamTop"));
					mapClient = getMemTeamTopMap(oracleps, oraclers, orgList);
				}
			}
		
		} catch (Exception e) {
			logger.error("--syncTop--" + desc + "异常:" + e.getMessage(), e);
		} finally {
			try {
				if(oraclestmtrs != null) {
					oraclestmtrs.close();
				}
				if(oraclers != null) {
					oraclers.close();
				}
				if(oraclestmt != null) {
					oraclestmt.close();
				}
				if(oracleps != null) {
					oracleps.close();
				}
				if(connection != null) {
					connection.close();
				}
				if(orgList != null){
					orgList.clear();
				}
			} catch (Exception ex) {
				logger.error("--syncTop--" + desc + "关闭资源异常:" + ex.getMessage(), ex);
			}
		}
		return mapClient;
	}
	
	/**
	 * 获得车辆排行榜
	 * @param oracleps
	 * @param oraclers
	 * @param orgList
	 * @return
	 */
	@SuppressWarnings("unchecked")
	private Map<String, List<GradeMonthstat>> getMemVehicleTopMap(PreparedStatement oracleps, ResultSet oraclers, List<Organization> orgList) {
		Map<String, List<GradeMonthstat>> mapClient = new HashMap<String, List<GradeMonthstat>>();
		Map<String, TsGradeMonthstat> map1 = null;
		Map<String, TsGradeMonthstat> map2 = null;
		try {		
			map1 = new HashMap<String, TsGradeMonthstat>();
			map2 = new HashMap<String, TsGradeMonthstat>();
			for (Organization org : orgList) {
				List<GradeMonthstat> monthList = new ArrayList<GradeMonthstat>(); 
				String[] dateStr1 = dateFormat(getMonth(-1)).split("-");
				String[] dateStr2 = dateFormat(getMonth(-2)).split("-");
				// 1个月前的数据
				map1 = getVehicleMonthstatList(oracleps, oraclers, dateStr1, org);
				
				// 2个月前的数据
				map2 = getVehicleMonthstatList(oracleps, oraclers, dateStr2, org);
				
				// 比较两个月的数据
				for (Entry<String, TsGradeMonthstat> entry : map1.entrySet()) {
					TsGradeMonthstat tsGradeMonthstatTmp1 = entry.getValue();
					TsGradeMonthstat tsGradeMonthstatTmp2 = map2.get(entry.getKey());
					if (null != tsGradeMonthstatTmp1) {
						GradeMonthstat gradeMonthstat = new GradeMonthstat();
						if (null != tsGradeMonthstatTmp1.getSeqId()) {
							gradeMonthstat.setSeqId(Integer.parseInt(String.valueOf(tsGradeMonthstatTmp1.getSeqId())));
						}
						if (null != tsGradeMonthstatTmp1.getVid()) {
							gradeMonthstat.setVid(String.valueOf(tsGradeMonthstatTmp1.getVid()));
						}
						if (null != tsGradeMonthstatTmp1.getVehicleNo()) {
							gradeMonthstat.setVehicleNo(tsGradeMonthstatTmp1.getVehicleNo());
						}
						if (null != tsGradeMonthstatTmp1.getAllScoreSum()) {
							gradeMonthstat.setAllScoreSum(tsGradeMonthstatTmp1.getAllScoreSum());
							if (null == tsGradeMonthstatTmp2) {
								gradeMonthstat.setSign(1);
							} else {
								if (tsGradeMonthstatTmp1.getAllScoreSum() > tsGradeMonthstatTmp2.getAllScoreSum()) {
									gradeMonthstat.setSign(1);
								} else if (tsGradeMonthstatTmp1.getAllScoreSum() == tsGradeMonthstatTmp2.getAllScoreSum()) {
									gradeMonthstat.setSign(0);
								} else if (tsGradeMonthstatTmp1.getAllScoreSum() < tsGradeMonthstatTmp2.getAllScoreSum()) {
									gradeMonthstat.setSign(-1);
								}
							}
						}
						monthList.add(gradeMonthstat);
					}
				}// End for
				Collections.sort(monthList);
				mapClient.put(String.valueOf(org.getEntId()), monthList);
			} // End for
			
		} catch (Exception e) {
			logger.error("--syncTop--获取排行榜数据失败",e);
		}finally{
			if(map1 != null ){
				map1.clear();
				map1 = null;
			}
			
			if(map2 != null){
				map2.clear();
				map2 = null;
			}
		}
		
		return mapClient;
	}
	
	private Map<String, TsGradeMonthstat> getVehicleMonthstatList(PreparedStatement oracleps, ResultSet oraclers, String[] dateStr, Organization org) {	
		List<TsGradeMonthstat> list = new ArrayList<TsGradeMonthstat>();
		Map<String, TsGradeMonthstat> map = new HashMap<String, TsGradeMonthstat>();
		
		try {
			oracleps.setString(1, org.getEntId());
			oracleps.setString(2, org.getEntId());
			oracleps.setString(3, dateStr[0]);
			oracleps.setString(4, dateStr[1]);
			oraclers = oracleps.executeQuery();
					
			while (oraclers.next()) {
				TsGradeMonthstat tsGradeMonthstat = new TsGradeMonthstat();
				tsGradeMonthstat.setSeqId(oraclers.getLong("SEQID"));
				tsGradeMonthstat.setVid(oraclers.getString("VID"));
				tsGradeMonthstat.setVehicleNo(oraclers.getString("vehicle_no"));
				tsGradeMonthstat.setAllScoreSum(oraclers.getLong("all_score_sum"));
				list.add(tsGradeMonthstat);
			} // End while
			
			if(list != null) {
				for (TsGradeMonthstat tsGradeMonthstat : list) {
					if(tsGradeMonthstat.getVid() != null) {
						map.put(String.valueOf(tsGradeMonthstat.getVid()), tsGradeMonthstat);
					}
				}// End for
			}
		} catch (Exception e) {
			logger.error("--syncTop--获取排行榜数据失败：",e);
		}
		return map;
	}
	
	/**
	 * 获得车队排行榜
	 * @param oracleps
	 * @param oraclers
	 * @param orgList
	 * @return
	 */
	@SuppressWarnings("unchecked")
	private Map<String, List<GradeMonthstat>> getMemTeamTopMap(PreparedStatement oracleps, ResultSet oraclers, List<Organization> orgList) {
		Map<String, List<GradeMonthstat>> mapClient = new HashMap<String, List<GradeMonthstat>>();
		Map<String, TsGradeMonthstat> map1 = null;
		Map<String, TsGradeMonthstat> map2 = null;
		try {		
			 map1 = new HashMap<String, TsGradeMonthstat>();
			 map2 = new HashMap<String, TsGradeMonthstat>();
			for (Organization org : orgList) {
				List<GradeMonthstat> monthList = new ArrayList<GradeMonthstat>(); 
				String[] dateStr1 = dateFormat(getMonth(-1)).split("-");
				String[] dateStr2 = dateFormat(getMonth(-2)).split("-");
				// 1个月前的数据
				map1 = getTeamMonthstatList(oracleps, oraclers, dateStr1, org);
				
				// 2个月前的数据
				map2 = getTeamMonthstatList(oracleps, oraclers, dateStr2, org);
				
				// 比较两个月的数据
				for (Entry<String, TsGradeMonthstat> entry : map1.entrySet()) {
					TsGradeMonthstat tsGradeMonthstatTmp1 = entry.getValue();
					TsGradeMonthstat tsGradeMonthstatTmp2 = map2.get(entry.getKey());
					if (null != tsGradeMonthstatTmp1) {
						GradeMonthstat gradeMonthstat = new GradeMonthstat();
						if (null != tsGradeMonthstatTmp1.getTeamId()) {
							gradeMonthstat.setTeamId(tsGradeMonthstatTmp1.getTeamId());
						}
						if (null != tsGradeMonthstatTmp1.getSeqId()) {
							gradeMonthstat.setSeqId(Integer.parseInt(String.valueOf(tsGradeMonthstatTmp1.getSeqId())));
						}
						if (null != tsGradeMonthstatTmp1.getTeamName()) {
							gradeMonthstat.setTeamName(tsGradeMonthstatTmp1.getTeamName());
						}
						if (null != tsGradeMonthstatTmp1.getAllScoreSum()) {
							gradeMonthstat.setAllScoreSum(tsGradeMonthstatTmp1.getAllScoreSum());
							if (null == tsGradeMonthstatTmp2) {
								gradeMonthstat.setSign(1);
							} else {
								if (tsGradeMonthstatTmp1.getAllScoreSum() > tsGradeMonthstatTmp2.getAllScoreSum()) {
									gradeMonthstat.setSign(1);
								} else if (tsGradeMonthstatTmp1.getAllScoreSum() == tsGradeMonthstatTmp2.getAllScoreSum()) {
									gradeMonthstat.setSign(0);
								} else if (tsGradeMonthstatTmp1.getAllScoreSum() < tsGradeMonthstatTmp2.getAllScoreSum()) {
									gradeMonthstat.setSign(-1);
								}
							}
						}
						monthList.add(gradeMonthstat);
					}
				}// End for
				Collections.sort(monthList);
				mapClient.put(String.valueOf(org.getEntId()), monthList);
			}// End for
			
		} catch (Exception e) {
			logger.error("--syncTop--获取排行榜数据失败：" ,e);
		}finally{
			if(map1 != null){
				map1.clear();
				map1 = null;
			}
			
			if(map2 != null){
				map2.clear();
				map2 = null;
			}
		}
		return mapClient;
	}
	
	private Map<String, TsGradeMonthstat> getTeamMonthstatList(PreparedStatement oracleps, ResultSet oraclers, 
			String[] dateStr, Organization org) {	
		List<TsGradeMonthstat> list = new ArrayList<TsGradeMonthstat>();
		Map<String, TsGradeMonthstat> map = new HashMap<String, TsGradeMonthstat>();
		
		try {
			oracleps.setString(1, org.getEntId());
			oracleps.setString(2, org.getEntId());
			oracleps.setString(3, dateStr[0]);
			oracleps.setString(4, dateStr[1]);
			oraclers = oracleps.executeQuery();
			
			while (oraclers.next()) {
				TsGradeMonthstat tsGradeMonthstat = new TsGradeMonthstat();
				tsGradeMonthstat.setSeqId(oraclers.getLong("SEQID"));
				tsGradeMonthstat.setTeamId(oraclers.getString("team_id"));
				tsGradeMonthstat.setTeamName(oraclers.getString("team_name"));
				tsGradeMonthstat.setAllScoreSum(oraclers.getLong("all_score_sum"));
				list.add(tsGradeMonthstat);		
			}// End while
			
			if(list != null) {
				for (TsGradeMonthstat tsGradeMonthstat : list) {
					if(tsGradeMonthstat.getTeamId() != null) {
						map.put(String.valueOf(tsGradeMonthstat.getTeamId()), tsGradeMonthstat);
					}
				}
			}
		} catch (Exception e) {
			logger.error("--syncTop--获取排行榜数据失败",e);
		}
		return map;
	}
	
	/**
	 * 时间格式化
	 * @param date
	 * @return
	 */
	private static String dateFormat(Date date) {
		String str = "";
		if (null != date) {
			str = new SimpleDateFormat("yyyy-MM").format(date);
		}
		return str;
	}
	
	/**
	 * 以当前月份为准，获取指定月份，如monthNum=-1获取上一个月
	 * @param monthNum
	 * @return
	 */
	private static Date getMonth(int monthNum) {
		Calendar calendar = Calendar.getInstance();
		calendar.add(Calendar.MONTH, monthNum);
		return calendar.getTime();
	}
	public static String getTop_minute() {
		return top_minute;
	}
	public static void setTop_minute(String top_minute) {
		VehicleAndTeamTop.top_minute = top_minute;
	}
	public static String getTop_hours() {
		return top_hours;
	}
	public static void setTop_hours(String top_hours) {
		VehicleAndTeamTop.top_hours = top_hours;
	}
}
