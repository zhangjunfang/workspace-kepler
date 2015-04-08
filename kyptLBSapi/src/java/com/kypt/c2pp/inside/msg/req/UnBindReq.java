/*******************************************************************************
 * @(#)ActiveTestRequest.java 2008-10-22
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.inside.msg.req;

import com.kypt.c2pp.inside.msg.InsideMsg;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-10-22 上午11:32:51
 */
public class UnBindReq extends InsideMsg {

    public static final String COMMAND = "0002";

    public static final String STATUS = "0000";
    
    public UnBindReq(){
		super.setCommand(COMMAND);
	}

}
