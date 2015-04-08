/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： StatusService_batch		</li><br>
 * <li>文件名称：com.ctfo.statusservice.model Region.java	</li><br>
 * <li>时        间：2013-12-5  上午9:30:35	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.model;

/*****************************************
 * <li>描        述：跨域		
 * 
 *****************************************/
public class Region {
	/**	跨域编号	*/
	private String regionId;
	/**	属地代码	*/
	private String localCode;
	/**	当前属地代码	*/
	private String currentCode;
	/**	当前时间	*/
	private Long currentTime;
	/**	属地城市编号	*/
	private String localCityCode;
	/**	当前城市编号	*/
	private String currentCityCode;
	/**	属地省域编号	*/
	private String localProvinceCode;
	/**	当前省域编号	*/
	private String currentProvinceCode;
	
	
	public String getRegionId() {
		return regionId;
	}
	public void setRegionId(String regionId) {
		this.regionId = regionId;
	}
	public String getLocalCode() {
		return localCode;
	}
	public void setLocalCode(String localCode) {
		this.localCode = localCode;
	}
	public String getCurrentCode() {
		return currentCode;
	}
	public void setCurrentCode(String currentCode) {
		this.currentCode = currentCode;
	}
	public Long getCurrentTime() {
		return currentTime;
	}
	public void setCurrentTime(Long currentTime) {
		this.currentTime = currentTime;
	}
	public String getLocalCityCode() {
		return localCityCode;
	}
	public void setLocalCityCode(String localCityCode) {
		this.localCityCode = localCityCode;
	}
	public String getCurrentCityCode() {
		return currentCityCode;
	}
	public void setCurrentCityCode(String currentCityCode) {
		this.currentCityCode = currentCityCode;
	}
	public String getLocalProvinceCode() {
		return localProvinceCode;
	}
	public void setLocalProvinceCode(String localProvinceCode) {
		this.localProvinceCode = localProvinceCode;
	}
	public String getCurrentProvinceCode() {
		return currentProvinceCode;
	}
	public void setCurrentProvinceCode(String currentProvinceCode) {
		this.currentProvinceCode = currentProvinceCode;
	}
	
}
