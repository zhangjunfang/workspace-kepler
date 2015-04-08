package com.ctfo.trackservice.model;

import com.ctfo.trackservice.util.ExcConstants;


public class OilPressureBean extends ExcBaseBean {
	
	private long statDate;
	
	private String vid;
	
	private CountTimeBean pressure_0 = null;

	private CountTimeBean pressure_0_50 = null;

	private CountTimeBean pressure_50_100 = null;

	private CountTimeBean pressure_100_150 = null;
	
	private CountTimeBean pressure_150_200 = null;
	
	private CountTimeBean pressure_200_250 = null;
	
	private CountTimeBean pressure_250_300 = null;

	private CountTimeBean pressure_300_350 = null;
	
	private CountTimeBean pressure_350_400 = null;
	
	private CountTimeBean pressure_400_450 = null;
	
	private CountTimeBean pressure_450_500 = null;

	private CountTimeBean pressure_500_550 = null;
	
	private CountTimeBean pressure_550_600 = null;
	
	private CountTimeBean pressure_600_650 = null;
	
	private CountTimeBean pressure_650_700 = null;
	
	private CountTimeBean pressure_700_750 = null;
	
	private CountTimeBean pressure_750_800 = null;
	
	private CountTimeBean pressure_800 = null;
	
	private long lastGpsTime = 0;
	
	private String preSection;
	
	private int tmpTime;
	
	public OilPressureBean(){
		pressure_0 = new CountTimeBean();

		pressure_0_50 = new CountTimeBean();

		pressure_50_100 = new CountTimeBean();

		pressure_100_150 = new CountTimeBean();
		
		pressure_150_200 = new CountTimeBean();
		
		pressure_200_250 = new CountTimeBean();
		
		pressure_250_300 = new CountTimeBean();

		pressure_300_350 = new CountTimeBean();
		
		pressure_350_400 = new CountTimeBean();
		
		pressure_400_450 = new CountTimeBean();
		
		pressure_450_500 = new CountTimeBean();

		pressure_500_550 = new CountTimeBean();
		
		pressure_550_600 = new CountTimeBean();
		
		pressure_600_650 = new CountTimeBean();
		
		pressure_650_700 = new CountTimeBean();
		
		pressure_700_750 = new CountTimeBean();
		
		pressure_750_800 = new CountTimeBean();
		
		pressure_800 = new CountTimeBean();
	}
	
	public OilPressureBean(long utc,String vid){
		this.statDate = utc;
		this.vid = vid;
		pressure_0 = new CountTimeBean();

		pressure_0_50 = new CountTimeBean();

		pressure_50_100 = new CountTimeBean();

		pressure_100_150 = new CountTimeBean();
		
		pressure_150_200 = new CountTimeBean();
		
		pressure_200_250 = new CountTimeBean();
		
		pressure_250_300 = new CountTimeBean();

		pressure_300_350 = new CountTimeBean();
		
		pressure_350_400 = new CountTimeBean();
		
		pressure_400_450 = new CountTimeBean();
		
		pressure_450_500 = new CountTimeBean();

		pressure_500_550 = new CountTimeBean();
		
		pressure_550_600 = new CountTimeBean();
		
		pressure_600_650 = new CountTimeBean();
		
		pressure_650_700 = new CountTimeBean();
		
		pressure_700_750 = new CountTimeBean();
		
		pressure_750_800 = new CountTimeBean();
		
		pressure_800 = new CountTimeBean();
	}
	
