package com.ctfo.dataanalysisservice.service;

import java.util.UUID;

import org.apache.log4j.Logger;

import com.ctfo.dataanalysisservice.BussinesDistributeThreadPool;
import com.ctfo.dataanalysisservice.DataAnalysisServiceMain;
import com.ctfo.dataanalysisservice.beans.Message;
import com.ctfo.dataanalysisservice.beans.VehicleMessage;
import com.ctfo.dataanalysisservice.io.DataPool;

/**
 * 报文数据初始分发分析服务
 * 
 * @author yangjian
 * 
 */
public class DataDistributeThread extends Thread {

	private static final Logger logger = Logger
			.getLogger(DataDistributeThread.class);

	private String name;

	public DataDistributeThread() {

		name = UUID.randomUUID().toString();
		// 记录总线程数
		DataAnalysisServiceMain.threadCount++;
	}

	// 运行标志
	private boolean isRunning = true;

	/**
	 * 线程执行
	 */
	public void run() {

		logger.info("DataDistributeThread--name:" + name);

		// 报文分析处理接口
		IAnalyseService iAnalyseService = null;
		// 报文
		VehicleMessage vehicleMessage = null;
		String analyseClass = null;
		Class<?> clanalyse = null;
		Message message = null;
		int queueCount;
		int thnum;

		try {
			// 读取配置 得到报文分析处理类
			analyseClass = DataAnalysisServiceMain.config
					.getStringValue("AnalyseClass");
			// 取模分组队列个数
			queueCount = DataAnalysisServiceMain.config
					.getIntValue("BussinesDistributeCount");
			// 获取分发业务类
			clanalyse = Class.forName(analyseClass);
			iAnalyseService = (IAnalyseService) clanalyse.newInstance();

			while (isRunning) {
				// System.out.println(DataAnalysisServiceMain.threadCount);

				// System.out.println(System.currentTimeMillis()+"name="+name+"{ DataDistributeThread"+DataPool.getReceivePacketSize());
				// 取得原始报文命令
				message = DataPool.getReceivePacketValue();
				// Thread.currentThread().interrupt();
				// logger.info("message"+message);
				if (message != null)
					logger.info("报文数据初始分发分析服务" + message.getCommand());
				if (message != null) {
					// 解析报文数据
					vehicleMessage = iAnalyseService.dealPacket(message);

					if (vehicleMessage != null) {

						if (vehicleMessage.getVid() != null) {
							thnum = (int) (vehicleMessage.getVid() % queueCount);
//							logger.info("thnum=========XXXXXXXXXXX==========>"
//									+ thnum);
							// System.out.println("thnum"+thnum);
							// System.out.println("BussinesDistributeThreadPool="+BussinesDistributeThreadPool.getPoolSize());
							// 根据取模将数据分配至不同队列
							DataPool.j++;

							BussinesDistributeThreadPool.addPacket(thnum,
									vehicleMessage);

						}

					}
				} 

			}

		} catch (Exception e) {
			e.printStackTrace();
			logger.error(e.fillInStackTrace());
		}

	}

}
