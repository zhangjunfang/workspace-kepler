package com.ctfo.storage.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.io.SendMsgThread;
import com.ctfo.storage.service.ProtocolAnalyService;
import com.ctfo.storage.util.ConfigLoader;
import com.ctfo.storage.util.Constant;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 指令解析线程<br>
 * 描述： 指令解析线程<br>
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
 * <td>2014-10-23</td>
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
public class Parse extends Thread {

	private static final Logger logger = LoggerFactory.getLogger(Parse.class);
	/** 数据队列 */
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(500000);
	/** 通讯发送数据处理类 */
	private SendMsgThread sendMsgThread;
	/** 数据解析 */
	private ProtocolAnalyService protocolAnalyService;
	/** 维修管理添加线程 */
	private MaintainManageThread maintainManageThread;
	/** 维修管理更新线程 */
	private MaintainManageUpdateThread maintainManageUpdateThread;
	/** 维修管理删除线程 */
	private MaintainManageDelThread maintainManageDelThread;
	/** 权限管理线程 */
	private PermissionManageThread permissionManageThread;
	/** 权限管理更新线程 */
	private PermissionManageUpdateThread permissionManageUpdateThread;
	/** 权限管理删除线程 */
	private PermissionManageDelThread permissionManageDelThread;
	/** 库存模块线程 */
	private PartsManageThread partsManageThread;
	/** 库存模块更新线程 */
	private PartsManageUpdateThread partsManageUpdateThread;
	/** 库存模块删除线程 */
	private PartsManageDelThread partsManageDelThread;
	/** 财务管理线程 */
	private FinanceManageThread financeManageThread;
	/** 财务管理修改线程 */
	private FinanceManageUpdateThread financeManageUpdateThread;
	/** 财务管理删除线程 */
	private FinanceManageDelThread financeManageDelThread;
	/** 会员管理线程 */
	private MemberManageThread memberManageThread;
	/** 会员管理更新线程 */
	private MemberManageUpdateThread memberManageUpdateThread;
	/** 会员管理删除线程 */
	private MemberManageDelThread memberManageDelThread;
	/** 基础数据线程 */
	private BaseDataManageThread baseDataManageThread;
	/** 基础数据更新线程 */
	private BaseDataManageUpdateThread baseDataManageUpdateThread;
	/** 基础数据删除线程 */
	private BaseDataManageDelThread baseDataManageDelThread;
	/** 系统设置线程 */
	private SystemSettingsManageThread systemSettingsManageThread;
	/** 系统设置更新线程 */
	private SystemSettingsManageUpdateThread systemSettingsManageUpdateThread;
	/** 系统设置删除线程 */
	private SystemSettingsManageDelThread systemSettingsManageDelThread;
	/** 支撑系统监控类同步线程 */
	private SupportThread supportThread;
	/** 计数器 */
	private int index;
	/** 最后记录时间 */
	private long lastTime = System.currentTimeMillis();

	public Parse() {
		protocolAnalyService = new ProtocolAnalyService();
		setName("Parse-thread");
		sendMsgThread = new SendMsgThread();
		sendMsgThread.start();
		supportThread = new SupportThread();
		supportThread.start();

		maintainManageThread = new MaintainManageThread();
		permissionManageThread = new PermissionManageThread();
		partsManageThread = new PartsManageThread();
		financeManageThread = new FinanceManageThread();
		memberManageThread = new MemberManageThread();
		baseDataManageThread = new BaseDataManageThread();
		systemSettingsManageThread = new SystemSettingsManageThread();
		maintainManageThread.start();
		permissionManageThread.start();
		partsManageThread.start();
		financeManageThread.start();
		memberManageThread.start();
		baseDataManageThread.start();
		systemSettingsManageThread.start();

		maintainManageUpdateThread = new MaintainManageUpdateThread();
		permissionManageUpdateThread = new PermissionManageUpdateThread();
		partsManageUpdateThread = new PartsManageUpdateThread();
		financeManageUpdateThread = new FinanceManageUpdateThread();
		memberManageUpdateThread = new MemberManageUpdateThread();
		baseDataManageUpdateThread = new BaseDataManageUpdateThread();
		systemSettingsManageUpdateThread = new SystemSettingsManageUpdateThread();
		maintainManageUpdateThread.start();
		permissionManageUpdateThread.start();
		partsManageUpdateThread.start();
		financeManageUpdateThread.start();
		memberManageUpdateThread.start();
		baseDataManageUpdateThread.start();
		systemSettingsManageUpdateThread.start();

		maintainManageDelThread = new MaintainManageDelThread();
		permissionManageDelThread = new PermissionManageDelThread();
		partsManageDelThread = new PartsManageDelThread();
		financeManageDelThread = new FinanceManageDelThread();
		memberManageDelThread = new MemberManageDelThread();
		baseDataManageDelThread = new BaseDataManageDelThread();
		systemSettingsManageDelThread = new SystemSettingsManageDelThread();
		maintainManageDelThread.start();
		permissionManageDelThread.start();
		partsManageDelThread.start();
		financeManageDelThread.start();
		memberManageDelThread.start();
		baseDataManageDelThread.start();
		systemSettingsManageDelThread.start();
	}

