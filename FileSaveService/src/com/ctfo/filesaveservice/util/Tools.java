/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.util Tools.java	</li><br>
 * <li>时        间：2013-9-9  下午4:40:48	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.util;


/*****************************************
 * <li>描        述：工具		
 * 
 *****************************************/
public class Tools {
	/*****************************************
	 * <li>描        述：添加逗号 		</li><br>
	 * <li>  addComma("ABC") = ,ABC,
	 * <li>时        间：2013-7-10  下午1:54:41	</li><br>
	 * <li>参数： @param alarmCode
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public static String addComma(Object alarmCode) {
		return new StringBuilder().append(Constant.COMMA).append(alarmCode).append(Constant.COMMA).toString();
	}
}
