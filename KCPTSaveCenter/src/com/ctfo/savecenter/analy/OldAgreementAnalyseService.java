package com.ctfo.savecenter.analy;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
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
 * 兼容老协议解析
 * 
 * @author yangyi
 * 
 */
public class OldAgreementAnalyseService implements IAnalyseService {
	private static final Logger logger = LoggerFactory.getLogger(OldAgreementAnalyseService.class);

	/**
	 * 监控处理报文
	 * 
	 */
	public Map<String, String> dealPacket(Message messagecommand) {
		try {
			String command = messagecommand.getCommand();
			// orgdata.fatal(command);
			String msgid = messagecommand.getMsgid();

			String[] message = command.split("\\s+");

			if (message.length < 5) {// 非业务包
				return null;
			}
			// 解析包头
			String head = message[0];// 包头
			if ((!head.equals("CAITS")) && (!head.equals("CAITR"))) {// 不合法包
				return null;
			}

			// 20:87,20:88,20:89,20:90,20:91,20:92,20:93,20:94,20:95
			// 扩展 500:2|4|6|8|9|12|14|16|18|20|22|24|26|28|30|32|34|36 ,基本
			// 502:19|21|23 ,512:2|4|5|7|9|11|13|15|17

			String seq = message[1];// 业务序列号
			String macid = message[2];// 通讯码
			String channel = message[3];// 通道
			String mtype = message[4];// 指令字
			String content = message[5];// 指令参数
			String usecontent = content.substring(1, content.length() - 1);

			String[] parm = usecontent.split(",");
			String[] tempKV = null;
			List<String> alarmList = new ArrayList<String>();
			Map<String, String> stateKV = new HashMap<String, String>();// 状态键值
			for (int i = 0; i < parm.length; i++) {
				tempKV = parm[i].split(":", 2);
				if (tempKV.length == 2) {
					if ("20".equals(tempKV[0])) {
						alarmList.add(tempKV[1]);
					} else {
						stateKV.put(tempKV[0], tempKV[1]);
					}
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

			// 查询车辆缓存对象
			ServiceUnit serviceUnit = TempMemory.getVehicleMapValue(macid);
			if (serviceUnit == null) {
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
				stateKV.put(Constant.TID, serviceUnit.getTid() + "");

			} else {
				if ("U_REPT".equals(mtype)
						&& ("36".equals(stateKV.get("TYPE"))
								|| "37".equals(stateKV.get("TYPE")) || "38"
								.equals(stateKV.get("TYPE")))) {
					stateKV.put(Constant.VID, "0");
					stateKV.put(Constant.VEHICLENO, "0");
					stateKV.put(Constant.PLATECOLORID, "0");
					stateKV.put(Constant.COMMDR, "0");
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

					stateKV.put(Constant.UTC, String.valueOf(CDate
							.stringConvertUtc(stateKV.get("4"))));
					stateKV.put(Constant.PTYPE, "track");

					// 报警状态
					if (alarmList.size() > 0) {
						char[] alarmChars = new char[64];
						for (int i = 0; i < alarmList.size(); i++) {
							String alarmcode = alarmList.get(i);
							if (alarmcode != null) {
								Integer index = OldAgreementData.alarmMap
										.get(alarmcode);
								if (index != null) {
									alarmChars[index] = '1';
								}
							}
						}
						String basealarm = "";
						String extendalarm = "";
						for (int i = alarmChars.length - 1; i >= 0; i--) {
							if (i < 32) {
								if (alarmChars[i] == '1') {
									basealarm += "1";
								} else {
									basealarm += "0";
								}
							} else {
								if (alarmChars[i] == '1') {
									extendalarm += "1";
								} else {
									extendalarm += "0";
								}
							}
						}
						stateKV.put("20", Integer.valueOf(basealarm, 2)
								.toString());
						stateKV.put("21", Integer.valueOf(extendalarm, 2)
								.toString());
						
						logger.debug(macid+ ",20:"+Integer.valueOf(basealarm, 2).toString()+",21:"+Integer.valueOf(extendalarm, 2)
								.toString());
					}else{
						stateKV.put("20", "0");
						stateKV.put("21", "0");
					}

					// 扩展状态
					String extendStatusString = stateKV.get("500");
					if (extendStatusString != null) {
						char[] extendstatusChars = new char[32];
						String[] extendstatusStrings = extendStatusString
								.split("\\|");
						for (int i = 0; i < extendstatusStrings.length; i++) {
							Integer index = OldAgreementData.extendStatusMap
									.get(extendstatusStrings[i]);
							if (index != null) {
								extendstatusChars[index] = '1';
							}
						}
						extendStatusString = "";
						for (int i = extendstatusChars.length - 1; i >= 0; i--) {
							if (extendstatusChars[i] == '1') {
								extendStatusString += "1";
							} else {
								extendStatusString += "0";
							}
						}
						stateKV.put("500",
								Integer.valueOf(extendStatusString, 2)
										.toString());
					}

					// 基本状态
					String baseStatusString1 = stateKV.get("512");
					String baseStatusString2 = stateKV.get("502");
					if (baseStatusString1 != null) {
						char[] baseStatusChars = new char[32];
						String[] baseStatusStrings1 = baseStatusString1
								.split("\\|");

						for (int i = 0; i < baseStatusStrings1.length; i++) {
							Integer index = OldAgreementData.baseStatusMap
									.get(baseStatusStrings1[i]);
							if (index != null) {
								baseStatusChars[index] = '1';
							}
						}
						if (baseStatusString2 != null) {
							String[] baseStatusStrings2 = baseStatusString2
									.split("\\|");
							for (int i = 0; i < baseStatusStrings2.length; i++) {
								Integer index = OldAgreementData.baseStatusMap
										.get(baseStatusStrings2[i]);
								if (index != null) {
									baseStatusChars[index] = '1';
								}
							}
						}
						String baseStatusString = "";
						for (int i = baseStatusChars.length - 1; i >= 0; i--) {
							if (baseStatusChars[i] == '1') {
								baseStatusString += "1";
							} else {
								baseStatusString += "0";
							}
						}
						stateKV.put("8", Integer.valueOf(baseStatusString, 2)
								.toString());
					}

				} else if ("5".equals(stateKV.get("TYPE"))) {// 上下线状态
					stateKV.put(Constant.PTYPE, "track");
				} else {
					stateKV.put(Constant.PTYPE, "control");
				}

			} else if ("D_REQD".equals(mtype)) {
				if ("1".equals(stateKV.get("TYPE"))) {
					String[] parm2 = usecontent.split(",", 2);
					for (int i = 0; i < parm2.length; i++) {
						tempKV = parm2[i].split(":", 2);
						if (tempKV.length == 2)
							stateKV.put(tempKV[0], tempKV[1]);
					}

				}
				stateKV.put(Constant.PTYPE, "control");
			} else {
				stateKV.put(Constant.PTYPE, "control");
			}
			return stateKV;
		} catch (Exception e) {
			e.printStackTrace();
			logger.error("协议解析错误："+e.getMessage());
			return null;
		}

	}

//	public static void main(String arg[]) {
//		System.out.print(Long.toBinaryString(19));
//	}
}
