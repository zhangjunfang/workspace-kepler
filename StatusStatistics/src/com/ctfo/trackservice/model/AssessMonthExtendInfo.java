/**
 * Copyright (c) 2012, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.trackservice.model;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： StatisticAnalysisServer<br>
 * 功能： 考核月延展信息<br>
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
public class AssessMonthExtendInfo {
    private String baseId;//基础考核月的Id
    private String extendedId;//延展的考核月的ID
    
    public String getBaseId() {
        return baseId;
    }
    public void setBaseId(String baseId) {
        this.baseId = baseId;
    }
    public String getExtendedId() {
        return extendedId;
    }
    public void setExtendedId(String extendedId) {
        this.extendedId = extendedId;
    }
}
