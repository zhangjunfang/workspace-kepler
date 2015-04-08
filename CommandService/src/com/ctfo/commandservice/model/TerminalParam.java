/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>文件名称：com.ctfo.commandservice.model TerminalParam.java	</li><br>
 * <li>时        间：2013-12-10  上午11:41:48	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.model;

/*****************************************
 * <li>描        述：终端参数		
 * 
 *****************************************/
public class TerminalParam {
	/**	终端编号	*/
	private String tid;
	/**	参数键	*/
	private String paramKey;
	/**	参数值	*/
	private String paramValue;
	/**	时间	*/
	private Long sysutc;
	
	public String getTid() {
		return tid;
	}
	public void setTid(String tid) {
		this.tid = tid;
	}
	public String getParamKey() {
		return paramKey;
	}
	public void setParamKey(String paramKey) {
		this.paramKey = paramKey;
	}
	public String getParamValue() {
		return paramValue;
	}
	public void setParamValue(String paramValue) {
		this.paramValue = paramValue;
	}
	public Long getSysutc() {
		return sysutc;
	}
	public void setSysutc(Long sysutc) {
		this.sysutc = sysutc;
	}
}
