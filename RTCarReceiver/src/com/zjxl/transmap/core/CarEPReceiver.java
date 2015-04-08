package com.zjxl.transmap.core;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

import org.apache.log4j.Logger;

import com.zjxl.transmap.rt.dcar.CarEP;
import com.zjxl.transmap.rt.dcar.EPTree;
import com.zjxl.transmap.rt.dcar.IndexManager;
import com.zjxl.transmap.rt.server.IReceiver;

/**
 * 权限数据接收器
 * 
 * @author dux(duxionggis@126.com)/
 */
public class CarEPReceiver implements IReceiver {
	private static Logger logger = Logger.getLogger(CarEPReceiver.class);
	private int port;

	public static void main(String[] args) throws IOException {
		// InputStream is = Thread.currentThread().getContextClassLoader().getResourceAsStream("eptest.txt");
		// new CarEPReceiver(4001).receive(is);
		// is.close();
		new CarEPReceiver(4001).run();
	}

	public CarEPReceiver() {
	}

	public CarEPReceiver(int port) {
		this.port = port;
	}

	public void setProperty(Properties ppt) {
		if (ppt != null) {
			String epPort = ppt.getProperty("epPort");
			if (epPort != null && !epPort.isEmpty()) {
				try {
					this.port = Integer.valueOf(epPort.trim());
				} catch (Exception e) {
					this.port = 4001;
				}
			}
		}
	}

	public void run() {
		try {
			this.run(port);
		} catch (IOException e) {
			logger.error("ep run error: ", e);
		}
	}

	public void run(int port) throws IOException {
		ServerSocket server = new ServerSocket(port);
		while (true) {
			Socket client = server.accept();
			try {
				receive(client.getInputStream());
			} catch (Exception e) {
				logger.error("ep receive error: ", e);
			}
			client.close();
		}
	}

	private void receive(InputStream is) throws IOException {
		long t1 = System.currentTimeMillis();
		EPTree tree = new EPTree();
		BufferedReader br = new BufferedReader(new InputStreamReader(is));
		String line = null;
		int level = 0;
		String epid = "-1";
		List<String> parentList = null;

		//构建EPTree
		while ((line = br.readLine()) != null) {
			if ("".equalsIgnoreCase(line))
				continue;
			if (line.indexOf("|") > -1) {
				level = 1 + line.indexOf("|");
				line = line.trim();
				epid = line.substring(line.indexOf("|--") + 3);
			} else {
				level = 1;
				epid = line.trim();
			}
			if (level == 1) {
				if (parentList != null)
					parentList.clear();
				parentList = null;
				parentList = new ArrayList<String>(25);
				parentList.add(0, "-1");
			}
			parentList.add(level, epid);
			CarEP ep = new CarEP(epid, parentList.get(level - 1));
			tree.insert(ep);
		}
		br.close();

		// 更新EPTree
		IndexManager.getInstance().updateEPTree(tree);
		logger.debug("ep tree:\r\n" + tree);
		logger.info("ep receive times: " + (System.currentTimeMillis() - t1) + " ms");
		logger.info("free memory：" + java.lang.Runtime.getRuntime().freeMemory() / 1024 / 1024 + "M");
	}

}
