/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.parse CommandParse.java	</li><br>
 * <li>时        间：2013-9-9  下午1:50:07	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.parse;

import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.commandservice.handler.CustomCommandProcess;
import com.ctfo.commandservice.model.Custom;
import com.ctfo.commandservice.model.ServiceUnit;
import com.ctfo.commandservice.util.Cache;
import com.ctfo.commandservice.util.ConfigLoader;
import com.ctfo.commandservice.util.Constant;
import com.ctfo.generator.pk.GeneratorPK;




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
	/** 自定义过期时间(单位:毫秒)	  */
	private long outTime = 60000;
	
	public CommandParseThread(){
		super("CommandParseThread");
		outTime = Long.parseLong(ConfigLoader.config.get("customOutTime"));
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
					AllocationInstructionManage.getAllocationInstructionThread().addData(map);
					validIndex++;
				}
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime) >= 3000){
					logger.info("--parse--数据解析3s处理:[" + index +"]条,有效数据:[" + validIndex + "]");
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
			if ( !head.equals(Constant.CAITS) && !head.equals(Constant.CAITR)) {
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
				tempKV = StringUtils.splitPreserveAllTokens(parm[i], Constant.COLON, 2);
				if (tempKV.length == 2){
					stateKV.put(tempKV[0], tempKV[1]);
				}
			}

			
			stateKV.put(Constant.COMMAND, command);
			stateKV.put(Constant.HEAD, head);
			stateKV.put(Constant.SEQ, seq);
			stateKV.put(Constant.MACID, macid);
			stateKV.put(Constant.CHANNEL, channel);
			stateKV.put(Constant.MTYPE, mtype);
			stateKV.put(Constant.CONTENT, usecontent);
			stateKV.put(Constant.UUID, GeneratorPK.instance().getPKString());
			
//			平台数据直接处理 
			if(channel.equals("4") && "L_PLAT".equals(mtype)){
				return stateKV;
			}
			
			String[] arr = null;
			if(macid != null){
				arr = StringUtils.split(macid, "_", 2);
				if(arr != null && arr.length == 2){
					stateKV.put(Constant.COMMDR, arr[1]);
					stateKV.put(Constant.OEMCODE, arr[0]); 
				}else{
					stateKV.put(Constant.COMMDR, "0");
					stateKV.put(Constant.OEMCODE, "");  
				}
			}
//			处理数据类型
			String type = stateKV.get(Constant.TYPE);
			ServiceUnit serviceUnit = Cache.getVehicleMapValue(macid);
			//--------------------------------------------------------------------	
			if (serviceUnit != null) {
				stateKV.put(Constant.VID, serviceUnit.getVid().toString());
				stateKV.put(Constant.VEHICLENO, serviceUnit.getVehicleno());
				stateKV.put(Constant.PLATECOLORID,	serviceUnit.getPlatecolorid());
				stateKV.put(Constant.COMMDR, serviceUnit.getCommaddr());
				stateKV.put(Constant.TID, serviceUnit.getTid()+"");
				stateKV.put(Constant.VIN_CODE, serviceUnit.getVinCode());
				stateKV.put(Constant.VEHICLE_TYPE, serviceUnit.getVehicleType()==null?"":serviceUnit.getVehicleType());
			} else {
				if (Constant.U_REPT.equals(mtype) && (Constant.N36.equals(type) || Constant.N37.equals(type) || Constant.N38.equals(type))) {
					for (int i = 0; i < parm.length; i++) {
						tempKV = StringUtils.split(parm[i], Constant.COLON, 2);
						if (tempKV.length == 2){
							stateKV.put(tempKV[0], tempKV[1]);
						}
					}
					
					stateKV.put(Constant.VID, "0");
					stateKV.put(Constant.VEHICLENO, "0");
					stateKV.put(Constant.PLATECOLORID, "0");
				
					stateKV.put(Constant.REARAXLERATE, "0");
					stateKV.put(Constant.TYRER, "0");
					stateKV.put(Constant.TID, "0");
				} else {
					logger.warn("-parse-不存在车辆"+macid);
					return null;
				}
			}
			
//			判断是否是自定义指令回传-----------------------------------
			String vid = stateKV.get(Constant.VID);
			Custom custom = Cache.getCustom(vid);
			if(custom != null ){
				if(head == null || type == null || mtype == null){
					if(!CustomCommandProcess.offer(stateKV)){
						logger.error("自定义指令存储队列已满:{}" , vid); 
					}
				} else {
					if(head.equals("CAITS") && type.equals("10") && mtype.equals("D_SETP")){
					} else if(head.equals("CAITS") && type.equals("10") && mtype.equals("U_REPT")){
					} else {
						if(!CustomCommandProcess.offer(stateKV)){
							logger.error("自定义指令存储队列已满:{}" , vid); 
						}
					}
				}
			} 
			
			//--------------------------------------------------------------------
			if ("U_REPT".equals(mtype)) {
				if (Constant.N0.equals(type) || Constant.N1.equals(type) || Constant.N7.equals(type) || Constant.N11.equals(type) || Constant.N5.equals(type)) {
					return null;
				} else {
					if(custom == null && type.equals("10") && head.equals("CAITS")){ 
						return null;
					}
					stateKV.put(Constant.PTYPE, "control");
					return stateKV;
				}
			} else if (Constant.D_REQD.equals(mtype)) {
				if ("1".equals(stateKV.get("TYPE"))){
					String[] parm2  = StringUtils.split(usecontent, Constant.COLON, 2);
					for (int i = 0; i < parm2.length; i++) {
						tempKV = StringUtils.split(parm2[i], Constant.COLON, 2);
						if (tempKV.length == 2)
							stateKV.put(tempKV[0], tempKV[1]);
					}
				}
				stateKV.put(Constant.PTYPE, "control");
				return stateKV;
			}else {
				if(type != null && mtype != null && head != null && type.equals("10") && mtype.equals("D_SETP") && head.equals("CAITS")){ // 自定义指令下发   D_SETP {TYPE:10
//					  自定义信息下发处理
					Custom c = new Custom();
					c.setOutTime(System.currentTimeMillis() + outTime);
					c.setSeq(seq);
					Cache.putCustom(vid, c); 
				}
				stateKV.put(Constant.PTYPE, "control");
				return stateKV;
			}
		} catch (Exception e) {
			logger.error("--parse---协议解析错误:", e);
			return null;
		}
	}
}
