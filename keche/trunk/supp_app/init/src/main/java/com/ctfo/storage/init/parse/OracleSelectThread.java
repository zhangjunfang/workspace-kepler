package com.ctfo.storage.init.parse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.init.parse.MQSendThread.MQSendTbDvr3GThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbDvrSerThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbOrgInfoThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbOrgThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbPredefinedMsgThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbProductTypeThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbSimThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbSpOperatorThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbSpRoleThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbTerminalOemThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbTerminalParamThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbTerminalProtocolThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTbVehicleThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendThTransferHistoryThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTrOperatorRoleThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTrRoleFunctionThread;
import com.ctfo.storage.init.parse.MQSendThread.MQSendTrServiceunitThread;
import com.ctfo.storage.init.parse.MQSendThread.MQTbTerminalThread;
import com.ctfo.storage.init.service.OracleService;
import com.ctfo.storage.init.util.ConfigLoader;


/**
 * OracleSelectThread
 * 
 * 
 * @author huangjincheng
 * 2014-5-23上午10:13:12
 * 
 */
public class OracleSelectThread extends Thread{
	
	private static Logger log = LoggerFactory.getLogger(OracleSelectThread.class);
	private OracleService oracleService;
	
	public OracleSelectThread(){
		setName("OracleSelectThread");
		oracleService = new OracleService();
		oracleService.setSqlMap(ConfigLoader.sqlMap);
	}
	public void run(){
		log.info("--------------init初始化线程启动！需要初始化[18]张表！--------------");
		long startTime = System.currentTimeMillis();
		MQSendThread mq = new MQSendThread(startTime,18);
		//========================================================================
		MQSendTbDvr3GThread tbDvr3GThread = mq.new MQSendTbDvr3GThread();
		tbDvr3GThread.start();
		oracleService.tbDvr3GSelect(tbDvr3GThread);
				
		//============================================================================================		
		MQSendTbDvrSerThread tbDvrSerThread = mq.new MQSendTbDvrSerThread();
		tbDvrSerThread.start();
		oracleService.tbDvrSerSelect(tbDvrSerThread);
		
		//============================================================================================				
		MQSendTbOrgThread tbOrgThread = mq.new MQSendTbOrgThread();
		tbOrgThread.start();
		oracleService.tbOrgSelect(tbOrgThread);
			
		//============================================================================================
		MQSendTbOrgInfoThread tbOrgInfoThread = mq.new MQSendTbOrgInfoThread();
		tbOrgInfoThread.start();
		oracleService.tbOrgInfoSelect(tbOrgInfoThread);
		
		//============================================================================================				
		MQSendTbPredefinedMsgThread tbPredefinedMsgThread = mq.new MQSendTbPredefinedMsgThread();
		tbPredefinedMsgThread.start();
		oracleService.tbPredefinedMsgSelect(tbPredefinedMsgThread);
		
		//============================================================================================		
		MQSendTbProductTypeThread tbProductThread = mq.new MQSendTbProductTypeThread();
		tbProductThread.start();
		oracleService.tbProductTypeSelect(tbProductThread);
		
		//============================================================================================		
		MQSendTbSimThread tbSimThread = mq.new MQSendTbSimThread();
		tbSimThread.start();
		oracleService.tbSimSelect(tbSimThread);
		
		//============================================================================================		
		MQSendTbSpOperatorThread tbSpOperatorThread = mq.new MQSendTbSpOperatorThread();
		tbSpOperatorThread.start();
		oracleService.tbSpOperatorSelect(tbSpOperatorThread);
		
		//============================================================================================		
		MQSendTbSpRoleThread tbSpRoleThread = mq.new MQSendTbSpRoleThread();
		tbSpRoleThread.start();
		oracleService.tbSpRoleSelect(tbSpRoleThread);
		
		//============================================================================================		
		MQTbTerminalThread tbTerminalThread = mq.new MQTbTerminalThread();
		tbTerminalThread.start();
		oracleService.tbTerminalSelect(tbTerminalThread);		
		
		//============================================================================================		
		MQSendTbTerminalOemThread tbTerminalOemThread = mq.new MQSendTbTerminalOemThread();
		tbTerminalOemThread.start();
		oracleService.tbTerminalOemSelect(tbTerminalOemThread);
		
		//============================================================================================	
		MQSendTbTerminalParamThread tbTerminalParamThread = mq.new MQSendTbTerminalParamThread();
		tbTerminalParamThread.start();
		oracleService.tbTerminalParamSelect(tbTerminalParamThread);
		
		//============================================================================================
		MQSendTbTerminalProtocolThread tbTerminalProtocolThread = mq.new MQSendTbTerminalProtocolThread();
		tbTerminalProtocolThread.start();
		oracleService.tbTerminalProtocolSelect(tbTerminalProtocolThread);
		
		//============================================================================================		
		MQSendTbVehicleThread tbVehicleThread = mq.new MQSendTbVehicleThread();
		tbVehicleThread.start();
		oracleService.tbVehicleSelect(tbVehicleThread);	
		
		//============================================================================================
		MQSendThTransferHistoryThread thTransferHistoryThread = mq.new MQSendThTransferHistoryThread();
		thTransferHistoryThread.start();
		oracleService.thTransferHistorySelect(thTransferHistoryThread);
		
		//============================================================================================
		MQSendTrOperatorRoleThread trOperatorRoleThread = mq.new MQSendTrOperatorRoleThread();
		trOperatorRoleThread.start();
		oracleService.trOperatorRoleSelect(trOperatorRoleThread);
		
		//============================================================================================		
		MQSendTrRoleFunctionThread trRoleFunctionThread = mq.new MQSendTrRoleFunctionThread();
		trRoleFunctionThread.start();
		oracleService.trRoleFunctionSelect(trRoleFunctionThread);
		
		//============================================================================================
		MQSendTrServiceunitThread trServiceunitThread = mq.new MQSendTrServiceunitThread();
		trServiceunitThread.start();
		oracleService.trServiceunitSelect(trServiceunitThread);
		
	}
}
