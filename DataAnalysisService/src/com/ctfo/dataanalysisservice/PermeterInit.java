package com.ctfo.dataanalysisservice;

public class PermeterInit {
	
	//判断关键点时间容差
	private static long KeyPointTimeTolerance = 300;

	public static long getKeyPointTimeTolerance() {
		return KeyPointTimeTolerance;
	}

	public static void setKeyPointTimeTolerance(long keyPointTimeTolerance) {
		KeyPointTimeTolerance = keyPointTimeTolerance;
	}
}
