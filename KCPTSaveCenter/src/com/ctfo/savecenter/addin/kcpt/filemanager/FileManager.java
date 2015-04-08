package com.ctfo.savecenter.addin.kcpt.filemanager;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.addin.kcpt.trackmanager.TrackManagerKcptMainThread;
import com.ctfo.savecenter.beans.DriftLatLon;
import com.ctfo.savecenter.beans.ServiceFileUnit;
import com.ctfo.savecenter.dao.TempMemory;
import com.ctfo.savecenter.util.CDate;
import com.ctfo.savecenter.util.FileUtil;
import com.ctfo.savecenter.util.Utils;
import com.lingtu.xmlconf.XmlConf;

public class FileManager extends Thread{
	//计数器
	private int index = 0 ;
	//当前线程编号
	private int idxNum = 0;
	
	private static final Logger logger = LoggerFactory.getLogger(FileManager.class);

	// 运行标志
	public boolean isRunning = true;
	
	// 轨迹文件目录
	private String trackfileurl;
	
	// 报警分析文件目录
	private String alarmFileUrl;

	// 文件批量提交时间(以ms为单位)
	private int commitFileTime;
	//提交频率（秒）	
	private int commit = 10;
	// 文件批量提交数量
	private int commitFileCount;
	
	//上一次提交时间
	private long lastCommitTime = System.currentTimeMillis();
	
	// 异步数据报向量
	private ArrayBlockingQueue<Map<String,String>> vPacket = new ArrayBlockingQueue<Map<String,String>>(100000);

	private Map<String,ServiceFileUnit> serviceFileMap = new ConcurrentHashMap<String,ServiceFileUnit>();
	
