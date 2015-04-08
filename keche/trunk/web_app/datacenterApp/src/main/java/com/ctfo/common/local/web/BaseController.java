package com.ctfo.common.local.web;

import java.io.IOException;
import java.io.PrintWriter;

import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.util.CookieUtil;
import com.ctfo.common.util.DesUtil;
import com.ctfo.common.util.OperatorInfoUtil;
import com.ctfo.common.util.StaticSession;
import com.ctfo.common.util.StringUtil;
import com.ctfo.storage.redis.core.RedisDaoSupport;


/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------<br>
 * 工程名 ： datacenterApp<br>
 * 功能：<br>
 * 描述：<br>
 * 授权 : (C) Copyright (c) 2011<br>
 * 公司 : 北京中交慧联信息科技有限公司<br>
 * -----------------------------------------------------------------------------<br>
 * 修改历史<br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014年5月23日</td>
 * <td>JiTuo</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font><br>
 * 
 * @version 1.0
 * 
 * @author JiTuo
 * @since JDK1.6
 */
public class BaseController {

	/** redis服务-主 */
	@Autowired
	protected RedisDaoSupport writeJedisDao;

	/** redis服务-从 */
	@Autowired
	protected RedisDaoSupport readJedisDao;

	/** 消息：操作成功 */
	protected final static String MES_SUCCESS_OPERATE = "操作成功!";

	/** 消息：添加成功 */
	protected final static String MES_SUCCESS_ADD = "添加成功!";

	/** 消息：修改成功 */
	protected final static String MES_SUCCESS_MODIFY = "修改成功!";

	/** 消息：删除成功 */
	protected final static String MES_SUCCESS_REMOVE = "删除成功!";

	/**
	 * 构建分页信息
	 * 
	 * @param request
	 * @return
	 */
	protected DynamicSqlParameter getPageParam(HttpServletRequest request) {
		DynamicSqlParameter param = new DynamicSqlParameter();
		String page = request.getParameter("requestParam.page");
		String pagesize = request.getParameter("requestParam.rows");
		String order = request.getParameter("requestParam.equal.sortorder");
		String sort = request.getParameter("requestParam.equal.sortname");
		if (StringUtil.isNotBlank(page)) {
			param.setPage(Integer.parseInt(page));
		} else {
			param.setPage(1);
		}
		if (StringUtil.isNotBlank(pagesize)) {
			param.setPagesize(Integer.parseInt(pagesize));
		} else {
			param.setPagesize(30);
		}
		if (StringUtil.isNotBlank(order)) {
			param.setOrder(order);
		}
		if (StringUtil.isNotBlank(sort)) {
			param.setSort(sort);
		}
		return param;
	}

	/**
	 * 直接输出HTML文本
	 * 
	 * @param response
	 * @param html
	 * @return
	 */
	protected String writeHTML(HttpServletResponse response, String html) {
		try {
			response.setContentType("text/html; charset=UTF-8"); // 设置 content-type
			response.setCharacterEncoding("UTF-8"); // 设置响应数据编码格式 (输出)
			PrintWriter out = response.getWriter();
			out.println(html);
		} catch (IOException e) {

		}
		return null;
	}

	/**
	 * 获取请求路径地址
	 * 
	 * @param request
	 * @return
	 */
	public String getUrl(HttpServletRequest request) {
		StringBuffer urlBuffer = request.getRequestURL();
		String url = urlBuffer.substring(0, urlBuffer.indexOf(request.getRequestURI())) + request.getContextPath();
		return url;
	}

	/**
	 * 返回json格式的操作信息
	 * 
	 * @param response
	 * @param message
	 *            操作标识
	 * @return
	 */
	public String returnInfoForJS(HttpServletResponse response, String message) {
		String displayMessage = "{\"displayMessage\":\"" + message + "\"}";
		return this.writeHTML(response, displayMessage);
	}

	/**
	 * 从缓存中获取用户id
	 * 
	 * @param request
	 * @return
	 */
	protected String getOperatorEntId(HttpServletRequest request) {
		String entId = OperatorInfoUtil.getOperatorEntId(this.getSessionOperatorId(request));
		return entId;
	}

	/**
	 * 从cookie中获取用户id
	 * 
	 * @param request
	 * @return
	 */
	protected String getSessionOperatorId(HttpServletRequest request) {
		String objId = null;
		Cookie cookie = CookieUtil.getInstance().getCookie(request, StaticSession.COOKIE_USERID);
		if (null != cookie && cookie.getValue() != null) {
			String cookieValue = cookie.getValue();
			String[] values = cookieValue.split("_");
			objId = DesUtil.getInstance().decryptStr(values[0]);
		}
		return objId;

	}
}
