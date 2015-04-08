package com.ctfo.storage.service;

import java.io.InputStream;
import java.lang.reflect.Method;
import java.math.BigDecimal;
import java.sql.Connection;
import java.sql.Date;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.dao.MySqlDataSource;
import com.ctfo.storage.parse.ExceptionDataListenHandler;
import com.ctfo.storage.parse.ExceptionDataThread;
import com.ctfo.storage.util.ConfigLoader;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： MySql数据接口<br>
 * 描述： MySql数据接口<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-11-3</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class BaseService {

	private static final Logger logger = LoggerFactory.getLogger(BaseService.class);

	public Connection conn = null;

	private PreparedStatement savePreparedStatement = null;

	private PreparedStatement updatePreparedStatement = null;

	private PreparedStatement deletePreparedStatement = null;

	private int batchSize = Integer.valueOf(ConfigLoader.commitParamMap.get("commitBatchCount"));

	/**
	 * 批量添加
	 * 
	 * @param data
	 */
	public void addBatch(List<Object> data) {
		try {
			String tableName = data.get(0).getClass().getSimpleName().replaceAll("[0-9A-Z]", "_$0");
			// SQL语句,insert into table name (
			String sql = "INSERT INTO " + tableName.substring(1) + "(";

			// 获得带有字符串get的所有方法的对象
			List<Method> list = this.matchPojoMethods(data.get(0), "get");

			Iterator<Method> iter = list.iterator();

			// 拼接字段顺序 insert into table name(id,name,email,
			while (iter.hasNext()) {
				Method method = iter.next();
				String column = method.getName().substring(3).replaceAll("[A-Z]", "_$0");
				sql += column.substring(1) + ",";
			}

			// 去掉最后一个,符号insert insert into table name(id,name,email) values(
			sql = sql.substring(0, sql.lastIndexOf(",")) + ") VALUES(";

			// 拼装预编译SQL语句insert insert into table name(id,name,email) values(?,?,?,
			for (int j = 0; j < list.size(); j++) {
				sql += "?,";
			}

			// 去掉SQL语句最后一个,符号insert insert into table name(id,name,email) values(?,?,?);
			sql = sql.substring(0, sql.lastIndexOf(",")) + ")";

			// 到此SQL语句拼接完成,打印SQL语句
			System.out.println(sql);

			conn = MySqlDataSource.getConnection();
			savePreparedStatement = conn.prepareStatement(sql);
			conn.setAutoCommit(false);
			int index = 0;
			for (Object model : data) {
				index++;
				int i = 0;
				// 把指向迭代器最后一行的指针移到第一行.
				iter = list.iterator();
				while (iter.hasNext()) {
					Method method = iter.next();
					// 此初判断返回值的类型,因为存入数据库时有的字段值格式需要改变,比如String,SQL语句是'"+abc+"'
					if (method.getReturnType().getSimpleName().indexOf("String") != -1) {
						savePreparedStatement.setString(++i, this.getString(method, model));
					} else if (method.getReturnType().getSimpleName().indexOf("Date") != -1) {
						savePreparedStatement.setDate(++i, this.getDate(method, model));
					} else if (method.getReturnType().getSimpleName().indexOf("InputStream") != -1) {
						savePreparedStatement.setAsciiStream(++i, this.getBlob(method, model), 1440);
					} else if (method.getReturnType().getSimpleName().indexOf("Long") != -1) {
						savePreparedStatement.setLong(++i, this.getLong(method, model));
					} else if (method.getReturnType().getSimpleName().indexOf("Double") != -1) {
						savePreparedStatement.setDouble(++i, this.getDouble(method, model));
					} else if (method.getReturnType().getSimpleName().indexOf("BigDecimal") != -1) {
						savePreparedStatement.setBigDecimal(++i, this.getBigDecimal(method, model));
					} else {
						savePreparedStatement.setInt(++i, this.getInt(method, model));
					}
				}
				savePreparedStatement.addBatch();
				if (index == batchSize) {
					savePreparedStatement.executeBatch();
					savePreparedStatement.clearBatch();
					index = 0;
				}
			}
			if (index != 0) {
				savePreparedStatement.executeBatch();
			}
			conn.commit();
		} catch (SQLException e) {
			ExceptionDataListenHandler.putAddQueue(data);
			logger.error(e.getMessage(), e);
		} catch (Exception e) {
			ExceptionDataListenHandler.putAddQueue(data);
			logger.error(e.getMessage(), e);
		} finally {
			try {
				if (savePreparedStatement != null) {
					savePreparedStatement.close();
				}
				if (conn != null) {
					conn.close();
				}

			} catch (SQLException e2) {
				logger.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	/**
	 * 批量修改(逻辑删除)
	 * 
	 * @param data
	 */
	public void updateBatch(List<Object> data) {
		try {
			String tableName = data.get(0).getClass().getSimpleName().replaceAll("[0-9A-Z]", "_$0"); // 表命
			String beanIdName = ConfigLoader.protocolMap.get(data.get(0).getClass().getSimpleName()); // 主键ID
			String sql = "UPDATE " + tableName.substring(1) + " SET enable_flag = 0";

			// 获得该类所有get方法对象集合
			List<Method> list = this.matchPojoMethods(data.get(0), "get");

			Iterator<Method> iter = list.iterator();

			// 把迭代指针移到第一位
			iter = list.iterator();

			// 添加条件
			beanIdName = beanIdName.substring(0, beanIdName.indexOf("#"));
			sql += " WHERE " + beanIdName.replaceAll("[A-Z]", "_$0").substring(1) + " = ?";

			// SQL拼接完成,打印SQL语句
			System.out.println(sql);

			conn = MySqlDataSource.getConnection();
			conn.setAutoCommit(false);
			updatePreparedStatement = this.conn.prepareStatement(sql);
			int index = 0;
			for (Object model : data) {
				index++;
				int i = 1;
				// 把指向迭代器最后一行的指针移到第一行.
				iter = list.iterator();
				boolean flag = true;
				while (flag) {
					Method method = iter.next();
					// 此初判断返回值的类型,因为存入数据库时有的字段值格式需要改变,比如String,SQL语句是'"+abc+"'
					if (method.getName().substring(3).equals(beanIdName)) {
						updatePreparedStatement.setString(i, this.getString(method, model));
						flag = false;
					}
				}
				updatePreparedStatement.addBatch();
				if (index == batchSize) {
					updatePreparedStatement.executeBatch();
					updatePreparedStatement.clearBatch();
					index = 0;
				}
			}
			if (index != 0) {
				// 执行SQL语句
				updatePreparedStatement.executeBatch();
			}
			conn.commit();
		} catch (SQLException e) {
			ExceptionDataListenHandler.putUpdateQueue(data);
			logger.error(e.getMessage(), e);
		} catch (Exception e) {
			ExceptionDataListenHandler.putUpdateQueue(data);
			logger.error(e.getMessage(), e);
		} finally {
			try {
				if (updatePreparedStatement != null) {
					updatePreparedStatement.close();
				}
				if (conn != null) {
					conn.close();
				}

			} catch (SQLException e2) {
				logger.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	/**
	 * 批量删除
	 * 
	 * @param data
	 */
	public void deleteBatch(List<Object> data) {
		try {
			String tableName = data.get(0).getClass().getSimpleName().replaceAll("[0-9A-Z]", "_$0"); // 表命
			String beanIdName = ConfigLoader.protocolMap.get(data.get(0).getClass().getSimpleName()); // 主键ID
			String sql = "DELETE FROM " + tableName.substring(1);

			// 获得该类所有get方法对象集合
			List<Method> list = this.matchPojoMethods(data.get(0), "get");

			Iterator<Method> iter = list.iterator();

			// 把迭代指针移到第一位
			iter = list.iterator();

			// 添加条件
			String id = beanIdName.substring(0, beanIdName.indexOf("#"));
			sql += " WHERE " + id.replaceAll("[A-Z]", "_$0").substring(1) + " = ?";

			// SQL拼接完成,打印SQL语句
			System.out.println(sql);

			conn = MySqlDataSource.getConnection();
			conn.setAutoCommit(false);
			deletePreparedStatement = this.conn.prepareStatement(sql);
			int index = 0;
			for (Object model : data) {
				index++;
				int i = 1;
				// 把指向迭代器最后一行的指针移到第一行.
				iter = list.iterator();
				boolean flag = true;
				while (flag) {
					Method method = iter.next();
					// 此初判断返回值的类型,因为存入数据库时有的字段值格式需要改变,比如String,SQL语句是'"+abc+"'
					if (method.getName().substring(3).equals(id)) {
						deletePreparedStatement.setString(i, this.getString(method, model));
						flag = false;
					}
				}
				deletePreparedStatement.addBatch();
				if (index == batchSize) {
					deletePreparedStatement.executeBatch();
					deletePreparedStatement.clearBatch();
					index = 0;
				}
			}
			if (index != 0) {
				// 执行SQL语句
				deletePreparedStatement.executeBatch();
			}
			conn.commit();
		} catch (SQLException e) {
			ExceptionDataListenHandler.putDeleteQueue(data);
			logger.error(e.getMessage(), e);
		} catch (Exception e) {
			ExceptionDataListenHandler.putDeleteQueue(data);
			logger.error(e.getMessage(), e);
		} finally {
			try {
				if (deletePreparedStatement != null) {
					deletePreparedStatement.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	/**
	 * 添加
	 * 
	 * @param data
	 */
	public void add(Object o) {
		try {
			String tableName = o.getClass().getSimpleName().replaceAll("[0-9A-Z]", "_$0");
			// SQL语句,insert into table name (
			String sql = "INSERT INTO " + tableName.substring(1) + "(";

			// 获得带有字符串get的所有方法的对象
			List<Method> list = this.matchPojoMethods(o, "get");

			Iterator<Method> iter = list.iterator();

			// 拼接字段顺序 insert into table name(id,name,email,
			while (iter.hasNext()) {
				Method method = iter.next();
				String column = method.getName().substring(3).replaceAll("[A-Z]", "_$0");
				sql += column.substring(1) + ",";
			}

			// 去掉最后一个,符号insert insert into table name(id,name,email) values(
			sql = sql.substring(0, sql.lastIndexOf(",")) + ") VALUES(";

			// 拼装预编译SQL语句insert insert into table name(id,name,email) values(?,?,?,
			for (int j = 0; j < list.size(); j++) {
				sql += "?,";
			}

			// 去掉SQL语句最后一个,符号insert insert into table name(id,name,email) values(?,?,?);
			sql = sql.substring(0, sql.lastIndexOf(",")) + ")";

			// 到此SQL语句拼接完成,打印SQL语句
			System.out.println(sql);

			conn = MySqlDataSource.getConnection();
			savePreparedStatement = conn.prepareStatement(sql);

			int i = 0;
			// 把指向迭代器最后一行的指针移到第一行.
			iter = list.iterator();
			while (iter.hasNext()) {
				Method method = iter.next();
				// 此初判断返回值的类型,因为存入数据库时有的字段值格式需要改变,比如String,SQL语句是'"+abc+"'
				if (method.getReturnType().getSimpleName().indexOf("String") != -1) {
					savePreparedStatement.setString(++i, this.getString(method, o));
				} else if (method.getReturnType().getSimpleName().indexOf("Date") != -1) {
					savePreparedStatement.setDate(++i, this.getDate(method, o));
				} else if (method.getReturnType().getSimpleName().indexOf("InputStream") != -1) {
					savePreparedStatement.setAsciiStream(++i, this.getBlob(method, o), 1440);
				} else if (method.getReturnType().getSimpleName().indexOf("Long") != -1) {
					savePreparedStatement.setLong(++i, this.getLong(method, o));
				} else if (method.getReturnType().getSimpleName().indexOf("Double") != -1) {
					savePreparedStatement.setDouble(++i, this.getDouble(method, o));
				} else if (method.getReturnType().getSimpleName().indexOf("BigDecimal") != -1) {
					savePreparedStatement.setBigDecimal(++i, this.getBigDecimal(method, o));
				} else {
					savePreparedStatement.setInt(++i, this.getInt(method, o));
				}
			}
			savePreparedStatement.executeUpdate();
		} catch (SQLException e) {
			ExceptionDataThread.put(o);
			logger.error(e.getMessage(), e);
		} catch (Exception e) {
			ExceptionDataThread.put(o);
			logger.error(e.getMessage(), e);
		} finally {
			try {
				if (savePreparedStatement != null) {
					savePreparedStatement.close();
				}
				if (conn != null) {
					conn.close();
				}

			} catch (SQLException e2) {
				logger.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	/**
	 * 删除
	 * 
	 * @param data
	 */
	public void delete(Object data) {
		try {
			String tableName = data.getClass().getSimpleName().replaceAll("[0-9A-Z]", "_$0"); // 表命
			String beanIdName = ConfigLoader.protocolMap.get(data.getClass().getSimpleName()); // 主键ID
			String sql = "DELETE FROM " + tableName.substring(1);

			// 获得该类所有get方法对象集合
			List<Method> list = this.matchPojoMethods(data, "get");

			Iterator<Method> iter = list.iterator();

			// 把迭代指针移到第一位
			iter = list.iterator();

			// 添加条件
			String id = beanIdName.substring(0, beanIdName.indexOf("#"));
			sql += " WHERE " + id.replaceAll("[A-Z]", "_$0").substring(1) + " = ?";

			// SQL拼接完成,打印SQL语句
			System.out.println(sql);

			conn = MySqlDataSource.getConnection();
			conn.setAutoCommit(false);
			deletePreparedStatement = this.conn.prepareStatement(sql);
			int i = 1;
			// 把指向迭代器最后一行的指针移到第一行.
			iter = list.iterator();
			boolean flag = true;
			while (flag) {
				Method method = iter.next();
				// 此初判断返回值的类型,因为存入数据库时有的字段值格式需要改变,比如String,SQL语句是'"+abc+"'
				if (method.getName().substring(3).equals(id)) {
					deletePreparedStatement.setString(i, this.getString(method, data));
					flag = false;
				}
			}
			// 执行SQL语句
			deletePreparedStatement.executeUpdate();
			conn.commit();
		} catch (SQLException e) {
			ExceptionDataThread.put(data);
			logger.error(e.getMessage(), e);
		} catch (Exception e) {
			ExceptionDataThread.put(data);
			logger.error(e.getMessage(), e);
		} finally {
			try {
				if (deletePreparedStatement != null) {
					deletePreparedStatement.close();
				}
				if (conn != null) {
					conn.close();
				}
			} catch (SQLException e2) {
				logger.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	/**
	 * 修改(逻辑删除)
	 * 
	 * @param data
	 */
	public void update(Object data) {
		try {
			String tableName = data.getClass().getSimpleName().replaceAll("[0-9A-Z]", "_$0"); // 表命
			String beanIdName = ConfigLoader.protocolMap.get(data.getClass().getSimpleName()); // 主键ID
			String sql = "UPDATE " + tableName.substring(1) + " SET enable_flag = 0";

			// 获得该类所有get方法对象集合
			List<Method> list = this.matchPojoMethods(data, "get");

			Iterator<Method> iter = list.iterator();

			// 把迭代指针移到第一位
			iter = list.iterator();

			// 添加条件
			beanIdName = beanIdName.substring(0, beanIdName.indexOf("#"));
			sql += " WHERE " + beanIdName.replaceAll("[A-Z]", "_$0").substring(1) + " = ?";

			// SQL拼接完成,打印SQL语句
			System.out.println(sql);

			conn = MySqlDataSource.getConnection();
			conn.setAutoCommit(false);
			updatePreparedStatement = this.conn.prepareStatement(sql);

			int i = 1;
			// 把指向迭代器最后一行的指针移到第一行.
			iter = list.iterator();
			boolean flag = true;
			while (flag) {
				Method method = iter.next();
				// 此初判断返回值的类型,因为存入数据库时有的字段值格式需要改变,比如String,SQL语句是'"+abc+"'
				if (method.getName().substring(3).equals(beanIdName)) {
					updatePreparedStatement.setString(i, this.getString(method, data));
					flag = false;
				}
			}
			// 执行SQL语句
			updatePreparedStatement.executeUpdate();
			conn.commit();
		} catch (SQLException e) {
			ExceptionDataThread.put(data);
			logger.error(e.getMessage(), e);
		} catch (Exception e) {
			ExceptionDataThread.put(data);
			logger.error(e.getMessage(), e);
		} finally {
			try {
				if (updatePreparedStatement != null) {
					updatePreparedStatement.close();
				}
				if (conn != null) {
					conn.close();
				}

			} catch (SQLException e2) {
				logger.error("关闭资源异常:" + e2.getMessage(), e2);
			}
		}
	}

	/**
	 * 过滤当前Pojo类所有带传入字符串的Method对象,返回List集合.
	 * 
	 * @param entity
	 * @param methodName
	 * @return
	 */
	private List<Method> matchPojoMethods(Object entity, String methodName) {
		// 获得当前Pojo所有方法对象
		Method[] methods = entity.getClass().getDeclaredMethods();

		// List容器存放所有带get字符串的Method对象
		List<Method> list = new ArrayList<Method>();

		// 过滤当前Pojo类所有带get字符串的Method对象,存入List容器
		for (int index = 0; index < methods.length; index++) {
			if (methods[index].getName().indexOf(methodName) != -1 && methods[index].getName().indexOf(methodName) == 0) {
				list.add(methods[index]);
			}
		}
		return list;
	}

	/**
	 * 方法返回类型为int或Integer类型时,返回的SQL语句值.对应get
	 */
	public Integer getInt(Method method, Object entity) throws Exception {
		Object o = method.invoke(entity, new Object[] {});
		return o != null ? (Integer) o : 0;
	}

	/**
	 * 方法返回类型为Long类型时,返回的SQL语句值.对应get
	 */
	public Long getLong(Method method, Object entity) throws Exception {
		Object o = method.invoke(entity, new Object[] {});
		return o != null ? (Long) o : 0;
	}

	/**
	 * 方法返回类型为String时,返回的SQL语句拼装值.比如'abc',对应get
	 */
	public String getString(Method method, Object entity) throws Exception {
		return (String) method.invoke(entity, new Object[] {});
	}

	/**
	 * 方法返回类型为Blob时,返回的SQL语句拼装值.对应get
	 */
	public InputStream getBlob(Method method, Object entity) throws Exception {
		return (InputStream) method.invoke(entity, new Object[] {});
	}

	/**
	 * 方法返回类型为Date时,返回的SQL语句拼装值,对应get
	 */
	public Date getDate(Method method, Object entity) throws Exception {
		return (Date) method.invoke(entity, new Object[] {});
	}

	/**
	 * 方法返回类型为Double类型时,返回的SQL语句值.对应get
	 */
	public Double getDouble(Method method, Object entity) throws Exception {
		Object o = method.invoke(entity, new Object[] {});
		return o != null ? (Double) o : 0;
	}

	/**
	 * 方法返回类型为BigDecimal类型时,返回的SQL语句值.对应get
	 */
	public BigDecimal getBigDecimal(Method method, Object entity) throws Exception {
		Object o = method.invoke(entity, new Object[] {});
		return o != null ? (BigDecimal) o : new BigDecimal(0);
	}
}
