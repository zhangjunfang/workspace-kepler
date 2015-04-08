package com.ctfo.commandservice.service;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.lang3.StringUtils;
import org.junit.Test;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.commandservice.model.EngineFaultInfo;
import com.ctfo.commandservice.model.IccId;
import com.ctfo.commandservice.model.LinkStatus;
import com.ctfo.commandservice.model.OilInfo;
import com.ctfo.commandservice.model.OracleProperties;
import com.ctfo.commandservice.model.PlatformMessage;
import com.ctfo.commandservice.model.ServiceUnit;
import com.ctfo.commandservice.model.Supervision;
import com.ctfo.commandservice.model.TerminalVersion;
import com.ctfo.commandservice.util.Cache;
import com.ctfo.commandservice.util.Constant;
import com.ctfo.generator.pk.GeneratorPK;


public class OracleJdbcServiceTest {
	ApplicationContext ac = null;
	OracleProperties oracleProperties = null;
	OracleJdbcService service = null;
	
	static String command = "CAITR 0_0 E001_15249674404 0 D_GETP {TYPE:0,RET:0,1:7709,10:,100:30,101:3,102:30,103:3,104:60,105:3,106:,107:,108:,109:58.83.210.8,110:7709,111:0,112:0,113:30,114:300,115:10,116:10,117:500,118:1000,119:1000,120:100,121:45,122:,123:,124:,125:1,126:600,127:18000,128:100,129:10,130:14400,131:57600,132:1200,133:0,134:36,135:100,136:5,137:0,138:0,139:0,140:0,141:,142:4143969800,143:1,144:1,145:0,146:7,147:0,15:,180:0,181:0,187:2,190:0,2:M2M.YUTONG.COM,200:0,201:,202:,203:ZK6908H,204:LZYTDTDF4D1072762,205:,206:,207:5,208:10,209:0,210:1600,211:3,212:600,213:300,214:600,215:29,216:1600,217:1000,218:0,219:0,3:ZZYTBJ.HA,300:50,301:1800,302:1337,303:8,304:400,305:////////////////AAAAAAAAAAAAAAAAAAAAAAAAAAA=,306:65280,307:1920,308:2,309:1,31:0,310:大型汽车,4:,41:赣CH1217,42:2,5:,7:60,9:}";
	static String[] message = null;
	String seq = null;// 业务序列号
	String macid = null;// 通讯码
	String channel = null;// 通道
	String mtype = null;// 指令字
	String content = null;// 指令参数
	String usecontent = null;
	String[] parm = null;
	Map<String, String> dataMap = new HashMap<String, String>();
	
