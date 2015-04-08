package com.ctfo.sys.service;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.junit.Test;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.sys.dao.SysFunctionDAO;

public class SysFunctionServiceTest {

	ApplicationContext ctx = new ClassPathXmlApplicationContext("com/ctfo/resource/applicationContext*.xml"); 
	SysFunctionDAO dao = (SysFunctionDAO)ctx.getBean("sysFunctionDAO");
	
//	@Test
//	public void selectTest(){
//		List<SysFunction> list = dao.select();
//		System.out.println(list.toString());
//	}
	
	@SuppressWarnings("unused")
	@Test
	public void selectFunListByOpIdTest(){
		Map<String,String> map = new HashMap<String,String>();
		map.put("opId", "8465a36d7e10450193f5346597b6e8e1");
		List<String> list = dao.selectFunListByOpId(map);
		System.out.println("");
	}
	
}
