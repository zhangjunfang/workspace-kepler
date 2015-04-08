package com.caits.analysisserver.addin.kcpt.addin;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.Iterator;
import java.util.Timer;
import java.util.concurrent.ConcurrentHashMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.Task;
import com.caits.analysisserver.database.TaskPool;

 
public class DispatchThread extends Thread{

	private static final Logger logger = LoggerFactory.getLogger(DispatchThread.class);
	
	private final static long fONCE_PER_DAY = 1000*60*60*24;
	
	private final static int fONE_DAY = 1;
	
	public void run(){
		Timer timer = new Timer();
		//System.out.println("----------------------启动调度线程成功----------------------------");
		ConcurrentHashMap<String,Task> mainTask = TaskPool.getinstance().getMainTask();
		logger.info("启动调度线程成功");
		Iterator<String> itTask = mainTask.keySet().iterator();
		while(itTask.hasNext()){
			String taskName = itTask.next();
			Task task = mainTask.get(taskName);
			timer.schedule(task.getTask(), getschedule(task.getTime()),fONCE_PER_DAY);	
		}// End while
	}
	
	 private static Date getschedule(String time){
	 	String[] arrTime = time.split(":");
	    Calendar tomorrow = new GregorianCalendar();
	    tomorrow.add(Calendar.DATE, fONE_DAY);
	    Calendar result = new GregorianCalendar(
	      tomorrow.get(Calendar.YEAR),
	      tomorrow.get(Calendar.MONTH),
	      tomorrow.get(Calendar.DATE),
	      Integer.parseInt(arrTime[0]),
	      Integer.parseInt(arrTime[1])
	    );
	    return result.getTime();
	 }
}
