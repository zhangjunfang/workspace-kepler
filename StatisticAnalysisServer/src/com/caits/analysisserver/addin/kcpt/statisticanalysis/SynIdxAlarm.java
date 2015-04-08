package com.caits.analysisserver.addin.kcpt.statisticanalysis;

import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.TimeoutException;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.SystemBaseInfo;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.database.SystemBaseInfoPool;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.SynDateUtils;
import com.ctfo.memcache.beans.AlarmNum;
import com.ctfo.redis.core.RedisAdapter;
import com.ctfo.redis.util.RedisJsonUtil;


public class SynIdxAlarm{
	public static Logger logger = LoggerFactory.getLogger(SynIdxAlarm.class);
    private Connection connection = null;
    private List<String> orgList = new ArrayList<String>();
    private Map<String,String> entToAlarmMap = new HashMap<String,String>();
    private List<String> savedOrgList = new ArrayList<String>(); // 已经存储过的组织
	private final String ALARM_DAY_KEY = "mAlarmInfoDay";
	private final String ALARM_WEEK_KEY = "mAlarmInfoWeek";
	private final String ALARM_MONTH_KEY = "mAlarmInfoMonth";
	//同步日天数
	private int dayCount=0;
	//同步周数
	private int weekCount=0;
	//同步月数
	private int monthCount=0;
	
	public SynIdxAlarm() {
		loadInit();
	}
	
	
	public void run(){
		try {
			/*long dayCostTime = 0;
			long weekCostTime = 0;
			long monthCostTime = 0;
			long startTime = System.currentTimeMillis();
			
			searchOrgId();*/
			
			initInfo();
			selectSynAlarmNum2();
			/*synDayStatAlarm();
			dayCostTime = (System.currentTimeMillis() - startTime);
			
			startTime = System.currentTimeMillis();
			synWeekStatAlarm();
			weekCostTime =  (System.currentTimeMillis() - startTime);
			
			startTime = System.currentTimeMillis();
			synMonthStatAlarm();
			monthCostTime =  (System.currentTimeMillis() - startTime);
			saveAlarmList();
			synToMemcache();
			closeConnection();*/
			//logger.info("Select day of alarm cost time : " + dayCostTime + ";" + "Select week of alarm cost time : " + weekCostTime +";Select month of alarm cost time : " + monthCostTime );
		} catch (Exception e) {
			logger.error("同步报警数据出错",e);
		}
	}
	
	/******
	 * 初始化加载PROPERTIES
	 * @param path
	 */
	private void loadInit(){
				
		//同步日天数
		SystemBaseInfo daySys = SystemBaseInfoPool.getinstance().getBaseInfoMap("dayCount");
		if(daySys.getIsLoad().equals("true")){
			dayCount = Integer.parseInt(daySys.getValue());
		}
		
		//同步周数
		SystemBaseInfo weekSys = SystemBaseInfoPool.getinstance().getBaseInfoMap("weekCount");
		if(weekSys.getIsLoad().equals("true")){
			weekCount = Integer.parseInt(weekSys.getValue());
		}
		
		//同步月数
		SystemBaseInfo monthSys = SystemBaseInfoPool.getinstance().getBaseInfoMap("monthCount");
		if(monthSys.getIsLoad().equals("true")){
			monthCount = Integer.parseInt(monthSys.getValue());
		}
	}
	
	/***
	 * 初始化连接数据库、MEMECACHE
	 */
	private void initInfo(){
		try {
			connection = OracleConnectionPool.getConnection();
		} catch (SQLException e) {
			logger.error("同步首页报警线程获取连接出错",e);
		}
	}
	
