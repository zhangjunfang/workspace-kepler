package com.kypt.c2pp.nio;

import java.util.concurrent.BlockingQueue;

import org.apache.log4j.MDC;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.back.SupCommandQueueMap;
import com.kypt.c2pp.buffer.ThreadPool;
import com.kypt.c2pp.buffer.UpCommandBuffer;
import com.kypt.c2pp.inside.msg.InsideMsgResp;
import com.kypt.c2pp.util.StringUtil;
import com.kypt.configuration.C2ppCfg;

public class SupUpCommandSend implements Runnable {

	private static Logger log = LoggerFactory.getLogger(SupUpCommandSend.class);

	private static final String NAME = "<SupUpCommandSend>";

	private ThreadPool pool = ThreadPool.instance();

	private SupCommService supCommService;

	private String address = null;

	private boolean shutdownFlag = true;

	public SupUpCommandSend(SupCommService supCommService) {
		this.supCommService = supCommService;
		this.address = supCommService.getRemoteAddress();
		shutdownFlag = false;
	}

	class ExeUpMsgRunner implements Runnable {
		private InsideMsgResp msgs;

		public ExeUpMsgRunner(InsideMsgResp msgs) {
			this.msgs = msgs;
		}

		public void run() {
			try {
				log.info(NAME + "开始将缓冲队列中的1个命令发送给服务器"+address+"!");
				System.out.println(NAME + "开始将缓冲队列中的1个命令发送给服务器"+address+"!");
				// 发送上行信息
				if (supCommService == null) {
					throw new Exception(NAME + "与服务器"+address+"之间链接断开，请重新连接。");
				}
				supCommService.send(msgs.toString().getBytes(C2ppCfg.props.getProperty("superviseEncoding")));
				log.info(NAME + "已成功将缓冲队列中的1个命令发送！");
			} catch (Exception e) {
				log.error(NAME + "缓冲队列上传时出现系统异常：" + e);
				linkDown(msgs);
			}
		}
	}

	@SuppressWarnings("static-access")
	public void run() {
		MDC.put("session", "[" + StringUtil.getLogRadomStr() + "]");
		MDC.put("modulename", "[SupUpCommandSend]");

		InsideMsgResp resp;
		try {
			while (!shutdownFlag) {
				resp = SupCommandQueueMap.getInstance().get(address).take();

				 System.out.println("上行命令："+resp.toString());
				ExeUpMsgRunner runner = new ExeUpMsgRunner(resp);
				if (!pool.start(runner, pool.HIGH_PRIORITY)) {
					log.info(NAME+ "用于执行命令发送的线程池已满！将该上行命令重新放入缓冲区中，并休眠"
							+ C2ppCfg.props.getProperty("sleepTimeWhenCommandThreadPoolFull")
							+ "毫秒!");
					SupCommandQueueMap.getInstance().get(address).put(resp);
				}
				log.warn(NAME + "当前上行命令队列大小为:" + SupCommandQueueMap.getInstance().get(address).size());
			}
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			log.error(NAME + "向服务器" + supCommService.getRemoteAddress()
					+ "发送上行命令失败:" + e.toString());
			e.printStackTrace();
		}
	}

	/*public void shutdown() {
		log.info("<commandQueue>开始执行线程池关闭操作");
		shutdownFlag = true;

		pool.shutdown();
		log.info("<commandQueue>sqlQueue", "线程池关闭结束！");
	}*/
	
	public void linkDown(InsideMsgResp resp){
		supCommService = null;
		shutdownFlag = true;
		if (resp!=null){
			UpCommandBuffer.getInstance().cacheQueueAdd(resp);
		}
		//当检测到链接断开时，首先链接信息从队列中移除,然后保存队列中未发送的消息。
		BlockingQueue<InsideMsgResp> bqueue = SupCommandQueueMap.getInstance().remove(address);

		UpCommandBuffer.getInstance().cacheQueueAdd(bqueue.poll());

	}

}
