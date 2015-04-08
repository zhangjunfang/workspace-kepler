package com.ctfo.analy.dao;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayDeque;
import java.util.Deque;
import java.util.List;
import java.util.TimerTask;

import org.apache.log4j.Logger;

import com.ctfo.analy.DataAnalyMain;
import com.ctfo.analy.TempMemory;
import com.ctfo.analy.beans.GPSInspectionConfig;
import com.ctfo.analy.beans.TimeInspection;
import com.ctfo.analy.beans.VehicleInfo;
import com.ctfo.analy.util.CDate;

public class LoadGPSInspectionConfig extends TimerTask{
	private static final Logger logger = Logger.getLogger(LoadGPSInspectionConfig.class);
	
	// 0标记初始化加载，1标记定时加载
	private int isLoad = 0; 
	
	public LoadGPSInspectionConfig(int isLoad){
		this.isLoad = isLoad;
	}
	
	public void run(){
		if(isLoad == 0){
			logger.info("Initialization of load GPS inspection cofiguration.");
		}else if(isLoad == 1){
			logger.info("Timing of load GPS inspection cofiguration.");
		}
		loadConfig();
	}
	/*****
	 * 加载GPS巡检配置文件
	 */
	public void loadConfig(){
		BufferedReader buf = null;
		File f = null;
		String str = null;
		try {
			logger.info("filePath:"+DataAnalyMain.gpsInspectionConfigFile);
			f = new File(DataAnalyMain.gpsInspectionConfigFile);
			if(f.exists()){
				buf = new BufferedReader(new FileReader(f));
				
				while(null != (str = buf.readLine())){
					if(!str.startsWith("#")){
						logger.info( "GPS inspection : "+ str);
						String[] arr = str.split("\\$");
						String orgId = arr[0];
						Deque<TimeInspection> qt = new ArrayDeque<TimeInspection>();
						
						if(null != arr && 2 == arr.length){
							String[] tArr = arr[1].split(";"); //09:00,12:00;12:00,18:00;18:00,24:00
							if(null != tArr && tArr.length >0){
								for(String time : tArr){
									String[] t = time.split(",");
									if(null != t && 2 == t.length){
										if(t[0].matches("\\d{2}:\\d{2}") && t[1].matches("\\d{2}:\\d{2}")){
											TimeInspection ti = new TimeInspection();
											ti.setStartTime(CDate.getTimeShort(t[0]));
											ti.setEndTime(CDate.getTimeShort(t[1]));
											qt.addLast(ti);
											
										}else{
											logger.error("时间配置格式不正确 : " + time + ";正确格式==>HH:ss");
											continue;
										}
									}
								}// End for
							}
						}
						
						if(qt.size() > 0){
							List<VehicleInfo> vList = MonitorDBAdapter.getGpsInsVehicleList(orgId);
							for(VehicleInfo vInfo : vList){ // 创建一辆车巡检时间列表
								GPSInspectionConfig gpsConf = new GPSInspectionConfig();
								gpsConf.setCommaddr(vInfo.getCommaddr());
								gpsConf.setVid(vInfo.getVid());
								gpsConf.setQt(qt,isLoad);
								TempMemory.addGpsInsConfig(vInfo.getCommaddr(),gpsConf);
							} // End for
						}
					}else{
						logger.info( "失效配置 : "+ str);
					}
				}// End while
			}else{
				logger.error("No found " + DataAnalyMain.gpsInspectionConfigFile + " file");
			}
				
		} catch (Exception e) {
			logger.error("加载:" + DataAnalyMain.gpsInspectionConfigFile + " 失败。",e);
		}finally{
			if(null != buf){
				try {
					buf.close();
				} catch (IOException e) {
					logger.equals(e);
				}
			}
		}
	}
	
	public static void main(String[] args){
		LoadGPSInspectionConfig load = new LoadGPSInspectionConfig(0);
		load.loadConfig();
	}
}
