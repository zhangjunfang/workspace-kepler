/**
 * Copyright (c) 2012, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.trackservice.model;

import java.io.Serializable;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： StatisticAnalysisServer<br>
 * 功能： 考核月设置表tb_checkmonth_set对应的javabean<br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史<br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2012-5-18</td>
 * <td>刘小磊</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font><br>
 * 
 * @version 1.0
 * 
 * @author 刘小磊
 * @since JDK1.6
 */
@SuppressWarnings("serial")
public class TbCheckmonthSetBean implements Serializable{
    private String checkTimeId;
    private String entId;
    private String checkTimeCode;
    private String checkTimeDesc;
    private Long startTime;
    private Long endTime;
    private String createBy;
    private Long createTime;
    private String modifyBy;
    private Long modifyTime;
    private String enableFlag;
    
    public String getCheckTimeId() {
        return checkTimeId;
    }
    public void setCheckTimeId(String checkTimeId) {
        this.checkTimeId = checkTimeId;
    }
    public String getEntId() {
        return entId;
    }
    public void setEntId(String entId) {
        this.entId = entId;
    }
    public String getCheckTimeCode() {
        return checkTimeCode;
    }
    public void setCheckTimeCode(String checkTimeCode) {
        this.checkTimeCode = checkTimeCode;
    }
    public String getCheckTimeDesc() {
        return checkTimeDesc;
    }
    public void setCheckTimeDesc(String checkTimeDesc) {
        this.checkTimeDesc = checkTimeDesc;
    }
    public Long getStartTime() {
        return startTime;
    }
    public void setStartTime(Long startTime) {
        this.startTime = startTime;
    }
    public Long getEndTime() {
        return endTime;
    }
    public void setEndTime(Long endTime) {
        this.endTime = endTime;
    }
    public String getCreateBy() {
        return createBy;
    }
    public void setCreateBy(String createBy) {
        this.createBy = createBy;
    }
    public Long getCreateTime() {
        return createTime;
    }
    public void setCreateTime(Long createTime) {
        this.createTime = createTime;
    }
    public String getModifyBy() {
        return modifyBy;
    }
    public void setModifyBy(String modifyBy) {
        this.modifyBy = modifyBy;
    }
    public Long getModifyTime() {
        return modifyTime;
    }
    public void setModifyTime(Long modifyTime) {
        this.modifyTime = modifyTime;
    }
    public String getEnableFlag() {
        return enableFlag;
    }
    public void setEnableFlag(String enableFlag) {
        this.enableFlag = enableFlag;
    }
}