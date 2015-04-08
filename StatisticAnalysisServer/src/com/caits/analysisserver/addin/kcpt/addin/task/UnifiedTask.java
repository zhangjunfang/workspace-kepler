package com.caits.analysisserver.addin.kcpt.addin.task;

import java.util.TimerTask;

public abstract class UnifiedTask extends TimerTask {
	
	// 是否按设置的日期统计数据
	public abstract void isUsingSettingTime(boolean isUse);
	
	// 设置指定日期
	public abstract void setDate(long date);
}
