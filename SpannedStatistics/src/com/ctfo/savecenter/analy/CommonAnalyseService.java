package com.ctfo.savecenter.analy;

import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.beans.Message;
import com.ctfo.savecenter.beans.ServiceUnit;
import com.ctfo.savecenter.dao.MonitorDBAdapter;
import com.ctfo.savecenter.dao.TempMemory;
import com.ctfo.savecenter.util.CDate;

/**
 * 通用解析类
 * 
 * @author yangyi
 * 
 */
public class CommonAnalyseService implements IAnalyseService {
	private static final Logger logger = LoggerFactory.getLogger(CommonAnalyseService.class);
	//存储原始记录
//	private static final Logger orgdata = Logger.getLogger("orgdata");
	/**
	 * 监控处理报文
	 * 
	 */
	public Map<String, String> dealPacket(Message messagecommand) {
		String[] parm = null;
		String[] message = null;
		String[] tempKV = null;
		try {
			String command = messagecommand.getCommand();
			//orgdata.fatal(command);
			String msgid = messagecommand.getMsgid();

			message = command.split("\\s+");

			if (message.length < 6) {// 非业务包
				return null;
			}
			// 解析包头
			String head = message[0];// 包头
			if ((!head.equals("CAITS")) && (!head.equals("CAITR"))) {// 不合法包
				return null;
			}

			String seq = message[1];// 业务序列号
			String macid = message[2];// 通讯码
			String channel = message[3];// 通道
			String mtype = message[4];// 指令字
			String content = message[5];// 指令参数
			String usecontent = content.substring(1, content.length() - 1);

			parm = usecontent.split(",");
			
			Map<String, String> stateKV = new HashMap<String, String>();// 状态键值
			for (int i = 0; i < parm.length; i++) {
				tempKV = parm[i].split(":",2);
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
			stateKV.put(Constant.MSGID, msgid);
			stateKV.put(Constant.UUID, UUID.randomUUID().toString());

			//查询车辆缓存对象
			ServiceUnit serviceUnit=TempMemory.getVehicleMapValue(macid);
			if(serviceUnit==null){
				serviceUnit = MonitorDBAdapter.queryVehicleByMacid(macid);
			}
			
			if (serviceUnit != null) {
				stateKV.put(Constant.VID, serviceUnit.getVid().toString());
				stateKV.put(Constant.VEHICLENO, serviceUnit.getVehicleno());
				stateKV.put(Constant.PLATECOLORID,
						serviceUnit.getPlatecolorid());
				stateKV.put(Constant.COMMDR, serviceUnit.getCommaddr());
				stateKV.put(Constant.REARAXLERATE, serviceUnit
						.getRearaxlerate().toString());
				stateKV.put(Constant.TYRER, serviceUnit.getTyrer().toString());
				stateKV.put(Constant.TID, serviceUnit.getTid()+"");

			} else {
				if ("U_REPT".equals(mtype)
						&& ("36".equals(stateKV.get("TYPE"))
								|| "37".equals(stateKV.get("TYPE")) || "38"
								.equals(stateKV.get("TYPE")))) {
					for (int i = 0; i < parm.length; i++) {
						tempKV = parm[i].split(":",2);
						if (tempKV.length == 2){
							stateKV.put(tempKV[0], tempKV[1]);
							
						}
					}
					String[] arr = null;
					if(macid != null){
						arr = macid.split("_",2);
					}
					
					stateKV.put(Constant.VID, "0");
					stateKV.put(Constant.VEHICLENO, "0");
					stateKV.put(Constant.PLATECOLORID, "0");
					if(arr != null){
						stateKV.put(Constant.COMMDR, arr[1]);
					}else{
						stateKV.put(Constant.COMMDR, "0");
					}
					stateKV.put(Constant.REARAXLERATE, "0");
					stateKV.put(Constant.TYRER, "0");
					stateKV.put(Constant.TID, "0");

				} else {
					logger.debug("不存在车辆"+macid);
					return null;
				}

			}

			if ("U_REPT".equals(mtype)) {
				if ("0".equals(stateKV.get("TYPE"))
						|| "1".equals(stateKV.get("TYPE"))
						|| "7".equals(stateKV.get("TYPE"))
						|| "11".equals(stateKV.get("TYPE"))) {// 位置

					stateKV.put(Constant.UTC, String.valueOf(CDate.stringConvertUtc(stateKV.get("4"))));
					stateKV.put(Constant.PTYPE, "track");

				} else if ("5".equals(stateKV.get("TYPE"))) {// 上下线状态
					stateKV.put(Constant.PTYPE, "track");
				} else {
					stateKV.put(Constant.PTYPE, "control");
				}

			} else if ("D_REQD".equals(mtype)) {
				if ("1".equals(stateKV.get("TYPE"))){
					String[] parm2 = usecontent.split(",",2);
					for (int i = 0; i < parm2.length; i++) {
						tempKV = parm2[i].split(":",2);
						if (tempKV.length == 2)
							stateKV.put(tempKV[0], tempKV[1]);
					}
					
				}
				stateKV.put(Constant.PTYPE, "control");
			}
			else {
				stateKV.put(Constant.PTYPE, "control");
			}
			return stateKV;
		} catch (Exception e) {
			logger.error("协议解析 " +  messagecommand.getCommand() + " 错误：" + e.getMessage());
			return null;
		}

	}

	public static void main(String arg[]) {
 
	}
}
