package com.ctfo.analy.addin;

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

	// 插件管理类 Description: 管理轨迹分析线程，进行深入的轨迹分析
	private static AddInManagerThread[] addinManagerThread = null;

	public static AddInManagerThread[] getAddinManagerThread() {
		return addinManagerThread;
	}

	public static String getThreadInfo() {
		String info = "";
		for (int i = 0; i < addinManagerThread.length; i++) {
			info = info + "插件线程" + i + "："+ addinManagerThread[i].getPacketsSize() + ",";
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

		@SuppressWarnings("unchecked")
		Vector<String> claddin = config.getSubConfigNames("AnalyserAddIns");// 加载类
		int addinThreadcount = config.getIntValue("AnalyserAddIns");// 插件分配个数
		
		
			int ptype;//业务类型
			String calssname;
			String nodeName;
			// 配置文件对应的加载类
			Hashtable<String, Vector<PacketAnalyser>> taAnalyser = new Hashtable<String, Vector<PacketAnalyser>>();
			// 加载配置文件
			for (int i = 0; i < claddin.size(); i++) {
				ptype = Integer.parseInt(claddin.elementAt(i));
				Vector<PacketAnalyser> vcl = new Vector<PacketAnalyser>();
				System.out.println("【加载类】----"+ptype);
				nodeName = "AnalyserAddIns|" + ptype;
				calssname = config.getStringValue(nodeName + "|class");
				int count = config.getIntValue(nodeName + "|count");
				if (count <= 0)
					continue;
	
				for (int j = 0; j < count; j++) {
					@SuppressWarnings("unchecked")
					Class<PacketAnalyser> clAnalyser = (Class<PacketAnalyser>) Class.forName(calssname);
					PacketAnalyser packetAnalyser = (PacketAnalyser) clAnalyser.newInstance();
					packetAnalyser.initAnalyser(j, config, nodeName);//初始化并启动分析线程
					vcl.add(packetAnalyser);
				}
	
				taAnalyser.put(String.valueOf(ptype), vcl);
			}// End for
			
			// 启动多插件分配线程
			addinManagerThread = new AddInManagerThread[addinThreadcount];
			for (int x = 0; x < addinThreadcount; x++) {
			//多个插件接收到的消息，共用同一个业务接口线程，将消息汇总后分发。
			addinManagerThread[x] = new AddInManagerThread(x, taAnalyser);
			addinManagerThread[x].start();
		}
	}

}
