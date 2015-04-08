package com.caits.analysisserver.addin.kcpt.addin.unifieddispatch;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.sql.Types;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.TreeMap;
import java.util.Vector;
import java.util.concurrent.ArrayBlockingQueue;

//import oracle.jdbc.driver.OraclePreparedStatement;


import oracle.jdbc.OraclePreparedStatement;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.addin.UnifiedFileDispatch;
import com.caits.analysisserver.bean.DataBean;
import com.caits.analysisserver.bean.StaPool;
import com.caits.analysisserver.bean.VehicleAlarm;
import com.caits.analysisserver.bean.VehicleAlarmEvent;
import com.caits.analysisserver.bean.VehicleInfo;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.DBAdapter;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.FileUtils;
import com.caits.analysisserver.utils.Utils;
import com.ctfo.generator.pk.GeneratorPK;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;
/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： AnalysisAlarmThread <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2011-10-18</td>
 * <td>刘志伟</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author 刘志伟
 * @since JDK1.6
 * @ Description: 用于统计车辆报警信息
 */
@SuppressWarnings("unused")
public class AnalysisAlarmThread extends UnifiedFileDispatch {
	private static final Logger logger = LoggerFactory.getLogger(AnalysisAlarmThread.class);
	
	private ArrayBlockingQueue<File> vPacket = new ArrayBlockingQueue<File>(100000);
	
	private final String keyWord = "track";
	
	private final String keyWords = "alarm";
	
	private long  serverRunTime=0l;
	
	private Vector<VehicleAlarmEvent> alarmEventList = new Vector<VehicleAlarmEvent>(); //报警事件

	private Map<String,Long> maxSpeedMap = new HashMap<String,Long>(); // 存储持续报警周中最大速度

	// 报警分析文件目录
	private String alarmFileUrl = null;
	
	// 驾驶行为事件目录
	private String eventFileUrl = null;
	
	private String saveVehicleAlarmInfo = null;
	
	private String procSaveVehicleAlarmInfo = null;
	
	private String saveVehicleAlarmEventInfo = null;
	
	private String sql_selectAlarmEvent = null; //查询车报警事件表
	
	private String saveDriverEventInfo = null;
	private String deleteDriverEventInfo = null;
	
	private Connection dbCon = null;
	
	private Map<String,VehicleAlarm> alarmSatDayMap = null; // 报警日统计存储列表
	
	private int threadId = 0;
	
	private long utc = 0; // 统计时间
	
	public void run() {	
		logger.info("报警统计分析线程" + this.threadId + "启动");
		
		while(true){
			File alarFile = null;
			try {
				File file = vPacket.take();
				
				//核实文件是否存在
				logger.info( "Thread Id : "+ this.threadId + ", alarm : " + file.getName());
				String filePath = FileUtils.replaceRoot(file.getPath(), alarmFileUrl,keyWord);
				alarFile = new File(filePath);
				alarmSatDayMap = new HashMap<String,VehicleAlarm>(); // 报警日统计存储列表
				if(alarFile.exists()){
					//从连接池获取连接
					dbCon = OracleConnectionPool.getConnection();
					analysisAlarmFile(alarFile);
				}
		
			} catch (Exception e) {
				logger.error(" 报警统计操作出错,文件：" + alarFile.getAbsolutePath() ,e);
			} finally{
				if(alarFile != null && alarFile.exists()){
					String vid = alarFile.getName().replaceAll("\\.txt", "");
					StaPool.addAlarm(vid,alarmSatDayMap);
				}
				//清空缓存
				alarmEventList.clear();
				try {
					if(dbCon != null){
						dbCon.close();
					}
				} catch (SQLException e) {
					logger.error("连接放回连接池出错.",e);
				}
			}
		}// End while	
	}
	
	public void analysisAlarm(String filePath,Long utc) {
			this.utc = utc;
			logger.info("报警统计分析线程" + this.threadId + "启动");
			File alarFile = null;
			try {
				String alarmFilePath = FileUtils.replaceRoot(filePath, alarmFileUrl,keyWord);
				alarFile = new File(alarmFilePath);
				alarmSatDayMap = new HashMap<String,VehicleAlarm>(); // 报警日统计存储列表
				if(alarFile.exists()){
					analysisAlarmFile(alarFile);
				}
		
			} catch (Exception e) {
				logger.error(" 报警统计操作出错,文件：" + alarFile.getAbsolutePath() ,e);
			} finally{
				if(alarFile != null && alarFile.exists()){
					String vid = alarFile.getName().replaceAll("\\.txt", "");
					StaPool.addAlarm(vid,alarmSatDayMap);
				}
				//清空缓存
				alarmEventList.clear();
			}
	}

	public void addPacket(File file) {
		try {
			vPacket.put(file);
		} catch (InterruptedException e) {
			logger.error("统计主线程添加file文件到队列出错",e);
		}
	}
	
	/**
	 * 初始化报警统计线程 
	 * @param nodeName
	 * @throws Exception
	 */
	public void initAnalyser() {
		alarmFileUrl = FilePool.getinstance().getFile(this.utc,"alarmfileurl");
		eventFileUrl = FilePool.getinstance().getFile(this.utc,"eventfileurl");
		//存储车辆报警日统计信息
		//saveVehicleAlarmInfo = SQLPool.getinstance().getSql("sql_saveVehicleAlarmInfo");
		
		procSaveVehicleAlarmInfo=SQLPool.getinstance().getSql("sql_procSaveVehicleAlarmInfo");
		//存储报警事件统计信息
		saveVehicleAlarmEventInfo = SQLPool.getinstance().getSql("sql_saveVehicleAlarmEventInfo"); 
		//查询车辆报警明细表
		//sql_selectAlarmEvent = SQLPool.getinstance().getSql("sql_selectAlarmEvent");

		//存储驾驶行为事件统计信息
		saveDriverEventInfo = SQLPool.getinstance().getSql("sql_saveDriverEventInfo");
		
		//删除驾驶行为事件统计信息
		deleteDriverEventInfo = SQLPool.getinstance().getSql("sql_deleteDriverEventInfo");
	}
	
	public void setDBCon(Connection dbCon) {
		this.dbCon = dbCon;
	}
	
