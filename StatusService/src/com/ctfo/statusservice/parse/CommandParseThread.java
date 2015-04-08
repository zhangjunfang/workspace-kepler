/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.parse CommandParse.java	</li><br>
 * <li>时        间：2013-9-9  下午1:50:07	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.parse;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statusservice.model.Pack;
import com.ctfo.statusservice.model.ServiceUnit;
import com.ctfo.statusservice.util.Cache;
import com.ctfo.statusservice.util.Constant;
import com.ctfo.statusservice.util.DateTools;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;




/*****************************************
 * <li>描        述：指令解析线程		
 * 
 *****************************************/
public class CommandParseThread extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(CommandParseThread.class);
	/** 指令队列  */
	private static ArrayBlockingQueue<String> commandQueue = new ArrayBlockingQueue<String>(100000);
	/** 计数器	  */ 
	private int index;
	/** 计数器	  */
	private int validIndex;
	/** 上次时间	  */
	private long lastTime = System.currentTimeMillis();
	/** 经纬度转换器	  */
	private Converter conver;
	
	private int speedLimit = 1400;
	
	public CommandParseThread(int speed){
		super("CommandParseThread");
		conver = new Converter();
		if(speed != 0){
			this.speedLimit = speed;
		}
	}
	
	/*****************************************
	 * <li>描        述：添加指令 		</li><br>
	 * <li>时        间：2013-9-9  下午5:29:33	</li><br>
	 * <li>参数： @param command			</li><br>
	 * 
	 *****************************************/
	public static void addCommand(String command) {
		try {
			commandQueue.put(command);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}
	
	public int getQueueSize() {
		return commandQueue.size();
	}
	
	/*****************************************
	 * <li>描        述：数据解析 		</li><br>
	 * <li>时        间：2013-9-9  下午5:28:37	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public void run(){
		String command = null;
		Pack pack = null;
		while (true) {
			try{
				command = commandQueue.take();
				pack = parseCommand(command);
				if(pack != null){
					AllocationInstructionManage.getAllocationInstructionThread().addData(pack);
					validIndex++;
				}
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime) > 10000){
					int size = getQueueSize();
					logger.info("--parse10s--数据解析10s处理:[" + index +"]条,有效数据:[" + validIndex + "]" + ",缓存区:["+ size +"]");
					index = 0;
					validIndex = 0;
					lastTime = currentTime;
				}
				index++;
			}catch(Exception e){
				logger.error("指令解析线程异常:"+ e.getMessage(), e);
			}
		}
	}

	/*****************************************
	 * <li>描        述：解析指令 		</li><br>
	 * <pre>
	 * 	只处理的数据类型
	 *	TYPE:0    	轨迹包
	 *	TYPE:1		报警包
	 *	TYPE:5		上下线包
	 *			</pre>
	 * <li>时        间：2013-9-9  下午2:03:40	</li><br>
	 * <li>参数： @param command
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	private Pack parseCommand(String commandMessage) {
		String[] parm = null;
		String[] message = null;
		String[] tempKV = null;
		try {
			message = StringUtils.split(commandMessage, Constant.SPACES);
			if (message.length < 6) {// 非业务包
				return null;
			}
			String head = message[0];	// 包头
			String seq = message[1];	// 业务序列号
			String macid = message[2];	// 通讯码
			String channel = message[3];// 通道
			String mtype = message[4];	// 指令字
			String content = message[5];// 指令内容
//			 判断是否不合法包
			if ( !head.equals(Constant.CAITS)) {
				return null;
			}
//			只处理位置汇报
			if (!Constant.U_REPT.equals(mtype) ){
				return null;
			}
//			解析指令内容
			String usecontent = content.substring(1, content.length() - 1);
			parm = StringUtils.split(usecontent, Constant.COMMA);
			Map<String, String> contentMap = new ConcurrentHashMap<String, String>();// 状态键值
			for (int i = 0; i < parm.length; i++) {
				tempKV = StringUtils.split(parm[i], Constant.COLON, 2);
				if (tempKV.length == 2){
					contentMap.put(tempKV[0], tempKV[1]);
				}
			}
//			判断是否轨迹位置信息
			String subType = contentMap.get(Constant.TYPE);
			if(Constant.N0.equals(subType) || Constant.N1.equals(subType)) {
				ServiceUnit serviceUnit = Cache.getVehicleMapValue(macid);
//				只处理合法的轨迹位置信息
				if (serviceUnit != null) {
					Pack pack = new Pack();
					pack.setKey(head);
					pack.setSeq(seq);
					pack.setMacid(macid);
					pack.setChannel(channel);
					pack.setType(mtype);
					pack.setContent(usecontent); 
					pack.setCommand(commandMessage); 
					pack.setVid(serviceUnit.getVid());
					pack.setPlate(serviceUnit.getVehicleno());
					pack.setPlateColor(serviceUnit.getPlatecolorid());
					pack.setPhoneNumber(serviceUnit.getCommaddr()); 
					pack.setTid(serviceUnit.getTid());
					pack.setVinCode(serviceUnit.getVinCode()); 

//					处理时间
					String dateStr = contentMap.get(Constant.N4);
					long terminalTime = DateTools.stringConvertUtc(dateStr);
					long curTime = System.currentTimeMillis();
					long currentTime = curTime + 86400000;
//					只处理6个月前及1天后的数据
					if((currentTime - 15552000000l) >  terminalTime || terminalTime > currentTime){
						logger.debug("-parse-设备时间是6个月前或者为未来时间,车牌号:{"+serviceUnit.getVehicleno()+"},指令:" + commandMessage);
						return null;
					} else {
						pack.setGpsUtc(terminalTime);
					}
					String[] macidArray = StringUtils.split(macid, "_");
					if(macidArray.length != 2){
						logger.debug(serviceUnit.getVehicleno()+"上报位置信息中硬件识别码解析错误:" + macid);
						return null;
					}
					pack.setOemCode(macidArray[0]);
					
//					解析地图经度、纬度
					String lonStr = contentMap.get("1");
					String latStr = contentMap.get("2");
					long lon = Long.parseLong(lonStr);
					long lat = Long.parseLong(latStr);
					long maplon = 0;
					long maplat = 0;
					// 偏移
					Point point = conver.getEncryPoint(lon / 600000.0, lat / 600000.0);
					if (point != null) {
						maplon = Math.round(point.getX() * 600000);
						maplat = Math.round(point.getY() * 600000);
					}
					pack.setLonStr(lonStr); 
					pack.setLatStr(latStr);
					pack.setLon(lon);
					pack.setLat(lat); 
					pack.setMaplon(maplon);
					pack.setMaplat(maplat); 
					
//					处理基础报警数据
					parseAlarmInfo(contentMap, pack);
					
//					解析报警
					String baseAlarm = contentMap.get(Constant.N20);
					String extendsAlarm = contentMap.get(Constant.N21);
					parseAlarm(serviceUnit.getVid(), baseAlarm, extendsAlarm, pack);
					
//					记录速度  优先GPS速度  速度来源(VSS:0; GPS:1)
					String speedFrom = contentMap.get(Constant.N218);
					String vssSpeed = contentMap.get(Constant.N7);
					String gpsSpeed = contentMap.get(Constant.N3);
					if(StringUtils.isNumeric(speedFrom) && speedFrom.equals("0") && StringUtils.isNumeric(vssSpeed)){
						int vss = Integer.parseInt(vssSpeed);
						if(vss >= speedLimit){
							logger.debug("车辆速度超过阀值[{}]", speedLimit); 
							return null;
						}
						pack.setSpeedSource(speedFrom); //速度来源
						pack.setVssSpeedStr(vssSpeed);	// VSS传感器速度
						pack.setGpsSpeedStr(gpsSpeed);	// GPS速度
					}else{
						if(StringUtils.isNumeric(gpsSpeed)){
							int gps = Integer.parseInt(gpsSpeed);
							if(gps >= speedLimit){
								logger.debug("车辆速度超过阀值[{}]", speedLimit); 
								return null;
							}
							pack.setGpsSpeedStr(gpsSpeed);	// GPS速度
						}else{
							pack.setGpsSpeedStr("");	// GPS速度
						}
						pack.setSpeedSource(speedFrom); //速度来源
						pack.setVssSpeedStr(vssSpeed);	// VSS传感器速度
					}
					
//					记录发动机转速
					String engineSpeed = contentMap.get(Constant.N210);
					if(StringUtils.isNumeric(engineSpeed)){
						pack.setEngineSpeed(Integer.parseInt(engineSpeed));
					}else{
						pack.setEngineSpeed(-1); 
					}
					
					
					return pack;
				} else {
					logger.info("-parse-不存在车辆:"+macid);
					return null;
				}
			} else {
				logger.debug("--typeError--:"+subType);
				return null;
			}
		} catch (Exception e) {
			logger.error("--processing---协议解析错误:" + commandMessage, e);
			return null;
		}
	}
	/**
	 * 解析报警相关信息
	 * @param contentMap	参数集合
	 * @param pack			数据包对象
	 */
	private void parseAlarmInfo(Map<String, String> contentMap, Pack pack) {
		// 方向
		String direction = contentMap.get("5"); 
		if(StringUtils.isNumeric(direction)){ 
			pack.setDirection(Integer.parseInt(direction)); 
		} else {
			pack.setDirection(-1); 
		}
		// 海拔
		String elevation = contentMap.get("6");
		if(StringUtils.isNumeric(elevation)){ 
			pack.setElevation(Integer.parseInt(elevation)); 
		} else {
			pack.setElevation(-1); 
		}
		// 里程
		String mileage = contentMap.get("9");
		if(StringUtils.isNumeric(mileage)){ 
			pack.setMileage(Integer.parseInt(mileage)); 
		} else {
			pack.setMileage(-1); 
		}
		// 总油量
		String oilTotal = contentMap.get("213");
		if(StringUtils.isNumeric(oilTotal)){ 
			pack.setOilTotal(Long.parseLong(oilTotal)); 
		} else {
			pack.setOilTotal(-1); 
		}
		// 基本状态位
		String baseStatus = contentMap.get("8");
		if(StringUtils.isNumeric(baseStatus)){ 
			pack.setBaseStatus(baseStatus); 
		} else {
			pack.setBaseStatus(""); 
		}
		// 扩展状态位
		String extendedStatus = contentMap.get("500");
		if(StringUtils.isNumeric(extendedStatus)){ 
			pack.setExtendedStatus(extendedStatus); 
		} else {
			pack.setExtendedStatus(""); 
		}
//		附加报警信息
		String alarmAdd = contentMap.get("32");
		if(alarmAdd == null || alarmAdd.length() == 0){
			pack.setAlarmAdded("");
		} else {
			pack.setAlarmAdded(alarmAdd); 
		}
		
	}

	/*****************************************
	 * <li>描        述：解析告警 		</li><br>
	 * <li>时        间：2013-7-11  下午9:52:14	</li><br>
	 * <li>参数： @param app	数据包		</li><br>
	 * @param pack 
	 * 
	 *****************************************/
	private void parseAlarm(String vid, String baseAlarm, String extendsAlarm, Pack pack) {
		String alarmString = baseAlarm;
		String allAlarm = Constant.COMMA;
		String status = null;
		int alarmLenght = 0;
//		 解析基础报警 -- 判断报警是否为空
		if (StringUtils.isNumeric(alarmString)) {
			String baseAlarmStr = Constant.COMMA;
			String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
			alarmLenght = alarmstatus.length();
			for (int j = 0; j < alarmLenght; j++) {
				status = alarmstatus.substring(alarmLenght - j - 1, alarmLenght - j);
				if (status.equals(Constant.N1)) { 
					if (Cache.vidEntMap.containsKey(vid)) { // 只实时处理企业对应设置的严重告警
						if (Cache.vidEntMap.get(vid).contains(addComma(j))) {
							baseAlarmStr += (j + Constant.COMMA);
						} 
					} else { // 如果不包含则取默认报警值
						if (Cache.entAlarmMap.get(Constant.N1).contains(addComma(j))) {
							baseAlarmStr += (j + Constant.COMMA);
						}
					}
				}
			}
			allAlarm += baseAlarmStr; 
		}
		alarmString = null;
		// 解析扩展报警标志位 -- 判断报警是否为空
		alarmString = extendsAlarm;
		if (StringUtils.isNumeric(alarmString)) {
			String alarmStr  = Constant.COMMA;
			String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
			alarmLenght = alarmstatus.length();
			for (int j = 0; j < alarmLenght; j++) {
				status = alarmstatus.substring(alarmLenght - j - 1, alarmLenght - j);
				if (status.equals(Constant.N1)) {
					if (Cache.vidEntMap.containsKey(vid)) {// 只实时处理企业对应设置的严重告警
						if (Cache.vidEntMap.get(vid).contains(addComma(j+32))) {
							alarmStr += ((j+32) + Constant.COMMA);
						}
					} else {// 如果不包含则取默认报警值
						if (Cache.entAlarmMap.get(Constant.N1).contains(addComma(j+32))) {
							alarmStr = alarmStr + ((j+32) + Constant.COMMA);
						}
					}
				}
			}
			allAlarm += alarmStr;
		}
//		不处理疲劳驾驶报警
		pack.setAllAlarm(allAlarm.replace(",2,", ",")); 
	}
	/*****************************************
	 * <li>描        述：添加逗号 		</li><br>
	 * <li>  addComma("ABC") = ,ABC,
	 * <li>时        间：2013-7-10  下午1:54:41	</li><br>
	 * <li>参数： @param alarmCode
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static String addComma(int alarmCode) {
		return "," + alarmCode + ",";
	}
}
