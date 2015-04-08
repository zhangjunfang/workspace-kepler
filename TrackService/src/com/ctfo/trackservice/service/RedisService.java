/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.service RedisService.java	</li><br>
 * <li>时        间：2013-9-16  下午4:59:50	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.service;

import java.util.List;
import java.util.Map;
import java.util.Set;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.exceptions.JedisException;

import com.alibaba.fastjson.JSON;
import com.ctfo.trackservice.dao.RedisConnectionPool;
import com.ctfo.trackservice.model.StationHistory;
import com.ctfo.trackservice.model.StationJson;
import com.ctfo.trackservice.util.Constant;

/*****************************************
 * <li>描        述：redis服务		
 * 
 *****************************************/
public class RedisService {
	private static final Logger logger = LoggerFactory.getLogger(RedisService.class);
	private Jedis jedisOffLine = null;
	
	private Jedis jedis = null;
	/**	上下线提示存储	*/
	private Jedis jedisOnOffPrompt = null;
	
	int storageIndex = 0;
	 
	public RedisService(String type) throws Exception {  
		if(type.equals("track")){
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(6);
		} else if(type.equals("OnOffLine")){
			jedisOffLine = RedisConnectionPool.getJedisConnection();
			jedisOffLine.select(6);
			jedisOnOffPrompt = RedisConnectionPool.getJedisConnection();
			jedisOnOffPrompt.select(7);
		} else {
			throw new Exception("RedisService启动类型错误!");
		}
	}

