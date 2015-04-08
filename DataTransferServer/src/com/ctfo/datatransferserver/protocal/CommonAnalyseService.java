package com.ctfo.datatransferserver.protocal;

import java.util.HashMap;
import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.datatransferserver.DataPool;
import com.ctfo.datatransferserver.beans.ServiceUnitBean;
import com.ctfo.datatransferserver.beans.VehiclePolymerizeBean;
import com.ctfo.datatransferserver.dao.ServiceUnitDao;
import com.ctfo.datatransferserver.util.DateConvert;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;

/**
 * 通用解析类
 * 
 * @author yangyi
 * 
 */
public class CommonAnalyseService implements IAnalyseService {
	private static final Logger logger = LoggerFactory
			.getLogger(CommonAnalyseService.class);

	/**
	 * 监控处理报文
	 * 
	 */
	public VehiclePolymerizeBean dealPacket(String messagecommand,ServiceUnitDao serviceUnitDao) {

		String[] tempKV = null;
		VehiclePolymerizeBean vehiclePolymerizeBean = null;
		try {

			String[] message = messagecommand.split("\\s+");

			// 非业务包
			if (message.length < 6) {
				return null;
			}

			String head = message[0];// 包头
			// 不合法包
			if ((!head.equals("CAITS")) && (!head.equals("CAITR"))) {
				return null;
			}

			String mtype = message[4];// 指令字
			// 只处理位置上报数据
			if (!"U_REPT".equals(mtype)) {
				return null;
			}

			String macid = message[2];// 通讯码

			String content = message[5];// 指令参数
			String usecontent = content.substring(1, content.length() - 1);// 指令内容
			String[] parm = usecontent.split(",");

			Map<String, String> stateKV = new HashMap<String, String>();// 状态键值
			for (int i = 0; i < parm.length; i++) {
				tempKV = parm[i].split(":", 2);
				if (tempKV.length == 2) {
					stateKV.put(tempKV[0], tempKV[1]);
				}
			}

			if ("0".equals(stateKV.get("TYPE"))
					|| "1".equals(stateKV.get("TYPE"))) {// 位置
				long lon = Long.parseLong(stateKV.get("1"));// 经度
				long lat = Long.parseLong(stateKV.get("2"));// 纬度
				
				String gpsspeed=stateKV.get("3");
				String vssspeed=stateKV.get("7");
				String speedsource=stateKV.get("218");
				int speed=0;
				if(speedsource != null){ // 车速来源 0：来自VSS 1：来自GPS
					if(speedsource.equals("0")){
						if ( vssspeed!=null){
						speed=Integer.parseInt(vssspeed);
						}
					}else{
						 if(gpsspeed!=null){
						speed=Integer.parseInt(gpsspeed);
						 }
					}
				}else{ 
					if ( vssspeed!=null&&!vssspeed.equals("0")){
						speed=Integer.parseInt(vssspeed);
					}else if(gpsspeed!=null){
						speed=Integer.parseInt(gpsspeed);
					}
				}
				
				//int speed=Integer.parseInt(stateKV.get("3"));//速度
				long utc = DateConvert.stringConvertUtc(stateKV.get("4"));// 时间
				int dir=Integer.parseInt(stateKV.get("5"));//方向
				String alarmcode = stateKV.get("20");// 报警代码
				int isPValid = 0;
				if(utc > 0){
					isPValid = isPValid(lon, lat, utc,speed,dir);// 合法性判断
				}
				if (isPValid == 0) {

					// 查询数据库报警设置
					ServiceUnitBean serviceUnitBean = DataPool.getVehicleMapValue(macid);
					
					if(serviceUnitBean==null){
						serviceUnitBean=serviceUnitDao.queryVehicleByMacid(macid);
					}
					if (serviceUnitBean != null) {
						vehiclePolymerizeBean = new VehiclePolymerizeBean();
						vehiclePolymerizeBean.setVid(serviceUnitBean.getVid());
						vehiclePolymerizeBean.setVehicleno(serviceUnitBean.getVehicleno());
						vehiclePolymerizeBean.setTranstypecode(serviceUnitBean.getTranstypecode());
						vehiclePolymerizeBean.setNativetareacode(serviceUnitBean.getAreacode());
						vehiclePolymerizeBean.setPlatecolorid(serviceUnitBean.getPlatecolorid());
						vehiclePolymerizeBean.setEntid(serviceUnitBean.getEntid());

						long maplon;
						long maplat;
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
						
						vehiclePolymerizeBean.setLon(maplon);
						vehiclePolymerizeBean.setLat(maplat);
						vehiclePolymerizeBean.setUtc(utc);
						vehiclePolymerizeBean.setAlarmcode(alarmcode);
						vehiclePolymerizeBean.setDir(dir);
						vehiclePolymerizeBean.setSpeed(speed);
						return vehiclePolymerizeBean;

					} else {
						logger.debug("数据库中不存在此车辆[" + macid + "]");
						return null;
					}

				} else {
					logger.debug("不合法车辆轨迹[" + messagecommand + "]");
					return null;
				}

			} else if("5".equals(stateKV.get("TYPE"))) {

				String state = stateKV.get("18");
				String parms[] = state.split("/");
 
				if ("0".equals(parms[0]) ) {
					ServiceUnitBean serviceUnitBean=serviceUnitDao.queryVehicleByMacid(macid);
					if (serviceUnitBean != null) {
						vehiclePolymerizeBean = new VehiclePolymerizeBean();
						vehiclePolymerizeBean.setVid(serviceUnitBean.getVid());
						vehiclePolymerizeBean.setVehicleno(serviceUnitBean.getVehicleno());
						vehiclePolymerizeBean.setTranstypecode(serviceUnitBean.getTranstypecode());
						vehiclePolymerizeBean.setNativetareacode(serviceUnitBean.getAreacode());
						vehiclePolymerizeBean.setPlatecolorid(serviceUnitBean.getPlatecolorid());
						vehiclePolymerizeBean.setEntid(serviceUnitBean.getEntid());
						
						if(serviceUnitBean.getLon()==0 || serviceUnitBean.getLat()==0||serviceUnitBean.getUtc()==0){
							logger.debug("不合法车辆轨迹[" + messagecommand + "]");
							return null;
						}
						
						vehiclePolymerizeBean.setLon(serviceUnitBean.getLon());
						vehiclePolymerizeBean.setLat(serviceUnitBean.getLat());
						vehiclePolymerizeBean.setUtc(serviceUnitBean.getUtc());
						vehiclePolymerizeBean.setAlarmcode(serviceUnitBean.getAlarmcode());
						vehiclePolymerizeBean.setDir(serviceUnitBean.getDir());
						vehiclePolymerizeBean.setSpeed(serviceUnitBean.getSpeed());
						vehiclePolymerizeBean.setIsonline("0");
						return vehiclePolymerizeBean;

					} else {
						logger.debug("数据库中不存在此车辆[" + macid + "]");
						return null;
					}
				}	
				return null;
			}else{
				return null;
			}

		} catch (Exception e) {
			logger.error("协议解析错误：" + e.getMessage());
			e.printStackTrace();
			return null;
		}

	}

	/**
	 * 判断轨迹的合法性
	 */
	private short isPValid(long lon, long lat, long utc,int speed,int dir) {

		// 1经度错误 2纬度错误 3定位时间错误
		if (lon < 43200000 || lon > 81600000) {// 经度范围72-136(43200000-81600000)
			return 1;
		} else if (lat < 10800000 || lat > 32400000) {// 纬度范围18-54(10800000-32400000)
			return 2;
		} else if (Math.abs(utc / 1000 - System.currentTimeMillis() / 1000) > 86400) {// 定位时间与当前服务器系统时间差不超过24小时
			return 3;
		} else if (speed < 0 || speed > 1600) {// 车辆速度0~1600(单位：0.1km/h)
			return 4;
		}else if(dir>360){
			return 5;
		}
			else {// 合法
			return 0;
		}
	}

	public static void main(String arg[]) {

	}
}
