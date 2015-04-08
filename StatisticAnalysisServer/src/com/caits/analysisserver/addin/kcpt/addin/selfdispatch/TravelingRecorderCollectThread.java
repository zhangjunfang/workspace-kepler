package com.caits.analysisserver.addin.kcpt.addin.selfdispatch;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.addin.SelfDispatch;
import com.caits.analysisserver.bean.AccidentDoubpointsDetail;
import com.caits.analysisserver.bean.AccidentDoubpointsMain;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.BASE64Utils;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.Converser;
import com.ctfo.generator.pk.GeneratorPK;

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
public class TravelingRecorderCollectThread extends SelfDispatch {
	private static final Logger logger = LoggerFactory.getLogger(TravelingRecorderCollectThread.class);
	
	// ------获得xml拼接的Sql语句
	private String queryUnparseRecorderSql; // 查询未解析的行驶记录命令
 	@SuppressWarnings("unused")
	private String updateRecorderStatusSql; // 修改行驶记录命令解析状态
	
	private int count=0;//清理未对应日志时间间隔
	
	// 初始化方法
	public void initAnalyser(){
		// 查询未解析的行驶记录命令
		queryUnparseRecorderSql = SQLPool.getinstance().getSql("sql_queryUnparseRecorderSql");
		// 修改行驶记录命令解析状态
		updateRecorderStatusSql = SQLPool.getinstance().getSql("sql_updateRecorderStatusSql");

	}

	public void run() {
		logger.info("行驶记录数据解析！");
		while (true) {
			ParseThread pt = new ParseThread();
			pt.run();
			count++;
			try {
				Thread.sleep(60 * 1000);// 每60秒执行一次
			} catch (InterruptedException e) {
 				e.printStackTrace();
			}
		}

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
		PreparedStatement dbPstmt3 = null;
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
			String vid = "";
			String content = "";
			double utc = 0;
//			String entId = "";
			String vehicleNo = "";
			String vinCode = "";
			String vehicleType = "";
			String driverName = "";
			String drivingNumber = "";
			String coSeq = "";
			dbPstmt0 = dbConnection.prepareStatement(queryUnparseRecorderSql);
			dbResultSet = dbPstmt0.executeQuery();
			//logger.info("get rows:::"+dbResultSet.getRow());
			// 逐个解析每个行驶记录命令
			while (dbResultSet.next()) {
				Long startTime = System.currentTimeMillis();
				pid = dbResultSet.getString(1);
				vid = dbResultSet.getString(2);
				utc = dbResultSet.getDouble(3);
				content = dbResultSet.getString(4);
				vehicleNo = dbResultSet.getString(5) == null ? "" : dbResultSet
						.getString(5);
				vinCode = dbResultSet.getString(6) == null ? "" : dbResultSet
						.getString(6);
				vehicleType = dbResultSet.getString(7) == null ? ""
						: dbResultSet.getString(7);
				// entId = dbResultSet.getString(8);
				coSeq = dbResultSet.getString(8);

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
				if (content==null){
					logger.error("透传内容为空，跳过本条数据。");
					continue;
				}
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
									
									String stopTimeStr = Converser.bcdToStr(stopTimeBt, 0, 6);
									
									//如果日期错误则表示提取失败
									if (stopTimeStr==null||"".equals(stopTimeStr)||"00000000000".equals(stopTimeStr)){
										//终端返回失败数据时更新提取日志表对应记录状态为提取失败
										String sql = "begin UPDATE TH_TRAVELLING_RECORDER_LOG SET STATUS='01' WHERE COMMAND_SEQ=? ;"
											+ " UPDATE TH_VEHICLE_RECORDER SET ISPARSE=1 WHERE PID= ? ; end;" ;
										dbPstmt3 = dbConnection.prepareStatement(sql);
										dbPstmt3.setString(1, coSeq);
										dbPstmt3.setString(2, pid);
										dbPstmt3.executeUpdate();
										if(dbPstmt3 != null){
											dbPstmt3.close();
										}
										break;
									}
									
									Date stopTime = CDate.strToDateByFormat(CDate.getUserDate("yyyy").substring(0,2)
											+ stopTimeStr,
											"yyyyMMddHHmmss");
									long stopTimeUTC = stopTime.getTime();
									loc += 6;
									
									String pointId = GeneratorPK.instance().getPKString();
									AccidentDoubpointsMain adm = new AccidentDoubpointsMain();
									adm.setPointId(pointId);
									adm.setGatherTime((new Date()).getTime());
									adm.setVid(vid);
									adm.setVinCode(vinCode);
									adm.setVehicleNo(vehicleNo);
									adm.setVehicleType(vehicleType);
									adm.setDriverName(driverName);
									adm.setDriverNumber(drivingNumber);
									adm.setStopTime(stopTimeUTC);
									
									List<AccidentDoubpointsDetail> ls = new ArrayList<AccidentDoubpointsDetail>();
									
									// 提取单次全部车速 、开关量值
									byte[] speedSwitch = new byte[200];
									System.arraycopy(onceRecorder, loc,
											speedSwitch, 0, 200);
									
//									StringBuffer sb = new StringBuffer();

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

										AccidentDoubpointsDetail add = new AccidentDoubpointsDetail();
										add.setAutoId(GeneratorPK.instance().getPKString());
										add.setPointId(pointId);
										add.setVehicleSpeed(speed);
										add.setD0(""+switchChar[7]);
										add.setD1(""+switchChar[6]);
										add.setD2(""+switchChar[5]);
										add.setD3(""+switchChar[4]);
										add.setD4(""+switchChar[3]);
										add.setD5(""+switchChar[2]);
										add.setD6(""+switchChar[1]);
										add.setD7(""+switchChar[0]);
										add.setPointTime(stopTimeUTC);
										ls.add(add);
										
									}
									adm.setStartSpeed(startSpeed);
									adm.setBrakingTime(brakingTime);
									
									saveAccidentPointsData(dbConnection,adm,ls);

									String sqlStr = "begin "
											+ " UPDATE TH_VEHICLE_RECORDER SET ISPARSE=1 WHERE PID=? ;" 
											+ " UPDATE TH_TRAVELLING_RECORDER_LOG SET status='00' WHERE COMMAND_SEQ=? ;" 
											+ " UPDATE TH_VEHICLE_COMMAND SET CO_STATUS='0' WHERE CO_SEQ= ? ;"
											+ " end; ";
									//logger.info("sqlStr：" + sqlStr);
									dbPstmt2 = dbConnection.prepareStatement(sqlStr);
									dbPstmt2.setString(1, pid);
									dbPstmt2.setString(2, coSeq);
									dbPstmt2.setString(3, coSeq);
									
									dbPstmt2.executeUpdate();
									if(dbPstmt2 != null){
										dbPstmt2.close();
									}
								}

