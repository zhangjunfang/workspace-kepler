package com.caits.analysisserver.bean;



public class VoltagedistDay extends ExcBaseBean{

	private String autoId;

	private String vid;
	
	private long statDate;

	private String vehicleNo;

	private String vinCode;

	private Long statTime=new Long(0);

	private Long voltage0=new Long(0);

	private Long voltage0Time=new Long(0);

	private Long voltage020=new Long(0);

	private Long voltage020Time=new Long(0);

	private Long voltage20211=new Long(0);

	private Long voltage20211Time=new Long(0);

	private Long voltage20212=new Long(0);

	private Long voltage20212Time=new Long(0);

	private Long voltage21221=new Long(0);

	private Long voltage21221Time=new Long(0);

	private Long voltage21222=new Long(0);

	private Long voltage21222Time=new Long(0);

	private Long voltage22231=new Long(0);

	private Long voltage22231Time=new Long(0);

	private Long voltage22232=new Long(0);

	private Long voltage22232Time=new Long(0);

	private Long voltage23241=new Long(0);

	private Long voltage23241Time=new Long(0);

	private Long voltage23242=new Long(0);

	private Long voltage23242Time=new Long(0);

	private Long voltage24251=new Long(0);

	private Long voltage24251Time=new Long(0);

	private Long voltage24252=new Long(0);

	private Long voltage24252Time=new Long(0);

	private Long voltage25261=new Long(0);

	private Long voltage25261Time=new Long(0);

	private Long voltage25262=new Long(0);

	private Long voltage25262Time=new Long(0);

	private Long voltage26271=new Long(0);

	private Long voltage26271Time=new Long(0);

	private Long voltage26272=new Long(0);

	private Long voltage26272Time=new Long(0);

	private Long voltage27281=new Long(0);

	private Long voltage27281Time=new Long(0);

	private Long voltage27282=new Long(0);

	private Long voltage27282Time=new Long(0);

	private Long voltage28291=new Long(0);

	private Long voltage28291Time=new Long(0);

	private Long voltage28292=new Long(0);

	private Long voltage28292Time=new Long(0);

	private Long voltage29Max=new Long(0);

	private Long voltage29MaxTime=new Long(0);

	private Long sumtime=new Long(0);

	private Long sumcount=new Long(0);

	private Long voltage0121=new Long(0);

	private Long voltage0121Time=new Long(0);

	private Long voltage0122=new Long(0);

	private Long voltage0122Time=new Long(0);

	private Long voltage12131=new Long(0);

	private Long voltage12131Time=new Long(0);

	private Long voltage12132=new Long(0);

	private Long voltage12132Time=new Long(0);

	private Long voltage13141=new Long(0);

	private Long voltage13141Time=new Long(0);

	private Long voltage13142=new Long(0);

	private Long voltage13142Time=new Long(0);

	private Long voltage141=new Long(0);

	private Long voltage141Time=new Long(0);

	private Long voltage14Max=new Long(0);

	private Long voltage14MaxTime=new Long(0);

	private Long lastGpsTime = 0L;
	
	private String preSection;
	
	private int tmpTime;
	
	public VoltagedistDay(){
		
	}
	
	public VoltagedistDay(long utc,String vid){
		this.statDate = utc;
		this.vid = vid;
	}
	
	public String getAutoId() {
		return autoId;
	}

	public void setAutoId(String autoId) {
		this.autoId = autoId;
	}

	public String getVid() {
		return vid;
	}

	public void setVid(String vid) {
		this.vid = vid;
	}

	public String getVehicleNo() {
		return vehicleNo;
	}

	public void setVehicleNo(String vehicleNo) {
		this.vehicleNo = vehicleNo;
	}

	public String getVinCode() {
		return vinCode;
	}

	public void setVinCode(String vinCode) {
		this.vinCode = vinCode;
	}

	public Long getStatTime() {
		return statTime;
	}

	public void setStatTime(Long statTime) {
		this.statTime = statTime;
	}

	public Long getVoltage0() {
		return voltage0;
	}

	public void setVoltage0(Long voltage0) {
		this.voltage0 = voltage0;
	}

	public Long getVoltage0Time() {
		return voltage0Time;
	}

	public void setVoltage0Time(Long voltage0Time) {
		this.voltage0Time = voltage0Time;
	}

	public Long getVoltage020() {
		return voltage020;
	}

	public void setVoltage020(Long voltage020) {
		this.voltage020 = voltage020;
	}

	public Long getVoltage020Time() {
		return voltage020Time;
	}

	public void setVoltage020Time(Long voltage020Time) {
		this.voltage020Time = voltage020Time;
	}

	public Long getVoltage20211() {
		return voltage20211;
	}

