package com.ctfo.sys.service.impl;

import java.util.ArrayList;
import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.sys.beans.SysAreaInfo;
import com.ctfo.sys.beans.SysGeneralCode;
import com.ctfo.sys.dao.SysAreaInfoDAO;
import com.ctfo.sys.dao.SysGeneralCodeDAO;
import com.ctfo.sys.service.SysGeneralCodeService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 字典码表<br>
 * 描述： 字典码表<br>
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
 * <td>2014-6-3</td>
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
public class SysGeneralCodeServiceImpl implements SysGeneralCodeService {

	private static Log log = LogFactory.getLog(SysGeneralCodeServiceImpl.class);

	/** 码表 */
	@Autowired
	private SysGeneralCodeDAO sysGeneralCodeDAO;

	/** 全国行政区划 */
	@Autowired
	private SysAreaInfoDAO sysAreaInfoDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.sys.service.SysGeneralCodeService#findSysGeneralCodeByCode()
	 */
	@Override
	public String findSysGeneralCodeByCode() throws CtfoAppException {
		String jsonResult = "{";
		try {
			List<SysGeneralCode> list = sysGeneralCodeDAO.select(); // 字典码表
			if (null != list && list.size() > 0) {
				jsonResult += this.getGeneralCodeJson(list);
			}
			List<SysAreaInfo> areaList = sysAreaInfoDAO.select(); // 行政区划
			if (null != areaList && areaList.size() > 0) {
				jsonResult += ",";
				jsonResult += this.getAreaInfoJson(areaList);
			}
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
		jsonResult += "}";
		return jsonResult;
	}

	/**
	 * 得到通用编码json数据
	 * 
	 * @param list
	 * @return
	 */
	private String getGeneralCodeJson(List<SysGeneralCode> list) {
		if (list.size() == 0) {
			return "";
		}
		List<SysGeneralCode> rootList = new ArrayList<SysGeneralCode>();
		List<SysGeneralCode> childList = new ArrayList<SysGeneralCode>();
		for (int i = 0; i < list.size(); i++) {
			SysGeneralCode info = list.get(i);
			if (info.getParentGeneralCode().equals("0")) {
				rootList.add(info);
			} else {
				childList.add(info);
			}
		}
		return this.creatGeneralCodeJson(rootList, childList);
	}

	/**
	 * 创建通用编码 json
	 * 
	 * @param rootList
	 * @param childList
	 * @return
	 */
	private String creatGeneralCodeJson(List<SysGeneralCode> rootList, List<SysGeneralCode> childList) {
		StringBuilder sb = new StringBuilder();
		for (SysGeneralCode info : rootList) {
			sb.append("\"").append(info.getGeneralCode()).append("\":[");
			String cJson = this.getChileGeneralCodeJson(childList, info.getGeneralCode());
			sb.append(cJson).append("],");
		}
		return sb.substring(0, sb.length() - 1);
	}

	/**
	 * 递归得到通用编码子类型
	 * 
	 * @param chileList
	 * @param code
	 * @return
	 */
	private String getChileGeneralCodeJson(List<SysGeneralCode> chileList, String code) {
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < chileList.size(); i++) {
			SysGeneralCode info = chileList.get(i);
			String pCode = info.getParentGeneralCode();
			if (pCode.equals(code)) {
				sb.append("{\"code\":\"").append(info.getGeneralCode()).append("\",");
				sb.append("\"name\":\"").append(info.getCodeName()).append("\"},");
			}
		}
		if (sb != null && !"".equals(sb) && sb.length() > 0) {
			return sb.substring(0, sb.length() - 1);
		} else {
			return "";
		}
	}

	/**
	 * 得到区域编码json数据
	 * 
	 * @param list
	 * @return
	 */
	private String getAreaInfoJson(List<SysAreaInfo> list) {
		if (list.size() == 0)
			return "";
		List<SysAreaInfo> rootList = new ArrayList<SysAreaInfo>();
		List<SysAreaInfo> childList = new ArrayList<SysAreaInfo>();
		for (int i = 0; i < list.size(); i++) {
			SysAreaInfo info = list.get(i);
			if (info.getAreaLevel() == 0) {
				rootList.add(info);
			} else {
				childList.add(info);
			}
		}
		return this.creatAreaInfoJson(rootList, childList);
	}

	/**
	 * 创建区域编码json
	 * 
	 * @param rootList
	 * @param chileList
	 * @return
	 */
	private String creatAreaInfoJson(List<SysAreaInfo> rootList, List<SysAreaInfo> childList) {
		StringBuilder sb = new StringBuilder();
		sb.append("\"SYS_AREA_INFO\":[");
		for (SysAreaInfo info : rootList) {
			sb.append("{\"code\":\"").append(info.getAreaCode()).append("\",");
			sb.append("\"name\":\"").append(info.getAreaName()).append("\",");
			sb.append("\"children\":[");
			String cJson = this.getChileAreaInfoJson(childList, info.getAreaCode());
			sb.append(cJson).append("]},");
		}
		return sb.substring(0, sb.length() - 1) + "]";
	}

	/**
	 * 递归得到省下的市
	 * 
	 * @param chileList
	 * @param code
	 * @return
	 */
	private String getChileAreaInfoJson(List<SysAreaInfo> childList, String code) {
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < childList.size(); i++) {
			SysAreaInfo info = childList.get(i);
			String rtCode = code.substring(0, 2);
			String ctCode = info.getAreaCode().substring(0, 2);
			if (ctCode.equals(rtCode)) {
				sb.append("{\"code\":\"").append(info.getAreaCode()).append("\",");
				sb.append("\"name\":\"").append(info.getAreaName()).append("\"},");
			}
		}
		if (sb != null && !"".equals(sb) && sb.length() > 0) {
			return sb.substring(0, sb.length() - 1);
		} else {
			return "";
		}
	}

	public void setSysGeneralCodeDAO(SysGeneralCodeDAO sysGeneralCodeDAO) {
		this.sysGeneralCodeDAO = sysGeneralCodeDAO;
	}

	public void setSysAreaInfoDAO(SysAreaInfoDAO sysAreaInfoDAO) {
		this.sysAreaInfoDAO = sysAreaInfoDAO;
	}

}
