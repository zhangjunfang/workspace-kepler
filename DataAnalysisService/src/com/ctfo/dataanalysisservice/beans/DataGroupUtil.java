package com.ctfo.dataanalysisservice.beans;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import com.ctfo.dataanalysisservice.mem.MemManager;

public class DataGroupUtil {
	private static Log log = LogFactory.getLog(DataGroupUtil.class);

	/**
	 * 根据线路ID分组线段,组装返回车辆线路信息
	 * 
	 * @param data
	 *            原始数据
	 * @return 组装完成数据
	 */
	public static boolean getListData(List<SectionsDataObject> data) {

		boolean result = false;
		try {
			List<SectionsDataObject> returnData;
			Map<String, List<SectionsDataObject>> map = new HashMap<String, List<SectionsDataObject>>();
			for (SectionsDataObject ob : data) {
				if (map.get(ob.getLineId()) != null) {
					map.get(ob.getLineId()).add(ob);
				} else {
					returnData = new ArrayList<SectionsDataObject>();
					returnData.add(ob);
					map.put(ob.getLineId(), returnData);
				}
			}
			for (SectionsDataObject ob : data) {

				MemManager.setLineMap(PlatAlarmTypeUtil.KEY_WORD + ob.getVid()
						+ "_" + PlatAlarmTypeUtil.PLAT_DEVIATE_LINE_ALARM,
						map.get(ob.getLineId()));

			}

			result = true;
		} catch (Exception e) {
			log.error("同步线段数据异常，数据处理 异常。。。");
			log.error(e.getMessage());
		}
		return result;
	}
	
	
	
	/**
	 * 根据车辆的VID缓存车辆的线路信息
	 * @param data
	 * @return
	 */
	public static boolean getListData2(List<SectionsDataObject> data) {

		boolean result = false;
		try {
			List<SectionsDataObject> returnData;
			Map<String, List<SectionsDataObject>> map = new HashMap<String, List<SectionsDataObject>>();
			for (SectionsDataObject ob : data) 
			{
				if (map.get(ob.getVid()) != null) 
				{
					map.get(ob.getVid()).add(ob);
				} else {
					returnData = new ArrayList<SectionsDataObject>();
					returnData.add(ob);
					map.put(ob.getVid(), returnData);
				}
			}
			for (SectionsDataObject ob : data) {

				MemManager.setLineMap(PlatAlarmTypeUtil.KEY_WORD + ob.getVid()
						+ "_" + PlatAlarmTypeUtil.PLAT_DEVIATE_LINE_ALARM,
						map.get(ob.getVid()));

			}

			result = true;
		} catch (Exception e) {
			log.error("根据车辆的VID缓存车辆的线路信息，数据处理 异常。。。");
			log.error(e.getMessage());
		}
		return result;
	}
	
	
	
	

	/**
	 * 获取关键点数据
	 * 
	 * @param keyPointList
	 *            关键点原始数据
	 * @return 组装完成数据
	 */
	public static boolean getPointData(List<KeyPointDataObject> keyPointList) {
		List<KeyPointDataObject> dataList;
		boolean result = false;
		try {
			synchronized (MemManager.keyPointMap) {
				MemManager.keyPointMap.clear();
				for (KeyPointDataObject bo : keyPointList) {
					String key = PlatAlarmTypeUtil.KEY_WORD + "_" + bo.getVid() + "_" + PlatAlarmTypeUtil.PLAT_KEY_POINT_ALARM;
					dataList = MemManager.getKeyPointMap(key);
					if (dataList != null) {
						dataList.add(bo);
						//MemManager.setKeyPointMap(key, dataList);
					} else {
						dataList = new ArrayList<KeyPointDataObject>();
						dataList.add(bo);
						MemManager.setKeyPointMap(key, dataList);
					}
				}
				result = true;
			}
		} catch (Exception e) {
			log.error("同步线段数据异常，数据处理 异常。。。");
			log.error(e.getMessage());
		}
		return result;
	}

