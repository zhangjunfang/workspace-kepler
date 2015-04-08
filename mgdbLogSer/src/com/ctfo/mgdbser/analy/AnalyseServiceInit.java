package com.ctfo.mgdbser.analy;

import org.apache.log4j.Logger;
import org.dom4j.DocumentException;

import com.ctfo.mgdb.util.XmlConfUtil;


public class AnalyseServiceInit {
	
	XmlConfUtil config;
	private static AnalyseServiceThread[] analyseServiceThread = null;
	private static final Logger logger = Logger.getLogger(AnalyseServiceInit.class);
	public AnalyseServiceThread[] getAnalyseServiceThread() {
		return analyseServiceThread;
	}

	public AnalyseServiceInit(XmlConfUtil config) {
		this.config = config;
	}

	public static String getThreadInfo() {
		String info = "";
		for (int i = 0; i < analyseServiceThread.length; i++) {
			info = info + "主线程" + i + "："
					+ analyseServiceThread[i].getPacketsSize() + ",";
		}
		return "[" + info + "]";
	}

	public void init() throws ClassNotFoundException, InstantiationException,
			IllegalAccessException, NumberFormatException, DocumentException {
		int count = Integer.parseInt(this.config.getStringValue("threadServiceManage|analyseServiceCount"));
		logger.info("启动分析线程数量："+count);
		// 启动主线程
		analyseServiceThread = new AnalyseServiceThread[count];
		for (int i = 0; i < count; i++) {
			IAnalyseService iAnalyseService= new CommonAnalyseService(); 
			analyseServiceThread[i] = new AnalyseServiceThread(i,
					iAnalyseService,this.config);
			analyseServiceThread[i].start();
		}
	}

}
