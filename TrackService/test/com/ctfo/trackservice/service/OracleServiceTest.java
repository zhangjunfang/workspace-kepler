/**
 * 
 */
package com.ctfo.trackservice.service;

import java.util.Map;
import java.util.UUID;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.commons.lang3.StringUtils;
import org.junit.Assert;
import org.junit.Test;

import com.ctfo.trackservice.common.ConfigLoader;
import com.ctfo.trackservice.common.Utils;
import com.ctfo.trackservice.dao.OracleConnectionPool;
import com.ctfo.trackservice.dao.RedisConnectionPool;
import com.ctfo.trackservice.handler.StatusHandlerThread;
import com.ctfo.trackservice.model.EquipmentStatus;
import com.ctfo.trackservice.model.StatusCode;

/**
 * @author hu
 *
 */
public class OracleServiceTest {

	OracleService service = null;
	Map<String, String> map = new ConcurrentHashMap<String, String>();
	
	public OracleServiceTest() throws Exception{
		try {
			ConfigLoader.init(new String[]{"-d" , "E:/WorkSpace/trank/TrackService/src/config.xml", "E:/WorkSpace/trank/TrackService/src/system.properties"});
			OracleConnectionPool.init(Utils.getOracleProperties(ConfigLoader.config));
			RedisConnectionPool.init(Utils.getRedisProperties(ConfigLoader.config));
//			OracleService.init();
			
			String command= "CAITS 0_1 E004_15238603446 0 U_REPT {TYPE:0,RET:0,1:75771290,2:27798260,20:7,21:7,210:10520,213:9676,214:123,215:106,216:15,218:0,219:-1,24:-1,3:952,4:20140312/144108,5:355,500:16384,503:128,504:48,505:8335,506:37,507:282,508:80,509:123,510:-1,511:199,519:1,550:5,551:0,552:10,553:0,554:190403,555:181669,6:192,7:950,8:262147,9:19040}";
			String[] p1 = StringUtils.split(command, " ");
			map.put("head", p1[0]);
			map.put("seq", p1[1]);
			map.put("macid", p1[2]);
			map.put("channel", p1[3]);
			map.put("mtype", p1[4]);
			map.put("content", p1[5]); 
			map.put("utc", String.valueOf(System.currentTimeMillis()));
			map.put("maplon", "0");
			map.put("maplat", "0");
			map.put("ratio", "-100");
			map.put("gears", "-100");
			
			
			String[] contents = StringUtils.split(p1[5].substring(1, p1[5].length()-1), ","); 
			for(String str : contents){
				String[] texts = StringUtils.split(str, ":");
				if(texts.length == 2){
					map.put(texts[0], texts[1]);
				}
			}
			map.put("vid", "32");
			map.put("3", "1390");
			service = new OracleService();
			service.initService();
			service.initOnlineStatement();
			service.initEquipmentPreparedStatement();
		} catch (Exception e) {
			e.printStackTrace();
		} 
	}
	
	/**
	 * 更新最后位置在线信息
	 */
	@Test
	public void testUpdateLastTrackLine() {
		map.put("vid", "32");
		map.put("3", "1390");
		service.updateLastTrackLine(map); 
		service.commit(); 
	}

	/**
	 * 更新最后位置在线信息
	 */
	@Test
	public void testUpdateLastTrack() {
		map.put("vid", "32");
		map.put("3", "1380");
		service.updateLastTrack(map);
		service.commit(); 
	}

	/**
	 * 更新非法轨迹信息状态
	 */
	@Test
	public void testUpdateLastTrackISonLine() {
		map.put("vid", "32");
		service.updateLastTrackISonLine("32", "222");
		service.commit(); 
	}

