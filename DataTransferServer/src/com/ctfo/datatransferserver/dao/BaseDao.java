package com.ctfo.datatransferserver.dao;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import com.ctfo.datatransferserver.connpool.OracleConnectionPool;

/**
 * 基础DAO
 * 
 * @author yangyi
 * 
 */
public class BaseDao {

	/**
	 * 根据条件查询
	 * 
	 * @param sql
	 * @param objs
	 * @param rowMapper
	 */
	protected void query(String sql, final Object[] objs, RowMapper<?> rowMapper) {
		Connection con = null;
		PreparedStatement preparedStatement = null;
		ResultSet rs = null;

		try {
			con = OracleConnectionPool.getConnection();
			preparedStatement = con.prepareStatement(sql);
			if (objs != null && objs.length > 0) {
				for (int i = 0, n = objs.length; i < n; i++) {
					preparedStatement.setObject(i + 1, objs[i]);
				}
			}
			rs = preparedStatement.executeQuery();
			while (rs.next()) {
				rowMapper.mapRow(rs);
			}

		} catch (SQLException e) {
			e.printStackTrace();
		} finally {
			closeConnection(rs, preparedStatement, con);

		}

	}

	/**
	 * 根据条件查询
	 * 
	 * @param sql
	 * @param objs
	 * @param rowMapper
	 */
	protected Object queryObject(String sql, final Object[] objs, RowMapper<?> rowMapper) {
		Connection con = null;
		PreparedStatement preparedStatement = null;
		ResultSet rs = null;
		Object o=null;
		try {
			con = OracleConnectionPool.getConnection();
			preparedStatement = con.prepareStatement(sql);
			if (objs != null && objs.length > 0) {
				for (int i = 0, n = objs.length; i < n; i++) {
					preparedStatement.setObject(i + 1, objs[i]);
				}
			}
			rs = preparedStatement.executeQuery();
			if (rs.next()) {
				o=rowMapper.mapRow(rs);
			}

		} catch (SQLException e) {
			e.printStackTrace();
		} finally {
			closeConnection(rs, preparedStatement, con);

		}
		return o;
	}
	/**
	 * 根据条件查询，返回list
	 * 
	 * @param sql
	 * @param objs
	 * @param rowMapper
	 * @return
	 */
	protected List<?> queryList(String sql, final Object[] objs,
			RowMapper<?> rowMapper) {
		Connection con = null;
		PreparedStatement preparedStatement = null;
		ResultSet rs = null;
		List<Object> list = new ArrayList<Object>();
		try {
			con = OracleConnectionPool.getConnection();
			preparedStatement = con.prepareStatement(sql);
			if (objs != null && objs.length > 0) {
				for (int i = 0, n = objs.length; i < n; i++) {
					preparedStatement.setObject(i + 1, objs[i]);
				}
			}
			rs = preparedStatement.executeQuery();
			while (rs.next()) {
				list.add(rowMapper.mapRow(rs));
			}

		} catch (SQLException e) {
			e.printStackTrace();
		} finally {
			closeConnection(rs, preparedStatement, con);

		}
		return list;
	}

	protected void closeConnection(ResultSet rs, Statement st, Connection con) {
		if (rs != null) {
			try {
				rs.close();
			} catch (SQLException exrs) {
			}
		}
		if (st != null) {
			try {
				st.close();
			} catch (SQLException exps) {
				exps.printStackTrace();
			}
		}
		if (con != null) {
			try {
				con.close();
			} catch (SQLException exconn) {
				exconn.printStackTrace();
			}
		}
	}
}
