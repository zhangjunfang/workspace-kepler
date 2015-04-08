package com.ctfo.storage.process.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.util.Tools;

/**
 * TransferProcess
 * 
 * 
 * @author huangjincheng
 * 2014-7-8上午9:55:44
 * 
 */
public class TransferProcess extends Thread {
	private static Logger log = LoggerFactory.getLogger(TransferProcess.class);
	/**	数据队列	*/
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(50000);
	
	private Parse parse;
	
	private ResponseProcess responseProcess;
	
	public TransferProcess() throws Exception{
		setName("TransferProcess-Thread");
		parse = new Parse();
		responseProcess = new ResponseProcess();
		
		parse.start();
		responseProcess.start();
	}
	@Override
	public void run() {
		log.info("--TransferProcess-- 转义校验线程启动！");
		while(true){
			try {
				String command = queue.take();
				process(command);
			} catch (InterruptedException e) {
				log.error("转义校验线程异常"+e.getMessage());
			}
			
		}
	}
	/**
	 * @param command
	 * @throws InterruptedException 
	 */
	private void process(String command){
		String tranCommand = Tools.getTransferContent(command);
		try {
			if(Tools.isRightDeviceCode(tranCommand)){
				parse.put(tranCommand);
				responseProcess.put(tranCommand);
			}
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}		
	}
	/**
	 * @param string
	 * @throws InterruptedException 
	 */
	public void put(String string) throws InterruptedException {
		queue.put(string);
	}
}
