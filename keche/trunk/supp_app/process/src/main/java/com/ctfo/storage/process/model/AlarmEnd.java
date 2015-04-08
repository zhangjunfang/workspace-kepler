/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： StatusService_batch		</li><br>
 * <li>文件名称：com.ctfo.statusservice.model AlarmEnd.java	</li><br>
 * <li>时        间：2013-12-4  下午6:59:28	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.storage.process.model;

/*****************************************
 * <li>描        述：报警结束		
 * 
 *****************************************/
public class AlarmEnd {
	/**	1. 报警结束时间	*/
	private long endUtc;
	/**	2. 经度	*/
	private long lon;
	/**	3. 纬度	*/
	private long lat;
	/**	4. 偏移经度	*/
	private long maplon;
	/**	5. 偏移纬度	*/
	private long maplat;
	/**	6.	海拔 	*/
	private long elevation;
	/**	7. 方向	*/
	private int direction;
	/**	8. gsp速度	*/
	private int gpsSpeed;
	/**	9.里程	*/
	private long mileage; 
	/**	10. 总油量	*/
	private long oilTotal;
	/**	11.报警附加信息	*/
	private String alarmAdded;
	/**	12.报警编号	*/
	private String alarmId;
	/**	13. redis报警键	*/
	private String alarmKey;
	/**	14. redis报警键	*/
	private long sysUtc;
	/**	15.最大转速	*/
	private String vid;
//	/**	15.最大转速	*/
//	private long maxRpm;
//	/**	16. 最大车速	*/
//	private int maxSpeed;
//	/**	17. 平均车速	*/
//	private int averageSpeed;
	
	public long getEndUtc() {
		return endUtc;
	}
	public void setEndUtc(long endUtc) {
		this.endUtc = endUtc;
	}
	public long getLon() {
		return lon;
	}
	public void setLon(long lon) {
		this.lon = lon;
	}
	public long getLat() {
		return lat;
	}
	public void setLat(long lat) {
		this.lat = lat;
	}
	public long getMaplon() {
		return maplon;
	}
	public void setMaplon(long maplon) {
		this.maplon = maplon;
	}
	public long getMaplat() {
		return maplat;
	}
	public void setMaplat(long maplat) {
		this.maplat = maplat;
	}
	public long getElevation() {
		return elevation;
	}
	public void setElevation(long elevation) {
		this.elevation = elevation;
	}
	public int getDirection() {
		return direction;
	}
	public void setDirection(int direction) {
		this.direction = direction;
	}
	public int getGpsSpeed() {
		return gpsSpeed;
	}
	public void setGpsSpeed(int gpsSpeed) {
		this.gpsSpeed = gpsSpeed;
	}
	public long getMileage() {
		return mileage;
	}
	public void setMileage(long mileage) {
		this.mileage = mileage;
	}
	public long getOilTotal() {
		return oilTotal;
	}
	public void setOilTotal(long oilTotal) {
		this.oilTotal = oilTotal;
	}
	public String getAlarmAdded() {
		return alarmAdded;
	}
	public void setAlarmAdded(String alarmAdded) {
		this.alarmAdded = alarmAdded;
	}
	public String getAlarmId() {
		return alarmId;
	}
	public void setAlarmId(String alarmId) {
		this.alarmId = alarmId;
	}
	public String getAlarmKey() {
		return alarmKey;
	}
	public void setAlarmKey(String alarmKey) {
		this.alarmKey = alarmKey;
	}
	public long getSysUtc() {
		return sysUtc;
	}
	public void setSysUtc(long sysUtc) {
		this.sysUtc = sysUtc;
	}
	/**
	 * @return 获取 15.最大转速
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置15.最大转速
	 * @param vid 15.最大转速 
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	
}
