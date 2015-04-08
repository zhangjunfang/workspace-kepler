package com.ctfo.savecenter.io;

import com.ctfo.savecenter.beans.UserInfo;
import com.lingtu.xmlconf.XmlConf;

public class IoInit {

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
					UserInfo userInfo = new UserInfo();
					userInfo.setMsgServicePort(config.getIntValue(nodeName
							+ "|msgServicePort"));
					userInfo.setMsgServiceAddr(config.getStringValue(nodeName
							+ "|msgServiceAddr"));
					userInfo.setReConnectTime(config.getIntValue(nodeName
							+ "|reConnectTime"));
					userInfo.setConnectStateTime(config.getIntValue(nodeName
							+ "|connectStateTime"));
					userInfo.setLogintype(config.getStringValue(nodeName
							+ "|logintype"));
					userInfo.setUserid(config.getStringValue(nodeName
							+ "|userid"));
					userInfo.setPassword(config.getStringValue(nodeName
							+ "|password"));

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
				return null;
			}
	}
}
