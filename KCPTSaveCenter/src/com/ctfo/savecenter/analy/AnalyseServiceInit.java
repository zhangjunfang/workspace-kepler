package com.ctfo.savecenter.analy;

import com.lingtu.xmlconf.XmlConf;

/**
 * 协议解析线程初始化
 * 
 * @author yangyi
 * 
 */
public class AnalyseServiceInit {
	XmlConf config;
	private static AnalyseServiceThread[] analyseServiceThread = null;

	public static AnalyseServiceThread[] getAnalyseServiceThread() {
		return analyseServiceThread;
	}

	public AnalyseServiceInit(XmlConf config) {
		this.config = config;
	}

	public static String getThreadInfo() {
		String info = "";
		for (int i = 0; i < analyseServiceThread.length; i++) {
			info = info + "主线程" + i + "：" + analyseServiceThread[i].getPacketsSize() + ",";
		}
		return "[" + info + "]";
	}

	public void init() throws ClassNotFoundException, InstantiationException, IllegalAccessException {

		int count = config.getIntValue("configServer|conNums");
		// 启动主线程
		analyseServiceThread = new AnalyseServiceThread[count];
		for (int i = 0; i < count; i++) {
			String analyseClass = config.getStringValue("AnalyseClass");
			Class<?> clanalyse = Class.forName(analyseClass);
			IAnalyseService iAnalyseService = (IAnalyseService) clanalyse.newInstance();
			analyseServiceThread[i] = new AnalyseServiceThread(i, iAnalyseService);
			analyseServiceThread[i].start();
		}
	}

}