	public void synToMemcache() throws SQLException{
		if(entToAlarmMap.size() > 0){
			Iterator<String> orgIt = entToAlarmMap.keySet().iterator();
			Map<String,List<AlarmNum>> dayMap = new HashMap<String,List<AlarmNum>>();
			Map<String,List<AlarmNum>> weekMap = new HashMap<String,List<AlarmNum>>();
			Map<String,List<AlarmNum>> monthMap = new HashMap<String,List<AlarmNum>>();
			while(orgIt.hasNext()){
				String orgId = orgIt.next();
				String alarmList = entToAlarmMap.get(orgId);
				String[] arr = alarmList.split("\\|");
				if(arr.length == 3){
					String[] dayAlarm = arr[0].split("\\]");
					String[] weekAlarm = arr[1].split("\\]");
					String[] monthAlarm = arr[2].split("\\]");
					if(dayAlarm.length == dayCount){// 获取日报警
						List<AlarmNum> list = new ArrayList<AlarmNum>();
						for(String alarm : dayAlarm){
							alarm = alarm.replaceAll("\\[", "");
							AlarmNum alarmNum = new AlarmNum();
							String[] elt = alarm.split(">>");
							if(elt.length == 2){
								String[] elts = elt[1].split(":");
								alarmNum.setAlarmDate(elt[0]);
								if(elts.length == 4){
									alarmNum.setGeneralCount(elts[0]);
									alarmNum.setSeriousCount(elts[1]);
									alarmNum.setSuggestionCount(elts[2]);
									alarmNum.setUrgentCount(elts[3]);
								}
							}
							
							list.add(alarmNum);
						}// End for
						dayMap.put(orgId + "", list);
					}
					
					if(weekAlarm.length == weekCount){ // 获取周报警
						List<AlarmNum> list = new ArrayList<AlarmNum>();
						for(String alarm : weekAlarm){
							alarm = alarm.replaceAll("\\[", "");
							AlarmNum alarmNum = new AlarmNum();
							String[] elt = alarm.split(">>");
							if(elt.length == 2){
								String[] elts = elt[1].split(":");
								alarmNum.setAlarmDate(elt[0]);
								if(elts.length == 4){
									alarmNum.setGeneralCount(elts[0]);
									alarmNum.setSeriousCount(elts[1]);
									alarmNum.setSuggestionCount(elts[2]);
									alarmNum.setUrgentCount(elts[3]);
								}
							}
							
							list.add(alarmNum);
						}// End for
						weekMap.put(orgId + "", list);
					}
					
					if(monthAlarm.length == monthCount){ // 获取月报警
						List<AlarmNum> list = new ArrayList<AlarmNum>();
						for(String alarm : monthAlarm){
							alarm = alarm.replaceAll("\\[", "");
							AlarmNum alarmNum = new AlarmNum();
							String[] elt = alarm.split(">>");
							if(elt.length == 2){
								String[] elts = elt[1].split(":");
								alarmNum.setAlarmDate(elt[0]);
								if(elts.length == 4){
									alarmNum.setGeneralCount(elts[0]);
									alarmNum.setSeriousCount(elts[1]);
									alarmNum.setSuggestionCount(elts[2]);
									alarmNum.setUrgentCount(elts[3]);
								}
							}
							
							list.add(alarmNum);
						}// End for
						monthMap.put(orgId + "", list);
					}
				}
			}// End while
			
			
			
			//yujch 2013-06-25 注释一下2行   数据改为向redis同步后无需判断memached是否连接
//			SystemBaseInfo memcacheMainAddr = SystemBaseInfoPool.getinstance().getBaseInfoMap("memcacheMainAddr");
//			if(memcacheMainAddr.getIsLoad().equals("true")){ // 判断是否同步MEMECACHE
				// 存储memecache
//				MemcachedClientBuilder builder = null;
//				MemcachedClient mcc = null;
//				builder = new XMemcachedClientBuilder(AddrUtil.getAddresses(memcacheMainAddr.getValue()));
				//builder.setSocketOption(StandardSocketOption.SO_SNDBUF, 20 * 1024); // 设置发送缓冲区为16K，默认为8K
		        //builder.setSocketOption(StandardSocketOption.TCP_NODELAY, false); // 启用nagle算法，提高吞吐量，默认关闭
				try {
					
//					mcc = builder.build();
//					mcc.setConnectTimeout(10 * 1000);
//					mcc.setOpTimeout(10 * 1000);		
//					mcc.setEnableHeartBeat(false);
					RedisAdapter.setAlarmTrends(ALARM_DAY_KEY, RedisJsonUtil.objectToJson(dayMap)); 
					RedisAdapter.setAlarmTrends(ALARM_WEEK_KEY, RedisJsonUtil.objectToJson(weekMap)); 
					RedisAdapter.setAlarmTrends(ALARM_MONTH_KEY, RedisJsonUtil.objectToJson(monthMap)); 
//					mcc.set(ALARM_DAY_KEY,0, dayMap); //同步日报警
//					mcc.set(ALARM_WEEK_KEY,0, weekMap); //同步周报警
//					mcc.set(ALARM_MONTH_KEY,0, monthMap); //同步月报警
				} catch (Exception e) {
					logger.error("连接Redis出错", e);
				}finally{
//					if(mcc != null){
//						try {
//							mcc.shutdown();
//						} catch (IOException e) {
//							logger.error(e);
//							mcc = null;
//						}
//					}
				}
//			}
		}
	}
	
