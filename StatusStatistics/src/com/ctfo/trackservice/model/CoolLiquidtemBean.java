package com.ctfo.trackservice.model;

import com.ctfo.trackservice.util.ExcConstants;



public class CoolLiquidtemBean extends ExcBaseBean {
	private long statDate;
	
	@SuppressWarnings("unused")
	private long allTime=0;
	
	private String vid;
	private long lastGpsTime = 0;
	private CountTimeBean collLiquidtem_0 = null;
	
	private CountTimeBean collLiquidtem_0_5 = null;
	
	private CountTimeBean collLiquidtem_5_10 = null;
	
	private CountTimeBean collLiquidtem_10_15 = null;
	
	private CountTimeBean collLiquidtem_15_20 = null;
	
	private CountTimeBean collLiquidtem_20_25 = null;
	
	private CountTimeBean collLiquidtem_25_30 = null;
	private CountTimeBean collLiquidtem_30_35 = null;
	
	private CountTimeBean collLiquidtem_35_40 = null;
	
	private CountTimeBean collLiquidtem_40_45 = null;
	
	private CountTimeBean collLiquidtem_45_50 = null;
	
	private CountTimeBean collLiquidtem_50_55 = null;
	
	private CountTimeBean collLiquidtem_55_60 = null;
	
	private CountTimeBean collLiquidtem_60_65 = null;
	
	private CountTimeBean collLiquidtem_65_70 = null;
	
	private CountTimeBean collLiquidtem_70_75 = null;
	
	private CountTimeBean collLiquidtem_75_80 = null;
	
	private CountTimeBean collLiquidtem_80_85 = null;
	
	private CountTimeBean collLiquidtem_85_90 = null;
	
	private CountTimeBean collLiquidtem_90_95 = null;
	
	private CountTimeBean collLiquidtem_95_100 = null;
	
	private CountTimeBean collLiquidtem_100_105 = null;
	
	private CountTimeBean collLiquidtem_105_110 = null;
	
	private CountTimeBean collLiquidtem_110_115 = null;
	
	private CountTimeBean collLiquidtem_115_120 = null;
	
	private CountTimeBean collLiquidtem_120 = null;
	
	private String preSection;
	
	private int tmpTime;
	
	public CoolLiquidtemBean(){
		collLiquidtem_0 = new CountTimeBean();
		
		collLiquidtem_0_5 = new CountTimeBean();
		
		collLiquidtem_5_10 = new CountTimeBean();
		
		collLiquidtem_10_15 = new CountTimeBean();
		
		collLiquidtem_15_20 = new CountTimeBean();
		
		collLiquidtem_20_25 = new CountTimeBean();
		
		collLiquidtem_25_30 = new CountTimeBean();

		collLiquidtem_30_35 = new CountTimeBean();
		
		collLiquidtem_35_40 = new CountTimeBean();
		
		collLiquidtem_40_45 = new CountTimeBean();
		
		collLiquidtem_45_50 = new CountTimeBean();
		
		collLiquidtem_50_55 = new CountTimeBean();
		
		collLiquidtem_55_60 = new CountTimeBean();
		
		collLiquidtem_60_65 = new CountTimeBean();
		
		collLiquidtem_65_70 = new CountTimeBean();
		
		collLiquidtem_70_75 = new CountTimeBean();
		
		collLiquidtem_75_80 = new CountTimeBean();
		
		collLiquidtem_80_85 = new CountTimeBean();
		
		collLiquidtem_85_90 = new CountTimeBean();
		
		collLiquidtem_90_95 = new CountTimeBean();
		
		collLiquidtem_95_100 = new CountTimeBean();
		
		collLiquidtem_100_105 = new CountTimeBean();
		
		collLiquidtem_105_110 = new CountTimeBean();
		
		collLiquidtem_110_115 = new CountTimeBean();
		
		collLiquidtem_115_120 = new CountTimeBean();
		
		collLiquidtem_120 = new CountTimeBean();
	}
	
