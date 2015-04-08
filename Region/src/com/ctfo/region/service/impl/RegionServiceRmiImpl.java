package com.ctfo.region.service.impl;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;
import java.util.TimeZone;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;

import com.ctfo.redis.pool.JedisSerPool;
import com.ctfo.region.service.RegionServiceRmi;
import com.ctfo.region.util.DateUtil;
import com.ctfo.region.util.GridUtil;
import com.ctfo.region.util.RedisUtil;

public class RegionServiceRmiImpl implements RegionServiceRmi {

	private static final Logger logger = LoggerFactory.getLogger(RegionServiceRmiImpl.class);

	/** reids池 */
//	private JedisSerPool jedisSerPool;
	/** reids地址 */
	private String redisHost;
	/** reids端口 */
	private int redisPort;
	/** reids密码 */
	private String redisPass;
	/** reids数据库索引 */
	private int redisIndex;
	
	

	/*	*//**
	 * 轨迹文件路径 window
	 */
	private String savePathWindow;

	/**
	 * 轨迹文件路径 window
	 */
	private String savePathLinux;

	public String getSavePathWindow() {
		return savePathWindow;
	}

	public void setSavePathWindow(String savePathWindow) {
		this.savePathWindow = savePathWindow;
	}

	public String getSavePathLinux() {
		return savePathLinux;
	}

	public void setSavePathLinux(String savePathLinux) {
		this.savePathLinux = savePathLinux;
	}

	/**
	 * 将轨迹数据写入文件
	 */
	@Override
	public void saveRegionFile() {

		String regionfileurl = null;
		// 此处判断系统 window 目录
		if (System.getProperty("os.name").toUpperCase().indexOf("WINDOWS") != -1) {
			// regionfileurl = GridBean.getSavePathWindow();
			regionfileurl = this.savePathWindow;
		} else {
			regionfileurl = this.savePathLinux;
			// regionfileurl = GridBean.getSavePathLinux();
		}

		Map<String, StringBuffer> regionMap = new HashMap<String, StringBuffer>();

		regionMap = findGrid();
		
		String startTime = DateUtil.getTodayTime();
		logger.info("保存文件的开始时间:" + startTime);

		if (regionMap.size() > 0) {
			Set<String> st = regionMap.keySet();
			Iterator<String> it = st.iterator();
			while (it.hasNext()) {
				String gridKey = it.next();
				StringBuffer buf = regionMap.get(gridKey);

				// 去掉最后的换行符
				String fileStr = buf.toString().substring(0,
						buf.lastIndexOf("\r\n"));

				/*
				 * if(gridKey == null){ gridKey=""; }
				 */

				// 文件目录结构 d:\region\20120112\16\35\xxx_xxx.txt
				Date time = new Date();
				Calendar cal = Calendar.getInstance();
				cal.setTime(time);
				cal.setTimeZone(TimeZone.getDefault());
				logger.debug(regionfileurl);
				StringBuffer regionFile = new StringBuffer(regionfileurl);
				regionFile.append(File.separator);

				regionFile.append(cal.get(Calendar.YEAR));
				if (cal.get(Calendar.MONTH) + 1 < 10) {
					regionFile.append("0");
				}
				regionFile.append(cal.get(Calendar.MONTH) + 1);
				if (cal.get(Calendar.DAY_OF_MONTH) < 10) {
					regionFile.append("0");
				}
				regionFile.append(cal.get(Calendar.DAY_OF_MONTH));
				regionFile.append(File.separator);

				if (cal.get(Calendar.HOUR_OF_DAY) < 10) {
					regionFile.append("0");
				}
				regionFile.append(cal.get(Calendar.HOUR_OF_DAY));
				regionFile.append(File.separator);

				if (cal.get(Calendar.MINUTE) < 10) {
					regionFile.append("0");
				}
				regionFile.append(cal.get(Calendar.MINUTE));

				File file = new File(regionFile.toString());
				// 如果目录、文件不存在,新建目录
				if (!file.isDirectory() || !file.exists()) {
					file.mkdirs();
				}
				
				
				logger.debug(regionFile.toString());

				regionFile.append(File.separator);
				regionFile.append(gridKey);
				regionFile.append(".txt");

				// 存文件
				RandomAccessFile rf = null;

				try {
					rf = new RandomAccessFile(regionFile.toString(), "rw");
					rf.seek(rf.length()); // 将指针移动到文件末尾
					rf.write(fileStr.getBytes("UTF-8"));

					logger.debug(gridKey + "写入轨迹文件成功!");
				} catch (FileNotFoundException e) {
					logger.error("在读 轨迹文件" + gridKey + ".txt 找不到.", e);
					continue;
				} catch (IOException e) {
					logger.error("在写入轨迹文件" + gridKey + ".txt 找不到.", e);
					continue;
				} finally {
					if (rf != null) {
						try {
							rf.close();
						} catch (IOException e) {
							logger.error("关闭轨迹文件" + gridKey + ".txt 找不到.", e);
						}
					}
				}
			}
		}
		regionMap.clear();

		logger.info("保存文件的结束时间:" + DateUtil.getTodayTime());
	}


