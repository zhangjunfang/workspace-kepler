package com.ctfo.savecenter.addin;

import java.util.Hashtable;
import java.util.Vector;

import com.lingtu.xmlconf.XmlConf;

/**
 * 初始化插件类
 * 
 * @author yangyi
 * 
 */
public class AddInManagerInit {

	private static AddInManagerThread[] addinManagerThread = null;

	public static AddInManagerThread[] getAddinManagerThread() {
		return addinManagerThread;
	}

	public static String getThreadInfo() {
		String info = "";
		for (int i = 0; i < addinManagerThread.length; i++) {
			info = info + "插件线程" + i + "："
					+ addinManagerThread[i].getPacketsSize() + ",";
		}
		return "[" + info + "]";
	}

	/**
	 * 初始化插件
	 * 
	 * @param config
	 * @throws Exception
	 */
	public void init(XmlConf config) throws Exception {
		// 配置文件对应的加载类
		Hashtable<String, Vector<Vector<PacketAnalyser>>> taAnalyser = new Hashtable<String, Vector<Vector<PacketAnalyser>>>();

		@SuppressWarnings("unchecked")
		Vector<String> claddin = config.getSubConfigNames("AnalyserAddIns");// 加载类
		int addinThreadcount = config.getIntValue("configServer|conNums");// 插件分配个数

		String ptype;
		String calssname;
		String nodeName;

		int nC;

		// 加载配置文件
		for (int i = 0; i < claddin.size(); i++) {
			ptype = (String) claddin.elementAt(i);
			Vector<Vector<PacketAnalyser>> vcl = new Vector<Vector<PacketAnalyser>>();
			nC = 1;
			while (true) {
				nodeName = "AnalyserAddIns|" + ptype + "|class" + nC;
				calssname = config.getStringValue(nodeName);
				nC++;
				if (calssname == null)
					break;

				int count = config.getIntValue(nodeName + "|count");
				if (config.getIntValue(nodeName + "|count") <= 0)
					continue;
				Vector<PacketAnalyser> analyers = new Vector<PacketAnalyser>();

				for (int j = 0; j < count; j++) {

					@SuppressWarnings("unchecked")
					Class<PacketAnalyser> clAnalyser = (Class<PacketAnalyser>) Class
							.forName(calssname);

					PacketAnalyser packetAnalyser = (PacketAnalyser) clAnalyser
							.newInstance();
					packetAnalyser.initAnalyser(j, config, nodeName);
					analyers.add(packetAnalyser);
				}
				vcl.add(analyers);

			}// End while
			taAnalyser.put(ptype, vcl);
		}// End for

		// 启动多插件分配线程
		addinManagerThread = new AddInManagerThread[addinThreadcount];
		for (int i = 0; i < addinThreadcount; i++) {
			addinManagerThread[i] = new AddInManagerThread(i, taAnalyser);
			addinManagerThread[i].start();
		}
	}

}
