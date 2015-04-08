package com.ctfo.trackservice.service;

import java.io.File;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.dao.ThreadPool;
import com.ctfo.trackservice.model.AirTempertureBean;
import com.ctfo.trackservice.model.CoolLiquidtemBean;
import com.ctfo.trackservice.model.GasPressureBean;
import com.ctfo.trackservice.model.OilPressureBean;
import com.ctfo.trackservice.model.RotateSpeedDay;
import com.ctfo.trackservice.model.SpeeddistDay;
import com.ctfo.trackservice.model.StatusCode;
import com.ctfo.trackservice.model.VehicleMessageBean;
import com.ctfo.trackservice.model.VoltagedistDay;
import com.ctfo.trackservice.util.ExcConstants;


@SuppressWarnings("unused")
public class VechicleReportService {
	private static final Logger logger = LoggerFactory.getLogger(VechicleReportService.class);

	private ArrayBlockingQueue<File> vPacket = new ArrayBlockingQueue<File>(
			100000);
	// 机油压力分析Bean
	private OilPressureBean oilPressureBean = null;
	// 冷却液温度分析Bean
	private CoolLiquidtemBean coolLiquidtemBean = null;
	// 进气压力分析Bean
	private GasPressureBean gsPressureBean = null;
	// 车速分析Bean
	private SpeeddistDay speeddistDay = null;
	// 转速分析Bean
	private RotateSpeedDay rotateSpeedDay = null;
	// 蓄电池电压Bean
	private VoltagedistDay voltagedistDay = null;
	// 进气温度Bean
	private AirTempertureBean airTempertureBean = null;

	private StatusCode statusCode = null;

	// 统计时间
	private long utc = 0;
	
	private String vid = "";
	
	public VechicleReportService(long utc,String vid){
		this.utc = utc;
		this.vid = vid;
		initAnalyser();
	}

	public void initAnalyser() {
		// 燃油压力
		oilPressureBean = new OilPressureBean(this.utc+12*60*60*1000,vid);
		// 冷却液温度
		coolLiquidtemBean = new CoolLiquidtemBean(this.utc+12*60*60*1000,vid);
		// 进气压力
		gsPressureBean = new GasPressureBean(this.utc+12*60*60*1000,vid);
		// 车速
		speeddistDay = new SpeeddistDay(this.utc+12*60*60*1000,vid);
		// 转速
		rotateSpeedDay = new RotateSpeedDay(this.utc+12*60*60*1000,vid);
		// 蓄电池电压
		voltagedistDay = new VoltagedistDay(this.utc+12*60*60*1000,vid);
		// 进气温度
		airTempertureBean = new AirTempertureBean(this.utc+12*60*60*1000,vid);

		// 查询蓄电池电压类型
		statusCode = ThreadPool.statusMap.get(vid);

	}
	
	/**
	 * 值区间分布分析
	 * @param trackBean
	 * @param isLastRow
	 * 
	 * 从车辆点火到第一次有值为第一次值区间命中时长
	 * 从上次区间命中开始至本次区间命中之间的时长即为本次区间命中时长
	 * 从最后一次值区间命中至车辆熄火，为最后一次值区间命中时长
	 */


	public void executeAnalyser(VehicleMessageBean trackBean,boolean isLastRow){
		try{
		    long gpsTime = trackBean.getUtc();
		    boolean accState = trackBean.isAccState();
		    long rpm = trackBean.getRpm();
		    //点火状态和转速组合判断，防止车辆不支持点火状态时判断失误
		    if (trackBean.getRpm()*ExcConstants.RPMUNIT>100&&trackBean.isAccState()){
		    	accState = true;
		    }else{
		    	accState = false;
		    }
		   
			statisRotatDay(gpsTime,trackBean.getRpm(),trackBean.getTorque());//转速
			statisSpeedDay(gpsTime,trackBean.getSpeed());//车速
			
			 //进气压力 需参考点火状态一同判断
			statisGasPressureDay(gpsTime,trackBean.getGsPres(),accState);//进气压力
			
			//以下四个值不用参考点火状态，只要有轨迹数据即开始算
			statisVoltageDay(gpsTime,trackBean.getVolgate(),accState,isLastRow);//蓄电池电压
			statisOilPressureDay(gpsTime,trackBean.getOilPres(),accState,isLastRow);//机油压力
			statisCoolLiquidtemDay(gpsTime,trackBean.getCoolLiquidtem(),accState,isLastRow);//冷却液温度
			statisAirTemperture(gpsTime,trackBean.getAirTemperture(),accState,isLastRow);// 进气温度
			
		}catch(Exception ex){
			ex.printStackTrace();
		}
	}

