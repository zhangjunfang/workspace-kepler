package com.caits.analysisserver.addin.kcpt.addin.unifieddispatch;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.Iterator;
import java.util.TreeMap;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.addin.UnifiedFileDispatch;
import com.caits.analysisserver.bean.AirTempertureBean;
import com.caits.analysisserver.bean.CoolLiquidtemBean;
import com.caits.analysisserver.bean.ExcConstants;
import com.caits.analysisserver.bean.GasPressureBean;
import com.caits.analysisserver.bean.OilPressureBean;
import com.caits.analysisserver.bean.RotateSpeedDay;
import com.caits.analysisserver.bean.SpeeddistDay;
import com.caits.analysisserver.bean.StatusCode;
import com.caits.analysisserver.bean.VoltagedistDay;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;
import com.caits.analysisserver.utils.Utils;
import com.ctfo.generator.pk.GeneratorPK;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： VechicleExcStatisticThread <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2011-11-12</td>
 * <td>刘志伟</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000>注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author 刘志伟
 * @since JDK1.6
 * @ Description: 用于
 * 专属统计类
 */

public class VechicleExcStatisticThread extends UnifiedFileDispatch {
	private static final Logger logger = LoggerFactory.getLogger(VechicleExcStatisticThread.class);
	
	private ArrayBlockingQueue<File> vPacket = new ArrayBlockingQueue<File>(100000);
	//机油压力分析Bean
	private OilPressureBean  oilPressureBean = null;
	//冷却液温度分析Bean
	private CoolLiquidtemBean coolLiquidtemBean = null;
	//进气压力分析Bean
	private GasPressureBean gsPressureBean = null;
	//车速分析Bean
	private SpeeddistDay  speeddistDay=null;
	//转速分析Bean
	private RotateSpeedDay rotateSpeedDay=null;	
	//蓄电池电压Bean
	private VoltagedistDay voltagedistDay=null;
	//进气温度Bean
	private AirTempertureBean airTempertureBean = null;
	
	private String sql_saveOilPressureDayStat = null;
	
	private String sql_saveGasPressureDayStat = null;
	
	private String sql_saveCoolLiquidtemDayStat = null;
	
	private String sql_saveSpeeddistDayStat = null;
	
	private String sql_saveRotateDayStat = null;
	
	private String sql_saveVoltageDayStat = null;
	
	private String sql_saveAirTemperture = null;
	
	private Connection dbCon = null;
	
	private TreeMap<Long,String> map = null;
	
	private StatusCode statusCode=null;
	
	private int threadId = 0;
	
	// 统计时间
	private long utc = 0;
	
	public void initAnalyser(){
		
		//机油压力存储
		sql_saveOilPressureDayStat = SQLPool.getinstance().getSql("sql_saveOilPressureDayStat");
		
		//存储进气压力
		sql_saveGasPressureDayStat = SQLPool.getinstance().getSql("sql_saveGasPressureDayStat");
		
		//存储冷却液温度
		sql_saveCoolLiquidtemDayStat = SQLPool.getinstance().getSql("sql_saveCoolLiquidtemDayStat");
		
		//车速分析存储
		sql_saveSpeeddistDayStat= SQLPool.getinstance().getSql("sql_saveSpeeddistDayStat");
		
		//转速分析存储
		sql_saveRotateDayStat = SQLPool.getinstance().getSql("sql_saveRotateDayStat");
		
		//蓄电池电压分布存储
		sql_saveVoltageDayStat= SQLPool.getinstance().getSql("sql_saveVoltageDayStat");
		
		//存储进气温度
		sql_saveAirTemperture = SQLPool.getinstance().getSql("sql_saveAirTemperture");
	}
	
	public void addPacket(File file) {
		try {
			vPacket.put(file);
		} catch (InterruptedException e) {
			logger.error("专属统计线程出错", e);
		}
	}
	
	public void run(){
		logger.info("车辆专属统计线程" + this.threadId + "启动");
		while(true){
			File file = null;
			try {
				file = vPacket.take();
				logger.info( "-------------------Thread Id : "+ this.threadId + ", VechicleExcStatisticThread : " + file.getName());
				readTrackFile(file); // 读取日轨迹文件，按GPS时间排序，滤重
				if(map != null && map.size() > 0){
					String vid = file.getName().replaceAll("\\.txt", "");
					//从连接池获取连接
					dbCon = OracleConnectionPool.getConnection();
					//燃油压力
					oilPressureBean = new OilPressureBean();
					//冷却液温度
					coolLiquidtemBean = new CoolLiquidtemBean();
					//进气压力
					gsPressureBean = new GasPressureBean();
					//车速
					speeddistDay=new SpeeddistDay();
					//转速
					rotateSpeedDay=new RotateSpeedDay();
					//蓄电池电压
					voltagedistDay=new VoltagedistDay();
					//进气温度
					airTempertureBean = new AirTempertureBean();
					
					//查询蓄电池电压类型
					statusCode = AnalysisDBAdapter.statusMap.get(vid);
				
					Iterator<Long> it = map.keySet().iterator();
					while(it.hasNext()){
						Long key = it.next();
						String value = map.get(key);
						String[] cols = value.split(":");
						statisRotatDay(cols);
						statisSpeedDay(cols);
						statisVoltageDay(cols,vid);
						statisOilPressureDay(cols);
						statisCoolLiquidtemDay(cols);
						statisGasPressureDay(cols);
						statisAirTemperture(cols);// 进气温度
					} // End while
					
					 //存储机油压力
					saveOilPressureDay(vid);	
					
					//存储冷却液温度
					saveCoolLiquidtemDayStat(vid);	
					
					//存储进气压力
					saveGasPressure(vid);					
													
					//-----存储车速分析
					//统计车速时间
					saveSpeeddistDay(vid);	
					
					//-----存储转速分析
					//统计转速时间
					saveRotatedistDay(vid);
					
					//-----存储序电压分析
					//统计蓄电压时间
					//voltagedistDay.analyseVoltageTime(voltagedistDay);
					
					saveVoltageDayStat(vid);
					
					//存储进气温度
					saveAirTemperture(vid);
				}
				
			} catch (InterruptedException e) {
				logger.error("专属统计线程数据库操作出错。",e);
			} catch (SQLException e) {
				logger.error("专属统计线程数据库操作出错。" + file.getAbsolutePath(),e);
			}finally{
				if(dbCon != null){
					try {
						dbCon.close();
					} catch (SQLException e) {
						logger.error("连接放回连接池出错.",e);
					}
				}
				if(map != null && map.size() > 0){
					map.clear();
				}
			}
		}// End while
	}	

