package com.ctfo.dataanalysisservice.service;

import com.ctfo.dataanalysisservice.beans.VehicleMessage;

/**
 * 软报警业务分析服务接口
 * 
 * @author yangjian
 * 
 */
public interface IBussinessAnalysisService {

	public void run();

	public void addPacket(VehicleMessage vehicleMessage);

	public int getPacketsSize();

	public int getPacket();
}
