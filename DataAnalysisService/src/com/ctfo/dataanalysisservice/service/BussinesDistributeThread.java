package com.ctfo.dataanalysisservice.service;

import java.util.UUID;
import java.util.Vector;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.log4j.Logger;

import com.ctfo.dataanalysisservice.DataAnalysisServiceMain;
import com.ctfo.dataanalysisservice.addin.IaddIn;
import com.ctfo.dataanalysisservice.beans.VehicleMessage;

/**
 * 业务分发线程类
 * 
 * @author yangjian
 * 
 */
public class BussinesDistributeThread extends Thread {

	private static final Logger logger = Logger
			.getLogger(BussinesDistributeThread.class);

	public ArrayBlockingQueue<VehicleMessage> vPacket = new ArrayBlockingQueue<VehicleMessage>(
			100000);

	// private Hashtable<String, IaddIn[]> addInMap = new Hashtable<String,
	// IaddIn[]>();
	// private Hashtable<String, IaddIn> addInMap = new Hashtable<String,
	// IaddIn>();
	private IaddIn[] addInArray = null;

	private String name;

	public BussinesDistributeThread() {
		name = UUID.randomUUID().toString();
		// 记录线程数
		DataAnalysisServiceMain.threadCount++;
	}

	public void addPacket(VehicleMessage packet) throws InterruptedException {

		// vPacket.offer(packet);
		vPacket.put(packet);
		//

	}

	public VehicleMessage getPacket() throws InterruptedException {

		// return vPacket.poll();
		return vPacket.take();
	}

	public int getPacketsSize() {

		return vPacket.size();

	}

	/**
	 * 线程执行
	 */
	public void run() {

		// logger.info("BussinesDistributeThread");
		VehicleMessage vehicleMessage = null;

		// 业务插件
		IaddIn addIn;
		// IaddIn addInObj;
		Vector<String> addInNames = null;
		String addInName;
		Class<?> clanalyse = null;
		String analyseConfig = null;
		String[] analyseConfigArray = null;
		String analyseClass = null;
		Integer analyseClassCount = 0;
		int m = 0;
		// 每类业务插件的集合
		IaddIn[] analyseClassArray = null;
		// Message message=null;
		// DBAdapter dba=null;
		int thnum;
		try {

			// 读取配置 插件类 得到软报警分析处理类
			addInNames = DataAnalysisServiceMain.config
					.getSubConfigNames("AddInClass");

			// logger.info("累计轨迹上报数据 "+BussinesDistributeThreadPool.getCount());
			logger.info("addInNames===============" + addInNames.size());

			if (addInNames != null) {

				// 插件类个数
				addInArray = new IaddIn[addInNames.size()];
				logger.info("@@@@@@@@@@@@@@@@@@length======="
						+ addInArray.length);
				for (int i = 0; i < addInNames.size(); i++) {

					addInName = (String) addInNames.elementAt(i);

					// analyseConfig =
					// DataAnalysisServiceMain.config.getStringValue("AddInClass|"
					// + addInName);
					analyseClass = DataAnalysisServiceMain.config
							.getStringValue("AddInClass|" + addInName);
					// logger.info("analyseConfig==============="
					// +analyseConfig);
					// if(analyseConfig!=null){
					if (analyseClass != null) {
						clanalyse = Class.forName(analyseClass);
						// 得到不同的软报警处理业务类
						addIn = (IaddIn) clanalyse.newInstance();
						addIn.start();
						addInArray[i] = addIn;
					}

				}

				System.out.println("------------");
				while (true) {
					// System.out.println(System.currentTimeMillis() + "name=" +
					// name+ " BussinesDistributeThread" + getPacketsSize());
					// 取得数据
					try {
						vehicleMessage = getPacket();
						//logger.info("待处理数据！！！！！！" + this.getPacketsSize());
					} catch (InterruptedException e) {
						logger.error(e);
						Thread.currentThread().interrupt();
					}
					if (vehicleMessage != null
							&& vehicleMessage.getVid() != null) {
						// 与缓存比对 判断属于的软报警类型（类型使用class名区分）
						String alarmType = vehicleMessage.getAlarmType();

						if (alarmType != null) {
							String[] typeArray = alarmType.split(",");

							if (typeArray != null) {
								for (String type : typeArray) {
									if (addInArray != null
											&& addInArray.length > 0) {
										try {
											addInArray[Integer.valueOf(type)]
													.addPacket(vehicleMessage);
//											logger.info("#################type"
//													+ type);
//											logger.info("PacketsSize=========="
//													+ addInArray[Integer
//															.valueOf(type)]
//															.getPacketsSize());
										} catch (InterruptedException e) {
											logger.error(e);
											Thread.currentThread().interrupt();
										}
									}
								}
							}
						}

					} else {
						sleep(1);
					}
				}

			}

		} catch (Exception e) {
			e.printStackTrace();
			logger.error(e);
		}

	}

}
