package test;

import java.io.Serializable;

public class Velinfo implements Serializable{

	/**
	 * serialVersionUID:long
	 */
	private static final long serialVersionUID = 1L;
	
	private String str;
	
	/**
	 * @return the str
	 */
	public String getStr() {
		return str;
	}

	/**
	 * @param str the str to set
	 */
	public void setStr(String str) {
		this.str = str;
	}
	
}
