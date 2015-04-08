/*******************************************************************************
 * @(#)BackMap.java 2009-3-31
 *
 * Copyright 2009 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.back;

import java.util.LinkedList;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2009-3-31 下午08:46:29
 */
public class SupBackMap extends ConcurrentHashMap<String, SupBack> {

    private static final long serialVersionUID = 5636658645097193353L;

    private static final Logger log = LoggerFactory.getLogger(SupBackMap.class);

    private static final SupBackMap supbackmap = new SupBackMap();
    
    private LinkedList<SupBack> backlist = new LinkedList<SupBack>();

    public static SupBackMap getInstance() {
        return supbackmap;
    }

    @Override
    public SupBack get(Object backId) {
        return super.get(backId);
    }

    @Override
    public SupBack put(String backId, SupBack back) {
        log.info("the back " + back.getAddress() + " has been put into the back map.");
        return super.put(backId, back);
    }

    @Override
    public SupBack remove(Object key) {
        log.info("the back " + key + " has been removed from the back map.");
        return super.remove(key);
    }
    
    public synchronized SupBack getList() {
    	SupBack back = backlist.poll();
    	backlist.add(back);
		return back;
	}
	
	public LinkedList<SupBack> getBacklist() {
		return backlist;
	}

	public synchronized void setList(SupBack back){
		backlist.add(back);
	}

	public void setBacklist(LinkedList<SupBack> backlist) {
		this.backlist = backlist;
	}
}
