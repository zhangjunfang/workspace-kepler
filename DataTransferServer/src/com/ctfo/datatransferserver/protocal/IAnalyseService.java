package com.ctfo.datatransferserver.protocal;

import com.ctfo.datatransferserver.beans.VehiclePolymerizeBean;
import com.ctfo.datatransferserver.dao.ServiceUnitDao;

/**
 * 报文解析接口
 * 
 * @author yangyi
 * 
 */
public interface IAnalyseService {
	public VehiclePolymerizeBean dealPacket(String message,ServiceUnitDao serviceUnitDao);
}
