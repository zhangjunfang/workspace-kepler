package com.ctfo.savecenter.addin.kcpt.trackmanager;

import java.util.Map;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.savecenter.Constant;
import com.ctfo.savecenter.dao.TempMemory;
import com.ctfo.savecenter.dao.TrackManagerKcptDBAdapter;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： KCPTSaveCenter <br>
 * 功能： 跨域统计存储线程<br>
 * 描述：跨域统计存储线程：<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
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
 * <td>2013-3-12</td>
 * <td>HUSHUANG</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author HUSHUANG
 * @since JDK1.6
 */
public class SpannedStatisticsThread extends Thread {
	/* 日志 */
	private static final Logger logger = LoggerFactory.getLogger(SpannedStatisticsThread.class);
	/* 存储数据库适配器 */
	private TrackManagerKcptDBAdapter oracleDB = null;
	/* 数据队列 */
	private ArrayBlockingQueue<Map<String,String>> vPacket = new ArrayBlockingQueue<Map<String,String>>(100000);
	/** 序号 */
	int index = 0;
	public int getPacketsSize() {
		return vPacket.size();
	}
	
	/**
	 * 添加消息到跨域统计队列中
	 * @param packet
	 * void
	 */
	public void addPacket(Map<String,String> packet) {
		try {
			vPacket.put(packet);
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
	}
	
	/**
	 * 功能： 跨域统计存储线程<br>
	 * 描述： 跨域统计存储线程<br>
	 * 
	 * @param oracleDB oracle数据库访问对象
	 */
	public SpannedStatisticsThread(TrackManagerKcptDBAdapter oracleDB){
		this.oracleDB = oracleDB;
	}
	
	/**
	 * 	跨域统计线程运行业务处理 <br>
	 *	出入境    <br>
	 *  市级：出本市算一次，从其他市到另外市也算一次。从其他市回本市不算。<br>
	 *  省级：出本省算一次，在外省内任何市之间不算，从其他省到另外省算一次，在另外省内任何市不算。<br>
	 * 
	 * 
	 * @see java.lang.Thread#run()
	 */
	public void run(){
		String lastCityCodeStr = null;
		String lastCityCode = null;
		Long cityCode = null;
		Long areaCode = null;
		while(TrackManagerKcptMainThread.isRunning){
			try{
				Map<String,String> app = vPacket.take();
				//1. 根据经纬度获取区域编码
				areaCode = TempMemory.getAreaAnalyzer(Double.parseDouble(app.get("1"))/600000, Double.parseDouble(app.get("2"))/600000);
				String currentCode = String.valueOf(areaCode);
				if(areaCode < 0){
//					logger.debug("===跨域统计存储服务===地理位置解析异常：经度:"+app.get("1")+" , 纬度:"+app.get("2")+" 车辆VID:"+app.get(Constant.VID)+" , 当前区域码:"+areaCode);
					continue;
				}
				//2. 根据VID获取上一次车辆所在地区域编码（前4位）
				lastCityCodeStr =TempMemory.getAreaLastMapValue(app.get(Constant.VID));
				if(lastCityCodeStr == null || lastCityCodeStr.length() < 4){
					TempMemory.setAreaLastMap(app.get(Constant.VID), currentCode);
					continue;
				}
				lastCityCode = lastCityCodeStr.substring(0, 4);
				cityCode = TempMemory.getVehicleMapValue(app.get(Constant.MACID)).getAreacode();
				String possessionCode = String.valueOf(cityCode);
				//3. 如果区域编码改变，存储当前变化信息，更新缓存中当前区域编码
//				logger.debug("---跨域统计存储服务---处理当前区域信息：经度:"+app.get("1")+" , 纬度:"+app.get("2")+" 车辆VID:"+app.get(Constant.VID)+" , 属地码:"+cityCode+" , 上一区域码:"+lastCityCode+" , 当前区域码:"+areaCode);
				if(immigrationLogical(currentCode, lastCityCode)){
					//判断回境
					if(currentCode.startsWith(possessionCode.substring(0, 4))){
						TempMemory.setAreaLastMap(app.get(Constant.VID), currentCode);
					}else{
						//存储语句｛INSERT INTO TH_SPANNED_STATISTICS(SUID,LOCAL_CODE,CURRENT_CODE,CURRENT_TIME) VALUES(?,?,?,?)｝
						oracleDB.saveThSpannedStatistics(UUID.randomUUID().toString(),possessionCode,currentCode,System.currentTimeMillis());
						TempMemory.setAreaLastMap(app.get(Constant.VID), currentCode);
					}
				}
				if((index % 100 ) == 0){
					logger.debug("---跨域统计存储服务---SpannedStatisticsTread---ArrayBlockingQueue---size:"+getPacketsSize());
					index = 0;
				}
				index ++;
			}catch(Exception ex){
				logger.error("跨域统计存储异常: 上一区域代码:"+lastCityCode +", 属地区域代码："+cityCode+", 解析经纬度区域代码:"+areaCode+",异常内容:\n"+ex.getMessage(),ex);
			}
		}
	}
	/**
	 * 判断出境
	 * 出入境    <br>
	 *  市级：出本市算一次，从其他市到另外市也算一次。从其他市回本市不算。<br>
	 *  省级：出本省算一次，在外省内任何市之间不算，从其他省到另外省算一次，在另外省内任何市不算。<br>
	 * @param currentCode 		当前区域编码
	 * @param lastCode			上一次区域编码
	 * @return
	 * boolean
	 *
	 */
	private boolean immigrationLogical(String currentCode, String lastCode){
//		判断省级
		if(currentCode.startsWith(lastCode.substring(0, 2))){ 
			//判断市级
			if(currentCode.startsWith(lastCode.substring(0, 4))){
				return false;
			}else{
				return true;
			}
		}else{
			return true;
		}
	}
}
