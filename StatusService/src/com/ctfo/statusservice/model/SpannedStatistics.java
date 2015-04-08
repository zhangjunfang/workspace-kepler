/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： StatusService		</li><br>
 * <li>文件名称：com.ctfo.statusservice.model SpannedStatistics.java	</li><br>
 * <li>时        间：2013-10-16  上午10:28:38	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.model;

/*****************************************
 * <li>描        述：跨域对象		
 * 
 *****************************************/
public class SpannedStatistics {
	/**	主键	*/
	private String primaryKey;
	/**	属地编码	*/
	private String localCode;
	/**	当前属地编码	*/
	private String currentCode;
	/**	当前时间	*/
	private Long currentTime;
	/**	属地城市编码	*/
	private String localCityCode;
	/**	当前城市编码	*/
	private String currentCityCode;
	/**	属地省域编码	*/
	private String localAreaCode;
	/**	当前省域编码	*/
	private String currentAreaCode;
	
	public String getPrimaryKey() {
		return primaryKey;
	}
	public void setPrimaryKey(String primaryKey) {
		this.primaryKey = primaryKey;
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
	public String getLocalAreaCode() {
		return localAreaCode;
	}
	public void setLocalAreaCode(String localAreaCode) {
		this.localAreaCode = localAreaCode;
	}
	public String getCurrentAreaCode() {
		return currentAreaCode;
	}
	public void setCurrentAreaCode(String currentAreaCode) {
		this.currentAreaCode = currentAreaCode;
	}
}
