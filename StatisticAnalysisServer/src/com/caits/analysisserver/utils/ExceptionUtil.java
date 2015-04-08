package com.caits.analysisserver.utils;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

/**
 * 异常信息辅助类，打印异常堆栈信息
 * 
 * @author yujch
 *
 */
public class ExceptionUtil {
	
	/**
	 * 打印异常堆栈信息
	 * @param e
	 * @param length <=0 表示打印所有行
	 * @return
	 */
	public static String getErrorStack(Exception e, int length) {
		  String error = null;
		  if (e != null) {
		   try {
		    ByteArrayOutputStream baos = new ByteArrayOutputStream();
		    PrintStream ps = new PrintStream(baos);
		    e.printStackTrace(ps);
		    error = baos.toString();
		    if (length > 0) {
		     if (length > error.length()) {
		      length = error.length();
		     }
		     error = error.substring(0, length);
		    }
		    baos.close();
		    ps.close();
		   } catch (Exception e1) {
		    error = e.toString();
		   }
		  }
		  /*
		   * try{ String str=new String(error.getBytes("ISO-8859-1"),"GBK");
		   * return str; }catch(Exception e1) { e1.printStackTrace(); }
		   */
		  return error;
		}

}
