/**
 * Storevm.org Inc.
 * Copyright (c) 2004-2010 All Rights Reserved.
 */
package org.storevm.toolkits.session.metadata;

import java.io.Serializable;

/**
 * SESSION的元数据定义
 * @author  ocean
 * @version $Id: SessionMetaData.java, v 0.1 2010-12-30 下午08:33:47  ocean Exp $
 */
public class SessionMetaData implements Serializable {
    private static final long serialVersionUID = -6446174402446690125L;
    private String            id;
    /**session的创建时间*/
    private Long              createTm;
    /**session的最大空闲时间*/
    private Long              maxIdle;
    /**session的最后一次访问时间*/
    private Long              lastAccessTm;
    /**是否可用*/
    private Boolean           validate         = false;
    /**当前版本*/
    private int               version          = 0;

    /**
     * 构造方法
     */
    public SessionMetaData() {
        this.createTm = System.currentTimeMillis();
        this.lastAccessTm = this.createTm;
        this.validate = true;
    }

    /**
     * @return Returns the createTm.
     */
    public Long getCreateTm() {
        return createTm;
    }

    /**
     * @param createTm The createTm to set.
     */
    public void setCreateTm(Long createTm) {
        this.createTm = createTm;
    }

    /**
     * @return Returns the maxIdle.
     */
    public Long getMaxIdle() {
        return maxIdle;
    }

    /**
     * @param maxIdle The maxIdle to set.
     */
    public void setMaxIdle(Long maxIdle) {
        this.maxIdle = maxIdle;
    }

    /**
     * @return Returns the lastAccessTm.
     */
    public Long getLastAccessTm() {
        return lastAccessTm;
    }

    /**
     * @param lastAccessTm The lastAccessTm to set.
     */
    public void setLastAccessTm(Long lastAccessTm) {
        this.lastAccessTm = lastAccessTm;
    }

    /**
     * @return Returns the validate.
     */
    public Boolean getValidate() {
        return validate;
    }

    /**
     * @param validate The validate to set.
     */
    public void setValidate(Boolean validate) {
        this.validate = validate;
    }

    /**
     * @return Returns the id.
     */
    public String getId() {
        return id;
    }

    /**
     * @param id The id to set.
     */
    public void setId(String id) {
        this.id = id;
    }

    /**
     * Getter method for property <tt>version</tt>.
     * 
     * @return property value of version
     */
    public int getVersion() {
        return version;
    }

    /**
     * Setter method for property <tt>version</tt>.
     * 
     * @param version value to be assigned to property version
     */
    public void setVersion(int version) {
        this.version = version;
    }

}
