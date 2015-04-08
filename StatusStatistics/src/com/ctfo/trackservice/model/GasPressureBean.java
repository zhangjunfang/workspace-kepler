package com.ctfo.trackservice.model;

import com.ctfo.trackservice.util.ExcConstants;


public class GasPressureBean extends ExcBaseBean {
	private long statDate;
	
	private String vid;
	private CountTimeBean gsPressure_0 = null;
	
	private CountTimeBean gsPressure_0_50 = null;
	
	private CountTimeBean gsPressure_50_55 = null;
	
	private CountTimeBean gsPressure_55_60 = null;
	
	private CountTimeBean gsPressure_60_65 = null;
	
	private CountTimeBean gsPressure_65_70 = null;
	
	private CountTimeBean gsPressure_70_75 = null;
	
	private CountTimeBean gsPressure_75_80 = null;
	
	private CountTimeBean gsPressure_80_85 = null;
	
	private CountTimeBean gsPressure_85_90 = null;
	
	private CountTimeBean gsPressure_90_95 = null;
	
	private CountTimeBean gsPressure_95_100 = null;
	
	private CountTimeBean gsPressure_100_105 = null;
	
	private CountTimeBean gsPressure_105_110 = null;
	
	private CountTimeBean gsPressure_110_115 = null;
	
	private CountTimeBean gsPressure_115_120 = null;
	
	private CountTimeBean gsPressure_120 = null;
	
	private long lastGpsTime = 0;
	
	private String preSection;
	
	private int tmpTime;
	
	public GasPressureBean(){
		 gsPressure_0 = new CountTimeBean();
			
		 gsPressure_0_50 = new CountTimeBean();
		
		 gsPressure_50_55 = new CountTimeBean();
		
		 gsPressure_55_60 = new CountTimeBean();
		
		 gsPressure_60_65 = new CountTimeBean();
		
		 gsPressure_65_70 = new CountTimeBean();
		
		 gsPressure_70_75 = new CountTimeBean();
		
		 gsPressure_75_80 = new CountTimeBean();
		
		 gsPressure_80_85 = new CountTimeBean();
		
		 gsPressure_85_90 = new CountTimeBean();
		
		 gsPressure_90_95 = new CountTimeBean();
		
		 gsPressure_95_100 = new CountTimeBean();
		
		 gsPressure_100_105 = new CountTimeBean();
		
		 gsPressure_105_110 = new CountTimeBean();
		
		 gsPressure_110_115 = new CountTimeBean();
		
		 gsPressure_115_120 = new CountTimeBean();
		
		 gsPressure_120 = new CountTimeBean();	
	}
	
	public GasPressureBean(long utc,String vid){
		this.statDate = utc;
		this.vid = vid;
		 gsPressure_0 = new CountTimeBean();
			
		 gsPressure_0_50 = new CountTimeBean();
		
		 gsPressure_50_55 = new CountTimeBean();
		
		 gsPressure_55_60 = new CountTimeBean();
		
		 gsPressure_60_65 = new CountTimeBean();
		
		 gsPressure_65_70 = new CountTimeBean();
		
		 gsPressure_70_75 = new CountTimeBean();
		
		 gsPressure_75_80 = new CountTimeBean();
		
		 gsPressure_80_85 = new CountTimeBean();
		
		 gsPressure_85_90 = new CountTimeBean();
		
		 gsPressure_90_95 = new CountTimeBean();
		
		 gsPressure_95_100 = new CountTimeBean();
		
		 gsPressure_100_105 = new CountTimeBean();
		
		 gsPressure_105_110 = new CountTimeBean();
		
		 gsPressure_110_115 = new CountTimeBean();
		
		 gsPressure_115_120 = new CountTimeBean();
		
		 gsPressure_120 = new CountTimeBean();	
	}
	
