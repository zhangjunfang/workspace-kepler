package com.ctfo.dataanalysisservice.service;

import com.ctfo.dataanalysisservice.beans.Message;
import com.ctfo.dataanalysisservice.beans.VehicleMessage;

/**
 * 报文解析接口
 * 
 * 
 */
public interface IAnalyseService {

	/**
	 * 解析报文字符串
	 * 
	 * @param message
	 * @return
	 */
	public VehicleMessage dealPacket(Message message);
}
