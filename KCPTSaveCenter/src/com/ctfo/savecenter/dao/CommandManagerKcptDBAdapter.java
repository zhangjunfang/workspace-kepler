package com.ctfo.savecenter.dao;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Types;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import oracle.jdbc.OracleConnection;
import oracle.jdbc.OraclePreparedStatement;
import oracle.jdbc.OracleResultSet;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.redis.pool.JedisConnectionPool;
import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.beans.EngineFaultInfo;
import com.ctfo.savecenter.beans.QualityRecordBean;
import com.ctfo.savecenter.connpool.OracleConnectionPool;
import com.ctfo.savecenter.util.Base64_URl;
import com.ctfo.savecenter.util.CDate;
import com.ctfo.savecenter.util.Utils;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;
import com.lingtu.xmlconf.XmlConf;

public class CommandManagerKcptDBAdapter {

	private static final Logger logger = LoggerFactory.getLogger(CommandManagerKcptDBAdapter.class);

	// 数据库连接对象
//	private OracleConnection conn;

	// 多媒体连接对象
	private OracleConnection mediaConn = null;

	// 多媒体事件连接
	private OracleConnection mediaEventConn = null;

	// 存储鉴权连接
	private OracleConnection vehicleAKeyConn = null;
	
	// 存储质检单连接
	private OracleConnection qualityRecordConn = null;

	/** 多媒体事件指令 */
	private OraclePreparedStatement stSaveMultimediaEvent = null;

	/** 存储控制指令 */
	private OraclePreparedStatement stSaveDownControlCommand;

	/** 更新控制指令 */
	private OraclePreparedStatement stUpdateUpControlCommand;

	/** 更新车辆拍照信息 */
	private OraclePreparedStatement stUpdateVehiclePicture;

	/** 存储车辆调度信息 ***/
	private OraclePreparedStatement stSaveVehicleDispatchMsg;

	/** 更新车辆调度信息 ***/
	private OraclePreparedStatement stUpdateVehicleDispatchMsg;

	/*** 更新终端参数历史 ***/
	private OraclePreparedStatement stUpdateHisTerminalParam;

	/** 更新电子围栏状态 **/
	private OraclePreparedStatement stUpdateAreaSetting;

	/** 更新线路状态 **/
	private OraclePreparedStatement stUpdateLineSetting;

	/** 查询指令子类型 **/
	private OraclePreparedStatement stQuerySubType;

	/** 更新终端参数信息 **/
	private OraclePreparedStatement stUpdateTerminalParam;

	/*** 核实终端参数设置 ****/
	private OraclePreparedStatement stCheckTerminalParam;

	/*** 存储终端参数设置 **/
	private OraclePreparedStatement stSaveTerminalParam;

	/*** 查询终端版本号 **/
	private OraclePreparedStatement stCheckTerminalVersion = null;

	/*** 更新终端版本号 **/
	private OraclePreparedStatement stUpdateTerminalVersion = null;

	/**** 更新终端升级状态 ***/
	private OraclePreparedStatement stUpdateTerminalUpdaeInfoVersion = null;

	/**** 存储终端注册 *****/
	private OraclePreparedStatement stSaveTerminalRegister = null;

	/***** 存储终端鉴权 *****/
	private OraclePreparedStatement stSaveVehicleAKey = null;

	/***** 存储终端注销 *****/
	private OraclePreparedStatement stSaveVehicleLogOff = null;

	/******* 存储多媒体检索信息 ****************/
	private OraclePreparedStatement stSaveMediaIdx = null;

	/******* 存储多媒体 ****************/
	private OraclePreparedStatement stSaveMultMedia = null;

	/******* 存储事件ID ****************/
	private OraclePreparedStatement stSaveEventId = null;

	/******* 更新历史提问 ****************/
	private OraclePreparedStatement stUpdateQuerstionAnswer = null;

	/******* 存储电子运单历史 ****************/
	private OraclePreparedStatement stSaveEticket = null;

	/******* 存储数据上行透传 ****************/
	private OraclePreparedStatement stSaveBridge = null;

	/******* 存储数据压缩上传 ****************/
	private OraclePreparedStatement stSaveCompress = null;

	/******* 存储行驶记录仪 ****************/
	private OraclePreparedStatement stSaveRecorder = null;

	/********* 更新行驶记录表 ******************/
	private OraclePreparedStatement stUpdateRecorder = null;

	/******* 存储信息点播取消 ****************/
	private OraclePreparedStatement stSaveInfoplay = null;

	/******* 更新车辆注销 ****************/
	private OraclePreparedStatement stUpdateVehicleLogOff = null;
	
	/** 存储质检单信息*/
	private OraclePreparedStatement qualityRecordStatement = null;

	// 存储控制指令
	private String saveDownControlCommand = null;

	// 更新控制指令
	private String updateUpControlCommand = null;

	// 更新车辆照片信息
	private String updateVehiclePicture = null;

	// 存储车辆调度信息
	private String saveVehicleDispatchMsg = null;

	// 更新车辆调度信息
	private String updateVehicleDispatchMsg = null;

	// 更新终端参数历史信息
	private String updateTerminalParam = null;

	// 更新终端参数信息
	private String sql_updateTerminalParam = null;

	// 更新电子围栏状态
	private String sql_updateAreaSetting = null;

	// 更新线路状态表
	private String sql_updateLineSetting = null;

	// 核实终端参数设置
	private String sql_checkTerminalParam = null;

	// 存储终端参数设置
	private String sql_saveTerminalParam = null;

	// 查询指令子类型
	private String sql_querySubType = null;

	// 查询终端版本升级号
	private String sql_checkTerminalVersion = null;

	// 更新终端版本号
	private String sql_updateTerminalVersion = null;

	// 更新终端升级状态
	private String sql_updateTerminalUpdaeInfoVersion = null;

	// 存储终端注册
	private String sql_saveVehicleRegister = null;

	// 存储终端鉴权
	private String sql_saveVehicleAKey = null;

	// 存储终端注销
	private String sql_saveVehicleLogOff = null;

	// 存储多媒体事件信息
	private String sql_saveMultimediaEvent = null;

	// 存储驾驶员身份信息
	private String sql_saveDriverInfo = null;

	// 存储电子运单历史
	private String sql_saveEticket = null;

	// 多媒体检索信息SQL
	private String sql_saveMediaIdx = null;

	// 多媒体数据
	private String sql_saveMultMedia = null;

	// 事件ID
	private String sql_saveEventId = null;

	// 更新历史提问
	private String sql_updateQuerstionAnswer = null;

	// 存储数据上行透传
	private String sql_saveBridge = null;

	// 存储数据压缩上传
	private String sql_saveCompress = null;

	// 存储行驶记录仪
	private String sql_saveRecorder = null;

	// 更新行驶记录表
	private String sql_updateRecorder = null;

	// 存储信息点播取消
	private String sql_saveInfoplay = null;

	// 更新车辆注销信息
	private String sql_updateVehicleLogOff = null;

	// 更新触发拍照状态表
	private String sql_updatePhotoSetting = null;

	// 存储油量变化记录
	private String sql_saveOilChanged = null;

	// 存储驾驶员登录信息
	private String sql_saveDriverLoginInfo = null;

	// 查询驾驶员信息
	private String sql_selectDriverInfo = null;

	// 更新SIM卡ICCID
	private String sql_updateSIMiccid = null;

	// 存储偷油告警
	private String sql_saveStealOilAlarm = null;

	/** 存储发动机故障信息  */
	private String sql_saveEngineFaultInfo = null;
	
	/** 更新发动机故障处理信息  */
	private String sql_updateEngBugDispose = null;
	
	/** 存储发动机版本信息  */
	private String  sql_saveEngVersionInfo = null;
	
	/** 删除发动机版本信息  */
	private String  sql_deleteEngVersionInfo = null;
	/** 插入或者更新锁车信息  */
	private String sql_saveOrUpdateLockVehicleDetail;
	/** 更新解锁码  */
	private String sql_updateUnlockCode;
	/** 更新锁车状态  */
	private String sql_updateLockVehicleStatusStatement;
	/** 保存质检单信息*/
	private String saveQualityRecordSql;
	
	// 指令批量数据库提交数
	private int commitCommandCount = 0;

	// 多媒体批量数据库提交数
	private int commitMediaCount = 0;

