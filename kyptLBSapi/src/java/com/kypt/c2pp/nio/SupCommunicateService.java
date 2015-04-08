package com.kypt.c2pp.nio;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.kypt.c2pp.back.SupBack;
import com.kypt.c2pp.back.SupBackMap;
import com.kypt.configuration.C2ppCfg;
import com.kypt.configuration.C2ppTerminalParamCfg;
import com.kypt.list.DoubleCircularList;

public final class SupCommunicateService {

	private Logger log = LoggerFactory.getLogger(SupCommunicateService.class);

	private static final String NAME = "<SupCommunicateService>";

	private DoubleCircularList list = new DoubleCircularList();

	private String address;

	public void init() {

		address = C2ppCfg.props.getProperty("superviseAddress");

		log.info("服务器地址：" + address + "++"
				+ C2ppTerminalParamCfg.props.getProperty("heartbeatinterval"));
		try {
			start();
		} catch (Throwable t) {
			log.error(NAME + "CommunicateService Module start failed.", t);
		}
	}

	public void destroy() throws Exception {
		closeConnection();
		clearBackList();
	}

	public void start() throws Exception {
		loadBack();
		buildConnection();
	}

	private void loadBack() throws Exception {
		String[] backAddress = (address != null) ? address.split(";")
				: new String[] {};
		if (backAddress.length == 0) {
			throw new Exception("backAddress is invalid.");
		}
		// LinkedList<Back> backlist = new LinkedList<Back>();
		for (int i = 0; i < backAddress.length; i++) {
			String[] split = backAddress[i].split(":");
			if (split.length < 1) {
				log.error(NAME + "backAddress:" + backAddress[i]
						+ " is invalid.");
				continue;
			}
			String ip = split[SupBack.IP_INDEX];
			int port = Integer.parseInt(split[SupBack.PORTINDEX]);
			SupBack back = new SupBack(ip, port);
			SupBackMap.getInstance().put(back.getIp(), back);
			// backlist.add(back);
			SupBackMap.getInstance().setList(back);
		}
		// BackMap.getInstance().setBacklist(backlist);

	}

	private void buildConnection() throws Exception {
		for (String backId : SupBackMap.getInstance().keySet()) {
			final SupBack back = SupBackMap.getInstance().get(backId);

			final SupCommService supCommService = new SupCommService(back
					.getIp(), back.getPort());

			back.setSupcommService(supCommService);
			// System.out.println("开始连接");
			if (supCommService.connect()) {
				list.add(back);
				// Constant.array.add(back.getAddress());
			} else {
				new Thread() {
					@Override
					public void run() {
						try {
							supCommService.reconnect();
							list.add(back);
							// Constant.array.add(back.getAddress());
						} catch (Throwable e) {
							log.error(NAME + "reconnect " + back.getAddress()
									+ "failed.");
						}
					}
				}.start();
			}
		}
	}

	private void closeConnection() {
		for (String backId : SupBackMap.getInstance().keySet()) {
			SupBack back = SupBackMap.getInstance().get(backId);
			try {
				back.getSupcommService().close();
			} catch (Exception e) {
				log.error(NAME + "close " + back.getAddress()
						+ " connection failed.");
			}
		}
	}

	private void clearBackList() {
		SupBackMap.getInstance().clear();
	}
}
