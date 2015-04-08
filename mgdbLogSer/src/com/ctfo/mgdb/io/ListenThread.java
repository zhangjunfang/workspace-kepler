package com.ctfo.mgdb.io;

import org.apache.log4j.Logger;

import com.ctfo.mgdbser.analy.AnalyseServiceInit;


public class ListenThread extends Thread{
	
	private static final Logger logger = Logger.getLogger(ListenThread.class);
	public ReceiveThread receiveThread;
	public AnalyseServiceInit analyseServiceInit;
	
	public ListenThread(ReceiveThread receiveThread,AnalyseServiceInit analyseServiceInit){
		this.receiveThread = receiveThread;
		this.analyseServiceInit = analyseServiceInit;
	}
	
	public void run(){
		
		while(true){
				logger.debug(""+this.analyseServiceInit.getAnalyseServiceThread()[0].mPacket.size());
			try {
				Thread.sleep(5000);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}

}
