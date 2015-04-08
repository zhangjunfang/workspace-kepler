package com.ctfo.dataanalysisservice.dao;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import org.springframework.jdbc.core.JdbcTemplate;

public class DbBase {

	private JdbcTemplate jdbcTemplate;

	public void setJdbcTemplate(JdbcTemplate jdbcTemplate) {
		this.jdbcTemplate = jdbcTemplate;
	}

	private Connection getConnection() throws SQLException {
		return jdbcTemplate.getDataSource().getConnection();
	}

	private void executeUpdate(String sql) throws SQLException {
		Connection con = getConnection();
		con.setAutoCommit(false);
		Statement st = con.createStatement();
		st.executeUpdate(sql);
		con.commit();
	}

	private void executeBatch(String[] sqls) throws SQLException {
		Connection con = getConnection();
		con.setAutoCommit(false);
		Statement st = con.createStatement();
		for (String sql : sqls) {
			st.addBatch(sql);
		}
		st.executeBatch();
		con.commit();
	}

	public ResultSet find(String sql) throws SQLException {
		Connection con = getConnection();
		Statement st = con.createStatement();
		return st.executeQuery(sql);

	}

	public void update(String sql) throws SQLException {
		executeUpdate(sql);
	}

	public void insert(String sql) throws SQLException {
		executeUpdate(sql);
	}

	public void insertBatch(String[] sqls) throws SQLException {
		executeBatch(sqls);
	}

	public void updateBatch(String[] sqls) throws SQLException {
		executeBatch(sqls);
	}

}