	/*****************************************
	 * <li>描        述：设置轨迹信息 		</li><br>
	 * <li>时        间：2013-9-16  下午5:08:08	</li><br>
	 * <li>参数： @param app			</li><br>
	 * @param redisStorageCache 
	 * TODO
	 *****************************************/
	public void setTrackInfo(Map<String, String> app, String vid) {
		try{
			String vehicleStr = jedis.get(vid); 
			String utcStr = app.get(Constant.UTC);
			String gpsSpeed = app.get(Constant.N3);
			if(gpsSpeed == null){
				gpsSpeed = Constant.N0;
			}
			String lon = app.get(Constant.N1);
			String lat = app.get(Constant.N2);
			String head = app.get(Constant.N5);
			String mapLon = app.get(Constant.MAPLON);
			String mapLat = app.get(Constant.MAPLAT);
			String alarmCode = app.get(Constant.N20);
			if(alarmCode != null){
				if (app.get(Constant.N21)!=null) {
					alarmCode = alarmCode.substring(0, alarmCode.length() - 1)	+ app.get(Constant.N21);
				}
				alarmCode = alarmCode.replaceAll("\\,\\,", Constant.COMMA);
			}
			String basestatus = app.get(Constant.N8);// 基本状态
			String extendstatus = app.get(Constant.N500);// 扩展状态
			StringBuffer strBuf = new StringBuffer();
			if(vehicleStr != null){
				String[] arr = vehicleStr.split(":", 45);
				if(arr.length == 45){
					strBuf.append(lat); // 0
					strBuf.append(":"); 
					strBuf.append(lon); // 1
					strBuf.append(":");
					strBuf.append(mapLon); // 2
					strBuf.append(":");
					strBuf.append(mapLat); // 3
					strBuf.append(":");
					strBuf.append(gpsSpeed); // 4
					strBuf.append(":");
					strBuf.append(head); // 5
					strBuf.append(":");
					strBuf.append(utcStr); // 6
					strBuf.append(":");
					if(null != alarmCode){
						strBuf.append(alarmCode); // 7
					}
					strBuf.append(":");
					if (null != app.get("210")) { // 引擎转速（发动机转速） // 8
						strBuf.append(app.get("210"));
					}else{
						strBuf.append(arr[8]);
					}
					strBuf.append(":");
					
					if (null != app.get("216") && !"-1".equals(app.get("216"))) { // 瞬时油耗 // 9
						strBuf.append(app.get("216"));
					}else{
						strBuf.append(arr[9]);
					}
					strBuf.append(":");
					
					if (null != app.get("215") && !"-1".equals(app.get("215"))) { // 机油压力 // 10
						strBuf.append(app.get("215"));
					}else{
						strBuf.append(arr[10]);
					}
					strBuf.append(":");
					
					if (null != app.get("508") && !"-1".equals(app.get("508"))) { // 机油温度（随位置汇报上传） 11
						strBuf.append(app.get("508"));
					}else{
						strBuf.append(arr[11]);
					}
					strBuf.append(":");
					
					if (null != app.get("504") && !"-1".equals(app.get("504"))) { // 油门踏板位置，1bit=0.4%，0=0%（随位置汇报上传） 12
						strBuf.append(app.get("504"));
					}else{
						strBuf.append(arr[12]);
					}
					strBuf.append(":");
					
					if (null != app.get("213") && !"-1".equals(app.get("213"))) { // 累计油耗 13
						strBuf.append(app.get("213"));
					}else{
						strBuf.append(arr[13]);
					}
					strBuf.append(":");
					
					if (null != app.get("505") && !"-1".equals(app.get("505"))) { // 发动机运行总时长，1bit=0.05h，0=0h（随位置汇报上传）14
						strBuf.append(app.get("505"));
					}else{
						strBuf.append(arr[14]);
					}
					strBuf.append(":");
					
					if (null != app.get("510") && !"-1".equals(app.get("510"))) { // 进气温度（随位置汇报上传）15
						strBuf.append(app.get("510"));
					}else{
						strBuf.append(arr[15]);
					}
					strBuf.append(":");
					
					if (null != app.get("511") && !"-1".equals(app.get("511"))) { // 大气压力（随位置汇报上传）16
						strBuf.append(app.get("511"));
					}else{
						strBuf.append(arr[16]);
					}
					strBuf.append(":");
					
					if (null != app.get("7")) { // 脉冲车速（随位置汇报上传）17
						strBuf.append(app.get("7"));
					}else{
						strBuf.append(arr[17]);
					}
					strBuf.append(":");
					
					if (null != app.get("507") && !"-1".equals(app.get("507"))) { // 终端内置电池电压（随位置汇报上传）18
						strBuf.append(app.get("507"));
					}else{
						strBuf.append(arr[18]);
					}
					strBuf.append(":");
					
					if (null != app.get("509") && !"-1".equals(app.get("509"))) { // 冷却液温度（随位置汇报上传）19
						strBuf.append(app.get("509"));
					}else{
						strBuf.append(arr[19]);
					}
					strBuf.append(":");
					
					if (null != app.get("506") && !"-1".equals(app.get("506"))) { // 车辆蓄电池电压（随位置汇报上传）20
						strBuf.append(app.get("506"));
					}else{
						strBuf.append(arr[20]);
					}
					strBuf.append(":");
					
					if (null != app.get("503") && !"-1".equals(app.get("503"))) { // 发动机扭矩（随位置汇报上传）21
						strBuf.append(app.get("503"));
					}else{
						strBuf.append(arr[21]);
					}
					strBuf.append(":");
					
//					if (null != app.get("9") && !"-1".equals(app.get("9"))) { // 累计里程 22
					if (null != app.get("9")){
						strBuf.append(app.get("9"));	
					}else{
						strBuf.append(arr[22]);
					}
					strBuf.append(":");
					
					if(null != basestatus){ //基本状态位 23
						strBuf.append(basestatus);
					}else{
						strBuf.append(arr[24]);
					}
					strBuf.append(":");
					
					if(null != extendstatus){ //扩展状态位 24
						strBuf.append(extendstatus);
					}else{
						strBuf.append(arr[24]);
					}
					strBuf.append(":");
					
					strBuf.append(app.get(Constant.SPEEDFROM)); // 25
					strBuf.append(":");
				
					if(null != app.get("219") && !"-1".equals(app.get("219"))){ //精准油耗 // 26
						strBuf.append(app.get("219"));
					}else{
						strBuf.append(arr[26]);
					}
					strBuf.append(":");
					
					strBuf.append(app.get("6")); // 海拔 27
					
					strBuf.append(":");
					
					if(null !=app.get("24") ){
						strBuf.append(app.get("24")); // 油箱存油量(升) 28
					}
					
					strBuf.append(":");
					
					strBuf.append(vid); // vid 29
					
					strBuf.append(":");
					
					strBuf.append(arr[30]); // 车牌颜色 30
					
					strBuf.append(":");
					
					strBuf.append(arr[31]); // 车牌号 31
					
					strBuf.append(":");
					
					strBuf.append(arr[32]); // 手机号 32
					
					strBuf.append(":");
					
					strBuf.append(arr[33]); // 终端ID 33
					
					strBuf.append(":");
					
					strBuf.append(arr[34]); // 终端型号 34
					
					strBuf.append(":");
					
					if(null != arr[35]){
						strBuf.append(arr[35]); // 驾驶员姓名 35
					}
					strBuf.append(":");
					
					strBuf.append(arr[36]);  //所属组织 36
					
					strBuf.append(":");
					
					strBuf.append(arr[37]); // 车队id 37
					
					strBuf.append(":");
					
					strBuf.append(arr[38]); // 企业id 38
					
					strBuf.append(":");
					if(app.get(Constant.OEMCODE) != null){
						strBuf.append(app.get(Constant.OEMCODE)); // OEMCOED 39
					}else{
						strBuf.append(arr[39]); // OEMCOED 39
					}
					strBuf.append(":");
					
					strBuf.append(System.currentTimeMillis()); // 系统时间 40
					
					strBuf.append(":");
					
					strBuf.append(1); //  是否在线41
					
					strBuf.append(":");
					
					strBuf.append(0); //  是否合法 42
					
					strBuf.append(":");
					
					strBuf.append(1); //  MSGID 43
					
					strBuf.append(":");
					
					strBuf.append(arr[44]); // 车队名称 44
					
					jedis.set(vid, strBuf.toString());
				} else {
					logger.error("非法位置信息:" + vehicleStr);
				}
			} else { // TODO
				strBuf.append(lat); // 0
				strBuf.append(":");
				strBuf.append(lon); // 1
				strBuf.append(":");
				strBuf.append(mapLon); // 2
				strBuf.append(":");
				strBuf.append(mapLat); // 3
				strBuf.append(":");
				strBuf.append(gpsSpeed); // 4
				strBuf.append(":");
				strBuf.append(head); // 5
				strBuf.append(":");
				strBuf.append(utcStr); // 6
				strBuf.append(":");
				if (null != alarmCode) {
					strBuf.append(alarmCode); // 7
				}

				strBuf.append(":");
				if (null != app.get("210")) { // 引擎转速（发动机转速） // 8
					strBuf.append(app.get("210"));
				} else {

				}
				strBuf.append(":");

				if (null != app.get("216")) { // 瞬时油耗 // 9
					strBuf.append(app.get("216"));
				}
				strBuf.append(":");

				if (null != app.get("215") && !"".equals(app.get("215"))) { // 机油压力
																			// //
																			// 10
					strBuf.append(app.get("215"));
				}
				strBuf.append(":");

				if (null != app.get("508")) { // 机油温度（随位置汇报上传） 11
					strBuf.append(app.get("508"));
				}
				strBuf.append(":");

				if (null != app.get("504") && !"".equals(app.get("504"))) { // 油门踏板位置，1bit=0.4%，0=0%（随位置汇报上传）
																			// 12
					strBuf.append(app.get("504"));
				}
				strBuf.append(":");

				if (null != app.get("213")) { // 累计油耗 13
					strBuf.append(app.get("213"));
				}
				strBuf.append(":");

				if (null != app.get("505")) { // 发动机运行总时长，1bit=0.05h，0=0h（随位置汇报上传）14
					strBuf.append(app.get("505"));
				}
				strBuf.append(":");

				if (null != app.get("510")) { // 进气温度（随位置汇报上传）15
					strBuf.append(app.get("510"));
				}
				strBuf.append(":");

				if (null != app.get("511") && !"".equals(app.get("511"))) { // 大气压力（随位置汇报上传）16
					strBuf.append(app.get("511"));
				}
				strBuf.append(":");

				if (null != app.get("7")) { // 脉冲车速（随位置汇报上传）17
					strBuf.append(app.get("7"));
				}
				strBuf.append(":");

				if (null != app.get("507")) { // 终端内置电池电压（随位置汇报上传）18
					strBuf.append(app.get("507"));
				}
				strBuf.append(":");

				if (null != app.get("509") && !"".equals(app.get("509"))) { // 冷却液温度（随位置汇报上传）19
					strBuf.append(app.get("509"));
				}
				strBuf.append(":");

				if (null != app.get("506")) { // 车辆蓄电池电压（随位置汇报上传）20
					strBuf.append(app.get("506"));
				}
				strBuf.append(":");

				if (null != app.get("503") && !"".equals(app.get("503"))) { // 发动机扭矩（随位置汇报上传）21
					strBuf.append(app.get("503"));
				}
				strBuf.append(":");
				// if (null != app.get("9")) {
				// strBuf.append(app.get("9"));
				// }
				if (null != app.get("9")) { // 累计里程 22
					strBuf.append(app.get("9"));
				}
				strBuf.append(":");

				if (null != basestatus) { // 基本状态位 23
					strBuf.append(basestatus);
				}
				strBuf.append(":");

				if (null != extendstatus) { // 扩展状态位 24
					strBuf.append(extendstatus);
				}

				strBuf.append(":");

				strBuf.append(app.get(Constant.SPEEDFROM)); // 25
				strBuf.append(":");

				if (null != app.get("219")) { // 计量仪油耗 // 26
					strBuf.append(app.get("219"));
				}
				strBuf.append(":");

				strBuf.append(app.get("6")); // 海拔 27

				strBuf.append(":");

				if (null != app.get("24")) {
					strBuf.append(app.get("24")); // 油箱存油量(升) 28
				}

				strBuf.append(":");

				strBuf.append(vid); // vid 29

				strBuf.append(":");

				strBuf.append(app.get(Constant.PLATECOLORID)); // 车牌颜色 30

				strBuf.append(":");

				strBuf.append(app.get(Constant.VEHICLENO)); // 车牌号 31

				strBuf.append(":");

				strBuf.append(app.get(Constant.COMMDR)); // 手机号 32

				strBuf.append(":");

				strBuf.append(app.get(Constant.TID)); // 终端ID 33

				strBuf.append(":");

				strBuf.append(Constant.EMPTY); // 终端型号 34

				strBuf.append(":");

				strBuf.append(Constant.EMPTY); // 驾驶员姓名 35

				strBuf.append(":");

				strBuf.append(Constant.EMPTY); // 所属组织 36

				strBuf.append(":");

				strBuf.append(Constant.EMPTY); // 车队id 37

				strBuf.append(":");

				strBuf.append(Constant.EMPTY); // 企业id 38

				strBuf.append(":");

				if (null != app.get(Constant.OEMCODE)) {
					strBuf.append(app.get(Constant.OEMCODE)); // OEMCOED 39
				}
				strBuf.append(":");

				strBuf.append(System.currentTimeMillis()); // 系统时间40
				strBuf.append(":");

				strBuf.append(1); // 是否在线41

				strBuf.append(":");

				strBuf.append(0); // 是否有效 42

				strBuf.append(":");

				strBuf.append(1); // MSGID 43

				strBuf.append(":");

				jedis.set(vid, strBuf.toString());
			}
		}catch(JedisException e){
			jedis = null;
			jedis = RedisConnectionPool.reConnectionReturnJedis();
			jedis.select(6); 
		}catch(Exception ex){
			jedis = null;
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(6); 
			logger.error("更新轨迹缓存获取数据超时异常:"+ ex.getMessage()+ app.get(Constant.COMMAND) +",vid:"+ app.get(Constant.VID),ex);
		}
		
	}



