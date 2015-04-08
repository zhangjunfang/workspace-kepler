package com.ctfo.storage.dispatch.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.dispatch.model.TbDvr3G;
import com.ctfo.storage.dispatch.model.TbDvrSer;
import com.ctfo.storage.dispatch.model.TbOrg;
import com.ctfo.storage.dispatch.model.TbOrgInfo;
import com.ctfo.storage.dispatch.model.TbPredefinedMsg;
import com.ctfo.storage.dispatch.model.TbProductType;
import com.ctfo.storage.dispatch.model.TbSim;
import com.ctfo.storage.dispatch.model.TbSpOperator;
import com.ctfo.storage.dispatch.model.TbSpRole;
import com.ctfo.storage.dispatch.model.TbTerminal;
import com.ctfo.storage.dispatch.model.TbTerminalOem;
import com.ctfo.storage.dispatch.model.TbTerminalParam;
import com.ctfo.storage.dispatch.model.TbTerminalProtocol;
import com.ctfo.storage.dispatch.model.TbVehicle;
import com.ctfo.storage.dispatch.model.ThTransferHistory;
import com.ctfo.storage.dispatch.model.TrOperatorRole;
import com.ctfo.storage.dispatch.model.TrRoleFunction;
import com.ctfo.storage.dispatch.model.TrServiceunit;
import com.ctfo.storage.dispatch.service.ProtocolAnalyService;
import com.ctfo.storage.dispatch.util.Tools;

public class Parse extends Thread{
	private static Logger log = LoggerFactory.getLogger(Parse.class);
	
	private static ArrayBlockingQueue<String> queue = new ArrayBlockingQueue<String>(500000);
	
	private ProtocolAnalyService protocolAnalyService;
	private TbDvr3GLoading tbDvr3GLoading;
	private TbDvrSerLoading tbDvrSerLoading;
	private TbOrgLoading tbOrgLoading;
	private TbOrgInfoLoading tbOrgInfoLoading;
	private TbPredefinedMsgLoading tbPredefinedMsgLoading;
	private TbProductTypeLoading tbProductTypeLoading;
	private TbSimLoading tbSimLoading;
	private TbSpOperatorLoading tbSpOperatorLoading ;
	private TbSpRoleLoading tbSpRoleLoading ;
	private TbTerminalLoading tbTerminalLoading;
	private TbTerminalOemLoading tbTerminalOemLoading;
	private TbTerminalParamLoading tbTerminalParamLoading;
	private TbTerminalProtocolLoading tbTerminalProtocolLoading;
	private TbVehicleLoading tbVehicleLoading;
	private ThTransferHistoryLoading thTransferHistoryLoading;
	private TrOperatorRoleLoading trOperatorRoleLoading;
	private TrRoleFunctionLoading trRoleFunctionLoading;
	private TrServiceunitLoading trServiceunitLoading;
	
	
	
