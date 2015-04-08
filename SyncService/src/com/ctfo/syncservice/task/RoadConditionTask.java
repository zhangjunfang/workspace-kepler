/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.syn.task DriverAuthenticationTask.java	</li><br>
 * <li>时        间：2013-8-21  下午7:16:43	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.task;

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

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.redis.util.RedisJsonUtil;
import com.ctfo.syncservice.model.RoadCondition;
import com.ctfo.syncservice.util.TaskAdapter;

/*****************************************
 * <li>描        述：路况信息同步任务		
 * 
 *****************************************/
public class RoadConditionTask  extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(RoadConditionTask.class);
	/** 区域代码   */
	private static String provinceCode;
	/** 区域代码说明   */
	private static String areaStr;
	/** 请求地址   */
	private static String requestUrl;
	/** 请求地址   */
	private static String enCoding = "GBK";
	/** 路况列表   */
	private Map<String, List<RoadCondition>> mapClient = new HashMap<String, List<RoadCondition>>();
	/** 区域列表   */
	private Map<String, String> areaMap = new HashMap<String, String>();
	/*****************************************
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		setName("RoadConditionTask");
		provinceCode = conf.get("provinceCode");
		areaStr = conf.get("areaStr");
		requestUrl = conf.get("requestUrl");
		enCoding =  conf.get("enCoding");
	}
	/*****************************************
	 * <li>描 述：同步信息 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void execute() {
		long start = System.currentTimeMillis();
		Jedis jedis = null;
		try {
			synMemRoadCondition();
			if(mapClient != null){
				jedis = this.redis.getJedisConnection();
				jedis.select(4);
				jedis.set("mRoadCondition",RedisJsonUtil.objectToJson(mapClient));
				logger.info("--syncRoad--路况信息全部同步完成,获取路况:({})条,耗时:[{}]ms", mapClient.size(), System.currentTimeMillis() - start);
			} else {
				logger.info("--syncRoad--路况信息全部同步完成,获取路况:0 条,耗时:[{}]ms", System.currentTimeMillis() - start);
			}
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("路况信息同步异常:" + e.getMessage(), e);
		}finally{
			if(jedis != null){
				this.redis.returnJedisConnection(jedis);
			}
			if(mapClient!=null){
				mapClient.clear();
			}
		}
	}
	
	/**
	 * 查询路况信息
	 * @return Map
	 */
	private void synMemRoadCondition() {	
		InputStream is = null;
		URL url = null;
		URLConnection connection = null;
		BufferedReader br = null;
		String remoteUrl = null;
		try {
//			获取区域代码名称对应表 
			areaMap = getAreaMap();
			String[] provinceCodes = provinceCode.split(",");
			if(provinceCodes != null && provinceCodes.length > 0) {
				StringBuffer sb = new StringBuffer();
				for (String provinceCode : provinceCodes) {
					try{
						remoteUrl = requestUrl.replace("{0}", provinceCode);
						// 发送
						url = new URL(remoteUrl);
						connection = url.openConnection();
						connection.setDoOutput(true);
//						设置读取超时10秒
						connection.setReadTimeout(10 * 1000);
						// 接收
						is = connection.getInputStream();
						br = new BufferedReader(new InputStreamReader(is, enCoding));
						while (br.ready()) {
							sb.append(br.readLine()).append("\r\n");
						}
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
							logger.info("{}路况信息获取成功" , areaMap.get(provinceCode));
							if(roadConditionsList.size() > 0){
								mapClient.put(provinceCode, roadConditionsList);
							}
						}	
						sb.delete(0, sb.length());
						strs = null;
					}catch (java.lang.NumberFormatException e1){
						logger.error("获取地区编码:[{}]解析返回数据异常:, 请检查网络状态:请在浏览器中输入[{}]后按回车测试; 异常内容:{}" , areaMap.get(provinceCode), remoteUrl, e1.getMessage());
						continue;
					}catch(java.net.UnknownHostException ex){
						logger.error("获取地区编码:[{}]路况信息时异常:{} 原因:访问远程主机异常 - 排查方式:(1)请在浏览器中输入[{}]后按回车测试  (2)10分钟后再试   (3)联系内容提供商", areaMap.get(provinceCode), ex.getMessage(), remoteUrl);
						continue;
					}catch(Exception e){
						if(e.getMessage().indexOf("Read timed out") > -1 || e.getMessage().indexOf("Connection refused") > -1){
							logger.error("获取地区编码:[" + areaMap.get(provinceCode) + "]路况信息10秒无数据返回,超时:" + e.getMessage());
							continue;
						}else{
							logger.error("获取地区编码:[" + areaMap.get(provinceCode) + "]路况信息异常:" + e.getMessage() , e);
							continue;
						}
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
				}
			}
		} catch (Exception e) {
				logger.error("获取路况信息数据异常:" + e.getMessage(), e);	
		} finally {
			try {
				if(is != null) {
					is.close();
				}
				if(br != null) {
					br.close();
				}
			} catch (Exception ex) {
				logger.error("获取路况信息数据关闭资源异常:" + ex.getMessage(), ex);	
			}
		}
	}
	
	/*****************************************
	 * <li>描        述：获取区域代码名称对应表 		</li><br>
	 * <li>时        间：2013-10-17  上午2:27:06	</li><br>
	 * <li>参数： @param areaStr2
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	private Map<String, String> getAreaMap() {
		try{
			String[] areaArray = areaStr.split(",");
			for(String area : areaArray){
				if(area != null){
					String[] keys = StringUtils.splitPreserveAllTokens(area, "#", 2);
					areaMap.put(keys[1], area);
				}
			}
		}catch(Exception e){
			logger.error("获取区域代码名称对应表异常:" + e.getMessage(), e);
		}
		return areaMap;
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
