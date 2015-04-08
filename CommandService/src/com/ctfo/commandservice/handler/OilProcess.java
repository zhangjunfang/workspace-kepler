package com.ctfo.commandservice.handler;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.alibaba.fastjson.JSON;
import com.ctfo.commandservice.dao.RedisConnectionPool;
import com.ctfo.commandservice.model.OilInfo;
import com.ctfo.commandservice.model.OilSpill;
import com.ctfo.commandservice.model.OracleProperties;
import com.ctfo.commandservice.service.OracleService;
import com.ctfo.commandservice.util.Base64_URl;
import com.ctfo.commandservice.util.Constant;
import com.ctfo.commandservice.util.Converser;
import com.ctfo.commandservice.util.DateTools;
/**
 *	驾驶员信息处理线程
 */
public class OilProcess extends Thread {
	/**	日志	*/
	private static final Logger logger = LoggerFactory.getLogger(OilProcess.class);
	/**	Driver队列	*/ 
	private ArrayBlockingQueue<Map<String, String>> oilQueue = new ArrayBlockingQueue<Map<String, String>>(10000);
	/** 批量提交数量	 */
	private int index;
	/** 日志间隔 （默认10秒）	 */
	private int interval = 10000;
	/** 最近处理时间	 */
	private long lastTime = System.currentTimeMillis();

	
	public OilProcess(OracleProperties oracleProperties, int threadId) throws Exception { 
		setName("OilProcess-[" + threadId+"]"); 
//		sql_saveOilChanged = ConfigLoader.sqlParamMap.get("sql_saveOilChanged");
//		sql_saveOilInfo = ConfigLoader.sqlParamMap.get("sql_saveOilInfo");
//		sql_saveStealingOilAlarm = ConfigLoader.sqlParamMap.get("sql_saveStealingOilAlarm");
	}

	/**
	 * 业务处理方法
	 */
	public void run(){
		while (true) {
			try {
				Map<String, String> map = oilQueue.take();
				index++;
//				处理油量透传信息
				processOil(map);
				
				long currentTime = System.currentTimeMillis();
				if(currentTime - lastTime > interval){
					int size = getQueueSize();
					int intervalTime = interval/1000;
					logger.info("OilProcess---{}s处理[{}]条, 排队:[{}]条", intervalTime, index, size);
					lastTime = System.currentTimeMillis();
					index = 0;
				}
			} catch (Exception e) {
				logger.error("Oil信息处理线程错误:" + e.getMessage(), e);
			}
		}
	}
	
