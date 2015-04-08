package com.ctfo.savecenter.dao;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.io.UnsupportedEncodingException;
import java.util.Calendar;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;
import java.util.UUID;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.bosch.exception.DiagException;
import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.beans.EngineFaultInfo;
import com.ctfo.savecenter.beans.QualityRecordBean;
import com.ctfo.savecenter.util.Base64_URl;
import com.ctfo.savecenter.util.CDate;
import com.ctfo.savecenter.util.Converser;
import com.ctfo.savecenter.util.FileUtil;
import com.ctfo.savecenter.util.Tools;
import com.ctfo.savecenter.util.Utils;
import com.lingtu.xmlconf.XmlConf;

public class SaveVehicleLineStatus {
	private static final Logger logger = LoggerFactory.getLogger(SaveVehicleLineStatus.class);

	public static String eloadistUrl = "";
	public static String eventUrl = "";
	public static String oilUrl = "";

	/**
	 * 文件存储：
	 * 1-加热器工作；2-空调工作；3-发动机超转；4-过长怠速；5-超经济区运行；6-空档滑行；7-怠速空调；8-二档起步；9-档位不当;10
	 * -超速；11-疲劳驾驶
	 * 
	 * @param fileName
	 * @param driverEvent
	 * @throws IOException
	 */
	public static void saveLineStatus(XmlConf config, String nodeName, String driverEvent, long vid) {
		String[] event = driverEvent.split("\\|", 3);

		if (event.length == 3) {
			String[] inner = event[1].split("]", 6);
			if (inner.length == 6) {

				String gpsTime = inner[5].replaceAll("\\[", "").replaceAll("\\]", "");
				String fileName = eventUrl + "/" + gpsTime.substring(0, 4) + "/" + gpsTime.substring(4, 6) + "/" + gpsTime.substring(6, 8) + "/" + vid + ".txt";
				RandomAccessFile eventrf = null;

				try {
					eventrf = new RandomAccessFile(fileName, "rw");
					eventrf.seek(eventrf.length());
					eventrf.writeBytes(driverEvent + "\r\n");

				} catch (FileNotFoundException e) {
					logger.error("车辆编号 ： " + vid + "找不到文件操作" + fileName + " 失败！" + e.getMessage());
					// FileUtil.coverFolder(eventUrl);
				} catch (Exception e) {
					logger.error("车辆编号 ： " + vid + "操作文件" + fileName + " 失败！" + e.getMessage());
				} finally {
					try {
						if (eventrf != null) {
							eventrf.close();
						}
					} catch (IOException e) {
						eventrf = null;
					}
				}
			}
		} else {
			logger.trace("存储驾驶行为事件数据格式不正确：" + driverEvent);
		}
	}

	/****
	 * 存储发动机负荷率
	 * 
	 * @param config
	 * @param nodeName
	 * @param value
	 * @param vid
	 */
	public static void saveEloaddist(XmlConf config, String nodeName, String value, long vid) {
		Calendar cal = Calendar.getInstance();
		int month = cal.get(Calendar.MONTH) + 1;
		int day = cal.get(Calendar.DAY_OF_MONTH);
		StringBuffer fileName = new StringBuffer(eloadistUrl);
		fileName.append("/");
		fileName.append(cal.get(Calendar.YEAR));
		fileName.append("/");
		if (month < 10) {
			fileName.append("0");
			fileName.append(month);
		} else {
			fileName.append(month);
		}
		fileName.append("/");
		if (day < 10) {
			fileName.append("0");
			fileName.append(day);
		} else {
			fileName.append(day);
		}
		fileName.append("/");
		fileName.append(vid);
		fileName.append(".txt");

		RandomAccessFile rf = null;

		try {
			byte[] b;
//			sun.misc.BASE64Decoder decoder = new sun.misc.BASE64Decoder();
			StringBuffer eload = new StringBuffer();
//			b = decoder.decodeBuffer(value); // Base64转换
			
			b = Base64_URl.base64DecodeToArray(value);
			for (int i = 0; i < b.length; i++) {
				eload.append(b[i] & 0xff);
				if (b.length - 1 > i) {
					eload.append(" ");
				}
			}
			rf = new RandomAccessFile(fileName.toString(), "rw");
			rf.seek(rf.length());
			rf.writeBytes(eload.toString() + "\r\n");

		} catch (FileNotFoundException e) {
			logger.error("车辆编号 ： " + vid + "找不到文件操作" + fileName + " 失败！" + e.getMessage());
			// FileUtil.coverFolder(eloadistUrl);
		} catch (IOException e) {
			logger.error("车辆编号 ： " + vid + "IO操作" + fileName + " 失败！" + e.getMessage());
		} catch (Exception e) {
			logger.error("车辆编号 ： " + vid + "操作文件" + fileName + " 失败！" + e.getMessage());
		} finally {
			try {
				if (rf != null) {
					rf.close();
				}
			} catch (IOException e) {
				rf = null;
			}
		}
	}

