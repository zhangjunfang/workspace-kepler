package com.ctfo.savecenter.dao;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.sql.Types;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.util.CDate;
import com.ctfo.savecenter.util.Utils;
import com.encryptionalgorithm.Point;
import com.lingtu.xmlconf.XmlConf;

public class CommandManagerMysqlDBAdapter {
	private static final Logger logger = LoggerFactory.getLogger(CommandManagerMysqlDBAdapter.class);

	/** mysql存储控制指令 */
	private PreparedStatement mysqlstSaveDownControlCommand;

	/** mysql更新控制指令 */
	private PreparedStatement mysqlstUpdateUpControlCommand;

	/** mysql存储车辆照片 */
	private PreparedStatement stupdateVehiclePicture;
	
	private String saveDownControlCommand = "";
	
	private String updateUpControlCommand = "";
	
	private String saveVehiclePicture = "";
	
	/**
	 * 构造函数
	 * 
	 * @param dbDriver
	 *            数据库驱动
	 * @param dbConString
	 *            数据库连接字
	 * @param dbUserName
	 *            数据库用户名
	 * @param dbPassword
	 *            数据库密码
	 * @param reconnectWait
	 *            数据库断线重新连接时间(秒)
	 */
	public void initDBAdapter(XmlConf config, String nodeName) throws Exception {
	
		// 存储控制指令
		saveDownControlCommand = config.getStringValue(nodeName + "|mysql_sql_saveDownControlCommand");

		// 更新控制指令
		updateUpControlCommand = config.getStringValue(nodeName + "|mysql_sql_updateUpControlCommand");

		// 存储车辆照片信息
		saveVehiclePicture = config.getStringValue(nodeName + "|mysql_sql_updateVehiclePicture");
	}
	
	/**
	 * mysql存储控制指令
	 * @throws SQLException 
	 * @throws Exception 
	 * 
	 */
	public void mysqlSaveControlCommand(Map<String,String> app) throws SQLException{
		Connection mysqldbCon = null;
		try {
			mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
			mysqlstSaveDownControlCommand = mysqldbCon.prepareStatement(saveDownControlCommand);
			
			String seq = app.get(Constant.SEQ);
			int opId = Integer.parseInt(seq.split("_")[0]);
			
			mysqlstSaveDownControlCommand.setInt(1, opId);
			mysqlstSaveDownControlCommand.setLong(2, Long.parseLong(app.get(Constant.VID)));
			mysqlstSaveDownControlCommand.setString(3, app.get(Constant.VEHICLENO)); // 车牌号
			mysqlstSaveDownControlCommand.setLong(4,System.currentTimeMillis());
			mysqlstSaveDownControlCommand.setString(5, app.get(Constant.MTYPE));
			mysqlstSaveDownControlCommand.setString(6, seq);
			mysqlstSaveDownControlCommand.setString(7, app.get(Constant.CHANNEL));
			mysqlstSaveDownControlCommand.setString(8, app.get(Constant.CONTENT));
			mysqlstSaveDownControlCommand.setString(9, app.get(Constant.COMMAND));
			mysqlstSaveDownControlCommand.setString(10, app.get(Constant.OEMCODE));
			mysqlstSaveDownControlCommand.setString(11, app.get("TYPE"));
			mysqlstSaveDownControlCommand.setLong(12, opId);
			mysqlstSaveDownControlCommand.setLong(13,System.currentTimeMillis());
			mysqlstSaveDownControlCommand.executeUpdate();
		} catch (SQLException e) {
			logger.error("MYSQL存储控制指令."+ e.getMessage());
		}finally{
			if(mysqlstSaveDownControlCommand != null){
				mysqlstSaveDownControlCommand.close();
			}
			
			if(mysqldbCon != null){
				mysqldbCon.close();
			}
		}
	}

	/****
	 * MYSQL更新控制指令
	 * @param app
	 * @throws SQLException
	 */
	public void mysqlUpdateControlCommand(Map<String,String> app)throws SQLException {
		Connection mysqldbCon = null;
		try{
			
			mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
			mysqlstUpdateUpControlCommand = mysqldbCon.prepareStatement(updateUpControlCommand);
			
			int status = Integer.parseInt( app.get("RET"));
			String seq = app.get(Constant.SEQ);
			mysqlstUpdateUpControlCommand.setInt(1, status);
			mysqlstUpdateUpControlCommand.setLong(2,System.currentTimeMillis());
			mysqlstUpdateUpControlCommand.setString(3, app.get(Constant.CONTENT));
			mysqlstUpdateUpControlCommand.setString(4, seq);
			mysqlstUpdateUpControlCommand.executeUpdate();
		}catch(SQLException e){
			logger.error("MYSQL更新控制指令."+ e.getMessage());
		}finally{
			if(mysqlstUpdateUpControlCommand != null){
				mysqlstUpdateUpControlCommand.close();
			}
			if(mysqldbCon != null){
				mysqldbCon.close();
			}
		}
	}

