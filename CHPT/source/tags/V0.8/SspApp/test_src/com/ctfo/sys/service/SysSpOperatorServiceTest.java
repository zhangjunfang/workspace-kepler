package com.ctfo.sys.service;

import org.junit.Test;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.sys.beans.SysSpOperator;
import com.ctfo.sys.dao.SysSpOperatorDAO;

public class SysSpOperatorServiceTest {

	ApplicationContext ctx = new ClassPathXmlApplicationContext("com/ctfo/resource/applicationContext*.xml"); 
	SysSpOperatorDAO dao = (SysSpOperatorDAO)ctx.getBean("sysSpOperatorDAO");
	
//	@Test
//	public void selectTest(){
//		Map<String, String> like=new HashMap<String,String>();
//	    Map<String, Object> equal=new HashMap<String,Object>();
//		
//	    like.put("opLoginname", "1");
////	    equal.put("opType", "3");
////	    equal.put("opStatus", "0");
//	    
//		DynamicSqlParameter param = new DynamicSqlParameter();
//		param.setPage(1);
//		param.setPagesize(1);
//		param.setLike(like);
////		param.setEqual(equal);
//		
//		int counts = dao.count(param);
//		PaginationResult<SysSpOperator> list = dao.selectPagination(param);
//	}
	
//	@Test
//	public void updateOperatorDeleteTest(){
//		Map<String,String> map = new HashMap<String,String>();
//		map.put("opId", "11opId");
//		dao.updateOperatorDelete(map);
//	}
	
//	@Test
//	public void updateOperatorRevokeTest(){
//		Map<String,String> map = new HashMap<String,String>();
//		map.put("opId", "11opId");
//		dao.updateOperatorRevoke(map);
//	}
	
//	@Test
//	public void selectPKTest(){
//		SysSpOperator op = dao.selectPK("1");
//		System.out.println(op.toString());
//	}
	
	@Test
	public void updateTest(){
		SysSpOperator sysSpOperator = new SysSpOperator();
		sysSpOperator.setOpId("fed67a82abe94017829935230e331d7f");
		sysSpOperator.setOpLoginname("ddddd");
		sysSpOperator.setRoleId("0833816f6da74829afae38b0f6295848");
		dao.update(sysSpOperator);
	}
	
//	@Test
//	public void  insertTest(){
//		SysSpOperator sysSpOperator = new SysSpOperator();
//		sysSpOperator.setCreateBy("11jiangdongqing");
//		sysSpOperator.setCreateTime(new Long("33423423523"));
//		sysSpOperator.setEnableFlag("4");
//		sysSpOperator.setEntId("11entId");
//		sysSpOperator.setImsi("11imsi");
//		sysSpOperator.setIsMember("7");
//		sysSpOperator.setIsMember("8");
//		sysSpOperator.setOpAddress("11opAddress");
//		sysSpOperator.setOpAuthcode("11opAuthcode");
//		sysSpOperator.setOpBirth(new Long("33423423523"));
//		sysSpOperator.setOpCity("11opCity");
//		sysSpOperator.setOpCountry("11opCountry");
//		sysSpOperator.setOpDuty("11opDuty");
//		sysSpOperator.setOpEmail("11opEmail");
//		sysSpOperator.setOpEndutc(new Long("33423423523"));
//		sysSpOperator.setOpFax("11opFax");
//		sysSpOperator.setOpId(GeneratorUUID.generateUUID());
//		sysSpOperator.setOpIdentityId("11opIdentityId");
//		sysSpOperator.setOpLoginname("11opLoginname");
//		sysSpOperator.setOpMem("11opMem");
//		sysSpOperator.setOpMobile("11opMobile");
//		sysSpOperator.setOpName("11opName");
//		sysSpOperator.setOpPass("11opPass");
//		sysSpOperator.setOpPhone("11opPhone");
//		sysSpOperator.setOpProvince("11opProvince");
//		sysSpOperator.setOpSex("2");
//		sysSpOperator.setOpStartutc(new Long("33423423523"));
//		sysSpOperator.setOpStatus("6");
//		sysSpOperator.setOpSuper("1");
//		sysSpOperator.setOpType("3");
//		sysSpOperator.setOpWorkid("11opWorkid");
//		sysSpOperator.setOpZip("11opZip");
//		sysSpOperator.setPhoto("11photo");
//		sysSpOperator.setSkinStyle("11skinStyle");
//		sysSpOperator.setUpdateBy("11updateBy");
//		sysSpOperator.setUpdateTime(new Long("33423423523"));
//		sysSpOperator.setRoleId("8465a36d7e10450193f5346597b6e8e1,6c6b3ca1c8fa4d8fbf3aa35a0361c9d5,1");
//		
//		dao.insert(sysSpOperator);
//		
//	}
	
}
