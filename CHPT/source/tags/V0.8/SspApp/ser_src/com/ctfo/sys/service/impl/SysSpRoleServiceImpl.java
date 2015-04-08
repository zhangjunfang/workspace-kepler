package com.ctfo.sys.service.impl;

import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.export.RemoteJavaServiceAbstract;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.exception.CtfoAppExceptionDefinition;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.SysSpRole;
import com.ctfo.sys.dao.SysSpRoleDAO;
import com.ctfo.sys.service.SysSpRoleService;

@Service
public class SysSpRoleServiceImpl extends RemoteJavaServiceAbstract implements SysSpRoleService {

	@Autowired
	SysSpRoleDAO sysSpRoleDAO;
	
	/**
	 * 
	 * @description:分页时获取记录总数量
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日上午10:00:41
	 * @modifyInformation：
	 */
	public int count(DynamicSqlParameter param) {
		return sysSpRoleDAO.count(param);
	}

	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日上午10:01:04
	 * @modifyInformation：
	 */
	public PaginationResult<SysSpRole> selectPagination(
			DynamicSqlParameter param) {
		return sysSpRoleDAO.selectPagination(param);
	}

	/**
	 * 
	 * @description:查询所有角色对象，用户分配角色权限时
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月4日上午10:58:35
	 * @modifyInformation：
	 */
	public List<SysSpRole> select(DynamicSqlParameter param){
		return sysSpRoleDAO.select(param);
	}
	
	/**
	 * 
	 * @description:角色名称是否存在
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日下午3:02:12
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public boolean isExistRoleName(Map map) {
		return sysSpRoleDAO.isExistRoleName(map);
	}

	/**
	 * 
	 * @description:角色管理-添加
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月31日下午3:24:23
	 * @modifyInformation：
	 */
	public void insert(SysSpRole sysSpRole) {
		sysSpRoleDAO.insert(sysSpRole);
	}

	/**
	 * 
	 * @description:角色管理-编辑
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日下午3:10:51
	 * @modifyInformation：
	 */
	public int update(SysSpRole sysSpRole){
		return sysSpRoleDAO.update(sysSpRole);
	}
	
	/**
	 * 
	 * @description:根据主键获取角色对象
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月1日上午9:43:54
	 * @modifyInformation：
	 */
	public SysSpRole selectPK(String tbId) {
		return sysSpRoleDAO.selectPK(tbId);
	}
	
	/**
	 * 
	 * @description:新建用户时，角色多选下拉数据
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月9日下午1:47:22
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<String> selectRoleByEntId(Map map){
		return sysSpRoleDAO.selectRoleByEntId(map);
	}

	/**
	 * 
	 * @description:角色管理-删除
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月17日下午2:53:12
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	@Override
	public int updateDelete(Map map) throws CtfoAppException {
		if(sysSpRoleDAO.haveTrOperator(map)){
			throw new CtfoAppException(CtfoAppExceptionDefinition.ROLE_D_HAVETROPERATOR);
		}
		return sysSpRoleDAO.updateDelete(map);
	}

	@Override
	public void updateRevoke(Map map) {
		// TODO Auto-generated method stub
		sysSpRoleDAO.updateRevoke(map);
	}

	@Override
	public List<SysSpRole> queryRoleList() {
		// TODO Auto-generated method stub
		return sysSpRoleDAO.queryRoleList();
	}

}
