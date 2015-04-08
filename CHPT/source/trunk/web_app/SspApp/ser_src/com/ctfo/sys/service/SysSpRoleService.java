package com.ctfo.sys.service;

import java.util.List;
import java.util.Map;

import com.ctfo.export.RemoteJavaService;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.SysSpRole;

public interface SysSpRoleService extends RemoteJavaService {
	/**
	 * 查询角色列表
	 * @return
	 */
	public List<SysSpRole> queryRoleList();
	/**
	 * 
	 * @description:分页时获取记录总数量
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日上午10:00:41
	 * @modifyInformation：
	 */
	public int count(DynamicSqlParameter param);
	
	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日上午10:01:04
	 * @modifyInformation：
	 */
	public PaginationResult<SysSpRole> selectPagination(DynamicSqlParameter param);
	
	/**
	 * 
	 * @description:查询所有角色对象，用户分配角色权限时
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月4日上午10:58:35
	 * @modifyInformation：
	 */
	public List<SysSpRole> select(DynamicSqlParameter param);
	
	/**
	 * 
	 * @description:角色名称是否存在
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日下午3:02:12
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public boolean isExistRoleName(Map map);
	
	/**
	 * 
	 * @description:角色管理-添加
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日下午3:24:23
	 * @modifyInformation：
	 */
	public void insert(SysSpRole sysSpRole);
	
	/**
	 * 
	 * @description:角色管理-编辑
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日下午3:10:51
	 * @modifyInformation：
	 */
	public int update(SysSpRole sysSpRole);
	
	/**
	 * 
	 * @description:根据主键获取角色对象
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日上午9:43:54
	 * @modifyInformation：
	 */
	public SysSpRole selectPK(String tbId);
	
	/**
	 * 
	 * @description:新建用户时，角色多选下拉数据
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月9日下午1:47:22
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<String> selectRoleByEntId(Map map);
	
	/**
	 * 
	 * @description:角色管理-删除
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月17日下午2:53:12
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map) throws CtfoAppException;
	
	@SuppressWarnings("rawtypes")
	public void updateRevoke(Map map);
}