	/****
	 * 存储发动机负荷率
	 * 
	 * @param config
	 * @param nodeName
	 * @param value
	 * @param vid
	 */
	public static void saveOilList(XmlConf config, String value, long vid, CommandManagerKcptDBAdapter db) {
		StringBuffer fileName = new StringBuffer(oilUrl);
		RandomAccessFile rf = null;

		try {
			byte[] b;
			byte[] a;
//			sun.misc.BASE64Decoder decoder = new sun.misc.BASE64Decoder();
			StringBuffer oil = new StringBuffer();
//			a = decoder.decodeBuffer(value); // Base64转换
			a = Base64_URl.base64DecodeToArray(value);
			b = new byte[a.length];
			for (int i = 0; i < a.length; i++) {
				b[i] = (byte) (a[i] & 0xff);
			}

			/*
			 * 0 纬度 DWORD 以度为单位的纬度值乘以10的6次方，精确到百万分之一度 4 经度 DWORD
			 * 以度为单位的经度值乘以10的6次方，精确到百万分之一度 8 高程 WORD 海拔高度，单位为米（m） 10 速度 WORD
			 * 1/10km/h 12 方向 WORD 0 至 359， 正北为0，顺时针 14 时间 BCD[6]
			 * YY-MM-DD-hh-mm-ss（GMT+8时间） 20 防偷漏油数据 BYTE[n] 防偷漏油数据内容
			 * ------------
			 * ------------------------------------------------------
			 * --------------------------- | 0x81(发动机消息头) | 0x01(发动机协议版本标识) | 纬度| 经度 | 海拔 | 速度 | 方向 | 上报时间 | 防偷漏油数据 |
			 * ----------------------------------------------------------------
			 */
			int locZspt = -1;
			// 跳过消息透传类型 0x82
			locZspt += 1;
			// 跳过协议版本号
			locZspt += 1;
			String baseInfo = Utils.getBasicInfo(b, locZspt);
			oil.append(baseInfo);
			String time = baseInfo.substring(baseInfo.lastIndexOf(":") + 1);

			// 创建文件路径
			if (time.length() == 12) {// 120910100652
				fileName.append("/");
				fileName.append("20" + time.substring(0, 2)); // 年
				fileName.append("/");
				fileName.append(time.substring(2, 4)); // 月
				fileName.append("/");
				fileName.append(time.substring(4, 6)); // 日
			} else { // 时间错误，怎么取当前系统时间
				Calendar cal = Calendar.getInstance();
				int month = cal.get(Calendar.MONTH) + 1;
				int day = cal.get(Calendar.DAY_OF_MONTH);
				fileName.append("/");
				fileName.append(cal.get(Calendar.YEAR));
				fileName.append("/");
				if (month < 10) {
					fileName.append("0");
					fileName.append(month);
				} else {
					fileName.append(month);
				}
				fileName.append("/");
				if (day < 10) {
					fileName.append("0");
					fileName.append(day);
				} else {
					fileName.append(day);
				}
			}
			fileName.append("/");
			fileName.append(vid);
			fileName.append(".txt");

			oil.append(":");
			locZspt += 20;
			// ----防偷油数据
			byte ftlyData[] = new byte[b.length - locZspt];
			System.arraycopy(b, locZspt, ftlyData, 0, b.length - locZspt);

			// log.info(NAME+"【防偷漏油】 VIN-->>"+uhc.getTerminalId()+" 数据--->> "+Converser.bytesToHexString(ftlyData));

			// ----油位异常标志 B1B0=00 油位正常; B1B0=01 偷油告警;B1B0=10 加油告警;B1B0=11 保留;
			int ftyLoc = 0;
			byte oilboxStateData[] = new byte[1];
			System.arraycopy(ftlyData, ftyLoc, oilboxStateData, 0, 1);
			String oilboxStateStr = Converser.hexTo2BCD(Converser.bytesToHexString(oilboxStateData));
			String oilboxState = oilboxStateStr.substring(6, oilboxStateStr.length());
			// log.info(NAME+"【防偷漏油】油位异常标志-->: "+oilboxState);

			oil.append(oilboxState);
			oil.append(":");

			ftyLoc += 1;
			// ----燃油液位
			byte oilboxLevelData[] = new byte[4];
			System.arraycopy(ftlyData, ftyLoc, oilboxLevelData, 3, 1);
			int oilboxLevelTemp = Converser.bytes2int(oilboxLevelData);
			// String oilboxLevel=df.format(oilboxLevelTemp);
			// log.info(NAME+"【防偷漏油】燃油液位-->: "+oilboxLevel);
			oil.append(oilboxLevelTemp);
			oil.append(":");

			ftyLoc += 3;
			// ----本次加油量
			byte addOilData[] = new byte[4];
			System.arraycopy(ftlyData, ftyLoc, addOilData, 2, 2);
			// 定义新数组，调整后两个数组的位置
			byte addOilNewData[] = new byte[4];
			addOilNewData[0] = addOilData[0];
			addOilNewData[1] = addOilData[1];
			addOilNewData[2] = addOilData[3];
			addOilNewData[3] = addOilData[2];

			int addOilTemp = Converser.bytes2int(addOilNewData);
			// String addOil=df.format(addOilTemp);
			// log.info(NAME+"【防偷漏油】本次加油量-->: "+addOil);
			oil.append(addOilTemp);
			oil.append(":");

			ftyLoc += 2;
			// ----油箱燃油油量
			byte oilboxMassData[] = new byte[4];
			System.arraycopy(ftlyData, ftyLoc, oilboxMassData, 2, 2);
			// 定义新数组，调整后两个数组的位置
			byte oilboxMassNewData[] = new byte[4];
			oilboxMassNewData[0] = oilboxMassData[0];
			oilboxMassNewData[1] = oilboxMassData[1];
			oilboxMassNewData[2] = oilboxMassData[3];
			oilboxMassNewData[3] = oilboxMassData[2];

			int oilboxMassTemp = Converser.bytes2int(oilboxMassNewData);
			// String oilboxMass=df.format(oilboxMassTemp);
			// log.info(NAME+"【防偷漏油】油箱燃油油量-->: "+oilboxMass);
			oil.append(oilboxMassTemp);

			rf = new RandomAccessFile(fileName.toString(), "rw");
			rf.seek(rf.length());
			rf.writeBytes(oil.toString() + "\r\n");

			if ("01".equals(oilboxState) || "10".equals(oilboxState)) { // 遇到B1B0=01
																		// 偷油告警;B1B0=10
																		// 加油告警则往油箱油量变动记录表（TH_OILMASS_CHANGE_DETAIL）存入变动记录
				db.saveOilChanged(oil.toString(), vid);
				if ("01".equals(oilboxState)) { // 如果是偷油则在报警表存储偷油告警
					db.saveStealingOilAlarm(oil.toString(), vid);
				}
			}

		} catch (FileNotFoundException e) {
			logger.error("车辆编号 ： " + vid + "找不到文件操作" + fileName + " 失败！" + e.getMessage());
			FileUtil.coverFolder(eloadistUrl);
		} catch (Exception e) {
			logger.error("车辆编号 ： " + vid + "操作文件" + fileName + " 失败！" + e.getMessage());
		} finally {
			try {
				if (rf != null) {
					rf.close();
				}
			} catch (IOException e) {
				rf = null;
			}
		}
	}

//	public static DiagnoseService service = DiagnoseServiceImpl.getInstance((byte) 0x01); // 获取服务实例

