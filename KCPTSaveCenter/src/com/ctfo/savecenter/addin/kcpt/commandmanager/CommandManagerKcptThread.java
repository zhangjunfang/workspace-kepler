package com.ctfo.savecenter.addin.kcpt.commandmanager;

import java.sql.SQLException;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.dao.CommandManagerKcptDBAdapter;
import com.ctfo.savecenter.dao.CommandManagerRedisDBAdapter;
import com.ctfo.savecenter.dao.SaveVehicleLineStatus;
import com.ctfo.savecenter.util.CommandCache;
import com.lingtu.xmlconf.XmlConf;

public class CommandManagerKcptThread extends Thread {

	private static final Logger logger = LoggerFactory.getLogger(CommandManagerKcptThread.class);

	private ArrayBlockingQueue<Map<String, String>> vPacket = new ArrayBlockingQueue<Map<String, String>>(100000);
	// 计数器
	private int index = 0;
	// 线程id
	private int nId = 0;

	// ORACLE
	private CommandManagerKcptDBAdapter oracleDba;

	private CommandManagerRedisDBAdapter redisDba;
	private XmlConf config = null;

	private String nodeName = null; // root节点
	// 批量数据库提交间隔时间(单位:ms)
	private static long commitTime = 0;
	// 提交频率（秒）
	private int commit = 10;
	// 最后提交时间
	private long lastCommitTime = System.currentTimeMillis();

	public CommandManagerKcptThread(CommandManagerKcptDBAdapter oracleDba, CommandManagerRedisDBAdapter redisDba, int id, XmlConf config, String nodeName) throws Exception {
		this.nId = id;
		this.config = config;
		this.nodeName = nodeName;
		this.oracleDba = oracleDba;
		this.redisDba = redisDba;

		this.oracleDba.createStatement();
		// 初始化指令对媒体数据库操作类
		// oracleDba = new CommandManagerKcptDBAdapter();
		// oracleDba.initDBAdapter(config, nodeName);
		// redisDba = new CommandManagerRedisDBAdapter();

		SaveVehicleLineStatus.eloadistUrl = config.getStringValue(nodeName + "|eloaddistfileurl");
		SaveVehicleLineStatus.eventUrl = config.getStringValue(nodeName + "|eventfileurl");
		SaveVehicleLineStatus.oilUrl = config.getStringValue(nodeName + "|oilUrl");
		// 批量数据库提交间隔时间(单位:S)
		commit = config.getIntValue(nodeName + "|commitTime");
		commitTime = commit * 1000;

		// oracleDba.createStatement();
	}

	public void addPacket(Map<String, String> packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}

	String commands = "";
	/**  */
	@Override
	public void run() {
		logger.info("监控指令线程" + nId + "启动");

		while (CommandManagerKcptMainThread.isRunning) {
			Map<String, String> app = null;
			try {
				app = vPacket.take();
				commands = app.get(Constant.COMMAND);
				dealCommand(app);
			
				long currentTime = System.currentTimeMillis();
				if ((currentTime - lastCommitTime) > commitTime) {
					oracleDba.commit();
					lastCommitTime = currentTime;
					logger.warn("command---:" + nId + ",size:" + vPacket.size() + "," + commit + "process:" + index + "条");
					index = 0;
				}
				index++;
			} catch (Exception e) {
				logger.error("CommandManagerKcptThread 指令线程处理数据出错:" + e.getMessage() + ",commands:" + commands , e);
			}
		}// End while
	}

