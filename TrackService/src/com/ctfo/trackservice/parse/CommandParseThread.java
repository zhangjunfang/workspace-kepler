/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.parse CommandParse.java	</li><br>
 * <li>时        间：2013-9-9  下午1:50:07	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.trackservice.parse;

import java.util.Map;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.model.ServiceUnit;
import com.ctfo.trackservice.util.Cache;
import com.ctfo.trackservice.util.Constant;
import com.ctfo.trackservice.util.DateTools;




/*****************************************
 * <li>描        述：指令解析线程		
 * 
 *****************************************/
public class CommandParseThread extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(CommandParseThread.class);
	/** 指令队列  */
	private ArrayBlockingQueue<String> commandQueue = new ArrayBlockingQueue<String>(100000);
	/** 计数器	  */
	private int index;
	/** 计数器	  */
	private int validIndex;
	/** 上次时间	  */
	private long lastTime = System.currentTimeMillis();
	/** 指令分发线程	  */
	private AllocationInstructionThread allocationInstructionThread;
	
	/**
	 * 初始化指令解析线程
	 */
	public CommandParseThread(){
		try {
			setName("CommandParseThread");
			allocationInstructionThread = new AllocationInstructionThread();
			allocationInstructionThread.start();
		} catch (Exception e) {
			logger.error("解析线程启动失败:" + e.getMessage(), e); 
		}
	}
	
	/*****************************************
	 * <li>描        述：添加指令 		</li><br>
	 * <li>时        间：2013-9-9  下午5:29:33	</li><br>
	 * <li>参数： @param command			</li><br>
	 * 
	 *****************************************/
	public void addCommand(String command) {
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
					allocationInstructionThread.addData(map);
					validIndex++;
				}
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime) > 10000){
					logger.info("--parse--数据解析10s处理:[" + index +"]条,有效数据:[" + validIndex + "]" + ",缓存区:["+ getQueueSize() +"]");
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
			message = StringUtils.split(command, Constant.SPACES);
			if (message.length < 6) {// 非业务包
				return null;
			}
			// 解析包头
			String head = message[0];// 包头
			// 判断是否不合法包
			if ( !head.equals(Constant.CAITS)) {
				return null;
			}
			String seq = message[1];// 业务序列号
			String macid = message[2];// 通讯码
			String channel = message[3];// 通道
			String mtype = message[4];// 指令字
			String content = message[5];// 指令参数
			String usecontent = content.substring(1, content.length() - 1);
			parm = StringUtils.split(usecontent, Constant.COMMA);
			
			Map<String, String> stateKV = new ConcurrentHashMap<String, String>();// 状态键值
			for (int i = 0; i < parm.length; i++) {
				tempKV = StringUtils.split(parm[i], Constant.COLON, 2);
				if (tempKV.length == 2){
					stateKV.put(tempKV[0], tempKV[1]);
				}
			}
//			只处理位置汇报
			if (!Constant.U_REPT.equals(mtype) ){
				return null;
			}
			stateKV.put(Constant.COMMAND, command);
			stateKV.put(Constant.HEAD, head);
			stateKV.put(Constant.SEQ, seq);
			stateKV.put(Constant.MACID, macid);
			stateKV.put(Constant.CHANNEL, channel);
			stateKV.put(Constant.MTYPE, mtype);
			stateKV.put(Constant.CONTENT, usecontent);
//			stateKV.put(Constant.MSGID, msgid);
			stateKV.put(Constant.UUID, UUID.randomUUID().toString());
			
//			判断是否轨迹位置信息
			String type = stateKV.get(Constant.TYPE);
			if(Constant.N0.equals(type) || Constant.N1.equals(type) || Constant.N11.equals(type) || Constant.N5.equals(type) ) {
				ServiceUnit serviceUnit = Cache.getVehicleMapValue(macid);
//				只处理合法车辆的轨迹位置信息
				if (serviceUnit != null) {
					stateKV.put(Constant.VID, serviceUnit.getVid());
					stateKV.put(Constant.VEHICLENO, serviceUnit.getVehicleno());
					stateKV.put(Constant.PLATECOLORID,	serviceUnit.getPlatecolorid());
					stateKV.put(Constant.COMMDR, serviceUnit.getCommaddr());
					stateKV.put(Constant.TID, serviceUnit.getTid()+"");
					stateKV.put(Constant.VIN_CODE, serviceUnit.getVinCode());
					
					if("5".equals(type)){
						return stateKV;
					}
					
					String mileage = stateKV.get("9");
//					验证里程合法性
					if(!StringUtils.isNumeric(mileage)){
						stateKV.put("9", "-1");
					}
//					 处理时间
					String timeStr = StringUtils.replace(stateKV.get(Constant.N4), "/", "");
					if(!StringUtils.isNumeric(timeStr)){
						logger.debug("非法时间字符;[{}]" , command);
						return null;
					}
					long terminalTime = DateTools.stringConvertUtc(timeStr);
					long currentTime = System.currentTimeMillis() + 86400000;
//					只处理6个月前及1天后的数据
					if((currentTime - 15552000000l) >  terminalTime || terminalTime > currentTime){
						logger.debug("-parse-上报时间是6个月前或者为未来时间,车牌号:{"+serviceUnit.getVehicleno()+"},指令:" + command);
						return null;
					} else {
						stateKV.put(Constant.UTC, String.valueOf(terminalTime));
					}
					
					return stateKV;
				} else {
					logger.debug("-parse-不存在车辆:"+macid);
					return null;
				}
			} else {
				return null;
			}
		} catch (Exception e) {
			logger.error("--parse---协议解析错误:"+ e.getMessage(), e);
			return null;
		}
	}
}
