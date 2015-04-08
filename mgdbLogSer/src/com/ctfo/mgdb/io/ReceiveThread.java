package com.ctfo.mgdb.io;

import java.io.BufferedReader;
import java.io.PrintWriter;
import java.net.Socket;

import org.apache.log4j.Logger;

import com.ctfo.mgdb.beans.Message;
import com.ctfo.mgdb.beans.UserInfo;
import com.ctfo.mgdb.util.AnalyUtil;
import com.ctfo.mgdb.util.SocketUtil;
import com.ctfo.mgdb.util.XmlConfUtil;
import com.ctfo.mgdbser.analy.AnalyseServiceInit;
public class ReceiveThread extends Thread {
	
	XmlConfUtil config = new XmlConfUtil();
	public UserInfo userInfo ;
	public Socket socket;
	private static final Logger logger = Logger.getLogger(ReceiveThread.class);
	/**
	 * 主线程对象
	 */
	/**
	 * 各线程是否需要保持运行的标志
	 */
	public int i = 0;
	public boolean isRunning;
	String msg_send = "mgserClient:>>>>>>>>>>>>>>>>>>>>> ";
	String msg_recive = "mgserClient:<<<<<<<<<<<<<<<<<<<< ";
	public PrintWriter out;
	public BufferedReader in;
	long lastSendNoopTime;
	long lastReceiveNoopTime;
	

	public ReceiveThread(boolean isRunning,UserInfo userInfo) throws Exception {
		this.isRunning = isRunning;
		this.userInfo = userInfo;
	}

	/**
	 * 建立Socket连接
	 */
	public void connect() {
		try {
			
			socket = SocketUtil.createSocket(userInfo.getMsgServiceAddr(),
					userInfo.getMsgServicePort(),
					userInfo.getReConnectTime());			
		} catch (Exception e) {
			logger.debug("连接MSG失败！"+e.getMessage());
		} 
		in = SocketUtil.createReader(socket);
		out = SocketUtil.createWriter(socket);
	}
	/**
	 * 登陆
	 */
	private void sendLoginData() {

		String logindata = "LOGI " + userInfo.getLogintype() + " "
				+ userInfo.getUserid() + " " + userInfo.getPassword()
				+ " \r\n";
		wirteData(logindata);
	}

	/**
	 * 心跳
	 */
	private void sendNoopData() {
		// 发送心跳30s
		if (System.currentTimeMillis() - lastSendNoopTime >= 30000) {
			logger.info("Receive data statistics： 30s接收数据：" + i);
			i = 0;
			wirteData("NOOP_ACK \r\n");
			lastSendNoopTime = System.currentTimeMillis();

		}
	}

	/**
	 * 发送数据
	 */
	private void wirteData(String data) {
		out.write(data);
		out.flush();
		logger.info(msg_send + data.replace("\r\n", ""));
	}

	/**
	 * 线程执行体
	 */
	public void run() {
		/*msginfo = luserInfo.getMsgServiceAddr() + ":"
				+ luserInfo.getMsgServicePort();*/
		String s;
		
		// 登陆
		sendLoginData();
		int reConnectTime = userInfo.getReConnectTime();
		
		lastReceiveNoopTime = System.currentTimeMillis();
		AnalyseServiceInit ai = new AnalyseServiceInit(config);
		try {
			ai.init();
		} catch (Exception e) {
			logger.debug("分析线程初始化错误！"+e.getMessage());
		} 
		while (isRunning) {
			try {
				if(in.ready()){
					s = in.readLine();
					if (s != null && !"".equals(s.trim()) && "LACK".equals(s.substring(0, 4))) {
						logger.debug(msg_recive  + s);
					
					}else {
						int judge = AnalyUtil.judgeData(s);
						if(judge!=-1){
					
							logger.debug(msg_recive  + s);
							i++;
							Message message = new Message();
							message.setEnable_flag(judge);
							message.setCommand(s);
							message.setMsgid(userInfo.getMsgServiceAddr());
		
							//DataPool.setReceivePacketValue(message);
						
							ai.getAnalyseServiceThread() [0]
									.addPacket(message);
						}
						
					}
					
					lastReceiveNoopTime = System.currentTimeMillis();
				}
				
			
				

				sendNoopData();
				//sendData();
				//System.out.println(lastReceiveNoopTime-lastSendNoopTime);
				if (lastSendNoopTime -lastReceiveNoopTime >= reConnectTime) {
					logger.info("长时间无数据接收，重新连接!");
					SocketUtil.closeReader(in);
					SocketUtil.closeWriter(out);
					SocketUtil.closeSocket(socket);
					connect();
					sendLoginData();
					i= 0;
					//sendLoginData();
					lastReceiveNoopTime = lastSendNoopTime = System
					.currentTimeMillis();
				}

			} catch (Exception e) {
				logger.error("接收数据错误:" + e.getMessage());

				/*try {
					sleep(2000);
					SocketUtil.closeReader(in);
					SocketUtil.closeWriter(out);
					SocketUtil.closeSocket(socket);
					connect();

					sendLoginData();
				} catch (Exception ee) {
					logger.error(msginfo + "关闭数据错误:" + ee.getMessage());
				}*/

			}

		}
	}
}
