package com.ctfo.analy.beans;

import java.util.ArrayDeque;
import java.util.Deque;
import java.util.Iterator;

import com.ctfo.analy.util.CDate;

public class GPSInspectionConfig {
	private String vid = "";
	
	private String commaddr = null;
	
	private int timeFlag = 0; // 标记当前CHECK时间点 
	
	private Deque<TimeInspection> qt = new ArrayDeque<TimeInspection>();
	
	public String getCommaddr() {
		return commaddr;
	}

	public void setCommaddr(String commaddr) {
		this.commaddr = commaddr;
	}

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public void putDeque(TimeInspection ti){
		qt.addLast(ti);
	}
	
	public TimeInspection popDeque(){
		return qt.pollFirst();
	}
	
	public int getDequeSize(){
		return qt.size(); 
	}

	public Deque<TimeInspection> getQt() {
		return qt;
	}

	/***
	 * CHECK 当前上报点是否在巡检时间范围之内
	 * @param systime
	 * @return
	 */
	public boolean checkGPSInspection(long systime){
		Iterator<TimeInspection> timeIt = qt.iterator();
		while(timeIt.hasNext()){
			TimeInspection timeInspection = timeIt.next();
			if((timeInspection.getStartTime() + CDate.getCurrentDayYearMonthDay()) <= systime && systime <= (timeInspection.getEndTime() + CDate.getCurrentDayYearMonthDay())){
				setTimeFlag(timeInspection.getEndTime());
				return true;
			}
		}// End while
		
		return false;
	}
	
	public void setQt(Deque<TimeInspection> qt,int isLoad) {
		if(0 == isLoad){ // 初始化加载
			Iterator<TimeInspection> timeIt = qt.iterator();
			long sysTime = System.currentTimeMillis();
			while(timeIt.hasNext()){
				TimeInspection timeInspection = timeIt.next();
				
				if((timeInspection.getStartTime() + CDate.getCurrentDayYearMonthDay()) <= sysTime && sysTime <= (timeInspection.getEndTime() + CDate.getCurrentDayYearMonthDay())){ // 初始化时间标记点
					setTimeFlag(timeInspection.getEndTime());
				}
			}
			
		}
		
		if(1 == isLoad){ // 定时加载
			// 当定时加载一次，设置标记时间为0
			setTimeFlag(0);
		}
		
		this.qt = qt;
	}

	public int getTimeFlag() {
		return timeFlag;
	}

	public void setTimeFlag(int timeFlag) {
		this.timeFlag = timeFlag;
	}
}
