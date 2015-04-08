package com.caits.analysisserver.bean;


public class AirTempertureBean extends ExcBaseBean {
	
	private long statDate;
	private String vid;
	private CountTimeBean temperature_0 = null;
	
	private CountTimeBean temperature_0_10 = null;
	
	private CountTimeBean temperature_10_20 = null;
	
	private CountTimeBean temperature_20_25 = null;
	
	private CountTimeBean temperature_25_30 = null;
	
	private CountTimeBean temperature_30_35 = null;
	
	private CountTimeBean temperature_35_40 = null;
	
	private CountTimeBean temperature_40_45 = null;
	
	private CountTimeBean temperature_45_50 = null;
	
	private CountTimeBean temperature_50_60 = null;
	
	private CountTimeBean temperature_60_70 = null;
	
	private CountTimeBean temperature_70 = null;
	
	private long lastGpsTime = 0;
	
	private String preSection;
	
	private int tmpTime;
	
	public AirTempertureBean(){
		temperature_0 = new CountTimeBean();
		
		temperature_0_10 = new CountTimeBean();
		
		temperature_10_20 = new CountTimeBean();
		
		temperature_20_25 = new CountTimeBean();
		
		temperature_25_30 = new CountTimeBean();
		
		temperature_30_35 = new CountTimeBean();
		
		temperature_35_40 = new CountTimeBean();
		
		temperature_40_45 = new CountTimeBean();
		
		temperature_45_50 = new CountTimeBean();
		
		temperature_50_60 = new CountTimeBean();
		
		temperature_60_70 = new CountTimeBean();
		
		temperature_70 = new CountTimeBean();
	}
	
	public AirTempertureBean(long utc,String vid){
		
		this.statDate = utc;
		this.vid = vid;
		temperature_0 = new CountTimeBean();
		
		temperature_0_10 = new CountTimeBean();
		
		temperature_10_20 = new CountTimeBean();
		
		temperature_20_25 = new CountTimeBean();
		
		temperature_25_30 = new CountTimeBean();
		
		temperature_30_35 = new CountTimeBean();
		
		temperature_35_40 = new CountTimeBean();
		
		temperature_40_45 = new CountTimeBean();
		
		temperature_45_50 = new CountTimeBean();
		
		temperature_50_60 = new CountTimeBean();
		
		temperature_60_70 = new CountTimeBean();
		
		temperature_70 = new CountTimeBean();
	}
	
	/****
	 * 计算进气温度区间值，进气温度属于总线数据，5分钟上报一次
	 * @param airTemperture
	 */
	public void account(long airTemperture,long gpsTime){
		airTemperture = airTemperture + ExcConstants.COOLLIQUID;
		int time = 0;
		if(lastGpsTime > 0){ // 第一条统计数据，则赋默认值10s
			time = (int)(gpsTime - lastGpsTime);
			if(time <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
				time = time/1000;
			}else{
				lastGpsTime = gpsTime;		
				return;
			}
		}else{
			time = 10;
		}
		lastGpsTime = gpsTime;
		if(airTemperture == 0){
			temperature_0.addNum(1);
			temperature_0.addTime(time);
		}else if(airTemperture < 10){
			temperature_0_10.addNum(1);
			temperature_0_10.addTime(time);
		}else if(airTemperture < 20){
			temperature_10_20.addNum(1);
			temperature_10_20.addTime(time);
		}else if(airTemperture < 25){
			temperature_20_25.addNum(1);
			temperature_20_25.addTime(time);
		}else if(airTemperture < 30){
			temperature_25_30.addNum(1);
			temperature_25_30.addTime(time);
		}else if(airTemperture < 35){
			temperature_30_35.addNum(1);
			temperature_30_35.addTime(time);
		}else if(airTemperture < 40){
			temperature_35_40.addNum(1);
			temperature_35_40.addTime(time);
		}else if(airTemperture < 45){
			temperature_40_45.addNum(1);
			temperature_40_45.addTime(time);
		}else if(airTemperture < 50){
			temperature_45_50.addNum(1);
			temperature_45_50.addTime(time);
		}else if(airTemperture < 60){
			temperature_50_60.addNum(1);
			temperature_50_60.addTime(time);
		}else if(airTemperture < 70){
			temperature_60_70.addNum(1);
			temperature_60_70.addTime(time);
		}else if(airTemperture > 70){
			temperature_70.addNum(1);
			temperature_70.addTime(time);
		}
	}

