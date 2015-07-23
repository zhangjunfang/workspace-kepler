package com.fujitsu.fnst.asm.demo;

import java.text.SimpleDateFormat;
import java.util.Date;

public class TimeRetriever implements ITimeRetriever{
	private static final String TIME_PATTERN = "yyyy/MM/dd hh:mm:ss";

	public String tellMeTheTime(){
		SimpleDateFormat format = new SimpleDateFormat(TIME_PATTERN);
		Date now = new Date();
		return format.format(now);
	}
}
