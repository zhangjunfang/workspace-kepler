package com.ctfo.storage.command.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.command.model.AuthModel;
import com.ctfo.storage.command.model.LogoutModel;
import com.ctfo.storage.command.model.OnOffLineModel;
import com.ctfo.storage.command.model.RegisterModel;
import com.ctfo.storage.command.service.ProtocolAnaly;
import com.ctfo.storage.command.util.Tools;




public class Parse extends Thread{
	private static Logger log = LoggerFactory.getLogger(Parse.class);
	
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(50000);
	private static ProtocolAnaly protocolAnaly = new ProtocolAnaly();
	private ThTerminalRegisterLoading thTerminalRegisterLoading;
	private ThVehicleCheckdLoading thVehicleCheckdLoading;
	private ThVehicleLogoffLoading thVehicleLogoffLoading;
	private ThVehicleOnoffLineLoading thVehicleOnOffLineLoading;

	
	
	
	private int index;
	private long lastTime = System.currentTimeMillis();
	/**
	 * @param dvrId
	 */
	public Parse() {
		setName("Parse");
		thTerminalRegisterLoading = new ThTerminalRegisterLoading();
		thVehicleCheckdLoading = new ThVehicleCheckdLoading();
		thVehicleLogoffLoading = new ThVehicleLogoffLoading();
		thVehicleOnOffLineLoading = new ThVehicleOnoffLineLoading();
		
		thTerminalRegisterLoading.start();
		thVehicleCheckdLoading.start();
		thVehicleLogoffLoading.start();
		thVehicleOnOffLineLoading.start();
	}
	public void run(){
		while(true){
			try {
//				Object o = queue.poll();//获取并移除此队列的头，如果此队列为空，则返回 null。 
				String message = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。
				index++;
				long current = System.currentTimeMillis();
				if((current- lastTime) > 10000){		
					log.info("-----------------10秒处理[{}]条,应答[{}],剩余[{}]", index,ResponseListen.getCount(),ResponseListen.getQueue().size());
					index = 0;
					lastTime = System.currentTimeMillis();
				}
				process(message); 
			} catch (Exception e) {
				log.error("Parse处理队列数据异常:" +e.getMessage());
			}
		}
	}
//	开始2  消息来源8	消息目的地8         类型4       流水号 8                长度  8          子类型4
//	5b 57c5d80a 05397fb1 1100 00000001 00000032 1101 6265696A696E67000000000000000000 3034323061323036616538623737623630663331346133336233386338373561 ED 5d 
	private void process(String command) {
		if(Tools.getMasterType(command).equals("1200")){
			if(Tools.getSlaveType(command).equals("1201")){
				OnOffLineModel o = new OnOffLineModel();
				o = protocolAnaly.getOnOffLineModel(command);
				thVehicleOnOffLineLoading.put(o);
			}else if(Tools.getSlaveType(command).equals("1202")){
				RegisterModel o = new RegisterModel();
				o = protocolAnaly.getRegisterModel(command);
				thTerminalRegisterLoading.put(o);
			}else if(Tools.getSlaveType(command).equals("1203")){
				LogoutModel o = new LogoutModel();
				o = protocolAnaly.getLogoutModel(command);
				thVehicleLogoffLoading.put(o);
			}else if(Tools.getSlaveType(command).equals("1204")){
				AuthModel o = new AuthModel();
				o = protocolAnaly.getAuthModel(command);
				thVehicleCheckdLoading.put(o);
			}
		}
		

	}

	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(String data){
		return queue.offer(data);
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public void put(String data){
		try {
			queue.put(data);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出 IllegalStateException。
	 * @param data
	 * @return
	 */
	public static boolean add(String data){
		return queue.add(data);
	}
}
