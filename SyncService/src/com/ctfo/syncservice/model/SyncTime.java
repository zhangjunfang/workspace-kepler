/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： SyncServer		</li><br>
 * <li>文件名称：com.ctfo.syncserver.model SyncTime.java	</li><br>
 * <li>时        间：2013-10-17  上午2:44:30	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.model;

/*****************************************
 * <li>描        述：同步时间对象		
 * 
 *****************************************/
public class SyncTime {
	/**	管理端口	*/
	private Integer managementPort;
	/**	路况信息同步间隔时间	*/
	private Long roadCondition;
	/**	入网统计信息同步间隔时间	*/
	private Long netWorkStatistics;
	/**	 码表信息同步间隔时间	*/
	private Long generalCode;
	/**	车辆服务信息同步间隔时间	*/
	private Long vehicleService;
	/**	注册鉴权信息同步间隔时间	*/
	private Long authInterval;
	/**	 驾驶员鉴权信息同步间隔时间	*/
	private Long driverInterval;
	/**	车辆静态信息同步间隔时间	*/
	private Long carInterval;
	/**	组织信息同步间隔时间	*/
	private Long organization;
	/**	车辆、车队排行同步间隔时间	*/
	private Long vehicleAndTeamTop;
	/**	多组织信息同步间隔时间	*/
	private Long multipleOrganization;
	/**	电子运单同步间隔	*/
	private Long eticketInterval;
	/**	车牌号获取手机号同步间隔	*/
	private Long phoneInterval;
	
	

	public Integer getManagementPort() {
		return managementPort;
	}
	public void setManagementPort(Integer managementPort) {
		this.managementPort = managementPort;
	}
	public Long getRoadCondition() {
		return roadCondition;
	}
	public void setRoadCondition(Long roadCondition) {
		this.roadCondition = roadCondition;
	}
	public Long getNetWorkStatistics() {
		return netWorkStatistics;
	}
	public void setNetWorkStatistics(Long netWorkStatistics) {
		this.netWorkStatistics = netWorkStatistics;
	}
	public Long getGeneralCode() {
		return generalCode;
	}
	public void setGeneralCode(Long generalCode) {
		this.generalCode = generalCode;
	}
	public Long getVehicleService() {
		return vehicleService;
	}
	public void setVehicleService(Long vehicleService) {
		this.vehicleService = vehicleService;
	}
	public Long getAuthInterval() {
		return authInterval;
	}
	public void setAuthInterval(Long authInterval) {
		this.authInterval = authInterval;
	}
	public Long getDriverInterval() {
		return driverInterval;
	}
	public void setDriverInterval(Long driverInterval) {
		this.driverInterval = driverInterval;
	}
	public Long getCarInterval() {
		return carInterval;
	}
	public void setCarInterval(Long carInterval) {
		this.carInterval = carInterval;
	}
	public Long getOrganization() {
		return organization;
	}
	public void setOrganization(Long organization) {
		this.organization = organization;
	}
	public Long getVehicleAndTeamTop() {
		return vehicleAndTeamTop;
	}
	public void setVehicleAndTeamTop(Long vehicleAndTeamTop) {
		this.vehicleAndTeamTop = vehicleAndTeamTop;
	}
	public Long getMultipleOrganization() {
		return multipleOrganization;
	}
	public void setMultipleOrganization(Long multipleOrganization) {
		this.multipleOrganization = multipleOrganization;
	}
	public Long getEticketInterval() {
		return eticketInterval;
	}
	public void setEticketInterval(Long eticketInterval) {
		this.eticketInterval = eticketInterval;
	}
	public Long getPhoneInterval() {
		return phoneInterval;
	}
	public void setPhoneInterval(Long phoneInterval) {
		this.phoneInterval = phoneInterval;
	}
	
}
