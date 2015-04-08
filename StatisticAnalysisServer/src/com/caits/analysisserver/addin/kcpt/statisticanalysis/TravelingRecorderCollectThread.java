package com.caits.analysisserver.addin.kcpt.statisticanalysis;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Date;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.utils.BASE64Utils;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.Converser;
import com.lingtu.xmlconf.XmlConf;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： StatisticAnalysisServer <br>
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
 * <td>2011-10-18</td>
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
public class TravelingRecorderCollectThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(TravelingRecorderCollectThread.class);

	// ------获得xml拼接的Sql语句
	private String queryUnparseRecorderSql; // 查询未解析的行驶记录命令
	@SuppressWarnings("unused")
	private String updateRecorderStatusSql; // 修改行驶记录命令解析状态

	public void run() {
		logger.info("行驶记录数据解析！");
		while (true) {
			ParseThread pt = new ParseThread();
			pt.run();
			try {
				Thread.sleep(60 * 1000);// 每60秒执行一次
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}

	}

	// 初始化方法
	public void initAnalyser(XmlConf config, String nodeName) throws Exception {
		// 查询未解析的行驶记录命令
		queryUnparseRecorderSql =  config.getStringValue(nodeName + "|sql_queryUnparseRecorderSql");
		logger.info(queryUnparseRecorderSql);

		// 修改行驶记录命令解析状态
		updateRecorderStatusSql = config.getStringValue(nodeName
				+ "|sql_updateRecorderStatusSql");
	}

	/**
	 * 执行行驶记录数据解析
	 * 
	 * 查询未解析的行驶记录命令
	 * 
	 * @param
	 * @return int 0:执行失败, 1执行成功
	 */
	public int executeParseTravelingRecorder() {
		//logger.info("start parse!");
		// PreparedStatement对象
		PreparedStatement dbPstmt0 = null;
		PreparedStatement dbPstmt1 = null;
		PreparedStatement dbPstmt2 = null;
		Connection dbConnection = null;
		// 结果集对象
		ResultSet dbResultSet = null;
		// 成功标志位 0:执行失败, >=1执行成功,成功解析个数
		int flag = 0;
		try {
			// 获得Connection对象
			dbConnection = OracleConnectionPool.getConnection();
			if (dbConnection!=null){
			// 变量定义
			String pid = "";
			int vid = 0;
			String content = "";
			double utc = 0;
//			String entId = "";
			String vehicleNo = "";
			String vinCode = "";
			String vehicleType = "";
			String driverName = "";
			String drivingNumber = "";
			dbPstmt0 = dbConnection.prepareStatement(queryUnparseRecorderSql);
			dbResultSet = dbPstmt0.executeQuery();
			//logger.info("get rows:::"+dbResultSet.getRow());
			// 逐个解析每个行驶记录命令
			while (dbResultSet.next()) {
				pid = dbResultSet.getString(1);
				vid = dbResultSet.getInt(2);
				utc = dbResultSet.getDouble(3);
				content = dbResultSet.getString(4);
				vehicleNo = dbResultSet.getString(5) == null ? "" : dbResultSet
						.getString(5);
				vinCode = dbResultSet.getString(6) == null ? "" : dbResultSet
						.getString(6);
				vehicleType = dbResultSet.getString(7) == null ? ""
						: dbResultSet.getString(7);
				// entId = dbResultSet.getString(8);

				logger.debug("--pid--:" + pid + " --vid--:" + vid
						+ "  --utc--:" + utc + " --content--:" + content);

				// 更新当前指令的解析次数,若3次未解析成功则不再解析
				String pasernumSql = " UPDATE TH_VEHICLE_RECORDER SET PARSENUM=NVL(PARSENUM,0)+1 WHERE PID='"
						+ pid + "'";
				dbPstmt1 = dbConnection.prepareStatement(pasernumSql);
				dbPstmt1.executeUpdate();
				if(dbPstmt1 != null){
					dbPstmt1.close();
				}
				
				byte[] command = null;
//				sun.misc.BASE64Decoder decoder = new sun.misc.BASE64Decoder();
//				command = decoder.decodeBuffer(content);
				command = BASE64Utils.base64DecodeToArray(content);
				logger.debug("原始内容：" + Converser.bytesToHexString(command));
				// 执行解析 按19056（2003）
				int location = 0;

				location += 2;// 跳过2字节命令字头

				// 命令字部分
				byte[] commandbyte = new byte[1];

				System.arraycopy(command, location, commandbyte, 0, 1);

				String commandWord = Converser.bytesToHexString(commandbyte);

				if (commandWord != null) {
					if (!commandWord.equals("FB")) {
						location += 1;

						byte datalentmp[] = new byte[2];

						System.arraycopy(command, location, datalentmp, 0, 2);

						int datalen = Converser.bytes2Short(datalentmp, 0);

						// 数据块长度 2字节
						// int datalen = Integer.parseInt(new
						// String(datalentmp));

						location += 2;
						location += 1;// 跳过 保留字 1字节
						if (datalen > 0
								&& (command.length > location + datalen)) {
							
							//取出行驶记录数据块 
							byte recorder[] = new byte[datalen];
							System.arraycopy(command, location, recorder,0, datalen);
							
							if (commandWord.equals("07")) {
								
								//按次解析事故疑点数据 每次事故疑点有206个字节
								for (int j=0;j<datalen;j+=206){
									byte onceRecorder[] = new byte[206];
									System.arraycopy(recorder, j, onceRecorder,
											0, 206);
									
									//单次记录中的位置计数器
									int loc = 0;
									
									byte[] stopTimeBt = new byte[6];
									System.arraycopy(onceRecorder, loc, stopTimeBt,
											0, 6);
									
									Date stopTime = CDate.strToDateByFormat("20"
											+ Converser.bcdToStr(stopTimeBt, 0, 6),
											"yyyyMMddHHmmss");
									long stopTimeUTC = stopTime.getTime();
									loc += 6;
									
									StringBuffer sb0 = new StringBuffer();
									sb0.append("INSERT INTO TB_ACCIDENT_DOUBPOINTS_MAIN (POINT_ID,GATHER_TIME,VID,VEHICLE_NO,");
									sb0.append("VIN_CODE,VEHICLE_TYPE,DRIVER_NAME,DRIVING_NUMBER,STOP_TIME,START_SPEED,BRAKING_TIME)");
									sb0.append(" VALUES (SYS_GUID(),"
											+ (new Date()).getTime() + "," + vid);
									sb0.append(",'" + vehicleNo + "','" + vinCode
											+ "','" + vehicleType + "','"
											+ driverName + "','" + drivingNumber
											+ "'," + stopTimeUTC + ",");
									
									// 提取单次全部车速 、开关量值
									byte[] speedSwitch = new byte[200];
									System.arraycopy(onceRecorder, loc,
											speedSwitch, 0, 200);
									
									StringBuffer sb = new StringBuffer();

									float brakingTime = 0;
									int startSpeed = 0;
									
									// 循环取出车速对应的开关量信息
									
									//开始停车时间=停车时间前20秒
									stopTimeUTC -= (20*1000);

									for (int i = 0; i < 200; i += 2) {

										stopTimeUTC += (1000 * 0.2);

										byte[] tmp0 = new byte[2];
										System.arraycopy(speedSwitch, i, tmp0, 1, 1);
										int speed = Converser.bytes2Short(tmp0, 0);
										if (i == 0) {
											startSpeed = speed;
										}
										byte[] tmp1 = new byte[1];
										System.arraycopy(speedSwitch, i + 1, tmp1,
												0, 1);
										String switch0 = Converser
												.hexTo2BCD(Converser
														.bytesToHexString(tmp1));

										char[] switchChar = switch0.toCharArray();
										
										if (switchChar[0]=='1'){
											brakingTime += 0.2;
										}
										// SEQ_ACCIDENT_MAIN_ID
										sb.append("INSERT INTO TB_ACCIDENT_DOUBPOINTS_DETAIL (AUTO_ID,POINT_ID,VEHICLE_SPEED,");
										sb.append("D0,D1,D2,D3,D4,D5,D6,D7,LON,LAT,MAPLON,MAPLAT,ELEVATION,POINT_TIME) ");
										sb.append("VALUES ( SYS_GUID(),mainId");
										sb.append("," + speed + "," + switchChar[7]
												+ "," + switchChar[6] + ","
												+ switchChar[5] + ","
												+ switchChar[4]);
										sb.append("," + switchChar[3] + ","
												+ switchChar[2] + ","
												+ switchChar[1] + ","
												+ switchChar[0]);
										sb.append(",null,null,null,null,null,"
												+ stopTimeUTC + ");");
									}
									
									sb0.append(startSpeed + "," + brakingTime
											+ ") RETURNING POINT_ID INTO mainId ;");

									String sqlStr = "declare mainId number(15); begin "
											+ sb0.toString()
											+ sb.toString()
											+ " UPDATE TH_VEHICLE_RECORDER SET ISPARSE=1 WHERE PID='"
											+ pid + "';" + " end;";
									//logger.info("sqlStr：" + sqlStr);
									dbPstmt2 = dbConnection.prepareStatement(sqlStr);
									dbPstmt2.executeUpdate();
									if(dbPstmt2 != null){
										dbPstmt2.close();
									}
								}

								//每条行驶记录疑点数据解析完成时加1
								flag += 1;
							}
						}

					}
				}

			}
			}else{
			logger.debug("获取数据库链接失败");
			}
		} catch (Exception e) {
			logger.error("解析行驶记录仪信息出错：", e);
			// flag = 0;
		} finally {
			try {
				if(dbResultSet != null){
					dbResultSet.close();
				}
				if(dbPstmt0 != null){
					dbPstmt0.close();
				}
				if(dbPstmt1 != null){
					dbPstmt1.close();
				}
				if(dbPstmt2 != null){
					dbPstmt2.close();
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

	class ParseThread implements Runnable {

		@Override
		public void run() {
			// TODO Auto-generated method stub
			executeParseTravelingRecorder();
		}
	}

}
