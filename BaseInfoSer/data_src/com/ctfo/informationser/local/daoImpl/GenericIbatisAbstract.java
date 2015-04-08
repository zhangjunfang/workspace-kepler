package com.ctfo.informationser.local.daoImpl;

import java.io.Serializable;
import java.sql.SQLException;
import java.util.List;

import org.springframework.orm.ibatis.SqlMapClientCallback;
import org.springframework.orm.ibatis.SqlMapClientTemplate;

import com.ctfo.informationser.local.dao.GenericIbatisDao;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.exception.CtfoExceptionLevel;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ibatis.sqlmap.client.SqlMapExecutor;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： BsSer <br>
 * 功能：iBatis DAO层泛型基类，实现了基本的DAO功能 利用了Spring的DaoSupport功能 <br>
 * 描述：iBatis DAO层泛型基类，实现了基本的DAO功能 利用了Spring的DaoSupport功能, 主键类，必须实现Serializable接口 <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
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
 * <td>Sep 20, 2011</td>
 * <td>wangpeng</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author wangpeng
 * @since JDK1.6
 */
@SuppressWarnings("unchecked")
public abstract class GenericIbatisAbstract<T, PK extends Serializable> implements GenericIbatisDao<T, PK> {

	// sqlmap.xml定义文件中对应的sqlid
	// public static final String SQLID_DELETE_PARAM = "deleteParam";
	public static final String SQLID_INSERT = "insert";
	public static final String SQLID_UPDATE = "update";
	public static final String SQLID_UPDATE_PARAM = "updateParam";
	public static final String SQLID_DELETE = "delete";
	public static final String SQLID_SELECT = "select";
	public static final String SQLID_SELECT_PK = "selectPk";
	public static final String SQLID_SELECT_PARAM = "selectParam";
	public static final String SQLID_SELECT_PARAM_PAGE = "selectPageForParam";
	public static final String SQLID_SELECT_FK = "selectFk";
	public static final String SQLID_COUNT = "count";
	public static final String SQLID_COUNT_PARAM = "countParam";
	public static final String SQLID_SELECT_PK_NAME = "selectPkName";

	protected String sqlmapNamespace = "";

	private SqlMapClientTemplate sqlMapClientTemplate;

	/**
	 * sqlmapNamespace，对应sqlmap.xml中的命名空间
	 * 
	 * @return
	 */
	public String getSqlmapNamespace() {
		return sqlmapNamespace;
	}

	/**
	 * sqlmapNamespace的设置方法，可以用于spring注入
	 * 
	 * @param sqlmapNamespace
	 */
	public void setSqlmapNamespace(String sqlmapNamespace) {

		this.sqlmapNamespace = sqlmapNamespace;
	}