	/***
	 * 统计分析报警信息
	 * @param file
	 * @throws IOException 
	 * @throws SQLException 
	 * @throws SQLException 
	 */
	private void analysisAlarmFile(File file) throws IOException, SQLException{
		long serverStartTime=System.currentTimeMillis();
		String vid = file.getName().replaceAll("\\.txt", "");
		VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
		if(info == null){
			return;
		}
		BufferedReader buf = null;
		TreeMap<Long, String> returnAlarmMap = null;
		
		Map<String, String[]> chMap = new HashMap<String, String[]>();
		Map<String, String[]> temp = new HashMap<String, String[]>();
		
		try{
			buf = new BufferedReader(new FileReader(file));
			String[] cols = null;
			// 获取报警文件并按GPS时间排序，过滤重复数据
			returnAlarmMap = getAlarmMap(buf);
			Long s = null;
			int count = 0;
			String alarms = null;
			if(returnAlarmMap != null && returnAlarmMap.size() > 0){
				Set<Long> key = returnAlarmMap.keySet();
				for (Iterator<Long> it = key.iterator(); it.hasNext();) {
					try{
		            s = it.next();
		                        
		            // 报警文件每行数据按：分割成数组
		            cols = returnAlarmMap.get(s).split(":");     
		            
		            // 读到最后一行，跳出循环
		            if(count == returnAlarmMap.size()-1) {
		            	break;
		            }
		            
		            String alarmCode = cols[0];
		            if(alarmCode.length() > 1){
		            	if(alarmCode.contains(","))
		            	alarms = alarmCode.substring(1, alarmCode.lastIndexOf(","));
		            }
		            	            
		            if (chMap.size() == 0) {
		            		            	
		            	if(alarmCode.length() > 1){	   
	
			            	// 报警类型数组
			            	String[] as = alarms.split(",");
			            	
		            		for (String code : as) {
								if(code != null && !"".equals(code)){
									chMap.put(vid + "_" + code, cols);
									maxSpeedMap.put(vid + "_" + code, (Long)formatValueByType(cols[6],"0",'L'));
								}
							}// End for 
		            	}
		            	
		            } else {
	            		// 如果当前报警数据没有报警，需要结束报警
	            		if(alarmCode.length() == 1){
	            			
	            			Set<String> keycode = chMap.keySet();
	            			for (Iterator<String> its = keycode.iterator(); its.hasNext();) {
	            				String cd = its.next();
	            				
	            				//报警事件统计
	            				VehicleAlarmEvent event = new VehicleAlarmEvent();
	            				String[] startCol = chMap.get(cd);
	            				String alarmCd = cd.substring(cd.indexOf("_")+1);
	            				
	            				event.setAlarmCode(alarmCd);
								event.setVid(vid);
								event.setPhoneNumber(info.getCommaddr());   
								event.setAREA_ID("");// 电子围栏编号
								event.setAlarmType(AnalysisDBAdapter.alarmTypeMap.get(alarmCd));
								event.setMtypeCode("");
								event.setMediaUrl("");
								event.setStartUtc(CDate.stringConvertUtc(startCol[5]));
								event.setStartLat((Long)formatValueByType(startCol[4],"-1",'L'));
								event.setStartLon((Long)formatValueByType(startCol[3],"-1",'L'));
								event.setStartMapLat((Long)formatValueByType(startCol[2],"-1",'L'));
								event.setStartMapLon((Long)formatValueByType(startCol[1],"-1",'L'));
								if(startCol[11] != null && !startCol[11].equals("")){
									event.setStartElevation((Integer)formatValueByType(startCol[11],"0",'I'));
								}
								if(startCol[7] != null && !startCol[7].equals("")){
									event.setStartHead((Integer)formatValueByType(startCol[7],"0",'I'));
								}
								event.setStartGpsSpeed((Long)formatValueByType(startCol[6],"0",'L'));
								event.setEndUtc(CDate.stringConvertUtc(cols[5]));
								event.setEndLat((Long)formatValueByType(cols[4],"-1",'L'));
								event.setEndLon((Long)formatValueByType(cols[3],"-1",'L'));
								event.setEndMapLat((Long)formatValueByType(cols[2],"-1",'L'));
								event.setEndMapLon((Long)formatValueByType(cols[1],"-1",'L'));
								if(!cols[11].equals("")){
									event.setEndElevation(Integer.parseInt(cols[11]));
								}
								event.setEndHead((Integer)formatValueByType(cols[7],"0",'I'));
								event.setEndGpsSpeed((Long)formatValueByType(cols[6],"0",'L'));
								long alarmTime = (event.getEndUtc() - event.getStartUtc())/1000;
								// 报警事件时长
								if(alarmTime > 0){
									event.setAccountTime(alarmTime);
								}
								// 核查结束时是否最大速度
								if(maxSpeedMap.get(cd) < (Integer)formatValueByType(cols[6],"0",'I')){
									event.setMaxSpeed((Long)formatValueByType(cols[6],"0",'L'));
								} else {
									event.setMaxSpeed(maxSpeedMap.get(cd));
								}
								event.setAlarmSrc(1);//修改为：车机告警。
								// 计算里程和油耗
								accountOilAndMelige(cols,startCol,event);
								alarmEventList.add(event);
								
	            			}// End for
	            			         			
	            		} else {
	            			
	            			// 报警类型数组
			            	String[] as = alarms.split(",");
			            	
			            	for (String code : as) {
			            		// 持续报警
			            		if(chMap.containsKey(vid + "_" + code)) {
			            			temp.put(vid + "_" + code, chMap.get(vid + "_" + code));
			            			if(maxSpeedMap.get(vid + "_" + code) < (Long)formatValueByType(cols[6],"0",'L')) {
			            				maxSpeedMap.put(vid + "_" + code, (Long)formatValueByType(cols[6],"0",'L'));
			            			}
			            		} else {
			            			// 保存最新报警编码
			            			temp.put(vid + "_" + code, cols);	
			            			maxSpeedMap.put(vid + "_" + code, (Long)formatValueByType(cols[6],"0",'L'));
			            		}		            			            		
			            	}// End for	
			            	
			            	// 根据最新报警编码map，查询结束的报警
		            		Set<String> keycode = chMap.keySet();
		            		
		            		for (Iterator<String> its = keycode.iterator(); its.hasNext();) {
		            			String cd = its.next();
		            			
		            			if(!temp.containsKey(cd)) {
		            				String[] startCol = chMap.get(cd);
		            				String alarmCd = cd.substring(cd.indexOf("_")+1);
		            				
		            				//报警事件统计
		            				VehicleAlarmEvent event = new VehicleAlarmEvent();

		            				event.setAlarmCode(alarmCd);
									event.setVid(vid);
									event.setPhoneNumber(info.getCommaddr());   
									event.setAREA_ID("");// 电子围栏编号
									event.setAlarmType(AnalysisDBAdapter.alarmTypeMap.get(alarmCd));
									event.setMtypeCode("");
									event.setMediaUrl("");
									event.setStartUtc(CDate.stringConvertUtc(startCol[5]));
									event.setStartLat((Long)formatValueByType(startCol[4],"-1",'L'));
									event.setStartLon((Long)formatValueByType(startCol[3],"-1",'L'));
									event.setStartMapLat((Long)formatValueByType(startCol[2],"-1",'L'));
									event.setStartMapLon((Long)formatValueByType(startCol[1],"-1",'L'));
									if(startCol[11] != null && !startCol[11].equals("")){
										event.setStartElevation((Integer)formatValueByType(startCol[11],"0",'I'));
									}
									event.setStartHead((Integer)formatValueByType(startCol[7],"0",'I'));
									event.setStartGpsSpeed((Long)formatValueByType(startCol[6],"0",'L'));
									event.setEndUtc(CDate.stringConvertUtc(cols[5]));
									event.setEndLat((Long)formatValueByType(cols[4],"-1",'L'));
									event.setEndLon((Long)formatValueByType(cols[3],"-1",'L'));
									event.setEndMapLat((Long)formatValueByType(cols[2],"-1",'L'));
									event.setEndMapLon((Long)formatValueByType(cols[1],"-1",'L'));
									if(cols[11] != null && !cols[11].equals("")){
										event.setEndElevation((Integer)formatValueByType(cols[11],"0",'I'));
									}
									event.setEndHead((Integer)formatValueByType(cols[7],"0",'I'));
									event.setEndGpsSpeed((Long)formatValueByType(cols[6],"0",'L'));
									long alarmTime = (event.getEndUtc() - event.getStartUtc())/1000;
									// 报警事件时长
									if(alarmTime > 0){
										event.setAccountTime(alarmTime);
									}
									// 核查结束时是否最大速度
									if(maxSpeedMap.get(cd) < (Integer)formatValueByType(cols[6],"0",'I')){
										event.setMaxSpeed((Long)formatValueByType(cols[6],"0",'L'));
									} else {
										event.setMaxSpeed(maxSpeedMap.get(cd));
									}
									
									event.setAlarmSrc(2);
									// 计算里程和油耗
									accountOilAndMelige(cols,startCol,event);
									
									alarmEventList.add(event);													
		            			}
		            		}
	            			
	            		}	            		
	      		        
	            		
 		            	// 生成最新的map
		            	chMap.clear();
		            	chMap.putAll(temp);
		            	temp.clear();     
		            }
					}catch(Exception ex){
						logger.error("分析告警数据过程中出错!VID="+vid+" "+returnAlarmMap.get(s));
					}
		            count++;
				} //for循环结束
				
				long serverEndTime=System.currentTimeMillis();
        		serverRunTime=serverRunTime+(serverEndTime-serverStartTime)/1000;
        		String message ="-------------解析告警文件时长:"+(serverEndTime-serverStartTime)/1000+"s";
        		logger.info(message);
        		
				if(cols != null){
					intterputAlarm(alarmSatDayMap,chMap,info,vid,cols);
				}

				// 读取驾驶行为事件文件,将超速和疲劳驾驶报警放在对应的map里
				String eventFile = FileUtils.replaceRoot(file.getPath(), eventFileUrl, keyWords);
				try{
					readEventFile(eventFile,vid,info,returnAlarmMap);
				}catch(Exception ex){
					logger.error("日报警统计，统计报警事件出错."+vid,ex);
				}

				//先存储报警事件明细表
				if(alarmEventList.size() > 0){
					try{
						saveEventInfo(info,alarmEventList);
					}catch(SQLException e){
						logger.error("车辆编号" + vid + "统计报警事件出错.",e);
					}
				}
				
				//通过查询报警事件明细表统计报警日表。
				try {
					saveAlarmInfo(info,vid);
				} catch (SQLException e) {
					logger.error( "车辆编号" + vid + " 统计报警日统计出错.",e);
				}
			}
		}finally{
			if(returnAlarmMap != null && returnAlarmMap.size() > 0){
				returnAlarmMap.clear();
			}
			
			if(buf != null){
				buf.close();
			}
		}
	}
	
