package com.ctfo.commandservice.model;

import java.util.ArrayList;
import java.util.List;

/**
 * 事故疑点数据主表
 * @author yujch
 *
 */
public class AccidentDoubpointsMain {
	
	private String pointId;
	private long gatherTime;
	private String vid;
	private String vehicleNo;
	private String vinCode;
	private String vehicleType;
	private String driverName;
	private String driverNumber;
	private long stopTime;
	private long startSpeed;
	private float brakingTime;
	
	private String licenseNo;
	
	private long lon=-1L;
	
	private long lat=-1L;
	
	private long mapLon=-1L;
	
	private long mapLat=-1L;
	
	private long elevation=-1L;
	
	List<AccidentDoubpointsDetail> detailList = new ArrayList<AccidentDoubpointsDetail>();
	
	public String getPointId() {
		return pointId;
	}
	public void setPointId(String pointId) {
		this.pointId = pointId;
	}
	public long getGatherTime() {
		return gatherTime;
	}
	public void setGatherTime(long gatherTime) {
		this.gatherTime = gatherTime;
	}
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public String getVehicleNo() {
		return vehicleNo;
	}
	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}
	public String getVinCode() {
		return vinCode;
	}
	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}
	public String getVehicleType() {
		return vehicleType;
	}
	public void setVehicleType(String vehicleType) {
		this.vehicleType = vehicleType;
	}
	public String getDriverName() {
		return driverName;
	}
	public void setDriverName(String driverName) {
		this.driverName = driverName;
	}
	public String getDriverNumber() {
		return driverNumber;
	}
	public void setDriverNumber(String driverNumber) {
		this.driverNumber = driverNumber;
	}
	public long getStopTime() {
		return stopTime;
	}
	public void setStopTime(long stopTime) {
		this.stopTime = stopTime;
	}
	public long getStartSpeed() {
		return startSpeed;
	}
	public void setStartSpeed(long startSpeed) {
		this.startSpeed = startSpeed;
	}
	public float getBrakingTime() {
		return brakingTime;
	}
	public void setBrakingTime(float brakingTime) {
		this.brakingTime = brakingTime;
	}
	public List<AccidentDoubpointsDetail> getDetailList() {
		return detailList;
	}
	public void setDetailList(List<AccidentDoubpointsDetail> detailList) {
		this.detailList = detailList;
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
	public long getMapLon() {
		return mapLon;
	}
	public void setMapLon(long mapLon) {
		this.mapLon = mapLon;
	}
	public long getMapLat() {
		return mapLat;
	}
	public void setMapLat(long mapLat) {
		this.mapLat = mapLat;
	}
	public long getElevation() {
		return elevation;
	}
	public void setElevation(long elevation) {
		this.elevation = elevation;
	}
	public String getLicenseNo() {
		return licenseNo;
	}
	public void setLicenseNo(String licenseNo) {
		this.licenseNo = licenseNo;
	}
	
}
