package com.ctfo.hessianproxy.monitoring.servlet;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;

import com.ctfo.hessianproxy.util.MonitoringUtil;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： HessianProxy <br>
 * 功能：读取配置文件monitoring.xml，加载定义文件。 <br>
 * 描述：读取配置文件monitoring.xml，加载定义文件。 <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
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
 * <td>Dec 29, 2011</td>
 * <td>DEVELOPER</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author DEVELOPER
 * @since JDK1.6
 */
@SuppressWarnings("serial")
public class MonitoringService extends HttpServlet {

	@Override
	public void init() throws ServletException {
		String filePath = this.getServletContext().getRealPath("/monitoring.xml");
		String path = this.getClass().getResource("/").getPath();
		filePath = path.substring(0, path.indexOf("classes") + 7);
		MonitoringUtil.getMonitoringDataMap(filePath + "/monitoring.xml");
		super.init();
	}

	public static void main(String[] args) {
		System.out.println("abcd".substring(0, "abcd".indexOf("b")));
	}
}
