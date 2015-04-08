package com.caits.analysisserver.services;

import com.caits.analysisserver.bean.ThVehicleSpeedAnomalous;
import com.caits.analysisserver.bean.VehicleMessageBean;

public class SpeedAnomalousAnalyserService {
	
	private ThVehicleSpeedAnomalous thVehicleSpeedAnomalous = new ThVehicleSpeedAnomalous();
	
	public SpeedAnomalousAnalyserService(long utc,String vid){
		thVehicleSpeedAnomalous.setStatDate(utc+12*60*60*1000);
		thVehicleSpeedAnomalous.setVid(vid);
	}
	
	
	public void executeAnalyser(VehicleMessageBean trackBean){
		Long vss = trackBean.getVssSpeed();
		Long gps = trackBean.getGpsSpeed();
		String spdFrom = trackBean.getSpeedFrom();
			if (vss>=50&&gps>=50){
				thVehicleSpeedAnomalous.setVssGPSSpeedTotal(vss, gps);
				//保存最后有效的车速来源
				thVehicleSpeedAnomalous.setSpeedForm(spdFrom);
			}
	}
	
	@SuppressWarnings("unused")
	private void saveSpeedAnomalous(){
		if (thVehicleSpeedAnomalous!=null&&thVehicleSpeedAnomalous.getCount()>0){
			//saveSpeedAnomalous();
		}
	}

	public ThVehicleSpeedAnomalous getThVehicleSpeedAnomalous() {
		return thVehicleSpeedAnomalous;
	}
	
	

}