	/***
	 * 业务处理
	 * 
	 * @param app
	 * @throws SQLException
	 */
	private void dealCommand(Map<String, String> app) throws SQLException {
		String head = app.get(Constant.HEAD);
		String mtype = app.get(Constant.MTYPE);
		String subtype = app.get("TYPE");
		String channel = app.get(Constant.CHANNEL);
		if ("CAITS".equals(head)) {
			if ("4".equals(channel)) {// 监管指令
				if ("D_SNDM".equals(mtype)) {// 发送短信
					oracleDba.saveControlCommand(app);
					// mysqlDba.mysqlSaveControlCommand(app);
				} else if ("D_CTLM".equals(mtype)) {// 终端控制指令
					oracleDba.saveControlCommand(app);
					// mysqlDba.mysqlSaveControlCommand(app);
					
				}
			} else if ("U_REPT".equals(mtype)) {// 终端主动上传指令类
				if ("3".equals(subtype)) { // 多媒体上传
					oracleDba.saveMultMedia(app);
					oracleDba.saveRedisMultMedia(app);
				} else if ("4".equals(subtype)) {// 车机主动上报普通短信
					oracleDba.saveVehicleDispatchMsg(app);
				} else if ("52".equals(subtype)) { // 驾驶行为事件上传存储文件
					if (app.containsKey("516")) {
						SaveVehicleLineStatus.saveLineStatus(config, nodeName, app.get("516"), Long.parseLong(app.get(Constant.VID)));
					}
				} else if ("53".equals(subtype)) {// 终端版本信息上传
					oracleDba.updateTernimalVersion(app);
				} else if ("39".equals(subtype)) { // 多媒体事件
					oracleDba.saveMultimediaEvent(app);
				} else if ("35".equals(subtype)) { // 电子路单
					oracleDba.saveEticket(app);
				} else if ("36".equals(subtype)) { // 终端注册
					oracleDba.saveTernimalRegisterInfo(app);
				} else if ("37".equals(subtype)) { // 终端注销
					oracleDba.saveVehicleLogOff(app);
					oracleDba.updateVehicleLogOff(app);
				} else if ("38".equals(subtype)) { // 终端鉴权
					oracleDba.saveVehicleAKey(app);
				} else if ("8".equals(subtype)) { // 存储驾驶员身份信息
					oracleDba.saveDriverInfo(app);
					oracleDba.saveDriverLoginingInfo(app);
				} else if ("50".equals(subtype)) { // 发动机负荷率分布表数据
					if (app.get("513") != null) {
						if (app.get("513").equals("1") && app.get("514") != null) {
							SaveVehicleLineStatus.saveEloaddist(config, nodeName, app.get("514"), Long.parseLong(app.get(Constant.VID)));
						}
					}
				} else if ("31".equals(subtype)) { // 事件ID
					oracleDba.saveEventId(app);
				} else if ("32".equals(subtype)) { // 提问应答
					oracleDba.updateQuerstionAnswer(app);
				} else if ("9".equals(subtype)) { // 数据上行透传
					if (null != app.get("91") && "130".equals(app.get("91"))) {
						// 防偷漏油
						SaveVehicleLineStatus.saveOilList(config, app.get("90"), Long.parseLong(app.get(Constant.VID)), oracleDba);
					}
					if (null != app.get("91") && "129".equals(app.get("91"))) {
						if (app.get("90") != null) {
							// 远程诊断
							SaveVehicleLineStatus.saveEngineFaultInfo(config, app, oracleDba);
						}
					}
					if (null != app.get("91") && "133".equals(app.get("91"))) {
						if (app.get("90") != null) {
							// 质检单
							SaveVehicleLineStatus.saveQualityRecordInfo(config, app, oracleDba);
						}
					}
					oracleDba.saveBridge(app);
				} else if ("14".equals(subtype)) { // 数据压缩上传
					oracleDba.saveCompress(app);
				} else if ("2".equals(subtype)) { // 记录仪数据
					oracleDba.saveRecorder(app);
				} else if ("33".equals(subtype)) { // 信息点播/取消
					oracleDba.saveInfoplay(app);
				} else if ("40".equals(subtype)) { // 照片进度 //
					oracleDba.updatePicProgress(app);
				//锁车&解锁状态上报
				} else if ("62".equals(subtype)) { 
					//更新锁车状态  
					if(StringUtils.isNotBlank(app.get("571"))){
						oracleDba.updateLockVehicleStatus(app.get(Constant.VID),app.get("571"));
					}
				}

			} else if ("D_ADDT".equals(mtype)) {// 订阅指令
				oracleDba.saveControlCommand(app);
			} else if ("D_DELT".equals(mtype)) {// 退订指令
				oracleDba.saveControlCommand(app);
			} else if ("D_REQD".equals(mtype)) { // 远程锁车
				oracleDba.saveMediaIdx(app);
			// 数据透传设置指令 -- 远程终端设置
			} else if ("D_SETP".equals(mtype)) { 
				if ("9".equals(subtype)) {
					if(app.get(Constant.SEQ) != null  &&  app.get(Constant.MACID) != null){
						CommandCache.set(app.get(Constant.SEQ), app.get(Constant.MACID), 60);
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
						SaveVehicleLineStatus.updateEngBugDispose(seq, oracleDba);
//						如果更新成功就删除序列对应对象
						CommandCache.remove(seq);
					}
				}
				oracleDba.updateControlCommand(app);
				redisDba.updateCommand(app);
			} else if ("D_GETP".equals(mtype)) {// 终端参数读取指令
				oracleDba.updateControlCommand(app);
				oracleDba.terminalParam(app);
				redisDba.updateCommand(app);
			} else if ("D_CTLM".equals(mtype) || "U_REPT".equals(mtype)) {// 终端控制指令
				if (!"U_REPT".equals(mtype)) {
					oracleDba.updateControlCommand(app);
				}
				redisDba.updateCommand(app);
//				如果是查询远程锁车参数，就插入或者更新TB_LOCK_VEHICLE_DETAIL远程锁车信息表
				String seq = app.get(Constant.SEQ);
				if(CommandCache.containsControlKey(seq) && CommandCache.getControlCache(seq).equals("27")){
//					插入或者修改的参数1.VID 2.锁车类型 3。发动机最高转速 4。预锁车时间
//					Long.parseLong(app.get(Constant.VID))
					oracleDba.saveOrUpdateLockVehicleDetail(app.get(Constant.VID),app.get("VALUE"));
					CommandCache.removeControlCache(seq);
				}
//				如果是远程锁车应答，就更新解锁码 (（解锁 -- 应答没有解锁码,不出来）)
				if(CommandCache.containsControlKey(seq) && CommandCache.getControlCache(seq).equals("26")){
					if(StringUtils.isNotBlank(app.get("VALUE"))){ 
						oracleDba.updateUnlockCode(app.get(Constant.VID), app.get("VALUE"));
					}
					CommandCache.removeControlCache(seq);
				}
			} else if ("D_SNDM".equals(mtype)) {// 发送短信
				oracleDba.updateVehicleDispatchMsg(app); // 更新调度信息表
				oracleDba.updateControlCommand(app); // 下行指令历史表
				redisDba.updateCommand(app);
			} else if ("D_REQD".equals(mtype)) {// 请求终端数据指令
				if ("1".equals(subtype)) {
					oracleDba.saveMediaIdx(app);
				} else {
					oracleDba.updateControlCommand(app);
					redisDba.updateCommand(app);
				}
			} else if ("D_ADDT".equals(mtype)) {// 订阅指令
				oracleDba.updateControlCommand(app);
				redisDba.updateCommand(app);
			} else if ("D_DELT".equals(mtype)) {// 退订指令
				oracleDba.updateControlCommand(app);
				redisDba.updateCommand(app);
			} else if ("D_CALL".equals(mtype)) {// 点名指令
				oracleDba.updateControlCommand(app);
				redisDba.updateCommand(app);
			}
		}
	}
}
