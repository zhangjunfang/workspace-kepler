package com.kypt.c2pp.back;

import java.util.Timer;
import java.util.TimerTask;
import java.util.concurrent.ConcurrentHashMap;

public class DownCommandSeqQueueMap extends ConcurrentHashMap<String,DownCommandSummaryBean> {

	private static final DownCommandSeqQueueMap dcq =new DownCommandSeqQueueMap();
	
	public static DownCommandSeqQueueMap getInstance(){
		return dcq;
	}
	
	public DownCommandSeqQueueMap(){
		//启动时要同时启动内部失效时间调度线程,每秒判定一次
		ElementTimeOut eto = new ElementTimeOut(1000);
	}
	
	@Override
	public DownCommandSummaryBean get(Object key) {
		// TODO Auto-generated method stub
		return super.get(key);
	}

	@Override
	public DownCommandSummaryBean put(String key, DownCommandSummaryBean value) {
		// TODO Auto-generated method stub
		return super.put(key, value);
	}

	@Override
	public DownCommandSummaryBean remove(Object key) {
		// TODO Auto-generated method stub
		return super.remove(key);
	}
	
	//下行消息要设置回复超时时间，如果超时时间结束时未收到回复信息，则从map中移除该消息，并向对应服务器发送应答超时
	class ElementTimeOut extends Timer{
		public ElementTimeOut(int delay) {
		    super(true); // Daemon thread
		    schedule(new TimerTask() {
		      public void run() {
		        for (String key:dcq.keySet()){
		        	DownCommandSummaryBean dcsb = DownCommandSeqQueueMap.getInstance().remove(key);
		        	if (dcsb.getTimeout()-1<=0){
		        		//向服务器发送应答超时指令
		        		/*String command = dcsb.getCommand();
		        		if (DownQuestionReq.COMMAND.equals(command)){
		        			DownQuestionResp resp = new DownQuestionResp();
		        		}else if (DownTextReq.COMMAND.equals(command)){
		        			
		        		}else if (MediaDataQueryReq.COMMAND.equals(command)){
		        			
		        		}else if (PhotoGraphReq.COMMAND.equals(command)){
		        			
		        		}else if (QueryTerminalParamReq.COMMAND.equals(command)){
		        			
		        		}else if (SetTerminalEventReq.COMMAND.equals(command)){
		        			
		        		}else if (SetTerminalParamReq.COMMAND.equals(command)){
		        			
		        		}else if (TerminalControlReq.COMMAND.equals(command)){
		        			
		        		}else if (UpLoadDataReq.COMMAND.equals(command)){
		        			
		        		}*/
		        	}else{
		        		dcsb.setTimeout(dcsb.getTimeout()-1);
		        		DownCommandSeqQueueMap.getInstance().put(key, dcsb);
		        	}
		        }
		      }
		    }, delay);
		  }
	}
}
