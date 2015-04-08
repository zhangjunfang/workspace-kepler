package com.ctfo.commandservice.handler;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.commandservice.model.AccidentDoubpointsDetail;
import com.ctfo.commandservice.model.AccidentDoubpointsMain;
import com.ctfo.commandservice.service.OracleJdbcService;
import com.ctfo.commandservice.util.Constant;
import com.ctfo.commandservice.util.Converser;
import com.ctfo.commandservice.util.DateTools;
import com.ctfo.generator.pk.GeneratorPK;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;
/**
 *	行驶记录仪数据处理线程
 */
public class TravellingRecorderParseThread extends Thread {
	/**	日志	*/
	private static final Logger logger = LoggerFactory.getLogger(TravellingRecorderParseThread.class);
	
	// 待处理数据队列
	private ArrayBlockingQueue<Map<String, String>> vPacket = new ArrayBlockingQueue<Map<String, String>>(10000);
	
//	private int batchSize;
//	/** 处理标识数量	 */
//	private int processSize;
//	/** 终端版本批量提交间隔时间  */
//	private long terminalBatchTime;

	/** 最近处理时间	 */
	private long lastTime = System.currentTimeMillis();
	/**	数据库接口	*/
	private OracleJdbcService oracleJdbcService;
	
	public TravellingRecorderParseThread(OracleJdbcService oracleJdbcService, int batchSize,int processSize) {
		setName("TravellingRecorderParseThread");
		this.oracleJdbcService = oracleJdbcService;
//		this.batchSize = batchSize; 
//		this.processSize = processSize; 
	}
	
