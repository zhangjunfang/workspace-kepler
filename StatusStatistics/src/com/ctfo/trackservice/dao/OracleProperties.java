/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.util OracleProperties.java	</li><br>
 * <li>时        间：2013-10-22  下午4:24:32	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.dao;

/*****************************************
 * <li>描        述：oracle参数		
 * 
 *****************************************/
public class OracleProperties {
	/** 轨迹批量提交数 */
	private int trackSubmit;
	/** 非法轨迹提批量交数 */
	private int trackValidSubmit;
	/** 设备状态更新批量提交数 */
	private int equipmentSubmit;
	/** 根据车辆ID查询车辆状态编码SQL */
	private String sql_queryStatusCode;
	// 轨迹包更新最后位置到数据库
	private String sql_updateLastTrack;
	// 轨迹包更新最后位置到数据库
	private String sql_updateLastTrackA;
	// 轨迹包带总线数据更新最后位置到数据库
	private String sql_updateLastTrackLine;
	// 轨迹包带总线数据更新最后位置到数据库
	private String sql_updateLastTrackALine;
	// 更新车辆总线状态信息
	private String sql_updateVehicleLineStatus;
	// 更新轨迹在线状态
	private String sql_UpdateLastTrackISonLine;
	// 更新轨迹在线状态
	private String sql_cacheAllVehicleStatus;
	/**	初始化所有车辆信息SQL	*/
	private String sql_initAllVehilceCache;
	/**	更新3g手机号对应的车辆缓存信息SQL	*/
	private String sql_update3GPhotoVehicleInfo;
	/**	更新车辆缓存SQL	*/
	private String sql_updateVehicle;
	/**	更新3g车辆缓存SQL	*/
	private String sql_update3GVehicle;
	/** 查询车辆对应企业SQL */
	private String sql_queryVehicleOrgMap;
	/** 查询企业对应报警设置SQL */
	private String sql_queryOrgAlarmCodeMap;
	/** 更新最后位置表车辆上下线状态提交数 */
	private Integer updateOfflineStatusSubmit;
	/** 上线提交数 */
	private Integer onlineSubmit;
	/** 下线提交数 */
	private Integer offlineSubmit;
	/** 更新最后位置表车辆上下线状态SQL */
	private String sql_updateOnOfflineStatus;
	/** 存储上线SQL */
	private String sql_saveOnline;
	/** 存储下线SQL */
	private String sql_saveOffline;
	/** 同步所有上级企业编号SQL */
	private String sql_orgParentSync;
	
	public String getSql_orgParentSync() {
		return sql_orgParentSync;
	}
	public void setSql_orgParentSync(String sql_orgParentSync) {
		this.sql_orgParentSync = sql_orgParentSync;
	}
	public Integer getUpdateOfflineStatusSubmit() {
		return updateOfflineStatusSubmit;
	}
	public void setUpdateOfflineStatusSubmit(Integer updateOfflineStatusSubmit) {
		this.updateOfflineStatusSubmit = updateOfflineStatusSubmit;
	}
	public Integer getOnlineSubmit() {
		return onlineSubmit;
	}
	public void setOnlineSubmit(Integer onlineSubmit) {
		this.onlineSubmit = onlineSubmit;
	}
	public Integer getOfflineSubmit() {
		return offlineSubmit;
	}
	public void setOfflineSubmit(Integer offlineSubmit) {
		this.offlineSubmit = offlineSubmit;
	}
	public String getSql_updateOnOfflineStatus() {
		return sql_updateOnOfflineStatus;
	}
	public void setSql_updateOnOfflineStatus(String sql_updateOnOfflineStatus) {
		this.sql_updateOnOfflineStatus = sql_updateOnOfflineStatus;
	}
	public String getSql_saveOnline() {
		return sql_saveOnline;
	}
	public void setSql_saveOnline(String sql_saveOnline) {
		this.sql_saveOnline = sql_saveOnline;
	}
	public String getSql_saveOffline() {
		return sql_saveOffline;
	}
	public void setSql_saveOffline(String sql_saveOffline) {
		this.sql_saveOffline = sql_saveOffline;
	}
	public int getTrackSubmit() {
		return trackSubmit;
	}
	public void setTrackSubmit(int trackSubmit) {
		this.trackSubmit = trackSubmit;
	}
	public int getTrackValidSubmit() {
		return trackValidSubmit;
	}
	public void setTrackValidSubmit(int trackValidSubmit) {
		this.trackValidSubmit = trackValidSubmit;
	}
	public int getEquipmentSubmit() {
		return equipmentSubmit;
	}
	public void setEquipmentSubmit(int equipmentSubmit) {
		this.equipmentSubmit = equipmentSubmit;
	}
	public String getSql_queryStatusCode() {
		return sql_queryStatusCode;
	}
	public void setSql_queryStatusCode(String sql_queryStatusCode) {
		this.sql_queryStatusCode = sql_queryStatusCode;
	}
	public String getSql_updateLastTrack() {
		return sql_updateLastTrack;
	}
	public void setSql_updateLastTrack(String sql_updateLastTrack) {
		this.sql_updateLastTrack = sql_updateLastTrack;
	}
	public String getSql_updateLastTrackA() {
		return sql_updateLastTrackA;
	}
	public void setSql_updateLastTrackA(String sql_updateLastTrackA) {
		this.sql_updateLastTrackA = sql_updateLastTrackA;
	}
	public String getSql_updateLastTrackLine() {
		return sql_updateLastTrackLine;
	}
	public void setSql_updateLastTrackLine(String sql_updateLastTrackLine) {
		this.sql_updateLastTrackLine = sql_updateLastTrackLine;
	}
	public String getSql_updateLastTrackALine() {
		return sql_updateLastTrackALine;
	}
	public void setSql_updateLastTrackALine(String sql_updateLastTrackALine) {
		this.sql_updateLastTrackALine = sql_updateLastTrackALine;
	}
	public String getSql_updateVehicleLineStatus() {
		return sql_updateVehicleLineStatus;
	}
	public void setSql_updateVehicleLineStatus(String sql_updateVehicleLineStatus) {
		this.sql_updateVehicleLineStatus = sql_updateVehicleLineStatus;
	}
	public String getSql_UpdateLastTrackISonLine() {
		return sql_UpdateLastTrackISonLine;
	}
	public void setSql_UpdateLastTrackISonLine(String sql_UpdateLastTrackISonLine) {
		this.sql_UpdateLastTrackISonLine = sql_UpdateLastTrackISonLine;
	}
	public String getSql_cacheAllVehicleStatus() {
		return sql_cacheAllVehicleStatus;
	}
	public void setSql_cacheAllVehicleStatus(String sql_cacheAllVehicleStatus) {
		this.sql_cacheAllVehicleStatus = sql_cacheAllVehicleStatus;
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
	public String getSql_queryVehicleOrgMap() {
		return sql_queryVehicleOrgMap;
	}
	public void setSql_queryVehicleOrgMap(String sql_queryVehicleOrgMap) {
		this.sql_queryVehicleOrgMap = sql_queryVehicleOrgMap;
	}
	public String getSql_queryOrgAlarmCodeMap() {
		return sql_queryOrgAlarmCodeMap;
	}
	public void setSql_queryOrgAlarmCodeMap(String sql_queryOrgAlarmCodeMap) {
		this.sql_queryOrgAlarmCodeMap = sql_queryOrgAlarmCodeMap;
	}
}
