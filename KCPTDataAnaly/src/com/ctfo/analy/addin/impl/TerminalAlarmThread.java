package com.ctfo.analy.addin.impl;


import java.util.Map;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.log4j.Logger;

import com.ctfo.analy.TempMemory;
import com.ctfo.analy.addin.PacketAnalyser;
import com.ctfo.analy.beans.AlarmBaseBean;
import com.ctfo.analy.beans.AlarmNotice;
import com.ctfo.analy.beans.MessageBean;
import com.ctfo.analy.beans.OrgAlarmConfBean;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.io.DataPool;
import com.ctfo.analy.util.Base64_URl;
import com.ctfo.analy.util.ExceptionUtil;
import com.lingtu.xmlconf.XmlConf;

/**
 * 终端上报的告警，平台下消息提醒类
 * 
 * @author cuis
 *
 */
public class TerminalAlarmThread extends Thread implements PacketAnalyser{
	
	private static final Logger logger = Logger.getLogger(TerminalAlarmThread.class);
	// 待处理数据队列
	private ArrayBlockingQueue<VehicleMessageBean> vPacket = new ArrayBlockingQueue<VehicleMessageBean>(100000);
	
	//private GPSInspectionAdapter gpsInspectionAdapter = null; 
	
	// 线程ID	 
	private int threadId = 0;
	
	private static Map<String, VehicleMessageBean> cacheAlarmCode = new ConcurrentHashMap<String, VehicleMessageBean>();//缓存位置信息汇报对象
	
	@Override
	public void addPacket(VehicleMessageBean vehicleMessage) {
		try {
			
			vPacket.put(vehicleMessage);
		} catch (InterruptedException e) {
			logger.error(e);
		}
	}
	
	public void run(){
		while(true){
			try {
				logger.debug(threadId+"，TerminalAlarmThread主线程开始处理" + vPacket.size());
				VehicleMessageBean vehicleMessage = vPacket.take();  
				if (vehicleMessage != null&&("0".equals(vehicleMessage.getMsgType())
						|| "1".equals(vehicleMessage.getMsgType()))) {
					checkIsHasALarmNotice(vehicleMessage);
				}
				logger.debug(threadId+"，TerminalAlarmThread主线程处理结束" + vPacket.size());
			} catch (InterruptedException e) {
				logger.error(e);
			}
		}
	}
	
 
	/**
	 * 判断告警是否需要下发消息提示
	 * @param vehicleMessage
	 */
	@SuppressWarnings("rawtypes")
	public void checkIsHasALarmNotice(VehicleMessageBean vehicleMessage){
		try{
		String cmddr = vehicleMessage.getCommanddr();
		long utc = vehicleMessage.getUtc();
		//
		AlarmBaseBean bean = TempMemory.getAlarmVehicleMap(cmddr);
		if (bean==null){
			logger.debug("Commanddr:" + cmddr+" 缓存中无该车辆");
			return ;
		}
		String entId = ""+bean.getTeamId();
		if (!TempMemory.containsOrgAlarmNoticeMap(entId)){
			logger.debug("Commanddr:" + cmddr+" 企业[entId="+entId+"]未设置告警提示信息");
			return;
		}
		
		ConcurrentHashMap<String,AlarmNotice> alarmNoticeMap = TempMemory.getOrgAlarmNoticeMap(entId);
		
 		//判断是否下发告警提示消息
		String alarmcodeStr = convertAlarmCode(vehicleMessage);
		
		logger.info("Commanddr:" + cmddr+" 告警编码串: " + alarmcodeStr);
		
		vehicleMessage.setAlarmcode(alarmcodeStr);
		if (alarmcodeStr!=null&&!"".equals(alarmcodeStr)){
			String alarmcode[] = alarmcodeStr.split(",");
			if (alarmcode!=null&&alarmcode.length>0){
				VehicleMessageBean cacheBean = cacheAlarmCode.get(cmddr);//从缓存中取出上一次缓存的对象
				if (cacheBean!=null){
				String lastAlarmCode = cacheBean.getAlarmcode();
				long lastUtc = cacheBean.getUtc();
				if (utc>=lastUtc){//当前时间大于缓存时间的数据进行分析
					if ((utc-lastUtc)<3*60*1000){//当前时间大于上次缓存的时间，并且差值在3分钟之内，认为是连续记录；连续记录只发送新增的告警
						
						for (int i=0;i<alarmcode.length;i++){
							if(alarmcode[i]!=null&&!"".equals(alarmcode[i])){
								if (!(lastAlarmCode.startsWith(alarmcode[i]+",")||lastAlarmCode.endsWith(","+alarmcode[i])||lastAlarmCode.indexOf(","+alarmcode[i]+",")>0)){
									if (alarmNoticeMap.containsKey(alarmcode[i])){
										AlarmNotice notice = alarmNoticeMap.get(alarmcode[i]);
										sendAlarmNotice(vehicleMessage,notice);
									}
								}
							}
						}
					}else{//中断记录发送所有告警
						for (int i=0;i<alarmcode.length;i++){
							if(alarmcode[i]!=null&&!"".equals(alarmcode[i])){
								if (alarmNoticeMap.containsKey(alarmcode[i])){
									AlarmNotice notice = alarmNoticeMap.get(alarmcode[i]);
									sendAlarmNotice(vehicleMessage,notice);
								}
							}
						}
					}
					cacheAlarmCode.put(cmddr, vehicleMessage);
				}else{
					logger.info("Commanddr:" + cmddr+" 无效信息：当前时间（utc="+utc+"）小于缓存时间(lastutc="+lastUtc+")");
				}
				}else{
					cacheAlarmCode.put(cmddr, vehicleMessage);
				}
			}
		}
		}catch(Exception ex){
			logger.debug("Commanddr:" + vehicleMessage.getCommanddr()+" 判断告警是否需要下发消息提示出错："+ex);
			ex.printStackTrace();
		}
	}
 