	/**	获得队列长度	*/
	private int getQueueSize() {
		return oilQueue.size();
	}
	/**
	 * 解析油量透传信息
	 * @param map
	 */
	private void processOil(Map<String, String> map) {
		try {
			long start = System.currentTimeMillis();
			String type = "";
			String value = map.get("90");
			String vid = map.get(Constant.VID);
			logger.debug("车辆[{}]收到油量信息[{}]", vid, value);
//			获取油箱油量信息对象
			OilInfo oilInfo = getOilBase(value);
			if(oilInfo != null){
				oilInfo.setVid(vid); 
				if(oilInfo.getStatus().equals("10")){			// 加油
					type = "加油";
//					Oracle中存储油量变化信息
					OracleService.saveOilChanged(oilInfo);
//					处理偷漏油信息 - 如果有偷漏油报警，遇到加油就解除报警
					String oilSpillKey = "SD_OIL_SPILL_" + oilInfo.getVid();
					disarmAlarm(oilSpillKey);
					logger.debug("车辆[{}]收到加油信息信息[{}]", vid, value);
				} else if(oilInfo.getStatus().equals("01")){	//	偷漏油 
					type = "偷漏油";
//					缓存偷漏油信息
					cacheOilSpill(oilInfo);
					logger.debug("车辆[{}]收到偷漏油信息[{}], 缓存偷漏油信息", vid, value);
				} else if(oilInfo.getStatus().equals("5")){		//	油量标定
					type = "油量标定";
					oilInfo.setSeq(map.get(Constant.SEQ));
//					存储油量标定信息
					OracleService.saveOilInfo(oilInfo);
					logger.debug("车辆[{}]收到油量标定信息SEQ[{}], 缓存油量标定信息", vid, oilInfo.getSeq());
				} else if(oilInfo.getStatus().equals("00")){
					type = "油量信息";
					if(oilInfo != null && oilInfo.getLon() == 0 && oilInfo.getLat() == 0 && oilInfo.getElevation() == 0){
					}else{
	//					缓存油量信息
						cacheOilChanged(oilInfo);
	//					处理偷漏油信息
						processOilSpill(oilInfo);
					}
				}
			} else {
				type = "解析异常";
				logger.error("解析油箱油量透传信息异常:{}", value);
			} 
			logger.debug("处理油箱油量透传[{}]信息完成, 耗时:[{}]ms, 透传内容[{}]", type, (System.currentTimeMillis() - start), value);
		} catch (Exception e) {
			logger.error("处理油量透传信息异常:" + e.getMessage(), e);
		}
	}
	/**
	 * 处理偷漏油报警信息
	 * @param oilInfo
	 */
	private void processOilSpill(OilInfo oilInfo) {
		try {
			// 1. 有偷漏油报警就进行判断
			String oilSpillKey = "SD_OIL_SPILL_" + oilInfo.getVid();
			String oilStr = getOilSpillCache(oilSpillKey);
			logger.debug("查询缓存中的油量报警:[{}]", oilStr); 
			String tips = "";
			if (StringUtils.isNotBlank(oilStr)) {
				OilSpill oilSpill = JSON.parseObject(oilStr, OilSpill.class);
//				 2. 油量继续减少说明有报警可能
				// long currentTime = DateTools.getStringToDate(oilInfo.getTime());
				// long spillTime =  DateTools.getStringToDate(oilSpill.getTime());
					if (oilSpill.getBeferOilSpillOil() > oilInfo.getOilAllowance()) { // 当前油量比偷漏油报警后油量还在减少
						tips = "偷漏油发生后,油量还在减少。";
//              2.1 里程差在14公里内说明是真偷漏油
						int mileage = getMileage(oilInfo.getVid()); // 获取当前里程
						if ((mileage - oilSpill.getMileage()) < 140) {
							OilInfo spill = oilSpill.getOilInfo();
							long endUtc = DateTools.getStringToDate(oilInfo.getTime());
							OracleService.saveStealingOilAlarm(oilSpill, endUtc);
							OracleService.saveOilChanged(spill);
							// 解除报警
							disarmAlarm(oilSpillKey);
							tips += (" 当前里程["+mileage+"], 缓存里程["+oilSpill.getMileage()+"],里程差在14公里内, 存储偷漏油报警["+spill.getStatus()+"], 并删除报警缓存。 redis4库:" + oilSpillKey);
						} else {
//				 2.2 里程差超过14公里，说明是误报，解除报警
							// 解除报警
							disarmAlarm(oilSpillKey);
							tips += (" 当前里程["+mileage+"], 缓存里程["+oilSpill.getMileage()+"],里程差超过14公里，说明是误报，解除报警  redis4库:" + oilSpillKey);
						}
					} else {
//				油量没有减少，且超过14公里，属于误报，解除报警
						int mileage = getMileage(oilInfo.getVid()); // 获取当前里程
						if ((mileage - oilSpill.getMileage()) > 140) {
							// 解除报警
							disarmAlarm(oilSpillKey);
							tips += ("油量没有减少，且超过14公里，属于误报，解除报警  redis4库:" + oilSpillKey);
						} else {
							tips += ("油量没有减少，且在14公里内，不予处理，继续观察  ");
						}
					}
				logger.debug("车辆[{}] 处理偷漏油情况:[{}]", oilInfo.getVid(), tips);
			} else {
				logger.debug("车辆[{}]查询偷漏油报警信息[空]", oilInfo.getVid());
			}
		} catch (Exception e) {
			logger.error("处理偷漏油报警信息异常:" + e.getMessage(), e);
		}

	}
	/**
	 * 解除报警
	 * @param oilKey
	 */
	private void disarmAlarm(String oilKey) {
		Jedis jedis = null;
		try {
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(4);
			jedis.del(oilKey); 
		} catch (Exception e) {
			logger.error("解除报警异常:" + e.getMessage(), e);
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis);
			}
		} finally {
			if(jedis != null){
				RedisConnectionPool.returnJedisConnection(jedis); 
			}
		}
	}

	/**
	 * 缓存偷漏油信息
	 * @param oilInfo
	 */
	private void cacheOilSpill(OilInfo oilInfo) {
		Jedis jedis = null;
		try {
//			记录偷油发生后的油量 - 当前油量
			OilSpill oilSpill = new OilSpill();
			oilSpill.setLon(oilInfo.getLon());
			oilSpill.setLat(oilInfo.getLat());
			oilSpill.setElevation(oilInfo.getElevation());
			oilSpill.setDirection(oilInfo.getDirection());
			oilSpill.setSpeed(oilInfo.getSpeed());
			oilSpill.setTime(oilInfo.getTime());
			oilSpill.setVid(oilInfo.getVid());
			oilSpill.setBeferOilSpillOil(oilInfo.getOilAllowance()); 
//			记录上上次油量变化的油量 - 上上次油量变化里程
			String oilKey = "SD_OIL_" + oilInfo.getVid();
			int mileage = getFirstMileage(oilKey);
			oilSpill.setMileage(mileage);
			oilSpill.setOilInfo(oilInfo); 
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(4);
			jedis.set("SD_OIL_SPILL_" + oilInfo.getVid(), JSON.toJSONString(oilSpill)); 
		} catch (Exception e) {
			logger.error("缓存偷漏油信息异常:" + e.getMessage(), e);
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis);
			}
		} finally {
			if(jedis != null){
				RedisConnectionPool.returnJedisConnection(jedis); 
			}
		}

		
	}
	/**
	 * 获取上上次里程值
	 * @param oilKey
	 * @return
	 */
	private int getFirstMileage(String oilKey) {
		try {
			String oilStr = getOilCache(oilKey);
			if(oilStr != null){
				String[] oilArray = StringUtils.split(oilStr, "_");
				if(oilArray.length == 2){ // 正常情况
					String[] array = StringUtils.split(oilArray[0], ":");
					if(array.length == 2 && StringUtils.isNumeric(array[1])){ // 必须有油量、里程数据
						return Integer.parseInt(array[1]);
					}
				} else if(oilArray.length == 1) { // 只记录过一次油量数据
					String[] array = StringUtils.split(oilArray[0], ":");
					if(array.length == 2 && StringUtils.isNumeric(array[1])){ // 必须有油量、里程数据
						return Integer.parseInt(array[1]);
					}
				}
			} 
			return 0;
		} catch (Exception e) {
			logger.error("获取车辆油量缓存信息异常:" + e.getMessage(), e);
			return 0;
		} 
	}

	/**
	 * 缓存每次加油量
	 * @param oilInfo
	 */
	private void cacheOilChanged(OilInfo oilInfo) { 
		try {
			boolean result = false;
//			如果油量有变化就存储当前油量值
			String oilKey = "SD_OIL_" + oilInfo.getVid();
			String oilStr = getOilCache(oilKey);
			if(oilStr != null){
				String[] oilArray = StringUtils.split(oilStr, "_");
				if(oilArray.length == 2){ // 正常情况
					result = processOilCache(oilInfo.getVid(), oilKey, oilArray[1], oilInfo.getOilAllowance());
				} else if(oilArray.length == 1) { // 只记录过一次油量数据
					result = processOilCache(oilInfo.getVid(), oilKey, oilArray[0], oilInfo.getOilAllowance());
				} else {// 没有记录过油量信息 
					result = saveOilCache(oilInfo.getVid(), oilKey, null, oilInfo.getOilAllowance());
				}
			} else {// 没有记录过油量信息
				result = saveOilCache(oilInfo.getVid(), oilKey, null, oilInfo.getOilAllowance());
			}
			logger.debug("缓存油量信息结果[{}], oilKey[{}], 当前油量[{}], 变化量[{}], 油量缓存信息[{}]", result, oilKey, oilInfo.getOilAllowance(), oilInfo.getOilChange(), oilStr );
		} catch (Exception e) {
			logger.error("缓存每次加油量异常:" + e.getMessage(), e);
		}
	}

	
	private boolean processOilCache(String vid, String oilKey, String oilStr, int currentOil) {
		String[] array = StringUtils.split(oilStr, ":");
		if(array.length == 2 && StringUtils.isNumeric(array[0])){ // 必须有油量、里程数据
			long oil = Long.parseLong(array[0]);
//			当前油量与上一次油量不能相同，才存储有变化的油量值
			if(currentOil - oil != 0){
				return saveOilCache(vid, oilKey, oilStr, currentOil);
			}
		}
		return false;
	}

	/**
	 * 获取油量缓存信息
	 * @param key
	 * @return
	 */
	private String getOilCache(String key) {
		Jedis jedis = null;
		try {
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(4);
			return jedis.get(key);
		} catch (Exception e) {
			logger.error("获取油量缓存信息异常:" + e.getMessage(), e);
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			return null;
		} finally {
			if(jedis != null){
				RedisConnectionPool.returnJedisConnection(jedis); 
			}
		}
	}
	/**
	 * 获取偷漏油缓存信息
	 * @param key
	 * @return
	 */
	private String getOilSpillCache(String key) {
		Jedis jedis = null;
		try {
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(4);
			return jedis.get(key);
		} catch (Exception e) {
			logger.error("获取偷漏油缓存异常:" + e.getMessage(), e);
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			return null;
		} finally {
			if(jedis != null){
				RedisConnectionPool.returnJedisConnection(jedis); 
			}
		}
	}
	/**
	 * 存储油量变化信息
	 * @param vid			车辆编号
	 * @param oilKey		车辆对应编号
	 * @param lastCache		最近一次缓存状态
	 * @param currentOil	当前油量
	 */
	private boolean saveOilCache(String vid, String oilKey, String lastCache, int currentOil) {
		Jedis jedis = null;
		try {
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(6);
			String cacheStr = jedis.get(vid);
			if(cacheStr != null ){
				String[] vehicle = StringUtils.splitPreserveAllTokens(cacheStr, ":");
				if(vehicle.length > 23){
					String mileage = vehicle[22]; // 获取累计里程
					if(StringUtils.isNumeric(mileage)){ 
						String value = null;
						if(lastCache == null){ // 首次缓存
							value = currentOil + ":" + mileage + "_";
						} else { 	
							value = lastCache + "_" + currentOil + ":" + mileage;
						}
						jedis.select(4);
						jedis.set(oilKey, value);
						return true;
					}
				}
			}
			return false;
		} catch (Exception e) {
			logger.error("存储油量、里程缓存异常:" + e.getMessage(), e);
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			return false;
		} finally {
			if(jedis != null){
				RedisConnectionPool.returnJedisConnection(jedis); 
			}
		}
	}
	/**
	 * 获取里程
	 * @param vid
	 * @return
	 */
	private int getMileage(String vid){
		Jedis jedis = null;
		try {
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(6);
			String cacheStr = jedis.get(vid);
			if(cacheStr != null ){
				String[] vehicle = StringUtils.splitPreserveAllTokens(cacheStr, ":");
				if(vehicle.length > 23){
					String mileage = vehicle[22]; // 获取累计里程
					if(StringUtils.isNumeric(mileage)){ 
						return Integer.parseInt(mileage);
					}
				}
			}
			return 0;
		}catch(Exception e){
			logger.error("获取里程异常:" + e.getMessage(), e);
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			return 0;
		} finally {
			if(jedis != null){
				RedisConnectionPool.returnJedisConnection(jedis); 
			}
		}
	}
	
	/**
	 * 解析油量数据
	 * @param value
	 * @return
	 */
	private OilInfo getOilBase(String value) {
		if(value == null || value.length() == 0){
			return null;
		}
		OilInfo oilInfo = new OilInfo();
		byte[] buf = Base64_URl.base64DecodeToArray(value);
		if(buf.length < 22){
			return null;
		}
		int locZspt = -1;
		// 透传类型
		locZspt += 1;
		// 协议版本号
		locZspt += 1;
		//纬度		
		byte latBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, latBytes, 0, 4);		
		int lattmp = Converser.bytes2int(latBytes);
		double lat = (lattmp*6)/10;
		oilInfo.setLat(Math.round(lat));
		
		locZspt += 4;
		//经度		
		byte lonBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, lonBytes, 0, 4);		
		int lontmp = Converser.bytes2int(lonBytes);		
		double lon = (lontmp*6)/10;
		oilInfo.setLon(Math.round(lon));
		
		locZspt += 4;
		//海拔高度		
		byte elevBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, elevBytes, 2, 2);		
		int elevtmp = Converser.bytes2int(elevBytes);		
		oilInfo.setElevation(elevtmp);
		
		locZspt += 2;
		//速度      WORD格式为什么还要new byte[4],本来可以new byte[2],
		//因为INT 类型是4个字节，所以为了避免两个字节强转出现异常,创建4个字节
		byte speedBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, speedBytes, 2, 2);		
		int speedtmp = Converser.bytes2int(speedBytes);		
		oilInfo.setSpeed(speedtmp);
		
		locZspt += 2;
		//方向				
		byte directionBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, directionBytes, 2, 2);		
		int direction = Converser.bytes2int(directionBytes);
		oilInfo.setDirection(direction); 
		
		locZspt += 2;
		//时间		
		byte timeBytes[] = new byte[6];		
		System.arraycopy(buf, locZspt, timeBytes, 0, 6);		
		String time = Converser.bcdToStr(timeBytes, 0, 6);	
		oilInfo.setTime(time); 
		
		locZspt += 6;
		//状态(0:油位正常 ; 1:偷油量提示 ; 2:加油提示 ; 3:偷油告警 ; 4:软件版本号 ; 5:参数设置查询 ;	)	
		byte statusBytes[] = new byte[1];		
		System.arraycopy(buf, locZspt, statusBytes, 0, 1);	
		int status = statusBytes[0] & 0xff;
		
		if(status < 3){
			String stateStr = Converser.hexTo2BCD(Converser.bytesToHexString(statusBytes));
			String state = stateStr.substring(6, stateStr.length());
			oilInfo.setStatus(state);
			locZspt += 1;
			//	燃油液位
			byte fuelLevelBytes[] = new byte[4];
			System.arraycopy(buf, locZspt, fuelLevelBytes, 3, 1);
			int fuelLevel = Converser.bytes2int(fuelLevelBytes);
			oilInfo.setFuelLevel(fuelLevel);
			
			locZspt += 3;
			//	变动油量
			byte oilChangeBytes[] = new byte[4];
			System.arraycopy(buf, locZspt, oilChangeBytes, 2, 2);
			byte oilChangeNewData[] = new byte[4];
			oilChangeNewData[0] = oilChangeBytes[0];
			oilChangeNewData[1] = oilChangeBytes[1];
			oilChangeNewData[2] = oilChangeBytes[3];
			oilChangeNewData[3] = oilChangeBytes[2];
			int oilChange = Converser.bytes2int(oilChangeNewData);
			oilInfo.setOilChange(oilChange); 

			locZspt += 2;
			//	当前油量
			byte oilAllowanceBytes[] = new byte[4];
			System.arraycopy(buf, locZspt, oilAllowanceBytes, 2, 2);
			// 定义新数组，调整后两个数组的位置
			byte oilAllowanceNewData[] = new byte[4];
			oilAllowanceNewData[0] = oilAllowanceBytes[0];
			oilAllowanceNewData[1] = oilAllowanceBytes[1];
			oilAllowanceNewData[2] = oilAllowanceBytes[3];
			oilAllowanceNewData[3] = oilAllowanceBytes[2];
			int oilAllowance = Converser.bytes2int(oilAllowanceNewData);
			oilInfo.setOilAllowance(oilAllowance);

			return oilInfo;
		} else if (status == 5) {
			oilInfo.setStatus("5"); 
			locZspt += 1;
			// 指令类别
			byte commandTypeBytes[] = new byte[1];
			System.arraycopy(buf, locZspt, commandTypeBytes, 0, 1);
			oilInfo.setCommandType(commandTypeBytes[0]); 
			
			locZspt += 1;
			// 标定油量
			byte oilCalibrationBytes[] = new byte[4];
			System.arraycopy(buf, locZspt, oilCalibrationBytes, 2, 2);
			byte oilCalibrationNewData[] = new byte[4];// 小端字节改大端方式
			oilCalibrationNewData[0] = oilCalibrationBytes[0];
			oilCalibrationNewData[1] = oilCalibrationBytes[1];
			oilCalibrationNewData[2] = oilCalibrationBytes[3];
			oilCalibrationNewData[3] = oilCalibrationBytes[2];
			int oilCalibration = Converser.bytes2int(oilCalibrationNewData);
			oilInfo.setOilCalibration(oilCalibration);

			locZspt += 2;
			// AD落差
			byte gapBytes[] = new byte[4];
			System.arraycopy(buf, locZspt, gapBytes, 2, 2);
			byte gapNewData[] = new byte[4];// 小端字节改大端方式
			gapNewData[0] = gapBytes[0];
			gapNewData[1] = gapBytes[1];
			gapNewData[2] = gapBytes[3];
			gapNewData[3] = gapBytes[2];
			int gap = Converser.bytes2int(gapNewData);
			oilInfo.setGap(gap);

			locZspt += 2;
			// 加油门限
			byte refuelThresholdBytes[] = new byte[1];
			System.arraycopy(buf, locZspt, refuelThresholdBytes, 0, 1);
			int refuelThreshold = refuelThresholdBytes[0] & 0xff;
			oilInfo.setRefuelThreshold(refuelThreshold);

			locZspt += 1;
			// 偷油门限
			byte stealThresholdBytes[] = new byte[1];
			System.arraycopy(buf, locZspt, stealThresholdBytes, 0, 1);
			int stealThreshold = stealThresholdBytes[0] & 0xff;
			oilInfo.setStealThreshold(stealThreshold);

			return oilInfo;
		} else {
			return null;
		}
	}

	/**
	 * 加入Driver队列
	 * @param driver
	 */
	public void putData(Map<String, String> map) {
		try {
			oilQueue.put(map);
		} catch (InterruptedException e) {
			logger.error("加入Driver队列异常:" + e.getMessage(), e);
		}
	}
	
}