	/**
	 * 强制结束未结束的持续报警
	 * @param alarmSatDayMap
	 * @param info
	 * @param vid
	 */
	private void intterputAlarm(Map<String,VehicleAlarm> alarmSatDayMap,Map<String,String[]> chMap,VehicleInfo info,String vid,String[] col){
		
		// 最后一行与chMap比较，如果存在的需要添加累计里程等数据。
		Set<String> key = chMap.keySet();		
		// 最后一行
		String lastCode = "";
		String[] tempCode = null;
		String[] mapCode = null;
		
		// 如果有上报的报警信息
		if(col[0].length() > 1){
			lastCode = col[0].substring(1, col[0].lastIndexOf(","));	
			
			tempCode = lastCode.split(",");
			
			// 最后一行都要结束的报警
			for (String code : tempCode) {
				if(code == null || "".equals(code)){
					continue;
				}
				int count = 0;
				code = vid+"_"+code;
				if(chMap.containsKey(code)) {
					count++;
				}
				if(count==0){
					// 报警事件统计
		            VehicleAlarmEvent event = new VehicleAlarmEvent();
					
		            String alarmCd = code.substring(code.indexOf("_")+1);
					event.setAlarmCode(alarmCd);
					event.setVid(vid);
					event.setPhoneNumber(info.getCommaddr());   
					event.setAREA_ID("");// 电子围栏编号
					event.setAlarmType(AnalysisDBAdapter.alarmTypeMap.get(alarmCd));
					event.setMtypeCode("");
					event.setMediaUrl("");
					event.setStartUtc(CDate.stringConvertUtc(col[5]));
					event.setStartLat((Long)formatValueByType(col[4],"-1",'L'));
					event.setStartLon((Long)formatValueByType(col[3],"-1",'L'));
					event.setStartMapLat((Long)formatValueByType(col[2],"-1",'L'));
					event.setStartMapLon((Long)formatValueByType(col[1],"-1",'L'));
					if(col[11] != null && !col[11].equals("")&&!"null".equals(col[11])){
						event.setStartElevation(Integer.parseInt(col[11]));
					}
					event.setStartHead((Integer)formatValueByType(col[7],"0",'I'));
					event.setStartGpsSpeed((Long)formatValueByType(col[6],"0",'L'));
					event.setEndUtc(CDate.stringConvertUtc(col[5]));
					event.setEndLat((Long)formatValueByType(col[4],"-1",'L'));
					event.setEndLon((Long)formatValueByType(col[3],"-1",'L'));
					event.setEndMapLat((Long)formatValueByType(col[2],"-1",'L'));
					event.setEndMapLon((Long)formatValueByType(col[1],"-1",'L'));
					if(col[11] != null && !col[11].equals("")&&!"null".equals(col[11])){
						event.setEndElevation(Integer.parseInt(col[11]));
					}
					event.setEndHead((Integer)formatValueByType(col[7],"0",'I'));
					event.setEndGpsSpeed((Long)formatValueByType(col[6],"0",'L'));
					long alarmTime = (event.getEndUtc() - event.getStartUtc())/1000;
					
					// 报警事件时长
					event.setAccountTime(alarmTime);

					// 核查结束时是否最大速度
					event.setMaxSpeed(Long.parseLong(col[6]));			
					// 计算里程和油耗
					accountOilAndMelige(col,col,event);
					
					alarmEventList.add(event);
					
				} else {
					// 报警事件统计
		            VehicleAlarmEvent event = new VehicleAlarmEvent();
					
		            mapCode = chMap.get(code);	    
		            
		            String alarmCd = code.substring(code.indexOf("_")+1);
					event.setAlarmCode(alarmCd);
					event.setVid(vid);
					event.setPhoneNumber(info.getCommaddr());   
					event.setAREA_ID("");// 电子围栏编号
					event.setAlarmType(AnalysisDBAdapter.alarmTypeMap.get(alarmCd));
					event.setMtypeCode("");
					event.setMediaUrl("");
					event.setStartUtc(CDate.stringConvertUtc(mapCode[5]));
					event.setStartLat(Long.parseLong(mapCode[4]));
					event.setStartLon(Long.parseLong(mapCode[3]));
					event.setStartMapLat(Long.parseLong(mapCode[2]));
					event.setStartMapLon(Long.parseLong(mapCode[1]));
					if(mapCode[11] != null && !mapCode[11].equals("")&&!"null".equals(mapCode[11])){
						event.setStartElevation(Integer.parseInt(mapCode[11]));
					}
					event.setStartHead((Integer)formatValueByType(mapCode[7],"0",'I'));
					event.setStartGpsSpeed((Long)formatValueByType(mapCode[6],"0",'L'));
					event.setEndUtc(CDate.stringConvertUtc(col[5]));
					event.setEndLat((Long)formatValueByType(col[4],"-1",'L'));
					event.setEndLon((Long)formatValueByType(col[3],"-1",'L'));
					event.setEndMapLat((Long)formatValueByType(col[2],"-1",'L'));
					event.setEndMapLon((Long)formatValueByType(col[1],"-1",'L'));
					if(col[11] != null && !col[11].equals("")&&!"null".equals(col[11])){
						event.setEndElevation(Integer.parseInt(col[11]));
					}
					event.setEndHead((Integer)formatValueByType(col[7],"0",'I'));
					event.setEndGpsSpeed((Long)formatValueByType(col[6],"0",'L'));
					long alarmTime = (event.getEndUtc() - event.getStartUtc())/1000;
					
					// 报警事件时长
					if(alarmTime > 0){
						event.setAccountTime(alarmTime);
					}
					// 核查结束时是否最大速度
					if(maxSpeedMap.get(code) < (Integer)formatValueByType(col[6],"0",'I')){
						event.setMaxSpeed((Long)formatValueByType(col[6],"0",'L'));
					} else {
						event.setMaxSpeed(maxSpeedMap.get(code));
					}
					// 计算里程和油耗
					accountOilAndMelige(col,mapCode,event);
					alarmEventList.add(event);
				}	
				
			} //for end
		}
			
		// chMap必须要结束的报警		
		String endCode = null;
		String[] endMapCode = null;
		int num = 0;
		
		for (Iterator<String> it = key.iterator(); it.hasNext();) {		
			endCode = it.next();
			endMapCode = chMap.get(endCode);					
			
			if(tempCode != null){
				for (String code : tempCode) {
					code = vid+"_"+code;
					if(endCode.equals(code)){
						num++;
					}				
				}// End for
			}
			
			if(num == 0){
				
				// 报警事件统计
	            VehicleAlarmEvent event = new VehicleAlarmEvent();
				String alarmcode = endCode.substring(endCode.indexOf("_")+1);            
				event.setAlarmCode(alarmcode);
				event.setVid(vid);
				event.setPhoneNumber(info.getCommaddr());   
				event.setAREA_ID("");// 电子围栏编号
				event.setAlarmType(AnalysisDBAdapter.alarmTypeMap.get(alarmcode));
				event.setMtypeCode("");
				event.setMediaUrl("");
				event.setStartUtc(CDate.stringConvertUtc(endMapCode[5]));
				event.setStartLat((Long)formatValueByType(endMapCode[4],"-1",'L'));
				event.setStartLon((Long)formatValueByType(endMapCode[3],"-1",'L'));
				event.setStartMapLat((Long)formatValueByType(endMapCode[2],"-1",'L'));
				event.setStartMapLon((Long)formatValueByType(endMapCode[1],"-1",'L'));
				if(endMapCode[11] != null && !endMapCode[11].equals("")&&!"null".equals(endMapCode[11])){
					event.setStartElevation(Integer.parseInt(endMapCode[11]));
				}
				event.setStartHead((Integer)formatValueByType(endMapCode[7],"0",'I'));
				event.setStartGpsSpeed((Long)formatValueByType(endMapCode[6],"0",'L'));
				event.setEndUtc(CDate.stringConvertUtc(col[5]));
				event.setEndLat((Long)formatValueByType(col[4],"-1",'L'));
				event.setEndLon((Long)formatValueByType(col[3],"-1",'L'));
				event.setEndMapLat((Long)formatValueByType(col[2],"-1",'L'));
				event.setEndMapLon((Long)formatValueByType(col[1],"-1",'L'));
				if(col[11] != null && !col[11].equals("")&&!"null".equals(col[11])){
					event.setEndElevation(Integer.parseInt(col[11]));
				}
				event.setEndHead((Integer)formatValueByType(col[7],"0",'I'));
				event.setEndGpsSpeed((Long)formatValueByType(col[6],"0",'L'));
				long alarmTime = (event.getEndUtc() - event.getStartUtc())/1000;
				
				// 报警事件时长
				if(alarmTime > 0){
					event.setAccountTime(alarmTime);
				}
				// 核查结束时是否最大速度
				if(maxSpeedMap.get(endCode) < (Integer)formatValueByType(col[6],"0",'I')){
					event.setMaxSpeed((Long)formatValueByType(col[6],"0",'L'));
				} else {
					event.setMaxSpeed(maxSpeedMap.get(endCode));
				}
				// 计算里程和油耗
				accountOilAndMelige(col,endMapCode,event);
				alarmEventList.add(event);
			}
		}// End for
	}
	
