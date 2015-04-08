/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： CommandService		</li><br>
 * <li>文件名称：com.ctfo.commandservice.model Test.java	</li><br>
 * <li>时        间：2013-12-10  下午6:00:57	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.commandservice.model;

import org.apache.commons.lang3.StringUtils;

/*****************************************
 * <li>描        述：TODO		
 * 
 *****************************************/
public class Test {

	/*****************************************
	 * <li>描        述：TODO 		</li><br>
	 * <li>时        间：2013-12-10  下午6:00:57	</li><br>
	 * <li>参数： @param args			</li><br>
	 * 
	 *****************************************/
	public static void main(String[] args) {
//		String[] str = "12,".split(",",2);
		String[] str = StringUtils.splitPreserveAllTokens("12:", ":",2);
		System.out.println((str[1].equals("")) +" ==" +  (str[1] == null) + " == " + str.length);

	}

}
