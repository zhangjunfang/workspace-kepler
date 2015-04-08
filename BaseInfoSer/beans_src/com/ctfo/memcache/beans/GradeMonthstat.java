package com.ctfo.memcache.beans;

import com.ctfo.local.bean.ETB_Base;

/**
 * 排行榜
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
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
 * <td>2011-11-18</td>
 * <td>zhangming</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author zhangming
 * @since JDK1.6
 */
@SuppressWarnings({ "serial", "rawtypes" })
public class GradeMonthstat extends ETB_Base implements Comparable {

	/** 序号 */
	private Integer seqId;

	/** 车辆id */
	private String vid;

	/** 车牌号 */
	private String vehicleNo;

	/** 车队id */
	private String teamId;

	/** 车队名称 */
	private String teamName;

	/** 总数 */
	private Long allScoreSum;

	/** 上升:1 持平:0 下降:-1 */
	private int sign;

	/**
	 * compareTo
	 */
	@Override
	public int compareTo(Object object) {
		return this.seqId - ((GradeMonthstat) object).getSeqId();
	}

	public Integer getSeqId() {
		return seqId;
	}

	public void setSeqId(Integer seqId) {
		this.seqId = seqId;
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

	public String getTeamId() {
		return teamId;
	}

	public void setTeamId(String teamId) {
		this.teamId = teamId;
	}

	public String getTeamName() {
		return teamName;
	}

	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}

	public Long getAllScoreSum() {
		return allScoreSum;
	}

	public void setAllScoreSum(Long allScoreSum) {
		this.allScoreSum = allScoreSum;
	}

	public int getSign() {
		return sign;
	}

	public void setSign(int sign) {
		this.sign = sign;
	}
}