	/**
	 * 
	 */
	@Test
	public void testUpdateVehicleLineStatus() {
		try {
			StatusHandlerThread state = new StatusHandlerThread(0, service);
			map.put("vid", "32");
			EquipmentStatus eqStatus = state.analyVehicleLineStatus(map);
			eqStatus.setVid("32");
			service.updateVehicleLineStatus(eqStatus);
			service.commit(); 
		} catch (Exception e) {
			e.printStackTrace(); 
		}
	}

	/**
	 * 查询状态码
	 * @param vid
	 * @return
	 */
	@Test
	public void testQueryStatusCode() {
//		SELECT V.VID, TERMINAL_STATUS_VALTYPE,MIN_TERMINAL_STATUS,MAX_TERMINAL_STATUS, GPS_STATUS_VALTYPE,MIN_GPS_STATUS,MAX_GPS_STATUS, E_WATER_TEMP_VALTYPE,MIN_E_WATER_TEMP,MAX_E_WATER_TEMP, EXT_VOLTAGE_VALTYPE,MIN_EXT_VOLTAGE,MAX_EXT_VOLTAGE, OIL_PRESSURE_VALTYPE,MIN_OIL_PRESSURE,MAX_OIL_PRESSURE, BRAKE_PRESSURE_VALTYPE,MIN_BRAKE_PRESSURE,MAX_BRAKE_PRESSURE, BRAKEPAD_FRAY_VALTYPE,MIN_BRAKEPAD_FRAY,MAX_BRAKEPAD_FRAY, OIL_ALARM_VALTYPE,MIN_OIL_ALARM,MAX_OIL_ALARM,ABS_BUG_VALTYPE, MIN_ABS_BUG,MAX_ABS_BUG,COOLANT_LEVEL_VALTYPE,MIN_COOLANT_LEVEL, MAX_COOLANT_LEVEL,AIR_FILTER_CLOG_VALTYPE,MIN_AIR_FILTER_CLOG, MAX_AIR_FILTER_CLOG,MWERE_BLOCKING_VALTYPE,MIN_MWERE_BLOCKING, MAX_MWERE_BLOCKING,FUEL_BLOCKING_VALTYPE,MIN_FUEL_BLOCKING, MAX_FUEL_BLOCKING,EOIL_TEMPERATURE_VALTYPE,MIN_EOIL_TEMPERATURE_ALARM, MAX_EOIL_TEMPERATURE_ALARM,RETARDER_HT_ALARM_VALTYPE,MIN_RETARDER_HT_ALARM, MAX_RETARDER_HT_ALARM,EHOUSING_HT_ALARM_VALTYPE,MIN_EHOUSING_HT_ALARM ,MAX_EHOUSING_HT_ALARM,AIR_PRESSURE_VALTYPE,MIN_AIR_PRESSURE,MAX_AIR_PRESSURE FROM TB_VSTATUS_REF S INNER JOIN TB_VEHICLE V ON S.VS_REF_ID = V.VS_REF_ID WHERE V.ENABLE_FLAG='1' ORDER BY vid
		StatusCode code = service.queryStatusCode("7");
		service.commit(); 
		Assert.assertNotNull(code); 
	}

	/**
	 * 测试 - 存储上线记录
	 */
	@Test
	public void testSaveOnline() {
		service.saveOnline("32", "测A00001", UUID.randomUUID().toString());
		service.commit(); 
	}

	/**
	 * 测试 - 存储上下线记录
	 */
	@Test
	public void saveOffline() {
		String uuid = UUID.randomUUID().toString();
		service.saveOnline("32", "测A00001", uuid);
		service.saveOffline(uuid);
		service.commit();
	}

	/**
	 * 更新上下线状态
	 */
	@Test
	public void testUpdateOnOfflineStatus() {
		service.updateOnOfflineStatus(0, "10001", "32");
		service.commit();
	}
	
	/**
	 * 更新线路信息
	 */
	@SuppressWarnings("static-access")
	@Test
	public void testQueryLineStationBind() {
		service.queryLineStationBind();
		service.commit();
	}
}
