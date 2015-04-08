/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.monitoring.service.test;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.informationser.monitoring.beans.ThVehicleAlarmtodo;
import com.ctfo.informationser.monitoring.service.VehicleAlarmTodoRmi;
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
public class TestThAlarmTodoRmiTest extends GeneralTestBase {

	private ClassPathXmlApplicationContext classPath = GeneralTestBase.getClassXmlContext();

	private VehicleAlarmTodoRmi service = (VehicleAlarmTodoRmi) classPath.getBean("thAlarmTodoRmi");

	/**
	 * 添加报警测试.
	 */
	public void testadd() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("supervisionEndUtc", "100");
		map1.put("supervisionId", "101");
		map1.put("supervisionLevel", "1");
		map1.put("supervisor", "11");
		map1.put("supervisorEmail", "xxx@163.com");
		map1.put("supervisorTel", "5212372");
		map1.put("vehicleColor", "2");
		map1.put("vehicleno", "3");
		map1.put("wanSrc", "4");
		map1.put("wanType", "103");
		map1.put("warUtc", "104");
		param.setEqual(map1);
		String str;
		try {
			str = service.add(param, "abc");
			System.out.print(str);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/**
	 * 查询报警测试.
	 */
	public void testfindVehicleEarlywarningByparam() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("supervisionEndUtc", "100");
		map1.put("supervisionId", "101");
		map1.put("supervisionLevel", "1");
		map1.put("supervisor", "11");
		map1.put("supervisorEmail", "xxx@163.com");
		map1.put("supervisorTel", "5212372");
		map1.put("vehicleColor", "2");
		map1.put("vehicleno", "3");
		map1.put("wanSrc", "4");
		map1.put("wanType", "103");
		map1.put("warUtc", "104");
		param.setEqual(map1);
		List<ThVehicleAlarmtodo> list = new ArrayList<ThVehicleAlarmtodo>();
		list = service.findVehicleAlarmTodoByparam(param);
		for(ThVehicleAlarmtodo tva : list){
			System.out.print(tva.getPid());
		}
	}

}
