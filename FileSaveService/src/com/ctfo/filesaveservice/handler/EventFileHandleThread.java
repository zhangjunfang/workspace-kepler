/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： FileSaveService		</li><br>
 * <li>文件名称：com.ctfo.filesaveservice.service EventFileHandleThread.java	</li><br>
 * <li>时        间：2013-9-9  下午3:57:38	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.filesaveservice.handler;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.filesaveservice.util.ConfigLoader;
import com.ctfo.filesaveservice.util.Constant;
import com.ctfo.filesaveservice.util.FileUtil;

/*****************************************
 * <li>描        述：		
 * 
 *****************************************/
public class EventFileHandleThread extends Thread{
	private static final Logger logger = LoggerFactory.getLogger(EventFileHandleThread.class);

	private ArrayBlockingQueue<Map<String, String>> dataQueue = new ArrayBlockingQueue<Map<String, String>>(100000);
	/** 线程编号	*/
	private int threadId;
	/** 计数器	  */
	private int index;
	/** 上次时间	  */
	private long lastTime = System.currentTimeMillis();
	
	/** 驾驶行为事件文件目录	  */
	private String eventFilePath;
	
	
	public EventFileHandleThread(int threadId){
		super("EventFileHandleThread" + threadId);
		this.threadId = threadId;
		this.eventFilePath = ConfigLoader.fileParamMap.get("eventPath");;
	}
	public void putDataMap(Map<String, String> dataMap) {
		try {
			dataQueue.put(dataMap);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}
	
	public int getQueueSize() {
		return dataQueue.size();
	}
	
	@Override
	public void run() {
		logger.info("驾驶事件文件线程" + threadId + "启动");
		while (true) {
			try {
				Map<String,String> dataMap = dataQueue.take();
				saveLineStatus(dataMap); 
				long currentTime = System.currentTimeMillis(); //按时间批量提交
				if((currentTime - lastTime) > 10000){
					lastTime = currentTime;
					logger.info("eventfile-:" + threadId + ",size:" + getQueueSize() + ",10秒处理数据:"+index+"条");
					index = 0;
				}
				index ++;
			} catch (Exception e) {
				logger.error("文件存储主线程队列出错" + e.getMessage(),e);
			}
		}
	}
	
	/**
	 * 文件存储：
	 * 1-加热器工作；2-空调工作；3-发动机超转；4-过长怠速；5-超经济区运行；6-空档滑行；7-怠速空调；8-二档起步；9-档位不当;10-超速；11-疲劳驾驶
	 * 
	 * @param fileName
	 * @param driverEvent
	 * @throws IOException
	 */
	public void saveLineStatus(Map<String, String> dataMap) { 
		String driverEvent = dataMap.get(Constant.N516);
		String vid = dataMap.get(Constant.VID);
		
		String[] event = driverEvent.split("\\|", 3);
		if (event.length == 3) {
			String[] inner = event[1].split("]", 6);
			if (inner.length == 6) {
				String gpsTime = inner[5].replaceAll("\\[", "").replaceAll("\\]", "");
				String gpsYear = gpsTime.substring(0, 4);
				String gpsMonth = gpsTime.substring(4, 6);
				String gpsDay = gpsTime.substring(6, 8);
				if(inValidDate(gpsTime)){
					logger.error("vid:[{}]驾驶行为事件处理异常,非法时间数据:[{}]", vid, event);
					return ;
				}
				String fileName = eventFilePath + Constant.BACKSLASH + gpsYear + Constant.BACKSLASH + gpsMonth + Constant.BACKSLASH + gpsDay + Constant.BACKSLASH + vid +  Constant.TXT;
				RandomAccessFile eventrf = null;
				try {
					eventrf = new RandomAccessFile(fileName, Constant.RW);
					eventrf.seek(eventrf.length());
					eventrf.writeBytes(driverEvent + Constant.NEWLINE);
				} catch (FileNotFoundException e) {
					logger.error("车辆编号 ： " + vid + "找不到文件操作" + fileName +",错误数据:" + dataMap.get(Constant.COMMAND)+ " 失败！" + e.getMessage(), e);
					FileUtil.coverFolder(eventFilePath);
					try {
						eventrf = new RandomAccessFile(fileName.toString(), "rw");
						eventrf.seek(eventrf.length());
						eventrf.writeBytes(driverEvent + "\r\n");
					} catch (Exception e1) {
						logger.error("重新创建目录后写入文件异常:" + e1.getMessage(), e1);
					}
				} catch (Exception e) {
					logger.error("车辆编号 ： " + vid + ",错误数据:" + dataMap.get(Constant.COMMAND) + " 失败！" + e.getMessage(), e);
				} finally {
					try {
						if (eventrf != null) {
							eventrf.close();
						}
					} catch (IOException e) {
						eventrf = null;
					}
				}
			}
		} else {
			logger.error("存储驾驶行为事件数据格式不正确:" + driverEvent);
		}
	}
	/*****************************************
	 * <li>描        述：非法时间		</li><br>
	 * <li>时        间：2013-9-12  上午10:29:41	</li><br>
	 * <li>参数： @param gpsYear
	 * <li>参数：@param gpsDay 
	 * <li>参数：@param gpsMonth 
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	private boolean inValidDate(String gpsTime) {
		SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMdd");
		try {
			Date gpsDate = sdf.parse(gpsTime);
			long gpsMillis = gpsDate.getTime();
			long currentTime = System.currentTimeMillis() + 86400000;
//			只处理6个月前及1天后的数据
			if((currentTime - 15552000000l) >  gpsMillis || gpsMillis > currentTime){
				return true;
			} else {
				return false;
			}
		} catch (ParseException e) {
			logger.error("驾驶行为事件处理--验证时间合法性异常,非法时间:"+gpsTime);
			return true;
		}
	}
}
