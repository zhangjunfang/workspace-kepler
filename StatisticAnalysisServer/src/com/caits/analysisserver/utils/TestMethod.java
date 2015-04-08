package com.caits.analysisserver.utils;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;

import com.caits.analysisserver.bean.VehicleAlarm;
import com.caits.analysisserver.bean.VehicleStatus;

public class TestMethod {
	private VehicleStatus vehicleStatus = new VehicleStatus();
	/**
	 * @param args
	 */
	public static void main(String[] args) {
		TestMethod test = new TestMethod();
		test.readEventFile("driverActiveEvent.txt");
	}
	@SuppressWarnings("resource")
	private void readEventFile(String path){
		File file = new File(path);
		if(file.exists()){
			String readLine = null;
			try{
				BufferedReader buf = new BufferedReader(new FileReader(file));
				while((readLine = buf.readLine()) != null ){
					String[] event = readLine.split("\\|",3);
					
					String[] startPos =  event[1].split("\\]",6);
					String[] endPos =  event[2].split("\\]",6);
					
					VehicleAlarm v = null;
					
					if(event[0].equals("1")){ // 加热器运行时间
						v = vehicleStatus.getAlarmList().get("e1");
					}
					if(event[0].equals("2")){ // 空调工作
						v = vehicleStatus.getAlarmList().get("e2");
					}
					if(event[0].equals("3")){ // 发动机速
						v = vehicleStatus.getAlarmList().get("e3");
					}
					if(event[0].equals("4")){ // 过长怠速
						v = vehicleStatus.getAlarmList().get("e4");
					}
					if(event[0].equals("5")){ // 超经济区运行
						v = vehicleStatus.getAlarmList().get("e5");
					}
					if(event[0].equals("6")){ // 空档滑行
						v = vehicleStatus.getAlarmList().get("e6");
					}
					if(event[0].equals("7")){ // 怠速空调
						v = vehicleStatus.getAlarmList().get("e7");
					}
					if(event[0].equals("8")){ // 二档起步
						v = vehicleStatus.getAlarmList().get("e8");
					}
					if(event[0].equals("9")){ //档位不当
						v = vehicleStatus.getAlarmList().get("e9");
					}
					String startTime = startPos[5].replaceAll("\\[", "").replaceAll("\\]", "");
					String endTime = endPos[5].replaceAll("\\[", "").replaceAll("\\]", "");
					if(v != null){
						v.addCount(1);
						v.addTime((CDate.stringConvertUtc(endTime) - CDate.stringConvertUtc(startTime))/1000);
					}else{
						v = new VehicleAlarm();
						v.addCount(1);
						v.addTime((CDate.stringConvertUtc(endTime) - CDate.stringConvertUtc(startTime))/1000);
						vehicleStatus.getAlarmList().put( "e" + event[0], v);
					}
				}// End while
			}catch(IOException e){
				e.printStackTrace();
			}
		}
	}
}
