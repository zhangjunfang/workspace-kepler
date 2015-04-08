/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.parse CommandParseManage.java	</li><br>
 * <li>时        间：2013-9-9  下午2:13:05	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.parse;

/*****************************************
 * <li>描        述：指令管理		
 * 
 *****************************************/
public class CommandParseManage {
	
	private static CommandParseThread commandParseThread = null;
	
	public CommandParseManage(CommandParseThread commandParseThread){
		CommandParseManage.commandParseThread = commandParseThread;
	}
	
	public static CommandParseThread getCommandParseThread(){
		return commandParseThread;
	}

	public static void setCommandParseThread(CommandParseThread commandParseThread) {
		CommandParseManage.commandParseThread = commandParseThread;
	}
	
}
