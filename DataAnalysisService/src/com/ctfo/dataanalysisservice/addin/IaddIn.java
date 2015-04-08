package com.ctfo.dataanalysisservice.addin;

import com.ctfo.dataanalysisservice.beans.VehicleMessage;

/**
 * 插件服务 （软报警业务分析服务接口）
 * 
 * @author yangjian
 * 
 */
public interface IaddIn {

	public void start();

	// public IaddIn init();

	public void addPacket(VehicleMessage vehicleMessage)
			throws InterruptedException;

	public int getPacketsSize();

	public VehicleMessage getPacket() throws InterruptedException;
}