	/*****************************************
	 * <li>描        述：设置非法轨迹信息 		</li><br>
	 * <li>时        间：2013-9-16  下午5:08:14	</li><br>
	 * <li>参数： @param isPvalid
	 * <li>参数： @param string
	 * <li>参数： @param msgid			</li><br>
	 * @param redisStorageCache 
	 * TODO
	 *****************************************/
	public void setTrackisPValidInfo(Map<String, String> app, String isPvalid, String vid) {
		try{
			if(jedis == null  || (!jedis.isConnected())){
				jedis = RedisConnectionPool.getJedisConnection();
				jedis.select(6); 
			}
			String trackInfo = jedis.get(vid); 
			if(null != trackInfo){
				String[] array = trackInfo.split(":", 45);
				if(array.length == 45){
					String basestatus = app.get(Constant.N8);// 基本状态
					if(basestatus == null){
						basestatus = "0";
					}
					array[23] = basestatus;
					array[40] = String.valueOf(System.currentTimeMillis());
					array[41] = "1";
					array[42] = isPvalid;
					StringBuffer sb  = new StringBuffer(512);
					for(int i = 0; i < array.length; i++){
						sb.append(array[i]).append(":");
					}
					String newTrack = sb.substring(0, sb.length() -1);
					jedis.set(vid, newTrack);
				} else {
					logger.error("非法位置信息:" + trackInfo);
				}
				
			}
		}catch(JedisException e){
			jedis = null;
			jedis = RedisConnectionPool.reConnectionReturnJedis();
			jedis.select(6); 
		}catch(Exception ex){
			jedis = null;
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(6); 
			logger.error("更新非法轨迹缓存获取数据超时异常:"+ ex.getMessage()+ app.get(Constant.COMMAND) +",vid:"+ app.get(Constant.VID),ex);
		}
	}


