package com.ctfo.hessianproxy.util;

import java.io.IOException;
import java.io.OutputStream;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
/**
 * <p>Title: InstantMessSer</p>
 * <p>Description: 封装了Java的Exception，打印堆栈信息</p>
 * <p>Copyright: Copyright (c) 2004</p>
 * <p>Company:北京中交兴路信息科技有限公司</p>
 * @author wuqiangjun
 * @version 1.1  2012-3-29
 */
public class CtfoException {

	private final static Log log = LogFactory.getLog(CtfoException.class);
	/**
	 * serialVersionUID:long
	 */
	@SuppressWarnings("unused")
	private static final long serialVersionUID = 1L;

	public CtfoException() {

	}

	/**
	 * 打印堆栈信息
	 * 
	 * @param e
	 */
	public static void printException(Exception e) {
		StackTraceElement se[] = e.getStackTrace();
		StringBuffer sb = new StringBuffer();
		log.error(e.toString());
		for (StackTraceElement s : se) {
			sb.setLength(0);
			sb.append(s.getClassName()).append(".");
			sb.append(s.getMethodName()).append("(");
			sb.append(s.getFileName()).append(":");
			sb.append(s.getLineNumber()).append(")");
			log.error(sb.toString());
		}

	}

	@SuppressWarnings("null")
	public static void main(String[] args) {

		OutputStream os = null;
		try {
			StringBuffer sb = null;
			System.out.println(sb.toString());
		} catch (Exception e) {
			CtfoException.printException(e);
		} finally {
			if (os != null)
				try {
					os.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
		}
	}
}
