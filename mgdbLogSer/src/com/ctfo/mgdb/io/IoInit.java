package com.ctfo.mgdb.io;

import org.apache.log4j.Logger;
import org.dom4j.DocumentException;

import com.ctfo.mgdb.beans.UserInfo;
import com.ctfo.mgdb.util.XmlConfUtil;


public class IoInit {
	
	public static final Logger log = Logger.getLogger(IoInit.class);
	public XmlConfUtil config;


	public IoInit() {
		this.config = new XmlConfUtil();
	}

	public ReceiveThread init() {
			ReceiveThread receiveThread = null;

			UserInfo userInfo = new UserInfo();
			try {
				userInfo.setMsgServiceAddr(config.getStringValue("msgServiceManage|msgServiceAddr"));	
				userInfo.setMsgServicePort(Integer.parseInt(config.getStringValue("msgServiceManage|msgServicePort")));
				userInfo.setReConnectTime(Integer.parseInt(config.getStringValue("msgServiceManage|reConnectTime")));
				userInfo.setUserid(config.getStringValue("msgServiceManage|userid"));
				userInfo.setPassword(config.getStringValue("msgServiceManage|password"));
				userInfo.setLogintype(config.getStringValue("msgServiceManage|logintype"));
			//	userInfo.setFileAddr("src/mgdbService.xml");
			} catch (DocumentException e) {
				log.info("解析配置文件错误！"+e.getMessage());
			}
			log.info("Mgdbser is initialized");
			// 启动报文接收线程
			try {
				receiveThread = new ReceiveThread(true,userInfo);
				receiveThread.connect();
				receiveThread.start();
			} catch (Exception e) {
				log.debug("服务线程启动错误！"+e.getMessage());
			}

			
			return receiveThread;
					
	}
	
	
	public static void main(String[] args) throws Exception {
		new IoInit().init();
	}
}
