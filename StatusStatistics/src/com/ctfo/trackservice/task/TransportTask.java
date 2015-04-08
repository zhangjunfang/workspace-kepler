package com.ctfo.trackservice.task;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.sql.Types;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.util.ConfigLoader;
import com.ctfo.trackservice.util.DateTools;


/**
 * 文件名：TransportTask.java
 * 功能：趟次统计
 *
 * @author huangjincheng
 * 2014-10-11下午4:44:21
 * 
 */
public class TransportTask  extends TaskAdapter{
	private final static Logger logger = LoggerFactory.getLogger(TransportTask.class);
	
	//private List<String> overIdList = new ArrayList<String>();
	/**当日12点utc*/
	private long beginTime;
	private long endTime;
	//private String dateStr;
	
	private String delTransportDaysSql;// 删除当日车辆趟次统计结果
	//private String setNullTransportSql; //m_seq置空
	//private String overidTransportSql;	//车辆过站记录表顺序号集合
	//private String updateTransportSql;  //更新车辆过站记录表
	private String tangciqufenSql;	// 车辆运行趟次存储过程
	private String statTransportDetailSql;	// 车辆运行趟次明细统计
	private String statTransportDaysSql;	// 车辆运行趟次日统计

	@Override
	public void init() {
		
		// 删除当日车辆趟次统计结果
		delTransportDaysSql = ConfigLoader.config.get(
				"sql_delTransportDays");
	/*	setNullTransportSql = ConfigLoader.config.get(
				"sql_setNullTransport");
		overidTransportSql = ConfigLoader.config.get(
				"sql_overidTransport");
		updateTransportSql = ConfigLoader.config.get(
				"sql_updateTransport");	*/
		statTransportDetailSql = ConfigLoader.config.get(
				"sql_statTransportDetail");	
		statTransportDaysSql = ConfigLoader.config.get(
				"sql_statTransportDays");
		tangciqufenSql = ConfigLoader.config.get(
				"tangciqufenSql");
	}

	@Override
	public void execute() throws Exception {
		if("restore".equals(this.type)){
			String restoreTime = "";
			System.out.print("--------------------【趟次统计】输入您需要补跑的日期(yyyy/mm/dd)：");
			while(true){
				BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
				restoreTime = br.readLine();
				if (!restoreTime.matches("\\d{4}/\\d{2}/\\d{2}")) {
					System.out.print("--------------------【趟次统计】输入错误,请重新选择输入：");
					continue;
				}else break;
			}
			SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd hh/mm/ss");
			Date date = sdf.parse(restoreTime+" 00/00/00");
			this.utc = date.getTime();
			logger.info("--------------------【趟次统计】任务开始！-------------------------");
		}else if("start".equals(this.type)){
			this.utc = DateTools.getYesDayYearMonthDay();
			logger.info("--------------------【趟次统计】任务开始！-------------------------");
		}else if("autoRestore".equals(this.type)){
			logger.info("--------------------【趟次统计】自动补跑启动,时间:{}",DateTools.getStringDate(this.utc));
		}
		this.beginTime = this.utc;
		this.endTime = this.utc + 1000 * 60 * 60 * 24;
		Date dt = new Date();
		dt.setTime(this.utc);
		
		long startTime = System.currentTimeMillis();  
		PreparedStatement dbPstmt1 = null;
		PreparedStatement dbPstmt2 = null;
		PreparedStatement dbPstmt3 = null;
		PreparedStatement dbPstmt4 = null;
		PreparedStatement dbPstmt5 = null;
		PreparedStatement dbPstmt6 = null;
		//ResultSet rs = null;
		Connection dbConnection = null;

		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection != null) {

				// 删除当日车辆趟次统计结果，重复统计时防止数据重复
				dbPstmt1 = dbConnection.prepareStatement(delTransportDaysSql);
				dbPstmt1.setLong(1, beginTime);
				dbPstmt1.setLong(2, endTime);
				dbPstmt1.setLong(3, beginTime);
				dbPstmt1.setLong(4, endTime);
				dbPstmt1.executeUpdate();

				// 车辆趟次明细统计
				/*dbPstmt2 = dbConnection.prepareStatement(setNullTransportSql);
				dbPstmt2.setLong(1, beginTime);
				dbPstmt2.setLong(2, endTime);
				dbPstmt2.executeUpdate();

				// 车辆趟次日统计
				dbPstmt3 = dbConnection.prepareStatement(overidTransportSql);
				dbPstmt3.setLong(1, beginTime);
				dbPstmt3.setLong(2, endTime);
				rs = dbPstmt3.executeQuery();
				while(rs.next()){
					overIdList.add(rs.getString("OVER_ID"));
					count ++;
				}
				//更新车辆过站记录表
				StringBuffer sbuff = new StringBuffer();
				for(int i=0;i<overIdList.size();i++){
					sbuff.append("'"+overIdList.get(i)+"'");
					if(i<overIdList.size()-1){
						sbuff.append(",");
					}
				}
				overIdList.clear();
				updateTransportSql = updateTransportSql.replace("?", sbuff.toString());
				dbPstmt4 = dbConnection.prepareStatement(updateTransportSql);
				//dbPstmt4.setString(1, sbuff.toString());
				dbPstmt4.executeUpdate();*/
				
				 //调用存储过程为区分趟次
				 dbPstmt3 = dbConnection.prepareCall(tangciqufenSql);
				 dbPstmt3.setString(1,DateTools.dateToStr2(dt));
				 ((CallableStatement) dbPstmt3).registerOutParameter(2, Types.INTEGER);
				 dbPstmt3.execute();
				
				// 车辆趟次明细统计
				dbPstmt5 = dbConnection.prepareStatement(statTransportDetailSql);
				dbPstmt5.setLong(1, beginTime);
				dbPstmt5.setLong(2, endTime);
				dbPstmt5.executeUpdate();

				// 车辆趟次日统计
				dbPstmt6 = dbConnection.prepareStatement(statTransportDaysSql);
				dbPstmt6.setLong(1, beginTime);
				dbPstmt6.setLong(2, endTime);
				dbPstmt6.executeUpdate();
					
				logger.info("--------------------【趟次统计】任务完成！,日期：【{}】,耗时：【{}】ms",DateTools.dateToStr(dt),(System.currentTimeMillis()-startTime));

			} else {
				logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("车辆趟次统计信息生成出错：", e);
		} finally {
			try {
				if (dbPstmt1 != null) {
					dbPstmt1.close();
				}
				if (dbPstmt2 != null) {
					dbPstmt2.close();
				}
				if (dbPstmt3 != null) {
					dbPstmt3.close();
				}
				if (dbPstmt4 != null) {
					dbPstmt4.close();
				}
				if (dbPstmt5 != null) {
					dbPstmt5.close();
				}
				if (dbPstmt6 != null) {
					dbPstmt6.close();
				}
				if (dbConnection != null) {
					dbConnection.close();
				}
			} catch (SQLException e) {
				logger.error("连接放回连接池出错.", e);
			}
		}


	}

	@Override
	public void isTimeRun() throws Exception {
		// TODO Auto-generated method stub
		
	}

}
