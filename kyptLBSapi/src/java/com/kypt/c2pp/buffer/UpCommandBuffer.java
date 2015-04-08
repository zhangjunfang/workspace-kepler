package com.kypt.c2pp.buffer;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;

import org.apache.log4j.MDC;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.back.DownCommandSeqQueueMap;
import com.kypt.c2pp.back.DownCommandSummaryBean;
import com.kypt.c2pp.back.SupCommandQueueMap;
import com.kypt.c2pp.inside.msg.InsideMsg;
import com.kypt.c2pp.inside.msg.InsideMsgResp;
import com.kypt.c2pp.inside.msg.resp.DownQuestionResp;
import com.kypt.c2pp.inside.msg.resp.DownTextResp;
import com.kypt.c2pp.inside.msg.resp.LocationReportResp;
import com.kypt.c2pp.inside.msg.resp.MediaDataUpResp;
import com.kypt.c2pp.inside.msg.resp.PhotoGraphResp;
import com.kypt.c2pp.inside.msg.resp.SetTerminalEventResp;
import com.kypt.c2pp.inside.msg.resp.SetTerminalParamResp;
import com.kypt.c2pp.inside.msg.resp.TerminalListenerResp;
import com.kypt.c2pp.inside.msg.resp.UpTerminalParamResp;
import com.kypt.c2pp.nio.SupCommService;
import com.kypt.c2pp.util.StringUtil;
import com.kypt.configuration.C2ppCfg;

/**
 * 命令分发缓冲区：根据命令分发规则把命令分发到不同的上行发送队列。
 * 分发规则：实时数据：全部转发
 *         下行指令回复信息：根据seqId分发到对应服务器的发送队列
 * @author yujch
 *
 */
public class UpCommandBuffer implements Runnable{

	private static Logger log = LoggerFactory.getLogger(UpCommandBuffer.class);

    private static final UpCommandBuffer upCommandBuffer = new UpCommandBuffer();
    
    private static final String NAME = "<UpCommandBuffer>";

    private Queue<InsideMsgResp> commandQueue;//主队列
    
    private Queue<InsideMsgResp> commandCachingQueue;//缓冲队列
    
    private SupCommService supCommService;

    private ThreadPool pool = ThreadPool.instance();
    
    private boolean shutdownFlag = true; 
    
    private  boolean cacheQueueFlag = false;//缓冲队列中是否有值
    
    //private boolean cacheFileFlag = false;//缓冲文件中是否有值
    
    //private String cacheFileName = "cachefile.txt";

    private UpCommandBuffer() {
    	commandQueue = new LinkedList<InsideMsgResp>();

        //判断缓冲文件中是否有值
        
    }

    public static UpCommandBuffer getInstance() {
        return upCommandBuffer;
    }

    /**
     * 向队列中加入一条上行命令
     * @param sql
     */
    public synchronized void add(InsideMsgResp msg) {
    	if (commandQueue.size()>=1000000){
    		cacheQueueAdd(commandQueue.poll());
    	}
    	commandQueue.offer(msg);
    }

    /**
     * 向队列中加入一批上行命令
     * @param sqlList
     */
    public synchronized void add(List<InsideMsgResp> msgList) {
    	commandQueue.addAll(msgList);
    }

    /**
     * 向队列中加入一批sql
     * @param sqlList
     */
    public synchronized void add(InsideMsgResp[] msgArray) {
        for (InsideMsgResp msg : msgArray) {
        	commandQueue.offer(msg);
        }
    }
    
    public synchronized void cacheQueueAdd(InsideMsgResp msg){
    	if (commandCachingQueue.size()>=1000000){
    		commandCachingQueue.poll();
    	}
    	commandCachingQueue.offer(msg);
    	cacheQueueFlag = true;
    }
    

