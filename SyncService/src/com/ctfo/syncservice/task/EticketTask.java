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
 * <li>描        述：同步电子运单任务		
 * 
 *****************************************/ 
public class EticketTask extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(EticketTask.class);
	/** 日期状态		*/
	private static String status = "";
	/** 最近更新时间		*/
	private static long lastUtc = System.currentTimeMillis();
	/** 清空时间		*/
	private static String clearTime = "04";

	/*****************************************
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		setName("EticketTask");
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
						logger.error("--Eticket--增量同步电子运单信息异常!!! 同步标记异常,请重启同步服务! ==>" + status);
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
			// ##################  测试
//			fullSync = true;
			
			conn = this.oracle.getConnection();
			if(fullSync){
				ps = conn.prepareStatement(conf.get("sql_syncAll"));
				rs = ps.executeQuery();
			} else {
				ps = conn.prepareStatement(conf.get("sql_syncIncrements"));
//				logger.info("当前时间"+ lastUtc + ", 可视化时间:"+ new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date(lastUtc)));
				ps.setLong(1, lastUtc);
				rs = ps.executeQuery();
			}
			long s1 = System.currentTimeMillis();
			
			Map<byte[],byte[]> map =  new HashMap<byte[],byte[]>();
			Charset cs = Charset.forName("GBK");
			while (rs.next()) {
				try {
					// 车牌颜色_车牌号码转码
					String carId = rs.getString("KEYS");
					if(carId == null || carId.length() < 3){
						error++;
						continue;
					}
					CharBuffer cbId = CharBuffer.wrap(carId);
					ByteBuffer bbfId = cs.encode(cbId);
					byte[] bsId = new byte[bbfId.limit()];
					System.arraycopy(bbfId.array(), 0, bsId, 0, bbfId.limit());
					
					// 车辆静态信息转码
					String carInfo = rs.getString("VAL");
					if(carInfo == null  || carInfo.length() == 0){
						error++;
						continue;
					}
					CharBuffer cbInfo = CharBuffer.wrap(carInfo);
					ByteBuffer bbfInfo = cs.encode(cbInfo);
					byte[] bsInfo = 	new byte[bbfInfo.limit()];
					System.arraycopy(bbfInfo.array(), 0, bsInfo, 0, bbfInfo.limit());
					
					map.put(bsId, bsInfo); 
					index++;
				} catch (redis.clients.jedis.exceptions.JedisConnectionException ex) {
					logger.error("--Eticket--增量同步电子运单信息 - 连接redis异常:" + ex.getMessage(), ex);
					map.clear();
					break;
				} catch (Exception e) {
					logger.error("--Eticket--增量同步电子运单信息 - 写入redis异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}

			jedis = this.redis.getJedisConnection();
			jedis.select(8);
			StringBuffer syncAll = new StringBuffer();
			String syncType = "";
			if(fullSync){
//				1。 全量同步电子运单信息
				syncType = "--All--全量";
				if(!firstSync){
					jedis.del("KCTX.EWAYBILL".getBytes());
				}
				jedis.hmset("KCTX.EWAYBILL".getBytes(), map);
				syncAll.append("syncAll-缓存全量更新[").append(map.size()).append("]条  "); 
//				处理完成就更新标记
				status = currentStr + "_1";
			} else {
				syncType = "--Increments--增量";
				if(map.size() > 0){
					jedis.hmset("KCTX.EWAYBILL".getBytes(), map);	
				}
			}
			map.clear();
			long end = System.currentTimeMillis();
			logger.info("--Eticket--{}同步电子运单信息结束,处理数据:[{}]条, 正常处理:[{}]条, 异常:[{}]条 , {} oracle查询耗时:[{}]ms, redis写入耗时:[{}]ms, 总耗时[{}]ms", syncType, (index + error), index, error, syncAll, s1 - start, end - s1, end -start);
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("--Eticket--同步电子运单信息异常:" + e.getMessage(), e);
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
				logger.error("--Eticket--同步电子运单信息关闭资源异常:" + e.getMessage(), e);
			}
			lastUtc = start;
		}
	}
}
