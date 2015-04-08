package com.ctfo.dataanalysisservice.beans;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： DataAnalysisService <br>
 * 功能：时间关键点车辆Bean <br>
 * 描述：时间关键点车辆Bean <br>
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
 * <td>Feb 17, 2012</td>
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
public class AlarmStationBean implements Serializable {
	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 1L;
	private String vid;// 车辆Id
	private String toUtc;// 到达时间
	private String leaveUtc;// 离开时间

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	/**
	 * @return the toUtc
	 */
	public String getToUtc() {
		return toUtc;
	}

	/**
	 * @return the leaveUtc
	 */
	public String getLeaveUtc() {
		return leaveUtc;
	}

	/**
	 * @param leaveUtc
	 *            the leaveUtc to set
	 */
	public void setLeaveUtc(String leaveUtc) {
		this.leaveUtc = leaveUtc;
	}

	/**
	 * @param toUtc
	 *            the toUtc to set
	 */
	public void setToUtc(String toUtc) {
		this.toUtc = toUtc;
	}

}