	/***
	 * 统计区间次数,时间
	 * @param num
	 */
	public void account(long oilPress,long gpsTime){
		oilPress = oilPress * ExcConstants.OILPRESSURE;
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
		if(oilPress == 0){
			pressure_0.addNum(1);
			pressure_0.addTime(time);
		}else if( oilPress > 0 && oilPress < 50){
			pressure_0_50.addNum(1);
			pressure_0_50.addTime(time);
		}else if(oilPress >= 50 && oilPress <100){
			pressure_50_100.addNum(1);
			pressure_50_100.addTime(time);
		}else if(oilPress >= 100 && oilPress <150){
			pressure_100_150.addNum(1);
			pressure_100_150.addTime(time);
		}else if(oilPress >= 150 && oilPress < 200){
			pressure_150_200.addNum(1);
			pressure_150_200.addTime(time);
		}else if( oilPress >= 200 && oilPress < 250){
			pressure_200_250.addNum(1);
			pressure_200_250.addTime(time);
		}else if(oilPress >= 250 && oilPress <300){
			pressure_250_300.addNum(1);
			pressure_250_300.addTime(time);
		}else if(oilPress >= 300 && oilPress <350){
			pressure_300_350.addNum(1);
			pressure_300_350.addTime(time);
		}else if(oilPress >= 350 && oilPress < 400){
			pressure_350_400.addNum(1);
			pressure_350_400.addTime(time);
		}else if(oilPress >= 400 && oilPress < 450){
			pressure_400_450.addNum(1);
			pressure_400_450.addTime(time);
		}else if( oilPress >= 450 && oilPress < 500){
			pressure_450_500.addNum(1);
			pressure_450_500.addTime(time);
		}else if(oilPress >= 500 && oilPress < 550){
			pressure_500_550.addNum(1);
			pressure_500_550.addTime(time);
		}else if(oilPress >= 550 && oilPress < 600){
			pressure_550_600.addNum(1);
			pressure_550_600.addTime(time);
		}else if(oilPress >= 600 && oilPress < 650){
			pressure_600_650.addNum(1);
			pressure_600_650.addTime(time);
		}else if(oilPress >= 650 && oilPress < 700){
			pressure_650_700.addNum(1);
			pressure_650_700.addTime(time);
		}else if(oilPress >= 700 && oilPress < 750){
			pressure_700_750.addNum(1);
			pressure_700_750.addTime(time);
		}else if(oilPress >= 750 && oilPress < 800){
			pressure_750_800.addNum(1);
			pressure_750_800.addTime(time);
		}else{
			pressure_800.addNum(1);
			pressure_800.addTime(time);
		}
	}

	public CountTimeBean getPressure_0() {
		return pressure_0;
	}

	public CountTimeBean getPressure_0_50() {
		return pressure_0_50;
	}

	public CountTimeBean getPressure_50_100() {
		return pressure_50_100;
	}

	public CountTimeBean getPressure_100_150() {
		return pressure_100_150;
	}

	public CountTimeBean getPressure_150_200() {
		return pressure_150_200;
	}

	public CountTimeBean getPressure_200_250() {
		return pressure_200_250;
	}

	public CountTimeBean getPressure_250_300() {
		return pressure_250_300;
	}

	public CountTimeBean getPressure_300_350() {
		return pressure_300_350;
	}

	public CountTimeBean getPressure_350_400() {
		return pressure_350_400;
	}

	public CountTimeBean getPressure_400_450() {
		return pressure_400_450;
	}

	public CountTimeBean getPressure_450_500() {
		return pressure_450_500;
	}

	public CountTimeBean getPressure_500_550() {
		return pressure_500_550;
	}

	public CountTimeBean getPressure_550_600() {
		return pressure_550_600;
	}

	public CountTimeBean getPressure_600_650() {
		return pressure_600_650;
	}

	public CountTimeBean getPressure_650_700() {
		return pressure_650_700;
	}

	public CountTimeBean getPressure_700_750() {
		return pressure_700_750;
	}

	public CountTimeBean getPressure_750_800() {
		return pressure_750_800;
	}

