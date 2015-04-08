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
import com.ctfo.sys.beans.TbOrg;
import com.ctfo.sys.beans.TbOrganization;
import com.ctfo.sys.dao.TbOrgDAO;
import com.ctfo.sys.dao.TbOrganizationDAO;
import com.ctfo.sys.service.TbOrgService;

@Service
public class TbOrgServiceImpl extends RemoteJavaServiceAbstract implements TbOrgService {

	@Autowired
	TbOrgDAO tbOrgDAO;
	
	@Autowired
	TbOrganizationDAO tbOrganizationDAO;
	
	/**
	 * 
	 * @description:机构管理-添加
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月29日下午8:02:08
	 * @modifyInformation：
	 */
	public void insert(TbOrg tbOrg) {
		TbOrganization tbOrganization = new TbOrganization();
		
//		BeanUtils.copyProperties(tbOrg, tbOrganization);
//		BeanUtils.copyProperties(tbOrg, tbOrgInfo);
		
		tbOrgDAO.insert(tbOrg);
	}

	/**
	 * 
	 * @description:机构管理-修改
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月29日下午8:02:26
	 * @modifyInformation：
	 */
	public int update(TbOrg tbOrg) {
//		TbOrganization tbOrganization = new TbOrganization();
		
//		BeanUtils.copyProperties(tbOrg, tbOrganization);
//		BeanUtils.copyProperties(tbOrg, tbOrgInfo);
		
		return tbOrgDAO.update(tbOrg);
	}

	/**
	 * 
	 * @description:机构管理-删除
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月29日下午8:02:39
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public int updateDelete(Map map) throws CtfoAppException {
/*		if(tbOrganizationDAO.haveSubOrg(map)){
			throw new CtfoAppException(CtfoAppExceptionDefinition.ORG_D_HAVESUBORG);
		}*/
		if(tbOrganizationDAO.haveSubOperator(map)){
			throw new CtfoAppException(CtfoAppExceptionDefinition.ORG_D_HAVESUBOPERATOR);
		}
/*		if(tbOrganizationDAO.haveSubRole(map)){
			throw new CtfoAppException(CtfoAppExceptionDefinition.ORG_D_HAVESUBROLE);
		}*/
		return tbOrganizationDAO.updateDelete(map);
	}
	
	/**
	 * 
	 * @description:分页时获取记录总数
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月27日下午2:46:38
	 * @modifyInformation：
	 */
	public int count(DynamicSqlParameter param) {
		return tbOrgDAO.count(param);
	}

	/**
	 * 
	 * @description:获取分页记录
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年3月27日下午2:47:02
	 * @modifyInformation：
	 */
	public PaginationResult<TbOrg> selectPagination(DynamicSqlParameter param) {
		return tbOrgDAO.selectPagination(param);
	}

	/**
	 * 
	 * @description:初始化机构树
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月2日上午8:57:59
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public List<TbOrganization> selectOrgTree(Map map) {
		return tbOrganizationDAO.selectOrgTree(map);
	}

	/**
	 * 
	 * @description:根据主键获取机构对象
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月2日下午3:17:18
	 * @modifyInformation：
	 */
	public TbOrg selectPK(String tbId) {
		return tbOrgDAO.selectPK(tbId);
	}

	/**
	 * 
	 * @description:获取同一级别的最大机构ID
	 * @param:
	 * @author: 蒋东卿
	 * @creatTime:  2014年4月21日下午1:25:50
	 * @modifyInformation：
	 */
	@SuppressWarnings("rawtypes")
	public String getMaxOrgId(Map map) {
		String maxOrgId = tbOrgDAO.getMaxOrgId(map);
		Integer newId = Integer.parseInt(maxOrgId) + 1;
		String newStringId = newId.toString();
		
		//判断长度是否相等   不等则补0
		int maxOrgIdLength = maxOrgId.length();
		int newStringIdLength = newStringId.length();
		if(maxOrgIdLength > newStringIdLength) {
			for(int i=0; i<(maxOrgIdLength-newStringIdLength); i++) {
				newStringId = "0" + newStringId;
			}
		}
		
		return newStringId;
	}

	@Override
	public int updateRevoke(Map map) {
		// TODO Auto-generated method stub
		return tbOrgDAO.updateRevoke(map);
	}

	@Override
	public int existLoginname(Map map) {
		// TODO Auto-generated method stub
		return tbOrgDAO.existLoginname(map);
	}

	@Override
	public List<TbOrg> queryEntList(Map map) {
		// TODO Auto-generated method stub
		return tbOrgDAO.queryEntList(map);
	}

}
