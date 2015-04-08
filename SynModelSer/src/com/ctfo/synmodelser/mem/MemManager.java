package com.ctfo.synmodelser.mem;

import java.io.IOException;
import java.util.concurrent.TimeoutException;

import net.rubyeye.xmemcached.MemcachedClient;
import net.rubyeye.xmemcached.MemcachedClientBuilder;
import net.rubyeye.xmemcached.XMemcachedClientBuilder;
import net.rubyeye.xmemcached.exception.MemcachedException;
import net.rubyeye.xmemcached.utils.AddrUtil;

import com.ctfo.synmodelser.util.Config;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SynModelSer <br>
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
 * <td>Feb 14, 2012</td>
 * <td>wuqj</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author wuqj
 * @since JDK1.6
 */
public class MemManager {
	/**
	 * 获取MemcachedClient
	 * 
	 * @param server
	 *            服务器
	 * @return MemcachedClient
	 */
	public static MemcachedClient getMemcachedClient() {
		Config c = Config.getInstance();
		MemcachedClientBuilder bulider = new XMemcachedClientBuilder(AddrUtil.getAddresses(c.getMemServer()));
		bulider.setConnectionPoolSize(2);
		try {
			return bulider.build();
		} catch (IOException e) {
			e.printStackTrace();
		}
		return null;
	}

	/**
	 * 获取MemcachedClient
	 * 
	 * @param server
	 *            服务器
	 * @return MemcachedClient
	 */
	public static MemcachedClient getMemcachedClient(Config c) {
		MemcachedClientBuilder bulider = new XMemcachedClientBuilder(AddrUtil.getAddresses(c.getMemServer()));
		bulider.setConnectionPoolSize(2);
		try {
			return bulider.build();
		} catch (IOException e) {
			e.printStackTrace();
		}
		return null;
	}

	/**
	 * 测试类
	 * 
	 * @param client
	 */
	public void test(MemcachedClient client) {
		try {
			System.out.println(client.get("1"));
			if (client.get("1") == null) {
				client.set("1", 0, "11111111111");
			}
		} catch (TimeoutException e) {
			e.printStackTrace();
		} catch (InterruptedException e) {
			e.printStackTrace();
		} catch (MemcachedException e) {
			e.printStackTrace();
		}
	}

	public static void main(String[] args) {
		// MemcachedClient client = MemManager.getMemcachedClient();

	}
}