	/**
	 * 获取电子围栏数据
	 * 
	 * @param areaDataList
	 *            电子围栏原始数据
	 * @return 组装完成数据
	 */
	public static boolean getAreaData(List<AreaDataObject> areaDataList) {

		List<AreaDataObject> lists = new ArrayList<AreaDataObject>();
		AreaDataObject areaDataObject = new AreaDataObject();
		List<String> list = new ArrayList<String>();
		boolean result = false;
		String tempvid = "";

		Long tempareaid = 0l;
		String key = "";
		try {
			for (AreaDataObject bo : areaDataList) {
				// 首次初始化key
				if ("".equals(key)) {
					key = PlatAlarmTypeUtil.KEY_WORD + "_" + bo.getVid() + "_"
							+ PlatAlarmTypeUtil.PLAT_AREA_ALARM;
				}

				// 一辆车对应多区域处理
				if (!"".equals(tempvid) && tempvid.equals(bo.getVid())
						&& !bo.getAreaID().equals(tempareaid)) {
					areaDataObject.setLonlats(list);
					lists.add(areaDataObject);
					areaDataObject = new AreaDataObject();
					list = new ArrayList<String>();
				}
				// 处理一辆车结束
				if (!"".equals(tempvid) && !tempvid.equals(bo.getVid())) {
					areaDataObject.setLonlats(list);
					lists.add(areaDataObject);
					MemManager.setAreaMap(key, lists);
					key = PlatAlarmTypeUtil.KEY_WORD + "_" + bo.getVid() + "_"
							+ PlatAlarmTypeUtil.PLAT_AREA_ALARM;
					lists = new ArrayList<AreaDataObject>();
					areaDataObject = new AreaDataObject();
					list = new ArrayList<String>();
				}
				// 每次赋值
				tempvid = bo.getVid();
				tempareaid = bo.getAreaID();
				list.add(bo.getLonlat());
				areaDataObject.setAreaID(bo.getAreaID());
				areaDataObject.setVid(bo.getVid());
				areaDataObject.setBeginTime(bo.getBeginTime());
				areaDataObject.setEndTime(bo.getEndTime());
				areaDataObject.setMaxSpeed(bo.getMaxSpeed());
				areaDataObject.setMaxSpeedTime(bo.getMaxSpeedTime());
			}
			// 最后赋值
			areaDataObject.setLonlats(list);
			lists.add(areaDataObject);
			MemManager.setAreaMap(key, lists);
			result = true;
		} catch (Exception e) {
			log.error("同步线段数据异常，数据处理 异常。。。");
			log.error(e.getMessage());
		}
		return result;
	}

	/**
	 * 获取线段数据
	 * 
	 * @param sectionsDataList
	 *            线段原始数据
	 * @return 组装完成数据
	 * 
	 *         2012-2-29 杨毅注释 与线路合并
	 * 
	 *         public static boolean getSectionsData( List<SectionsDataObject>
	 *         sectionsDataList) { List<SectionsDataObject> dataList;
	 *         MemcachedClient client = MemManager.getMemcachedClient(); boolean
	 *         result = false; try { for (SectionsDataObject bo :
	 *         sectionsDataList) { String key = PlatAlarmTypeUtil.KEY_WORD +
	 *         bo.getVid() + "_" + PlatAlarmTypeUtil.PLAT_SECTION_ALARM; if
	 *         (client.get(key) != null) { List<SectionsDataObject> list =
	 *         client.get(key); list.add(bo); client.delete(key);
	 *         client.add(key, 0, list); } else { dataList = new
	 *         ArrayList<SectionsDataObject>(); dataList.add(bo);
	 *         client.delete(key); client.add(key, 0, dataList); } } result =
	 *         true; } catch (Exception e) { log.error("同步线段数据异常，数据处理 异常。。。");
	 *         log.error(e.getMessage()); } finally { try { client.shutdown(); }
	 *         catch (IOException e) { log.error("关闭MEMCACHED异常。");
	 *         log.error(e.getMessage()); } } return result; }
	 */

