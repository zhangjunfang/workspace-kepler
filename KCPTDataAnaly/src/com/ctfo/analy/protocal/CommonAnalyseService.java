package com.ctfo.analy.protocal;

import java.util.HashMap;
import java.util.Map;

import org.apache.log4j.Logger;

import com.ctfo.analy.Constant;
import com.ctfo.analy.TempMemory;
import com.ctfo.analy.beans.AlarmBaseBean;
import com.ctfo.analy.beans.MessageBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.util.CDate;
import com.ctfo.analy.util.MathUtils;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;

 

/**
 * 通用解析类
 * 
 * @author yangyi
 * 
 */
public class CommonAnalyseService implements IAnalyseService {
	private static final Logger logger = Logger.getLogger(CommonAnalyseService.class);

	/**
	 * 监控处理报文
	 * 
	 */
	public VehicleMessageBean dealPacket(MessageBean messagecommand) {
  
		String[] tempKV = null;
		VehicleMessageBean vehicleMessage=null;
		try {
			String command = messagecommand.getCommand();//指令
			String msgid = messagecommand.getMsgid();//消息服务器ID

			String[] message = command.split("\\s+");

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
			if (!Constant.COMMAND_MTYPE_POSITION.equals(mtype)) {
				return null;
			}
 
			String macid = message[2];// 通讯码	
			String [] macidarray = macid.split("_");
			String oemcode;
			String commaddr;
			if(macidarray.length==2){
				oemcode=macidarray[0];
				commaddr=macidarray[1];
			}else{
				return null;
			}
			
			String content = message[5];// 指令参数
			String usecontent = content.substring(1, content.length() - 1);//指令内容
			String[] parm  = usecontent.split(",");

			Map<String, String> stateKV = new HashMap<String, String>();// 状态键值
			for (int i = 0; i < parm.length; i++) {
				tempKV = parm[i].split(":", 2);
				if (tempKV.length == 2) {
					stateKV.put(tempKV[0], tempKV[1]);
				}
			}
			
			String msgType = stateKV.get("TYPE");

				if ("0".equals(msgType)
						|| "1".equals(msgType)) {// 位置
					long lon = 0L;
					long lat = 0L;
					int speed = 0;
					int dir = 0;
					if (null!=stateKV.get("1")&&MathUtils.isNumeric(stateKV.get("1"))){
						lon=Long.parseLong(stateKV.get("1"));//经度
					}
					if (null!=stateKV.get("2")&&MathUtils.isNumeric(stateKV.get("2"))){
						lat=Long.parseLong(stateKV.get("2"));//纬度
					}
					if (null!=stateKV.get("3")&&MathUtils.isNumeric(stateKV.get("3"))){
						speed=Integer.parseInt(stateKV.get("3"));//速度
					}
					String dateString=stateKV.get("4");
					long utc=CDate.stringConvertUtc(dateString);//时间
					if (null!=stateKV.get("5")&&MathUtils.isNumeric(stateKV.get("5"))){
						dir=Integer.parseInt(stateKV.get("5"));//角度
					}
					String baseStatus=stateKV.get("8");//基本状态位
					
					String baseAlarmStatus = stateKV.get("20");//基本报警位
					
					String extendAlarmStatus = stateKV.get("21");//扩展报警位
					
					long mileage=-1;
					if(null != stateKV.get("9")&&MathUtils.isNumeric(stateKV.get("9")))
						mileage = Long.parseLong(stateKV.get("9"));//里程0.1
					
					long rpm=-1;
					if(null != stateKV.get("210")&&MathUtils.isNumeric(stateKV.get("210"))){
						rpm=Long.parseLong(stateKV.get("210"));//发动机转速0.125
					}
					long oil =-1;
					if(null != stateKV.get("213")&&MathUtils.isNumeric(stateKV.get("213")))
						oil=Long.parseLong(stateKV.get("213"));//累计油耗0.5
					
					long metOil=0;
					if(null != stateKV.get("219")&&MathUtils.isNumeric(stateKV.get("219")) )
						metOil = Long.parseLong(stateKV.get("219"));//精准油耗 0.01
					
					
					String speedSource = stateKV.get("218");//速度来源标识 0：来自VSS 1：来自GPS 	
					if(!"".equals(speedSource)&&null!=speedSource){
						if("0".equals(speedSource)){
							if(!"".equals(stateKV.get("7"))&&null!=stateKV.get("7")){
								speed = Integer.parseInt(stateKV.get("7"));;//行驶记录仪速度(km/h)				
							}
						}
					}
					
					//int isPValid = isPValid(lon, lat, utc, speed,dir);//合法性判断
//					if(isPValid==0){
			
							//查询数据库报警设置
							AlarmBaseBean alarmVehicleBean=TempMemory.getAlarmVehicleMap(commaddr);
							if(alarmVehicleBean!=null){
//								if(alarmVehicleBean.getVid()!=63){
//									return null;
//								}
								vehicleMessage=new VehicleMessageBean();
								vehicleMessage.setVid(alarmVehicleBean.getVid());
								vehicleMessage.setAlarmtype(alarmVehicleBean.getAlarmtype());
								vehicleMessage.setVehicleno(alarmVehicleBean.getVehicleno());
								vehicleMessage.setMsgid(msgid);
								vehicleMessage.setOemcode(oemcode);
								vehicleMessage.setCommanddr(commaddr);
								vehicleMessage.setMsgType(msgType);

								vehicleMessage.setLon(lon);
								vehicleMessage.setLat(lat);
								vehicleMessage.setSpeed(speed);
								vehicleMessage.setDateString(dateString);
								vehicleMessage.setUtc(utc);
								vehicleMessage.setDir(dir);
								vehicleMessage.setBaseStatus(baseStatus);
								vehicleMessage.setBaseAlarmStatus(baseAlarmStatus);
								vehicleMessage.setExtendAlarmStatus(extendAlarmStatus);
								vehicleMessage.setMileage(mileage);
								vehicleMessage.setRpm(rpm);
								vehicleMessage.setOil(oil);
								vehicleMessage.setMetOil(metOil);
								// 偏移
								Converter conver = new Converter();
								Point point = conver.getEncryPoint(lon / 600000.0, lat / 600000.0);
								if (point != null) {
									long maplon = Math.round(point.getX() * 600000);
									long maplat = Math.round(point.getY() * 600000);
									vehicleMessage.setMaplon(maplon);
									vehicleMessage.setMaplat(maplat);
								}
								 
								vehicleMessage.setCommand(command);
								return vehicleMessage;
								
							}else{
								//logger.debug("不存在线路围栏报警设置[" + macid+"]");
								return null;
							}
						
//					}else{
//						logger.debug("不合法车辆轨迹[" + command+"]");
//						return null;
//					}

			}else if ("5".equals(msgType)) {//上下线通知
				String tmpStr = stateKV.get("18")==null?"":stateKV.get("18");
				String onofflinestate[] = tmpStr.split("\\/");
				if (onofflinestate!=null&&onofflinestate[0]!=null){//车辆上下线
					AlarmBaseBean alarmVehicleBean=TempMemory.getAlarmVehicleMap(commaddr);
					
					if(alarmVehicleBean!=null){
						vehicleMessage=new VehicleMessageBean();
						vehicleMessage.setVid(alarmVehicleBean.getVid());
						vehicleMessage.setAlarmtype(alarmVehicleBean.getAlarmtype());
						vehicleMessage.setVehicleno(alarmVehicleBean.getVehicleno());
						vehicleMessage.setMsgid(msgid);
						vehicleMessage.setOemcode(oemcode);
						vehicleMessage.setCommanddr(commaddr);
						vehicleMessage.setMsgType(msgType);
						vehicleMessage.setOnlineState(onofflinestate[0]);
						vehicleMessage.setCommand(command);
						
						return vehicleMessage;
						
					}else{
						return null;
					}
				}else{
					return null;
				}
			}else{
				return null;
			}
				
			//return vehicleMessage;
		} catch (Exception e) {
			logger.error("协议解析错误：" ,e);
			return null;
		}

	}
	
	/**
	 * 判断轨迹的合法性
	 */
	public static short isPValid(long lon, long lat, long utc, int speed, int head) {

		// 1经度错误 2纬度错误 3定位时间错误 4车辆速度错误 5行驶方向错误 6车辆状态错误
		if (lon < 43200000 || lon > 81600000) {// 经度范围72-136(43200000-81600000)
			return 1;
		} else if (lat < 10800000 || lat > 32400000) {// 纬度范围18-54(10800000-32400000)
			return 2;
		} else if (Math.abs(utc / 1000 - System.currentTimeMillis() / 1000) > 86400) {// 定位时间与当前服务器系统时间差不超过24小时
			return 3;
		} else if (speed < 0 || speed > 1600) {// 车辆速度0~1600(单位：0.1km/h)
			return 4;
		} else if (head < 0 || head > 360) {// 行驶方向0~360
			return 5;
		} else {// 合法
			return 0;
		}
	}

	public static void main(String arg[]) {

	}
}
