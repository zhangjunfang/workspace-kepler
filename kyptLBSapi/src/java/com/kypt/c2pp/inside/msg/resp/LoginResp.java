package com.kypt.c2pp.inside.msg.resp;

import com.kypt.c2pp.inside.msg.InsideMsg;

/**
 * @author <a href="mailto:pud@neusoft.com">pu dong </a>
 * @version $Revision 1.1 $ 2008-11-28 上午09:55:55
 */
public class LoginResp extends InsideMsg {

	public static final String COMMAND = "LACK";

	private String result;

	public String getResult() {
		return result;
	}

	public void setResult(String result) {
		this.result = result;
	}

}
