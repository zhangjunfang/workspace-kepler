package com.ctfo.commandservice.util;


import java.lang.reflect.InvocationTargetException;
import java.util.Map;

import org.apache.commons.lang3.StringUtils;

import com.ctfo.commandservice.model.OilInfo;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;

public class Utils {
	public static final int RATIO_BASE = 10; 
	
	public static String checkLength(String param,int len){
		if(param.length() > len){
			return param.substring(0,len);
		}else{
			return param;
		}
	}
	
	/***
	 * 
	 * @param org
	 * @param key
	 * @return
	 */
	public static boolean checkAdditionalStatus(String org,String key){
		String[] array = org.split("\\|");
		for(String ar : array){
			if(ar.equals(key)){
				return true;
			}
		}//End for
		
		return false;
	}
	
	/***
	 * 平移经纬度
	 * @param lon
	 * @param lat
	 * @return
	 */
	public static Point convertLatLon(long lon,long lat){
		Converter conver = new Converter();
		Point point = conver.getEncryPoint(lon / 600000.0, lat / 600000.0);
		return point;
	}
	
	
	/***
	 *  根据速度来源，获取是GPS速度或VSS速度
	 * @param spdFrom
	 * @param app
	 * @return
	 */
	public static Long getSpeed(Map<String, String> app){
		try{
			if(app.get(Constant.SPEEDFROM).equals(Constant.N0)){// 0：来自VSS
				return Long.parseLong(app.get(Constant.N7));
			}else{
				return Long.parseLong(app.get(Constant.N3));
			}
		}catch(Exception e){
			return 0l;
		}
	}
	/***
	 *  获取发动机最大转速
	 * @param spdFrom
	 * @param app
	 * @return
	 */
	public static Long getMaxRPM(String maxRPM){
		if(StringUtils.isNumeric(maxRPM)){
			return Long.parseLong(maxRPM);
		}else{
			return 0l;
		}
	}
	/*****
	 * 解析防偷油基本信息
	 * @param buf
	 * @param locZspt
	 * @return
	 */
	public static String getBasicInfo(byte[] buf,int locZspt){	
		//纬度		
		byte latBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, latBytes, 0, 4);		
		int lattmp = Converser.bytes2int(latBytes);
		int lat = lattmp;
		
		double tmpLat = (lat*6)/10;
		
		locZspt += 4;
		//经度		
		byte lonBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, lonBytes, 0, 4);		
		int lontmp = Converser.bytes2int(lonBytes);		
		int lon = lontmp;
		
		double tmpLon = (lon*6)/10;
		
		locZspt += 4;
		//海拔高度		
		byte elevBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, elevBytes, 2, 2);		
		int elevtmp = Converser.bytes2int(elevBytes);		
		int elev = elevtmp;
		
		locZspt += 2;
		//速度      WORD格式为什么还要new byte[4],本来可以new byte[2],
		//因为INT 类型是4个字节，所以为了避免两个字节强转出现异常,创建4个字节
		byte speedBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, speedBytes, 2, 2);		
		int speedtmp = Converser.bytes2int(speedBytes);		
		//double speed = elevtmp/10;
		
		locZspt += 2;
		//方向				
		byte directionBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, directionBytes, 2, 2);		
		int direction = Converser.bytes2int(directionBytes);
		
		locZspt += 2;
		//时间		
		byte timeBytes[] = new byte[6];		
		System.arraycopy(buf, locZspt, timeBytes, 0, 6);		
		String time = Converser.bcdToStr(timeBytes, 0, 6);	
	   //log.info(NAME+"【"+moduleName+"】纬度-->:"+lat+" 经度-->:"+lon+" 海拔-->:"+elev+"速度-->:"+speed+" 方向-->:"+direction+" 时间-->:"+time);		
		
		//赋值对象
		StringBuffer bsBuf = new StringBuffer();
		//bsBuf.append(lat);//纬度
		bsBuf.append(Math.round(tmpLat));
		bsBuf.append(":");
		//bsBuf.append(lon);//经度
		bsBuf.append(Math.round(tmpLon));
		bsBuf.append(":");
		bsBuf.append(elev);//海拔
		bsBuf.append(":");
		bsBuf.append(direction);//方向
		bsBuf.append(":");
		bsBuf.append(speedtmp);//GPS车速
		bsBuf.append(":");
		bsBuf.append(time);//终端上报时间	
		return bsBuf.toString();
	}
	
	/**
	 * 获取油量信息
	 * @param buf
	 * @return
	 */
