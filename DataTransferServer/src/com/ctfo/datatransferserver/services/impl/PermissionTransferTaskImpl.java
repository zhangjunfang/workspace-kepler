package com.ctfo.datatransferserver.services.impl;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.datatransferserver.beans.OrganizationBean;
import com.ctfo.datatransferserver.dao.OrganizationDao;
import com.ctfo.datatransferserver.services.TransferTask;
import com.ctfo.datatransferserver.util.SocketUtil;
import com.lingtu.xmlconf.XmlConf;

/**
 * 传输权限数据
 * 
 * @author yangyi
 * 
 */
public class PermissionTransferTaskImpl implements TransferTask {

	private static final Logger logger = LoggerFactory.getLogger(PermissionTransferTaskImpl.class);
	OrganizationDao organizationDao;
	String IP;
	int port;
	public PermissionTransferTaskImpl(){
		Thread.currentThread().setName("PermissionTransferTaskImpl"); 
	}
	@Override
	public void run() {

		logger.info("任务PermissionTransferTaskImpl执行");
		List<OrganizationBean> list = organizationDao.queryAllOrganization();
		byte[] data = dealOrganizationData(list);

		if (data != null) {
			// byte[] zipdata=ZipUtil.GZIP(data);
			// int length=zipdata.length;
			// byte[] senddata = new byte[length+ 4];
			// int pos = ByteConvert.int2byteArray(length, senddata, 0);
			// System.arraycopy(zipdata, 0, senddata, pos, length);

			boolean state = SocketUtil.sendTCPData(IP, port, data);
			String statecontent = "失败";
			if (state) {
				statecontent = "成功";
			}
			logger.info("发送权限数据[" + statecontent + "],长度[" + list.size() + "],包长度[" + data.length + "]");
			// ",包内容["+ByteConvert.bytesToHexString(data)+"]");
		}
	}

	/**
	 * 处理组织结构数据
	 * 
	 * @param list
	 * @return
	 */
	public byte[] dealOrganizationData(List<OrganizationBean> list) {
		String data = "";

		for (OrganizationBean organizationBean : list) {
			int level = organizationBean.getLevel();
			if (level == 1) {
				data += organizationBean.getEntid() + "\r\n";
			} else {
				for (int i = 1; i < level; i++) {
					data += "\t";
				}
				data += "|--" + organizationBean.getEntid() + "\r\n";
			}
		}
		return data.getBytes();

	}

	/**
	 * 初始化
	 */
	@Override
	public void initTask(XmlConf conf, String nodename) throws ClassNotFoundException, InstantiationException, IllegalAccessException {
		String strDB = conf.getStringValue("database|OrganizationDaoImpl");
		Class<?> cldb = Class.forName(strDB);
		organizationDao = (OrganizationDao) cldb.newInstance();
		organizationDao.initDBAdapter(conf);
		IP = conf.getStringValue(nodename + "|IP");
		port = conf.getIntValue(nodename + "|Port");
	}

}