	/***
	 * 区间统计次数，时间
	 * @param gsPres
	 */
	public void account(long gsPres,long gpsTime){
		float newGsPres = gsPres * ExcConstants.GSPRESSURE;
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
		if(gsPres ==0){
			gsPressure_0.addNum(1);
			gsPressure_0.addTime(time);
		}else if( newGsPres > 0 && newGsPres < 50){
			gsPressure_0_50.addNum(1);
			gsPressure_0_50.addTime(time);
		}else if( newGsPres >= 50 && newGsPres < 55){
			gsPressure_50_55.addNum(1);
			gsPressure_50_55.addTime(time);
		}else if( newGsPres >= 55 && newGsPres < 60){
			gsPressure_55_60.addNum(1);
			gsPressure_55_60.addTime(time);
		}else if( newGsPres >= 60 && newGsPres < 65){
			gsPressure_60_65.addNum(1);
			gsPressure_60_65.addTime(time);
		}else if( newGsPres >= 65 && newGsPres < 70){
			gsPressure_65_70.addNum(1);
			gsPressure_65_70.addTime(time);
		}else if( newGsPres >= 70 && newGsPres < 75){
			gsPressure_70_75.addNum(1);
			gsPressure_70_75.addTime(time);
		}else if( newGsPres >= 75 && newGsPres < 80){
			gsPressure_75_80.addNum(1);
			gsPressure_75_80.addTime(time);
		}else if( newGsPres >= 80 && newGsPres < 85){
			gsPressure_80_85.addNum(1);
			gsPressure_80_85.addTime(time);
		}else if( newGsPres >= 85 && newGsPres < 90){
			gsPressure_85_90.addNum(1);
			gsPressure_85_90.addTime(time);
		}else if( newGsPres >= 90 && newGsPres < 95){
			gsPressure_90_95.addNum(1);
			gsPressure_90_95.addTime(time);
		}else if( newGsPres >= 95 && newGsPres < 100){
			gsPressure_95_100.addNum(1);
			gsPressure_95_100.addTime(time);
		}else if( newGsPres >= 100 && newGsPres < 105){
			gsPressure_100_105.addNum(1);
			gsPressure_100_105.addTime(time);
		}else if( newGsPres >= 105 && newGsPres < 110){
			gsPressure_105_110.addNum(1);
			gsPressure_105_110.addTime(time);
		}else if( newGsPres >= 110 && newGsPres < 115){
			gsPressure_110_115.addNum(1);
			gsPressure_110_115.addTime(time);
		}else if( newGsPres >= 115 && newGsPres < 120){
			gsPressure_115_120.addNum(1);
			gsPressure_115_120.addTime(time);
		}else if( newGsPres >= 120){
			gsPressure_120.addNum(1);
			gsPressure_120.addTime(time);
		}		
	}
	
	/***
	 * 区间统计次数，时间
	 * @param gsPres
	 */
	public void account(long gsPres,long gpsTime,boolean accState){
		
		//计算时间差
		int time = 0;
		if(lastGpsTime > 0){
			time = (int)(gpsTime - lastGpsTime);
			if(time <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
				time = time/1000;
			}else{
				time = 0;
			}
		}
		
		float newGsPres = gsPres * ExcConstants.GSPRESSURE;
		String section = this.getPressSection(newGsPres);
		
		boolean isChange = false;
		
		if (!"".equals(section)&&!section.equals(this.getPreSection())){
			isChange = true;
		}
		
		if (accState){
			tmpTime += time;
			if (isChange){
				addupGasPressure(section,tmpTime,isChange);
				this.setPreSection(section);
				tmpTime = 0;
			}
			lastGpsTime = gpsTime;
		}else{
			tmpTime += time;
			if (!isChange){
				//addupGasPressure(this.getPreSection(),tmpTime,isChange);
				this.setPreSection("");
				tmpTime = 0;
			}
			lastGpsTime = -1;
		}
	}
	
