package com.ctfo.storage.process.service;


import com.ctfo.storage.process.model.Media;
import com.ctfo.storage.process.util.Tools;


/**
 * ProtocolAnalyService
 * 
 * 
 * @author huangjincheng
 * 2014-5-14下午05:20:46
 * 
 */
public class ProtocolAnalyService {
	
	/**
	 * 
	 * 多媒体历史数据解析
	 * @param message 消息
	 * @return
	 */
	public Media getMediaDataFromControl(String message) {
		Media media = new Media();
		String mediaContent = Tools.getTransferContent(message);
		String mediaBody = mediaContent.substring(36,mediaContent.length());//消息体
		String slaveType = mediaBody.substring(0,4);
		String masterType = mediaContent.substring(16,20);
		media.setMasterType(masterType);
		media.setSlaveType(slaveType);
		media.setCenterSourceIp(Tools.getASCIIByHex(mediaContent.substring(0,16)));
		media.setMessageRuuningNumber(mediaContent.substring(20,28));
		media.setMessagelength(Integer.parseInt(mediaContent.substring(28,36),16));
		
		media.setPhoneNum(mediaBody.substring(4,16));
		media.setMediaId(mediaBody.substring(16,24));
		media.setMediaType(mediaBody.substring(24,26));
		media.setMediaFormatCode(mediaBody.substring(26,28));
		media.setEventCode(mediaBody.substring(28,30));
		media.setChannelId(mediaBody.substring(30,32));
		
		media.setLocationData(mediaBody.substring(32,88));
		media.setMediaData(Tools.hexStrToBytes(mediaBody.substring(88)));
		
		return media;
	}
	
	/**
	 * 
	 * 多媒体事件解析
	 * @param message 消息
	 * @return
	 */
	public Media getMediaEventFromControl(String message) {
		Media media = new Media();
		String mediaContent = Tools.getTransferContent(message);
		String mediaBody = mediaContent.substring(36,mediaContent.length());//消息体
		String slaveType = mediaBody.substring(0,4);
		String masterType = mediaContent.substring(16,20);
		media.setMasterType(masterType);
		media.setSlaveType(slaveType);
		media.setCenterSourceIp(Tools.getASCIIByHex(mediaContent.substring(0,16)));
		media.setMessageRuuningNumber(mediaContent.substring(20,28));
		media.setMessagelength(Integer.parseInt(mediaContent.substring(28,36),16));
		
		media.setPhoneNum(mediaBody.substring(4,16));
		media.setMediaId(mediaBody.substring(16,24));
		media.setMediaType(mediaBody.substring(24,26));
		media.setMediaFormatCode(mediaBody.substring(26,28));
		media.setEventCode(mediaBody.substring(28,30));
		media.setChannelId(mediaBody.substring(30,32));
		
		return media;
	}
}
