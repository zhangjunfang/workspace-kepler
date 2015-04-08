package com.ctfo.trackservice.handler;

import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

import com.ctfo.trackservice.util.Utils;


public class TrackAnalysisThreadTest{
	public static void main(String[] args) {
		String s = "65982190:16508009:20140927/074514:0:273::,11,24,:65979444:16510053:232:1694336:-1::-1:3::::-1:0:::-1:16:1:0:::-1::::::::::1411781727064";
		String[] cols = s.split(":");
		System.out.println(Integer.parseInt(Utils.checkEmpty(cols[25]) ? "0":cols[25]));
		//System.out.println(Long.parseLong("0.5"));
		ScheduledExecutorService executorService = Executors.newScheduledThreadPool(10);
		System.out.println(TimeUnit.MINUTES);
	    for(int i =0 ; i<2;i++){
	    	executorService.scheduleAtFixedRate(new Cat(),1,5, TimeUnit.SECONDS);
	    }
	 

	}
	static class Cat implements Runnable{
		 
	    @Override
	    public void run() {
	        System.out.println("我起来了");
/*	        try {
	            Thread.sleep(2000);
	        } catch (InterruptedException e) {
	            System.out.println("shit");
	        }
	        System.out.println("我回去了");*/
	    }
	}

}