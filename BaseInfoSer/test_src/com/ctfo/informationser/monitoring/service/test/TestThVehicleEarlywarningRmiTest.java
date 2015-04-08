/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.monitoring.service.test;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.informationser.monitoring.beans.ThVehicleEarlywarning;
import com.ctfo.informationser.monitoring.service.VehicleEarlywarningServiceRmi;
import com.ctfo.informationser.test.util.GeneralTestBase;
import com.ctfo.local.obj.DynamicSqlParameter;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： BaseInfoSer <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>Dec 25, 2011</td>
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
public class TestThVehicleEarlywarningRmiTest extends GeneralTestBase {

	private ClassPathXmlApplicationContext classPath = GeneralTestBase.getClassXmlContext();

	private VehicleEarlywarningServiceRmi service = (VehicleEarlywarningServiceRmi) classPath.getBean("thVehicleEarlywarningRmi");

	/**
	 * 添加预警测试.
	 */
	public void testadd() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map1 = new HashMap<String, String>();

		map1.put("alarmType", "100");
		map1.put("alarmTime", "101");
		map1.put("alarmFrom", "1");
		map1.put("alarmDescr", "2");
		map1.put("vid", "1");
		param.setEqual(map1);
		String str1;
		try {
			str1 = service.add(param, "abc");
			System.out.print(str1);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/**
	 * 查询预警测试.
	 */
	public void testfindVehicleEarlywarningByparam() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("alarmType", "100");
		map1.put("alarmTime", "101");
		map1.put("alarmFrom", "1");
		map1.put("alarmDescr", "2");
		param.setEqual(map1);
		List<ThVehicleEarlywarning> list = new ArrayList<ThVehicleEarlywarning>();
		list = service.findVehicleEarlywarningByparam(param);
		System.out.print(list.size());
	}

}
