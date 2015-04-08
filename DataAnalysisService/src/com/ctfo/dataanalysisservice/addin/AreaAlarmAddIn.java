package com.ctfo.dataanalysisservice.addin;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;

import org.apache.log4j.Logger;

import com.ctfo.dataanalysisservice.DataAnalysisServiceMain;
import com.ctfo.dataanalysisservice.beans.AlarmDataCache;
import com.ctfo.dataanalysisservice.beans.AreaDataObject;
import com.ctfo.dataanalysisservice.beans.PlatAlarmTypeUtil;
import com.ctfo.dataanalysisservice.beans.ThVehicleAlarm;
import com.ctfo.dataanalysisservice.beans.VehicleMessage;
import com.ctfo.dataanalysisservice.gis.PoiUtil;
import com.ctfo.dataanalysisservice.io.DataPool;
import com.ctfo.dataanalysisservice.mem.MemManager;
import com.ctfo.dataanalysisservice.util.Base64_URl;

/**
 * 围栏区域报警处理
 * 
 * @author yangjian
 * 
 */
public class AreaAlarmAddIn extends Thread implements IaddIn {

	private static final Logger logger = Logger.getLogger(AreaAlarmAddIn.class);
	// 待处理数据队列
	private ArrayBlockingQueue<VehicleMessage> vPacket = new ArrayBlockingQueue<VehicleMessage>(
			100000);

	private String name;

	public AreaAlarmAddIn() {
		name = UUID.randomUUID().toString();
		// 记录线程数
		DataAnalysisServiceMain.threadCount++;
	}

