package com.ctfo.savecenter.io;

import java.io.BufferedReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.text.SimpleDateFormat;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.analy.AnalyseServiceInit;
import com.ctfo.savecenter.beans.Message;
import com.ctfo.savecenter.beans.UserInfo;
import com.ctfo.savecenter.util.SocketUtil;

/**
 * 数据收发服务
 */
public class ReceiveThread extends Thread {

	public Socket socket;
	private static final Logger logger = LoggerFactory.getLogger(ReceiveThread.class);
	/**
	 * IP,端口,断线重连时间
	 */

	UserInfo luserInfo;
	/**
	 * 主线程对象
	 */
	/**
	 * 各线程是否需要保持运行的标志
	 */
	public boolean isRunning = true;
	String msginfo = "";
	public PrintWriter out;
	public BufferedReader in;
	long lastNoopTime;
	long lastReceiveNoopTime;

	long receivecount;

	SimpleDateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");

	public ReceiveThread(UserInfo userInfo) throws Exception {

		luserInfo = userInfo;
		connect();
	}

	/**
	 * 建立Socket连接
	 */
	private void connect() {
		socket = SocketUtil.createSocket(luserInfo.getMsgServiceAddr(),
				luserInfo.getMsgServicePort(), luserInfo.getReConnectTime());
		in = SocketUtil.createReader(socket);
		out = SocketUtil.createWriter(socket);
	}

	/**
	 * 心跳
	 */
	private void sendNoopData() {
		// 发送心跳30s
		if (System.currentTimeMillis() - lastNoopTime >= 30000) {
			logger.info(msginfo + " 30s接收：" + receivecount);
			receivecount = 0;

			wirteData("NOOP \r\n");

			lastNoopTime = System.currentTimeMillis();

		}
	}

	/**
	 * 登陆
	 */
	private void sendLoginData() {

		String logindata = "LOGI " + luserInfo.getLogintype() + " "
				+ luserInfo.getUserid() + " " + luserInfo.getPassword()
				+ " \r\n";
		wirteData(logindata);
	}

	/**
	 * 发送数据
	 */
//	private void sendData() {
//		Message message = DataPool.getSendPacketValue();
//		if (message != null)
//			wirteData(message.getCommand());
//	}

	/**
	 * 发送数据
	 */
	private void wirteData(String data) {
		out.write(data);
		out.flush();
		logger.info(msginfo + "发送数据 :" + data.replace("\r\n", ""));
	}

	/**
	 * 线程执行体
	 */
	public void run() {
		msginfo = luserInfo.getMsgServiceAddr() + ":"
				+ luserInfo.getMsgServicePort();
		String s;
		// 登陆
		sendLoginData();

		lastReceiveNoopTime = System.currentTimeMillis();
		sendNoopData();
		receivecount = 0;
		String msgid = "0";

		while (isRunning) {
			try {
				if (in.ready()) {
					s = in.readLine();
					DataPool.setCountValue();
					
					logger.debug(msginfo + "收到:" + s);
					if (s != null && !"".equals(s.trim()) && "LACK".equals(s.substring(0, 4))) {
						String[] command = s.split("\\s+");
						if (command.length == 5) {
							msgid = command[4];
						}
					}
					if ((s != null && !"".equals(s.trim())) && (!s.equals("NOOP_ACK") || !s.equals("LACK"))) {
						Message message = new Message();
						message.setCommand(s);
						message.setMsgid(msgid);

						//DataPool.setReceivePacketValue(message);
						
						AnalyseServiceInit.getAnalyseServiceThread() [0]
								.addPacket(message);
					}
					lastReceiveNoopTime = System.currentTimeMillis();

				} else {
					sleep(1);
				}

				sendNoopData();
				//sendData();

				if (lastNoopTime - lastReceiveNoopTime >= luserInfo
						.getConnectStateTime()) {
					logger.info(msginfo + "长时间没收到数据重新连接");
					lastReceiveNoopTime = lastNoopTime = System
							.currentTimeMillis();

					SocketUtil.closeReader(in);
					SocketUtil.closeWriter(out);
					SocketUtil.closeSocket(socket);
					connect();
					sendLoginData();
				}

			} catch (Exception e) {
				logger.error(msginfo + "接收数据错误:",e);

				try {
					sleep(2000);
					SocketUtil.closeReader(in);
					SocketUtil.closeWriter(out);
					SocketUtil.closeSocket(socket);
					connect();
					sendLoginData();
				} catch (Exception ee) {
					logger.error(msginfo + "关闭数据错误:" + ee.getMessage());
				}

			}

		}
	}
}
