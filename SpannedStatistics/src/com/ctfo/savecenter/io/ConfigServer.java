package com.ctfo.savecenter.io;

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

	@SuppressWarnings("unused")
	private XmlConf config;
	private String localIP;// 本地IP
	private short localPort;// 本地端口
	private int loginID;// 登陆id
	private int conNums;// 连接数
	private String handlerClass;// 处理类
	private String configIP;// 节点管理器IP
	private int configPort;// 节点管理器端口
	private int reconTimes; // 重连时间
	private int groupID = 1; // 节点管理组ID
	private ConfigServerClientHandler handler;
	private MinaClient minaClient;

	public ConfigServer() {

	}
 
	public boolean init(XmlConf config) throws InterruptedException {

		this.config = config;

		localIP = config.getStringValue("configServer|localIP");
		localPort = (short) config.getIntValue("configServer|localPort");
		loginID = config.getIntValue("configServer|loginID");
		conNums = config.getIntValue("configServer|conNums");
		handlerClass = config.getStringValue("configServer|handlerClass");
		configIP = config.getStringValue("configServer|configIP");
		configPort = config.getIntValue("configServer|configPort");
		reconTimes = config.getIntValue("configServer|reconTimes");
		groupID = config.getIntValue("configServer|groupID");
		handler = new ConfigServerClientHandler(localIP, localPort, loginID,
				conNums, handlerClass);
		handler.setCsTypeId((short)groupID);
		minaClient = new MinaClient(configIP, configPort, handler);

		minaClient.USE_CUSTOM_CODEC = false;
		//如果与节点管理器断开，设置重连时间
		minaClient.setReconTimes(reconTimes);
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
		// //  
		// e.printStackTrace();
		// }

		// MsgConnectUtil.sendMessage(String ip, short port, Object o)
		// //根据IP和port发送消息
		// MsgConnectUtil.sendMessage(String msgid Object o) //根据MSGid发送消息
		
//		Message message = DataPool.getReceivePacketValue();
//		int s=0;
	}

}