	public void setVoltage20211(Long voltage20211) {
		this.voltage20211 = voltage20211;
	}

	public Long getVoltage20211Time() {
		return voltage20211Time;
	}

	public void setVoltage20211Time(Long voltage20211Time) {
		this.voltage20211Time = voltage20211Time;
	}

	public Long getVoltage20212() {
		return voltage20212;
	}

	public void setVoltage20212(Long voltage20212) {
		this.voltage20212 = voltage20212;
	}

	public Long getVoltage20212Time() {
		return voltage20212Time;
	}

	public void setVoltage20212Time(Long voltage20212Time) {
		this.voltage20212Time = voltage20212Time;
	}

	public Long getVoltage21221() {
		return voltage21221;
	}

	public void setVoltage21221(Long voltage21221) {
		this.voltage21221 = voltage21221;
	}

	public Long getVoltage21221Time() {
		return voltage21221Time;
	}

	public void setVoltage21221Time(Long voltage21221Time) {
		this.voltage21221Time = voltage21221Time;
	}

	public Long getVoltage21222() {
		return voltage21222;
	}

	public void setVoltage21222(Long voltage21222) {
		this.voltage21222 = voltage21222;
	}

	public Long getVoltage21222Time() {
		return voltage21222Time;
	}

	public void setVoltage21222Time(Long voltage21222Time) {
		this.voltage21222Time = voltage21222Time;
	}

	public Long getVoltage22231() {
		return voltage22231;
	}

	public void setVoltage22231(Long voltage22231) {
		this.voltage22231 = voltage22231;
	}

	public Long getVoltage22231Time() {
		return voltage22231Time;
	}

	public void setVoltage22231Time(Long voltage22231Time) {
		this.voltage22231Time = voltage22231Time;
	}

	public Long getVoltage22232() {
		return voltage22232;
	}

	public void setVoltage22232(Long voltage22232) {
		this.voltage22232 = voltage22232;
	}

	public Long getVoltage22232Time() {
		return voltage22232Time;
	}

	public void setVoltage22232Time(Long voltage22232Time) {
		this.voltage22232Time = voltage22232Time;
	}

	public Long getVoltage23241() {
		return voltage23241;
	}

	public void setVoltage23241(Long voltage23241) {
		this.voltage23241 = voltage23241;
	}

	public Long getVoltage23241Time() {
		return voltage23241Time;
	}

	public void setVoltage23241Time(Long voltage23241Time) {
		this.voltage23241Time = voltage23241Time;
	}

	public Long getVoltage23242() {
		return voltage23242;
	}

	public void setVoltage23242(Long voltage23242) {
		this.voltage23242 = voltage23242;
	}

	public Long getVoltage23242Time() {
		return voltage23242Time;
	}

	public void setVoltage23242Time(Long voltage23242Time) {
		this.voltage23242Time = voltage23242Time;
	}

	public Long getVoltage24251() {
		return voltage24251;
	}

	public void setVoltage24251(Long voltage24251) {
		this.voltage24251 = voltage24251;
	}

	public Long getVoltage24251Time() {
		return voltage24251Time;
	}

	public void setVoltage24251Time(Long voltage24251Time) {
		this.voltage24251Time = voltage24251Time;
	}

	public Long getVoltage24252() {
		return voltage24252;
	}

	public void setVoltage24252(Long voltage24252) {
		this.voltage24252 = voltage24252;
	}

	public Long getVoltage24252Time() {
		return voltage24252Time;
	}

	public void setVoltage24252Time(Long voltage24252Time) {
		this.voltage24252Time = voltage24252Time;
	}

	public Long getVoltage25261() {
		return voltage25261;
	}

	public void setVoltage25261(Long voltage25261) {
		this.voltage25261 = voltage25261;
	}

	public Long getVoltage25261Time() {
		return voltage25261Time;
	}

	public void setVoltage25261Time(Long voltage25261Time) {
		this.voltage25261Time = voltage25261Time;
	}

	public Long getVoltage25262() {
		return voltage25262;
	}

	public void setVoltage25262(Long voltage25262) {
		this.voltage25262 = voltage25262;
	}

	public Long getVoltage25262Time() {
		return voltage25262Time;
	}

	public void setVoltage25262Time(Long voltage25262Time) {
		this.voltage25262Time = voltage25262Time;
	}

	public Long getVoltage26271() {
		return voltage26271;
	}

	public void setVoltage26271(Long voltage26271) {
		this.voltage26271 = voltage26271;
	}

	public Long getVoltage26271Time() {
		return voltage26271Time;
	}

	public void setVoltage26271Time(Long voltage26271Time) {
		this.voltage26271Time = voltage26271Time;
	}

	public Long getVoltage26272() {
		return voltage26272;
	}

