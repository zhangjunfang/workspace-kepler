package com.ctfo.sys.service;

import java.util.HashMap;
import java.util.Map;

import org.junit.Test;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.sys.dao.SysSpRoleDAO;

public class SysSpRoleServiceTest {

	ApplicationContext ctx = new ClassPathXmlApplicationContext("com/ctfo/resource/applicationContext*.xml"); 
	SysSpRoleDAO dao = (SysSpRoleDAO)ctx.getBean("sysSpRoleDAO");
	
//	@Test
//	public void selectTest(){
//		Map<String, String> like=new HashMap<String,String>();
//		
//	    like.put("roleName", "1");
//	    
//		DynamicSqlParameter param = new DynamicSqlParameter();
//		param.setPage(1);
//		param.setPagesize(1);
//		param.setLike(like);
//		
//		dao.count(param);
//		dao.selectPagination(param);
//	}
	
	@Test
	public void selectRoleByEntIdTest(){
		Map<String, String> like=new HashMap<String,String>();
		like.put("opId", "8465a36d7e10450193f5346597b6e8e1");
		dao.selectRoleByEntId(like);
	}
	
}
