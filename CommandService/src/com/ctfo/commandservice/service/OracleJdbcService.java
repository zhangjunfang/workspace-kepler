/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.service OracleJdbcService.java	</li><br>
 * <li>时        间：2013-9-12  下午6:50:16	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.service;

import java.io.UnsupportedEncodingException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import java.sql.Types;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.bosch.exception.DiagException;
import com.ctfo.commandservice.dao.OracleConnectionPool;
import com.ctfo.commandservice.dao.RedisConnectionPool;
import com.ctfo.commandservice.model.AccidentDoubpointsDetail;
import com.ctfo.commandservice.model.AccidentDoubpointsMain;
import com.ctfo.commandservice.model.Driver;
import com.ctfo.commandservice.model.DriverInfo;
import com.ctfo.commandservice.model.EngineFaultInfo;
import com.ctfo.commandservice.model.IccId;
import com.ctfo.commandservice.model.LinkStatus;
import com.ctfo.commandservice.model.OilInfo;
import com.ctfo.commandservice.model.OracleProperties;
import com.ctfo.commandservice.model.PlatformMessage;
import com.ctfo.commandservice.model.Points;
import com.ctfo.commandservice.model.QualityRecordBean;
import com.ctfo.commandservice.model.ServiceUnit;
import com.ctfo.commandservice.model.Supervision;
import com.ctfo.commandservice.model.TerminalParam;
import com.ctfo.commandservice.model.TerminalVersion;
import com.ctfo.commandservice.model.Warning;
import com.ctfo.commandservice.util.Base64_URl;
import com.ctfo.commandservice.util.Cache;
import com.ctfo.commandservice.util.Constant;
import com.ctfo.commandservice.util.Converser;
import com.ctfo.commandservice.util.DateTools;
import com.ctfo.commandservice.util.Tools;
import com.ctfo.commandservice.util.Utils;
import com.ctfo.generator.pk.GeneratorPK;
import com.encryptionalgorithm.Point;

/*****************************************
 * <li>描 述：oracle数据服务
 * 
 *****************************************/
public class OracleJdbcService {
	private static final Logger logger = LoggerFactory.getLogger(OracleJdbcService.class);
	/*---------------------------------------参数-------------------------------------------*/
	/** oracle数据库连接模版 */
	/*private DruidDataSource druidDataSource;*/
	/** 存储控制指令SQL */
	private String sql_saveControlCommand;
	/** 存储多媒体信息 SQL */
	private String sql_saveMultMedia;
	/** 存储车辆调度信息SQL */
	private String sql_saveVehicleDispatchMsg;
	/** 更新终端版本号 SQL */
	private String sql_updateTernimalVersion;
	/** 查询终端版本记录VID SQL */
	private String sql_queryTernimalRecord;
	/** 更新终端版本记录SQL */
	private String sql_updateTernimalRecord;
	/** 插入终端版本记录SQL */
	private String sql_insertTernimalRecord;
	/** 更新SIM卡ICCIDSQL */
	private String sql_updateSIMICCID;
	/** 更新SIM卡ICCIDSQL */
	private String sql_saveMultimediaEvent;
	/** 存储电子运单历史SQL */
	private String sql_saveEticket;
	/** 存储终端注册信息SQL */
	private String sql_saveTernimalRegisterInfo;
	/** 存储终端注销SQL */
	private String sql_saveVehicleLogOff;
	/** 更新车辆注销SQL */
	private String sql_updateVehicleLogOff;
	/** 存储终端鉴权信息 SQL */
	private String sql_saveVehicleAKey;
	/** 存储驾驶员身份信息 SQL */
	private String sql_saveDriverInfo;
	/** 存储驾驶员登录信息 SQL */
	private String sql_saveDriverWork;
	/** 更新驾驶员登录信息 SQL */
	private String sql_updateDriverWork;
	/** 查询驾驶员信息 SQL */
	private String sql_getDriverInfo;
	/** 存储事件IDSQL */
	private String sql_saveEventId;
	/** 更新历史提问SQL */
	private String sql_updateQuerstionAnswer;
	/** 存储油量变化记录SQL */
	private String sql_saveOilChanged;
	/** 存储偷油告警SQL */
	private String sql_saveStealingOilAlarm;
	/** 删除发动机版本信息SQL */
	private String sql_deleteEngVersionInfo;
	/** 存储发动机版本信息SQL */
	private String sql_saveEngVersionInfo;
	/** 存储发动机故障信息SQL */
	private String sql_saveEngBug;
	/** 存储数据上行透传SQL */
	private String sql_saveBridge;
	/** 存储数据压缩上传SQL */
	private String sql_saveCompress;
	/** 存储行驶记录仪SQL */
	private String sql_saveRecorder;
	/** 存储信息点播取消SQL */
	private String sql_saveInfoplay;
	/** 更新照片状态SQL */
	private String sql_updateControlCommand;
	/** 更新触发拍照状态表SQL */
	private String sql_updatePhotoSetting;
	/** 更新终端参数历史SQL */
	private String sql_updateTerminalHisParam;
	/** 更新电子围栏状态SQL */
	private String sql_updateLineSetting;
	/** 更新电子围栏设置SQL */
	private String sql_updateAreaSetting;
	/** 更新电子围栏设置SQL */
	private String sql_updateLockVehicleStatus;
	/** 存储多媒体检索信息SQL */
	private String sql_saveMediaIdx;
	/** 更新发动机故障处理信息SQL */
	private String sql_updateEngBugDispose;
	/** 根据终端ID查询数据库终端参数值SQL */
	private String sql_selectDBTerminalParam;
	/** 存储参数设置SQL */
	private String sql_saveBatchTernimalParam;
	/** 更新参数设置SQL */
	private String sql_updateBatchTernimalParam;
	/** 存储或者更新远程锁车信息表SQL */
	private String sql_saveOrUpdateLockVehicleDetail;
	/** 更新解锁指令状态SQL */
	private String sql_updateLockCommandStatus;
	/** 更新 解锁码	SQL */
	private String sql_updateUnlockCode;
	/** 更新调度信息	SQL */
	private String sql_updateVehicleDispatchMsg;
	/** 询指令子类型SQL */
	private String sql_querySubType;
	/**	全量更新车辆缓存信息SQL	*/
	private String sql_initAllVehilceCache;
	/**	全量更新3G车辆缓存信息SQL	*/
	private String sql_update3GPhotoVehicleInfo;
	/**	增量更新车辆缓存信息SQL	*/
	private String sql_updateVehicle;
	/**	增量更新3g车辆缓存SQL	*/
	private String sql_update3GVehicle;
	/**	存储质检单信息主表信息SQL	*/
	private String sql_saveQualityRecordCacheInfo;
	/**	存储质检单信息SQL	*/
	private String sql_saveQualityRecordInfo;
	/**	存储报警督办信息SQL	*/
	private String sql_saveSupervision;
	/**	存储预警信息SQL	*/
	private String sql_saveWarning;
	/**	存储平台信息SQL	*/
	private String sql_savePlatformMessage;
	/**	存储平台上线连接信息SQL	*/
	private String sql_savePlatformOnline;
	/**	存储平台下线连接信息SQL	*/
	private String sql_savePlatformOffline;
	
	/**	更新终端国标版本号SQL	*/
	private String sql_updateTerminalStandardVersion;
	/** 查询终端标准版本信息**/
	private String sql_queryTerminalStandardVersion;
	/** 删除事故疑点数据信息**/
	private String sql_delDoubpointsMainAndDetail;
	/** 添加事故疑点主数据信息**/
	private String sql_addDoubpointsMain;
	/** 添加事故疑点明细数据信息**/
	private String sql_addDoubpointsDetail;
	/** 更新行驶记录提取历史记录状态**/
	private String sql_updateTravellingRecorderLog;
	/** 添加事故疑点修正数据**/
	private String sql_addVehicleSpeedCheck;
	/** 油箱油量标定信息	**/
	private String sql_saveOilInfo;
	/** 终端上报调度信息查询SQL	**/
	private String sql_queryDispatch;
	/** 终端上报调度信息存储SQL	**/
	private String sql_saveDispatch;
	
	public OracleJdbcService(OracleProperties oracleProperties){
		/** 存储控制指令SQL */
		this.sql_saveControlCommand = oracleProperties.getSql_saveControlCommand();
		/** 存储多媒体信息 SQL */
		this.sql_saveMultMedia = oracleProperties.getSql_saveMultMedia();
		/** 存储车辆调度信息SQL */
		this.sql_saveVehicleDispatchMsg = oracleProperties.getSql_saveVehicleDispatchMsg();
		/** 更新终端版本号 SQL */
		this.sql_updateTernimalVersion = oracleProperties.getSql_updateTernimalVersion();
		/** 查询终端版本记录VIDSQL */
		this.sql_queryTernimalRecord = oracleProperties.getSql_queryTernimalRecord();
		/** 更新终端版本记录SQL */
		this.sql_updateTernimalRecord = oracleProperties.getSql_updateTernimalRecord();
		/** 插入终端版本号 SQL */
		this.sql_insertTernimalRecord = oracleProperties.getSql_insertTernimalRecord();
		/** 更新SIM卡ICCIDSQL */
		this.sql_updateSIMICCID = oracleProperties.getSql_updateSIMICCID();
		/** 更新SIM卡ICCIDSQL */
		this.sql_saveMultimediaEvent = oracleProperties.getSql_saveMultimediaEvent();
		/** 存储电子运单历史SQL */
		this.sql_saveEticket = oracleProperties.getSql_saveEticket();
		/** 存储终端注册信息SQL */
		this.sql_saveTernimalRegisterInfo = oracleProperties.getSql_saveTernimalRegisterInfo();
		/** 存储终端注销SQL */
		this.sql_saveVehicleLogOff = oracleProperties.getSql_saveVehicleLogOff();
		/** 更新车辆注销SQL */
		this.sql_updateVehicleLogOff = oracleProperties.getSql_updateVehicleLogOff();
		/** 存储终端鉴权信息 SQL */
		this.sql_saveVehicleAKey = oracleProperties.getSql_saveVehicleAKey();
		/** 存储驾驶员身份信息 SQL */
		this.sql_saveDriverInfo = oracleProperties.getSql_saveDriverInfo();
		/** 存储驾驶员登录信息 SQL */
		this.sql_saveDriverWork = oracleProperties.getSql_saveDriverWork();
		/** 存储驾驶员登录信息 SQL */
		this.sql_updateDriverWork = oracleProperties.getSql_updateDriverWork();
		/** 查询驾驶员信息 SQL */
		this.sql_getDriverInfo = oracleProperties.getSql_getDriverInfo();
		/** 存储事件IDSQL */
		this.sql_saveEventId = oracleProperties.getSql_saveEventId();
		/** 更新历史提问SQL */
		this.sql_updateQuerstionAnswer = oracleProperties.getSql_updateQuerstionAnswer();
		/** 存储油量变化记录SQL */
		this.sql_saveOilChanged = oracleProperties.getSql_saveOilChanged();
		/** 存储偷油告警SQL */
		this.sql_saveStealingOilAlarm = oracleProperties.getSql_saveStealingOilAlarm();
		/** 删除发动机版本信息SQL */
		this.sql_deleteEngVersionInfo = oracleProperties.getSql_deleteEngVersionInfo();
		/** 存储发动机版本信息SQL */
		this.sql_saveEngVersionInfo = oracleProperties.getSql_saveEngVersionInfo();
		/** 存储发动机故障信息SQL */
		this.sql_saveEngBug = oracleProperties.getSql_saveEngBug();
		/** 存储数据上行透传SQL */
		this.sql_saveBridge = oracleProperties.getSql_saveBridge();
		/** 存储数据压缩上传SQL */
		this.sql_saveCompress = oracleProperties.getSql_saveCompress();
		/** 存储行驶记录仪SQL */
		this.sql_saveRecorder = oracleProperties.getSql_saveRecorder();
		/** 存储信息点播取消SQL */
		this.sql_saveInfoplay = oracleProperties.getSql_saveInfoplay();
		/** 更新照片状态SQL */
		this.sql_updateControlCommand = oracleProperties.getSql_updateControlCommand();
		/** 更新触发拍照状态表SQL */
		this.sql_updatePhotoSetting = oracleProperties.getSql_updatePhotoSetting();
		/** 更新终端参数历史SQL */
		this.sql_updateTerminalHisParam = oracleProperties.getSql_updateTerminalHisParam();
		/** 更新电子围栏状态SQL */
		this.sql_updateLineSetting = oracleProperties.getSql_updateLineSetting();
		/** 更新电子围栏设置SQL */
		this.sql_updateAreaSetting = oracleProperties.getSql_updateAreaSetting();
		/** 更新电子围栏设置SQL */
		this.sql_updateLockVehicleStatus = oracleProperties.getSql_updateLockVehicleStatus();
		/** 存储多媒体检索信息SQL */
		this.sql_saveMediaIdx = oracleProperties.getSql_saveMediaIdx();
		/** 更新发动机故障处理信息SQL */
		this.sql_updateEngBugDispose = oracleProperties.getSql_updateEngBugDispose();
		/** 根据终端ID查询数据库终端参数值SQL */
		this.sql_selectDBTerminalParam = oracleProperties.getSql_selectDBTerminalParam();
		/** 存储参数设置SQL */
		this.sql_saveBatchTernimalParam = oracleProperties.getSql_saveBatchTernimalParam();
		/** 更新参数设置SQL */
		this.sql_updateBatchTernimalParam = oracleProperties.getSql_updateBatchTernimalParam();
		/** 存储或者更新远程锁车信息表SQL */
		this.sql_saveOrUpdateLockVehicleDetail = oracleProperties.getSql_saveOrUpdateLockVehicleDetail();
		/** 更新 解锁码	SQL */
		this.sql_updateUnlockCode = oracleProperties.getSql_updateUnlockCode();
		/** 更新调度信息	SQL */
		this.sql_updateVehicleDispatchMsg = oracleProperties.getSql_updateVehicleDispatchMsg();
		/** 询指令子类型SQL */
		this.sql_querySubType = oracleProperties.getSql_querySubType();
		/**	全量更新车辆缓存信息SQL	*/
		this.sql_initAllVehilceCache = oracleProperties.getSql_initAllVehilceCache();
		/**	全量更新3G车辆缓存信息SQL	*/
		this.sql_update3GPhotoVehicleInfo = oracleProperties.getSql_update3GPhotoVehicleInfo();
		/**	增量更新车辆缓存信息SQL	*/
		this.sql_updateVehicle = oracleProperties.getSql_updateVehicle();
		/**	增量更新3g车辆缓存SQL	*/
		this.sql_update3GVehicle = oracleProperties.getSql_update3GVehicle();
		/**	存储质检单信息SQL	*/
		this.sql_saveQualityRecordCacheInfo = oracleProperties.getSql_saveQualityRecordCacheInfo();
		this.sql_saveQualityRecordInfo = oracleProperties.getSql_saveQualityRecordInfo();
		/**	存储报警督办信息SQL	*/
		this.sql_saveSupervision = oracleProperties.getSql_saveSupervision();
		/**	存储预警信息SQL	*/
		this.sql_saveWarning = oracleProperties.getSql_saveWarning();
		/**	存储平台信息SQL	*/
		this.sql_savePlatformMessage = oracleProperties.getSql_savePlatformMessage();
		/**	存储平台连接信息SQL	*/
		this.sql_savePlatformOnline = oracleProperties.getSql_savePlatformOnline();
		/**	存储平台下线连接信息SQL	*/
		this.sql_savePlatformOffline = oracleProperties.getSql_savePlatformOffline();
		/** 更新解锁指令状态SQL */
		this.sql_updateLockCommandStatus = oracleProperties.getSql_updateLockCommandStatus();
		
		this.sql_updateTerminalStandardVersion = oracleProperties.getSql_updateTerminalStandardVersion();
		
		this.sql_queryTerminalStandardVersion = oracleProperties.getSql_queryTerminalStandardVersion();
		
		this.sql_delDoubpointsMainAndDetail = oracleProperties.getSql_delDoubpointsMainAndDetail();
		this.sql_addDoubpointsMain = oracleProperties.getSql_addDoubpointsMain();
		this.sql_addDoubpointsDetail = oracleProperties.getSql_addDoubpointsDetail();
		this.sql_updateTravellingRecorderLog = oracleProperties.getSql_updateTravellingRecorderLog();
		this.sql_addVehicleSpeedCheck = oracleProperties.getSql_addVehicleSpeedCheck();
		/** 油箱油量标定信息	**/
		this.sql_saveOilInfo = oracleProperties.getSql_saveOilInfo();
		/** 终端上报调度信息查询SQL	**/
		this.sql_queryDispatch = oracleProperties.getSql_queryDispatch(); 
		/** 终端上报调度信息存储SQL	**/
		this.sql_saveDispatch = oracleProperties.getSql_saveDispatch();
		
	}
	
	/*---------------------------------------业务方法-------------------------------------------*/
	