	/**
	 * @description:删除已经同步的数据
	 * @param:
	 * @author: cuis
	 * @throws SQLException 
	 * @creatTime:  2013-9-26下午01:58:41
	 * @modifyInformation：
	 */
	public void  deleteSynAlarmDatas () throws SQLException {
		PreparedStatement  oraclestmtrs = null;
  		try{
			logger.info("---------开始执行删除已同步的数据 : ");
 			Long startTime = System.currentTimeMillis();
  			if(connection==null){
  				connection = OracleConnectionPool.getConnection();
  			}
 			oraclestmtrs = connection.prepareCall(SQLPool.getinstance().getSql("sql_deleteThSynAlarmNum"));
			oraclestmtrs.executeBatch();
			if(oraclestmtrs != null){
				oraclestmtrs.close();
			}
			if(connection != null){
				connection.close();
			}
 			Long enttime = System.currentTimeMillis();
			logger.info("--------删除数据执行完成，花费时长: "+(enttime-startTime)/1000+"s.");
		}finally{
			if(oraclestmtrs != null){
				oraclestmtrs.close();
			}
			if(connection != null){
				connection.close();
			}
		}
 	}
	
	
	/******
	 * 同步前日报警数据
	 * @throws SQLException
	 * @throws TimeoutException
	 * @throws InterruptedException
	 * @throws MemcachedException
	 */
	public void synDayStatAlarm() throws SQLException, TimeoutException, InterruptedException{
		PreparedStatement  oraclestmtrs = null;
		ResultSet rs = null;
		
		if(dayCount > 0){
			try{
				oraclestmtrs = connection.prepareCall(SQLPool.getinstance().getSql("synDayStatAlarmSQL"));
				Iterator<String> orgIt = orgList.iterator();
				while(orgIt.hasNext()){
					long currentUtc = SynDateUtils.getCurrentDayYearMonthDay(); //获取当天凌晨UTC时间
					long beforeUtc = SynDateUtils.getBeforeDayUTC(currentUtc); //获取前一天凌晨UTC时间
					String orgId = orgIt.next();
					String alarmList = new String();
					boolean isContain = false;
					if(savedOrgList.contains(orgId)){
						String[] arr = entToAlarmMap.get(orgId).split("\\|",3);
						if(arr.length == 3){
							alarmList = arr[0];
							isContain = true;
						}
					}
					
					for(int i = 0;i<dayCount;i++){
						
						if(!alarmList.contains(SynDateUtils.utcToString(beforeUtc) + ">>")){ // 判断该日日否已经查询
							AlarmNum alarm = new AlarmNum();
							
							oraclestmtrs.setString(1, orgId);
							oraclestmtrs.setString(2, orgId);
							oraclestmtrs.setLong(3, beforeUtc);
							oraclestmtrs.setLong(4, currentUtc);
							oraclestmtrs.setString(5, orgId);
							oraclestmtrs.setString(6, orgId);
							rs = oraclestmtrs.executeQuery();
							
							if (rs.next()) {
								alarm.setAlarmDate(SynDateUtils.utcToString(beforeUtc));
								alarm.setGeneralCount(rs.getLong("GENERALCOUNT") + ""); // 一般报警
								alarm.setSeriousCount(rs.getLong("SERIOUSCOUNT") + ""); // 严重报警
								alarm.setSuggestionCount(rs.getLong("SUGGESTIONCOUNT") + ""); // 提醒报警
								alarm.setUrgentCount(rs.getLong("URGENTCOUNT") + ""); //紧急报警
								if(isContain){
									alarmList = combinAlarmData(alarm) + alarmList;
									//删除最前面一天
									alarmList = alarmList.substring(0,alarmList.lastIndexOf("["));
								}else{
									alarmList =  alarmList + combinAlarmData(alarm);
								}
							}	
							//logger.info("同步日报警查询组织 ID : " + orgId + ";开始时间:" + SynDateUtils.utcToString(beforeUtc) + " - 结束时间" + SynDateUtils.utcToString(currentUtc) + "; GENERALCOUNT:" + alarm.getGeneralCount() + "; SERIOUSCOUNT:" + alarm.getSeriousCount() + "; SUGGESTIONCOUNT:" + alarm.getSuggestionCount() +"; URGENTCOUNT:" + alarm.getUrgentCount() );
						}
						currentUtc = beforeUtc;
						beforeUtc = SynDateUtils.getBeforeDayUTC(currentUtc); //获取前一天凌晨UTC时间
					}// End for
					
					if(isContain){
						entToAlarmMap.put(orgId,recombinAlarmData(entToAlarmMap.get(orgId),0,alarmList));
					}else{
						entToAlarmMap.put(orgId,alarmList);
					}
					logger.info("同步日报警查询组织 ID : " + orgId + ";data:" +  entToAlarmMap.get(orgId));
					logger.info("=====================");
				}// End while
			}finally{
				if(rs != null){
					rs.close();
				}
				
				if(oraclestmtrs != null){
					oraclestmtrs.close();
				}
			}
		}
	}
	