	/**
	 * 存储报警事件统计信息
	 * @param alarmEventList
	 * @throws SQLException
	 * 
	 */
	private void saveEventInfo(VehicleInfo info, Vector<VehicleAlarmEvent> alarmEventList) throws SQLException{

		PreparedStatement stSaveVehicleAlarmEventInfo = null;
		try{
			stSaveVehicleAlarmEventInfo = dbCon.prepareStatement(saveVehicleAlarmEventInfo);
			Iterator<VehicleAlarmEvent> eventList = alarmEventList.iterator();
			int count = 0;
			while(eventList.hasNext()){
				VehicleAlarmEvent v = eventList.next();
				if(v.getAlarmCode() == null || v.getAlarmCode().equals("")){
					continue;
				}
				stSaveVehicleAlarmEventInfo.setString(1, GeneratorPK.instance().getPKString());
				stSaveVehicleAlarmEventInfo.setString(2, v.getVid());
				stSaveVehicleAlarmEventInfo.setString(3, v.getPhoneNumber());
				stSaveVehicleAlarmEventInfo.setString(4, v.getAlarmCode());
				
				if(v.getAREA_ID() != null && !"".equals(v.getAREA_ID())){
					stSaveVehicleAlarmEventInfo.setString(5, v.getAREA_ID());
				}else{
					stSaveVehicleAlarmEventInfo.setNull(5, Types.VARCHAR);
				}
				
				if(v.getMtypeCode() != null){
					stSaveVehicleAlarmEventInfo.setString(6, v.getMtypeCode());
				}else{
					stSaveVehicleAlarmEventInfo.setString(6, null);
				}
				
				if(v.getMediaUrl() != null){
					stSaveVehicleAlarmEventInfo.setString(7, v.getMediaUrl());
				}else{
					stSaveVehicleAlarmEventInfo.setString(7, null);
				}
				
				stSaveVehicleAlarmEventInfo.setLong(8, v.getStartUtc());
				if (v.getStartLat()>=0){
					stSaveVehicleAlarmEventInfo.setLong(9,v.getStartLat());
				}else{
					stSaveVehicleAlarmEventInfo.setNull(9,Types.NULL);
				}
				if (v.getStartLon()>=0){
					stSaveVehicleAlarmEventInfo.setLong(10, v.getStartLon());
				}else{
					stSaveVehicleAlarmEventInfo.setNull(10, Types.NULL);
				}
				if (v.getStartMapLat()>=0){
					stSaveVehicleAlarmEventInfo.setLong(11, v.getStartMapLat());
				}else{
					stSaveVehicleAlarmEventInfo.setNull(11, Types.NULL);
				}
				if (v.getStartMapLon()>=0){
					stSaveVehicleAlarmEventInfo.setLong(12, v.getStartMapLon());
				}else{
					stSaveVehicleAlarmEventInfo.setNull(12, Types.NULL);
				}
				
				stSaveVehicleAlarmEventInfo.setInt(13, v.getStartElevation());
				stSaveVehicleAlarmEventInfo.setInt(14, v.getStartHead());
				stSaveVehicleAlarmEventInfo.setLong(15, v.getStartGpsSpeed());
				stSaveVehicleAlarmEventInfo.setLong(16, v.getEndUtc());
				if (v.getEndLat()>=0){
					stSaveVehicleAlarmEventInfo.setLong(17,v.getEndLat());
				}else{
					stSaveVehicleAlarmEventInfo.setNull(17,Types.NULL);
				}
				if (v.getEndLon()>=0){
					stSaveVehicleAlarmEventInfo.setLong(18, v.getEndLon());
				}else{
					stSaveVehicleAlarmEventInfo.setNull(18, Types.NULL);
				}
				if (v.getEndMapLat()>=0){
					stSaveVehicleAlarmEventInfo.setLong(19, v.getEndMapLat());
				}else{
					stSaveVehicleAlarmEventInfo.setNull(19, Types.NULL);
				}
				if (v.getEndMapLon()>=0){
					stSaveVehicleAlarmEventInfo.setLong(20, v.getEndMapLon());
				}else{
					stSaveVehicleAlarmEventInfo.setNull(20, Types.NULL);
				}
				
				stSaveVehicleAlarmEventInfo.setInt(21, v.getEndElevation());
				stSaveVehicleAlarmEventInfo.setInt(22, v.getEndHead());
				stSaveVehicleAlarmEventInfo.setLong(23, v.getEndGpsSpeed());
				stSaveVehicleAlarmEventInfo.setDouble(24, v.getAccountTime());
				stSaveVehicleAlarmEventInfo.setLong(25, v.getMaxSpeed());
				
				if(info.getVlineId()  != null && !"".equals(info.getVlineId())){
					stSaveVehicleAlarmEventInfo.setString(26, info.getVlineId());
				}else{
					stSaveVehicleAlarmEventInfo.setNull(26, Types.VARCHAR);
				}
				stSaveVehicleAlarmEventInfo.setString(27, info.getInnerCode());
				stSaveVehicleAlarmEventInfo.setString(28, info.getVehicleNo());
				stSaveVehicleAlarmEventInfo.setLong(29, v.getMileage());
				stSaveVehicleAlarmEventInfo.setLong(30, v.getCostOil());
				stSaveVehicleAlarmEventInfo.setString(31, info.getVinCode());
				
				if(info.getLineName() != null){
					stSaveVehicleAlarmEventInfo.setString(32, info.getLineName());
				}else{
					stSaveVehicleAlarmEventInfo.setString(32, null);
				}
				
				stSaveVehicleAlarmEventInfo.setString(33, info.getEntId());
				stSaveVehicleAlarmEventInfo.setString(34, info.getEntName());
				stSaveVehicleAlarmEventInfo.setString(35, info.getTeamId());
				stSaveVehicleAlarmEventInfo.setString(36, info.getTeamName());
				stSaveVehicleAlarmEventInfo.setInt(37, v.getAlarmSrc()==0?1:v.getAlarmSrc());
				
				stSaveVehicleAlarmEventInfo.addBatch();
				
				count++;
				if(count % 100 == 0){
					stSaveVehicleAlarmEventInfo.executeBatch();
					stSaveVehicleAlarmEventInfo.clearBatch();
					count = 0;
				}
			}//End while
			
			if(count > 0){
				stSaveVehicleAlarmEventInfo.executeBatch();
				stSaveVehicleAlarmEventInfo.clearBatch();
			}
		}catch(SQLException e){
			logger.error(info.getVehicleNo() + " 存储报警事件统计信息出错.",e);
		}finally{
			if(alarmEventList.size() > 0){
				alarmEventList.clear(); //清空内存
			}
			
			if(stSaveVehicleAlarmEventInfo != null){
				stSaveVehicleAlarmEventInfo.close();
			}
		}
	}
	
