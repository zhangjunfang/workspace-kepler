/*******************************************************************************
 * @(#)ActiveTestRequest.java 2008-10-22
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.inside.msg.req;

import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsg;
import com.kypt.configuration.C2ppCfg;



/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-10-22 上午11:32:51
 */
public class ActiveTestReq extends InsideMsg {

    public static final String COMMAND = "NOOP";

    public static final String STATUS = "0000";
    
    @Override
    public byte[] getBytes() throws UnsupportedEncodingException {
    	
    	String req = this.toString();
    	String encoding = C2ppCfg.props.getProperty("superviseEncoding");
        if (encoding!=null&&encoding.length()>0){
        	return req.getBytes(encoding);
        }else{
        	return req.getBytes();
        }
        
    }
    
    public String toString() {
    	return COMMAND+"\r\n";
    }

}
