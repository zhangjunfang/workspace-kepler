package com.ctfo.sys.service;

import net.sf.json.JSONObject;

public class RemoteTest {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		String json = "{aaa:1,bbb:2}";
		JSONObject remoteJson=JSONObject.fromObject(json);
		System.out.println(remoteJson);
	}

}
