package com.ctfo.analy.io;


import org.apache.log4j.Logger;

import com.ctfo.analy.beans.UserInfoBean;
import com.lingtu.xmlconf.XmlConf;

/**
 * 初始化IO
 * @author Administrator
 *
 */
public class IoInit {
	private static final Logger logger = Logger.getLogger(IoInit.class);

	XmlConf config;

	public IoInit(XmlConf config) {
		this.config = config;
	}

	public ReceiveThread init() throws Exception {
			String manageFlag = config.getStringValue("ManageFlag");
			if ("1".equals(manageFlag)) {
				// 多msg处理
				String msg;
				String nodeName;
				int i = 1;
				ReceiveThread receiveThread=null;
				while (true) {
					nodeName = "msgServiceManage|msg" + i;
					msg = config.getStringValue("msgServiceManage|msg" + i);
					if (msg == null)
						break;
					UserInfoBean userInfo = new UserInfoBean();
					userInfo.setMsgServicePort(config.getIntValue(nodeName + "|msgServicePort"));
					userInfo.setMsgServiceAddr(config.getStringValue(nodeName + "|msgServiceAddr"));
					userInfo.setReConnectTime(config.getIntValue(nodeName + "|reConnectTime"));
					userInfo.setConnectStateTime(config.getIntValue(nodeName + "|connectStateTime"));
					userInfo.setLogintype(config.getStringValue(nodeName + "|logintype"));
					userInfo.setUserid(config.getStringValue(nodeName + "|userid"));
					userInfo.setPassword(config.getStringValue(nodeName + "|password"));

					// 启动报文接收线程
					receiveThread = new ReceiveThread(userInfo);
					receiveThread.start();
					i++;
				}
				return receiveThread;
			} else {
				// 初始化节点管理器
				ConfigServer c = new ConfigServer();
				c.init(config);
				
				SendMsgThread sendMsgThread=new SendMsgThread();
				sendMsgThread.start();
			return null;
		}
	}
	
	/**
	 * 调整直连方式代码
	 * @return
	 * @throws Exception
	 */
	public ReceiveThread init1() throws Exception {
		String manageFlag = config.getStringValue("ManageFlag");
		if ("1".equals(manageFlag)) {
			
			
			// 多msg处理
			
			String msg;
			String nodeName;
			int i = 1;

			while (true) {
				nodeName = "msgServiceManage|msg" + i;
				msg = config.getStringValue("msgServiceManage|msg" + i);
				if (msg == null)
					break;
				/*UserInfoBean userInfo = new UserInfoBean();
				userInfo.setMsgServicePort(config.getIntValue(nodeName + "|msgServicePort"));
				userInfo.setMsgServiceAddr(config.getStringValue(nodeName + "|msgServiceAddr"));
				userInfo.setReConnectTime(config.getIntValue(nodeName + "|reConnectTime"));
				userInfo.setConnectStateTime(config.getIntValue(nodeName + "|connectStateTime"));
				userInfo.setLogintype(config.getStringValue(nodeName + "|logintype"));
				userInfo.setUserid(config.getStringValue(nodeName + "|userid"));
				userInfo.setPassword(config.getStringValue(nodeName + "|password"));*/
				
				//IoHandler handle = new IoHandler();
				//handle.setUserName(config.getStringValue(nodeName + "|userid"));
				//handle.setPassword(config.getStringValue(nodeName + "|password"));
				/*handle.setGroup(group);
				handle.setGroupId(groupId);*/
				//handle.setLoginType(config.getStringValue(nodeName + "|logintype"));
				
				/*ReceiveThread rt = new ReceiveThread(userInfo);
				rt.start();*/
				
				
				CommClient ioClient = new CommClient();
				ioClient.setHost(config.getStringValue(nodeName + "|msgServiceAddr"));
				ioClient.setPort(config.getIntValue(nodeName + "|msgServicePort"));
				ioClient.setUserName(config.getStringValue(nodeName + "|userid"));
				ioClient.setPassword(config.getStringValue(nodeName + "|password"));
				ioClient.setLoginType(config.getStringValue(nodeName + "|logintype"));
				
				try {
					ioClient.connect();
					logger.info("-DataAnalyMain--(3)-通讯部分启动完成");
				} catch (InterruptedException e) {
					throw new Exception("初始化通讯服务异常:"+e.getMessage(),e);
				}
				i++;
			}
			return null;
		} else {
			// 初始化节点管理器
			ConfigServer c = new ConfigServer();
			c.init(config);
			
			SendMsgThread sendMsgThread=new SendMsgThread();
			sendMsgThread.start();
		return null;
	}
}
}