	/***
	 * 初始化SQL参数
	 */
	public void initDBAdapter(XmlConf config, String nodeName) throws Exception {

		// 存储控制指令
		saveDownControlCommand = config.getStringValue(nodeName + "|sql_saveDownControlCommand");

		// 更新控制指令
		updateUpControlCommand = config.getStringValue(nodeName + "|sql_updateUpControlCommand");

		// 更新车辆照片信息
		updateVehiclePicture = config.getStringValue(nodeName + "|sql_updateVehiclePicture");

		// 存储车辆调度信息
		saveVehicleDispatchMsg = config.getStringValue(nodeName + "|sql_saveVehicleDispatchMsg");

		// 更新车辆调度信息
		updateVehicleDispatchMsg = config.getStringValue(nodeName + "|sql_updateVehicleDispatchMsg");

		// 更新终端参数历史信息
		updateTerminalParam = config.getStringValue(nodeName + "|sql_updateTerminalParam");

		// 更新终端参数信息
		sql_updateTerminalParam = config.getStringValue(nodeName + "|sql_updateTerminalParamInfo");

		// 更新电子围栏状态
		sql_updateAreaSetting = config.getStringValue(nodeName + "|sql_updateAreaSetting");

		// 更新线路状态
		sql_updateLineSetting = config.getStringValue(nodeName + "|sql_updateLineSetting");

		// 核实终端参数设置
		sql_checkTerminalParam = config.getStringValue(nodeName + "|sql_checkTerminalParam");

		// 存储终端参数设置
		sql_saveTerminalParam = config.getStringValue(nodeName + "|sql_saveTerminalParam");

		// 查询指令子类型
		sql_querySubType = config.getStringValue(nodeName + "|sql_querySubType");

		// 查询终端版本升级号
		sql_checkTerminalVersion = config.getStringValue(nodeName + "|sql_checkTerminalUpdaeInfoVersion");

		// 更新终端版本号
		sql_updateTerminalVersion = config.getStringValue(nodeName + "|sql_updateTerminalVersion");

		// 更新终端版本升级状态
		sql_updateTerminalUpdaeInfoVersion = config.getStringValue(nodeName + "|sql_updateTerminalUpdaeInfoVersion");

		// 存储终端注册
		sql_saveVehicleRegister = config.getStringValue(nodeName + "|sql_saveVehicleRegister");

		// 存储终端鉴权
		sql_saveVehicleAKey = config.getStringValue(nodeName + "|sql_saveVehicleAKey");

		// 存储终端注销
		sql_saveVehicleLogOff = config.getStringValue(nodeName + "|sql_saveVehicleLogOff");

		// 存储多媒体事件信息
		sql_saveMultimediaEvent = config.getStringValue(nodeName + "|sql_saveMultimediaEvent");

		// 存储驾驶员身份信息
		sql_saveDriverInfo = config.getStringValue(nodeName + "|sql_saveDriverInfo");

		// 多媒体检索信息
		sql_saveMediaIdx = config.getStringValue(nodeName + "|sql_saveMediaIdx");

		// 多媒体数据
		sql_saveMultMedia = config.getStringValue(nodeName + "|sql_saveMultMedia");

		// 电子路单
		sql_saveEticket = config.getStringValue(nodeName + "|sql_saveEticket");

		// 事件ID
		sql_saveEventId = config.getStringValue(nodeName + "|sql_saveEventId");

		// 更新历史提问
		sql_updateQuerstionAnswer = config.getStringValue(nodeName + "|sql_updateQuerstionAnswer");

		// 数据上行透传
		sql_saveBridge = config.getStringValue(nodeName + "|sql_saveBridge");

		// 数据压缩上传
		sql_saveCompress = config.getStringValue(nodeName + "|sql_saveCompress");

		// 存储行驶记录仪
		sql_saveRecorder = config.getStringValue(nodeName + "|sql_saveRecorder");

		// 更新行驶记录表SQL
		sql_updateRecorder = config.getStringValue(nodeName + "|sql_updateRecorder");

		// 存储信息点播取消
		sql_saveInfoplay = config.getStringValue(nodeName + "|sql_saveInfoplay");

		// 更新车辆注销
		sql_updateVehicleLogOff = config.getStringValue(nodeName + "|sql_updateVehicleLogOff");

		// 更新车辆触发拍照状态
		sql_updatePhotoSetting = config.getStringValue(nodeName + "|sql_updatePhotoSetting");

		// 存储油量变化记录
		sql_saveOilChanged = config.getStringValue(nodeName + "|sql_saveOilChanged");

		// 存储驾驶员登录信息
		sql_saveDriverLoginInfo = config.getStringValue(nodeName + "|sql_saveDriverLoginInfo");

		// 查询驾驶员信息
		sql_selectDriverInfo = config.getStringValue(nodeName + "|sql_selectDriverInfo");

		// 更新SIM卡ICCID
		sql_updateSIMiccid = config.getStringValue(nodeName + "|sql_updateSIMiccid");

		// 存储偷油告警
		sql_saveStealOilAlarm = config.getStringValue(nodeName + "|sql_saveStealOilAlarm");

		/** 存储发动机故障信息  */
		sql_saveEngineFaultInfo = config.getStringValue(nodeName + "|sql_saveEngineFaultInfo");
		
		/** 更新发动机故障处理信息  */
		sql_updateEngBugDispose = config.getStringValue(nodeName + "|sql_updateEngBugDispose");
		
		/** 存储发动机版本信息  */
		sql_saveEngVersionInfo = config.getStringValue(nodeName + "|sql_saveEngVersionInfo");
		
		/** 删除发动机版本信息  */
		sql_deleteEngVersionInfo = config.getStringValue(nodeName + "|sql_deleteEngVersionInfo");
		/** 插入或者更新锁车信息  */
		sql_saveOrUpdateLockVehicleDetail = config.getStringValue(nodeName + "|sql_saveOrUpdateLockVehicleDetail");
		/** 更新解锁码  */
		sql_updateUnlockCode = config.getStringValue(nodeName + "|sql_updateUnlockCode");
		/** 更新锁车状态  */
		sql_updateLockVehicleStatusStatement = config.getStringValue(nodeName + "|sql_updateLockVehicleStatusStatement");
		
		saveQualityRecordSql = config.getStringValue(nodeName + "|sql_saveQualityRecord");
		
		// 指令批量数据库提交数
		commitCommandCount = config.getIntValue(nodeName + "|commitCommandCount");

		// 多媒体批量数据库提交数
		commitMediaCount = config.getIntValue(nodeName + "|commitMediaCount");
	}

	public void createStatement() {
		try {
			/** ------存储多媒体事件指令 ------ */
			
			mediaEventConn = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveMultimediaEvent = (OraclePreparedStatement) mediaEventConn.prepareStatement(sql_saveMultimediaEvent);
			stSaveMultimediaEvent.setExecuteBatch(commitMediaCount);

			/** ------存储多媒体 ------ */
			mediaConn = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveMultMedia = (OraclePreparedStatement) mediaConn.prepareStatement(sql_saveMultMedia);
			stSaveMultMedia.setExecuteBatch(commitMediaCount);

			/** --------存储终端鉴权 -------- */
			vehicleAKeyConn = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveVehicleAKey = (OraclePreparedStatement) vehicleAKeyConn.prepareStatement(sql_saveVehicleAKey);
			stSaveVehicleAKey.setExecuteBatch(commitCommandCount);

		} catch (SQLException e) {
			logger.error("oracle初始化指令控制线程statement出错.", e);
		}
	}

	/*****************************************
	 * <li>描        述：批量提交 		</li><br>
	 * <li>时        间：2013-6-19  下午3:13:14	</li><br>
	 * <li>参数： @throws SQLException			</li><br>
	 * 
	 ****************************************/
	public void commit() throws SQLException {
//		提交多媒体事件
		try {
			stSaveMultimediaEvent.sendBatch();
		} catch (Exception e) {
			logger.error("提交多媒体事件:"+e.getMessage(),e);
			try {
				mediaEventConn.getMetaData();
				if (stSaveMultimediaEvent == null) {
					stSaveMultimediaEvent = createStatement(mediaEventConn, commitMediaCount, sql_saveMultimediaEvent);
				}
			} catch (Exception ex) {
				stSaveMultimediaEvent = recreateStatement(mediaEventConn, commitMediaCount, sql_saveMultimediaEvent);
			}
		}
//		提交多媒体信息
		try {
			stSaveMultMedia.sendBatch();
//			logger.info("提交多媒体事件完成:" + i);
		} catch (Exception e) {
			logger.error("提交多媒体信息异常:"+e.getMessage(),e);
			try {
				mediaConn.getMetaData();
				if (stSaveMultMedia == null) {
					stSaveMultMedia = createStatement(mediaConn, commitMediaCount, sql_saveMultMedia);
				}
			} catch (Exception ex) {
				stSaveMultMedia = recreateStatement(mediaConn, commitMediaCount, sql_saveMultMedia);
			}
		}
//		提交车辆鉴权信息
		try {
			stSaveVehicleAKey.sendBatch();
		} catch (Exception e) {
			logger.error("提交车辆鉴权信息异常:"+e.getMessage(),e);
			try {
				vehicleAKeyConn.getMetaData();
				if (stSaveVehicleAKey == null) {
					stSaveVehicleAKey = createStatement(vehicleAKeyConn, 1, sql_saveVehicleAKey);
				}
			} catch (Exception ex) {
				stSaveVehicleAKey = recreateStatement(vehicleAKeyConn, 1, sql_saveVehicleAKey);
			}
		}
	}

	/*****************************************
	 * <li>描 述：创建 oracle链接语句</li><br>
	 * <li>时 间：2013-6-19 下午1:48:06</li><br>
	 * <li>参数： @param dbCon <li>参数： @param count <li>参数： @param sql <li>参数： @return
	 * </li><br>
	 * 
	 *****************************************/
	private OraclePreparedStatement createStatement(OracleConnection dbCon, int count, String sql) {
		// 轨迹包更新最后位置到数据库
		OraclePreparedStatement stat = null;
		try {
			stat = (OraclePreparedStatement) dbCon.prepareStatement(sql);
			stat.setExecuteBatch(count);
		} catch (SQLException e) {
			logger.error(Constant.SPACES,e);
		}
		return stat;
	}

	/*****************************************
	 * <li>描 述：重新创建语句</li><br>
	 * <li>时 间：2013-6-19 下午1:49:29</li><br>
	 * <li>参数： @param dbCon <li>参数： @param count <li>参数： @param sql <li>参数： @return
	 * </li><br>
	 * 
	 *****************************************/
	private OraclePreparedStatement recreateStatement(OracleConnection dbCon, int count, String sql) {
		OraclePreparedStatement stat = null;
		try {
			if (dbCon != null) {
				dbCon.close();
				dbCon = null;
			}
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();

			// 轨迹包更新最后位置到数据库
			stat = (OraclePreparedStatement) dbCon.prepareStatement(sql);
			stat.setExecuteBatch(count);
			logger.info("Create statement successfully!");
		} catch (SQLException e) {
			logger.error("Create statement error", e);
		}
		return stat;
	}