	public OracleJdbcServiceTest(){
		ac = new ClassPathXmlApplicationContext("spring-dataaccess.xml");
		oracleProperties = (OracleProperties) ac.getBean("oracleProperties");
		service = new OracleJdbcService(oracleProperties);
		
		message = StringUtils.split(command, Constant.SPACES);
		seq = message[1];// 业务序列号
		macid = message[2];// 通讯码
		channel = message[3];// 通道
		mtype = message[4];// 指令字
		content = message[5];// 指令参数
		usecontent = content.substring(1, content.length() - 1);
		parm = StringUtils.split(usecontent, Constant.COMMA);
		String[] tempKV = null;
		for (int i = 0; i < parm.length; i++) {
			tempKV = StringUtils.splitPreserveAllTokens(parm[i], Constant.COLON, 2);
			if (tempKV.length == 2){
				dataMap.put(tempKV[0], tempKV[1]);
			}
		}
	}
	/**
	 * 存储指令
	 */
	@Test
	public void testSaveControlCommand() {
		String seq = "156998_1376631000_4";
		System.out.println("插入前:"+service.getResult("SELECT * FROM TH_VEHICLE_COMMAND WHERE CO_SEQ='"+seq+"' "));
		Map<String, String> app = new HashMap<String, String>();
//		CAITS 156998_1376631795_4 E001_18013010979 0 D_SETP {TYPE:9,RETRY:1,91:129,90:AQAEYgAzmQ==} 
		app.put("vid", "246830");
		app.put("vehicleno", "豫111125");
		app.put("mtype", "U_REPT");// L_PROV   L_PLAT
		app.put("seq", seq);
		app.put("channel", "0");
		app.put("content", "TYPE:9,RETRY:1,91:129,90:AQAEYgAzmQ==");
		app.put("command", "CAITS 156998_1376631795_4 E001_18013010979 0 D_SETP {TYPE:9,RETRY:1,91:129,90:AQAEYgAzmQ==}");
		app.put("TYPE", "0");
		app.put("oecode", "E001_18013010979");
		long s = System.currentTimeMillis();
		service.saveControlCommand(app); 
		String re = service.getResult("SELECT * FROM TH_VEHICLE_COMMAND WHERE CO_SEQ='"+seq+"' ");
		System.out.println("测试结果："+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}
	/**
	 * 存储多媒体数据
	 */
	@Test
	public void testSaveMultMedia() {
		String uuid = GeneratorPK.instance().getPKString();
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_MEDIA WHERE MEDIA_ID='"+uuid+"' "));
		Map<String, String> app = new HashMap<String, String>();
		app.put("uuid", uuid);
		app.put("vid", "123456");// L_PROV   L_PLAT
		app.put("macid", "E001_18013010979");
		app.put("CHANNEL_TYPE", "0");  //多媒体上传设备类型 (0:2G;1:3G)
		app.put("1", "18377310");
		app.put("2", "70525899");
		app.put("3", "16");
		app.put("5", "16");
		app.put("6", "16");
		app.put("8", "3");
		app.put("120", "12345");
		app.put("121", "12345");
		app.put("122", "12345");
		app.put("123", "12345");
		app.put("124", "12345");
		app.put("125", "12345");
		app.put("126", "12345");
		
		long s = System.currentTimeMillis();
		service.saveMultMedia(app); 
		String re = service.getResult("SELECT * FROM TH_VEHICLE_MEDIA WHERE MEDIA_ID='"+uuid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveVehicleDispatchMsg() {
		String uuid = GeneratorPK.instance().getPKString();
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_MEDIA WHERE MEDIA_ID='"+uuid+"' "));
		Map<String, String> app = new HashMap<String, String>();
		app.put("uuid", uuid);
		app.put("vid", "123456");// L_PROV   L_PLAT
		app.put("macid", "E001_18013010979");
		app.put("CHANNEL_TYPE", "0");  //多媒体上传设备类型 (0:2G;1:3G)
		app.put("1", "18377310");
		app.put("2", "70525899");
		app.put("3", "16");
		app.put("5", "16");
		app.put("6", "16");
		app.put("8", "3");
		app.put("120", "12345");
		app.put("121", "12345");
		app.put("122", "12345");
		app.put("123", "12345");
		app.put("124", "12345");
		app.put("125", "12345");
		app.put("126", "12345");
		
		long s = System.currentTimeMillis();
		service.saveMultMedia(app); 
		String re = service.getResult("SELECT * FROM TH_VEHICLE_MEDIA WHERE MEDIA_ID='"+uuid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testUpdateTernimalVersion() {
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TB_TERMINAL WHERE tid >= 10901 and tid < 10909"));
		List<TerminalVersion> list = new ArrayList<TerminalVersion>();
		int batchSize = 2;
		for(int i = 1; i < 10; i++){
			TerminalVersion t = new TerminalVersion();
			t.setTerminalHardVersion("FIRMWARE");
			t.setTerminalFirmwareVersion("FIRMWARE");
			t.settId("1090" + i);
			list.add(t);
		}
		long s = System.currentTimeMillis();
		service.updateTernimalVersion(list, batchSize); 
		String re = service.getResult("SELECT * FROM TB_TERMINAL WHERE tid >= 10901 and tid < 10909");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveMultimediaEvent() {
		String uuid = GeneratorPK.instance().getPKString();
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_MULTIMEDIA_EVENT WHERE PID='"+uuid+"' "));
		Map<String, String> app = new HashMap<String, String>();
		app.put("uuid", uuid);
		app.put("vid", "123456");// L_PROV   L_PLAT
		app.put("120", "1");
		app.put("121", "1");
		app.put("122", "1");
		app.put("123", "1");
		app.put("124", "1");
		app.put("125", "1");
		app.put("126", "1325909864560");
		app.put("127", "1");
		long s = System.currentTimeMillis();
		service.saveMultimediaEvent(app); 
		String re = service.getResult("SELECT * FROM TH_VEHICLE_MULTIMEDIA_EVENT WHERE PID='"+uuid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveEticket() {
		String uuid = GeneratorPK.instance().getPKString();
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_ETICKET WHERE PID='"+uuid+"' "));
		Map<String, String> app = new HashMap<String, String>();
		app.put("uuid", uuid);
		app.put("vid", "123456");// L_PROV   L_PLAT
		app.put("87", "x+vNo7O1");
		long s = System.currentTimeMillis();
		service.saveEticket(app); 
		String re = service.getResult("SELECT * FROM TH_VEHICLE_ETICKET WHERE PID='"+uuid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveTernimalRegisterInfo() {
		String uuid = GeneratorPK.instance().getPKString();
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_TERMINAL_REGISTER WHERE AUTO_ID='"+uuid+"' "));
		Map<String, String> app = new HashMap<String, String>();
		app.put("uuid", uuid);
		app.put("40", "0");
		app.put("41", "0");
		app.put("42", "0");
		app.put("43", "0");
		app.put("44", "0");
		app.put("45", "0");
		app.put("104", "0");
		app.put("platecolorid", "02");
		app.put("commdr", "1331133113");
		
		long s = System.currentTimeMillis();
		service.saveTernimalRegisterInfo(app);
		String re = service.getResult("SELECT * FROM TH_TERMINAL_REGISTER WHERE AUTO_ID='"+uuid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testUpdateVehicleLogOff() {
		System.out.println("插入前:"+service.getResult("SELECT * FROM TH_TERMINAL_REGISTER WHERE UTC = (SELECT MAX(UTC)  FROM TH_TERMINAL_REGISTER  WHERE commaddr ='18013010979') AND RESULT = 0 AND commaddr ='18013010979' "));
		Map<String, String> app = new HashMap<String, String>();
		app.put("46", "0");
		app.put("macid", "E001_18013010979");
		ServiceUnit serviceUnit = new ServiceUnit();
		serviceUnit.setCommaddr("18013010979");
		Cache.setVehicleMapValue("E001_18013010979", serviceUnit);
		long s = System.currentTimeMillis();
		service.updateVehicleLogOff(app);
		String re = service.getResult("SELECT * FROM TH_TERMINAL_REGISTER WHERE UTC = (SELECT MAX(UTC)  FROM TH_TERMINAL_REGISTER  WHERE commaddr ='18013010979') AND RESULT = 0 AND commaddr ='18013010979'");
		System.out.println("测试结果："+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveVehicleLogOff() {
		String uuid = GeneratorPK.instance().getPKString();
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_LOGOFF WHERE PID='"+uuid+"' "));
		Map<String, String> app = new HashMap<String, String>();
		app.put("uuid", uuid);
		app.put("commdr", "1331133113");
		app.put("seq", "12345_1331133113_12345");
		app.put("macid", "E001_123456578901");
		app.put("46", "0");
		long s = System.currentTimeMillis();
		service.saveVehicleLogOff(app);
		String re = service.getResult("SELECT * FROM TH_VEHICLE_LOGOFF WHERE PID='"+uuid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}


	@Test
	public void testSaveIccId() {
		String phoneNumber = "38749493574";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TB_SIM WHERE COMMADDR='"+phoneNumber+"' "));
		List<IccId> list = new ArrayList<IccId>();
		int batchSize = 2;
		for(int i = 1; i < 5; i++){
			IccId icc = new IccId();
			icc.setIccId("0");
			icc.setPhoneNumber(phoneNumber);
			list.add(icc);
		}
		long s = System.currentTimeMillis();
		service.saveIccId(list, batchSize);
		String re = service.getResult("SELECT * FROM TB_SIM WHERE COMMADDR='"+phoneNumber+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveDriverInfo() {
		String uuid = GeneratorPK.instance().getPKString();
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_DRIVER WHERE PID='"+uuid+"' "));
		Map<String, String> app = new HashMap<String, String>();
		app.put("uuid", uuid);
		app.put("vid", "123456");
		app.put("RESULT", "0");
		app.put("110", "110");
		app.put("111", "111");
		app.put("112", "112");
		app.put("113", "113");
		
		long s = System.currentTimeMillis();
		service.saveDriverInfo(app);
		String re = service.getResult("SELECT * FROM TH_VEHICLE_DRIVER WHERE PID='"+uuid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveDriverLoginingInfo() {
		String uuid = GeneratorPK.instance().getPKString();
//		uuid = "d88a15bd-a310-441f-95c5-5dafb7cd9a2c";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_DRIVER_LOGIN_RECORD WHERE AUTO_ID='"+uuid+"' "));
		Map<String, String> app = new HashMap<String, String>();
		app.put("uuid", uuid);
		app.put("vid", "123456");
		app.put("macid", "E001_13311331121");
		app.put("107", "1"); //从业资格证IC卡插拔状态(1: 上班，2:下班)
		app.put("108", "140304163030");
		app.put("109", "0");
		app.put("110", "0");
		app.put("111", "0");
		app.put("112", "0");
		app.put("113", "0");
		app.put("114", "20140305");
		
		long s = System.currentTimeMillis();
		service.saveDriverLoginingInfo(app); 
		String re = service.getResult("SELECT * FROM TH_DRIVER_LOGIN_RECORD WHERE AUTO_ID='"+uuid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveEventId() {
		String uuid = GeneratorPK.instance().getPKString();
//		uuid = "d88a15bd-a310-441f-95c5-5dafb7cd9a2c";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_EVENTS WHERE PID='"+uuid+"' "));
		Map<String, String> app = new HashMap<String, String>();
		app.put("uuid", uuid);
		app.put("vid", "123456");
		app.put("81", "13311331121"); // EVENT_ID
		
		long s = System.currentTimeMillis();
		service.saveEventId(app);
		String re = service.getResult("SELECT * FROM TH_VEHICLE_EVENTS WHERE PID='"+uuid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testUpdateQuerstionAnswer() {
		String uuid = GeneratorPK.instance().getPKString();
		uuid = "211111";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_QUESTION_ANSWER WHERE AUTO_ID='"+uuid+"' "));
		Map<String, String> app = new HashMap<String, String>();
		app.put("seq", "0_0");
		app.put("82", "13311331121"); 
		long s = System.currentTimeMillis();
		service.updateQuerstionAnswer(app);
		String re = service.getResult("SELECT * FROM TH_QUESTION_ANSWER WHERE AUTO_ID='"+uuid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveOilChanged() {
		String vid = "242447";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_OILMASS_CHANGE_DETAIL WHERE VID='"+vid+"' "));
		long s = System.currentTimeMillis();
//		service.saveOilList("AQHbcLwGdbKCALwAAABGFAMEFCYzALdADgAAVQ4=", "242447");
		String re = service.getResult("SELECT * FROM TH_OILMASS_CHANGE_DETAIL WHERE VID='"+vid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveStealingOilAlarm() {
		String uuid = GeneratorPK.instance().getPKString();
		String vid = "242447";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_ALARM WHERE ALARM_ID='"+uuid+"' "));
		long s = System.currentTimeMillis();
		service.saveStealingOilAlarm("0:0:2:3:4:040304173920:0:0:0:0", vid, uuid); 
		String re = service.getResult("SELECT * FROM TH_VEHICLE_ALARM WHERE ALARM_ID='"+uuid+"' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveEngineFaultInfo() {
		String vid = "242447";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_ENG_BUG WHERE vid='"+vid+"'  and commaddr='13311331121'"));
		Map<String, String> dataMap = new HashMap<String, String>();
//		存储版本信息TB_ENG_VERSION  【AQACEWInBsanXAB8AAAAABMIFRESBgAoogAzoPEQWptDNTIwNDJGQVczNTYzMEo2VF9NMkQzX0JYRTE1MDDiwQ==】【AQACEWInBsanXAB8AAAAABMIFRESBgApogAzofEQWoNQXzUyMCBWNDIgVk9JREVEQzdVQzMxIFA1MjAgVjQy1aj///////8=】
		
		dataMap.put("90", "AQACEWL4BsanCgB0AAAAABMIFRckRAALUwAzg/EQVP8A1z//");
		dataMap.put("vid", vid); 
		dataMap.put("vehicleno", "测A12345");
		dataMap.put("VIN_CODE", "LZYTATE61C1064611"); 
		dataMap.put("commdr", "13311331121");
		long s = System.currentTimeMillis();
		service.saveEngineFaultInfo(dataMap);
		String re = service.getResult("SELECT * FROM TH_ENG_BUG WHERE vid='"+vid+"' and commaddr='13311331121' ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveEngVersionInfo() {
		String vid = "242448";
		String uuid = GeneratorPK.instance().getPKString();
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TB_ENG_VERSION WHERE version_id='"+uuid+"'  "));
		
		EngineFaultInfo engineFaultInfo = new EngineFaultInfo();
		engineFaultInfo.setVid(vid);
		engineFaultInfo.setVinCode("LZYTATE61C1064611");
		engineFaultInfo.setCommaddr("13311331121");
		engineFaultInfo.setBugId(uuid);
		engineFaultInfo.setVehicleNo("测A12345");
		engineFaultInfo.setVersionCode("1234567890");
		engineFaultInfo.setVersionValue("版本描述");
		engineFaultInfo.setReportTime(1393927747806l);
		
		long s = System.currentTimeMillis();
		service.saveEngVersionInfo(engineFaultInfo);
		String re = service.getResult("SELECT * FROM TB_ENG_VERSION WHERE version_id='"+uuid+"'   ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveEngBug() {
		String vid = "242448";
		String uuid = GeneratorPK.instance().getPKString();
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_ENG_BUG WHERE bug_id='"+uuid+"'  "));
		
		EngineFaultInfo engineFaultInfo = new EngineFaultInfo();
		engineFaultInfo.setVid(vid);
		engineFaultInfo.setVinCode("LZYTATE61C1064611");
		engineFaultInfo.setCommaddr("13311331121");
		engineFaultInfo.setBugId(uuid);
		engineFaultInfo.setVehicleNo("测A12345");
		engineFaultInfo.setVersionCode("1234567890");
		engineFaultInfo.setVersionValue("版本描述");
		engineFaultInfo.setReportTime(140207234620l);// 上报时间
		engineFaultInfo.setStatus("0");  // 状态
		engineFaultInfo.setBugCode("E877A");// 故障码
		engineFaultInfo.setBugDesc("故障描述");// 故障码描述
		engineFaultInfo.setBugFlag("故障码状态说明");// 故障码状态说明
		engineFaultInfo.setBasicCode("BASE_CODE");// 原始故障码
		
		engineFaultInfo.setLat(0l); // 纬度
		engineFaultInfo.setLon(0l); // 经度
		engineFaultInfo.setMaplat(0l); // 偏移后纬度
		engineFaultInfo.setMaplon(0l); // 偏移后经度
		engineFaultInfo.setElevation(0); // 海拔
		engineFaultInfo.setDirection(0); // 方向
		engineFaultInfo.setGpsSpeeding(0);// GPS速度

		
		long s = System.currentTimeMillis();
		service.saveEngBug(engineFaultInfo);
		String re = service.getResult("SELECT * FROM TH_ENG_BUG WHERE bug_id='"+uuid+"'   ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveBridge() {
		String uuid = GeneratorPK.instance().getPKString();
		String vid = "242447";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_BRIDGE WHERE pid='"+uuid+"'  "));
		Map<String, String> dataMap = new HashMap<String, String>();
		dataMap.put("uuid", uuid);
		dataMap.put("vid", vid); 
		dataMap.put("90", "x+vNo7O1");
		dataMap.put("91", "0"); 
		
		long s = System.currentTimeMillis();
		service.saveBridge(dataMap);
		String re = service.getResult("SELECT * FROM TH_VEHICLE_BRIDGE WHERE pid='"+uuid+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveRecorder() {
		String uuid = GeneratorPK.instance().getPKString();
		String vid = "242447";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_RECORDER WHERE pid='"+uuid+"'  "));
		Map<String, String> dataMap = new HashMap<String, String>();
		dataMap.put("uuid", uuid);
		dataMap.put("vid", vid); 
		dataMap.put("61", "0");
		dataMap.put("seq", "E001_13311331121"); 
		long s = System.currentTimeMillis();
		service.saveRecorder(dataMap);
		String re = service.getResult("SELECT * FROM TH_VEHICLE_RECORDER WHERE pid='"+uuid+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testUpdateLockVehicleStatus() {
		String vid = "246830";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TB_LOCK_VEHICLE_DETAIL WHERE VID='"+vid+"'  "));
		long s = System.currentTimeMillis();
		service.updateLockVehicleStatus(vid, "3");
		String re = service.getResult("SELECT * FROM TB_LOCK_VEHICLE_DETAIL WHERE VID='"+vid+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testUpdateControlCommand() {
		String seq = "156998_1376631222_4";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_COMMAND WHERE co_seq='"+seq+"'  "));
		Map<String, String> dataMap = new HashMap<String, String>();
		dataMap.put("RET", "0");// 发送状态-1等待回应 0:成功 3:设备不支持此功能 4:设备不在线 5:超时
		dataMap.put("content", "AAAAAAAAAAAAAA"); 
		dataMap.put("seq", "156998_1376631222_4");
		
		long s = System.currentTimeMillis();
		service.updateControlCommand(dataMap);
		String re = service.getResult("SELECT * FROM TH_VEHICLE_COMMAND WHERE co_seq='"+seq+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test 
	public void testTerminalParamUpdate() {
		String tid = "11127";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TB_TERMINAL_PARAM WHERE tid='"+tid+"'  "));
		dataMap.put("tid", tid); 
		
		long s = System.currentTimeMillis();
		service.terminalParamUpdate(dataMap); 
		String re = service.getResult("SELECT * FROM TB_TERMINAL_PARAM WHERE tid='"+tid+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveOrUpdateLockVehicleDetail() {
		String vid = "246830";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TB_LOCK_VEHICLE_DETAIL WHERE vid='"+vid+"'  "));
//		 value			锁车控制|锁车类型|发动机最高转速|自锁预定时间|预警提醒锁车时间|车辆状态
//		 *  锁车状态(0:未锁,1:已锁,2待锁; 3:锁车装置异常或被拆除 ;
		
		long s = System.currentTimeMillis();
		service.saveOrUpdateLockVehicleDetail(vid, "01|1|1000|20140304152111|20140304152111|0", "0");
		String re = service.getResult("SELECT * FROM TB_LOCK_VEHICLE_DETAIL WHERE vid='"+vid+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
		
	}

	@Test
	public void testUpdateUnlockCode() {
		String vid = "246830";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TB_LOCK_VEHICLE_DETAIL WHERE vid='"+vid+"'  "));
		long s = System.currentTimeMillis();
		service.updateUnlockCode(vid, "123456", "0"); 
		String re = service.getResult("SELECT * FROM TB_LOCK_VEHICLE_DETAIL WHERE vid='"+vid+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testUpdateVehicleDispatchMsg() {
		String seq = "0_0";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_DISPATCH_MSG WHERE seq='"+seq+"'  "));
		Map<String, String> dataMap = new HashMap<String, String>();
		dataMap.put("RET", "5");// 发送状态-1等待回应 0:成功 3:设备不支持此功能 4:设备不在线 5:超时
		dataMap.put("seq", seq);
		
		long s = System.currentTimeMillis();
		
		service.updateVehicleDispatchMsg(dataMap); 
		String re = service.getResult("SELECT * FROM TH_VEHICLE_DISPATCH_MSG WHERE seq='"+seq+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}
//TODO
	@Test
	public void testUpdateEngBugDispose() {
		String seq = "156998_1376900415_29";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_ENG_BUG_DISPOSE WHERE co_seq='"+seq+"'  "));
		
		long s = System.currentTimeMillis();
		
		service.updateEngBugDispose(seq);
		String re = service.getResult("SELECT * FROM TH_ENG_BUG_DISPOSE WHERE co_seq='"+seq+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testInitAllVehilceCache() {
		long s = System.currentTimeMillis();
		service.initAllVehilceCache();
		System.out.println("测试结束耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testUpdate3GPhotoVehicleInfo() {
		long s = System.currentTimeMillis();
		service.update3GPhotoVehicleInfo();
		System.out.println("测试结束耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testUpdateVehilceCache() {
		long s = System.currentTimeMillis();
		service.updateVehilceCache(System.currentTimeMillis() - 864000000); 
		System.out.println("测试结束耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testClearUpdateVehicle() {
		long s = System.currentTimeMillis();
		service.clearUpdateVehicle(); 
		System.out.println("测试结束耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveQualityRecordInfo() {
		String vid = "123456";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TB_QUALITY_RECORD WHERE vid='"+vid+"'  "));
		Map<String, String> dataMap = new HashMap<String, String>();
		dataMap.put("vid", vid); 
		dataMap.put("commdr", "");
		dataMap.put("90", "AQEUAwUIVSRMWllURFRENjZFMTAwNTcyNwAE7x0CABgBAQIBAwEEAQUBBgIHAQgBCQEKAQsBDAENAQ4BDwEQARECEgITAhQBFQEWARcCGAI=");
		long s = System.currentTimeMillis();
		
		service.saveQualityRecordInfo(dataMap);
		String re = service.getResult("SELECT * FROM TB_QUALITY_RECORD WHERE vid='"+vid+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveSupervision() {
		String uuid = GeneratorPK.instance().getPKString();
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_VEHICLE_ALARMTODO WHERE pid='"+uuid+"'  "));
		Supervision su = new Supervision();
		su.setId(uuid);
		su.setPlate("测A12345");
		su.setPlateColor("02");
		su.setSource(1);
		su.setType(1);
		su.setAlarmUtc(System.currentTimeMillis());
		su.setSupervisionId(uuid);
		su.setSupervisionDeadline(System.currentTimeMillis());
		su.setLevel(1);
		su.setSupervisor("1");
		su.setTel("1111111111");
		su.setEmail("emali@email.com");
		su.setUtc(System.currentTimeMillis());
		su.setStatus(1);
		long s = System.currentTimeMillis();
		
		service.saveSupervision(su);
		String re = service.getResult("SELECT * FROM TH_VEHICLE_ALARMTODO WHERE pid='"+uuid+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSavePlatformMessage() {
		String uuid = GeneratorPK.instance().getPKString();
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_PLAT_INFOS WHERE pid='"+uuid+"'  "));
		PlatformMessage platformMessage = new PlatformMessage();
		platformMessage.setId(uuid);
		platformMessage.setContent("测试内容");
		platformMessage.setMessageId(uuid);
		platformMessage.setOperatingLicense("AAAA");
		platformMessage.setOperatingType(1);
		platformMessage.setUtc(System.currentTimeMillis());
		platformMessage.setOperateType(1);
		platformMessage.setAreaId("123456");
		platformMessage.setSeq("111_111_111");
		long s = System.currentTimeMillis();
		
		service.savePlatformMessage(platformMessage);
		String re = service.getResult("SELECT * FROM TH_PLAT_INFOS WHERE pid='"+uuid+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSavePlatformOnline() {
		String uuid = GeneratorPK.instance().getPKString();
//		uuid = "a7555a6e0fd342cea8c54f711d1ea22f";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_LINK_STATUS WHERE pid='"+uuid+"'  "));
		LinkStatus ls = new LinkStatus();
		ls.setId(uuid);
		ls.setAreaId("123456");
		ls.setLikeType(1);
		ls.setOnlineUtc(System.currentTimeMillis());
		ls.setUtc(System.currentTimeMillis());
		ls.setAccessCode("123456");
		ls.setChannelCode("4");;
		long s = System.currentTimeMillis();
		
		service.savePlatformOnline(ls);
		String re = service.getResult("SELECT * FROM TH_LINK_STATUS WHERE pid='"+uuid+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSavePlatformOffline() {
		String uuid = GeneratorPK.instance().getPKString();
		uuid = "8573196796274818581";
		System.out.println("插入前:\r\n"+service.getResult("SELECT * FROM TH_LINK_STATUS WHERE pid='"+uuid+"'  "));
		LinkStatus ls = new LinkStatus();
		ls.setId(uuid);
		ls.setOfflineUtc(System.currentTimeMillis()); 
		long s = System.currentTimeMillis();
		
		service.savePlatformOffline(ls);
		String re = service.getResult("SELECT * FROM TH_LINK_STATUS WHERE pid='"+uuid+"'  ");
		System.out.println("测试结果:\r\n"+re+"，耗时:"+ (System.currentTimeMillis() - s)); 
	}

	@Test
	public void testSaveOrUpdateTerminalVersion() {
		String vid = "123479";
		System.out.println("插入前:\r\n" + service.getResult("SELECT COUNT(*) FROM TH_TERMINAL_VERSION_RECORD "));
		List<TerminalVersion> list = new ArrayList<TerminalVersion>();
		int batchSize = 2;
		for (int i = 1; i < 2; i++) {
			TerminalVersion terminalVersion = new TerminalVersion();
			String uuid = GeneratorPK.instance().getPKString();
			terminalVersion.setUuid(uuid);// 主键
			terminalVersion.setVid(vid + i);// 车辆编号
			terminalVersion.setVinCode("LFZBGCNJ2DAS01414");//VIN
			terminalVersion.setPlate("测A12345");//车牌号
			terminalVersion.setPlateColor("02");//车牌颜色(不以ASCII码表表示数字的方式表示车牌颜色，统一按照JT/T415-2006定义标准定义车牌颜色，0x00—未上牌，0x01—蓝色，0x02—黄色，0x03—黑色，0x04—白色，0x09—其它)
			terminalVersion.setPhoneNumber("12345678900");//手机号
			terminalVersion.setIccid("123456");//SIM卡ICCID
			terminalVersion.settMac("123456");//终端号（ID）
			terminalVersion.settProtocolVersion("123456");//终端协议版本号
			terminalVersion.setTerminalHardVersion("FIRMWARE");//终端硬件版本号
			terminalVersion.setTerminalFirmwareVersion("FIRMWARE");//终端固件版本号
			terminalVersion.setLcdHardVersion("123456");//显示屏硬件版本号
			terminalVersion.setLcdFirmwareVersion("123456");//显示屏固件版本号
			terminalVersion.setDvrHardVersion("123456");//硬盘录像机硬件版本号
			terminalVersion.setDvrFirmwareVersion("123456");//硬盘录像机固件版本号
			terminalVersion.setRfCardHardVersion("123456");//射频读卡器硬件版本号
			terminalVersion.setRfCardFirmwareVersion("123456");//射频读卡器固件版本号
			terminalVersion.setSysUtc(System.currentTimeMillis()); // 时间
//			terminalVersion.settId(GeneratorPK.instance().getPKString());
			list.add(terminalVersion);
		}
		long s = System.currentTimeMillis();
		service.saveOrUpdateTerminalVersion(list, batchSize);
		String re = service.getResult("SELECT COUNT(*) FROM TH_TERMINAL_VERSION_RECORD ");
		System.out.println("测试结果:\r\n" + re + "，耗时:" + (System.currentTimeMillis() - s));
	}
	@Test
	public void testSaveOilInfo(){
		OilInfo info = new OilInfo();
		info.setVid("12345");
		info.setGap(0);
		info.setOilCalibration(0);
		info.setRefuelThreshold(0);
		info.setSeq("111_11");
		info.setStealThreshold(0);
		service.saveOilInfo(info);
	} 
}
