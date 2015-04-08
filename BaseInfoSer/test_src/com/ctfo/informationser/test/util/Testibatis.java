package com.ctfo.informationser.test.util;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import junit.framework.TestCase;

import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.informationser.monitoring.beans.ThVehicleEarlywarning;
import com.ctfo.informationser.monitoring.service.VehicleEarlywarningServiceRmi;
import com.ctfo.local.obj.DynamicSqlParameter;

/**
 * @author dz
 * 
 */
public class Testibatis extends TestCase {
//	private static Log log = LogFactory.getLog(Testibatis.class);
	private ClassPathXmlApplicationContext classPath = GeneralTestBase.getClassXmlContext();
	private VehicleEarlywarningServiceRmi trVehicleEarlywarningServiceRmi = (VehicleEarlywarningServiceRmi) classPath.getBean("vehicleEarlywarningService");

	/**
	 * 按条件查询
	 */
	@SuppressWarnings("rawtypes")
	public void testFindByParams() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> equal = new HashMap<String, String>();
		equal.put("vid", "10012");
		param.setEqual(equal);
		Map<String, String> like = new HashMap<String, String>();
		like.put("alarmType", "aaaa");
		param.setLike(like);
		List list = new ArrayList();
//		list = trVehicleEarlywarningServiceRmi.findVehicleEarlywarningByparam(param);
		System.out.println("===============" + list.size());
	}

	/**
	 * 插入一条记录
	 */
	public void testinsert() {
		ThVehicleEarlywarning entity = new ThVehicleEarlywarning();
		entity.setPid("10010");
		entity.setVid("10011");
		entity.setAlarmType("aaaa");
		entity.setAlarmTime(Long.parseLong("10"));
		entity.setAlarmFrom(Short.parseShort("5"));
		entity.setAlarmDescr("abcdefg");
		// trVehicleEarlywarningServiceRmi.add(entity);
	}

	/**
	 * 修改记录
	 */
	public void testupdateByParams() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> equal = new HashMap<String, String>();
		equal.put("pid", "10010");
		param.setEqual(equal);
		Map<String, String> like = new HashMap<String, String>();
		like.put("alarmFrom", "5");
		like.put("alarmType", "aaaa");
		param.setLike(like);
		List<ThVehicleEarlywarning>  list = new ArrayList<ThVehicleEarlywarning> ();
		list = trVehicleEarlywarningServiceRmi.findVehicleEarlywarningByparam(param);
		System.out.println("===============" + list.size());
		Map<String, Object> update = new HashMap<String, Object>();
		update.put("vid", "10012");
		param.setUpdateValue(update);
		// trVehicleEarlywarningServiceRmi.update(param);
	}
}
