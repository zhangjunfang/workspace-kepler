
package com.ctfo.storage.dispatch.service;

import java.sql.SQLException;
import java.util.Date;

import org.junit.Test;

import com.ctfo.storage.dispatch.dao.MySqlDataSource;

/**
 * MySqlServiceTest
 * 
 * 
 * @author huangjincheng
 * 2014-5-15下午01:59:18
 * 
 */
public class MySqlServiceTest {

	/**
	 * 
	 * @throws SQLException 
	 */
	@Test
	public void testSave() throws SQLException {
		MySqlDataSource mds = MySqlDataSource.getInstance();
		mds.setDriver("com.mysql.jdbc.Driver");
		mds.setUrl("jdbc:mysql://192.168.2.111:3306/CENTER");
		mds.setUsername("root");
		mds.setPassword("123456");
		mds.setMaxActive(10);
		mds.init();
	/*	TbDvrSer tds = new TbDvrSer();
		tds.setCenterGoalIp("123");
		tds.setDvrSerId(666l);
		tds.setDvrSerCity("0");*/
		
		
//		TbTest tds2 = new TbTest();
//		tds2.setTest_id(987654321 + l);
//		tds2.setTest_name("qqq");
//		tds2.setTest_type(3);
		
//		List<Object> list  = new ArrayList<Object>();
		for(int i=0;i<500;i++){
//			TbTest tds1 = new TbTest();
//			long l = System.nanoTime();
//			tds1.setTest_id(l +i);
//			tds1.setTest_name("jjj");
//			tds1.setTest_type(2);
//			list.add(tds1);
//			list.add(tds2);
		}
		
//		MySqlService msService = new MySqlService();
		Date s1 = new Date();
//		msService.tbDvr3GSave(list);
		Date e1 = new Date();
		System.out.println("耗时:"+(e1.getTime()-s1.getTime()));
		
/*		Date s2 = new Date();
		for(int i=0;i<1;i++){
			msService.testSave("", tds1);
			msService.testSave("", tds2);
		}
		Date e2 = new Date();
		System.out.println("耗时:"+(e2.getTime()-s2.getTime()));*/
	}

}