	public CountTimeBean getTemperature_0() {
		return temperature_0;
	}

	public void setTemperature_0(CountTimeBean temperature_0) {
		this.temperature_0 = temperature_0;
	}

	public CountTimeBean getTemperature_0_10() {
		return temperature_0_10;
	}

	public void setTemperature_0_10(CountTimeBean temperature_0_10) {
		this.temperature_0_10 = temperature_0_10;
	}

	public CountTimeBean getTemperature_10_20() {
		return temperature_10_20;
	}

	public void setTemperature_10_20(CountTimeBean temperature_10_20) {
		this.temperature_10_20 = temperature_10_20;
	}

	public CountTimeBean getTemperature_20_25() {
		return temperature_20_25;
	}

	public void setTemperature_20_25(CountTimeBean temperature_20_25) {
		this.temperature_20_25 = temperature_20_25;
	}

	public CountTimeBean getTemperature_25_30() {
		return temperature_25_30;
	}

	public void setTemperature_25_30(CountTimeBean temperature_25_30) {
		this.temperature_25_30 = temperature_25_30;
	}

	public CountTimeBean getTemperature_30_35() {
		return temperature_30_35;
	}

	public void setTemperature_30_35(CountTimeBean temperature_30_35) {
		this.temperature_30_35 = temperature_30_35;
	}

	public CountTimeBean getTemperature_35_40() {
		return temperature_35_40;
	}

	public void setTemperature_35_40(CountTimeBean temperature_35_40) {
		this.temperature_35_40 = temperature_35_40;
	}

	public CountTimeBean getTemperature_40_45() {
		return temperature_40_45;
	}

	public void setTemperature_40_45(CountTimeBean temperature_40_45) {
		this.temperature_40_45 = temperature_40_45;
	}

	public CountTimeBean getTemperature_45_50() {
		return temperature_45_50;
	}

	public void setTemperature_45_50(CountTimeBean temperature_45_50) {
		this.temperature_45_50 = temperature_45_50;
	}

	public CountTimeBean getTemperature_50_60() {
		return temperature_50_60;
	}

	public void setTemperature_50_60(CountTimeBean temperature_50_60) {
		this.temperature_50_60 = temperature_50_60;
	}

	public CountTimeBean getTemperature_60_70() {
		return temperature_60_70;
	}

	public void setTemperature_60_70(CountTimeBean temperature_60_70) {
		this.temperature_60_70 = temperature_60_70;
	}

	public CountTimeBean getTemperature_70() {
		return temperature_70;
	}

	public void setTemperature_70(CountTimeBean temperature_70) {
		this.temperature_70 = temperature_70;
	}

	public long getStatDate() {
		return statDate;
	}

	public void setStatDate(long statDate) {
		this.statDate = statDate;
	}

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getPreSection() {
		return preSection;
	}

	public void setPreSection(String preSection) {
		this.preSection = preSection;
	}

	public int getTmpTime() {
		return tmpTime;
	}

	public void setTmpTime(int tmpTime) {
		this.tmpTime = tmpTime;
	}
	
