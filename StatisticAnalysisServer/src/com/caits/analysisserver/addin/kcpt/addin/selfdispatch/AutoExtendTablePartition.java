package com.caits.analysisserver.addin.kcpt.addin.selfdispatch;


import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Types;
import java.util.Calendar;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.caits.analysisserver.addin.kcpt.addin.SelfDispatch;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： StatisticAnalysisServer <br>
 * 功能： 自动扩展表分区<br>
 * 描述： 每月1日扩展数据表下月所有分区<br>
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
 * <td>2012-10-31</td>
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
 */
public class AutoExtendTablePartition extends SelfDispatch {
	private static final Logger logger = LoggerFactory.getLogger(AutoExtendTablePartition.class);
	
	// ------获得xml拼接的Sql语句
	private String procExtendTablePartitionSql; // 查询扩展表分区语句
	
	// 初始化方法
	public void initAnalyser(){
		// 查询扩展表分区语句
		procExtendTablePartitionSql = SQLPool.getinstance().getSql("sql_procExtendTablePartitionSql");

	}

	public void run() {
		/*logger.info("自动扩展表分区任务加载成功！");
		while (true) {
			try {
				Long interval = sleepInterval();
				this.sleep(interval);// 每月1日0时30分执行一次
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
			
			AutoExtendThread aet = new AutoExtendThread();
			Thread t = new Thread(aet);
			t.start();
			
		}*/

	}
	
	public long sleepInterval(){     
	  
	       Calendar lastDate = Calendar.getInstance();
	       long currTime = lastDate.getTimeInMillis();
	       lastDate.add(Calendar.MONTH,1);//加一个月
	       lastDate.set(Calendar.DATE, 1);//把日期设置为月第一天
	       lastDate.set(Calendar.HOUR_OF_DAY, 0);//把时间设置为0时30分
	       lastDate.set(Calendar.MINUTE, 30);

	       long nextMonthTime = lastDate.getTimeInMillis();

	       return nextMonthTime - currTime;
	} 
	

	/**
	 * 执行行驶记录数据解析
	 * 
	 * 查询未解析的行驶记录命令
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int extendTablePartition() {
		//logger.info("start parse!");
		// PreparedStatement对象
		CallableStatement dbCstmt = null;
		
		Connection dbConnection = null;

		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		try {
			// 获得Connection对象
			Long startTime = System.currentTimeMillis();
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection!=null){
				String dayFrom = CDate.getNextMonthFirstDayByFormat("yyyyMMdd");
				int partnum = CDate.getMonthDaynum(CDate.getNextMonthFirstDay());
				String tableName = "TH_VEHICLE_STOPSTART";
				String partPreflag = "P_STOPSTART_";
				//扩展TH_VEHICLE_STOPSTART表下月分区
				dbCstmt = dbConnection.prepareCall(procExtendTablePartitionSql);
				//add_diurnal_partition_proc('20120801','31','TH_VEHICLE_STOPSTART','KCPT','P_STOPSTART_',successtag);
				dbCstmt.setString(1, dayFrom);//从那天开始创建 VARCHAR2
				dbCstmt.setInt(2, partnum);//创建多少个分区  NUMBER
				dbCstmt.setString(3, tableName);//针对那个分区表 VARCHAR2
				dbCstmt.setString(4, "KCPT");//针对那个表空间 VARCHAR2
				dbCstmt.setString(5, partPreflag);//分区表前缀 VARCHAR2
				dbCstmt.registerOutParameter(6, Types.INTEGER);//添加是否成功 NUMBER
				
				dbCstmt.execute();
				
				int successTag = dbCstmt.getInt(6); 

				if (successTag==1){
					logger.debug("表"+tableName+"扩展从" +dayFrom + "日期开始的" + partnum
							+ "个分区成功!");
				}else{
					logger.debug("表"+tableName+"扩展从" +dayFrom + "日期开始的" + partnum
							+ "个分区失败!");
				}
				
				successTag = 0;
				tableName = "TH_VEHICLE_RUNNING";
				partPreflag = "P_RUNNING_";
				//扩展TH_VEHICLE_RUNNING表下月分区
				//add_diurnal_partition_proc('20120801','31','TH_VEHICLE_RUNNING','KCPT','P_RUNNING_',successtag);
				dbCstmt.setString(1, dayFrom);//从那天开始创建 VARCHAR2
				dbCstmt.setInt(2, partnum);//创建多少个分区  NUMBER
				dbCstmt.setString(3, tableName);//针对那个分区表 VARCHAR2
				dbCstmt.setString(4, "KCPT");//针对那个表空间 VARCHAR2
				dbCstmt.setString(5, partPreflag);//分区表前缀 VARCHAR2
				dbCstmt.registerOutParameter(6, Types.INTEGER);//添加是否成功 NUMBER
				
				dbCstmt.execute();
				
				successTag = dbCstmt.getInt(6); 
				
				if (successTag==1){
					logger.debug("表"+tableName+"扩展从" +dayFrom + "日期开始的" + partnum
							+ "个分区成功!");
				}else{
					logger.debug("表"+tableName+"扩展从" +dayFrom + "日期开始的" + partnum
							+ "个分区失败!");
				}
			}else{
				logger.debug("获取数据库链接失败");
			}
			Long endTime= System.currentTimeMillis();
			System.out.println("自动扩展表分区,处理时长："+ (endTime-startTime)/1000+"s");
			logger.info("自动扩展表分区,处理时长："+ (endTime-startTime)/1000+"s");
		} catch (Exception e) {
			logger.error("自动扩展表分区出错：", e);
			// flag = 0;
		} finally {
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
		return flag;
	}

	/**
	 * 将空值转换为空字符串
	 * 
	 * @param str
	 *            字符串
	 * @return String 返回处理后的字符串
	 */
	public static String nullToStr(String str) {
		return str == null || str.equals("null") ? "" : str.trim();
	}

	class AutoExtendThread implements Runnable {

		@Override
		public void run() {
			// TODO Auto-generated method stub
			extendTablePartition();
		}
	}

	@Override
	public void costTime() {
		// TODO Auto-generated method stub
		
	}
	
	public static void main(String args[]){
		System.out.println(CDate.getUserDate("yyyy").substring(0,2));
	}

}