	/*******
	 * 组合数据
	 * @param num
	 * @return
	 */
	private String combinAlarmData(AlarmNum alarm){
		StringBuffer buf = new StringBuffer("");
		buf.append("[");
		buf.append(alarm.getAlarmDate());
		buf.append(">>");
		buf.append(alarm.getGeneralCount());
		buf.append(":");
		buf.append(alarm.getSeriousCount());
		buf.append(":");
		buf.append(alarm.getSuggestionCount());
		buf.append(":");
		buf.append(alarm.getUrgentCount());
		buf.append("]");
		return buf.toString();
	}
	
	private String recombinAlarmData(String main,int idx,String str){
		String[] spStr = main.split("\\|",3);
		StringBuffer buf = new StringBuffer();
		if(idx == 0){
			buf.append(str);
			buf.append("|");
			buf.append(spStr[1]);
			buf.append("|");
			buf.append(spStr[2]);
		}else if(idx == 1){
			buf.append(spStr[0]);
			buf.append("|");
			buf.append(str);
			buf.append("|");
			buf.append(spStr[2]);
		}else if(idx == 2){
			buf.append(spStr[0]);
			buf.append("|");
			buf.append(spStr[1]);
			buf.append("|");
			buf.append(str);
		}
		return buf.toString();
	}
	
