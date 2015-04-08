package com.ctfo.storage.media.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.apache.mina.core.session.IoSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.media.model.ResponseModel;
import com.ctfo.storage.media.util.SerialUtil;
import com.ctfo.storage.media.util.Tools;


/**
 * ResponseListen
 * 
 * 
 * @author huangjincheng
 * 2014-6-12下午03:01:56
 * 
 */
public class ResponseListen extends Thread{
	private static Logger log = LoggerFactory.getLogger(ResponseListen.class);
	private static ArrayBlockingQueue<ResponseModel> queue = new ArrayBlockingQueue<ResponseModel>(10000);
	private boolean flag = true;
	public ResponseListen(){
		setName("ResponseListen");
	}
	
	public void run(){
		log.info("--ResponseListen-- 应答监听线程启动！");
		while(flag){
			try {
				ResponseModel response = queue.take();
				process(response);			
			} catch (InterruptedException e) {
				log.error("应答监听线程异常:"+e.getMessage());
			}
		}
	}
	
	public void process(ResponseModel response){
		IoSession session = response.getSession();
		String sourceStr = response.getSourceStr();
		StringBuffer buff = new StringBuffer();
		buff.append(sourceStr.substring(10,18))
		.append(sourceStr.substring(2,10)).append(response.getCommand())
		.append(Tools.fillNBitBefore(Integer.toHexString(SerialUtil.getInt()), 8, "0"))
		.append(response.getLength()).append(sourceStr.substring(22,30)).append("00");
		buff.append(Tools.getCheckCode(buff.toString())).append("5D");
		String res = Tools.getReverTransferContent("5B"+buff.toString());      		
		session.write(res);
		log.info("--应答--[{}]",res);
	}
	
	public static void put(ResponseModel response){
		try {
			queue.put(response);
		} catch (InterruptedException e) {
			log.error("应答监听queue异常！"+e.getMessage());
		}
	}
}
