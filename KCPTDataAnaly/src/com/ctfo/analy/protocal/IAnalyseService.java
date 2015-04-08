package com.ctfo.analy.protocal;
 

import com.ctfo.analy.beans.MessageBean;
import com.ctfo.analy.beans.VehicleMessageBean;
 

/**
 * 报文解析接口
 * 
 * @author yangyi
 * 
 */
public interface IAnalyseService {
	public VehicleMessageBean dealPacket(MessageBean message);
}