//	public static OilInfo getBasicInfo(byte[] buf){	
//		OilInfo oilInfo = new OilInfo();
//		int locZspt = -1;
//		// 消息透传类型 0x82
//		locZspt += 1;
////		byte[] type = new byte[1];
////		System.arraycopy(buf, locZspt, type, 0, 1); 
////		oilInfo.setType(String.valueOf(type[0]));
//
//		// 协议版本号
//		locZspt += 1;
////		byte[] version = new byte[1];
////		System.arraycopy(buf, locZspt, version, 0, 1); 
////		oilInfo.setVersion(String.valueOf(version[0])); 
//		
//		//纬度		
////		byte latBytes[] = new byte[4];		
////		System.arraycopy(buf, locZspt, latBytes, 0, 4);		
////		int lattmp = Converser.bytes2int(latBytes);
////		int lat = lattmp;
////		double tmpLat = (lat*6)/10;
////		oilInfo.setLat(Math.round(tmpLat));
//		
//		locZspt += 4;
//		//经度		
////		byte lonBytes[] = new byte[4];		
////		System.arraycopy(buf, locZspt, lonBytes, 0, 4);		
////		int lontmp = Converser.bytes2int(lonBytes);		
////		int lon = lontmp;
////		double tmpLon = (lon*6)/10;
////		oilInfo.setLon(Math.round(tmpLon));
//		
//		locZspt += 4;
//		//海拔高度		
////		byte elevBytes[] = new byte[4];		
////		System.arraycopy(buf, locZspt, elevBytes, 2, 2);		
////		int elevtmp = Converser.bytes2int(elevBytes);		
////		oilInfo.setElevation(elevtmp);
//		
//		locZspt += 2;
//		//速度      WORD格式为什么还要new byte[4],本来可以new byte[2],
//		//因为INT 类型是4个字节，所以为了避免两个字节强转出现异常,创建4个字节
////		byte speedBytes[] = new byte[4];		
////		System.arraycopy(buf, locZspt, speedBytes, 2, 2);		
////		int speedtmp = Converser.bytes2int(speedBytes);		
////		oilInfo.setSpeed(speedtmp);
//		
//		locZspt += 2;
//		//方向				
////		byte directionBytes[] = new byte[4];		
////		System.arraycopy(buf, locZspt, directionBytes, 2, 2);		
////		int direction = Converser.bytes2int(directionBytes);
////		oilInfo.setDirection(direction); 
//		
//		locZspt += 2;
//		//时间		
////		byte timeBytes[] = new byte[6];		
////		System.arraycopy(buf, locZspt, timeBytes, 0, 6);		
////		String time = Converser.bcdToStr(timeBytes, 0, 6);	
////		oilInfo.setTime(time); 
//	   //log.info(NAME+"【"+moduleName+"】纬度-->:"+lat+" 经度-->:"+lon+" 海拔-->:"+elev+"速度-->:"+speed+" 方向-->:"+direction+" 时间-->:"+time);		
//		
//		locZspt += 6;
//		//状态		
////		byte statusBytes[] = new byte[1];		
////		System.arraycopy(buf, locZspt, statusBytes, 0, 1);		
////		oilInfo.setStatus(statusBytes[0]);
//		
//		locZspt += 1;
//		//指令类别		
//		byte commandTypeBytes[] = new byte[1];		
//		System.arraycopy(buf, locZspt, commandTypeBytes, 0, 1);		
//		if(commandTypeBytes[0] == 0x01){
//			locZspt += 1;
//			//标定油量		
//			byte oilCalibrationBytes[] = new byte[4];		
//			System.arraycopy(buf, locZspt, oilCalibrationBytes, 2, 2);		
//			int oilCalibration = Converser.bytes2int(oilCalibrationBytes);		
//			oilInfo.setOilCalibration(oilCalibration); 
//			
//			locZspt += 2;
//			//AD落差	
//			byte gapBytes[] = new byte[4];		
//			System.arraycopy(buf, locZspt, gapBytes, 2, 2);		
//			int gap = Converser.bytes2int(gapBytes);		
//			oilInfo.setGap(gap);
//			
//			locZspt += 2;
//			//加油门限		
//			byte refuelThresholdBytes[] = new byte[1];		
//			System.arraycopy(buf, locZspt, refuelThresholdBytes, 0, 1);		
//			oilInfo.setRefuelThreshold(refuelThresholdBytes[0]);
//			
//			locZspt += 1;
//			//偷油门限		
//			byte stealThresholdBytes[] = new byte[1];		
//			System.arraycopy(buf, locZspt, stealThresholdBytes, 0, 1);		
//			oilInfo.setStealThreshold(stealThresholdBytes[0]);
//
//			return oilInfo;
//		} else {
//			return null;
//		}
//	}
	
	public static void main(String args[]) throws IllegalArgumentException, IllegalAccessException, InvocationTargetException{
		String base64Str = "AQAAAAAAAAAAAAAAAAAAFAcCEDgGBQA0gABkChQ=";
//		byte[] buf = Base64_URl.base64DecodeToArray(base64Str);
		OilInfo result = getOilBase(base64Str);
//		if(buf[0] == 0x01){
//			result = getBasicInfo(buf);
//		}
		System.out.println(result.toString()); 
//		int lon = 114382651;
//		int lat = 27877683;
//		
//		double tmpLat = (lat*6)/10;
//		double tmpLon = (lon*6)/10;
//		
//		Point point = Utils.convertLatLon(Math.round(tmpLon),Math.round(tmpLat));
//		
//		System.out.println(point.getX()+"  "+Math.round(point.getX()*600000));
//		System.out.println(point.getY()+"  "+Math.round(point.getY()*600000));
		System.exit(0); 
	}
	
	public static OilInfo getOilBase(String value) {
		if(value == null || value.length() == 0){
			return null;
		}
		OilInfo oilInfo = new OilInfo();
		byte[] buf = Base64_URl.base64DecodeToArray(value);
		if(buf.length < 22){
			return null;
		}
		int locZspt = -1;
		// 透传类型
		locZspt += 1;
		// 协议版本号
		locZspt += 1;
		//纬度		
		byte latBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, latBytes, 0, 4);		
		int lattmp = Converser.bytes2int(latBytes);
		double lat = (lattmp*6)/10;
		oilInfo.setLat(Math.round(lat));
		
		locZspt += 4;
		//经度		
		byte lonBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, lonBytes, 0, 4);		
		int lontmp = Converser.bytes2int(lonBytes);		
		double lon = (lontmp*6)/10;
		oilInfo.setLon(Math.round(lon));
		
		locZspt += 4;
		//海拔高度		
		byte elevBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, elevBytes, 2, 2);		
		int elevtmp = Converser.bytes2int(elevBytes);		
		oilInfo.setElevation(elevtmp);
		
		locZspt += 2;
		//速度      WORD格式为什么还要new byte[4],本来可以new byte[2],
		//因为INT 类型是4个字节，所以为了避免两个字节强转出现异常,创建4个字节
		byte speedBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, speedBytes, 2, 2);		
		int speedtmp = Converser.bytes2int(speedBytes);		
		oilInfo.setSpeed(speedtmp);
		
		locZspt += 2;
		//方向				
		byte directionBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, directionBytes, 2, 2);		
		int direction = Converser.bytes2int(directionBytes);
		oilInfo.setDirection(direction); 
		
		locZspt += 2;
		//时间		
		byte timeBytes[] = new byte[6];		
		System.arraycopy(buf, locZspt, timeBytes, 0, 6);		
		String time = Converser.bcdToStr(timeBytes, 0, 6);	
		oilInfo.setTime(time); 
		
		locZspt += 6;
		//状态(0:油位正常 ; 1:偷油量提示 ; 2:加油提示 ; 3:偷油告警 ; 4:软件版本号 ; 5:参数设置查询 ;	)	
		byte statusBytes[] = new byte[1];		
		System.arraycopy(buf, locZspt, statusBytes, 0, 1);	
		int status = statusBytes[0] & 0xff;
		
		if(status < 3){
			String stateStr = Converser.hexTo2BCD(Converser.bytesToHexString(statusBytes));
			String state = stateStr.substring(6, stateStr.length());
			oilInfo.setStatus(state);
			locZspt += 1;
			//	燃油液位
			byte fuelLevelBytes[] = new byte[4];
			System.arraycopy(buf, locZspt, fuelLevelBytes, 3, 1);
			int fuelLevel = Converser.bytes2int(fuelLevelBytes);
			oilInfo.setFuelLevel(fuelLevel);
			
			locZspt += 3;
			//	变动油量
			byte oilChangeBytes[] = new byte[4];
			System.arraycopy(buf, locZspt, oilChangeBytes, 2, 2);
			byte oilChangeNewData[] = new byte[4];
			oilChangeNewData[0] = oilChangeBytes[0];
			oilChangeNewData[1] = oilChangeBytes[1];
			oilChangeNewData[2] = oilChangeBytes[3];
			oilChangeNewData[3] = oilChangeBytes[2];
			int oilChange = Converser.bytes2int(oilChangeNewData);
			oilInfo.setOilChange(oilChange); 

			locZspt += 2;
			//	油箱余量
			byte oilAllowanceBytes[] = new byte[4];
			System.arraycopy(buf, locZspt, oilAllowanceBytes, 2, 2);
			// 定义新数组，调整后两个数组的位置
			byte oilAllowanceNewData[] = new byte[4];
			oilAllowanceNewData[0] = oilAllowanceBytes[0];
			oilAllowanceNewData[1] = oilAllowanceBytes[1];
			oilAllowanceNewData[2] = oilAllowanceBytes[3];
			oilAllowanceNewData[3] = oilAllowanceBytes[2];
			int oilAllowance = Converser.bytes2int(oilAllowanceNewData);
			oilInfo.setOilAllowance(oilAllowance);

			return oilInfo;
		} else if (status == 5) {
			oilInfo.setStatus("5"); 
			locZspt += 1;
			// 指令类别
			byte commandTypeBytes[] = new byte[1];
			System.arraycopy(buf, locZspt, commandTypeBytes, 0, 1);
			oilInfo.setCommandType(commandTypeBytes[0]); 
			
			locZspt += 1;
			// 标定油量
			byte oilCalibrationBytes[] = new byte[4];
			System.arraycopy(buf, locZspt, oilCalibrationBytes, 2, 2);
			byte oilCalibrationNewData[] = new byte[4];// 小端字节改大端方式
			oilCalibrationNewData[0] = oilCalibrationBytes[0];
			oilCalibrationNewData[1] = oilCalibrationBytes[1];
			oilCalibrationNewData[2] = oilCalibrationBytes[3];
			oilCalibrationNewData[3] = oilCalibrationBytes[2];
			int oilCalibration = Converser.bytes2int(oilCalibrationNewData);
			oilInfo.setOilCalibration(oilCalibration);

			locZspt += 2;
			// AD落差
			byte gapBytes[] = new byte[4];
			System.arraycopy(buf, locZspt, gapBytes, 2, 2);
			byte gapNewData[] = new byte[4];// 小端字节改大端方式
			gapNewData[0] = gapBytes[0];
			gapNewData[1] = gapBytes[1];
			gapNewData[2] = gapBytes[3];
			gapNewData[3] = gapBytes[2];
			int gap = Converser.bytes2int(gapNewData);
			oilInfo.setGap(gap);

			locZspt += 2;
			// 加油门限
			byte refuelThresholdBytes[] = new byte[1];
			System.arraycopy(buf, locZspt, refuelThresholdBytes, 0, 1);
			int refuelThreshold = refuelThresholdBytes[0] & 0xff;
			oilInfo.setRefuelThreshold(refuelThreshold);

			locZspt += 1;
			// 偷油门限
			byte stealThresholdBytes[] = new byte[1];
			System.arraycopy(buf, locZspt, stealThresholdBytes, 0, 1);
			int stealThreshold = stealThresholdBytes[0] & 0xff;
			oilInfo.setStealThreshold(stealThreshold);

			return oilInfo;
		} else {
			return null;
		}
	}
}
