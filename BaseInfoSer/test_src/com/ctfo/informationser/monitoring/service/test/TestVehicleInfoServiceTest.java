/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.monitoring.service.test;

import java.util.HashMap;
import java.util.Map;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.informationser.monitoring.service.VehicleInfoService;
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
public class TestVehicleInfoServiceTest extends GeneralTestBase {

	private ClassPathXmlApplicationContext classPath = GeneralTestBase.getClassXmlContext();

	private VehicleInfoService service = (VehicleInfoService) classPath.getBean("vehicleInforService");

	/**
	 * 日志
	 */
	private static Log log = LogFactory.getLog(TestVehicleInfoServiceTest.class);

	/**
	 * Test method for {@link com.ctfo.informationser.monitoring.service.VehicleInfoService#getRegVehicleInfo(com.ctfo.local.obj.DynamicSqlParameter, java.lang.String)}.
	 */
	public void testgetRegVehicleInfo() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("commaddr", "13112345678");
		map.put("tmodelCode", "1");
		param.setEqual(map);
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("tmac", "terminalid");
		map1.put("commaddr", "phoneNum");
		map1.put("vehicleNo", "vehicleno");
		map1.put("plateColor", "vehicleColor");
		map1.put("oemCode", "manufacturerid");
		map1.put("tmodelCode", "terminaltype");
		map1.put("cityId", "cityid");
		param.setOutputValue(map1);
		log.info(service.getRegVehicleInfo(param, "1"));
	}

	public void testGetBaseVehicleInfo() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("commaddr", "13112345678");
		map.put("tmodelCode", "13112345678");
		param.setEqual(map);
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("plateColor", "vehicleColor");
		map1.put("vehicleNo", "vehicleno");
		map1.put("cityId", "cityid");
		param.setOutputValue(map1);
		log.info(service.getBaseVehicleInfo(param, "1"));
	}

	public void testGetDriverOfVehicleByType() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("plateColor", "1");
		map.put("vehicleNo", "11");
		param.setEqual(map);
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("plateColor", "vehicleColor");
		map1.put("vehicleNo", "vehicleno");
		map1.put("cityId", "cityid");
		map1.put("staffName", "driverName");
		map1.put("driverNo", "driverNo");
		map1.put("bussinessId", "driverCertificate");
		map1.put("drivercardDep", "certificateAgency");
		param.setOutputValue(map1);
		log.info(service.getDriverOfVehicleByType(param, "1"));
	}

	public void testGetEticketByVehicle() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("plateColor", "1");
		map.put("vehicleNo", "11");
		param.setEqual(map);
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("plateColor", "vehicleColor");
		map1.put("vehicleNo", "vehicleno");
		map1.put("eticketContent", "eticketContent");
		map1.put("cityId", "cityid");
		param.setOutputValue(map1);
		log.info(service.getEticketByVehicle(param, "1"));
	}

	public void testGetTernimalByVehicleByType() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("plateColor", "1");
		map.put("vehicleNo", "11");
		param.setEqual(map);
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("tmodelCode", "terminaltype");
		map1.put("commaddr", "phoneNum");
		map1.put("cityId", "cityid");
		param.setOutputValue(map1);
		log.info(service.getTernimalByVehicleByType(param, "1"));
	}

	public void testIsDriverOfVehicle() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("commaddr", "13112345678");
		map.put("vehicleNo", "11");
		map.put("driverNo", "1");
		map.put("bussinessId", "1");
		param.setEqual(map);
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("result", "result");
		param.setOutputValue(map1);
		log.info(service.isDriverOfVehicle(param, "1"));
	}

	public void testGetDetailOfVehicleInfo() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("plateColor", "2");
		map.put("vehicleNo", "京A00001");
		param.setEqual(map);
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("vehicleNo", "vehicleno");
		map1.put("plateColor", "plateColorId");
		map1.put("vehicletypeId", "prodCode");
		map1.put("generalCode", "vehicletypeId");
		map1.put("codeName", "typeName");
		map1.put("transtypeCode", "transTypeDesc");
		map1.put("cityId", "city");
		map1.put("licenceNo", "corpId");
		map1.put("orgCname", "corpName");
		map1.put("orgCphone", "linkPhone");
		map1.put("vehicleOperationId", "spId");
		param.setOutputValue(map1);
		log.info(service.getDetailOfVehicleInfo(param, "1"));

	}

	public void testIsRegVehicle() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("commaddr", "15294604174");
		map.put("plateColor", "0");
		map.put("vehicleNo", "LZYTBTD68B1048614");
		map.put("tmac", "1203071");
		param.setEqual(map);
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("result", "result");
		map1.put("akey", "akey");
		map1.put("oemcode", "oemcode");
		param.setOutputValue(map1);
		try {
			log.info(service.isRegVehicle(param, "1"));
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	public void testisCheckVehicle() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("commaddr", "15290424620");
		map.put("akey", "1325095316491");
		param.setEqual(map);
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("result", "result");
		map1.put("oemcode", "oemcode");
		param.setOutputValue(map1);
		log.info(service.isCheckVehicle(param, "1"));
	}

	public void testisLogoffVehicle() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("commaddr", "15000000001");
		map.put("plateColor", "2");
		map.put("vehicleNo", "京A00001");
		map.put("tmac", "0000001");
		map.put("tmodelCode", "15000000001");
		param.setEqual(map);
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("result", "result");
		param.setOutputValue(map1);
		log.info(service.isLogoffVehicle(param, "1"));
	}

	public void testgetDetailOfVehicleInfo() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("vehicleNo", "11");
		map.put("plateColor", "1");
		param.setEqual(map);
		Map<String, String> map1 = new HashMap<String, String>();
		map1.put("vehicleNo", "vehicleno");
		map1.put("plateColor", "plateColorId");
		map1.put("generalCode", "vehicletypeId");
		map1.put("codeName", "typeName");
		map1.put("transtypeCode", "transTypeDesc");
		map1.put("cityId", "city");
		map1.put("licenceNo", "corpId");
		map1.put("orgCname", "corpName");
		map1.put("orgCphone", "linkPhone");
		map1.put("vehicleOperationId", "spId");
		param.setOutputValue(map1);
		log.info(service.getDetailOfVehicleInfo(param, "1"));
	}

	/**
	 * 测试 -- 根据2G卡号获取3G卡号
	 */
	public void testGet2gBy3g() {
		long start = System.currentTimeMillis();
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("sim3", "13333333333");
		// map.put("oemcode", "13333333333");

		Map<String, String> output = new HashMap<String, String>();
		output.put("commaddr", "commaddr");
		output.put("oemcode", "oemcode");
		param.setOutputValue(output);
		param.setEqual(map);
		try {
			log.info("\n------根据3G卡号获取2G卡号测试------开始--------");
			log.info("\n" + service.get2gBy3g(param, "alarm"));
			long end = System.currentTimeMillis();
			log.info("\n根据3G卡号获取2G卡号测试-------结束-------耗时:" + (end - start));
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	/**
	 * 测试 -- 根据3G卡号获取2G卡号
	 */
	public void testGet3gBy2g() {
		long start = System.currentTimeMillis();
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("sim2", "13523452764");
		// map.put("oemcode", "13333333333");

		Map<String, String> output = new HashMap<String, String>();
		output.put("commaddr", "commaddr");
		output.put("oemcode", "oemcode");
		param.setOutputValue(output);
		param.setEqual(map);
		try {
			log.info("\n------根据3G卡号获取2G卡号测试------开始--------");
			log.info("\n" + service.get3gBy2g(param, "alarm"));
			long end = System.currentTimeMillis();
			log.info("\n根据3G卡号获取2G卡号测试-------结束-------耗时:" + (end - start));
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	/**
	 * 测试 -- 获得2g3gSim卡号映射表
	 */
	public void testGet2g3gSimMapping() {
		long start = System.currentTimeMillis();
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("sim3", "13333333333");
		// map.put("oemcode", "13333333333");

		Map<String, String> output = new HashMap<String, String>();
		output.put("commaddr", "commaddr");
		output.put("oemcode", "oemcode");
		param.setOutputValue(output);
		param.setEqual(map);
		try {
			log.info("\n------获得2g3gSim卡号映射表------开始--------");
			log.info("\n" + service.get2g3gSimMapping(param, "alarm"));
			long end = System.currentTimeMillis();
			log.info("\n获得2g3gSim卡号映射表-------结束-------耗时:" + (end - start));
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/**
	 * 老运营非国标鉴权服务
	 */
	public void testGetAllBaseInfoByTid() {
		DynamicSqlParameter param = new DynamicSqlParameter();
		Map<String, String> map = new HashMap<String, String>();
		map.put("tmac", "11C1169");
		Map<String, String> output = new HashMap<String, String>();
		output.put("result", "result");
		output.put("akey", "akey");
		output.put("oemcode", "oemcode");
		output.put("plateColor", "plateColor");
		output.put("vehicleNo", "vehicleNo");
		output.put("commaddr", "phoneNum");
		param.setOutputValue(output);
		param.setEqual(map);
		try {
			String result = service.isRegVehicleNOGBNew(param, "1");
			System.out.println(result);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