	public CoolLiquidtemBean(long utc,String vid){
		this.statDate = utc;
		this.vid = vid;
		collLiquidtem_0 = new CountTimeBean();
		
		collLiquidtem_0_5 = new CountTimeBean();
		
		collLiquidtem_5_10 = new CountTimeBean();
		
		collLiquidtem_10_15 = new CountTimeBean();
		
		collLiquidtem_15_20 = new CountTimeBean();
		
		collLiquidtem_20_25 = new CountTimeBean();
		
		collLiquidtem_25_30 = new CountTimeBean();

		collLiquidtem_30_35 = new CountTimeBean();
		
		collLiquidtem_35_40 = new CountTimeBean();
		
		collLiquidtem_40_45 = new CountTimeBean();
		
		collLiquidtem_45_50 = new CountTimeBean();
		
		collLiquidtem_50_55 = new CountTimeBean();
		
		collLiquidtem_55_60 = new CountTimeBean();
		
		collLiquidtem_60_65 = new CountTimeBean();
		
		collLiquidtem_65_70 = new CountTimeBean();
		
		collLiquidtem_70_75 = new CountTimeBean();
		
		collLiquidtem_75_80 = new CountTimeBean();
		
		collLiquidtem_80_85 = new CountTimeBean();
		
		collLiquidtem_85_90 = new CountTimeBean();
		
		collLiquidtem_90_95 = new CountTimeBean();
		
		collLiquidtem_95_100 = new CountTimeBean();
		
		collLiquidtem_100_105 = new CountTimeBean();
		
		collLiquidtem_105_110 = new CountTimeBean();
		
		collLiquidtem_110_115 = new CountTimeBean();
		
		collLiquidtem_115_120 = new CountTimeBean();
		
		collLiquidtem_120 = new CountTimeBean();
	}
	
