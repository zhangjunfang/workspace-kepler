package com.ctfo.dataanalysisservice;

import com.ctfo.dataanalysisservice.beans.VehicleMessage;
import com.ctfo.dataanalysisservice.service.BussinesDistributeThread;

/**
 * 业务分发类缓存池
 * 
 * @author yangjian
 * 
 */
public class BussinesDistributeThreadPool {

	private static BussinesDistributeThread[] bussinesDistributeThreadArray;

	public static BussinesDistributeThread[] getBussinesDistributeThreadArray() {
		return bussinesDistributeThreadArray;
	}

	public static void setBussinesDistributeThreadArray(
			BussinesDistributeThread[] bussinesDistributeThreadArray) {
		BussinesDistributeThreadPool.bussinesDistributeThreadArray = bussinesDistributeThreadArray;
	}

	// 测试计数器（记录处理的轨迹数据）
	private static int count;

	public static int getCount() {
		return count;
	}

	public synchronized static void addCount() {
		count++;
	}

	/**
	 * 是否运行启动缓存池
	 */
	private static boolean inited = true;

	/**
	 * 初始化业务分发缓存池对象
	 */
	public static synchronized void init() {

		if (inited) {

			int queueCount;
			int m = 0;

			// 取模分组队列个数
			queueCount = DataAnalysisServiceMain.config
					.getIntValue("BussinesDistributeCount");

			// 业务分发处理线程数组
			bussinesDistributeThreadArray = new BussinesDistributeThread[queueCount];

			// 缓存业务分发类线程对象
			while (m < queueCount) {
				BussinesDistributeThread bussinessThread = new BussinesDistributeThread();
				bussinessThread.start();
				bussinesDistributeThreadArray[m] = bussinessThread;
				m++;
			}
			inited = false;

		}
	}

	/**
	 * 得到缓存池大小
	 * 
	 * @return
	 */
	public static int getPoolSize() {

		if (bussinesDistributeThreadArray != null) {

			return bussinesDistributeThreadArray.length;
		} else {
			return 0;
		}
	}

	/**
	 * 获得业务分发类缓存池中的线程对象并将数据保存在线程对象的队列中
	 * 
	 * @param m
	 *            取模得到的值
	 * @return 业务分发类对象
	 */
	public static void addPacket(int m, VehicleMessage vehicleMessage) {

		// if(bussinesDistributeThreadArray!=null &&
		// bussinesDistributeThreadArray[m]!=null && vehicleMessage!=null){

		try {
			bussinesDistributeThreadArray[m].addPacket(vehicleMessage);
		} catch (InterruptedException e) {
			e.printStackTrace();
			Thread.currentThread().interrupt();
		}
		// }
	}

	/**
	 * 获得业务分发类缓存池中的线程对象中的队列数据
	 * 
	 * @param m
	 *            取模得到的值
	 * @return 业务分发类对象
	 */
	public static VehicleMessage getPacket(int m) {

		VehicleMessage message = null;

		// if(bussinesDistributeThreadArray!=null &&
		// bussinesDistributeThreadArray[m]!=null){

		try {
			message = bussinesDistributeThreadArray[m].getPacket();
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			Thread.currentThread().interrupt();
		}
		// }
		return message;
	}
}
