package com.ctfo.mgdb.io;

/**
 * 
 * 
 * 缓冲池
 * @author huangjincheng
 *
 */
public class DataPool {
	
	// 接收数据量
	private static int receiveCount = 0;
	
	
	/**
	 * 获取数据量值
	 * 
	 * @return
	 */
	public static long getReceiveCountValue() {
		long temp = receiveCount;
		receiveCount = 0;
		return temp;

	}

	/**
	 * 添加数据量值
	 * 
	 * @param value
	 */
	public static void setCountValue() {
		receiveCount++;
	}

}
