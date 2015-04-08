package com.ctfo.mgdbser.save;


import org.apache.log4j.Logger;
import org.dom4j.DocumentException;

import com.ctfo.mgdb.util.XmlConfUtil;



public class SaveManagerInit {
	
	XmlConfUtil config;
	private static SaveManagerThread[] addinManagerThread = null;
	private static final Logger logger = Logger.getLogger(SaveManagerInit.class);

	public static SaveManagerThread[] getAddinManagerThread() {
		return addinManagerThread;
	}
	public SaveManagerInit(XmlConfUtil config) {
		this.config = config;

	}
	
	
	public void init() throws ClassNotFoundException, InstantiationException,
	IllegalAccessException, NumberFormatException, DocumentException {
		int count =  Integer.parseInt(this.config.getStringValue("threadServiceManage|saveServiceCount"));
		// 启动主线程
		logger.info("启动存储线程数量："+count);
		addinManagerThread = new SaveManagerThread[count];
		for (int i = 0; i < count; i++) {
			addinManagerThread[i] = new SaveManagerThread(i);
			addinManagerThread[i].start();
		}
	}
}