	/**
	 * 存储控制指令
	 * 
	 */
	public void saveControlCommand(Map<String, String> app) throws SQLException {
		ResultSet rs = null;
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveDownControlCommand = (OraclePreparedStatement) dbCon.prepareStatement(saveDownControlCommand);

			String seq = app.get(Constant.SEQ);
			int opId = Integer.parseInt(seq.split("_")[0]);
			stSaveDownControlCommand.setExecuteBatch(1);

			stSaveDownControlCommand.setInt(1, opId);
			stSaveDownControlCommand.setLong(2, Long.parseLong(app.get(Constant.VID)));
			stSaveDownControlCommand.setString(3, app.get(Constant.VEHICLENO)); // 车牌号码
			stSaveDownControlCommand.setLong(4, CDate.getCurrentUtcMsDate());
			stSaveDownControlCommand.setString(5, app.get(Constant.MTYPE));
			if (app.get(Constant.MTYPE).equals("L_PROV")) {
				stSaveDownControlCommand.setInt(6, 1); // 指令来源 1监管平台
			} else {
				stSaveDownControlCommand.setInt(6, 0); // 指令来源 0本平台
			}
			stSaveDownControlCommand.setString(7, app.get(Constant.SEQ));
			stSaveDownControlCommand.setString(8, app.get(Constant.CHANNEL));
			stSaveDownControlCommand.setString(9, app.get(Constant.CONTENT));
			stSaveDownControlCommand.setString(10, app.get(Constant.COMMAND));
			stSaveDownControlCommand.setString(11, app.get("TYPE"));
			stSaveDownControlCommand.setLong(12, opId);
			stSaveDownControlCommand.setLong(13, CDate.getCurrentUtcMsDate()); // 创建时间
			stSaveDownControlCommand.setString(14, app.get(Constant.OEMCODE));
			stSaveDownControlCommand.executeUpdate();
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stSaveDownControlCommand != null) {
				stSaveDownControlCommand.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/****
	 * 更新控制指令
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void updateControlCommand(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateUpControlCommand = (OraclePreparedStatement) dbCon.prepareStatement(updateUpControlCommand);

			int status = Integer.parseInt(app.get("RET"));
			String seq = app.get(Constant.SEQ);
			stUpdateUpControlCommand.setExecuteBatch(1);

			stUpdateUpControlCommand.setInt(1, status); // 发送状态-1等待回应 0:成功
														// 1:设备返回失败 2:发送失败
														// 3:设备不支持此功能 4:设备不在线 5:超时
			stUpdateUpControlCommand.setLong(2, CDate.getCurrentUtcMsDate());
			stUpdateUpControlCommand.setString(3, app.get(Constant.CONTENT));
			stUpdateUpControlCommand.setString(4, seq);
			stUpdateUpControlCommand.executeUpdate();

			String subType = querySubType(seq);
			if (subType != null) {
				if (subType.equals("14")) {// 围栏设置
					updateAreaSetting(seq, status);
				} else if (subType.equals("15")) { // 线路设置
					updateLineSetting(seq, status);
				} else if (subType.equals("11") || subType.equals("0")) { // 终端参数设置
					updateTerminalHisParam(seq, status);
					updatePhotoSetting(seq, status); // 更新触发拍照
				}
			}
		} finally {
			if (stUpdateUpControlCommand != null) {
				stUpdateUpControlCommand.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/******
	 * 更新触发拍照状态表
	 * 
	 * @param seq
	 * @param status
	 * @throws SQLException
	 */
	private void updatePhotoSetting(String seq, int status) throws SQLException {
		OracleConnection dbCon = null;
		OraclePreparedStatement stUpdatePhotoSetting = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection)OracleConnectionPool.getConnection();
			stUpdatePhotoSetting = (OraclePreparedStatement) dbCon.prepareStatement(sql_updatePhotoSetting);
			stUpdatePhotoSetting.setExecuteBatch(1);
			stUpdatePhotoSetting.setInt(1, status);
			stUpdatePhotoSetting.setString(2, seq);
			stUpdatePhotoSetting.executeUpdate();
		} finally {
			if (stUpdatePhotoSetting != null) {
				stUpdatePhotoSetting.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/***
	 * 更新电子围栏状态
	 * 
	 * @param packetBean
	 * @throws SQLException
	 */
	private void updateLineSetting(String seq, int status) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateLineSetting = (OraclePreparedStatement) dbCon.prepareStatement(sql_updateLineSetting);
			stUpdateLineSetting.setExecuteBatch(1);
			stUpdateLineSetting.setInt(1, status);
			stUpdateLineSetting.setLong(2, CDate.getCurrentUtcMsDate());
			stUpdateLineSetting.setString(3, seq);
			stUpdateLineSetting.executeUpdate();
		} finally {
			if (stUpdateLineSetting != null) {
				stUpdateLineSetting.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/***
	 * 更新电子围栏状态
	 * 
	 * @param packetBean
	 * @throws SQLException
	 */
	private void updateAreaSetting(String seq, int status) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateAreaSetting = (OraclePreparedStatement) dbCon.prepareStatement(sql_updateAreaSetting);
			stUpdateAreaSetting.setExecuteBatch(1);
			stUpdateAreaSetting.setInt(1, status);
			stUpdateAreaSetting.setLong(2, CDate.getCurrentUtcMsDate());
			stUpdateAreaSetting.setString(3, seq);
			stUpdateAreaSetting.executeUpdate();
		} finally {
			if (stUpdateAreaSetting != null) {
				stUpdateAreaSetting.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/***
	 * 更新车辆照片
	 * 
	 * @param packetBean
	 * @throws SQLException
	 */
	public void saveVehiclePicture(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			if (!app.containsKey("121")) { // 上传不包含图片不保存数据
				return;
			}
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateVehiclePicture = (OraclePreparedStatement) dbCon.prepareStatement(updateVehiclePicture);
			stUpdateVehiclePicture.setExecuteBatch(1);

			if (app.containsKey("121")) { // 多媒体类型
				stUpdateVehiclePicture.setString(1, app.get("121"));
			} else {
				stUpdateVehiclePicture.setString(1, null);
			}

			stUpdateVehiclePicture.setLong(2, CDate.getCurrentUtcMsDate());

			if (app.containsKey("125")) { // 多媒体URL
				stUpdateVehiclePicture.setString(3, app.get("125"));
			} else {
				stUpdateVehiclePicture.setString(3, null);
			}

			if (app.containsKey("124")) {// 通道号
				stUpdateVehiclePicture.setString(4, app.get("124"));
			} else {
				stUpdateVehiclePicture.setString(4, null);
			}

			if (app.containsKey("1") && app.containsKey("2")) {
				long lon = Long.parseLong(app.get("1"));
				long lat = Long.parseLong(app.get("2"));
				stUpdateVehiclePicture.setLong(5, lat);
				stUpdateVehiclePicture.setLong(6, lon);
				Point point = Utils.convertLatLon(lon, lat);
				if (point != null) {
					stUpdateVehiclePicture.setLong(7, Math.round(point.getX() * 600000));
					stUpdateVehiclePicture.setLong(8, Math.round(point.getY() * 600000));
				} else {
					stUpdateVehiclePicture.setNull(7, Types.INTEGER);
					stUpdateVehiclePicture.setNull(8, Types.INTEGER);
				}
			} else {
				stUpdateVehiclePicture.setNull(5, Types.INTEGER);
				stUpdateVehiclePicture.setNull(6, Types.INTEGER);
				stUpdateVehiclePicture.setNull(7, Types.INTEGER);
				stUpdateVehiclePicture.setNull(8, Types.INTEGER);
			}

			if (app.containsKey("6")) { // 海拔高度
				stUpdateVehiclePicture.setInt(9, Integer.parseInt(app.get("6")));
			} else {
				stUpdateVehiclePicture.setNull(9, Types.INTEGER);
			}

			if (app.containsKey("5")) { // 方向
				stUpdateVehiclePicture.setInt(10, Integer.parseInt(app.get("5")));
			} else {
				stUpdateVehiclePicture.setNull(10, Types.INTEGER);
			}

			if (app.containsKey("3")) { // 速度
				stUpdateVehiclePicture.setInt(11, Integer.parseInt(app.get("3")));
			} else {
				stUpdateVehiclePicture.setNull(11, Types.INTEGER);
			}

			// 0：平台下发指令，1：定时动作，2：抢劫报警触发，3：碰撞侧翻报警触发，4：门开拍照，5：门关拍照，6：车门由开变关，时速从＜20公里超过20公里
			if (app.containsKey("16")) {
				stUpdateVehiclePicture.setString(12, app.get("16"));
			} else {
				stUpdateVehiclePicture.setString(12, null);
			}
			stUpdateVehiclePicture.setInt(13, 0);// 成功
			if (app.containsKey("122")) {// 多媒体格式
				stUpdateVehiclePicture.setString(14, app.get("122"));
			} else {
				stUpdateVehiclePicture.setString(14, null);
			}

			if (app.containsKey("123")) {// 事件项编码
				stUpdateVehiclePicture.setString(15, app.get("123"));
			} else {
				stUpdateVehiclePicture.setString(15, null);
			}

			stUpdateVehiclePicture.setString(16, app.get(Constant.VEHICLENO));

			stUpdateVehiclePicture.setLong(17, Long.parseLong(app.get(Constant.VID)));
			stUpdateVehiclePicture.executeUpdate();

		} finally {
			if (stUpdateVehiclePicture != null) {
				stUpdateVehiclePicture.close();
			}
			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/**
	 * 存储车辆调度信息
	 * 
	 * @param packetBean
	 * @throws SQLException
	 */
	public void saveVehicleDispatchMsg(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveVehicleDispatchMsg = (OraclePreparedStatement) dbCon.prepareStatement(saveVehicleDispatchMsg);
			stSaveVehicleDispatchMsg.setExecuteBatch(1);
			stSaveVehicleDispatchMsg.setLong(1, Long.parseLong(app.get(Constant.VID)));
			stSaveVehicleDispatchMsg.setString(2, app.get(Constant.VEHICLENO)); // 车牌号
			stSaveVehicleDispatchMsg.setLong(3, CDate.getCurrentUtcMsDate());
			stSaveVehicleDispatchMsg.setInt(4, 1);
			stSaveVehicleDispatchMsg.setString(5, app.get(Constant.SEQ));
			stSaveVehicleDispatchMsg.setString(6, app.get(Constant.PLATECOLORID));
			stSaveVehicleDispatchMsg.setLong(7, CDate.getCurrentUtcMsDate());
			stSaveVehicleDispatchMsg.setString(8, Base64_URl.base64Decode(app.get("17")));
			stSaveVehicleDispatchMsg.executeUpdate();
		} finally {

			if (stSaveVehicleDispatchMsg != null) {
				stSaveVehicleDispatchMsg.close();
			}
			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/**
	 * 更新车辆调度信息
	 * 
	 * @param packetBean
	 * @throws SQLException
	 */
	public void updateVehicleDispatchMsg(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateVehicleDispatchMsg = (OraclePreparedStatement) dbCon.prepareStatement(updateVehicleDispatchMsg);
			stUpdateVehicleDispatchMsg.setExecuteBatch(1);
			String seq = app.get(Constant.SEQ);
			int status = Integer.parseInt(app.get("RET"));
			stUpdateVehicleDispatchMsg.setLong(1, CDate.getCurrentUtcMsDate());
			stUpdateVehicleDispatchMsg.setInt(2, status); // 发送状态-1等待回应 0:成功
															// 1:设备返回失败 2:发送失败
															// 3:设备不支持此功能
															// 4:设备不在线
			stUpdateVehicleDispatchMsg.setInt(3, 0); // 是否已读 1-未读 0-已读
			stUpdateVehicleDispatchMsg.setString(4, seq);
			stUpdateVehicleDispatchMsg.executeUpdate();
		} finally {
			if (stUpdateVehicleDispatchMsg != null) {
				stUpdateVehicleDispatchMsg.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/**
	 * 更新终端参数历史
	 * 
	 * @param packetBean
	 * @throws SQLException
	 */
	private void updateTerminalHisParam(String seq, int status) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateHisTerminalParam = (OraclePreparedStatement) dbCon.prepareStatement(updateTerminalParam);
			stUpdateHisTerminalParam.setExecuteBatch(1);
			String opId = seq.split("_")[0];
			stUpdateHisTerminalParam.setInt(1, status);
			stUpdateHisTerminalParam.setLong(2, CDate.getCurrentUtcMsDate());
			stUpdateHisTerminalParam.setLong(3, Integer.parseInt(opId));
			stUpdateHisTerminalParam.setString(4, seq);
			stUpdateHisTerminalParam.executeUpdate();
		} finally {
			if (stUpdateHisTerminalParam != null) {
				stUpdateHisTerminalParam.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/***
	 * 查询指令子类型
	 * 
	 * @param seq
	 * @return
	 * @throws SQLException
	 */
	public String querySubType(String seq) throws SQLException {
		String subType = null;
		OracleResultSet rs = null;
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stQuerySubType = (OraclePreparedStatement) dbCon.prepareStatement(sql_querySubType);
			stQuerySubType.setExecuteBatch(1);
			stQuerySubType.setString(1, seq);
			rs = (OracleResultSet) stQuerySubType.executeQuery();
			if (rs.next()) {
				subType = rs.getString("CO_SUBTYPE");
			}
		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stQuerySubType != null) {
				stQuerySubType.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
		return subType;
	}

	/**
	 * 存储终端参数信息
	 * 
	 * @param packetBean
	 * @throws SQLException
	 */
	public void terminalParam(Map<String, String> app) throws SQLException {
		String seq = app.get(Constant.SEQ);
		String subType = querySubType(seq);
		if (subType != null && subType.equals("0")) { // 终端参数设置
			String content = app.get(Constant.CONTENT);
			String[] key_value = content.split(",");
			List<Integer> paramDBList = new ArrayList<Integer>();
			Long vid = Long.parseLong(app.get(Constant.VID));
			selectDBTerminalParam(paramDBList, vid);

			for (String kv : key_value) {
				String[] arr = kv.split(":");
				if (arr[0].equals("TYPE") || arr[0].equals("RET")) {
					continue;
				}
				String key = "";
				String value = "";
				key = arr[0];
				if (arr.length == 2) {
					value = arr[1];
				}
				int paramId = Integer.parseInt(key);
				if (paramDBList.contains(paramId)) {
					updateTernimalParam(vid, key, value);
				} else {
					saveTernimalParam(vid, key, value);
				}
			}// End for

			// 清空临时集合
			paramDBList.clear();
		}
	}

	/***
	 * 根据终端ID查询数据库终端参数值
	 * 
	 * @param param
	 * @param tid
	 * @throws SQLException
	 */
	private void selectDBTerminalParam(List<Integer> param, Long vid) throws SQLException {
		OracleResultSet rs = null;
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stCheckTerminalParam = (OraclePreparedStatement) dbCon.prepareStatement(sql_checkTerminalParam);

			stCheckTerminalParam.setLong(1, vid);

			rs = (OracleResultSet) stCheckTerminalParam.executeQuery();
			while (rs.next()) {
				param.add(rs.getInt("PARAM_ID"));
			} // End while

		} finally {
			if (rs != null) {
				rs.close();
			}

			if (stCheckTerminalParam != null) {
				stCheckTerminalParam.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/***
	 * 更新参数设置
	 * 
	 * @param tid
	 * @param paramKey
	 * @param value
	 * @throws SQLException
	 */
	private void updateTernimalParam(Long vid, String paramKey, String value) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateTerminalParam = (OraclePreparedStatement) dbCon.prepareStatement(sql_updateTerminalParam);
			stUpdateTerminalParam.setExecuteBatch(1);
			stUpdateTerminalParam.setString(1, value);
			stUpdateTerminalParam.setLong(2, CDate.getCurrentUtcMsDate());
			stUpdateTerminalParam.setLong(3, vid);
			stUpdateTerminalParam.setInt(4, Integer.parseInt(paramKey));
			stUpdateTerminalParam.executeUpdate();
		} finally {
			if (stUpdateTerminalParam != null) {
				stUpdateTerminalParam.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/***
	 * 存储参数设置
	 * 
	 * @param tid
	 * @param paramKey
	 * @param value
	 * @throws SQLException
	 */
	private void saveTernimalParam(Long vid, String paramKey, String value) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveTerminalParam = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveTerminalParam);
			stSaveTerminalParam.setExecuteBatch(1);
			stSaveTerminalParam.setInt(1, Integer.parseInt(paramKey));
			stSaveTerminalParam.setLong(2, vid);
			stSaveTerminalParam.setString(3, value);
			long sysTime = CDate.getCurrentUtcMsDate();
			stSaveTerminalParam.setLong(4, sysTime);
			stSaveTerminalParam.setLong(5, sysTime);
			stSaveTerminalParam.executeUpdate();
		} finally {
			if (stSaveTerminalParam != null) {
				stSaveTerminalParam.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/**
	 * 更新终端版本号
	 * 
	 * @param packetBean
	 * @throws SQLException
	 */
	public void updateTernimalVersion(Map<String, String> app) throws SQLException {
		String[] version = app.get("518").split("\\|");

		if (version.length >= 2) {
			long vid = Long.parseLong(app.get(Constant.VID));
			OracleConnection dbCon = null;

			OracleResultSet rs = null;
			try {
				// 从连接池获得连接
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stUpdateTerminalVersion = (OraclePreparedStatement) dbCon.prepareStatement(sql_updateTerminalVersion);
				stUpdateTerminalVersion.setExecuteBatch(1);

				stUpdateTerminalVersion.setString(1, version[0]);
				stUpdateTerminalVersion.setString(2, version[1]);
				stUpdateTerminalVersion.setLong(3, vid);
				stUpdateTerminalVersion.executeUpdate();

				stCheckTerminalVersion = (OraclePreparedStatement) dbCon.prepareStatement(sql_checkTerminalVersion);
				stCheckTerminalVersion.setLong(1, vid);
				rs = (OracleResultSet) stCheckTerminalVersion.executeQuery();
				if (rs.next()) {
					if (rs.getString("HARDWARE_VERSION").equals(version[0]) && rs.getString("FIRMWARE_VERSION").equals(version[1])) {
						stUpdateTerminalUpdaeInfoVersion = (OraclePreparedStatement) dbCon.prepareStatement(sql_updateTerminalUpdaeInfoVersion);
						stUpdateTerminalUpdaeInfoVersion.setExecuteBatch(1);
						stUpdateTerminalUpdaeInfoVersion.setInt(1, 1);
						stUpdateTerminalUpdaeInfoVersion.setLong(2, vid);
						stUpdateTerminalUpdaeInfoVersion.setLong(3, CDate.getCurrentUtcMsDate());
						stUpdateTerminalUpdaeInfoVersion.executeUpdate();
					}
				}

				if (version.length >= 3) {
					// 更新SIM卡ICCID
					updateSIMICCID(version[2], app.get(Constant.COMMDR));
				}
			} finally {
				if (rs != null) {
					rs.close();
				}

				if (stCheckTerminalVersion != null) {
					stCheckTerminalVersion.close();
				}

				if (stUpdateTerminalUpdaeInfoVersion != null) {
					stUpdateTerminalUpdaeInfoVersion.close();
				}
				if (stUpdateTerminalVersion != null) {
					stUpdateTerminalVersion.close();
				}

				if (dbCon != null) {
					dbCon.close();
				}
			}

		}
	}

	/******
	 * 
	 * 更新SIM卡表ICCID信息
	 * 
	 * @param iccid
	 * @param commaddr
	 * @throws SQLException
	 */
	private void updateSIMICCID(String iccid, String commaddr) throws SQLException {
		OracleConnection dbCon = null;
		OraclePreparedStatement stUpdateSimIccid = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateSimIccid = (OraclePreparedStatement) dbCon.prepareStatement(sql_updateSIMiccid);
			stUpdateSimIccid.setExecuteBatch(1);
			stUpdateSimIccid.setString(1, iccid);
			stUpdateSimIccid.setString(2, commaddr);
			stUpdateSimIccid.executeUpdate();

		} finally {
			if (null != stUpdateSimIccid) {
				stUpdateSimIccid.close();
			}

			if (null != dbCon) {
				dbCon.close();
			}
		}
	}

	/***
	 * 存储终端注册信息
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveTernimalRegisterInfo(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveTerminalRegister = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveVehicleRegister);
			stSaveTerminalRegister.setExecuteBatch(1);
			stSaveTerminalRegister.setString(1, app.get("44"));
			if (app.get("43") != null) {
				stSaveTerminalRegister.setString(2, app.get("43"));
			} else {
				stSaveTerminalRegister.setString(2, null);
			}
			if (app.containsKey("40")) {
				stSaveTerminalRegister.setString(3, app.get("40"));
			} else {
				stSaveTerminalRegister.setString(3, null);
			}
			if (app.containsKey("41")) {
				stSaveTerminalRegister.setString(4, app.get("41"));
			} else {
				stSaveTerminalRegister.setString(4, null);
			}
			if (app.containsKey("42")) {
				stSaveTerminalRegister.setString(5, app.get("42"));
			} else {
				stSaveTerminalRegister.setString(5, null);
			}
			if (app.containsKey("104")) {
				stSaveTerminalRegister.setString(6, app.get("104"));
			} else {
				stSaveTerminalRegister.setString(6, null);
			}
			stSaveTerminalRegister.setString(7, app.get(Constant.PLATECOLORID));
			stSaveTerminalRegister.setLong(8, CDate.getCurrentUtcMsDate());
			if (app.get("45") != null) {
				stSaveTerminalRegister.setInt(9, Integer.parseInt(app.get("45")));
			} else {
				stSaveTerminalRegister.setNull(9, Types.NULL);
			}
			stSaveTerminalRegister.setString(10, app.get(Constant.COMMDR));
			stSaveTerminalRegister.executeUpdate();
		} finally {
			if (stSaveTerminalRegister != null) {
				stSaveTerminalRegister.close();
			}
			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/***
	 * 存储终端鉴权信息 TODO
	 * INSERT INTO TH_VEHICLE_CHECKED(PID,COMMADDR,AKEY,RESULT,SEQ,UTC,RESULTUTC,OEM_CODE) VALUES(?,?,?,?,?,?,?,?)
	 * @param app
	 * @throws SQLException
	 */
	public void saveVehicleAKey(Map<String, String> app) throws SQLException {
		if ((app.get(Constant.UUID) == null) ) { // 上传不包含多媒体不保存数据
			return;
		}
		if((app.get(Constant.COMMDR) == null)|| app.get(Constant.COMMDR).equals("")){ 
			return;
		}
		try {
//			stSaveVehicleAKey.setExecuteBatch(1);
			stSaveVehicleAKey.setString(1, app.get(Constant.UUID)); // PID
			stSaveVehicleAKey.setString(2, app.get(Constant.COMMDR)); //
			if (app.get("47") != null && app.get("47").length() < 16) {
				stSaveVehicleAKey.setString(3, app.get("47")); //
			} else {
				stSaveVehicleAKey.setString(3, null); //
			}
			if (app.get("48") != null) {
				stSaveVehicleAKey.setString(4, app.get("48")); //
			} else {
				stSaveVehicleAKey.setString(4, null); //
			}
			stSaveVehicleAKey.setString(5, app.get(Constant.SEQ)); //
			stSaveVehicleAKey.setLong(6, CDate.getCurrentUtcMsDate());
			stSaveVehicleAKey.setLong(7, CDate.getCurrentUtcMsDate());
			stSaveVehicleAKey.setString(8, app.get(Constant.MACID).split("_")[0]);
			stSaveVehicleAKey.executeUpdate();
		} catch (NullPointerException ex) {
			logger.error("存储终端鉴权信息异常--save VehicleAKey NullPointerException ERROR:" + app.get(Constant.COMMAND)+ ":"+ex.getStackTrace().toString(), ex);
		} catch (SQLException e) { 
			logger.error("存储终端鉴权信息异常--save VehicleAKey SQLException ERROR:"+e.getStackTrace().toString(), e);
			try {
				vehicleAKeyConn.getMetaData();
				if (stSaveVehicleAKey == null) {
					stSaveVehicleAKey = createStatement(vehicleAKeyConn, 1, sql_saveVehicleAKey);
				}
			} catch (Exception ex) {
				stSaveVehicleAKey = recreateStatement(vehicleAKeyConn, 1, sql_saveVehicleAKey);
			}
		}
	}

	/****
	 * 存储终端注销
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveVehicleLogOff(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveVehicleLogOff = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveVehicleLogOff);
			stSaveVehicleLogOff.setExecuteBatch(1);
			stSaveVehicleLogOff.setString(1, app.get(Constant.UUID)); // PID
			stSaveVehicleLogOff.setString(2, app.get(Constant.COMMDR)); //
			stSaveVehicleLogOff.setString(3, app.get("46")); //
			stSaveVehicleLogOff.setString(4, app.get(Constant.SEQ)); //
			stSaveVehicleLogOff.setLong(5, CDate.getCurrentUtcMsDate());
			stSaveVehicleLogOff.setLong(6, CDate.getCurrentUtcMsDate());
			stSaveVehicleLogOff.setString(7, app.get(Constant.MACID).split("_")[0]);
			stSaveVehicleLogOff.executeUpdate();

		} finally {
			if (stSaveVehicleLogOff != null) {
				stSaveVehicleLogOff.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/****
	 * 存储多媒体事件 TODO  JedisConnectionServer.saveMediaEvent
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveMultimediaEvent(Map<String, String> app) throws SQLException {
		if( (app.get(Constant.UUID) == null) || (app.get(Constant.VID) == null)){
			return ;
		}
		try {
			stSaveMultimediaEvent.setString(1, app.get(Constant.UUID));
			stSaveMultimediaEvent.setLong(2, Long.parseLong(app.get(Constant.VID)));
			stSaveMultimediaEvent.setInt(3, Integer.parseInt(app.get("121")));
			stSaveMultimediaEvent.setInt(4, Integer.parseInt(app.get("122")));
			stSaveMultimediaEvent.setInt(5, Integer.parseInt(app.get("123")));
			stSaveMultimediaEvent.setInt(6, Integer.parseInt(app.get("124")));
			// 拍摄序号
			if (app.get("127") != null) {
				stSaveMultimediaEvent.setString(7, app.get("127"));
			} else {
				stSaveMultimediaEvent.setString(7, null);
			}
			// 事件触发时间（yyMMDDHHmmss（GMT+8时间）；记录多媒体事件触发的时间）
			if (app.get("126") != null) {
				stSaveMultimediaEvent.setLong(8, Long.parseLong(app.get("126")));
			} else {
				stSaveMultimediaEvent.setNull(8, Types.INTEGER);
			}
			stSaveMultimediaEvent.setLong(9, System.currentTimeMillis());

			stSaveMultimediaEvent.setString(10, app.get("120"));

			stSaveMultimediaEvent.executeUpdate();

			if (null != app.get("127") && null != app.get("126") && null != app.get("123") && "0".equals(app.get("123"))) {
				Jedis jedis = null;
				try {
//					jedis = RedisConnectionPool.getJedisConnection();
					jedis = JedisConnectionPool.getJedisConnection();
					jedis.select(0); // Index 0 缓存回复状态
					jedis.set(app.get(Constant.VID) + "_" + app.get("126"), app.get("127"));
					jedis.expire(app.get(Constant.VID) + "_" + app.get("126"), CDate.getIntvalTime());
				} catch (Exception ex) {
					JedisConnectionPool.returnBrokenResource(jedis);
					logger.error("Connect redis server to save taking SEQ fail,", ex);
				} finally {
//					RedisConnectionPool.returnJedisConnection(jedis);
					JedisConnectionPool.returnJedisConnection(jedis);
				}
			}
		} catch (NullPointerException ex) {
			logger.error("存储多媒体事件异常--save mediaEvent ERROR:" + app.get(Constant.COMMAND),ex);
		} catch (SQLException e) {
			logger.error("存储多媒体事件异常--save mediaEvent ERROR:", e);
			try {
				mediaEventConn.getMetaData();
				if (stSaveMultimediaEvent == null) {
					stSaveMultimediaEvent = createStatement(mediaEventConn, commitMediaCount, sql_saveMultimediaEvent);
				}
			} catch (Exception ex) {
				stSaveMultimediaEvent = recreateStatement(mediaEventConn, commitMediaCount, sql_saveMultimediaEvent);
			}
		}
	}

	/****
	 * 存储驾驶员登录信息
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveDriverLoginingInfo(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		OraclePreparedStatement stsaveDriverLoginInfo = null;
		try {
			String staff_name = app.get("110");
			String cardId = app.get("111");
			String[] driverInfo = new String[2];
			if (null != app.get("110") || null != app.get("111")) {
				driverInfo = getDriverInfo(staff_name, cardId);
			}
			long time = CDate.getCurrentUtcMsDate();

			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stsaveDriverLoginInfo = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveDriverLoginInfo);
			stsaveDriverLoginInfo.setExecuteBatch(1);
			stsaveDriverLoginInfo.setString(1, app.get(Constant.UUID));
			stsaveDriverLoginInfo.setLong(2, Long.parseLong(app.get(Constant.VID)));
			stsaveDriverLoginInfo.setString(3, app.get(Constant.COMMDR));
			stsaveDriverLoginInfo.setString(4, staff_name);
			stsaveDriverLoginInfo.setString(5, driverInfo[0]); // 驾驶ID
			stsaveDriverLoginInfo.setString(6, app.get("111")); // 驾驶员身份证编码
			stsaveDriverLoginInfo.setString(7, app.get("112")); // 从业资格证编码
			stsaveDriverLoginInfo.setString(8, app.get("113")); // 发证机构名称
			stsaveDriverLoginInfo.setString(9, driverInfo[1]); // 驾驶员IC卡号
			stsaveDriverLoginInfo.setLong(10, time);
			stsaveDriverLoginInfo.setInt(11, Integer.parseInt(app.get("RESULT")));
			stsaveDriverLoginInfo.setLong(12, time);
			stsaveDriverLoginInfo.setLong(13, time);
			stsaveDriverLoginInfo.executeUpdate();
		} finally {
			if (stsaveDriverLoginInfo != null) {
				stsaveDriverLoginInfo.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/****
	 * 通过staff_name 驾驶员名称，cardId 驾驶员身份证号 查询驾驶员ID号 和驾驶员IC卡号
	 * 
	 * @param staff_name
	 *            驾驶员名称，cardId 驾驶员身份证号
	 * @throws SQLException
	 */
	public String[] getDriverInfo(String staff_name, String cardId) throws SQLException {
		OracleConnection dbCon = null;
		OraclePreparedStatement st = null;
		OracleResultSet rs = null;
		try {
			String driverInfo[] = new String[2];
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			st = (OraclePreparedStatement) dbCon.prepareStatement(sql_selectDriverInfo);
			st.setString(1, staff_name);
			st.setString(2, cardId);
			rs = (OracleResultSet) st.executeQuery();
			if (rs.next()) {
				driverInfo[0] = String.valueOf(rs.getLong("STAFF_ID")); // 驾驶员ID
				driverInfo[1] = rs.getString("DRIVER_ICCARD"); // 驾驶员IC卡号
			}
			return driverInfo;
		} finally {
			if (st != null) {
				st.close();
			}

			if (rs != null) {
				rs.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/****
	 * 存储驾驶员身份信息
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveDriverInfo(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		OraclePreparedStatement stSaveDriverInfo = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveDriverInfo = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveDriverInfo);
			stSaveDriverInfo.setExecuteBatch(1);
			stSaveDriverInfo.setString(1, app.get(Constant.UUID));
			stSaveDriverInfo.setLong(2, Long.parseLong(app.get(Constant.VID)));
			stSaveDriverInfo.setString(3, app.get("110"));
			stSaveDriverInfo.setString(4, app.get("111"));
			stSaveDriverInfo.setString(5, app.get("112"));
			stSaveDriverInfo.setString(6, app.get("113"));
			stSaveDriverInfo.setLong(7, CDate.getCurrentUtcMsDate());
			stSaveDriverInfo.setInt(8, Integer.parseInt(app.get("RESULT")));
			stSaveDriverInfo.executeUpdate();
		} finally {
			if (stSaveDriverInfo != null) {
				stSaveDriverInfo.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/***
	 * 存储电子运单历史
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveEticket(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveEticket = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveEticket);
			stSaveEticket.setExecuteBatch(1);
			stSaveEticket.setString(1, app.get(Constant.UUID));
			stSaveEticket.setLong(2, Long.parseLong(app.get(Constant.VID)));
			stSaveEticket.setString(3, Base64_URl.base64Decode(app.get("87")));
			stSaveEticket.setLong(4, CDate.getCurrentUtcMsDate());
			stSaveEticket.executeUpdate();
		} finally {
			if (stSaveEticket != null) {
				stSaveEticket.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/***
	 * 存储多媒体检索信息
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveMediaIdx(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			String values = app.get("20");
			if (values != null && !values.equals("")) {
				String[] subValue = values.split("\\]");
				// 从连接池获得连接
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stSaveMediaIdx = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveMediaIdx);
				for (String cn : subValue) {
					cn = cn.replaceAll("\\[", "").replaceAll("\\]", "");
					String[] str = cn.split(",");
					Map<String, String> stateKV = new HashMap<String, String>();
					for (String subStr : str) {
						String[] qt = subStr.split(":");
						if (qt.length == 2) {
							stateKV.put(qt[0], qt[1]);
						}
					}// End for;

					stSaveMediaIdx.setString(1, UUID.randomUUID().toString());
					stSaveMediaIdx.setString(2, app.get(Constant.SEQ));

					stSaveMediaIdx.setLong(3, Long.parseLong(app.get(Constant.VID)));
					stSaveMediaIdx.setString(4, stateKV.get("120"));
					stSaveMediaIdx.setString(5, stateKV.get("121"));
					stSaveMediaIdx.setString(6, stateKV.get("124"));
					stSaveMediaIdx.setString(7, stateKV.get("123"));
					long lon = Long.parseLong(stateKV.get("1"));
					long lat = Long.parseLong(stateKV.get("2"));

					stSaveMediaIdx.setLong(8, lat);
					stSaveMediaIdx.setLong(9, lon);
					long maplon = -100;
					long maplat = -100;
					// 偏移
					Converter conver = new Converter();
					Point point = conver.getEncryPoint(lon / 600000.0, lat / 600000.0);
					if (point != null) {
						maplon = Math.round(point.getX() * 600000);
						maplat = Math.round(point.getY() * 600000);
					} else {
						maplon = 0;
						maplat = 0;
					}
					stSaveMediaIdx.setLong(10, maplon);
					stSaveMediaIdx.setLong(11, maplat);
					stSaveMediaIdx.setInt(12, Integer.parseInt(stateKV.get("5")));
					stSaveMediaIdx.setInt(13, Integer.parseInt(stateKV.get("3")));
					stSaveMediaIdx.setLong(14, CDate.stringConvertUtc(stateKV.get("4")));
					stSaveMediaIdx.setLong(15, CDate.getCurrentUtcMsDate());
					stSaveMediaIdx.addBatch();
					stateKV.clear();
				}// End for
				stSaveMediaIdx.executeBatch();
			}
		} finally {
			if (stSaveEticket != null) {
				stSaveEticket.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/*****
	 * 存储多媒体 TODO
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveMultMedia(Map<String, String> app) throws SQLException {
		if (!app.containsKey("125") || (app.get(Constant.UUID) == null) || (app.get(Constant.VID) == null)) { // 上传不包含多媒体不保存数据
			return;
		}
		try {
			stSaveMultMedia.setString(1, app.get(Constant.UUID));
			stSaveMultMedia.setString(2, app.get("120"));
			stSaveMultMedia.setLong(3, Long.parseLong(app.get(Constant.VID)));
			stSaveMultMedia.setString(4, app.get(Constant.MACID).split("_", 2)[1]);
			stSaveMultMedia.setString(5, app.get("121")); // 多媒体类型
			stSaveMultMedia.setString(6, app.get("122")); // 多媒体格式编码
			stSaveMultMedia.setString(7, app.get("123")); // 事件项编码
			stSaveMultMedia.setLong(8, CDate.getCurrentUtcMsDate());
			stSaveMultMedia.setString(9, app.get("125")); // 多媒体数据地址URL
			stSaveMultMedia.setString(10, app.get("124")); // 通道ID
			if (app.containsKey("1") && app.containsKey("2")) {
				long lon = Long.parseLong(app.get("1"));
				long lat = Long.parseLong(app.get("2"));
				stSaveMultMedia.setLong(11, lat);
				stSaveMultMedia.setLong(12, lon);
				Point point = Utils.convertLatLon(lon, lat);
				if (point != null) {
					stSaveMultMedia.setLong(13, Math.round(point.getX() * 600000));
					stSaveMultMedia.setLong(14, Math.round(point.getY() * 600000));
				} else {
					stSaveMultMedia.setNull(13, Types.INTEGER);
					stSaveMultMedia.setNull(14, Types.INTEGER);
				}
			} else {
				stSaveMultMedia.setNull(11, Types.INTEGER);
				stSaveMultMedia.setNull(12, Types.INTEGER);
				stSaveMultMedia.setNull(13, Types.INTEGER);
				stSaveMultMedia.setNull(14, Types.INTEGER);
			}
			if (app.containsKey("6")) { // 海拔高度
				stSaveMultMedia.setInt(15, Integer.parseInt(app.get("6")));
			} else {
				stSaveMultMedia.setNull(15, Types.INTEGER);
			}
			if (app.containsKey("5")) { // 方向
				stSaveMultMedia.setInt(16, Integer.parseInt(app.get("5")));
			} else {
				stSaveMultMedia.setNull(16, Types.INTEGER);
			}
			if (app.containsKey("3")) { // 速度
				stSaveMultMedia.setInt(17, Integer.parseInt(app.get("3")));
			} else {
				stSaveMultMedia.setNull(17, Types.INTEGER);
			}
			if (app.get("8") != null) {
				stSaveMultMedia.setString(18, app.get("8"));
			} else {
				stSaveMultMedia.setString(18, null);
			}
			// 事件触发时间（YY-MM-DD-hh-mm-ss（GMT+8时间）；记录多媒体事件触发的时间）
			if (app.get("126") != null) {
				stSaveMultMedia.setLong(19, Long.parseLong(app.get("126")));
			} else {
				stSaveMultMedia.setNull(19, Types.INTEGER);
			}
			stSaveMultMedia.setLong(20, System.currentTimeMillis());
			String devType = app.get(Constant.DEV_TYPE);
			if (devType != null && devType.equals("1")) {
				stSaveMultMedia.setString(21, "1");// 多媒体上传设备类型 (0:2G;1:3G)
			} else {
				stSaveMultMedia.setString(21, "0");// 多媒体上传设备类型 (0:2G;1:3G)
			}
			stSaveMultMedia.executeUpdate();
//			logger.info("存储多媒体消息1条:" + app.get(Constant.UUID));
		} catch (NullPointerException ex) {
			logger.error("存储多媒体消息异常--save media ERROR:" + app.get(Constant.COMMAND), ex);
		} catch (SQLException e) {
			logger.error("存储多媒体消息异常--save media ERROR:", e);
			try {
				mediaConn.getMetaData();
				if (stSaveMultMedia == null) {
					stSaveMultMedia = createStatement(mediaConn, commitMediaCount, sql_saveMultMedia);
				}
			} catch (Exception ex) {
				stSaveMultMedia = recreateStatement(mediaConn, commitMediaCount, sql_saveMultMedia);
			}
		}
	}

	/*****
	 * 存储REDIS照片信息  TODO JedisConnectionServer.saveMediaPhoto
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveRedisMultMedia(Map<String, String> app) throws SQLException {
		// yujch modify 2013-05-21 redis不缓存音视频文件，只缓存图像文件 添加
		// ||null==app.get("121")||!"0".equals(app.get("121")) 过滤
		if (!app.containsKey("125") || null == app.get("123") || !"0".equals(app.get("123")) || null == app.get("121") || !"0".equals(app.get("121"))) { // 跳过多媒体事件和不属于平台下发拍照
			return;
		}
		// String eventTime = null;
		String vid = app.get(Constant.VID);
		// if(null != app.get("126")){ // 获取事件触发时间，查询对应拍摄序号(支持808B车机)
		// eventTime = app.get("126");
		// Jedis jedis = null;
		// try {
		//
		// jedis = RedisConnectionPool.getJedisConnection();
		// jedis.select(0);
		//
		// if(jedis.exists(vid + "_" + eventTime)){
		// String takingSEQ = jedis.get(vid + "_" + eventTime);
		// String ky = vid + ":" + takingSEQ;
		// if(jedis.exists(ky)){ // CHECK 是否对应照片KY
		// String value = jedis.get(ky);
		// String[] arr = value.split("#");
		// if(3 == arr.length){
		// jedis.set(ky, app.get("125") + "#" + app.get(Constant.UUID) + "#" +
		// arr[2]); // 状态或URL#照片主键#几路
		// jedis.expire(ky,CDate.getIntvalTime());
		// }
		// }
		// }
		// }catch(Exception ex){
		// logger.error("Save 808B picture to redis for error."+
		// ex.getMessage());
		// }finally{
		// if(null != jedis)
		// RedisConnectionPool.returnJedisConnection(jedis);
		// }
		// }else{
		Jedis jedis = null;
		try {
//			jedis = RedisConnectionPool.getJedisConnection();
			jedis = JedisConnectionPool.getJedisConnection();
			jedis.select(0);
			StringBuffer value = new StringBuffer();
			value.append(app.get("125"));
			value.append("#");
			value.append(app.get(Constant.UUID));
			value.append("#");
			value.append(app.get("124"));
			jedis.lpush(vid + "media", value.toString());
			jedis.expire(vid + "media", CDate.getIntvalTime());
		} catch (Exception ex) {
			JedisConnectionPool.returnBrokenResource(jedis);
			logger.error("Save 808 picture to redis for error.", ex);
		} finally {
			if (null != jedis)
//				RedisConnectionPool.returnJedisConnection(jedis);
				JedisConnectionPool.returnJedisConnection(jedis);
		}
		// }
	}

	/******
	 * 更新图片上传中状态:内部协议1:开始,2：成功，3：失败。对应数据库6:开始上传，7：上传成功，8：上传失败
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void updatePicProgress(Map<String, String> app) throws SQLException {
		String eventTime = app.get("151");
		String stus = app.get("150");

		if (null != stus && null != eventTime) {
			String vid = app.get(Constant.VID);
			Jedis jedis = null;
			try {
				// 从连接池获得连接
//				jedis = RedisConnectionPool.getJedisConnection();
				jedis = JedisConnectionPool.getJedisConnection();
				jedis.select(0);

				if (jedis.exists(vid + "_" + eventTime)) {
					String takingSEQ = jedis.get(vid + "_" + eventTime);

					String ky = vid + ":" + takingSEQ;
					String status = "8";
					if ("1".equals(stus)) {
						status = "6";
					} else if ("2".equals(stus)) {
						status = "7";
					}

					if (jedis.exists(ky)) { // CHECK 是否对应照片KY
						String value = jedis.get(ky);
						String[] arr = value.split("#");
						if (3 == arr.length) {
							jedis.set(ky, status + "#" + app.get(Constant.UUID) + "#" + arr[2]); // 状态或URL#照片主键#几路
							jedis.expire(ky, CDate.getIntvalTime());
							app.put("RET", status);
							app.put(Constant.SEQ, takingSEQ);
							updateControlCommand(app);
						}
					}
				}
			} catch (Exception ex) {
				JedisConnectionPool.returnBrokenResource(jedis);
				logger.error("Save picture to redis for error." + ex.getMessage());
			} finally {
				if (null != jedis)
//					RedisConnectionPool.returnJedisConnection(jedis);
					JedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}

	/***
	 * 存储事件ID
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveEventId(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveEventId = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveEventId);
			stSaveEventId.setExecuteBatch(1);
			stSaveEventId.setString(1, app.get(Constant.UUID));
			stSaveEventId.setLong(2, Long.parseLong(app.get(Constant.VID)));
			stSaveEventId.setString(3, app.get("81"));
			stSaveEventId.setLong(4, CDate.getCurrentUtcMsDate());
			stSaveEventId.executeUpdate();
		} finally {
			if (stSaveEventId != null) {
				stSaveEventId.close();
			}
			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/****
	 * 更新历史提问
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void updateQuerstionAnswer(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateQuerstionAnswer = (OraclePreparedStatement) dbCon.prepareStatement(sql_updateQuerstionAnswer);
			stUpdateQuerstionAnswer.setExecuteBatch(1);
			stUpdateQuerstionAnswer.setLong(1, CDate.getCurrentUtcMsDate());
			stUpdateQuerstionAnswer.setString(2, app.get("82"));
			stUpdateQuerstionAnswer.setString(3, app.get(Constant.SEQ));
			stUpdateQuerstionAnswer.executeUpdate();
		} finally {
			if (stUpdateQuerstionAnswer != null) {
				stUpdateQuerstionAnswer.close();
			}
			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/****
	 * 存储数据上行透传
	 * 
	 * @throws SQLException
	 */
	public void saveBridge(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveBridge = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveBridge);
			stSaveBridge.setExecuteBatch(1);
			stSaveBridge.setString(1, app.get(Constant.UUID));
			stSaveBridge.setLong(2, Long.parseLong(app.get(Constant.VID)));
			stSaveBridge.setLong(3, CDate.getCurrentUtcMsDate());
			stSaveBridge.setString(4, Base64_URl.base64Decode(app.get("90")));
			stSaveBridge.setInt(5, Integer.parseInt(app.get("91")));
			stSaveBridge.executeUpdate();
		} finally {
			if (stSaveBridge != null) {
				stSaveBridge.close();
			}
			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/****
	 * 存储数据压缩上传
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveCompress(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveCompress = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveCompress);
			stSaveCompress.setExecuteBatch(1);
			stSaveCompress.setString(1, app.get(Constant.UUID));
			stSaveCompress.setLong(2, Long.parseLong(app.get(Constant.VID)));
			stSaveCompress.setLong(3, CDate.getCurrentUtcMsDate());
			stSaveCompress.setString(4, Base64_URl.base64Decode(app.get("92")));
			stSaveCompress.executeUpdate();
		} finally {
			if (stSaveCompress != null) {
				stSaveCompress.close();
			}
			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/***
	 * 存储行驶记录仪
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveRecorder(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveRecorder = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveRecorder);
			stSaveRecorder.setExecuteBatch(1);
			stSaveRecorder.setString(1, app.get(Constant.UUID));
			stSaveRecorder.setLong(2, Long.parseLong(app.get(Constant.VID)));
			stSaveRecorder.setLong(3, CDate.getCurrentUtcMsDate());
			stSaveRecorder.setString(4, app.get("61"));
			stSaveRecorder.setString(5, app.get(Constant.SEQ));
			stSaveRecorder.executeUpdate();
		} finally {
			if (stSaveRecorder != null) {
				stSaveRecorder.close();
			}
			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/****
	 * 更新行驶记录仪记录表
	 * 
	 * @param seq
	 * @throws SQLException
	 */
	public void updateRecorder(String seq) throws SQLException {
		OracleConnection dbCon = null;
		try {
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stUpdateRecorder = (OraclePreparedStatement) dbCon.prepareStatement(sql_updateRecorder);
			stUpdateRecorder.setExecuteBatch(1);
			stUpdateRecorder.setLong(1, CDate.getCurrentUtcMsDate());
			stUpdateRecorder.setString(2, "03");
			stUpdateRecorder.setString(3, seq);
			stUpdateRecorder.executeUpdate();
		} finally {
			if (stUpdateRecorder != null) {
				stUpdateRecorder.close();
			}
			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/****
	 * 存储信息点播取消
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveInfoplay(Map<String, String> app) throws SQLException {
		OracleConnection dbCon = null;
		try {
			String value = app.get("83");
			String[] v = value.split("\\|");
			if (v.length != 2) {
				return;
			}
			// 从连接池获得连接
			dbCon = (OracleConnection) OracleConnectionPool.getConnection();
			stSaveInfoplay = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveInfoplay);
			stSaveInfoplay.setExecuteBatch(1);
			stSaveInfoplay.setString(1, app.get(Constant.UUID));
			stSaveInfoplay.setLong(2, Long.parseLong(app.get(Constant.VID)));
			stSaveInfoplay.setString(3, v[0]);
			stSaveInfoplay.setLong(4, CDate.getCurrentUtcMsDate());
			stSaveInfoplay.setInt(5, Integer.parseInt(v[1]));
			stSaveInfoplay.executeUpdate();
		} finally {
			if (stSaveInfoplay != null) {
				stSaveInfoplay.close();
			}
			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/****
	 * 更新车辆注销
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void updateVehicleLogOff(Map<String, String> app) throws SQLException {
		if ("0".equals(app.get("46"))) {
			OracleConnection dbCon = null;
			try {

				// 从连接池获得连接
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stUpdateVehicleLogOff = (OraclePreparedStatement) dbCon.prepareStatement(sql_updateVehicleLogOff);
				stUpdateVehicleLogOff.setExecuteBatch(1);
				long vid = Long.parseLong(app.get(Constant.VID));
				stUpdateVehicleLogOff.setLong(1, CDate.getCurrentUtcMsDate());
				stUpdateVehicleLogOff.setLong(2, vid);
				stUpdateVehicleLogOff.setLong(3, vid);
				stUpdateVehicleLogOff.executeUpdate();

			} finally {
				if (stUpdateVehicleLogOff != null) {
					stUpdateVehicleLogOff.close();
				}

				if (dbCon != null) {
					dbCon.close();
				}
			}
		}
	}

	/****
	 * 存储油量变化记录，当为偷油告警和加油提示作为变化记录。
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveOilChanged(String baseInfo, long vid) throws SQLException {
		OracleConnection dbCon = null;
		OraclePreparedStatement saveOilChanged = null;
		try {
			String[] arr = baseInfo.split(":");
			if (arr.length == 10) {
				// 从连接池获得连接
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				saveOilChanged = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveOilChanged);
				saveOilChanged.setExecuteBatch(1);
				saveOilChanged.setString(1, arr[6]);
				saveOilChanged.setLong(2, vid);
				saveOilChanged.setLong(3, CDate.shortDateConvertUtc(arr[5]));
				saveOilChanged.setLong(4, Long.parseLong(arr[0]));
				saveOilChanged.setLong(5, Long.parseLong(arr[1]));
				Point point = Utils.convertLatLon(Long.parseLong(arr[1]), Long.parseLong(arr[0]));
				if (point != null) {
					saveOilChanged.setLong(6, Math.round(point.getX() * 600000));
					saveOilChanged.setLong(7, Math.round(point.getY() * 600000));
				} else {
					saveOilChanged.setNull(6, Types.INTEGER);
					saveOilChanged.setNull(7, Types.INTEGER);
				}
				saveOilChanged.setLong(8, Long.parseLong(arr[2]));
				saveOilChanged.setLong(9, Long.parseLong(arr[4]));
				saveOilChanged.setLong(10, Long.parseLong(arr[3]));
				saveOilChanged.setLong(11, CDate.getCurrentUtcMsDate());
				saveOilChanged.setLong(12, Long.parseLong(arr[7]));
				saveOilChanged.setLong(13, Long.parseLong(arr[9])); // 当前油量
				saveOilChanged.setLong(14, Long.parseLong(arr[8])); // 变化油量
				saveOilChanged.executeUpdate();
			}
		} finally {
			if (saveOilChanged != null) {
				saveOilChanged.close();
			}

			if (dbCon != null) {
				dbCon.close();
			}
		}
	}

	/******
	 * 存储偷油告警
	 * 
	 * @param msg
	 */
	public void saveStealingOilAlarm(String msg, long vid) {
		OracleConnection dbCon = null;
		OraclePreparedStatement stStealingOil = null;
		try {
			String[] arr = msg.split(":");
			if (arr.length == 10) {
				// 从连接池获得连接 
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				stStealingOil = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveStealOilAlarm);
				stStealingOil.setExecuteBatch(1);
				stStealingOil.setString(1, vid + "111" + CDate.getCurrentUtcMsDate());
				stStealingOil.setLong(2, vid);
				stStealingOil.setLong(3, CDate.getStringToDate(arr[5]));
				stStealingOil.setLong(4, Long.parseLong(arr[0]));
				stStealingOil.setLong(5, Long.parseLong(arr[1]));
				Point point = Utils.convertLatLon(Long.parseLong(arr[1]), Long.parseLong(arr[0]));
				if (point != null) {
					stStealingOil.setLong(6, Math.round(point.getX() * 600000));
					stStealingOil.setLong(7, Math.round(point.getY() * 600000));
				} else {
					stStealingOil.setNull(6, Types.INTEGER);
					stStealingOil.setNull(7, Types.INTEGER);
				}
				stStealingOil.setLong(8, Long.parseLong(arr[2]));
				stStealingOil.setLong(9, Long.parseLong(arr[4]));
				stStealingOil.setLong(10, Long.parseLong(arr[3]));
				stStealingOil.setString(11, "111"); // 报警CODE
				stStealingOil.setLong(12, CDate.getStringToDate(arr[5]));
				stStealingOil.setLong(13, CDate.getStringToDate(arr[5]));
				stStealingOil.setString(14, "A005"); // 所属类型
				stStealingOil.setLong(15, CDate.getCurrentUtcMsDate());
				stStealingOil.setLong(16, Long.parseLong(arr[0]));
				stStealingOil.setLong(17, Long.parseLong(arr[1]));
				point = Utils.convertLatLon(Long.parseLong(arr[1]), Long.parseLong(arr[0]));
				if (point != null) {
					stStealingOil.setLong(18, Math.round(point.getX() * 600000));
					stStealingOil.setLong(19, Math.round(point.getY() * 600000));
				} else {
					stStealingOil.setNull(18, Types.INTEGER);
					stStealingOil.setNull(19, Types.INTEGER);
				}
				stStealingOil.setLong(20, Long.parseLong(arr[2]));
				stStealingOil.setLong(21, Long.parseLong(arr[4]));
				stStealingOil.setLong(22, Long.parseLong(arr[3]));
				stStealingOil.executeUpdate();
			}
		} catch (SQLException e) {
			logger.error("Saving for Stealing oil alarm to failed:" + msg + e.getMessage());
		} finally {
			try {
				if (stStealingOil != null) {
					stStealingOil.close();
				}

				if (dbCon != null) {
					dbCon.close();
				}
			} catch (Exception e) {
				logger.error(Constant.SPACES,e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：存储发动机故障信息 		</li><br>
	 * <li>时        间：2013-8-7  下午7:27:16	</li><br>
	 * <li>参数： @param engineFaultInfo			</li><br>
	 * 
	 *****************************************/
	public void saveEngBug(EngineFaultInfo engineFaultInfo) {
		OracleConnection dbCon = null;
		OraclePreparedStatement engineFaultInfoStatement = null;
		try {
				// 从连接池获得连接 
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				engineFaultInfoStatement = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveEngineFaultInfo);
				engineFaultInfoStatement.setExecuteBatch(1);
				engineFaultInfoStatement.setString(1, engineFaultInfo.getBugId());		//bugId
				engineFaultInfoStatement.setLong(2, engineFaultInfo.getVid());			//vid
				engineFaultInfoStatement.setString(3, engineFaultInfo.getVehicleNo());	//车牌号
				engineFaultInfoStatement.setString(4, engineFaultInfo.getVinCode());	//车架号
				engineFaultInfoStatement.setString(5, engineFaultInfo.getCommaddr());	//手机号
				engineFaultInfoStatement.setString(6, engineFaultInfo.getStatus());		//状态
				engineFaultInfoStatement.setString(7, engineFaultInfo.getBugCode());	//故障码
				engineFaultInfoStatement.setString(8, engineFaultInfo.getBugDesc());	//故障码描述
				engineFaultInfoStatement.setString(9, engineFaultInfo.getBugFlag()); 	//故障码状态说明
				engineFaultInfoStatement.setString(10, engineFaultInfo.getBasicCode());	//原始故障码
				engineFaultInfoStatement.setLong(11, engineFaultInfo.getLat());			//纬度
				engineFaultInfoStatement.setLong(12, engineFaultInfo.getLon()); 		//经度
				engineFaultInfoStatement.setLong(13, engineFaultInfo.getMaplat());		//偏移后纬度
				engineFaultInfoStatement.setLong(14, engineFaultInfo.getMaplon());		//偏移后经度
				engineFaultInfoStatement.setLong(15, engineFaultInfo.getElevation());	//海拔
				engineFaultInfoStatement.setLong(16, engineFaultInfo.getDirection());	//方向
				engineFaultInfoStatement.setLong(17, engineFaultInfo.getGpsSpeeding());//GPS速度
				engineFaultInfoStatement.setString(18, engineFaultInfo.getReportTime());//上报时间
				
				engineFaultInfoStatement.executeUpdate();
		} catch (SQLException e) {
			logger.error("--saveEngBug--ERROR--存储发动机故障信息 异常:" + e.getMessage(), e);
		} finally {
			try {
				if (engineFaultInfoStatement != null) {
					engineFaultInfoStatement.close();
				}
				if (dbCon != null) {
					dbCon.close();
				}
			} catch (Exception e) {
				logger.error("--saveEngBug--ERROR-closed!-存储发动机故障信息 异常:关闭连接失败！",e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：更新发动机故障处理信息		</li><br>
	 * <li>时        间：2013-8-7  下午8:12:12	</li><br>
	 * <li>参数： @param engineFaultInfo			</li><br>
	 * 
	 *****************************************/
	public void updateEngBugDispose(EngineFaultInfo engineFaultInfo) {
		OracleConnection dbCon = null;
		OraclePreparedStatement engBugDisposeStatement = null;
		try {
				// 从连接池获得连接 
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				engBugDisposeStatement = (OraclePreparedStatement) dbCon.prepareStatement(sql_updateEngBugDispose);
				engBugDisposeStatement.setExecuteBatch(1);
				engBugDisposeStatement.setString(1, engineFaultInfo.getClearFlag());		//清除标记
				engBugDisposeStatement.setString(2, engineFaultInfo.getBugSeqId());			//故障序列号
//				engBugDisposeStatement.setString(3, engineFaultInfo.getVid());	// 
				
				engBugDisposeStatement.executeUpdate();
		} catch (SQLException e) {
			logger.error("--updateEngBugDispose--ERROR--更新发动机故障处理信息 异常:" + e.getMessage(), e);
		} finally {
			try {
				if (engBugDisposeStatement != null) {
					engBugDisposeStatement.close();
				}
				if (dbCon != null) {
					dbCon.close();
				}
			} catch (Exception e) {
				logger.error("--updateEngBugDispose--ERROR-closed!-更新发动机故障处理信息 异常:关闭连接失败！",e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：存储发动机版本信息		</li><br>
	 * <li>时        间：2013-8-8  下午11:54:00	</li><br>
	 * <li>参数： @param engineFaultInfo			</li><br>
	 * 
	 *****************************************/
	public void saveEngVersionInfo(EngineFaultInfo engineFaultInfo) {
		OracleConnection dbCon = null;
		OraclePreparedStatement engVersionInfoStatement = null;
		try {
				// 从连接池获得连接 
				dbCon = (OracleConnection) OracleConnectionPool.getConnection();
				//删除原版本记录
				engVersionInfoStatement = (OraclePreparedStatement) dbCon.prepareStatement(sql_deleteEngVersionInfo);
				engVersionInfoStatement.setExecuteBatch(1);
				engVersionInfoStatement.setLong(1, engineFaultInfo.getVid());			//VID 	
				engVersionInfoStatement.setString(2, engineFaultInfo.getVinCode());	//所属车辆识别码 
				engVersionInfoStatement.setString(3, engineFaultInfo.getCommaddr());	//终端手机号 
				engVersionInfoStatement.executeUpdate();
				engVersionInfoStatement.sendBatch();
//				存储新版本信息
				engVersionInfoStatement = (OraclePreparedStatement) dbCon.prepareStatement(sql_saveEngVersionInfo);
				engVersionInfoStatement.setExecuteBatch(1);
				engVersionInfoStatement.setString(1, engineFaultInfo.getBugId());		//版本序列号
				engVersionInfoStatement.setLong(2, engineFaultInfo.getVid());			//VID 
				engVersionInfoStatement.setString(3, engineFaultInfo.getVehicleNo());	//车牌号
				engVersionInfoStatement.setString(4, engineFaultInfo.getVinCode());		//所属车辆识别码 
				engVersionInfoStatement.setString(5, engineFaultInfo.getCommaddr());	//终端手机号 
				engVersionInfoStatement.setString(6, engineFaultInfo.getVersionCode());	//版本代码 
				engVersionInfoStatement.setString(7, engineFaultInfo.getVersionValue());//版本描述
				engVersionInfoStatement.setLong(8, Long.parseLong(engineFaultInfo.getReportTime()));	//上报日期
				
				engVersionInfoStatement.executeUpdate();
		} catch (SQLException e) {
			logger.error("--saveEngVersionInfo--ERROR--存储发动机版本信息 异常:" + e.getMessage(), e);
		} finally {
			try {
				if (engVersionInfoStatement != null ) {
					engVersionInfoStatement.close();
				}
				if (dbCon != null) {
					dbCon.close();
				}
			} catch (Exception e) {
				logger.error("--saveEngVersionInfo--ERROR-closed!-存储发动机版本信息 异常:关闭连接失败！",e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：存储或者更新远程锁车信息表 		</li><br>
	 * <li>时        间：2013-9-5  上午10:14:19	</li><br>
	 * <li>参数： @param vid			VID
	 * <li>参数： @param value			锁车控制|锁车类型|发动机最高转速|自锁预定时间|预警提醒锁车时间
	 *****************************************/
	public void saveOrUpdateLockVehicleDetail(String vid, String value) {
		OracleConnection conn = null;
		OraclePreparedStatement lockVehicleStatement = null;
		SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss");
		try {
			String[] lockArray = StringUtils.splitPreserveAllTokens(value, "|");
			if(lockArray == null || lockArray.length != 5){
				logger.error("--saveOrUpdateLockVehicleDetail--ERROR--存储或者更新远程锁车信息表非法数据:" + value);
				return;
			}
			// 从连接池获得连接 
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			//删除原版本记录
			lockVehicleStatement = (OraclePreparedStatement) conn.prepareStatement(sql_saveOrUpdateLockVehicleDetail);
			lockVehicleStatement.setExecuteBatch(1);
			//UUID 	
			lockVehicleStatement.setString(1, UUID.randomUUID().toString().replace("-", ""));		
			//VID
			lockVehicleStatement.setLong(2, Long.parseLong(vid));	
			//锁车状态
			lockVehicleStatement.setString(3, lockArray[0]);		
			//锁车类型 01：限制转速 02：切断ACC电路 03：切断油路电路 
			lockVehicleStatement.setString(4, "0"+lockArray[1]);		
			//发动机最高转速 
			if(lockArray[2].equals("")){
				lockVehicleStatement.setNull(5, Types.INTEGER);			
			} else{
				
				lockVehicleStatement.setLong(5, Long.parseLong(lockArray[2]));	
			}
			//预锁车时间
			if(lockArray[3].equals("")){
				lockVehicleStatement.setNull(6, Types.INTEGER);	
			} else{
				lockVehicleStatement.setLong(6, sdf.parse(lockArray[3]).getTime());	
			}
			lockVehicleStatement.setLong(7, System.currentTimeMillis());	
			lockVehicleStatement.executeUpdate();
		} catch (SQLException e) {
			logger.error("--saveOrUpdateLockVehicleDetail--ERROR--存储或者更新远程锁车信息表 异常:" + e.getMessage(), e);
		} catch (ParseException e) {
			logger.error("--saveOrUpdateLockVehicleDetail--ERROR--存储或者更新远程锁车信息表--(yyyyMMddHHmmss)非法时间格式:"+value+"异常:" + e.getMessage(), e);
		} finally {
			try {
				lockVehicleStatement.close();
				conn.close();
			} catch (Exception e) {
				logger.error("--saveOrUpdateLockVehicleDetail--ERROR-closed!-存储或者更新远程锁车信息表 异常:关闭连接失败！",e);
			}
		}
		
	}

	/*****************************************
	 * <li>描        述：更新 解锁码		</li><br>
	 * <li>时        间：2013-9-5  上午11:01:12	</li><br>
	 * <li>参数： @param vid			</li><br>
	 * @param unlockCode 	解锁码
	 * 
	 *****************************************/
	public void updateUnlockCode(String vid, String unlockCode) {
		OracleConnection conn = null;
		OraclePreparedStatement updateUnlockCodeStatement = null;
		try {
			// 从连接池获得连接 
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			//删除原版本记录
			updateUnlockCodeStatement = (OraclePreparedStatement) conn.prepareStatement(sql_updateUnlockCode);
			updateUnlockCodeStatement.setExecuteBatch(1);
			//解锁码
			updateUnlockCodeStatement.setString(1, unlockCode);
			updateUnlockCodeStatement.setString(2, "2");	
			//VID
			updateUnlockCodeStatement.setLong(3, Long.parseLong(vid));	
			updateUnlockCodeStatement.executeUpdate();
		} catch (SQLException e) {
			logger.error("--updateUnlockCode--ERROR--更新 解锁码	异常:" + e.getMessage(), e);
		} finally {
			try {
				updateUnlockCodeStatement.close();
				conn.close();
			} catch (Exception e) {
				logger.error("--updateUnlockCode--ERROR-closed!-更新 解锁码	异常:关闭连接失败！",e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：更新锁车状态 		</li><br>
	 * <li>时        间：2013-9-5  上午11:37:38	</li><br>
	 * <li>参数： @param vid		车辆编号
	 * <li>参数： @param status	状态			</li><br>
	 * 
	 *****************************************/
	public void updateLockVehicleStatus(String vid, String status) {
		OracleConnection conn = null;
		OraclePreparedStatement updateLockVehicleStatusStatement = null;
		try {
			if(StringUtils.startsWithAny(status, "1","2")){
				status = "0";
			} else {
				status = "1";
			}
			// 从连接池获得连接 
			conn = (OracleConnection) OracleConnectionPool.getConnection();
			//删除原版本记录
			updateLockVehicleStatusStatement = (OraclePreparedStatement) conn.prepareStatement(sql_updateLockVehicleStatusStatement);
			updateLockVehicleStatusStatement.setExecuteBatch(1);
			//解锁码
			updateLockVehicleStatusStatement.setString(1, status);
			//更新时间
			updateLockVehicleStatusStatement.setLong(2, System.currentTimeMillis());	
			//VID
			updateLockVehicleStatusStatement.setLong(3, Long.parseLong(vid));	
			updateLockVehicleStatusStatement.executeUpdate();
		} catch (SQLException e) {
			logger.error("--updateLockVehicleStatus--ERROR--更新锁车状态异常:" + e.getMessage(), e);
		} finally {
			try {
				updateLockVehicleStatusStatement.close();
				conn.close();
			} catch (Exception e) {
				logger.error("--updateLockVehicleStatus--ERROR-closed!-更新锁车状态异常:关闭连接失败！",e);
			}
		}
		
	}
	
	
	/*****************************************
	 * <li>描        述：初始化存储质检单信息链接 	</li><br>
	 * <li>时        间：2013-9-17  	</li><br>
	 * <li>参数： 	</li><br>
	 * 
	 *****************************************/
	public void initQualityRecordStatement(){
		try{
			qualityRecordConn = (OracleConnection) OracleConnectionPool.getConnection();
			qualityRecordStatement = this.createStatement(qualityRecordConn,20,saveQualityRecordSql);
		}catch(Exception e){
			logger.error("oracle初始化保存质检单连接statement出错.", e);
		}
	}
	
	/**
	 * 提交质检单信息
	 */
	public void commitQulityRecord(){
		try{
			qualityRecordStatement.sendBatch();
		}catch(Exception e){
			logger.error("提交质检单信息异常:"+e.getMessage(),e);
		}finally{
			try {
				if (qualityRecordStatement != null) {
					qualityRecordStatement.close();
				}
				if (qualityRecordConn != null) {
					qualityRecordConn.close();
				}
			} catch (Exception e) {
				logger.error("--commitQulityRecord--ERROR-closed!-存储质检单信息 异常:关闭连接失败！",e);
			}
		}
	}
	/**
	 * 存储质检单信息
	 * @param qualityRecordBean
	 */
	public void saveQualityRecord(QualityRecordBean qualityRecordBean) {
		try {
				qualityRecordStatement.setString(1, qualityRecordBean.getRecordId());		//记录编号
				qualityRecordStatement.setString(2, qualityRecordBean.getVid());			//vid
				qualityRecordStatement.setString(3, qualityRecordBean.getVinCode());	//vin
				qualityRecordStatement.setLong(4, qualityRecordBean.getUtc());	//终端时间
				qualityRecordStatement.setString(5, qualityRecordBean.getParamId());	//检测单项编号
				qualityRecordStatement.setString(6, qualityRecordBean.getParamValue());		//检测单项状态
				qualityRecordStatement.setString(7, qualityRecordBean.getTerminalConfig());	//终端配置
				qualityRecordStatement.setString(8, qualityRecordBean.getGprsStrength());	//GPRS强度
				qualityRecordStatement.setString(9, qualityRecordBean.getGpsState()); 	//gps状态
				qualityRecordStatement.setString(10, ""+qualityRecordBean.getSpeedPlus());	//特征系数
				
				qualityRecordStatement.executeUpdate();
		} catch (SQLException e) {
			logger.error("存储质检单信息异常--saveQualityRecord ERROR:", e);
			try {
				qualityRecordConn.getMetaData();
				if (qualityRecordStatement == null) {
					qualityRecordStatement = createStatement(qualityRecordConn, 20, saveQualityRecordSql);
				}
			} catch (Exception ex) {
				stSaveMultimediaEvent = recreateStatement(qualityRecordConn, 20, saveQualityRecordSql);
			}
		}catch (Exception ex) {
			logger.error("存储质检单信息异常--saveQualityRecord ERROR:" + qualityRecordBean.getCommaddr(),ex);
		} 
	}

}
