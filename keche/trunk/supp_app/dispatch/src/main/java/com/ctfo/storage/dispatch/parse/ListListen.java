package com.ctfo.storage.dispatch.parse;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.dispatch.service.MySqlService;
import com.ctfo.storage.dispatch.util.ConfigLoader;


/**
 * ListListen
 * 
 * 
 * @author huangjincheng
 * 2014-5-29上午10:08:59
 * 
 */
public class ListListen extends Thread{
	private static Logger log = LoggerFactory.getLogger(ListListen.class);
	
	private long batchTime = 30000; // 默认每30秒提交一次
	private MySqlService mySqlService = new MySqlService();
	
	public ListListen(){
		mySqlService = new MySqlService();
		mySqlService.setSqlMap(ConfigLoader.sqlMap); 
	}
	public void run(){
		while(true){
			long curTime1 = System.currentTimeMillis();
			log.info("TbDvr3G的大小和等待时间:"+TbDvr3GLoading.getList().size()+"/"+(curTime1-TbDvr3GLoading.getLastTime()));
		//	log.info(TbDvr3GLoading.getList().size() != 0);
			if(TbDvr3GLoading.getList().size() != 0 &&(curTime1-TbDvr3GLoading.getLastTime()) > batchTime){
				TbDvr3GLoading.updateList(mySqlService);
			}
			long curTime2 = System.currentTimeMillis();
			log.info("TbDvrSer的大小和等待时间:"+TbDvrSerLoading.getList().size()+"/"+(curTime2-TbDvrSerLoading.getLastTime()));
			if(TbDvrSerLoading.getList().size() != 0 &&(curTime2-TbDvrSerLoading.getLastTime()) >batchTime){
				TbDvrSerLoading.updateList(mySqlService);
			}
			
			long curTime3 = System.currentTimeMillis();
			log.info("TbOrg的大小和等待时间:"+TbOrgLoading.getList().size()+"/"+(curTime3-TbOrgLoading.getLastTime()));
			if(TbOrgLoading.getList().size() != 0 &&(curTime3-TbOrgLoading.getLastTime()) >batchTime){
				TbOrgLoading.updateList(mySqlService);
			}
			
			long curTime4 = System.currentTimeMillis();
			log.info("TbOrgInfo的大小和等待时间:"+TbOrgInfoLoading.getList().size()+"/"+(curTime4-TbOrgInfoLoading.getLastTime()));
			if(TbOrgInfoLoading.getList().size() != 0 &&(curTime4-TbOrgInfoLoading.getLastTime()) >batchTime){
				TbOrgInfoLoading.updateList(mySqlService);
			}
			
			long curTime5 = System.currentTimeMillis();
			log.info("TbPredefined的大小和等待时间:"+TbPredefinedMsgLoading.getList().size()+"/"+(curTime5-TbPredefinedMsgLoading.getLastTime()));
			if(TbPredefinedMsgLoading.getList().size() != 0 &&(curTime5-TbPredefinedMsgLoading.getLastTime()) >batchTime){
				TbPredefinedMsgLoading.updateList(mySqlService);
			}
			
			long curTime6 = System.currentTimeMillis();
			log.info("TbProductType的大小和等待时间:"+TbProductTypeLoading.getList().size()+"/"+(curTime6-TbProductTypeLoading.getLastTime()));
			if(TbProductTypeLoading.getList().size() != 0 &&(curTime6-TbProductTypeLoading.getLastTime()) >batchTime){
				TbProductTypeLoading.updateList(mySqlService);
			}
			
			long curTime7 = System.currentTimeMillis();
			log.info("TbSim的大小和等待时间:"+TbSimLoading.getList().size()+"/"+(curTime7-TbSimLoading.getLastTime()));
			if(TbSimLoading.getList().size() != 0 &&(curTime7-TbSimLoading.getLastTime()) >batchTime){
				TbSimLoading.updateList(mySqlService);
			}
			
			long curTime8 = System.currentTimeMillis();
			log.info("TbSpOperator的大小和等待时间:"+TbSpOperatorLoading.getList().size()+"/"+(curTime8-TbSpOperatorLoading.getLastTime()));
			if(TbSpOperatorLoading.getList().size() != 0 &&(curTime8-TbSpOperatorLoading.getLastTime()) >batchTime){
				TbSpOperatorLoading.updateList(mySqlService);
			}
			
			long curTime9 = System.currentTimeMillis();
			log.info("TbSpRole的大小和等待时间:"+TbSpRoleLoading.getList().size()+"/"+(curTime9-TbSpRoleLoading.getLastTime()));
			if(TbSpRoleLoading.getList().size() != 0 &&(curTime9-TbSpRoleLoading.getLastTime()) >batchTime){
				TbSpRoleLoading.updateList(mySqlService);
			}
			
			long curTime10 = System.currentTimeMillis();
			log.info("TbTerminal的大小和等待时间:"+TbTerminalLoading.getList().size()+"/"+(curTime10-TbTerminalLoading.getLastTime()));
			if(TbTerminalLoading.getList().size() != 0 &&(curTime10-TbTerminalLoading.getLastTime()) >batchTime){
				TbTerminalLoading.updateList(mySqlService);
			}
			
			long curTime11 = System.currentTimeMillis();
			log.info("TbTerminalOem的大小和等待时间:"+TbTerminalOemLoading.getList().size()+"/"+(curTime11-TbTerminalOemLoading.getLastTime()));
			if(TbTerminalOemLoading.getList().size() != 0 &&(curTime11-TbTerminalOemLoading.getLastTime()) >batchTime){
				TbTerminalOemLoading.updateList(mySqlService);
			}
			
			long curTime12 = System.currentTimeMillis();
			log.info("TbTerminalParam的大小和等待时间:"+TbTerminalParamLoading.getList().size()+"/"+(curTime12-TbTerminalParamLoading.getLastTime()));
			if(TbTerminalParamLoading.getList().size() != 0 &&(curTime12-TbTerminalParamLoading.getLastTime()) >batchTime){
				TbTerminalParamLoading.updateList(mySqlService);
			}
			
			long curTime13 = System.currentTimeMillis();
			log.info("TbTerminalProtocol的大小和等待时间:"+TbTerminalProtocolLoading.getList().size()+"/"+(curTime13-TbTerminalProtocolLoading.getLastTime()));
			if(TbTerminalProtocolLoading.getList().size() != 0 &&(curTime13-TbTerminalProtocolLoading.getLastTime()) >batchTime){
				TbTerminalProtocolLoading.updateList(mySqlService);
			}
			
			long curTime14 = System.currentTimeMillis();
			log.info("TbVehicle的大小和等待时间:"+TbVehicleLoading.getList().size()+"/"+(curTime14-TbVehicleLoading.getLastTime()));
			if(TbVehicleLoading.getList().size() != 0 &&(curTime14-TbVehicleLoading.getLastTime()) >batchTime){
				TbVehicleLoading.updateList(mySqlService);
			}
			
			long curTime15 = System.currentTimeMillis();
			log.info("ThTransferHistory的大小和等待时间:"+ThTransferHistoryLoading.getList().size()+"/"+(curTime15-ThTransferHistoryLoading.getLastTime()));
			if(ThTransferHistoryLoading.getList().size() != 0 &&(curTime15-ThTransferHistoryLoading.getLastTime()) >batchTime){
				ThTransferHistoryLoading.updateList(mySqlService);
			}
			
			long curTime16 = System.currentTimeMillis();
			log.info("TrOperatorRole的大小和等待时间:"+TrOperatorRoleLoading.getList().size()+"/"+(curTime16-TrOperatorRoleLoading.getLastTime()));
			if(TrOperatorRoleLoading.getList().size() != 0 &&(curTime16-TrOperatorRoleLoading.getLastTime()) >batchTime){
				TrOperatorRoleLoading.updateList(mySqlService);
			}
			
			long curTime17 = System.currentTimeMillis();
			log.info("TrRoleFunction的大小和等待时间:"+TrRoleFunctionLoading.getList().size()+"/"+(curTime17-TrRoleFunctionLoading.getLastTime()));
			if(TrRoleFunctionLoading.getList().size() != 0 &&(curTime17-TrRoleFunctionLoading.getLastTime()) >batchTime){
				TrRoleFunctionLoading.updateList(mySqlService);
			}
			
			long curTime18 = System.currentTimeMillis();
			log.info("TrServiceunit的大小和等待时间:"+TrServiceunitLoading.getList().size()+"/"+(curTime18-TrServiceunitLoading.getLastTime()));
			if(TrServiceunitLoading.getList().size() != 0 &&(curTime18-TrServiceunitLoading.getLastTime()) >batchTime){
				TrServiceunitLoading.updateList(mySqlService);
			}
			
			try {
				Thread.sleep(20000);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
		}
	}
}
