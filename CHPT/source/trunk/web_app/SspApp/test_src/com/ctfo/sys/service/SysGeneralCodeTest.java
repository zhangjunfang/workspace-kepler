package com.ctfo.sys.service;

import net.sf.json.JSONObject;

import org.junit.Test;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.baseinfo.dao.SysGeneralCodeDao;
import com.ctfo.baseinfo.service.SysGeneralCodeService;
import com.ctfo.baseinfo.service.impl.SysGeneralCodeServiceImpl;
import com.ctfo.local.obj.DynamicSqlParameter;

public class SysGeneralCodeTest {
	String[] classXmlContexts = new String[]{"classpath:com/ctfo/resource/springmvc-servlet.xml","com/ctfo/resource/applicationContext*.xml"};
	private ClassPathXmlApplicationContext classPathXmlContext = new ClassPathXmlApplicationContext(classXmlContexts);
	SysGeneralCodeDao dao = (SysGeneralCodeDao)classPathXmlContext.getBean("sysGeneralCodeDao");
	SysGeneralCodeService service = (SysGeneralCodeService)classPathXmlContext.getBean(SysGeneralCodeServiceImpl.class);
	
	@Test
	public void selectRoleByEntIdTest(){
/*		Map<String, String> like=new HashMap<String,String>();
		like.put("opId", "8465a36d7e10450193f5346597b6e8e1");*/
		DynamicSqlParameter params = new DynamicSqlParameter();
		String jsonResult = service.findSysGeneralCodeByCode(params);
		JSONObject json = JSONObject.fromObject(jsonResult);
		
		System.out.println(json);
/*		List<Integer> list = new ArrayList<Integer>();
		list.add(1, 0);
		list.add(2, 1);
		Map<String,List> areaMap = new HashMap<String, List>();
		areaMap.put("areaLevel", list);
		params.setInMap(areaMap);
		dao.selectPagination(params);*/
	}
}