	/***
	 * 统计冷却液温度 区间次数，时间
	 * @param coolLiquidtem
	 */
	public void account(long coolLiquidtem,long gpsTime){
		
		coolLiquidtem = coolLiquidtem + ExcConstants.COOLLIQUID;
		
		int time = 0;
		if(lastGpsTime > 0 ){ // 第一条统计数据，则赋默认值10s
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
		if(coolLiquidtem <= 0 ){
			collLiquidtem_0.addNum(1);
			collLiquidtem_0.addTime(time);
		}else if (coolLiquidtem > 0 && coolLiquidtem < 5){
			collLiquidtem_0_5.addNum(1);
			collLiquidtem_0_5.addTime(time);
		}else if (coolLiquidtem >= 5 && coolLiquidtem < 10){
			collLiquidtem_5_10.addNum(1);
			collLiquidtem_5_10.addTime(time);
		}else if (coolLiquidtem >= 10 && coolLiquidtem < 15){
			collLiquidtem_10_15.addNum(1);
			collLiquidtem_10_15.addTime(time);
		}else if (coolLiquidtem >= 15 && coolLiquidtem < 20){
			collLiquidtem_15_20.addNum(1);
			collLiquidtem_15_20.addTime(time);
		}else if (coolLiquidtem >= 20 && coolLiquidtem < 25){
			collLiquidtem_20_25.addNum(1);
			collLiquidtem_20_25.addTime(time);
		}else if (coolLiquidtem >= 25 && coolLiquidtem < 30){
			collLiquidtem_25_30.addNum(1);
			collLiquidtem_25_30.addTime(time);
		}else if (coolLiquidtem >= 30 && coolLiquidtem < 35){
			collLiquidtem_30_35.addNum(1);
			collLiquidtem_30_35.addTime(time);
		}else if (coolLiquidtem >= 35 && coolLiquidtem < 40){
			collLiquidtem_35_40.addNum(1);
			collLiquidtem_35_40.addTime(time);
		}else if (coolLiquidtem >= 40 && coolLiquidtem < 45){
			collLiquidtem_40_45.addNum(1);
			collLiquidtem_40_45.addTime(time);
		}else if (coolLiquidtem >= 45 && coolLiquidtem < 50){
			collLiquidtem_45_50.addNum(1);
			collLiquidtem_45_50.addTime(time);
		}else if (coolLiquidtem >= 50 && coolLiquidtem < 55){
			collLiquidtem_50_55.addNum(1);
			collLiquidtem_50_55.addTime(time);
		}else if (coolLiquidtem >= 55 && coolLiquidtem < 60){
			collLiquidtem_55_60.addNum(1);
			collLiquidtem_55_60.addTime(time);
		}else if (coolLiquidtem >= 60 && coolLiquidtem < 65){
			collLiquidtem_60_65.addNum(1);
			collLiquidtem_60_65.addTime(time);
		}else if (coolLiquidtem >= 65 && coolLiquidtem < 70){
			collLiquidtem_65_70.addNum(1);
			collLiquidtem_65_70.addTime(time);
		}else if (coolLiquidtem >= 70 && coolLiquidtem < 75){
			collLiquidtem_70_75.addNum(1);
			collLiquidtem_70_75.addTime(time);
		}else if (coolLiquidtem >= 75 && coolLiquidtem < 80){
			collLiquidtem_75_80.addNum(1);
			collLiquidtem_75_80.addTime(time);
		}else if (coolLiquidtem >= 80 && coolLiquidtem < 85){
			collLiquidtem_80_85.addNum(1);
			collLiquidtem_80_85.addTime(time);
		}else if (coolLiquidtem >= 85 && coolLiquidtem < 90){
			collLiquidtem_85_90.addNum(1);
			collLiquidtem_85_90.addTime(time);
		}else if (coolLiquidtem >= 90 && coolLiquidtem < 95){
			collLiquidtem_90_95.addNum(1);
			collLiquidtem_90_95.addTime(time);
		}else if (coolLiquidtem >= 95 && coolLiquidtem < 100){
			collLiquidtem_95_100.addNum(1);
			collLiquidtem_95_100.addTime(time);
		}else if (coolLiquidtem >= 100 && coolLiquidtem < 105){
			collLiquidtem_100_105.addNum(1);
			collLiquidtem_100_105.addTime(time);
		}else if (coolLiquidtem >= 105 && coolLiquidtem < 110){
			collLiquidtem_105_110.addNum(1);
			collLiquidtem_105_110.addTime(time);
		}else if (coolLiquidtem >= 110 && coolLiquidtem < 115){
			collLiquidtem_110_115.addNum(1);
			collLiquidtem_110_115.addTime(time);
		}else if (coolLiquidtem >= 115 && coolLiquidtem < 120){
			collLiquidtem_115_120.addNum(1);
			collLiquidtem_115_120.addTime(time);
		}else if (coolLiquidtem >= 120){
			collLiquidtem_120.addNum(1);
			collLiquidtem_120.addTime(time);
		}
	}

	public CountTimeBean getCollLiquidtem_0() {
		return collLiquidtem_0;
	}

	public CountTimeBean getCollLiquidtem_0_5() {
		return collLiquidtem_0_5;
	}

	public CountTimeBean getCollLiquidtem_5_10() {
		return collLiquidtem_5_10;
	}

	public CountTimeBean getCollLiquidtem_10_15() {
		return collLiquidtem_10_15;
	}

	public CountTimeBean getCollLiquidtem_15_20() {
		return collLiquidtem_15_20;
	}

	public CountTimeBean getCollLiquidtem_20_25() {
		return collLiquidtem_20_25;
	}

	public CountTimeBean getCollLiquidtem_25_30() {
		return collLiquidtem_25_30;
	}

	public CountTimeBean getCollLiquidtem_30_35() {
		return collLiquidtem_30_35;
	}

	public CountTimeBean getCollLiquidtem_35_40() {
		return collLiquidtem_35_40;
	}

	public CountTimeBean getCollLiquidtem_40_45() {
		return collLiquidtem_40_45;
	}

	public CountTimeBean getCollLiquidtem_45_50() {
		return collLiquidtem_45_50;
	}

	public CountTimeBean getCollLiquidtem_50_55() {
		return collLiquidtem_50_55;
	}

	public CountTimeBean getCollLiquidtem_55_60() {
		return collLiquidtem_55_60;
	}

	public CountTimeBean getCollLiquidtem_60_65() {
		return collLiquidtem_60_65;
	}

	public CountTimeBean getCollLiquidtem_65_70() {
		return collLiquidtem_65_70;
	}

	public CountTimeBean getCollLiquidtem_70_75() {
		return collLiquidtem_70_75;
	}

	public CountTimeBean getCollLiquidtem_75_80() {
		return collLiquidtem_75_80;
	}

	public CountTimeBean getCollLiquidtem_80_85() {
		return collLiquidtem_80_85;
	}

	public CountTimeBean getCollLiquidtem_85_90() {
		return collLiquidtem_85_90;
	}

	public CountTimeBean getCollLiquidtem_90_95() {
		return collLiquidtem_90_95;
	}

	public CountTimeBean getCollLiquidtem_95_100() {
		return collLiquidtem_95_100;
	}

	public CountTimeBean getCollLiquidtem_100_105() {
		return collLiquidtem_100_105;
	}

	public CountTimeBean getCollLiquidtem_105_110() {
		return collLiquidtem_105_110;
	}

	public CountTimeBean getCollLiquidtem_110_115() {
		return collLiquidtem_110_115;
	}

	public CountTimeBean getCollLiquidtem_115_120() {
		return collLiquidtem_115_120;
	}

	public CountTimeBean getCollLiquidtem_120() {
		return collLiquidtem_120;
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
	
	/***
	 * 统计冷却液温度 区间次数，时间
	 * @param coolLiquidtem
	 */
	public void account(long coolLiquidtem,long gpsTime,boolean accState,boolean isLastRow){
		int time = 0;
		if(lastGpsTime > 0 ){ // 第一条统计数据，则赋默认值10s
			time = (int)(gpsTime - lastGpsTime);
			if(time <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
				time = time/1000;
			}else{
				time = 0;
			}
		}
		
		coolLiquidtem = coolLiquidtem + ExcConstants.COOLLIQUID;
		
		String section = this.getCoolLiquidtemSection(coolLiquidtem);
		
		boolean isChange = false;
		
		if (!"".equals(section)&&!section.equals(this.getPreSection())){
			isChange = true;
		}
		
		if (accState){
			tmpTime += time;
			if (isChange){
				addupCoolLiquidtem(section,tmpTime,isChange);
				this.setPreSection(section);
				tmpTime = 0;
			}
			lastGpsTime = gpsTime;
		}else{
			tmpTime += time;
			if (isChange){
				//addupCoolLiquidtem(section,tmpTime,isChange);
				this.setPreSection("");
				tmpTime = 0;
			}
			lastGpsTime = -1;
		}
		
		//最后一行特殊处理
		if (isLastRow&&accState){
			if (tmpTime>0){
				addupCoolLiquidtem(this.getPreSection(),tmpTime,false);
				tmpTime = 0;
			}
		}
//		
//		lastGpsTime = gpsTime;
		
	}
	
	
	private String getCoolLiquidtemSection(long coolLiquidtem){
		if(coolLiquidtem>=-40&&coolLiquidtem <= 0 ){
			return "0";
		}else if (coolLiquidtem > 0 && coolLiquidtem < 5){
			return "0_5";
		}else if (coolLiquidtem >= 5 && coolLiquidtem < 10){
			return "5_10";
		}else if (coolLiquidtem >= 10 && coolLiquidtem < 15){
			return "10_15";
		}else if (coolLiquidtem >= 15 && coolLiquidtem < 20){
			return "15_20";
		}else if (coolLiquidtem >= 20 && coolLiquidtem < 25){
			return "20_25";
		}else if (coolLiquidtem >= 25 && coolLiquidtem < 30){
			return "25_30";
		}else if (coolLiquidtem >= 30 && coolLiquidtem < 35){
			return "30_35";
		}else if (coolLiquidtem >= 35 && coolLiquidtem < 40){
			return "35_40";
		}else if (coolLiquidtem >= 40 && coolLiquidtem < 45){
			return "40_45";
		}else if (coolLiquidtem >= 45 && coolLiquidtem < 50){
			return "45_50";
		}else if (coolLiquidtem >= 50 && coolLiquidtem < 55){
			return "50_55";
		}else if (coolLiquidtem >= 55 && coolLiquidtem < 60){
			return "55_60";
		}else if (coolLiquidtem >= 60 && coolLiquidtem < 65){
			return "60_65";
		}else if (coolLiquidtem >= 65 && coolLiquidtem < 70){
			return "65_70";
		}else if (coolLiquidtem >= 70 && coolLiquidtem < 75){
			return "70_75";
		}else if (coolLiquidtem >= 75 && coolLiquidtem < 80){
			return "75_80";
		}else if (coolLiquidtem >= 80 && coolLiquidtem < 85){
			return "80_85";
		}else if (coolLiquidtem >= 85 && coolLiquidtem < 90){
			return "85_90";
		}else if (coolLiquidtem >= 90 && coolLiquidtem < 95){
			return "90_95";
		}else if (coolLiquidtem >= 95 && coolLiquidtem < 100){
			return "95_100";
		}else if (coolLiquidtem >= 100 && coolLiquidtem < 105){
			return "100_105";
		}else if (coolLiquidtem >= 105 && coolLiquidtem < 110){
			return "105_110";
		}else if (coolLiquidtem >= 110 && coolLiquidtem < 115){
			return "110_115";
		}else if (coolLiquidtem >= 115 && coolLiquidtem < 120){
			return "115_120";
		}else if (coolLiquidtem >= 120){
			return "120";
		}else{
			return "";
		}
	}
	
	
	private void addupCoolLiquidtem(String section,int time,boolean isChange){
		if("0".equals(section)){
			if (isChange){
				collLiquidtem_0.addNum(1);
			}
			collLiquidtem_0.addTime(time);
		}else if ("0_5".equals(section)){
			if (isChange){
				collLiquidtem_0_5.addNum(1);
			}
			collLiquidtem_0_5.addTime(time);
		}else if ("5_10".equals(section)){
			if (isChange){
				collLiquidtem_5_10.addNum(1);
			}
			collLiquidtem_5_10.addTime(time);
		}else if ("10_15".equals(section)){
			if (isChange){
				collLiquidtem_10_15.addNum(1);
			}
			collLiquidtem_10_15.addTime(time);
		}else if ("15_20".equals(section)){
			if (isChange){
				collLiquidtem_15_20.addNum(1);
			}
			collLiquidtem_15_20.addTime(time);
		}else if ("20_25".equals(section)){
			if (isChange){
				collLiquidtem_20_25.addNum(1);
			}
			collLiquidtem_20_25.addTime(time);
		}else if ("25_30".equals(section)){
			if (isChange){
				collLiquidtem_25_30.addNum(1);
			}
			collLiquidtem_25_30.addTime(time);
		}else if ("30_35".equals(section)){
			if (isChange){
				collLiquidtem_30_35.addNum(1);
			}
			collLiquidtem_30_35.addTime(time);
		}else if ("35_40".equals(section)){
			if (isChange){
				collLiquidtem_35_40.addNum(1);
			}
			collLiquidtem_35_40.addTime(time);
		}else if ("40_45".equals(section)){
			if (isChange){
				collLiquidtem_40_45.addNum(1);
			}
			collLiquidtem_40_45.addTime(time);
		}else if ("45_50".equals(section)){
			if (isChange){
				collLiquidtem_45_50.addNum(1);
			}
			collLiquidtem_45_50.addTime(time);
		}else if ("50_55".equals(section)){
			if (isChange){
				collLiquidtem_50_55.addNum(1);
			}
			collLiquidtem_50_55.addTime(time);
		}else if ("55_60".equals(section)){
			if (isChange){
				collLiquidtem_55_60.addNum(1);
			}
			collLiquidtem_55_60.addTime(time);
		}else if ("60_65".equals(section)){
			if (isChange){
				collLiquidtem_60_65.addNum(1);
			}
			collLiquidtem_60_65.addTime(time);
		}else if ("65_70".equals(section)){
			if (isChange){
				collLiquidtem_65_70.addNum(1);
			}
			collLiquidtem_65_70.addTime(time);
		}else if ("70_75".equals(section)){
			if (isChange){
				collLiquidtem_70_75.addNum(1);
			}
			collLiquidtem_70_75.addTime(time);
		}else if ("75_80".equals(section)){
			if (isChange){
				collLiquidtem_75_80.addNum(1);
			}
			collLiquidtem_75_80.addTime(time);
		}else if ("80_85".equals(section)){
			if (isChange){
				collLiquidtem_80_85.addNum(1);
			}
			collLiquidtem_80_85.addTime(time);
		}else if ("85_90".equals(section)){
			if (isChange){
				collLiquidtem_85_90.addNum(1);
			}
			collLiquidtem_85_90.addTime(time);
		}else if ("90_95".equals(section)){
			if (isChange){
				collLiquidtem_90_95.addNum(1);
			}
			collLiquidtem_90_95.addTime(time);
		}else if ("95_100".equals(section)){
			if (isChange){
				collLiquidtem_95_100.addNum(1);
			}
			collLiquidtem_95_100.addTime(time);
		}else if ("100_105".equals(section)){
			if (isChange){
				collLiquidtem_100_105.addNum(1);
			}
			collLiquidtem_100_105.addTime(time);
		}else if ("105_110".equals(section)){
			if (isChange){
				collLiquidtem_105_110.addNum(1);
			}
			collLiquidtem_105_110.addTime(time);
		}else if ("110_115".equals(section)){
			if (isChange){
				collLiquidtem_110_115.addNum(1);
			}
			collLiquidtem_110_115.addTime(time);
		}else if ("115_120".equals(section)){
			if (isChange){
				collLiquidtem_115_120.addNum(1);
			}
			collLiquidtem_115_120.addTime(time);
		}else if ("120".equals(section)){
			if (isChange){
				collLiquidtem_120.addNum(1);
			}
			collLiquidtem_120.addTime(time);
		}
	}
	
	
}
