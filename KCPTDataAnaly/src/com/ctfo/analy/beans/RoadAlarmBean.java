package com.ctfo.analy.beans;


/**
 * 道路等级报警实体对象
 * 
 * @author LiangJian
 * 2012年12月7日10:57:21
 */
public class RoadAlarmBean {

	/**
	 * 
create table TB_SECTION_SPEEDLIMIT
(
  config_id         NUMBER(15),
  config_name       VARCHAR2(100),
  ent_id            NUMBER(15),
  create_by         NUMBER(15),
  create_time       NUMBER(15),
  update_by         NUMBER(15),
  update_time       NUMBER(15),
  enable_flag       VARCHAR2(2),
  is_default        VARCHAR2(2),
  ew_speed_limit    NUMBER(10),
  ew_continue_limit NUMBER(10),
  nr_speed_limit    NUMBER(10),
  nr_continue_limit NUMBER(10),
  pr_speed_limit    NUMBER(10),
  pr_continue_limit NUMBER(10),
  cr_speed_limit    NUMBER(10),
  cr_continue_limit NUMBER(10),
  or_speed_limit    NUMBER(10),
  or_continue_limit NUMBER(10)
)
*/
	
	private String vid;//vid
	private String config_id;//config_id
	private String config_name;//名称
	private String ent_id;//所属企业
	private String create_by;//创建人
	private Long create_time;//创建时间
	private String update_by;//修改人
	private Long update_time;//修改时间
	private String enable_flag;//有效标记（1、有效 0、无效）
	private String is_default;//是否默认配置（1、是）
	private Long ew_speed_limit;//高速速度限制(Km/h)
	private Long ew_continue_limit;//高速超速持续时间(s)
	private Long nr_speed_limit;//国道速度限制(Km/h)
	private Long nr_continue_limit;//国道超速持续时间(s)
	private Long pr_speed_limit;//省道速度限制(Km/h)
	private Long pr_continue_limit;//省道超速持续时间(s)
	private Long cr_speed_limit;//城区速度限制(Km/h)
	private Long cr_continue_limit;//城区超速持续时间(s)
	private Long or_speed_limit;//其他道路速度限制(Km/h)
	private Long or_continue_limit;//其他道路超速持续时间(s)
	
	public String getVid() {
		return vid;
	}
	public void setVid(String vid) {
		this.vid = vid;
	}
	public String getConfig_id() {
		return config_id;
	}
	public void setConfig_id(String configId) {
		config_id = configId;
	}
	public String getConfig_name() {
		return config_name;
	}
	public void setConfig_name(String configName) {
		config_name = configName;
	}
	public String getEnt_id() {
		return ent_id;
	}
	public void setEnt_id(String entId) {
		ent_id = entId;
	}
	public String getCreate_by() {
		return create_by;
	}
	public void setCreate_by(String createBy) {
		create_by = createBy;
	}
	public Long getCreate_time() {
		return create_time;
	}
	public void setCreate_time(Long createTime) {
		create_time = createTime;
	}
	public String getUpdate_by() {
		return update_by;
	}
	public void setUpdate_by(String updateBy) {
		update_by = updateBy;
	}
	public Long getUpdate_time() {
		return update_time;
	}
	public void setUpdate_time(Long updateTime) {
		update_time = updateTime;
	}
	public String getEnable_flag() {
		return enable_flag;
	}
	public void setEnable_flag(String enableFlag) {
		enable_flag = enableFlag;
	}
	public String getIs_default() {
		return is_default;
	}
	public void setIs_default(String isDefault) {
		is_default = isDefault;
	}
	public Long getEw_speed_limit() {
		return ew_speed_limit;
	}
	public void setEw_speed_limit(Long ewSpeedLimit) {
		ew_speed_limit = ewSpeedLimit;
	}
	public Long getEw_continue_limit() {
		return ew_continue_limit;
	}
	public void setEw_continue_limit(Long ewContinueLimit) {
		ew_continue_limit = ewContinueLimit;
	}
	public Long getNr_speed_limit() {
		return nr_speed_limit;
	}
	public void setNr_speed_limit(Long nrSpeedLimit) {
		nr_speed_limit = nrSpeedLimit;
	}
	public Long getNr_continue_limit() {
		return nr_continue_limit;
	}
	public void setNr_continue_limit(Long nrContinueLimit) {
		nr_continue_limit = nrContinueLimit;
	}
	public Long getPr_speed_limit() {
		return pr_speed_limit;
	}
	public void setPr_speed_limit(Long prSpeedLimit) {
		pr_speed_limit = prSpeedLimit;
	}
	public Long getPr_continue_limit() {
		return pr_continue_limit;
	}
	public void setPr_continue_limit(Long prContinueLimit) {
		pr_continue_limit = prContinueLimit;
	}
	public Long getCr_speed_limit() {
		return cr_speed_limit;
	}
	public void setCr_speed_limit(Long crSpeedLimit) {
		cr_speed_limit = crSpeedLimit;
	}
	public Long getCr_continue_limit() {
		return cr_continue_limit;
	}
	public void setCr_continue_limit(Long crContinueLimit) {
		cr_continue_limit = crContinueLimit;
	}
	public Long getOr_speed_limit() {
		return or_speed_limit;
	}
	public void setOr_speed_limit(Long orSpeedLimit) {
		or_speed_limit = orSpeedLimit;
	}
	public Long getOr_continue_limit() {
		return or_continue_limit;
	}
	public void setOr_continue_limit(Long orContinueLimit) {
		or_continue_limit = orContinueLimit;
	}
}
