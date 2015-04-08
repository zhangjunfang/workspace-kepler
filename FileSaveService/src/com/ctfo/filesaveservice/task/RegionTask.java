/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.task RegionTask.java	</li><br>
 * <li>时        间：2013-9-9  上午11:50:45	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.task;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.TimeZone;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.filesaveservice.dao.RedisConnectionPool;
import com.ctfo.filesaveservice.util.Constant;
import com.ctfo.filesaveservice.util.GridUtil;
import com.ctfo.filesaveservice.util.TaskAdapter;



/*****************************************
 * <li>描        述：区域文件生成任务		
 * 
 *****************************************/
public class RegionTask extends TaskAdapter {
	private static final Logger logger = LoggerFactory.getLogger(RegionTask.class);
	/**	区域文件路径	*/
	private String regionFilePath;
	/**	地图工具	*/
	private GridUtil grid = null;
	/**
	 * 初始化参数
	 */
	public void init() {
		setName("RegionTask");
		regionFilePath = config.get("regionFilePath");
		String grid_x = config.get("grid_x");
		String grid_y = config.get("grid_y");
		String grid_len = config.get("grid_len");
		grid = new GridUtil();
		grid.setStartx(grid_x);
		grid.setStarty(grid_y);
		grid.setLen(grid_len);
		
		logger.info("区域文件任务初始化完成!"); 
	}

	/**
	 * 生成区域协查文件
	 */
	public void execute() {
		long start = System.currentTimeMillis();
		try {
			Map<String, StringBuffer> regionMap = new HashMap<String, StringBuffer>();
			regionMap = findGrid();
			long s1 = System.currentTimeMillis();
			int mapSize = regionMap.size();
			if (mapSize > 0) {
				Set<String> st = regionMap.keySet();
				Iterator<String> it = st.iterator();
				while (it.hasNext()) {
					String gridKey = it.next();
					StringBuffer buf = regionMap.get(gridKey);
					// 去掉最后的换行符
					String fileStr = buf.toString().substring(0, buf.lastIndexOf("\r\n"));
					/*
					 * if(gridKey == null){ gridKey=""; }
					 */
					Date time = new Date();
					Calendar cal = Calendar.getInstance();
					cal.setTime(time);
					cal.setTimeZone(TimeZone.getDefault());
					logger.debug(regionFilePath);
					StringBuffer regionFile = new StringBuffer(regionFilePath);
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
			long end = System.currentTimeMillis();
			logger.info("生成区域文件结束, 有效数据:[{}]条, 查询处理轨迹耗时:[{}]ms, 存储文件耗时:[{}]ms", mapSize, (s1 -start), (end - s1));
		} catch (Exception e) {
			logger.error("生成区域文件异常:" + e.getMessage(), e);
		}
	}

	/**
	 * 筛选相同格子名的数据，重复的格子名不再保存  RedisDao.getAllTrackKeys   RedisDao.getTrackValue
	 * @throws Exception 
	 */
	private Map<String, StringBuffer> findGrid() {
		Map<String, StringBuffer> regionMap = new HashMap<String, StringBuffer>();
		StringBuffer memStr = new StringBuffer("");
		StringBuffer oldMemStr = new StringBuffer("");
		String gridKey = null; 
		Jedis jedis = null;
		int index = 0;
		int valueSize = 0;
		try {
			jedis = RedisConnectionPool.getJedisConnection();
			//一小时内的时间
			long thisTime = System.currentTimeMillis() - 3600000;
			jedis.select(6);
			Set<String> vehicleSet = jedis.keys("[0-9]*");
			if (vehicleSet != null && vehicleSet.size() > 0) {
				String[] keys = setToArray(vehicleSet);
				List<String> values = jedis.mget(keys);
				for (String vehicleStr : values) {
					if(vehicleStr == null){
						continue;
					}
					String[] vehicleArray = StringUtils.splitPreserveAllTokens(vehicleStr, ":");
//				判断位置信息是否合法
					if(vehicleArray.length != 45){
						continue;
					}
//				过滤一个小时没有更新的数据
					if(StringUtils.isNumeric(vehicleArray[40])){ 
						long l = Long.parseLong(vehicleArray[40]);
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
						logger.warn("VID:"+vehicleStr+"的车辆信息经纬度不合法,经度:"+lon+",纬度:"+lat);
						continue;
					}
					if (lon > 0 && lat > 0) {
						memStr = getMemTbServiceviewManageString2(vehicleArray);
						// 根据经纬度确定格子名
						gridKey = grid.queryKey(lon, lat);
						// 根据格子名作为key判断Map里是否存在当前格子， 如果存在追加value
						if (regionMap.containsKey(gridKey)) {
							oldMemStr = regionMap.get(gridKey);
							memStr = oldMemStr.append(memStr);
						}
						// 像MAP里添加一条数据
						regionMap.put(gridKey, memStr);
						index++;
					}
				}
				valueSize = values.size();
			}
			logger.debug("实时位置解析结束,处理数量:[" + valueSize + "]条, 有效数据:[" + index+"]条");
		} catch (Exception e1) {
			if(jedis != null){
				RedisConnectionPool.returnBrokenResource(jedis);
			}
			logger.error("处理区域数据异常:"+e1.getMessage(),e1); 
		}finally{
			if(jedis != null){
				RedisConnectionPool.returnJedisConnection(jedis);
			}
		}
		return regionMap;
	}


	/*****************************************
	 * <li>描        述：字符串集合转数组 		</li><br>
	 * <li>时        间：2013-9-12  下午1:05:40	</li><br>
	 * <li>参数： @param vehicleSet
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	private String[] setToArray(Set<String> vehicleSet) {
		List<String> vehicleStrList = new ArrayList<String>();
		vehicleStrList.addAll(vehicleSet);
		String[] keys = new String[vehicleStrList.size()];
		for(int i = 0; i< vehicleStrList.size();i++){
			keys[i] = vehicleStrList.get(i);
		}
		return keys;
	}
	/**
	 * 基本数据文件保存
	 */
	private StringBuffer getMemTbServiceviewManageString2(String[] vehicleArray) {
		StringBuffer buf = new StringBuffer("");
		buf.append(vehicleArray[29]); // 车辆id 0
		buf.append(Constant.COLON);
		buf.append(vehicleArray[31]); // 车牌号 1
		buf.append(Constant.COLON);
		buf.append(vehicleArray[4]); // 速度 2
		buf.append(Constant.COLON);
		buf.append(vehicleArray[2]); // 偏移后经度 3
		buf.append(Constant.COLON);
		buf.append(vehicleArray[3]); // 偏移后纬度 4
		buf.append(Constant.COLON);
		buf.append(vehicleArray[40]); // UTC 5
		buf.append(Constant.COLON);
		buf.append(vehicleArray[38]); // 企业id 6
		buf.append(Constant.COLON);
		buf.append(vehicleArray[36]); // 企业名称 7
		buf.append(Constant.COLON);
		buf.append(vehicleArray[37]); // 车队id 8
		buf.append(Constant.COLON);
		buf.append(vehicleArray[44]); // 车队名称 9
		buf.append(Constant.COLON);
		buf.append(vehicleArray[35]); // 驾驶员姓名 10
		buf.append(Constant.COLON);
		buf.append(vehicleArray[7]); // 报警  CODE11
		buf.append(Constant.COLON);
		buf.append(vehicleArray[30]); // 车牌颜色id 12
		buf.append(Constant.NEWLINE); // 标记换行
		return buf;
	}
}
