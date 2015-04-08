package com.ctfo.advice.test;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Map;

import com.ctfo.advice.dao.OracleDataSource;
import com.ctfo.advice.util.ConfigLoader;

public class OracleTest {
	private Connection conn = null;
	private ResultSet rs = null;
	private Map<String, String> sqlMap;
	private PreparedStatement statement = null;
	
	public void selectOracle() throws SQLException{
		conn = OracleDataSource.getConnection();
		statement = conn.prepareStatement("SELECT * FROM TB_VEHICLE");
		rs = statement.executeQuery();
		if(rs.next()){
			System.out.println(rs.getObject("vid"));
			System.out.println(rs.getObject("VID"));
		}
		//System.out.println(rs.next());
	}

	public static void main(String[] args) throws Exception {
		ConfigLoader.init("");
		OracleTest ot = new OracleTest();
		ot.selectOracle();
	}
}
