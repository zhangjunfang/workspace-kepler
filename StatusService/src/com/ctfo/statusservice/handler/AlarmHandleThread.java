/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.savecenter.addin.kcpt.trackmanager AlarmHandlerThread.java	</li><br>
 * <li>时        间：2013-7-2  下午4:21:48	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.handler;

import java.sql.SQLException;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statusservice.model.Alarm;
import com.ctfo.statusservice.model.Alarm809;
import com.ctfo.statusservice.model.AlarmEnd;
import com.ctfo.statusservice.model.AlarmStart;
import com.ctfo.statusservice.model.AlarmTypeBean;
import com.ctfo.statusservice.model.Driver;
import com.ctfo.statusservice.model.Pack;
import com.ctfo.statusservice.model.ParentInfo;
import com.ctfo.statusservice.model.ServiceUnit;
import com.ctfo.statusservice.util.Cache;
import com.ctfo.statusservice.util.Constant;
import com.ctfo.statusservice.util.LocalDriverCacle;
import com.ctfo.statusservice.util.Tools;


/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： statusservice		</li><br>
 * <li>时        间：2013-7-2  下午4:21:48	</li><br>
 * </ul>
 *****************************************/
public class AlarmHandleThread extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(AlarmHandleThread.class);
	/** 数据缓存队列 	*/
	private ArrayBlockingQueue<Pack> arrayQueue = new ArrayBlockingQueue<Pack>(100000);
	/** 线程编号	 */
	private int threadId;
	/** 存储报警开始线程 	*/
	private AlarmEndStorageThread alarmEndStorageThread;
	/** 存储报警结束线程 	*/
	private AlarmStartStorageThread alarmStartStorageThread;
	/** 计数器 	*/
	int index = 0;
	/** 存储开始计数器 	*/
	int savestartIndex = 0;
	/** 存储结束计数器	 */
	int saveEndIndex = 0;
	/** 最近时间	 */
	long lastTime = System.currentTimeMillis();

	/*****************************************
	 * <li>描 述：初始化方法</li><br>
	 * <li>参 数：@param oracleJdbcService 
	 * <li>参 数：@param threadId
	 *****************************************/
	public AlarmHandleThread(int threadId, AlarmStartStorageThread alarmStartStorageThread, AlarmEndStorageThread alarmEndStorageThread) {
		super("AlarmHandleThread" + threadId);
		this.alarmStartStorageThread = alarmStartStorageThread;
		this.alarmEndStorageThread = alarmEndStorageThread;
		this.threadId = threadId;
	}
	
	
	@Override
	public void run() {
//		Pack pack = null;
		while (true) {
			try {
				Pack pack = arrayQueue.take();
//				处理报警
				alarmHandling(pack);
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime) > 10000){
					int size = getPacketsSize();
					logger.info("alarmhandler:" + threadId + ",排队:(" + size + "),10秒处理("+index+")条,--报警开始:（" + savestartIndex + ")条,报警结束:("+ saveEndIndex +")条");
					index = 0;
					savestartIndex = 0;
					saveEndIndex = 0;
					lastTime = System.currentTimeMillis();
				}
				index ++;
			} catch (Exception e) {
				logger.error("报警处理线程错误:" + e.getMessage(), e);
			}
		}
	}


	/*****************************************
	 * <li>描 述：处理报警信息</li><br>
	 * <li>判断开始报警 --- 1.如果缓存中没有mac_id对应的报警表，2.有报警缓存，无对应报警编号</li><br>
	 * <li>判断持续报警 --- 1.报警列表中有相同报警 <li>判断结束报警 --- 1.剔除持续报警后的历史报警 <li>时
	 * 间：2013-7-2 下午4:51:47</li><br>
	 * <li>参数： @param alarmMap</li><br>
	 * @throws SQLException  
	 * 
	 *****************************************/
	private void alarmHandling(Pack pack) throws SQLException { 
		// 获取上一次报警
		Map<String, Alarm> alarmMap = Cache.vehicleStatusMap.get(pack.getMacid());
		// 本次报警类型数组
		String[] alarmCodeArray = null;
		// 上一次报警代码
		String lastAlarmCodeStr = null;
		String alarmId = null;
		String currentAlarmCode = pack.getAllAlarm();
		alarmCodeArray = StringUtils.split(currentAlarmCode, Constant.COMMA);
		
		if(alarmMap!=null){
			lastAlarmCodeStr = join(alarmMap.keySet(), Constant.COMMA);
		}
		//		处理速度
		parseVehicleSpeed(pack);
		int spd = pack.getSpeed(); 		// 车速
		int engineSpeed = pack.getEngineSpeed();// 发动机转速
		//		 (1)开始报警 --- 1.如果缓存中没有mac_id对应的报警表，2.有报警缓存，无对应报警编号
		if (alarmMap == null) {
			alarmMap = new ConcurrentHashMap<String, Alarm>();
			if(alarmCodeArray != null){
				for (String alarmCode : alarmCodeArray) {
					alarmId =  UUID.randomUUID().toString().replaceAll("-", ""); 
					Alarm alarm = new Alarm();
					alarm.setStartUtc(pack.getGpsUtc());
					alarm.setId(alarmId);
					alarm.setMaxSpeed(spd); // 记录当前最大车速
					if(alarmCode.equals("1")){ // 超速告警记录最大车速和平均车速
						alarm.setSpeedSum(spd); // 记录速度总和以及记速次数
					}
					if(alarmCode.equals("47")){ 
						alarm.setMaxRpm(engineSpeed); // 记录发动机转速
					}
					saveAlarmStart(pack, alarmCode, alarmId, alarm); 
					alarmMap.put(alarmCode, alarm); 
				}
			}
			Cache.vehicleStatusMap.put(pack.getMacid(), alarmMap);
		} else {
//			有报警缓存，无对应报警编号 -- 存储报警
			lastAlarmCodeStr = join(alarmMap.keySet(), Constant.COMMA);
			if(alarmCodeArray != null){
				for (String alarmCode : alarmCodeArray) {
					// 如果是新上传的报警，报警缓存中没有对应报警，就存储报警
					if (!alarmMap.containsKey(alarmCode)) {
						alarmId =  UUID.randomUUID().toString().replaceAll("-", ""); 
						Alarm alarm = new Alarm();
						alarm.setStartUtc(pack.getGpsUtc());
						alarm.setId(alarmId);
						alarm.setMaxSpeed(spd); // 记录当前最大车速
						if(alarmCode.equals("1")){ // 超速告警记录最大车速和平均车速
							alarm.setSpeedSum(spd); // 记录速度总和以及记速次数
						}
						if(alarmCode.equals("47")){ // 存储发动机最大转速
							alarm.setMaxRpm(engineSpeed); 
						}
						saveAlarmStart(pack,alarmCode,alarmId,alarm); 
						alarmMap.put(alarmCode, alarm); 
					} else {
//		(2)持续报警 --- 如果报警列表中有相同报警，就从上一次报警代码中删除，更新报警结束时不处理 
						lastAlarmCodeStr = StringUtils.replace(lastAlarmCodeStr, addComma(alarmCode), Constant.COMMA);
						if(alarmCode.equals("1")){ // 超速告警记录最大车速和平均车速
							Alarm alarm = alarmMap.get("1");
							if(spd > alarm.getMaxSpeed()){
								alarm.setMaxSpeed(spd); // 记录当前最大车速
							}
							alarm.setSpeedSum(spd); // 记录速度总和以及记速次数
						}
						if(alarmCode.equals("47")){ 
							Alarm alarm = alarmMap.get("47");
							if(engineSpeed > alarm.getMaxRpm()){
								alarm.setMaxRpm(engineSpeed); // 记录发动机最大转速
							}
						}
					}
				}
			}
//		(3)结束报警 --- 1.将剔除持续报警后的历史报警中的报警信息进行更新
			alarmCodeArray = StringUtils.split(lastAlarmCodeStr, Constant.COMMA);
			for (String lastCode : alarmCodeArray) {
				Alarm alarm = alarmMap.get(lastCode);
				if(!lastCode.equals("1")){
					alarm.setMaxSpeed(spd);  
				}
				if (pack.getGpsUtc() > alarm.getStartUtc()) {
					saveAlarmEnd(pack,lastCode,alarm);
					alarmMap.remove(lastCode);
				}
			}
		}
	}

	/*****************************************
	 * <li>描 述：保存报警信息</li><br>
	 * <li>时 间：2013-7-2 下午6:56:57</li><br>
	 * <li>参数：</li><br>
	 * @param alarm 
	 * @throws SQLException 
	 * 
	 *****************************************/
	private void saveAlarmStart(Pack pack, String alarmCode,String alarmId, Alarm alarm) throws SQLException {
		savestartIndex ++;
		int spd = alarm.getMaxSpeed();//车速
		String vidStr = String.valueOf(pack.getVid());
		AlarmStart as = new AlarmStart();
//		发送报警到实时服务指令（cs客户端使用）
		String realTimeAlarmCommand = getRealTimeAlarmCommand(pack.getCommand(), alarmId + "_" + pack.getGpsUtc(), alarmCode, "0"); ;
		if(realTimeAlarmCommand != null){
			as.setRealTimeAlarmCommand(realTimeAlarmCommand); 
		}
		as.setAlarmId(alarmId);
		as.setVid(vidStr);
		as.setUtc(pack.getGpsUtc());
		as.setLon(pack.getLon());
		as.setLat(pack.getLat());
		as.setMaplon(pack.getMaplon());
		as.setMaplat(pack.getMaplat());
		as.setElevation(pack.getElevation()); // 海拔
		as.setDirection(pack.getDirection());// 方向
		as.setGpsSpeed(spd);
		as.setMileage(pack.getMileage());
		as.setOilTotal(pack.getOilTotal());
		as.setAlarmCode(alarmCode);
		as.setSysUtc(System.currentTimeMillis());
		as.setAlarmStatus(1);
		as.setAlarmStartUtc(pack.getGpsUtc());
		as.setAlarmDriver("");
		as.setPlate(pack.getPlate());

		AlarmTypeBean alarmTypeBean = Cache.getAlarmtypeMapValue(Integer.parseInt(alarmCode));
		if (alarmTypeBean != null) {
			as.setAlarmLevel(alarmTypeBean.getParentCode());
		} else {
			as.setAlarmLevel("");
		}
		as.setBaseStatus(pack.getBaseStatus());
		as.setExtendStatus(pack.getExtendedStatus());
		as.setAlarmAdded(pack.getAlarmAdded());// 附加报警信息
		
//		拼接redis缓存报警数据   key: ALARM0##1#200#201#13897#15432#17653#PLATE湘A12345:8a46c870b2374eb7980a7b69d429cc95,
		String macid = pack.getMacid();
		ServiceUnit su = Cache.getVehicleMapValue(macid);
		String teamId = su.getTeamId();
		ParentInfo parentInfo = Cache.getOrgParent(teamId);
		String plate = su.getVehicleno();
//		存储组织信息
		as.setTeamId(teamId);
		as.setTeamName(su.getTeamName() != null ? su.getTeamName() : "");
		as.setEntId(su.getEntId());
		as.setEntName(su.getEntName() != null ? su.getEntName() : "");
		if(parentInfo == null){
			logger.debug("处理报警获取组织信息数据为空,车牌号:" + plate + ", 指令:" + pack.getCommand());
			return;
		} else {
			if(parentInfo.getParent() != null && parentInfo.getEntName() != null){
				String value = parentInfo.getParent().replaceAll(",", "#");
				parentInfo.setParent(value);
			} else {
				logger.debug("处理报警获取组织信息内容为空,车牌号:" + plate + ", 指令:" + pack.getCommand());
				return;
			} 
		}
		Driver driver = LocalDriverCacle.getInstance().getDriverInfo(vidStr);
		if(driver != null){ // 加入驾驶员缓存信息
			as.setDriverId(driver.getDriverId());
			as.setDriverSource(driver.getDriverSource()); 
		} else {
			as.setDriverId(""); 
			as.setDriverSource(-1); 
		}
		StringBuffer sb = new StringBuffer();
		sb.append("ALARM").append(as.getAlarmCode()).append("#"); 
		sb.append(parentInfo.getParent());
		sb.append("PLATE").append(plate);
		sb.append(":").append(as.getAlarmId()).append(",");
		String cacheAlarmId = sb.toString();
		alarm.setCacheAlarmId(cacheAlarmId);
		as.setAlarmKey(cacheAlarmId);
		as.setAlarmValue(as.toString(su, parentInfo.getEntName()));
//		判断报警是否发送PCC监管
		if(isPccAlarm(alarmCode)){
			Alarm809 alarm809 = new Alarm809();
			alarm809.setAlarmCode(alarmCode);
			alarm809.setMacid(macid);
			alarm809.setAlarmAdd(pack.getAlarmAdded());
			alarm809.setAlarmUtc(pack.getGpsUtc());
			as.setPccAlarmCommand(alarm809.toKeyValue());
		}
		alarmStartStorageThread.putDataMap(as);
	}

	/**
	 * 判断是否为发送监管报警类型
	 * @param alarmCode
	 * @return
	 */
	private boolean isPccAlarm(String alarmCode) {
		if(alarmCode.equals("0") 
				||alarmCode.equals("1")
				||alarmCode.equals("2")
				||alarmCode.equals("18")
				||alarmCode.equals("23")
				||alarmCode.equals("28") 
				||alarmCode.equals("20")){
			return true;
		}
		return false;
	}


	/*****************************************
	 * <li>描 述：更新报警</li><br>
	 * <li>时 间：2013-7-2 下午7:40:01</li><br>
	 * <li>参数： @param dataMap</li><br>
	 * @throws SQLException 
	 * 
	 *****************************************/
	private void saveAlarmEnd(Pack pack, String alarmCode,Alarm alarm) throws SQLException { 
		saveEndIndex ++;
		int spd = alarm.getMaxSpeed();//车速
//		删除最大转速缓存内容
		Cache.vehicleMaxRpmMap.remove(pack.getMacid());
//		logger.info("----update----更新报警--"+alarmCode); 
		AlarmEnd alarmEnd = new AlarmEnd(); 
//		发送报警数据指令
		String realTimeAlarmCommand = getRealTimeAlarmCommand(pack.getCommand(), alarm.getId() + "_" + pack.getGpsUtc(), alarmCode, "1"); ;
		if(realTimeAlarmCommand != null){
			alarmEnd.setRealTimeAlarmCommand(realTimeAlarmCommand); 
		}
//		sendAlarm(pack , alarm.getId() + "_" + pack.getGpsUtc(), alarmCode, "1");  
		alarmEnd.setEndUtc(pack.getGpsUtc());
		alarmEnd.setLon(pack.getLon());
		alarmEnd.setLat(pack.getLat());
		alarmEnd.setMaplon(pack.getMaplon());
		alarmEnd.setMaplat(pack.getMaplat());
		alarmEnd.setElevation(pack.getElevation());
		alarmEnd.setDirection(pack.getDirection());
		alarmEnd.setGpsSpeed(spd);
		alarmEnd.setMileage(pack.getMileage());
		alarmEnd.setOilTotal(pack.getOilTotal());
		alarmEnd.setAlarmAdded(pack.getAlarmAdded());
		alarmEnd.setMaxRpm(alarm.getMaxRpm());
		// 存储缓存报警编号
		if(alarm.getCacheAlarmId() == null){
			logger.debug("存储缓存报警结束编号异常:" + alarm.getId());
			return ;
		}
		alarmEnd.setAlarmKey(alarm.getCacheAlarmId());		//	redis报警键
		alarmEnd.setSysUtc(System.currentTimeMillis());		//	系统时间
		alarmEnd.setAlarmId(alarm.getId()); 				//	报警编号
		alarmEnd.setMaxSpeed(alarm.getMaxSpeed());			//	最大车速
		alarmEnd.setAverageSpeed(alarm.getAverageSpeed()); 	// 	平均车速
		
		alarmEndStorageThread.putDataMap(alarmEnd);
	}
 

	/*****************************************
	 * <li>描        述：发送监管协议 		</li><br>
	 * <li>时        间：2013-7-4  下午4:44:10	</li><br>
	 * <li>参数： @param app
	 * <li>参数： @param alarmid
	 * <li>参数： @param alarmcode
	 * <li>参数： @param status			</li><br>
	 * 
	 *****************************************/
	private String getRealTimeAlarmCommand(String command, String alarmid, String alarmcode, String status){
		if(alarmid != null && alarmcode != null){
			return Tools.replaceCommandFlag(command, alarmid, alarmcode, status);
		} else {
			return null;
		}
	}
	/*****************************************
	 * <li>描        述：拼接字符串 		</li><br>
	 * <li> join([a,b,c] , ",")  = ",a,b,c,"
	 * <li> 
	 * <li>时        间：2013-7-4  下午4:23:47	</li><br>
	 * <li>参数： @param set
	 * <li>参数： @param separator
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static String join(Set<?> set, String separator) {
		StringBuilder buf = new StringBuilder(256); // Java default is 16,
		buf.append(separator);
		Iterator<?> iterator = set.iterator();
		while (iterator.hasNext()) {
			Object obj = iterator.next();
			if (obj != null) {
				buf.append(obj);
			}
			buf.append(separator);
		}
		return buf.toString();
	}
	
	/*****************************************
	 * <li>描        述：添加逗号 		</li><br>
	 * <li>  addComma("ABC") = ,ABC,
	 * <li>时        间：2013-7-10  下午1:54:41	</li><br>
	 * <li>参数： @param alarmCode
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	private String addComma(String alarmCode) {
		return new StringBuilder().append(Constant.COMMA).append(alarmCode).append(Constant.COMMA).toString();
	}
	
	/*****************************************
	 * <li>描        述：更新当前最大转速 		</li><br>
	 * <li>时        间：2013-7-4  下午5:33:24	</li><br>
	 * <li>参数： @param macId 
	 * <li>参数： @param currentRPM 当前转速			</li><br>
	 * 
	 *****************************************/
	public void updateMaxRPM(String macId, Long currentRPM){
		if(Cache.vehicleMaxRpmMap.get(macId) != null){
			if(Cache.vehicleMaxRpmMap.get(macId).compareTo(currentRPM) < 0){
				Cache.vehicleMaxRpmMap.put(macId, currentRPM);
			}
		}else{
			Cache.vehicleMaxRpmMap.put(macId, 0l);
		}
	}
	


	/****************************************
	 * <li>描 述：将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。</li><br>
	 * <li>时 间：2013-7-2 下午4:33:59</li><br>
	 * <li>参数： @param packet</li><br>
	 * 
	 *****************************************/
	public void putDataMap(Pack pack) {
		try {
			arrayQueue.put(pack);
		} catch (InterruptedException e) {
			logger.error("向报警队列插入元素异常:", e);
		}
	}

	/*****************************************
	 * <li>描 述：获得队列大小</li><br>
	 * <li>时 间：2013-7-2 下午4:36:57</li><br>
	 * <li>参数： @return</li><br>
	 * 
	 *****************************************/
	public int getPacketsSize() {
		return arrayQueue.size();
	}
	/*****************************************
	 * <li>描        述：解析车速 		</li><br>
	 * <li>时        间：2013-9-27  下午7:17:46	</li><br>
	 * <li>参数： @param app
	 * <li>参数： @return			</li><br>
	 * 优先去GPS速度  速度来源(VSS:0; GPS:1)
	 *****************************************/
	public void parseVehicleSpeed(Pack pack){
		String source = pack.getSpeedSource();
		String vssStr = pack.getVssSpeedStr();
		String gpsStr = pack.getGpsSpeedStr();
		int speed = 0;
		if(StringUtils.equals(source, "0")){// 速度来源是VSS
			if(StringUtils.isNumeric(vssStr)){
				speed = Integer.parseInt(vssStr);
				pack.setVssSpeed(speed);
				pack.setSpeed(speed);
			}else {
				pack.setVssSpeed(-1);
				pack.setSpeed(-1);
			}
			if(StringUtils.isNumeric(gpsStr)){
				pack.setGpsSpeed(Integer.parseInt(gpsStr)); 
			} else {
				pack.setGpsSpeed(-1);
			}
		} else {
			if(StringUtils.isNumeric(gpsStr)){ //速度来源是GPS
				speed = Integer.parseInt(gpsStr);
				pack.setSpeed(speed);
				pack.setGpsSpeed(speed); 
				pack.setSpeedSource("1"); 
			} else {
				pack.setSpeed(-1);
				pack.setGpsSpeed(-1); 
				pack.setSpeedSource("1");
			}
			if(StringUtils.isNumeric(vssStr)){
				pack.setVssSpeed(Integer.parseInt(vssStr)); 
			} else {
				pack.setVssSpeed(-1);
			}
		}
	}
}