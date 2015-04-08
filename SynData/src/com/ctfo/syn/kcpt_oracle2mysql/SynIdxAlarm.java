package com.ctfo.syn.kcpt_oracle2mysql;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
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
import java.util.Properties;
import java.util.TimerTask;
import java.util.concurrent.TimeoutException;

import org.apache.log4j.Logger;

import com.ctfo.memcache.beans.AlarmNum;
import com.ctfo.redis.util.RedisJsonUtil;
import com.ctfo.syn.dao.RedisServer;
import com.ctfo.syn.database.OracleConnectionPool;
import com.ctfo.syn.kcpt.utils.CDate;
import com.ctfo.syn.membeans.TbOrganization;
import com.ctfo.unifiedstorage.service.JedisUnifiedStorageService;


public class SynIdxAlarm extends TimerTask{
	public static Logger logger = Logger.getLogger(SynIdxAlarm.class);
//	private final String ALARM_DAY_KEY = "mAlarmInfoDay";
//	private final String ALARM_WEEK_KEY = "mAlarmInfoWeek";
//	private final String ALARM_MONTH_KE = "mAlarmInfoMonth";
	private Properties property = null;
	private FileInputStream in = null;
//	private String memcacheMainAddr = null;	
	private String orgSql = null;
	private String synDayStatAlarmSQL = null;
	private String synMonthStatAlarmSQL = null;
    private Connection connection = null;
    private List<TbOrganization> orgList = new ArrayList<TbOrganization>();
//    private MemcachedClientBuilder builder = null;
//	private MemcachedClient mcc = null;
	private JedisUnifiedStorageService redis = null;
	//同步日天数
	private int dayCount=7;
	//同步周数
	private int weekCount=7;
	//同步月数
	private int monthCount=6;
	
	public SynIdxAlarm(String arg) {
		loadProperties(arg);
		
	}
	
	@Override
	public void run(){
		try {
			long dayCostTime = 0;
			long weekCostTime = 0;
			long monthCostTime = 0;
			long startTime = System.currentTimeMillis();
			initInfo();
			searchOrgId();
			synDayStatAlarm();
			dayCostTime = (System.currentTimeMillis() - startTime);
			
			startTime = System.currentTimeMillis();
			synWeekStatAlarm();
			weekCostTime =  (System.currentTimeMillis() - startTime);
			
			startTime = System.currentTimeMillis();
			synMonthStatAlarm();
			monthCostTime =  (System.currentTimeMillis() - startTime);
			
			closeConnection();
			logger.info("Select day of alarm cost time : " + dayCostTime + ";" + "Select week of alarm cost time : " + weekCostTime +";Select month of alarm cost time : " + monthCostTime );
		} catch (Exception e) {
			logger.error("同步报警数据出错",e);
		}
	}
	
	/******
	 * 初始化加载PROPERTIES
	 * @param path
	 */
	private void loadProperties(String path){
		try {
			in = new FileInputStream(path);
			property = new Properties();			
			try {
				property.load(in);
//				memcacheMainAddr = property.getProperty("memcacheMainAddr");	
				orgSql = property.getProperty("oracle_memcache_org");				
				synDayStatAlarmSQL = property.getProperty("synDayStatAlarmSQL");
				synMonthStatAlarmSQL = property.getProperty("synMonthStatAlarmSQL");
				//同步日天数
				dayCount = Integer.parseInt(property.getProperty("dayCount"));
				//同步周数
				weekCount = Integer.parseInt(property.getProperty("weekCount"));
				//同步月数
				monthCount = Integer.parseInt(property.getProperty("monthCount"));
			} catch (IOException e) {
				logger.error("同步首页报警线程加载perperites出错:" +path ,e);
			} 
		} catch (FileNotFoundException e) {
			logger.error("同步首页报警线程出错,",e);
			property = null;
			in = null;
		} finally {
			property = null;
			if(in != null) {
				try {
					in.close();
				} catch (Exception e) {
					logger.error(e);
				}			
			}
		}
	}
	
	/***
	 * 初始化连接数据库、MEMECACHE
	 */
	private void initInfo(){
		try {
			redis = RedisServer.getJedisServiceInstance();
			connection = OracleConnectionPool.getConnection();
//			builder = new XMemcachedClientBuilder(AddrUtil.getAddresses(memcacheMainAddr));
			//builder.setSocketOption(StandardSocketOption.SO_SNDBUF, 20 * 1024); // 设置发送缓冲区为16K，默认为8K
	        //builder.setSocketOption(StandardSocketOption.TCP_NODELAY, false); // 启用nagle算法，提高吞吐量，默认关闭
//			try {
////				mcc = builder.build();
////				mcc.setConnectTimeout(10 * 1000);
////				mcc.setOpTimeout(10 * 1000);		
////				mcc.setEnableHeartBeat(false);
//				//mcc.setConnectionPoolSize(2);
//			} catch (IOException e) {
//				logger.error("连接memcache出错", e);
//			}
		} catch (SQLException e) {
			logger.error("同步首页报警线程获取连接出错",e);
		}
	}
	
