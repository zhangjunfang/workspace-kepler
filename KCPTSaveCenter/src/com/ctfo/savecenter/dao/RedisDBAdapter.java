package com.ctfo.savecenter.dao;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.redis.pool.JedisConnectionPool;
import com.ctfo.savecenter.Constant;

public class RedisDBAdapter {
	private static final Logger logger = LoggerFactory.getLogger(RedisDBAdapter.class);
	private Jedis jdTrack = null;
	private Jedis jdValid = null;
	private Jedis jdOffLine = null;
	//判断里程只能是数字
	public RedisDBAdapter(){
//		jdTrack = RedisConnectionPool.getJedisConnection();
		jdTrack = JedisConnectionPool.getJedisConnection();
		jdTrack.select(6); // 选择数据库
		
//		jdValid = RedisConnectionPool.getJedisConnection();
		jdValid = JedisConnectionPool.getJedisConnection();
		jdValid.select(6);
		
//		jdOffLine = RedisConnectionPool.getJedisConnection();
		jdOffLine = JedisConnectionPool.getJedisConnection();
		jdOffLine.select(6);
		
	}
	
	/*****************************************
	 * <li>描        述：初始化车辆报警代码 		</li><br>
	 * <li>参数：  		</li><br>
	 * <li>时        间：2013-7-20  下午11:39:35	</li><br>
	 * 
	 *****************************************/
	public synchronized void initVehicleAlarmCode() {
		if(TempMemory.vehicleStatusMap.size() < 1){
			long start = System.currentTimeMillis();
			logger.info("--初始化车辆报警编码开始。。。");
			try{
				//取出所以VID集合
				Set<String> vids = jdTrack.keys("*");
				String trackStr = null;
				String head = null;
				for(String vid : vids ){
	//				获取轨迹文件字符串
					trackStr = jdTrack.get(vid);
					if(trackStr == null){
						continue;
					}
	//				取报警编号到map中
					int i = StringUtils.ordinalIndexOf(trackStr, ":", 7);
					int w = StringUtils.ordinalIndexOf(trackStr, ":", 8);
					if(i > 10 && w > 10){
						head = trackStr.substring(i+1,w);
						Map<String, String> map = new HashMap<String, String>();
						map.put(Constant.ALARMCODE, head);
						TempMemory.vehicleStatusMap.put(vid, map);
					}
				}
				logger.info("--初始化车辆报警编码完成!耗时(ms):"+(System.currentTimeMillis() - start));
			}catch(Exception e){
				logger.warn("---init alarmCode warn--初始化车辆报警代码异常:"+e.getMessage(),e);
				JedisConnectionPool.returnJedisConnection(jdTrack);
				jdTrack = JedisConnectionPool.getJedisConnection();
				jdTrack.select(6); // 选择数据库
			}
		}
	}

