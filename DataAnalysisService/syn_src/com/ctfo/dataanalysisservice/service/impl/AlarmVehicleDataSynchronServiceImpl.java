package com.ctfo.dataanalysisservice.service.impl;

import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import com.ctfo.dataanalysisservice.beans.AlarmStationBean;
import com.ctfo.dataanalysisservice.beans.AlarmVehicleBean;
import com.ctfo.dataanalysisservice.beans.AreaDataObject;
import com.ctfo.dataanalysisservice.beans.DataGroupUtil;
import com.ctfo.dataanalysisservice.beans.KeyPointDataObject;
import com.ctfo.dataanalysisservice.beans.SectionsDataObject;
import com.ctfo.dataanalysisservice.dao.AlarmVehicleDataSynchronDao;
import com.ctfo.dataanalysisservice.dao.impl.AlarmVehicleDataSynchronDaoImpl;
import com.ctfo.dataanalysisservice.service.AlarmVehicleDataSynchronService;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： DataAnalysisService <br>
 * 功能：配置同步获取数据库车辆围栏，线路，线段，关键点配置 <br>
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
 * <td>2012-2-16</td>
 * <td>wuqj</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author wuqj
 * @since JDK1.6
 */
public class AlarmVehicleDataSynchronServiceImpl extends Thread implements
		AlarmVehicleDataSynchronService{


	private static Log log = LogFactory
			.getLog(AlarmVehicleDataSynchronServiceImpl.class);
	private AlarmVehicleDataSynchronDao alarmVehicleDataSynchronDao;

	public AlarmVehicleDataSynchronServiceImpl() {
		this.setAlarmVehicleDataSynchronDao(new AlarmVehicleDataSynchronDaoImpl());
	}

	/**
	 * 获取时间关键点车辆信息
	 * 
	 * @return
	 */
	private boolean getAlarmStationByTimeList() {

		// 获取报警车辆信息
		List<AlarmStationBean> alarmStationDataList = alarmVehicleDataSynchronDao
				.getAlarmStationVehicleList();
		// 组装数据
		return DataGroupUtil
				.getAlarmStationByTimeListData(alarmStationDataList);
	}

	/**
	 * 获取报警车辆的列表
	 * 
	 * @return
	 */
	private boolean getAlarmVehicleList() {

		// 获取报警车辆信息
		List<AlarmVehicleBean> alarmVehicleDataList = alarmVehicleDataSynchronDao
				.getAlarmVehicleList();
		// 组装数据
		return DataGroupUtil.getAlarmVehicleListData(alarmVehicleDataList);
	}

	/**
	 * 获取车辆的线路信息
	 * 
	 * @return
	 */
	private boolean getLineDataObject() {
		// 获取车辆的线路信息
		List<SectionsDataObject> list = alarmVehicleDataSynchronDao
				.getLineDataObject();
		// 组装数据
		return DataGroupUtil.getListData2(list);
	}

	/**
	 * 获取车辆关键点信息
	 * 
	 * @return
	 */
	private boolean getKeyPointDataObject() {
		// 获取车辆关键点信息
		List<KeyPointDataObject> list = alarmVehicleDataSynchronDao
				.getKeyPointDataObject();
		// 组装数据
		return DataGroupUtil.getPointData(list);
	}

	/**
	 * 获取电子围栏信息
	 * 
	 * @return
	 */
	private boolean getAreaDataObject() {
		// 获取数据
		List<AreaDataObject> list = alarmVehicleDataSynchronDao
				.getAreaDataObject();
		// 组装数据
		return DataGroupUtil.getAreaData(list);
	}

	/**
	 * 获取线段数据
	 * 
	 * @return
	 * 
	 *         private boolean getSectionDataObject() { // 获取线段数据
	 *         List<SectionsDataObject> list =
	 *         alarmVehicleDataSynchronDao.getSectionDataObject(); // 组装数据
	 *         return DataGroupUtil.getSectionsData(list); }
	 */

	/**
	 * @param args
	 */
	public static void main(String[] args) {

		AlarmVehicleDataSynchronServiceImpl service = new AlarmVehicleDataSynchronServiceImpl();
		service.getAlarmVehicleList();
	}

	// 构造器和访问器
	public AlarmVehicleDataSynchronDao getAlarmVehicleDataSynchronDao() {
		return alarmVehicleDataSynchronDao;
	}

	public void setAlarmVehicleDataSynchronDao(
			AlarmVehicleDataSynchronDao alarmVehicleDataSynchronDao) {
		this.alarmVehicleDataSynchronDao = alarmVehicleDataSynchronDao;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see
	 * com.ctfo.dataanalysisservice.service.AlarmVehicleDataSynchronService#
	 * init()
	 */
	@Override
	public boolean init() {
		boolean result = false;
		try {
			this.getAreaDataObject();
			this.getLineDataObject();
			this.getKeyPointDataObject();
			this.getAlarmVehicleList();
			this.getAlarmStationByTimeList();
			result = true;
		} catch (Exception e) {
			log.error("配置同步数据异常。"+e.getMessage());
			e.printStackTrace();
		}
		return result;
	}
	
	
	public void run(){
		while(true){
			init();
			log.debug("同步数据成功");
			try {
				sleep(1000*60*5);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
	}

}
