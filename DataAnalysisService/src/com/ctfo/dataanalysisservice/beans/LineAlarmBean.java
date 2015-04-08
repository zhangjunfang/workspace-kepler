package com.ctfo.dataanalysisservice.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： DataAnalysisService <br>
 * 功能：偏移报警在缓存中的对象 <br>
 * 描述：偏移报警在缓存中的对象 <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>Feb 18, 2012</td>
 * <td>张高</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author 张高
 * @since JDK1.6
 */
public class LineAlarmBean implements Serializable {
	private static final long serialVersionUID = 1L;
	private String sectionsId;// 线段ID
	private String vid_lineId;// 车辆ID和线路ID
	private boolean isShifted;// 是否偏移【true:偏移 false:不偏移】
	private String shiftStartTime;// 偏移开始时间
	private String shiftUpdateTime;// 偏移更新时间
	private String shiftEndTime;// 偏移结束时间

	private boolean isOverSpeed;// 是否超速【true:超速 false:不超速】
	private String overSpeedStartTime;// 超速开始时间
	private String overSpeedUpdateTime;// 偏移更新时间
	private String overSpeedEndTime;// 超速更新时间
	private boolean isToAlarmed;// 是否已经到达过阀值，产生过报警【true:到达过 false:没到达】

	public String getSectionsId() {
		return sectionsId;
	}

	public void setSectionsId(String sectionsId) {
		this.sectionsId = sectionsId;
	}

	public String getShiftUpdateTime() {
		return shiftUpdateTime;
	}

	public void setShiftUpdateTime(String shiftUpdateTime) {
		this.shiftUpdateTime = shiftUpdateTime;
	}

	public String getOverSpeedEndTime() {
		return overSpeedEndTime;
	}

	public void setOverSpeedEndTime(String overSpeedEndTime) {
		this.overSpeedEndTime = overSpeedEndTime;
	}

	public String getVid_lineId() {
		return vid_lineId;
	}

	public void setVid_lineId(String vid_lineId) {
		this.vid_lineId = vid_lineId;
	}

	public boolean isShifted() {
		return isShifted;
	}

	public void setShifted(boolean isShifted) {
		this.isShifted = isShifted;
	}

	public String getShiftStartTime() {
		return shiftStartTime;
	}

	public void setShiftStartTime(String shiftStartTime) {
		this.shiftStartTime = shiftStartTime;
	}

	public String getShiftEndTime() {
		return shiftEndTime;
	}

	public void setShiftEndTime(String shiftEndTime) {
		this.shiftEndTime = shiftEndTime;
	}

	public boolean isOverSpeed() {
		return isOverSpeed;
	}

	public void setOverSpeed(boolean isOverSpeed) {
		this.isOverSpeed = isOverSpeed;
	}

	public String getOverSpeedStartTime() {
		return overSpeedStartTime;
	}

	public void setOverSpeedStartTime(String overSpeedStartTime) {
		this.overSpeedStartTime = overSpeedStartTime;
	}

	public String getOverSpeedUpdateTime() {
		return overSpeedUpdateTime;
	}

	public void setOverSpeedUpdateTime(String overSpeedUpdateTime) {
		this.overSpeedUpdateTime = overSpeedUpdateTime;
	}

	public boolean isToAlarmed() {
		return isToAlarmed;
	}

	public void setToAlarmed(boolean isToAlarmed) {
		this.isToAlarmed = isToAlarmed;
	}

}
