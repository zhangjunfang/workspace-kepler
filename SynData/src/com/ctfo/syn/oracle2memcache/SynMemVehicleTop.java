package com.ctfo.syn.oracle2memcache;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
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
import java.util.concurrent.TimeoutException;

import org.apache.log4j.Logger;

import com.ctfo.memcache.beans.GradeMonthstat;
import com.ctfo.redis.util.RedisJsonUtil;
import com.ctfo.syn.dao.RedisServer;
import com.ctfo.syn.database.OracleConnectionPool;
import com.ctfo.syn.kcpt.utils.SynPool;
import com.ctfo.syn.membeans.StaticMemcache;
import com.ctfo.syn.membeans.TbOrganization;
import com.ctfo.syn.membeans.TsGradeMonthstat;
import com.ctfo.unifiedstorage.service.JedisUnifiedStorageService;

/**
 * 车辆、车队排行榜同步到memcache服务 (1小时)
 * @author xuehui
 *
 */
public class SynMemVehicleTop implements Runnable{

	public static Logger logger = Logger.getLogger(SynMemVehicleTop.class);
	
	public SynMemVehicleTop() {
	}
	
	public void run() {
		logger.info("开始车辆排行榜查询");
		initMemTop(StaticMemcache.TBPUBLISHINFO_SYS, StaticMemcache.MEMCACHE_VEHICLETOP);
		initMemTop(StaticMemcache.TBPUBLISHINFO_ORG, StaticMemcache.MEMCACHE_VEHICLETEAMTOP);
	}
	
	/**
	 * 初始化同步服务
	 */
	private void initMemTop(String type, String topType) {
		Map<String, List<GradeMonthstat>> mapClient = new HashMap<String, List<GradeMonthstat>>();
		JedisUnifiedStorageService jedis = null;
		try {
			jedis = RedisServer.getJedisServiceInstance();
			mapClient = synMemTop(type);
//			jedis.set(0, topType, RedisJsonUtil.objectToJson(mapClient));//TODO
			jedis.saveVehicleTop(topType, RedisJsonUtil.objectToJson(mapClient));
//			ConnectMemcachePool.getSqlMap(SynPool.getinstance().getSql("memcacheMainAddr")).set(topType, 0, mapClient);
		} catch (TimeoutException e) {
			logger.error(e);
		} catch (InterruptedException e) {
			logger.error(e);
		} catch (Exception e) {
			logger.error("连接memcache出错", e);
		} finally {
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
	private Map<String, List<GradeMonthstat>> synMemTop(String type) {
		Connection connection = null;	
		Statement oraclestmt = null;
		PreparedStatement oracleps = null;
		// 企业用户结果集
		ResultSet oraclestmtrs = null;
		// 车辆排行结果集
		ResultSet oraclers = null;
		List<TbOrganization> orgList = new ArrayList<TbOrganization>();
		Map<String, List<GradeMonthstat>> mapClient = new HashMap<String, List<GradeMonthstat>>();
		
		try {		
			connection = OracleConnectionPool.getConnection();
			logger.debug("oracle数据库连接成功");
			
			oraclestmt = connection.createStatement();
			
			oraclestmtrs = oraclestmt.executeQuery(SynPool.getinstance().getSql("oracle_memcache_org"));
			
			while (oraclestmtrs.next()) {
				TbOrganization tbOrganization = new TbOrganization();
				tbOrganization.setEntId(oraclestmtrs.getLong("ENT_ID"));
				tbOrganization.setEntName(oraclestmtrs.getString("ENT_NAME"));
				orgList.add(tbOrganization);
			}// End while
			
			if(type != null && !type.equals("")) {
				// 车辆排行
				if(type.equals(StaticMemcache.VEHICLE_TOP)) {
					oracleps = connection.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_vehicleTop"));
					mapClient = getMemVehicleTopMap(oracleps, oraclers, orgList);
				}
				
				// 车队排行
				if(type.equals(StaticMemcache.TEAM_TOP)) {
					oracleps = connection.prepareStatement(SynPool.getinstance().getSql("oracle_memcache_vehicleTeamTop"));
					mapClient = getMemTeamTopMap(oracleps, oraclers, orgList);
				}
			}
		
		} catch (Exception e) {
			logger.error("获取信息反馈数据失败：",e);
			try {
				connection.close();
			} catch (SQLException e1) {
				logger.error(e1);
			}	
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
				logger.error("CLOSED PROPERTIES,ORACLE TO FAIL.",ex);
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
	private Map<String, List<GradeMonthstat>> getMemVehicleTopMap(PreparedStatement oracleps, ResultSet oraclers, 
			List<TbOrganization> orgList) {
		Map<String, List<GradeMonthstat>> mapClient = new HashMap<String, List<GradeMonthstat>>();
		Map<String, TsGradeMonthstat> map1 = null;
		Map<String, TsGradeMonthstat> map2 = null;
		try {		
			map1 = new HashMap<String, TsGradeMonthstat>();
			map2 = new HashMap<String, TsGradeMonthstat>();
			for (TbOrganization org : orgList) {
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
			logger.error("获取排行榜数据失败",e);
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
	
	private Map<String, TsGradeMonthstat> getVehicleMonthstatList(PreparedStatement oracleps, ResultSet oraclers, 
			String[] dateStr, TbOrganization org) {	
		List<TsGradeMonthstat> list = new ArrayList<TsGradeMonthstat>();
		Map<String, TsGradeMonthstat> map = new HashMap<String, TsGradeMonthstat>();
		
		try {
			oracleps.setLong(1, org.getEntId());
			oracleps.setLong(2, org.getEntId());
			oracleps.setString(3, dateStr[0]);
			oracleps.setString(4, dateStr[1]);
			oraclers = oracleps.executeQuery();
					
			while (oraclers.next()) {
				TsGradeMonthstat tsGradeMonthstat = new TsGradeMonthstat();
				tsGradeMonthstat.setSeqId(oraclers.getLong("SEQID"));
				tsGradeMonthstat.setVid(oraclers.getLong("VID"));
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
			logger.error("获取排行榜数据失败：",e);
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
	private Map<String, List<GradeMonthstat>> getMemTeamTopMap(PreparedStatement oracleps, ResultSet oraclers, 
			List<TbOrganization> orgList) {
		Map<String, List<GradeMonthstat>> mapClient = new HashMap<String, List<GradeMonthstat>>();
		Map<String, TsGradeMonthstat> map1 = null;
		Map<String, TsGradeMonthstat> map2 = null;
		try {		
			 map1 = new HashMap<String, TsGradeMonthstat>();
			 map2 = new HashMap<String, TsGradeMonthstat>();
			for (TbOrganization org : orgList) {
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
			logger.error("获取排行榜数据失败：" ,e);
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
			String[] dateStr, TbOrganization org) {	
		List<TsGradeMonthstat> list = new ArrayList<TsGradeMonthstat>();
		Map<String, TsGradeMonthstat> map = new HashMap<String, TsGradeMonthstat>();
		
		try {
			oracleps.setLong(1, org.getEntId());
			oracleps.setLong(2, org.getEntId());
			oracleps.setString(3, dateStr[0]);
			oracleps.setString(4, dateStr[1]);
			oraclers = oracleps.executeQuery();
			
			while (oraclers.next()) {
				TsGradeMonthstat tsGradeMonthstat = new TsGradeMonthstat();
				tsGradeMonthstat.setSeqId(oraclers.getLong("SEQID"));
				tsGradeMonthstat.setTeamId(oraclers.getLong("team_id"));
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
			logger.error("获取排行榜数据失败",e);
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
}
