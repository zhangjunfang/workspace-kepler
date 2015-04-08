package com.ctfo.datatransferserver.services.impl;

import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.datatransferserver.DataPool;
import com.ctfo.datatransferserver.beans.ServiceUnitBean;
import com.ctfo.datatransferserver.services.TransferTask;
import com.ctfo.datatransferserver.util.SocketUtil;
import com.ctfo.datatransferserver.util.ZipUtil;
import com.lingtu.xmlconf.XmlConf;

/**
 * 初始化处理GPS数据
 * 
 * @author ruky
 * 
 */
public class InitTransferImpl implements TransferTask {
	// private String charSetName = "UTF-8";
	private static final Logger logger = LoggerFactory.getLogger(InitTransferImpl.class);

	String IP;
	int port;
	int iszip;

	@Override
	public void run() {
		logger.info("初始化InitTransferImpl执行");
		Map<String, ServiceUnitBean> map = DataPool.getVehicleMap();
		int size = map.size();
		logger.info("初始化GPS总数[" + size + "]");
		if (size == 0) {
			return;
		}
		// byte[] data = dealgpsdata(map);
		byte[] data = processGpsData(map);

		if (data != null) {
			byte[] zipdata;
			if (iszip == 1) {
				long t1 = System.currentTimeMillis();
				zipdata = ZipUtil.GZIP(data);
				logger.info("初始化GZIP 时间ms[" + (System.currentTimeMillis() - t1) + "],包长度[" + data.length + "],压缩包长度[" + zipdata.length + "]");
			} else {
				zipdata = data;
			}

			boolean state = SocketUtil.sendTCPData(IP, port, zipdata);
			String statecontent = "失败";
			if (state) {
				statecontent = "成功";
			}

			logger.info("初始化发送GPS数据[" + statecontent + "],iszip[" + iszip + "],长度[" + map.size() + "],包长度[" + data.length + "],压缩包长度[" + zipdata.length + "]");

		}
	}

	/**
	 * 处理GPS数据
	 * 
	 * @param map
	 * @return
	 */
	private byte[] processGpsData(Map<String, ServiceUnitBean> map) {
		StringBuffer sb = new StringBuffer();
		for (Map.Entry<String, ServiceUnitBean> m : map.entrySet()) {
			sb.append(m.getValue().getGpsDataTransferInfo()).append(";");
		}
		return sb.substring(0, sb.length() - 1).getBytes();
	}

	/**
	 * 初始化
	 */
	@Override
	public void initTask(XmlConf conf, String nodename) throws ClassNotFoundException, InstantiationException, IllegalAccessException {
		IP = conf.getStringValue(nodename + "|IP");
		port = conf.getIntValue(nodename + "|Port");
		iszip = conf.getIntValue("IsZIP");
		run();
	}

}
