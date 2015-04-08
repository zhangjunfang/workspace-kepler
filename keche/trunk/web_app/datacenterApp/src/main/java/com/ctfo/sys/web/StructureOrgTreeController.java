package com.ctfo.sys.web;

import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;

import com.ctfo.common.local.web.BaseController;
import com.ctfo.common.util.StaticSession;
import com.ctfo.common.util.StringUtil;
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
@Controller
@RequestMapping("/structureOrgTree")
public class StructureOrgTreeController extends BaseController {

	/** 组织树service */
	@Autowired
	private StructureOrgTreeService structureOrgTreeService;

	/**
	 * 分中心按省市查询组织树(支持模糊)-同步树
	 * 
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/findSynchronizedOrgTreeByProvince.do")
	public String findSynchronizedOrgTreeByProvince(HttpServletRequest request, HttpServletResponse response) {
		Map<String, String> map = new HashMap<String, String>();
		String entId = request.getParameter("entId"); // 组织id
		String centerCode = request.getParameter("centerCode"); // 分中心编码
		String corpProvince = request.getParameter("corpProvince"); // 省市编码
		String entName = request.getParameter("entName"); // 组织名称
		if (StringUtil.isNotBlank(entId)) {
			map.put("entId", "#" + entId + "#");
		}
		if (StringUtil.isNotBlank(centerCode)) {
			map.put("centerCode", centerCode);
		}
		if (StringUtil.isNotBlank(corpProvince)) {
			map.put("corpProvince", corpProvince);
		}
		if (StringUtil.isNotBlank(entName)) {
			map.put("entName", entName);
		}
		String json = structureOrgTreeService.findSynchronizedOrgTreeByProvince(map);
		return this.writeHTML(response, json);
	}

	/**
	 * 分主中心组织树查询
	 * 
	 * @param request
	 * @param response
	 * @return
	 */
	@RequestMapping(value = "/findOrganizationTree.do")
	public String findOrganizationTree(HttpServletRequest request, HttpServletResponse response) {
		Map<String, String> map = new HashMap<String, String>();
		String json = null;
		String entId = request.getParameter("entId"); // 组织id
		String treeType = request.getParameter("treeType"); // 树类型 1 分中心树 2主中心树
		if (StringUtil.isNotBlank(entId)) {
			map.put("entId", entId);
		}
		if (StaticSession.ONE.equals(treeType)) {
			json = structureOrgTreeService.findSynchronizedOrgTree(map);
		} else {
			json = structureOrgTreeService.findAsynchronousCenterOrgTree(map);
		}
		return this.writeHTML(response, json);
	}

}