	public void setVoltage26272(Long voltage26272) {
		this.voltage26272 = voltage26272;
	}

	public Long getVoltage26272Time() {
		return voltage26272Time;
	}

	public void setVoltage26272Time(Long voltage26272Time) {
		this.voltage26272Time = voltage26272Time;
	}

	public Long getVoltage27281() {
		return voltage27281;
	}

	public void setVoltage27281(Long voltage27281) {
		this.voltage27281 = voltage27281;
	}

	public Long getVoltage27281Time() {
		return voltage27281Time;
	}

	public void setVoltage27281Time(Long voltage27281Time) {
		this.voltage27281Time = voltage27281Time;
	}

	public Long getVoltage27282() {
		return voltage27282;
	}

	public void setVoltage27282(Long voltage27282) {
		this.voltage27282 = voltage27282;
	}

	public Long getVoltage27282Time() {
		return voltage27282Time;
	}

	public void setVoltage27282Time(Long voltage27282Time) {
		this.voltage27282Time = voltage27282Time;
	}

	public Long getVoltage28291() {
		return voltage28291;
	}

	public void setVoltage28291(Long voltage28291) {
		this.voltage28291 = voltage28291;
	}

	public Long getVoltage28291Time() {
		return voltage28291Time;
	}

	public void setVoltage28291Time(Long voltage28291Time) {
		this.voltage28291Time = voltage28291Time;
	}

	public Long getVoltage28292() {
		return voltage28292;
	}

	public void setVoltage28292(Long voltage28292) {
		this.voltage28292 = voltage28292;
	}

	public Long getVoltage28292Time() {
		return voltage28292Time;
	}

	public void setVoltage28292Time(Long voltage28292Time) {
		this.voltage28292Time = voltage28292Time;
	}

	public Long getVoltage29Max() {
		return voltage29Max;
	}

	public void setVoltage29Max(Long voltage29Max) {
		this.voltage29Max = voltage29Max;
	}

	public Long getVoltage29MaxTime() {
		return voltage29MaxTime;
	}

	public void setVoltage29MaxTime(Long voltage29MaxTime) {
		this.voltage29MaxTime = voltage29MaxTime;
	}
	
	public Long getSumtime() {
		return sumtime;
	}

	public void setSumtime(Long sumtime) {
		this.sumtime = sumtime;
	}

	public Long getSumcount() {
		return sumcount;
	}

	public void setSumcount(Long sumcount) {
		this.sumcount = sumcount;
	}

	public Long getVoltage0121() {
		return voltage0121;
	}

	public void setVoltage0121(Long voltage0121) {
		this.voltage0121 = voltage0121;
	}

	public Long getVoltage0121Time() {
		return voltage0121Time;
	}

	public void setVoltage0121Time(Long voltage0121Time) {
		this.voltage0121Time = voltage0121Time;
	}

	public Long getVoltage0122() {
		return voltage0122;
	}

	public void setVoltage0122(Long voltage0122) {
		this.voltage0122 = voltage0122;
	}

	public Long getVoltage0122Time() {
		return voltage0122Time;
	}

	public void setVoltage0122Time(Long voltage0122Time) {
		this.voltage0122Time = voltage0122Time;
	}

	public Long getVoltage12131() {
		return voltage12131;
	}

	public void setVoltage12131(Long voltage12131) {
		this.voltage12131 = voltage12131;
	}

	public Long getVoltage12131Time() {
		return voltage12131Time;
	}

	public void setVoltage12131Time(Long voltage12131Time) {
		this.voltage12131Time = voltage12131Time;
	}

	public Long getVoltage12132() {
		return voltage12132;
	}

	public void setVoltage12132(Long voltage12132) {
		this.voltage12132 = voltage12132;
	}

	public Long getVoltage12132Time() {
		return voltage12132Time;
	}

	public void setVoltage12132Time(Long voltage12132Time) {
		this.voltage12132Time = voltage12132Time;
	}

	public Long getVoltage13141() {
		return voltage13141;
	}

	public void setVoltage13141(Long voltage13141) {
		this.voltage13141 = voltage13141;
	}

	public Long getVoltage13141Time() {
		return voltage13141Time;
	}

	public void setVoltage13141Time(Long voltage13141Time) {
		this.voltage13141Time = voltage13141Time;
	}

	public Long getVoltage13142() {
		return voltage13142;
	}

	public void setVoltage13142(Long voltage13142) {
		this.voltage13142 = voltage13142;
	}

	public Long getVoltage13142Time() {
		return voltage13142Time;
	}

	public void setVoltage13142Time(Long voltage13142Time) {
		this.voltage13142Time = voltage13142Time;
	}

	public Long getVoltage141() {
		return voltage141;
	}