	public void setTrackInfo(Map<String, String> app){
		String vid = app.get(Constant.VID);
		if(vid == null){
			logger.warn("setTrackInfo---vid==null:"+app.get(Constant.COMMAND)); 
			return;
		}
		String utcStr = app.get(Constant.UTC);
		if(utcStr==null){
			logger.warn("setTrackInfo---utc==null:"+app.get(Constant.COMMAND)); 
			return;
		}
		Integer gpsSpeed = 0;
		if(app.get("3") != null){
			gpsSpeed = Integer.parseInt(app.get("3"));
		}
		String lonStr = app.get("1");
		String latStr = app.get("2");
		long lon = 0;
		long lat = 0;
		if(lonStr != null){
			lon = Long.parseLong(lonStr);
		}
		if(latStr != null){
			lat = Long.parseLong(latStr);
		}
		String head = app.get("5");
		//String gpsTime = app.get("4");
//		long utc = Long.parseLong(app.get(Constant.UTC));
		long mapLon = Long.parseLong(app.get(Constant.MAPLON));
		long mapLat = Long.parseLong(app.get(Constant.MAPLAT));
		String alarmCode = app.get("20");
		if(alarmCode != null){
			if (app.get("21")!=null) {
				alarmCode = alarmCode.substring(0, alarmCode.length() - 1)	+ app.get("21");
			}
			alarmCode = alarmCode.replaceAll("\\,\\,", ",");
		}
		String basestatus = app.get("8");// 基本状态
		String extendstatus = app.get("500");// 扩展状态
		String msgid=app.get(Constant.MSGID);
		
		String v = null;
		StringBuffer strBuf = new StringBuffer("");
		try{
			
			if(jdTrack.exists(vid)){
				v = jdTrack.get(vid);
				String[] arr = StringUtils.splitPreserveAllTokens(v, ":");
				
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
				
//				if (null != app.get("9") && !"-1".equals(app.get("9"))) { // 累计里程 22
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
				
				strBuf.append(msgid); //  MSGID 43
				
				strBuf.append(":");
				
				strBuf.append(arr[44]); // 车队名称 44
				
				jdTrack.set(vid, strBuf.toString());
			}else{
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
					
				}
				strBuf.append(":");
				
				if (null != app.get("216")) { // 瞬时油耗 // 9
					strBuf.append(app.get("216"));
				}
				strBuf.append(":");
				
				if (null != app.get("215") && !"".equals(app.get("215"))) { // 机油压力 // 10
					strBuf.append(app.get("215"));
				}
				strBuf.append(":");
				
				if (null != app.get("508")) { // 机油温度（随位置汇报上传） 11
					strBuf.append(app.get("508"));
				}
				strBuf.append(":");
				
				if (null != app.get("504") && !"".equals(app.get("504"))) { // 油门踏板位置，1bit=0.4%，0=0%（随位置汇报上传） 12
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
//				if (null != app.get("9")) {
//					strBuf.append(app.get("9"));
//				}
				if (null != app.get("9")){ // 累计里程 22
					strBuf.append(app.get("9"));	
				}
				strBuf.append(":");
				
				if(null != basestatus){ //基本状态位 23
					strBuf.append(basestatus);
				}
				strBuf.append(":");
				
				if(null != extendstatus){ //扩展状态位 24
					strBuf.append(extendstatus);
				}
	
				strBuf.append(":");
				
				strBuf.append(app.get(Constant.SPEEDFROM)); // 25
				strBuf.append(":");
			
				if(null != app.get("219")){ //计量仪油耗 // 26
					strBuf.append(app.get("219"));
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
				
				strBuf.append(app.get(Constant.PLATECOLORID)); // 车牌颜色 30
				
				strBuf.append(":");
				
				strBuf.append(app.get(Constant.VEHICLENO)); // 车牌号 31
				
				strBuf.append(":");
				
				strBuf.append(app.get(Constant.COMMDR)); // 手机号 32
				
				strBuf.append(":");
				
				strBuf.append(app.get(Constant.TID)); // 终端ID 33
				
				strBuf.append(":");
				
				strBuf.append(""); // 终端型号 34
				
				strBuf.append(":");
				
				strBuf.append(""); // 驾驶员姓名 35
				
				strBuf.append(":");
				
				strBuf.append("");  //所属组织 36
				
				strBuf.append(":");
				
				strBuf.append(""); // 车队id 37
				
				strBuf.append(":");
				
				strBuf.append(""); // 企业id 38
				
				strBuf.append(":");
				
				if(null != app.get(Constant.OEMCODE)){
					strBuf.append(app.get(Constant.OEMCODE)); // OEMCOED 39
				}else{
					strBuf.append(""); // OEMCOED 39
				}
				strBuf.append(":");
				
				strBuf.append(System.currentTimeMillis()); // 系统时间40
				strBuf.append(":");
				
				strBuf.append(1); //  是否在线41
				
				strBuf.append(":");
				
				strBuf.append(0); //  是否有效 42
				
				strBuf.append(":");
				
				strBuf.append(msgid); //  MSGID 43
				
				strBuf.append(":");
				
				strBuf.append(""); // 车队名称 44
				
				jdTrack.set(vid, strBuf.toString());
			}
		}catch(Exception ex){
			logger.error("更新轨迹缓存异常:"+ ex.getMessage()+ app.get(Constant.COMMAND) +",vid:"+ app.get(Constant.VID),ex); 
			
			JedisConnectionPool.returnJedisConnection(jdTrack);
			jdTrack = JedisConnectionPool.getJedisConnection();
			jdTrack.select(6); // 选择数据库
		}finally{
			strBuf.delete(0, strBuf.length());
			strBuf = null;
		}
	}
	
	/*****
	 * 更新无效状态   JedisConnectionServer.updateTrackValidStatus
	 * @param isValid
	 * @param vid
	 */
	public void setTrackisPValidInfo(String isValid, String vid,String msgId){
		
//		RedisAdapter.updateTrackValidStatus(String.valueOf(isValid), String.valueOf(vid), msgId, baseStatus);
		try{
			String trackInfo = jdValid.get(String.valueOf(vid));
			if(null != trackInfo){
//				String teamName = trackInfo.substring(trackInfo.lastIndexOf(":")+1,trackInfo.length() );
//				trackInfo = trackInfo.substring(0, trackInfo.lastIndexOf(":")); // 移除车队名称
//				trackInfo = trackInfo.substring(0, trackInfo.lastIndexOf(":")); // 移除MSGID
//				trackInfo = trackInfo.substring(0, trackInfo.lastIndexOf(":")); // 移除是否有效
//				trackInfo = trackInfo.substring(0, trackInfo.lastIndexOf(":")); // 移除是否在线
//				trackInfo = trackInfo.substring(0, trackInfo.lastIndexOf(":")); // 移除是系统时间
//				trackInfo = trackInfo + ":" + System.currentTimeMillis() + ":1:" + isValid + ":" + msgId + ":" + teamName;
//				trackInfo = trackInfo + ":" + System.currentTimeMillis() + ":" + "1:"  + isValid + ":" + msgId;
				//更新基本状态
				String head = trackInfo.substring(0,StringUtils.ordinalIndexOf(trackInfo, ":", 23) +1);
				String end = trackInfo.substring(StringUtils.ordinalIndexOf(trackInfo, ":", 24));
				String newStr = head+32+end;
				String start = newStr.substring(0,StringUtils.ordinalIndexOf(newStr, ":", 40) +1);
				String over = newStr.substring(StringUtils.ordinalIndexOf(newStr, ":", 42));
				String newTrack = start + System.currentTimeMillis() +":"+1 + over;
				jdValid.set(String.valueOf(vid), newTrack); 
			}
		}catch(Exception ex){
			logger.error("更新无效状态异常:"+ ex.getMessage(),ex);
			JedisConnectionPool.returnBrokenResource(jdValid);
			JedisConnectionPool.returnJedisConnection(jdValid);
			jdValid = JedisConnectionPool.getJedisConnection();
			jdValid.select(6);
		}
	}
	
	/*****
	 * 更新车辆上下线状态信息    JedisConnectionServer.updateOnlineAndOfflineStatus
	 * @param packet
	 */
	public void setOffLine(Map<String, String> packet){
		String parm = packet.get("18");
		String parms[] = parm.split("/");
		String vid = packet.get(Constant.VID);
		String msgid = packet.get(Constant.MSGID);
		if (parms.length == 4) {
			try{
				String trackInfo = jdOffLine.get(String.valueOf(vid));
				if(null != trackInfo){
					String isOnline = parms[0];
					String teamName = trackInfo.substring(trackInfo.lastIndexOf(":")+1,trackInfo.length() );
					trackInfo = trackInfo.substring(0, trackInfo.lastIndexOf(":")); // 移除车队名称
					trackInfo = trackInfo.substring(0, trackInfo.lastIndexOf(":")); // 移除MSGID
					String isValid = trackInfo.substring(trackInfo.lastIndexOf(":")+1,trackInfo.length() );
					trackInfo = trackInfo.substring(0, trackInfo.lastIndexOf(":")); // 移除是否有效
					trackInfo = trackInfo.substring(0, trackInfo.lastIndexOf(":")); // 移除是否在线
					trackInfo = trackInfo + ":" + isOnline + ":" + isValid + ":" + msgid + ":" + teamName;
					jdOffLine.set(vid, trackInfo);
					
				}
			}catch(Exception ex){
				logger.error("更新车辆上下线状态信息异常:"+ ex.getMessage(),ex);
				JedisConnectionPool.returnJedisConnection(jdOffLine);
				jdOffLine = JedisConnectionPool.getJedisConnection();
				jdOffLine.select(6);
			}
		}
	}
	
	/*****
	 * 启动存储服务，从REDIS获取最近一次报警列表
	 * @param vid
	 * @return
	 */
	public Map<String, String> getLastAlarmCode(String vid){
		Jedis jedis =null;
		try{
			jedis = JedisConnectionPool.getJedisConnection();
//			jedis = RedisConnectionPool.getJedisConnection();
			if(jedis.exists(vid)){
				
				String value = jedis.get(vid);
				String[] arr = value.split(":");
				if(null != arr[7]){
					Map<String, String> map = new HashMap<String, String>();
					map.put(Constant.VID, vid);
					map.put(Constant.ALARMCODE, arr[7]);
					return map;
				}
			}
		}catch(Exception ex){
			JedisConnectionPool.returnBrokenResource(jedis);
			logger.error("Connection redis server time out:"+ ex.getMessage());
		}finally{
//			RedisConnectionPool.returnJedisConnection(jedis);
			JedisConnectionPool.returnJedisConnection(jedis);;
		}
		return null;
	}
	
}
