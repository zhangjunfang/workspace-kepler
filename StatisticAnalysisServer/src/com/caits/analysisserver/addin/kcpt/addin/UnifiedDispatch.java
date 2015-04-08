package com.caits.analysisserver.addin.kcpt.addin;

import java.io.File;

public abstract class UnifiedDispatch extends Thread{
	// 初始化方法
	public abstract void initAnalyser();
	
	// 读报警文件
	public abstract void addPacket(File file);
	
	public abstract void costTime();
	
	// 设置线程ID
	public abstract void setThreadId(int threadId);
	
	// 设置统计时间
	public abstract void setTime(long utc);
	
}
