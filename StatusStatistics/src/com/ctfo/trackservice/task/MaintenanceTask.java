package com.ctfo.trackservice.task;

import java.math.BigDecimal;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.generator.pk.GeneratorPK;
import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.model.MainTainStatistic;
import com.ctfo.trackservice.util.ConfigLoader;
import com.ctfo.trackservice.util.DateTools;

/**
 * 文件名：MaintenanceTask.java
 * 功能：智能维保
 *
 * @author huangjincheng
 * 2014-12-29下午1:47:54
 * 
 */
public class MaintenanceTask extends TaskAdapter{
	private static final Logger logger = LoggerFactory.getLogger(MaintenanceTask.class);
	private Connection dbConnection;
	// 查询MainTain计划表
//	private String queryMainTainPlanSql;
	// 查询MainTain明细表
	private String queryMainTainDetailSql;
	// 更新MainTain明细表 MILEAGE_MESS字段 TIME_MESS字段
	private String updateMainTainDetailSql;
	// 生成一条记录 插入MainTain明细表
	private String createNewMainTainDataSql;
	
	//更新MainTain明细表MAINTAIN_ONTIME_STAT字段
	private String updateMainTainDetailStatSql;
	
	private String queryVehicleMaintainTimesSql;
	
	private String queryVehicleMaintainNumSql;
	
	private String queryMaintainClassSql;
	
	private String queryExtendMaintainDetailSql;
	
	private String saveMaintainDetailSql;
	@Override
	public void init() {
	
		// 查询维保计划表
//		queryMainTainPlanSql = ConfigLoader.config.get("sql_queryMainTainPlanSql");
		// 查询维保明细表
		queryMainTainDetailSql = ConfigLoader.config.get("sql_queryMainTainDetailSql");
		
		// 更新MainTain明细表 MILEAGE_MESS字段 TIME_MESS字段
		updateMainTainDetailSql = ConfigLoader.config.get("sql_updateMainTainDetailSql");
		
		// 生成一条记录 插入MainTain明细表
		createNewMainTainDataSql = ConfigLoader.config.get("sql_createNewMainTainDataSql");
		
		//更新MainTain明细表MAINTAIN_ONTIME_STAT字段
		updateMainTainDetailStatSql = ConfigLoader.config.get("sql_updateMainTainDetailStatSql");
		
		//查询车辆已维保次数
		queryVehicleMaintainTimesSql = ConfigLoader.config.get("sql_queryVehicleMaintainTimesSql");
		
		//查询车辆最新维护项目编号
		queryVehicleMaintainNumSql = ConfigLoader.config.get("sql_queryVehicleMaintainNumSql");
		
		//查询维保项目简称
		queryMaintainClassSql = ConfigLoader.config.get("sql_queryMaintainClassSql");

		//查询要扩展的维保明细信息
		queryExtendMaintainDetailSql = ConfigLoader.config.get("sql_queryExtendMaintainDetailSql");
		
		//保存维保明细数据
		saveMaintainDetailSql = ConfigLoader.config.get("sql_saveMaintainDetailSql");
		
	}

	@Override
	public void execute() throws Exception {
		logger.info("--------------------【智能维保】任务开始！-------------------------");
		long start = System.currentTimeMillis();
		int ret = queryMainTainDetail();	
		if(ret == 1){
			logger.info("--------------------【智能维保】任务成功！耗时：【{}】s",(System.currentTimeMillis()-start)/1000);
		}else if(ret == 0){
			logger.info("--------------------【智能维保】任务失败！耗时：【{}】s",(System.currentTimeMillis()-start)/1000);
		}
	}

	@Override
	public void isTimeRun() throws Exception {
		// TODO Auto-generated method stub
		
	}
	
