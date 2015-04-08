package com.ruishenh.dubbo.example;

public class DemoServiceImpl implements DemoService{
	
	public void sayHello() {
		System.out.println("hello world!");
	}

	public String returnHello() {
		return "hello world!";
	}

	public MsgInfo returnMsgInfo(MsgInfo info) {
		info.getMsgs().add("处理完毕");
		return info;
	}
}