	public void addPacket(VehicleMessage vehicleMessage) {

//		logger.info(name + "AreaAlarmAddIn" + vehicleMessage.getVid() + "size"
//				+ getPacketsSize());

		if (vehicleMessage != null) {
			try {
				vPacket.put(vehicleMessage);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			// System.out.println(getPacketsSize());

		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}

	public VehicleMessage getPacket() throws InterruptedException {
		return vPacket.take();
	}

	public void run() {

		while (true) {
			try {
				// 获得要处理的位置信息数据
				VehicleMessage vehicleMessage = getPacket();
				if (vehicleMessage != null) {
					logger.debug("区域报警数据：" + vehicleMessage.getCommanddr());
					// 判断并记录报警信息
					checkAlarm(vehicleMessage);

				} 
			} catch (Exception e) {
				// TODO Auto-generated catch block
				logger.error(e);
				
			}
		}

	}

	// 测试对象
	// 围栏信息 key=vid
	// private Map<Long, List<AreaDataCache>> areaMap = new HashMap<Long,
	// List<AreaDataCache>>();

	// 报警map 缓存 key=vId_areaId
	private Map<String, AlarmDataCache> alarmMap = new ConcurrentHashMap<String, AlarmDataCache>();

	// 持续超速 已报警 更新
	private Integer speedType1 = 1;
	// 持续超速 上次未报警 本次上报 更新
	private Integer speedType2 = 2;
	// 持续超速 上次未报警 本次也未上报 更新
	private Integer speedType3 = 3;
	// 新增缓存中的超速报警数据 本次未上报
	private Integer speedType4 = 4;

	/**
	 * 判断报警信息
	 * 
	 * @param vehicleMessage
	 *            实时数据
	 */
	public void checkAlarm(VehicleMessage vehicleMessage) {

		// 缓存中取得的每辆车围栏信息
		List<AreaDataObject> areaList = null;
		Long vid = vehicleMessage.getVid();
		
		boolean speedflag=true;
		// 车围栏缓存
		AlarmDataCache alarmDataCache = null;
		// 判断缓存
 
			String key = PlatAlarmTypeUtil.KEY_WORD+ "_" + vid + "_"
					+ PlatAlarmTypeUtil.PLAT_AREA_ALARM;
			// 根据vid查询车辆的围栏信息集合
			areaList = MemManager.getAreaMap(key);

			if (areaList != null) {
				areaList=new	ArrayList<AreaDataObject>(areaList);
				// 实时数据与逐条围栏比对
				for (AreaDataObject areaDataObject : areaList) {
					try {

							//速度和持续时间为空判断
							if(areaDataObject.getMaxSpeedTime()==null||areaDataObject.getMaxSpeed()==null){
								speedflag=false;
							}else{ 
								speedflag=true;
							}
							
							// 取得本车本围栏缓存数据 vid+areaid
							alarmDataCache = getAlarmCache(vehicleMessage.getVid(),areaDataObject.getAreaID());
 
							// 在围栏判定周期内 [A]
							if (isInCheckTime(areaDataObject, vehicleMessage)) {
								// 本次记录在围栏内 [A1]
								if (isInArea(areaDataObject, vehicleMessage)) {
									// 存在缓存
									if (alarmDataCache != null) {
										// 上一记录在围栏内 [A11]
										if (alarmDataCache.isInOutAreaAlarm()) {
											// 本次超速 [A111]
											if (speedflag&&checkOverspeed(Long.parseLong(areaDataObject.getMaxSpeed()),vehicleMessage.getSpeed())) {
												// 上一记录超速 [A1111]
												if (alarmDataCache.isSpeedingAlarm()) {
													// 上一记录超速报警 已上报 [A11111]
													if (alarmDataCache.isSpeedAndTimeAlarm()) {
														// 更新缓存中得超速报警数据
														modifySpeedCache(speedType1,alarmDataCache,vehicleMessage);

														// 上一记录超速报警 未上报 [A11112]
													} else {
														// 本次满足上报条件 [A111121]
														if (checkOverSpeedAlarm(	alarmDataCache.getBeginOverSpeedTime(),
																vehicleMessage.getUtc(),	Long.parseLong(areaDataObject.getMaxSpeedTime()))) {
															// 插入新超速报警记录
															addOverSpeedAlarm(false,alarmDataCache,vehicleMessage);
															// 更新缓存中得超速报警数据
															// 本次已上报
															modifySpeedCache(speedType2,alarmDataCache,vehicleMessage);

															// 本次不满足上报条件
															// [A111122]
														} else {
															// 更新缓存中得超速报警数据
															// 本次未上报
															modifySpeedCache(speedType3,alarmDataCache,vehicleMessage);
														}

													}

													// 上一记录未超速 [A1112]
												} else {

													// 新增缓存中的超速报警数据 本次未上报
													modifySpeedCache(speedType4,alarmDataCache,vehicleMessage);
												}

												// 本次未超速 [A112]
											} else {
												// 上次超速 [A1121]
												if (speedflag&&alarmDataCache.isSpeedingAlarm()) {

													// 上次已上报超速报警 [A11211]
													if (alarmDataCache.isSpeedAndTimeAlarm()) {
														// 更新超速报警记录
														addOverSpeedAlarm(true,alarmDataCache,vehicleMessage);

													}
													// 清空超速报警缓存
													removeOverSpeedCache(alarmDataCache);

												}

											}
											// 上次在围栏外[A12]
										} else {
											
											// 更新状态缓存为围栏内
											modifyInOutAreaAlarmCache(alarmDataCache, true);
											// 进围栏报警
											addAreaOutOrInAlarm(alarmDataCache,vehicleMessage);
										
											// 本次超速 [A121]
											if (  speedflag && checkOverspeed(Long.parseLong(areaDataObject.getMaxSpeed()),vehicleMessage.getSpeed())) {
												// 新增缓存超速状态
												modifySpeedCache(speedType3,alarmDataCache,vehicleMessage);
											}
										}

										// 不存在缓存 说明首条记录
									} else {

										// 新增缓存 记录本次在围栏内
										alarmDataCache=newCache(true, vehicleMessage.getVid(),areaDataObject.getAreaID());
										// 判断是否超速
										if (checkOverspeed(Long.parseLong(areaDataObject.getMaxSpeed()),vehicleMessage.getSpeed())) {
											// 新增缓存中的超速报警数据 本次未上报
											modifySpeedCache(speedType4,alarmDataCache,vehicleMessage);
										}

									}

									// 本次记录在围栏外 [A2]
								} else {

									// 存在缓存
									if (alarmDataCache != null) {
										// 上次在围栏内
										if (alarmDataCache.isInOutAreaAlarm()) {
											// 更新状态缓存为围栏外
											modifyInOutAreaAlarmCache(alarmDataCache, false);
											// 出围栏报警
											addAreaOutOrInAlarm(alarmDataCache,vehicleMessage);
											
											// 上次超速报警 已上报
											if (speedflag&&alarmDataCache.isSpeedAndTimeAlarm()) {
												// 更新超速报警记录
												addOverSpeedAlarm(true,alarmDataCache,vehicleMessage);
											}
											
											// 清空超速报警缓存
											removeOverSpeedCache(alarmDataCache);
										}

									} else {
										// 新增缓存 记录本次在围栏外
										alarmDataCache=newCache(false,vehicleMessage.getVid(),areaDataObject.getAreaID());
									}

								}
								// 在围栏判定周期外 [B]
							} else {

								if (alarmDataCache != null) {
									// 上次 超速 已上报
									if (speedflag&&alarmDataCache.isSpeedAndTimeAlarm()) {
										// 更新超速报警结束
										addOverSpeedAlarm(true, alarmDataCache,vehicleMessage);

									}
									// 清空本车本围栏报警
									removeAllCache(vehicleMessage.getVid(),alarmDataCache.getAreaId());
								}

							}
						 
					} catch (Exception e) {
						e.printStackTrace();
					}
				}
				areaList=null;
			}
		 
	}

	/**
	 * 新增缓存 并记录围栏位置
	 */
	private AlarmDataCache newCache(boolean inOrOut, Long vid, Long areaId) {

		String key = null;
		AlarmDataCache alarmDataCache =null;
		if (vid != null && areaId != null) {
			 alarmDataCache = new AlarmDataCache();
			alarmDataCache.setInOutAreaAlarm(inOrOut);
			key = String.valueOf(vid) + "_" + String.valueOf(areaId);
			alarmMap.put(key, alarmDataCache);
		}
		return alarmDataCache;
	}

	/**
	 * 更新进出区域缓存状态
	 * 
	 * @param alarmDataCache
	 */
	private void modifyInOutAreaAlarmCache(AlarmDataCache alarmDataCache,
			boolean inOrOut) {
		alarmDataCache.setInOutAreaAlarm(inOrOut);
	}

	/**
	 * 清空所有告警缓存
	 * 
	 * @param vid
	 * @param areaId
	 */
	private void removeAllCache(Long vid, Long areaId) {

		String key = null;

		if (vid != null && areaId != null) {
			key = String.valueOf(vid) + "_" + String.valueOf(areaId);
			alarmMap.remove(key);
		}
	}

	/**
	 * 清空超速告警缓存
	 * 
	 * @param alarmDataCache
	 */
	private void removeOverSpeedCache(AlarmDataCache alarmDataCache) {

		if (alarmDataCache != null) {
			alarmDataCache.setSpeedAndTimeAlarm(false);
			alarmDataCache.setSpeedingAlarm(false);
			alarmDataCache.setBeginOverSpeedTime(null);
			alarmDataCache.setEndOverSpeedTime(null);
			alarmDataCache.setEndSpeed(null);
			
		}
	}

	/**
	 * 新增或修改超速报警记录
	 * 
	 * @param isUpdate
	 *            是否更新
	 * @param alarmDataCache
	 * @param vehicleMessage
	 */
	private void addOverSpeedAlarm(boolean isUpdate,
			AlarmDataCache alarmDataCache, VehicleMessage vehicleMessage) {
         
		//新增记录需要完善字段
		
		if (alarmDataCache != null && vehicleMessage != null) {

			// 满足报警需求 保存到数据库存储队列
			ThVehicleAlarm thVehicleAlarm = new ThVehicleAlarm();
			// 更新超速报警
			if (isUpdate) {
				thVehicleAlarm.setVid(vehicleMessage.getVid());
				thVehicleAlarm.setAlarmStartUtc(alarmDataCache.getBeginOverSpeedTime());
				thVehicleAlarm.setAlarmEndUtc(alarmDataCache.getEndOverSpeedTime());
				thVehicleAlarm.setEndLat(vehicleMessage.getLat());
				thVehicleAlarm.setEndLon(vehicleMessage.getLon());
				thVehicleAlarm.setEndElevation(0l);
				thVehicleAlarm.setEndDirection(0l);
				thVehicleAlarm.setEndMileage(0l);
				thVehicleAlarm.setEndOilTotal(0l);
				thVehicleAlarm.setEndGpsSpeed(Long.valueOf(vehicleMessage	.getSpeed()));
				thVehicleAlarm.setAlarmCode("1");
				thVehicleAlarm.setIsUpdate(isUpdate);
				thVehicleAlarm.setEndMaplon(vehicleMessage.getMaplon());
				thVehicleAlarm.setEndMaplat(vehicleMessage.getMaplat());
				
				// 新增超速报警
			} else {
				//thVehicleAlarm.setAlarmId(vehicleMessage.getVid()+"1"+vehicleMessage.getUtc());
				thVehicleAlarm.setVid(vehicleMessage.getVid());
				thVehicleAlarm.setUtc(vehicleMessage.getUtc());
				thVehicleAlarm.setAlarmStartUtc(alarmDataCache.getBeginOverSpeedTime());
				thVehicleAlarm.setLat(vehicleMessage.getLat());
				thVehicleAlarm.setLon(vehicleMessage.getLon());
				thVehicleAlarm.setElevation(0l);
				thVehicleAlarm.setDirection(0l);
				thVehicleAlarm.setGpsSpeed(vehicleMessage.getSpeed());
				thVehicleAlarm.setAlarmCode("1");
				thVehicleAlarm.setMileage(0l);
				thVehicleAlarm.setOilTotal(0l);
				thVehicleAlarm.setAlarmSrc((short) 1);
				thVehicleAlarm.setAlarmAddInfoStart("3||2");
				thVehicleAlarm.setMaplon(vehicleMessage.getMaplon());
				thVehicleAlarm.setMaplat(vehicleMessage.getMaplat());
				
				
				// 新增记录开始时间
				thVehicleAlarm.setIsUpdate(isUpdate);
				
				
				String s=Base64_URl.base64Encode("围栏超速报警");
				String sendcommand="CAITS 0_0_0 "+vehicleMessage.getOemCode()+"_"+vehicleMessage.getCommanddr()+" 0 D_SNDM {TYPE:1,1:9,2:"+s+"} \r\n";
				DataPool.setSendPacketValue(sendcommand);
			}

			try {
				DataPool.setSaveDataPacket(thVehicleAlarm);
			} catch (InterruptedException e) {
				Thread.currentThread().interrupt();
				logger.error(e);

			}
		}

	}

	/**
	 * 新增进出围栏报警记录
	 * 
	 * @param isUpdate
	 *            是否更新
	 * @param alarmDataCache
	 * @param vehicleMessage
	 */
	private void addAreaOutOrInAlarm(AlarmDataCache alarmDataCache,
			VehicleMessage vehicleMessage) {

		if (alarmDataCache != null && vehicleMessage != null) {

			// 满足报警需求 保存到数据库存储队列
			ThVehicleAlarm thVehicleAlarm = new ThVehicleAlarm();

			thVehicleAlarm.setVid(vehicleMessage.getVid());
			thVehicleAlarm.setAlarmCode("20");
			thVehicleAlarm.setUtc(vehicleMessage.getUtc());
			thVehicleAlarm.setAlarmStartUtc(vehicleMessage.getUtc());
			thVehicleAlarm.setLat(vehicleMessage.getLat());
			thVehicleAlarm.setLon(vehicleMessage.getLon());
			thVehicleAlarm.setElevation(0l);
			thVehicleAlarm.setDirection(0l);
			thVehicleAlarm.setGpsSpeed(vehicleMessage.getSpeed());
			thVehicleAlarm.setMileage(0l);
			thVehicleAlarm.setOilTotal(0l);
			thVehicleAlarm.setAlarmSrc((short) 1);
			thVehicleAlarm.setMaplon(vehicleMessage.getMaplon());
			thVehicleAlarm.setMaplat(vehicleMessage.getMaplat());

String s="";
			// 进围栏报警
			if (alarmDataCache.isInOutAreaAlarm()) {
				thVehicleAlarm.setAlarmAddInfoStart("3||0");
				s=Base64_URl.base64Encode("进围栏报警");
				// 出围栏报警
			} else {
				thVehicleAlarm.setAlarmAddInfoStart("3||1");
				s=Base64_URl.base64Encode("出围栏报警");
			}
			String sendcommand="CAITS 0_0_0 "+vehicleMessage.getOemCode()+"_"+vehicleMessage.getCommanddr()+" 0 D_SNDM {TYPE:1,1:9,2:"+s+"} \r\n";
			DataPool.setSendPacketValue(sendcommand);
			try {
				DataPool.setSaveDataPacket(thVehicleAlarm);
			} catch (InterruptedException e) {
				Thread.currentThread().interrupt();
				logger.error(e);

			}
		}

	}

	/**
	 * 更新缓存中的持续报警信息
	 * 
	 * @param alarmDataCache
	 *            缓存原数据对象
	 * @param vehicleMessage
	 */
	private void modifySpeedCache(Integer type, AlarmDataCache alarmDataCache,
			VehicleMessage vehicleMessage) {

		if (type != null && alarmDataCache != null) {
			switch (type) {
			// 持续超速 已报警 更新
			case 1:
				// 更新最新时间
				alarmDataCache.setEndOverSpeedTime(vehicleMessage.getUtc());
				// 更新最新速度
				alarmDataCache.setEndSpeed(vehicleMessage.getSpeed());
				break;
			// 持续超速 上次未报警 本次上报 更新
			case 2:
				// 更新最新时间
				alarmDataCache.setEndOverSpeedTime(vehicleMessage.getUtc());
				// 更新最新速度
				alarmDataCache.setEndSpeed(vehicleMessage.getSpeed());
				// 超速已报警
				alarmDataCache.setSpeedAndTimeAlarm(true);
				break;
			// 持续超速 上次未报警 本次也未上报 更新
			case 3:
				// 更新最新时间
				alarmDataCache.setEndOverSpeedTime(vehicleMessage.getUtc());
				// 更新最新速度
				alarmDataCache.setEndSpeed(vehicleMessage.getSpeed());
				// 超速
				alarmDataCache.setSpeedingAlarm(true);
				break;
			case 4:
				// 更新开始时间
				alarmDataCache.setBeginOverSpeedTime(vehicleMessage.getUtc());
				alarmDataCache.setBeginOverSpeed(vehicleMessage.getSpeed());
				// 更新最新时间
				alarmDataCache.setEndOverSpeedTime(vehicleMessage.getUtc());
				// 更新最新速度
				alarmDataCache.setEndSpeed(vehicleMessage.getSpeed());
				// 超速
				alarmDataCache.setSpeedingAlarm(true);
				break;
			}

		}

	}

	/**
	 * 判断是否已保存持续超速报警（速度+持续时间）
	 * 
	 * @param beginTime
	 *            超速开始时间
	 * @param currentTime
	 *            超速当前时间
	 * @param thresholdTime
	 *            超速持续时间阀值 （秒）
	 * @return true 持续超速报警 false 未达到报警条件
	 */
	private boolean checkOverSpeedAlarm(Long beginTime, Long currentTime,
			Long thresholdTime) {

		if (beginTime != null && currentTime != null && thresholdTime != null) {
			// 秒转换毫秒
			if (currentTime - beginTime >= (thresholdTime * 1000)) {
				return true;
			} else {
				return false;
			}

		} else {
			return false;
		}

	}

	/**
	 * 判断是否超速 （只判断speed 不判断持续时间）
	 * 
	 * @param thresholdSpeed
	 *            超速阀值
	 * @param speed
	 *            当前记录速度
	 * @return true超速 false不超速
	 */
	private boolean checkOverspeed(Long thresholdSpeed, Long speed) {

		if (speed != null && thresholdSpeed != null) {
			if (speed > thresholdSpeed) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}

	}

	/**
	 * 得到本车本围栏的缓存信息
	 */
	private AlarmDataCache getAlarmCache(Long vid, Long areaId) {

		String key = null;

		if (vid != null && areaId != null) {
			key = String.valueOf(vid) + "_" + String.valueOf(areaId);
			return alarmMap.get(key);
		} else {
			return null;
		}

	}

	/**
	 * 判断是否在围栏内
	 * 
	 * @param area
	 * @param vehicleMessage
	 * @return
	 */
	private boolean isInArea(AreaDataObject area, VehicleMessage vehicleMessage) {

		boolean result=PoiUtil.PoiInPoly(area.getLonlats(), vehicleMessage.getLon()+","+
		 vehicleMessage.getLat());
		
		 return result;

	}

	/**
	 * 判断是否在围栏判定时间周期内
	 */
	private boolean isInCheckTime(AreaDataObject area,
			VehicleMessage vehicleMessage) {

		// 设置了围栏判定时间周期
		if (area.getBeginTime() != null && area.getEndTime() != null) {
			if (vehicleMessage.getUtc() >= area.getBeginTime()
					&& vehicleMessage.getUtc() <= area.getEndTime()) {
				return true;
			} else {
				return false;
			}
		} else {
			// 未设置
			return false;
		}

	}

}