	public  static void checkIsHasALarmNotice(VehicleMessageBean vehicleMessage,String alarmcode){
		try{
			String cmddr = vehicleMessage.getCommanddr();
			//
			AlarmBaseBean bean = TempMemory.getAlarmVehicleMap(cmddr);
			if (bean==null){
				logger.debug("Commanddr:" + cmddr+" 缓存中无该车辆");
				return ;
			}
			String entId = ""+bean.getTeamId();
			if (!TempMemory.containsOrgAlarmNoticeMap(entId)){
				logger.debug("Commanddr:" + cmddr+" 企业[entId="+entId+"]未设置告警提示信息");
				return;
			}
			ConcurrentHashMap<String,AlarmNotice> alarmNoticeMap = TempMemory.getOrgAlarmNoticeMap(entId);
			
	 		//判断是否下发告警提示消息
			if(alarmcode!=null){
				if (alarmNoticeMap.containsKey(alarmcode)){
					AlarmNotice notice = alarmNoticeMap.get(alarmcode);
					sendAlarmNotice(vehicleMessage,notice);
				}
			}
		logger.info("Commanddr:" + cmddr+" 判断告警是否需要下发消息提示完成");
		}catch(Exception ex){
			logger.debug("Commanddr:" + vehicleMessage.getCommanddr()+" 判断告警是否需要下发消息提示出错："+ex);
		}
	}
	
	@Override
	public void endAnalyser() {
		
	}

	@Override
	public int getPacketsSize() {
		return vPacket.size();
	}

	@Override
	public void initAnalyser(int nId, XmlConf config, String nodeName) throws Exception {
		this.threadId = nId;
		//gpsInspectionAdapter =  new GPSInspectionAdapter();
		start();
	}

