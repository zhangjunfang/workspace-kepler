package com.ctfo.storage.process.core;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.ServerSocket;
import java.net.Socket;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.util.ConfigLoader;

/**
 *	监控
 *
 */
public class Linstener extends Thread {
	private Logger log = LoggerFactory.getLogger(Linstener.class);
	/**	运行标记	*/
	public static boolean RUNNING = false;
	/**	启动标记	*/
	public static final String ST_START = "start";
	/**	状态标记	*/
	public static final String ST_STATUS = "status";
	/**	运行标记	*/
	private Integer listenPort = 9421;
	/**	运行标记	*/
	ServerSocket listener = null;
	
	public ConfigLoader configLoader;
	public Linstener(){
		setName("Process-Linstener");
	}


	/**
	 * 启动接口
	 */
	public void run() {
		log.info("管理线程启动，监听端口:"+this.listenPort);
		try {
			listener = new ServerSocket(listenPort);
			RUNNING = true;
			while(true) {
				Socket socket = listener.accept();
				new RcvThread(socket).start();
			}
		} catch (IOException e) {
			log.error("监听管理主线程异常:" + e.getMessage() , e);
		}
		log.info("管理线程停止！");
	}
	
	/**
	 * 数据接收处理线程
	 */
	private class RcvThread extends Thread {
		private Socket socket = null;
		public RcvThread(Socket socket) {
			this.socket = socket;
		}
		
		public void run() {
			BufferedWriter bw = null;
			BufferedReader br = null;
			try {
				socket.setSoTimeout(10*1000);
				bw = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));
				br = new BufferedReader(new InputStreamReader(socket.getInputStream()));
				String str = br.readLine();
				String rep = "";
				if (ST_STATUS.equals(str.toLowerCase())) {
					rep = getStatus();
				} else if (ST_START.equals(str.toLowerCase())) {
					rep = RUNNING ? "数据管理服务正在运行..." : "数据管理服务已停止！";
				} else {
					rep = getUseAge();
				}
				bw.write(rep+"\r\n");
				bw.flush();
				socket.close();
			} catch (IOException e) {
				log.error("管理线程"+getName()+"异常:" + e.getMessage() , e);
			}
		}
		
		public String getStatus() {
			String res = "";
			if (RUNNING) {
				res = "数据管理服务正在运行...";
			} else {
				res = "数据管理服务已停止！";
			}
			return res;
		}
		
		public String getUseAge() {
			return "Usage: DataSharingService <start|stop|status|version> [-f configfile]";
		}
	}

	/**
	 * @return 获取 运行标记
	 */
	public Integer getListenPort() {
		return listenPort;
	}
	/**
	 * 设置运行标记
	 * @param listenPort 运行标记 
	 */
	public void setListenPort(Integer listenPort) {
		this.listenPort = listenPort;
	}
	/**
	 * @return 获取 configLoader
	 */
	public ConfigLoader getConfigLoader() {
		return configLoader;
	}
	/**
	 * 设置configLoader
	 * @param configLoader configLoader 
	 */
	public void setConfigLoader(ConfigLoader configLoader) {
		this.configLoader = configLoader;
	}
	
}
