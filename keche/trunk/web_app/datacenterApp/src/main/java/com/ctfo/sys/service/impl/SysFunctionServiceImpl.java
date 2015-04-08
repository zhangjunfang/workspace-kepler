package com.ctfo.sys.service.impl;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.FunctionTree;
import com.ctfo.sys.beans.SysFunction;
import com.ctfo.sys.dao.SysFunctionDAO;
import com.ctfo.sys.service.SysFunctionService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 权限<br>
 * 描述： 权限<br>
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
 * <td>2014-5-21</td>
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
public class SysFunctionServiceImpl implements SysFunctionService {

	private static Log log = LogFactory.getLog(SysFunctionServiceImpl.class);

	@Autowired
	private SysFunctionDAO sysFunctionDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.SysFunctionService#findByRoleId(java.util.Map)
	 */
	@Override
	public List<FunctionTree> findByRoleId(Map<String, String> map) throws CtfoAppException {
		try {
			List<FunctionTree> treeList = new ArrayList<FunctionTree>();
			List<SysFunction> functionList = sysFunctionDAO.findByRoleId(map);
			if (null != functionList) {
				for (SysFunction sysFunction : functionList) {
					if ("-1".equals(sysFunction.getFunParentId())) {
						FunctionTree functionTree = new FunctionTree(); // 组装权限树
						functionTree.setText(sysFunction.getFunName()); // 权限名称
						functionTree.setIsexpand("true"); // 是否展开
						functionTree.setIschecked(sysFunction.getIsChecked()); // 是否选中
						functionTree.setNodeId(sysFunction.getFunId()); // 权限id
						functionTree.setParentId("-1");
						functionTree.setChildrenList(this.findSubFunction(functionList, sysFunction.getFunId()));
						if (functionTree.getChildrenList().size() != 0) {
							functionTree.setIsChildrenList("true");
						}
						treeList.add(functionTree);
					}
				}
			}
			return treeList;
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.SysFunctionService#findFunListByOpId(java.util.Map)
	 */
	@Override
	public List<String> findFunListByOpId(Map<String, String> map) throws CtfoAppException {
		try {
			return sysFunctionDAO.findFunListByOpId(map);
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.SysFunctionService#findFunTreeRoleEdit(java.util.Map)
	 */
	@Override
	public List<FunctionTree> findFunTreeRoleEdit(Map<String, String> map) throws CtfoAppException {
		try {
			List<FunctionTree> treeList = new ArrayList<FunctionTree>();
			List<SysFunction> functionList = sysFunctionDAO.findFunTreeRoleEdit(map);
			if (null != functionList) {
				for (SysFunction sysFunction : functionList) {
					if ("-1".equals(sysFunction.getFunParentId())) {
						FunctionTree functionTree = new FunctionTree(); // 组装权限树
						functionTree.setText(sysFunction.getFunName()); // 权限名称
						functionTree.setIsexpand("true"); // 是否展开
						functionTree.setIschecked(sysFunction.getIsChecked()); // 是否选中
						functionTree.setNodeId(sysFunction.getFunId()); // 权限id
						functionTree.setParentId("-1");
						functionTree.setChildrenList(this.findSubFunction(functionList, sysFunction.getFunId()));
						if (functionTree.getChildrenList().size() != 0) {
							functionTree.setIsChildrenList("true");
						}
						treeList.add(functionTree);
					}
				}
			}
			return treeList;
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.SysFunctionService#initFunTree(java.util.Map)
	 */
	@Override
	public List<FunctionTree> initFunTree(Map<String, String> map) throws CtfoAppException {
		try {
			List<FunctionTree> treeList = new ArrayList<FunctionTree>();
			List<SysFunction> functionList = sysFunctionDAO.initFunTree(map);
			if (null != functionList) {
				for (SysFunction sysFunction : functionList) {
					if ("-1".equals(sysFunction.getFunParentId())) {
						FunctionTree functionTree = new FunctionTree(); // 组装权限树
						functionTree.setText(sysFunction.getFunName()); // 权限名称
						functionTree.setIsexpand("true"); // 是否展开
						functionTree.setIschecked(sysFunction.getIsChecked()); // 是否选中
						functionTree.setNodeId(sysFunction.getFunId()); // 权限id
						functionTree.setParentId("-1");
						functionTree.setChildrenList(this.findSubFunction(functionList, sysFunction.getFunId()));
						if (functionTree.getChildrenList().size() != 0) {
							functionTree.setIsChildrenList("true");
						}
						treeList.add(functionTree);
					}
				}
			}
			return treeList;
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
	}

	/**
	 * 递归权限树
	 * 
	 * @param functionList
	 * @param parentId
	 * @return
	 */
	private List<FunctionTree> findSubFunction(List<SysFunction> functionList, String parentId) {
		List<FunctionTree> subTreeList = new ArrayList<FunctionTree>();
		for (SysFunction sysFunction : functionList) {
			if (parentId.equals(sysFunction.getFunParentId())) {
				FunctionTree functionTree = new FunctionTree();
				functionTree.setText(sysFunction.getFunName()); // 权限名称
				functionTree.setIsexpand("true"); // 是否展开
				functionTree.setIschecked(sysFunction.getIsChecked()); // 是否选中
				functionTree.setNodeId(sysFunction.getFunId()); // 权限id
				functionTree.setParentId(parentId);
				functionTree.setChildrenList(this.findSubFunction(functionList, sysFunction.getFunId()));
				if (functionTree.getChildrenList().size() != 0) {
					functionTree.setIsChildrenList("true");
				}
				subTreeList.add(functionTree);
			}
		}
		return subTreeList;
	}

	public void setSysFunctionDAO(SysFunctionDAO sysFunctionDAO) {
		this.sysFunctionDAO = sysFunctionDAO;
	}

}
