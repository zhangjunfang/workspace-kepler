package com.kypt.c2pp.inside.msg.req;

import com.kypt.c2pp.inside.msg.InsideMsg;



/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-10-22 上午11:32:51
 */
public class NoopReq extends InsideMsg {

    public static final String COMMAND = "NOOP";

    public static final String STATUS = "0000";
    
    public NoopReq(){
    	super.setCommand(COMMAND);
    }

}

