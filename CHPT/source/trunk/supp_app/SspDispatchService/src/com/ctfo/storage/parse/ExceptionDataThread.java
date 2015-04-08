package com.ctfo.storage.parse;

import java.lang.reflect.Method;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.io.SendMsgThread;
import com.ctfo.storage.util.ConfigLoader;
import com.ctfo.storage.util.Constant;
import com.ctfo.storage.util.SerialUtil;
import com.ctfo.storage.util.Tools;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能：存储操作异常监听进程<br>
 * 描述：存储操作异常监听进程<br>
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
 * <td>2014-11-11</td>
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
public class ExceptionDataThread extends Thread {

	private static Logger logger = LoggerFactory.getLogger(ExceptionDataThread.class);

	/** 异常数据队列 */
	private static ArrayBlockingQueue<Object> queue = new ArrayBlockingQueue<Object>(500000);

	public ExceptionDataThread() {
		setName("ExceptionDataThread");
	}

	public void run() {
		while (true) {
			try {
				Object o = queue.take();
				String beanName = o.getClass().getSimpleName(); // 对象名
				String fieldName = ConfigLoader.protocolMap.get(beanName); // 字段名

				String getterId = "get" + fieldName.substring(0, fieldName.indexOf("#"));
				Method getFieldId = o.getClass().getMethod(getterId, new Class[] {});
				Object fieldId = getFieldId.invoke(o, new Object[] {}); // 主键值

				String getterStationId = "getSerStationId";
				Method getStationId = o.getClass().getMethod(getterStationId, new Class[] {});
				Object stationId = getStationId.invoke(o, new Object[] {}); // 服务站id

				StringBuffer message = new StringBuffer(); // 错误数据指令(服务端发送至服务站客户端)
				message.append(Constant.LEFT_BRACKET);
				message.append(stationId + Constant.DOLLAR);
				message.append(SerialUtil.getInt()); // 流水号
				message.append(Constant.TYPE_W); // 主类型
				message.append("W" + fieldName.substring(fieldName.indexOf("#") + 1) + Constant.DOLLAR); // 子类型
				message.append(System.currentTimeMillis() + Constant.DOLLAR); // 时间戳
				message.append(fieldId + Constant.DOLLAR);
				message.append(Tools.getCheckCode(message.toString())); // 校验码
				message.append(Constant.RIGHT_BRACKET);
				SendMsgThread.put(message.toString());
			} catch (Exception e) {
				logger.error("异常数据处理队列数据异常:" + e);
			}
		}
	}

	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * 
	 * @param data
	 * @return
	 */
	public static void put(Object o) {
		try {
			queue.put(o);
		} catch (InterruptedException e) {
			logger.error("插入数据到队列异常!");
		}
	}
}
