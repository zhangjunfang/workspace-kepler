package com.ctfo.util;

public class GeneratingCode {
	private static int comCode ;
	private static int entCode ;
	private static int opCode ;
	private static int roleCode ;
	
	public GeneratingCode(int comCode,int entCode,int opCode,int roleCode){
		this.comCode = comCode;
		this.entCode = entCode;
		this.opCode = opCode;
		this.roleCode = roleCode;
	}
	
	public static int getOpCode() {
		return ++opCode;
	}

	public static synchronized int getComCode(){
		return ++comCode;
	}
	public static synchronized int getEntCode(){
		return ++entCode;
	}

	public static int getRoleCode() {
		return ++roleCode;
	}
	
}