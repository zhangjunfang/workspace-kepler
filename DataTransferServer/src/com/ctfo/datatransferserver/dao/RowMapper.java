package com.ctfo.datatransferserver.dao;

import java.sql.ResultSet;
import java.sql.SQLException;

public interface RowMapper<E> {

	/**
	 * 将结果集中的一行转化为JAVA对象
	 * 
	 * @param rs
	 * @return
	 * @throws SQLException
	 */
	public E mapRow(ResultSet rs) throws SQLException;
}
