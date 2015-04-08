/*******************************************************************************
 * @(#)BackQueueObject.java 2008-9-3
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.list;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-9-3 下午04:03:29
 */
public class ListObject {

    ListObject previous;

    ListObject next;

    private String key;

    public void setKey(String key) {
        if (key == null || key.equals("")) {
            throw new RuntimeException("the key is invalid : " + key);
        }
        this.key = key;
    }

    public String getKey() {
        return key;
    }

}
