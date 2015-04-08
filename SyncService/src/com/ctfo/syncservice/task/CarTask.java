/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.syn.task DriverAuthenticationTask.java	</li><br>
 * <li>时        间：2013-8-21  下午7:16:43	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.task;

import java.nio.ByteBuffer;
import java.nio.CharBuffer;
import java.nio.charset.Charset;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.syncservice.util.TaskAdapter;

/*****************************************
 * <li>描        述：同步静态鉴权车辆任务		
 * 
 *****************************************/ 
public class CarTask extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(CarTask.class);
	/** 日期状态		*/
	private static String status = "";
	/** 最近更新时间		*/
	private static long lastUtc = System.currentTimeMillis();
	/** 清空时间		*/
	private static String clearTime = "01";

	/*****************************************
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		setName("CarTask");
		clearTime = conf.get("clearTime");
		
	}
	/*****************************************
	 * <li>描 述：同步信息 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void execute() {
		long start = System.currentTimeMillis();
		int index = 0;
		int error = 0;
		Connection conn = null;
		PreparedStatement ps = null;
		ResultSet rs = null;
		Jedis jedis = null;
//		首次更新
		boolean firstSync = false;
//		全量同步标志
		boolean fullSync = false;
		try {
//			判断是否进行无效数据处理  (明天凌晨2点进行一次)
			String currentDate = new SimpleDateFormat("yyyyMMddHH").format(new Date());
			String currentStr = currentDate.substring(0, 8);
			String hours = currentDate.substring(8);
			if(hours.equals(clearTime)){
				String[] dayStr = StringUtils.split(status, "_");
				if(dayStr.length == 2){
					if(dayStr == null || dayStr.length != 2){
						logger.error("--Car--增量同步车辆静态异常!!! 同步标记异常,请重启同步服务! ==>" + status);
					}
	//				当天删除次数为0
					if(dayStr[0].equals(currentStr)){ 
						if(dayStr[1].equals("0")){
							fullSync = true;
						}
	//				不是当天就进行处理
					} else {
						fullSync = true;
					}
				}
			}
			if(status.equals("")){
				fullSync = true;
				firstSync = true;
			}
			
			conn = this.oracle.getConnection();
			if(fullSync){
				ps = conn.prepareStatement(conf.get("sql_syncAll"));
				rs = ps.executeQuery();
			} else {
				ps = conn.prepareStatement(conf.get("sql_syncIncrements"));
//				logger.info("当前时间"+ lastUtc + ", 可视化时间:"+ new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date(lastUtc)));
				ps.setLong(1, lastUtc);
				ps.setLong(2, lastUtc);
				ps.setLong(3, lastUtc);
				ps.setLong(4, lastUtc);
				ps.setLong(5, lastUtc);
				ps.setLong(6, lastUtc);
				ps.setLong(7, lastUtc);
				ps.setLong(8, lastUtc);
				ps.setLong(9, lastUtc);
				ps.setLong(10, lastUtc);
				rs = ps.executeQuery();
			}
			long s1 = System.currentTimeMillis();
			
			Map<byte[],byte[]> map =  new HashMap<byte[],byte[]>();
			Charset cs = Charset.forName("GBK");
			while (rs.next()) {
				try {
					String key = rs.getString("KEYS");
					CharBuffer cbKey = CharBuffer.wrap(key);
					ByteBuffer bbfKey = cs.encode(cbKey);
					byte[] bsKey = 	new byte[bbfKey.limit()];
					System.arraycopy(bbfKey.array(), 0, bsKey, 0, bbfKey.limit());
					
					String value = rs.getString("VAL");
					value = value.replaceAll(",", ";");
					value = value.replaceAll("#", ",");
					CharBuffer cbValue = CharBuffer.wrap(value);
					ByteBuffer bbfValue = cs.encode(cbValue);
					byte[] bsValue = 	new byte[bbfValue.limit()];
					System.arraycopy(bbfValue.array(), 0, bsValue, 0, bbfValue.limit());
					
					map.put(bsKey, bsValue); 
					index++;
				} catch (redis.clients.jedis.exceptions.JedisConnectionException ex) {
					logger.error("--Car--同步车辆静态 - 连接redis异常:" + ex.getMessage(), ex);
					map.clear();
					break;
				} catch (Exception e) {
					logger.error("--Car--同步车辆静态 - 写入redis异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}
			StringBuffer syncAll = new StringBuffer();
			String syncType = "";
			jedis = this.redis.getJedisConnection();
			jedis.select(8);
			if(fullSync){
//				1。 全量同步车辆静态
				syncType = "--All--全量";
				if(!firstSync){
					jedis.del("KCTX.CARINFO".getBytes());
				}
				jedis.hmset("KCTX.CARINFO".getBytes(), map);
				syncAll.append("syncAll-缓存全量更新[").append(map.size()).append("]条  "); 
//				处理完成就更新标记
				status = currentStr + "_1";
			} else {
				syncType = "--Increments--增量";
				if(map.size() > 0){
					jedis.hmset("KCTX.CARINFO".getBytes(), map);	
				}
			}
			map.clear();
			long end = System.currentTimeMillis();
			logger.info("--Car--{}同步车辆静态结束,处理数据:[{}]条, 正常处理:[{}]条, 异常:[{}]条 , {} oracle查询耗时:[{}]ms, redis写入耗时:[{}]ms, 总耗时[{}]ms", syncType, (index + error), index, error, syncAll, s1 - start, end - s1, end -start);
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("--Car--同步车辆静态异常:" + e.getMessage(), e);
		} finally {
			if(jedis != null){
				this.redis.returnJedisConnection(jedis);
			}
			try {
				if(rs != null){
					rs.close();
				}
				if(ps != null){
					ps.close();
				}
				if(conn != null){
					conn.close();
				}
			} catch (SQLException e) {
				logger.error("--Car--同步车辆静态关闭资源异常:" + e.getMessage(), e);
			}
			lastUtc = start;
		}
	}
	public static String getStatus() {
		return status;
	}
	public static void setStatus(String status) {
		CarTask.status = status;
	}
	
}