	/**
	 * 存储日统计报警信息
	 * @param info
	 * @param alarmMap
	 * @param vid
	 * @throws SQLException
	 */
	private void saveAlarmInfo(VehicleInfo info,String vid) throws SQLException{
		System.out.println("----存储车辆告警日统计信息---saveAlarmInfo--start");
		//PreparedStatement stSaveVehicleAlarmInfo = null;
		//PreparedStatement stSelectAlarmEvent = null;
		CallableStatement dbCstmt = null;
		Connection dbConnection = null;
 	//	ResultSet rs = null;
		try{
		//	stSelectAlarmEvent=dbCon.prepareCall("{call insert_ts_alarm_daystat("+vid+","+this.utc+","+this.utc + 24 * 60 * 60 * 1000+")}");
		//	stSelectAlarmEvent.execute();
		// 获得Connection对象
		   dbConnection = OracleConnectionPool.getConnection();
		  if (dbConnection!=null){
			dbCstmt = dbConnection.prepareCall(procSaveVehicleAlarmInfo);
 			dbCstmt.setString(1, vid);
			dbCstmt.setLong(2,this.utc);
			dbCstmt.setLong(3, this.utc + 24 * 60 * 60 * 1000); 
			dbCstmt.registerOutParameter(4, Types.INTEGER); 
			dbCstmt.execute();
		  }
		  int successTag = dbCstmt.getInt(4); 
			
			if (successTag==1){
				logger.debug(vid + " 存储日统计报警信息成功！");
			}else{
				logger.debug(vid + " 存储日统计报警信息出错!");
			}
			
		/*  stSelectAlarmEvent = dbCon.prepareStatement(sql_selectAlarmEvent);
			stSaveVehicleAlarmInfo = dbCon.prepareStatement(saveVehicleAlarmInfo);
			stSelectAlarmEvent.setString(1, vid);
			stSelectAlarmEvent.setLong(2, this.utc);
			stSelectAlarmEvent.setLong(3, this.utc + 24 * 60 * 60 * 1000);
			rs = stSelectAlarmEvent.executeQuery();
		 		
			while(rs.next()){
				String alarmCode = rs.getString("ALARM_CODE");
				stSaveVehicleAlarmInfo.setLong(1, this.utc + 12 * 60 * 60 * 1000);
				stSaveVehicleAlarmInfo.setString(2, vid);
				stSaveVehicleAlarmInfo.setString(3, info.getEntId());
				stSaveVehicleAlarmInfo.setString(4, info.getEntName());
				stSaveVehicleAlarmInfo.setString(5, info.getTeamId());
				stSaveVehicleAlarmInfo.setString(6, info.getTeamName());
				stSaveVehicleAlarmInfo.setString(7, info.getVehicleNo());
				stSaveVehicleAlarmInfo.setString(8,info.getVinCode());
				stSaveVehicleAlarmInfo.setString(9,alarmCode);
				stSaveVehicleAlarmInfo.setInt(10, rs.getInt("NUM"));
				stSaveVehicleAlarmInfo.setString(11, getAlarmType(alarmCode.trim()));
				stSaveVehicleAlarmInfo.setLong(12, rs.getLong("TIME"));
				stSaveVehicleAlarmInfo.setLong(13, rs.getLong("MILEAGE"));
				stSaveVehicleAlarmInfo.setLong(14, rs.getLong("OIL_WEAR"));
				if(info.getVlineId() != -1){
					stSaveVehicleAlarmInfo.setString(15, info.getVlineId());
				}else{
					stSaveVehicleAlarmInfo.setNull(15, Types.INTEGER);
				}
				
				if(info.getLineName() != null){
					stSaveVehicleAlarmInfo.setString(16, info.getLineName());
				}else{
					stSaveVehicleAlarmInfo.setString(16, null);
				}
				stSaveVehicleAlarmInfo.addBatch();
			}// End while
			stSaveVehicleAlarmInfo.executeBatch();
			*/
			System.out.println("----存储车辆告警日统计信息---saveAlarmInfo--end----");
		}catch(SQLException e){
			logger.error(vid + " 存储日统计报警信息出错",e);
		}finally{
			
			/*	if(stSelectAlarmEvent != null){
				stSelectAlarmEvent.close();
			}
		    if(rs != null){
				rs.close();
			}
		 	if(stSaveVehicleAlarmInfo != null){
				stSaveVehicleAlarmInfo.close();
			}
			*/
			try {
				if(dbCstmt != null){
					dbCstmt.close();
				}
				if(dbConnection != null){
					dbConnection.close();
				}
			} catch (SQLException e) {
				logger.error("连接放回连接池出错.",e);
			}
			
		}
	}
	
