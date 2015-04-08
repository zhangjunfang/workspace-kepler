package com.kypt.c2pp.inside.processor.resp;

import com.kypt.c2pp.inside.msg.resp.LoginResp;
import com.kypt.c2pp.inside.processor.AbstractInsideProcessor;
import com.kypt.c2pp.nio.SupCommService;
import com.kypt.exception.HandleException;
import com.kypt.exception.InvalidMessageException;
import com.kypt.exception.ParseException;


/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-11-24 下午03:30:48
 */
public final class LoginRespProcessor extends AbstractInsideProcessor<LoginResp, SupCommService> {

    private static final LoginRespProcessor PROCESSOR = new LoginRespProcessor();

    private LoginRespProcessor() {
    }

    public static LoginRespProcessor getInstance() {
        return PROCESSOR;
    }

    public LoginResp parse(byte[] buf) throws ParseException {
        try {
        	LoginResp resp = new LoginResp();
            super.parseHeader(buf, resp);
            return resp;
        } catch (Throwable t) {
            throw new ParseException("parse bind response failed.", t);
        }
    }

    public void valid(LoginResp msg) throws InvalidMessageException {
        super.validHeader(msg);
    }

    public void handle(LoginResp msg, SupCommService supCommService) throws HandleException {
    }

	public LoginResp parse(String msg) throws ParseException {
		// TODO Auto-generated method stub
		try {
        	LoginResp resp = new LoginResp();
        	
        	String message[] = msg.split("\\s+");
        	
        	if (message==null||message.length<2){
        		resp.setResult("-4");
        	}else{
        		resp.setResult(message[1]);
        	}
        	
            return resp;
        } catch (Throwable t) {
            throw new ParseException("parse bind response failed.", t);
        }
	}

	public LoginResp parse(String[] msg) throws ParseException {
		// TODO Auto-generated method stub
		LoginResp resp = new LoginResp();
		resp.setResult(msg[1]);
		System.out.println(":"+msg[0]+"---"+msg[1]);
		resp.setStatusCode(msg[1]);
		return resp;
	}
}
