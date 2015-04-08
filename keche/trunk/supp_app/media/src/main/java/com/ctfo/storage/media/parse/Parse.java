package com.ctfo.storage.media.parse;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.media.model.MediaEvent;
import com.ctfo.storage.media.model.MediaFile;
import com.ctfo.storage.media.model.MediaInfo;
import com.ctfo.storage.media.model.Track;
import com.ctfo.storage.media.model.Vehicle;
import com.ctfo.storage.media.util.Cache;
import com.ctfo.storage.media.util.Converter;
import com.ctfo.storage.media.util.Point;
import com.ctfo.storage.media.util.Tools;

public class Parse extends Thread {
	private static Logger log = LoggerFactory.getLogger(Parse.class);

	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(50000);


	private Distribute distribute;

	private int index;
	
	
	private long lastTime = System.currentTimeMillis();

	private MediaEventStorage mediaEventStorage;
	
	private MediaFileStorage mediaFileStorage;
	
	private MediaInfoStorage mediaInfoStorage;
	
	
	private SimpleDateFormat dateFormat = new SimpleDateFormat("yyMMddHHmmss");
	
	/**
	 * @param dvrId
	 */
	public Parse() {
		setName("Parse-thread");
		distribute = new Distribute();
		distribute.start();
		
		mediaEventStorage = new MediaEventStorage();
		mediaEventStorage.start();
		
		mediaFileStorage = new MediaFileStorage();
		mediaFileStorage.start();
		
		mediaInfoStorage = new MediaInfoStorage();
		mediaInfoStorage.start();
	}

	public void run() {
		while (true) {
			try {
				String message = queue.take();// 获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
				index++;
				long current = System.currentTimeMillis();
				if ((current - lastTime) > 10000) {
					log.info("-----------------10秒处理[{}]条", index);
					index = 0;
					lastTime = System.currentTimeMillis();
				}
				process(message);
			} catch (Exception e) {
				log.error("处理队列数据异常:" + e.getMessage(), e);
			}
		}
	}

	// 开始2 消息来源8 消息目的地8 类型4 流水号 8 长度 8 子类型4
	// 5b 57c5d80a 05397fb1 1100 00000001 00000032 1101
	// 6265696A696E67000000000000000000
	// 3034323061323036616538623737623630663331346133336233386338373561 ED 5d
	private void process(String command) {
		String subType = command.substring(38, 42);  // 消息子类型  4
		if (subType.equals("1207")) {
			processMediaEvent(command);
		} else if (subType.equals("1208")) { 
			processMediaData(command);
		}
	}

