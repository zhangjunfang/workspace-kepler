package com.ctfo.storage.parse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.service.MySqlService;
import com.ctfo.storage.util.ConfigLoader;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 批量操作监听进程<br>
 * 描述： 批量操作监听进程<br>
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
public class ListListen extends Thread {

	private static Logger logger = LoggerFactory.getLogger(ListListen.class);

	/** 默认每30秒提交一次 */
	private long batchTime = 30000;

	private MySqlService mySqlService = new MySqlService();

	public ListListen() {
		batchTime = Integer.valueOf(ConfigLoader.commitParamMap.get("commitBatchTime"));
		mySqlService = new MySqlService();
		mySqlService.setSqlMap(ConfigLoader.sqlMap);
	}

	public void run() {
		logger.info("--------------------批量操作监听线程已启动--------------------");
		while (true) {
			// 维修
			long curTime1 = System.currentTimeMillis();
			if (curTime1 - MaintainManageThread.getLastTime() > batchTime) {
				MaintainManageThread.updateList(mySqlService);
			}

			long curTime2 = System.currentTimeMillis();
			if (curTime2 - MaintainManageUpdateThread.getLastTime() > batchTime) {
				MaintainManageUpdateThread.updateList(mySqlService);
			}

			long curTime3 = System.currentTimeMillis();
			if (curTime3 - MaintainManageDelThread.getLastTime() > batchTime) {
				MaintainManageDelThread.updateList(mySqlService);
			}

			// 基础数据
			long curTime4 = System.currentTimeMillis();
			if (curTime4 - BaseDataManageThread.getLastTime() > batchTime) {
				BaseDataManageThread.updateList(mySqlService);
			}

			long curTime5 = System.currentTimeMillis();
			if (curTime5 - BaseDataManageUpdateThread.getLastTime() > batchTime) {
				BaseDataManageUpdateThread.updateList(mySqlService);
			}

			long curTime6 = System.currentTimeMillis();
			if (curTime6 - BaseDataManageDelThread.getLastTime() > batchTime) {
				BaseDataManageDelThread.updateList(mySqlService);
			}

			// 财务管理
			long curTime7 = System.currentTimeMillis();
			if (curTime7 - FinanceManageThread.getLastTime() > batchTime) {
				FinanceManageThread.updateList(mySqlService);
			}

			long curTime8 = System.currentTimeMillis();
			if (curTime8 - FinanceManageUpdateThread.getLastTime() > batchTime) {
				FinanceManageUpdateThread.updateList(mySqlService);
			}

			long curTime9 = System.currentTimeMillis();
			if (curTime9 - FinanceManageDelThread.getLastTime() > batchTime) {
				FinanceManageDelThread.updateList(mySqlService);
			}

			// 会员管理
			long curTime10 = System.currentTimeMillis();
			if (curTime10 - MemberManageThread.getLastTime() > batchTime) {
				MemberManageThread.updateList(mySqlService);
			}

			long curTime11 = System.currentTimeMillis();
			if (curTime11 - MemberManageUpdateThread.getLastTime() > batchTime) {
				MemberManageUpdateThread.updateList(mySqlService);
			}

			long curTime12 = System.currentTimeMillis();
			if (curTime12 - MemberManageDelThread.getLastTime() > batchTime) {
				MemberManageDelThread.updateList(mySqlService);
			}

			// 库存管理
			long curTime13 = System.currentTimeMillis();
			if (curTime13 - PartsManageThread.getLastTime() > batchTime) {
				PartsManageThread.updateList(mySqlService);
			}

			long curTime14 = System.currentTimeMillis();
			if (curTime14 - PartsManageUpdateThread.getLastTime() > batchTime) {
				PartsManageUpdateThread.updateList(mySqlService);
			}

			long curTime15 = System.currentTimeMillis();
			if (curTime15 - PartsManageDelThread.getLastTime() > batchTime) {
				PartsManageDelThread.updateList(mySqlService);
			}

			// 权限管理
			long curTime16 = System.currentTimeMillis();
			if (curTime16 - PermissionManageThread.getLastTime() > batchTime) {
				PermissionManageThread.updateList(mySqlService);
			}

			long curTime17 = System.currentTimeMillis();
			if (curTime17 - PermissionManageUpdateThread.getLastTime() > batchTime) {
				PermissionManageUpdateThread.updateList(mySqlService);
			}

			long curTime18 = System.currentTimeMillis();
			if (curTime18 - PermissionManageDelThread.getLastTime() > batchTime) {
				PermissionManageDelThread.updateList(mySqlService);
			}

			// 系统设置
			long curTime19 = System.currentTimeMillis();
			if (curTime19 - SystemSettingsManageThread.getLastTime() > batchTime) {
				SystemSettingsManageThread.updateList(mySqlService);
			}

			long curTime20 = System.currentTimeMillis();
			if (curTime20 - SystemSettingsManageUpdateThread.getLastTime() > batchTime) {
				SystemSettingsManageUpdateThread.updateList(mySqlService);
			}

			long curTime21 = System.currentTimeMillis();
			if (curTime21 - SystemSettingsManageDelThread.getLastTime() > batchTime) {
				SystemSettingsManageDelThread.updateList(mySqlService);
			}

			long curTime22 = System.currentTimeMillis();
			if (curTime22 - SupportThread.getLastTime() > batchTime) {
				SupportThread.updateList(mySqlService);
			}

			try {
				Thread.sleep(20000);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}
}