	/**
	 * 封装下发消息内容
	 * 仅TTS播报：9;
                  仅终端显示屏显示：17;
       TTS播报且终端显示屏显示：25;
	 * @param alarmNotice
	 */
	public static void sendAlarmNotice(VehicleMessageBean vehicleMessage,AlarmNotice alarmNotice) {
		
		Short tts =alarmNotice.getTtsFlag();
		Short display =alarmNotice.getDisplayFlag();
		String styleValue="9";
 		if(tts==0&&display==1){
 			styleValue="17";
 		}
	    if(tts==1&&display==0){
	    	styleValue="9";
 		}
	    if(tts==1&&display==1){
	    	styleValue="25";
		}	    
		String sendcommand = "CAITS 0_0_0 " + vehicleMessage.getOemcode() + "_" + vehicleMessage.getCommanddr()
				+ " 0 D_SNDM {TYPE:1,1:"+styleValue+",2:" + Base64_URl.base64Encode(alarmNotice.getMsg())
				+ "} \r\n" + vehicleMessage.getVid() + "";
		sendMessage(vehicleMessage.getMsgid(), sendcommand);
		logger.debug("告警提示消息发送终端成功[" + vehicleMessage.getCommanddr() + "] alarmCode:"+alarmNotice.getAlarmCode());
	}
	/**
	 * 封装下发消息内容
	 * 仅TTS播报：9;
                  仅终端显示屏显示：17;
       TTS播报且终端显示屏显示：25;
	 * @param alarmNotice
	 */
	public void sendAlarmNotice(AlarmNotice alarmNotice) {
		
		Short tts =alarmNotice.getTtsFlag();
		Short display =alarmNotice.getDisplayFlag();
		String styleValue="9";
 		if(tts==0&&display==1){
 			styleValue="17";
 		}
	    if(tts==1&&display==0){
	    	styleValue="9";
 		}
	    if(tts==1&&display==1){
	    	styleValue="25";
		}	    
		String sendcommand = "CAITS 0_0_0 " + alarmNotice.getOemcode() + "_" + alarmNotice.getCommaddr()
				+ " 0 D_SNDM {TYPE:1,1:"+styleValue+",2:" + Base64_URl.base64Encode(alarmNotice.getMsg())
				+ "} \r\n" + alarmNotice.getVid() + "";
		sendMessage(alarmNotice.getMsgid(), sendcommand);
		logger.debug("告警提示消息发送终端成功[" + alarmNotice.getCommaddr() + "]");
	}

	/**
	 * 信息下发
	 * 
	 * @param msgid
	 * @param sendcommand
	 */
	private static void sendMessage(String msgid, String sendcommand) {
		MessageBean message = new MessageBean();
		message.setMsgid(msgid);
		message.setCommand(sendcommand);
		DataPool.setReceivePacket(message);
	}
	
	/**
	 * 转换车辆报警位，把当前指令中企业开放的报警转换为以逗号分隔的字符串形式
	 * @param vehicleMessage
	 */
	private String convertAlarmCode(VehicleMessageBean vehicleMessage){
		String alarmCode = ",";
		try{
		String commaddr = vehicleMessage.getCommanddr();
		AlarmBaseBean bean = TempMemory.getAlarmVehicleMap(commaddr);//软报警缓存车辆信息
		
		if (bean!=null){
			OrgAlarmConfBean oacBean = TempMemory.getOrgAlarmConfMap(bean.getTeamId());//企业告警设置信息
			if (oacBean!=null){
			String openAlarmCode = oacBean.getAlarmCode();
			
			String alarmString = vehicleMessage.getBaseAlarmStatus();//基本告警位
			if (alarmString != null) {
				String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
				String status;
				for (int i = 0; i < alarmstatus.length(); i++) {
					status = alarmstatus.substring(alarmstatus.length() - i - 1,alarmstatus.length() - i);
					if ("1".equals(status)) {
						String tmpcode = ""+i;
						if (openAlarmCode.startsWith(tmpcode+",")||openAlarmCode.endsWith(","+tmpcode)||openAlarmCode.indexOf(","+tmpcode+",")>-1){
							alarmCode = alarmCode + tmpcode + ",";
						}
					}
				}// End for
				alarmString = null;
			}
			
			alarmString = vehicleMessage.getExtendAlarmStatus();//扩展告警位
			if (alarmString != null) {
				String alarmstatus = Long.toBinaryString(Long.parseLong(alarmString, 10));
				String status;
				for (int i = 0; i < alarmstatus.length(); i++) {
					status = alarmstatus.substring(alarmstatus.length() - i - 1,alarmstatus.length() - i);
					if ("1".equals(status)) {
						String tmpcode = ""+(i+32);
						if (openAlarmCode.startsWith(tmpcode+",")||openAlarmCode.endsWith(","+tmpcode)||openAlarmCode.indexOf(","+tmpcode+",")>-1){
							alarmCode = alarmCode + tmpcode + ",";
						}
					}
				}// End for
				alarmString = null;
			}
		}
		}
		
		if (alarmCode.length()>1){
			alarmCode = alarmCode.substring(1);
		}else{
			alarmCode="";
		}
		
		}catch(Exception ex){
			alarmCode="";
			logger.error("GJTSXF [COMMADDR="+vehicleMessage.getCommanddr()+"] 告警编码转换出错"+ExceptionUtil.getErrorStack(ex, 0));
		}
		return alarmCode;
	}
	
	
}
