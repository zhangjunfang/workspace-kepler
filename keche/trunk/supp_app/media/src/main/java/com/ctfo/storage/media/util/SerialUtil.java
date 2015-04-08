/**
 * 
 */
package com.ctfo.storage.media.util;

import java.util.concurrent.atomic.AtomicInteger;

/**
 * 序列号获取工具（单例）
 * 
 */
public class SerialUtil {
	private static SerialUtil serialUtil = new SerialUtil();
	/**	原子计数器	*/
	private AtomicInteger atomicInteger;
	
	private SerialUtil(){
		atomicInteger = new AtomicInteger(0);
	}
	/**
	 * 以原子方式将当前值加 1
	 * @return 更新的值
	 */
	public static int getInt(){
		if(serialUtil == null){
			return new SerialUtil().incrementAndGet();
		} else {
			return serialUtil.incrementAndGet();
		}
	}
	
	private int incrementAndGet(){
		return atomicInteger.incrementAndGet();
	}
}
