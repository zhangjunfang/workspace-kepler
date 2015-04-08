package com.kypt.c2pp.inside.msg.req;

import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsg;


/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-11-28 上午09:56:05
 */
public class LogoReq extends InsideMsg {

    public static final String COMMAND = "LOGO";
    
    public LogoReq(){
    	super.setCommand(COMMAND);
    }

    @Override
    public byte[] getBytes() throws UnsupportedEncodingException {
    	
    	String req = this.toString();
        if (this.getEncoding()!=null&&this.getEncoding().length()>0){
        	return req.getBytes(this.getEncoding());
        }else{
        	return req.getBytes();
        }
        
    }
    
    public String toString() {
    	return COMMAND+"\r\n";
    }


}