	private void readTrackFile(File file){
		if(file.exists()){ 
			BufferedReader buf = null;
			String bufLine = null;
			map = new TreeMap<Long,String>();
			
			Long gpstime = null;
			try {
				buf = new BufferedReader(new FileReader(file));
				while((bufLine = buf.readLine()) != null){
					String[] cols = bufLine.split(":");
						String gpsdate = cols[2];
						// 将gpsdate转换成utc格式
						gpstime = CDate.stringConvertUtc(gpsdate);
								
						map.put(gpstime, bufLine);	
				} //End while
			} catch (FileNotFoundException e) {
				logger.error("专属统计线程操作文件出错：" + file.getAbsolutePath(),e);
			} catch (IOException e) {
				logger.error("专属统计线程读文件出错：" + file.getAbsolutePath(),e);
			}finally{
				if(buf != null){
					try {
						buf.close();
					} catch (IOException e) {
						logger.error("关闭文件出错.",e);
					}
				}
			}
		}
	}
	
	/**
	 * 车速分布统计
	 * @param line
	 */
	private void statisSpeedDay(String[] cols){
		//第四位 车速次数
		String spdFrom = cols[24];	//车速来源
		Long speed = (long)Utils.getSpeed(spdFrom, cols);
		String gpsTime = cols[2];
		if(speed >= 50){
			//统计车速次数
			speeddistDay.analyseSpeed(speeddistDay, speed,CDate.stringConvertUtc(gpsTime));
		}
		speeddistDay.setLastGpsTime(CDate.stringConvertUtc(gpsTime));		
	}

	/**
	 * 转速分布统计
	 * @param line
	 */
	private void statisRotatDay(String[] cols){
		//第十四位 转速次数
		if(cols[13] != null && !"".equals(cols[13]) && !"-1".equals(cols[13])){
			long rotateStr = Long.parseLong(cols[13]);
			String torque=(cols[22]==null || "".equals(cols[22])?"0":cols[22]);//第23位  发动机扭矩暂定 
			long gpsTime = CDate.stringConvertUtc(cols[2]);//第2位  gps时间
			//统计转速次数
			rotateSpeedDay.analyseRotateLineByLine(rotateSpeedDay, rotateStr,torque,gpsTime);					
		}
	}
	/**
	 * 蓄电池电压分布统计
	 * @param line
	 */
	private void statisVoltageDay(String[] cols,String vid){
		//第十九位 蓄电池电压
		if(cols[17] != null && !"".equals(cols[17]) && !"-1".equals(cols[17])){
			long voltage = Long.valueOf(cols[17]);
			long gpsTime = CDate.stringConvertUtc(cols[2]);//第2位  gps时间
			String vType = "24V";//默认蓄电池电压为24V
			if (statusCode!=null&&statusCode.getExtVoltage()!=null&&statusCode.getExtVoltage().getVoltageType().equals("12V")){
				vType = "12V";
			}

			//统计蓄电池电压
			
			voltagedistDay.analyseVoltage(voltagedistDay, voltage,vType,gpsTime);	
			voltagedistDay.setMax((int)voltage);
			voltagedistDay.setMin((int)voltage);
		}	
	}
	
	/**
	 * 机油压力分布统计
	 * @param line
	 */
	private void statisOilPressureDay(String[] cols){
		if(cols[20] != null && !cols[20].equals("") && !cols[20].equals("-1")){
			int oilPres = Integer.parseInt(cols[20]);
			long gpsTime = CDate.stringConvertUtc(cols[2]);//第2位  gps时间
			oilPressureBean.setMax(oilPres);
			oilPressureBean.setMin(oilPres);
			oilPressureBean.account(oilPres,gpsTime);
		}
	}
	
	/**
	 * 冷却液温度分布统计
	 * @param line
	 */
	private void statisCoolLiquidtemDay(String[] cols){
		if(cols[16] != null && !cols[16].equals("") && !cols[16].equals("-1")){
			int coolLi = Integer.parseInt(cols[16]);
			long gpsTime = CDate.stringConvertUtc(cols[2]);
			coolLiquidtemBean.setMax(coolLi);
			int tempCoolLi = coolLi + ExcConstants.COOLLIQUID;
			if(tempCoolLi > 0 ){ // 过滤换算后负值
				coolLiquidtemBean.setMin(coolLi);
			}
			coolLiquidtemBean.account(coolLi,gpsTime);
		}
	}
	
	/**
	 * 进气压力分布统计
	 * @param line
	 */
	private void statisGasPressureDay(String[] cols){
		if(cols[21] != null && !cols[21].equals("") && !cols[21].equals("-1")){
			int gsPres = Integer.parseInt(cols[21]);
			long gpsTime = CDate.stringConvertUtc(cols[2]);
			gsPressureBean.setMax(gsPres);
			gsPressureBean.setMin(gsPres);
			gsPressureBean.account(gsPres,gpsTime);
		}
	}
	
	/*****
	 * 进气温度分布统计
	 * @param cols
	 */
	private void statisAirTemperture(String[] cols){
		if(cols[32] != null && !cols[32].equals("") && !cols[32].equals("-1")){
			int airTemperture = Integer.parseInt(cols[32]);
			long gpsTime = CDate.stringConvertUtc(cols[2]);
			airTempertureBean.setMax(airTemperture);
			airTempertureBean.setMin(airTemperture);
			airTempertureBean.account(airTemperture,gpsTime);
		}
	}
		