	/******
	 * 同步前周报警数据
	 * @throws SQLException
	 * @throws TimeoutException
	 * @throws InterruptedException
	 * @throws MemcachedException
	 */
	public void synWeekStatAlarm() throws SQLException, TimeoutException, InterruptedException{
		PreparedStatement  oraclestmtrs = null;
		ResultSet rs = null;
		if(weekCount > 0){
			try{
				
				oraclestmtrs = connection.prepareCall(SQLPool.getinstance().getSql("synDayStatAlarmSQL"));
				Iterator<String> orgIt = orgList.iterator();
				while(orgIt.hasNext()){
					long currentUtc = SynDateUtils.getWeekUtc();
					long beforeUtc = SynDateUtils.getPreviousWeekUtc(SynDateUtils.getWeekUtc());
					String orgId = orgIt.next();
					String alarmList = new String();
					boolean isContain = false;
					if(savedOrgList.contains(orgId)){
						String[] arr = entToAlarmMap.get(orgId).split("\\|",3);
						if(arr.length == 3){
							alarmList = arr[1];
							isContain = true;
						}
					}
					for(int i = 0;i < weekCount; i++){
						int week = SynDateUtils.getPreviousWeek(beforeUtc);
						if(!alarmList.contains("\\["+ week + ">>")){
							AlarmNum alarm = new AlarmNum();
							
							oraclestmtrs.setString(1, orgId);
							oraclestmtrs.setString(2, orgId);
							oraclestmtrs.setLong(3, beforeUtc);
							oraclestmtrs.setLong(4, currentUtc);
							oraclestmtrs.setString(5, orgId);
							oraclestmtrs.setString(6, orgId);
							rs = oraclestmtrs.executeQuery();
							
							if (rs.next()) {
								alarm.setAlarmDate(SynDateUtils.getPreviousWeek(beforeUtc) + "");
								alarm.setGeneralCount(rs.getLong("GENERALCOUNT") + ""); // 一般报警
								alarm.setSeriousCount(rs.getLong("SERIOUSCOUNT") + ""); // 严重报警
								alarm.setSuggestionCount(rs.getLong("SUGGESTIONCOUNT") + ""); // 提醒报警
								alarm.setUrgentCount(rs.getLong("URGENTCOUNT") + ""); //紧急报警
								if(isContain){
									alarmList = combinAlarmData(alarm) + alarmList; //新添加一周
									//删除最前面一周
									alarmList = alarmList.substring(0,alarmList.lastIndexOf("["));
								}else{
									alarmList =  alarmList + combinAlarmData(alarm);
								}
							}	
							//logger.info("同步周报警查询组织 ID : " + orgId + ";第" + SynDateUtils.getPreviousWeek(beforeUtc)+ "周；开始时间:" + SynDateUtils.utcToString(beforeUtc) + " - 结束时间" + SynDateUtils.utcToString(currentUtc) + "; GENERALCOUNT:" + alarm.getGeneralCount() + "; SERIOUSCOUNT:" + alarm.getSeriousCount() + "; SUGGESTIONCOUNT:" + alarm.getSuggestionCount() +"; URGENTCOUNT:" + alarm.getUrgentCount() );
						}
						currentUtc = beforeUtc;
						beforeUtc = SynDateUtils.getPreviousWeekUtc(currentUtc);
					}// End for
					
					logger.info("=====================");
					
					if(isContain){
						entToAlarmMap.put(orgId,recombinAlarmData(entToAlarmMap.get(orgId),1,alarmList));
					}else{
						entToAlarmMap.put(orgId,entToAlarmMap.get(orgId) + "|" + alarmList);
					}
					logger.info("同步周报警查询组织 ID : " + orgId + ";" + entToAlarmMap.get(orgId));
				}// End while
			}finally{
				if(rs != null){
					rs.close();
				}
				
				if(oraclestmtrs != null){
					oraclestmtrs.close();
				}
			}
		}
	}