	/*****************************************
	 * <li>描 述：存储发动机故障信息</li><br>
	 * <li>时 间：2013-8-6 下午3:08:31</li><br>
	 * <li>参数： @param config 配置文件 <li>参数： @param value 诊断内容 <li>参数： @param vid
	 * vid <li>参数： @param db 数据库访问类</li><br>
	 * 
	 *****************************************/
	public static void saveEngineFaultInfo(XmlConf config, Map<String, String> dataMap, CommandManagerKcptDBAdapter commandDBAdapter) {
		// 1. 解析数据
		byte[] buf;
		// 解析为byte数组
		buf = Base64_URl.base64DecodeToArray(dataMap.get("90"));
		EngineFaultInfo engineFaultInfo = new EngineFaultInfo();
		engineFaultInfo.setVid(Long.parseLong(dataMap.get(Constant.VID)));
		engineFaultInfo.setVehicleNo(dataMap.get(Constant.VEHICLENO));
		engineFaultInfo.setVinCode(dataMap.get(Constant.VIN_CODE));
		engineFaultInfo.setBasicCode(dataMap.get("90"));
		engineFaultInfo.setCommaddr(dataMap.get(Constant.COMMDR));
		// 解析基础数据包
		String diagnosis = "";
		try {
			if(buf == null || buf.length < 23){
				logger.error("远程诊断解析非法数据:"+ buf + ",原始指令:"+ dataMap.get(Constant.COMMAND));
				return;
			}
			engineFaultInfo = Tools.getBasicInfo(engineFaultInfo, buf);
			diagnosis = Tools.service.parseData(engineFaultInfo.getVinCode(), engineFaultInfo.getDiagnosisBytes());
		} catch (DiagException e1) {
			logger.error("远程诊断解析数据异常:"+ e1.getMessage() + ",原始指令:"+ dataMap.get(Constant.COMMAND), e1);
		}
		/*
		 * (char)0x01 ┌ (char)0x02 ┐ (char)0x03 └ (char)0x04 ┘ (char)0x05 │
		 */
		if (!"".equals(diagnosis)) {
			// 返回 系统类型
			String sysType = diagnosis.substring(0, diagnosis.indexOf(":"));
			// 返回数据类型
			String dataStr = diagnosis.substring(diagnosis.indexOf(":") + 1, diagnosis.length());
			// 0112:X0011┌空调压缩机开关故障┐历史故障└0011┘X0012┌HFM传感器进气温度故障;HFM传感器进气温度电压值超出上限门槛值┐现行故障└0012┘X0013┌空调压缩机开关故障┐└0013┘│
			if (sysType.equals("0112") || sysType.equals("12")) {
				try {
					String bugCode = "", bugDesc = "", bugFlag = "", basicCode = "";
					String[] bugArray = dataStr.split((char) 0x04 + "");
					for (int i = 0; i < bugArray.length; i++) {
						if (bugArray[i].indexOf((char) 0x02) > -1) {
							bugCode = bugArray[i].substring(0, bugArray[i].indexOf((char) 0x01));
							bugDesc = bugArray[i].substring(bugArray[i].indexOf((char) 0x01) + 1, bugArray[i].indexOf((char) 0x02));
							bugFlag = bugArray[i].substring(bugArray[i].indexOf((char) 0x02) + 1, bugArray[i].indexOf((char) 0x03));
							basicCode = bugArray[i].substring(bugArray[i].indexOf((char) 0x03) + 1, bugArray[i].length());
							logger.debug(" <远程诊断-->故障码bugCode: " + bugCode + " bugDesc: " + bugDesc + " bugFlag: " + bugFlag + " basicCode: " + basicCode);
							// 获得插入故障码表序列号,用来作为发送请求冻结帧的唯一标识
							engineFaultInfo.setBugId(UUID.randomUUID().toString().replace("-", ""));
							engineFaultInfo.setBugCode(bugCode);
							engineFaultInfo.setBugDesc(bugDesc);
							engineFaultInfo.setBugFlag(bugFlag);
							engineFaultInfo.setBasicCode(basicCode);
							commandDBAdapter.saveEngBug(engineFaultInfo);
						}
					}
					if(bugArray.length == 0){
						//如果没有故障，就插入一条空数据，状态为02，代表没有故障
						engineFaultInfo.setStatus("02");
						engineFaultInfo.setBugId(UUID.randomUUID().toString().replace("-", ""));
						engineFaultInfo.setBugCode("NULL");
						engineFaultInfo.setBugDesc("NULL");
						engineFaultInfo.setBugFlag("0");
						engineFaultInfo.setBasicCode("NULL");
						commandDBAdapter.saveEngBug(engineFaultInfo);
					}
				} catch (Exception e) {
					logger.error("--prase-EngBug-ERROR--解析发动机故障信息异常:", e);
				}
			}
//			// 清除故障码信息 (13),设置时间信息(01) 插入表ZSPT_ENG_MESSAGE
//			if (sysType.equals("0113") || sysType.equals("13")) {
//				try {
//					engineFaultInfo.setBugSeqId(dataMap.get(Constant.SEQ));
//					engineFaultInfo.setClearFlag("1");
//					commandDBAdapter.updateEngBugDispose(engineFaultInfo);
//				} catch (Exception e) {
//					logger.error("--clear-EngBug-ERROR--清除发动机故障信息异常:", e);
//				}
//			}
			// 0162:ECU版本号└10373223432┘ECU识别号└10001┘ECU零件号└T000001┘生产日期└2008-12-01┘│
			if (sysType.equals("0162") || sysType.equals("62")) {
				try {
					// 插入版本信息表
					String versionCode = "", versionValue = "";
					String[] versionArray = dataStr.split((char) 0x04 + "");
					for (int i = 0; i < versionArray.length; i++) {
						if (versionArray[i].indexOf((char) 0x03) > -1) {
							versionCode = versionArray[i].substring(0, versionArray[i].indexOf((char) 0x03));
							versionValue = versionArray[i].substring(versionArray[i].indexOf((char) 0x03) + 1, versionArray[i].length());
							// 赋值 版本代码,版本描述
							engineFaultInfo.setBugId(UUID.randomUUID().toString().replace("-", ""));// 版本系列号
							engineFaultInfo.setVersionCode(versionCode); // 版本代码
							engineFaultInfo.setVersionValue(versionValue); // 版本描述
							engineFaultInfo.setReportTime(String.valueOf(System.currentTimeMillis()));
							commandDBAdapter.saveEngVersionInfo(engineFaultInfo);
						}
					}
				} catch (Exception e) {
					e.printStackTrace();
				}
			}

		}
	}

