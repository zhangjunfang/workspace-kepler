/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.parse CommandParse.java	</li><br>
 * <li>时        间：2013-9-9  下午1:50:07	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.parse;

import java.util.Map;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.filesaveservice.model.ServiceUnit;
import com.ctfo.filesaveservice.util.Cache;
import com.ctfo.filesaveservice.util.Constant;
import com.ctfo.filesaveservice.util.DateTools;
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
	
	private AllocationInstructionThread allocation;
	
	public CommandParseThread() throws Exception{
		super("CommandParseThread");
		allocation = new AllocationInstructionThread(); 
		allocation.start();
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
		Map<String, String> map = null;
		while (true) {
			try{
				command = commandQueue.take();
				map = parseCommand(command);
				if(map != null){
					allocation.addData(map);
					validIndex++;
				}
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime) > 10000){
					logger.info("--parse--数据解析10s处理:[" + index +"]条,有效数据:[" + validIndex + "]");
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
	 * <li>时        间：2013-9-9  下午2:03:40	</li><br>
	 * <li>参数： @param command
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	private Map<String, String> parseCommand(String command) {
		String[] parm = null;
		String[] message = null;
		String[] tempKV = null;
		try {
			//orgdata.fatal(command);
//			message = command.split("\\s+");
			message = StringUtils.split(command, Constant.SPACES);
			if (message.length < 6) {// 非业务包
				return null;
			}
			// 解析包头
			String head = message[0];// 包头
			// 判断是否不合法包
			if ((!head.equals(Constant.CAITS))) {
				return null;
			}
			String seq = message[1];// 业务序列号
			String macid = message[2];// 通讯码
			String channel = message[3];// 通道
			String mtype = message[4];// 指令字
			String content = message[5];// 指令参数
//			只处理位置汇报中的类型
			if(!mtype.equals(Constant.U_REPT)){
				return null;
			}
			String usecontent = content.substring(1, content.length() - 1);
			parm = StringUtils.split(usecontent, Constant.COMMA);
			
			Map<String, String> stateKV = new ConcurrentHashMap<String, String>();// 状态键值
			for (int i = 0; i < parm.length; i++) {
				tempKV = StringUtils.split(parm[i], Constant.COLON, 2);
				if (tempKV.length == 2){
					stateKV.put(tempKV[0], tempKV[1]);
				}
			}
//			处理数据类型
//			TYPE:0    	轨迹包
//			TYPE:7		盲区补传
//			TYPE:9		数据透传
//			TYPE:50		发动机负荷率
//			TYPE:52		驾驶行为事件
			String type = stateKV.get(Constant.TYPE);
			if(type.equals(Constant.N0) || type.equals(Constant.N7) || type.equals(Constant.N9) || type.equals(Constant.N50) || type.equals(Constant.N52)){
				//查询车辆缓存对象
				ServiceUnit serviceUnit = Cache.getVehicleMapValue(macid);
				if (serviceUnit != null) {
					stateKV.put(Constant.VID, serviceUnit.getVid().toString());
					stateKV.put(Constant.VEHICLENO, serviceUnit.getVehicleno());
					stateKV.put(Constant.PLATECOLORID,serviceUnit.getPlatecolorid());
					stateKV.put(Constant.COMMDR, serviceUnit.getCommaddr());
					stateKV.put(Constant.TID, serviceUnit.getTid()+"");
					stateKV.put(Constant.VIN_CODE, serviceUnit.getVinCode());
				} else {
					logger.warn("--parse---不存在车辆:"+macid);
					return null;
				}
				stateKV.put(Constant.COMMAND, command);
				stateKV.put(Constant.HEAD, head);
				stateKV.put(Constant.SEQ, seq);
				stateKV.put(Constant.MACID, macid);
				stateKV.put(Constant.CHANNEL, channel);
				stateKV.put(Constant.MTYPE, mtype);
				stateKV.put(Constant.CONTENT, usecontent);
//				stateKV.put(Constant.MSGID, msgid);
				stateKV.put(Constant.UUID, UUID.randomUUID().toString());
				// 位置信息解析时间字段
				if (type.equals(Constant.N0) || type.equals(Constant.N7)) {
					long terminalTime = DateTools.stringConvertUtc(stateKV.get(Constant.N4));
					long currentTime = System.currentTimeMillis() + 86400000;
//					只处理6个月前及1天后的数据
					if((currentTime - 15552000000l) >  terminalTime || terminalTime > currentTime){
						logger.warn("-parse-上报时间是6个月前或者为未来时间,车牌号:{"+serviceUnit.getVehicleno()+"},指令:" + command);
						return null;
					}
					stateKV.put(Constant.UTC, String.valueOf(terminalTime));
//					解析经度、纬度
					long lon = Long.parseLong(stateKV.get("1"));
					long lat = Long.parseLong(stateKV.get("2"));
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
					stateKV.put(Constant.MAPLON, maplon + "");
					stateKV.put(Constant.MAPLAT, maplat + "");
					if(!StringUtils.isNumeric(stateKV.get(Constant.MAPLON))){ 
						stateKV.put(Constant.MAPLON,  "0");
					}
					if(!StringUtils.isNumeric(stateKV.get(Constant.MAPLAT))){ 
						stateKV.put(Constant.MAPLAT,  "0");
					}
//					解析报警
					parseAlarm(stateKV);
//					解析速度来源
					parseSpeed(stateKV);
				} 
				return stateKV;
			} else {
				return null;
			}
		} catch (Exception e) {
			logger.error("--parse---协议解析错误:" + command, e);
			return null;
		}
	}
	/*****************************************
	 * <li>描        述：解析速度 		</li><br>
	 * <li>时        间：2013-11-5  下午1:32:58	</li><br>
	 * <li>参数： @param stateKV			</li><br>
	 *  0:来自VSS
	 *  1:来自GPS		
	 *  <pre>2013-11-19 由于渝BP1673超时报警速度为0讨论:将不支持速度来源的车辆,上报车速直接取GPS速度
	 *****************************************/
	private void parseSpeed(Map<String, String> app) {
		String speedFrom = app.get("218");
		String speed_vss = app.get("7");
		String speed_gps = app.get("3");
		if (StringUtils.isNumeric(speedFrom)) {
			app.put(Constant.SPEEDFROM, speedFrom);
			if (speedFrom.equals("0")) {
				app.put("SPEED", speed_vss);
			} else {
				app.put("SPEED", speed_gps);
			}
		} else {
			app.put("SPEED", speed_gps);
			app.put(Constant.SPEEDFROM, "1");
		}
	}
	/*****************************************
	 * <li>描        述：解析告警 		</li><br>
	 * <li>时        间：2013-7-11  下午9:52:14	</li><br>
	 * <li>参数： @param app	数据包		</li><br>
	 * 
	 *****************************************/
	private void parseAlarm(Map<String, String> app) {
		String alarmString = app.get(Constant.N20);
		StringBuffer alarmBuffer = new StringBuffer(128);
		alarmBuffer.append(Constant.COMMA);
		String status = null;
		int alarmLenght = 0;
//		 解析基础报警 -- 判断报警是否为空
		if (StringUtils.isNumeric(alarmString)) {
			String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
			alarmLenght = alarmstatus.length();
			for (int j = 0; j < alarmLenght; j++) {
				status = alarmstatus.substring(alarmLenght - j - 1, alarmLenght - j);
				if (status.equals(Constant.N1)) { 
					alarmBuffer.append(j).append(Constant.COMMA);
				}
			}
		}
		// 解析扩展报警标志位 -- 判断报警是否为空
		alarmString = app.get(Constant.N21);
		if (StringUtils.isNumeric(alarmString)) {
			String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
			alarmLenght = alarmstatus.length();
			for (int j = 0; j < alarmstatus.length(); j++) {
				status = alarmstatus.substring(alarmLenght - j - 1, alarmLenght - j);
				if (status.equals(Constant.N1)) {
					alarmBuffer.append(j+32).append(Constant.COMMA);
				}
			}
		}
		app.put(Constant.FILEALARMCODE, alarmBuffer.toString()); 
	}
}