	/**
	 * 处理多媒体数据
	 * @param command
	 */
	private void processMediaData(String command) {
//		String from = command.substring(2, 10); 	// 	消息来源       8
//		String to = command.substring(10, 18);		//	消息目的地   8
//		String patype = command.substring(18, 22);	//	消息大类       4
//		String length = command.substring(30, 38);	//	长度               8
//		int dataLength = Integer.parseInt(length, 16);//
		String serial = command.substring(22, 30);	//	序列号           8
//		String subType = command.substring(38, 42);  	// 消息子类型  4
		String mediaId = command.substring(66, 74);		// 	多媒体编号 	8
		String mediaType = command.substring(74, 76);	// 	多媒体类型 	2
		String mediaFormat = command.substring(76, 78);	//  多媒体格式	2
		String eventType = command.substring(78, 80);	// 	事件类型 	2
		String channelId = command.substring(80, 82);	//  通道编号	2

//		String p = Tools.getChinese(hexStr)("313336343338343134353900");
		String phone = command.substring(42, 66);		// 手机号          24
		String trackStr = command.substring(82, 138);		//	位置信息汇报 56
//		String trackStr = Tools.getChinese(track);
		Track track = getTrack(trackStr);
		String phoneNumber = Tools.getChinese(phone);
		Vehicle vehicle = Cache.getVehicle(phoneNumber); // 车辆信息缓存
		vehicle = new Vehicle();
		vehicle.setPlate("testaa");
		vehicle.setPlateId("2");
		vehicle.setVehicleType("1");
		if(vehicle != null){
//			track = command.substring(62, 90);				//	位置信息汇报
			log.debug("-------位置:" + track.toString());
			String data = command.substring(138, command.length() - 4); // 多媒体数据
			MediaFile mediaFile = new MediaFile();
//			日期时间-手机号-多媒体ID-事件类型编码-通道ID-多媒体类型-多媒体格式.jpeg 
//			例如20130816114845- 14527155210-36-1-3-0-0.jpeg
			StringBuffer mediaName = new StringBuffer();
			mediaName.append(track.getBcdTime().trim()).append("-"); 
			mediaName.append(phoneNumber.trim()).append("-");
			mediaName.append(eventType.trim()).append("-");
			mediaName.append(channelId.trim()).append("-");
			mediaName.append(mediaType.trim()).append("-");
			mediaName.append(mediaFormat.trim()).append(".jpeg");
			mediaFile.setName(mediaName.toString());
			byte[] content = Tools.hexStrToBytes(data);
			mediaFile.setContent(content);
			mediaFileStorage.put(mediaFile);
			
			MediaInfo mediaInfo = new MediaInfo();
			mediaInfo.setChannelId(Integer.parseInt(channelId, 16));
			mediaInfo.setAlarmCode(track.getAlarm());
//			mediaInfo.setDevType("");
			mediaInfo.setDirection(track.getDirection());
			mediaInfo.setElevation(track.getElevation());
			mediaInfo.setEnableFlag(1);
//			mediaInfo.setEventStatus(eventStatus);
			mediaInfo.setEventTriggerTime(track.getUtcTime());
			mediaInfo.setEventType(Integer.parseInt(eventType, 16));
			mediaInfo.setEventUpTime(track.getUtcTime());
			mediaInfo.setGpsSpeed(track.getSpeed());
//			mediaInfo.setImageUnit(imageUnit);
//			mediaInfo.setIsOverload(isOverload);
			mediaInfo.setLat(track.getLat());
			mediaInfo.setLon(track.getLon());
			mediaInfo.setMaplon(track.getMaplon());
			mediaInfo.setMaplat(track.getMaplat());
			mediaInfo.setMediaDataId(Long.parseLong(mediaId, 16));			
			mediaInfo.setMediaFormat(Integer.parseInt(mediaFormat, 16));
			mediaInfo.setMediaId(UUID.randomUUID().toString());
			mediaInfo.setMediaSize(content.length);
			mediaInfo.setMediaType(Integer.parseInt(mediaType, 16));
			mediaInfo.setMediaUrl(mediaName.toString());
			mediaInfo.setMemo("");
//			mediaInfo.setOverloadById(overloadById);
//			mediaInfo.setOverloadNum(overloadNum);
//			mediaInfo.setOverloadTime(overloadTime);
			mediaInfo.setPhoneNumber(phoneNumber);
			mediaInfo.setPlate(vehicle.getPlate());
			mediaInfo.setPlateId(vehicle.getPlateId());
//			mediaInfo.setReadFlag(readFlag);
//			mediaInfo.setSampleRate(sampleRate);
//			mediaInfo.setSendUserId(sendUserId);
			mediaInfo.setSeq(String.valueOf(Integer.parseInt(serial, 16)));
			mediaInfo.setStatusCode(track.getStatus());
			mediaInfo.setSysTime(System.currentTimeMillis());
			mediaInfo.setVehicleType(vehicle.getVehicleType());
			mediaInfoStorage.put(mediaInfo); 
		}
	}

