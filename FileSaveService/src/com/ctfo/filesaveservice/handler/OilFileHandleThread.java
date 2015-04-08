/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.service OilFileHandleThread.java	</li><br>
 * <li>时        间：2013-9-9  下午4:00:54	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.handler;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Calendar;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.io.FileUtils;
import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.alibaba.fastjson.JSON;
import com.ctfo.filesaveservice.dao.RedisConnectionPool;
import com.ctfo.filesaveservice.model.OilInfo;
import com.ctfo.filesaveservice.model.OilSpill;
import com.ctfo.filesaveservice.util.Base64_URl;
import com.ctfo.filesaveservice.util.ConfigLoader;
import com.ctfo.filesaveservice.util.Constant;
import com.ctfo.filesaveservice.util.Converser;
import com.ctfo.filesaveservice.util.FileUtil;


/*****************************************
 * <li>描        述：油量处理线程
 * 
 *****************************************/
public class OilFileHandleThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(OilFileHandleThread.class);

	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
	/** 线程编号	*/
	private int threadId;
	/** 计数器	  */
	private int index;
	/** 上次时间	  */
	private long lastTime = System.currentTimeMillis();
	/** 油量数据文件目录	  */
	private String oilFilePath;
	
	public OilFileHandleThread(int threadId){
		super("OilFileHandleThread" + threadId);
		this.threadId = threadId;
		this.oilFilePath = ConfigLoader.fileParamMap.get("oilPath");
	}
	public void putDataMap(Map<String, String> dataMap) {
		try {
			dataQueue.put(dataMap);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}
	
	public int getQueueSize() {
		return dataQueue.size();
	}
	
	@Override
	public void run() {
		logger.info("油量文件存储线程" + threadId + "启动");
		while (true) {
			try {// 依次向各个分析线程分发报文
				Map<String,String> dataMap = dataQueue.take();
				saveOilList(dataMap.get(Constant.N90), dataMap.get(Constant.VID)); 
				long currentTime = System.currentTimeMillis(); //按时间批量提交
				if((currentTime - lastTime) > 10000){
					lastTime = currentTime;
					logger.info("oilfile-:" + threadId + ",size:" + getQueueSize() + ",10秒处理数据:"+index+"条");
					index = 0;
				}
				index ++;
			} catch (Exception e) {
				logger.error("文件存储主线程队列出错" + e.getMessage(),e);
			}
		}
	}
	
	/****
	 * 存储油量数据
	 * <pre>
	 * 0~3	 纬度 DWORD 以度为单位的纬度值乘以10的6次方，精确到百万分之一度 
	 * 4~7	 经度 DWORD以度为单位的经度值乘以10的6次方，精确到百万分之一度 
	 * 8~9 	高程 WORD 海拔高度，单位为米（m）
	 * 10 ~11	速度 WORD  1/10km/h 
	 * 12~13 	方向 WORD 0 至 359， 正北为0，顺时针 
	 * 14~19	 时间 BCD[6] YY-MM-DD-hh-mm-ss（GMT+8时间）
	 * 20 	防偷漏油数据 BYTE[n] 防偷漏油数据内容
	 * -------------------------------------------------------------------------------
	 * | 0x81(发动机消息头) | 0x01(发动机协议版本标识) | 纬度| 经度 | 海拔 | 速度 | 方向 | 上报时间 | 防偷漏油数据 |
	 * -------------------------------------------------------------------------------
	 * </pre>
	 * @param config
	 * @param nodeName
	 * @param value
	 * @param vid
	 */
	public void saveOilList(String value, String vid) {
		try {
			long start = System.currentTimeMillis();
			String type = "";
			logger.debug("车辆[{}]收到油量信息[{}]", vid, value);
			OilInfo oilInfo = getOilBase(value);
			if(oilInfo != null){
				oilInfo.setVid(vid); 
				if(oilInfo.getStatus().equals("10")){			// 加油
					type = "加油";
//					存储油量变化文件
					saveOilChangeFile(oilInfo);
//					处理偷漏油信息 - 如果有偷漏油报警，遇到加油就解除报警
					String oilSpillKey = "SD_OIL_FILE_SPILL_" + oilInfo.getVid();
					disarmAlarm(oilSpillKey);
					logger.debug("车辆[{}]收到加油信息[{}]", vid, value);
				} else if(oilInfo.getStatus().equals("01")){	//	偷漏油 
					type = "偷漏油";
//					缓存偷漏油信息
					cacheOilSpill(oilInfo);
					logger.debug("车辆[{}]收到偷漏油信息[{}], 缓存偷漏油信息", vid, value);
				} else if(oilInfo.getStatus().equals("00")){ // 油量信息
					type = "油量信息";
					if(oilInfo != null && oilInfo.getLon() == 0 && oilInfo.getLat() == 0 && oilInfo.getElevation() == 0){
					}else{
//					缓存油量变化信息
						cacheOilChanged(oilInfo);
//					处理偷漏油信息
						processOilSpill(oilInfo);
						
					}
//					存储油量信息文件
					saveOilChangeFile(oilInfo);
				}
			} else {
				type = "解析异常";
				logger.error("解析油箱油量透传信息异常:{}", value);
			} 
			logger.debug("处理油箱油量透传[{}]信息完成, 耗时:[{}]ms, 透传内容[{}]", type, (System.currentTimeMillis() - start), value);
		} catch (Exception e) {
			
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
	 * 处理偷漏油报警信息
	 * @param oilInfo
	 */
	private void processOilSpill(OilInfo oilInfo) {
		try {
//			1. 有偷漏油报警就进行判断
			String oilSpillKey = "SD_OIL_FILE_SPILL_" + oilInfo.getVid();
			String oilStr = getOilSpillCache(oilSpillKey);
			logger.debug("查询缓存中的油量报警:[{}]", oilStr); 
			String tips = "";
			if(StringUtils.isNotBlank(oilStr)){
				OilSpill oilSpill = JSON.parseObject(oilStr, OilSpill.class);
//			2. 油量继续减少说明有报警可能
//				long currentTime = getStringToDate(oilInfo.getTime());
//				long spillTime = getStringToDate(oilSpill.getTime());
					if(oilSpill.getBeferOilSpillOil() > oilInfo.getOilAllowance()){ // 当前油量比偷漏油报警后油量还在减少
						tips = "偷漏油发生后,油量还在减少。";
//			2.1 里程差在14公里内说明是真偷漏油
						int mileage = getMileage(oilInfo.getVid()); // 获取当前里程
						if((mileage - oilSpill.getMileage()) < 140){
//						存储偷漏油文件记录
							saveOilChangeFile(oilSpill.getOilInfo());
//						解除报警
							disarmAlarm(oilSpillKey);
							tips += (" 当前里程["+mileage+"], 缓存里程["+oilSpill.getMileage()+"],里程差在14公里内, 存储偷漏油报警"+oilSpill.getOilInfo().getStatus()+", 并删除报警缓存。 redis4库:" + oilSpillKey);
						} else {
//			2.2 里程差超过14公里，说明是误报，解除报警
//						解除报警
							disarmAlarm(oilSpillKey);
							tips += ("当前里程["+mileage+"], 缓存里程["+oilSpill.getMileage()+"],里程差超过14公里，说明是误报，解除报警  redis4库:" + oilSpillKey);
						}
					} else {
//			油量没有减少，且超过14公里，属于误报，解除报警
						int mileage = getMileage(oilInfo.getVid()); // 获取当前里程
						if((mileage - oilSpill.getMileage()) > 140){
//						解除报警
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
	 * 获取UTC时间
	 * @param time
	 * @return
	 */
//	private long getStringToDate(String time) {
//		SimpleDateFormat formatter = new SimpleDateFormat("yyMMddhhmmss");
//		Date d = null;
//		try {
//			d = formatter.parse(time);
//		} catch (ParseException e) {
//			e.printStackTrace();
//		}
//		Calendar c = Calendar.getInstance();
//		c.setTime(d);
//		return c.getTimeInMillis();
//	}
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
			String oilKey = "SD_OIL_FILE_" + oilInfo.getVid();
			int mileage = getFirstMileage(oilKey);
			oilSpill.setMileage(mileage);
			oilSpill.setOilInfo(oilInfo); 
			jedis = RedisConnectionPool.getJedisConnection();
			jedis.select(4);
			String oilJson = JSON.toJSONString(oilSpill);
			jedis.set("SD_OIL_FILE_SPILL_" + oilInfo.getVid(), oilJson); 
			logger.debug("偷油报警缓存完成:[{}]", oilJson);
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
	 * 缓存每次加油量
	 * @param oilInfo
	 */
	private void cacheOilChanged(OilInfo oilInfo) { 
		try {
			boolean result = false;
//			如果油量有变化就存储当前油量值
			String oilKey = "SD_OIL_FILE_" + oilInfo.getVid();
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
			logger.debug("缓存加油量结果[{}], oilKey[{}], 当前油量[{}], 变化量[{}], 油量缓存信息[{}]", result, oilKey, oilInfo.getOilAllowance(), oilInfo.getOilChange(), oilStr );
		} catch (Exception e) {
			logger.error("缓存每次加油量异常:" + e.getMessage(), e);
		}
	}

	/**
	 * 处理油量缓存
	 * @param vid
	 * @param oilKey
	 * @param oilStr
	 * @param oilChange
	 * @param currentOil
	 * @return
	 */
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
	 * 存储油量变化文件
	 * @param fileName
	 * @param content
	 */
	private void saveOilChangeFile(OilInfo oilInfo) {
		RandomAccessFile rf = null;
		StringBuffer fileName = null;
		String content = null;
		try {
			if(oilInfo == null){
				return;
			}
			fileName = new StringBuffer(oilFilePath);
			fileName.append(getTimePath(oilInfo.getTime()));
			fileName.append(oilInfo.getVid());
			fileName.append(".txt");
			content = getContent(oilInfo);
			
			rf = new RandomAccessFile(fileName.toString(), "rw");
			rf.seek(rf.length());
			rf.writeBytes(content + "\r\n"); 
			logger.debug("油量文件[{}]存储完成,内容:[{}]", fileName.toString(), content);
		} catch (FileNotFoundException e) {
			logger.error("车辆编号 ： " + oilInfo.getVid() + "找不到文件操作" + fileName + " 失败！" + e.getMessage(), e);
			FileUtil.coverFolder(oilFilePath);
			try {
				rf = new RandomAccessFile(fileName.toString(), "rw");
				rf.seek(rf.length());
				rf.writeBytes(content + "\r\n");
			} catch (Exception e1) {
				logger.error("重新创建目录后写入文件异常:" + e1.getMessage(), e1);
			}
		} catch (Exception e) {
			logger.error("车辆编号 ： " + oilInfo.getVid() + "操作文件" + fileName + " 失败！" + e.getMessage(), e);
		} finally {
			try {
				if (rf != null) {
					rf.close();
				}
			} catch (IOException e) {
				rf = null;
			}
		}
	}
	/**
	 * 获取油量信息内容
	 * @param oilInfo
	 * @return
	 */
	private String getContent(OilInfo oilInfo) {
		if (oilInfo != null) {
			StringBuffer oil = new StringBuffer();
			oil.append(oilInfo.getLat());// 纬度
			oil.append(":");
			oil.append(oilInfo.getLon());// 经度
			oil.append(":");
			oil.append(oilInfo.getElevation());// 海拔
			oil.append(":");
			oil.append(oilInfo.getDirection());// 方向
			oil.append(":");
			oil.append(oilInfo.getSpeed());// GPS车速
			oil.append(":");
			oil.append(oilInfo.getTime());// 终端上报时间
			oil.append(":");
			oil.append(oilInfo.getStatus()); // 油位异常标志
			oil.append(":");
			oil.append(oilInfo.getFuelLevel()); // 燃油液位
			oil.append(":");
			oil.append(oilInfo.getOilChange()); // 本次加油量
			oil.append(":");
			oil.append(oilInfo.getOilAllowance()); // 油箱燃油油量
//			oil.append(":");
//			oil.append(getMileage(oilInfo.getVid()));

			return oil.toString();
		}else{
			return null;
		}
	}
	/**
	 * 获取时间路径
	 * @param time
	 * @return
	 */
	private Object getTimePath(String time) {
		StringBuffer timePath = new StringBuffer();
		// 创建文件路径
		if (time != null && time.length() == 12) {// 120910100652
			timePath.append("/");
			timePath.append("20" + time.substring(0, 2)); // 年
			timePath.append("/");
			timePath.append(time.substring(2, 4)); // 月
			timePath.append("/");
			timePath.append(time.substring(4, 6)); // 日
		} else { // 时间错误，怎么取当前系统时间
			Calendar cal = Calendar.getInstance();
			int month = cal.get(Calendar.MONTH) + 1;
			int day = cal.get(Calendar.DAY_OF_MONTH);
			timePath.append("/");
			timePath.append(cal.get(Calendar.YEAR));
			timePath.append("/");
			if (month < 10) {
				timePath.append("0");
				timePath.append(month);
			} else {
				timePath.append(month);
			}
			timePath.append("/");
			if (day < 10) {
				timePath.append("0");
				timePath.append(day);
			} else {
				timePath.append(day);
			}
		}
		timePath.append("/");
		return timePath.toString();
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
	public static void main(String[] args) throws IOException { 
		List<String> list = FileUtils.readLines(new File("e:/temp/18013111640.txt"));
		for(String str : list){
			String[] array = str.split(",90:");
			OilInfo oilInfo = getOilBase2(array[1].replace("}", "").trim());
			if(oilInfo!=null && oilInfo.getLat()==0&&oilInfo.getLon()==0&&oilInfo.getDirection()==0){
				
				System.out.println(getContent2(oilInfo)); 
			}
			
		}
	}
	/**
	 * 获取油量信息内容
	 * @param oilInfo
	 * @return
	 */
	private static String getContent2(OilInfo oilInfo) {
		if (oilInfo != null) {
			StringBuffer oil = new StringBuffer();
			oil.append(oilInfo.getLat());// 纬度
			oil.append(":");
			oil.append(oilInfo.getLon());// 经度
			oil.append(":");
			oil.append(oilInfo.getElevation());// 海拔
			oil.append(":");
			oil.append(oilInfo.getDirection());// 方向
			oil.append(":");
			oil.append(oilInfo.getSpeed());// GPS车速
			oil.append(":");
			oil.append(oilInfo.getTime());// 终端上报时间
			oil.append(":");
			oil.append(oilInfo.getStatus()); // 油位异常标志
			oil.append(":");
			oil.append(oilInfo.getFuelLevel()); // 燃油液位
			oil.append(":");
			oil.append(oilInfo.getOilChange()); // 本次加油量
			oil.append(":");
			oil.append(oilInfo.getOilAllowance()); // 油箱燃油油量
//			oil.append(":");
//			oil.append(getMileage(oilInfo.getVid()));

			return oil.toString();
		}else{
			return null;
		}
	}
	/**
	 * 解析油量数据
	 * @param value
	 * @return
	 */
	private static OilInfo getOilBase2(String value) {
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
}
