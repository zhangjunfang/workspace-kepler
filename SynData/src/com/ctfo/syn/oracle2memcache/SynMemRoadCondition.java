package com.ctfo.syn.oracle2memcache;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.URL;
import java.net.URLConnection;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.log4j.Logger;

import com.ctfo.portalmng.beans.RoadCondition;
import com.ctfo.redis.util.RedisJsonUtil;
import com.ctfo.syn.dao.RedisServer;
import com.ctfo.syn.kcpt.utils.SynPool;
import com.ctfo.unifiedstorage.service.JedisUnifiedStorageService;

/**
 * 路况信息同步到memcache服务 (10分钟)
 * @author xuehui
 *
 */
public class SynMemRoadCondition implements Runnable{

	private Logger logger = Logger.getLogger(SynMemRoadCondition.class);
	
	private final String CODE = "GBK";
	
	private Map<String, List<RoadCondition>> mapClient = new HashMap<String, List<RoadCondition>>();
	
	public SynMemRoadCondition() {
	}

	public void run() {
		logger.info("开始执行路况信息查询");
		initMemRoadCondition();
	}
	
	private void initMemRoadCondition() {		
		JedisUnifiedStorageService jedis = null;
		try {
			jedis = RedisServer.getJedisServiceInstance();
//			mapClient = ConnectMemcachePool.getSqlMap(SynPool.getinstance().getSql("memcacheMainAddr")).get(StaticMemcache.MEMCACHE_ROADCONDITION);
//			if(null == mapClient){
//				mapClient = new HashMap<String, List<RoadCondition>>();
//			}
			
			// 查询路况信息
			synMemRoadCondition();
			if(mapClient!=null){
//				jedis.set(0, StaticMemcache.MEMCACHE_ROADCONDITION, RedisJsonUtil.objectToJson(mapClient)); 
				jedis.saveRoadCondition(RedisJsonUtil.objectToJson(mapClient));
				logger.info("*********同步数据:"+mapClient.size());
			}
//			ConnectMemcachePool.getSqlMap(SynPool.getinstance().getSql("memcacheMainAddr")).set(StaticMemcache.MEMCACHE_ROADCONDITION, 0, mapClient);			
		} catch (Exception e) {
			logger.error("同步路况信息出错",e);
		}finally{
			if(mapClient!=null){
				mapClient.clear();
			}
		}
	}
	
	/**
	 * 查询路况信息
	 * @return Map
	 */
	@SuppressWarnings("null")
	private void synMemRoadCondition() {	
		
		InputStream is = null;
		URL url = null;
		URLConnection connection = null;
		BufferedReader br = null;
		String[] provinceCodes = null;
		provinceCodes = SynPool.getinstance().getSql("provincecodes").split(",");
		if(provinceCodes != null && provinceCodes.length > 0) {
			StringBuffer sb = new StringBuffer();
			try {
				
				for (String provinceCode : provinceCodes) {
					try{
						//boolean b = true;		
						// 发送
						url = new URL(SynPool.getinstance().getSql("RoadConditionsURLID").replace("{0}", provinceCode));
						connection = url.openConnection();
						connection.setDoOutput(true);
						connection.setReadTimeout(10 * 1000);
						// 接收
						is = connection.getInputStream();
						br = new BufferedReader(new InputStreamReader(is, CODE));
						
						while (br.ready()) {
							sb.append(br.readLine()).append("\r\n");
							
						}// End while
						
						String[] strs = sb.toString().split("\r\n");
						if (null != strs && 0 < strs.length) {
							List<RoadCondition> roadConditionsList = new ArrayList<RoadCondition>();
							String roadConditionTime = getTime(strs[0]);
							if (null != roadConditionTime) {
								for (int i = 0; i < strs.length; i++) {
									if (i == 0) {
										continue;
									}
									RoadCondition roadCondition = new RoadCondition();
									roadCondition.setProvinceCode(Long.valueOf(provinceCode));
									roadCondition.setRoadConditionTime(roadConditionTime);
									roadCondition.setDescriptions(strs[i]);
									roadConditionsList.add(roadCondition);
								}
							}
							logger.info("provinceCode :" + provinceCode + " 获取成功。");
							if(roadConditionsList.size() > 0)
							mapClient.put(provinceCode, roadConditionsList);
						}	
						sb.delete(0, sb.length());
						strs = null;
					}catch(Exception e){
						logger.warn("获取地区编码provinceCode :" + provinceCode + "路况信息异常:" + e.getMessage()+"\r\n "+url.toString());
						continue;
					}finally{
						if(br != null){
							br.close();
							br = null;
						}
						if(is != null){
							is.close();
						}
						url = null;
						connection = null;
					}
				} // End for	
				if(mapClient != null){
					logger.info("provinceCode size:" + mapClient.size());
				}
			} catch (Exception e) {
				logger.error("获取路况信息数据失败：",e);	
			} finally {
				try {
					if(is != null) {
						is.close();
					}
					if(br != null) {
						br.close();
					}
				} catch (Exception ex) {
					logger.error("CLOSED PROPERTIES,ORACLE TO FAIL.",ex);
				}
				provinceCodes = null;
			}
		}	
	}
	
	/**
	 * 破解时间 1~288，五分钟
	 * @param time
	 * @return
	 */
	private String getTime(String time) {
		String timeStr = null;
		if (null != time) {
			Long ltime = new Long(0);
			Long utc = getZeroUtc(0);
			Long l = Long.parseLong(time);
			ltime = l * 5 * 60 * 1000 + utc;

			timeStr = dateFormat(new Date(ltime));
		}
		return timeStr;
	}
	
	
	/**
	 * 获取UTC时间
	 * @param dayNum
	 * 天数,以当天零点为基数，指定天数UTC时间(如传递-1获取前一天零点UTC时间)
	 * @return
	 */
	private long getZeroUtc(int dayNum) {
		Calendar calendar = Calendar.getInstance();
		calendar.add(Calendar.DAY_OF_MONTH, dayNum);
		calendar.set(Calendar.HOUR_OF_DAY, 0);
		calendar.set(Calendar.MINUTE, 0);
		calendar.set(Calendar.SECOND, 0);
		calendar.set(Calendar.MILLISECOND, 0);
		return calendar.getTimeInMillis();
	}
	
	/**
	 * 时间格式化 yyyy-MM-dd HH:mm:ss
	 * @param date
	 * java.util.Date
	 * @return
	 */
	private String dateFormat(Date date) {
		String str = "";
		if (null != date) {
			str = new SimpleDateFormat("yyyy-MM-dd HH:mm").format(date);
		}
		return str;
	}
}
