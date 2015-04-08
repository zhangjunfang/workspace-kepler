package com.kypt.c2pp.inside.msg.req;

import java.io.UnsupportedEncodingException;

import com.kypt.c2pp.inside.msg.InsideMsg;
import com.kypt.c2pp.inside.msg.utils.InsideMsgUtils;


/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-11-28 上午09:56:05
 */
public class LoginReq extends InsideMsg {

    public static final String COMMAND = "LOGI";

    private String systemId;
    
    private String userId;

    private String password;
    
    private String userType;
    
    public LoginReq(){
    	super.setCommand(COMMAND);
    }

    public void setSystemId(String systemId) {
        this.systemId = (systemId == null || systemId.equals("")) ? null : InsideMsgUtils
                .formatSystemId(systemId);
    }

    public String getSystemId() {
        return systemId;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getPassword() {
        return password;
    }
    
	public String getUserId() {
		return userId;
	}

	public void setUserId(String userId) {
		this.userId = userId;
	}
	
	public String getUserType() {
		return userType;
	}

	public void setUserType(String userType) {
		this.userType = userType;
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
    	return COMMAND+" "+userType+" "+userId+" "+password+"\r\n";
    }


}