	/**
	 * 查询TB_MAINTAIN_DETAIL表
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int queryMainTainDetail() {
		// 获得Connection对象
		try {
			dbConnection = OracleConnectionPool.getConnection();
		} catch (SQLException e) {
			logger.error("智能维保任务获取数据库连接异常！",e);
		}
		int flag = 0;
		PreparedStatement dbPstmt = null;
		ResultSet dbResultSet = null;
//		String mileageWarnMessage = "", timeWarnMessage = "";
		String  planCode = "", vehicleNo = "";
		String exeFrequency = "", exeTime = "", mainTainStat = "", mainTainDate = "", mainTainName = "",   maintainId = "";
		int mainTainTimes = 0, intervalDays = 0, warnDays = 0;
		String vid = "";
		String planId = "";
		
		//维保状态，新增加，用于标示是否按时维护
		String maintainOntimeStat;
		//计划维保时间
		String planMaintainDate;
		//计划维保里程
		String planMaintainMileage;
		
		double currentMileage = 0, mainTainMileage = 0, intervalMileage = 0, warnMileage = 0;
		// 信息标识
//		boolean mesFlag = false;
		try {
			// i,查询汇总数据
			dbPstmt = dbConnection.prepareStatement(queryMainTainDetailSql);			
			dbResultSet = dbPstmt.executeQuery();
			while (dbResultSet.next()) {
				MainTainStatistic mainTainStatistic = new MainTainStatistic();
				// vid
				vid = dbResultSet.getString(1);
				mainTainStatistic.setVid(vid);
				// VEHICLE_NO
				vehicleNo = dbResultSet.getString(3);
				mainTainStatistic.setVehicleNo(vehicleNo);
				// 0:循环执行 1:单次执行
				exeFrequency = dbResultSet.getString(4);
				mainTainStatistic.setExeFrequency(exeFrequency);
				// 第几维保
				mainTainTimes = dbResultSet.getInt(5);
				mainTainStatistic.setMainTainTimes(mainTainTimes);
				// 当前行驶里程
				currentMileage = dbResultSet.getDouble(6);
				mainTainStatistic.setCurrentMileage(currentMileage);
				// 维保间隔里程
				intervalMileage = dbResultSet.getDouble(7);
				mainTainStatistic.setIntervalMileage(intervalMileage);
				// 提醒里程
				warnMileage = dbResultSet.getDouble(8);
				mainTainStatistic.setWarnMileage(warnMileage);
				// 维保间隔天数
				intervalDays = dbResultSet.getInt(9);
				mainTainStatistic.setIntervalDays(intervalDays);
				// 提醒天数
				warnDays = dbResultSet.getInt(10);
				mainTainStatistic.setWarnDays(warnDays);
				// 执行具体时间
				exeTime = dbResultSet.getString(11);
				mainTainStatistic.setExeTime(exeTime);
				// 本次维护结束时间
				mainTainDate = dbResultSet.getString(12);
				mainTainStatistic.setMainTainDate(mainTainDate);
				// 本次维护结束时里程
				mainTainMileage = dbResultSet.getDouble(13);
				mainTainStatistic.setMainTainMileage(mainTainMileage);
				//所属类别ID
				maintainId = dbResultSet.getString(14);
				mainTainStatistic.setMaintainId(maintainId);
				// planId
				planId = dbResultSet.getString(15);
				mainTainStatistic.setPlanId(planId);
				// 维保计划编号
				planCode = dbResultSet.getString(16);
				mainTainStatistic.setPlanCode(planCode);
				// 维护状态 (0:未维护 1:已维护)
				mainTainStat = dbResultSet.getString(18);
				mainTainStatistic.setMainTainStat(mainTainStat);
				// 维保类别名称
				mainTainName = dbResultSet.getString(19);
				mainTainStatistic.setMainTainName(mainTainName);
				
				//维保状态，新增加，用于标示是否按时维护
				maintainOntimeStat = dbResultSet.getString(20);
				mainTainStatistic.setMaintainOntimeStat(maintainOntimeStat);
				//计划维保时间
				planMaintainDate = dbResultSet.getString(21);
				mainTainStatistic.setPlanMaintainDate(planMaintainDate);
				//计划维保里程
				planMaintainMileage = dbResultSet.getString(22);
				mainTainStatistic.setPlanMaintainMileage(planMaintainMileage);
				
				// 目的 ：生成提醒记录
				// 0:记录未维护,则根据条件 计算 提醒相应信息 更新TB_MAINTAIN_DETAIL WARN_MESS
				if (mainTainStat.equals("0")) {
					//获取提醒信息
					this.getWarnMessage(mainTainStatistic);
					updateMainTainDetail(mainTainStatistic);
				}
				// 目的 ：生成新一条维保计划
				// 1:记录已维护,
				// 根据维保号(planId),车辆编号(vid),第几维保(MAINTAIN_TIMES),查询下一条记录是否生成
				// 如果未生成,则生成下一条记录,插入到数据库 TB_MAINTAIN_DETAIL 中,
				// 如果已经生成，则不再生成下一条记录
				else {
					logger.debug("-----------【循环执行】 记录 已 维护,生成下一条维保计划！-------- ");
					logger.debug("第 " + mainTainTimes + " 次维保");
					logger.debug("vid:" + vid + " vehicleNo:"+ vehicleNo+" exeFrequency:"+exeFrequency+" mainTainTimes:"+mainTainTimes);
					createNewMainTainData(mainTainStatistic);
				}
			}
			flag = 1;
		} catch (Exception e) {
			logger.debug(" 查询 维保 明细 表数据  出错", e);
			flag = 0;
		} finally {
			try {	
				if(dbResultSet != null){
					dbResultSet.close();
				}
				if(dbPstmt != null){
					dbPstmt.close();
				}
				if(dbConnection != null){
					dbConnection.close();
				}
			} catch (Exception e2) {
				logger.error("智能维保任务关闭数据库连接异常！",e2);
			}
		}
		return flag;
	}
	
	//获取提醒维保信息
	public void getWarnMessage(MainTainStatistic mainTainStatistic){
		//获取里程提醒信息
		mainTainStatistic.setWarnMileageMessage(this.getWarnMileageMessage(mainTainStatistic));
		//获取时间提醒信息
		mainTainStatistic.setWarnTimeMessage(this.getWarnTimeMessage(mainTainStatistic));
	}
	
	//获取里程提醒信息
	private String getWarnMileageMessage(MainTainStatistic mainTainStatistic){
		//计划维保里程 是否为空
		String warnMileageMessage = "";
		boolean planMaintainMileageIsNotNull = mainTainStatistic.getPlanMaintainMileage()!=null&&!"".equals(mainTainStatistic.getPlanMaintainMileage())&&!"0".equals(mainTainStatistic.getPlanMaintainMileage());
		//维保提醒里程是否为空
		boolean warnMileageIsNotNull = mainTainStatistic.getWarnMileage()!=0;
		
		if(planMaintainMileageIsNotNull&&warnMileageIsNotNull){
			//获取里程提醒信息
			if(this.isMileWarn(mainTainStatistic)){
				warnMileageMessage = mainTainStatistic.getVehicleNo() + ",行驶至" + mainTainStatistic.getPlanMaintainMileage() + " 公里后,请进行 " + mainTainStatistic.getMainTainName();
			}
			//判断里程维保是否未按期，未按期的更新状态
			if(this.isMileMainTainNotOnTime(mainTainStatistic)){
				this.updateMainTainStat(mainTainStatistic.getPlanCode());
			}
		}
		return warnMileageMessage;
	}
	
	//判断是否到达按里程提醒
	private boolean isMileWarn(MainTainStatistic mainTainStatistic){
		return (mainTainStatistic.getCurrentMileage() + mainTainStatistic.getWarnMileage())>Double.valueOf(mainTainStatistic.getPlanMaintainMileage());
	}
	
	//根据里程判断是否延期
	private boolean isMileMainTainNotOnTime(MainTainStatistic mainTainStatistic){
		return mainTainStatistic.getCurrentMileage()>Double.valueOf(mainTainStatistic.getPlanMaintainMileage());
	}
	
	//获取时间提醒信息
	private String getWarnTimeMessage(MainTainStatistic mainTainStatistic){
		
		String warnTimeMessage = "";
		
		//计划维保时间是否为空
		boolean planMaintainTimeIsNotNull = mainTainStatistic.getPlanMaintainDate()!=null&&!"".equals(mainTainStatistic.getPlanMaintainDate())&&!"0".equals(mainTainStatistic.getPlanMaintainMileage());
		//维保提醒时间是否为空
		boolean warnTimeIsNotNull = mainTainStatistic.getIntervalDays()!=0;
		if(planMaintainTimeIsNotNull&&warnTimeIsNotNull){
			//获取时间提醒信息
			if(this.isTimeWarn(mainTainStatistic)){
				warnTimeMessage = mainTainStatistic.getVehicleNo() + ",请于 " + mainTainStatistic.getPlanMaintainDate() + " 日后,进行 " + mainTainStatistic.getMainTainName();
			}
			//判断时间维保是否未按期，未按期的更新状态
			if(this.isTimeMainTainNotOnTime(mainTainStatistic)){
				this.updateMainTainStat(mainTainStatistic.getPlanCode());
			}
		}
		return warnTimeMessage;
	}
	
	//判断是否到达提醒时间
	private boolean isTimeWarn(MainTainStatistic mainTainStatistic){
		String currentDate = DateTools.getStringDateShort();
		return DateTools.compareDoubleDate(DateTools.addDays(currentDate, mainTainStatistic.getWarnDays()),mainTainStatistic.getPlanMaintainDate());
	}
	
	//根据时间判断是否已延期
	private boolean isTimeMainTainNotOnTime(MainTainStatistic mainTainStatistic){
		return DateTools.compareDoubleDate(DateTools.getStringDateShort(),mainTainStatistic.getPlanMaintainDate());
	}
	
	/*
	 * 更新TB_MAINTAIN_DETAIL表 MAINTAIN_ONTIME_STAT字段
	 * @param
	 * @return int 0:执行失败, 1执行成功

	 */
	public int updateMainTainStat(String planCode){
		
		int flag = 0;
		PreparedStatement dbPstmt = null;
		try {
			dbPstmt = dbConnection.prepareStatement(updateMainTainDetailStatSql);
			dbPstmt.setString(1, "1");
			dbPstmt.setString(2, planCode);
			dbPstmt.executeUpdate();
			flag = 1;
		} catch (Exception e) {
			logger.debug("更新 维保  明细  表数据  维保状态字段  出错", e);
			flag = 0;
		} finally {
			try {			
				if(dbPstmt != null){
					dbPstmt.close();
				}
			} catch (Exception e2) {
				logger.error("智能维保任务关闭数据库连接异常！",e2);
			}
		}
		return flag;
		
	}
	