	/******
	 * 同步前月报警数据
	 * @throws SQLException
	 * @throws TimeoutException
	 * @throws InterruptedException
	 * @throws MemcachedException
	 */
	public void synMonthStatAlarm() throws SQLException, TimeoutException, InterruptedException{
		PreparedStatement  oraclestmtrs = null;
		ResultSet rs = null;
		if(monthCount > 0){
			try{
				oraclestmtrs = connection.prepareCall(SQLPool.getinstance().getSql("synMonthStatAlarmSQL"));
				Iterator<String> orgIt = orgList.iterator();
				while(orgIt.hasNext()){
					long lastMonthDayUtc = SynDateUtils.getPreviousMonth(System.currentTimeMillis());
					String orgId = orgIt.next();
					String alarmList = new String();
					boolean isContain = false;
					if(savedOrgList.contains(orgId)){
						String[] arr = entToAlarmMap.get(orgId).split("\\|",3);
						if(arr.length == 3){
							alarmList = arr[2];
							isContain = true;
						}
					}
					
					for(int i = 0;i < monthCount; i++){
						AlarmNum alarm = new AlarmNum();
						if(!alarmList.contains(SynDateUtils.getYear(lastMonthDayUtc) + "-" + SynDateUtils.getMonth(lastMonthDayUtc) + ">>")){
							oraclestmtrs.setString(1, orgId);
							oraclestmtrs.setString(2, orgId);
							oraclestmtrs.setLong(3, SynDateUtils.getMonth(lastMonthDayUtc));
							oraclestmtrs.setLong(4, SynDateUtils.getYear(lastMonthDayUtc));
							oraclestmtrs.setString(5, orgId);
							oraclestmtrs.setString(6, orgId);
							rs = oraclestmtrs.executeQuery();
							
							if (rs.next()) {
								alarm.setAlarmDate(SynDateUtils.getYear(lastMonthDayUtc) + "-" + SynDateUtils.getMonth(lastMonthDayUtc));
								alarm.setGeneralCount(rs.getLong("GENERALCOUNT") + ""); // 一般报警
								alarm.setSeriousCount(rs.getLong("SERIOUSCOUNT") + ""); // 严重报警
								alarm.setSuggestionCount(rs.getLong("SUGGESTIONCOUNT") + ""); // 提醒报警
								alarm.setUrgentCount(rs.getLong("URGENTCOUNT") + ""); //紧急报警
								if(isContain){
									alarmList =   combinAlarmData(alarm) + alarmList; // 添加一月
									//删除最前面一月
									alarmList = alarmList.substring(0,alarmList.lastIndexOf("["));
								}else{
									alarmList =  alarmList + combinAlarmData(alarm);
								}
							}	
							//logger.info("同步月报警查询组织 ID : " + orgId + ";日期:" + SynDateUtils.getYear(lastMonthDayUtc) + "-" + SynDateUtils.getMonth(lastMonthDayUtc) + "; GENERALCOUNT:" + alarm.getGeneralCount() + "; SERIOUSCOUNT:" + alarm.getSeriousCount() + "; SUGGESTIONCOUNT:" + alarm.getSuggestionCount() +"; URGENTCOUNT:" + alarm.getUrgentCount() );
						}
						lastMonthDayUtc = SynDateUtils.getPreviousMonth(lastMonthDayUtc);
					}// End for
					
					logger.info("=====================");
					if(isContain){
						entToAlarmMap.put(orgId,recombinAlarmData(entToAlarmMap.get(orgId),2,alarmList));
					}else{
						entToAlarmMap.put(orgId,entToAlarmMap.get(orgId) + "|" + alarmList);
					}
					logger.info("同步月报警查询组织 ID : " + orgId + ";" + entToAlarmMap.get(orgId));
				}// End while
			}finally{
				if(rs != null){
					rs.close();
				}
				
				if(oraclestmtrs != null){
					oraclestmtrs.close();
				}
			}
		}
	}
	
	/*****
	 * 查询组织信息
	 * @throws SQLException
	 */
	public void searchOrgId() throws SQLException{
		PreparedStatement  oraclestmtrs = null;
		ResultSet rs = null;
		try{
		oraclestmtrs = connection.prepareCall(SQLPool.getinstance().getSql("sql_selectOPerator"));
		rs = oraclestmtrs.executeQuery();
		
		while (rs.next()) {
			orgList.add(rs.getString("ENT_ID"));
		}// End while
		logger.info("组织信息总数:" + orgList.size());
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(oraclestmtrs != null){
				oraclestmtrs.close();
			}
		}
	}
	
