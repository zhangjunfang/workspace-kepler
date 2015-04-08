package com.ctfo.commandservice.model;
/**
 *	油量标定信息
 * @author zjhl
 *
 */
public class OilCalibration extends OilBase {
	/** 车辆编号	*/
	private String vid;
	/** 油量容量（标定）	*/
	private int oilCalibration;
	/** 加油前后落差	*/
	private int gap;
	/** 加油阀值（门限）	*/
	private int refuelThreshold;
	/** 偷油阀值（门限）	*/
	private int stealThreshold;
	/** 序列号	*/
	private String seq;
	
	/**
	 * 获得消息类型的值
	 * @return the vid 消息类型  
	 */
	public String getVid() {
		return vid;
	}

	/**
	 * 设置消息类型的值
	 * @param vid 消息类型  
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}

	/**
	 * 获得油量容量（标定）的值
	 * @return the oilCalibration 油量容量（标定）  
	 */
	public int getOilCalibration() {
		return oilCalibration;
	}

	/**
	 * 设置油量容量（标定）的值
	 * @param oilCalibration 油量容量（标定）  
	 */
	public void setOilCalibration(int oilCalibration) {
		this.oilCalibration = oilCalibration;
	}

	/**
	 * 获得加油前后落差的值
	 * @return the gap 加油前后落差  
	 */
	public int getGap() {
		return gap;
	}

	/**
	 * 设置加油前后落差的值
	 * @param gap 加油前后落差  
	 */
	public void setGap(int gap) {
		this.gap = gap;
	}

	/**
	 * 获得加油阀值（门限）的值
	 * @return the refuelThreshold 加油阀值（门限）  
	 */
	public int getRefuelThreshold() {
		return refuelThreshold;
	}

	/**
	 * 设置加油阀值（门限）的值
	 * @param refuelThreshold 加油阀值（门限）  
	 */
	public void setRefuelThreshold(int refuelThreshold) {
		this.refuelThreshold = refuelThreshold;
	}

	/**
	 * 获得偷油阀值（门限）的值
	 * @return the stealThreshold 偷油阀值（门限）  
	 */
	public int getStealThreshold() {
		return stealThreshold;
	}

	/**
	 * 设置偷油阀值（门限）的值
	 * @param stealThreshold 偷油阀值（门限）  
	 */
	public void setStealThreshold(int stealThreshold) {
		this.stealThreshold = stealThreshold;
	}

	/**
	 * 获得序列号的值
	 * @return the seq 序列号  
	 */
	public String getSeq() {
		return seq;
	}

	/**
	 * 设置序列号的值
	 * @param seq 序列号  
	 */
	public void setSeq(String seq) {
		this.seq = seq;
	}

	public String toString() {
		StringBuffer sb = new StringBuffer();
//		sb.append("type:").append(type);
//		sb.append(",version:").append(version);
//		sb.append(",lat:").append(lat);
//		sb.append(",lon:").append(lon);
//		sb.append(",elevation:").append(elevation);
//		sb.append(",speed:").append(speed);
//		sb.append(",direction:").append(direction);
//		sb.append(",time:").append(time);
//		sb.append(",status:").append(status);
//		sb.append(",commandType:").append(commandType);
		sb.append("vid:").append(vid);
		sb.append(",seq:").append(seq);
		sb.append(",oilCalibration:").append(oilCalibration);
		sb.append(",gap:").append(gap);
		sb.append(",refuelThreshold:").append(refuelThreshold);
		sb.append(",stealThreshold").append(stealThreshold);
		return sb.toString();
	}
	
}