	/**
	 * 欲处理行驶记录数据队列
	 * @param app
	 */
	public void addPacket(Map<String, String> app) {
		try {
			vPacket.put(app);
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}

	/**
	 * 业务处理方法
	 */
	public void run(){
		while (true) {
			long currentTime = System.currentTimeMillis();
			try {
				//从队列中获取欲处理数据
				Map<String,String> app = vPacket.take();
//				String uuid = app.get(Constant.UUID);
				String vid = app.get(Constant.VID);
				String vinCode = app.get(Constant.VIN_CODE);
				String vehicleNo = app.get(Constant.VEHICLENO);
				String vehicleType = app.get(Constant.VEHICLE_TYPE);
				String tid = app.get(Constant.TID);
				String content = app.get("61");
				String coSeq = app.get(Constant.SEQ);
				
				app.put("RET","1");
				
				if (content==null||"".equals(content)){
					logger.info("VID:[{}]透传内容为空，跳过本条数据。",vid);
					continue;
				}else{
					sun.misc.BASE64Decoder decoder = new sun.misc.BASE64Decoder();
					//获取行驶记录数据内容
					byte[] command = decoder.decodeBuffer(content);
//					System.out.println("协议原始内容："+Converser.bytesToHexString(command));
					//19056上报数据格式（16进制形式，2003版和2012版相同）：
					/*
					 * a)起始字头（2字节，#55H,#7AH） 
					 * b)命令字（1字节）
					 * c)数据块长度（2字节，高字节在前低字节在后）
					 * d)保留（备用）字（1字节，12版默认为#00H）
					 * e)数据块（若干字节）
					 * f)校验字节（1字节）
					 */
					//解析并获取命令字、数据块长度、数据块
					if (command.length>=5){
						String commandWord = Converser.byteToHexString(command[2]);
						if ("FB".equals(commandWord)){//终端返回失败数据时更新提取日志表对应记录状态为提取失败
							oracleJdbcService.updateTravellingRecorderLog(coSeq,"01");
							app.put("RET","0");
						}else {
							//获取数据块长度
							byte datalentmp[] = new byte[2];
							System.arraycopy(command, 3, datalentmp, 0, 2);

							int datalen = Converser.bytes2Short(datalentmp, 0);
							
							if (datalen>0 && (command.length > 6 + datalen)){
								//取出行驶记录数据块 
								byte recorder[] = new byte[datalen];
								System.arraycopy(command, 6, recorder,0, datalen);
								
								if ("00".equals(commandWord)){//12版新增的 采集标准版本号指令  数据块为2个字节，字节1为版本号 BCD码；字节2为修改单号 默认为 00H
									if (recorder.length==2){
										String version = Converser.bcdToStr(recorder, 0, 1);
										oracleJdbcService.updateTerminalStandardVersion(tid,version);
										app.put("RET","0");
									}else{
										logger.info("终端版本号上报数据格式错误！");
									}
								}else if ("07".equals(commandWord)){//12版新增的 采集标准版本号指令
									//查询获取终端标准版本号
									String standardVersion = oracleJdbcService.queryTerminalStandardVersion(tid);
									
									if (!"12".equals(standardVersion)){
										List<AccidentDoubpointsMain> mainList = parseAccidentPoint03(vid,vehicleNo,vinCode,vehicleType,datalen,recorder);
										
										if (mainList!=null&&mainList.size()>0){
											//事故疑点数据提取成功，保存解析后的数据，更行提取状态
											oracleJdbcService.saveAccidentPointsData(mainList);
											oracleJdbcService.updateTravellingRecorderLog(coSeq,"00");
											app.put("RET","0");
										}else{
											//事故疑点数据提取失败，更行提取状态
											oracleJdbcService.updateTravellingRecorderLog(coSeq,"01");
										}
									}
								}else if ("10".equals(commandWord)){//12版新增的 采集标准版本号指令
									List<AccidentDoubpointsMain> mainList = parseAccidentPoint12(vid,vehicleNo,vinCode,vehicleType,datalen,recorder);
									
									if (mainList!=null&&mainList.size()>0){
										//事故疑点数据提取成功，保存解析后的数据，更行提取状态
										oracleJdbcService.saveAccidentPointsData(mainList);
										oracleJdbcService.updateTravellingRecorderLog(coSeq,"00");
										app.put("RET","0");
									}else{
										//事故疑点数据提取失败，更行提取状态
										oracleJdbcService.updateTravellingRecorderLog(coSeq,"01");
									}
								}
								
							}

						}
					}
				}
				//更新指令表结果状态
				oracleJdbcService.updateControlCommand(app);
			} catch (Exception e) {
				logger.error("行驶记录解析线程错误:" + e.getMessage(), e);
			}finally{
				
			}
			
			try {
//				10秒输出一次线程状态
				if(currentTime - lastTime >= 10000){
					logger.info("---TravellingRecorderParseThread---行驶记录解析当前状态:行驶记录队列[{}]条", vPacket.size());
					lastTime = System.currentTimeMillis();
				}
//				暂停1秒
				Thread.sleep(1000);
			} catch (Exception e) {
				logger.error(e.getMessage(), e);
			}
		}
		
	}
	
	private List<AccidentDoubpointsMain> parseAccidentPoint03(String vid,String vehicleNo,String vinCode,String vehicleType,int datalen,byte[] recorder){
		List<AccidentDoubpointsMain> ls = new ArrayList<AccidentDoubpointsMain>();
		try{
		//按次解析事故疑点数据 每次事故疑点有206个字节
		for (int j=0;j<datalen;j+=206){
			byte onceRecorder[] = new byte[206];
			System.arraycopy(recorder, j, onceRecorder,
					0, 206);
			
			//单次记录中的位置计数器
			int loc = 0;
			
			byte[] stopTimeBt = new byte[6];
			System.arraycopy(onceRecorder, loc, stopTimeBt,
					0, 6);
			
			String stopTimeStr = Converser.bcdToStr(stopTimeBt, 0, 6);
			
			//如果日期错误则表示提取失败
			if (stopTimeStr==null||"".equals(stopTimeStr)||"00000000000".equals(stopTimeStr)){
				//终端返回失败数据时更新提取日志表对应记录状态为提取失败
				logger.info("事故疑点数据停车时间格式错误，解析失败！");
				return ls;
			}

			Date stopTime = DateTools.changeDateFormat("yyMMddHHmmss", stopTimeStr);
			long stopTimeUTC = stopTime.getTime();
			loc += 6;
			
			String pointId = GeneratorPK.instance().getPKString();
			AccidentDoubpointsMain adm = new AccidentDoubpointsMain();
			adm.setPointId(pointId);
			adm.setGatherTime((new Date()).getTime());
			adm.setVid(vid);
			adm.setVinCode(vinCode);
			adm.setVehicleNo(vehicleNo);
			adm.setVehicleType(vehicleType);
			/*adm.setDriverName(driverName);
			adm.setDriverNumber(drivingNumber);*/
			adm.setStopTime(stopTimeUTC);
			
			// 提取单次全部车速 、开关量值
			byte[] speedSwitch = new byte[200];
			System.arraycopy(onceRecorder, loc,
					speedSwitch, 0, 200);
			
//			StringBuffer sb = new StringBuffer();

			float brakingTime = 0;
			int startSpeed = 0;
			
			// 循环取出车速对应的开关量信息
			
			//开始停车时间=停车时间前20秒
			stopTimeUTC -= (20*1000);

			for (int i = 0; i < 200; i += 2) {

				stopTimeUTC += (1000 * 0.2);

				byte[] tmp0 = new byte[2];
				System.arraycopy(speedSwitch, i, tmp0, 1, 1);
				int speed = Converser.bytes2Short(tmp0, 0);
				if (i == 0) {
					startSpeed = speed;
				}
				byte[] tmp1 = new byte[1];
				System.arraycopy(speedSwitch, i + 1, tmp1,
						0, 1);
				String switch0 = Converser
						.hexTo2BCD(Converser
								.bytesToHexString(tmp1));

				char[] switchChar = switch0.toCharArray();
				
				if (switchChar[0]=='1'){
					brakingTime += 0.2;
				}
				// SEQ_ACCIDENT_MAIN_ID

				AccidentDoubpointsDetail add = new AccidentDoubpointsDetail();
				add.setAutoId(GeneratorPK.instance().getPKString());
				add.setPointId(pointId);
				add.setVehicleSpeed(speed);
				add.setD0(""+switchChar[7]);
				add.setD1(""+switchChar[6]);
				add.setD2(""+switchChar[5]);
				add.setD3(""+switchChar[4]);
				add.setD4(""+switchChar[3]);
				add.setD5(""+switchChar[2]);
				add.setD6(""+switchChar[1]);
				add.setD7(""+switchChar[0]);
				add.setPointTime(stopTimeUTC);
				
				adm.getDetailList().add(add);
			}
			adm.setStartSpeed(startSpeed);
			adm.setBrakingTime(brakingTime);
			
			ls.add(adm);
		}
		}catch(Exception ex){
			logger.debug("按照03版协议格式解析行驶记录仪数据失败！");
			return null;
		}
		return ls;
	}
	
	private List<AccidentDoubpointsMain> parseAccidentPoint12(String vid,String vehicleNo,String vinCode,String vehicleType,int datalen,byte[] recorder){
		List<AccidentDoubpointsMain> ls = new ArrayList<AccidentDoubpointsMain>();
		try{
		//按次解析事故疑点数据 每次事故疑点有234个字节
		for (int j=0;j<datalen;j+=234){
			byte onceRecorder[] = new byte[234];
			System.arraycopy(recorder, j, onceRecorder,
					0, 234);
			
			//单次记录中的位置计数器
			int loc = 0;
			
			byte[] stopTimeBt = new byte[6];
			System.arraycopy(onceRecorder, loc, stopTimeBt,
					0, 6);
			
			String stopTimeStr = Converser.bcdToStr(stopTimeBt, 0, 6);
			
			//如果日期错误则表示提取失败
			if (stopTimeStr==null||"".equals(stopTimeStr)||"00000000000".equals(stopTimeStr)){
				//终端返回失败数据时更新提取日志表对应记录状态为提取失败
				logger.info("事故疑点数据停车时间格式错误，解析失败！");
				return ls;
			}

			Date stopTime = DateTools.changeDateFormat("yyMMddHHmmss", stopTimeStr);
			long stopTimeUTC = stopTime.getTime();
			loc += 6;
			
			//机动车驾驶证号
			byte[] licenseNoBt = new byte[18];
			System.arraycopy(onceRecorder, loc, licenseNoBt,
					0, 18);
			String licenseNo = Converser.bytetoString(licenseNoBt);
			loc += 18;
			
			String pointId = GeneratorPK.instance().getPKString();
			AccidentDoubpointsMain adm = new AccidentDoubpointsMain();
			adm.setPointId(pointId);
			adm.setGatherTime((new Date()).getTime());
			adm.setVid(vid);
			adm.setVinCode(vinCode);
			adm.setVehicleNo(vehicleNo);
			adm.setVehicleType(vehicleType);
			/*adm.setDriverName(driverName);
			adm.setDriverNumber(drivingNumber);*/
			adm.setLicenseNo(licenseNo);
			adm.setStopTime(stopTimeUTC);
			
			// 提取单次全部车速 、开关量值
			byte[] speedSwitch = new byte[200];
			System.arraycopy(onceRecorder, loc,
					speedSwitch, 0, 200);
			
//			StringBuffer sb = new StringBuffer();

			float brakingTime = 0;
			int startSpeed = 0;
			
			// 循环取出车速对应的开关量信息
			
			//开始停车时间=停车时间前20秒
			//stopTimeUTC -= (20*1000);

			for (int i = 0; i < 200; i += 2) {

				stopTimeUTC -= (1000 * 0.2);

				byte[] tmp0 = new byte[2];
				System.arraycopy(speedSwitch, i, tmp0, 1, 1);
				int speed = Converser.bytes2Short(tmp0, 0);
				if (i == 0) {
					startSpeed = speed;
				}
				byte[] tmp1 = new byte[1];
				System.arraycopy(speedSwitch, i + 1, tmp1,
						0, 1);
				String switch0 = Converser
						.hexTo2BCD(Converser
								.bytesToHexString(tmp1));

				char[] switchChar = switch0.toCharArray();
				
				if (switchChar[0]=='1'){
					brakingTime += 0.2;
				}
				// SEQ_ACCIDENT_MAIN_ID

				AccidentDoubpointsDetail add = new AccidentDoubpointsDetail();
				add.setAutoId(GeneratorPK.instance().getPKString());
				add.setPointId(pointId);
				add.setVehicleSpeed(speed);
				add.setD0(""+switchChar[7]);
				add.setD1(""+switchChar[6]);
				add.setD2(""+switchChar[5]);
				add.setD3(""+switchChar[4]);
				add.setD4(""+switchChar[3]);
				add.setD5(""+switchChar[2]);
				add.setD6(""+switchChar[1]);
				add.setD7(""+switchChar[0]);
				add.setPointTime(stopTimeUTC);
				
				adm.getDetailList().add(add);
			}
			adm.setStartSpeed(startSpeed);
			adm.setBrakingTime(brakingTime);
			
			loc += 200;
			
			//最近一次有效位置信息
			byte[] lonBt = new byte[4];
			System.arraycopy(onceRecorder, loc,
					lonBt, 0, 4);
			Long lon = Long.parseLong(Converser.bytesToHexString(lonBt),16);
			
			adm.setLon(lon);
			
			loc += 4;
			
			byte[] latBt = new byte[4];
			System.arraycopy(onceRecorder, loc,
					latBt, 0, 4);
			Long lat = Long.parseLong(Converser.bytesToHexString(latBt),16);
			adm.setLat(lat);
			
			loc += 4;
			
			long[] mapPoint = convertLatLonToMap(lat,lon); //偏移经纬度
			adm.setMapLat(mapPoint[0]);
			adm.setMapLon(mapPoint[1]);
			
			byte[] elevationBt = new byte[2];
			System.arraycopy(onceRecorder, loc,
					elevationBt, 0, 2);
			Long elevation = Long.parseLong(Converser.bytesToHexString(elevationBt),16);
			adm.setElevation(elevation);
			loc += 2;
			
			ls.add(adm);
			
		}
		}catch(Exception ex){
			logger.debug("按照12版协议格式解析行驶记录仪数据失败！",ex);
			return null;
		}
		return ls;
	}
	
	/****
	 * 偏移经纬度
	 * @param lt
	 * @param ln
	 */
	private long[] convertLatLonToMap(Long lt,Long ln){
		long pointArr[] = new long[2];
		long lon = ln;
		long lat = lt;
		long maplon = -100;
		long maplat = -100;
		// 偏移
		Converter conver = new Converter();
		Point point = conver.getEncryPoint(lon / 600000.0,
				lat / 600000.0);
		if (point != null) {
			maplon = Math.round(point.getX() * 600000);
			maplat = Math.round(point.getY() * 600000);
		} else {
			maplon = 0;
			maplat = 0;
		}
		pointArr[0] = maplat;
		pointArr[1] = maplon;
		return pointArr;
	}
	
	public static void main(String[] args){
		String con = "VXoQAOoAFAYwERdTMTAwMDI0MgAAAAAAAAAAAAAAAAEAAQABAAGyAbIB3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHeAd4B3gHBAcEBwQHBAcEBAAEEEMp7AT2gaABydQ==";
		byte[] command = null;
		sun.misc.BASE64Decoder decoder = new sun.misc.BASE64Decoder();
		try {
			command = decoder.decodeBuffer(con);
		} catch (IOException e) {
 			e.printStackTrace();
		}
		System.out.println("原始内容：" + Converser.bytesToHexString(command));
		//System.out.println(CDate.getUserDate("yyyy").substring(0,2));
		
		byte recorder[] = new byte[234];
		System.arraycopy(command, 6, recorder,0, 234);
		
		TravellingRecorderParseThread t = new TravellingRecorderParseThread(null,0,0);
		t.parseAccidentPoint12("VID", "vehicleNo", "vinCode", "1", 234, recorder);
	}
	
}