	/*****
	 * 查询报警同步表
	 * @throws SQLException 
	 */
	@SuppressWarnings("unused")
	private void selectSynAlarmNum2() throws SQLException{
		PreparedStatement  oraclestmtrs = null;
		PreparedStatement  daystmt = null;
		PreparedStatement  weekstmt = null;
		PreparedStatement  monthstmt = null;
		ResultSet rs = null;
		try{
		oraclestmtrs = connection.prepareStatement(SQLPool.getinstance().getSql("selectSynalarmNum"));
		rs = oraclestmtrs.executeQuery();
		
		while (rs.next()) {
			String orgId = rs.getString("ENT_ID");
			String alarmList = rs.getString("ALARM_LIST");
			String alarmTend[] = alarmList.split("\\|");
			//解析日数据
			 Pattern p = Pattern.compile("\\d{4}-\\d+-\\d+>>\\d+:\\d+:\\d+:\\d+");
			 Matcher m = p.matcher(alarmTend[0]);
			 boolean result = m.find();   
			 System.out.println("该次查找获得匹配组的数量为："+m.groupCount());   
			 while (result) {
				 String dayStr = m.group(0);
			      System.out.println(dayStr);
			      
			      String dateStr = dayStr.split(">>")[0];
			      String dataStr = dayStr.split(">>")[1];
			      
			      String alarmData[] = dataStr.split(":");
			      
			      long utc = CDate.changeDateFormat("yyyy-M-d HH", dateStr+" 12").getTime();
			      
			      String daySql = "insert into ts_ent_alarm_daystat"+
			      " (stat_date,ent_id,GENERALCOUNT,SERIOUSCOUNT,SUGGESTIONCOUNT,URGENTCOUNT)"
			      + "values (?,?,?,?,?,?)";
			      
			      daystmt = connection.prepareStatement(daySql);
			      daystmt.setLong(1, utc);
			      daystmt.setString(2, orgId);
			      daystmt.setString(3, alarmData[0]);
			      daystmt.setString(4, alarmData[1]);
			      daystmt.setString(5, alarmData[2]);
			      daystmt.setString(6, alarmData[3]);
			      daystmt.executeUpdate();
			      
			      
			      //System.out.println(m.group(2));  
			      result = m.find();   
			 }  

			
			//解析周数据
			 Pattern p2 = Pattern.compile("\\d+>>\\d+:\\d+:\\d+:\\d+");
			 Matcher m2 = p2.matcher(alarmTend[1]);
			 boolean result2 = m2.find();   
			 System.out.println("该次查找获得匹配组的数量为："+m2.groupCount());   
			 while (result2) {
				 String dayStr = m2.group(0);
			      System.out.println(dayStr);
			      
			      String dateStr = dayStr.split(">>")[0];
			      String dataStr = dayStr.split(">>")[1];
			      
			      String alarmData[] = dataStr.split(":");
			      int year = 2014;
			      
			      if (Integer.parseInt(dateStr)>=20){
			    	  year = 2013;
			      }
			      
			      long utc = CDate.getFirstDayOfWeek(year, Integer.parseInt(dateStr)).getTime() + 12*60*60*1000;
			      
			      String weekSql = "insert into ts_ent_alarm_weekstat "+
			      " (STAT_DATE,STAT_YEAR,STAT_WEEK,ENT_ID,GENERALCOUNT,SERIOUSCOUNT,SUGGESTIONCOUNT,URGENTCOUNT)"
			      + "values (?,?,?,?,?,?,?,?)";
			      
			      weekstmt = connection.prepareStatement(weekSql);
			      weekstmt.setLong(1, utc);
			      weekstmt.setString(2, ""+year);
			      weekstmt.setString(3, dateStr);
			      weekstmt.setString(4, orgId);
			      weekstmt.setString(5, alarmData[0]);
			      weekstmt.setString(6, alarmData[1]);
			      weekstmt.setString(7, alarmData[2]);
			      weekstmt.setString(8, alarmData[3]);
			      weekstmt.executeUpdate();
			      
			      
			      //System.out.println(m.group(2));  
			      result2 = m2.find();   
			 }  
			//解析月数据
			 Pattern p3 = Pattern.compile("\\d{4}-\\d+>>\\d+:\\d+:\\d+:\\d+");
			 Matcher m3 = p.matcher(alarmTend[0]);
			 boolean result3 = m3.find();   
			 System.out.println("该次查找获得匹配组的数量为："+m3.groupCount());   
			 while (result3) {
				 String dayStr = m3.group(0);
			      System.out.println(dayStr);
			      
			      String dateStr = dayStr.split(">>")[0];
			      String dataStr = dayStr.split(">>")[1];
			      
			      String alarmData[] = dataStr.split(":");
			      
			      String year = dateStr.split("-")[0];
			      String month = dateStr.split("-")[1];
			      
			      long utc = CDate.getFirstDayOfMonth(Integer.parseInt(year), Integer.parseInt(month)).getTime()+12*60*60*1000;
			      
			      String monthSql = "insert into TS_ENT_ALARM_MONTHSTAT"+
			      " (stat_date,STAT_YEAR,STAT_MONTH,ent_id,GENERALCOUNT,SERIOUSCOUNT,SUGGESTIONCOUNT,URGENTCOUNT)"
			      + "values (?,?,?,?,?,?,?,?)";
			      
			      monthstmt = connection.prepareStatement(monthSql);
			      monthstmt.setLong(1, utc);
			      monthstmt.setString(2, year);
			      monthstmt.setString(3, month);
			      monthstmt.setString(4, orgId);
			      monthstmt.setString(5, alarmData[0]);
			      monthstmt.setString(6, alarmData[1]);
			      monthstmt.setString(7, alarmData[2]);
			      monthstmt.setString(8, alarmData[3]);
			      monthstmt.executeUpdate();
			      
			      //System.out.println(m.group(2));  
			      result3 = m3.find();   
			 }  
			 
			/*entToAlarmMap.put(orgId,rs.getString("ALARM_LIST"));
			savedOrgList.add(orgId);*/
		}// End while
		logger.info("已同步过组织信息总数:" + entToAlarmMap.size());
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(oraclestmtrs != null){
				oraclestmtrs.close();
			}
		}
		
	}
	
