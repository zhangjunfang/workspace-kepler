package com.ctfo.storage.command.core;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.command.util.ConfigLoader;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： command <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>2014-8-14</td>
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
public class CommandMain {
	private static final Logger logger = LoggerFactory.getLogger(CommandMain.class);

	public static void main(String[] args) {
		try {
			// -start
			// if (args.length < 3) {//参数不合法
			// //System.out.println("输入参数不合法，请输入形式如“-d src start”的参数。");
			// logger.error("输入参数不合法，请输入形式如“-d src start”的参数。");
			// return;
			// }
			// 程序初始化
			ConfigLoader.init("");
		} catch (Exception e) {
			logger.error("服务启动异常:" + e.getMessage(), e);
			logger.error("系统退出...");
			System.exit(0);
		}
	}
}
