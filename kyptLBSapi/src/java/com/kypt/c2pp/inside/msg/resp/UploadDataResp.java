/*******************************************************************************
 * @(#)UploadDataResp.java 2008-10-24
 *
 * Copyright 2008 Neusoft Group Ltd. All rights reserved.
 * Neusoft PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 *******************************************************************************/
package com.kypt.c2pp.inside.msg.resp;

import com.kypt.c2pp.inside.msg.InsideMsgResp;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-10-24 下午06:13:28
 */
public class UploadDataResp extends InsideMsgResp {

	public static final String COMMAND = "1011";

	public static final String STATUS = "0000";
	
	public UploadDataResp(){
		super.setCommand(COMMAND);
	}
}
