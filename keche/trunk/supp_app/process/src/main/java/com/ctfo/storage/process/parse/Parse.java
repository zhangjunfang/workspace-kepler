package com.ctfo.storage.process.parse;

import java.nio.ByteBuffer;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.model.Location;
import com.ctfo.storage.process.model.Oil;
import com.ctfo.storage.process.model.ThEvent;
import com.ctfo.storage.process.model.Track;
import com.ctfo.storage.process.model.Vehicle;
import com.ctfo.storage.process.model.VehicleTemp;
import com.ctfo.storage.process.service.MySqlService;
import com.ctfo.storage.process.util.Cache;
import com.ctfo.storage.process.util.Converter;
import com.ctfo.storage.process.util.Point;
import com.ctfo.storage.process.util.Tools;

public class Parse extends Thread {
	private static Logger log = LoggerFactory.getLogger(Parse.class);
	/**	数据队列	*/
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(500000);
	/**	计数器	*/
	private int index;
	/**	最后记录时间	*/
	private long lastTime = System.currentTimeMillis();
	/**		*/
	private StatusProcess statusProcess;
	/**		*/
	private TrackFileProcess trackFileProcess;
	/**		*/
	private AlarmProcess alarmProcess;
	/**		*/
	private EventProcess eventProcess;
	/**		*/
	private OilProcess oilProcess;
	/**		*/
	private RealTimeProcess realTimeProcess;
	/**		*/
	private SimpleDateFormat dateFormat = new SimpleDateFormat("yyMMddHHmmss");

	private List<VehicleTemp> vehicleList = new ArrayList<VehicleTemp>();
	
	private int batch = 0;
	
	private MySqlService mySqlService;
	
	/**
	 * 初始化数据
	 * @throws Exception 
	 */
	public Parse() throws Exception {
		setName("Parse-Thread"); 
		mySqlService = new MySqlService();
		mySqlService.setSql_insertVehicle("insert into VEHICLE(VID,ENTID,PHONE,PLATE,PLATECOLOR,VINCODE,INNERCODE,TID,TERMINALTYPE,OEMCODE,ENTNAME,TEAMID,TEAMNAME,STAFFID,STAFFNAME,ISONLINE) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)");
		
		statusProcess = new StatusProcess();
		statusProcess.start();

		trackFileProcess = new TrackFileProcess();
		trackFileProcess.start();

		//alarmProcess = new AlarmProcess();
		//alarmProcess.start();

		eventProcess = new EventProcess();
		eventProcess.start();

		oilProcess = new OilProcess();
		oilProcess.start();

		realTimeProcess = new RealTimeProcess();
		realTimeProcess.start();
	}