	private int index;
	private long lastTime = System.currentTimeMillis();
	/**
	 * @param dvrId
	 */
	public Parse() {
		protocolAnalyService = new ProtocolAnalyService();
		setName("Parse-thread");
		tbDvr3GLoading = new TbDvr3GLoading();
		tbDvrSerLoading = new TbDvrSerLoading();
		tbOrgLoading = new TbOrgLoading();
		tbOrgInfoLoading = new TbOrgInfoLoading();
		tbPredefinedMsgLoading = new TbPredefinedMsgLoading();
		tbProductTypeLoading = new TbProductTypeLoading();
		tbSimLoading = new TbSimLoading();
		tbSpOperatorLoading = new TbSpOperatorLoading();
		tbSpRoleLoading  = new TbSpRoleLoading();
		tbTerminalLoading = new TbTerminalLoading();
		tbTerminalOemLoading = new TbTerminalOemLoading();
		tbTerminalParamLoading = new TbTerminalParamLoading();
		tbTerminalProtocolLoading = new TbTerminalProtocolLoading();
		tbVehicleLoading = new TbVehicleLoading();
		thTransferHistoryLoading = new ThTransferHistoryLoading();
		trOperatorRoleLoading = new TrOperatorRoleLoading();
		trRoleFunctionLoading = new TrRoleFunctionLoading();
		trServiceunitLoading = new TrServiceunitLoading();
			
		tbDvr3GLoading.start();
		tbDvrSerLoading.start();
		tbOrgLoading.start();
		tbOrgInfoLoading.start();
		tbPredefinedMsgLoading.start();
		tbProductTypeLoading.start();
		tbSimLoading.start();
		tbSpOperatorLoading.start();
		tbSpRoleLoading.start();
		tbTerminalLoading.start();
		tbTerminalOemLoading.start();
		tbTerminalParamLoading.start();
		tbTerminalProtocolLoading.start();
		tbVehicleLoading.start();
		thTransferHistoryLoading.start();
		trOperatorRoleLoading.start();
		trRoleFunctionLoading.start();
		trServiceunitLoading.start();
		
		
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
		if(Tools.getMasterType(command).equals("1600")){
			if(Tools.getSlaveType(command).equals("1611")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbDvr3G");
				tbDvr3GLoading.put((TbDvr3G)obj);
			}
			else if(Tools.getSlaveType(command).equals("1612")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbDvrSer");
				tbDvrSerLoading.put((TbDvrSer)obj);
			}
			else if(Tools.getSlaveType(command).equals("161E")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbOrg");
				tbOrgLoading.put((TbOrg)obj);
			}
			else if(Tools.getSlaveType(command).equals("161D")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbOrgInfo");
				tbOrgInfoLoading.put((TbOrgInfo)obj);
			}
			else if(Tools.getSlaveType(command).equals("1623")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbPredefinedMsg");
				tbPredefinedMsgLoading.put((TbPredefinedMsg)obj);
			}
			else if(Tools.getSlaveType(command).equals("1624")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbProductType");
				tbProductTypeLoading.put((TbProductType)obj);
			}
			else if(Tools.getSlaveType(command).equals("1629")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbSim");
				tbSimLoading.put((TbSim)obj);
			}
			else if(Tools.getSlaveType(command).equals("162A")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbSpOperator");
				tbSpOperatorLoading.put((TbSpOperator)obj);
			}
			else if(Tools.getSlaveType(command).equals("162B")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbSpRole");
				tbSpRoleLoading.put((TbSpRole)obj);
			}
			else if(Tools.getSlaveType(command).equals("162D")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbTerminal");
				tbTerminalLoading.put((TbTerminal)obj);
			}
			else if(Tools.getSlaveType(command).equals("1631")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbTerminalOem");
				tbTerminalOemLoading.put((TbTerminalOem)obj);
			}
			else if(Tools.getSlaveType(command).equals("1632")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbTerminalParam");
				tbTerminalParamLoading.put((TbTerminalParam)obj);
			}
			else if(Tools.getSlaveType(command).equals("1633")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbTerminalProtocol");
				tbTerminalProtocolLoading.put((TbTerminalProtocol)obj);
			}
			else if(Tools.getSlaveType(command).equals("1637")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TbVehicle");
				tbVehicleLoading.put((TbVehicle)obj);
			}
			else if(Tools.getSlaveType(command).equals("1643")){
				Object obj = protocolAnalyService.getTableFromControl(command, "ThTransferHistory");
				thTransferHistoryLoading.put((ThTransferHistory)obj);
			}
			else if(Tools.getSlaveType(command).equals("164F")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TrOperatorRole");
				trOperatorRoleLoading.put((TrOperatorRole)obj);
			}
			else if(Tools.getSlaveType(command).equals("1651")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TrRoleFunction");
				trRoleFunctionLoading.put((TrRoleFunction)obj);
			}
			else if(Tools.getSlaveType(command).equals("1652")){
				Object obj = protocolAnalyService.getTableFromControl(command, "TrServiceunit");
				trServiceunitLoading.put((TrServiceunit)obj);
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
