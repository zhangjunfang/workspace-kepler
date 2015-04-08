package com.caits.analysisserver.addin.kcpt.addin;

public abstract class SelfDispatch extends Thread{
	// 初始化方法
	public abstract void initAnalyser();
	
	public abstract void costTime();
	
}