	public void run() {
		while (true) {
			try {
				String message = queue.take(); // 获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）
				
				index++;
				long current = System.currentTimeMillis();
				if (current - lastTime > 10000) {
					// logger.info("-----------------10秒处理[{}]条,应答[{}],剩余[{}]", index, ResponseListen.getCount(), ResponseListen.getQueue().size());
					index = 0;
					lastTime = System.currentTimeMillis();
				}
				process(message);
			} catch (Exception e) {
				logger.error("Parse处理队列数据异常:" + e.getMessage());
			}
		}
	}

	/**
	 * 数据解析
	 * 
	 * @param message
	 */
	@SuppressWarnings("static-access")
	private void process(String message) {
		if (message.indexOf(Constant.TYPE_U) > 0) { // 服务站上传数据类 U
			// 分析数据
			String data[] = message.substring(1, message.length() - 1).split("\\" + Constant.DOLLAR);
			Object obj = protocolAnalyService.getTableFromControl(data[6], ConfigLoader.protocolMap.get(data[3].substring(0, 2)), ConfigLoader.protocolMap.get(data[3]));
			if (data[5].equals(Constant.N0)) { // 删除
				if (message.indexOf(Constant.TYPE_U1) > 0) { // 维修管理
					maintainManageDelThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U2) > 0) { // 权限管理
					permissionManageDelThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U3) > 0) { // 库存管理
					partsManageDelThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U4) > 0) { // 财务管理
					financeManageDelThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U5) > 0) { // 会员管理
					memberManageDelThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U6) > 0) { // 基础数据
					baseDataManageDelThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U7) > 0) { // 系统设置
					systemSettingsManageDelThread.put(obj);
				}
			} else if (data[5].equals(Constant.N1)) { // 新增
				if (message.indexOf(Constant.TYPE_U1) > 0) { // 维修管理
					maintainManageThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U2) > 0) { // 权限管理
					permissionManageThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U3) > 0) { // 库存管理
					partsManageThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U4) > 0) { // 财务管理
					financeManageThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U5) > 0) { // 会员管理
					memberManageThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U6) > 0) { // 基础数据
					baseDataManageThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U7) > 0) { // 系统设置
					systemSettingsManageThread.put(obj);
				}
			} else if (data[5].equals(Constant.N2)) { // 修改
				if (message.indexOf(Constant.TYPE_U1) > 0) { // 维修管理
					maintainManageUpdateThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U2) > 0) { // 权限管理
					permissionManageUpdateThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U3) > 0) { // 库存管理
					partsManageUpdateThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U4) > 0) { // 财务管理
					financeManageUpdateThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U5) > 0) { // 会员管理
					memberManageUpdateThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U6) > 0) { // 基础数据
					baseDataManageUpdateThread.put(obj);
				} else if (message.indexOf(Constant.TYPE_U7) > 0) { // 系统设置
					systemSettingsManageUpdateThread.put(obj);
				}
			}
		} else if (message.indexOf(Constant.TYPE_Y) > 0 || message.indexOf(Constant.TYPE_S) > 0 || message.indexOf(Constant.TYPE_C) > 0) { // 客户端(非服务站)上传数据至云平台类
			// 同步数据到服务站
			sendMsgThread.put(message);
		} else if (message.indexOf(Constant.TYPE_T) > 0) { // 服务站上传客户端状态类 T
			// 同步数据到MySql
			String data[] = message.substring(1, message.length() - 1).split("\\" + Constant.DOLLAR);
			Object obj = protocolAnalyService.getTableFromControl(data[6], ConfigLoader.protocolMap.get(data[2]), ConfigLoader.protocolMap.get(data[3]));
			supportThread.put(obj);
		}
	}

	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * 
	 * @param data
	 * @return
	 */
	public void put(String data) {
		try {
			queue.put(data);
		} catch (InterruptedException e) {
			logger.error("插入数据到队列异常!");
		}
	}
}