    @SuppressWarnings("static-access")
	public void run(){
        MDC.put("session", "[" + StringUtil.getLogRadomStr() + "]");
        MDC.put("modulename", "[UpCommandBuf]");
        if (shutdownFlag){
        	shutdownFlag = false;
        }
        System.out.println("send command thread begin::"+shutdownFlag);
        
        try{

        while(!shutdownFlag){
        	InsideMsgResp msg = commandQueue.poll();
            if (null == msg ) {
                log.debug(NAME+"缓冲主队列中暂时没有数据！");
                System.out.println(NAME+"缓冲主队列中暂时没有数据！");
                if (cacheQueueFlag){
                	msg = commandCachingQueue.poll();
                }
                if (null==msg){
                	log.debug(NAME+"二级缓冲队列中暂时没有数据！");
                	System.out.println(NAME+"二级缓冲队列中暂时没有数据！");
                	cacheQueueFlag = false;
                	try { 
                        Thread.sleep(Long.parseLong(C2ppCfg.props.getProperty("sleepTimeWhenCommandQueueIsNull")));
                    } catch (InterruptedException e) {
                        log.error(NAME+"缓冲队列处理在休眠时出现异常，" + e);
                    }
                    continue;
                }
            }
            
            
            if (LocationReportResp.COMMAND.equals(msg.getCommand())){
            	//位置信息汇报信息：全部转发
            	for (String key:SupCommandQueueMap.getInstance().keySet()){
            		if (!forwardMsg(key,msg)){
            			log.error("分发信息失败！");
            			cacheQueueAdd(msg);
            		}
            	}
            }else if (DownQuestionResp.COMMAND.equals(msg.getCommand())||DownTextResp.COMMAND.equals(msg.getCommand())||MediaDataUpResp.COMMAND.equals(msg.getCommand())||PhotoGraphResp.COMMAND.equals(msg.getCommand())||SetTerminalEventResp.COMMAND.equals(msg.getCommand())||SetTerminalParamResp.COMMAND.equals(msg.getCommand())||TerminalListenerResp.COMMAND.equals(msg.getCommand())||UpTerminalParamResp.COMMAND.equals(msg.getCommand())){
            	//终端拍照、调度信息等的回复信息:根据下行服务器的IP及应答序列转发
            	String respSeqId = msg.getRespSeqId();
            	for (String key:SupCommandQueueMap.getInstance().keySet()){
            		DownCommandSummaryBean dcsb = DownCommandSeqQueueMap.getInstance().remove(key+respSeqId);
            		
	            	if (dcsb!=null&&dcsb.getAddress()!=null&&!"".equals(dcsb.getAddress())){
	            		if (!forwardMsg(dcsb.getAddress(),msg)){
	            			log.error("分发信息失败！");
	            			cacheQueueAdd(msg);
	            		}
	            	}
            	}
            }
            log.warn(NAME+"当前上行命令队列大小为:" + commandQueue.size());
        }      
//        log.warn(NAME+"数据库缓冲队列处理终止！");
        }finally{
        	this.run();
        }
    }
    
    
    private boolean forwardMsg(String address,InsideMsgResp msg){
    	try {
    		//此处要判断各服务器的发送队列是否已满，若满则需要把信息放入二级缓冲队列中，
    		//当一级缓冲队列无信息时发送二级缓冲队列中数据。
    		//当二级缓冲队列满时考虑使用缓冲文件
    		if (SupCommandQueueMap.getInstance().get(address)!=null&&SupCommandQueueMap.getInstance().get(address).size()>=10000){
    			cacheQueueAdd(msg);
    		}else{
    			System.out.println(NAME+"中转数据！"+address);
    			SupCommandQueueMap.getInstance().get(address).put(msg);
    		}
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			log.error(NAME +"向"+address+"中转信息失败！"+msg.toString()+"\r\n"+e);
			return false;
		}
    	return true;
    }


    public void shutdown() {
        log.info("<commandQueue>开始执行线程池关闭操作");
        shutdownFlag = true;
        
        pool.shutdown();
        log.info("<commandQueue>sqlQueue","线程池关闭结束！");
    }

	public boolean isShutdownFlag() {
		return shutdownFlag;
	}

	public void setShutdownFlag(boolean shutdownFlag) {
		this.shutdownFlag = shutdownFlag;
	}

}