	/**
	 * 根据gps时间将读取的报警文件数据进行排序
	 */
	private TreeMap<Long, String> getAlarmMap(BufferedReader buf) {
		TreeMap<Long, String> returnAlarmMap = new TreeMap<Long, String>();
		String readLine = null;
		String gpsdate = null;
		String[] alarm = null;
		List<DataBean> list = new ArrayList<DataBean>();
		try {
			while ((readLine = buf.readLine()) != null) {
				
				// 报警文件每行的数据分割
				alarm = readLine.split(":");
				
				if(alarm.length == 14) {
					
					gpsdate = alarm[5];

					addList(readLine,gpsdate,list); // 按GPS时间排序
					
				}	
			}// End while
			
			sortList(list); // 集合按GPS UTC时间排序
			Utils.clearDuplicateRecord(list, returnAlarmMap); // 滤过GPS 开始时间重复记录
		} catch (Exception e) {
			logger.error("读取报警文件信息出错",e);
		}finally{
			if(list.size() > 0){
				list.clear();
			}
		}
		
		return returnAlarmMap;
	}	
	
	/**
	 * 读取驾驶行为事件
	 * 1-加热器工作
	 * 2-空调工作
	 * 3-发动机超转
	 * 4-过长怠速
	 * 5-超经济区运行
	 * 6-空档滑行
	 * 7-怠速空调
	 * 8-二档起步
	 * 9-档位不当
	 *
	 * 集合Map列表对应键
	 * e1-加热器工作；
	 * e2-空调工作
	 * e3-发动机超转
	 * e4-过长怠速
	 * e5-超经济区运行
	 * e6-空档滑行
	 * e7-怠速空调
	 * e8-二档起步
	 * e9-档位不当
	 * 
	 * 车辆日统计表中存储加热器运行时间、空调工作、二档起步、档位不当
	 * 报警事件明细表中存储超速、疲劳驾驶、空档滑行、过长怠速、怠速空调、发动机超转
	 * @param path
	 * @throws SQLException 
	 * @throws IOException
	 */
	private void readEventFile(String path, String vid, VehicleInfo info, TreeMap<Long, String> returnAlarmMap) throws SQLException{
		
		PreparedStatement stDeleteDriverEventInfo = null;
		OraclePreparedStatement stSaveDriverEventInfo = null;
		long serverStartTime=System.currentTimeMillis();
		try{
			TreeMap<String, String>  dataMap = new TreeMap<String,String>();
			readEvent(path,dataMap); // 读驾驶行为事件文件，并排序
			if(dataMap.size() > 0){// 为了支持8080B车机，如果有驾驶行为事件文件，则清除事实上报超速、疲劳驾驶、空档滑行、过长怠速、怠速空调、发动机超转，否则则使用实时上报
				// 清除在日报警文件统计的超速、疲劳驾驶、空档滑行、过长怠速数据
				clearOtherAlarm("1"); // 超速
				clearOtherAlarm("2"); // 疲劳驾驶
				clearOtherAlarm("44"); // 空档滑行
				clearOtherAlarm("45"); // 过长怠速
				clearOtherAlarm("46"); // 怠速空调
				clearOtherAlarm("47"); // 发动机超转
				
				//读入之前先清空原有数据
				stDeleteDriverEventInfo = dbCon.prepareStatement(deleteDriverEventInfo);
				stDeleteDriverEventInfo.setString(1, vid);
				stDeleteDriverEventInfo.executeUpdate();
				
				stSaveDriverEventInfo = (OraclePreparedStatement)dbCon.prepareStatement(saveDriverEventInfo);
				stSaveDriverEventInfo.setExecuteBatch(1000);
				Set<String> set = dataMap.keySet();
				Iterator<String> it = set.iterator();
				while(it.hasNext()){
					String key = it.next();
					String data = dataMap.get(key);
					
					String[] event = data.split("\\|");
					
					readDriverEventFile(stSaveDriverEventInfo,event, vid ); // 存储驾驶行为事件
					
					if(event.length == 3){
						
						String[] startPos =  event[1].split("\\]",6);
						String[] endPos =  event[2].split("\\]",6);
						
						if(startPos.length == 6 && endPos.length == 6){
							String ky = event[0];
							String startTime = startPos[5].replaceAll("\\[", "").replaceAll("\\]", "");
							String endTime = endPos[5].replaceAll("\\[", "").replaceAll("\\]", "");									
							
							// 报警日统计信息
							VehicleAlarm v = null;
							
							if(ky.equals("1")){ // 加热器运行时间
								v = alarmSatDayMap.get("e1");
							}else if(ky.equals("2")){ // 空调工作
								v = alarmSatDayMap.get("e2");
							}else if(ky.equals("8")){ // 二档起步
								v = alarmSatDayMap.get("e8");
							}else if(ky.equals("9")){ // 档位不当
								v = alarmSatDayMap.get("e9");
							}else if(ky.equals("5")){ // 超经济区运行
								v = alarmSatDayMap.get("e5");
							}
							
							if(v != null){
								v.addCount(1);
								long time = (CDate.stringConvertUtc(endTime) - CDate.stringConvertUtc(startTime))/1000;
								if(time >0){
									v.addTime(time);
								}																
							}else{
								v = new VehicleAlarm();
								v.addCount(1);
								long time = (CDate.stringConvertUtc(endTime) - CDate.stringConvertUtc(startTime))/1000;
								if(time > 0){
									v.addTime(time);
								}					
								
								//车辆日统计表中存储加热器运行时间、空调工作、二档起步、档位不当
								if(ky.equals("5") || ky.equals("1") || ky.equals("2") || ky.equals("8") || ky.equals("9")){	// 其他						
									v.setAlarmType("");
									alarmSatDayMap.put("e" + event[0], v);
								}
							}
							
							// 报警事件明细表中存储超速、疲劳驾驶、空档滑行、过长怠速、怠速空调、发动机超转
							if(ky.equals("11") || ky.equals("10") || ky.equals("6") || ky.equals("4") || ky.equals("7") || ky.equals("3")){
								// 封装报警事件统计bean
								addAlarmEventList(startPos,endPos,event,vid,info, returnAlarmMap);
							}
							
						}
						
					}				
				}// End while
				
			}
			long serverEndTime=System.currentTimeMillis();
    		serverRunTime=serverRunTime+(serverEndTime-serverStartTime)/1000;
			String message="----------------------解析驾驶行为事件："+(serverEndTime-serverEndTime)/1000+"s";
			logger.info(message);
		}catch(Exception e){
			logger.error(vid + " 存储驾驶行为事件信息出错.",e);
		}finally{
			if(stDeleteDriverEventInfo != null){
				stDeleteDriverEventInfo.close();
			}
			if(stSaveDriverEventInfo != null){
				stSaveDriverEventInfo.close();
			}
		}
	}

