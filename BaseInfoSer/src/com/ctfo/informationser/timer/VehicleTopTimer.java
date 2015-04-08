package com.ctfo.informationser.timer;

import org.springframework.context.ApplicationContext;

import com.ctfo.informationser.memcache.service.MemcacheSetServiceRmi;
import com.ctfo.informationser.util.SpringBUtils;

/**
 * 排行榜Timer
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
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
 * <td>2011-11-16</td>
 * <td>zhangming</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author zhangming
 * @since JDK1.6
 */
public class VehicleTopTimer {

	private ApplicationContext applicationContext = null;

	private MemcacheSetServiceRmi memcacheSetServiceRmi = null;

	public void timerVehicleTop() {
		applicationContext = SpringBUtils.getApplicationContext();
		memcacheSetServiceRmi = (MemcacheSetServiceRmi) applicationContext.getBean("memcacheSetServiceRmi");
		memcacheSetServiceRmi.setVehicleTop();
		memcacheSetServiceRmi.setVehicleTeamTop();
	}
}
