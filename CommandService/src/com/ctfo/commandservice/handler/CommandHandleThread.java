/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.service BlindFileHandleThread.java	</li><br>
 * <li>时        间：2013-9-9  下午2:37:20	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.handler;

import java.sql.SQLException;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.commandservice.dao.RedisConnectionPool;
import com.ctfo.commandservice.model.IccId;
import com.ctfo.commandservice.model.LinkStatus;
import com.ctfo.commandservice.model.OracleProperties;
import com.ctfo.commandservice.model.PlatformMessage;
import com.ctfo.commandservice.model.Supervision;
import com.ctfo.commandservice.model.TerminalVersion;
import com.ctfo.commandservice.model.Warning;
import com.ctfo.commandservice.service.OracleJdbcService;
import com.ctfo.commandservice.service.RedisService;
import com.ctfo.commandservice.util.Base64_URl;
import com.ctfo.commandservice.util.CommandCache;
import com.ctfo.commandservice.util.Constant;
import com.ctfo.commandservice.util.DateTools;
import com.ctfo.generator.pk.GeneratorPK;



/*****************************************
 * <li>描        述：指令处理线程		
 * 
 *****************************************/
public class CommandHandleThread  extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(CommandHandleThread.class);

	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
	private int threadId;
	/** 计数器	  */
	private int index;
	/** 上次时间	  */
	private long lastTime = System.currentTimeMillis();
	/** oracle数据接口	  */
	private OracleJdbcService oracleJdbcService;
	/** redis数据接口	  */
	private RedisService redisService; 
	/** 终端版本存储线程	  */
	private TerminalVersionStorageThread terminalVersionStorageThread;
	/** ICC存储线程	  */
	private IccIdUpdateThread iccIdUpdateThread;
	/** 行驶记录仪处理线程	  */
	private TravellingRecorderParseThread travellingRecorderParseThread;
	/** 驾驶员消息处理线程	  */
	private DriverProcess driverProcess; 
	/** 油量处理线程	  */
	private OilProcess oilProcess;