	/**
	 * 读取当前车辆驾驶行为事件并保存
	 * @param file
	 * @throws IOException 
	 * @throws NumberFormatException 
	 */
	private void readDriverEventFile(PreparedStatement stSaveDriverEventInfo,String[] event,String vid ) {
		try{
			if(event.length == 3){
				String[] startPos =  event[1].split("\\]",6);
				String[] endPos =  event[2].split("\\]",6);
				if(startPos.length == 6 && endPos.length == 6){
					String eventCode = event[0];
					long beginLon = (Long)formatValueByType(startPos[0].replaceAll("\\[", "").replaceAll("\\]", ""),"-1",'L');
					long beginLat = (Long)formatValueByType(startPos[1].replaceAll("\\[", "").replaceAll("\\]", ""),"-1",'L');
					long beginElevation = (Long)formatValueByType(startPos[2].replaceAll("\\[", "").replaceAll("\\]", ""),"-1",'L');
					long beginSpeed = (Long)formatValueByType(startPos[3].replaceAll("\\[", "").replaceAll("\\]", ""),"-1",'L');
					long beginDirection = (Long)formatValueByType(startPos[4].replaceAll("\\[", "").replaceAll("\\]", ""),"-1",'L');
					String startTime = startPos[5].replaceAll("\\[", "").replaceAll("\\]", "");
					long beginUtc = CDate.stringConvertUtc(startTime);
					
					long endLon = (Long)formatValueByType(endPos[0].replaceAll("\\[", "").replaceAll("\\]", ""),"-1",'L');
					long endLat = (Long)formatValueByType(endPos[1].replaceAll("\\[", "").replaceAll("\\]", ""),"-1",'L');
					long endElevation = (Long)formatValueByType(endPos[2].replaceAll("\\[", "").replaceAll("\\]", ""),"-1",'L');
					long endSpeed = (Long)formatValueByType(endPos[3].replaceAll("\\[", "").replaceAll("\\]", ""),"-1",'L');
					long endDirection = (Long)formatValueByType(endPos[4].replaceAll("\\[", "").replaceAll("\\]", ""),"-1",'L');
					String endTime = endPos[5].replaceAll("\\[", "").replaceAll("\\]", "");
					long endUtc = CDate.stringConvertUtc(endTime);
					
					stSaveDriverEventInfo.setString(1, vid);
					stSaveDriverEventInfo.setString(2, "");
					stSaveDriverEventInfo.setString(3, eventCode);
					stSaveDriverEventInfo.setLong(4, beginUtc);
					stSaveDriverEventInfo.setLong(5, beginLat);
					stSaveDriverEventInfo.setLong(6, beginLon);
					stSaveDriverEventInfo.setNull(7, Types.INTEGER);
					stSaveDriverEventInfo.setNull(8, Types.INTEGER);
					stSaveDriverEventInfo.setLong(9, beginElevation);
					stSaveDriverEventInfo.setLong(10, beginDirection);
					stSaveDriverEventInfo.setLong(11, beginSpeed);
					stSaveDriverEventInfo.setLong(12, endUtc);
					stSaveDriverEventInfo.setLong(13, endLat);
					stSaveDriverEventInfo.setLong(14, endLon);
					stSaveDriverEventInfo.setNull(15, Types.INTEGER);
					stSaveDriverEventInfo.setNull(16, Types.INTEGER);
					stSaveDriverEventInfo.setLong(17, endElevation);
					stSaveDriverEventInfo.setLong(18, endDirection);
					stSaveDriverEventInfo.setLong(19, endSpeed);
					
					stSaveDriverEventInfo.executeUpdate();
				}
			}
		}catch(Exception e){
			logger.error(vid + " 存储驾驶行为事件信息出错.",e);
		}
	}
	
	/****
	 * 清除在日报警文件统计的超速、疲劳驾驶数据
	 * @param alarmCode
	 */
	private void clearOtherAlarm(String alarmCode){
		LinkedList<Integer> idxList = new LinkedList<Integer>();
		for(int i = 0 ; i < alarmEventList.size(); i++ ){
			VehicleAlarmEvent event = alarmEventList.get(i);
			if(event.getAlarmCode().equals(alarmCode)){ // 获取列表下标
				idxList.addFirst(i);
			}
		}// End for
		
		if(idxList.size() >0){
			for(int i = idxList.size() -1; i >=0; i-- ){
				Integer idx = idxList.removeFirst();
				alarmEventList.removeElementAt(idx); // 清除
			}// End for
		}
		idxList.clear();
	}
	
	/****
	 * 获取报警类型
	 * @param v
	 * @param alarmCode
	 */
	private String getAlarmType(String alarmCode){
		String alarmType = "";
		if(AnalysisDBAdapter.alarmTypeMap.containsKey(alarmCode)){
			alarmType = AnalysisDBAdapter.alarmTypeMap.get(alarmCode);
		}
		
		logger.info("alarm code : " + alarmCode + "; alarm type:" + alarmType);
		
		return alarmType;
	}
	
	private void readEvent(String path,TreeMap<String,String> map){
		File file = new File(path);
		List<DataBean> list = new ArrayList<DataBean>();
		if(file.exists()){
			String readLine = null;
			BufferedReader buf = null;
			try{
				buf = new BufferedReader(new FileReader(file));
				while((readLine = buf.readLine()) != null ){
					String[] event = readLine.split("\\|");
					if(event.length == 3){
						String[] startPos =  event[1].split("\\]");
						if(startPos.length == 6){
							String startTime = startPos[5].replaceAll("\\[", "").replaceAll("\\]", "");
							map.put(event[0] + "_" +CDate.stringConvertUtc(startTime), readLine); // 滤过GPS 开始时间重复记录,
						}
					}
				}// End while
			}catch(IOException e){
				if(buf != null){
					try {
						buf.close();
					} catch (IOException ex) {
						logger.error("关闭文件 " + path + " 出错",ex);
					}
				}
				
				if(list != null && list.size() > 0){
					list.clear();
				}
			}
		}
	}
	
	/***
	 * 添加数据到集合文件
	 * @param data
	 * @param time
	 */
	private void addList(String data,String time,List<DataBean> a){
		long gpsTime = 0;
		gpsTime = CDate.stringConvertUtc(time);
		DataBean db = new DataBean();
		db.setData(data);
		db.setGpsTime(gpsTime);
		a.add(db);
	}
	
	/****
	 * 对List集合按日期时间排序
	 * @param list
	 */
	private void sortList(List<DataBean> list){
		Collections.sort(list, new LongComparator());
	}
	
	class LongComparator implements Comparator<Object> {
        public int compare(Object o1, Object o2) {
        	DataBean d1 = (DataBean)o1;
        	DataBean d2 = (DataBean)o2;
        	Long g1 = d1.getGpsTime();
        	Long g2 = d2.getGpsTime();
               //如果有空值，直接返回0
               if (g1 == null || g2 == null)
                   return 0; 
             
              return g1.compareTo(g2);
        }
	}
	
