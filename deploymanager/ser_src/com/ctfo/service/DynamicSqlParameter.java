package com.ctfo.service;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;



public class DynamicSqlParameter implements Serializable {

	private static final long serialVersionUID = 1L;

	/**
	 * 参数使用Like方式查询设置key与value{字段名：值}
	 */
	private Map<String, String> like;

	/**
	 * 参数使用==方式查询设置key与value{字段名：值}
	 */
	private Map<String, String> equal;
	
	/**
	 * 参数使用!=方式查询设置key与value{字段名：值}
	 */
	private Map<String, String> notequal;
	
	/**
	 * 参数使用以xxx开始的方式查询设置key与value{字段名：值}
	 */
	private Map<String, String> startwith;
	
	/**
	 * 参数使用以xxx结束方式查询设置key与value{字段名：值}
	 */
	private Map<String, String> endwith;
	
	/**
	 * 参数使用in方式查询ID{值}
	 */
	private Map<String, Object> inMap;

	
	/**
	 * 需要修改的参数列与值{字段名：值}
	 */
	private Map<String, Object> updateValue;

	/** 去除自身ID情况下使用 */
	private String noId;

	/** 开始页码 */
	private int page;

	/** 每页多少 */
	private int rows;
	
	/**共计多少页*/
	private int pagesize=1;

	/** 排序名称 */
	private String order;

	/** 排序desc/asc */
	private String sort;

	

	/**
	 * 
	 * @param equalKey
	 *            等于的条件
	 * @param equalIdValue
	 *            等于的值
	 * @param updateValueMap
	 *            需要修改的键值对
	 * @return
	 * @throws CtfoAppException
	 */
	public static DynamicSqlParameter getUpdateDynamicSqlParameter(String equalKey, String equalIdValue, Map<String, Object> updateValueMap) {

		DynamicSqlParameter parameter = null;
		if (null == equalKey || null == equalIdValue || null == updateValueMap || "".equals(equalKey) || "".equals(equalIdValue)) {
			return parameter;
		}

		parameter = new DynamicSqlParameter();
		Map<String, String> equalMap = new HashMap<String, String>();
		// Map<String, String> updateValueMap = new HashMap<String, String>();
		equalMap.put(equalKey, equalIdValue);
		// updateValueMap.put(updateKey, updateValue);
		parameter.setEqual(equalMap);
		parameter.setUpdateValue(updateValueMap);
		return parameter;
	}

	public String getNoId() {
		return noId;
	}

	public void setNoId(String noId) {
		this.noId = noId;
	}

	/**
	 * 开始的记录数
	 * 
	 * @return
	 */
	public int getStartNum() {
		return (page - 1) * rows;
	}

	/**
	 * 结束的记录数
	 * 
	 * @return
	 */
	public int getEndNum() {
		//页数＊每页行数
		return this.getStartNum() + pagesize*rows;
	}

	public int getPage() {
		return page;
	}

	public void setPage(int page) {
		this.page = page;
	}

	public int getRows() {
		return rows;
	}

	public void setRows(int rows) {
		this.rows = rows;
	}

	public int getPagesize() {
		return pagesize;
	}

	public void setPagesize(int pagesize) {
		this.pagesize = pagesize;
	}

	public String getOrder() {
		return order;
	}

	public void setOrder(String order) {
		// 更改为数据库相应的order名称
		this.order = order;
		//this.order = order.replaceAll("([A-Z])", "_$1").toUpperCase();
	}

	public String getSort() {
		return sort;
	}

	public void setSort(String sort) {
		this.sort = sort;
	}

	public Map<String, Object> getUpdateValue() {
		return updateValue;
	}

	public void setUpdateValue(Map<String, Object> updateValue) {
		this.updateValue = updateValue;
	}
	

	public Map<String, String> getLike() {
		return like;
	}

	public void setLike(Map<String, String> like) {
		this.like = like;
	}

	public Map<String, String> getEqual() {
		return equal;
	}

	public void setEqual(Map<String, String> equal) {
		this.equal = equal;
	}

	public Map<String, Object> getInMap() {
		return inMap;
	}

	public void setInMap(Map<String, Object> inMap) {
		this.inMap = inMap;
	}
	
	

	public Map<String, String> getNotequal() {
		return notequal;
	}

	public void setNotequal(Map<String, String> notequal) {
		this.notequal = notequal;
	}

	public Map<String, String> getStartwith() {
		return startwith;
	}

	public void setStartwith(Map<String, String> startwith) {
		this.startwith = startwith;
	}

	public Map<String, String> getEndwith() {
		return endwith;
	}

	public void setEndwith(Map<String, String> endwith) {
		this.endwith = endwith;
	}

	@Override
	public String toString(){
		StringBuffer buff = new StringBuffer();
		if(!(like == null||like.size()==0)) {buff.append("[包含条件:").append(like).append("]");}
		if(!(equal == null||equal.size()==0)) {buff.append("[等于条件:").append(equal).append("]");}
		if(!(notequal == null||notequal.size()==0)) {buff.append("[不等于条件:").append(notequal).append("]");}
		if(!(startwith == null||startwith.size()==0)) {buff.append("[以...条件开始:").append(startwith).append("]");}
		if(!(endwith == null||endwith.size()==0)) {buff.append("[以...条件结束:").append(endwith).append("]");}
		if(!(inMap == null||inMap.size()==0)) {buff.append("[在...中条件:").append(inMap).append("]");
		}
		return buff.toString();
	}


}