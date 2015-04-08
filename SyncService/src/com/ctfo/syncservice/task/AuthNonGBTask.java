/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.syn.task AuthTask.java	</li><br>
 * <li>时        间：2013-8-21  下午4:33:03	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.task;

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

import com.ctfo.redis.util.RedisJsonUtil;
import com.ctfo.syncservice.model.AuthInfo;
import com.ctfo.syncservice.util.TaskAdapter;


/*****************************************
 * <li>描 述：非国标终端鉴权同步任务
 * 
 *****************************************/
public class AuthNonGBTask extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(AuthNonGBTask.class);
	/** 鉴权信息同步状态		*/
	private static String auth = "";
	/** 鉴权信息最近更新时间		*/
	private static long authUtc = System.currentTimeMillis();
	/** 清空时间		*/
	private static String clearTime = "01";

	/*****************************************
	 * <li>描 述：同步鉴权信息 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void execute() {
		long start = System.currentTimeMillis();
		long lastUtc = authUtc;
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
			jedis = this.redis.getJedisConnection();
			jedis.select(2);
//			判断是否进行无效数据处理  (明天凌晨2点进行一次)
			String currentDate = new SimpleDateFormat("yyyyMMddHH").format(new Date());
			String currentStr = currentDate.substring(0, 8);
			String hours = currentDate.substring(8);
			if(hours.equals(clearTime)){
				String[] dayStr = StringUtils.split(auth, "_");
				if(dayStr.length == 2){
					if(dayStr == null || dayStr.length != 2){
						logger.error("--AuthNonGB--增量更新[非国标]静态鉴权车辆异常!!! 同步标记异常,请重启同步服务! ==>" + auth);
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
			if(auth.equals("")){
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
				ps.setLong(2, lastUtc);
				ps.setLong(3, lastUtc);
				ps.setLong(4, lastUtc);
				ps.setLong(5, lastUtc);
				ps.setLong(6, lastUtc);
				ps.setLong(7, lastUtc);
				ps.setLong(8, lastUtc);
				rs = ps.executeQuery();
			}
			long s1 = System.currentTimeMillis();
			
			Map<String, String> map =  new HashMap<String, String>();
			while (rs.next()) {
				try {
					AuthInfo authInfo = new AuthInfo();
					authInfo.setAkey(rs.getString("AUTH_CODE"));
					authInfo.setOemcode(rs.getString("OEM_CODE"));
					authInfo.setVid(rs.getString("VID"));
					authInfo.setVehicleState(rs.getString("VEHICLE_STATE"));
					authInfo.setVehicleRegStatus(rs.getString("REG_STATUS"));
					authInfo.setVehicleNo(rs.getString("VEHICLE_NO"));
					authInfo.setPlateColor(rs.getString("PLATE_COLOR"));
					authInfo.setTid(rs.getString("TID"));
					authInfo.setCommaddr(rs.getString("COMMADDR"));
					authInfo.setTerRegStatus(rs.getString("REG_STATUS"));
					authInfo.setTerState(rs.getString("TER_STATE"));
					authInfo.setSid(rs.getString("SID"));
					String authInfoStr = RedisJsonUtil.objectToJson(authInfo);
					String keys = "PCC_" + rs.getString("TMAC");
					map.put(keys,authInfoStr); 
					index++;
				} catch (redis.clients.jedis.exceptions.JedisConnectionException ex) {
					logger.error("--AuthNonGB--增量同步[非国标]鉴权信息 - 连接redis异常:" + ex.getMessage(), ex);
					map.clear();
					break;
				} catch (Exception e) {
					logger.error("--AuthNonGB--增量同步[非国标]鉴权信息 - 写入redis异常:" + e.getMessage(), e);
					error++;
					continue;
				}
			}
			StringBuffer syncAll = new StringBuffer();
			String syncType = "";
			if(fullSync){
//				1。 全量同步鉴权信息
				syncType = "--All--全量";
				if(!firstSync){
					jedis.del("KCPT.AUTH");
				}
				jedis.hmset("KCPT.AUTH", map);
				syncAll.append("syncAll-缓存[非国标]全量更新[").append(map.size()).append("]条  "); 
//				处理完成就更新标记
				auth = currentStr + "_1";
			} else {
				syncType = "--Increments--增量";
				if(map.size() > 0){
					jedis.hmset("KCPT.AUTH", map);	
				}
			}
			map.clear();
			long end = System.currentTimeMillis();
			logger.info("--AuthNonGB--{}同步[非国标]鉴权信息结束,处理数据:[{}]条, 正常处理:[{}]条, 异常:[{}]条 , {} oracle查询耗时:[{}]ms, redis写入耗时:[{}]ms, 总耗时[{}]ms", syncType, (index + error), index, error, syncAll, s1 - start, end - s1, end -start);
		} catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("--AuthNonGB--增量同步[非国标]鉴权信息异常:" + e.getMessage(), e);
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
				logger.error("--AuthNonGB--增量同步[非国标]鉴权信息关闭资源异常:" + e.getMessage(), e);
			}
			authUtc = start - 60000;
		}
	}
	/*****************************************
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		setName("AuthNonGBTask");
		clearTime = conf.get("clearTime");
	}
	public static String getAuth() {
		return auth;
	}
	public static void setAuth(String auth) {
		AuthNonGBTask.auth = auth;
	}
	
}