	/**
	 * 更新TB_MAINTAIN_DETAIL表 MILEAGE_MESS,TIME_MESS(告警信息字段)等
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int updateMainTainDetail(MainTainStatistic mainTainStatistic) {
		int flag = 0;
		PreparedStatement dbPstmt = null;
		try {
			dbPstmt = dbConnection.prepareStatement(updateMainTainDetailSql);
			dbPstmt.setString(1, mainTainStatistic.getWarnMileageMessage());
			dbPstmt.setString(2, mainTainStatistic.getWarnTimeMessage());
			dbPstmt.setString(3, mainTainStatistic.getPlanCode());
			dbPstmt.executeUpdate();
			flag = 1;
		} catch (Exception e) {
			logger.debug("更新 维保  明细  表数据  提示信息字段  出错", e);
			flag = 0;
		} finally {
			try {			
				if(dbPstmt != null){
					dbPstmt.close();
				}
			} catch (Exception e2) {
				logger.error("智能维保任务关闭数据库连接异常！",e2);
			}
		}
		return flag;
	}

	/**
	 * 生成新一条记录 更新插入TB_MAINTAIN_DETAIL表
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int createNewMainTainData0(MainTainStatistic mainTainStatistic) {
		int flag = 0;
		PreparedStatement dbPstmt = null;
		logger.debug("planId:" + mainTainStatistic.getPlanId() + " exeFrequency:" + mainTainStatistic.getExeFrequency() + " vid:" + mainTainStatistic.getVid());
		// logger.debug("---生成新一条记录 更新  维保【明细】表数据 --->:" +
		// createNewMainTainDataSql);
		try {
			dbPstmt = dbConnection.prepareStatement(createNewMainTainDataSql);
			dbPstmt.setString(1, mainTainStatistic.getPlanId());
			dbPstmt.setString(2, mainTainStatistic.getExeFrequency());
			dbPstmt.setString(3, mainTainStatistic.getVid());
			
			dbPstmt.setString(4, mainTainStatistic.getVid());
			
			dbPstmt.setString(5, mainTainStatistic.getPlanId());
			
			dbPstmt.setInt(6, Integer.parseInt(mainTainStatistic.getMaintainId()));
			
			dbPstmt.setString(7, mainTainStatistic.getVid());
			dbPstmt.setString(8, this.getPlanMainTainTime(mainTainStatistic));
			dbPstmt.setString(9, this.getPlanMainTainMile(mainTainStatistic));
			
			dbPstmt.setString(10, mainTainStatistic.getPlanId());
			dbPstmt.setString(11, mainTainStatistic.getExeFrequency());
			dbPstmt.setString(12, mainTainStatistic.getVid());
			
			dbPstmt.setString(13, mainTainStatistic.getPlanId());
			dbPstmt.setString(14, mainTainStatistic.getExeFrequency());
			dbPstmt.setString(15, mainTainStatistic.getVid());
			dbPstmt.executeUpdate();
			flag = 1;
		} catch (Exception e) {
			logger.debug("生成新一条记录 更新  维保 明细 表数据   出错", e);
			flag = 0;
		} finally {
			try {			
				if(dbPstmt != null){
					dbPstmt.close();
				}
			} catch (Exception e2) {
				logger.error("智能维保任务关闭数据库连接异常！",e2);
			}
		}
		return flag;
	}
	
	public int createNewMainTainData(MainTainStatistic mainTainStatistic) {
		int flag = 0;
		PreparedStatement dbPstmt = null;
		PreparedStatement dbPstmt0 = null;
		PreparedStatement dbPstmt1 = null;
		PreparedStatement dbPstmt3 = null;
		PreparedStatement dbPstmt4 = null;
		ResultSet rs = null;
		ResultSet rs0 = null;
		ResultSet rs1 = null;
		ResultSet rs3 = null;
		logger.debug("planId:" + mainTainStatistic.getPlanId() + " exeFrequency:" + mainTainStatistic.getExeFrequency() + " vid:" + mainTainStatistic.getVid());
		// logger.debug("---生成新一条记录 更新  维保【明细】表数据 --->:" +
		// createNewMainTainDataSql);
		try {
			//查询车辆已维保次数
			dbPstmt = dbConnection.prepareStatement(queryVehicleMaintainTimesSql);
			dbPstmt.setString(1, mainTainStatistic.getPlanId());
			dbPstmt.setString(2, mainTainStatistic.getExeFrequency());
			dbPstmt.setString(3, mainTainStatistic.getVid());
			rs = dbPstmt.executeQuery();
			int vTimes = 0;
			while(rs.next()){
				vTimes = rs.getInt("V_TIMES");
			}
			
			//查询车辆最新维保项目编号
			dbPstmt0 = dbConnection.prepareStatement(queryVehicleMaintainNumSql);
			dbPstmt0.setString(1, mainTainStatistic.getVid());
			dbPstmt0.setString(2, mainTainStatistic.getPlanId());
			rs0 = dbPstmt0.executeQuery();
			int vPlanNum = 0 ;
			while(rs0.next()){
				vPlanNum = rs0.getInt("V_PLAN_NUM");
			}
			
			//查询维保项目简称
			dbPstmt1 = dbConnection.prepareStatement(queryMaintainClassSql);
			dbPstmt1.setBigDecimal(1, new BigDecimal(mainTainStatistic.getMaintainId()));
			rs1 = dbPstmt1.executeQuery();
			String  abbreviaName = "";
			logger.info("---ID为："+mainTainStatistic.getMaintainId());
			while(rs1.next()){
				abbreviaName = rs1.getString("V_ABBREVIATION_NAME");
				logger.info("简称为："+abbreviaName+"---ID为："+mainTainStatistic.getMaintainId());
			}
			
			//查询要扩展的维保明细信息
			dbPstmt3 = dbConnection.prepareStatement(queryExtendMaintainDetailSql);
			dbPstmt3.setString(1, abbreviaName);
			dbPstmt3.setInt(2, vPlanNum);
			dbPstmt3.setInt(3, vTimes);
			dbPstmt3.setString(4, this.getPlanMainTainTime(mainTainStatistic));
			dbPstmt3.setString(5, this.getPlanMainTainMile(mainTainStatistic));
			dbPstmt3.setString(6, mainTainStatistic.getPlanId());
			dbPstmt3.setString(7, mainTainStatistic.getExeFrequency());
			dbPstmt3.setString(8, mainTainStatistic.getVid()); 
			dbPstmt3.setInt(9, vTimes);
			dbPstmt3.setString(10, mainTainStatistic.getPlanId());
			dbPstmt3.setString(11, mainTainStatistic.getExeFrequency());
			dbPstmt3.setString(12, mainTainStatistic.getVid());
			dbPstmt3.setInt(13, vTimes);
			rs3 = dbPstmt3.executeQuery();
			
			//保存维保明细数据
			dbPstmt4 = dbConnection.prepareStatement(saveMaintainDetailSql);
			int count = 0;
			while(rs3.next()){
				dbPstmt4.setString(1, GeneratorPK.instance().getPKString());
				dbPstmt4.setString(2, rs3.getString("MAINTAIN_ID"));
				dbPstmt4.setString(3, rs3.getString("MAINTAIN_NAME"));
				dbPstmt4.setString(4, rs3.getString("PLAN_ID"));
				dbPstmt4.setString(5, rs3.getString("PLAN_CODE"));
				dbPstmt4.setString(6, rs3.getString("EXE_FREQUENCY"));
				dbPstmt4.setString(7, rs3.getString("VID"));
				dbPstmt4.setString(8, rs3.getString("C_VIN"));
				dbPstmt4.setString(9, rs3.getString("VEHICLE_NO"));
				dbPstmt4.setString(10, rs3.getString("PROD_CODE"));
				dbPstmt4.setString(11, rs3.getString("PROD_NAME"));
				dbPstmt4.setString(12, rs3.getString("LINE_ID"));
				dbPstmt4.setString(13, rs3.getString("LINE_NAME"));
				dbPstmt4.setString(14, rs3.getString("TEAM_ID"));
				dbPstmt4.setString(15, rs3.getString("TEAM_NAME"));
				dbPstmt4.setString(16, rs3.getString("ENT_ID"));
				dbPstmt4.setString(17, rs3.getString("ENT_NAME"));
				dbPstmt4.setInt(18, rs3.getInt("MAINTAIN_TIMES"));
				dbPstmt4.setLong(19, rs3.getLong("PLAN_MAINTAIN_DATE"));
				dbPstmt4.setLong(20, rs3.getLong("PLAN_MAINTAIN_MILEAGE"));
				dbPstmt4.setString(21, rs3.getString("MAINTAIN_ONTIME_STAT"));
				dbPstmt4.addBatch();
				count++;
				if (count>=100){
					dbPstmt4.executeBatch();
					count = 0;
				}
			}
			if (count>0){
				dbPstmt4.executeBatch();
			}
			
			flag = 1;
		} catch (Exception e) {
			logger.debug("生成新一条记录 更新  维保 明细 表数据   出错", e);
			flag = 0;
		} finally {
			try {
				if (rs!=null){
						rs.close();
				}
				if (rs0!=null){
					rs0.close();
				}
				if (rs1!=null){
					rs1.close();
				}
				if (rs3!=null){
					rs3.close();
				}				
				if(dbPstmt != null){
					dbPstmt.close();
				}
				if(dbPstmt0 != null){
					dbPstmt0.close();
				}
				if(dbPstmt1 != null){
					dbPstmt1.close();
				}
				if(dbPstmt3 != null){
					dbPstmt3.close();
				}
				if(dbPstmt4 != null){
					dbPstmt4.close();
				}
			} catch (Exception e2) {
				logger.error("智能维保任务关闭数据库连接异常！",e2);
			}
		}
		return flag;
	}
	
	//获取下次维保的维保里程
	public String getPlanMainTainMile(MainTainStatistic mainTainStatistic){
		
		Double planMainTainMile = 0D;
		if(mainTainStatistic.getIntervalMileage()!=0){
			planMainTainMile = mainTainStatistic.getMainTainMileage() + mainTainStatistic.getIntervalMileage();
		}
		return String.valueOf(planMainTainMile.intValue());
		
	}
	
	//获取下次维保的维保时间
	public String getPlanMainTainTime(MainTainStatistic mainTainStatistic){
		
		String planMainTainTime = "";
		
		if(mainTainStatistic.getIntervalDays()!=0){
			planMainTainTime = DateTools.addDays(mainTainStatistic.getMainTainDate(),mainTainStatistic.getIntervalDays());
		}
		
		return planMainTainTime;
		
	}

	public static void main(String[] args) {
		//MaintenanceTask m  = new MaintenanceTask();
		//m.execute();
	}


}