	public void setVoltage141(Long voltage141) {
		this.voltage141 = voltage141;
	}

	public Long getVoltage141Time() {
		return voltage141Time;
	}

	public void setVoltage141Time(Long voltage141Time) {
		this.voltage141Time = voltage141Time;
	}

	public Long getVoltage14Max() {
		return voltage14Max;
	}

	public void setVoltage14Max(Long voltage14Max) {
		this.voltage14Max = voltage14Max;
	}

	public Long getVoltage14MaxTime() {
		return voltage14MaxTime;
	}

	public void setVoltage14MaxTime(Long voltage14MaxTime) {
		this.voltage14MaxTime = voltage14MaxTime;
	}

	// 根据区间号 统计蓄电池电压 次数 1:存在; 0:不存
	public int getVoltageFlag(int seqId, Long vol) {
		int result = 0;
		double voltage=0.0;
		if (vol > 0) {
			// 因为电压单位是0.1V
			voltage = vol*1.0 / 10;
		}		
		if ((seqId == 0 && voltage == 0)
				|| (seqId == 1 && 0 < voltage && voltage < 20)
				|| (seqId == 2 && 20 <= voltage && voltage < 20.5)
				|| (seqId == 3 && 20.5 <= voltage && voltage < 21)
				|| (seqId == 4 && 21 <= voltage && voltage < 21.5)
				|| (seqId == 5 && 21.5 <= voltage && voltage < 22)
				|| (seqId == 6 && 22 <= voltage && voltage < 22.5)
				|| (seqId == 7 && 22.5 <= voltage && voltage < 23)
				|| (seqId == 8 && 23 <= voltage && voltage < 23.5)
				|| (seqId == 9 && 23.5 <= voltage && voltage < 24)
				|| (seqId == 10 && 24 <= voltage && voltage < 24.5)
				|| (seqId == 11 && 24.5 <= voltage && voltage < 25)
				|| (seqId == 12 && 25 <= voltage && voltage < 25.5)
				|| (seqId == 13 && 25.5 <= voltage && voltage < 26)
				|| (seqId == 14 && 26 <= voltage && voltage < 26.5)
				|| (seqId == 15 && 26.5 <= voltage && voltage < 27)
				|| (seqId == 16 && 27 <= voltage && voltage < 27.5)
				|| (seqId == 17 && 27.5 <= voltage && voltage < 28)
				|| (seqId == 18 && 28 <= voltage && voltage < 28.5)
				|| (seqId == 19 && 28.5 <= voltage && voltage < 29)
				|| (seqId == 20 && voltage >= 29)
				|| (seqId == 21 && 0 < voltage && voltage < 11.5)
				|| (seqId == 22 && 11.5 <= voltage && voltage < 12)
				|| (seqId == 23 && 12 <= voltage && voltage < 12.5)
				|| (seqId == 24 && 12.5 <= voltage && voltage < 13)
				|| (seqId == 25 && 13 <= voltage && voltage < 13.5)
				|| (seqId == 26 && 13.5 <= voltage && voltage < 14)
				|| (seqId == 27 && 14 <= voltage && voltage < 14.4)
				|| (seqId == 28 && voltage >= 14.4)) {
			result = 1;
		}
		return result;
	}

