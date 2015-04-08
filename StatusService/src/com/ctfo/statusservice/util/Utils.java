package com.ctfo.statusservice.util;


import java.util.Map;

import org.apache.commons.lang3.StringUtils;

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
	
	/*****************************************
	 * <li>描        述：获得车速 		</li><br>
	 * <li>时        间：2013-9-27  下午7:17:46	</li><br>
	 * <li>参数： @param app
	 * <li>参数： @return			</li><br>
	 * 优先去GPS速度  速度来源(VSS:0; GPS:1)
	 *****************************************/
	public static int getVehicleSpeed(String speedSourceStr, String vssStr, String gpsStr){
		if(StringUtils.equals(speedSourceStr, "0")){
			if(StringUtils.isNumeric(vssStr)){
				return Integer.parseInt(vssStr);
			}else {
				return 0;
			}
		} else {
			if(StringUtils.isNumeric(gpsStr)){
				return Integer.parseInt(gpsStr);
			}else if(StringUtils.isNumeric(vssStr)){
				return Integer.parseInt(vssStr);
			}else {
				return 0;
			}
		}
	}
	/***
	 *  获取发动机最大转速
	 * @param spdFrom
	 * @param app
	 * @return
	 */
	public static int getMaxRPM(String maxRPM){
		if(StringUtils.isNumeric(maxRPM)){
			return Integer.parseInt(maxRPM);
		}else{
			return 0;
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
		
//		uhc.setLatitude(""+lat);   
//		uhc.setLongesttalk(""+lon);
//		uhc.setElevation(""+elev); 
//		uhc.setDirection(""+direction);
//		uhc.setGps_speeding(""+speed);
//		uhc.setSpeed(""+speed);    		
//		uhc.setTerminal_time(time);		
		return bsBuf.toString();
	}
	
//	/*****
//	 * 偏移经纬度
//	 * @param lat
//	 * @param lon
//	 * @return
//	 */
//	public static Long[] convertLatLon(long lat,long lon){
//		Long[] latLon = new Long[2];
//		// 偏移
//		Converter conver = new Converter();
//		Point point = conver.getEncryPoint(lon / 600000.0,
//				lat / 600000.0);
//		if (point != null) {
//			latLon[0] = Math.round(point.getX() * 600000); // MapLon
//			latLon[1] = Math.round(point.getY() * 600000); // MapLat
//		} else {
//			latLon[0] = 0l;
//			latLon[1] = 0l;
//		}
//		return latLon;
//	}
	public static void main(String args[]){
		/*
		 7e 09 00 00 1e 01 38 03 84 98 41 00 ef 
			82 01 
			01 a9 61 33 ---27877683	
			06 d1 57 3b ---114382651
			00 69 
			02 54 
			01 4f 
			12 11 21 09 21 21 
			00 ce 00 00 00 00 ed 0c 28 7e 
		 */
		int lon = 114382651;
		int lat = 27877683;
		
		double tmpLat = (lat*6)/10;
		double tmpLon = (lon*6)/10;
		
		Point point = Utils.convertLatLon(Math.round(tmpLon),Math.round(tmpLat));
		
		System.out.println(point.getX()+"  "+Math.round(point.getX()*600000));
		System.out.println(point.getY()+"  "+Math.round(point.getY()*600000));
	}
	
	
}
