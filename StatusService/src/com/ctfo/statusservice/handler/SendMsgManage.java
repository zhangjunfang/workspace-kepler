/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： StatusService		</li><br>
 * <li>文件名称：com.ctfo.statusservice.handler SendMsgManage.java	</li><br>
 * <li>时        间：2013-9-26  下午6:11:21	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.handler;


/*****************************************
 * <li>描        述：发送消息分配管理类		
 * 
 *****************************************/
public class SendMsgManage {
	
	public SendMsgManage(SendMsgThread sendMsgThread){
		SendMsgManage.sendMsgThread = sendMsgThread;
	}
	private static SendMsgThread sendMsgThread = null;

	public static SendMsgThread getSendMsgThread() {
		return sendMsgThread;
	}
	public static void setSendMsgThread(SendMsgThread sendMsgThread) {
		SendMsgManage.sendMsgThread = sendMsgThread;
	}
	
}