	/**
	 * 封装报警事件统计bean
	 * @param 起始位置信息，结束位置信息，事件类型
	 */
	private void addAlarmEventList(String[] startPos, String[] endPos, String[] event, String vid, VehicleInfo info,TreeMap<Long, String> returnAlarmMap){			
		String beginLat = startPos[0].replaceAll("\\[", "").replaceAll("\\]", "");
		String beginLon = startPos[1].replaceAll("\\[", "").replaceAll("\\]", "");
		String beginElevation = startPos[2].replaceAll("\\[", "").replaceAll("\\]", "");
		String beginGpsSpeed = startPos[3].replaceAll("\\[", "").replaceAll("\\]", "");
		String beginDirection = startPos[4].replaceAll("\\[", "").replaceAll("\\]", "");
		String beginTime = startPos[5].replaceAll("\\[", "").replaceAll("\\]", "");
		String endLat = endPos[0].replaceAll("\\[", "").replaceAll("\\]", "");
		String endLon = endPos[1].replaceAll("\\[", "").replaceAll("\\]", "");
		String endElevation = endPos[2].replaceAll("\\[", "").replaceAll("\\]", "");
		String endGpsSpeed = endPos[3].replaceAll("\\[", "").replaceAll("\\]", "");
		String endDirection = endPos[4].replaceAll("\\[", "").replaceAll("\\]", "");
		String endTime = endPos[5].replaceAll("\\[", "").replaceAll("\\]", "");		
		String ky = event[0];
		// 报警事件统计
		VehicleAlarmEvent events = new VehicleAlarmEvent();
		if(ky.equals("10")){ // 超速
			events.setAlarmType(getAlarmType("1"));
			events.setAlarmCode("1");
 		} else if(ky.equals("11")){ // 疲劳驾驶							
			events.setAlarmType(getAlarmType("2"));
			events.setAlarmCode("2");
		} else if(ky.equals("6")){	// 空档滑行						
			events.setAlarmType(getAlarmType("44"));
			events.setAlarmCode("44");
		} else if(ky.equals("4")){	// 过长怠速						
			events.setAlarmType(getAlarmType("45"));
			events.setAlarmCode("45");
		} else if(ky.equals("7")){	// 怠速空调						
			events.setAlarmType(getAlarmType("46"));
			events.setAlarmCode("46");
		} else if(ky.equals("3")){	// 发动机超转						
			events.setAlarmType(getAlarmType("47"));
			events.setAlarmCode("47");
		}
		
		events.setVid(vid);
		events.setPhoneNumber(info.getCommaddr());   
		events.setAREA_ID("");// 电子围栏编号
		events.setMtypeCode("");
		events.setMediaUrl("");
		events.setStartUtc(CDate.stringConvertUtc(beginTime));
		events.setStartLat((Long)formatValueByType(beginLat,"-1",'L'));
		events.setStartLon((Long)formatValueByType(beginLon,"-1",'L'));
		long[] beginMapPoint = convertLatLonToMap(beginLat,beginLon); //偏移经纬度
		events.setStartMapLat(beginMapPoint[0]);
		events.setStartMapLon(beginMapPoint[1]);
		events.setStartElevation((Integer)formatValueByType(beginElevation,"0",'I'));
		events.setStartHead((Integer)formatValueByType(beginDirection,"0",'I'));
		events.setStartGpsSpeed((Long)formatValueByType(beginGpsSpeed,"0",'L'));
		
		events.setEndUtc(CDate.stringConvertUtc(endTime));
		events.setEndLat((Long)formatValueByType(endLat,"-1",'L'));
		events.setEndLon((Long)formatValueByType(endLon,"-1",'L'));
		
		long[] endMapPoint = convertLatLonToMap(endLat,endLon); // 偏移经纬度
		events.setEndMapLat(endMapPoint[0]);
		events.setEndMapLon(endMapPoint[1]);
		events.setEndElevation((Integer)formatValueByType(endElevation,"0",'I'));
		events.setEndHead((Integer)formatValueByType(endDirection,"0",'I'));
		events.setEndGpsSpeed((Long)formatValueByType(endGpsSpeed,"0",'L'));
		long alarmTime = (events.getEndUtc() - events.getStartUtc())/1000;
		
		// 报警事件时长
		if(alarmTime > 0){
			events.setAccountTime(alarmTime);
		}
		
		// 最大速度
		if(events.getStartGpsSpeed() > events.getEndGpsSpeed()){
			events.setMaxSpeed(events.getStartGpsSpeed());
		} else {
			events.setMaxSpeed(events.getEndGpsSpeed());
		}
		
		events.setAlarmSrc(1);
		
		// 计算累计里程和累计油耗
		staOilAndMil(events,returnAlarmMap,beginTime,endTime);
		
		alarmEventList.add(events);
	}
	
	/****
	 * 偏移经纬度
	 * @param lt
	 * @param ln
	 */
	private long[] convertLatLonToMap(String lt,String ln){
		long pointArr[] = new long[2];
		long lon = Long.parseLong(ln);
		long lat = Long.parseLong(lt);
		long maplon = -100;
		long maplat = -100;
		// 偏移
		Converter conver = new Converter();
		Point point = conver.getEncryPoint(lon / 600000.0,
				lat / 600000.0);
		if (point != null) {
			maplon = Math.round(point.getX() * 600000);
			maplat = Math.round(point.getY() * 600000);
		} else {
			maplon = 0;
			maplat = 0;
		}
		pointArr[0] = maplat;
		pointArr[1] = maplon;
		return pointArr;
	}
	
	
	/**
	 * 统计超速报警的累计油耗和里程
	 */
	private void staOilAndMil(VehicleAlarmEvent v,TreeMap<Long, String> returnAlarmMap,String startTime,String endTime){
		int startOil = 0;
		int endOil = 0;
		int startMileage = 0;
		int endMileage = 0;
		Long k = null;
		String[] col = null;
						
		Set<Long> key = returnAlarmMap.keySet();		
		for (Iterator<Long> it = key.iterator(); it.hasNext();) {
			k = it.next();
			col = returnAlarmMap.get(k).split(":"); 
			 long tmpStartTime = CDate.stringConvertUtc(startTime);
			 long tmpEndTime = CDate.stringConvertUtc(endTime);
			 if(k >= tmpStartTime){
				 // 超速累计油耗
				if(!Utils.checkEmpty(col[8])){
					if(tmpStartTime == k){
						startOil = Integer.parseInt(col[8]);
					}
					
					if(tmpEndTime == k){
						endOil = Integer.parseInt(col[8]);
					}	
				}
				
				// 超速行驶里程
				if(!Utils.checkEmpty(col[9])){
					if(tmpStartTime == k ){
						startMileage = Integer.parseInt(col[9]);
					}
					
					if(tmpEndTime == k){
						endMileage = Integer.parseInt(col[9]);
					}	
				}
			 }else{
				 continue;
			 }
			 
			 if(startOil > 0 && endOil >0 && startMileage > 0 && endMileage >0){
					break;
			 }
			 
			 if(tmpEndTime <= k){
				 break;
			 }
		}// End for	
		if(endOil != 0 && startOil != 0){
			if(endOil > startOil  && endOil != 4294967295l && startOil != 4294967295l && endOil != -1 && startOil != -1){
				v.setCostOil(endOil-startOil);
			}			
		}
		if(endMileage != 0 && startMileage != 0 && startMileage != 4294967295l && endMileage != 4294967295l && endMileage != -1 && startMileage != -1){
			if(endMileage > startMileage){
				v.setMileage(endMileage-startMileage);
			}		
		}		
	}	
	
	/****
	 * 计算报警下行驶里程和油耗
	 * @param cols 结束点
	 * @param startCol 开始点
	 * @param event 事件bean
	 */
	private void accountOilAndMelige(String[] cols,String[] startCol,VehicleAlarmEvent event){
		 //计算报警下行驶里程
		if(cols[9] != null && !cols[9].equals("") && startCol[9] != null && !startCol[9].equals("") && !startCol[9].equals("-1") && !cols[9].equals("-1")){
			int value = (Integer.parseInt(cols[9])-Integer.parseInt(startCol[9]));
			if(value > 0){
				event.setMileage(value);
			}
		}
		
		// 计算报警下耗油
		if(cols[8] != null && !cols[8].equals("") && startCol[8] != null && !startCol[8].equals("") && !startCol[8].equals("-1") && !cols[8].equals("-1")){
			int value = (Integer.parseInt(cols[8])-Integer.parseInt(startCol[8]));
			if(value > 0){
				event.setCostOil(value);
			}
		}	
	}

	@Override
	public void costTime() {
		// TODO Auto-generated method stub
	}

	@Override
	public void setThreadId(int threadId) {
		this.threadId = threadId;
		
	}

	/****
	 * 设置统计时间
	 */
	@Override
	public void setTime(long utc) {
		this.utc = utc;
	}
	
	private Object formatValueByType(String str,String defaultVal,char type){
		Object obj = null;
		switch (type)	{
			case 'S': obj=((str==null || "".equals(str)|| "null".equals(str))?defaultVal:str.trim());break;
			case 'L': obj=Long.parseLong((str==null || "".equals(str)|| "null".equals(str))?defaultVal:str.trim());break;
			case 'I': obj=Integer.parseInt((str==null || "".equals(str)|| "null".equals(str))?defaultVal:str.trim());break;
		}
		return obj;
	}
}
