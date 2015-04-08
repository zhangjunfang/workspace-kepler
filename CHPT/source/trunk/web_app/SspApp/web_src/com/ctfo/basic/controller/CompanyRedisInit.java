package com.ctfo.basic.controller;

import java.util.List;
import java.util.concurrent.TimeoutException;

import org.springframework.beans.factory.annotation.Autowired;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;

import com.ctfo.operation.beans.CompanyInfo;
import com.ctfo.operation.dao.AuthManageDao;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspApp <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>2014-12-1</td>
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
public class CompanyRedisInit {

	/** redis主连接池 */
	@Autowired
	private JedisPool writeJedisPool;

	@Autowired
	private AuthManageDao authManageDAO;

	public void init() throws TimeoutException {
		List<CompanyInfo> list = authManageDAO.getCompanyList();
		if (null != list && list.size() > 0) {
			Jedis client = writeJedisPool.getResource();
			for (CompanyInfo companyInfo : list) {
				client.hset("HS_LOGIN", companyInfo.getComAccount(), companyInfo.getComPassWord() + "_" + companyInfo.getAuthCode());
			}
		}
	}

	public void setWriteJedisPool(JedisPool writeJedisPool) {
		this.writeJedisPool = writeJedisPool;
	}

	public void setAuthManageDAO(AuthManageDao authManageDAO) {
		this.authManageDAO = authManageDAO;
	}

	
}
