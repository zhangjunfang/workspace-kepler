package com.ctfo.storage.process.model;

/**
 * ThEvent 驾驶行为事件文件记录
 * 
 * 
 * @author huangjincheng
 * 2014-5-21下午03:47:44
 * 
 */
public class ThEvent {
	/**	车辆编号	*/
	private String vid;
	/** 事件类型编号	*/
	private int eventType ;
	/** 系统时间	*/
	private long sysTime;
	/** 起始位置经度*/
	private long startLon ;
	
	/** 起始位置纬度*/
	private long startLat ;
	
	/** 起始位置高度*/
	private int startElevation ;
	
	/** 起始位置速度*/
	private int startSpeed ;
	
	/** 起始位置方向*/
	private int startDirection ;
	
	/** 起始位置时间,*/
	private long startTime ;
	
	/** 结束位置经度*/
	private long endLon ;
	
	/** 结束位置纬度*/
	private long endLat ;
	
	/** 结束位置高度*/
	private int endElevation ;
	
	/** 结束位置速度*/
	private int endSpeed ;
	
	/** 结束位置方向*/
	private int endDirection ;
	
	/** 结束位置时间,*/
	private long endTime ;
	/**
	 * @return 获取 车辆编号
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号
	 * @param vid 车辆编号 
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * 获取事件类型的值
	 * @return eventType  
	 */
	public int getEventType() {
		return eventType;
	}
	/**
	 * 设置事件类型的值
	 * @param eventType
	 */
	public void setEventType(int eventType) {
		this.eventType = eventType;
	}
	/**
	 * @return 获取 系统时间
	 */
	public long getSysTime() {
		return sysTime;
	}
	/**
	 * 设置系统时间
	 * @param sysTime 系统时间 
	 */
	public void setSysTime(long sysTime) {
		this.sysTime = sysTime;
	}
	/**
	 * 获取起始位置经度的值
	 * @return startLon  
	 */
	public long getStartLon() {
		return startLon;
	}

	/**
	 * 设置起始位置经度的值
	 * @param startLon
	 */
	public void setStartLon(long startLon) {
		this.startLon = startLon;
	}

	/**
	 * 获取起始位置纬度的值
	 * @return startLat  
	 */
	public long getStartLat() {
		return startLat;
	}

	/**
	 * 设置起始位置纬度的值
	 * @param startLat
	 */
	public void setStartLat(long startLat) {
		this.startLat = startLat;
	}

	/**
	 * 获取起始位置高度的值
	 * @return startElevation  
	 */
	public int getStartElevation() {
		return startElevation;
	}

	/**
	 * 设置起始位置高度的值
	 * @param startElevation
	 */
	public void setStartElevation(int startElevation) {
		this.startElevation = startElevation;
	}

	/**
	 * 获取起始位置速度的值
	 * @return startSpeed  
	 */
	public int getStartSpeed() {
		return startSpeed;
	}

	/**
	 * 设置起始位置速度的值
	 * @param startSpeed
	 */
	public void setStartSpeed(int startSpeed) {
		this.startSpeed = startSpeed;
	}

	/**
	 * 获取起始位置方向的值
	 * @return startDirection  
	 */
	public int getStartDirection() {
		return startDirection;
	}

	/**
	 * 设置起始位置方向的值
	 * @param startDirection
	 */
	public void setStartDirection(int startDirection) {
		this.startDirection = startDirection;
	}

	/**
	 * 获取起始位置时间的值
	 * @return startTime  
	 */
	public long getStartTime() {
		return startTime;
	}

	/**
	 * 设置起始位置时间的值
	 * @param startTime
	 */
	public void setStartTime(long startTime) {
		this.startTime = startTime;
	}

	/**
	 * 获取结束位置经度的值
	 * @return endLon  
	 */
	public long getEndLon() {
		return endLon;
	}

	/**
	 * 设置结束位置经度的值
	 * @param endLon
	 */
	public void setEndLon(long endLon) {
		this.endLon = endLon;
	}

	/**
	 * 获取结束位置纬度的值
	 * @return endLat  
	 */
	public long getEndLat() {
		return endLat;
	}

	/**
	 * 设置结束位置纬度的值
	 * @param endLat
	 */
	public void setEndLat(long endLat) {
		this.endLat = endLat;
	}

	/**
	 * 获取结束位置高度的值
	 * @return endElevation  
	 */
	public int getEndElevation() {
		return endElevation;
	}

	/**
	 * 设置结束位置高度的值
	 * @param endElevation
	 */
	public void setEndElevation(int endElevation) {
		this.endElevation = endElevation;
	}

	/**
	 * 获取结束位置速度的值
	 * @return endSpeed  
	 */
	public int getEndSpeed() {
		return endSpeed;
	}

	/**
	 * 设置结束位置速度的值
	 * @param endSpeed
	 */
	public void setEndSpeed(int endSpeed) {
		this.endSpeed = endSpeed;
	}

	/**
	 * 获取结束位置方向的值
	 * @return endDirection  
	 */
	public int getEndDirection() {
		return endDirection;
	}

	/**
	 * 设置结束位置方向的值
	 * @param endDirection
	 */
	public void setEndDirection(int endDirection) {
		this.endDirection = endDirection;
	}

	/**
	 * 获取结束位置时间的值
	 * @return endTime  
	 */
	public long getEndTime() {
		return endTime;
	}

	/**
	 * 设置结束位置时间的值
	 * @param endTime
	 */
	public void setEndTime(long endTime) {
		this.endTime = endTime;
	}
	
	
	
}