	/*****
	 * 查询报警同步表
	 * @throws SQLException 
	 */
	@SuppressWarnings("unused")
	public void selectSynAlarmNum() throws SQLException{
		PreparedStatement  oraclestmtrs = null;
		ResultSet rs = null;
		try{
		oraclestmtrs = connection.prepareStatement(SQLPool.getinstance().getSql("selectSynalarmNum"));
		rs = oraclestmtrs.executeQuery();
		
		while (rs.next()) {
			String orgId = rs.getString("ENT_ID");
			String alarmList = rs.getString("ALARM_LIST");
			entToAlarmMap.put(orgId,rs.getString("ALARM_LIST"));
			savedOrgList.add(orgId);
		}// End while
		logger.info("已同步过组织信息总数:" + entToAlarmMap.size());
		}finally{
			if(rs != null){
				rs.close();
			}
			
			if(oraclestmtrs != null){
				oraclestmtrs.close();
			}
		}
		
	}
	
	/******
	 * 存储报警
	 * @throws SQLException
	 */
	public void saveAlarmList() throws SQLException{
		PreparedStatement savePs = null;
		PreparedStatement updatePs = null;
		try{
			updatePs = connection.prepareStatement(SQLPool.getinstance().getSql("updateSynalarmNum"));
			savePs = connection.prepareStatement(SQLPool.getinstance().getSql("insertSynalarmNum"));
			
			Set<String> orgSet = entToAlarmMap.keySet();
			Iterator<String> orgIt = orgSet.iterator();
			while(orgIt.hasNext()){
				String orgId = orgIt.next();
				if(savedOrgList.contains(orgId)){
					updateAlarmList(updatePs,orgId,entToAlarmMap.get(orgId));
				}else{
					saveAlarmList(savePs,orgId,entToAlarmMap.get(orgId));
				}
			}// End while
			updatePs.executeBatch();
			savePs.executeBatch();
		}finally{
			if(updatePs != null){
				updatePs.close();
			}
			
			if(savePs != null){
				savePs.close();
			}
		}
	}
	
	/*****
	 * 更新已有的用户对应企业报警列表
	 * @param updatePs
	 * @param orgId
	 * @param value
	 * @throws SQLException
	 */
	private void updateAlarmList(PreparedStatement updatePs,String orgId,String value ) throws SQLException{
		
		updatePs.setString(1, value);
		updatePs.setString(2, orgId);
		updatePs.addBatch();
	}
	
	/******
	 * 存储新的用户对应企业报警列表
	 * @param savePs
	 * @param orgId
	 * @param value
	 * @throws SQLException
	 */
	private void saveAlarmList(PreparedStatement savePs,String orgId,String value ) throws SQLException{
		savePs.setString(1, orgId);
		savePs.setString(2, value);
		savePs.addBatch();
	}
	
	/*****
	 * 断开数据库和MEMECACHE连接
	 * @throws SQLException
	 * @throws IOException
	 */
	public void closeConnection() throws SQLException, IOException{
		if(connection != null){
			connection.close();
		}
		orgList.clear();
		entToAlarmMap.clear();
		savedOrgList.clear();
	}
}