	// 生成分析的VoltagedistDay对象
	public void analyseVoltage(VoltagedistDay vd, Long voltage,String vType,long gpsTime) {
		//System.out.println("---------voltage-------->>:"+voltage);
		int time = 0;
		if(lastGpsTime > 0){ // 统计第一条包含蓄电池电压，则默认时间值为10s
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
		
		if (vType.equals("24V")){
			if (getVoltageFlag(0, voltage) == 1) {
				vd.setVoltage0(vd.getVoltage0() + 1);			
				vd.setVoltage0Time(vd.getVoltage0Time() + time);
			}
			
			if (getVoltageFlag(1, voltage) == 1) {
				vd.setVoltage020(vd.getVoltage020() + 1);	
				vd.setVoltage020Time(vd.getVoltage020Time() + time);
			}
			if (getVoltageFlag(2, voltage) == 1) {
				vd.setVoltage20211(vd.getVoltage20211() + 1);		
				vd.setVoltage20211Time(vd.getVoltage20211Time() + time);
			}
			if (getVoltageFlag(3, voltage) == 1) {
				vd.setVoltage20212(vd.getVoltage20212() + 1);
				vd.setVoltage20212Time(vd.getVoltage20212Time() + time);
			}
			if (getVoltageFlag(4, voltage) == 1) {
				vd.setVoltage21221(vd.getVoltage21221() + 1);
				vd.setVoltage21221Time(vd.getVoltage21221Time() + time);
			}
			if (getVoltageFlag(5, voltage) == 1) {
				vd.setVoltage21222(vd.getVoltage21222() + 1);	
				vd.setVoltage21222Time(vd.getVoltage21222Time() + time);
			}
			if (getVoltageFlag(6, voltage) == 1) {
				vd.setVoltage22231(vd.getVoltage22231() + 1);
				vd.setVoltage22231Time(vd.getVoltage22231Time() + time);
			}
			if (getVoltageFlag(7, voltage) == 1) {
				vd.setVoltage22232(vd.getVoltage22232() + 1);
				vd.setVoltage22232Time(vd.getVoltage22232Time() + time);
			}
			if (getVoltageFlag(8, voltage) == 1) {
				vd.setVoltage23241(vd.getVoltage23241() + 1);
				vd.setVoltage23241Time(vd.getVoltage23241Time() + time);
			}
			if (getVoltageFlag(9, voltage) == 1) {
				vd.setVoltage23242(vd.getVoltage23242() + 1);
				vd.setVoltage23242Time(vd.getVoltage23242Time() + time);
			}
			if (getVoltageFlag(10, voltage) == 1) {
				vd.setVoltage24251(vd.getVoltage24251() + 1);
				vd.setVoltage24251Time(vd.getVoltage24251Time() + time);
			}
			if (getVoltageFlag(11, voltage) == 1) {
				vd.setVoltage24252(vd.getVoltage24252() + 1);
				vd.setVoltage24252Time(vd.getVoltage24252Time() + time);
			}
			if (getVoltageFlag(12, voltage) == 1) {
				vd.setVoltage25261(vd.getVoltage25261() + 1);	
				vd.setVoltage25261Time(vd.getVoltage25261Time() + time);
			}
			if (getVoltageFlag(13, voltage) == 1) {
				vd.setVoltage25262(vd.getVoltage25262() + 1);	
				vd.setVoltage25262Time(vd.getVoltage25262Time() + time);
			}
			if (getVoltageFlag(14, voltage) == 1) {
				vd.setVoltage26271(vd.getVoltage26271() + 1);		
				vd.setVoltage26271Time(vd.getVoltage26271Time() + time);
			}
			if (getVoltageFlag(15, voltage) == 1) {
				vd.setVoltage26272(vd.getVoltage26272() + 1);	
				vd.setVoltage26272Time(vd.getVoltage26272Time() + time);
			}
			if (getVoltageFlag(16, voltage) == 1) { 			
				vd.setVoltage27281(vd.getVoltage27281() + 1);	
				vd.setVoltage27281Time(vd.getVoltage27281Time() + time);
			}
			if (getVoltageFlag(17, voltage) == 1) {
				vd.setVoltage27282(vd.getVoltage27282() + 1);
				vd.setVoltage27282Time(vd.getVoltage27282Time() + time);
			}
			if (getVoltageFlag(18, voltage) == 1) {
				vd.setVoltage28291(vd.getVoltage28291() + 1);
				vd.setVoltage28291Time(vd.getVoltage28291Time() + time);
			}
			if (getVoltageFlag(19, voltage) == 1) {
				vd.setVoltage28292(vd.getVoltage28292() + 1);		
				vd.setVoltage28292Time(vd.getVoltage28292Time() + time);
			}
			if (getVoltageFlag(20, voltage) == 1) {
				vd.setVoltage29Max(vd.getVoltage29Max() + 1);
				vd.setVoltage29MaxTime(vd.getVoltage29MaxTime() + time);
			}
		}else{
			if (getVoltageFlag(21, voltage) == 1) {
				vd.setVoltage0121(vd.getVoltage0121() + 1);	
				vd.setVoltage0121Time(vd.getVoltage0121Time() + time);
			}
			if (getVoltageFlag(22, voltage) == 1) {
				vd.setVoltage0122(vd.getVoltage0122() + 1);
				vd.setVoltage0122Time(vd.getVoltage0122Time() + time);
			}
			if (getVoltageFlag(23, voltage) == 1) {
				vd.setVoltage12131(vd.getVoltage12131() + 1);
				vd.setVoltage12131Time(vd.getVoltage12131Time() + time);
			}
			if (getVoltageFlag(24, voltage) == 1) {
				vd.setVoltage12132(vd.getVoltage12132() + 1);
				vd.setVoltage12132Time(vd.getVoltage12132Time() + time);
			}
			if (getVoltageFlag(25, voltage) == 1) {
				vd.setVoltage13141(vd.getVoltage13141() + 1);
				vd.setVoltage13141Time(vd.getVoltage13141Time() + time);
			}
			if (getVoltageFlag(26, voltage) == 1) {
				vd.setVoltage13142(vd.getVoltage13142() + 1);
				vd.setVoltage13142Time(vd.getVoltage13142Time() + time);
			}
			if (getVoltageFlag(27, voltage) == 1) {
				vd.setVoltage141(vd.getVoltage141() + 1);
				vd.setVoltage141Time(vd.getVoltage141Time() + time);
			}
			if (getVoltageFlag(28, voltage) == 1) {
				vd.setVoltage14Max(vd.getVoltage14Max() + 1);
				vd.setVoltage14MaxTime(vd.getVoltage14MaxTime() + time);
			}
		}
		
		// 当天运行电压总时间
		vd.setSumtime(vd.getSumtime() + time);	
					
		if (voltage>0){
			// 当天运行电压总次数
			vd.setSumcount(vd.getSumcount() + 1);
		}
	}
	
	// 分析的VoltagedistDay对象时间
	public VoltagedistDay analyseVoltageTime(VoltagedistDay vd){		
		    int second = 60 * 5;
			vd.setVoltage0Time(vd.getVoltage0() * second);		
			vd.setVoltage020Time(vd.getVoltage020() * second);		
			vd.setVoltage20211Time(vd.getVoltage20211() * second);		
			vd.setVoltage20212Time(vd.getVoltage20212() * second);	
			vd.setVoltage21221Time(vd.getVoltage21221() * second);		
			vd.setVoltage21222Time(vd.getVoltage21222() * second);		
			vd.setVoltage22231Time(vd.getVoltage22231() * second);		
			vd.setVoltage22232Time(vd.getVoltage22232() * second);	
			vd.setVoltage23241Time(vd.getVoltage23241() * second);		
			vd.setVoltage23242Time(vd.getVoltage23242() * second);		
			vd.setVoltage24251Time(vd.getVoltage24251() * second);		
			vd.setVoltage24252Time(vd.getVoltage24252() * second);		
			vd.setVoltage25261Time(vd.getVoltage25261() * second);		
			vd.setVoltage25262Time(vd.getVoltage25262() * second);		
			vd.setVoltage26271Time(vd.getVoltage26271() * second);	
			vd.setVoltage26272Time(vd.getVoltage26272() * second);		
			vd.setVoltage27281Time(vd.getVoltage27281() * second);		
			vd.setVoltage27282Time(vd.getVoltage27282() * second);		
			vd.setVoltage28291Time(vd.getVoltage28291() * second);		
			vd.setVoltage28292Time(vd.getVoltage28292() * second);		
			vd.setVoltage29MaxTime(vd.getVoltage29Max() * second);	
			vd.setVoltage0121Time(vd.getVoltage0121() * second);		
			vd.setVoltage0122Time(vd.getVoltage0122() * second);		
			vd.setVoltage12131Time(vd.getVoltage12131() * second);		
			vd.setVoltage12132Time(vd.getVoltage12132() * second);		
			vd.setVoltage13141Time(vd.getVoltage13141() * second);		
			vd.setVoltage13142Time(vd.getVoltage13142() * second);		
			vd.setVoltage141Time(vd.getVoltage141() * second);		
			vd.setVoltage14MaxTime(vd.getVoltage14Max() * second);		
			// 当天运行电压总时间
			vd.setSumtime(vd.getSumcount() * second);	
		return vd;
	}

	public long getStatDate() {
		return statDate;
	}

	public void setStatDate(long statDate) {
		this.statDate = statDate;
	}
	
	private String getVoltageSection(String vType,long vol){
		double voltage=0.0;
		if (vol > 0) {
			// 因为电压单位是0.1V
			voltage = vol*1.0 / 10;
		}
		if ("24V".equals(vType)){
			if (vol ==0 ){
				return "24V_0";
			}else if (0 < voltage && voltage < 20){
				return "0_20";
			}else if(20 <= voltage && voltage < 20.5){
				return "20_20.5";
			}else if(20.5 <= voltage && voltage < 21){
				return "20.5_21";
			}else if(21 <= voltage && voltage < 21.5){
				return "21_21.5";
			}else if(21.5 <= voltage && voltage < 22){
				return "21.5_22";
			}else if(22 <= voltage && voltage < 22.5){
				return "22_22.5";
			}else if(22.5 <= voltage && voltage < 23){
				return "22.5_23";
			}else if(23 <= voltage && voltage < 23.5){
				return "23_23.5";
			}else if(23.5 <= voltage && voltage < 24){
				return "23.5_24";
			}else if(24 <= voltage && voltage < 24.5){
				return "24_24.5";
			}else if(24.5 <= voltage && voltage < 25){
				return "24.5_25";
			}else if(25 <= voltage && voltage < 25.5){
				return "25_25.5";
			}else if(25.5 <= voltage && voltage < 26){
				return "25.5_26";
			}else if(26 <= voltage && voltage < 26.5){
				return "26_26.5";
			}else if(26.5 <= voltage && voltage < 27){
				return "26.5_27";
			}else if(27 <= voltage && voltage < 27.5){
				return "27_27.5";
			}else if(27.5 <= voltage && voltage < 28){
				return "27.5_28";
			}else if(28 <= voltage && voltage < 28.5){
				return "28_28.5";
			}else if(28.5 <= voltage && voltage < 29){
				return "28.5_29";
			}else if(voltage >= 29){
				return "29";
			}
		}else{
			if (vol == 0 ){
				return "12V_0";
			}else if(0 < voltage && voltage < 11.5){
				return "0_11.5";
			}else if(11.5 <= voltage && voltage < 12){
				return "11.5_12";
			}else if(12 <= voltage && voltage < 12.5){
				return "12_12.5";
			}else if(12.5 <= voltage && voltage < 13){
				return "12.5_13";
			}else if(13 <= voltage && voltage < 13.5){
				return "13_13.5";
			}else if(13.5 <= voltage && voltage < 14){
				return "13.5_14";
			}else if(14 <= voltage && voltage < 14.4){
				return "14_14.4";
			}else if(voltage >= 14.4){
				return "14.4";
			}
		}
		return "";
	}
	
	// 生成分析的VoltagedistDay对象
	public void analyseVoltage(Long voltage,String vType,long gpsTime,boolean accState,boolean isLastRow) {
		long time = 0L;
		if(lastGpsTime > 0){ // 统计第一条包含蓄电池电压，则默认时间值为10s
			time = gpsTime - lastGpsTime;
			if(time <= ExcConstants.TERNIMAL__REPORT_DATA_LONGEST_INTERVAL_TIME){
				time = time/1000;
			}else{
				time = 0;
			}
		}
		
		String section = this.getVoltageSection(vType,voltage);
		
		boolean isChange = false;
		
		if (!"".equals(section)&&!section.equals(this.getPreSection())){
			isChange = true;
		}
		
		if (accState){
			tmpTime += time;
			if (isChange){
				addupvoltage(section,tmpTime,isChange);
				this.setPreSection(section);
				tmpTime = 0;
			}
			lastGpsTime = gpsTime;
		}else{
			tmpTime += time;
			if (!isChange){
				addupvoltage(this.preSection,tmpTime,isChange);
				this.setPreSection("");
				tmpTime = 0;
			}
			lastGpsTime = -1L;
		}
		
		//最后一行特殊处理
		if (isLastRow&&accState){
			if (tmpTime>0){
				addupvoltage(this.getPreSection(),tmpTime,false);
				tmpTime = 0;
			}
		}

	}
	
	private void addupvoltage(String section,long time,boolean isChange){
			if ("12V_0".equals(section)) {
				if (isChange){
					this.setVoltage0(this.getVoltage0() + 1);
				}
				this.setVoltage0Time(this.getVoltage0Time() + time);
			}else if ("24V_0".equals(section)) {
				if (isChange){
					this.setVoltage0(this.getVoltage0() + 1);
				}		
				this.setVoltage0Time(this.getVoltage0Time() + time);
			}else if ("0_11.5".equals(section)) {
				if (isChange){
					this.setVoltage0121(this.getVoltage0121() + 1);	
				}
				this.setVoltage0121Time(this.getVoltage0121Time() + time);
			}else if ("11.5_12".equals(section)) {
				if (isChange){
					this.setVoltage0122(this.getVoltage0122() + 1);
				}
				this.setVoltage0122Time(this.getVoltage0122Time() + time);
			}else if ("12_12.5".equals(section)) {
				if (isChange){
					this.setVoltage12131(this.getVoltage12131() + 1);
				}
				this.setVoltage12131Time(this.getVoltage12131Time() + time);
			}else if ("12.5_13".equals(section)) {
				if (isChange){
					this.setVoltage12132(this.getVoltage12132() + 1);
				}
				this.setVoltage12132Time(this.getVoltage12132Time() + time);
			}else if ("13_13.5".equals(section)) {
				if (isChange){
					this.setVoltage13141(this.getVoltage13141() + 1);
				}
				this.setVoltage13141Time(this.getVoltage13141Time() + time);
			}else if ("13.5_14".equals(section)) {
				if (isChange){
					this.setVoltage13142(this.getVoltage13142() + 1);
				}
				this.setVoltage13142Time(this.getVoltage13142Time() + time);
			}else if ("14_14.4".equals(section)) {
				if (isChange){
					this.setVoltage141(this.getVoltage141() + 1);
				}
				this.setVoltage141Time(this.getVoltage141Time() + time);
			}else if ("14.4".equals(section)) {
				if (isChange){
					this.setVoltage14Max(this.getVoltage14Max() + 1);
				}
				this.setVoltage14MaxTime(this.getVoltage14MaxTime() + time);
			}else if ("0_20".equals(section)) {
				if (isChange){
					this.setVoltage020(this.getVoltage020() + 1);
				}
				this.setVoltage020Time(this.getVoltage020Time() + time);
			}else if ("20_20.5".equals(section)) {
				if (isChange){
					this.setVoltage20211(this.getVoltage20211() + 1);	
				}
				this.setVoltage20211Time(this.getVoltage20211Time() + time);
			}else if ("20.5_21".equals(section)) {
				if (isChange){
					this.setVoltage20212(this.getVoltage20212() + 1);
				}
				this.setVoltage20212Time(this.getVoltage20212Time() + time);
			}else if ("21_21.5".equals(section)) {
				if (isChange){
					this.setVoltage21221(this.getVoltage21221() + 1);
				}
				this.setVoltage21221Time(this.getVoltage21221Time() + time);
			}else if ("21.5_22".equals(section)) {
				if (isChange){
					this.setVoltage21222(this.getVoltage21222() + 1);
				}
				this.setVoltage21222Time(this.getVoltage21222Time() + time);
			}else if ("22_22.5".equals(section)) {
				if (isChange){
					this.setVoltage22231(this.getVoltage22231() + 1);
				}
				this.setVoltage22231Time(this.getVoltage22231Time() + time);
			}else if ("22.5_23".equals(section)) {
				if (isChange){
					this.setVoltage22232(this.getVoltage22232() + 1);
				}
				this.setVoltage22232Time(this.getVoltage22232Time() + time);
			}else if ("23_23.5".equals(section)) {
				if (isChange){
					this.setVoltage23241(this.getVoltage23241() + 1);
				}
				this.setVoltage23241Time(this.getVoltage23241Time() + time);
			}else if ("23.5_24".equals(section)) {
				if (isChange){
					this.setVoltage23242(this.getVoltage23242() + 1);
				}
				this.setVoltage23242Time(this.getVoltage23242Time() + time);
			}else if ("24_24.5".equals(section)) {
				if (isChange){
					this.setVoltage24251(this.getVoltage24251() + 1);
				}
				this.setVoltage24251Time(this.getVoltage24251Time() + time);
			}else if ("24.5_25".equals(section)) {
				if (isChange){
					this.setVoltage24252(this.getVoltage24252() + 1);
				}
				this.setVoltage24252Time(this.getVoltage24252Time() + time);
			}else if ("25_25.5".equals(section)) {
				if (isChange){
					this.setVoltage25261(this.getVoltage25261() + 1);	
				}
				this.setVoltage25261Time(this.getVoltage25261Time() + time);
			}else if ("25.5_26".equals(section)) {
				if (isChange){
					this.setVoltage25262(this.getVoltage25262() + 1);
				}
				this.setVoltage25262Time(this.getVoltage25262Time() + time);
			}else if ("26_26.5".equals(section)) {
				if (isChange){
					this.setVoltage26271(this.getVoltage26271() + 1);
				}
				this.setVoltage26271Time(this.getVoltage26271Time() + time);
			}else if ("26.5_27".equals(section)) {
				if (isChange){
					this.setVoltage26272(this.getVoltage26272() + 1);	
				}
				this.setVoltage26272Time(this.getVoltage26272Time() + time);
			}else if ("27_27.5".equals(section)) { 
				if (isChange){
					this.setVoltage27281(this.getVoltage27281() + 1);	
				}
				this.setVoltage27281Time(this.getVoltage27281Time() + time);
			}else if ("27.5_28".equals(section)) {
				if (isChange){
					this.setVoltage27282(this.getVoltage27282() + 1);
				}
				this.setVoltage27282Time(this.getVoltage27282Time() + time);
			}else if ("28_28.5".equals(section)) {
				if (isChange){
					this.setVoltage28291(this.getVoltage28291() + 1);
				}
				this.setVoltage28291Time(this.getVoltage28291Time() + time);
			}else if ("28.5_29".equals(section)) {
				if (isChange){
					this.setVoltage28292(this.getVoltage28292() + 1);	
				}
				this.setVoltage28292Time(this.getVoltage28292Time() + time);
			}else if ("29".equals(section)) {
				if (isChange){
					this.setVoltage29Max(this.getVoltage29Max() + 1);
				}
				this.setVoltage29MaxTime(this.getVoltage29MaxTime() + time);
			}

		
		// 当天运行电压总时间
			this.setSumtime(this.getSumtime() + time);	
					
		if (isChange){
			// 当天运行电压总次数
			this.setSumcount(this.getSumcount() + 1);
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
