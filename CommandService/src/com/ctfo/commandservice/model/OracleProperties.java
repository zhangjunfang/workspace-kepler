/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>文件名称：com.ctfo.commandservice.model OracleProperties.java	</li><br>
 * <li>时        间：2013-10-23  下午1:34:09	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.model;

/*****************************************
 * <li>描        述：Oracle配置		
 * 
 *****************************************/
public class OracleProperties {
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
	/** 终端最后上报版本信息**/
	private String sql_saveTerminalLastVersion;
	/** 查询终端标准版本信息**/
	private String sql_queryTerminalStandardVersion;
	/** 更新终端标准版本信息**/
	private String sql_updateTerminalStandardVersion;
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
	public void setSql_saveDriverInfo(String sql_saveDriverInfo) {
		this.sql_saveDriverInfo = sql_saveDriverInfo;
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
	public String getSql_saveSupervision() {
		return sql_saveSupervision;
	}
	public void setSql_saveSupervision(String sql_saveSupervision) {
		this.sql_saveSupervision = sql_saveSupervision;
	}
	public String getSql_saveWarning() {
		return sql_saveWarning;
	}
	public void setSql_saveWarning(String sql_saveWarning) {
		this.sql_saveWarning = sql_saveWarning;
	}
	public String getSql_savePlatformMessage() {
		return sql_savePlatformMessage;
	}
	public void setSql_savePlatformMessage(String sql_savePlatformMessage) {
		this.sql_savePlatformMessage = sql_savePlatformMessage;
	}
	public String getSql_savePlatformOnline() {
		return sql_savePlatformOnline;
	}
	public void setSql_savePlatformOnline(String sql_savePlatformOnline) {
		this.sql_savePlatformOnline = sql_savePlatformOnline;
	}
	public String getSql_savePlatformOffline() {
		return sql_savePlatformOffline;
	}
	public void setSql_savePlatformOffline(String sql_savePlatformOffline) {
		this.sql_savePlatformOffline = sql_savePlatformOffline;
	}
	public String getSql_saveQualityRecordCacheInfo() {
		return sql_saveQualityRecordCacheInfo;
	}
	public void setSql_saveQualityRecordCacheInfo(
			String sql_saveQualityRecordCacheInfo) {
		this.sql_saveQualityRecordCacheInfo = sql_saveQualityRecordCacheInfo;
	}
	public String getSql_saveTerminalLastVersion() {
		return sql_saveTerminalLastVersion;
	}
	public void setSql_saveTerminalLastVersion(String sql_saveTerminalLastVersion) {
		this.sql_saveTerminalLastVersion = sql_saveTerminalLastVersion;
	}
	public String getSql_updateLockCommandStatus() {
		return sql_updateLockCommandStatus;
	}
	public void setSql_updateLockCommandStatus(String sql_updateLockCommandStatus) {
		this.sql_updateLockCommandStatus = sql_updateLockCommandStatus;
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
	public String getSql_queryTerminalStandardVersion() {
		return sql_queryTerminalStandardVersion;
	}
	public void setSql_queryTerminalStandardVersion(
			String sql_queryTerminalStandardVersion) {
		this.sql_queryTerminalStandardVersion = sql_queryTerminalStandardVersion;
	}
	public String getSql_updateTerminalStandardVersion() {
		return sql_updateTerminalStandardVersion;
	}
	public void setSql_updateTerminalStandardVersion(
			String sql_updateTerminalStandardVersion) {
		this.sql_updateTerminalStandardVersion = sql_updateTerminalStandardVersion;
	}
	public String getSql_addDoubpointsMain() {
		return sql_addDoubpointsMain;
	}
	public void setSql_addDoubpointsMain(String sql_addDoubpointsMain) {
		this.sql_addDoubpointsMain = sql_addDoubpointsMain;
	}
	public String getSql_addDoubpointsDetail() {
		return sql_addDoubpointsDetail;
	}
	public void setSql_addDoubpointsDetail(String sql_addDoubpointsDetail) {
		this.sql_addDoubpointsDetail = sql_addDoubpointsDetail;
	}
	public String getSql_delDoubpointsMainAndDetail() {
		return sql_delDoubpointsMainAndDetail;
	}
	public void setSql_delDoubpointsMainAndDetail(
			String sql_delDoubpointsMainAndDetail) {
		this.sql_delDoubpointsMainAndDetail = sql_delDoubpointsMainAndDetail;
	}
	public String getSql_updateTravellingRecorderLog() {
		return sql_updateTravellingRecorderLog;
	}
	public void setSql_updateTravellingRecorderLog(
			String sql_updateTravellingRecorderLog) {
		this.sql_updateTravellingRecorderLog = sql_updateTravellingRecorderLog;
	}
	public String getSql_addVehicleSpeedCheck() {
		return sql_addVehicleSpeedCheck;
	}
	public void setSql_addVehicleSpeedCheck(String sql_addVehicleSpeedCheck) {
		this.sql_addVehicleSpeedCheck = sql_addVehicleSpeedCheck;
	}
	/**
	 * 获得油箱油量标定信息的值
	 * @return the sql_saveOilInfo 油箱油量标定信息  
	 */
	public String getSql_saveOilInfo() {
		return sql_saveOilInfo;
	}
	/**
	 * 设置油箱油量标定信息的值
	 * @param sql_saveOilInfo 油箱油量标定信息  
	 */
	public void setSql_saveOilInfo(String sql_saveOilInfo) {
		this.sql_saveOilInfo = sql_saveOilInfo;
	}
	/**
	 * 获得终端上报调度信息查询SQL的值
	 * @return the sql_queryDispatch 终端上报调度信息查询SQL  
	 */
	public String getSql_queryDispatch() {
		return sql_queryDispatch;
	}
	/**
	 * 设置终端上报调度信息查询SQL的值
	 * @param sql_queryDispatch 终端上报调度信息查询SQL  
	 */
	public void setSql_queryDispatch(String sql_queryDispatch) {
		this.sql_queryDispatch = sql_queryDispatch;
	}
	/**
	 * 获得终端上报调度信息存储SQL的值
	 * @return the sql_saveDispatch 终端上报调度信息存储SQL  
	 */
	public String getSql_saveDispatch() {
		return sql_saveDispatch;
	}
	/**
	 * 设置终端上报调度信息存储SQL的值
	 * @param sql_saveDispatch 终端上报调度信息存储SQL  
	 */
	public void setSql_saveDispatch(String sql_saveDispatch) {
		this.sql_saveDispatch = sql_saveDispatch;
	}

}
