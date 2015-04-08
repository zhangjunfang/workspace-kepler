package com.ctfo.sys.service;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.poi2.hssf.usermodel.HSSFWorkbook;
import org.junit.Test;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.ExcelExport;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.TbOrg;
import com.ctfo.sys.dao.TbOrgDAO;

public class ExcelExportTest {

	ApplicationContext ctx = new ClassPathXmlApplicationContext("com/ctfo/resource/applicationContext*.xml"); 
	TbOrgDAO dao = (TbOrgDAO)ctx.getBean("tbOrgDAO");
	
	@SuppressWarnings("unused")
	@Test
	public void ExportTest() throws FileNotFoundException, CtfoAppException, IOException{
	    Map<String, Object> equal=new HashMap<String,Object>();
		
	    equal.put("corpCity", "110104");
	    equal.put("corpProvince", "110000");
	    equal.put("entId", "1");
	    
		DynamicSqlParameter param = new DynamicSqlParameter();
		param.setPage(1);
		param.setPagesize(30);
		param.setEqual(equal);
		
		int counts = dao.count(param);
		PaginationResult<TbOrg> list = dao.selectPagination(param);
		Collection<TbOrg> orgList = list.getData();
		List<TbOrg> oList = new ArrayList<TbOrg>();
		for(TbOrg org : orgList){
			oList.add(org);
			oList.add(org);
			oList.add(org);
		}
		
		File f = new File("G:\\org.xls");
		ExcelExport xlsExport=new ExcelExport();
		HSSFWorkbook wb=xlsExport.genExcel(null, f, oList);
	
		OutputStream out=new FileOutputStream("G:"+File.separator+"test_2.xls");//文件本
		wb.write(out);
		System.out.println("0000000");

	}
	
	
}
