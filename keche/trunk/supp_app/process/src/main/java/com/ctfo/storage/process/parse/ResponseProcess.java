package com.ctfo.storage.process.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.util.SerialUtil;
import com.ctfo.storage.process.util.Tools;

/**
 * ResponseProcess
 * 
 * 
 * @author huangjincheng
 * 2014-7-8下午1:32:02
 * 
 */
public class ResponseProcess extends Thread{
	private static Logger log = LoggerFactory.getLogger(ResponseProcess.class);
	/**	数据队列	*/
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(50000);
		
	private ResponseListen responseListen;
	
	public ResponseProcess() throws Exception{
		setName("ResponseProcess-Thread");
		responseListen = new ResponseListen();
		
		responseListen.start();
	}
	@Override
	public void run() {
		log.info("--ResponseProcess-- 应答处理线程启动！");
		while(true){
			try {
				String response = queue.take();
				process(response);
			} catch (InterruptedException e) {
				log.error("应答处理线程异常"+e.getMessage());
			}
			
		}
	}
	/**
	 * @param command
	 */
	private void process(String response) {
		StringBuffer buff = new StringBuffer();
		buff.append(response.substring(10,18))
		.append(response.substring(2,10)).append("C001")
		.append(Tools.fillNBitBefore(Integer.toHexString(SerialUtil.getInt()), 8, "0"))
		.append("00000005").append(response.substring(22,30)).append("00");
		buff.append(Tools.getCheckCode(buff.toString())).append("5D");
		String res = Tools.getReverTransferContent("5B"+buff.toString());
		responseListen.put(res);
	}
	/**
	 * @param string
	 * @throws InterruptedException 
	 */
	public void put(String string) throws InterruptedException {
		queue.put(string);		
	}
}