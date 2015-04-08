/*******************************************************************************
 * @(#)SpringBootStrap.java 2008-7-11
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.demo.spring;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.FileSystemXmlApplicationContext;

/**
 * @author <a href="mailto:fang-lei@neusoft.com">Lei Fang</a>
 * @version $Revision 1.1 $ 2008-7-11 下午02:31:41
 */
public class SpringBootStrap {

    private String config;

    private ApplicationContext context;

    private static final SpringBootStrap INSTANCE = new SpringBootStrap();

    private volatile boolean isInit = false;

    private SpringBootStrap() {

    }

    public static SpringBootStrap getInstance() {
        return INSTANCE;
    }

    public void setConfig(String config) {
        this.config = config;
    }

    public synchronized void init() {
        if (isInit) {
            return;
        }
        if (null == config || config.length() <= 0) {
            throw new NullPointerException("Spring config");
        }
        context = new FileSystemXmlApplicationContext(config);
        isInit = true;
    }

    public Object getBean(String name) {
        return context.getBean(name);
    }

    @SuppressWarnings("unchecked")
    public Object getBean(String name, Class requiredType) {
        return context.getBean(name, requiredType);
    }

    public boolean isInit() {
        return isInit;
    }

	public void setInit(boolean isInit) {
		this.isInit = isInit;
	}

}
