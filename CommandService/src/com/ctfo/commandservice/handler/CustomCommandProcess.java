package com.ctfo.commandservice.handler;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.commandservice.model.Custom;
import com.ctfo.commandservice.model.CustomIssued;
import com.ctfo.commandservice.model.CustomUpload;
import com.ctfo.commandservice.service.OracleService;
import com.ctfo.commandservice.service.RedisService;
import com.ctfo.commandservice.util.Base64_URl;
import com.ctfo.commandservice.util.Cache;
import com.ctfo.commandservice.util.ConfigLoader;
import com.ctfo.commandservice.util.Constant;
import com.ctfo.generator.pk.GeneratorPK;
/**
 *	自定义指令处理线程
 */
public class CustomCommandProcess extends Thread {
	private static CustomCommandProcess instance = new CustomCommandProcess();
	/**	日志	*/
	private static final Logger logger = LoggerFactory.getLogger(CustomCommandProcess.class);
	/**	Driver队列	*/ 
	private static ArrayBlockingQueue<Map<String, String>> queue = new ArrayBlockingQueue<Map<String, String>>(10000);
	/**	自定指令下发列表	*/ 
	private List<CustomIssued> issuedList = new ArrayList<CustomIssued>();
	/**	自定指令上传列表	*/ 
	private List<CustomUpload> uploadList = new ArrayList<CustomUpload>();
	/**	运行状态	*/ 
	private static boolean RUNNING = false; 
	/** 批量提交数量	 */
	private int index;
	/** 日志间隔 （默认60秒）	 */
	private int interval = 60000;
	/** 最近处理时间	 */
	private long lastTime = System.currentTimeMillis();
	/** 批量存储数量	 */
	private int batchSize = 500;
	/** 批量存储间隔时间	 */
	private long batchTime = 3000;
	/** 最后批量处理时间	 */
	private long lastBatch = 0;
	/** 清理间隔次数	 */
	private int clearIndex = 0;
	/** 过期秒数	 */
	private int expireSeconds;
	
	private CustomCommandProcess(){ 
		setName("CustomCommandProcess"); 
		batchSize = Integer.parseInt(ConfigLoader.config.get("customBatchSize"));
		batchTime = Long.parseLong(ConfigLoader.config.get("customBatchTime"));
		expireSeconds = Integer.parseInt(ConfigLoader.config.get("customExpireSeconds"));
	}
	public static CustomCommandProcess getInstance(){
		if(instance == null){
			return new CustomCommandProcess();
		}
		return instance;
	}

	/**
	 * 业务处理方法
	 */
	public void run(){
		RUNNING = true;
		while (true) {
			try {
				Map<String, String> map = queue.poll();
				if(map != null){
					index++;
					String head = map.get(Constant.HEAD);
					String mtype = map.get(Constant.MTYPE); 
					String type = map.get(Constant.TYPE); 
					String vid = map.get(Constant.VID);
					Custom custom = Cache.getCustom(vid);
					if(custom != null ){
						map.put(Constant.SEQ, custom.getSeq());
					}
					if(type != null && mtype.equals("D_SETP") && type.equals("10") && head.equals("CAITS")){ // 自定指令下发
						CustomIssued issued = getCustomIssued(map);
						CustomUpload upload = getCustomUpload(map);
						upload.setCommandType(1); 
						if(issued != null && upload != null ){
							uploadList.add(upload);
							issuedList.add(issued);
						}else{ 
							logger.error("获取自定义下发信息失败:{}" , map.get(Constant.COMMAND)); 
						}
					} else if(type != null && mtype.equals("U_REPT") && type.equals("10") && head.equals("CAITS")){ // 自定指令上传
						CustomUpload upload = getCustomUpload(map);
						if(upload != null){
							uploadList.add(upload);
						}else{ 
							logger.error("获取自定义上传信息失败:{}" , map.get(Constant.COMMAND)); 
						}
					} else {
						CustomUpload upload = getAllCustomUpload(map);
						if(upload != null){
							uploadList.add(upload);
						}else{ 
							logger.error("获取自定义上传信息失败:{}" , map.get(Constant.COMMAND)); 
						}
					}
				} else {
					Thread.sleep(1);
				}
				long cur = System.currentTimeMillis();
				if(cur - lastBatch > batchTime){
					int updateSize = 0;
					if(issuedList.size() > 0){
						RedisService.saveCustomCommand(issuedList, expireSeconds);
						updateSize = OracleService.saveCustomCommand(issuedList, batchSize);
						issuedList.clear();
					}
					long utime = System.currentTimeMillis();
					int saveSize = 0;
					if(uploadList.size() > 0){
						saveSize = OracleService.saveCustomCommandDetail(uploadList, batchSize);
						uploadList.clear();
					} 
					long stime = System.currentTimeMillis();
					logger.debug("存储[{}]条下发耗时[{}]ms, 存储[{}]条上传耗时[{}]ms", updateSize, utime-cur, saveSize, stime-utime);
					lastBatch = System.currentTimeMillis();
				}
				
				long currentTime = System.currentTimeMillis();
				if(currentTime - lastTime > interval){
					clearIndex++;
					int size = getQueueSize();
					int intervalTime = interval/1000;
					logger.info("CustomCommandProcess---{}s处理[{}]条, 排队:[{}]条", intervalTime, index, size);
					lastTime = System.currentTimeMillis();
					index = 0;
					if(clearIndex == 60){ // 每小时清理一次自定义指令缓存
						Cache.clearCustomCommandCache();
					}
				}
			} catch (Exception e) {
				logger.error("Oil信息处理线程错误:" + e.getMessage(), e);
			}
		}
	}
	
