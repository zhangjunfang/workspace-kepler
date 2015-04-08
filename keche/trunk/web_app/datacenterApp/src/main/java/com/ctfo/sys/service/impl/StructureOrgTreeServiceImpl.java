package com.ctfo.sys.service.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.sys.beans.StructureOrgTree;
import com.ctfo.sys.dao.StructureOrgTreeDAO;
import com.ctfo.sys.service.StructureOrgTreeService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 组织树<br>
 * 描述： 组织树<br>
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
 * <td>2014-6-6</td>
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
public class StructureOrgTreeServiceImpl implements StructureOrgTreeService {

	private static Log log = LogFactory.getLog(StructureOrgTreeServiceImpl.class);

	/** 组织树 */
	@Autowired
	private StructureOrgTreeDAO structureOrgTreeDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.StructureOrgTreeService#findAsynchronousOrgTree(java.util.Map)
	 */
	@Override
	public String findAsynchronousOrgTree(Map<String, String> map) throws CtfoAppException {
		String json = "";
		List<StructureOrgTree> list = new ArrayList<StructureOrgTree>();
		try {
			list = structureOrgTreeDAO.asynchronousFindById(map);
			if (list.size() > 0) {
				json = this.getOrgTreeJSON(list);
			} else {
				json = "[]";
			}
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
		return json;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.StructureOrgTreeService#findSynchronizedOrgTree(java.util.Map)
	 */
	@Override
	public String findSynchronizedOrgTree(Map<String, String> map) throws CtfoAppException {
		String json = "";
		List<StructureOrgTree> list = new ArrayList<StructureOrgTree>(); // 分中心集合
		List<StructureOrgTree> areaList = new ArrayList<StructureOrgTree>(); // 省市编码集合
		try {
			list = structureOrgTreeDAO.asynchronousFindById(map);
			if (list.size() > 0) {
				for (StructureOrgTree structureOrgTree : list) {
					areaList = structureOrgTreeDAO.findAreaByLevel();
					for (StructureOrgTree area : areaList) {
						area.setCenterCode(structureOrgTree.getCenterCode());
						area.setCenterName(structureOrgTree.getCenterName());
						area.setEntId(structureOrgTree.getEntId());
					}
					structureOrgTree.setNodeList(areaList);
				}
				json = this.getOrgTreeJSON(list);
			} else {
				json = "[]";
			}
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
		return json;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.StructureOrgTreeService#findAsynchronousCenterOrgTree(java.util.Map)
	 */
	@Override
	public String findAsynchronousCenterOrgTree(Map<String, String> map) throws CtfoAppException {
		String json = "";
		List<StructureOrgTree> list = new ArrayList<StructureOrgTree>();
		try {
			list = structureOrgTreeDAO.asynchronousDataFindById(map);
			if (list.size() > 0) {
				json = this.getOrgTreeJSON(list);
			} else {
				json = "[]";
			}
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
		return json;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.StructureOrgTreeService#findSynchronizedOrgTreeByProvince(java.util.Map)
	 */
	@Override
	public String findSynchronizedOrgTreeByProvince(Map<String, String> map) throws CtfoAppException {
		String json = "";
		List<StructureOrgTree> list = new ArrayList<StructureOrgTree>();
		// 数据Map(用于组装树)
		Map<String, StructureOrgTree> mapData = null;
		try {
			// 同步树-模糊查询组织(通过模糊查询返回对应的组织ID)
			List<StructureOrgTree> orgEntIdList = structureOrgTreeDAO.synchronizedOrgByParam(map);
			// 传入的组织ID构成组织结构
			map.remove("entName");
			map.remove("corpProvince");
			Map<String, StructureOrgTree> mapTree = this.synchronizedOrgByMap(map);
			// 组装组织树
			if (orgEntIdList.size() > 0) {
				mapData = new HashMap<String, StructureOrgTree>();
				for (StructureOrgTree structureOrgTree : orgEntIdList) {
					this.setContrastNode(structureOrgTree.getEntId(), mapTree, mapData);
				}
			}
			StructureOrgTree tree = null;
			tree = this.getTree(mapData);
			if (null != tree) {
				list.add(tree);
			}
			// 按组织名称排序
			if (list.size() > 0) {
				json = this.getOrgTreeJSON(list);
			} else {
				json = "[]";
			}
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
		return json;
	}

	/**
	 * 向上递归设置组织关系-比较需要的组织结构
	 * 
	 * @param engId
	 * @param mapOrg
	 * @param mapTmp
	 */
	private void setContrastNode(String engId, Map<String, StructureOrgTree> mapOrg, Map<String, StructureOrgTree> mapContrast) {
		StructureOrgTree structureOrgTree = mapOrg.get(engId);
		if (null != structureOrgTree) {
			mapContrast.put(structureOrgTree.getEntId(), structureOrgTree);
			StructureOrgTree structureOrgTreeTmp = mapOrg.get(structureOrgTree.getParentId());
			if (null != structureOrgTreeTmp) {
				setContrastNode(structureOrgTreeTmp.getEntId(), mapOrg, mapContrast);
			}
		}
	}

	/**
	 * 构建树结构(循环算法)
	 * 
	 * @param map
	 * @param mapVehicle
	 * @return
	 */
	private StructureOrgTree getTree(Map<String, StructureOrgTree> map) {
		StructureOrgTree treeObj = new StructureOrgTree();
		if (null == map) {
			return treeObj;
		}
		for (Entry<String, StructureOrgTree> entry : map.entrySet()) {
			StructureOrgTree structureOrgTree = entry.getValue();
			StructureOrgTree parentStructureOrgTree = map.get(structureOrgTree.getParentId());
			if (null != parentStructureOrgTree) {
				List<StructureOrgTree> nodeList = parentStructureOrgTree.getNodeList();
				if (null == nodeList) {
					nodeList = new ArrayList<StructureOrgTree>();
					parentStructureOrgTree.setNodeList(nodeList);
				}
				nodeList.add(structureOrgTree);
			} else {
				treeObj = structureOrgTree;
			}
		}
		return treeObj;
	}

	/**
	 * 根据组织ID查询组织
	 * 
	 * @param map
	 * @return
	 */
	private Map<String, StructureOrgTree> synchronizedOrgByMap(Map<String, String> map) {
		List<StructureOrgTree> listTree = structureOrgTreeDAO.synchronizedOrgByParam(map);
		Map<String, StructureOrgTree> treeMap = new HashMap<String, StructureOrgTree>();
		if (listTree.size() > 0) {
			for (StructureOrgTree structureOrgTree : listTree) {
				treeMap.put(structureOrgTree.getEntId(), structureOrgTree);
			}
		}
		return treeMap;
	}

	/**
	 * 生成组织树json
	 * 
	 * @param list
	 * @return
	 */
	private String getOrgTreeJSON(List<StructureOrgTree> list) {
		StringBuilder json = new StringBuilder();
		json.append("[");
		if (null != list) {
			for (int i = 0; i < list.size(); i++) {
				StructureOrgTree tree = list.get(i);
				if (i != 0) {
					json.append(",");
				}
				json.append("{");
				json.append("\"i\":\"").append(tree.getEntId()).append("\",");
				json.append("\"n\":\"").append(tree.getEntName()).append("\",");
				json.append("\"t\":\"").append(tree.getEntType()).append("\",");
				json.append("\"p\":\"").append(tree.getParentId()).append("\",");
				json.append("\"ci\":\"").append(tree.getCorpCode()).append("\",");
				json.append("\"cp\":\"").append(tree.getCorpProvince()).append("\",");
				json.append("\"cc\":\"").append(tree.getCenterCode()).append("\",");
				json.append("\"cn\":\"").append(tree.getCenterName()).append("\"");
				if (null != tree.getNodeList() && tree.getNodeList().size() > 0) {
					json.append(",");
					this.recursionNodeJSON(tree.getNodeList(), json);
				}
				json.append("}");
			}
		}
		json.append("]");
		return json.toString();
	}

	/**
	 * 递归子节点
	 * 
	 * @param list
	 * @param json
	 */
	private void recursionNodeJSON(List<StructureOrgTree> list, StringBuilder json) {
		json.append("\"ch\":").append("[");
		for (int i = 0; i < list.size(); i++) {
			StructureOrgTree tree = list.get(i);
			if (i != 0) {
				json.append(",");
			}
			json.append("{");
			json.append("\"i\":\"").append(tree.getEntId()).append("\",");
			json.append("\"n\":\"").append(tree.getEntName()).append("\",");
			json.append("\"t\":\"").append(tree.getEntType()).append("\",");
			json.append("\"p\":\"").append(tree.getParentId()).append("\",");
			json.append("\"ci\":\"").append(tree.getCorpCode()).append("\",");
			json.append("\"cp\":\"").append(tree.getCorpProvince()).append("\",");
			json.append("\"cc\":\"").append(tree.getCenterCode()).append("\",");
			json.append("\"cn\":\"").append(tree.getCenterName()).append("\"");
			json.append(",");
			if (null != tree.getNodeList() && tree.getNodeList().size() > 0) {
				this.recursionNodeJSON(tree.getNodeList(), json);
			} else {
				json.append("\"ex\":\"").append("false").append("\"");
			}
			json.append("}");
		}
		json.append("]");
	}

	public void setStructureOrgTreeDAO(StructureOrgTreeDAO structureOrgTreeDAO) {
		this.structureOrgTreeDAO = structureOrgTreeDAO;
	}

}