	/****
	 * 计算进气温度区间值，进气温度属于总线数据，5分钟上报一次
	 * @param airTemperture
	 */
	public void account(long airTemperture,long gpsTime,boolean accState,boolean isLastRow){
		
		int time = 0;
		if(lastGpsTime > 0){ // 第一条统计数据，则赋默认值10s
			time = (int)(gpsTime - lastGpsTime);
			if(time <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
				time = time/1000;
			}else{
				time = 0;
			}
		}

		airTemperture = airTemperture + ExcConstants.COOLLIQUID;
		
		String section = this.getAirTempertureSection(airTemperture);
		
		boolean isChange = false;
		
		if (!"".equals(section)&&!section.equals(this.getPreSection())){
			isChange = true;
		}
		
		if (accState){
			tmpTime += time;
			if (isChange){
				addupAirTemperture(section,tmpTime,isChange);
				this.setPreSection(section);
				tmpTime = 0;
			}
			lastGpsTime = gpsTime;
		}else{
			tmpTime += time;
			if (!isChange){
				addupAirTemperture(this.preSection,tmpTime,isChange);
				this.setPreSection("");
				tmpTime = 0;
			}
			lastGpsTime = -1;
		}
		
		//最后一行特殊处理
		if (isLastRow&&accState){
			if (tmpTime>0){
				addupAirTemperture(this.getPreSection(),tmpTime,false);
				tmpTime = 0;
			}
		}
	}
	
	private String getAirTempertureSection(long airTemperture){
		if(airTemperture >= -40&&airTemperture <= 0){
			return "0";
		}else if(airTemperture > 0&&airTemperture <= 10){
			return "0_10";
		}else if(airTemperture > 10&&airTemperture <= 20){
			return "10_20";
		}else if(airTemperture > 20&&airTemperture <= 25){
			return "20_25";
		}else if(airTemperture > 25&&airTemperture <= 30){
			return "25_30";
		}else if(airTemperture > 30&&airTemperture <= 35){
			return "30_35";
		}else if(airTemperture > 35&&airTemperture <= 40){
			return "35_40";
		}else if(airTemperture > 40&&airTemperture <= 45){
			return "40_45";
		}else if(airTemperture > 45&&airTemperture <= 50){
			return "45_50";
		}else if(airTemperture > 50&&airTemperture <= 60){
			return "50_60";
		}else if(airTemperture > 60&&airTemperture <= 70){
			return "60_70";
		}else if(airTemperture > 70){
			return "70";
		}else{
			return "";
		}
	}
	
	private void addupAirTemperture(String section,int time,boolean isChange){
		if("0".equals(section)){
			if (isChange){
				temperature_0.addNum(1);
			}
			temperature_0.addTime(time);
		}else if("0_10".equals(section)){
			if (isChange){
				temperature_0_10.addNum(1);
			}
			temperature_0_10.addTime(time);
		}else if("10_20".equals(section)){
			if (isChange){
				temperature_10_20.addNum(1);
			}
			temperature_10_20.addTime(time);
		}else if("20_25".equals(section)){
			if (isChange){
				temperature_20_25.addNum(1);
			}
			temperature_20_25.addTime(time);
		}else if("25_30".equals(section)){
			if (isChange){
				temperature_25_30.addNum(1);
			}
			temperature_25_30.addTime(time);
		}else if("30_35".equals(section)){
			if (isChange){
				temperature_30_35.addNum(1);
			}
			temperature_30_35.addTime(time);
		}else if("35_40".equals(section)){
			if (isChange){
				temperature_35_40.addNum(1);
			}
			temperature_35_40.addTime(time);
		}else if("40_45".equals(section)){
			if (isChange){
				temperature_40_45.addNum(1);
			}
			temperature_40_45.addTime(time);
		}else if("45_50".equals(section)){
			if (isChange){
				temperature_45_50.addNum(1);
			}
			temperature_45_50.addTime(time);
		}else if("50_60".equals(section)){
			if (isChange){
				temperature_50_60.addNum(1);
			}
			temperature_50_60.addTime(time);
		}else if("60_70".equals(section)){
			if (isChange){
				temperature_60_70.addNum(1);
			}
			temperature_60_70.addTime(time);
		}else if("70".equals(section)){
			if (isChange){
				temperature_70.addNum(1);
			}
			temperature_70.addTime(time);
		}
	}
}