	/*****************************************
	 * <li>描 述：存储控制指令</li><br>
	 * <li>时 间：2013-9-12 下午6:52:02</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void saveControlCommand(Map<String, String> app) {
		Connection dbCon = null;
		PreparedStatement preparedStatement = null;
		String autoId = null;
		try {
			autoId = GeneratorPK.instance().getPKString();
			// 从连接池获得连接
			dbCon = OracleConnectionPool.getConnection();
			
			preparedStatement = dbCon.prepareStatement(sql_saveControlCommand);
			String seq = app.get(Constant.SEQ);
			String opId = seq.split("_")[0];
			
			preparedStatement.setString(1, opId);							//	1	OP_ID,,,,,,,,,
			preparedStatement.setString(2, app.get(Constant.VID)); 			// 2	VID
			preparedStatement.setString(3, app.get(Constant.VEHICLENO)); 	// 3	车牌号码 VEHICLE_NO
			preparedStatement.setLong(4, DateTools.getCurrentUtcMsDate());	// 4	CO_SUTC
			preparedStatement.setString(5, app.get(Constant.MTYPE));		//	5	CO_TYPE
			String mType = app.get(Constant.MTYPE);							
			if (mType.equals("L_PROV") || mType.equals("L_PLAT")) { 		//TODO 永远不会进入
				preparedStatement.setInt(6, 1); // 指令来源 1监管平台				//	6	CO_FROM
			} else {
				preparedStatement.setInt(6, 0); // 指令来源 0本平台				//	6	CO_FROM
			}
			preparedStatement.setString(7, app.get(Constant.SEQ));			//	7	CO_SEQ
			preparedStatement.setString(8, app.get(Constant.CHANNEL));		//	8	CO_CHANNEL	
			preparedStatement.setString(9, app.get(Constant.CONTENT));		//	9	CO_PARM	
			preparedStatement.setString(10, app.get(Constant.COMMAND));		//	10	CO_COMMAND
			preparedStatement.setString(11, app.get("TYPE"));				//	11	CO_SUBTYPE
			preparedStatement.setString(12, opId);							//	12	CREATE_BY					
			preparedStatement.setLong(13, DateTools.getCurrentUtcMsDate()); // 创建时间 13	CREATE_TIME
			preparedStatement.setString(14, app.get(Constant.OEMCODE));		//	14	CO_OEMCODE
			preparedStatement.setString(15, autoId);						//	15	AUTO_ID		
			preparedStatement.executeUpdate();
		} catch (Exception e) {
			logger.error("存储控制指令异常["+autoId+"]["+app.get(Constant.COMMAND)+"]:" + e.getMessage(), e);
		} finally { 
			try {
				if(preparedStatement != null){
					preparedStatement.close();
				}
				if(dbCon != null){
					dbCon.close();
				}
			} catch (SQLException e) {
				logger.error("存储控制指令关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储多媒体信息</li><br>
	 * <li>时 间：2013-9-12 下午6:52:07</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void saveMultMedia(Map<String, String> app) {
		if (!app.containsKey("125") || (app.get(Constant.UUID) == null) || (app.get(Constant.VID) == null)) { // 上传不包含多媒体不保存数据
			return;
		}
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveMultMedia);
			ps.setString(1, app.get(Constant.UUID));
			ps.setString(2, app.get("120"));
			ps.setString(3, app.get(Constant.VID));
			ps.setString(4, app.get(Constant.MACID).split("_", 2)[1]);
			ps.setString(5, app.get("121")); // 多媒体类型
			ps.setString(6, app.get("122")); // 多媒体格式编码
			ps.setString(7, app.get("123")); // 事件项编码
			ps.setLong(8, DateTools.getCurrentUtcMsDate());
			ps.setString(9, app.get("125")); // 多媒体数据地址URL
			ps.setString(10, app.get("124")); // 通道ID
			if (app.containsKey("1") && app.containsKey("2")) {
				long lon = Long.parseLong(app.get("1"));
				long lat = Long.parseLong(app.get("2"));
				ps.setLong(11, lat);
				ps.setLong(12, lon);
				Point point = Utils.convertLatLon(lon, lat);
				if (point != null) {
					ps.setLong(13, Math.round(point.getX() * 600000));
					ps.setLong(14, Math.round(point.getY() * 600000));
				} else {
					ps.setNull(13, Types.INTEGER);
					ps.setNull(14, Types.INTEGER);
				}
			} else {
				ps.setNull(11, Types.INTEGER);
				ps.setNull(12, Types.INTEGER);
				ps.setNull(13, Types.INTEGER);
				ps.setNull(14, Types.INTEGER);
			}
			if (app.containsKey("6")) { // 海拔高度
				ps.setInt(15, Integer.parseInt(app.get("6")));
			} else {
				ps.setNull(15, Types.INTEGER);
			}
			if (app.containsKey("5")) { // 方向
				ps.setInt(16, Integer.parseInt(app.get("5")));
			} else {
				ps.setNull(16, Types.INTEGER);
			}
			if (app.containsKey("3")) { // 速度
				ps.setInt(17, Integer.parseInt(app.get("3")));
			} else {
				ps.setNull(17, Types.INTEGER);
			}
			if (app.get("8") != null) {
				ps.setString(18, app.get("8"));
			} else {
				ps.setString(18, null);
			}
			// 事件触发时间（YY-MM-DD-hh-mm-ss（GMT+8时间）；记录多媒体事件触发的时间）
			if (StringUtils.isNumeric(app.get("126"))) {
				ps.setLong(19, Long.parseLong(app.get("126")));
			} else {
				ps.setNull(19, Types.INTEGER);
			}
			ps.setLong(20, System.currentTimeMillis());
			String devType = app.get(Constant.DEV_TYPE);
			if (devType != null && devType.equals("1")) {
				ps.setString(21, "1");// 多媒体上传设备类型 (0:2G;1:3G)
			} else {
				ps.setString(21, "0");// 多媒体上传设备类型 (0:2G;1:3G)
			}
			ps.executeUpdate();
		} catch (Exception e) {
			logger.error("存储多媒体消息异常--save media ERROR:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储多媒体信息 关闭连接异常:" + e.getMessage(), e);
			}
		}

	}

	/*****************************************
	 * <li>描 述：存储车辆调度信息</li><br>
	 * <li>时 间：2013-9-12 下午6:52:11</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void saveVehicleDispatchMsg(Map<String, String> app) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			String dmsgId = GeneratorPK.instance().getPKString();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveVehicleDispatchMsg);
			ps.setString(1, app.get(Constant.VID));
			ps.setString(2, app.get(Constant.VEHICLENO)); // 车牌号
			ps.setLong(3, DateTools.getCurrentUtcMsDate());
			ps.setInt(4, 1);
			ps.setString(5, app.get(Constant.SEQ));
			ps.setString(6, app.get(Constant.PLATECOLORID));
			ps.setLong(7, DateTools.getCurrentUtcMsDate());
			ps.setString(8, Base64_URl.base64Decode(app.get("17")));
			ps.setString(9, dmsgId);
			ps.executeUpdate();
		} catch (Exception e) {
			logger.error("存储车辆调度信息--异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储车辆调度信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：批量更新终端版本号</li><br>
	 * <li>时 间：2013-9-12 下午6:52:20</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void updateTernimalVersion(List<TerminalVersion> list, int batchSize) {
		Connection conn = null;
		PreparedStatement ps = null;
		int index = 0;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateTernimalVersion);
			for (TerminalVersion terminalVersion : list) {
				index++;
				ps.setString(1, terminalVersion.getTerminalHardVersion());
				ps.setString(2, terminalVersion.getTerminalFirmwareVersion());
				ps.setString(3, terminalVersion.gettId());
				ps.addBatch();
				if (index == batchSize) {
					ps.executeBatch();
					ps.clearBatch();
					index = 0;
				}
			}
			if (index != 0) {
				ps.executeBatch();
				ps.clearBatch();
			}
		} catch (Exception e) {
			logger.error("批量更新终端版本号异常:" + e.getMessage(), e);
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("批量更新终端版本号关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储对媒体事件</li><br>
	 * <li>时 间：2013-9-12 下午6:52:25</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void saveMultimediaEvent(Map<String, String> app) {
		if ((app.get(Constant.UUID) == null) || (app.get(Constant.VID) == null)) {
			return;
		}
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveMultimediaEvent);
			ps.setString(1, app.get(Constant.UUID));
			ps.setString(2, app.get(Constant.VID));
			ps.setInt(3, Integer.parseInt(app.get("121")));
			ps.setInt(4, Integer.parseInt(app.get("122")));
			ps.setInt(5, Integer.parseInt(app.get("123")));
			ps.setInt(6, Integer.parseInt(app.get("124")));
			// 拍摄序号
			if (app.get("127") != null) {
				ps.setString(7, app.get("127"));
			} else {
				ps.setString(7, null);
			}
			// 事件触发时间（yyMMDDHHmmss（GMT+8时间）；记录多媒体事件触发的时间）
			if (StringUtils.isNumeric(app.get("126"))) {
				ps.setLong(8, Long.parseLong(app.get("126")));
			} else {
				ps.setNull(8, Types.INTEGER);
			}
			ps.setLong(9, System.currentTimeMillis());
			ps.setString(10, app.get("120"));
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("存储多媒体事件异常--save mediaEvent ERROR:" + ex.getMessage(), ex);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储车辆调度信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储电子运单历史</li><br>
	 * <li>时 间：2013-9-12 下午6:52:30</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void saveEticket(Map<String, String> app) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveEticket);
			ps.setString(1, app.get(Constant.UUID));
			ps.setString(2, app.get(Constant.VID));
			ps.setString(3, Base64_URl.base64Decode(app.get("87")));
			ps.setLong(4, DateTools.getCurrentUtcMsDate());
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("存储电子运单历史异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储电子运单历史关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储终端注册信息</li><br>
	 * <li>时 间：2013-9-12 下午6:52:33</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void saveTernimalRegisterInfo(Map<String, String> app) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveTernimalRegisterInfo);
			ps.setString(1, app.get("44"));
			if (app.get("43") != null) {
				ps.setString(2, app.get("43"));
			} else {
				ps.setString(2, null);
			}
			if (app.containsKey("40")) {
				ps.setString(3, app.get("40"));
			} else {
				ps.setString(3, null);
			}
			if (app.containsKey("41")) {
				ps.setString(4, app.get("41"));
			} else {
				ps.setString(4, null);
			}
			if (app.containsKey("42")) {
				ps.setString(5, app.get("42"));
			} else {
				ps.setString(5, null);
			}
			if (app.containsKey("104")) {
				ps.setString(6, app.get("104"));
			} else {
				ps.setString(6, null);
			}
			ps.setString(7, app.get(Constant.PLATECOLORID));
			ps.setLong(8, DateTools.getCurrentUtcMsDate());
			if (app.get("45") != null) {
				ps.setInt(9, Integer.parseInt(app.get("45")));
			} else {
				ps.setNull(9, Types.NULL);
			}
			ps.setString(10, app.get(Constant.COMMDR)); 
			ps.setString(11, app.get(Constant.UUID)); 
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("存储终端注册信息异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储终端注册信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：更新车辆注销</li><br>
	 * <li>时 间：2013-9-12 下午6:52:42</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void updateVehicleLogOff(Map<String, String> app) {
		if ("0".equals(app.get("46"))) {
			Connection conn = null;
			PreparedStatement ps = null;
			try {
				ServiceUnit su = Cache.getVehicleMapValue(app.get(Constant.MACID));
				if(su == null){
					logger.error("更新车辆注销异常,无车辆服务信息:" + app.get(Constant.COMMAND)); 
					return;
				}
				String phoneNumber = su.getCommaddr();
				if(phoneNumber == null){
					logger.error("更新车辆注销异常,无车辆服务信息:" + app.get(Constant.COMMAND)); 
					return;
				}
				conn = OracleConnectionPool.getConnection();
				ps = conn.prepareStatement(sql_updateVehicleLogOff);
				ps.setLong(1, DateTools.getCurrentUtcMsDate());
				ps.setString(2, phoneNumber);
				ps.setString(3, phoneNumber);
				ps.executeUpdate();
			} catch (Exception ex) {
				logger.error("更新车辆注销异常:" + ex.getMessage(), ex);
			} finally {
				try {
					if(ps != null){
						ps.close();
					}
					if(conn != null){
						conn.close();
					}
				} catch (SQLException e) {
					logger.error("更新车辆注销关闭连接异常:" + e.getMessage(), e);
				}
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储终端注销</li><br>
	 * <li>时 间：2013-9-12 下午6:52:49</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void saveVehicleLogOff(Map<String, String> app) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveVehicleLogOff);
			ps.setString(1, app.get(Constant.UUID)); // PID
			ps.setString(2, app.get(Constant.COMMDR)); //
			ps.setString(3, app.get("46")); //
			ps.setString(4, app.get(Constant.SEQ)); //
			ps.setLong(5, DateTools.getCurrentUtcMsDate());
			ps.setLong(6, DateTools.getCurrentUtcMsDate());
			ps.setString(7, app.get(Constant.MACID).split("_")[0]);
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("存储终端注销信息异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储终端注销关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/**
	 * 存储IccId信息
	 * @param list
	 * @param batchSize
	 */
	public void saveIccId(List<IccId> list , int batchSize) {
		Connection conn = null;
		PreparedStatement ps = null;
		int index = 0;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateSIMICCID);
			for (IccId iccId : list) {
				index++;
				ps.setString(1, iccId.getIccId());
				ps.setString(2, iccId.getPhoneNumber());
				ps.addBatch();
				if (index == batchSize) {
					ps.executeBatch();
					ps.clearBatch();
					index = 0;
				}
			}
			if (index != 0) {
				ps.executeBatch();
				ps.clearBatch();
			}
		} catch (Exception e) {
			logger.error("批量存储IccId信息异常:" + e.getMessage(), e);
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("批量存储IccId信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储驾驶员身份信息</li><br>
	 * <li>时 间：2013-9-12 下午6:52:58</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void saveDriverInfo(Map<String, String> app) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			int result = -1;
			String resultStr = app.get("RESULT");
			if(StringUtils.isNumeric(resultStr)){
				result = Integer.parseInt(resultStr);
			}
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveDriverInfo);
			ps.setString(1, app.get(Constant.UUID));
			ps.setString(2, app.get(Constant.VID));
			ps.setString(3, app.get("110"));
			ps.setString(4, app.get("111"));
			ps.setString(5, app.get("112"));
			ps.setString(6, app.get("113"));
			ps.setLong(7, DateTools.getCurrentUtcMsDate());
			ps.setInt(8, result);
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("存储驾驶员身份信息异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储驾驶员身份信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储驾驶员登录信息</li><br>
	 * <li>时 间：2013-9-12 下午6:53:01</li><br>
	 * <li>参数： @param app</li><br>
	 *  TODO
	 *****************************************/
	public void saveDriverLoginingInfo(Map<String, String> app) { 
		Jedis jedis = null;
		Jedis vehicleInfoJedis = null;
		try {
			String status = app.get("107"); //从业资格证IC卡插拔状态(1: 上班，2:下班)
			if(status == null || status.length() == 0){
//				logger.error("驾驶员身份信息采集上报IC卡插拔状态为空[" +app.get(Constant.COMMAND)+ "]"); 
				return;
			}
			//解析时间
			String time = app.get("108");
			SimpleDateFormat sdf = new SimpleDateFormat("yyMMddHHmmss");
			Long cardTime = System.currentTimeMillis();
			try {
				if(time != null && time.length() > 10){
					Date date =  sdf.parse(time);
					cardTime = date.getTime();
				}
			} catch (Exception e) {
				logger.error("驾驶员身份信息采集上报解析上报时间异常" + e.getMessage(), e);
			}
			String vid = app.get(Constant.VID);
			String macid = app.get(Constant.MACID);
////			是否在线
//			boolean isOnline = false;
//			获取上次驾驶员登录信息缓存状态
			jedis =  RedisConnectionPool.getJedisConnection();
			jedis.select(7);
//			上线编号
			String onlineId = jedis.hget("H_KCPT_DRIVER_ONOFF_STATUS", macid);
//			驾驶员上班处理
			if(status.equals("1")){ 
//				规则(1)	若为上班但缓存中已有上班信息，则结束缓存中已有的上班记录，更新数据库中对应的上班数据的拔卡时间、状态，并缓存此次上班记录并向数据库中添加上班记录
				if(onlineId != null && onlineId.length() > 15){
//					上班记录编号
					if(onlineId != null && onlineId.length() > 10){
						DriverInfo driverInfo = new DriverInfo();
						driverInfo.setAutoId(onlineId);
						driverInfo.setOfflineTime(cardTime);
						updateDriverWork(driverInfo);
					} else {
						logger.error("存储驾驶员登录信息异常,缓存中上班记录编号异常["+onlineId+"]");
					}
				}
				// 获取车队编号、车队名称、组织编号、组织名称
				vehicleInfoJedis = RedisConnectionPool.getJedisConnection();
				vehicleInfoJedis.select(6);
				String vehicleInfo = vehicleInfoJedis.get(vid);
				String[] array = null;
				if(vehicleInfo != null){
					array = StringUtils.splitPreserveAllTokens(vehicleInfo, ":", 45);
				}
				String teamId = "";
				String teamName = "";
				String entId = "";
				String entName = "";
				if(array != null && array.length ==45){
					if(StringUtils.isNumeric(array[37])){
						teamId = array[37];
					}
					teamName = array[44];
					if(StringUtils.isNumeric(array[38])){
						entId = array[38];
					}
					entName = array[36];
				}
				String result = app.get("109");				//从业资格证IC卡读取结果
				if(result != null && result.equals("0")){
					result = "0";
				} else {
					if(!StringUtils.isNumeric(result)){
						result = "-1";
					}
					DriverInfo driverInfo = new DriverInfo();
					String uuid = app.get(Constant.UUID);
					driverInfo.setAutoId(uuid);//记录编号
					driverInfo.setVid(vid);//车辆编号
					driverInfo.setPhoneNumber(app.get(Constant.COMMDR));//手机号
					driverInfo.setStaffName("");//姓名
					driverInfo.setIdNumber("");//身份证号 
					driverInfo.setQualificationId("");//从业资格证号
					driverInfo.setQualificationName("");//资格证发证机构名称 
					driverInfo.setQualificationValid(-1);//从业资格证有效期
					driverInfo.setOnlineTime(cardTime);	//上线时间
					driverInfo.setIcStatus(Integer.parseInt(result));//IC卡读取状态
					driverInfo.setOnoffStatus(1);	//上下班状态
					driverInfo.setTeamId(teamId);//所属车队编号
					driverInfo.setTeamName(teamName);
					driverInfo.setEntId(entId);
					driverInfo.setEntName(entName);
					driverInfo.setSysUtc(System.currentTimeMillis());
					driverInfo.setPlate(app.get(Constant.VEHICLENO));
					driverInfo.setPlateColor(app.get(Constant.PLATECOLORID));
					saveDriverWork(driverInfo);
					jedis.hset("H_KCPT_DRIVER_ONOFF_STATUS", macid, uuid);
					return;
				}
				String staffName = app.get("110");			//驾驶员姓名
				if(staffName == null || staffName.length() == 0){
					staffName = "";
				}
				String idNumber = app.get("111");			//驾驶员身份证号码
				if(idNumber == null || idNumber.length() == 0){
					idNumber = "";
				}
				String qualificationId = app.get("112");	//从业资格证编码
				if(qualificationId == null || qualificationId.length() == 0){
					qualificationId = "";
				}
				String qualificationName = app.get("113");	//从业资格证发证机构名称
				if(qualificationName == null || qualificationName.length() == 0){
					qualificationName = "";
				}
				String qualificationValidStr = app.get("114");	//从业资格证有效期
				long qualificationValid = 0;
				if(qualificationValidStr == null || qualificationValidStr.length() == 0){
					qualificationValid = -1;
				} else {
					try {
						SimpleDateFormat df = new SimpleDateFormat("yyyyMMdd");
						qualificationValid = df.parse(qualificationValidStr).getTime();
					} catch (Exception e) {
						logger.error("驾驶员身份信息采集上报解析从业资格证有效期异常" + e.getMessage(), e);
					}
				}

				DriverInfo driverInfo = new DriverInfo();
				String uuid = app.get(Constant.UUID);
				driverInfo.setAutoId(uuid);//记录编号
				driverInfo.setVid(vid);//车辆编号
				driverInfo.setPhoneNumber(app.get(Constant.COMMDR));//手机号
				driverInfo.setStaffName(staffName);//姓名
				driverInfo.setIdNumber(idNumber);//身份证号 
				driverInfo.setQualificationId(qualificationId);//从业资格证号
				driverInfo.setQualificationName(qualificationName);//资格证发证机构名称 
				driverInfo.setQualificationValid(qualificationValid);//从业资格证有效期
				driverInfo.setOnlineTime(cardTime);	//上线时间
				driverInfo.setIcStatus(0);			//IC卡读取状态
				driverInfo.setOnoffStatus(1);	//上下班状态
				driverInfo.setTeamId(teamId);//所属车队编号
				driverInfo.setTeamName(teamName);
				driverInfo.setEntId(entId);
				driverInfo.setEntName(entName);
				driverInfo.setSysUtc(System.currentTimeMillis());
				driverInfo.setPlate(app.get(Constant.VEHICLENO));
				driverInfo.setPlateColor(app.get(Constant.PLATECOLORID));
//规则(2) 若为上班且缓存中无上班信息，则缓存此次上班记录并向数据库中添加上班记录；				
				saveDriverWork(driverInfo);
				jedis.hset("H_KCPT_DRIVER_ONOFF_STATUS", macid, uuid);
//			驾驶员下班
			} else if(status.equals("2")){
//规则(3) 若上传为下班且缓存中已有上班记录信息，更新数据库中对应的上班记录的拔卡时间、状态，并删除缓存中对应的上班记录
				if(onlineId != null && onlineId.length() > 10){
					DriverInfo driverInfo = new DriverInfo();
					driverInfo.setAutoId(onlineId);
					driverInfo.setOfflineTime(cardTime);
					updateDriverWork(driverInfo);
					jedis.hdel("H_KCPT_DRIVER_ONOFF_STATUS", macid);
				} else {
					logger.error("更新驾驶员下班信息异常,缓存中上班记录编号异常["+onlineId+"]");
				}
			} else {
				logger.error("无法处理的驾驶员身份信息采集上报信息[" +app.get(Constant.COMMAND)+ "]"); 
				return;
			}
		} catch (Exception ex) {
			logger.error("存储驾驶员登录信息异常:" + ex.getMessage(), ex);
			RedisConnectionPool.returnBrokenResource(vehicleInfoJedis);
			RedisConnectionPool.returnBrokenResource(jedis);
		} finally {
			RedisConnectionPool.returnJedisConnection(vehicleInfoJedis);
			RedisConnectionPool.returnJedisConnection(jedis);
		}
	}
	
	/*****************************************
	 * <li>描        述：存储驾驶员上报信息 		</li><br>
	 * <li>时        间：2013-12-12  下午3:24:22	</li><br>
	 * <li>参数： @param driverInfo			</li><br>
	 * 
	 *****************************************/
	private void saveDriverWork(DriverInfo driverInfo){
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveDriverWork);
			ps.setString(1, driverInfo.getAutoId());  				//记录编号
			ps.setString(2, driverInfo.getVid());					//车辆编号
			ps.setString(3, driverInfo.getPhoneNumber());			//手机号
			ps.setString(4, driverInfo.getStaffName());				//驾驶员姓名
			ps.setString(5, driverInfo.getIdNumber()); 				//身份证号 
			ps.setString(6, driverInfo.getQualificationId()); 		//从业资格证号
			ps.setString(7, driverInfo.getQualificationName()); 	//资格证发证机构名称 
			ps.setLong(8, driverInfo.getOnlineTime()); 				//上线时间
			ps.setInt(9,  driverInfo.getIcStatus());				//IC卡读取状态
			ps.setInt(10, driverInfo.getOnoffStatus());				//上下班状态   (1:上班;2:下班)
			ps.setString(11, driverInfo.getTeamId());				//所属车队编号
			ps.setLong(12, driverInfo.getQualificationValid());		//从业资格证有效期
			ps.setLong(13, driverInfo.getSysUtc());					//系统记录时间
			ps.setString(14, driverInfo.getPlate());				//车牌号码
			ps.setString(15, driverInfo.getPlateColor());			//车牌颜色
			ps.setString(16, driverInfo.getEntName());				//组织名称
			ps.executeUpdate();
		} catch (Exception e) {
			logger.error("更新驾驶员下线信息异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("更新驾驶员下线信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	/*****************************************
	 * <li>描        述：更新驾驶员上报信息 		</li><br>
	 * <li>时        间：2013-12-12  下午3:24:22	</li><br>
	 * <li>参数： @param driverInfo			</li><br>
	 * 
	 *****************************************/
	private void updateDriverWork(DriverInfo driverInfo){
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateDriverWork);
			ps.setLong(1, driverInfo.getOfflineTime()); 	//下线时间
			ps.setString(2, driverInfo.getAutoId());		//记录编号
			ps.executeUpdate();
		} catch (Exception e) {
			logger.error("更新驾驶员下线信息异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("更新驾驶员下线信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}


	/*****************************************
	 * <li>描 述：存储事件ID</li><br>
	 * <li>时 间：2013-9-12 下午6:53:04</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void saveEventId(Map<String, String> app) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveEventId);
			ps.setString(1, app.get(Constant.UUID));
			ps.setString(2, app.get(Constant.VID));
			ps.setString(3, app.get("81"));
			ps.setLong(4, DateTools.getCurrentUtcMsDate());
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("存储事件ID异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储事件ID关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 存储终端事件上报信息
	 * @param app
	 */
	public void saveDispatch(Map<String, String> app) {
		long s1 = System.currentTimeMillis();
		long s2 = s1;
		long s3 = s1;
		long s4 = s1;
		long s5 = s1;
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		String content = null;
		try {
			String contentId = app.get("81");
			String seq = app.get(Constant.SEQ);
			String vid = app.get(Constant.VID);
			String macid = app.get(Constant.MACID);
			if(StringUtils.isNumeric(contentId)){
				ServiceUnit serviceUnit = Cache.getVehicleMapValue(macid);
				s2 = System.currentTimeMillis();
				conn = OracleConnectionPool.getConnection();
				
				s3 = System.currentTimeMillis();
				ps = conn.prepareStatement(sql_queryDispatch);
				ps.setString(1, vid); 
				rs = ps.executeQuery();
				
				if(rs.next()){
					content = rs.getString("CONTENT");
				} 
				s4 = System.currentTimeMillis();
				saveNewMessageTips(serviceUnit.getTeamId(), serviceUnit.getVehicleno());
//				查询经纬度
				Points p = getPoints(vid);
				String context = getContext(content, contentId, vid);
				String id = GeneratorPK.instance().getPKString();
				long utc = System.currentTimeMillis();
				s5 = System.currentTimeMillis();
				
//				获取终端事件设置内容
				ps = conn.prepareStatement(sql_saveDispatch);
				ps.setString(1, id);
				ps.setString(2, vid);
				ps.setString(3, serviceUnit.getVehicleno());
				ps.setString(4, serviceUnit.getPlatecolorid());
				ps.setLong(5, utc);
				ps.setLong(6, utc);
				ps.setLong(7, utc);
				ps.setString(8, context);
				ps.setString(9, seq);
				ps.setInt(10, Integer.parseInt(contentId)); 
				ps.setLong(11, p.getLon());
				ps.setLong(12, p.getLat());
				ps.executeUpdate();
			}
			long end = System.currentTimeMillis();
			logger.debug("存储终端事件上报信息存储完成, 缓存获取[{}]ms, 连接获取[{}]ms, 查询[{}]ms, redis[{}]ms, 存储[{}]ms, 总耗时[{}]ms", s2-s1,s3-s2,s4-s3,s5-s4,end-s5,end-s1);
		} catch (Exception ex) {
			logger.error("存储终端事件上报信息异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if(rs != null){
					rs.close();
				}
				if(ps != null){
					ps.close();
				}
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储终端事件上报信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 获取车辆经纬度
	 * @param vid
	 * @return
	 */
	private Points getPoints(String vid) {
		Points p = new Points();
		Jedis jedis = null;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(6);
			String str = jedis.get(vid);
			if(str != null && str.length() > 0){
				String[] array = StringUtils.splitPreserveAllTokens(str, ":");
				if(array != null && array.length > 3){
					if(StringUtils.isNumeric(array[0]) && StringUtils.isNumeric(array[1])){
						p.setLat(Long.parseLong(array[0]));
						p.setLon(Long.parseLong(array[1]));
					}
				}
			}
		}catch(Exception e){
			logger.error("获取车辆经纬度异常:" + e.getMessage(), e);
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis);
			}
		}finally{
			if(jedis != null){
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
		return p;
	}

	/**
	 * 存储终端回传事件消息提示
	 * @param motorcadeKey 	车队编号
	 * @param plate			车牌号
	 */
	private void saveNewMessageTips(String motorcadeKey, String plate) {
//		获取车队编号
		String prompt = plate + " 新短消息";
		if(motorcadeKey != null && motorcadeKey.length() > 0){
			String promptKey = Cache.getOrgParent(motorcadeKey);// 根据车队编号获取车辆队列
			if(promptKey != null){
				motorcadeKey = promptKey + plate;
				Jedis jedis = null;
				try{
					jedis = RedisConnectionPool.getJedisConnection();
					jedis.select(7);
					String result = jedis.setex(motorcadeKey, 120, prompt);
					logger.debug("redis缓存回传事件消息提示[{}], Key[{}], value[{}]",result, motorcadeKey, prompt);
				}catch(Exception ex){
					if(jedis != null ){
						RedisConnectionPool.returnBrokenResource(jedis);
					}
					logger.error("存储终端回传事件消息提示异常:"+ ex.getMessage() ,ex);
				}finally{
					if(jedis != null ){
						RedisConnectionPool.returnJedisConnection(jedis); 
					}
				}
			}
		}
		logger.debug("存储终端回传事件消息提示, Key[{}], value[{}]", motorcadeKey, prompt);
	}

	/**
	 * 获取终端事件设置参数
	 * @param content
	 * @param contentId
	 * @param vid
	 * @return
	 */
	private String getContext(String content, String contentId, String vid) {
		if(StringUtils.isNotBlank(content)){
			String[] array = StringUtils.split(content, "#");
			for(String parmas : array){
				String[] parma = StringUtils.split(parmas, "=");
				if(parma.length == 2 && parma[0].equals(contentId)){
					return parma[1];
				}
			}
		} else {
			logger.debug("终端事件编号["+contentId+"],VID["+vid+"]未设置");
		}
		return "终端事件内容未设置!";
	}

	/*****************************************
	 * <li>描 述：更新历史提问</li><br>
	 * <li>时 间：2013-9-12 下午6:53:08</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void updateQuerstionAnswer(Map<String, String> app) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateQuerstionAnswer);
			ps.setLong(1, DateTools.getCurrentUtcMsDate());
			ps.setString(2, app.get("82"));
			ps.setString(3, app.get(Constant.SEQ));
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("更新历史提问异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("更新历史提问关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储油量监控信息</li><br>
	 * 
	 * <pre>
	 * 0~3	 纬度 DWORD 以度为单位的纬度值乘以10的6次方，精确到百万分之一度 
	 * 4~7	 经度 DWORD以度为单位的经度值乘以10的6次方，精确到百万分之一度 
	 * 8~9 	高程 WORD 海拔高度，单位为米（m）
	 * 10 ~11	速度 WORD  1/10km/h 
	 * 12~13 	方向 WORD 0 至 359， 正北为0，顺时针 
	 * 14~19	 时间 BCD[6] YY-MM-DD-hh-mm-ss（GMT+8时间）
	 * 20 	防偷漏油数据 BYTE[n] 防偷漏油数据内容
	 * -------------------------------------------------------------------------------
	 * | 0x81(发动机消息头) | 0x01(发动机协议版本标识) | 纬度| 经度 | 海拔 | 速度 | 方向 | 上报时间 | 防偷漏油数据 |
	 * -------------------------------------------------------------------------------
	 * </pre>
	 * 
	 * <li>时 间：2013-9-13 下午3:11:00</li><br>
	 * <li>参数： @param string <li>参数： @param parseLong</li><br>
	 * 
	 *****************************************/
	public void saveOilList(Map<String, String> app) {
		try {
			String value = app.get("90");
			String vid = app.get(Constant.VID);
//			获取油箱油量信息对象
			OilInfo oilInfo = Utils.getOilBase(value);
			if(oilInfo != null){
				oilInfo.setVid(vid); 
				if(oilInfo.getStatus().equals("10")){			// 油量变化
					saveOilChanged(oilInfo);
				} else if(oilInfo.getStatus().equals("01")){	//	偷漏油
					saveOilChanged(oilInfo);
					saveStealingOilAlarm(oilInfo);
				} else if(oilInfo.getStatus().equals("5")){		//	油量标定
					oilInfo.setSeq(app.get(Constant.SEQ));
					saveOilInfo(oilInfo);
				}
			} else {
				logger.error("油箱油量解析异常:{}", value);
			} 
		} catch (Exception e) {
			logger.error("存储油量监控信息异常:" + e.getMessage(), e);
		}
	}
	/**
	 * 更新或者插入油箱油量标定信息
	 * @param info
	 */
	public void saveOilInfo(OilInfo info) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveOilInfo);
			ps.setInt(1, info.getOilCalibration());
			ps.setInt(2, info.getGap());
			ps.setInt(3, info.getRefuelThreshold());
			ps.setInt(4, info.getStealThreshold());
			ps.setString(5, info.getSeq());
			ps.setString(6, info.getVid());
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("更新或者插入油箱油量标定信息异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("更新或者插入油箱油量标定信息关闭连接异常:" + e.getMessage(), e);
			}
		}
	}

	/****
	 * 存储油量变化记录 当为偷油告警和加油提示作为变化记录。
	 * 
	 * @param app
	 * @throws SQLException
	 */
	public void saveOilChanged(OilInfo oilInfo) throws SQLException {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			String changeId = GeneratorPK.instance().getPKString();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveOilChanged);
			ps.setString(1, oilInfo.getStatus()); // CHANGE_TYPE
			ps.setString(2, oilInfo.getVid()); // VID
			ps.setLong(3, DateTools.shortDateConvertUtc(oilInfo.getTime()));// UTC
			ps.setLong(4, oilInfo.getLat()); // LAT
			ps.setLong(5, oilInfo.getLon()); // LON
			Point point = Utils.convertLatLon(oilInfo.getLon(), oilInfo.getLat());
			if (point != null) {
				ps.setLong(6, Math.round(point.getX() * 600000)); // MAPLON
				ps.setLong(7, Math.round(point.getY() * 600000)); // MAPLAT
			} else {
				ps.setNull(6, Types.INTEGER);
				ps.setNull(7, Types.INTEGER);
			}
			ps.setInt(8, oilInfo.getElevation()); 			// ELEVATION 海拔
			ps.setInt(9, oilInfo.getDirection()); 			// DIRECTION 方向
			ps.setLong(10, oilInfo.getSpeed()); 			// GPS_SPEED 速度
			ps.setLong(11, DateTools.getCurrentUtcMsDate());// SYSUTC 系统时间
			ps.setLong(12, oilInfo.getFuelLevel()); 		// CURR_OILLEVEL 燃油液位
			ps.setLong(13, oilInfo.getOilAllowance()); 		// CURR_OILMAS 当前油量
			ps.setLong(14, oilInfo.getOilChange()); 		// CHANGE_OILMASS 变化油量
			ps.setString(15, changeId); 					// CHANGE_ID 变化油量编号
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("存储油量变化记录异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储油量变化记录关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	public void saveOilChanged(String baseInfo, String vid) throws SQLException {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			String changeId = GeneratorPK.instance().getPKString();
			String[] arr = baseInfo.split(":");
			if (arr.length == 10) {
				conn = OracleConnectionPool.getConnection();
				ps = conn.prepareStatement(sql_saveOilChanged);
				ps.setString(1, arr[6]);
				ps.setString(2, vid);
				ps.setLong(3, DateTools.shortDateConvertUtc(arr[5]));
				ps.setLong(4, Long.parseLong(arr[0]));
				ps.setLong(5, Long.parseLong(arr[1]));
				Point point = Utils.convertLatLon(Long.parseLong(arr[1]), Long.parseLong(arr[0]));
				if (point != null) {
					ps.setLong(6, Math.round(point.getX() * 600000));
					ps.setLong(7, Math.round(point.getY() * 600000));
				} else {
					ps.setNull(6, Types.INTEGER);
					ps.setNull(7, Types.INTEGER);
				}
				ps.setLong(8, Long.parseLong(arr[2]));
				ps.setLong(9, Long.parseLong(arr[4]));
				ps.setLong(10, Long.parseLong(arr[3]));
				ps.setLong(11, DateTools.getCurrentUtcMsDate());
				ps.setLong(12, Long.parseLong(arr[7]));
				ps.setLong(13, Long.parseLong(arr[9])); // 当前油量
				ps.setLong(14, Long.parseLong(arr[8])); // 变化油量
				ps.setString(15, changeId); // 变化油量编号
				ps.executeUpdate();
			}
		} catch (Exception ex) {
			logger.error("存储油量变化记录异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储油量变化记录关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	/******
	 * 存储偷油告警
	 * 
	 * @param msg
	 */
	public void saveStealingOilAlarm(OilInfo oilInfo) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			String uuid = GeneratorPK.instance().getPKString();
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveStealingOilAlarm);
			long utc = DateTools.getStringToDate(oilInfo.getTime());
			ps.setString(1, uuid);			//	ALARM_ID
			ps.setString(2, oilInfo.getVid());			//	VID
			ps.setLong(3, utc);//	UTC
			ps.setLong(4, oilInfo.getLat());//	LAT
			ps.setLong(5, oilInfo.getLon());//	LON
			Point point = Utils.convertLatLon(oilInfo.getLon(), oilInfo.getLat());//	
			long maplon = 0;
			long maplat = 0;
			if (point != null) {
				maplon = Math.round(point.getX() * 600000);
				maplat = Math.round(point.getY() * 600000);
				ps.setLong(6, maplon);//	MAPLON
				ps.setLong(7, maplat);//	MAPLAT
			} else {
				ps.setNull(6, Types.INTEGER);
				ps.setNull(7, Types.INTEGER);
			}
			ps.setLong(8, oilInfo.getElevation());//	ELEVATION
			ps.setLong(9, oilInfo.getDirection());//	DIRECTION
			ps.setLong(10, oilInfo.getSpeed());//	GPS_SPEED
			ps.setString(11, "111"); // 报警CODE ALARM_CODE
			ps.setLong(12, utc);//	SYSUTC
			ps.setLong(13, utc);//	ALARM_START_UTC
			ps.setString(14, "A005"); // BGLEVEL	所属类型
			ps.setLong(15, System.currentTimeMillis());//	ALARM_END_UTC	
			ps.setLong(16, oilInfo.getLat());//	END_LAT
			ps.setLong(17, oilInfo.getLon());//	END_LON
			if (point != null) {
				ps.setLong(18, maplat);	//	END_MAPLAT
				ps.setLong(19, maplon);//	END_MAPLON
			} else {
				ps.setNull(18, Types.INTEGER);
				ps.setNull(19, Types.INTEGER);
			}
			ps.setLong(20, oilInfo.getElevation());	//	END_ELEVATION
			ps.setLong(21, oilInfo.getDirection());	//	END_DIRECTION
			ps.setLong(22, oilInfo.getSpeed());	//	END_GPS_SPEED
			ps.executeUpdate();
		} catch (Exception ex) {
			logger.error("存储偷油告警异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if (ps != null) {
					ps.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储偷油告警关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	public void saveStealingOilAlarm(String msg, String vid, String uuid) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			String[] arr = msg.split(":");
			if (arr.length == 10) {
				conn = OracleConnectionPool.getConnection();
				ps = conn.prepareStatement(sql_saveStealingOilAlarm);
				ps.setString(1, uuid);
				ps.setString(2, vid);
				ps.setLong(3, DateTools.getStringToDate(arr[5]));
				ps.setLong(4, Long.parseLong(arr[0]));
				ps.setLong(5, Long.parseLong(arr[1]));
				Point point = Utils.convertLatLon(Long.parseLong(arr[1]), Long.parseLong(arr[0]));
				if (point != null) {
					ps.setLong(6, Math.round(point.getX() * 600000));
					ps.setLong(7, Math.round(point.getY() * 600000));
				} else {
					ps.setNull(6, Types.INTEGER);
					ps.setNull(7, Types.INTEGER);
				}
				ps.setLong(8, Long.parseLong(arr[2]));
				ps.setLong(9, Long.parseLong(arr[4]));
				ps.setLong(10, Long.parseLong(arr[3]));
				ps.setString(11, "111"); // 报警CODE
				ps.setLong(12, DateTools.getStringToDate(arr[5]));
				ps.setLong(13, DateTools.getStringToDate(arr[5]));
				ps.setString(14, "A005"); // 所属类型
				ps.setLong(15, DateTools.getCurrentUtcMsDate());
				ps.setLong(16, Long.parseLong(arr[0]));
				ps.setLong(17, Long.parseLong(arr[1]));
				point = Utils.convertLatLon(Long.parseLong(arr[1]), Long.parseLong(arr[0]));
				if (point != null) {
					ps.setLong(18, Math.round(point.getX() * 600000));
					ps.setLong(19, Math.round(point.getY() * 600000));
				} else {
					ps.setNull(18, Types.INTEGER);
					ps.setNull(19, Types.INTEGER);
				}
				ps.setLong(20, Long.parseLong(arr[2]));
				ps.setLong(21, Long.parseLong(arr[4]));
				ps.setLong(22, Long.parseLong(arr[3]));
				ps.executeUpdate();
			}
		} catch (Exception ex) {
			logger.error("存储偷油告警异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("存储偷油告警关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	/*****************************************
	 * <li>描 述：存储发动机故障信息</li><br>
	 * <li>时 间：2013-9-12 下午6:55:16</li><br>
	 * <li>参数： @param app 
	 * <li>参数： @param oracleJdbcService</li><br>
	 *****************************************/
	public void saveEngineFaultInfo(Map<String, String> dataMap) {
		// 1. 解析数据
		byte[] buf;
		// 解析为byte数组
		String engineStr = dataMap.get("90");
		buf = Base64_URl.base64DecodeToArray(engineStr);
		EngineFaultInfo engineFaultInfo = new EngineFaultInfo();
		engineFaultInfo.setVid(dataMap.get(Constant.VID));
		engineFaultInfo.setVehicleNo(dataMap.get(Constant.VEHICLENO));
		engineFaultInfo.setVinCode(dataMap.get(Constant.VIN_CODE));
		engineFaultInfo.setBasicCode(engineStr);
		engineFaultInfo.setCommaddr(dataMap.get(Constant.COMMDR));
		// 解析基础数据包
		String diagnosis = "";
		try {
			if (buf == null || buf.length < 23) {
				logger.warn("远程诊断解析非法数据,车牌号:{"+dataMap.get(Constant.VEHICLENO)+"},原始指令:" + dataMap.get(Constant.COMMAND)); 
				return;
			}
			engineFaultInfo = Tools.getBasicInfo(engineFaultInfo, buf);
			diagnosis = Tools.service.parseData(engineFaultInfo.getVinCode(), engineFaultInfo.getDiagnosisBytes());
		} catch (DiagException e1) {
			logger.error("远程诊断解析数据异常:" + e1.getMessage() + ",原始指令:" + dataMap.get(Constant.COMMAND), e1);
		}
		/*
		 * (char)0x01 ┌ (char)0x02 ┐ (char)0x03 └ (char)0x04 ┘ (char)0x05 │
		 */
		if (!"".equals(diagnosis)) {
			// 返回 系统类型
			String sysType = diagnosis.substring(0, diagnosis.indexOf(":"));
			// 返回数据类型
			String dataStr = diagnosis.substring(diagnosis.indexOf(":") + 1, diagnosis.length());
			// 0112:X0011┌空调压缩机开关故障┐历史故障└0011┘X0012┌HFM传感器进气温度故障;HFM传感器进气温度电压值超出上限门槛值┐现行故障└0012┘X0013┌空调压缩机开关故障┐└0013┘│
			if (sysType.equals("0112") || sysType.equals("12")) {
				try {
					String bugCode = "", bugDesc = "", bugFlag = "", basicCode = "";
					String[] bugArray = dataStr.split((char) 0x04 + "");
					for (int i = 0; i < bugArray.length; i++) {
						if (bugArray[i].indexOf((char) 0x02) > -1) {
							bugCode = bugArray[i].substring(0, bugArray[i].indexOf((char) 0x01));
							bugDesc = bugArray[i].substring(bugArray[i].indexOf((char) 0x01) + 1, bugArray[i].indexOf((char) 0x02));
							bugFlag = bugArray[i].substring(bugArray[i].indexOf((char) 0x02) + 1, bugArray[i].indexOf((char) 0x03));
							basicCode = bugArray[i].substring(bugArray[i].indexOf((char) 0x03) + 1, bugArray[i].length());
							logger.debug(" <远程诊断-->故障码bugCode: " + bugCode + " bugDesc: " + bugDesc + " bugFlag: " + bugFlag + " basicCode: " + basicCode);
							// 获得插入故障码表序列号,用来作为发送请求冻结帧的唯一标识
							engineFaultInfo.setBugId(GeneratorPK.instance().getPKString());
							if(bugCode == null){
								bugCode = "-1";
							}
							engineFaultInfo.setBugCode(bugCode);
							if(bugDesc == null){
								bugDesc = "无";
							}
							engineFaultInfo.setBugDesc(bugDesc);
							if(bugFlag == null){
								bugFlag = "-1";
							}
							engineFaultInfo.setBugFlag(bugFlag);
							if(basicCode == null){
								basicCode = "-1";
							}
							engineFaultInfo.setBasicCode(basicCode);
							saveEngBug(engineFaultInfo);
						}
					}
					if (bugArray.length == 0) {
						// 如果没有故障，就插入一条空数据，状态为02，代表没有故障
						engineFaultInfo.setStatus("02");
						engineFaultInfo.setBugId(GeneratorPK.instance().getPKString());
						engineFaultInfo.setBugCode("-1");
						engineFaultInfo.setBugDesc("无");
						engineFaultInfo.setBugFlag("-1");
						engineFaultInfo.setBasicCode("-1");
						saveEngBug(engineFaultInfo);
					}
				} catch (Exception e) {
					logger.error("--prase-EngBug-ERROR--解析发动机故障信息异常:", e);
				}
			}
			// // 清除故障码信息 (13),设置时间信息(01) 插入表ZSPT_ENG_MESSAGE
			// if (sysType.equals("0113") || sysType.equals("13")) {
			// try {
			// engineFaultInfo.setBugSeqId(dataMap.get(Constant.SEQ));
			// engineFaultInfo.setClearFlag("1");
			// commandDBAdapter.updateEngBugDispose(engineFaultInfo);
			// } catch (Exception e) {
			// logger.error("--clear-EngBug-ERROR--清除发动机故障信息异常:", e);
			// }
			// }
			// 0162:ECU版本号└10373223432┘ECU识别号└10001┘ECU零件号└T000001┘生产日期└2008-12-01┘│
			if (sysType.equals("0162") || sysType.equals("62")) {
				try {
					// 插入版本信息表
					String versionCode = "", versionValue = "";
					String[] versionArray = dataStr.split((char) 0x04 + "");
					for (int i = 0; i < versionArray.length; i++) {
						if (versionArray[i].indexOf((char) 0x03) > -1) {
							versionCode = versionArray[i].substring(0, versionArray[i].indexOf((char) 0x03));
							versionValue = versionArray[i].substring(versionArray[i].indexOf((char) 0x03) + 1, versionArray[i].length());
							// 赋值 版本代码,版本描述
							engineFaultInfo.setBugId(GeneratorPK.instance().getPKString());// 版本系列号
							engineFaultInfo.setVersionCode(versionCode); // 版本代码
							engineFaultInfo.setVersionValue(versionValue); // 版本描述
							engineFaultInfo.setReportTime(System.currentTimeMillis());
							saveEngVersionInfo(engineFaultInfo);
						}
					}
				} catch (Exception e) {
					logger.error("处理发动机故障信息异常:" + e.getMessage(),e);
				}
			}

		}
	}

	/*****************************************
	 * <li>描 述：存储发动机版本信息</li><br>
	 * <li>时 间：2013-8-8 下午11:54:00</li><br>
	 * <li>参数： @param engineFaultInfo</li><br>
	 * 
	 *****************************************/
	public void saveEngVersionInfo(EngineFaultInfo engineFaultInfo) {
		Connection conn = null;
		PreparedStatement deletePs = null;
		PreparedStatement savePs = null;
		try {
			conn = OracleConnectionPool.getConnection();
			// 删除原版本记录
			deletePs = conn.prepareStatement(sql_deleteEngVersionInfo);
			deletePs.setString(1, engineFaultInfo.getVid()); // VID
			deletePs.setString(2, engineFaultInfo.getVinCode()); // 所属车辆识别码
			deletePs.setString(3, engineFaultInfo.getCommaddr()); // 终端手机号
			deletePs.executeUpdate();
			// 存储新版本信息
			savePs = conn.prepareStatement(sql_saveEngVersionInfo);
			savePs.setString(1, engineFaultInfo.getBugId()); // 版本序列号
			savePs.setString(2, engineFaultInfo.getVid()); // VID
			savePs.setString(3, engineFaultInfo.getVehicleNo()); // 车牌号
			savePs.setString(4, engineFaultInfo.getVinCode()); // 所属车辆识别码
			savePs.setString(5, engineFaultInfo.getCommaddr()); // 终端手机号
			savePs.setString(6, engineFaultInfo.getVersionCode()); // 版本代码
			savePs.setString(7, engineFaultInfo.getVersionValue());// 版本描述
			savePs.setLong(8, engineFaultInfo.getReportTime()); // 上报日期
			savePs.executeUpdate();
		} catch (SQLException e) {
			logger.error("--saveEngVersionInfo--ERROR--存储发动机版本信息 异常:" + e.getMessage(), e);
		} finally {
			try {
				if(deletePs != null){
					deletePs.close();
				}
				if(savePs != null){
					savePs.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("--saveEngVersionInfo--ERROR-closed!-存储发动机版本信息 异常:关闭连接失败！", e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储发动机故障信息</li><br>
	 * <li>时 间：2013-8-7 下午7:27:16</li><br>
	 * <li>参数： @param engineFaultInfo</li><br>
	 * 
	 *****************************************/
	public void saveEngBug(EngineFaultInfo engineFaultInfo) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveEngBug);
			ps.setString(1, engineFaultInfo.getBugId()); // bugId
			ps.setString(2, engineFaultInfo.getVid()); // vid
			ps.setString(3, engineFaultInfo.getVehicleNo()); // 车牌号
			ps.setString(4, engineFaultInfo.getVinCode()); // 车架号
			ps.setString(5, engineFaultInfo.getCommaddr()); // 手机号
			ps.setString(6, engineFaultInfo.getStatus()); // 状态
			ps.setString(7, engineFaultInfo.getBugCode()); // 故障码
			ps.setString(8, engineFaultInfo.getBugDesc()); // 故障码描述
			ps.setString(9, engineFaultInfo.getBugFlag()); // 故障码状态说明
			ps.setString(10, engineFaultInfo.getBasicCode()); // 原始故障码
			ps.setLong(11, engineFaultInfo.getLat()); // 纬度
			ps.setLong(12, engineFaultInfo.getLon()); // 经度
			ps.setLong(13, engineFaultInfo.getMaplat()); // 偏移后纬度
			ps.setLong(14, engineFaultInfo.getMaplon()); // 偏移后经度
			ps.setLong(15, engineFaultInfo.getElevation()); // 海拔
			ps.setLong(16, engineFaultInfo.getDirection()); // 方向
			ps.setLong(17, engineFaultInfo.getGpsSpeeding());// GPS速度
			ps.setLong(18, engineFaultInfo.getReportTime());// 上报时间

			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("--saveEngBug--ERROR--存储发动机故障信息 异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("--saveEngBug--ERROR-closed!-存储发动机故障信息 异常:关闭连接失败！", e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储数据压缩上传</li><br>
	 * <li>时 间：2013-9-12 下午6:53:15</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
//	public void saveCompress(Map<String, String> app) {
//		Connection conn = null;
//		PreparedStatement ps = null;
//		try {
//			conn = OracleConnectionPool.getConnection();
//			ps = conn.prepareStatement(sql_saveCompress);
//			ps.setString(1, app.get(Constant.UUID));
//			ps.setLong(2, Long.parseLong(app.get(Constant.VID)));
//			ps.setLong(3, DateTools.getCurrentUtcMsDate());
//			ps.setString(4, Base64_URl.base64Decode(app.get("92")));
//			ps.executeUpdate();
//		} catch (SQLException e) {
//			logger.error("--saveCompress--ERROR--存储数据压缩上传异常:" + e.getMessage(), e);
//		} finally {
//			try {
//				if(ps != null){
//					ps.close();
//				}
//				if(conn != null){
//					conn.close();
//				}
//			} catch (Exception e) {
//				logger.error("--saveCompress--ERROR-closed!-存储数据压缩上传异常:关闭连接失败！", e);
//			}
//		}
//	}

	/*****************************************
	 * <li>描 述：存储数据上行透传</li><br>
	 * <li>时 间：2013-9-12 下午6:53:18</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void saveBridge(Map<String, String> app) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveBridge);
			ps.setString(1, app.get(Constant.UUID));
			ps.setString(2, app.get(Constant.VID));
			ps.setLong(3, DateTools.getCurrentUtcMsDate());
			ps.setString(4, Base64_URl.base64Decode(app.get("90")));
			ps.setInt(5, Integer.parseInt(app.get("91")));
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("--saveBridge--ERROR--存储数据上行透传异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("--saveBridge--ERROR-closed!-存储数据上行透传 异常:关闭连接失败！", e);
			}
		}
	}

	/*****************************************
	 * <li>描 述：存储行驶记录仪</li><br>
	 * <li>时 间：2013-9-12 下午6:53:21</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void saveRecorder(Map<String, String> app) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveRecorder);
			ps.setString(1, app.get(Constant.UUID));
			ps.setString(2, app.get(Constant.VID));
			ps.setLong(3, DateTools.getCurrentUtcMsDate());
			ps.setString(4, app.get("61"));
			ps.setString(5, app.get(Constant.SEQ));
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("--saveRecorder--ERROR--存储行驶记录仪异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("--saveRecorder--ERROR-closed!-存储行驶记录仪异常:关闭连接失败！", e);
			}
		}
	}


	/*****************************************
	 * <li>描 述：更新锁车状态</li><br>
	 * <li>时 间：2013-9-12 下午6:53:34</li><br>
	 * <li>参数： @param string 
	 * <li>参数： @param string2</li><br>
	 *  00：自动解锁成功；
		01：手动解锁成功；
		02：解锁码解锁成功；
		03：自动锁车成功；
		04：手动锁车成功；
	 *****************************************/
	public void updateLockVehicleStatus(String vid, String status) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			if(StringUtils.startsWithAny(status, "0","1","2")){
				status = "0"; //解锁成功
			} else {
				status = "1";//锁车成功
			}
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateLockVehicleStatus);
			//删除原版本记录
			//解锁码
			ps.setString(1, status);
			//更新时间
			ps.setLong(2, System.currentTimeMillis());	
			//VID
			ps.setString(3, vid);	
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("--updateLockVehicleStatus--ERROR--更新锁车状态异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("--updateLockVehicleStatus--ERROR-closed!-更新锁车状态异常:关闭连接失败！",e);
			}
		}
	}


	/*****************************************
	 * <li>描 述：更新照片状态</li><br>
	 * <li>时 间：2013-9-12 下午6:53:46</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void updateControlCommand(Map<String, String> app) {
		if(app == null){
			return ;
		}
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateControlCommand);
			int status = Integer.parseInt(app.get("RET"));
			String seq = app.get(Constant.SEQ);
			ps.setInt(1, status); // 发送状态-1等待回应 0:成功 3:设备不支持此功能 4:设备不在线 5:超时
			ps.setLong(2, DateTools.getCurrentUtcMsDate());
			ps.setString(3, app.get(Constant.CONTENT));
			ps.setString(4, seq);
			int i = ps.executeUpdate();
			logger.debug("更新D_SETP回应指令结束，更新条数:[" + i + "]条"); 
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
		} catch (SQLException e) {
			logger.error("更新照片状态异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新照片状态异常:关闭连接失败！", e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：更新触发拍照状态表 		</li><br>
	 * <li>时        间：2013-9-13  下午4:22:18	</li><br>
	 * <li>参数： @param seq
	 * <li>参数： @param status			</li><br>
	 * 
	 *****************************************/
	private void updatePhotoSetting(String seq, int status) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updatePhotoSetting);
			ps.setInt(1, status);
			ps.setString(2, seq);
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("更新触发拍照状态表 异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新触发拍照状态表 :关闭连接失败！", e);
			}
		}
		
	}

	/*****************************************
	 * <li>描        述：更新终端参数历史 		</li><br>
	 * <li>时        间：2013-9-13  下午4:22:16	</li><br>
	 * <li>参数： @param seq
	 * <li>参数： @param status			</li><br>
	 * 
	 *****************************************/
	private void updateTerminalHisParam(String seq, int status) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateTerminalHisParam);
			String opId = seq.split("_")[0];
			ps.setInt(1, status);
			ps.setLong(2, DateTools.getCurrentUtcMsDate());
			ps.setString(3, opId);
			ps.setString(4, seq);
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("更新终端参数历史异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新终端参数历史:关闭连接失败！", e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：更新电子围栏状态 		</li><br>
	 * <li>时        间：2013-9-13  下午4:22:14	</li><br>
	 * <li>参数： @param seq
	 * <li>参数： @param status			</li><br>
	 * 
	 *****************************************/
	private void updateLineSetting(String seq, int status) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateLineSetting);
			ps.setInt(1, status);
			ps.setLong(2, DateTools.getCurrentUtcMsDate());
			ps.setString(3, seq);
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("更新电子围栏状态异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新电子围栏状态:关闭连接失败！", e);
			}
		}
		
	}

	/*****************************************
	 * <li>描        述：更新电子围栏设置 		</li><br>
	 * <li>时        间：2013-9-13  下午4:22:11	</li><br>
	 * <li>参数： @param seq
	 * <li>参数： @param status			</li><br>
	 * 
	 *****************************************/
	private void updateAreaSetting(String seq, int status) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateAreaSetting);
			ps.setInt(1, status);
			ps.setLong(2, DateTools.getCurrentUtcMsDate());
			ps.setString(3, seq);
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("更新电子围栏设置异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新电子围栏设置:关闭连接失败！", e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：查询指令子类型 		</li><br>
	 * <li>时        间：2013-9-13  下午4:22:06	</li><br>
	 * <li>参数： @param seq
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	private String querySubType(String seq) {
		String subType = null;
		ResultSet rs = null;
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_querySubType);
			ps.setString(1, seq);
			rs = ps.executeQuery();
			if (rs.next()) {
				subType = rs.getString("CO_SUBTYPE");
			}
		} catch (SQLException e) {
			logger.error("查询指令子类型异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("查询指令子类型:关闭连接失败！", e);
			}
		}
		return subType;
	}

	/*****************************************
	 * <li>描 述：存储终端参数信息</li><br>
	 * <li>时 间：2013-9-12 下午6:53:52</li><br>
	 * <li>参数： @param app</li><br>
	 * @throws SQLException  
	 * 
	 *****************************************/
	public void terminalParamUpdate(Map<String, String> app) {
		String seq = app.get(Constant.SEQ);
		String tid = app.get(Constant.TID);
		String subType = querySubType(seq); 
//		subType = "0";
		if (subType != null && subType.equals("0")) { // 终端参数设置
//			String tId = Long.parseLong(tid);
			String content = app.get(Constant.CONTENT);
//			查询已有终端编号
			List<String> paramIdList = new ArrayList<String>();
			try {
				selectDBTerminalParam(paramIdList, tid);
			} catch (SQLException e) {
				logger.error("存储终端参数信息异常:查询已有终端参数编号异常" + e.getMessage(), e);
				return;
			}
//			解析终端参数对象列表
			String[] paramArray = StringUtils.split(content, ",");
			List<TerminalParam> saveList = new ArrayList<TerminalParam>(); 
			List<TerminalParam> updateList = new ArrayList<TerminalParam>(); 
			for(String params : paramArray){
				String[] param = StringUtils.splitPreserveAllTokens(params ,":", 2);
				if(param != null && param.length == 2){
					if (param[0].equals("TYPE") || param[0].equals("RET")) {
						continue;
					}
					String paramId = param[0].trim();
					TerminalParam  pt = new TerminalParam();
					pt.setParamKey(paramId);
					pt.setParamValue(param[1].trim());
					pt.setSysutc(System.currentTimeMillis());
					pt.setTid(tid);
					if(paramIdList.contains(paramId)){
						updateList.add(pt);
					} else {
						saveList.add(pt);
					}
				}
			}
//			存储终端参数对象列表
			if(updateList.size() > 0){
				updateBatchTerminalParam(updateList);
			}
			if(saveList.size() > 0){
				saveBatchTerminalParam(saveList);
			}
			updateList.clear();
			saveList.clear();
			paramIdList.clear();
		}
	}
	
	
	/*****************************************
	 * <li>描        述：批量存储终端参数 		</li><br>
	 * <li>时        间：2013-12-10  上午11:50:13	</li><br>
	 * <li>参数： @param tpList			</li><br>
	 * 
	 *****************************************/
	private void saveBatchTerminalParam(List<TerminalParam> tpList) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveBatchTernimalParam);
			for(TerminalParam tp : tpList){
				ps.setString(1, tp.getTid());
				ps.setString(2, tp.getParamKey());
				ps.setString(3, tp.getParamValue());
				ps.setLong(4, tp.getSysutc());
				ps.setLong(5, tp.getSysutc());
				ps.addBatch();
			}
			ps.executeBatch();
		} catch (SQLException e) {
			logger.error("批量存储参数设置异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("批量存储参数设置:关闭连接失败！", e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：批量更新参数设置 		</li><br>
	 * <li>时        间：2013-9-13  下午5:35:07	</li><br>
	 * <li>参数： @param vid
	 * <li>参数： @param key
	 * <li>参数： @param value			</li><br>
	 * 
	 *****************************************/
	private void updateBatchTerminalParam(List<TerminalParam> tpList) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateBatchTernimalParam);
			for(TerminalParam tp : tpList){
				ps.setLong(1, tp.getSysutc());
				ps.setString(2, tp.getParamValue());
				ps.setString(3, tp.getParamKey());
				ps.setString(4, tp.getTid());
				ps.addBatch();
			}
			ps.executeBatch();
		} catch (SQLException e) {
			logger.error("更新参数设置异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新参数设置:关闭连接失败！", e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：根据终端ID查询数据库终端参数值 		</li><br>
	 * <li>时        间：2013-9-13  下午5:09:44	</li><br>
	 * <li>参数： @param paramDBList
	 * <li>参数： @param tId			</li><br>
	 * 
	 *****************************************/
	private void selectDBTerminalParam(List<String> param, String tId) throws SQLException {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_selectDBTerminalParam);
			ps.setString(1, tId);
			rs = ps.executeQuery();
			while (rs.next()) {
				param.add(rs.getString("PARAM_ID"));
			}
		} catch (SQLException e) {
			throw e;
		} finally {
			try {
				if(rs != null){
					rs.close();
				}
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("根据终端ID查询数据库终端参数值 :关闭连接失败！", e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：存储或者更新远程锁车信息表 		</li><br>
	 * <li>时        间：2013-9-5  上午10:14:19	</li><br>
	 * <li>参数： @param vid			VID
	 * <li>参数： @param value			锁车控制|锁车类型|发动机最高转速|自锁预定时间|预警提醒锁车时间|车辆状态
	 *  锁车状态(0:未锁,1:已锁,2待锁; 3:锁车装置异常或被拆除 ;
	 * @param result 
	 *****************************************/
	public void saveOrUpdateLockVehicleDetail(String vid, String value, String result) { 
		if(!StringUtils.isNumeric(result)){ 
			logger.error("--saveOrUpdateLockVehicleDetail--ERROR--锁车返回结果异常,vid:" + vid +" value:"+value);
			return;
		}
		boolean resultError = false;
		if(!result.equals("0")){
//			返回结果异常
			resultError = true;
		}
		Connection conn = null;
		PreparedStatement ps = null;
		SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss");
		try {
			String[] lockArray = StringUtils.splitPreserveAllTokens(value, "|", 6);
			if(lockArray == null || lockArray.length != 6){
				logger.error("--saveOrUpdateLockVehicleDetail--ERROR--存储或者更新远程锁车信息表非法数据,vid:" + vid +" value:"+value);
				return;
			}
			conn = OracleConnectionPool.getConnection();
			if(resultError){
//				更新返回结果异常状态
//				指令状态:  1: 设备返回失败; 2:发送失败 ; 3: 设备不支持此功能; 4:设备不在线 ; 5:超时 ; 6:返回数据异常 ;
				ps = conn.prepareStatement(sql_updateLockCommandStatus);
				ps.setInt(1, Integer.parseInt(result));
				ps.setString(2, vid);	
			} else {
				ps = conn.prepareStatement(sql_saveOrUpdateLockVehicleDetail);
				//UUID 	
				ps.setString(1, GeneratorPK.instance().getPKString());		
				//VID
				ps.setString(2, vid);	
				//锁车状态(0:未锁,1:已锁,2待锁; 3:锁车装置异常或被拆除 ;)
				ps.setString(3, Tools.parseLockStatus(lockArray[5]));
				//锁车类型 01：限制转速 02：切断ACC电路 03：切断油路电路 
				ps.setString(4, "0"+lockArray[1]);		
				//发动机最高转速 
				String rpm = lockArray[2];
				if(rpm.equals("") || rpm.equals("-1")){
					ps.setNull(5, Types.INTEGER);			
				} else{
					ps.setLong(5, Long.parseLong(rpm));	
				}
				//预锁车时间
				if(lockArray[3].equals("") || lockArray[3].equals("-1")){
					ps.setNull(6, Types.INTEGER);	
				} else{
					ps.setLong(6, sdf.parse(lockArray[3]).getTime());	
				}
				ps.setInt(7, 0); //指令发送状态
			}
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("存储或者更新远程锁车信息表异常:" + e.getMessage(), e);
		} catch (ParseException e) {
			logger.error("存储或者更新远程锁车信息表--(yyyyMMddHHmmss)非法时间格式:"+value+"异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("存储或者更新远程锁车信息表:关闭连接失败！",e);
			}
		}
		
	}



	/*****************************************
	 * <li>描        述：更新 解锁码		</li><br>
	 * <li>时        间：2013-9-5  上午11:01:12	</li><br>
	 * <li>参数： @param vid			</li><br>
	 * @param unlockCode 	解锁码
	 * @param result 
	 * 
	 *****************************************/
	public void updateUnlockCode(String vid, String unlockCode, String result) {
		if(!StringUtils.isNumeric(result)){
			logger.error("--saveOrUpdateLockVehicleDetail--ERROR--锁车返回结果异常,vid:" + vid +" result:"+result);
			return;
		}
		boolean resultStatus = true;
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			if(!result.equals("0")){
				resultStatus = false;
			}
//			返回结果正常为0, 且为锁车应答     指令状态:  1: 设备返回失败; 2:发送失败 ; 3: 设备不支持此功能; 4:设备不在线 ; 5:超时 ; 6:返回数据异常 ;
			if(resultStatus && StringUtils.isAlphanumeric(unlockCode)){
				conn = OracleConnectionPool.getConnection();
				ps = conn.prepareStatement(sql_updateUnlockCode);
				//解锁码
				ps.setString(1, unlockCode);//解锁码
				ps.setString(2, "2");		// 待锁状态
				ps.setInt(3, 0);			//指令发送状态
				ps.setString(4, vid);	//VID
			} else {
				conn = OracleConnectionPool.getConnection();
				ps = conn.prepareStatement(sql_updateLockCommandStatus);
				ps.setInt(1, Integer.parseInt(result));
				ps.setString(2, vid);	
			}
			int i = ps.executeUpdate();
			if(unlockCode == null ){
				unlockCode = "空";
			}
			logger.debug("远程锁车executeUpdate执行结果:" + i + " , unlockCode:" + unlockCode);
		} catch (SQLException e) {
			logger.error("更新 解锁码异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新 解锁码异常:关闭连接失败！",e);
			}
		}
	}


	/*****************************************
	 * <li>描 述：更新调度信息</li><br>
	 * <li>时 间：2013-9-12 下午6:54:09</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 *****************************************/
	public void updateVehicleDispatchMsg(Map<String, String> app) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateVehicleDispatchMsg);
			String seq = app.get(Constant.SEQ);
			int status = Integer.parseInt(app.get("RET"));
			ps.setLong(1, DateTools.getCurrentUtcMsDate());
			ps.setInt(2, status); 	// 发送状态-1等待回应 0:成功 1:设备返回失败 2:发送失败3:设备不支持此功能4:设备不在线
			ps.setInt(3, 0);	 	// 是否已读 1-未读 0-已读
			ps.setString(4, seq);
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("更新调度信息异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新调度信息异常:关闭连接失败！",e);
			}
		}

	}

	/*****************************************
	 * <li>描 述：更新发动机故障处理信息</li><br>
	 * <li>时 间：2013-9-12 下午6:56:18</li><br>
	 * <li>参数： @param seq</li><br>
	 * 
	 *****************************************/
	public void updateEngBugDispose(String seq) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateEngBugDispose);
			ps.setString(1, "1");		//清除标记
			ps.setString(2, seq);			//故障序列号
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("更新发动机故障处理信息异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新发动机故障处理信息 异常:关闭连接失败！",e);
			}
		}

	}
	
	/*****************************************
	 * <li>描        述：初始化所有车辆信息 		</li><br>TODO
	 * <li>时        间：2013-9-10  上午10:53:43	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public void initAllVehilceCache() {
		logger.info("--initAllVehilceCache--车辆服务信息更新开始...");
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement stQueryAllVehicle = null;
		ResultSet rs = null;
		String oemcode;
		String t_identifyno;
		ServiceUnit su;
		// 从连接池获得连接
		int index = 0;
		try {
			conn =OracleConnectionPool.getConnection();
			stQueryAllVehicle = conn.prepareStatement(sql_initAllVehilceCache);
			rs = stQueryAllVehicle.executeQuery();
			while (rs.next()) {
				su = new ServiceUnit();
				oemcode = rs.getString("oemcode");// 车机类型码
				t_identifyno = rs.getString("t_identifyno");// 终端标识号
				su.setMacid(oemcode + "_" + t_identifyno);
				su.setSuid(rs.getString("suid"));
				su.setPlatecolorid(rs.getString("plate_color_id"));
				su.setVid(rs.getString("vid"));
				su.setTeminalCode(rs.getString("tmodel_code"));
				su.setTid(rs.getString("tid"));
				su.setCommaddr(t_identifyno);
				su.setOemcode(oemcode);
				su.setVehicleno(rs.getString("vehicle_no"));
				su.setVinCode(rs.getString("VIN_CODE"));
				su.setTeamId(rs.getString("TEAM_ID"));
				su.setVehicleType(rs.getString("VEHICLE_TYPE"));
				Cache.setVehicleMapValue(oemcode + "_" + t_identifyno, su);
				index++;
			}
		} catch (SQLException e) {
			logger.error("--initAllVehilceCache--初始化所有车辆信息异常:" + e.getMessage(),e);
		} finally {
			try {
				if(rs != null){
					rs.close();
				}
				if(stQueryAllVehicle != null){
					stQueryAllVehicle.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("--initAllVehilceCache--初始化所有车辆信息--关闭连接资源异常:" + e.getMessage(),e);
			}

		}
		long end = System.currentTimeMillis();
		logger.info("--initAllVehilceCache--车辆服务信息更新结束,更新车辆数:" + index + ",耗时:" + (end - start));
	}

	/*****************************************
	 * <li>描        述：更新3g手机号对应的车辆缓存信息 		</li><br>TODO
	 * <li>时        间：2013-7-24  上午9:57:13	</li><br>
	 * <li>参数： @param sql
	 * <li>参数： @throws SQLException			</li><br>
	 * 
	 *****************************************/
	public void update3GPhotoVehicleInfo() {
		logger.info("更新3g手机号对应的车辆缓存信息...");
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps= null;
		ResultSet rs = null;
		String oemcode;
		String t_identifyno;
		String mac_id;
		ServiceUnit su;
		int index  = 0;
		// 从连接池获得连接
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_update3GPhotoVehicleInfo);
			rs = ps.executeQuery();

			while (rs.next()) {
				su = new ServiceUnit();
				oemcode = rs.getString("oemcode");// 车机类型码
				t_identifyno = rs.getString("dvr_simnum");// 终端标识号
				mac_id = oemcode + "_" + t_identifyno;
				su.setMacid(mac_id);
				su.setSuid(rs.getString("suid"));
				su.setPlatecolorid(rs.getString("plate_color_id"));
				su.setVid(rs.getString("vid"));
				su.setTeminalCode(rs.getString("tmodel_code"));
				su.setTid(rs.getString("tid"));
				su.setCommaddr(rs.getString("t_identifyno"));
				su.setOemcode(oemcode);
				su.setVehicleno(rs.getString("vehicle_no"));
				su.setVinCode(rs.getString("VIN_CODE"));
				su.setTeamId(rs.getString("TEAM_ID"));
				su.setVehicleType(rs.getString("VEHICLE_TYPE"));
				Cache.setVehicleMapValue(mac_id, su);
				index++;
			}
		} catch (SQLException e) {
			logger.error("查询所有车辆"+ e.getMessage());
		} finally {
			try {
				if(rs != null){
					rs.close();
				}
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("--initAllVehilceCache--初始化所有车辆信息--关闭连接资源异常:" + e.getMessage(),e);
			}
		}
		long end = System.currentTimeMillis();
		logger.info("更新3g手机号对应的车辆缓存信息结束,更新数量:"+index+",耗时:"+(end - start));
	}
	/*****************************************
	 * <li>描        述：更新车辆缓存 		</li><br> TODO
	 * <li>参数：  		</li><br>
	 * <li>时        间：2013-9-20  下午8:34:21	</li><br>
	 * 
	 *****************************************/
	public void updateVehilceCache(Long interval) {
//		logger.info("更新不存在车辆缓存...");
		long start = System.currentTimeMillis();
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		Connection conn3g = null;
		PreparedStatement ps3g = null;
		ResultSet rs3g = null;
		
		String oemcode;
		String t_identifyno;
		ServiceUnit su;
		// 从连接池获得连接
		int index = 0;
		int index3g = 0;
		try {
			conn = OracleConnectionPool.getConnection();
//			logger.info(sql_updateVehicle);
			ps = conn.prepareStatement(sql_updateVehicle);
			ps.setLong(1, interval);
			ps.setLong(2, interval);
			ps.setLong(3, interval);
			ps.setLong(4, interval);
			ps.setLong(5, interval);
			ps.setLong(6, interval);
			ps.setLong(7, interval);
			ps.setLong(8, interval);
			rs = ps.executeQuery();
			while (rs.next()) {
				su = new ServiceUnit();
				oemcode = rs.getString("oemcode");// 车机类型码
				t_identifyno = rs.getString("t_identifyno");// 终端标识号
				su.setMacid(oemcode + "_" + t_identifyno);
				su.setSuid(rs.getString("suid"));
				su.setPlatecolorid(rs.getString("plate_color_id"));
				su.setVid(rs.getString("vid"));
				su.setTeminalCode(rs.getString("tmodel_code"));
				su.setTid(rs.getString("tid"));
				su.setCommaddr(t_identifyno);
				su.setOemcode(oemcode);
				su.setVehicleno(rs.getString("vehicle_no"));
				su.setVinCode(rs.getString("VIN_CODE"));
				su.setTeamId(rs.getString("TEAM_ID"));
				su.setVehicleType(rs.getString("VEHICLE_TYPE"));
				Cache.setVehicleMapValue(oemcode + "_" + t_identifyno, su);
				index++;
			}
			conn3g = OracleConnectionPool.getConnection();
//			logger.info(sql_update3GVehicle);
			ps3g = conn3g.prepareStatement(sql_update3GVehicle);
			ps3g.setLong(1, interval);
			ps3g.setLong(2, interval);
			ps3g.setLong(3, interval);
			ps3g.setLong(4, interval);
			ps3g.setLong(5, interval);
			ps3g.setLong(6, interval);
			ps3g.setLong(7, interval);
			ps3g.setLong(8, interval);
			ps3g.setLong(9, interval);
			ps3g.setLong(10, interval);
			rs3g = ps3g.executeQuery();
			String mac_id = "";
			while (rs3g.next()) {
				su = new ServiceUnit();
				oemcode = rs3g.getString("oemcode");// 车机类型码
				t_identifyno = rs3g.getString("dvr_simnum");// 终端标识号
				mac_id = oemcode + "_" + t_identifyno;
				su.setMacid(mac_id);
				su.setSuid(rs3g.getString("suid"));
				su.setPlatecolorid(rs3g.getString("plate_color_id"));
				su.setVid(rs3g.getString("vid"));
				su.setTeminalCode(rs3g.getString("tmodel_code"));
				su.setTid(rs3g.getString("tid"));
				su.setCommaddr(rs3g.getString("t_identifyno"));
				su.setOemcode(oemcode);
				su.setVehicleno(rs3g.getString("vehicle_no"));
				su.setVinCode(rs3g.getString("VIN_CODE"));
				su.setTeamId(rs3g.getString("TEAM_ID"));
				su.setVehicleType(rs.getString("VEHICLE_TYPE"));
				Cache.setVehicleMapValue(mac_id, su);
				index3g++;
			}
		} catch (SQLException e) {
			logger.error("更新不存在车辆缓存异常:" + e.getMessage(),e);
		} finally {
			try {
				if(rs != null){
					rs.close();
				}
				if(rs3g != null){
					rs3g.close();
				}
				if(ps != null){
					ps.close();
				}
				if(ps3g != null){
					ps3g.close();
				}
				if(conn != null){
					conn.close();
				}
				if(conn3g != null){
					conn3g.close();
				}
			} catch (SQLException e) {
				logger.error("更新车辆缓存--关闭连接资源异常:" + e.getMessage(),e);
			}

		}
		long end = System.currentTimeMillis();
		logger.info("更新车辆缓存结束,更新车辆数:[" + index + "]条, 3g视频终端["+index3g+"],耗时:" + (end - start));
	
		
	}
	/*****************************************
	 * <li>描        述：车辆清除更新任务 		</li><br>  TODO
	 * <pre>
	 * 每天凌晨3点半对缓存的车辆信息进行重新更新，删除所有缓存，然后更新最新数据。
	 * 解决部分车辆删除后还存在缓存中的问题
	 * </pre>
	 * <li>时        间：2013-9-22  上午9:47:26	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public  void clearUpdateVehicle() {
		long start = System.currentTimeMillis();
		synchronized (Cache.registerVehicleMap) {
			Cache.registerVehicleMap.clear();
			initAllVehilceCache();
			update3GPhotoVehicleInfo();
		}
		long end = System.currentTimeMillis();
		logger.info("车辆清除更新任务结束,耗时(ms):" + (end - start));
	}
	
	/*****************************************
	 * <li>描        述：存储质检单信息 		</li><br>
	 * <li>时        间：2013-10-8  上午10:29:51	</li><br>
	 * <li>参数： @param app			</li><br>
	 * TODO
	 *****************************************/
	public void saveQualityRecordInfo(Map<String, String> dataMap) {
		// 1. 解析数据
		byte[] buf;
		// 解析为byte数组
		buf = Base64_URl.base64DecodeToArray(dataMap.get("90"));
		QualityRecordBean qualityRecordBean = new QualityRecordBean();
		qualityRecordBean.setVid(dataMap.get(Constant.VID));
		qualityRecordBean.setCommaddr(dataMap.get(Constant.COMMDR));
		Connection conn = null;
		PreparedStatement ps= null;
		PreparedStatement ps0= null;
		try {
			if(buf == null ){
				logger.error("质检单非法数据:"+ buf + ",原始指令:"+ dataMap.get(Constant.COMMAND));
				return;
			}
			int loc = 0;
			//跳过版本号
			loc += 1;
			//消息ID
//			byte subcommand = buf[loc];
//			int subCommandId = subcommand;
			
			loc += 1;
			
			//解析终端时间
			byte timeBytes[] = new byte[6];		
			System.arraycopy(buf, loc, timeBytes, 0, 6);		
			String time = Converser.bcdToStr(timeBytes, 0, 6);
			
			Long utcTime = DateTools.stringConvertUtc(DateTools.changeDateFormat("yyMMddHHmmss", "yyyyMMdd/HHmmss", time));
			
			loc += 6;
			//解析车辆VIN
			byte vin[] = new byte[17];
			System.arraycopy(buf, loc, vin, 0, 17);
			String vinCode = new String(vin,"gbk");
			loc += 17;
			//解析终端配置
			byte cfg[] = new byte[2];
			cfg[1] = buf[loc];
			int terminalConfig = Converser.bytes2Short(cfg, 0);
			loc += 1;
			//特征系数
			byte plus[] = new byte[4];
			System.arraycopy(buf, loc, plus, 2, 2);
			int speedPlus = Converser.bytes2int(plus);
			loc += 2;
			//gprs强度
			byte strength[] = new byte[2];
			strength[1] = buf[loc];
			int gprsStrength= Converser.bytes2Short(strength, 0);
			loc += 1;
			//gps状态
			byte state[] = new byte[2];
			state[1] = buf[loc];
			int gpsState= Converser.bytes2Short(state, 0);
			
			loc += 1;
			
			//异常项数
			byte num[] = new byte[2];
			num[1] = buf[loc];
//			int exceptionNum = Converser.bytes2Short(num, 0);
			loc += 1;
			//检测项数
			byte num2[] = new byte[2];
			num2[1] = buf[loc];
			loc += 1;
			qualityRecordBean.setUtc(utcTime);
			qualityRecordBean.setVinCode(vinCode);
			qualityRecordBean.setTerminalConfig(""+terminalConfig);
			qualityRecordBean.setSpeedPlus(speedPlus);
			qualityRecordBean.setGprsStrength(""+gprsStrength);
			qualityRecordBean.setGpsState(""+gpsState);
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveQualityRecordInfo);
			//单项代号 和 状态 各占一个字节
			while (buf.length-loc>=2){
				//单项代号
				byte id[] = new byte[2];
				id[1] = buf[loc];
				int recordParam = Converser.bytes2Short(id, 0);
				loc += 1;
				//单项状态 （0|1）
				byte  val[] = new byte[2];
				val[1] = buf[loc];
				int recordValue =  Converser.bytes2Short(val, 0);
				loc +=1;
				try{
					ps.setString(1, GeneratorPK.instance().getPKString());		//记录编号
					ps.setString(2, qualityRecordBean.getVid());			//vid
					ps.setString(3, qualityRecordBean.getVinCode());	//vin
					ps.setLong(4, qualityRecordBean.getUtc());	//终端时间
					ps.setString(5, ""+recordParam);	//检测单项编号
					ps.setString(6, ""+recordValue);		//检测单项状态
					ps.setString(7, qualityRecordBean.getTerminalConfig());	//终端配置
					ps.setString(8, qualityRecordBean.getGprsStrength());	//GPRS强度
					ps.setString(9, qualityRecordBean.getGpsState()); 	//gps状态
					ps.setString(10, ""+qualityRecordBean.getSpeedPlus());	//特征系数
					ps.addBatch();
				}catch(Exception exx){
					logger.error("存储质检单信息-批量设置异常:" + exx.getMessage(),exx);
				}
			}
			ps.executeBatch();
			
			//获取缓存中的车辆最后位置信息
			// 获取车队编号、车队名称、组织编号、组织名称
			/*vehicleInfoJedis = RedisConnectionPool.getJedisConnection();
			vehicleInfoJedis.select(6);
			String vehicleInfo = vehicleInfoJedis.get(dataMap.get(Constant.VID));
			String[] array = null;
			if(vehicleInfo != null){
				array = StringUtils.splitPreserveAllTokens(vehicleInfo, ":", 45);
			}*/
			
			//保存质检单主表信息
			/*ID,VID,VEHICLE_NO_PLAT,PLATE_COLOR_PLAT,VIN_CODE_TER,
				VIN_CODE_PLAT,COMMADDR_PLAT,RECORD_TIME,
				ICCID_ELECTRON_PLAT,TMAC_PLAT,
				TPROTOCOL_VERSION_PLAT,HARD_VERSION_PLAT,SOFT_VERSION_PLAT,ALARM_CODE,BASESTATUS,
				EXTENDSTATUS,BATTERY_VOLTAGE,OIL_TEMPERATURE,E_WATER_TEMP,AIR_INFLOW_TPR,
				OIL_PRESSURE,AIR_PRESSURE,E_TORQUE*/
			ps0 = conn.prepareStatement(sql_saveQualityRecordCacheInfo);
			ps0.setString(1, GeneratorPK.instance().getPKString());//主表编号
			ps0.setString(2, dataMap.get(Constant.VID));//VID
			ps0.setString(3, dataMap.get(Constant.VEHICLENO));//车牌号
			ps0.setString(4, dataMap.get(Constant.PLATECOLORID));//车牌颜色
			ps0.setString(5, vinCode);//终端VINCODE
			ps0.setString(6, dataMap.get(Constant.VIN_CODE));//平台VINCODE
			ps0.setString(7, dataMap.get(Constant.COMMDR));//手机号
			ps0.setLong(8, utcTime);//上报时间
			
			
			//暂时先保存前7个字段，
			ps0.executeUpdate();
			
		} catch (UnsupportedEncodingException e) {
			logger.error("质检单解析数据异常:系统不支持此字符集；"+ e.getMessage() + ",原始指令:"+ dataMap.get(Constant.COMMAND), e);
		}catch (Exception e1) {
			logger.error("质检单解析数据异常:"+ e1.getMessage() + ",原始指令:"+ dataMap.get(Constant.COMMAND), e1);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException se) {
				logger.error("存储质检单信息-关闭连接资源异常:" + se.getMessage(),se);
			}
		}
		
	}
	
	/**
	 * 存储报警督办
	 * @param su
	 * TODO
	 */
	public void saveSupervision(Supervision su) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveSupervision);
			ps.setString(1, su.getId());
			ps.setString(2, su.getPlate());
			ps.setString(3, su.getPlateColor());
			ps.setInt(4, su.getSource());
			ps.setInt(5, su.getType());
			ps.setLong(6, su.getAlarmUtc());
			ps.setString(7, su.getSupervisionId());
			ps.setLong(8, su.getSupervisionDeadline());
			ps.setInt(9, su.getLevel());
			ps.setString(10, su.getSupervisor());
			ps.setString(11, su.getTel());
			ps.setString(12, su.getEmail());
			ps.setLong(13, su.getUtc());
			ps.setInt(14, su.getStatus());
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("存储报警督办异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("存储报警督办:关闭连接失败！", e);
			}
		}
	}
	/**
	 * 存储预警信息
	 * @param warn
	 * TODO
	 */ 
	public void saveWarning(Warning warn) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveWarning);
			ps.setString(1, warn.getId());
			ps.setString(2, warn.getSource());
			ps.setInt(3, warn.getType());
			ps.setLong(4, warn.getWarnUtc());
			ps.setString(5, warn.getDesc());
			ps.setString(6, warn.getVid());
			ps.setLong(7, warn.getUtc());
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("存储预警信息异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("存储预警信息:关闭连接失败！", e);
			}
		}
	}
	
	/**
	 * 存储平台信息
	 * TODO
	 */
	public void savePlatformMessage(PlatformMessage platformMessage) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_savePlatformMessage);
			ps.setString(1, platformMessage.getId());
			ps.setString(2, platformMessage.getContent());
			ps.setString(3, platformMessage.getMessageId());
			ps.setString(4, platformMessage.getOperatingLicense());
			ps.setInt(5, platformMessage.getOperatingType());
			ps.setLong(6, platformMessage.getUtc());
			ps.setInt(7, platformMessage.getOperateType());
			ps.setString(8, platformMessage.getAreaId());
			ps.setString(9, platformMessage.getSeq());
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("存储预警信息异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("存储预警信息:关闭连接失败！", e);
			}
		}
	}
	
	/**
	 * 存储平台上线
	 * @param ls
	 */
	public void savePlatformOnline(LinkStatus ls) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_savePlatformOnline);
			ps.setString(1, ls.getId());
			ps.setString(2, ls.getAreaId());
			ps.setInt(3, ls.getLikeType());
			ps.setLong(4, ls.getOnlineUtc());
			ps.setLong(5, ls.getUtc());
			ps.setString(6, ls.getAccessCode());
			ps.setString(7, ls.getChannelCode());
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("存储平台上线异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("存储平台上线:关闭连接失败！", e);
			}
		}
	}
	/**
	 * 存储平台下线
	 * @param ls
	 */
	public void savePlatformOffline(LinkStatus ls) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_savePlatformOffline);
			ps.setLong(1, ls.getOfflineUtc());
			ps.setString(2, ls.getId());
			ps.executeUpdate();
		} catch (SQLException e) {
			logger.error("存储平台下线异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("存储平台下线:关闭连接失败！", e);
			}
		}
	}
	
	/**
	 * 保存最后终端程序版本信息记录
	 */
	public void saveOrUpdateTerminalVersion(List<TerminalVersion> list, int batchSize) {
		Connection conn = null;
		PreparedStatement updatePs = null;
		PreparedStatement insertPs = null;
		PreparedStatement psQuery = null;
		ResultSet rsQuery = null;
		List<String> vidList = null;
		List<TerminalVersion> insertList = null;
		List<TerminalVersion> updateList = null;
		int index = 0 ;
		try {
//			查询数据库中的列表
			vidList = new ArrayList<String>();
			conn = OracleConnectionPool.getConnection();
			psQuery = conn.prepareStatement(sql_queryTernimalRecord);	// 	查询已有记录的VID列表
			rsQuery = psQuery.executeQuery();
			while (rsQuery.next()) {
				vidList.add(rsQuery.getString(1)); 
			}
//			logger.info("vidList.size():" + vidList.size());
			insertList = new ArrayList<TerminalVersion>();
			updateList = new ArrayList<TerminalVersion>();
			for(TerminalVersion terminalVersion : list){
				if(vidList.contains(terminalVersion.getVid()) == true){		// 判断记录是更新 还是 插入
					updateList.add(terminalVersion);
				} else {
					insertList.add(terminalVersion);
				}
			}
			updatePs = conn.prepareStatement(sql_updateTernimalRecord);
			for(TerminalVersion terminalVersion : updateList){		// 批量更新
				index++;
				updatePs.setLong(1, terminalVersion.getSysUtc());// 时间
				updatePs.setString(2, terminalVersion.getVinCode());//VIN
				updatePs.setString(3, terminalVersion.getPlate());//车牌号
				updatePs.setString(4, terminalVersion.getPlateColor());//车牌颜色(不以ASCII码表表示数字的方式表示车牌颜色，统一按照JT/T415-2006定义标准定义车牌颜色，0x00—未上牌，0x01—蓝色，0x02—黄色，0x03—黑色，0x04—白色，0x09—其它)
				updatePs.setString(5, terminalVersion.getPhoneNumber());
				updatePs.setString(6, terminalVersion.getIccid());//SIM卡ICCID
				updatePs.setString(7, terminalVersion.gettMac());//终端号（ID）
				updatePs.setString(8, terminalVersion.gettProtocolVersion());//终端协议版本号
				updatePs.setString(9, terminalVersion.getTerminalHardVersion());//终端硬件版本号
				updatePs.setString(10, terminalVersion.getTerminalFirmwareVersion());//终端固件版本号
				updatePs.setString(11, terminalVersion.getLcdHardVersion());//显示屏硬件版本号
				updatePs.setString(12, terminalVersion.getLcdFirmwareVersion());//显示屏固件版本号
				updatePs.setString(13, terminalVersion.getDvrHardVersion());//硬盘录像机硬件版本号
				updatePs.setString(14, terminalVersion.getDvrFirmwareVersion());//硬盘录像机固件版本号
				updatePs.setString(15, terminalVersion.getRfCardHardVersion());//射频读卡器硬件版本号
				updatePs.setString(16, terminalVersion.getRfCardFirmwareVersion());//射频读卡器固件版本号
				updatePs.setString(17, terminalVersion.getVid()); // 车辆编号
				updatePs.addBatch();
				if(index == batchSize){
					updatePs.executeBatch();
					updatePs.clearBatch();
					index = 0;
				}
			}
			if(index > 0){
				updatePs.executeBatch();
				updatePs.clearBatch();
			}
			/**
			 *  INSERT INTO TH_TERMINAL_VERSION_RECORD(ID,VID,VIN_CODE,VEHICLE_NO,PLATE_COLOR,COMMADDR,
				     ICCID_ELECTRON,TMAC,TPROTOCOL_VERSION,HARDWARE_VERSION,FIRMWARE_VERSION,DISPLAY_HARD_VERSION,
				     DISPALY_FIRM_VERSION,VCR_HARD_VERSION,VCR_FIRM_VERSION,READER_HARD_VERSION,READER_FIRM_VERSION,RECORD_TIME)
				  VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)
			 */
			insertPs = conn.prepareStatement(sql_insertTernimalRecord);
			
			for(TerminalVersion terminalVersion : insertList){		// 批量插入
				index++;
				insertPs.setString(1, terminalVersion.getUuid());// 主键
				insertPs.setString(2, terminalVersion.getVid());// 车辆编号
				insertPs.setString(3, terminalVersion.getVinCode());//VIN
				insertPs.setString(4, terminalVersion.getPlate());//车牌号
				insertPs.setString(5, terminalVersion.getPlateColor());//车牌颜色(不以ASCII码表表示数字的方式表示车牌颜色，统一按照JT/T415-2006定义标准定义车牌颜色，0x00—未上牌，0x01—蓝色，0x02—黄色，0x03—黑色，0x04—白色，0x09—其它)
				insertPs.setString(6, terminalVersion.getPhoneNumber());//手机号
				insertPs.setString(7, terminalVersion.getIccid());//SIM卡ICCID
				insertPs.setString(8, terminalVersion.gettMac());//终端号（ID）
				insertPs.setString(9, terminalVersion.gettProtocolVersion());//终端协议版本号
				insertPs.setString(10, terminalVersion.getTerminalHardVersion());//终端硬件版本号
				insertPs.setString(11, terminalVersion.getTerminalFirmwareVersion());//终端固件版本号
				insertPs.setString(12, terminalVersion.getLcdHardVersion());//显示屏硬件版本号
				insertPs.setString(13, terminalVersion.getLcdFirmwareVersion());//显示屏固件版本号
				insertPs.setString(14, terminalVersion.getDvrHardVersion());//硬盘录像机硬件版本号
				insertPs.setString(15, terminalVersion.getDvrFirmwareVersion());//硬盘录像机固件版本号
				insertPs.setString(16, terminalVersion.getRfCardHardVersion());//射频读卡器硬件版本号
				insertPs.setString(17, terminalVersion.getRfCardFirmwareVersion());//射频读卡器固件版本号
				insertPs.setLong(18, terminalVersion.getSysUtc()); // 时间
				insertPs.addBatch();
				if(index == batchSize){
					insertPs.executeBatch();
					insertPs.clearBatch();
					index = 0;
				}
			}
			if(index > 0){
				insertPs.executeBatch();
				insertPs.clearBatch();
			}
			
		} catch (Exception e) {
			logger.error("批量更新终端版本号异常:" + e.getMessage(), e);
		} finally {
			// 关闭 清理资源
			try {
				if(vidList != null && vidList.size() > 0){ vidList.clear();}
				if(insertList != null && insertList.size() > 0){ insertList.clear();}
				if(updateList != null && updateList.size() > 0){ updateList.clear();}
				if(rsQuery != null){ rsQuery.close(); }
				if(psQuery != null){ psQuery.close(); }
				if(updatePs != null){ updatePs.close(); }
				if(insertPs != null){ insertPs.close(); }
				if(conn != null){ conn.close(); }
			} catch (SQLException e) {
				logger.error("批量更新终端版本号关闭连接异常:" + e.getMessage(), e);
			}
		}
	}
	/*---------------------------------------getter & setter-------------------------------------------*/
	public String getSql_saveControlCommand() {
		return sql_saveControlCommand;
	}
	public void setSql_saveControlCommand(String sql_saveControlCommand) {
		this.sql_saveControlCommand = sql_saveControlCommand;
	}
	public String getSql_saveMultMedia() {
		return sql_saveMultMedia;
	}
	public void setSql_saveMultMedia(String sql_saveMultMedia) {
		this.sql_saveMultMedia = sql_saveMultMedia;
	}
	public String getSql_saveVehicleDispatchMsg() {
		return sql_saveVehicleDispatchMsg;
	}
	public void setSql_saveVehicleDispatchMsg(String sql_saveVehicleDispatchMsg) {
		this.sql_saveVehicleDispatchMsg = sql_saveVehicleDispatchMsg;
	}
	public String getSql_updateTernimalVersion() {
		return sql_updateTernimalVersion;
	}
	public void setSql_updateTernimalVersion(String sql_updateTernimalVersion) {
		this.sql_updateTernimalVersion = sql_updateTernimalVersion;
	}
	public String getSql_queryTernimalRecord() {
		return sql_queryTernimalRecord;
	}
	public void setSql_queryTernimalRecord(String sql_queryTernimalRecord) {
		this.sql_queryTernimalRecord = sql_queryTernimalRecord;
	}
	public String getSql_updateTernimalRecord() {
		return sql_updateTernimalRecord;
	}
	public void setSql_updateTernimalRecord(String sql_updateTernimalRecord) {
		this.sql_updateTernimalRecord = sql_updateTernimalRecord;
	}
	public String getSql_insertTernimalRecord() {
		return sql_insertTernimalRecord;
	}
	public void setSql_insertTernimalRecord(String sql_insertTernimalRecord) {
		this.sql_insertTernimalRecord = sql_insertTernimalRecord;
	}
	public String getSql_updateSIMICCID() {
		return sql_updateSIMICCID;
	}
	public void setSql_updateSIMICCID(String sql_updateSIMICCID) {
		this.sql_updateSIMICCID = sql_updateSIMICCID;
	}
	public String getSql_saveMultimediaEvent() {
		return sql_saveMultimediaEvent;
	}
	public void setSql_saveMultimediaEvent(String sql_saveMultimediaEvent) {
		this.sql_saveMultimediaEvent = sql_saveMultimediaEvent;
	}
	public String getSql_saveEticket() {
		return sql_saveEticket;
	}
	public void setSql_saveEticket(String sql_saveEticket) {
		this.sql_saveEticket = sql_saveEticket;
	}
	public String getSql_saveTernimalRegisterInfo() {
		return sql_saveTernimalRegisterInfo;
	}
	public void setSql_saveTernimalRegisterInfo(String sql_saveTernimalRegisterInfo) {
		this.sql_saveTernimalRegisterInfo = sql_saveTernimalRegisterInfo;
	}
	public String getSql_saveVehicleLogOff() {
		return sql_saveVehicleLogOff;
	}
	public void setSql_saveVehicleLogOff(String sql_saveVehicleLogOff) {
		this.sql_saveVehicleLogOff = sql_saveVehicleLogOff;
	}
	public String getSql_updateVehicleLogOff() {
		return sql_updateVehicleLogOff;
	}
	public void setSql_updateVehicleLogOff(String sql_updateVehicleLogOff) {
		this.sql_updateVehicleLogOff = sql_updateVehicleLogOff;
	}
	public String getSql_saveVehicleAKey() {
		return sql_saveVehicleAKey;
	}
	public void setSql_saveVehicleAKey(String sql_saveVehicleAKey) {
		this.sql_saveVehicleAKey = sql_saveVehicleAKey;
	}
	public String getSql_saveDriverInfo() {
		return sql_saveDriverInfo;
	}
	public String getSql_saveDriverWork() {
		return sql_saveDriverWork;
	}
	public void setSql_saveDriverWork(String sql_saveDriverWork) {
		this.sql_saveDriverWork = sql_saveDriverWork;
	}
	public String getSql_updateDriverWork() {
		return sql_updateDriverWork;
	}
	public void setSql_updateDriverWork(String sql_updateDriverWork) {
		this.sql_updateDriverWork = sql_updateDriverWork;
	}
	public void setSql_saveDriverInfo(String sql_saveDriverInfo) {
		this.sql_saveDriverInfo = sql_saveDriverInfo;
	}
	public String getSql_getDriverInfo() {
		return sql_getDriverInfo;
	}
	public void setSql_getDriverInfo(String sql_getDriverInfo) {
		this.sql_getDriverInfo = sql_getDriverInfo;
	}
	public String getSql_saveEventId() {
		return sql_saveEventId;
	}
	public void setSql_saveEventId(String sql_saveEventId) {
		this.sql_saveEventId = sql_saveEventId;
	}
	public String getSql_updateQuerstionAnswer() {
		return sql_updateQuerstionAnswer;
	}
	public void setSql_updateQuerstionAnswer(String sql_updateQuerstionAnswer) {
		this.sql_updateQuerstionAnswer = sql_updateQuerstionAnswer;
	}
	public String getSql_saveOilChanged() {
		return sql_saveOilChanged;
	}
	public void setSql_saveOilChanged(String sql_saveOilChanged) {
		this.sql_saveOilChanged = sql_saveOilChanged;
	}
	public String getSql_saveStealingOilAlarm() {
		return sql_saveStealingOilAlarm;
	}
	public void setSql_saveStealingOilAlarm(String sql_saveStealingOilAlarm) {
		this.sql_saveStealingOilAlarm = sql_saveStealingOilAlarm;
	}
	public String getSql_deleteEngVersionInfo() {
		return sql_deleteEngVersionInfo;
	}
	public void setSql_deleteEngVersionInfo(String sql_deleteEngVersionInfo) {
		this.sql_deleteEngVersionInfo = sql_deleteEngVersionInfo;
	}
	public String getSql_saveEngVersionInfo() {
		return sql_saveEngVersionInfo;
	}
	public void setSql_saveEngVersionInfo(String sql_saveEngVersionInfo) {
		this.sql_saveEngVersionInfo = sql_saveEngVersionInfo;
	}
	public String getSql_saveEngBug() {
		return sql_saveEngBug;
	}
	public void setSql_saveEngBug(String sql_saveEngBug) {
		this.sql_saveEngBug = sql_saveEngBug;
	}
	public String getSql_saveBridge() {
		return sql_saveBridge;
	}
	public void setSql_saveBridge(String sql_saveBridge) {
		this.sql_saveBridge = sql_saveBridge;
	}
	public String getSql_saveCompress() {
		return sql_saveCompress;
	}
	public void setSql_saveCompress(String sql_saveCompress) {
		this.sql_saveCompress = sql_saveCompress;
	}
	public String getSql_saveRecorder() {
		return sql_saveRecorder;
	}
	public void setSql_saveRecorder(String sql_saveRecorder) {
		this.sql_saveRecorder = sql_saveRecorder;
	}
	public String getSql_saveInfoplay() {
		return sql_saveInfoplay;
	}
	public void setSql_saveInfoplay(String sql_saveInfoplay) {
		this.sql_saveInfoplay = sql_saveInfoplay;
	}
	public String getSql_updateControlCommand() {
		return sql_updateControlCommand;
	}
	public void setSql_updateControlCommand(String sql_updateControlCommand) {
		this.sql_updateControlCommand = sql_updateControlCommand;
	}
	public String getSql_updatePhotoSetting() {
		return sql_updatePhotoSetting;
	}
	public void setSql_updatePhotoSetting(String sql_updatePhotoSetting) {
		this.sql_updatePhotoSetting = sql_updatePhotoSetting;
	}
	public String getSql_updateTerminalHisParam() {
		return sql_updateTerminalHisParam;
	}
	public void setSql_updateTerminalHisParam(String sql_updateTerminalHisParam) {
		this.sql_updateTerminalHisParam = sql_updateTerminalHisParam;
	}
	public String getSql_updateLineSetting() {
		return sql_updateLineSetting;
	}
	public void setSql_updateLineSetting(String sql_updateLineSetting) {
		this.sql_updateLineSetting = sql_updateLineSetting;
	}
	public String getSql_updateAreaSetting() {
		return sql_updateAreaSetting;
	}
	public void setSql_updateAreaSetting(String sql_updateAreaSetting) {
		this.sql_updateAreaSetting = sql_updateAreaSetting;
	}
	public String getSql_updateLockVehicleStatus() {
		return sql_updateLockVehicleStatus;
	}
	public void setSql_updateLockVehicleStatus(String sql_updateLockVehicleStatus) {
		this.sql_updateLockVehicleStatus = sql_updateLockVehicleStatus;
	}
	public String getSql_saveMediaIdx() {
		return sql_saveMediaIdx;
	}
	public void setSql_saveMediaIdx(String sql_saveMediaIdx) {
		this.sql_saveMediaIdx = sql_saveMediaIdx;
	}
	public String getSql_updateEngBugDispose() {
		return sql_updateEngBugDispose;
	}
	public void setSql_updateEngBugDispose(String sql_updateEngBugDispose) {
		this.sql_updateEngBugDispose = sql_updateEngBugDispose;
	}
	public String getSql_selectDBTerminalParam() {
		return sql_selectDBTerminalParam;
	}
	public void setSql_selectDBTerminalParam(String sql_selectDBTerminalParam) {
		this.sql_selectDBTerminalParam = sql_selectDBTerminalParam;
	}
	public String getSql_saveBatchTernimalParam() {
		return sql_saveBatchTernimalParam;
	}
	public void setSql_saveBatchTernimalParam(String sql_saveBatchTernimalParam) {
		this.sql_saveBatchTernimalParam = sql_saveBatchTernimalParam;
	}
	public String getSql_updateBatchTernimalParam() {
		return sql_updateBatchTernimalParam;
	}
	public void setSql_updateBatchTernimalParam(String sql_updateBatchTernimalParam) {
		this.sql_updateBatchTernimalParam = sql_updateBatchTernimalParam;
	}
	public String getSql_saveOrUpdateLockVehicleDetail() {
		return sql_saveOrUpdateLockVehicleDetail;
	}
	public void setSql_saveOrUpdateLockVehicleDetail(String sql_saveOrUpdateLockVehicleDetail) {
		this.sql_saveOrUpdateLockVehicleDetail = sql_saveOrUpdateLockVehicleDetail;
	}
	public String getSql_updateUnlockCode() {
		return sql_updateUnlockCode;
	}
	public void setSql_updateUnlockCode(String sql_updateUnlockCode) {
		this.sql_updateUnlockCode = sql_updateUnlockCode;
	}
	public String getSql_updateVehicleDispatchMsg() {
		return sql_updateVehicleDispatchMsg;
	}
	public void setSql_updateVehicleDispatchMsg(String sql_updateVehicleDispatchMsg) {
		this.sql_updateVehicleDispatchMsg = sql_updateVehicleDispatchMsg;
	}
	public String getSql_querySubType() {
		return sql_querySubType;
	}
	public void setSql_querySubType(String sql_querySubType) {
		this.sql_querySubType = sql_querySubType;
	}
	public String getSql_initAllVehilceCache() {
		return sql_initAllVehilceCache;
	}
	public void setSql_initAllVehilceCache(String sql_initAllVehilceCache) {
		this.sql_initAllVehilceCache = sql_initAllVehilceCache;
	}
	public String getSql_update3GPhotoVehicleInfo() {
		return sql_update3GPhotoVehicleInfo;
	}
	public void setSql_update3GPhotoVehicleInfo(String sql_update3GPhotoVehicleInfo) {
		this.sql_update3GPhotoVehicleInfo = sql_update3GPhotoVehicleInfo;
	}
	public String getSql_updateVehicle() {
		return sql_updateVehicle;
	}
	public void setSql_updateVehicle(String sql_updateVehicle) {
		this.sql_updateVehicle = sql_updateVehicle;
	}
	public String getSql_update3GVehicle() {
		return sql_update3GVehicle;
	}
	public void setSql_update3GVehicle(String sql_update3GVehicle) {
		this.sql_update3GVehicle = sql_update3GVehicle;
	}
	public String getSql_saveQualityRecordInfo() {
		return sql_saveQualityRecordInfo;
	}
	public void setSql_saveQualityRecordInfo(String sql_saveQualityRecordInfo) {
		this.sql_saveQualityRecordInfo = sql_saveQualityRecordInfo;
	}

	public String getResult(String sql) {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql);
			rs = ps.executeQuery();
			ResultSetMetaData rsmd = rs.getMetaData();
			int columnCount = rsmd.getColumnCount();
			StringBuffer sb = new StringBuffer();
			while (rs.next()) {
				for (int i = 1; i <= columnCount; i++) {
					sb.append(rsmd.getColumnName(i)).append("=").append(rs.getString(i)).append(",");
				}
				sb.append("\r\n");
			}
			return sb.toString();
		} catch (Exception e) {
			logger.error(e.getMessage(), e);
			return null;
		} finally {
			try {
				if (null != rs) {
					rs.close();
				}
				if (null != ps) {
					ps.close();
				}
				if (null != conn) {
					conn.close();
				}
			} catch (SQLException e) {
				logger.error(e.getMessage(), e);
			}
		}
	}
	
	/**
	 * 查询终端国标版本号
	 * @param warn
	 * TODO
	 */ 
	public String queryTerminalStandardVersion(String tid) {
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		String version = "03";
		try {
			conn = OracleConnectionPool.getConnection();
			if (conn!=null){
				ps = conn.prepareStatement(sql_queryTerminalStandardVersion);
				ps.setString(1, tid);
				rs = ps.executeQuery();
				if (rs.next()) {
					version = rs.getString("STANDARD_VERSION");
				}
			}
		} catch (SQLException e) {
			logger.error("TID:"+tid+" 查询终端国标版本号异常:" + e.getMessage(), e);
		} finally {
			try {
				if (rs!=null){
					rs.close();
				}
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("查询国标版本号:关闭连接失败！", e);
			}
		}
		return version;
	}
	
	/**
	 * 更新终端国标版本号
	 * @param warn
	 * TODO
	 */ 
	public void updateTerminalStandardVersion(String tid,String version) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			if (conn!=null){
				ps = conn.prepareStatement(sql_updateTerminalStandardVersion);
				ps.setString(1, version);
				ps.setString(2, tid);
				ps.executeUpdate();
			}
		} catch (SQLException e) {
			logger.error("TID:"+tid+" 更新终端国标版本号异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新国标版本号:关闭连接失败！", e);
			}
		}
	}
	
	
	public void saveAccidentPointsData(List<AccidentDoubpointsMain> list){
		Connection conn = null;
		PreparedStatement dbPstmt4 = null;
		PreparedStatement dbPstmt5 = null;
		PreparedStatement dbPstmt6 = null;
		try{
			if (list == null || list.size() == 0) return;
			conn = OracleConnectionPool.getConnection();
			if (conn!=null){
			for (int i=0;i<list.size();i++){
				AccidentDoubpointsMain adm = list.get(i);
				
				//按当前条件删除数据，防止数据重复(重复提取时会重复)
				dbPstmt4 = conn.prepareStatement(this.sql_delDoubpointsMainAndDetail);
				dbPstmt4.setString(1, adm.getVid());
				dbPstmt4.setLong(2, adm.getStopTime());
				dbPstmt4.setString(3, adm.getVid());
				dbPstmt4.setLong(4, adm.getStopTime());
				dbPstmt4.executeUpdate();
				
				//添加子表数据
				dbPstmt5 = conn.prepareStatement(this.sql_addDoubpointsDetail);
				
				List<AccidentDoubpointsDetail> detailList = adm.getDetailList();

				for (int j = 0; j<detailList.size(); j++){
					AccidentDoubpointsDetail detail = detailList.get(j);
					
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
				dbPstmt6 = conn.prepareStatement(this.sql_addDoubpointsMain);
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
				
				if (adm.getLon()>0){
					dbPstmt6.setLong(12, adm.getLon());
				}else{
					dbPstmt6.setNull(12, Types.INTEGER);
				}
				if (adm.getLat()>0){
					dbPstmt6.setLong(13, adm.getLat());
				}else{
					dbPstmt6.setNull(13, Types.INTEGER);
				}
				if (adm.getMapLon()>0){
					dbPstmt6.setLong(14, adm.getMapLon());
				}else{
					dbPstmt6.setNull(14, Types.INTEGER);
				}
				if (adm.getMapLat()>0){
					dbPstmt6.setLong(15, adm.getMapLat());
				}else{
					dbPstmt6.setNull(15, Types.INTEGER);
				}
				if (adm.getElevation()>0){
					dbPstmt6.setLong(16, adm.getElevation());
				}else{
					dbPstmt6.setNull(16, Types.INTEGER);
				}
				
				if (adm.getLicenseNo()!=null&&!"".equals(adm.getLicenseNo())){
					dbPstmt6.setString(17, adm.getLicenseNo());
				}else{
					dbPstmt6.setNull(17, Types.VARCHAR);
				}
				
				dbPstmt6.executeUpdate();
			}
			}
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
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("连接放回连接池出错.",e);
			}
		}
		
	}
	
	/**
	 * 更新行驶记录提取历史记录状态
	 * @param warn
	 * TODO
	 */ 
	public void updateTravellingRecorderLog(String seq,String status) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			if (conn!=null){
				ps = conn.prepareStatement(this.sql_updateTravellingRecorderLog);
				ps.setString(1, status);
				ps.setString(2, seq);
				ps.executeUpdate();
			}
		} catch (SQLException e) {
			logger.error("CO_SEQ:"+seq+" 更新行驶记录提取历史记录状态异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新行驶记录提取历史记录状态:关闭连接失败！", e);
			}
		}
	}
	
	/*****************************************
	 * <li>描 述：存储终端特征系数修正结果</li><br>
	 * <li>时 间：2013-9-12 下午6:53:21</li><br>
	 * <li>参数： @param app</li><br>
	 * 
	 * INSERT INTO TS_VEHICLESPEED_CHECK
				(AUTO_ID,VID,VIN_CODE,TID,COMMADDR,GPS_SPEED,
				VSS_SPEED,PFEATURES_RATIO,REPORT_TIME,CHECK_BEGIN_TIME,CHECK_END_TIME)
				 VALUES (?,?,?,?,?,?,?,?,?,?)
	 *****************************************/
	public void saveVehicleSpeedCheck(Map<String, String> app) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			if (conn!=null){
				ps = conn.prepareStatement(this.sql_addVehicleSpeedCheck);
				ps.setString(1, UUID.randomUUID().toString().replace("-", ""));
				ps.setString(2, app.get(Constant.VID));
				ps.setString(3, app.get(Constant.VIN_CODE));
				ps.setString(4, app.get(Constant.TID));
				ps.setString(5, app.get(Constant.COMMDR));
				if (app.get("602")!=null&&!"".equals(app.get("602"))){
					ps.setLong(6, Long.parseLong(app.get("602")));
				}else{
					ps.setNull(6, Types.INTEGER);
				}
				if (app.get("603")!=null&&!"".equals(app.get("603"))){
					ps.setLong(7, Long.parseLong(app.get("603")));
				}else{
					ps.setNull(7, Types.INTEGER);
				}
				
				if (app.get("604")!=null&&!"".equals(app.get("604"))){
					ps.setLong(8, Long.parseLong(app.get("604")));
				}else{
					ps.setNull(8, Types.INTEGER);
				}
				ps.setLong(9, (new Date()).getTime());
				
				if (app.get("600")!=null&&!"".equals(app.get("600"))){
					ps.setLong(10, DateTools.changeDateFormat("yyMMddhhmmss",app.get("600")).getTime());
				}else{
					ps.setNull(10, Types.INTEGER);
				}
				
				if (app.get("601")!=null&&!"".equals(app.get("601"))){
					ps.setLong(11, DateTools.changeDateFormat("yyMMddhhmmss",app.get("601")).getTime());
				}else{
					ps.setNull(11, Types.INTEGER);
				}
				
				ps.executeUpdate();
			}
			
		} catch (SQLException e) {
			logger.error("--saveRecorder--ERROR--存储特征系数修正SQL异常:" + e.getMessage(), e);
		}catch(Exception ex){
			logger.error("--saveRecorder--ERROR--存储特征系数修正数据异常:" + ex.getMessage(), ex);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("--saveRecorder--ERROR-closed!-存储特征系数修正数据异常:关闭连接失败！", e);
			}
		}
	}
	/**
	 * 存储驾驶员历史记录列表
	 * @param list
	 */
	public void saveDriverHistoryList(List<Driver> list) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveDriverInfo);
			for(Driver driver : list){
				ps.setString(1, driver.getUuid());
				ps.setString(2, driver.getVid());
				ps.setString(3, driver.getStaffName());
				ps.setString(4, driver.getIdNumber());
				ps.setString(5, driver.getQualificationId());
				ps.setString(6, driver.getQualificationName());
				ps.setLong(7, System.currentTimeMillis());
				ps.setInt(8, 0);
				ps.addBatch(); 
			} 	
			ps.executeBatch();
		} catch (SQLException e) {
			logger.error("存储驾驶员历史记录列表异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("存储驾驶员历史记录列表-关闭连接失败！", e);
			}
		}
	}
	/**
	 * 更新驾驶员下线记录列表
	 * @param list
	 */
	public void updateDriverOfflineList(List<Driver> list) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_updateDriverWork);
			
			for(Driver driver : list){
				ps.setLong(1, driver.getMatingTime()); 	//下线时间
				ps.setString(2, driver.getUuid());		//记录编号
				ps.addBatch(); 
			} 	
			ps.executeBatch();
		} catch (SQLException e) {
			logger.error("更新驾驶员下线记录列表异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("更新驾驶员下线记录列表-关闭连接失败！", e);
			}
		}
	}
	/**
	 * 存储驾驶员上班信息列表
	 * @param list
	 */
	public void saveDriverOnlineList(List<Driver> list) {
		Connection conn = null;
		PreparedStatement ps = null;
		try {
			conn = OracleConnectionPool.getConnection();
			ps = conn.prepareStatement(sql_saveDriverWork);
			for(Driver driver : list){
				ps.setString(1, driver.getUuid());  				//记录编号
				ps.setString(2, driver.getVid());					//车辆编号
				ps.setString(3, driver.getPhoneNumber());			//手机号
				ps.setString(4, driver.getStaffName());				//驾驶员姓名
				ps.setString(5, driver.getIdNumber()); 				//身份证号 
				ps.setString(6, driver.getQualificationId()); 		//从业资格证号
				ps.setString(7, driver.getQualificationName()); 	//资格证发证机构名称 
				ps.setLong(8, driver.getMatingTime()); 				//上线时间
				ps.setInt(9,  driver.getReadStatus());				//IC卡读取状态
				ps.setInt(10, driver.getMatingStatus());			//上下班状态   (1:上班;2:下班)
				ps.setString(11, driver.getTeamId());				//所属车队编号
				ps.setLong(12, driver.getQualificationValid());		//从业资格证有效期
				ps.setLong(13, driver.getSysUtc());					//系统记录时间
				ps.setString(14, driver.getPlate());				//车牌号码
				ps.setString(15, driver.getPlateColor());			//车牌颜色
				ps.setString(16, driver.getEntName());				//组织名称
				ps.addBatch(); 
			} 	
			ps.executeBatch();
		} catch (SQLException e) {
			logger.error("存储驾驶员上班信息列表异常:" + e.getMessage(), e);
		} finally {
			try {
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (Exception e) {
				logger.error("存储驾驶员上班信息列表-关闭连接失败！", e);
			}
		}
	}

	
}