	/**
	 * 车速分布统计
	 * 
	 * @param line
	 */
	private void statisSpeedDay(long gpsTime,long speed) {
		// 统计车速次数
		speeddistDay.analyseSpeed(speeddistDay, speed, gpsTime);

	}

	/**
	 * 转速分布统计
	 * 
	 * @param line
	 */
	private void statisRotatDay(long gpsTime,long rpm,String torque) {
		// 统计转速次数
		rotateSpeedDay.analyseRotateLineByLine(rotateSpeedDay, rpm,
				torque, gpsTime);
		// }
	}

	/**
	 * 蓄电池电压分布统计
	 * 
	 * @param line
	 */
	private void statisVoltageDay(long gpsTime,long voltage,boolean accState,boolean isLastRow) {
		String vType = "24V";// 默认蓄电池电压为24V
		if (statusCode != null
				&& statusCode.getExtVoltage() != null
				&& statusCode.getExtVoltage().getVoltageType()
						.equals("12V")) {
			vType = "12V";
		}

		// 统计蓄电池电压
		voltagedistDay.analyseVoltage(voltage, vType,
				gpsTime,accState,isLastRow);
		
		if (voltage>0){
		voltagedistDay.setMax((int) voltage);
		voltagedistDay.setMin((int) voltage);
		}
		

	}

	/**
	 * 机油压力分布统计
	 * 
	 * @param line
	 */
	private void statisOilPressureDay(long gpsTime,long oilPres,boolean accState,boolean isLastRow) {
		if (oilPres>0){	
			oilPressureBean.setMax(oilPres);
			oilPressureBean.setMin(oilPres);
		}
		oilPressureBean.account(oilPres, gpsTime,accState,isLastRow);
	}

	/**
	 * 冷却液温度分布统计
	 * 
	 * @param line
	 */
	private void statisCoolLiquidtemDay(long gpsTime,long coolLi,boolean accState,boolean isLastRow) {
		if (coolLi>0){
			coolLiquidtemBean.setMax(coolLi);
			coolLiquidtemBean.setMin(coolLi);
		}
		coolLiquidtemBean.account(coolLi, gpsTime,accState,isLastRow);
	}

	/**
	 * 进气压力分布统计
	 * 
	 * @param line
	 */
	private void statisGasPressureDay(long gpsTime,long gsPres,boolean accState) {
		if (gsPres>0) {
			gsPressureBean.setMax(gsPres);
			gsPressureBean.setMin(gsPres);
		}
		gsPressureBean.account(gsPres, gpsTime,accState);
		
	}

	/*****
	 * 进气温度分布统计
	 * 
	 * @param cols
	 */
	private void statisAirTemperture(long gpsTime,long airTemperture,boolean accState,boolean isLastRow) {
		if (airTemperture>=0){
			airTempertureBean.setMax(airTemperture);
			airTempertureBean.setMin(airTemperture);
		}
		airTempertureBean.account(airTemperture, gpsTime,accState,isLastRow);
	}

	public OilPressureBean getOilPressureBean() {
		return oilPressureBean;
	}

	public CoolLiquidtemBean getCoolLiquidtemBean() {
		return coolLiquidtemBean;
	}

	public GasPressureBean getGsPressureBean() {
		return gsPressureBean;
	}

	public SpeeddistDay getSpeeddistDay() {
		return speeddistDay;
	}

	public RotateSpeedDay getRotateSpeedDay() {
		return rotateSpeedDay;
	}

	public VoltagedistDay getVoltagedistDay() {
		return voltagedistDay;
	}

	public AirTempertureBean getAirTempertureBean() {
		return airTempertureBean;
	}

}
