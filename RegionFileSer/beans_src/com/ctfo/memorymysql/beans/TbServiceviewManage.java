package com.ctfo.memorymysql.beans;

import java.math.BigDecimal;

/**
 * 
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
 * <td>2011-10-20</td>
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
@SuppressWarnings("serial")
public class TbServiceviewManage  {

	/**
	 * 车辆ID
	 */
	private Integer vid;

	/**
	 * 企业id
	 */
	private Integer corpId;

	/**
	 * 企业名称
	 */
	private String corpName;

	/**
	 * 车队id
	 */
	private Integer teamId;

	/**
	 * 车队名称
	 */
	private String teamName;

	/**
	 * 终端型号
	 */
	private String oemcode;

	/**
	 * 终端名称
	 */
	private String oemname;

	/**
	 * SIM卡通讯地址
	 */
	private String commaddr;

	/**
	 * 终端id
	 */
	private Integer tid;

	/**
	 * 终端硬件标识
	 */
	private String tMac;

	/**
	 * 驾驶员id
	 */
	private Integer cid;

	/**
	 * 驾驶员姓名
	 */
	private String cname;

	/**
	 * 驾驶员身份证号
	 */
	private String ccardId;

	/**
	 * sim卡id
	 */
	private Integer sid;

	/**
	 * 车牌号
	 */
	private String vehicleno;

	/**
	 * 车牌颜色id
	 */
	private Integer plateColorId;

	/**
	 * 行业类别编码
	 */
	private String transTypeCode;

	/**
	 * 车辆类型编码
	 */
	private Integer vehicletypeId;

	/**
	 * 车辆类型名称
	 */
	private String typeName;

	/**
	 * 车牌颜色名称
	 */
	private String ptypeName;

	/**
	 * 纬度
	 */
	private Integer lat;

	/**
	 * 经度
	 */
	private Integer lon;

	/**
	 * 偏移后经度
	 */
	private BigDecimal maplon;

	/**
	 * 偏移后纬度
	 */
	private BigDecimal maplat;

	/**
	 * 方向
	 */
	private Integer head;

	/**
	 * 速度
	 */
	private Integer speed;

	/**
	 * TODO 未知
	 */
	private Long utc;

	/**
	 * 报警编码
	 */
	private String alarmcode;

	/**
	 * 最后一次报警时间
	 */
	private Long alarmutc;

	/**
	 * 报警描述
	 */
	private String alarmdesc;

	/**
	 * 是否状态
	 */
	private Integer isonline;

	/**
	 * 区域编码
	 */
	private Integer areaCode;

	/**
	 * 运营商id
	 */
	private Integer spId;

	/**
	 * 平台接入码
	 */
	private Integer accessCode;

	/**
	 * 用于区分数据是监管还是监控，取值为JG和JK
	 */
	private String status;

	/**
	 * 同步时间
	 */
	private Long synUtc;

	/**
	 * 终端识别ID号，用以识别终端的唯一标识，用于车机通讯唯一标识
	 */
	private String tIdentifyno;

	/**
	 * 0：监控，其他：监管
	 */
	private String ocode;

	/**
	 * 所属运营商id，多个以逗号分隔
	 */
	private String belongSpids;

	/**
	 * 所属企业id，多个以逗号分隔
	 */
	private String belongCorpids;

	/**
	 * 所属车队id，多个以逗号分隔
	 */
	private String belongTeamids;

	/**
	 * TODO 未知
	 */
	private Integer vehicleStatus;

	/**
	 * TODO 未知
	 */
	private Integer height;

	/**
	 * 当前编码
	 */
	private Integer curAreaCode;

	/**
	 * 判断更新时间
	 */
	private Long updatetime;

	/**
	 * 发动机转速
	 */
	private Integer engineRotateSpeed;

	/**
	 * 瞬时油耗（接收时单位：L/H）
	 */
	private Double oilInstant;

	public Integer getVid() {
		return vid;
	}

	public void setVid(Integer vid) {
		this.vid = vid;
	}

	public Integer getCorpId() {
		return corpId;
	}

	public void setCorpId(Integer corpId) {
		this.corpId = corpId;
	}

	public String getCorpName() {
		return corpName;
	}

	public void setCorpName(String corpName) {
		this.corpName = corpName;
	}

	public Integer getTeamId() {
		return teamId;
	}

	public void setTeamId(Integer teamId) {
		this.teamId = teamId;
	}

	public String getTeamName() {
		return teamName;
	}

	public void setTeamName(String teamName) {
		this.teamName = teamName;
	}

	public String getOemcode() {
		return oemcode;
	}

	public void setOemcode(String oemcode) {
		this.oemcode = oemcode;
	}

	public String getOemname() {
		return oemname;
	}

	public void setOemname(String oemname) {
		this.oemname = oemname;
	}

	public String getCommaddr() {
		return commaddr;
	}

	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

	public Integer getTid() {
		return tid;
	}

	public void setTid(Integer tid) {
		this.tid = tid;
	}

	public String gettMac() {
		return tMac;
	}

	public void settMac(String tMac) {
		this.tMac = tMac;
	}

	public Integer getCid() {
		return cid;
	}

	public void setCid(Integer cid) {
		this.cid = cid;
	}

	public String getCname() {
		return cname;
	}

	public void setCname(String cname) {
		this.cname = cname;
	}

	public String getCcardId() {
		return ccardId;
	}

	public void setCcardId(String ccardId) {
		this.ccardId = ccardId;
	}

	public Integer getSid() {
		return sid;
	}

	public void setSid(Integer sid) {
		this.sid = sid;
	}

	public String getVehicleno() {
		return vehicleno;
	}

	public void setVehicleno(String vehicleno) {
		this.vehicleno = vehicleno;
	}

	public Integer getPlateColorId() {
		return plateColorId;
	}

	public void setPlateColorId(Integer plateColorId) {
		this.plateColorId = plateColorId;
	}

	public String getTransTypeCode() {
		return transTypeCode;
	}

	public void setTransTypeCode(String transTypeCode) {
		this.transTypeCode = transTypeCode;
	}

	public Integer getVehicletypeId() {
		return vehicletypeId;
	}

	public void setVehicletypeId(Integer vehicletypeId) {
		this.vehicletypeId = vehicletypeId;
	}

	public String getTypeName() {
		return typeName;
	}

	public void setTypeName(String typeName) {
		this.typeName = typeName;
	}

	public String getPtypeName() {
		return ptypeName;
	}

	public void setPtypeName(String ptypeName) {
		this.ptypeName = ptypeName;
	}

	public Integer getLat() {
		return lat;
	}

	public void setLat(Integer lat) {
		this.lat = lat;
	}

	public Integer getLon() {
		return lon;
	}

	public void setLon(Integer lon) {
		this.lon = lon;
	}

	public BigDecimal getMaplon() {
		return maplon;
	}

	public void setMaplon(BigDecimal maplon) {
		this.maplon = maplon;
	}

	public BigDecimal getMaplat() {
		return maplat;
	}

	public void setMaplat(BigDecimal maplat) {
		this.maplat = maplat;
	}

	public Integer getHead() {
		return head;
	}

	public void setHead(Integer head) {
		this.head = head;
	}

	public Integer getSpeed() {
		return speed;
	}

	public void setSpeed(Integer speed) {
		this.speed = speed;
	}

	public Long getUtc() {
		return utc;
	}

	public void setUtc(Long utc) {
		this.utc = utc;
	}

	public String getAlarmcode() {
		return alarmcode;
	}

	public void setAlarmcode(String alarmcode) {
		this.alarmcode = alarmcode;
	}

	public Long getAlarmutc() {
		return alarmutc;
	}

	public void setAlarmutc(Long alarmutc) {
		this.alarmutc = alarmutc;
	}

	public String getAlarmdesc() {
		return alarmdesc;
	}

	public void setAlarmdesc(String alarmdesc) {
		this.alarmdesc = alarmdesc;
	}

	public Integer getIsonline() {
		return isonline;
	}

	public void setIsonline(Integer isonline) {
		this.isonline = isonline;
	}

	public Integer getAreaCode() {
		return areaCode;
	}

	public void setAreaCode(Integer areaCode) {
		this.areaCode = areaCode;
	}

	public Integer getSpId() {
		return spId;
	}

	public void setSpId(Integer spId) {
		this.spId = spId;
	}

	public Integer getAccessCode() {
		return accessCode;
	}

	public void setAccessCode(Integer accessCode) {
		this.accessCode = accessCode;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public Long getSynUtc() {
		return synUtc;
	}

	public void setSynUtc(Long synUtc) {
		this.synUtc = synUtc;
	}

	public String gettIdentifyno() {
		return tIdentifyno;
	}

	public void settIdentifyno(String tIdentifyno) {
		this.tIdentifyno = tIdentifyno;
	}

	public String getOcode() {
		return ocode;
	}

	public void setOcode(String ocode) {
		this.ocode = ocode;
	}

	public String getBelongSpids() {
		return belongSpids;
	}

	public void setBelongSpids(String belongSpids) {
		this.belongSpids = belongSpids;
	}

	public String getBelongCorpids() {
		return belongCorpids;
	}

	public void setBelongCorpids(String belongCorpids) {
		this.belongCorpids = belongCorpids;
	}

	public String getBelongTeamids() {
		return belongTeamids;
	}

	public void setBelongTeamids(String belongTeamids) {
		this.belongTeamids = belongTeamids;
	}

	public Integer getVehicleStatus() {
		return vehicleStatus;
	}

	public void setVehicleStatus(Integer vehicleStatus) {
		this.vehicleStatus = vehicleStatus;
	}

	public Integer getHeight() {
		return height;
	}

	public void setHeight(Integer height) {
		this.height = height;
	}

	public Integer getCurAreaCode() {
		return curAreaCode;
	}

	public void setCurAreaCode(Integer curAreaCode) {
		this.curAreaCode = curAreaCode;
	}

	public Long getUpdatetime() {
		return updatetime;
	}

	public void setUpdatetime(Long updatetime) {
		this.updatetime = updatetime;
	}

	public Integer getEngineRotateSpeed() {
		return engineRotateSpeed;
	}

	public void setEngineRotateSpeed(Integer engineRotateSpeed) {
		this.engineRotateSpeed = engineRotateSpeed;
	}

	public Double getOilInstant() {
		return oilInstant;
	}

	public void setOilInstant(Double oilInstant) {
		this.oilInstant = oilInstant;
	}
}