	/*****************************************
	 * <li>描        述：更新发动机清码信息 		</li><br>
	 * <li>时        间：2013-8-19  下午12:48:01	</li><br>
	 * <li>参数： @param app			</li><br>
	 * 
	 *****************************************/
	public static void updateEngBugDispose(String seq, CommandManagerKcptDBAdapter commandDBAdapter) {
		EngineFaultInfo engineFaultInfo = new EngineFaultInfo();
		engineFaultInfo.setBugSeqId(seq);
		engineFaultInfo.setClearFlag("1");
		commandDBAdapter.updateEngBugDispose(engineFaultInfo);
		
	}
	
	
	/*****************************************
	 * <li>描 述：存储质检单信息</li><br>
	 * <li>时 间：2013-8-6 下午3:08:31</li><br>
	 * <li>参数： @param config 配置文件 <li>参数： @param value 质检单内容 <li>参数： @param vid
	 * vid <li>参数： @param db 数据库访问类</li><br>
	 * 
	 *****************************************/
	public static void saveQualityRecordInfo(XmlConf config, Map<String, String> dataMap, CommandManagerKcptDBAdapter commandDBAdapter) {
		
		// 1. 解析数据
		byte[] buf;
		// 解析为byte数组
		buf = Base64_URl.base64DecodeToArray(dataMap.get("90"));
		QualityRecordBean qualityRecordBean = new QualityRecordBean();
		qualityRecordBean.setVid(dataMap.get(Constant.VID));
		qualityRecordBean.setCommaddr(dataMap.get(Constant.COMMDR));
		// 解析基础数据包
//		String diagnosis = "";
		try {
			if(buf == null ){
				logger.error("质检单非法数据:"+ buf + ",原始指令:"+ dataMap.get(Constant.COMMAND));
				return;
			}
			int loc = 0;
			//跳过版本号
			loc += 1;
			//消息ID
//			byte subcommand = buf[loc];
//			int subCommandId = subcommand;
			
			loc += 1;
			
			//解析终端时间
			byte timeBytes[] = new byte[6];		
			System.arraycopy(buf, loc, timeBytes, 0, 6);		
			String time = Converser.bcdToStr(timeBytes, 0, 6);
			
			Long utcTime = CDate.stringConvertUtc(CDate.changeDateFormat("yyMMddHHmmss", "yyyyMMdd/HHmmss", time));
			
			loc += 6;
			
			//解析车辆VIN
			byte vin[] = new byte[17];
			System.arraycopy(buf, loc, vin, 0, 17);
			String vinCode = new String(vin,"gbk");
			
			loc += 17;
			
			//解析终端配置
			byte cfg[] = new byte[2];
			cfg[1] = buf[loc];
			int terminalConfig = Converser.bytes2Short(cfg, 0);
			
			loc += 1;
			
			//特征系数
			byte plus[] = new byte[4];
			System.arraycopy(buf, loc, plus, 2, 2);
			int speedPlus = Converser.bytes2int(plus);
			
			loc += 2;
			
			//gprs强度
			byte strength[] = new byte[2];
			strength[1] = buf[loc];
			int gprsStrength= Converser.bytes2Short(strength, 0);
			
			loc += 1;
			
			//gps状态
			byte state[] = new byte[2];
			state[1] = buf[loc];
			int gpsState= Converser.bytes2Short(state, 0);
			
			loc += 1;
			
			//异常项数
			byte num[] = new byte[2];
			num[1] = buf[loc];
//			int exceptionNum = Converser.bytes2Short(num, 0);
			
			loc += 1;
			
			//检测项数
			byte num2[] = new byte[2];
			num2[1] = buf[loc];
//			int testNum = Converser.bytes2Short(num2, 0);
			
			loc += 1;
			
			qualityRecordBean.setUtc(utcTime);
			qualityRecordBean.setVinCode(vinCode);
			qualityRecordBean.setTerminalConfig(""+terminalConfig);
			qualityRecordBean.setSpeedPlus(speedPlus);
			qualityRecordBean.setGprsStrength(""+gprsStrength);
			qualityRecordBean.setGpsState(""+gpsState);
			
			//检测内容
//			Map<Integer,Integer> map = new HashMap<Integer,Integer>();
			
			commandDBAdapter.initQualityRecordStatement();
			//单项代号 和 状态 各占一个字节
			while (buf.length-loc>=2){
				//单项代号
				byte id[] = new byte[2];
				id[1] = buf[loc];
				int recordParam = Converser.bytes2Short(id, 0);
				
				loc += 1;
				
				//单项状态 （0|1）
				byte  val[] = new byte[2];
				val[1] = buf[loc];
				int recordValue =  Converser.bytes2Short(val, 0);

				loc +=1;
				
				qualityRecordBean.setParamId(""+recordParam);
				qualityRecordBean.setParamValue(""+recordValue);
				
				qualityRecordBean.setRecordId(UUID.randomUUID().toString().trim().replaceAll("-", ""));
				
				//执行保存
				commandDBAdapter.saveQualityRecord(qualityRecordBean);
			}
			
			commandDBAdapter.commitQulityRecord();

		} catch (UnsupportedEncodingException e) {
			logger.error("质检单解析数据异常:系统不支持此字符集；"+ e.getMessage() + ",原始指令:"+ dataMap.get(Constant.COMMAND), e);
		}catch (Exception e1) {
			logger.error("质检单解析数据异常:"+ e1.getMessage() + ",原始指令:"+ dataMap.get(Constant.COMMAND), e1);
		} 
		
	}

}
