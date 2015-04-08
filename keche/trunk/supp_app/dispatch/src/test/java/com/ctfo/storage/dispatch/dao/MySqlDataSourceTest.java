/**
 * 
 */
package com.ctfo.storage.dispatch.dao;

import static org.junit.Assert.*;

import java.sql.SQLException;

import org.junit.Test;

import com.ctfo.storage.dispatch.dao.MySqlDataSource;

/**
 * @author zjhl
 *
 */
public class MySqlDataSourceTest {

	/**
	 * 测试	获取实例
	 */
	@Test
	public void testGetInstance() {
		MySqlDataSource msd = MySqlDataSource.getInstance();
		assertNotNull(msd);
	}

	/**
	 * 测试	初始化
	 */
	@Test
	public void testInit() {
		MySqlDataSource msd = MySqlDataSource.getInstance();
		try {
			msd.setDriver("com.mysql.jdbc.Driver");
			msd.setUrl("jdbc:mysql://192.168.100.52:3306/center");
			msd.setUsername("root");
			msd.setPassword("123456");
			msd.setMaxActive(10);
			msd.init();
			assertTrue(true);
		} catch (SQLException e) {
			fail("异常"); 
		}
	}
}