	/**
	 * 获取报警车辆列表
	 * 
	 * @param alarmVehicleDataList
	 *            报警车辆原始数据
	 * @return 组装完成数据
	 */
	public static boolean getAlarmVehicleListData(
			List<AlarmVehicleBean> alarmVehicleDataList) {
		boolean result = false;
		try {
			for (AlarmVehicleBean bo : alarmVehicleDataList) {
				String key = PlatAlarmTypeUtil.KEY_WORD + "_"
						+ bo.getMobileNo();
				// AlarmVehicleBean alarmVehicleBean = MemManager
				// .getAlarmVehicleMap(key);
				// if (alarmVehicleBean != null) {
				//
				// alarmVehicleBean.setAlarmType(alarmVehicleBean
				// .getAlarmType() + "," + bo.getAlarmType());
				// MemManager.setAlarmVehicleMap(key, alarmVehicleBean);
				//
				// } else {
				AlarmVehicleBean alarmVehicleBean = new AlarmVehicleBean();
				alarmVehicleBean.setAlarmType(bo.getAlarmType());
				alarmVehicleBean.setVid(bo.getVid());
				alarmVehicleBean.setMobileNo(bo.getMobileNo());
				MemManager.setAlarmVehicleMap(key, alarmVehicleBean);
				// }
			}
			result = true;
		} catch (Exception e) {
			log.error("同步报警车辆列表数据异常，数据处理 异常。。。");
			log.error(e.getMessage());
		}

		return result;
	}

	/**
	 * 获取时间关键点报警车辆列表
	 * 
	 * @param alarmStationDataList
	 *            时间关键点报警车辆信息
	 * 
	 * @return 组装完成数据
	 */
	public static boolean getAlarmStationByTimeListData(
			List<AlarmStationBean> alarmStationDataList) {

		Map<String, List<String>> keyPointData = new HashMap<String, List<String>>();
		boolean result = false;
		try {
			String key = PlatAlarmTypeUtil.KEY_WORD + "_"
					+ PlatAlarmTypeUtil.KEY_POINT_WORD;
			for (AlarmStationBean station : alarmStationDataList) {
				// 到达报警
				if (station.getToUtc() != null) {
					String key1 = String.valueOf(station.getToUtc());
					List<String> list1 = MemManager.getStationTimeMap(key1);
					if (list1 == null) {
						list1 = new ArrayList<String>();
					}
					list1.add(station.getVid());
					keyPointData.put(station.getToUtc(), list1);
				}
				// 离开报警
				if (station.getLeaveUtc() != null) {
					String key2 = PlatAlarmTypeUtil.KEY_WORD + "_"
							+ station.getLeaveUtc();
					List<String> list2 = MemManager.getStationTimeMap(key2);
					if (list2 == null) {
						list2 = new ArrayList<String>();
					}
					list2.add(station.getVid());
					keyPointData.put(station.getToUtc(), list2);

				}
			}
			// 根据时间进行Map排序
			List<Map.Entry<String, List<String>>> infoIds = new ArrayList<Map.Entry<String, List<String>>>(
					keyPointData.entrySet());
			Collections.sort(infoIds,
					new Comparator<Map.Entry<String, List<String>>>() {
						public int compare(Map.Entry<String, List<String>> o1,
								Map.Entry<String, List<String>> o2) {
							return (o1.getKey()).compareTo(o2.getKey());
						}
					});

			MemManager.setStationMap(key, infoIds);
			result = true;
		} catch (Exception e) {
			log.error("同步时间关键点报警车辆数据异常，数据处理 异常。。。" + e.getMessage());
			e.printStackTrace();
		}
		return result;
	}

	public static void main(String[] args) {

	}

}