	/**
	 * 获取所有自定义上传对象
	 * @param map
	 * @return
	 */
	private CustomUpload getAllCustomUpload(Map<String, String> map) {
		String seq = map.get(Constant.SEQ); // 车辆编号
		String command = map.get(Constant.COMMAND); // 报文内容
		String mtype = map.get(Constant.MTYPE); // 报文类型
		if(StringUtils.isNotBlank(seq) && StringUtils.isNotBlank(command)){ 
			CustomUpload upload = new CustomUpload();
			upload.setSeq(seq);
			upload.setType(mtype); 
			upload.setContent(command);
			upload.setCommandType(2);
			return upload; 
		}
		return null;
	}

	/**
	 * 获取自定义上传对象
	 * @param map
	 * @return
	 */
	private CustomUpload getCustomUpload(Map<String, String> map) {
		String seq = map.get(Constant.SEQ); // 车辆编号
		String type = map.get("95");	// 报文类型
		String content = map.get("96"); // 报文内容
		if(StringUtils.isNumeric(type) && StringUtils.isNotBlank(seq) && StringUtils.isNotBlank(content)){ 
			CustomUpload upload = new CustomUpload();
			upload.setSeq(seq);
			String t = getHex(Integer.toHexString(Integer.parseInt(type)));
			upload.setType(t);
			upload.setContent(Base64_URl.byteToString(content));
			upload.setCommandType(2);
			return upload;
		}
		return null;
	}

	private String getHex(String hexString) {
		int l = 4 - hexString.length();
		for(int i = 0 ; i < l; i++){
			hexString = "0" + hexString;
		}
		return hexString;
	}
	
	/**
	 * 获取自定义下发对象
	 * @param map
	 * @return
	 */
	private CustomIssued getCustomIssued(Map<String, String> map) {
		String seq = map.get(Constant.SEQ);
		String vid = map.get(Constant.VID);
		String createId = map.get("97");
		if(StringUtils.isNotBlank(seq) && StringUtils.isNotBlank(vid) && StringUtils.isNotBlank(createId)){ 
			CustomIssued issued = new CustomIssued();
			issued.setId(GeneratorPK.instance().getPKString()); 
			issued.setVid(vid);
			issued.setSeq(seq); 
			issued.setStatus(0);
			issued.setCreateId(createId);
			issued.setCreateUtc(System.currentTimeMillis()); 
			return issued;
		}
		return null;
	}

	/**	获得队列长度	*/
	private int getQueueSize() {
		return queue.size();
	}
	
	
	/**
	 * 加入队列
	 * @param driver
	 */
	public void put(Map<String, String> map) {
		try {
			queue.put(map);
		} catch (InterruptedException e) {
			logger.error("加入队列异常:" + e.getMessage(), e);
		}
	}
	/**
	 * 加入Driver队列
	 * @param driver
	 */
	public static boolean offer(Map<String, String> map) {
		if(RUNNING){
			return queue.offer(map);
		}
		return false;
	}
}