								//每条行驶记录疑点数据解析完成时加1
								flag += 1;
							}
						}

					}else{
						//终端返回失败数据时更新提取日志表对应记录状态为提取失败
						String sql = " UPDATE TH_TRAVELLING_RECORDER_LOG SET STATUS='01' WHERE COMMAND_SEQ='"
							+ coSeq + "'";
						dbPstmt2 = dbConnection.prepareStatement(sql);
						dbPstmt2.executeUpdate();
						if(dbPstmt2 != null){
							dbPstmt2.close();
						}
					}
				}
				Long endTime= System.currentTimeMillis();
				System.out.println("逐个解析每个行驶记录命令,处理时长："+ (endTime-startTime)/1000+"s");
				logger.info("逐个解析每个行驶记录命令,处理时长："+ (endTime-startTime)/1000+"s");
			}
			//更新提取日志表中显示提取成功，但长时间(3分钟)没有更新
			if (count==3){
				String sql = "UPDATE TH_TRAVELLING_RECORDER_LOG SET STATUS='01' WHERE STATUS='02' AND COMMAND_DOWN_TIME<"
					+ (CDate.getYearMonthDayUtc()-4*60*1000) + " AND COMMAND_DOWN_TIME>="+(CDate.getYearMonthDayUtc()-1*60*60*1000);
				dbPstmt2 = dbConnection.prepareStatement(sql);
				dbPstmt2.executeUpdate();
				if(dbPstmt2 != null){
					dbPstmt2.close();
				}
				count=0;
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
				if(dbPstmt3 != null){
					dbPstmt3.close();
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
	
	
	private void saveAccidentPointsData(Connection dbConnection,AccidentDoubpointsMain adm,List<AccidentDoubpointsDetail> ls){
		PreparedStatement dbPstmt4 = null;
		PreparedStatement dbPstmt5 = null;
		PreparedStatement dbPstmt6 = null;
		try{
			//按当前条件删除数据，防止数据重复
			StringBuffer delStr = new StringBuffer();
			delStr.append("BEGIN ");
			delStr.append("DELETE FROM TB_ACCIDENT_DOUBPOINTS_DETAIL WHERE POINT_ID IN (" );
			delStr.append("SELECT POINT_ID FROM TB_ACCIDENT_DOUBPOINTS_MAIN WHERE VID= ? and STOP_TIME=? ); ");
			delStr.append("DELETE FROM TB_ACCIDENT_DOUBPOINTS_MAIN WHERE VID= ? and STOP_TIME=? ; ");
			delStr.append("END;");
			
			dbPstmt4 = dbConnection.prepareStatement(delStr.toString());
			dbPstmt4.setString(1, adm.getVid());
			dbPstmt4.setLong(2, adm.getStopTime());
			dbPstmt4.setString(3, adm.getVid());
			dbPstmt4.setLong(4, adm.getStopTime());
			dbPstmt4.executeUpdate();
			
			//添加子表数据
			StringBuffer sb = new StringBuffer();
			sb.append("INSERT INTO TB_ACCIDENT_DOUBPOINTS_DETAIL (AUTO_ID,POINT_ID,VEHICLE_SPEED,");
			sb.append("D0,D1,D2,D3,D4,D5,D6,D7,LON,LAT,MAPLON,MAPLAT,ELEVATION,POINT_TIME) ");
			sb.append("VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)");
			dbPstmt5 = dbConnection.prepareStatement(sb.toString());
			
			for (int i = 0; i<ls.size(); i++){
				AccidentDoubpointsDetail detail = ls.get(i);
				
				dbPstmt5.setString(1,detail.getAutoId());
				dbPstmt5.setString(2,detail.getPointId());
				dbPstmt5.setLong(3, detail.getVehicleSpeed());
				dbPstmt5.setString(4, detail.getD0());
				dbPstmt5.setString(5, detail.getD1());
				dbPstmt5.setString(6, detail.getD2());
				dbPstmt5.setString(7, detail.getD3());
				dbPstmt5.setString(8, detail.getD4());
				dbPstmt5.setString(9, detail.getD5());
				dbPstmt5.setString(10, detail.getD6());
				dbPstmt5.setString(11, detail.getD7());
				
				dbPstmt5.setNull(12, Types.INTEGER);
				dbPstmt5.setNull(13, Types.INTEGER);
				dbPstmt5.setNull(14, Types.INTEGER);
				dbPstmt5.setNull(15, Types.INTEGER);
				dbPstmt5.setNull(16, Types.INTEGER);
				
				dbPstmt5.setLong(17, detail.getPointTime());
				
				dbPstmt5.addBatch();
			}
			dbPstmt5.executeBatch();
			
			//添加主表数据
			StringBuffer mainStr = new StringBuffer();
			mainStr.append("INSERT INTO TB_ACCIDENT_DOUBPOINTS_MAIN (POINT_ID,GATHER_TIME,VID,VEHICLE_NO,");
			mainStr.append("VIN_CODE,VEHICLE_TYPE,DRIVER_NAME,DRIVING_NUMBER,STOP_TIME,START_SPEED,BRAKING_TIME)");
			mainStr.append(" VALUES (?,?,?,?,?,?,?,?,?,?,?)");
			
			dbPstmt6 = dbConnection.prepareStatement(mainStr.toString());
			dbPstmt6.setString(1, adm.getPointId());
			dbPstmt6.setLong(2, adm.getGatherTime());
			dbPstmt6.setString(3, adm.getVid());
			dbPstmt6.setString(4, adm.getVehicleNo());
			dbPstmt6.setString(5, adm.getVinCode());
			dbPstmt6.setString(6, adm.getVehicleType());
			dbPstmt6.setString(7, adm.getDriverName());
			dbPstmt6.setString(8, adm.getDriverNumber());
			dbPstmt6.setLong(9, adm.getStopTime());
			dbPstmt6.setLong(10, adm.getStartSpeed());
			dbPstmt6.setFloat(11, adm.getBrakingTime());
			dbPstmt6.executeUpdate();
			
		}catch(Exception ex){
			logger.error("保存事故疑点数据过程中出错.",ex);
		}finally{
			try {
				if(dbPstmt4 != null){
					dbPstmt4.close();
				}
				if(dbPstmt5 != null){
					dbPstmt5.close();
				}
				if(dbPstmt6 != null){
					dbPstmt6.close();
				}
			} catch (SQLException e) {
				logger.error("连接放回连接池出错.",e);
			}
		}
		
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
			//logger.info("Starting traveling recorder");
 			executeParseTravelingRecorder();
		}
	}

	@Override
	public void costTime() {
 		
	}
	
	public static void main(String args[]){
		
		String con = "VXoHCAwAEgYBFyMhAoACgAKCAoACgg6ADoAOgA6ADoAOgA6AEYAUgBSAFIAUgBSAFIAUgBSAFIANgAaABoAGgAaABoAGgAaABoAGgAaABoAEgAKAAoACgAKACIAOgA6ADoAOgAmABIAEgASABIAEgASABIAEgASABIAEgASABIAEgAOAApACgAKAAoACgAKEAoACgAKEAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAASBgEXGBUOgA6ADoAOgA6ADoAOgBGAFIAUgBSAFIAUgBSAFIAUgBSADYAGgAaABoAGgAaABoAGgAaABoAGgAaABIACgAKAAoACgAiADoAOgA6ADoAJgASABIAEgASABIAEgASABIAEgASABIAEgASABIADgAKQAoACgAKAAoAChAKAAoAChAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABIGARcXIRSAFIAUgBSAFIAUgBSAFIAUgBSABoAGgAaABoAGgAaABoAGgAaABoAGgAaAAoACgAKAAoACgA6ADoAOgA6ADoAEgASABIAEgASABIAEgASABIAEgASABIAEgASABIACkAKQAoACgAKQAoQChAKAAoAChAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEgYBFxZIBoAGgAaABoAGgAaABoAGgAaABoAGgAaAAoACgAKAAoACgA6ADoAOgA6ADoAEgASABIAEgASABIAEgASABIAEgASABIAEgASABIACkAKQAoACgAKQAoQChAKAAoAChAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAASBgEWBgYCgAKAAoACgAKADoAOgA6ADoAOgASABIAEgASABIAEgASABIAEgASABIAEgASABIAEgAKQApACgAKAApAChAKEAoACgAKEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABIGARVYMg6ADoAOgA6ADoAEgASABIAEgASABIAEgASABIAEgASABIAEgASABIACkAKQAoACgAKQAoQChAKAAoAChAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEgYBFTESBIAEgASABIAEgASABIAEgASABIAEgASABIAEgASAApACkAKAAoACkAKEAoQCgAKAAoQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAASBgEUVDgCkAKQAoACgAKQAoQChAKAAoAChAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABIGARQYMQKEAoQCgAKAAoQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEgYBEzYxAoACgAKAAoACgAKAApQCgAKAAoAClAuAFIAUgBSAFIAPgAqACoAKgAqACoAKgAqACoAKgAqACoAKgAqACoAKgAqACoAKgAqACoAKgAqACoACgAKAAoACgAKACIAIgAiACIAIgAiACIAIgAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADN";
		byte[] byte2 = null;
//		String decode1 = null;
		byte[] command = null;
		try {
//		    decode1 = 	BASE64Utils.base64Decode(con);
			long s1 = System.currentTimeMillis();
			byte2 = BASE64Utils.base64DecodeToArray(con);
			long s2 = System.currentTimeMillis();
//			sun.misc.BASE64Decoder decoder = new sun.misc.BASE64Decoder();
//			command = decoder.decodeBuffer(con);
//			command = BASE64Utils.base64DecodeToArray(con);
			long s3 = System.currentTimeMillis();
			System.out.println("base64DecodeToArray耗时:["+(s2-s1)+"]ms\r\nBASE64Decoder耗时：[" + (s3-s2) +"]ms");
			
		} catch (Exception e) {
 			e.printStackTrace();
		}
		System.out.println("原始内容：" + Converser.bytesToHexString(command));
		System.out.println("base64DecodeToArray\r\n原始内容：" + Converser.bytesToHexString(byte2));
//		System.out.println("base64Decode\r\n原始内容：" + decode1);
		System.out.println(CDate.getUserDate("yyyy").substring(0,2));
	}

}
