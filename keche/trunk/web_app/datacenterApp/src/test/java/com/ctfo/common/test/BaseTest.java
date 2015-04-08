package com.ctfo.common.test;

import junit.framework.TestCase;

import org.springframework.context.support.ClassPathXmlApplicationContext;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： BaseTest<br>
 * 描述： BaseTest<br>
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
 * <td>2014-5-27</td>
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
public class BaseTest extends TestCase {

	protected static ClassPathXmlApplicationContext classPath = null;

	protected void setUp() throws Exception {
		System.out.println("Base up..............");
	}

	protected void tearDown() throws Exception {
		System.out.println("Base down............");
	}

	protected void init() {
	}

	public static synchronized ClassPathXmlApplicationContext getClassPath() {
		if (null == classPath) {
			String[] classXmlContexts = { "classpath:/com/ctfo/resource/*/spring-*.xml" };
			try {
				classPath = new ClassPathXmlApplicationContext(classXmlContexts);
			} catch (Exception e) {
				System.out.println("-----------------------------------------------------------");
				e.printStackTrace();
			}
		}
		return classPath;
	}

}