//	/** 自定义指令处理线程	  */
//	private CustomCommandProcess customCommandProcess;
	
	public CommandHandleThread(int threadId, OracleProperties oracleProperties,TerminalVersionStorageThread terminalVersionStorageThread, IccIdUpdateThread iccIdUpdateThread,TravellingRecorderParseThread travellingRecorderParseThread) throws Exception{
		super("CommandHandleThread-" + threadId);
		this.threadId = threadId;
		this.oracleJdbcService = new OracleJdbcService(oracleProperties);
		this.redisService = new RedisService();
		this.terminalVersionStorageThread = terminalVersionStorageThread;
		this.iccIdUpdateThread = iccIdUpdateThread;
		this.travellingRecorderParseThread = travellingRecorderParseThread;
		this.oilProcess = new OilProcess(oracleProperties, threadId);
		this.oilProcess.start();
	}
	
	
	public void putDataMap(Map<String, String> dataMap) {
		try {
			dataQueue.put(dataMap);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}
	
	public int getQueueSize() {
		return dataQueue.size();
	}
	@Override
	public void run() {
		logger.info("监控指令线程" + threadId + "启动");
		while (true) {
			Map<String, String> app = null;
			try {
				app = dataQueue.take();
				dealCommand(app);
				long currentTime = System.currentTimeMillis();
				if ((currentTime - lastTime) > 3000) {
					lastTime = currentTime;
					logger.info("command---:{}, 排队:[{}] , 3秒处理:[{}]条", threadId, getQueueSize(), index); 
					index = 0;
				}
				index++;
			} catch (Exception e) {
				logger.error("CommandManagerKcptThread 指令线程处理数据出错:" + e.getMessage() + ",commands:"  , e);
			}
		}
	}

	/***
	 * 业务处理
	 * 
	 * @param app
	 * @throws SQLException
	 */
	private void dealCommand(Map<String, String> app) throws SQLException {
		long start = System.currentTimeMillis();
		String head = app.get(Constant.HEAD);
		String mtype = app.get(Constant.MTYPE);
		String subtype = app.get("TYPE");
		String channel = app.get(Constant.CHANNEL);
		if ("CAITS".equals(head)) {
			if ("4".equals(channel)) {// 监管指令  & 终端控制指令
				if ("D_SNDM".equals(mtype) || "D_CTLM".equals(mtype)) {// 发送短信
					oracleJdbcService.saveControlCommand(app);
				}
				if("L_PROV".equals(mtype)){ // 报警相关
					if("D_WARN".equals(subtype)){ 
						saveSupervision(app);
					}
					if("D_MESG".equals(subtype)){ // 交换报警
						saveWarning(app);
					}
				}
				if("L_PLAT".equals(mtype)){ // 平台数据
					if("D_PLAT".equals(subtype)){
						savePlatformMessage(app, channel);
					}
					if("U_UCNT".equals(subtype)){ // 平台连接状态
						savePlatformOnline(app);
					}
				}
			} else if ("U_REPT".equals(mtype)) {// 终端主动上传指令类
				if ("3".equals(subtype)) { // 多媒体上传
					oracleJdbcService.saveMultMedia(app);
					redisService.saveRedisMultMedia(app, DateTools.getIntvalTime());
				} else if ("4".equals(subtype)) {// 车机主动上报普通短信
					oracleJdbcService.saveVehicleDispatchMsg(app);
				} else if ("53".equals(subtype)) {// 终端版本信息上传      //TODO
					TerminalVersion terminalVersion = parseTerminalVersion(app);
					if(terminalVersion != null){
						terminalVersionStorageThread.putTerminalVersion(terminalVersion.getVid(), terminalVersion);	// 存储版本信息
						String iccId = terminalVersion.getIccid();
						String phoneNumber = terminalVersion.getPhoneNumber();
						if(phoneNumber != null && phoneNumber.length() > 7 && iccId != null && iccId.length() > 1){
							IccId iccIdInfo = new IccId();
							iccIdInfo.setIccId(iccId);
							iccIdInfo.setPhoneNumber(phoneNumber);
							iccIdUpdateThread.putIccId(iccIdInfo);	// 更新IccId信息
						}
					}
				} else if ("39".equals(subtype)) { // 多媒体事件
					oracleJdbcService.saveMultimediaEvent(app);
					redisService.saveMultimediaEvent(app.get(Constant.N127),app.get(Constant.N126),app.get(Constant.N123),app.get(Constant.VID),DateTools.getIntvalTime());
				} else if ("35".equals(subtype)) { // 电子路单
					oracleJdbcService.saveEticket(app);
				} else if ("36".equals(subtype)) { // 终端注册
					oracleJdbcService.saveTernimalRegisterInfo(app);
				} else if ("37".equals(subtype)) { // 终端注销
					oracleJdbcService.saveVehicleLogOff(app);  
					oracleJdbcService.updateVehicleLogOff(app);
				} else if ("8".equals(subtype)) { // 存储驾驶员身份信息
					driverProcess.putData(app);
				} else if ("31".equals(subtype)) { // 事件ID
					oracleJdbcService.saveDispatch(app);
				} else if ("32".equals(subtype)) { // 提问应答
					oracleJdbcService.updateQuerstionAnswer(app);
				} else if ("9".equals(subtype)) { // 数据上行透传
					if (null != app.get("91") && "130".equals(app.get("91"))) {
						// 防偷漏油
						oracleJdbcService.saveBridge(app);
						oilProcess.putData(app);
					}
					if (null != app.get("91") && "129".equals(app.get("91"))) {
						if (app.get("90") != null) {
							// 远程诊断
							oracleJdbcService.saveEngineFaultInfo(app);
							oracleJdbcService.saveBridge(app);
						}
					}
					if (null != app.get("91") && "133".equals(app.get("91"))) {
						if (app.get("90") != null) {
							// 质检单
							oracleJdbcService.saveQualityRecordInfo(app);
							oracleJdbcService.saveBridge(app);
						}
					}
				} else if ("2".equals(subtype)) { // 记录仪数据
					oracleJdbcService.saveRecorder(app);
					travellingRecorderParseThread.addPacket(app);
				} else if ("40".equals(subtype)) { // 照片进度 //
					app = redisService.updatePicProgress(app, DateTools.getIntvalTime());
					oracleJdbcService.updateControlCommand(app);
				} else if ("62".equals(subtype)) {  //更新锁车状态  
					if(StringUtils.isNotBlank(app.get("571"))){
						oracleJdbcService.updateLockVehicleStatus(app.get(Constant.VID),app.get("571"));
					}
				}else if ("65".equals(subtype)) { //特征系数结果上报
					oracleJdbcService.saveVehicleSpeedCheck(app);
				}else if ("10".equals(subtype)) { //自定义指令上传
					if(!CustomCommandProcess.offer(app)){
						logger.error("自定义指令存储队列已满:{}" , app.get(Constant.COMMAND)); 
					}
				}
			} else if ("D_ADDT".equals(mtype)) {// 订阅指令
				oracleJdbcService.saveControlCommand(app);
			} else if ("D_DELT".equals(mtype)) {// 退订指令
				oracleJdbcService.saveControlCommand(app);
			// 数据透传设置指令 -- 远程终端设置
			} else if ("D_SETP".equals(mtype)) { 
				if ("9".equals(subtype)) {
					if(app.get(Constant.SEQ) != null  &&  app.get(Constant.MACID) != null){
						CommandCache.set(app.get(Constant.SEQ), app.get(Constant.MACID), 60);
					}
				}
				if ("10".equals(subtype)) { // 自定义指令下发 - 更新自定义指令状态
					if(!CustomCommandProcess.offer(app)){
						logger.error("自定义指令存储队列已满:{}" , app.get(Constant.COMMAND)); 
					}
				}
			} else if ("D_CTLM".equals(mtype)) {// 终端控制指令
				if(subtype.equals("27") ){
					CommandCache.setControlCache(app.get(Constant.SEQ), "27");
				}
				if(subtype.equals("26") ){
					CommandCache.setControlCache(app.get(Constant.SEQ), "26");
				}
			}
		} else {// 回应指令
			if ("D_SETP".equals(mtype)) {// 终端参数设置指令
				// 如果是设置成功指令 & 并在远程终端缓存存在的数据 就更新远程诊断中清码状态
				if ("0".equals(app.get("RET"))) {
					String seq = app.get(Constant.SEQ);
					if (CommandCache.get(seq) != null) {
						oracleJdbcService.updateEngBugDispose(seq);
//						如果更新成功就删除序列对应对象
						CommandCache.remove(seq);
					}
				}
				oracleJdbcService.updateControlCommand(app);
				redisService.updateCommand(app.get("RET"), app.get(Constant.SEQ), app.get(Constant.VID) ,DateTools.getIntvalTime());
			} else if ("D_GETP".equals(mtype)) {// 终端参数读取指令
				oracleJdbcService.updateControlCommand(app);
				oracleJdbcService.terminalParamUpdate(app);
				if(subtype.equals("1")){ 
					TerminalVersion tv = parseTerminalGetVersion(app);
					if(tv != null){
						terminalVersionStorageThread.putTerminalVersion(tv.getVid(), tv);
						String iccId = tv.getIccid();
						String phoneNumber = tv.getPhoneNumber();
						if(phoneNumber != null && phoneNumber.length() > 7 && iccId != null && iccId.length() > 1){
							IccId iccIdInfo = new IccId();
							iccIdInfo.setIccId(iccId);
							iccIdInfo.setPhoneNumber(phoneNumber);
							iccIdUpdateThread.putIccId(iccIdInfo);	// 更新IccId信息
						}
					}
				}
				redisService.updateCommand(app.get("RET"), app.get(Constant.SEQ), app.get(Constant.VID) ,DateTools.getIntvalTime());
			} else if ("D_CTLM".equals(mtype) || "U_REPT".equals(mtype)) {// 终端控制指令
				if (!"U_REPT".equals(mtype)) {
					oracleJdbcService.updateControlCommand(app);
				}
				String result = app.get("RET");
				redisService.updateCommand(result, app.get(Constant.SEQ), app.get(Constant.VID) ,DateTools.getIntvalTime());
//				如果是查询远程锁车参数，就插入或者更新TB_LOCK_VEHICLE_DETAIL远程锁车信息表
				String seq = app.get(Constant.SEQ);
				if(CommandCache.containsControlKey(seq) && CommandCache.getControlCache(seq).equals("27")){
//					插入或者修改的参数1.VID 2.锁车类型 3。发动机最高转速 4。预锁车时间 5.车辆状态
					oracleJdbcService.saveOrUpdateLockVehicleDetail(app.get(Constant.VID),app.get("VALUE"), result); 
					CommandCache.removeControlCache(seq);
				}
//				如果是远程锁车应答，就更新解锁码 (（解锁 -- 应答没有解锁码,不出来）)
				if(CommandCache.containsControlKey(seq) && CommandCache.getControlCache(seq).equals("26")){
					oracleJdbcService.updateUnlockCode(app.get(Constant.VID), app.get("VALUE"), result); 
					CommandCache.removeControlCache(seq);
				}
			} else if ("D_SNDM".equals(mtype)) {// 发送短信
				oracleJdbcService.updateVehicleDispatchMsg(app); // 更新调度信息表
				oracleJdbcService.updateControlCommand(app); // 下行指令历史表
				redisService.updateCommand(app.get("RET"), app.get(Constant.SEQ), app.get(Constant.VID) ,DateTools.getIntvalTime());
			} else if ("D_REQD".equals(mtype)) {// 请求终端数据指令
				if ("1".equals(subtype)) {
//					oracleJdbcService.saveMediaIdx(app); // 去除无用数据 多媒体检索
				} else {
					oracleJdbcService.updateControlCommand(app);
					redisService.updateCommand(app.get("RET"), app.get(Constant.SEQ), app.get(Constant.VID) ,DateTools.getIntvalTime());
				}
			} else if ("D_ADDT".equals(mtype)) {// 订阅指令
				oracleJdbcService.updateControlCommand(app);
				redisService.updateCommand(app.get("RET"), app.get(Constant.SEQ), app.get(Constant.VID) ,DateTools.getIntvalTime());
			} else if ("D_DELT".equals(mtype)) {// 退订指令
				oracleJdbcService.updateControlCommand(app);
				redisService.updateCommand(app.get("RET"), app.get(Constant.SEQ), app.get(Constant.VID) ,DateTools.getIntvalTime());
			} else if ("D_CALL".equals(mtype)) {// 点名指令
				oracleJdbcService.updateControlCommand(app);
				redisService.updateCommand(app.get("RET"), app.get(Constant.SEQ), app.get(Constant.VID) ,DateTools.getIntvalTime());
			}
		}
		logger.debug("threadId:" + threadId+", MTYPE:" + mtype + ", TYPE:" + subtype + ", 耗时:" + (System.currentTimeMillis() - start) + "ms"); 
	}
	



	/**
	 * 存储平台连接状态
	 * @param app
	 */
	private void savePlatformOnline(Map<String, String> app) {
		String[] array = StringUtils.splitPreserveAllTokens(app.get("FLAG"), "|", 2);
		if(array != null && array.length == 2){
			LinkStatus ls = new LinkStatus();
			String accessAndChannelStr = app.get(Constant.MACID);
			String[] accessAndChannel = StringUtils.splitPreserveAllTokens(accessAndChannelStr, "_", 2);
			if(accessAndChannel.length == 2){
				ls.setAccessCode(accessAndChannel[0]);
				ls.setChannelCode(accessAndChannel[1]);
			} else {
				logger.error("存储平台连接状态,接入码异常:" + app.get(Constant.COMMAND));
				return;
			}
			Jedis jedis = null;
			try {
				jedis = RedisConnectionPool.getJedisConnection();
				jedis.select(7);
				String pid = jedis.hget("H.KCPT.PLATFROM.ONOFF", accessAndChannelStr);
				if(array[1].equals("1")){
					if(pid != null){
						ls.setId(pid);
						ls.setOfflineUtc(System.currentTimeMillis());
						oracleJdbcService.savePlatformOffline(ls);
						jedis.hdel("H.KCPT.PLATFROM.ONOFF", accessAndChannelStr);
					}
					ls.setId(app.get(Constant.UUID));
					ls.setAreaId("");
					if(array[0].equals("UP")){
						ls.setLikeType(0);
					} else if(array[0].equals("DOWN")){
						ls.setLikeType(1);
					} else {
						ls.setLikeType(-1);
					}
					ls.setUtc(System.currentTimeMillis());
					ls.setOnlineUtc(System.currentTimeMillis());
					oracleJdbcService.savePlatformOnline(ls);
					jedis.hset("H.KCPT.PLATFROM.ONOFF", accessAndChannelStr, ls.getId());
				} else if(array[1].equals("0")){
					if(pid != null){
						ls.setId(pid);
						ls.setOfflineUtc(System.currentTimeMillis());
						oracleJdbcService.savePlatformOffline(ls);
						jedis.hdel("H.KCPT.PLATFROM.ONOFF", accessAndChannelStr);
					}
				} else {
					logger.error("存储平台处理连接状态异常:" + app.get(Constant.COMMAND));  
				}
			} catch (Exception e) {
				RedisConnectionPool.returnBrokenResource(jedis);
				logger.error("存储平台处理连接状态异常:" + e.getMessage(), e);  
			} finally {
				if(jedis != null){
					RedisConnectionPool.returnJedisConnection(jedis);
				}
			}
		}
	}

	/**
	 * 存储平台消息
	 * @param app
	 * @param channel
	 */
	private void savePlatformMessage(Map<String, String> app, String channel) {
		int operateType = -1;
		String chagang = app.get("PLATQUERY");
		String pakete = app.get("PLATMSG");
		String platformInfo = null;
		String[] chagangArray = null;
		if(chagang != null) {
			platformInfo = chagang; //平台查岗 
			operateType = 0;
		} else if(pakete != null){
			platformInfo = pakete; //平台报文
			operateType = 1;
		}
		chagangArray = StringUtils.splitPreserveAllTokens(platformInfo, "|", 4);
		if(chagangArray != null && chagangArray.length == 4){ //平台查岗 
			PlatformMessage platformMessage= new PlatformMessage();
			
			platformMessage.setId(app.get(Constant.UUID));
			platformMessage.setContent(Base64_URl.decodeString(chagangArray[3]));
			platformMessage.setMessageId(chagangArray[2]);
			platformMessage.setOperatingLicense(chagangArray[1]);
			if(StringUtils.isNumeric(chagangArray[0])){
				platformMessage.setOperatingType(Integer.parseInt(chagangArray[0]));
			} else {
				platformMessage.setOperatingType(-1);
			}
			platformMessage.setUtc(System.currentTimeMillis());
			String paltMac = app.get(Constant.MACID);
			String [] macid = StringUtils.splitPreserveAllTokens(paltMac,"_");
			if(macid != null && macid.length == 2){
				platformMessage.setAreaId(macid[1]);
			} else {
				platformMessage.setAreaId("");
			}
			platformMessage.setOperateType(operateType);
			platformMessage.setSeq(app.get(Constant.SEQ) + "#" + paltMac + "#" + channel); 
			platformMessage.setUtc(System.currentTimeMillis());
			oracleJdbcService.savePlatformMessage(platformMessage);
		}
	}

	/**
	 * 存储交换报警
	 * @param app
	 */
	private void saveWarning(Map<String, String> app) {
		String warnExg = app.get("WARNEXG");
		String[] array = StringUtils.splitPreserveAllTokens(warnExg, "|", 4);
		if(array != null && array.length == 4){
			Warning warn = new Warning();
			warn.setId(app.get(Constant.UUID));
			warn.setVid(app.get(Constant.VID));
			warn.setSource(array[0]);
			if(StringUtils.isNumeric(array[1])){
				warn.setType(Integer.parseInt(array[1]));
			} else {
				warn.setType(-1);
			}
			if(StringUtils.isNumeric(array[2])){
				warn.setWarnUtc(Long.parseLong(array[2]));
			} else {
				warn.setWarnUtc(-1);
			}
			warn.setDesc(Base64_URl.decodeString(array[3]));
			warn.setUtc(System.currentTimeMillis());
			oracleJdbcService.saveWarning(warn);
		}
	}

	/**
	 * 存储报警督办、预警
	 * @param app
	 */
	private void saveSupervision(Map<String, String> app) {
		String supervision = app.get("WARNTODO");// 报警督办 
		String warning = app.get("WARNTIPS");// 报警预警
		String[] array = StringUtils.splitPreserveAllTokens(supervision, "|", 9);
		Supervision su = new Supervision();
		if(array != null && array.length == 9){// 报警督办
			su.setId(app.get(Constant.UUID));
			su.setPlate(app.get(Constant.VEHICLENO));
			su.setPlateColor(app.get(Constant.PLATECOLORID));
			if(StringUtils.isNumeric(array[0])){
				su.setSource(Integer.parseInt(array[0]));
			} else {
				su.setSource(-1);
			}
			if(StringUtils.isNumeric(array[1])){
				su.setType(Integer.parseInt(array[1]));
			} else {
				su.setType(-1);
			}
			if(StringUtils.isNumeric(array[2])){
				su.setAlarmUtc(Long.parseLong(array[2]));
			} else {
				su.setAlarmUtc(-1);
			}
			su.setSupervisionId(array[3]);
			if(StringUtils.isNumeric(array[4])){
				su.setSupervisionDeadline(Long.parseLong(array[4]));
			} else {
				su.setSupervisionDeadline(-1);
			}
			if(StringUtils.isNumeric(array[5])){
				su.setLevel(Integer.parseInt(array[5]));
			} else {
				su.setLevel(-1);
			}
			su.setSupervisor(Base64_URl.decodeString(array[6]));
			su.setTel(array[7]);
			su.setEmail(array[8]);
			su.setUtc(System.currentTimeMillis());
			oracleJdbcService.saveSupervision(su);
		}
		String[] warningArray = StringUtils.splitPreserveAllTokens(warning, "|", 4);
		if(warningArray != null && warningArray.length == 4){
			Warning warn = new Warning();
			warn.setId(app.get(Constant.UUID));
			warn.setVid(app.get(Constant.VID));
			warn.setSource(warningArray[0]);
			if(StringUtils.isNumeric(warningArray[1])){
				warn.setType(Integer.parseInt(warningArray[1]));
			} else {
				warn.setType(-1);
			}
			if(StringUtils.isNumeric(warningArray[2])){
				warn.setWarnUtc(Long.parseLong(warningArray[2]));
			} else {
				warn.setWarnUtc(-1);
			}
			warn.setDesc(Base64_URl.decodeString(warningArray[3]));
			warn.setUtc(System.currentTimeMillis());
			oracleJdbcService.saveWarning(warn);
		}
		
	}


	/**
	 * 解析终端版本信息
	 * @param app
	 * @return
	 */
	private TerminalVersion parseTerminalGetVersion(Map<String, String> app) {
		String terminalHardVersion = null;// 终端硬件版本号
		String terminalFirmwareVersion = null;// 终端固件版本号
		String iccid = null;// SIM卡ICCID
		String tMac = null;// 终端号（ID）
		TerminalVersion terminalVersion = null;
		try {
			terminalVersion = new TerminalVersion();
			tMac = app.get("20003");
			if (tMac == null) {
				tMac = "";
			}
			iccid = app.get("20004");
			if (iccid == null) {
				iccid = "";
			}
			terminalHardVersion = app.get("20005");
			if (terminalHardVersion == null) {
				terminalHardVersion = "";
			}
			terminalFirmwareVersion = app.get("20006");
			if (terminalFirmwareVersion == null) {
				terminalFirmwareVersion = "";
			}
			terminalVersion.setTerminalHardVersion(terminalHardVersion); // 终端硬件版本号
			terminalVersion.setTerminalFirmwareVersion(terminalFirmwareVersion); // 终端固件版本号
			terminalVersion.setIccid(iccid); // SIM卡ICCID
			terminalVersion.setLcdHardVersion("");// 显示屏硬件版本号
			terminalVersion.setLcdFirmwareVersion("");// 显示屏固件版本号
			terminalVersion.setDvrHardVersion("");// 硬盘录像机硬件版本号
			terminalVersion.setDvrFirmwareVersion("");// 硬盘录像机固件版本号
			terminalVersion.setRfCardHardVersion("");// 射频读卡器硬件版本号
			terminalVersion.setRfCardFirmwareVersion(""); // 射频读卡器固件版本号
			terminalVersion.setPlate(app.get(Constant.VEHICLENO));// 车牌号
			terminalVersion.setPlateColor(app.get(Constant.PLATECOLORID));// 车牌颜色(不以ASCII码表表示数字的方式表示车牌颜色，统一按照JT/T415-2006定义标准定义车牌颜色，0x00—未上牌，0x01—蓝色，0x02—黄色，0x03—黑色，0x04—白色，0x09—其它)
			terminalVersion.setVinCode(app.get(Constant.VIN_CODE));// VIN
			terminalVersion.settMac(tMac);// 终端号（ID）
			terminalVersion.settProtocolVersion("");// 终端协议版本号
			terminalVersion.setVid(app.get(Constant.VID));
			terminalVersion.setPhoneNumber(app.get(Constant.COMMDR));
			terminalVersion.settId(app.get(Constant.TID));
			terminalVersion.setSysUtc(System.currentTimeMillis());
			terminalVersion.setUuid(GeneratorPK.instance().getPKString());
			return terminalVersion;
		} catch (Exception ex) {
			logger.error("解析终端版本信息异常:" + ex.getMessage(), ex);
		}
		return null;
	}

	/**
	 * 解析终端版本信息
	 * @param app
	 * @return
	 */
	private TerminalVersion parseTerminalVersion(Map<String, String> app) {
		TerminalVersion terminalVersion = null;
		try {
			terminalVersion = new TerminalVersion();
			String terminalInfo = app.get("518");
			String[] version = StringUtils.splitPreserveAllTokens(terminalInfo, "|"); 
			if (version != null && version.length == 14){
				terminalVersion.setTerminalHardVersion(version[0]);		//终端硬件版本号
				terminalVersion.setTerminalFirmwareVersion(version[1]);	//终端固件版本号
				terminalVersion.setIccid(version[2]);					//SIM卡ICCID
				terminalVersion.setLcdHardVersion(version[3]);//显示屏硬件版本号
				terminalVersion.setLcdFirmwareVersion(version[4]);//显示屏固件版本号
				terminalVersion.setDvrHardVersion(version[5]);//硬盘录像机硬件版本号
				terminalVersion.setDvrFirmwareVersion(version[6]);//硬盘录像机固件版本号
				terminalVersion.setRfCardHardVersion(version[7]);//射频读卡器硬件版本号
				terminalVersion.setRfCardFirmwareVersion(version[8]); //射频读卡器固件版本号
				terminalVersion.setPlate(version[9]);//车牌号
				terminalVersion.setPlateColor(version[10]);//车牌颜色(不以ASCII码表表示数字的方式表示车牌颜色，统一按照JT/T415-2006定义标准定义车牌颜色，0x00—未上牌，0x01—蓝色，0x02—黄色，0x03—黑色，0x04—白色，0x09—其它)
				terminalVersion.setVinCode(version[11]);//VIN
				terminalVersion.settMac(version[12]);//终端号（ID）
				terminalVersion.settProtocolVersion(version[13]);//终端协议版本号
				terminalVersion.setVid(app.get(Constant.VID)); 
				terminalVersion.setPhoneNumber(app.get(Constant.COMMDR)); 
				terminalVersion.settId(app.get(Constant.TID)); 
				terminalVersion.setSysUtc(System.currentTimeMillis()); 
				terminalVersion.setUuid(GeneratorPK.instance().getPKString());  
				return terminalVersion;
			}
		} catch (Exception ex) {
			logger.error("解析终端版本信息异常:" + ex.getMessage(), ex);
		} 
		return null;
	}


	/**
	 * 获得驾驶员消息处理线程的值
	 * @return the driverProcess 驾驶员消息处理线程  
	 */
	public DriverProcess getDriverProcess() {
		return driverProcess;
	}
	/**
	 * 设置驾驶员消息处理线程的值
	 * @param driverProcess 驾驶员消息处理线程  
	 */
	public void setDriverProcess(DriverProcess driverProcess) {
		this.driverProcess = driverProcess;
	}
//	/**
//	 * @return the 自定义指令处理线程
//	 */
//	public CustomCommandProcess getCustomCommandProcess() {
//		return customCommandProcess;
//	}
//	/**
//	 * 设置自定义指令处理线程的值
//	 * @param customCommandProcess 自定义指令处理线程  
//	 */
//	public void setCustomCommandProcess(CustomCommandProcess customCommandProcess) {
//		this.customCommandProcess = customCommandProcess;
//	}
	
}
