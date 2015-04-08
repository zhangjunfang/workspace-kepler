package com.ctfo.common.local.service.impl;

import java.io.Serializable;

import com.ctfo.common.local.service.BaseService;
import com.ctfo.storage.redis.core.RedisDaoSupport;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 抽象Service<br>
 * 描述： 抽象Service<br>
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
 * <td>2014-6-27</td>
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
@SuppressWarnings("serial")
public class BaseServiceImpl<T extends Serializable, PK extends Serializable> implements BaseService<T, PK>, Serializable {

	/** redis服务-主 */
	protected RedisDaoSupport writeJedisDao;

	/** redis服务-从 */
	protected RedisDaoSupport readJedisDao;

	public void setWriteJedisDao(RedisDaoSupport writeJedisDao) {
		this.writeJedisDao = writeJedisDao;
	}

	public void setReadJedisDao(RedisDaoSupport readJedisDao) {
		this.readJedisDao = readJedisDao;
	}

}
