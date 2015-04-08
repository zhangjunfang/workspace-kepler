package com.ctfo.datatransferserver.services;

import com.lingtu.xmlconf.XmlConf;

/**
 * 数据传输任务通用接口
 * 
 * @author yangyi
 * 
 */
public interface TransferTask extends Runnable {

	public void initTask(XmlConf config, String nodename)
			throws ClassNotFoundException, InstantiationException,
			IllegalAccessException;

}
