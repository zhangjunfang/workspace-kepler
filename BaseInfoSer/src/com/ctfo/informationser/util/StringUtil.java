/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.util;

import java.io.UnsupportedEncodingException;
import java.net.*;
import java.util.UUID;
/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： BsApp
 * <br>
 * 功能：
 * <br>
 * 描述：
 * <br>
 * 授权 : (C) Copyright (c) 2011
 * <br>
 * 公司 : 北京中交兴路信息科技有限公司
 * <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史
 * <br>
 * <table width="432" border="1">
 * <tr><td>版本</td><td>时间</td><td>作者</td><td>改变</td></tr>
 * <tr><td>1.0</td><td>2011-11-9</td><td>yangjian</td><td>创建</td></tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font>
 * <br>
 * 
 * @version 1.0
 * 
 * @author yangjian
 * @since JDK1.6
 */
public class StringUtil {

	/**
	 * 编码格式 UTF-8
	 */
	public static String ENC_UTF8="utf-8";
	
	/**
	 * 编码格式 GBK
	 */
	public static String ENC_GBK="gbk";	
	
	/**
	 * 中文转码为url编码格式  默认编码格式为GBK
	 * @param gbkStr 转换的字符串
	 * @return
	 */
	public static String gbk2UrlEncode(String gbkStr)throws UnsupportedEncodingException{
	
		return str2UrlEncode(gbkStr,ENC_GBK);
	}
	
	
	/**
	 * 中文转码为url编码格式  
	 * @param gbkStr 转换的字符串
	 * @param enc 编码格式
	 * @return 
	 */
	public static String str2UrlEncode(String str,String enc)throws UnsupportedEncodingException{
	
		if(str!=null && !"".equals(str.trim())  && enc!=null && !"".equals(enc.trim())){
		return URLEncoder.encode(str,enc);	
		}else{
			return null;
		}
	}
	
	
	/**
	 * url编码格式转码为普通中文    默认编码格式为GBK
	 * @param urlStr 转换的字符串
	 * @param enc 编码格式
	 * @return
	 * @throws UnsupportedEncodingException 
	 */
	public static String urlEncode2Str(String urlStr,String enc) throws UnsupportedEncodingException{
		
		if(urlStr!=null && !"".equals(urlStr.trim())  && enc!=null && !"".equals(enc.trim())){
			return URLDecoder.decode(urlStr,enc);	
			}else{
				return null;
			}
	}
	
	
	/**
	 * url编码格式转码为普通中文    默认编码格式为GBK
	 * @param urlStr 转换的字符串
	 * @return
	 * @throws UnsupportedEncodingException 
	 */
	public static String urlEncode2Gbk(String urlStr) throws UnsupportedEncodingException{
		
		return urlEncode2Str(urlStr,ENC_GBK);
	}
	
	
	/**
	 * 得到uuid 随机数
	 * @return
	 */
	public static String getUUid(){
		return UUID.randomUUID().toString();
	}
	
	
	public static void main(String[] args) throws UnsupportedEncodingException{
		System.out.println(gbk2UrlEncode("中文"));
		System.out.println(urlEncode2Gbk(gbk2UrlEncode("中文")));
		System.out.println(urlEncode2Gbk("%D0%CB%C2%B7%B2%E2%CA%D4"));
		

		
	}

}
