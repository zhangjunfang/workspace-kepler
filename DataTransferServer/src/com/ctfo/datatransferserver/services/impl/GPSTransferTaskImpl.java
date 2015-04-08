package com.ctfo.datatransferserver.services.impl;

import java.util.Map;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.datatransferserver.DataPool;
import com.ctfo.datatransferserver.beans.VehiclePolymerizeBean;
import com.ctfo.datatransferserver.dao.ServiceUnitDao;
import com.ctfo.datatransferserver.services.TransferTask;
import com.ctfo.datatransferserver.util.SocketUtil;
import com.ctfo.datatransferserver.util.ZipUtil;
import com.lingtu.xmlconf.XmlConf;

/**
 * F 传输GPS数据
 * 
 * @author yangyi
 * 
 */
public class GPSTransferTaskImpl implements TransferTask {
//	private String charSetName = "UTF-8";
	private static final Logger logger = LoggerFactory.getLogger(GPSTransferTaskImpl.class);
	ServiceUnitDao serviceUnitDao;
	String IP;
	int port;
	int iszip;
	
	public GPSTransferTaskImpl(){
		Thread.currentThread().setName("GPSTransferTaskImpl"); 
	}
	
	@Override
	public void run() {
		
		logger.info("任务GPSTransferTaskImpl执行");
		Map<String, VehiclePolymerizeBean> map = DataPool.getTempVehicleMap();
		int size = map.size();
		logger.info("实时接收GPS总数[" + size + "]");
		if (size == 0) {
			return;
		}
//		byte[] data = dealgpsdata(map);

		byte[] data = processGpsData(map);
		
		if (data != null) {
			byte[] zipdata;
			if (iszip == 1) {
				long t1 = System.currentTimeMillis();
				zipdata = ZipUtil.GZIP(data);
				logger.info("GZIP 时间ms[" + (System.currentTimeMillis() - t1) + "],包长度[" + data.length + "],压缩包长度[" + zipdata.length + "]");
			} else {
				zipdata = data;
			}

			boolean state = SocketUtil.sendTCPData(IP, port, zipdata);
			String statecontent = "失败";
			if (state) {
				statecontent = "成功";
			}
			DataPool.clearTempOfflineVehicleMap();

			logger.info("发送GPS数据[" + statecontent + "],iszip[" + iszip + "],长度[" + map.size() + "],包长度[" + data.length + "],压缩包长度[" + zipdata.length + "]");
			// ,包内容["+ByteConvert.bytesToHexString(data)+"]");
		}
	}

	/**
	 * 处理GPS数据
	 * @param map
	 * @return
	 */
	private byte[] processGpsData(Map<String, VehiclePolymerizeBean> map) {
		StringBuffer sb = new StringBuffer();
		for(Map.Entry<String, VehiclePolymerizeBean> m : map.entrySet()){
			sb.append(m.getValue().getGpsDataTransferInfo()).append(";");
		}
		return sb.substring(0, sb.length() - 1).getBytes();
	}

	/**
	 * 处理GPS数据
	 * 
	 * @param map
	 * @return
	 */
	public byte[] testdealgpsdata(Map<Long, VehiclePolymerizeBean> map) {
		try {
			// int size = map.size();
			byte[] data = new byte[46 + 4];
			for (int j = 0; j < data.length; j++) {
				data[j] = '0';
			}
			data[3] = '1';// 长度4
			data[7] = '1';// suid8

			String s = "京A00001";// 车牌号码22
			byte[] sk = s.getBytes();
			for (int i = 8, j = 0; j < sk.length; i++, j++) {
				data[i] = sk[j];
			}

			data[23] = '1';// 类型25

			data[25] = '1';// 权限31
			data[26] = '1';

			data[43] = '2';// 颜色
			data[44] = '1';// 是否本地

			data[48] = '1';// 报警
			data[49] = '1';// 在线

			return data;
		} catch (Exception e) {
			e.printStackTrace();
			return null;
		}
	}

	/**
	 * 初始化
	 */
	@Override
	public void initTask(XmlConf conf, String nodename) throws ClassNotFoundException, InstantiationException, IllegalAccessException {
		String strDB = conf.getStringValue("database|ServiceUnitDaoImpl");
		Class<?> cldb = Class.forName(strDB);
		serviceUnitDao = (ServiceUnitDao) cldb.newInstance();
		serviceUnitDao.initDBAdapter(conf);
		serviceUnitDao.queryAllVehicle();

		InitTransferImpl initTransferImpl = new InitTransferImpl();
		initTransferImpl.initTask(conf, nodename);

		IP = conf.getStringValue(nodename + "|IP");
		port = conf.getIntValue(nodename + "|Port");
		iszip = conf.getIntValue("IsZIP");
	}


}