	/**
	 * 筛选相同格子名的数据，重复的格子名不再保存
	 */
	private Map<String, StringBuffer> findGrid() {
		Map<String, StringBuffer> regionMap = new HashMap<String, StringBuffer>();
		StringBuffer memStr = new StringBuffer("");
		StringBuffer oldMemStr = new StringBuffer("");
		String gridKey = null;
		
		//创建redis服务
		JedisSerPool jedisSerPool = RedisUtil.getPool(RedisUtil.getSource(redisHost, redisPort, redisPass));
		JedisPool jedisPool =  jedisSerPool.getJedisPool();
		Jedis jedis = jedisPool.getResource();
		
		//一小时内的时间
		long thisTime = System.currentTimeMillis() - 3600000;
		jedis.auth(redisPass);
		jedis.select(redisIndex);
		Set<String> vehicleSet = jedis.keys("*");
		if (vehicleSet != null && vehicleSet.size() > 0) {
			for (String keysStr : vehicleSet) {
				String vehicleStr = jedis.get(keysStr);
				if(vehicleStr == null){
					continue;
				}
				String[] vehicleArray = vehicleStr.split(":");
//				判断位置信息是否合法
				if(vehicleArray.length != 45){
					continue;
				}
//				过滤一个小时没有更新的数据
				if(StringUtils.isNumeric(vehicleArray[40])){ 
					Long l = Long.parseLong(vehicleArray[40]);
					if(l  < thisTime){
						continue;
					}
				}else{
					continue;
				}
				
//				 只处理车辆状态为在线的数据
				if(vehicleArray[41] == null || vehicleArray[41].equals("0")){ 
					continue;
				}
//				经度纬度必须要合法
				if (vehicleArray[2] == null || vehicleArray[3] == null) {
					continue;
				}
				int lon = 0;
				int lat = 0;
				try {
					lon = Integer.parseInt(vehicleArray[2]);
					lat = Integer.parseInt(vehicleArray[3]);
				} catch (Exception e) {
					logger.warn("VID:"+keysStr+"的车辆信息经纬度不合法,经度:"+lon+",纬度:"+lat);
					continue;
				}
				if (lon > 0 && lat > 0) {
					memStr = getMemTbServiceviewManageString2(vehicleArray);
					// 根据经纬度确定格子名
					gridKey = GridUtil.queryKey(lon, lat);
					// 根据格子名作为key判断Map里是否存在当前格子， 如果存在追加value
					if (regionMap.containsKey(gridKey)) {
						oldMemStr = regionMap.get(gridKey);
						memStr = oldMemStr.append(memStr);
					}
					// 像MAP里添加一条数据
					regionMap.put(gridKey, memStr);
				}
			}
		}
		jedisPool.returnResource(jedis);
		jedisPool.returnBrokenResource(jedis);
		return regionMap;
	}


	/**
	 * 基本数据文件保存
	 */
	private StringBuffer getMemTbServiceviewManageString2(String[] vehicleArray) {

		StringBuffer buf = new StringBuffer("");
		String str = ":";

		buf.append(vehicleArray[29]); // 车辆id 0
		buf.append(str);

		buf.append(vehicleArray[31]); // 车牌号 1
		buf.append(str);

		buf.append(vehicleArray[4]); // 速度 2
		buf.append(str);

		buf.append(vehicleArray[2]); // 偏移后经度 3
		buf.append(str);

		buf.append(vehicleArray[3]); // 偏移后纬度 4
		buf.append(str);

		buf.append(vehicleArray[40]); // UTC 5
		buf.append(str);

		buf.append(vehicleArray[38]); // 企业id 6
		buf.append(str);

		buf.append(vehicleArray[36]); // 企业名称 7
		buf.append(str);

		buf.append(vehicleArray[37]); // 车队id 8
		buf.append(str);

		buf.append(vehicleArray[44]); // 车队名称 9
		buf.append(str);

		buf.append(vehicleArray[35]); // 驾驶员姓名 10
		buf.append(str);

		buf.append(vehicleArray[7]); // 报警  CODE11
		buf.append(str);

		buf.append(vehicleArray[30]); // 车牌颜色id 12

		buf.append("\r\n"); // 标记换行
		return buf;
	}

	/*
	 * public void setSavePathWindow(String savePathWindow) {
	 * this.savePathWindow = savePathWindow; }
	 * 
	 * public void setSavePathLinux(String savePathLinux) { this.savePathLinux =
	 * savePathLinux; }
	 */

	public String getRedisPass() {
		return redisPass;
	}

	public void setRedisPass(String redisPass) {
		this.redisPass = redisPass;
	}

	public int getRedisIndex() {
		return redisIndex;
	}

	public void setRedisIndex(int redisIndex) {
		this.redisIndex = redisIndex;
	}

	public String getRedisHost() {
		return redisHost;
	}

	public void setRedisHost(String redisHost) {
		this.redisHost = redisHost;
	}

	public int getRedisPort() {
		return redisPort;
	}

	public void setRedisPort(int redisPort) {
		this.redisPort = redisPort;
	}
	
}
