package com.ctfo.storage.util;

import java.util.concurrent.atomic.AtomicInteger;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 序列号获取工具（单例）<br>
 * 描述： 序列号获取工具（单例）<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-10-29</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class SerialUtil {

	private static SerialUtil serialUtil = new SerialUtil();

	/** 原子计数器 */
	private AtomicInteger atomicInteger;

	private SerialUtil() {
		atomicInteger = new AtomicInteger(0);
	}

	/**
	 * 以原子方式将当前值加 1
	 * 
	 * @return 更新的值
	 */
	public static int getInt() {
		if (serialUtil == null) {
			return new SerialUtil().incrementAndGet();
		} else {
			return serialUtil.incrementAndGet();
		}
	}

	private int incrementAndGet() {
		return atomicInteger.incrementAndGet();
	}

}