	public FileManager(int idxNum){
		this.idxNum = idxNum;
	}
	public int getPacketsSize() {
		return vPacket.size();
	}
	public void addPacket(Map<String,String> packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			logger.error("",e);
		}
	}

	public void initAnalyser(XmlConf config, String nodeName) throws Exception {
		// 轨迹文件目录
		trackfileurl = config.getStringValue(nodeName + "|trackfileurl");

		// 车辆报警统计分析目录
		alarmFileUrl = config.getStringValue(nodeName + "|alarmfileurl");
		
		//文件处理间隔时间
		commit = config.getIntValue(nodeName + "|commitFileTime");
		commitFileTime = commit * 1000;
		
		commitFileCount = config.getIntValue(nodeName + "|commitFileCount");
	}
	/**  */
	@Override
	public void run() {
		logger.info("文件存储主线程" + idxNum + "启动");
		while (TrackManagerKcptMainThread.isRunning) {
			try { 
				Map<String,String> app = vPacket.take();
				//存储文件
				saveFileTrack(app);					
				long currentTime = System.currentTimeMillis(); //按时间批量提交
				if((currentTime - lastCommitTime) > commitFileTime){
					saveData();
					lastCommitTime = currentTime;
					logger.warn("file------:" + idxNum + ",size:" + vPacket.size() + ","+commit+"秒处理数据:"+index+"条");
					index = 0;
				}
				index ++;
			} catch (Exception e) {
//				e.printStackTrace(); //TODO
				logger.error("文件存储主线程队列出错"+ e.getMessage(), e);
			}
		}// End while
	}

	/**
	 * 基本数据文件保存
	 * 
	 */
	private void saveFileTrack(Map<String,String> app){
		String vid = app.get(Constant.VID);
		if(!StringUtils.isNumeric(vid)){ 
			return ;
		}
		String tempStatus = app.get("8");
		if(tempStatus != null){
			String binarystatus = Long.toBinaryString(Long.parseLong(tempStatus));
			if(binarystatus.endsWith("0")){ // 判断ACC关
				DriftLatLon driftLatLon = TempMemory.getDriftLatLon(Long.parseLong(vid));
				if(driftLatLon.getAccCount() == 0){
					driftLatLon.addAccCount(1);
					driftLatLon.setLat(Long.parseLong(app.get("2")));
					driftLatLon.setLon(Long.parseLong(app.get("1")));
					driftLatLon.setMapLat(Long.parseLong(app.get(Constant.MAPLAT)));
					driftLatLon.setMapLon(Long.parseLong(app.get(Constant.MAPLON)));
				}else if(driftLatLon.getAccCount() == 1){
					app.put("1", driftLatLon.getLon()+"");
					app.put("2", driftLatLon.getLat()+"");
					app.put(Constant.MAPLAT, driftLatLon.getMapLat()+"");
					app.put(Constant.MAPLON, driftLatLon.getMapLon()+"");
				}
			}else{
				DriftLatLon driftLatLon = TempMemory.getDriftLatLon(Long.parseLong(vid));
				driftLatLon.resetAll();
			}
		}
		
		String gpsTime = app.get("4");
		if(gpsTime == null){
			logger.warn("saveFileTrack---app.get(4)==null:"+app.get(Constant.COMMAND)); 
			return ;
		}
		StringBuffer buf = new StringBuffer("");
		StringBuffer alarmBuf = new StringBuffer("");
		
		buf.append(app.get(Constant.MAPLON)); // 经度0
		buf.append(":");
		
		buf.append(app.get(Constant.MAPLAT)); // 纬度1
		
		buf.append(":");
		
		buf.append(gpsTime); // GPS时间2
		
		buf.append(":");
		buf.append(app.get("3")); // GPS 速度3
		buf.append(":");
		
		if(app.get("5")  != null && !"".equals(app.get("5"))){
			buf.append(app.get("5")); // 正北方向夹角4
		}
		buf.append(":");
		if(app.get("26") != null){
			buf.append(app.get("26")); // 车辆状态5
		}
		buf.append(":");
//		String alarmCode = "";
//		if(app.containsKey("20")){
//			alarmCode = alarmCode + app.get("20");
//		}
//		if(app.containsKey("21")){
//			alarmCode = alarmCode + app.get("21");
//		}
//		alarmCode = alarmCode.toString().replaceAll("\\,\\,",",");
		buf.append(app.get(Constant.FILEALARMCODE)); // 报警编码6
		buf.append(":");
		buf.append(app.get("1")); // 经度7
		buf.append(":");
		buf.append(app.get("2")); // 纬度8
		buf.append(":");
		if(app.get("6") != null){
			buf.append(app.get("6")); // 海拔9
		}
		buf.append(":");
		if (app.containsKey("9")) {
			buf.append(app.get("9")); // 里程10
		}
		buf.append(":");

		if (app.containsKey("213")) {
			buf.append(app.get("213")); // 累计油耗11
		}
		buf.append(":");

		if (app.containsKey("505")) {
			buf.append(app.get("505")); // 发动机运行总时长12
		}
		buf.append(":");

		if (app.containsKey("210")) {
			buf.append(app.get("210")); // 引擎转速（发动机转速）13
		}

		buf.append(":");
		
		if (app.containsKey("8") && !"null".equals(app.get("8"))) {// 位置基本信息状态位14
			buf.append(app.get("8")); 
		}
		
		buf.append(":");
		if(app.containsKey("32")){
			String areaInfo = app.get("32");
			if(areaInfo!=null){
				String temp = areaInfo.replaceAll("null", "");
				buf.append(temp); //区域/线路报警附加信息15
			}
		}
		buf.append(":");

		if (app.containsKey("509")) { // 冷却液温度16
			buf.append(app.get("509"));
		}

		buf.append(":");

		// 蓄电池电压17
		if (app.containsKey("507")) {
			buf.append(app.get("507"));
		}

		buf.append(":");

		// 瞬时油耗18
		if (app.containsKey("216")) {
			buf.append(app.get("216"));
		}

		buf.append(":");

		// 行驶记录仪速度(km/h)19
		if (app.containsKey("7")) {
			buf.append(app.get("7"));
		}
		
		buf.append(":");
		
		//机油压力 (20 COL)
		if(app.containsKey("215")){
			buf.append(app.get("215"));
		}
		
		buf.append(":");
		
		//大气压力21
		if(app.containsKey("511")){
			buf.append(app.get("511"));
		}
			
		buf.append(":");
		// 发动机扭矩百分比，1bit=1%，0=-125%    22
		if (app.containsKey("503")) {
			buf.append(app.get("503")); 
		}
		
		buf.append(":");
		
		//车辆信号状态 23
		if(app.containsKey("500")){
			buf.append(app.get("500"));
		}
		
		buf.append(":");
		
		buf.append(app.get(Constant.SPEEDFROM)); // 车速来源 24
		
		buf.append(":");
		
		if(app.get("24") != null){
			buf.append(app.get("24")); // 油量（对应仪表盘读数） 25 
		}
		
		buf.append(":");
		
		if(app.get("31") != null){
			buf.append(app.get("31")); // 超速报警附加信息 26
		}
		
		buf.append(":");
		
		if(app.get("35") != null){
			buf.append(app.get("35")); // 路线行驶时间不足/过长 27
		}
		
		buf.append(":");
		
		if(app.get("504") != null){
			buf.append(app.get("504")); // 油门踏板位置，(1bit=0.4%，0=0%) 28 
		}
		
		buf.append(":");
		
		if(app.get("506") != null){
			buf.append(app.get("506")); // 终端内置电池电压 29
		}
		
		buf.append(":");
		
		if(app.get("214") != null){
			buf.append(app.get("214")); // 发动机水温 30
		}
		
		buf.append(":");
		
		if(app.get("508") != null){
			buf.append(app.get("508")); // 机油温度 31
		}
		
		buf.append(":");
		
		if(app.get("510") != null){
			buf.append(app.get("510")); // 进气温度 32
		}
		
		buf.append(":");
		
		if(app.get("217") != null){
			buf.append(app.get("217")); // 开门状态 33
		}
		
		buf.append(":");
		
		if(app.get("519") != null){
			buf.append(app.get("519")); // 需要人工确认报警事件的ID 34
		}
		
		buf.append(":");
		
		if(app.get("219") != null){
			buf.append(app.get("219")); // 计量仪油耗，1bit=0.01L,0=0L 35
		}
		
		buf.append(":");
		
		buf.append(CDate.getCurrentUtcMsDate()); //系统时间 36
		
		buf.append("\r\n"); // 标记换行
		
		// 报警存储
		alarmBuf.append(app.get(Constant.FILEALARMCODE)); // 报警编码0
		alarmBuf.append(":");
		alarmBuf.append(app.get(Constant.MAPLON)); // MAP经度1
		alarmBuf.append(":");
		alarmBuf.append(app.get(Constant.MAPLAT)); // MAP纬度2
		alarmBuf.append(":");
		alarmBuf.append(app.get("1"));// 经度3
		alarmBuf.append(":");
		alarmBuf.append(app.get("2")); // 纬度4
		alarmBuf.append(":");
		alarmBuf.append(gpsTime); // GPS时间5
		alarmBuf.append(":");
		alarmBuf.append(Utils.getSpeed(app)); // 根据车速来源获取车速6
		alarmBuf.append(":");
		alarmBuf.append(app.get("5")); //正北方向夹角7
		alarmBuf.append(":");
		if (app.containsKey("213")) {
			alarmBuf.append(app.get("213")); // 累计油耗8
		}
		alarmBuf.append(":");
		if (app.containsKey("9")) {
			alarmBuf.append(app.get("9")); // 里程9
		}
		alarmBuf.append(":");
		if(app.containsKey("32")){
			alarmBuf.append(app.get("32")); //报区域/线路报警10
		}
		alarmBuf.append(":");
		if(app.get("6") != null){
			alarmBuf.append(app.get("6")); // 海拔11
		}
		alarmBuf.append(":");
		alarmBuf.append(app.get(Constant.SPEEDFROM)); // 车速来源 12
		
		alarmBuf.append(":");
		
		alarmBuf.append(CDate.getCurrentUtcMsDate()); //系统时间13
		alarmBuf.append("\r\n"); // 标记换行
		
		ServiceFileUnit serviceFileUnit = serviceFileMap.get(vid);
		if (serviceFileUnit == null) {
			ServiceFileUnit service = new ServiceFileUnit();
			service.setDay(gpsTime.substring(6, 8));
			service.setGpsTime(gpsTime);
			service.setVid(vid);
			service.setFilecontent(buf.toString().replaceAll("null", ""));
			service.setAlarmfilecontent(alarmBuf.toString().replaceAll("null", ""));
			service.addRecordCount(1);
			serviceFileMap.put(vid, service);
		} else {

			String day = serviceFileUnit.getDay();
			if (!gpsTime.substring(6, 8).equals(day)) {// 跨天提交
				String year = serviceFileUnit.getGpsTime().substring(0, 4);
				String month = serviceFileUnit.getGpsTime().substring(4, 6);
				String trackFile = trackfileurl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";
				try{
					RandomAccessFile rf = new RandomAccessFile(trackFile, "rw");
					rf.seek(rf.length());// 将指针移动到文件末尾
					rf.writeBytes(serviceFileUnit.getFilecontent());
					rf.close();
					logger.debug(vid + "跨天写入轨迹文件成功");
					
					String alarmPath = alarmFileUrl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";
					
					RandomAccessFile rfAlarm = new RandomAccessFile(alarmPath, "rw");
					rfAlarm.seek(rfAlarm.length());// 将指针移动到文件末尾
					rfAlarm.writeBytes(serviceFileUnit.getAlarmfilecontent());
					rfAlarm.close();
					logger.debug( vid+ "跨天写入报警文件成功");
				}catch(Exception ex){
					logger.error("跨天存储出错",ex);
				}
				serviceFileUnit.resetUnit();
				
				serviceFileUnit.setDay(gpsTime.substring(6, 8));//存入当天日
				serviceFileUnit.setGpsTime(gpsTime);
			}
			
			//存储当天
			serviceFileUnit.setFilecontent(serviceFileUnit.getFilecontent() + buf.toString().replaceAll("null", ""));
			serviceFileUnit.setAlarmfilecontent(serviceFileUnit.getAlarmfilecontent() + alarmBuf.toString().replaceAll("null", ""));
			serviceFileUnit.addRecordCount(1);
			if(serviceFileUnit.getRecordCount() > commitFileCount){
				String year = serviceFileUnit.getGpsTime().substring(0, 4);
				String month = serviceFileUnit.getGpsTime().substring(4, 6);
				day = serviceFileUnit.getGpsTime().substring(6, 8);
				String trackFile = trackfileurl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";
				
				RandomAccessFile rf = null;
				try {
					rf = new RandomAccessFile(trackFile, "rw");
				
					rf.seek(rf.length());// 将指针移动到文件末尾
					rf.writeBytes(serviceFileUnit.getFilecontent());
					
					//logger.debug(vid + "写入轨迹文件成功");
				} catch (FileNotFoundException e) {
					logger.warn("在" + serviceFileUnit.getGpsTime() + "读 轨迹文件" + vid + ".txt 找不到.",e);
					FileUtil.coverFolder(trackfileurl);
				} catch (IOException e) {
					logger.error("在" + serviceFileUnit.getGpsTime() + "写入轨迹文件" + vid + ".txt",e);
				}finally{
					if(rf != null){
						try {
							rf.close();
						} catch (IOException e) {
							logger.error("在" + serviceFileUnit.getGpsTime() + "关闭轨迹文件" + vid + ".txt 找不到.",e);
						}
					}
				}
				
				String alarmPath = alarmFileUrl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";
				RandomAccessFile rfAlarm = null;
				try {
					rfAlarm = new RandomAccessFile(alarmPath, "rw");
					rfAlarm.seek(rfAlarm.length());// 将指针移动到文件末尾
					rfAlarm.writeBytes(serviceFileUnit.getAlarmfilecontent());
					//logger.debug(vid + "写入报警文件成功");
				} catch (FileNotFoundException e) {
					logger.warn("在" + serviceFileUnit.getGpsTime() + "读 报警文件" + vid + ".txt 找不到.",e);
					FileUtil.coverFolder(alarmFileUrl);
				} catch (IOException e) {
					logger.error("在" + serviceFileUnit.getGpsTime() + "写入报警文件" + vid + ".txt",e);
				}finally{
					if(rfAlarm != null){
						try {
							rfAlarm.close();
						} catch (IOException e) {
							logger.error("在" + serviceFileUnit.getGpsTime() + "关闭报警文件" + vid + ".txt 找不到.",e);
						}
					}
				}
				serviceFileUnit.resetRecordCount();
			}
		}
	}
	
	/***
	 * 将数据写入文件
	 */
	private void saveData(){
		Set<String> st = serviceFileMap.keySet();
		Iterator<String> it = st.iterator();
		while(it.hasNext()){
			String vid = it.next();
			ServiceFileUnit serviceFileUnit = serviceFileMap.get(vid);
			if(serviceFileUnit.getRecordCount() == 0){
				continue;
			}
			
			//logger.error("VID:" + vid + "文件:" + serviceFileUnit.getGpsTime());
			String year = serviceFileUnit.getGpsTime().substring(0, 4);
			String month = serviceFileUnit.getGpsTime().substring(4, 6);
			String day = serviceFileUnit.getGpsTime().substring(6, 8);
			String trackFile = trackfileurl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";
			
			RandomAccessFile rf = null;
			try {
				rf = new RandomAccessFile(trackFile, "rw");
			
				rf.seek(rf.length());// 将指针移动到文件末尾
				rf.writeBytes(serviceFileUnit.getFilecontent());
				
				//logger.debug(vid + "写入轨迹文件成功");
			} catch (FileNotFoundException e) {
				logger.warn("在" + serviceFileUnit.getGpsTime() + "读 轨迹文件" + vid + ".txt 找不到." + e.getMessage());
				FileUtil.coverFolder(trackfileurl);
			} catch (IOException e) {
				logger.error("在" + serviceFileUnit.getGpsTime() + "写入轨迹文件" + vid + ".txt"  + e.getMessage());
			}finally{
				if(rf != null){
					try {
						rf.close();
					} catch (IOException e) {
						logger.error("在" + serviceFileUnit.getGpsTime() + "关闭轨迹文件" + vid + ".txt 找不到." + e.getMessage());
					}
				}
			}
			String alarmPath = alarmFileUrl + "/" + year + "/" + month + "/" + day + "/" + vid + ".txt";

			RandomAccessFile rfAlarm = null;
			try {
				rfAlarm = new RandomAccessFile(alarmPath, "rw");
				rfAlarm.seek(rfAlarm.length());// 将指针移动到文件末尾
				rfAlarm.writeBytes(serviceFileUnit.getAlarmfilecontent());
				//logger.debug(vid + "写入报警文件成功");
			} catch (FileNotFoundException e) {
				logger.warn("在" + serviceFileUnit.getGpsTime() + "读 报警文件" + vid + ".txt 找不到." + e.getMessage());
				FileUtil.coverFolder(alarmFileUrl);
			} catch (IOException e) {
				logger.error("在" + serviceFileUnit.getGpsTime() + "写入报警文件" + vid + ".txt" + e.getMessage());
			}finally{
				if(rfAlarm != null){
					try {
						rfAlarm.close();
					} catch (IOException e) {
						logger.error("在" + serviceFileUnit.getGpsTime() + "关闭报警文件" + vid + ".txt 找不到." + e.getMessage());
					}
				}
			}
			serviceFileUnit.resetRecordCount();
		}// End while
	}
}