	@Override
	public void insert(T entity) throws CtfoAppException {
		try {
			sqlMapClientTemplate.insert(sqlmapNamespace + "." + SQLID_INSERT, entity);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	@Override
	public int update(T entity) throws CtfoAppException {
		try {
			return sqlMapClientTemplate.update(sqlmapNamespace + "." + SQLID_UPDATE, entity);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	@Override
	public int update(DynamicSqlParameter param) throws CtfoAppException {
		if (param == null || param.getUpdateValue() == null || ((param.getEqual() == null || param.getEqual().size() == 0) && (param.getInMap()==null
				&& param.getInMap().size() == 0))) {
			throw new CtfoAppException("参数设置错误:使用带参数的update必须设定条件的column和条件！",
					CtfoExceptionLevel.recoverError);
		}
		try {
			return sqlMapClientTemplate.update(sqlmapNamespace + "."
					+ SQLID_UPDATE_PARAM, param);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}
	@Override
	public int delete(PK primaryKey) throws CtfoAppException {

		try {
			int rows = sqlMapClientTemplate.delete(sqlmapNamespace + "." + SQLID_DELETE, primaryKey);
			sqlMapClientTemplate.getSqlMapClient().startBatch();
			return rows;
		} catch (SQLException e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}

	}

	// public int delete(DynamicSqlParameter param) {
	// int rows = sqlMapClientTemplate.delete(
	// sqlmapNamespace + "." + SQLID_DELETE_PARAM, param);
	// return rows;
	// }

	@Override
	public int count() throws CtfoAppException {
		try {
			Integer count = (Integer) sqlMapClientTemplate.queryForObject(sqlmapNamespace + "." + SQLID_COUNT);
			return count.intValue();
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}

	}




	@Override
	public List<T> select() throws CtfoAppException {
		try {
			return sqlMapClientTemplate.queryForList(sqlmapNamespace + "." + SQLID_SELECT);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	@Override
	public T selectPK(PK primaryKey) throws CtfoAppException {
		try {
			return (T) sqlMapClientTemplate.queryForObject(sqlmapNamespace + "." + SQLID_SELECT_PK, primaryKey);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	@Override
	public List<T> selectPKName(DynamicSqlParameter param) throws CtfoAppException {
		try {
			return sqlMapClientTemplate.queryForList(sqlmapNamespace + "." + SQLID_SELECT_PK_NAME);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	@Override
	public List<T> selectFk(DynamicSqlParameter param) throws CtfoAppException {
		try {
			return sqlMapClientTemplate.queryForList(sqlmapNamespace + "." + SQLID_SELECT_FK, param);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	@Override
	public List<T> select(DynamicSqlParameter param) throws CtfoAppException {
		try {
			return sqlMapClientTemplate.queryForList(sqlmapNamespace + "." + SQLID_SELECT_PARAM, param);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}



	@Override
	public int count(DynamicSqlParameter param) throws CtfoAppException {
		return this.countDynamic(param, SQLID_COUNT_PARAM);
	}
	
	@Override
	public PaginationResult<T> selectPagination(DynamicSqlParameter param)
			throws CtfoAppException {
		return this.selectPaginationDynamic(param, SQLID_SELECT_PARAM_PAGE,
				SQLID_COUNT_PARAM);
	}

	@Override
	public int countDynamic(DynamicSqlParameter param, String countSqlId)
			throws CtfoAppException {
		try {
			
			Integer count = (Integer) sqlMapClientTemplate.queryForObject(
					sqlmapNamespace + "." + countSqlId, param);
			return count.intValue();
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	@Override
	public PaginationResult<T> selectPaginationDynamic(
			DynamicSqlParameter param, String dataSqlId, String countSqlId)
			throws CtfoAppException {
		// if (param != null)
		// param.setDbDialect(this.dbDialect);
		if (param == null) {
			throw new CtfoAppException("分页参数为空，不能查询分页信息",
					CtfoExceptionLevel.systemError);
		}
		PaginationResult<T> result = new PaginationResult<T>();
		if (param.getStartNum() < 0 || param.getEndNum() <= 0
				|| param.getEndNum() <= param.getStartNum()) {
			param.setRows(30);
			param.setPage(1);
		}
		int count = countDynamic(param, countSqlId);
		result.setTotalCount(count);
		result.setStart(param.getStartNum());
		result.setPageSize(param.getRows());
		if (count > 0) {
			try {
				List<T> data = sqlMapClientTemplate.queryForList(
						sqlmapNamespace + "." + dataSqlId, param);
				result.setData(data);
			} catch (Exception e) {
				throw new CtfoAppException(e.fillInStackTrace());
			}
		}

		return result;
	}
	// public PaginationResult<T> selectFkPagination(DynamicSqlParameter param)
	// {
	// if (param != null)
	// param.setDbDialect(this.dbDialect);
	// PaginationResult<T> result = new PaginationResult<T>();
	// int count = count(param);
	// result.setTotalCount(count);
	// if (count > 0) {
	// List<T> data = sqlMapClientTemplate.queryForList(
	// sqlmapNamespace + "." + SQLID_SELECT_FK, param);
	// result.setData(data);
	// }
	//
	// return result;
	// }

	@Override
	public void batchInsert(final List<T> list) throws CtfoAppException {
		try {
			SqlMapClientCallback callback = new SqlMapClientCallback() {
				@Override
				public Object doInSqlMapClient(SqlMapExecutor executor) throws SQLException {
					executor.startBatch();
					int batch = 0;
					for (T member : list) {
						executor.insert(sqlmapNamespace + "." + SQLID_INSERT, member);
						batch++;
						// 每500条批量提交一次。
						if (batch == 500) {
							executor.executeBatch();
							batch = 0;
						}
					}
					executor.executeBatch();
					return null;
				}
			};
			this.sqlMapClientTemplate.execute(callback);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	@Override
	public void batchUpdate(final List<T> list) throws CtfoAppException {
		try {
			SqlMapClientCallback callback = new SqlMapClientCallback() {
				@Override
				public Object doInSqlMapClient(SqlMapExecutor executor) throws SQLException {
					executor.startBatch();
					int batch = 0;
					for (T member : list) {
						executor.update(sqlmapNamespace + "." + SQLID_UPDATE, member);
						batch++;
						// 每500条批量提交一次。
						if (batch == 500) {
							executor.executeBatch();
							batch = 0;
						}
					}
					executor.executeBatch();
					return null;
				}
			};
			this.sqlMapClientTemplate.execute(callback);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	@Override
	public void batchUpdateParam(final List<DynamicSqlParameter> list) throws CtfoAppException {
		try {
			SqlMapClientCallback callback = new SqlMapClientCallback() {
				@Override
				public Object doInSqlMapClient(SqlMapExecutor executor) throws SQLException {
					executor.startBatch();
					int batch = 0;
					for (DynamicSqlParameter member : list) {
						executor.update(sqlmapNamespace + "." + SQLID_UPDATE_PARAM, member);
						batch++;
						// 每500条批量提交一次。
						if (batch == 500) {
							executor.executeBatch();
							batch = 0;
						}
					}
					executor.executeBatch();
					return null;
				}
			};
			this.sqlMapClientTemplate.execute(callback);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	@Override
	public void batchDelete(final List<PK> list) throws CtfoAppException {
		try {
			SqlMapClientCallback callback = new SqlMapClientCallback() {
				@Override
				public Object doInSqlMapClient(SqlMapExecutor executor) throws SQLException {
					executor.startBatch();
					int batch = 0;
					for (PK member : list) {
						executor.delete(sqlmapNamespace + "." + SQLID_DELETE, member);
						batch++;
						// 每500条批量提交一次。
						if (batch == 500) {
							executor.executeBatch();
							batch = 0;
						}
					}
					executor.executeBatch();
					return null;
				}
			};
			this.sqlMapClientTemplate.execute(callback);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	public SqlMapClientTemplate getSqlMapClientTemplate() {
		return sqlMapClientTemplate;
	}

	public void setSqlMapClientTemplate(SqlMapClientTemplate sqlMapClientTemplate) {
		this.sqlMapClientTemplate = sqlMapClientTemplate;
	}
}
