package com.ctfo.dataanalysisservice;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import org.springframework.jdbc.core.JdbcTemplate;



public class TestDb {

	
/*
	private String dbdriver;
	
	private String dburl;
	
	private String username;
	
	private String password;
	

	private String selectsql;


	public String getDbdriver() {
		return dbdriver;
	}


	public void setDbdriver(String dbdriver) {
		this.dbdriver = dbdriver;
	}


	public String getDburl() {
		return dburl;
	}


	public void setDburl(String dburl) {
		this.dburl = dburl;
	}


	public String getUsername() {
		return username;
	}


	public void setUsername(String username) {
		this.username = username;
	}


	public String getPassword() {
		return password;
	}


	public void setPassword(String password) {
		this.password = password;
	}


	public String getSelectsql() {
		return selectsql;
	}


	public void setSelectsql(String selectsql) {
		this.selectsql = selectsql;
	}
	
	
	private ApplicationContext applicationContext = null;*/
	
/*	public void doit(){
		
          applicationContext = SpringBUtils.getApplicationContext();
		
          TestDb  test = (TestDb)applicationContext.getBean("testdb");
          
          test.select();
	}*/
	
	
	
	
	private JdbcTemplate jdbcTemplate;
	
	

	    
	    private Connection getConnection() throws SQLException{
	    	return jdbcTemplate.getDataSource().getConnection();
	    }
	    
		private void executeUpdate(String sql)  throws SQLException{
			Connection con= getConnection();
			 con.setAutoCommit(false);
			 Statement st=con.createStatement();
			 st.executeUpdate(sql);
	    	 con.commit();
		}
		
		private void executeBatch(String[] sqls)  throws SQLException{
			Connection con= getConnection();
			 con.setAutoCommit(false);
			 Statement st=con.createStatement();
			 for(String sql :sqls){
			 st.addBatch(sql);
			 }
			 st.executeBatch();
	    	 con.commit();
		}
	    
	    public ResultSet find(String sql) throws SQLException{
			Connection con= getConnection();
			 Statement st=con.createStatement();
			 return st.executeQuery(sql);
	    	
		}
	    
		public void update(String sql) throws SQLException{
			executeUpdate(sql);
		}
		
		public void insert(String sql) throws SQLException{
			executeUpdate(sql);
		}
		
		public void insertBatch(String[] sqls)throws SQLException{
			executeBatch(sqls);
		}
		
		public void updateBatch(String[] sqls)throws SQLException{
			executeBatch(sqls);
		}
	
}