	/****
	 * 保存车辆照片
	 * @param app
	 * @throws SQLException
	 */
	public void mysqlsaveVehiclePicture(Map<String,String> app)throws SQLException {
		if(!app.containsKey("125")){
			return;
		}
		Connection mysqldbCon = null;
		try{
			mysqldbCon = DriverManager.getConnection(Constant.MYSQL_POOL_SUFFIX);
			stupdateVehiclePicture = mysqldbCon.prepareStatement(saveVehiclePicture);
			if( app.containsKey("121")){ // 多媒体类型
				stupdateVehiclePicture.setString(1, app.get("121"));
			}else{
				stupdateVehiclePicture.setString(1, null);
			}
			
			stupdateVehiclePicture.setLong(2, CDate.getCurrentUtcMsDate());
			
			if(app.containsKey("125")){ // 多媒体URL
				stupdateVehiclePicture.setString(3, app.get("125"));
			}else{
				stupdateVehiclePicture.setString(3,null);
			}
			
			if(app.containsKey("124")){// 通道号
				stupdateVehiclePicture.setString(4, app.get("124"));
			}else{
				stupdateVehiclePicture.setString(4,  null); 
			}
			
			if(app.containsKey("1") && app.containsKey("2")){
				long lon = Long.parseLong(app.get("1"));
				long lat = Long.parseLong(app.get("2"));
				stupdateVehiclePicture.setLong(5, lat);
				stupdateVehiclePicture.setLong(6, lon);
				Point point = Utils.convertLatLon(lon, lat);
				if(point != null){
					stupdateVehiclePicture.setLong(7, Math.round(point.getX() * 600000));
					stupdateVehiclePicture.setLong(8, Math.round(point.getY() * 600000));
				}else{
					stupdateVehiclePicture.setNull(7,Types.INTEGER);
					stupdateVehiclePicture.setNull(8, Types.INTEGER);
				}
			}else{
				stupdateVehiclePicture.setNull(5,Types.INTEGER);
				stupdateVehiclePicture.setNull(6, Types.INTEGER);
				stupdateVehiclePicture.setNull(7,Types.INTEGER);
				stupdateVehiclePicture.setNull(8, Types.INTEGER);
			}
			
			if( app.containsKey("6")){ // 海拔高度
				stupdateVehiclePicture.setInt(9, Integer.parseInt(app.get("6")));
			}else{
				stupdateVehiclePicture.setNull(9, Types.INTEGER);
			}
			
			if( app.containsKey("5")){ // 方向
				stupdateVehiclePicture.setInt(10, Integer.parseInt(app.get("5")));
			}else{
				stupdateVehiclePicture.setNull(10, Types.INTEGER);
			}
			
			if( app.containsKey("3")){ // 速度
				stupdateVehiclePicture.setInt(11, Integer.parseInt(app.get("3")));
			}else{
				stupdateVehiclePicture.setNull(11, Types.INTEGER);
			}
			
			// 0：平台下发指令，1：定时动作，2：抢劫报警触发，3：碰撞侧翻报警触发，4：门开拍照，5：门关拍照，6：车门由开变关，时速从＜20公里超过20公里
			if( app.containsKey("123")){
				stupdateVehiclePicture.setString(12,  app.get("123")); 
			}else{
				stupdateVehiclePicture.setString(12,  null);
			}
			
			stupdateVehiclePicture.setString(13, app.get(Constant.UUID));
			stupdateVehiclePicture.setLong(14, Long.parseLong(app.get(Constant.VID)));
			stupdateVehiclePicture.setString(15, app.get(Constant.MACID).split("_",2)[1]);
			if(app.get("8") != null){
				stupdateVehiclePicture.setString(16, app.get("8"));
			}else{
				stupdateVehiclePicture.setString(16, null);
			}
			
			// 事件触发时间（yyMMDDHHmmss（GMT+8时间）；记录多媒体事件触发的时间）
			if(app.get("126") != null){
				stupdateVehiclePicture.setLong(17, CDate.shortDateConvertUtc(app.get("126")));
			}else{
				stupdateVehiclePicture.setNull(17, Types.INTEGER);
			}
			
			stupdateVehiclePicture.executeUpdate();
		}finally{
			if(stupdateVehiclePicture != null){
				stupdateVehiclePicture.close();
			}
			if(mysqldbCon != null){
				mysqldbCon.close();
			}
		}
	}
}
