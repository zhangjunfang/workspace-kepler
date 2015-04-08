package com.ctfo.analy.io;

import com.ctfo.node.client.ConfigServerClientHandler;
import com.ctfo.node.client.MinaClient;
 
import com.lingtu.xmlconf.XmlConf;

/**
 * 节点管理器初始化
 * 
 * @author Administrator
 * 
 */
public class ConfigServer {


	private String localIP;// 本地IP
	private short localPort;// 本地端口
	private int loginID;// 登陆id
	private int conNums;// 连接数
	private String handlerClass;// 处理类
	private String configIP;// 节点管理器IP
	private int configPort;// 节点管理器端口

	private ConfigServerClientHandler handler;
	private MinaClient minaClient;

	public ConfigServer() {

	}

	public boolean init(XmlConf config) throws InterruptedException {

		localIP = config.getStringValue("configServer|localIP");
		localPort = (short) config.getIntValue("configServer|localPort");
		loginID = config.getIntValue("configServer|loginID");
		conNums = config.getIntValue("configServer|conNums");
		handlerClass = config.getStringValue("configServer|handlerClass");
		configIP = config.getStringValue("configServer|configIP");
		configPort = config.getIntValue("configServer|configPort");

		handler = new ConfigServerClientHandler(localIP, localPort, loginID,conNums, handlerClass);

		minaClient = new MinaClient(configIP, configPort, handler);

		minaClient.USE_CUSTOM_CODEC = false;
		minaClient.setReconTimes(10);
		minaClient.connect();

		return true;
	}

	/**
	 * quit
	 * 
	 * @return
	 */
	public boolean quit() {
		minaClient.quit("");
		return true;
	}

	public static void main(String[] args) {
		// ConfigServer c = new ConfigServer();
		// try {
		// c.init();
		// } catch (InterruptedException e) {
		// // TODO Auto-generated catch block
		// e.printStackTrace();
		// }

		// MsgConnectUtil.sendMessage(String ip, short port, Object o)
		// //根据IP和port发送消息
		// MsgConnectUtil.sendMessage(String msgid Object o) //根据MSGid发送消息
		
//		Message message = DataPool.getReceivePacketValue();
//		int s=0;
	}

}
