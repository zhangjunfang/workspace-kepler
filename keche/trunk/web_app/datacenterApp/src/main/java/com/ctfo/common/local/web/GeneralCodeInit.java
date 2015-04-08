package com.ctfo.common.local.web;

import java.util.concurrent.TimeoutException;

import com.ctfo.storage.redis.core.RedisDaoSupport;
import com.ctfo.sys.service.SysGeneralCodeService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 码表初始化<br>
 * 描述： 码表初始化<br>
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
 * <td>2014-6-4</td>
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
public class GeneralCodeInit {

	/** redis服务-主 */
	private RedisDaoSupport writeJedisDao;

	/** 字典码表 */
	private SysGeneralCodeService sysGeneralCodeService;

	public void init() throws TimeoutException {
		String jsonResult = sysGeneralCodeService.findSysGeneralCodeByCode();
		try {
			if (null != jsonResult && !"".equals(jsonResult)) {
				this.writeJedisDao.setStaticGeneralCode(jsonResult);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void setWriteJedisDao(RedisDaoSupport writeJedisDao) {
		this.writeJedisDao = writeJedisDao;
	}

	public void setSysGeneralCodeService(SysGeneralCodeService sysGeneralCodeService) {
		this.sysGeneralCodeService = sysGeneralCodeService;
	}

}
