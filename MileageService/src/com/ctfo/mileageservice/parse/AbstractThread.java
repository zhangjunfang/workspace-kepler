package com.ctfo.mileageservice.parse;



public abstract class AbstractThread extends Thread{
	// 初始化方法
	public abstract void init();
	
	// 读报警文件
	public abstract void addPacket(Object o);
	
	public abstract void close();
	
	// 设置线程ID
	public abstract void setThreadId(int threadId);
	
	// 设置统计时间
	public abstract void setTime(long utc);

	
}
