package com.caits.analysisserver.addin.kcpt.statisticanalysis;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;


public class SummaryVehicleDay {
	private static final Logger logger = LoggerFactory.getLogger(SummaryVehicleDay.class);
	
	private String sql_selectVehicleDayStat = null;
	
	private String updateStaInfo = null; 
	
	private long utc = 0; // 指定查询UTC时间
	
	public void initAnalyser(){

		// 更新车辆总累计信息
		updateStaInfo = SQLPool.getinstance().getSql("sql_updateStaInfo");
		
		sql_selectVehicleDayStat = SQLPool.getinstance().getSql("sql_selectVehicleDayStat");
	}
	
	public void run(){
		//从连接池获取连接
		try {
			updateVehicleStat();
		} catch (SQLException e) {
			logger.error("更新车辆总累计表出错.",e);
		}
		
	}
	
	// 设置统计时间
	public  void setTime(long utc){
		this.utc = utc;
	}
	
	/****
	 * 更新车辆总累计表
	 * @throws SQLException
	 */
	private void updateVehicleStat() throws SQLException{
		PreparedStatement stUpdateStaInfo = null;
		PreparedStatement stSelectStatInfo = null;
		ResultSet rs = null;
		Connection dbCon = null;
		String currVid = "";
		try{
			dbCon = OracleConnectionPool.getConnection();
			stSelectStatInfo = dbCon.prepareStatement(sql_selectVehicleDayStat);
			stUpdateStaInfo = dbCon.prepareStatement(updateStaInfo);
			stSelectStatInfo.setLong(1, this.utc - 12*60*60*1000);
			stSelectStatInfo.setLong(2, this.utc + 12*60*60*1000);
			
			rs = stSelectStatInfo.executeQuery();
			int count = 0;
			while(rs.next()){
				stUpdateStaInfo.setLong(1, rs.getLong("ONLINE_TIMES"));
				stUpdateStaInfo.setLong(2, rs.getLong("ONLINE_TIME"));
				stUpdateStaInfo.setLong(3, rs.getLong("ENGINE_ROTATE_TIME"));
				stUpdateStaInfo.setLong(4, rs.getLong("MILEAGE"));
				stUpdateStaInfo.setLong(5, rs.getLong("OIL_WEAR"));	
				stUpdateStaInfo.setLong(6, rs.getLong("SPEEDING_OIL"));
				stUpdateStaInfo.setLong(7, rs.getLong("SPEEDING_MILEAGE"));
			
				stUpdateStaInfo.setLong(8, rs.getLong("SPEED_MAX"));
				stUpdateStaInfo.setLong(9, rs.getLong("RPM_MAX"));
				stUpdateStaInfo.setLong(10, rs.getLong("VCL_GPS_AMOUNT"));
				stUpdateStaInfo.setLong(11, rs.getLong("VCL_GPS_INVALID_AMOUNT"));
				stUpdateStaInfo.setLong(12, rs.getLong("VCL_GPS_TIMEINVALID_AMOUNT"));
				stUpdateStaInfo.setLong(13, rs.getLong("VCL_GPS_LONINVALID_AMOUNT"));
				stUpdateStaInfo.setLong(14, rs.getLong("TOTAL_ALARM"));
				stUpdateStaInfo.setLong(15, rs.getLong("ALARM_DEALT"));
				stUpdateStaInfo.setLong(16, rs.getLong("EMERGENCY_ALARM"));
				
				stUpdateStaInfo.setLong(17, rs.getLong("ACC_CLOSE_NUM"));
				stUpdateStaInfo.setLong(18, rs.getLong("ACC_CLOSE_TIME"));
				stUpdateStaInfo.setLong(19, rs.getLong("OVERSPEED_ALARM"));
				stUpdateStaInfo.setLong(20, rs.getLong("OVERSPEED_TIME"));
				
				stUpdateStaInfo.setLong(21,rs.getLong("FATIGUE_ALARM"));
				stUpdateStaInfo.setLong(22,rs.getLong("FATIGUE_TIME"));
				
				stUpdateStaInfo.setLong(23,rs.getLong("GNSS_BUG_NUM"));
				stUpdateStaInfo.setLong(24,rs.getLong("GNSS_BUG_TIME"));
			
				stUpdateStaInfo.setLong(25,rs.getLong("GNSS_UNANTENAN_NUM"));
				stUpdateStaInfo.setLong(26,rs.getLong("GNSS_UNANTENAN_TIME"));
			
				stUpdateStaInfo.setLong(27,rs.getLong("ANTENAN_SHORTOUT_NUM"));
				stUpdateStaInfo.setLong(28,rs.getLong("ANTENAN_SHORTOUT_TIME"));
			
				stUpdateStaInfo.setLong(29,rs.getLong("MPOWER_UNDERVOLTAGE_NUM"));
				stUpdateStaInfo.setLong(30,rs.getLong("MPOWER_UNDERVOLTAGE_TIME"));
			
				stUpdateStaInfo.setLong(31,rs.getLong("MPOWER_DOWN_NUM"));
				stUpdateStaInfo.setLong(32,rs.getLong("MPOWER_DOWN_TIME"));

				stUpdateStaInfo.setLong(33,rs.getLong("LCD_BUG_NUM"));
				stUpdateStaInfo.setLong(34,rs.getLong("LCD_BUG_TIME"));
	
				stUpdateStaInfo.setLong(35,rs.getLong("TIS_BUG_NUM"));
				stUpdateStaInfo.setLong(36,rs.getLong("TIS_BUG_TIME"));
			
				stUpdateStaInfo.setLong(37,rs.getLong("CAMERA_BUG_NUM"));
				stUpdateStaInfo.setLong(38,rs.getLong("CAMERA_BUG_TIME"));

				stUpdateStaInfo.setLong(39,rs.getLong("DRIVER_TIMEOUT_TIME"));
			
				stUpdateStaInfo.setLong(40,rs.getLong("STOP_TIMOUT_NUM"));
				stUpdateStaInfo.setLong(41,rs.getLong("STOP_TIMOUT_TIME"));
			
				stUpdateStaInfo.setLong(42,rs.getLong("INAREA_ALARM"));
				stUpdateStaInfo.setLong(43,rs.getLong("OUTAREA_ALARM"));
			
				stUpdateStaInfo.setLong(44,rs.getLong("IN_ROUTE_NUM"));
				stUpdateStaInfo.setLong(45,rs.getLong("OUT_ROUTE_NUM"));
				
				stUpdateStaInfo.setLong(46,rs.getLong("ROUTE_RUN_DIFF_NUM"));
		
				stUpdateStaInfo.setLong(47,rs.getLong("ROUTE_RUN_NUM"));
			
				stUpdateStaInfo.setLong(48,rs.getLong("DEVIATE_ROUTE_ALARM"));
				stUpdateStaInfo.setLong(49,rs.getLong("DEVIATE_ROUTE_TIME"));

				stUpdateStaInfo.setLong(50,rs.getLong("VSS_BUG_NUM"));
				stUpdateStaInfo.setLong(51,rs.getLong("VSS_BUG_TIME"));
			
				// 车辆油量异常告警次数 // 车辆油量异常告警时长
				stUpdateStaInfo.setLong(52,rs.getLong("OILMASS_UNUSUAL_NUM"));
				stUpdateStaInfo.setLong(53,rs.getLong("OILMASS_UNUSUAL_TIME"));
			
				// 车辆被盗时长
				stUpdateStaInfo.setLong(54,rs.getLong("VEHICLE_BESTOLEN_TIME"));
			
				// 车辆非法点火次数
				stUpdateStaInfo.setLong(55,rs.getLong("ILLEGAL_FIRE_NUM"));
			
				// 车辆非法移位次数
				stUpdateStaInfo.setLong(56,rs.getLong("ILLEGAL_MOVE_NUM"));
			
				// 碰撞侧翻报警次数 // 碰撞侧翻报警时长
				stUpdateStaInfo.setLong(57,rs.getLong("CASH_ALARM_NUM"));
				stUpdateStaInfo.setLong(58,rs.getLong("CASH_ALARM_TIME"));
			
				// 冷却液温度告警次数 // 冷却液温度告警时长
				stUpdateStaInfo.setLong(59,rs.getLong("E_WATER_TEMP_NUM"));
				stUpdateStaInfo.setLong(60,rs.getLong("E_WATER_TEMP_TIME"));
			
				// 机油压力告警次数 // 机油压力告警时长
				stUpdateStaInfo.setLong(61,rs.getLong("EOIL_PRESSURE_NUM"));
				stUpdateStaInfo.setLong(62,rs.getLong("EOIL_PRESSURE_TIME"));
			
				// 蓄电池电压告警次数 // 蓄电池电压告警时长
				stUpdateStaInfo.setLong(63,rs.getLong("BATTERY_VOLTAGE_NUM"));
				stUpdateStaInfo.setLong(64,rs.getLong("BATTERY_VOLTAGE_TIME"));
			
				// 制动气压告警次数 // 制动气压告警时长
				stUpdateStaInfo.setLong(65,rs.getLong("TRIG_PRESSURE_NUM"));
				stUpdateStaInfo.setLong(66,rs.getLong("TRIG_PRESSURE_TIME"));
			
				// 燃油告警次数  // 燃油告警时长
				stUpdateStaInfo.setLong(67,rs.getLong("OIL_ALARM_NUM"));
				stUpdateStaInfo.setLong(68,rs.getLong("OIL_ALARM_TIME"));
			
				// 水位低告警次数 // 水位低告警时长
				stUpdateStaInfo.setLong(69,rs.getLong("STAGE_LOW_ALARM_NUM"));
				stUpdateStaInfo.setLong(70,rs.getLong("STAGE_LOW_ALARM_TIME"));
			
				// 燃油堵塞次数 // 燃油堵塞时长
				stUpdateStaInfo.setLong(71,rs.getLong("FUEL_BLOCKING_ALARM_NUM"));
				stUpdateStaInfo.setLong(72,rs.getLong("FUEL_BLOCKING_ALARM_TIME"));
			
				// 机油温度次数 // 机油温度时长
				stUpdateStaInfo.setLong(73,rs.getLong("EOIL_TEMPERATURE_ALARM_NUM"));
				stUpdateStaInfo.setLong(74,rs.getLong("EOIL_TEMPERATURE_ALARM_TIME"));
			
				// 缓速器高温次数 // 缓速器高温时长
				stUpdateStaInfo.setLong(75,rs.getLong("RETARDER_HT_ALARM_NUM"));
				stUpdateStaInfo.setLong(76,rs.getLong("RETARDER_HT_ALARM_TIME"));
			
				// 仓温高告警次数 // 仓温高告警时长
				stUpdateStaInfo.setLong(77,rs.getLong("EHOUSING_HT_ALARM_NUM"));
				stUpdateStaInfo.setLong(78,rs.getLong("EHOUSING_HT_ALARM_TIME"));
			
				// 发动机超转次数 // 发动机超转时长
				stUpdateStaInfo.setLong(79,rs.getLong("OVERRPM_ALARM"));
				stUpdateStaInfo.setLong(80,rs.getLong("OVERRPM_TIME"));
			
				// 二档起步次数 // 二档起步时长
				stUpdateStaInfo.setLong(81,rs.getLong("GEAR_WRONG_NUM"));
				stUpdateStaInfo.setLong(82,rs.getLong("GEAR_WRONG_TIME"));
			
				// 空档滑行次数  // 空档滑行时长
				stUpdateStaInfo.setLong(83,rs.getLong("GEAR_GLIDE_NUM"));
				stUpdateStaInfo.setLong(84,rs.getLong("GEAR_GLIDE_TIME"));
			
				// 急加速次数 // 急加速时长
				stUpdateStaInfo.setLong(85,rs.getLong("URGENT_SPEED_NUM"));
				stUpdateStaInfo.setLong(86,rs.getLong("URGENT_SPEED_TIME"));
			
				// 急减速次数 // 急减速时长
				stUpdateStaInfo.setLong(87,rs.getLong("URGENT_LOWDOWN_NUM"));
				stUpdateStaInfo.setLong(88,rs.getLong("URGENT_LOWDOWN_TIME"));
			
				// 超长怠速次数 // 超长怠速时长
				stUpdateStaInfo.setLong(89,rs.getLong("LONG_IDLE_NUM"));
				stUpdateStaInfo.setLong(90,rs.getLong("LONG_IDLE_TIME"));
			
				// 怠速空调次数 // 怠速空调时长
				stUpdateStaInfo.setLong(91,rs.getLong("AIR_CONDITION_NUM"));
				stUpdateStaInfo.setLong(92,rs.getLong("AIR_CONDITION_TIME"));
			
				// 制动蹄片磨损次数 // 制动蹄片磨损时长
				stUpdateStaInfo.setLong(93,rs.getLong("BRAKE_SHOE_NUM"));
				stUpdateStaInfo.setLong(94,rs.getLong("BRAKE_SHOE_TIME"));
			
				// 空滤堵塞次数 // 空滤堵塞时长
				stUpdateStaInfo.setLong(95,rs.getLong("AIR_FILTER_CLOG_NUM"));
				stUpdateStaInfo.setLong(96,rs.getLong("AIR_FILTER_CLOG_TIME"));
			
				// 超经济区运行时长
				stUpdateStaInfo.setLong(97,rs.getLong("ECONOMIC_RUN_TIME"));
			
				// 区域超速告警次数 // 区域超速告警时长
				stUpdateStaInfo.setLong(98,rs.getLong("AREA_OVERSPEED_ALARM"));
				stUpdateStaInfo.setLong(99,rs.getLong("AREA_OVERSPEED_TIME"));
			
				// 加热器运行时长
				stUpdateStaInfo.setLong(100,rs.getLong("HEATUP_TIME"));
			
				// 空调开启时间
				stUpdateStaInfo.setLong(101,rs.getLong("AIRCONDITION_TIME"));
				
				stUpdateStaInfo.setLong(102,rs.getLong("DOOR1_OPEN_NUM"));
				stUpdateStaInfo.setLong(103,rs.getLong("DOOR2_OPEN_NUM"));
				stUpdateStaInfo.setLong(104,rs.getLong("DOOR3_OPEN_NUM"));
				stUpdateStaInfo.setLong(105, rs.getLong("DOOR4_OPEN_NUM"));
				stUpdateStaInfo.setLong(106,rs.getLong("DOOR_OPEN_NUM"));
				stUpdateStaInfo.setLong(107, rs.getLong("AREA_OPENDOOR_NUM"));
				stUpdateStaInfo.setLong(108,rs.getLong("AREA_OPENDOOR_TIME"));
		
				stUpdateStaInfo.setLong(109,rs.getLong("MWERE_BLOCKING_NUM"));
				stUpdateStaInfo.setLong(110,rs.getLong("MWERE_BLOCKING_TIME"));
				
				stUpdateStaInfo.setLong(111,rs.getLong("OVERLOAD_NUM"));
				//非法停靠次数 // 非法停靠时长
				stUpdateStaInfo.setLong(112,rs.getLong("ILLEGAL_STOP_NUM"));
				stUpdateStaInfo.setLong(113,rs.getLong("ILLEGAL_STOP_TIME"));
			
				//档位不当次数 // 档位不当持续时间
				stUpdateStaInfo.setLong(114,rs.getLong("GEAR_IMPROPER"));
				stUpdateStaInfo.setLong(115,rs.getLong("GEAR_TIME"));
				
				stUpdateStaInfo.setLong(116,rs.getLong("IDLING_TIME"));
				stUpdateStaInfo.setLong(117,rs.getLong("RUNNING_OIL")); // 行车总累计油耗
				stUpdateStaInfo.setLong(118,rs.getLong("RETARDER_WORK_TIME"));
				stUpdateStaInfo.setLong(119,rs.getLong("RETARDER_WORK_NUM"));
				stUpdateStaInfo.setLong(120,rs.getLong("BRAKE_TIME"));
				stUpdateStaInfo.setLong(121,rs.getLong("BRAKE_NUM"));
				stUpdateStaInfo.setLong(122,rs.getLong("REVERSE_GEAR_TIME"));
				stUpdateStaInfo.setLong(123,rs.getLong("REVERSE_GEAR_NUM"));
				stUpdateStaInfo.setLong(124,rs.getLong("LOWER_BEAM_TIME"));
				stUpdateStaInfo.setLong(125,rs.getLong("LOWER_BEAM_NUM"));
				stUpdateStaInfo.setLong(126,rs.getLong("HIGH_BEAM_TIME"));
				stUpdateStaInfo.setLong(127,rs.getLong("HIGH_BEAM_NUM"));
				stUpdateStaInfo.setLong(128,rs.getLong("LEFT_TURNING_SIGNAL_TIME"));
				stUpdateStaInfo.setLong(129,rs.getLong("LEFT_TURNING_SIGNAL_NUM"));
				stUpdateStaInfo.setLong(130,rs.getLong("RIGHT_TURNING_SIGNAL_TIME"));
				stUpdateStaInfo.setLong(131,rs.getLong("RIGHT_TURNING_SIGNAL_NUM"));
				stUpdateStaInfo.setLong(132,rs.getLong("OUTLINE_LAMP_TIME"));
				stUpdateStaInfo.setLong(133,rs.getLong("OUTLINE_LAMP_NUM"));
				stUpdateStaInfo.setLong(134,rs.getLong("TRUMPET_TIME"));
				stUpdateStaInfo.setLong(135,rs.getLong("TRUMPET_NUM"));
				stUpdateStaInfo.setLong(136,rs.getLong("AIRCONDITION_NUM"));
				stUpdateStaInfo.setLong(137,rs.getLong("FREE_POSITION_TIME"));
				stUpdateStaInfo.setLong(138,rs.getLong("FREE_POSITION_NUM"));
				stUpdateStaInfo.setLong(139,rs.getLong("ABS_WORK_TIME"));
				stUpdateStaInfo.setLong(140,rs.getLong("ABS_WORK_NUM"));
				stUpdateStaInfo.setLong(141,rs.getLong("HEAT_UP_TIME"));
				stUpdateStaInfo.setLong(142,rs.getLong("HEAT_UP_NUM"));
				stUpdateStaInfo.setLong(143,rs.getLong("CLUTCH_TIME"));
				stUpdateStaInfo.setLong(144,rs.getLong("CLUTCH_NUM"));
				stUpdateStaInfo.setLong(145,rs.getLong("OPENING_DOOR_EX_NUM"));
				stUpdateStaInfo.setLong(146,rs.getLong("PRECISE_OIL"));
				stUpdateStaInfo.setLong(147,rs.getLong("HEAD_COLLIDE"));
				stUpdateStaInfo.setLong(148,rs.getLong("VEHICLE_DEVIATE"));
				stUpdateStaInfo.setString(149,rs.getString("VID"));
				currVid = rs.getString("VID");
				stUpdateStaInfo.addBatch();
				count++;
				if(count % 100 == 0){
					stUpdateStaInfo.executeBatch();
					count = 0;
					stUpdateStaInfo.clearBatch();
				}
			}
			
			if(count >0){
				stUpdateStaInfo.executeBatch(); // 更新车辆总累计表
			}

		}catch(SQLException e){
			logger.error("VID:"+currVid+"更新车辆总累计出错。",e);
		}finally{
			
			if(rs != null){
				rs.close();
			}
			
			if(stSelectStatInfo != null){
				stSelectStatInfo.close();
			}
			
			if(stUpdateStaInfo != null){
				stUpdateStaInfo.close();
			}
			
			if(dbCon != null){
				dbCon.close();
			}
		}
	}
}