	/**
	 * 存储机油压力
	 * @throws SQLException 
	 * @throws SQLException 
	 */
	private void saveOilPressureDay(String vid) throws SQLException{	
	
		PreparedStatement stSaveOilPressureDayStat = null;
		try{
			stSaveOilPressureDayStat = dbCon.prepareStatement(sql_saveOilPressureDayStat);
			stSaveOilPressureDayStat.setString(1, vid);
			if(AnalysisDBAdapter.queryVechileInfo(vid) != null){
				stSaveOilPressureDayStat.setString(2, AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo());
				stSaveOilPressureDayStat.setString(3, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode());
			} else {
				stSaveOilPressureDayStat.setString(2, null);
				stSaveOilPressureDayStat.setString(3, null);
			}
			stSaveOilPressureDayStat.setLong(4, this.utc + 12 * 60 * 60 * 1000);
			
			stSaveOilPressureDayStat.setInt(5, oilPressureBean.getPressure_0().getNum());
			stSaveOilPressureDayStat.setInt(6, oilPressureBean.getPressure_0().getTime());
			stSaveOilPressureDayStat.setInt(7, oilPressureBean.getPressure_0_50().getNum());
			stSaveOilPressureDayStat.setInt(8, oilPressureBean.getPressure_0_50().getTime());
			
			stSaveOilPressureDayStat.setInt(9, oilPressureBean.getPressure_50_100().getNum());
			stSaveOilPressureDayStat.setInt(10, oilPressureBean.getPressure_50_100().getTime());
			
			stSaveOilPressureDayStat.setInt(11, oilPressureBean.getPressure_100_150().getNum());
			stSaveOilPressureDayStat.setInt(12, oilPressureBean.getPressure_100_150().getTime());
			
			stSaveOilPressureDayStat.setInt(13, oilPressureBean.getPressure_150_200().getNum());
			stSaveOilPressureDayStat.setInt(14, oilPressureBean.getPressure_150_200().getTime());
			
			stSaveOilPressureDayStat.setInt(15, oilPressureBean.getPressure_200_250().getNum());
			stSaveOilPressureDayStat.setInt(16, oilPressureBean.getPressure_200_250().getTime());
			stSaveOilPressureDayStat.setInt(17, oilPressureBean.getPressure_250_300().getNum());
			stSaveOilPressureDayStat.setInt(18, oilPressureBean.getPressure_250_300().getTime());
			
			stSaveOilPressureDayStat.setInt(19, oilPressureBean.getPressure_300_350().getNum());
			stSaveOilPressureDayStat.setInt(20, oilPressureBean.getPressure_300_350().getTime());
			
			stSaveOilPressureDayStat.setInt(21, oilPressureBean.getPressure_350_400().getNum());
			stSaveOilPressureDayStat.setInt(22, oilPressureBean.getPressure_350_400().getTime());
			
			stSaveOilPressureDayStat.setInt(23, oilPressureBean.getPressure_400_450().getNum());
			stSaveOilPressureDayStat.setInt(24, oilPressureBean.getPressure_400_450().getTime());
			
			stSaveOilPressureDayStat.setInt(25, oilPressureBean.getPressure_450_500().getNum());
			stSaveOilPressureDayStat.setInt(26, oilPressureBean.getPressure_450_500().getTime());
			
			stSaveOilPressureDayStat.setInt(27, oilPressureBean.getPressure_500_550().getNum());
			stSaveOilPressureDayStat.setInt(28, oilPressureBean.getPressure_500_550().getTime());
			
			stSaveOilPressureDayStat.setInt(29, oilPressureBean.getPressure_550_600().getNum());
			stSaveOilPressureDayStat.setInt(30, oilPressureBean.getPressure_550_600().getTime());
			
			stSaveOilPressureDayStat.setInt(31, oilPressureBean.getPressure_600_650().getNum());
			stSaveOilPressureDayStat.setInt(32, oilPressureBean.getPressure_600_650().getTime());
			
			stSaveOilPressureDayStat.setInt(33, oilPressureBean.getPressure_650_700().getNum());
			stSaveOilPressureDayStat.setInt(34, oilPressureBean.getPressure_650_700().getTime());
			
			stSaveOilPressureDayStat.setInt(35, oilPressureBean.getPressure_700_750().getNum());
			stSaveOilPressureDayStat.setInt(36, oilPressureBean.getPressure_700_750().getTime());
			
			stSaveOilPressureDayStat.setInt(37, oilPressureBean.getPressure_750_800().getNum());
			stSaveOilPressureDayStat.setInt(38, oilPressureBean.getPressure_750_800().getTime());
			
			stSaveOilPressureDayStat.setInt(39, oilPressureBean.getPressure_800().getNum());
			stSaveOilPressureDayStat.setInt(40, oilPressureBean.getPressure_800().getTime());
			
			stSaveOilPressureDayStat.setLong(41, oilPressureBean.getMax());
			stSaveOilPressureDayStat.setLong(42, oilPressureBean.getMin());
			
			stSaveOilPressureDayStat.executeUpdate();
		}catch(SQLException e){
			logger.error("统计车辆" + vid + "机油压力出错",e);
		}finally{
			if(stSaveOilPressureDayStat != null){
				stSaveOilPressureDayStat.close();
				stSaveOilPressureDayStat = null;
			}
		}
		
	}
	
	/***
	 * 存储进气压力
	 * @param vid
	 * @throws SQLException
	 */
	private void saveGasPressure(String vid) throws SQLException{		
		PreparedStatement stSaveGasPressureDayStat = null;	
		try{
			stSaveGasPressureDayStat = dbCon.prepareStatement(sql_saveGasPressureDayStat);	
			stSaveGasPressureDayStat.setString(1, vid);
			if(AnalysisDBAdapter.queryVechileInfo(vid) != null){
				stSaveGasPressureDayStat.setString(2, AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo());
				stSaveGasPressureDayStat.setString(3, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode());
			} else {
				stSaveGasPressureDayStat.setString(2, null);
				stSaveGasPressureDayStat.setString(3, null);
			}
			stSaveGasPressureDayStat.setLong(4, this.utc + 12 * 60 * 60 * 1000);
			
			stSaveGasPressureDayStat.setInt(5, gsPressureBean.getGsPressure_0().getNum());
			stSaveGasPressureDayStat.setInt(6, gsPressureBean.getGsPressure_0().getTime());
			
			stSaveGasPressureDayStat.setInt(7, gsPressureBean.getGsPressure_0_50().getNum());
			stSaveGasPressureDayStat.setInt(8, gsPressureBean.getGsPressure_0_50().getTime());
			
			stSaveGasPressureDayStat.setInt(9, gsPressureBean.getGsPressure_50_55().getNum());
			stSaveGasPressureDayStat.setInt(10, gsPressureBean.getGsPressure_50_55().getTime());
			
			stSaveGasPressureDayStat.setInt(11, gsPressureBean.getGsPressure_55_60().getNum());
			stSaveGasPressureDayStat.setInt(12, gsPressureBean.getGsPressure_55_60().getTime());
			
			stSaveGasPressureDayStat.setInt(13, gsPressureBean.getGsPressure_60_65().getNum());
			stSaveGasPressureDayStat.setInt(14, gsPressureBean.getGsPressure_60_65().getTime());
			
			stSaveGasPressureDayStat.setInt(15, gsPressureBean.getGsPressure_65_70().getNum());
			stSaveGasPressureDayStat.setInt(16, gsPressureBean.getGsPressure_65_70().getTime());
			
			stSaveGasPressureDayStat.setInt(17, gsPressureBean.getGsPressure_70_75().getNum());
			stSaveGasPressureDayStat.setInt(18, gsPressureBean.getGsPressure_70_75().getTime());
			
			stSaveGasPressureDayStat.setInt(19, gsPressureBean.getGsPressure_75_80().getNum());
			stSaveGasPressureDayStat.setInt(20, gsPressureBean.getGsPressure_75_80().getTime());
			
			stSaveGasPressureDayStat.setInt(21, gsPressureBean.getGsPressure_80_85().getNum());
			stSaveGasPressureDayStat.setInt(22, gsPressureBean.getGsPressure_80_85().getTime());
			
			stSaveGasPressureDayStat.setInt(23, gsPressureBean.getGsPressure_85_90().getNum());
			stSaveGasPressureDayStat.setInt(24, gsPressureBean.getGsPressure_85_90().getTime());
			
			stSaveGasPressureDayStat.setInt(25, gsPressureBean.getGsPressure_90_95().getNum());
			stSaveGasPressureDayStat.setInt(26, gsPressureBean.getGsPressure_90_95().getTime());
			
			stSaveGasPressureDayStat.setInt(27, gsPressureBean.getGsPressure_95_100().getNum());
			stSaveGasPressureDayStat.setInt(28, gsPressureBean.getGsPressure_95_100().getTime());
			
			stSaveGasPressureDayStat.setInt(29, gsPressureBean.getGsPressure_100_105().getNum());
			stSaveGasPressureDayStat.setInt(30, gsPressureBean.getGsPressure_100_105().getTime());
			
			stSaveGasPressureDayStat.setInt(31, gsPressureBean.getGsPressure_105_110().getNum());
			stSaveGasPressureDayStat.setInt(32, gsPressureBean.getGsPressure_105_110().getTime());
			
			stSaveGasPressureDayStat.setInt(33, gsPressureBean.getGsPressure_110_115().getNum());
			stSaveGasPressureDayStat.setInt(34, gsPressureBean.getGsPressure_110_115().getTime());
			
			stSaveGasPressureDayStat.setInt(35, gsPressureBean.getGsPressure_115_120().getNum());
			stSaveGasPressureDayStat.setInt(36, gsPressureBean.getGsPressure_115_120().getTime());
			
			stSaveGasPressureDayStat.setInt(37, gsPressureBean.getGsPressure_120().getNum());
			stSaveGasPressureDayStat.setInt(38, gsPressureBean.getGsPressure_120().getTime());
			
			stSaveGasPressureDayStat.setLong(39, gsPressureBean.getMax());
			stSaveGasPressureDayStat.setLong(40, gsPressureBean.getMin());
			
			stSaveGasPressureDayStat.executeUpdate();
		}catch(SQLException e){
			logger.error("统计" + vid + " 进气压力出错.",e);
		}finally{
			if(stSaveGasPressureDayStat != null){
				stSaveGasPressureDayStat.close();
			}
		}
	}
	