	public void run() {
		while (true) {
			try {
				String message = queue.take();// 获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
				index++;
				process(message);
				long current = System.currentTimeMillis();
				if ((current - lastTime) > 10000) { 
					log.info("-----【totalProcess】：10秒处理[{}]条,处理线程排队[{}],应答[{}],应答线程排队[{}],【Cache】：[{}]条------------", index,queue.size(),ResponseListen.getCount(),ResponseListen.getQueue().size(),Cache.getVehicleMap().size());
					index = 0;
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				log.error("处理队列数据异常:" + e.getMessage(), e);
			}
		}
	}

	/**
	 * 根据类型处理数据
	 * @param command
	 */
	private void process(String command) {
		String subType = command.substring(38, 42); // 消息子类型 4
		if (subType.equals("1205")) { 			// 实时位置
			processRealTimeLocationData(command);
		} else if (subType.equals("1206")) { 	// 位置补传
			processHistoryLocationData(command);
		} else if (subType.equals("1209")) { 	// 驾驶行为事件
			processEventData(command);
		} else if (subType.equals("120b")) { 	// 数据透传
			processPassThroughData(command);
		}
	}

	/**
	 * 处理透传数据
	 * 
	 * @param command
	 */
	private void processPassThroughData(String command) {
		// String from = command.substring(2, 10); // 消息来源 8
		// String to = command.substring(10, 18); // 消息目的地 8
		// String patype = command.substring(18, 22); // 消息大类 4
		// String length = command.substring(30, 38); // 长度 8
		// int dataLength = Integer.parseInt(length, 16);//
		// String serial = command.substring(22, 30); // 序列号 8
		String phone = command.substring(42, 66); // 手机号 24
		String phoneNumber = Tools.getChinese(phone).trim();
		//Vehicle vehicle = Cache.getVehicle(phoneNumber); // 车辆信息缓存
		Vehicle vehicle = getVehicle(phoneNumber);
//		vehicle = new Vehicle();
//		vehicle.setPlate("testaa");
//		vehicle.setVid("2");
//		vehicle.setVehicleType("1");
		String passThroughTypeStr = command.substring(66, 68); // 透传类型
		int passThroughType = Integer.parseInt(passThroughTypeStr, 16);
		if (passThroughType == 131) {
			String passThroughStr = command.substring(68, command.length() - 4); // 位置信息汇报
																					// 56
			Oil oil = new Oil();
			oil.setPassThroughStr(passThroughStr);
			oil.setVid(vehicle.getVid());
			oilProcess.put(oil);
		}
	}

	/**
	 * 处理驾驶行为事件数据
	 * 
	 * @param command
	 */
	private void processEventData(String command) {
		// String from = command.substring(2, 10); // 消息来源 8
		// String to = command.substring(10, 18); // 消息目的地 8
		// String patype = command.substring(18, 22); // 消息大类 4
		// String length = command.substring(30, 38); // 长度 8
		// int dataLength = Integer.parseInt(length, 16);//
		// String serial = command.substring(22, 30); // 序列号 8
		String phone = command.substring(42, 66); // 手机号 24
		String phoneNumber = Tools.getChinese(phone).trim();
		//Vehicle vehicle = Cache.getVehicle(phoneNumber); // 车辆信息缓存
		Vehicle vehicle = getVehicle(phoneNumber);
		if (vehicle != null) {
			String eventIdStr = command.substring(66, 68); // 事件编号
			int eventId = Integer.parseInt(eventIdStr, 16);
			String startTrackStr = command.substring(68, 124); // 位置信息汇报 56
			String endTrackStr = command.substring(124, 180); // 位置信息汇报 56
			Track startTrack = getTrack(startTrackStr);
			Track endTrack = getTrack(endTrackStr);
			ThEvent event = new ThEvent();
			event.setVid(vehicle.getPlate());
			event.setEventType(eventId);
			event.setSysTime(System.currentTimeMillis());
			event.setStartLon(startTrack.getLon());
			event.setStartLat(startTrack.getLat());
			event.setStartElevation(startTrack.getElevation());
			event.setStartDirection(startTrack.getDirection());
			event.setStartSpeed(startTrack.getSpeed());
			event.setStartTime(startTrack.getUtcTime());
			event.setEndLon(endTrack.getLon());
			event.setEndLat(endTrack.getLat());
			event.setEndElevation(endTrack.getElevation());
			event.setEndDirection(endTrack.getDirection());
			event.setEndSpeed(endTrack.getSpeed());
			event.setEndTime(endTrack.getUtcTime());
			eventProcess.put(event);
		}
	}

	/**
	 * 处理历史位置数据
	 * 
	 * @param command
	 */
	private void processHistoryLocationData(String command) {
		// String from = command.substring(2, 10); // 消息来源 8
		// String to = command.substring(10, 18); // 消息目的地 8
		// String patype = command.substring(18, 22); // 消息大类 4
		// String length = command.substring(30, 38); // 长度 8
		// int dataLength = Integer.parseInt(length, 16);//
		// String serial = command.substring(22, 30); // 序列号 8
		// String phone = command.substring(42, 66); // 手机号 24
		// String phoneNumber = Tools.getChinese(phone).trim();
		String phone = command.substring(42, 66); // 手机号 24
		String phoneNumber = Tools.getChinese(phone).trim();
		//Vehicle vehicle = Cache.getVehicle(phoneNumber); // 车辆信息缓存
		Vehicle vehicle = getVehicle(phoneNumber);
		if (vehicle != null) {
			String paramCount = command.substring(66, 70);
			String paramsStr = command.substring(70, command.length() - 4);
			int count = Integer.parseInt(paramCount, 16);
			Location location = parseLocation(paramsStr, count);
			location.setVid(vehicle.getVid());
			location.setPlate(vehicle.getPlate());
			location.setPlateColor(vehicle.getPlateColor());
			location.setVinCode(vehicle.getVinCode());
			location.setInnerCode(vehicle.getInnerCode());
			location.setTid(vehicle.getTid());
			location.setTerminalType(vehicle.getTerminalType());
			location.setPhoneNumber(phoneNumber);
			location.setOemCode(vehicle.getOemCode());
			location.setEntId(vehicle.getEntId());
			location.setEntName(vehicle.getEntName());
			location.setTeamId(vehicle.getTeamId());
			location.setTeamName(vehicle.getTeamName());
			location.setStaffId(vehicle.getStaffId());
			location.setStaffName(vehicle.getStaffName());
			location.setOnline(1);

			trackFileProcess.put(location);
		}
	}

	/**
	 * 处理实时位置数据
	 * 
	 * @param command
	 */
	private void processRealTimeLocationData(String command) {
		// String from = command.substring(2, 10); // 消息来源 8
		// String to = command.substring(10, 18); // 消息目的地 8
		// String patype = command.substring(18, 22); // 消息大类 4
		// String length = command.substring(30, 38); // 长度 8
		// int dataLength = Integer.parseInt(length, 16);//
		// String serial = command.substring(22, 30); // 序列号 8
		String phone = command.substring(42, 66); // 手机号 24
		String phoneNumber = Tools.getChinese(phone).trim();
		//Vehicle vehicle = Cache.getVehicle(phoneNumber); // 车辆信息缓存
		Vehicle vehicle = getVehicle(phoneNumber);
		if(vehicle != null){
			String paramCount = command.substring(66, 70); // 参数总数
			String paramsStr = command.substring(70, command.length() - 4);
			int count = Integer.parseInt(paramCount, 16);
			Location location = parseLocation(paramsStr, count);
			location.setVid(vehicle.getVid());
			location.setPlate(vehicle.getPlate()); 
			location.setPlateColor(vehicle.getPlateColor());
			location.setVinCode(vehicle.getVinCode()); 
			location.setInnerCode(vehicle.getInnerCode());
			location.setTid(vehicle.getTid());
			location.setTerminalType(vehicle.getTerminalType());
			location.setPhoneNumber(phoneNumber); 
			location.setOemCode(vehicle.getOemCode());
			location.setEntId(vehicle.getEntId()); 
			location.setEntName(vehicle.getEntName());
			location.setTeamId(vehicle.getTeamId());
			location.setTeamName(vehicle.getTeamName());
			location.setStaffId(vehicle.getStaffId());
			location.setStaffName(vehicle.getStaffName());
			location.setOnline(1); 
			
			trackFileProcess.put(location);
			//alarmProcess.put(location);
			statusProcess.put(location);
			realTimeProcess.put(location);
		} else {
			VehicleTemp v = new VehicleTemp();
			v.setVid(System.nanoTime()); 
			v.setEntId(System.currentTimeMillis()); 
			v.setPhoneNumber(Long.parseLong(phoneNumber));
			v.setPlate("测C12345");
			v.setPlateColor(2);
			v.setVinCode("123456");
			v.setInnerCode(654321);
			v.setTid(11111);
			v.setTerminalType(1);
			v.setOemCode("E001");
			v.setEntName("宇通客车");
			v.setTeamId(601260);
			v.setTeamName("默认车队");
			v.setStaffId(999);
			v.setStaffName("无名");
			v.setOnline(1);
			
			
			
			vehicleList.add(v);
			batch++;
			if(batch == 1000){
				mySqlService.insertVehicle(vehicleList); 
				vehicleList.clear();
				batch = 0;
			}
		}
	}

	/**
	 * 解析参数
	 * 
	 * @param params
	 * @param count
	 * @return
	 */
	private Location parseLocation(String params, int count) {
		Location location = new Location();
		byte[] paramsByte = Tools.hexStrToBytes(params);
		ByteBuffer bb = ByteBuffer.wrap(paramsByte);
		for (int i = 0; i < count; i++) {
			switch (bb.getInt()) {
			case 1:
				bb.getInt(); // 字段长度
				int value = bb.getInt();
				String alarmStr = Integer.toBinaryString(value);
				String alarmFlag = parseAlarm(alarmStr);
				location.setAlarmFlag(alarmFlag);
				break;
			case 2:
				bb.getInt(); // 字段长度
				int status = bb.getInt();
				String statusFlag = Integer.toBinaryString(status);
				location.setStatusFlag(statusFlag);
				break;
			case 3:
				bb.getInt(); // 字段长度
				int lon = bb.getInt();
				location.setLon(lon);
				break;
			case 4:
				bb.getInt(); // 字段长度
				int lat = bb.getInt();
				location.setLat(lat);
				break;
			case 5: // 高度海拔
				bb.getInt(); // 字段长度
				Short elevation = bb.getShort();
				location.setElevation(elevation);
				break;
			case 6:// GPS速度
				bb.getInt(); // 字段长度
				Short gpsSpeed = bb.getShort();
				location.setGpsSpeed(gpsSpeed);
				break;
			case 7: // 方向
				bb.getInt(); // 字段长度
				Short direction = bb.getShort();
				location.setDirection(direction);
				break;
			case 8: // 时间
				bb.getInt(); // 字段长度
				byte[] timeByte = new byte[14];
				bb.get(timeByte, 0, 14);
				String bcdTime = Tools.bytesToHexStr(timeByte);
				String bcdTime2 =Tools.getChinese(bcdTime);
				location.setBcdTime(bcdTime2);
				break;
			case 9: // 里程
				bb.getInt(); // 字段长度
				int mileage = bb.getInt();
				location.setMileage(mileage);
				break;
			case 10: // 油量
				bb.getInt(); // 字段长度
				Short oil = bb.getShort();
				location.setOil(oil);
				break;
			case 11: // 行驶记录仪速度
				bb.getInt(); // 字段长度
				Short vssSpeed = bb.getShort();
				location.setVssSpeed(vssSpeed);
				break;
			case 12: // 人工确认报警编号
				bb.getInt(); // 字段长度
				Short acknowledgeAlarmId = bb.getShort();
				location.setAcknowledgeAlarmId(acknowledgeAlarmId);
				break;
			case 13: // 超速告警附加信息
				bb.getInt(); // 字段长度
				byte trackType = bb.get(); // 位置类型
				switch (trackType) {
				case 0x01: // 圆形区域
					int round = bb.getInt();
					location.setOverspeedAlarmAdded("1=" + round);
					break;
				case 0x02: // 矩形区域
					int rectangle = bb.getInt();
					location.setOverspeedAlarmAdded("1=" + rectangle);
					break;
				case 0x03: // 多边形区域
					int polygon = bb.getInt();
					location.setOverspeedAlarmAdded("1=" + polygon);
					break;
				case 0x04: // 路段
					int road = bb.getInt();
					location.setOverspeedAlarmAdded("1=" + road);
					break;
				default:
					location.setOverspeedAlarmAdded("0");
					break;
				}
				break;
			case 14: // 进出区域/路线报警附加信息
				bb.getInt(); // 字段长度
				byte[] area = new byte[6];
				bb.get(area, 0, 6);
				String areaAndLineAlarmAdded = Tools.bytesToHexStr(area);
				location.setAreaAndLineAlarmAdded(areaAndLineAlarmAdded);
				break;
			case 15:// 路段行驶时间不足/过长报警附加信息
				bb.getInt(); // 字段长度
				byte[] roadTime = new byte[7];
				bb.get(roadTime, 0, 7);
				String roadTimeAlarmAdded = Tools.bytesToHexStr(roadTime);
				location.setRoadTimeAlarmAdded(roadTimeAlarmAdded);
				break;
			case 16:// 发动机转速
				bb.getInt(); // 字段长度
				Short engineRpm = bb.getShort();
				location.setEngineRpm(engineRpm);
				break;
			case 17:// 瞬时油耗
				bb.getInt(); // 字段长度
				Short instantFuel = bb.getShort();
				location.setInstantFuel(instantFuel);
				break;
			case 18:// 发动机扭矩百分比
				bb.getInt(); // 字段长度
				Short engineTorque = bb.getShort();
				location.setEngineTorque(engineTorque);
				break;
			case 19:// 油门踏板位置
				bb.getInt(); // 字段长度
				Short throttlePedalPosition = bb.getShort();
				location.setThrottlePedalPosition(throttlePedalPosition);
				break;
			case 20:// 扩展车辆报警标志
				bb.getInt(); // 字段长度
				int expandAlarm = bb.getInt();
				String expandAlarmFlag = Integer.toBinaryString(expandAlarm);
				location.setExpandAlarmFlag(expandAlarmFlag);
				break;
			case 21:// 扩展车辆信号状态
				bb.getInt(); // 字段长度
				int expandStatus = bb.getInt();
				String expandStatusFlag = Integer.toBinaryString(expandStatus);
				location.setExpandStatusFlag(expandStatusFlag);
				break;
			case 22:// 累计油耗
				bb.getInt(); // 字段长度
				int cumulativeFuel = bb.getInt();
				location.setCumulativeFuel(cumulativeFuel);
				break;
			case 23:// 开门报警附加信息
				bb.getInt(); // 字段长度
				Byte doorAlarmAdded = bb.get();
				location.setDoorAlarmAdded(doorAlarmAdded.intValue());
				break;
			case 24:// 速度来源标识
				bb.getInt(); // 字段长度
				Byte speedSource = bb.get();
				location.setSpeedSource(speedSource.intValue());
				break;
			case 25:// 计量仪油耗
				bb.getInt(); // 字段长度
				int meterOil = bb.getInt();
				location.setMeterOil(meterOil);
				break;
			case 26:// IO 状态位
				bb.getInt(); // 字段长度
				Short ioStatusFlag = bb.getShort();
				location.setIoStatusFlag(ioStatusFlag.intValue());
				break;
			case 27:// 模拟量
				bb.getInt(); // 字段长度
				int analog = bb.getInt();
				location.setAnalog(analog);
				break;
			case 28:// 无线通信网络信号强度
				bb.getInt(); // 字段长度
				Byte signalStrength = bb.get();
				location.setSignalStrength(signalStrength.intValue());
				break;
			case 29:// GNSS 定位卫星数
				bb.getInt(); // 字段长度
				Byte satellites = bb.get();
				location.setSatellites(satellites.intValue());
				break;
			case 30:// 发动机运行总时长
				bb.getInt(); // 字段长度
				int engineRunTotal = bb.getInt();
				location.setEngineRunTotal(engineRunTotal);
				break;
			case 31:// 终端内置电池电压
				bb.getInt(); // 字段长度
				Short terminalBatteryVoltage = bb.getShort();
				location.setTerminalBatteryVoltage(terminalBatteryVoltage.intValue());
				break;
			case 32:// 蓄电池电压
				bb.getInt(); // 字段长度
				Short batteryVoltage = bb.getShort();
				location.setBatteryVoltage(batteryVoltage.intValue());
				break;
			case 33:// 机油温度
				bb.getInt(); // 字段长度
				Short oilTemperature = bb.getShort();
				location.setOilTemperature(oilTemperature.intValue());
				break;
			case 34:// 发动机冷却液温度
				bb.getInt(); // 字段长度
				Short coolantTemperature = bb.getShort();
				location.setCoolantTemperature(coolantTemperature.intValue());
				break;
			case 35:// 进气温度
				bb.getInt(); // 字段长度
				Short inletTemperature = bb.getShort();
				location.setInletTemperature(inletTemperature.intValue());
				break;
			case 36:// 机油压力
				bb.getInt(); // 字段长度
				Short oilPressure = bb.getShort();
				location.setOilPressure(oilPressure.intValue());
				break;
			case 37:// 大气压力
				bb.getInt(); // 字段长度
				Short atmosphericPressure = bb.getShort();
				location.setAtmosphericPressure(atmosphericPressure.intValue());
				break;
			case 38:// 公共自定义信息内容
				int customLength = bb.getInt(); // 字段长度
				byte[] customValue = new byte[customLength];
				bb.get(customValue, 0, customLength);
				String customContent = Tools.bytesToHexStr(customValue);
				location.setCustomContent(customContent);
				break;
			default:
				break;
			}
		}
		
		long lon = location.getLon();
		long lat = location.getLat();
		long maplon = -100;
		long maplat = -100;
		// 偏移
		Converter conver = new Converter();
		Point point = conver.getEncryPoint(lon / 600000.0, lat / 600000.0);
		if (point != null) {
			maplon = Math.round(point.getX() * 600000);
			maplat = Math.round(point.getY() * 600000);
		} else {
			maplon = 0;
			maplat = 0;
		}
		location.setMaplon(maplon);
		location.setMaplat(maplat);
		return location;
	}

	/**
	 * 获取位置基本信息
	 * 
	 * @param trackStr
	 * @return
	 */
	private Track getTrack(String trackStr) {
		String alarmStr = Integer.toBinaryString(Integer.parseInt(trackStr.substring(0, 8), 16)); // 报警标志
																									// 8
		String alarm = parseAlarm(alarmStr);
		String statusStr = Integer.toBinaryString(Integer.parseInt(trackStr.substring(8, 16), 16)); // 状态
																									// 8
		String status = parseStatus(statusStr);
		String latStr = trackStr.substring(16, 24); // 纬度 8
		String lonStr = trackStr.substring(24, 32); // 经度 8
		String elevationStr = trackStr.substring(32, 36); // 海拔高度 4
		String speedStr = trackStr.substring(36, 40); // 速度 4
		String directionStr = trackStr.substring(40, 44); // 方向 4
		String timeStr = trackStr.substring(44, 56); // 时间 8
		Track track = new Track();
		track.setAlarm(alarm);
		track.setStatus(status);
		long lon = Long.parseLong(lonStr, 16);
		long lat = Long.parseLong(latStr, 16);
		long maplon = -100;
		long maplat = -100;
		// 偏移
		Converter conver = new Converter();
		Point point = conver.getEncryPoint(lon / 600000.0, lat / 600000.0);
		if (point != null) {
			maplon = Math.round(point.getX() * 600000);
			maplat = Math.round(point.getY() * 600000);
		} else {
			maplon = 0;
			maplat = 0;
		}
		track.setLon(lon);
		track.setLat(lat);
		track.setMaplon(maplon);
		track.setMaplat(maplat);
		track.setElevation(Integer.parseInt(elevationStr, 16));
		track.setSpeed(Integer.parseInt(speedStr, 16));
		track.setDirection(Integer.parseInt(directionStr, 16));
		track.setBcdTime(timeStr);
		long utc;
		try {
			utc = dateFormat.parse(timeStr).getTime();
		} catch (ParseException e) {
			log.error("基础位置消息时间转换异常:" + timeStr + ", " + e.getMessage(), e);
			utc = System.currentTimeMillis();
		}
		track.setUtcTime(utc);
		return track;
	}


	/**
	 * @param statusStr
	 * @return
	 */
	private String parseStatus(String statusStr) {
		String status = ",";
		int statusLenght = 0;
		// 解析基础报警 -- 判断报警是否为空
		if (StringUtils.isNumeric(statusStr)) {
			statusLenght = statusStr.length();
			for (int j = 0; j < statusLenght; j++) {
				String state = statusStr.substring(statusLenght - j - 1, statusLenght - j);
				if (state.equals("1")) {
					// alarmBuffer.append(j).append(",");
					status += j + ",";
				}
			}
		}
		// 解析扩展报警标志位 -- 判断报警是否为空
		return status;
	}

	/*****************************************
	 * <li>描 述：解析告警</li><br>
	 * <li>时 间：2013-7-11 下午9:52:14</li><br>
	 * <li>参数： @param app 数据包</li><br>
	 * 
	 *****************************************/
	private String parseAlarm(String alarmStr) {
		String alarm = ",";
		int sz = alarmStr.length();
		for (int i = 0; i < sz; i++) {
			char c = alarmStr.charAt(i);
			if (c == '1') {
				alarm += c + ",";
			}
		}
		return alarm;
	}

	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * 
	 * @param data
	 */
	public static boolean offer(String data) {
		return queue.offer(data);
	}

	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * 
	 * @param data
	 * @return
	 */
	public void put(String data) {
		try {
			queue.put(data);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!");
		}
	}

	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出
	 * IllegalStateException。
	 * 
	 * @param data
	 * @return
	 */
	public static boolean add(String data) {
		return queue.add(data);
	}
	
	
	public static Vehicle getVehicle(String phone){
		Vehicle v = new Vehicle();
		v.setVid("123"); 
		v.setEntId(String.valueOf(System.currentTimeMillis())); 
		v.setPhoneNumber(phone);
		v.setPlate("测C12345");
		v.setPlateColor("2");
		v.setVinCode("123456");
		v.setInnerCode("654321");
		v.setTid("11111");
		v.setTerminalType("1");
		v.setOemCode("E001");
		v.setEntName("宇通客车");
		v.setTeamId("601260");
		v.setTeamName("默认车队");
		v.setStaffId("999");
		v.setStaffName("无名");
		v.setOnline(1);
		
		return v;
	}

	
//	for (int i = 0; i < count; i++) {
//	byte[] p = new byte[4];
//	bb.get(p, 0, 4);
//	String pa = Tools.bytesToHexStr(p);
//	// Integer param = bb.getInt();
//	int paramLength = bb.getInt();
//	byte[] value = new byte[paramLength];
//	bb.get(value, 0, paramLength);
//	String hex = Tools.bytesToHexStr(value);
//	map.put(pa, hex);
//}

// for (int i = 0; i < count; i++) {
// mark = pos + 4;
//
// String param = params.substring(pos, mark);
// if (param.startsWith("000")) {
// if (param.equals("0001")) { // 报警标记
// pos = mark + 8;
// String alarmStr = params.substring(mark, pos);
// String alarmFlagStr =
// Integer.toBinaryString(Integer.parseInt(alarmStr, 16));
// String alarmFlag = parseAlarm(alarmFlagStr);
// location.setAlarmFlag(alarmFlag);
// }
// if (param.equals("0002")) { // 状态标记
// pos = mark + 8;
// String statusStr = params.substring(mark, pos);
// String statusFlag =
// Integer.toBinaryString(Integer.parseInt(statusStr, 16));
// location.setStatusFlag(statusFlag);
// }
// if (param.equals("0003")) { // 经度
// pos = mark + 8;
// String lonStr = params.substring(mark, pos);
// long lon = Long.parseLong(lonStr, 16);
// location.setLon(lon);
// }
// if (param.equals("0004")) { // 纬度
// pos = mark + 8;
// String latStr = params.substring(mark, pos);
// long lat = Long.parseLong(latStr, 16);
// location.setLon(lat);
// }
// if (param.equals("0005")) { // 海拔高度
// pos = mark + 4;
// String elevationStr = params.substring(mark, pos);
// int elevation = Integer.parseInt(elevationStr, 16);
// location.setElevation(elevation);
// }
// if (param.equals("0006")) { // 速度（GPS）
// pos = mark + 4;
// String speedStr = params.substring(mark, pos);
// int speed = Integer.parseInt(speedStr, 16);
// location.setSpeed(speed);
// }
// if (param.equals("0007")) { // 方向
// pos = mark + 4;
// String directionStr = params.substring(mark, pos);
// int direction = Integer.parseInt(directionStr, 16);
// location.setDirection(direction);
// }
// if (param.equals("0008")) { // 时间
// pos = mark + 28;
// String utcTimeStr = params.substring(mark, pos);
// long utcTime = Long.parseLong(utcTimeStr, 16);
// location.setUtcTime(utcTime);
// }
// if (param.equals("0009")) { // 里程
// pos = mark + 8;
// String mileageStr = params.substring(mark, pos);
// long mileage = Long.parseLong(mileageStr, 16);
// location.setMileage(mileage);
// }
// if (param.equals("000a")) { // 油量
// pos = mark + 4;
// String oilStr = params.substring(mark, pos);
// int oil = Integer.parseInt(oilStr, 16);
// location.setOil(oil);
// }
// if (param.equals("000b")) { // 行驶记录仪速度
// pos = mark + 4;
// String vssSpeedStr = params.substring(mark, pos);
// int vssSpeed = Integer.parseInt(vssSpeedStr, 16);
// location.setVssSpeed(vssSpeed);
// }
// if (param.equals("000c")) { // 人工确认报警ID
// pos = mark + 4;
// String acknowledgeAlarmIdStr = params.substring(mark, pos);
// int acknowledgeAlarmId = Integer.parseInt(acknowledgeAlarmIdStr, 16);
// location.setAcknowledgeAlarmId(acknowledgeAlarmId);
// }
// if (param.equals("000d")) { // 超速报警附加信息
// pos = mark + 2;
// String overspeedAlarmAddedStr = params.substring(mark, pos);
// // String overspeedAlarmAdded =
// // String.valueOf(Integer.parseInt(overspeedAlarmAddedStr,
// // 16));
// location.setOverspeedAlarmAdded(overspeedAlarmAddedStr);
// }
// if (param.equals("000e")) { // 进出区域/路线报警附加信息
// pos = mark + 12;
// String areaAndLineAlarmAddedStr = params.substring(mark, pos);
// // String areaAndLineAlarmAdded =
// // String.valueOf(Integer.parseInt(overspeedAlarmAddedStr,
// // 16));
// location.setAreaAndLineAlarmAdded(areaAndLineAlarmAddedStr);
// }
// if (param.equals("000f")) { // 路段行驶时间不足/过长报警附加信息
// pos = mark + 14;
// String roadTimeAlarmAddedStr = params.substring(mark, pos);
// // String areaAndLineAlarmAdded =
// // String.valueOf(Integer.parseInt(overspeedAlarmAddedStr,
// // 16));
// location.setRoadTimeAlarmAdded(roadTimeAlarmAddedStr);
// }
// } else if (param.startsWith("001")) {
// if (param.equals("0010")) { // 发动机转速
// pos = mark + 4;
// String engineRpmStr = params.substring(mark, pos);
// int engineRpm = Integer.parseInt(engineRpmStr, 16);
// location.setEngineRpm(engineRpm);
// }
// if (param.equals("0011")) { // 瞬时油耗
// pos = mark + 4;
// String instantFuelStr = params.substring(mark, pos);
// int instantFuel = Integer.parseInt(instantFuelStr, 16);
// location.setInstantFuel(instantFuel);
// }
// if (param.equals("0012")) { // 发动机扭矩百分比
// pos = mark + 4;
// String engineTorqueStr = params.substring(mark, pos);
// int engineTorque = Integer.parseInt(engineTorqueStr, 16);
// location.setEngineTorque(engineTorque);
// }
// if (param.equals("0013")) { // 油门踏板位置
// pos = mark + 4;
// String throttlePedalPositionStr = params.substring(mark, pos);
// int throttlePedalPosition =
// Integer.parseInt(throttlePedalPositionStr, 16);
// location.setThrottlePedalPosition(throttlePedalPosition);
// }
// if (param.equals("0014")) { // 扩展车辆报警标志
// pos = mark + 8;
// String expandAlarmFlagStr = params.substring(mark, pos);
// String expandAlarmFlag =
// Integer.toBinaryString(Integer.parseInt(expandAlarmFlagStr, 16));
// location.setExpandAlarmFlag(expandAlarmFlag);
// }
// if (param.equals("0015")) { // 扩展车辆信号状态
// pos = mark + 8;
// String expandStatusFlagStr = params.substring(mark, pos);
// String expandStatusFlag =
// Integer.toBinaryString(Integer.parseInt(expandStatusFlagStr, 16));
// location.setExpandStatusFlag(expandStatusFlag);
// }
// if (param.equals("0016")) { // 累计油耗
// pos = mark + 8;
// String cumulativeFuelStr = params.substring(mark, pos);
// long cumulativeFuel = Long.parseLong(cumulativeFuelStr, 16);
// location.setCumulativeFuel(cumulativeFuel);
// }
// if (param.equals("0017")) { // 开门报警附加信息
// pos = mark + 2;
// String doorAlarmAddedStr = params.substring(mark, pos);
// // int doorAlarmAdded = Integer.parseInt(doorAlarmAddedStr,
// // 16);
// location.setDoorAlarmAdded(doorAlarmAddedStr);
// ;
// }
// if (param.equals("0018")) { // 速度来源标识
// pos = mark + 2;
// String speedSourceStr = params.substring(mark, pos);
// int speedSource = Integer.parseInt(speedSourceStr, 16);
// location.setSpeedSource(speedSource);
// }
// if (param.equals("0019")) { // 计量仪油耗
// pos = mark + 8;
// String meterOilStr = params.substring(mark, pos);
// long meterOil = Long.parseLong(meterOilStr, 16);
// location.setMeterOil(meterOil);
// }
// if (param.equals("001a")) { // IO 状态位
// pos = mark + 4;
// String ioStatusFlagStr = params.substring(mark, pos);
// // int ioStatusFlag = Integer.parseInt(ioStatusFlagStr, 16);
// location.setIoStatusFlag(ioStatusFlagStr);
// }
// if (param.equals("001b")) { // 模拟量
// pos = mark + 8;
// String analogStr = params.substring(mark, pos);
// long analog = Long.parseLong(analogStr, 16);
// location.setAnalog(analog);
// }
// if (param.equals("001c")) { // 无线通信网络信号强度
// pos = mark + 2;
// String signalStrengthStr = params.substring(mark, pos);
// int signalStrength = Integer.parseInt(signalStrengthStr, 16);
// location.setSignalStrength(signalStrength);
// }
// if (param.equals("001d")) { // GNSS 定位卫星数
// pos = mark + 2;
// String satellitesStr = params.substring(mark, pos);
// int satellites = Integer.parseInt(satellitesStr, 16);
// location.setSatellites(satellites);
// }
// if (param.equals("001e")) { // 发动机运行总时长
// pos = mark + 8;
// String engineRunTotalStr = params.substring(mark, pos);
// long engineRunTotal = Long.parseLong(engineRunTotalStr, 16);
// location.setEngineRunTotal(engineRunTotal);
// }
// if (param.equals("001f")) { // 终端内置电池电压
// pos = mark + 4;
// String terminalBatteryVoltageStr = params.substring(mark, pos);
// int terminalBatteryVoltage =
// Integer.parseInt(terminalBatteryVoltageStr, 16);
// location.setTerminalBatteryVoltage(terminalBatteryVoltage);
// }
// } else if (param.startsWith("002")) {
// if (param.equals("0020")) { // 蓄电池电压
// pos = mark + 4;
// String batteryVoltageStr = params.substring(mark, pos);
// int batteryVoltage = Integer.parseInt(batteryVoltageStr, 16);
// location.setBatteryVoltage(batteryVoltage);
// }
// if (param.equals("0021")) { // 机油温度
// pos = mark + 4;
// String oilTemperatureStr = params.substring(mark, pos);
// int oilTemperature = Integer.parseInt(oilTemperatureStr, 16);
// location.setOilTemperature(oilTemperature);
// }
// if (param.equals("0022")) { // 发动机冷却液温度
// pos = mark + 4;
// String coolantTemperatureStr = params.substring(mark, pos);
// int coolantTemperature = Integer.parseInt(coolantTemperatureStr, 16);
// location.setCoolantTemperature(coolantTemperature);
// }
// if (param.equals("0023")) { // 进气温度
// pos = mark + 4;
// String inletTemperatureStr = params.substring(mark, pos);
// int inletTemperature = Integer.parseInt(inletTemperatureStr, 16);
// location.setInletTemperature(inletTemperature);
// }
// if (param.equals("0024")) { // 机油压力
// pos = mark + 4;
// String oilPressureStr = params.substring(mark, pos);
// int oilPressure = Integer.parseInt(oilPressureStr, 16);
// location.setOilPressure(oilPressure);
// }
// if (param.equals("0025")) { // 大气压力
// pos = mark + 4;
// String atmosphericPressureStr = params.substring(mark, pos);
// int atmosphericPressure = Integer.parseInt(atmosphericPressureStr,
// 16);
// location.setAtmosphericPressure(atmosphericPressure);
// }
// if (param.equals("0026")) { // 公共自定义信息内容
// // pos = mark + 4;
// String customContentStr = params.substring(mark);
// // int inletTemperature =
// // Integer.parseInt(inletTemperatureStr, 16);
// location.setCustomContent(customContentStr);
// }
// }
// }
}
