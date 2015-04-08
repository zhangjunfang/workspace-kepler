/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.syn.task AuthTask.java	</li><br>
 * <li>时        间：2013-8-21  下午4:33:03	</li><br>		
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

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.syncservice.util.TaskAdapter;


/*****************************************
 * <li>描 述：驾驶员信息同步任务（鉴权、监管上报相关）
 * 
 *****************************************/
public class DriverQueryTask  extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(DriverQueryTask.class);
	/**	最后处理天	*/
	private static String lastDay = "";
	/**	清理时间	*/
	private static String clearTime = "01";
	/**	清理标记	*/
	private static boolean clearFlag = false;
	/*****************************************
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		setName("DriverQueryTask");
		String time = conf.get("clearTime");
		if(time != null){
			clearTime = time;
		}
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
		try {
//			判断当天
			String currentDate = new SimpleDateFormat("yyyyMMddHH").format(new Date());
			String currentStr = currentDate.substring(0, 8);
			if(!currentStr.equals(lastDay)){ // 隔天
				String hours = currentDate.substring(8);
				if(hours.equals(clearTime)){ 
					clearFlag = true;
				}
			}
			conn = this.oracle.getConnection();
			ps = conn.prepareStatement(conf.get("sql_syncAll"));
			rs = ps.executeQuery();
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
					CharBuffer cbValue = CharBuffer.wrap(value);
					ByteBuffer bbfValue = cs.encode(cbValue);
					byte[] bsValue = 	new byte[bbfValue.limit()];
					System.arraycopy(bbfValue.array(), 0, bsValue, 0, bbfValue.limit());
					
					map.put(bsKey, bsValue); 
					index++;
				} catch (redis.clients.jedis.exceptions.JedisConnectionException ex) {
					logger.error("--DriverQuery--同步驾驶员信息 - 连接redis异常:" + ex.getMessage(), ex);
					map.clear();
					break;
				} catch (Exception e) {
					logger.error("--DriverQuery--同步驾驶员信息 - 写入redis异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}
			jedis = this.redis.getJedisConnection();
			jedis.select(8);
			if(map.size() > 0){
				if(clearFlag){
					jedis.del("KCTX.DRIVER809");
					lastDay = currentStr;
					clearFlag = false;
				}
				jedis.hmset("KCTX.DRIVER809".getBytes(), map);
				map.clear();
			}
			
			long end = System.currentTimeMillis();
			logger.info("--DriverQuery--同步驾驶员信息结束,处理数据:[{}]条, 正常处理:[{}]条, 异常:[{}]条 , oracle查询耗时:[{}]ms, redis写入耗时:[{}]ms, 总耗时[{}]ms", (index + error), index, error, s1 - start, end - s1, end -start);
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("--DriverQuery--同步驾驶员信息异常:" + e.getMessage(), e);
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
				logger.error("--DriverQuery--同步驾驶员信息关闭资源异常:" + e.getMessage(), e);
			}
		}
	}
	
	
}
