package com.ctfo.basic.beans;

import java.io.Serializable;

public class TbBranchCenter implements Serializable  {

    private static final long serialVersionUID = 7563191591731580876L;

    /** */
    private String id;

    /** 中心编码*/
    private String centerCode;

    /** 中心名称*/
    private String centerName;

    /** 有效标记 1:有效 0:无效  默认为1*/
    private String enableFlag;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getCenterCode() {
        return centerCode;
    }

    public void setCenterCode(String centerCode) {
        this.centerCode = centerCode;
    }

    public String getCenterName() {
        return centerName;
    }

    public void setCenterName(String centerName) {
        this.centerName = centerName;
    }

    public String getEnableFlag() {
        return enableFlag;
    }

    public void setEnableFlag(String enableFlag) {
        this.enableFlag = enableFlag;
    }



}

