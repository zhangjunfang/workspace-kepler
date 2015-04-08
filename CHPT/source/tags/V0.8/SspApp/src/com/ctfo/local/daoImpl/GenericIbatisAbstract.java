package com.ctfo.local.daoImpl;

import java.io.Serializable;
import java.sql.SQLException;
import java.util.List;
import java.util.Map;

import org.springframework.orm.ibatis.SqlMapClientCallback;
import org.springframework.orm.ibatis.SqlMapClientTemplate;

import com.ctfo.local.dao.GenericIbatisDao;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.exception.CtfoExceptionLevel;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ibatis.sqlmap.client.SqlMapExecutor;


public abstract class GenericIbatisAbstract<T, PK extends Serializable> implements GenericIbatisDao<T, PK> {
	public static final String SQLID_INSERT = "insert";
	public static final String SQLID_UPDATE = "update";
	public static final String SQLID_UPDATE_PARAM = "updateParam";
	public static final String SQLID_DELETE = "delete";
	public static final String SQLID_MUTI_DELETE = "deleteMuti";
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
	
	public SqlMapClientTemplate getSqlMapClientTemplate() {
		return sqlMapClientTemplate;
	}

	public void setSqlMapClientTemplate(SqlMapClientTemplate sqlMapClientTemplate) {
		this.sqlMapClientTemplate = sqlMapClientTemplate;
	}
	

	public void insert(T entity) throws CtfoAppException {
		try {
			sqlMapClientTemplate.insert(sqlmapNamespace + "." + SQLID_INSERT, entity);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

	public int update(T entity) throws CtfoAppException {
		try {
			return sqlMapClientTemplate.update(sqlmapNamespace + "." + SQLID_UPDATE, entity);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}

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
	public int delete(PK primaryKey) throws CtfoAppException {

		try {
			int rows = sqlMapClientTemplate.delete(sqlmapNamespace + "." + SQLID_DELETE, primaryKey);
			sqlMapClientTemplate.getSqlMapClient().startBatch();
			return rows;
		} catch (SQLException e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}

	}
	
	public int deleteMuti(List<String> pkList) throws CtfoAppException {
		try {
			int rows = sqlMapClientTemplate.delete(sqlmapNamespace + "." + SQLID_MUTI_DELETE, pkList);
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

	public int count() throws CtfoAppException {
		try {
			Integer count = (Integer) sqlMapClientTemplate.queryForObject(sqlmapNamespace + "." + SQLID_COUNT);
			return count.intValue();
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}

	}



	@SuppressWarnings("unchecked")
	public List<T> select() throws CtfoAppException {
		try {
			return sqlMapClientTemplate.queryForList(sqlmapNamespace + "." + SQLID_SELECT);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}
	@SuppressWarnings("unchecked")
	public T selectPK(PK primaryKey) throws CtfoAppException {
		try {
			List<T> list = sqlMapClientTemplate.queryForList(sqlmapNamespace + "." + SQLID_SELECT_PK, primaryKey);
			T tt = null;
			if(list!=null&&list.size()>0){
				tt=list.get(0);
			}
			return tt;
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}
	
	public List<Map<String,Object>> selectPK(PK primaryKey,String s) throws CtfoAppException {
		try {
			List<Map<String,Object>> list = sqlMapClientTemplate.queryForList(sqlmapNamespace + "." + SQLID_SELECT_PK, primaryKey);
//			T tt = null;
//			if(list!=null&&list.size()>0){
//				tt=list.get(0);
//			}
			return list;
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}
	@SuppressWarnings("unchecked")
	public List<T> selectPKName(DynamicSqlParameter param) throws CtfoAppException {
		try {
			return sqlMapClientTemplate.queryForList(sqlmapNamespace + "." + SQLID_SELECT_PK_NAME);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}
	@SuppressWarnings("unchecked")
	public List<T> selectFk(DynamicSqlParameter param) throws CtfoAppException {
		try {
			return sqlMapClientTemplate.queryForList(sqlmapNamespace + "." + SQLID_SELECT_FK, param);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}
	@SuppressWarnings("unchecked")
	public List<T> select(DynamicSqlParameter param) throws CtfoAppException {
		try {
			return sqlMapClientTemplate.queryForList(sqlmapNamespace + "." + SQLID_SELECT_PARAM, param);
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		}
	}



	public int count(DynamicSqlParameter param) throws CtfoAppException {
		return this.countDynamic(param, SQLID_COUNT_PARAM);
	}
	
	public PaginationResult<T> selectPagination(DynamicSqlParameter param)
			throws CtfoAppException {
		return this.selectPaginationDynamic(param, SQLID_SELECT_PARAM_PAGE,
				SQLID_COUNT_PARAM);
	}

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

	@SuppressWarnings("unchecked")
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

	public void batchInsert(final List<T> list) throws CtfoAppException {
		try {
			SqlMapClientCallback callback = new SqlMapClientCallback() {
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
	
	@SuppressWarnings("rawtypes")
	public void batchInsert(final List<Map> list, final String statementName) throws CtfoAppException {
		try {
			SqlMapClientCallback callback = new SqlMapClientCallback() {
				public Object doInSqlMapClient(SqlMapExecutor executor) throws SQLException {
					executor.startBatch();
					int batch = 0;
					for (Map member : list) {
						executor.insert(sqlmapNamespace + "." + statementName, member);
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

	public void batchUpdate(final List<T> list) throws CtfoAppException {
		try {
			SqlMapClientCallback callback = new SqlMapClientCallback() {
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

	public void batchUpdateParam(final List<DynamicSqlParameter> list) throws CtfoAppException {
		try {
			SqlMapClientCallback callback = new SqlMapClientCallback() {
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

	public void batchDelete(final List<PK> list) throws CtfoAppException {
		try {
			SqlMapClientCallback callback = new SqlMapClientCallback() {
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
}
