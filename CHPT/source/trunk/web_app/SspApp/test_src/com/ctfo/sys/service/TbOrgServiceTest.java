package com.ctfo.sys.service;

import java.util.HashMap;
import java.util.Map;

import org.junit.Test;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.TbOrg;
import com.ctfo.sys.dao.TbOrgDAO;

public class TbOrgServiceTest {
	
	ApplicationContext ctx = new ClassPathXmlApplicationContext("com/ctfo/resource/applicationContext*.xml"); 
	TbOrgDAO dao = (TbOrgDAO)ctx.getBean("tbOrgDAO");
	
	@Test
	public void selectTest(){
	    Map<String, Object> equal=new HashMap<String,Object>();
		
	    equal.put("corpCity", "corpCity");
	    equal.put("corpProvince", "c33");
	    
		DynamicSqlParameter param = new DynamicSqlParameter();
		param.setPage(1);
		param.setPagesize(1);
		param.setEqual(equal);
		
		@SuppressWarnings("unused")
		int counts = dao.count(param);
		@SuppressWarnings("unused")
		PaginationResult<TbOrg> list = dao.selectPagination(param);
	}
	
//	@Test
//	public void  insertTest(){
//		TbOrganization org = new TbOrganization();
//		TbOrgInfo inf = new TbOrgInfo();
//		
//		String id = GeneratorUUID.generateUUID();
//		
//		org.setCreateBy("createBy");
//		org.setCreateTime(DateUtil.dateToUtcTime(new Date()));
//		org.setEnableFlag("1");
//		org.setEntId(id);
//		org.setEntName("entName");
//		org.setEntState("1");
//		org.setEntType(new Short("1"));
//		org.setIscompany(new Short("1"));
//		org.setMemo("memo");
//		org.setParentId("-1");
//		org.setUpdateBy("updateBy");
//		org.setUpdateTime(DateUtil.dateToUtcTime(new Date()));
//		
//		inf.setBusinessLicense("businessLicense");
//		inf.setBusinessScope("businessScope");
//		inf.setCertificateOffice("certificateOffice");
//		inf.setCorpBoss(new BigDecimal(11));
//		inf.setCorpBusinessno("corpBusinessno");
//		inf.setCorpCity("corpCity");
//		inf.setCorpCode("corpCode");
//		inf.setCorpCountry("11111");
//		inf.setCorpEconomytype("corpEconomytype");
//		inf.setCorpLevel("corpLevel");
//		inf.setCorpPaystate(new Short("1"));
//		inf.setCorpPaytype(new Short("1"));
//		inf.setCorpProvince("c33");
//		inf.setCorpQuale("corpQuale");
//		inf.setCreateUtc(new Long("1"));
//		inf.setEntId(id);
//		inf.setIsdeteam("i");
//		inf.setLicenceNo("licenceNo");
//		inf.setLicenceWord("licenceWord");
//		inf.setOrgAddress("orgAddress");
//		inf.setOrgCfax("orgCfax");
//		inf.setOrgCmail("orgCmail");
//		inf.setOrgCname("orgCname");
//		inf.setOrgCno("orgCno");
//		inf.setOrgCphone("orgCphone");
//		inf.setOrgCzip("orgCzip");
//		inf.setOrgLogo("orgLogo");
//		inf.setOrgMem("orgMem");
//		inf.setOrgShortname("orgShortname");
//		inf.setSpecialId("specialId");
//		inf.setUrl("url");
//		
//		dao.insert(org, inf);
//	}
}
