package com.ctfo.sys.service;

import java.util.List;
import java.util.Map;

import com.ctfo.export.RemoteJavaService;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.TbOrg;
import com.ctfo.sys.beans.TbOrganization;

public interface TbOrgService extends RemoteJavaService {
	/**
	 * 查询公司列表
	 * @return
	 */
	public List<TbOrg> queryEntList(Map<String,String> map);
	/**
	 * 
	 * @description:机构管理-添加
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月29日下午8:02:08
	 * @modifyInformation：
	 */
	public void insert(TbOrg tbOrg);
	
	/**
	 * 
	 * @description:机构管理-修改
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月29日下午8:02:26
	 * @modifyInformation：
	 */
	public int update(TbOrg tbOrg);
	
	/**
	 * 
	 * @description:机构管理-删除
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月29日下午8:02:39
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map) throws CtfoAppException;
	
	/**
	 * 
	 * @description:分页时获取记录总数
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月27日下午2:46:38
	 * @modifyInformation：
	 */
	public int count(DynamicSqlParameter param);
	
	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月27日下午2:47:02
	 * @modifyInformation：
	 */
	public PaginationResult<TbOrg> selectPagination(DynamicSqlParameter param);
	
	/**
	 * 
	 * @description:初始化机构树
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月2日上午8:57:59
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<TbOrganization> selectOrgTree(Map map);
	
	/**
	 * 
	 * @description:根据主键获取机构对象
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月2日下午3:17:18
	 * @modifyInformation：
	 */
	public TbOrg selectPK(String tbId);
	
	/**
	 * 
	 * @description:获取同一级别的最大机构ID
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月21日下午1:25:50
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public String getMaxOrgId(Map map);
	
	@SuppressWarnings("rawtypes")
	public int updateRevoke(Map map);
	
	@SuppressWarnings("rawtypes")
	public int existLoginname(Map map);
}
 