/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.model LastTrack.java	</li><br>
 * <li>时        间：2013-10-19  下午7:12:50	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.model;

/*****************************************
 * <li>描        述：最后位置		
 * 
 *****************************************/
public class LastTrack {
	/**	经度	*/
	private Long lon;
	/**	纬度	*/
	private Long lat;
	/**	偏移经度	*/
	private Long lonMap;
	/**	偏移纬度	*/
	private Long latMap;
	/**	锁定状态	*/
	private String lockStatus;
	/**	报警编码	*/
	private String alarmCode;
	/**	报警时间	*/
	private Long alarmTime;
	
	/*****************************************
	 * <li>描        述：构造方法			</li><br>
	 * <li>参        数：@param b
	 *****************************************/
	public LastTrack(boolean b) {
		this.lon = 0l;
		this.lat = 0l;
		this.lonMap = 0l;
		this.latMap = 0l;
		this.lockStatus = "";
		this.alarmCode = "";
		this.alarmTime = 0l;
	}
	public LastTrack() {
	}
	public Long getLon() {
		return lon;
	}
	public void setLon(Long lon) {
		this.lon = lon;
	}
	public Long getLat() {
		return lat;
	}
	public void setLat(Long lat) {
		this.lat = lat;
	}
	public Long getLonMap() {
		return lonMap;
	}
	public void setLonMap(Long lonMap) {
		this.lonMap = lonMap;
	}
	public Long getLatMap() {
		return latMap;
	}
	public void setLatMap(Long latMap) {
		this.latMap = latMap;
	}
	public String getLockStatus() {
		return lockStatus;
	}
	public void setLockStatus(String lockStatus) {
		this.lockStatus = lockStatus;
	}
	public String getAlarmCode() {
		return alarmCode;
	}
	public void setAlarmCode(String alarmCode) {
		this.alarmCode = alarmCode;
	}
	public Long getAlarmTime() {
		return alarmTime;
	}
	public void setAlarmTime(Long alarmTime) {
		this.alarmTime = alarmTime;
	}
}
