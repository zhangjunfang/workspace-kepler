package com.ctfo.storage.core;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.util.ConfigLoader;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： 数据处理服务（启动入口）<br>
 * 描述： 数据处理服务（启动入口）<br>
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
 * <td>2014-10-14</td>
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
public class SspDataMain {

	private static final Logger logger = LoggerFactory.getLogger(SspDataMain.class);

	public static void main(String[] args) {
		try {
			ConfigLoader.init(args);
		} catch (Exception e) {
			logger.error("服务启动异常:" + e.getMessage(), e);
			logger.error("系统退出...");
			System.exit(0);
		}
	}

}