	/**
	 * 获取位置基本信息
	 * @param trackStr
	 * @return
	 */
	private Track getTrack(String trackStr) {
		String alarmStr = Integer.toBinaryString(Integer.parseInt(trackStr.substring(0, 8), 16)); 		// 	报警标志       	8
		String alarm = parseAlarm(alarmStr);
		String statusStr = Integer.toBinaryString(Integer.parseInt(trackStr.substring(8, 16), 16));		//	状态   		8
		String status = parseStatus(statusStr);
		String latStr = trackStr.substring(16, 24);			//	纬度       	8
		String lonStr = trackStr.substring(24, 32);			//	经度               	8
		String elevationStr = trackStr.substring(32, 36); 	// 	海拔高度      	4
		String speedStr = trackStr.substring(36, 40);		//	速度       	4
		String directionStr = trackStr.substring(40, 44);	//	方向   		4
		String timeStr = trackStr.substring(44, 56);		//	时间               	8
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
//		 解析基础报警 -- 判断报警是否为空
		if (StringUtils.isNumeric(statusStr)) {
			statusLenght = statusStr.length();
			for (int j = 0; j < statusLenght; j++) {
				String state = statusStr.substring(statusLenght - j - 1, statusLenght - j);
				if (state.equals("1")) { 
//					alarmBuffer.append(j).append(",");
					status += j + ",";
				}
			}
		}
		// 解析扩展报警标志位 -- 判断报警是否为空
		return status;
	}

	/**
	 * 处理多媒体时间
	 * @return
	 */
	private void processMediaEvent(String command) {
		MediaEvent mediaEvent = new MediaEvent();
//		String from = command.substring(2, 10); 	// 	消息来源       8
//		String to = command.substring(10, 18);		//	消息目的地   8
//		String patype = command.substring(18, 22);	//	消息大类       4
//		String length = command.substring(30, 38);	//	长度               8
//		int dataLength = Integer.parseInt(length, 16);//
		String serial = command.substring(22, 30);	//	序列号           8
//		String subType = command.substring(38, 42);  	// 消息子类型  4
		String phone = command.substring(42, 66);
		String phoneNumber = Tools.getChinese(phone);
		Vehicle vehicle = Cache.getVehicle(phoneNumber); // 车辆信息缓存
		vehicle = new Vehicle();
		vehicle.setPlate("testaa");
		vehicle.setPlateId("2");
		vehicle.setVehicleType("1");
		if(vehicle != null){
			String mediaId = command.substring(66, 74);		// 	多媒体编号 	8
			String mediaType = command.substring(74, 76);	// 	多媒体类型 	2
			String mediaFormat = command.substring(76, 78);	//  多媒体格式	2
			String eventType = command.substring(78, 80);	// 	事件类型 	2
			String channelId = command.substring(80, 82);	//  通道编号	2
			
			mediaEvent.setMediaId(mediaId); 
			mediaEvent.setMediaType(Integer.parseInt(mediaType, 16));//多媒体类型  0:图像，1：音频，2：视频
			mediaEvent.setMediaFormat(Integer.parseInt(mediaFormat, 16)); 
			mediaEvent.setEventType(Integer.parseInt(eventType, 16));
			mediaEvent.setChannelId(Integer.parseInt(channelId, 16));
			mediaEvent.setTakeSerial(serial); 
			mediaEvent.setEventTime(System.currentTimeMillis());
			mediaEvent.setSysTime(System.currentTimeMillis());
			mediaEvent.setPlate(vehicle.getPlate());
			mediaEvent.setPlateId(vehicle.getPlateId());
			mediaEventStorage.put(mediaEvent);
		}
	}
	/*****************************************
	 * <li>描        述：解析告警 		</li><br>
	 * <li>时        间：2013-7-11  下午9:52:14	</li><br>
	 * <li>参数： @param app	数据包		</li><br>
	 * 
	 *****************************************/
	private String parseAlarm(String alarmStr) {
		String alarm = ",";
		int alarmLenght = 0;
//		 解析基础报警 -- 判断报警是否为空
		if (StringUtils.isNumeric(alarmStr)) {
			alarmLenght = alarmStr.length();
			for (int j = 0; j < alarmLenght; j++) {
				String state = alarmStr.substring(alarmLenght - j - 1, alarmLenght - j);
				if (state.equals("1")) { 
//					alarmBuffer.append(j).append(",");
					alarm += j + ",";
				}
			}
		}
		// 解析扩展报警标志位 -- 判断报警是否为空
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
	
}
