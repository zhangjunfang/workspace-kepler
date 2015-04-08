/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.cache CommandCache.java	</li><br>
 * <li>时        间：2013-8-19  上午11:32:05	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.util;

import java.util.concurrent.ConcurrentHashMap;

/*****************************************
 * <li>描        述：指令缓存管理类		
 * 
 *****************************************/
public class CommandCache {
	
	private static ConcurrentHashMap<String, Cache> cacheMap = new ConcurrentHashMap<String, Cache>(); 
	/*****************************************
	 * <li>描        述：缓存对象 		</li><br>
	 * <li>时        间：2013-8-19  上午11:55:47	</li><br>
	 * <li>参数： @param key		缓存对象主键
	 * <li>参数： @param obj		缓存对象
	 * <li>参数： @param timeOut	过期时间:秒(s)		</li><br>
	 * 
	 *****************************************/
	public synchronized static void set(String key, Object obj, long timeOut){
		Cache cache = new CommandCache().new Cache();
		cache.setKey(key);
		cache.setTimeOut((timeOut * 1000) + System.currentTimeMillis());
		cache.setValue(obj);
		cacheMap.put(key, cache);
	}
	/*****************************************
	 * <li>描        述：获取缓存对象 		</li><br>
	 * <li>时        间：2013-8-19  上午11:58:15	</li><br>
	 * <li>参数： @param key	缓存对象主键
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public synchronized static Object get(String key){
		if (hasCache(key)) {
			Cache cache = getCache(key);
			//是否过期
			if (cacheExpired(cache)) { 
				cacheMap.remove(key);
				return null;
			}else {
				return cache.getValue();
			}
		} else
			return null;
	}
	/*****************************************
	 * <li>描        述：得到缓存。同步静态方法 		</li><br>
	 * <li>时        间：2013-8-19  下午12:00:20	</li><br>
	 * <li>参数： @param key
	 * <li>参数： @return			</li><br>
	 * 
	 **************************************** */
	private synchronized static Cache getCache(String key) {
		return  cacheMap.get(key);
	}
	// 判断缓存是否过期
	private static boolean cacheExpired(Cache cache) {
		if (null == cache) { // 传入的缓存不存在
			return false;
		}
		long nowDt = System.currentTimeMillis(); // 系统当前的毫秒数
		long cacheDt = cache.getTimeOut(); // 缓存内的过期毫秒数
		if (cacheDt <= 0 || cacheDt > nowDt) { // 过期时间小于等于零时,或者过期时间大于当前时间时，则为FALSE
			return false;
		} else { // 大于过期时间 即过期
			return true;
		}
	}
	// 判断缓存中是否存在主键
	private synchronized static boolean hasCache(String key) {
		return cacheMap.containsKey(key);
	}
	/*****************************************
	 * <li>描        述：删除对象 		</li><br>
	 * <li>时        间：2013-8-19  下午3:26:45	</li><br>
	 * <li>参数： @param key		对象主键	</li><br>
	 * 
	 ****************************************
	 */
	public synchronized static void remove(String key){
		cacheMap.remove(key);
	}
	
	class Cache{
		/** 缓存ID  */
		private String key;  
		/** 缓存数据  */
		private Object value; 
		/** 更新时间  */
		private long timeOut; 
		
		public String getKey() {
			return key;
		}
		public void setKey(String key) {
			this.key = key;
		}
		public Object getValue() {
			return value;
		}
		public void setValue(Object value) {
			this.value = value;
		}
		public long getTimeOut() {
			return timeOut;
		}
		public void setTimeOut(long timeOut) {
			this.timeOut = timeOut;
		}
	}
}
