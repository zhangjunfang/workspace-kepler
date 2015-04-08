package com.ctfo.local.dao;

import java.io.Serializable;
import java.util.List;
import java.util.Map;

import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;


/**
 * 
 * 功能：GenericDao DAO层泛型接口，定义基本的DAO功能 <br>
 * 描述：GenericDao DAO层泛型接口，定义基本的DAO功能,主键类，必须实现Serializable接口
 */
public abstract interface GenericIbatisDao<T, PK extends Serializable> {

	/**
	 * 插入一个实体（在数据库INSERT一条记录）
	 * 
	 * @param entity
	 *            实体对象
	 * @throws CtfoAppException
	 *             抛出数据库运行时异常
	 */
	public abstract void insert(T entity) throws CtfoAppException;

	/**
	 * 修改一个实体对象（UPDATE一条记录）
	 * 
	 * @param entity
	 *            实体对象
	 * @return 修改的对象个数，正常情况=1
	 * @throws CtfoAppException
	 *             抛出数据库运行时异常
	 */
	public abstract int update(T entity) throws CtfoAppException;

	/**
	 * 修改符合条件的记录
	 * <p>
	 * 此方法特别适合于一次性把多条记录的某些字段值设置为新值（定值）的情况，比如修改符合条件的记录的状态字段
	 * </p>
	 * <p>
	 * 此方法的另一个用途是把一条记录的个别字段的值修改为新值（定值），此时要把条件设置为该记录的主键
	 * </p>
	 * 
	 * @param param
	 *            用于产生SQL的参数值，包括WHERE条件、目标字段和新值等
	 * @return 修改的记录个数，用于判断修改是否成功
	 * @throws CtfoAppException
	 *             抛出数据库运行时异常
	 */
	public abstract int update(DynamicSqlParameter param) throws CtfoAppException;

	/**
	 * 按主键删除记录
	 * 
	 * @param primaryKey
	 *            主键对象
	 * @return 删除的对象个数，正常情况=1
	 * @throws CtfoAppException
	 *             抛出数据库运行时异常
	 */
	public abstract int delete(PK primaryKey) throws CtfoAppException;
	
	
	
	public int deleteMuti(List<String> pkList) throws CtfoAppException;

	/**
	 * 删除符合条件的记录
	 * <p>
	 * <strong>此方法一定要慎用，如果条件设置不当，可能会删除有用的记录！</strong>
	 * </p>
	 * 
	 * @param param
	 *            用于产生SQL的参数值，包括WHERE条件（其他参数内容不起作用）
	 * @return
	 */
	// public abstract int delete(DynamicSqlParameter param);

	/**
	 * 查询整表总记录数
	 * 
	 * @return 整表总记录数
	 */
	public abstract int count() throws CtfoAppException;

	/**
	 * 查询符合条件的记录数
	 * 
	 * @param param
	 *            查询条件参数，包括WHERE条件（其他参数内容不起作用）。此参数设置为null，则相当于count()
	 * @return
	 */
	public abstract int count(DynamicSqlParameter param) throws CtfoAppException;

	/**
	 * 查询符合条件的记录数
	 * 
	 * @param param
	 *            查询条件参数，包括WHERE条件（其他参数内容不起作用）。此参数设置为null，则相当于count()
	 * @param countSqlId
	 *            需要关联的ibatisXML文件中对应的sql
	 * @return
	 */
	public abstract int countDynamic(DynamicSqlParameter param, String countSqlId) throws CtfoAppException;

	/**
	 * 按主键取记录
	 * 
	 * @param primaryKey
	 *            主键值
	 * @return 记录实体对象，如果没有符合主键条件的记录，则返回null
	 */
	public abstract T selectPK(PK primaryKey) throws CtfoAppException;
	public abstract List<Map<String,Object>> selectPK(PK primaryKey,String s) throws CtfoAppException;
	/**
	 * 取符合条件的ID和Name集合
	 * 
	 * @param param
	 *            查询条件参数，包括WHERE条件、分页条件、排序条件
	 * @return 记录实体对象，返回主键与Name
	 */
	public abstract List<T> selectPKName(DynamicSqlParameter param) throws CtfoAppException;


	/**
	 * 取全部记录
	 * 
	 * @return 全部记录实体对象的List
	 */
	public abstract List<T> select() throws CtfoAppException;

	/**
	 * 按条件查询记录
	 * 
	 * @param param
	 *            查询条件参数，包括WHERE条件、分页条件、排序条件
	 * @return 符合条件记录的实体对象的List
	 */
	public abstract List<T> select(DynamicSqlParameter param) throws CtfoAppException;

	/**
	 * 按条件查询记录，并处理成分页结果
	 * 
	 * @param param
	 *            查询条件参数，包括WHERE条件、分页条件、排序条件
	 * @return PaginationResult对象，包括（符合条件的）总记录数、页实体对象List等
	 * @throws Exception
	 */
	public abstract PaginationResult<T> selectPagination(DynamicSqlParameter param) throws CtfoAppException;

	/**
	 * 按条件查询记录，并处理成分页结果
	 * 
	 * @param param
	 *            查询条件参数，包括WHERE条件、分页条件、排序条件
	 * @param dataSqlId
	 *            对应的根据param查询数据ibatis xml中的sql
	 * @param countSqlId
	 *            对应的根据param查询总数ibatis xml中的sql
	 * @return PaginationResult 对象，包括（符合条件的）总记录数、页实体对象List等
	 * @throws Exception
	 */
	public abstract PaginationResult<T> selectPaginationDynamic(DynamicSqlParameter param, String dataSqlId, String countSqlId) throws CtfoAppException;

	/**
	 * 按条件查询记录，并把有外键关联的字段的关联对象也处理出来
	 * <p>
	 * 此方法是为了避免1+N而设置的
	 * </p>
	 * 
	 * @param param
	 *            查询条件参数，包括WHERE条件、分页条件、排序条件<strong>（不包括外键关联表条件）</strong>
	 * @return 符合条件记录的实体对象的List
	 */
	public abstract List<T> selectFk(DynamicSqlParameter param) throws CtfoAppException;

	/**
	 * 按条件查询记录，并把有外键关联的字段的关联对象也处理出来，并处理成分页结果
	 * <p>
	 * 此方法是为了避免1+N而设置的
	 * </p>
	 * 
	 * @param param
	 *            查询条件参数，包括WHERE条件、分页条件、排序条件<strong>（不包括外键关联表条件）</strong>
	 * @return PaginationResult对象，包括（符合条件的）总记录数、页实体对象List等
	 */
	// public abstract PaginationResult<T> selectFkPagination(
	// DynamicSqlParameter param);

	/**
	 * 批量插入
	 * 
	 * @param list
	 */
	public abstract void batchInsert(final List<T> list) throws CtfoAppException;

	/**
	 * 批量修改
	 * 
	 * @param list
	 */
	public abstract void batchUpdate(final List<T> list) throws CtfoAppException;

	/**
	 * 批量修改部分参数
	 * 
	 * @param list
	 */
	public abstract void batchUpdateParam(final List<DynamicSqlParameter> list) throws CtfoAppException;

	/**
	 * 批量删除
	 * 
	 * @param list
	 */
	public abstract void batchDelete(final List<PK> list) throws CtfoAppException;
}
