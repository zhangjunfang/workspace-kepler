/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.util OracleProperties.java	</li><br>
 * <li>时        间：2013-10-22  下午4:24:32	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.model;

/*****************************************
 * <li>描        述：oracle参数		
 * 
 *****************************************/
public class OracleProperties {
	/** 初始化所有车辆信息缓存SQL */
	private String sql_initAllVehilceCache;
	/** 初始化所有3g视频车辆缓存SQL */
	private String sql_update3GPhotoVehicleInfo;
	/** 更新增量车辆缓存SQL */
	private String sql_updateVehicle;
	/** 更新增量3g视频车辆缓存SQL */
	private String sql_update3GVehicle;
	/** 查询车辆对应企业SQL */
	private String sql_queryVehicleOrgMap;
	/** 查询企业对应报警设置SQL */
	private String sql_queryOrgAlarmCodeMap;
	/** 跨域统计SQL */
	private String sql_insertSpannedStatistics;
	/** 存储报警开始SQL */
	private String sql_saveAlarmStart;
	/** 存储报警结束SQL */
	private String sql_saveAlarmEnd;
	/** 车队父级组织查询SQL */
	private String sql_orgParentSync;
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
	public String getSql_insertSpannedStatistics() {
		return sql_insertSpannedStatistics;
	}
	public void setSql_insertSpannedStatistics(String sql_insertSpannedStatistics) {
		this.sql_insertSpannedStatistics = sql_insertSpannedStatistics;
	}
	public String getSql_saveAlarmStart() {
		return sql_saveAlarmStart;
	}
	public void setSql_saveAlarmStart(String sql_saveAlarmStart) {
		this.sql_saveAlarmStart = sql_saveAlarmStart;
	}
	public String getSql_saveAlarmEnd() {
		return sql_saveAlarmEnd;
	}
	public void setSql_saveAlarmEnd(String sql_saveAlarmEnd) {
		this.sql_saveAlarmEnd = sql_saveAlarmEnd;
	}
	public String getSql_orgParentSync() {
		return sql_orgParentSync;
	}
	public void setSql_orgParentSync(String sql_orgParentSync) {
		this.sql_orgParentSync = sql_orgParentSync;
	}
	
}