	public CountTimeBean getPressure_800() {
		return pressure_800;
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
	 * 统计区间次数,时间
	 * @param num
	 */
	public void account(long oilPress,long gpsTime,boolean accState,boolean isLastRow){
		oilPress = oilPress * ExcConstants.OILPRESSURE;
		int time = 0;
		if(lastGpsTime > 0){ // 第一条统计数据，则赋默认值10s
			time = (int)(gpsTime - lastGpsTime);
			if(time <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
				time = time/1000;
			}else{
				time = 0;
			}
		}
		
		String section = this.getOilPressureSection(oilPress);
		
		boolean isChange = false;
		
		if (!"".equals(section)&&!section.equals(this.getPreSection())){
			isChange = true;
		}
		
		if (accState){
			tmpTime += time;
			if (isChange){
				addupOilPressure(section,tmpTime,isChange);
				this.setPreSection(section);
				tmpTime = 0;
			}
			lastGpsTime = gpsTime;
		}else{
			tmpTime += time;
			if (!isChange){
				addupOilPressure(section,tmpTime,isChange);
				this.setPreSection("");
				tmpTime = 0;
			}
			lastGpsTime = -1L;
		}
		
		//最后一行特殊处理
		if (isLastRow&&accState){
			if (tmpTime>0){
				addupOilPressure(this.getPreSection(),tmpTime,false);
				tmpTime = 0;
			}
		}
	}
	
	private String getOilPressureSection(long oilPress){
		if(oilPress == 0){
			return "0";
		}else if( oilPress > 0 && oilPress < 50){
			return "0_50";
		}else if(oilPress >= 50 && oilPress <100){
			return "50_100";
		}else if(oilPress >= 100 && oilPress <150){
			return "100_150";
		}else if(oilPress >= 150 && oilPress < 200){
			return "150_200";
		}else if( oilPress >= 200 && oilPress < 250){
			return "200_250";
		}else if(oilPress >= 250 && oilPress <300){
			return "250_300";
		}else if(oilPress >= 300 && oilPress <350){
			return "300_350";
		}else if(oilPress >= 350 && oilPress < 400){
			return "350_400";
		}else if(oilPress >= 400 && oilPress < 450){
			return "400_450";
		}else if( oilPress >= 450 && oilPress < 500){
			return "450_500";
		}else if(oilPress >= 500 && oilPress < 550){
			return "500_550";
		}else if(oilPress >= 550 && oilPress < 600){
			return "550_600";
		}else if(oilPress >= 600 && oilPress < 650){
			return "600_650";
		}else if(oilPress >= 650 && oilPress < 700){
			return "650_700";
		}else if(oilPress >= 700 && oilPress < 750){
			return "700_750";
		}else if(oilPress >= 750 && oilPress < 800){
			return "750_800";
		}else if (oilPress >= 800){
			return "800";
		}else{
			return "";
		}
	}
	
	private void addupOilPressure(String section,int time,boolean isChange){
		if("0".equals(section)){
			if (isChange){
				pressure_0.addNum(1);
			}
			pressure_0.addTime(time);
		}else if( "0_50".equals(section)){
			if (isChange){
				pressure_0_50.addNum(1);
			}
			pressure_0_50.addTime(time);
		}else if("50_100".equals(section)){
			if (isChange){
				pressure_50_100.addNum(1);
			}
			pressure_50_100.addTime(time);
		}else if("100_150".equals(section)){
			if (isChange){
				pressure_100_150.addNum(1);
			}
			pressure_100_150.addTime(time);
		}else if("150_200".equals(section)){
			if (isChange){
				pressure_150_200.addNum(1);
			}
			pressure_150_200.addTime(time);
		}else if( "200_250".equals(section)){
			if (isChange){
				pressure_200_250.addNum(1);
			}
			pressure_200_250.addTime(time);
		}else if("250_300".equals(section)){
			if (isChange){
				pressure_250_300.addNum(1);
			}
			pressure_250_300.addTime(time);
		}else if("300_350".equals(section)){
			if (isChange){
				pressure_300_350.addNum(1);
			}
			pressure_300_350.addTime(time);
		}else if("350_400".equals(section)){
			if (isChange){
				pressure_350_400.addNum(1);
			}
			pressure_350_400.addTime(time);
		}else if("400_450".equals(section)){
			if (isChange){
				pressure_400_450.addNum(1);
			}
			pressure_400_450.addTime(time);
		}else if( "450_500".equals(section)){
			if (isChange){
				pressure_450_500.addNum(1);
			}
			pressure_450_500.addTime(time);
		}else if("500_550".equals(section)){
			if (isChange){
				pressure_500_550.addNum(1);
			}
			pressure_500_550.addTime(time);
		}else if("550_600".equals(section)){
			pressure_550_600.addNum(1);
			pressure_550_600.addTime(time);
		}else if("600_650".equals(section)){
			if (isChange){
				pressure_600_650.addNum(1);
			}
			pressure_600_650.addTime(time);
		}else if("650_700".equals(section)){
			if (isChange){
				pressure_650_700.addNum(1);
			}
			pressure_650_700.addTime(time);
		}else if("700_750".equals(section)){
			if (isChange){
				pressure_700_750.addNum(1);
			}
			pressure_700_750.addTime(time);
		}else if("750_800".equals(section)){
			if (isChange){
				pressure_750_800.addNum(1);
			}
			pressure_750_800.addTime(time);
		}else if ("800".equals(section)){
			if (isChange){
				pressure_800.addNum(1);
			}
			pressure_800.addTime(time);
		}
	}
}