	/*****************************************
	 * <li>描        述：更新redis上下线状态 		</li><br>
	 * <li>时        间：2013-9-25  下午6:45:51	</li><br>
	 * <li>参数： @param packet			</li><br>
	 * TODO
	 *****************************************/
	public void setOffLine(String status,Long sysTime, String msgid, String vid){
		try{
			if(jedisOffLine == null){
				jedisOffLine = RedisConnectionPool.getJedisConnection();
				jedisOffLine.select(6); 
			}
			String trackInfo = jedisOffLine.get(vid);
			if(null != trackInfo){
				String[] array = trackInfo.split(":", 45);
				if(array.length == 45){
					if(status.equals("0")){ // 离线
						array[4]  = "0";  	// GPS速度
						array[17] = "0";  	// VSS速度
//						array[23] = "0";  	// 基本状态位
					}
					array[40] = String.valueOf(System.currentTimeMillis()); // 系统时间
					array[41] = status;   // 是否在线
					StringBuffer sb  = new StringBuffer(512);
					for(int i = 0; i < array.length; i++){
						sb.append(array[i]).append(":");
					}
					String newTrack = sb.substring(0, sb.length() -1);
					jedisOffLine.set(vid, newTrack);
				} else {
					logger.error("非法位置信息:" + trackInfo);
				}
			}
		}catch(JedisException e){
			jedisOffLine = null;
			jedisOffLine = RedisConnectionPool.reConnectionReturnJedis();
			jedisOffLine.select(6); 
		}catch(Exception ex){
			jedisOffLine = null;
			jedisOffLine = RedisConnectionPool.getJedisConnection();
			jedisOffLine.select(6); 
			logger.error("更新车辆上下线状态获取数据超时异常:"+ ex.getMessage() ,ex);
		}
	}

