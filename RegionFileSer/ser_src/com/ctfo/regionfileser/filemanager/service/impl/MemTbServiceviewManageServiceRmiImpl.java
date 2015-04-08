package com.ctfo.regionfileser.filemanager.service.impl;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.TimeZone;

import org.apache.log4j.Logger;

import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;

import com.ctfo.analyser.util.GridUtil;
import com.ctfo.memorymysql.beans.MemTbServiceviewManage;
import com.ctfo.redis.pool.JedisSerPool;
import com.ctfo.regionfileser.filemanager.service.MemTbServiceviewManageServiceRmi;
import com.ctfo.regionfileser.memorymysql.dao.MemTbServiceviewManageDao;
import com.ctfo.regionfileser.util.DateUtil;

public class MemTbServiceviewManageServiceRmiImpl implements
		MemTbServiceviewManageServiceRmi {

	private static final Logger logger = Logger
			.getLogger(MemTbServiceviewManageServiceRmiImpl.class);

	private MemTbServiceviewManageDao memTbServiceviewManageDao;

	/** reids池 */
	private JedisSerPool jedisSerPool;
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

//		regionMap = findGrid(findListMemTbServiceview());
		regionMap = findGrid2();
		
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
				// 如果目录不存在,新建目录
				if (!file.exists()) {
					file.mkdirs();
				}

				logger.debug(regionFile);

				regionFile.append(File.separator);
				regionFile.append(gridKey);
				regionFile.append(".txt");

				// 存文件
				RandomAccessFile rf = null;

				try {
					rf = new RandomAccessFile(regionFile.toString(), "rw");
					rf.seek(rf.length()); // 将指针移动到文件末尾
					rf.write(fileStr.getBytes("UTF-8"));
//					rf.write(fileStr.getBytes("GBK"));

					logger.debug(gridKey + "写入轨迹文件成功!");
				} catch (FileNotFoundException e) {
					logger.error("在读 轨迹文件" + gridKey + ".txt 找不到.", e);
				} catch (IOException e) {
					logger.error("在写入轨迹文件" + gridKey + ".txt 找不到.", e);
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
	 * 查询所有上报轨迹信息
	 */
	public List<MemTbServiceviewManage> findListMemTbServiceview() {

		logger.info("查询区域轨迹的开始时间:" + DateUtil.getTodayTime() + "utc:"
				+ System.currentTimeMillis());

		List<MemTbServiceviewManage> list = memTbServiceviewManageDao
				.queryAll();

		if (list != null) {
			logger.info("记录总数:" + list.size());
		}
		logger.info("查询区域轨迹的结束时间:" + DateUtil.getTodayTime() + "utc:"
				+ System.currentTimeMillis());

		return list;
	}

	/**
	 * 插入测试数据
	 */
	public void insertTbService() {
		memTbServiceviewManageDao.insertTbService();
	}

	/**
	 * 筛选相同格子名的数据，重复的格子名不再保存
	 */
	private Map<String, StringBuffer> findGrid2() {
		Map<String, StringBuffer> regionMap = new HashMap<String, StringBuffer>();
		StringBuffer memStr = new StringBuffer("");
		StringBuffer oldMemStr = new StringBuffer("");
		String gridKey = null;
		JedisPool jedisPool = jedisSerPool.getJedisPool();
		Jedis jedis = jedisPool.getResource();
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
		return regionMap;
	}

	/**
	 * 筛选相同格子名的数据，重复的格子名不再保存
	 */
	private Map<String, StringBuffer> findGrid( List<MemTbServiceviewManage> memList) {
		Map<String, StringBuffer> regionMap = new HashMap<String, StringBuffer>();
		StringBuffer memStr = new StringBuffer("");
		StringBuffer oldMemStr = new StringBuffer("");
		String gridKey = null;

		for (MemTbServiceviewManage tbService : memList) {

			if (tbService.getMaplon().intValue() > 0
					&& tbService.getMaplat().intValue() > 0) {

				memStr = getMemTbServiceviewManageString(tbService);

				// 根据经纬度确定格子名
				gridKey = GridUtil.queryKey(tbService.getMaplon(),
						tbService.getMaplat());

				// 根据格子名作为key判断Map里是否存在当前格子， 如果存在追加value
				if (regionMap.containsKey(gridKey)) {

					oldMemStr = regionMap.get(gridKey);

					memStr = oldMemStr.append(memStr);

				}

				// 像MAP里添加一条数据
				regionMap.put(gridKey, memStr);

			}

		}
		return regionMap;
	}

	/**
	 * 基本数据文件保存
	 */
	private StringBuffer getMemTbServiceviewManageString(
			MemTbServiceviewManage mem) {

		StringBuffer buf = new StringBuffer("");
		String str = ":";

		if (mem != null) {

			buf.append(mem.getVid()); // 车辆id 0
			buf.append(str);

			buf.append(mem.getVehicleno()); // 车牌号 1
			buf.append(str);

			buf.append(mem.getSpeed()); // 速度 2
			buf.append(str);

			buf.append(mem.getMaplon()); // 偏移后经度 3
			buf.append(str);

			buf.append(mem.getMaplat()); // 偏移后纬度 4
			buf.append(str);

			buf.append(mem.getUtc()); // 偏移后纬度 5
			buf.append(str);

			buf.append(mem.getCorpId()); // 企业id 6
			buf.append(str);

			buf.append(mem.getCorpName()); // 企业名称 7
			buf.append(str);

			buf.append(mem.getTeamId()); // 车队id 8
			buf.append(str);

			buf.append(mem.getTeamName()); // 车队名称 9
			buf.append(str);

			buf.append(mem.getCname()); // 驾驶员姓名 10
			buf.append(str);

			buf.append(mem.getAlarmcode()); // 报警类型 11
			buf.append(str);

			buf.append(mem.getPlateColorId()); // 车牌颜色id 12

			buf.append("\r\n"); // 标记换行
		}
		return buf;
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

		buf.append(vehicleArray[7]); // 报警类型 11 alarmcode
		buf.append(str);

		buf.append(vehicleArray[30]); // 车牌颜色id 12

		buf.append("\r\n"); // 标记换行
		return buf;
	}

	public void setMemTbServiceviewManageDao(
			MemTbServiceviewManageDao memTbServiceviewManageDao) {
		this.memTbServiceviewManageDao = memTbServiceviewManageDao;
	}

	/*
	 * public void setSavePathWindow(String savePathWindow) {
	 * this.savePathWindow = savePathWindow; }
	 * 
	 * public void setSavePathLinux(String savePathLinux) { this.savePathLinux =
	 * savePathLinux; }
	 */

	public JedisSerPool getJedisSerPool() {
		return jedisSerPool;
	}

	public void setJedisSerPool(JedisSerPool jedisSerPool) {
		this.jedisSerPool = jedisSerPool;
	}
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
}
