package com.caits.analysisserver.utils;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;


/**
 * 发送http请求
 * 
 * @author yangjian
 * 
 */
public class HttplUtil {

	public static void main(String[] args) {
	
		 String s=doPost("http://engine.gis.cttic.cn/engine?st=LocalSearch&city=%E5%85%A8%E5%9B%BD&area=RECT(116.332761%2039.996743,116.332890%2039.996812)&ser=SE_LS&uid=cttic_beijing");
		 System.out.println(s);
	}
	
	@SuppressWarnings("finally")
	public static String doPost(String urlStr)  {
		StringBuffer st = new StringBuffer("");
		BufferedReader br = null;
		try {
			if (urlStr != null) {
				URL url = new URL(urlStr);// 生成url对象
				URLConnection urlConnection = url.openConnection();// 打开url连接
				br = new BufferedReader(new InputStreamReader(urlConnection.getInputStream(), "utf-8"));
				
			    int c;
			    while ((c = br.read()) != -1)
			    {
			    	st.append((char)c);
			    }// End while
			} 
		} catch (MalformedURLException e) {
			System.out.println("连接到URL抛出异常信息：" + e);
		} catch (Exception e) {
			System.out.println("连接到URL抛出异常信息：" + e);
		}finally{
			if(null != br){
				try {
					br.close();
				} catch (IOException e) {
					System.out.println("Closing bufferedReader to failed：" + e);
				}
			}
			return st.toString();
		}
          
		
	}
}