	/******
	 * 同步前日报警数据
	 * @throws SQLException
	 * @throws TimeoutException
	 * @throws InterruptedException
	 * @throws MemcachedException
	 */
	private void synDayStatAlarm() throws SQLException, TimeoutException, InterruptedException, Exception{
		PreparedStatement  oraclestmtrs = null;
		ResultSet rs = null;
		
		if(dayCount > 0){
			try{
				Map<String,List<AlarmNum>> dayMap = new HashMap<String,List<AlarmNum>>();
				oraclestmtrs = connection.prepareCall(synDayStatAlarmSQL);
				Iterator<TbOrganization> orgIt = orgList.iterator();
				while(orgIt.hasNext()){
					long currentUtc = CDate.getCurrentDayYearMonthDay(); //获取当天凌晨UTC时间
					long beforeUtc = CDate.getBeforeDayUTC(currentUtc); //获取前一天凌晨UTC时间
					TbOrganization org = orgIt.next();
					
					List<AlarmNum> list = new ArrayList<AlarmNum>();
					for(int i = 0;i<dayCount;i++){
						AlarmNum alarm = new AlarmNum();
					
						oraclestmtrs.setLong(1, beforeUtc);
						oraclestmtrs.setLong(2, currentUtc);
						oraclestmtrs.setLong(3, org.getEntId());
						oraclestmtrs.setLong(4, org.getEntId());
						oraclestmtrs.setLong(5, org.getEntId());
						oraclestmtrs.setLong(6, org.getEntId());
						rs = oraclestmtrs.executeQuery();
						
						if (rs.next()) {
							alarm.setAlarmDate(CDate.utcToString(beforeUtc));
							alarm.setGeneralCount(rs.getLong("GENERALCOUNT") + "");
							alarm.setSeriousCount(rs.getLong("SERIOUSCOUNT") + "");
							alarm.setSuggestionCount(rs.getLong("SUGGESTIONCOUNT") + "");
							alarm.setUrgentCount(rs.getLong("URGENTCOUNT") + "");
						}	
						logger.info("同步日报警查询组织 ID : " + org.getEntId() + ";开始时间:" + CDate.utcToString(beforeUtc) + " - 结束时间" + CDate.utcToString(currentUtc) + "; GENERALCOUNT:" + alarm.getGeneralCount() + "; SERIOUSCOUNT:" + alarm.getSeriousCount() + "; SUGGESTIONCOUNT:" + alarm.getSuggestionCount() +"; URGENTCOUNT:" + alarm.getUrgentCount() );
						list.add(alarm);
						currentUtc = beforeUtc;
						beforeUtc = CDate.getBeforeDayUTC(currentUtc); //获取前一天凌晨UTC时间
					}// End for
					logger.info("=====================");
					dayMap.put(org.getEntId()+ "", list);
				}// End while
				redis.saveDayAlarm(RedisJsonUtil.objectToJson(dayMap));
//				mcc.set(ALARM_DAY_KEY,0, dayMap);
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
	 * 同步前周报警数据
	 * @throws SQLException
	 * @throws TimeoutException
	 * @throws InterruptedException
	 * @throws MemcachedException
	 */
	private void synWeekStatAlarm() throws SQLException, TimeoutException, InterruptedException, Exception{
		PreparedStatement  oraclestmtrs = null;
		ResultSet rs = null;
		if(weekCount > 0){
			try{
				Map<String,List<AlarmNum>> dayMap = new HashMap<String,List<AlarmNum>>();
				oraclestmtrs = connection.prepareCall(synDayStatAlarmSQL);
				Iterator<TbOrganization> orgIt = orgList.iterator();
				while(orgIt.hasNext()){
					long currentUtc = CDate.getWeekUtc();
					long beforeUtc = CDate.getPreviousWeekUtc(CDate.getWeekUtc());
					TbOrganization org = orgIt.next();
					
					List<AlarmNum> list = new ArrayList<AlarmNum>();
					for(int i = 0;i < weekCount; i++){
						AlarmNum alarm = new AlarmNum();
						oraclestmtrs.setLong(1, beforeUtc);
						oraclestmtrs.setLong(2, currentUtc);
						oraclestmtrs.setLong(3, org.getEntId());
						oraclestmtrs.setLong(4, org.getEntId());
						oraclestmtrs.setLong(5, org.getEntId());
						oraclestmtrs.setLong(6, org.getEntId());
						rs = oraclestmtrs.executeQuery();
						
						if (rs.next()) {
							alarm.setAlarmDate(CDate.getPreviousWeek(beforeUtc) + "");
							alarm.setGeneralCount(rs.getLong("GENERALCOUNT") + "");
							alarm.setSeriousCount(rs.getLong("SERIOUSCOUNT") + "");
							alarm.setSuggestionCount(rs.getLong("SUGGESTIONCOUNT") + "");
							alarm.setUrgentCount(rs.getLong("URGENTCOUNT") + "");
						}	
						logger.info("同步周报警查询组织 ID : " + org.getEntId() + ";第" + CDate.getPreviousWeek(beforeUtc)+ "周；开始时间:" + CDate.utcToString(beforeUtc) + " - 结束时间" + CDate.utcToString(currentUtc) + "; GENERALCOUNT:" + alarm.getGeneralCount() + "; SERIOUSCOUNT:" + alarm.getSeriousCount() + "; SUGGESTIONCOUNT:" + alarm.getSuggestionCount() +"; URGENTCOUNT:" + alarm.getUrgentCount() );
						list.add(alarm);
						currentUtc = beforeUtc;
						beforeUtc = CDate.getPreviousWeekUtc(currentUtc);
					}// End for
					logger.info("=====================");
					dayMap.put(org.getEntId()+ "", list);
				}// End while
				redis.saveWeekAlarm(RedisJsonUtil.objectToJson(dayMap));
//				mcc.set(ALARM_WEEK_KEY,0, dayMap);
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
	private void synMonthStatAlarm() throws SQLException, TimeoutException, InterruptedException, Exception{
		PreparedStatement  oraclestmtrs = null;
		ResultSet rs = null;
		if(monthCount > 0){
			try{
			Map<String,List<AlarmNum>> dayMap = new HashMap<String,List<AlarmNum>>();
			oraclestmtrs = connection.prepareCall(synMonthStatAlarmSQL);
			Iterator<TbOrganization> orgIt = orgList.iterator();
			while(orgIt.hasNext()){
				long lastMonthDayUtc = CDate.getPreviousMonth(System.currentTimeMillis());
				TbOrganization org = orgIt.next();
				
				List<AlarmNum> list = new ArrayList<AlarmNum>();
				for(int i = 0;i < monthCount; i++){
					AlarmNum alarm = new AlarmNum();
				
					oraclestmtrs.setLong(1, CDate.getMonth(lastMonthDayUtc));
					oraclestmtrs.setLong(2, CDate.getYear(lastMonthDayUtc));
					oraclestmtrs.setLong(3, org.getEntId());
					oraclestmtrs.setLong(4, org.getEntId());
					oraclestmtrs.setLong(5, org.getEntId());
					oraclestmtrs.setLong(6, org.getEntId());
					rs = oraclestmtrs.executeQuery();
					
					if (rs.next()) {
						alarm.setAlarmDate(CDate.getYear(lastMonthDayUtc) + "-" + CDate.getMonth(lastMonthDayUtc));
						alarm.setGeneralCount(rs.getLong("GENERALCOUNT") + "");
						alarm.setSeriousCount(rs.getLong("SERIOUSCOUNT") + "");
						alarm.setSuggestionCount(rs.getLong("SUGGESTIONCOUNT") + "");
						alarm.setUrgentCount(rs.getLong("URGENTCOUNT") + "");
					}	
					logger.info("同步月报警查询组织 ID : " + org.getEntId() + ";日期:" + CDate.getYear(lastMonthDayUtc) + "-" + CDate.getMonth(lastMonthDayUtc) + "; GENERALCOUNT:" + alarm.getGeneralCount() + "; SERIOUSCOUNT:" + alarm.getSeriousCount() + "; SUGGESTIONCOUNT:" + alarm.getSuggestionCount() +"; URGENTCOUNT:" + alarm.getUrgentCount() );
					list.add(alarm);
					lastMonthDayUtc = CDate.getPreviousMonth(lastMonthDayUtc);
				}// End for
				logger.info("=====================");
				dayMap.put(org.getEntId()+ "", list);
			}// End while
			redis.saveMonthAlarm(RedisJsonUtil.objectToJson(dayMap));
//			mcc.set(ALARM_MONTH_KE,0, dayMap);
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
	private void searchOrgId() throws SQLException{
		PreparedStatement  oraclestmtrs = null;
		ResultSet rs = null;
		try{
		oraclestmtrs = connection.prepareCall(orgSql);
		rs = oraclestmtrs.executeQuery();
		
		while (rs.next()) {
			TbOrganization tbOrganization = new TbOrganization();
			tbOrganization.setEntId(rs.getLong("ENT_ID"));
			orgList.add(tbOrganization);
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
	 * 断开数据库和MEMECACHE连接
	 * @throws SQLException
	 * @throws IOException
	 */
	private void closeConnection() throws SQLException, IOException{
		if(connection != null){
			connection.close();
		}
//		if(mcc != null){
//			mcc.shutdown();
//		}
		orgList.clear();
	}
}