	/*****************************************
	 * <li>描        述：存储上下线提示 		</li><br>
	 * <li>时        间：2013-10-28  下午8:01:10	</li><br>
	 * <li>参数： @param motorcade
	 * <li>参数： @param prompt			</li><br>
	 * TODO
	 *****************************************/
	public void saveOnOffStatusPrompt(String motorcadeKey, String prompt) {
		try{
			if(jedisOnOffPrompt == null){
				jedisOnOffPrompt = RedisConnectionPool.getJedisConnection();
				jedisOnOffPrompt.select(7); 
			}
			if(motorcadeKey != null && motorcadeKey.length() > 0){
				jedisOnOffPrompt.setex(motorcadeKey, 60, prompt);
			}
		}catch(JedisException e){
			jedisOnOffPrompt = null;
			jedisOnOffPrompt = RedisConnectionPool.reConnectionReturnJedis();
			jedisOnOffPrompt.select(7); 
		}catch(Exception ex){
			jedisOnOffPrompt = null;
			jedisOnOffPrompt = RedisConnectionPool.getJedisConnection();
			jedisOnOffPrompt.select(7);  
			logger.error("更新车辆上下线提示获取数据超时异常:"+ ex.getMessage() ,ex);
		}
	}
	/**
	 * 缓存车辆站点信息
	 * @param stationMap 站点信息(KEY:vid;VALUE:站点信息)
	 * TODO
	 */
	public static void saveVehicleStationInfo(Map<String, String> stationMap) {
		Jedis jedis = null;
		boolean connection = true;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(3);
			jedis.hmset("VEHICLE_STATION", stationMap);
		}catch(JedisException e){
			RedisConnectionPool.reConnection();
			connection = false;
			logger.error("缓存车辆站点信息Jedis异常:"+ e.getMessage() ,e);
		}catch(Exception ex){
			logger.error("缓存车辆站点信息异常:"+ ex.getMessage() ,ex);
		}finally{
			if(jedis != null && connection){ 
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
	/**
	 * 获取车辆所有线路站点信息
	 * @param vid
	 * @return
	 * TODO
	 */
	public static List<StationJson> getVehicleStationList(String vid) {
		Jedis jedis = null;
		boolean connection = true;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(3);
			String json = jedis.hget("VEHICLE_STATION", vid);
			return JSON.parseArray(json, StationJson.class);
		}catch(JedisException e){
			RedisConnectionPool.reConnection();
			connection = false;
			logger.error("获取车辆站点信息Jedis异常:"+ e.getMessage() ,e);
			return null;
		}catch(Exception ex){
			logger.error("获取车辆站点信息异常:"+ ex.getMessage() ,ex);
			return null;
		}finally{
			if(jedis != null && connection){ 
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
	/**
	 * 获取车辆站点信息
	 * @param vid
	 * @return
	 * TODO
	 */
	public static StationJson getVehicleStationInfo(String vid) {
		Jedis jedis = null;
		boolean connection = true;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(3);
			String json = jedis.hget("VEHICLE_STATION", vid);
			if(json == null){
				return null;
			}
			return JSON.parseObject(json, StationJson.class);
		}catch(JedisException e){
			RedisConnectionPool.reConnection();
			connection = false;
			logger.error("获取车辆站点信息Jedis异常:"+ e.getMessage() ,e);
			return null;
		}catch(Exception ex){
			logger.error("获取车辆站点信息异常:"+ ex.getMessage() ,ex);
			return null;
		}finally{
			if(jedis != null && connection){ 
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
	/**
	 * 缓存线路关联车辆表
	 * @param lineVehicle
	 * TODO
	 */
	public static void saveLineVehicleMap(Map<String, String> lineVehicle) {
		Jedis jedis = null;
		boolean connection = true;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(3);
			jedis.hmset("LINE_VEHICLE", lineVehicle); 
		}catch(JedisException e){
			RedisConnectionPool.reConnection();
			connection = false;
			logger.error("缓存线路关联车辆表异常:"+ e.getMessage() ,e);
		}catch(Exception ex){
			logger.error("缓存线路关联车辆表异常:"+ ex.getMessage() ,ex);
		}finally{
			if(jedis != null && connection){ 
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
	/**
	 * 获取线路关联车辆表所有KEYS
	 * @return
	 * TODO
	 */
	public static Set<String> getLineVehicleMapKeys() {
		Jedis jedis = null;
		boolean connection = true;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(3);
			return jedis.hkeys("LINE_VEHICLE"); 
		}catch(JedisException e){
			RedisConnectionPool.reConnection();
			connection = false;
			logger.error("获取线路关联车辆表所有KEYSJedis异常:"+ e.getMessage() ,e);
			return null;
		}catch(Exception ex){
			logger.error("获取线路关联车辆表所有KEYS异常:"+ ex.getMessage() ,ex);
			return null;
		}finally{
			if(jedis != null && connection){ 
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
	/**
	 * 删除线路关联车辆表无效数据
	 * @param oldSets
	 */
	public static void removeLineVehicleKeys(Set<String> oldSets) {
		Jedis jedis = null;
		boolean connection = true;
		try{
			if(oldSets != null && oldSets.size() > 0){
				String[] keys = oldSets.toArray(new String[oldSets.size()]); 
				jedis = RedisConnectionPool.getJedisConnection();
				jedis.select(3);
				jedis.hdel("LINE_VEHICLE", keys);
			}
		}catch(JedisException e){
			RedisConnectionPool.reConnection();
			connection = false;
			logger.error("删除线路关联车辆表无效数据Jedis异常:"+ e.getMessage() ,e);
		}catch(Exception ex){
			logger.error("删除线路关联车辆表无效数据异常:"+ ex.getMessage() ,ex);
		}finally{
			if(jedis != null && connection){ 
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
	/**
	 * 获取站点历史记录
	 * @param vid
	 * @return
	 */
	public static StationHistory getStationHistory(String vid) {
		Jedis jedis = null; 
		boolean connection = true;
		try{
			if(vid != null){
				jedis = RedisConnectionPool.getJedisConnection();
				jedis.select(3);
				String sh = jedis.get("STATION_HISTORY_" + vid);
				return JSON.parseObject(sh, StationHistory.class);
			}
			return null;
		}catch(JedisException e){
			RedisConnectionPool.reConnection();
			connection = false;
			logger.error("获取站点历史记录Jedis异常:"+ e.getMessage() ,e);
			return null;
		}catch(Exception ex){
			logger.error("获取站点历史记录异常:"+ ex.getMessage() ,ex);
			return null;
		}finally{
			if(jedis != null && connection){ 
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
	/**
	 * 存储站点历史记录
	 * @param vid
	 * @param sh
	 */
	public static void saveStationHistory(String vid, StationHistory sh) {
		Jedis jedis = null; 
		boolean connection = true;
		try{
			if(sh != null){
				jedis = RedisConnectionPool.getJedisConnection();
				jedis.select(3);
				jedis.set("STATION_HISTORY_" + vid, JSON.toJSONString(sh)); 
			}
		}catch(JedisException e){
			RedisConnectionPool.reConnection();
			connection = false;
			logger.error("存储站点历史记录Jedis异常:"+ e.getMessage() ,e);
		}catch(Exception ex){
			logger.error("存储站点历史记录异常:"+ ex.getMessage() ,ex);
		}finally{
			if(jedis != null && connection){ 
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
	/**
	 * 删除站点历史记录
	 * @param vid
	 */
	public static Long removeStationHistory(String vid) {
		Jedis jedis = null; 
		boolean connection = true;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(3);
			return jedis.del("STATION_HISTORY_" + vid); 
		}catch(JedisException e){
			RedisConnectionPool.reConnection();
			connection = false;
			logger.error("删除站点历史记录Jedis异常:"+ e.getMessage() ,e);
			return null;
		}catch(Exception ex){
			logger.error("删除站点历史记录异常:"+ ex.getMessage() ,ex);
			return null;
		}finally{
			if(jedis != null && connection){ 
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
	}
}