	/**
	 * 存储冷却液温度
	 * @param vid
	 * @throws SQLException
	 */
	private void saveCoolLiquidtemDayStat(String vid) throws SQLException{		
		PreparedStatement stSaveCoolLiquidtemDayStat = null;
		try{
			stSaveCoolLiquidtemDayStat = dbCon.prepareStatement(sql_saveCoolLiquidtemDayStat);
			stSaveCoolLiquidtemDayStat.setString(1, vid);
			if(AnalysisDBAdapter.queryVechileInfo(vid) != null){
				stSaveCoolLiquidtemDayStat.setString(2, AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo());
				stSaveCoolLiquidtemDayStat.setString(3, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode());
			} else {
				stSaveCoolLiquidtemDayStat.setString(2, null);
				stSaveCoolLiquidtemDayStat.setString(3, null);
			}
			stSaveCoolLiquidtemDayStat.setLong(4, this.utc + 12 * 60 * 60 * 1000);
			stSaveCoolLiquidtemDayStat.setInt(5, coolLiquidtemBean.getCollLiquidtem_0().getNum());
			stSaveCoolLiquidtemDayStat.setInt(6, coolLiquidtemBean.getCollLiquidtem_0().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(7, coolLiquidtemBean.getCollLiquidtem_0_5().getNum());
			stSaveCoolLiquidtemDayStat.setInt(8, coolLiquidtemBean.getCollLiquidtem_0_5().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(9, coolLiquidtemBean.getCollLiquidtem_5_10().getNum());
			stSaveCoolLiquidtemDayStat.setInt(10, coolLiquidtemBean.getCollLiquidtem_5_10().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(11, coolLiquidtemBean.getCollLiquidtem_10_15().getNum());
			stSaveCoolLiquidtemDayStat.setInt(12, coolLiquidtemBean.getCollLiquidtem_10_15().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(13, coolLiquidtemBean.getCollLiquidtem_15_20().getNum());
			stSaveCoolLiquidtemDayStat.setInt(14, coolLiquidtemBean.getCollLiquidtem_15_20().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(15, coolLiquidtemBean.getCollLiquidtem_20_25().getNum());
			stSaveCoolLiquidtemDayStat.setInt(16, coolLiquidtemBean.getCollLiquidtem_20_25().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(17, coolLiquidtemBean.getCollLiquidtem_25_30().getNum());
			stSaveCoolLiquidtemDayStat.setInt(18, coolLiquidtemBean.getCollLiquidtem_25_30().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(19, coolLiquidtemBean.getCollLiquidtem_30_35().getNum());
			stSaveCoolLiquidtemDayStat.setInt(20, coolLiquidtemBean.getCollLiquidtem_30_35().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(21, coolLiquidtemBean.getCollLiquidtem_35_40().getNum());
			stSaveCoolLiquidtemDayStat.setInt(22, coolLiquidtemBean.getCollLiquidtem_35_40().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(23, coolLiquidtemBean.getCollLiquidtem_40_45().getNum());
			stSaveCoolLiquidtemDayStat.setInt(24, coolLiquidtemBean.getCollLiquidtem_40_45().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(25, coolLiquidtemBean.getCollLiquidtem_45_50().getNum());
			stSaveCoolLiquidtemDayStat.setInt(26, coolLiquidtemBean.getCollLiquidtem_45_50().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(27, coolLiquidtemBean.getCollLiquidtem_50_55().getNum());
			stSaveCoolLiquidtemDayStat.setInt(28, coolLiquidtemBean.getCollLiquidtem_50_55().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(29, coolLiquidtemBean.getCollLiquidtem_55_60().getNum());
			stSaveCoolLiquidtemDayStat.setInt(30, coolLiquidtemBean.getCollLiquidtem_55_60().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(31, coolLiquidtemBean.getCollLiquidtem_60_65().getNum());
			stSaveCoolLiquidtemDayStat.setInt(32, coolLiquidtemBean.getCollLiquidtem_60_65().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(33, coolLiquidtemBean.getCollLiquidtem_65_70().getNum());
			stSaveCoolLiquidtemDayStat.setInt(34, coolLiquidtemBean.getCollLiquidtem_65_70().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(35, coolLiquidtemBean.getCollLiquidtem_70_75().getNum());
			stSaveCoolLiquidtemDayStat.setInt(36, coolLiquidtemBean.getCollLiquidtem_70_75().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(37, coolLiquidtemBean.getCollLiquidtem_75_80().getNum());
			stSaveCoolLiquidtemDayStat.setInt(38, coolLiquidtemBean.getCollLiquidtem_75_80().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(39, coolLiquidtemBean.getCollLiquidtem_80_85().getNum());
			stSaveCoolLiquidtemDayStat.setInt(40, coolLiquidtemBean.getCollLiquidtem_80_85().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(41, coolLiquidtemBean.getCollLiquidtem_85_90().getNum());
			stSaveCoolLiquidtemDayStat.setInt(42, coolLiquidtemBean.getCollLiquidtem_85_90().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(43, coolLiquidtemBean.getCollLiquidtem_90_95().getNum());
			stSaveCoolLiquidtemDayStat.setInt(44, coolLiquidtemBean.getCollLiquidtem_90_95().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(45, coolLiquidtemBean.getCollLiquidtem_95_100().getNum());
			stSaveCoolLiquidtemDayStat.setInt(46, coolLiquidtemBean.getCollLiquidtem_95_100().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(47, coolLiquidtemBean.getCollLiquidtem_100_105().getNum());
			stSaveCoolLiquidtemDayStat.setInt(48, coolLiquidtemBean.getCollLiquidtem_100_105().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(49, coolLiquidtemBean.getCollLiquidtem_105_110().getNum());
			stSaveCoolLiquidtemDayStat.setInt(50, coolLiquidtemBean.getCollLiquidtem_105_110().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(51, coolLiquidtemBean.getCollLiquidtem_110_115().getNum());
			stSaveCoolLiquidtemDayStat.setInt(52, coolLiquidtemBean.getCollLiquidtem_110_115().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(53, coolLiquidtemBean.getCollLiquidtem_115_120().getNum());
			stSaveCoolLiquidtemDayStat.setInt(54, coolLiquidtemBean.getCollLiquidtem_115_120().getTime());
			
			stSaveCoolLiquidtemDayStat.setInt(55, coolLiquidtemBean.getCollLiquidtem_120().getNum());
			stSaveCoolLiquidtemDayStat.setInt(56, coolLiquidtemBean.getCollLiquidtem_120().getTime());
			
			stSaveCoolLiquidtemDayStat.setLong(57, coolLiquidtemBean.getMax());
			stSaveCoolLiquidtemDayStat.setLong(58, coolLiquidtemBean.getMin());
			
			stSaveCoolLiquidtemDayStat.executeUpdate();
		}catch(SQLException e){
			logger.error("统计 " + vid + " 冷却液温度出错.",e);
		}finally{
			if(stSaveCoolLiquidtemDayStat != null){
				stSaveCoolLiquidtemDayStat.close();
			}			
		}
	}
	
	/**
	 * 存储车速分析表
	 * @throws SQLException 
	 */
	public void saveSpeeddistDay(String vid) throws SQLException{		
		PreparedStatement stSaveSpeeddistDayStat = null;	
		try{
			stSaveSpeeddistDayStat = dbCon.prepareStatement(sql_saveSpeeddistDayStat);
			stSaveSpeeddistDayStat.setString(1,GeneratorPK.instance().getPKString());
			stSaveSpeeddistDayStat.setString(2, vid);
			if(AnalysisDBAdapter.queryVechileInfo(vid) != null){
				stSaveSpeeddistDayStat.setString(3, AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo());
				stSaveSpeeddistDayStat.setString(4, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode());
			} else {	
				stSaveSpeeddistDayStat.setString(3, null);
				stSaveSpeeddistDayStat.setString(4, null);
			}
			stSaveSpeeddistDayStat.setLong(5, this.utc + 12 * 60 * 60 * 1000);
			
			stSaveSpeeddistDayStat.setLong(6, speeddistDay.getSpeed0());
			stSaveSpeeddistDayStat.setLong(7, speeddistDay.getSpeed0Time());
			
			stSaveSpeeddistDayStat.setLong(8, speeddistDay.getSpeed010());
			stSaveSpeeddistDayStat.setLong(9, speeddistDay.getSpeed010Time());
			
			stSaveSpeeddistDayStat.setLong(10, speeddistDay.getSpeed1020());
			stSaveSpeeddistDayStat.setLong(11, speeddistDay.getSpeed1020Time());
			
			stSaveSpeeddistDayStat.setLong(12, speeddistDay.getSpeed2030());
			stSaveSpeeddistDayStat.setLong(13, speeddistDay.getSpeed2030Time());
			
			stSaveSpeeddistDayStat.setLong(14, speeddistDay.getSpeed3040());
			stSaveSpeeddistDayStat.setLong(15, speeddistDay.getSpeed3040Time());
			
			stSaveSpeeddistDayStat.setLong(16, speeddistDay.getSpeed4050());
			stSaveSpeeddistDayStat.setLong(17, speeddistDay.getSpeed4050Time());
			
			stSaveSpeeddistDayStat.setLong(18, speeddistDay.getSpeed5060());
			stSaveSpeeddistDayStat.setLong(19, speeddistDay.getSpeed5060Time());
			
			stSaveSpeeddistDayStat.setLong(20, speeddistDay.getSpeed6070());
			stSaveSpeeddistDayStat.setLong(21, speeddistDay.getSpeed6070Time());
			
			stSaveSpeeddistDayStat.setLong(22, speeddistDay.getSpeed7080());
			stSaveSpeeddistDayStat.setLong(23, speeddistDay.getSpeed7080Time());
			
			stSaveSpeeddistDayStat.setLong(24, speeddistDay.getSpeed8090());
			stSaveSpeeddistDayStat.setLong(25, speeddistDay.getSpeed8090Time());
			
			stSaveSpeeddistDayStat.setLong(26, speeddistDay.getSpeed90100());
			stSaveSpeeddistDayStat.setLong(27, speeddistDay.getSpeed90100Time());
			
			stSaveSpeeddistDayStat.setLong(28, speeddistDay.getSpeed100110());
			stSaveSpeeddistDayStat.setLong(29, speeddistDay.getSpeed100110Time());
			
			stSaveSpeeddistDayStat.setLong(30, speeddistDay.getSpeed110120());
			stSaveSpeeddistDayStat.setLong(31, speeddistDay.getSpeed110120Time());
			
			stSaveSpeeddistDayStat.setLong(32, speeddistDay.getSpeed120130());
			stSaveSpeeddistDayStat.setLong(33, speeddistDay.getSpeed120130Time());
			
			stSaveSpeeddistDayStat.setLong(34, speeddistDay.getSpeed130140());
			stSaveSpeeddistDayStat.setLong(35, speeddistDay.getSpeed130140Time());
			
			stSaveSpeeddistDayStat.setLong(36, speeddistDay.getSpeed140150());
			stSaveSpeeddistDayStat.setLong(37, speeddistDay.getSpeed140150Time());
			
			stSaveSpeeddistDayStat.setLong(38, speeddistDay.getSpeed150160());
			stSaveSpeeddistDayStat.setLong(39, speeddistDay.getSpeed150160Time());
			
			stSaveSpeeddistDayStat.setLong(40, speeddistDay.getSpeed160170());
			stSaveSpeeddistDayStat.setLong(41, speeddistDay.getSpeed160170Time());
			
			stSaveSpeeddistDayStat.setLong(42, speeddistDay.getSpeed170180());
			stSaveSpeeddistDayStat.setLong(43, speeddistDay.getSpeed170180Time());
			
			stSaveSpeeddistDayStat.setLong(44, speeddistDay.getSpeed180190());
			stSaveSpeeddistDayStat.setLong(45, speeddistDay.getSpeed180190Time());
			
			stSaveSpeeddistDayStat.setLong(46, speeddistDay.getSpeed190200());
			stSaveSpeeddistDayStat.setLong(47, speeddistDay.getSpeed190200Time());
			
			stSaveSpeeddistDayStat.setLong(48, speeddistDay.getSpeedMax());
			stSaveSpeeddistDayStat.setLong(49, speeddistDay.getSpeedMaxTime());
			
			stSaveSpeeddistDayStat.setLong(50, speeddistDay.getMaxSpeed());
			stSaveSpeeddistDayStat.setLong(51, speeddistDay.getMinSpeed());		
			
			stSaveSpeeddistDayStat.executeUpdate();
		}catch(SQLException e){
			logger.error("统计 " + vid + " 车速分析表出错.",e);
		}finally{
			if(stSaveSpeeddistDayStat != null){
				stSaveSpeeddistDayStat.close();
			}
		}
	}
	
	/**
	 * 存储转速分析表
	 * @throws SQLException 
	 */
	public void saveRotatedistDay(String vid) throws SQLException{
		PreparedStatement stSaveRoatedistDayStat = null;	
		try{
			stSaveRoatedistDayStat = dbCon.prepareStatement(sql_saveRotateDayStat);	
			stSaveRoatedistDayStat.setString(1, vid);
			if(AnalysisDBAdapter.queryVechileInfo(vid) != null){
				stSaveRoatedistDayStat.setString(2, AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo());
				stSaveRoatedistDayStat.setString(3, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode());
			} else {	
				stSaveRoatedistDayStat.setString(2, null);
				stSaveRoatedistDayStat.setString(3, null);
			}
			stSaveRoatedistDayStat.setLong(4, this.utc + 12 * 60 * 60 * 1000);		
			stSaveRoatedistDayStat.setLong(5, this.utc + 12 * 60 * 60 * 1000);
			
			stSaveRoatedistDayStat.setLong(6, rotateSpeedDay.getRotateSpeed0());
			stSaveRoatedistDayStat.setLong(7, rotateSpeedDay.getRotateSpeed0Time());
			
			stSaveRoatedistDayStat.setLong(8, rotateSpeedDay.getRotateSpeed0100());
			stSaveRoatedistDayStat.setLong(9, rotateSpeedDay.getRotateSpeed0100Time());
			
			stSaveRoatedistDayStat.setLong(10, rotateSpeedDay.getRotateSpeed100200());
			stSaveRoatedistDayStat.setLong(11, rotateSpeedDay.getRotateSpeed100200Time());
			
			stSaveRoatedistDayStat.setLong(12, rotateSpeedDay.getRotateSpeed200300());
			stSaveRoatedistDayStat.setLong(13, rotateSpeedDay.getRotateSpeed200300Time());
			
			stSaveRoatedistDayStat.setLong(14, rotateSpeedDay.getRotateSpeed300400());
			stSaveRoatedistDayStat.setLong(15, rotateSpeedDay.getRotateSpeed300400Time());
			
			stSaveRoatedistDayStat.setLong(16, rotateSpeedDay.getRotateSpeed400500());
			stSaveRoatedistDayStat.setLong(17, rotateSpeedDay.getRotateSpeed400500Time());
			
			stSaveRoatedistDayStat.setLong(18, rotateSpeedDay.getRotateSpeed500600());
			stSaveRoatedistDayStat.setLong(19, rotateSpeedDay.getRotateSpeed500600Time());
			
			stSaveRoatedistDayStat.setLong(20, rotateSpeedDay.getRotateSpeed600700());
			stSaveRoatedistDayStat.setLong(21, rotateSpeedDay.getRotateSpeed600700Time());
			
			stSaveRoatedistDayStat.setLong(22, rotateSpeedDay.getRotateSpeed700800());
			stSaveRoatedistDayStat.setLong(23, rotateSpeedDay.getRotateSpeed700800Time());
			
			stSaveRoatedistDayStat.setLong(24, rotateSpeedDay.getRotateSpeed800900());
			stSaveRoatedistDayStat.setLong(25, rotateSpeedDay.getRotateSpeed800900Time());
			
			stSaveRoatedistDayStat.setLong(26, rotateSpeedDay.getRotateSpeed9001000());
			stSaveRoatedistDayStat.setLong(27, rotateSpeedDay.getRotateSpeed9001000Time());
			
			stSaveRoatedistDayStat.setLong(28, rotateSpeedDay.getRotateSpeed10001100());
			stSaveRoatedistDayStat.setLong(29, rotateSpeedDay.getRotateSpeed10001100Time());
			
			stSaveRoatedistDayStat.setLong(30, rotateSpeedDay.getRotateSpeed11001200());
			stSaveRoatedistDayStat.setLong(31, rotateSpeedDay.getRotateSpeed11001200Time());
			
			stSaveRoatedistDayStat.setLong(32, rotateSpeedDay.getRotateSpeed12001300());
			stSaveRoatedistDayStat.setLong(33, rotateSpeedDay.getRotateSpeed12001300Time());
			
			stSaveRoatedistDayStat.setLong(34, rotateSpeedDay.getRotateSpeed13001400());
			stSaveRoatedistDayStat.setLong(35, rotateSpeedDay.getRotateSpeed13001400Time());
			
			stSaveRoatedistDayStat.setLong(36, rotateSpeedDay.getRotateSpeed14001500());
			stSaveRoatedistDayStat.setLong(37, rotateSpeedDay.getRotateSpeed14001500Time());
		
			stSaveRoatedistDayStat.setLong(38, rotateSpeedDay.getRotateSpeed15001600());
			stSaveRoatedistDayStat.setLong(39, rotateSpeedDay.getRotateSpeed15001600Time());
			
			stSaveRoatedistDayStat.setLong(40, rotateSpeedDay.getRotateSpeed16001700());
			stSaveRoatedistDayStat.setLong(41, rotateSpeedDay.getRotateSpeed16001700Time());
			
			stSaveRoatedistDayStat.setLong(42, rotateSpeedDay.getRotateSpeed17001800());
			stSaveRoatedistDayStat.setLong(43, rotateSpeedDay.getRotateSpeed17001800Time());
			
			stSaveRoatedistDayStat.setLong(44, rotateSpeedDay.getRotateSpeed18001900());
			stSaveRoatedistDayStat.setLong(45, rotateSpeedDay.getRotateSpeed18001900Time());
			
			stSaveRoatedistDayStat.setLong(46, rotateSpeedDay.getRotateSpeed19002000());
			stSaveRoatedistDayStat.setLong(47, rotateSpeedDay.getRotateSpeed19002000Time());
			
			stSaveRoatedistDayStat.setLong(48, rotateSpeedDay.getRotateSpeed20002100());
			stSaveRoatedistDayStat.setLong(49, rotateSpeedDay.getRotateSpeed20002100Time());
			
			stSaveRoatedistDayStat.setLong(50, rotateSpeedDay.getRotateSpeed21002200());
			stSaveRoatedistDayStat.setLong(51, rotateSpeedDay.getRotateSpeed21002200Time());
			
			stSaveRoatedistDayStat.setLong(52, rotateSpeedDay.getRotateSpeed22002300());
			stSaveRoatedistDayStat.setLong(53, rotateSpeedDay.getRotateSpeed22002300Time());
			
			stSaveRoatedistDayStat.setLong(54, rotateSpeedDay.getRotateSpeed23002400());
			stSaveRoatedistDayStat.setLong(55, rotateSpeedDay.getRotateSpeed23002400Time());
			
			stSaveRoatedistDayStat.setLong(56, rotateSpeedDay.getRotateSpeed24002500());
			stSaveRoatedistDayStat.setLong(57, rotateSpeedDay.getRotateSpeed24002500Time());
			
			stSaveRoatedistDayStat.setLong(58, rotateSpeedDay.getRotateSpeed25002600());
			stSaveRoatedistDayStat.setLong(59, rotateSpeedDay.getRotateSpeed25002600Time());
			
			stSaveRoatedistDayStat.setLong(60, rotateSpeedDay.getRotateSpeed26002700());
			stSaveRoatedistDayStat.setLong(61, rotateSpeedDay.getRotateSpeed26002700Time());
			
			stSaveRoatedistDayStat.setLong(62, rotateSpeedDay.getRotateSpeed27002800());
			stSaveRoatedistDayStat.setLong(63, rotateSpeedDay.getRotateSpeed27002800Time());
			
			stSaveRoatedistDayStat.setLong(64, rotateSpeedDay.getRotateSpeed28002900());
			stSaveRoatedistDayStat.setLong(65, rotateSpeedDay.getRotateSpeed28002900Time());
			
			stSaveRoatedistDayStat.setLong(66, rotateSpeedDay.getRotateSpeed29003000());
			stSaveRoatedistDayStat.setLong(67, rotateSpeedDay.getRotateSpeed29003000Time());		
			
			stSaveRoatedistDayStat.setLong(68, rotateSpeedDay.getRotateSpeedMax());
			stSaveRoatedistDayStat.setLong(69, rotateSpeedDay.getRotateSpeedMaxTime());
			
			stSaveRoatedistDayStat.setLong(70, rotateSpeedDay.getPercent6080Fuhelv());
			
			stSaveRoatedistDayStat.setLong(71, rotateSpeedDay.getMinRotateSpeed());		
			stSaveRoatedistDayStat.setLong(72, rotateSpeedDay.getMaxRotateSpeed());	
			
			stSaveRoatedistDayStat.executeUpdate();
		}catch(SQLException e){
			logger.error("统计 " + vid + " 转速分析表出错。",e);
		}finally{
			if(stSaveRoatedistDayStat != null){
				stSaveRoatedistDayStat.close();
			}
		}
	}
	
	/**
	 * //蓄电池电压分布存储
	 * @throws SQLException
	 */
	public void saveVoltageDayStat(String vid) throws SQLException{
		PreparedStatement stSaveVoltageDayStat = null;
		try{
			stSaveVoltageDayStat = dbCon.prepareStatement(sql_saveVoltageDayStat);
			stSaveVoltageDayStat.setString(1, vid);
			if(AnalysisDBAdapter.queryVechileInfo(vid) != null){
				stSaveVoltageDayStat.setString(2, AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo());
				stSaveVoltageDayStat.setString(3, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode());
			} else {		
				stSaveVoltageDayStat.setString(2, null);
				stSaveVoltageDayStat.setString(3, null);
			}
			stSaveVoltageDayStat.setLong(4, this.utc + 12 * 60 * 60 * 1000);	
			
			stSaveVoltageDayStat.setLong(5, voltagedistDay.getVoltage0()); 
			stSaveVoltageDayStat.setLong(6, voltagedistDay.getVoltage0Time()); 
			
			stSaveVoltageDayStat.setLong(7, voltagedistDay.getVoltage020()); 
			stSaveVoltageDayStat.setLong(8, voltagedistDay.getVoltage020Time()); 
			
			stSaveVoltageDayStat.setLong(9, voltagedistDay.getVoltage20211()); 
			stSaveVoltageDayStat.setLong(10, voltagedistDay.getVoltage20211Time()); 
			
			stSaveVoltageDayStat.setLong(11, voltagedistDay.getVoltage20212()); 
			stSaveVoltageDayStat.setLong(12, voltagedistDay.getVoltage20212Time()); 
			
			stSaveVoltageDayStat.setLong(13, voltagedistDay.getVoltage21221()); 
			stSaveVoltageDayStat.setLong(14, voltagedistDay.getVoltage21221Time()); 
			
			stSaveVoltageDayStat.setLong(15, voltagedistDay.getVoltage21222()); 
			stSaveVoltageDayStat.setLong(16, voltagedistDay.getVoltage21222Time()); 
			
			stSaveVoltageDayStat.setLong(17, voltagedistDay.getVoltage22231()); 
			stSaveVoltageDayStat.setLong(18, voltagedistDay.getVoltage22231Time()); 
			
			stSaveVoltageDayStat.setLong(19, voltagedistDay.getVoltage22232()); 
			stSaveVoltageDayStat.setLong(20, voltagedistDay.getVoltage22232Time()); 
			
			stSaveVoltageDayStat.setLong(21, voltagedistDay.getVoltage23241()); 
			stSaveVoltageDayStat.setLong(22, voltagedistDay.getVoltage23241Time()); 
			
			stSaveVoltageDayStat.setLong(23, voltagedistDay.getVoltage23242()); 
			stSaveVoltageDayStat.setLong(24, voltagedistDay.getVoltage23242Time()); 
			
			stSaveVoltageDayStat.setLong(25, voltagedistDay.getVoltage24251()); 
			stSaveVoltageDayStat.setLong(26, voltagedistDay.getVoltage24251Time()); 
			
			stSaveVoltageDayStat.setLong(27, voltagedistDay.getVoltage24252()); 
			stSaveVoltageDayStat.setLong(28, voltagedistDay.getVoltage24252Time()); 
			
			stSaveVoltageDayStat.setLong(29, voltagedistDay.getVoltage25261()); 
			stSaveVoltageDayStat.setLong(30, voltagedistDay.getVoltage25261Time()); 
			
			stSaveVoltageDayStat.setLong(31, voltagedistDay.getVoltage25262()); 
			stSaveVoltageDayStat.setLong(32, voltagedistDay.getVoltage25262Time()); 
			
			stSaveVoltageDayStat.setLong(33, voltagedistDay.getVoltage26271()); 
			stSaveVoltageDayStat.setLong(34, voltagedistDay.getVoltage26271Time()); 
			
			stSaveVoltageDayStat.setLong(35, voltagedistDay.getVoltage26272()); 
			stSaveVoltageDayStat.setLong(36, voltagedistDay.getVoltage26272Time()); 
			
			stSaveVoltageDayStat.setLong(37, voltagedistDay.getVoltage27281()); 
			stSaveVoltageDayStat.setLong(38, voltagedistDay.getVoltage27281Time()); 
			
			stSaveVoltageDayStat.setLong(39, voltagedistDay.getVoltage27282()); 
			stSaveVoltageDayStat.setLong(40, voltagedistDay.getVoltage27282Time()); 
			
			stSaveVoltageDayStat.setLong(41, voltagedistDay.getVoltage28291()); 
			stSaveVoltageDayStat.setLong(42, voltagedistDay.getVoltage28291Time()); 
			
			stSaveVoltageDayStat.setLong(43, voltagedistDay.getVoltage28292()); 
			stSaveVoltageDayStat.setLong(44, voltagedistDay.getVoltage28292Time()); 
			
			stSaveVoltageDayStat.setLong(45, voltagedistDay.getVoltage29Max()); 
			stSaveVoltageDayStat.setLong(46, voltagedistDay.getVoltage29MaxTime()); 
			
			stSaveVoltageDayStat.setLong(47, voltagedistDay.getMax()); 
			stSaveVoltageDayStat.setLong(48, voltagedistDay.getMin()); 
			stSaveVoltageDayStat.setLong(49, voltagedistDay.getSumtime()); 
			stSaveVoltageDayStat.setLong(50, voltagedistDay.getSumcount()); 
			
			stSaveVoltageDayStat.setLong(51, voltagedistDay.getVoltage0121()); 
			stSaveVoltageDayStat.setLong(52, voltagedistDay.getVoltage0121Time()); 
			
			stSaveVoltageDayStat.setLong(53, voltagedistDay.getVoltage0122()); 
			stSaveVoltageDayStat.setLong(54, voltagedistDay.getVoltage0122Time()); 
			
			stSaveVoltageDayStat.setLong(55, voltagedistDay.getVoltage12131()); 
			stSaveVoltageDayStat.setLong(56, voltagedistDay.getVoltage12131Time()); 
			
			stSaveVoltageDayStat.setLong(57, voltagedistDay.getVoltage12132()); 
			stSaveVoltageDayStat.setLong(58, voltagedistDay.getVoltage12132Time()); 
			
			stSaveVoltageDayStat.setLong(59, voltagedistDay.getVoltage13141()); 
			stSaveVoltageDayStat.setLong(60, voltagedistDay.getVoltage13141Time()); 
			
			stSaveVoltageDayStat.setLong(61, voltagedistDay.getVoltage13142()); 
			stSaveVoltageDayStat.setLong(62, voltagedistDay.getVoltage13142Time()); 
			
			stSaveVoltageDayStat.setLong(63, voltagedistDay.getVoltage141()); 
			stSaveVoltageDayStat.setLong(64, voltagedistDay.getVoltage141Time()); 
			
			stSaveVoltageDayStat.setLong(65, voltagedistDay.getVoltage14Max()); 
			stSaveVoltageDayStat.setLong(66, voltagedistDay.getVoltage14MaxTime()); 	
			
			stSaveVoltageDayStat.executeUpdate();
		}catch(SQLException e){
			logger.error("统计 " + vid + "蓄电池电压出错.",e);
		}finally{
			if(stSaveVoltageDayStat != null){
				stSaveVoltageDayStat.close();
			}
		}
	}
	
	/*****
	 * 存储进气温度
	 * @param vid
	 */
	private void saveAirTemperture(String vid){
		PreparedStatement stSaveAirTempertureStat = null;
		try {
			stSaveAirTempertureStat = dbCon.prepareStatement(sql_saveAirTemperture);
			stSaveAirTempertureStat.setString(1, vid);
			if(AnalysisDBAdapter.queryVechileInfo(vid) != null){
				stSaveAirTempertureStat.setString(2, AnalysisDBAdapter.queryVechileInfo(vid).getVehicleNo());
				stSaveAirTempertureStat.setString(3, AnalysisDBAdapter.queryVechileInfo(vid).getVinCode());
			} else {		
				stSaveAirTempertureStat.setString(2, null);
				stSaveAirTempertureStat.setString(3, null);
			}
			stSaveAirTempertureStat.setLong(4, this.utc + 12 * 60 * 60 * 1000);
			stSaveAirTempertureStat.setLong(5, airTempertureBean.getTemperature_0().getNum());
			stSaveAirTempertureStat.setLong(6, airTempertureBean.getTemperature_0().getTime());
			stSaveAirTempertureStat.setLong(7, airTempertureBean.getTemperature_0_10().getNum());
			stSaveAirTempertureStat.setLong(8, airTempertureBean.getTemperature_0_10().getTime());
			stSaveAirTempertureStat.setLong(9, airTempertureBean.getTemperature_10_20().getNum());
			stSaveAirTempertureStat.setLong(10, airTempertureBean.getTemperature_10_20().getTime());
			stSaveAirTempertureStat.setLong(11, airTempertureBean.getTemperature_20_25().getNum());
			stSaveAirTempertureStat.setLong(12, airTempertureBean.getTemperature_20_25().getTime());
			stSaveAirTempertureStat.setLong(13, airTempertureBean.getTemperature_25_30().getNum());
			stSaveAirTempertureStat.setLong(14, airTempertureBean.getTemperature_25_30().getTime());
			stSaveAirTempertureStat.setLong(15, airTempertureBean.getTemperature_30_35().getNum());
			stSaveAirTempertureStat.setLong(16, airTempertureBean.getTemperature_30_35().getTime());
			stSaveAirTempertureStat.setLong(17, airTempertureBean.getTemperature_35_40().getNum());
			stSaveAirTempertureStat.setLong(18, airTempertureBean.getTemperature_35_40().getTime());
			stSaveAirTempertureStat.setLong(19, airTempertureBean.getTemperature_40_45().getNum());
			stSaveAirTempertureStat.setLong(20, airTempertureBean.getTemperature_40_45().getTime());
			stSaveAirTempertureStat.setLong(21, airTempertureBean.getTemperature_45_50().getNum());
			stSaveAirTempertureStat.setLong(22, airTempertureBean.getTemperature_45_50().getTime());
			stSaveAirTempertureStat.setLong(23, airTempertureBean.getTemperature_50_60().getNum());
			stSaveAirTempertureStat.setLong(24, airTempertureBean.getTemperature_50_60().getTime());
			stSaveAirTempertureStat.setLong(25, airTempertureBean.getTemperature_60_70().getNum());
			stSaveAirTempertureStat.setLong(26, airTempertureBean.getTemperature_60_70().getTime());
			stSaveAirTempertureStat.setLong(27, airTempertureBean.getTemperature_70().getNum());
			stSaveAirTempertureStat.setLong(28, airTempertureBean.getTemperature_70().getTime());
			stSaveAirTempertureStat.setLong(29, airTempertureBean.getMax());
			stSaveAirTempertureStat.setLong(30, airTempertureBean.getMin());
			stSaveAirTempertureStat.executeUpdate();
		} catch (SQLException e) {
			logger.error("统计进气温度出错." + vid, e);
		}finally{
			if(stSaveAirTempertureStat != null){
				try {
					stSaveAirTempertureStat.close();
				} catch (SQLException e) {
					logger.error(e.getMessage(), e);
				}
			}
		}
	}

	@Override
	public void costTime() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setThreadId(int threadId) {
		this.threadId = threadId;
		
	}
	
	/*****
	 * 设置统计时间
	 */
	@Override
	public void setTime(long utc) {
		this.utc = utc;
		
	}
}
