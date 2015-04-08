/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.region.util RedisUtil.java	</li><br>
 * <li>时        间：2013-7-13  下午3:29:42	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.region.util;

import com.ctfo.redis.JedisDataSource;
import com.ctfo.redis.pool.JedisSerPool;

/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.region.util RedisUtil.java	</li><br>
 * <li>时        间：2013-7-13  下午3:29:42	</li><br>
 * </ul>
 *****************************************/
public class RedisUtil {
	public static JedisDataSource getSource(String host, int port, String pass) {
		JedisDataSource jedisDataSource = new JedisDataSource();
		jedisDataSource.setHost(host);
		jedisDataSource.setPort(port);
		jedisDataSource.setPass(pass);
		return jedisDataSource;
	}

	public static JedisSerPool getPool(JedisDataSource jedisDataSource) {
		JedisSerPool jedisSerPool = new JedisSerPool(jedisDataSource);
		return jedisSerPool;
	}
}