	private void addupGasPressure(String section,int diffTime,boolean isChange){
		if("0".equals(section)){
			/*if (isChange){
				gsPressure_0.addNum(1);
			}
			
			gsPressure_0.addTime(diffTime);*/
		}else if("0_50".equals(section)){
			if (isChange){
				gsPressure_0_50.addNum(1);
			}
			gsPressure_0_50.addTime(diffTime);
		}else if( "50_55".equals(section)){
			if (isChange){
				gsPressure_50_55.addNum(1);
			}

			gsPressure_50_55.addTime(diffTime);
		}else if("55_60".equals(section)){
			if (isChange){
				gsPressure_55_60.addNum(1);
			}

			gsPressure_55_60.addTime(diffTime);
		}else if("60_65".equals(section)){
			if (isChange){
				gsPressure_60_65.addNum(1);
			}

			gsPressure_60_65.addTime(diffTime);
		}else if( "65_70".equals(section)){
			if (isChange){
				gsPressure_65_70.addNum(1);
			}

			gsPressure_65_70.addTime(diffTime);
		}else if("70_75".equals(section)){
			if (isChange){
				gsPressure_70_75.addNum(1);
			}

			gsPressure_70_75.addTime(diffTime);
		}else if( "75_80".equals(section)){
			if (isChange){
				gsPressure_75_80.addNum(1);
			}

			gsPressure_75_80.addTime(diffTime);
		}else if( "80_85".equals(section)){
			if (isChange){
				gsPressure_80_85.addNum(1);
			}

			gsPressure_80_85.addTime(diffTime);
		}else if( "85_90".equals(section)){
			if (isChange){
				gsPressure_85_90.addNum(1);
			}

			gsPressure_85_90.addTime(diffTime);
		}else if( "90_95".equals(section)){
			if (isChange){
				gsPressure_90_95.addNum(1);
			}

			gsPressure_90_95.addTime(diffTime);
		}else if( "95_100".equals(section)){
			if (isChange){
				gsPressure_95_100.addNum(1);
			}

			gsPressure_95_100.addTime(diffTime);
		}else if( "100_105".equals(section)){
			if (isChange){
				gsPressure_100_105.addNum(1);
			}

			gsPressure_100_105.addTime(diffTime);
		}else if( "105_110".equals(section)){
			if (isChange){
				gsPressure_105_110.addNum(1);
			}
			
			gsPressure_105_110.addTime(diffTime);
		}else if( "110_115".equals(section)){
			if (isChange){
				gsPressure_110_115.addNum(1);
			}
			gsPressure_110_115.addTime(diffTime);
		}else if( "115_120".equals(section)){
			if (isChange){
				gsPressure_115_120.addNum(1);
			}
			gsPressure_115_120.addTime(diffTime);
		}else if( "120".equals(section)){
			if (isChange){
				gsPressure_120.addNum(1);
			}
			gsPressure_120.addTime(diffTime);
		}	
	}

	public CountTimeBean getGsPressure_0() {
		return gsPressure_0;
	}

	public CountTimeBean getGsPressure_0_50() {
		return gsPressure_0_50;
	}

	public CountTimeBean getGsPressure_50_55() {
		return gsPressure_50_55;
	}

	public CountTimeBean getGsPressure_55_60() {
		return gsPressure_55_60;
	}

	public CountTimeBean getGsPressure_60_65() {
		return gsPressure_60_65;
	}

	public CountTimeBean getGsPressure_65_70() {
		return gsPressure_65_70;
	}

	public CountTimeBean getGsPressure_70_75() {
		return gsPressure_70_75;
	}

	public CountTimeBean getGsPressure_75_80() {
		return gsPressure_75_80;
	}

	public CountTimeBean getGsPressure_80_85() {
		return gsPressure_80_85;
	}

	public CountTimeBean getGsPressure_85_90() {
		return gsPressure_85_90;
	}

	public CountTimeBean getGsPressure_90_95() {
		return gsPressure_90_95;
	}

	public CountTimeBean getGsPressure_95_100() {
		return gsPressure_95_100;
	}

	public CountTimeBean getGsPressure_100_105() {
		return gsPressure_100_105;
	}

	public CountTimeBean getGsPressure_105_110() {
		return gsPressure_105_110;
	}

	public CountTimeBean getGsPressure_110_115() {
		return gsPressure_110_115;
	}

	public CountTimeBean getGsPressure_115_120() {
		return gsPressure_115_120;
	}

	public CountTimeBean getGsPressure_120() {
		return gsPressure_120;
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
	
	private String getPressSection(float gsPres){
		if( gsPres > 0 && gsPres < 50){
			return "0_50";
		}else if( gsPres >= 50 && gsPres < 55){
			return "50_55";
		}else if( gsPres >= 55 && gsPres < 60){
			return "55_60";
		}else if( gsPres >= 60 && gsPres < 65){
			return "60_65";
		}else if( gsPres >= 65 && gsPres < 70){
			return "65_70";
		}else if( gsPres >= 70 && gsPres < 75){
			return "70_75";
		}else if( gsPres >= 75 && gsPres < 80){
			return "75_80";
		}else if( gsPres >= 80 && gsPres < 85){
			return "80_85";
		}else if( gsPres >= 85 && gsPres < 90){
			return "85_90";
		}else if( gsPres >= 90 && gsPres < 95){
			return "90_95";
		}else if( gsPres >= 95 && gsPres < 100){
			return "95_100";
		}else if( gsPres >= 100 && gsPres < 105){
			return "100_105";
		}else if( gsPres >= 105 && gsPres < 110){
			return "105_110";
		}else if( gsPres >= 110 && gsPres < 115){
			return "110_115";
		}else if( gsPres >= 115 && gsPres < 120){
			return "115_120";
		}else if( gsPres >= 120){
			return "120";
		}else {
			return "0";
		}
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
}

