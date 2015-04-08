package com.ctfo.savecenter.analy;

import java.util.Map;

import com.ctfo.savecenter.beans.Message;

/**
 * 报文解析接口
 * 
 * @author yangyi
 * 
 */
public interface IAnalyseService {
	public Map<String, String> dealPacket(Message message);
}
