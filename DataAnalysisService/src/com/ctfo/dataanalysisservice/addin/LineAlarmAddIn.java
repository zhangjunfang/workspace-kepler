package com.ctfo.dataanalysisservice.addin;

import java.util.List;
import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.log4j.Logger;

import com.ctfo.dataanalysisservice.DataAnalysisServiceMain;
import com.ctfo.dataanalysisservice.beans.LineAlarmBean;
import com.ctfo.dataanalysisservice.beans.PlatAlarmTypeUtil;
import com.ctfo.dataanalysisservice.beans.SectionsDataObject;
import com.ctfo.dataanalysisservice.beans.ThVehicleAlarm;
import com.ctfo.dataanalysisservice.beans.VehicleMessage;
import com.ctfo.dataanalysisservice.gis.PoiUtil;
import com.ctfo.dataanalysisservice.io.DataPool;
import com.ctfo.dataanalysisservice.mem.MemManager;
import com.ctfo.dataanalysisservice.util.Base64_URl;

/**
 * 偏移线路报警处理
 * 
 * @author yangjian
 * 
 */
public class LineAlarmAddIn extends Thread implements IaddIn {

	private static final Logger logger = Logger.getLogger(LineAlarmAddIn.class);

	// 待处理数据队列
	private ArrayBlockingQueue<VehicleMessage> vPacket = new ArrayBlockingQueue<VehicleMessage>(
			100000);

	private String name;

	public LineAlarmAddIn() {
		name = UUID.randomUUID().toString();
		// 记录线程数
		DataAnalysisServiceMain.threadCount++;
	}

	public void run() {

		while (true) {
			try {
				// 获得要处理的位置信息数据
				VehicleMessage vehicleMessage = getPacket();

				if (vehicleMessage != null) {
					logger.debug("偏移线路报警数据：" + vehicleMessage.getCommanddr());

					// 与缓存判断是否告警信息

					this.checkPacket(vehicleMessage);

				}  
			} catch ( Exception e) {
				e.printStackTrace();
			}
		}

	}

	/**
	 * 检查上报的位置是否偏移路线
	 * 
	 * @param vehicleMessage
	 *            上报的位置信息
	 * @return boolean true:已经偏移 false:没有偏移
	 */
	private boolean checkPacket(VehicleMessage vehicleMessage) {
		
		//vehicleMessage.setUtc(System.currentTimeMillis());
		Long vid = vehicleMessage.getVid();
		// Long lineId = vehicleMessage.

		try {
			// 从缓存获取车辆的线路信息
			List<SectionsDataObject> returnData = this.getVehicleLineFromCacheByVid(vid);

			// 取出上次缓存的偏移信息
			LineAlarmBean lineAlarmCache = MemManager
					.getTempLineMap(PlatAlarmTypeUtil.KEY_WORD + vid);
			if (null == lineAlarmCache) {
				lineAlarmCache = new LineAlarmBean();
			}

			if (null != returnData) 
			{

				// 遍历车辆的线路缓存信息，判断车辆是否偏移路线
				for (int i = 0; i < returnData.size(); i++) 
				{
					SectionsDataObject data = returnData.get(i);// 取出每一条线路信进行比对

					// 对比上报的时间和线路的有效时间段 在？“进行下面的操作”：“清空
					// (超时开始时间=null,阀值布尔值=false,更新时间=null)+ 更新缓存中属于报警的，更新报警结束时间”
					if (!(vehicleMessage.getUtc() >= data.getValidStartTime() && vehicleMessage
							.getUtc() <= data.getValidEndTime())) {
						// --> 【上报时间不在线段有效时间范围内，清空缓存的信息，已经上报报警的，更新报警结束时间】

						// 判断上次是否报过警
						if (lineAlarmCache.isToAlarmed()) 
						{
							// 已经上报过警的，更新 报警的结束时间
							lineAlarmCache.setShiftUpdateTime(String.valueOf(vehicleMessage.getUtc()));
							MemManager.setTempLineMap(PlatAlarmTypeUtil.KEY_WORD + vid,lineAlarmCache);
						}

						lineAlarmCache.setToAlarmed(false);
						lineAlarmCache.setOverSpeed(false);
						lineAlarmCache.setShifted(false);
						lineAlarmCache.setOverSpeedStartTime(null);
						lineAlarmCache.setOverSpeedUpdateTime(null);
						lineAlarmCache.setOverSpeedEndTime(null);
						MemManager.setTempLineMap(PlatAlarmTypeUtil.KEY_WORD+ vid, lineAlarmCache);

					} else 
					{
						// -->【上报时间在线段有效时间范围内，进行下面的操作】

						// 判断车辆时候在线路内（lat,lon）
						
						String line=data.getStartLon()+","+data.getStartLat()+" "+data.getEndLon()+","+data.getEndLat();//线路的开始和结束的经纬度
						String distance=data.getWight();//路宽
						String point=vehicleMessage.getLon()+","+vehicleMessage.getLat();//目标经纬度
						
						boolean isshift = PoiUtil.PoiInLineBuffer(line, distance, point);// true:不偏移     false:偏移

						// 判断车辆是否偏移
						if (isshift) 
						{
							// 本次不偏移，上次偏移了
							if (lineAlarmCache.isShifted()) 
							{
								// 偏移结束，更新数据库，加上偏移结束时间
								ThVehicleAlarm thVehicleAlarm = new ThVehicleAlarm();

								thVehicleAlarm.setAlarmEndUtc(vehicleMessage.getUtc());
								thVehicleAlarm.setEndLat(vehicleMessage.getLat());
								thVehicleAlarm.setEndLon(vehicleMessage.getLon());
								thVehicleAlarm.setEndElevation(0L);// 海拔暂缺
								thVehicleAlarm.setEndDirection(0L);// 方向暂缺
								thVehicleAlarm.setEndGpsSpeed(vehicleMessage.getSpeed());
								thVehicleAlarm.setEndMileage(0L);// 里程暂缺
								thVehicleAlarm.setEndOilTotal(0L);// 油耗暂缺
								//thVehicleAlarm.setAlarmId(vid + "_"+ vehicleMessage.getOemCode() + "_"+ lineAlarmCache.getShiftStartTime());
								thVehicleAlarm.setVid(vid);
								thVehicleAlarm.setAlarmCode("23");
								thVehicleAlarm.setAlarmStartUtc(Long.parseLong( lineAlarmCache.getShiftStartTime()));
								thVehicleAlarm.setIsUpdate(true);
								thVehicleAlarm.setMaplat(vehicleMessage.getMaplat());
								thVehicleAlarm.setMaplon(vehicleMessage.getMaplon());
								DataPool.setSaveDataPacket(thVehicleAlarm);
							}

							// 更新缓存中保存本次不偏移的状态
							lineAlarmCache.setShiftStartTime(null);// 清空偏移的开始时间和结束时间
							lineAlarmCache.setShiftEndTime(null);
							lineAlarmCache.setShifted(false);
							MemManager.setTempLineMap(PlatAlarmTypeUtil.KEY_WORD + vid,lineAlarmCache);

							// 超速处理 start
							// --------------------------------------------

							// 判断车辆是否与缓存中的线路段一致
							if (null!=lineAlarmCache.getSectionsId() && lineAlarmCache.getSectionsId().equals(data.getSectionsId()))
							{
								// -->【如果一致，进行超速判断】

								// 判断本次是否超速 （取出缓存是否到达过阀值布尔值）
								if (vehicleMessage.getSpeed() > data.getLimitSpeed()) 
								{
									// -->【如果本次超速】

									// 上次超速且本次超速 （更新缓存，更新时间，速度）
									// 【超时持续时间是否到达阀值？（判断阀值布尔值？“只更新”：“上报+更新”）】
									if (lineAlarmCache.isOverSpeed()) 
									{
										// 获取超速开始时间
										long overSpeedStartTime = Long.valueOf(lineAlarmCache.getOverSpeedStartTime());
										// 获取超速上报时间
										long alarmTime = vehicleMessage.getUtc();

										// 判断报警持续时间是否达到阀值
										if (alarmTime - overSpeedStartTime > data.getOverSpeedTimer()*1000) 
										{
											// —->【已达到阀值】
											// 判断上次的超速是否已经发出报警
											if (lineAlarmCache.isToAlarmed()) 
											{
												// 已经报过警，因此只更新
												lineAlarmCache.setShiftUpdateTime(String.valueOf(vehicleMessage.getUtc()));
												MemManager.setTempLineMap(PlatAlarmTypeUtil.KEY_WORD+ vid,lineAlarmCache);
											} 
											else 
											{
												// -->尚未报过警，那么 【上报报警+更新状态】

												// 上报报警
												ThVehicleAlarm thVehicleAlarm = new ThVehicleAlarm();
												//thVehicleAlarm.setAlarmId(vid+ "_"+ vehicleMessage.getOemCode()+ "_"+ vehicleMessage.getUtc());
												thVehicleAlarm.setVid(vehicleMessage.getVid());
												thVehicleAlarm.setUtc(vehicleMessage.getUtc());
												thVehicleAlarm.setLat(vehicleMessage.getLat());
												thVehicleAlarm.setLon(vehicleMessage.getLon());
												thVehicleAlarm.setElevation(0L);// 海拔暂缺
												thVehicleAlarm.setDirection(0L);// 方向暂缺
												thVehicleAlarm.setGpsSpeed(Long.valueOf(vehicleMessage.getSpeed()));
												thVehicleAlarm.setAlarmCode(vehicleMessage.getOemCode());
												thVehicleAlarm.setSysutc(System.currentTimeMillis());
												thVehicleAlarm.setAlarmStartUtc(vehicleMessage.getUtc());
												thVehicleAlarm.setAlarmDriver(1L);// 驾驶员暂缺
												thVehicleAlarm.setMileage(0L);// 里程暂缺
												thVehicleAlarm.setOilTotal(0L);// 油耗暂缺
												thVehicleAlarm.setVehicleNo("");// 车牌号码暂缺
												thVehicleAlarm.setAlarmSrc(Short.parseShort("2"));// 报警信息来源（1：车载终端，2：企业监控平台，3：政府监管平台，9：其它）
												thVehicleAlarm.setAlarmAddInfoStart("4||2");
												thVehicleAlarm.setAlarmCode("1");
												thVehicleAlarm.setIsUpdate(false);
												thVehicleAlarm.setMaplat(vehicleMessage.getMaplat());
												thVehicleAlarm.setMaplon(vehicleMessage.getMaplon());
												DataPool.setSaveDataPacket(thVehicleAlarm);
												
												String s=Base64_URl.base64Encode("路线超速报警");
												String sendcommand="CAITS 0_0_0 "+vehicleMessage.getOemCode()+"_"+vehicleMessage.getCommanddr()+" 0 D_SNDM {TYPE:1,1:9,2:"+s+"} \r\n";
												DataPool.setSendPacketValue(sendcommand);

												// 更新状态
												lineAlarmCache.setToAlarmed(true);
												lineAlarmCache.setOverSpeedStartTime(String.valueOf(vehicleMessage.getUtc()));
												lineAlarmCache.setOverSpeedUpdateTime(String.valueOf(vehicleMessage.getUtc()));
												MemManager.setTempLineMap(PlatAlarmTypeUtil.KEY_WORD+ vid,lineAlarmCache);
											}

										} else 
										{
											// —->【未达到阀值，只更新时间】
											lineAlarmCache.setOverSpeedUpdateTime(String.valueOf(vehicleMessage.getUtc()));
											MemManager.setTempLineMap(PlatAlarmTypeUtil.KEY_WORD+ vid,lineAlarmCache);
										}
									} else 
									{
										// --> 【上次未超速而本次超速
										// （只更新缓存，加上本次超速状态，GPS速度，超时开始时间，更新时间，阀值布尔值=false）】
										lineAlarmCache.setOverSpeed(true);
										lineAlarmCache.setToAlarmed(false);
										lineAlarmCache.setOverSpeedStartTime(String.valueOf(vehicleMessage.getUtc()));
										lineAlarmCache.setOverSpeedUpdateTime(String.valueOf(vehicleMessage.getUtc()));
										MemManager.setTempLineMap(PlatAlarmTypeUtil.KEY_WORD+ vid, lineAlarmCache);
									}

								} else {

									// --> 【上次超速 ，本次未超速
									// （1.已经上报了（更新报警结束时间，超时开始时间=null,阀值布尔值=false,更新时间=null）
									// 2.未上报
									// (超时开始时间=null,阀值布尔值=false,更新时间=null)）】
									if (lineAlarmCache.isOverSpeed()) {
										// 判断报警是否已经上报
										if (lineAlarmCache.isToAlarmed()) {
											// 报警已经上报
											// 更新数据库该报警的结束时间
											ThVehicleAlarm thVehicleAlarm = new ThVehicleAlarm();
											thVehicleAlarm.setAlarmEndUtc(vehicleMessage.getUtc());
											thVehicleAlarm.setEndLat(vehicleMessage.getLat());
											thVehicleAlarm.setEndLon(vehicleMessage.getLon());
											thVehicleAlarm.setEndElevation(0L);// 海拔暂缺
											thVehicleAlarm.setEndDirection(0L);// 方向暂缺
											thVehicleAlarm.setEndGpsSpeed(vehicleMessage.getSpeed());
											thVehicleAlarm.setEndMileage(0L);// 里程暂缺
											thVehicleAlarm.setEndOilTotal(0L);// 油耗暂缺
											//thVehicleAlarm.setAlarmId(vid+ "_"+ vehicleMessage.getOemCode() + "_"+ lineAlarmCache.getOverSpeedStartTime());
											thVehicleAlarm.setVid(vid);
											thVehicleAlarm.setAlarmCode("1");
											thVehicleAlarm.setAlarmStartUtc(Long.parseLong( lineAlarmCache.getShiftStartTime()));
											thVehicleAlarm.setIsUpdate(true);
											thVehicleAlarm.setMaplat(vehicleMessage.getMaplat());
											thVehicleAlarm.setMaplon(vehicleMessage.getMaplon());
											DataPool.setSaveDataPacket(thVehicleAlarm);
										}

										// 更新缓存，将车辆置为未超速状态
										lineAlarmCache.setToAlarmed(false);
										lineAlarmCache.setOverSpeed(false);
										lineAlarmCache.setOverSpeedStartTime(null);
										lineAlarmCache.setOverSpeedUpdateTime(null);
										MemManager.setTempLineMap(PlatAlarmTypeUtil.KEY_WORD+ vid, lineAlarmCache);
									}
								}

							} else {
								// --> 【如果不一致，初始化缓存中的车辆状态，因为车辆时分段限速的】
								lineAlarmCache.setSectionsId(data.getSectionsId());//车辆进入新的路段 2012-03-06
								// 判断上次是否已经发出报警
								if (lineAlarmCache.isToAlarmed()) 
								{
									// 更新数据库该报警的结束时间
									ThVehicleAlarm thVehicleAlarm = new ThVehicleAlarm();
									thVehicleAlarm.setAlarmEndUtc(vehicleMessage.getUtc());
									thVehicleAlarm.setEndLat(vehicleMessage.getLat());
									thVehicleAlarm.setEndLon(vehicleMessage.getLon());
									thVehicleAlarm.setEndElevation(0L);// 海拔暂缺
									thVehicleAlarm.setEndDirection(0L);// 方向暂缺
									thVehicleAlarm.setEndGpsSpeed(vehicleMessage.getSpeed());
									thVehicleAlarm.setEndMileage(0L);// 里程暂缺
									thVehicleAlarm.setEndOilTotal(0L);// 油耗暂缺
									//thVehicleAlarm.setAlarmId(vid + "_"+ vehicleMessage.getOemCode() + "_"+ lineAlarmCache.getOverSpeedStartTime());
									thVehicleAlarm.setVid(vid);
									thVehicleAlarm.setAlarmCode("1");
									thVehicleAlarm.setAlarmStartUtc(Long.parseLong( lineAlarmCache.getShiftStartTime()));
									thVehicleAlarm.setIsUpdate(true);
									thVehicleAlarm.setMaplat(vehicleMessage.getMaplat());
									thVehicleAlarm.setMaplon(vehicleMessage.getMaplon());
									DataPool.setSaveDataPacket(thVehicleAlarm);
								}

								// 判断本次是否超速
								if (vehicleMessage.getSpeed() > data.getLimitSpeed()) 
								{
									// 如果超速就缓存中记录本次报警的状态
									lineAlarmCache.setOverSpeed(true);
									lineAlarmCache.setOverSpeedStartTime(String.valueOf(vehicleMessage.getUtc()));
									lineAlarmCache.setOverSpeedUpdateTime(String.valueOf(vehicleMessage.getUtc()));
									MemManager.setTempLineMap(PlatAlarmTypeUtil.KEY_WORD + vid,lineAlarmCache);
								} 
								else 
								{
									lineAlarmCache.setOverSpeed(false);
									lineAlarmCache.setOverSpeedStartTime(null);
									lineAlarmCache.setOverSpeedUpdateTime(null);
									lineAlarmCache.setOverSpeedEndTime(null);
									MemManager.setTempLineMap(PlatAlarmTypeUtil.KEY_WORD + vid,lineAlarmCache);
								}

							}

							// 超速处理 end
							// ----------------------------------------------------

							break;
						} else {
							// 遍历到了最后一条，确定本次确实存在偏移
							if (i == returnData.size() - 1) {
								// 判断上次是否偏移
								if (!lineAlarmCache.isShifted()) {
									// 如果上次不存在偏移，上报报警
									ThVehicleAlarm thVehicleAlarm = new ThVehicleAlarm();
									//thVehicleAlarm.setAlarmId(vid + "_"+ vehicleMessage.getOemCode() + "_"+ vehicleMessage.getUtc());
									thVehicleAlarm.setVid(vehicleMessage.getVid());
									thVehicleAlarm.setUtc(vehicleMessage.getUtc());
									thVehicleAlarm.setLat(vehicleMessage.getLat());
									thVehicleAlarm.setLon(vehicleMessage.getLon());
									thVehicleAlarm.setElevation(0L);// 海拔暂缺
									thVehicleAlarm.setDirection(0L);// 方向暂缺
									thVehicleAlarm.setGpsSpeed(Long.valueOf(vehicleMessage.getSpeed()));
									thVehicleAlarm.setAlarmCode("23");
									thVehicleAlarm.setSysutc(System.currentTimeMillis());
									thVehicleAlarm.setAlarmStartUtc(Long.parseLong( lineAlarmCache.getShiftStartTime()));
									thVehicleAlarm.setAlarmDriver(0L);// 驾驶员暂缺
									thVehicleAlarm.setMileage(0L);// 里程暂缺
									thVehicleAlarm.setOilTotal(0L);// 油耗暂缺
									thVehicleAlarm.setVehicleNo("");// 车牌号码暂缺
									thVehicleAlarm.setAlarmSrc(Short.parseShort("2"));// 报警信息来源（1：车载终端，2：企业监控平台，3：政府监管平台，9：其它）
									thVehicleAlarm.setIsUpdate(false);
									thVehicleAlarm.setMaplat(vehicleMessage.getMaplat());
									thVehicleAlarm.setMaplon(vehicleMessage.getMaplon());
									DataPool.setSaveDataPacket(thVehicleAlarm);

									String s=Base64_URl.base64Encode("偏移路线报警");
									String sendcommand="CAITS 0_0_0 "+vehicleMessage.getOemCode()+"_"+vehicleMessage.getCommanddr()+" 0 D_SNDM {TYPE:1,1:9,2:"+s+"} \r\n";
									DataPool.setSendPacketValue(sendcommand);
									
									// 缓存记录本次报警信息
									lineAlarmCache.setShifted(true);// 修改为偏移
									lineAlarmCache.setShiftStartTime(String.valueOf(vehicleMessage.getUtc()));
									lineAlarmCache.setShiftUpdateTime(String.valueOf(vehicleMessage.getUtc()));

									// 更新偏移状态，开始时间和结束时间
									MemManager.setTempLineMap(
											PlatAlarmTypeUtil.KEY_WORD + vid,
											lineAlarmCache);

								} else {
									// 上次也存在偏移,更新时间，速度
									lineAlarmCache.setShiftUpdateTime(String.valueOf(vehicleMessage.getUtc()));
									MemManager.setTempLineMap(
											PlatAlarmTypeUtil.KEY_WORD + vid,
											lineAlarmCache);
								}
							}
						}
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return false;
	}

	/**
	 * 通过Vid获取车辆在缓存中的线路信息
	 * 
	 * @param client
	 *            MemcachedClient客户端
	 * @param vid
	 *            车辆ID
	 * @return List<SectionsDataObject> 该车辆的线路信息
	 */
	private List<SectionsDataObject> getVehicleLineFromCacheByVid(Long vid) {
		List<SectionsDataObject> returnData = null;
		if (null != vid) {
			//DAS50031_1
			String key=PlatAlarmTypeUtil.KEY_WORD + vid+ "_" + PlatAlarmTypeUtil.PLAT_DEVIATE_LINE_ALARM;
			returnData = MemManager.getLineMap(key);

		}
		return returnData;
	}

	/**
	 * 单元测试
	 * 
	 * @param args
	 */
	public static void main(String[] args) {

		try {
			// 测试开始

			VehicleMessage vehicleMessage = new VehicleMessage();

			vehicleMessage.setAlarm(true);
			vehicleMessage.setAlarmType("pianyi");
			vehicleMessage.setCommanddr("13689249318");
			vehicleMessage.setLat(11111111111L);
			vehicleMessage.setLon(22222222222L);
			vehicleMessage.setOemCode("CC5C");
			vehicleMessage.setSeq("123456789");
			vehicleMessage.setSpeed(80L);
			vehicleMessage.setUtc(160L);
			vehicleMessage.setUuid("ERTY654643");
			vehicleMessage.setVid(100200300L);

			LineAlarmAddIn in = new LineAlarmAddIn();

			in.checkPacket(vehicleMessage);

			// 初始化Memcached信息
			/*
			 * LineAlarmBean lineAlarmCache=new LineAlarmBean();
			 * lineAlarmCache.setOverSpeed(true);
			 * lineAlarmCache.setOverSpeedEndTime(null);
			 * lineAlarmCache.setOverSpeedStartTime("120");
			 * lineAlarmCache.setOverSpeedUpdateTime("120");
			 * lineAlarmCache.setSectionsId("1234");
			 * lineAlarmCache.setShifted(false);
			 * lineAlarmCache.setShiftEndTime(null);
			 * lineAlarmCache.setShiftStartTime("120");
			 * lineAlarmCache.setShiftUpdateTime("120");
			 * lineAlarmCache.setToAlarmed(true);
			 * lineAlarmCache.setVid_lineId("100200300_1024");
			 * 
			 * boolean bb=newclient.set(PlatAlarmTypeUtil.KEY_WORD +
			 * "100200300",0,lineAlarmCache);
			 * System.out.println("--------------"+bb); LineAlarmBean
			 * lineAlarmCache2
			 * =(LineAlarmBean)newclient.get(PlatAlarmTypeUtil.KEY_WORD +
			 * "100200300");
			 * System.out.println("--------------"+lineAlarmCache2.
			 * getVid_lineId());
			 */

			/*
			 * SectionsDataObject obj=new SectionsDataObject();
			 * obj.setBusinessType("123"); obj.setEndLat(1111111111111L);
			 * obj.setEndLon(2222222222222L); obj.setEndPoint(100L);
			 * obj.setId("1"); obj.setLat(333333333333L);
			 * obj.setLon(444444444444L); obj.setLimitSpeed(120);
			 * obj.setLineId("ling_100"); obj.setMaxSpeed("125");
			 * obj.setMaxSpeedTime("60"); obj.setOverSpeedTimer(70);
			 * obj.setSectionsId("123"); obj.setStartLat(111111111111L);
			 * obj.setStartLon(222222222222L); obj.setStartPoint(1L);
			 * obj.setVid("100200300"); obj.setWight("50");
			 * 
			 * SectionsDataObject obj2=new SectionsDataObject();
			 * obj2.setBusinessType("321"); obj2.setEndLat(5555555555555L);
			 * obj2.setEndLon(6666666666666L); obj2.setEndPoint(200L);
			 * obj2.setId("2"); obj2.setLat(777777777777L);
			 * obj2.setLon(888888888888L); obj2.setLimitSpeed(80);
			 * obj2.setLineId("ling_100"); obj2.setMaxSpeed("90");
			 * obj2.setMaxSpeedTime("120"); obj2.setOverSpeedTimer(120);
			 * obj2.setSectionsId("124"); obj2.setStartLat(999999999999L);
			 * obj2.setStartLon(999999999998L); obj2.setStartPoint(1L);
			 * obj2.setVid("100200300"); obj2.setWight("50");
			 * 
			 * //List<SectionsDataObject> list=new
			 * ArrayList<SectionsDataObject>(); //list.add(obj);
			 * //list.add(obj2);
			 * 
			 * //boolean bb=newclient.set(PlatAlarmTypeUtil.KEY_WORD +
			 * "100200300" + "_" +
			 * PlatAlarmTypeUtil.PLAT_DEVIATE_LINE_ALARM,0,list);
			 * //System.out.println("--------------"+bb);
			 * 
			 * List<SectionsDataObject>
			 * list2=newclient.get(PlatAlarmTypeUtil.KEY_WORD + "100200300" +
			 * "_" + PlatAlarmTypeUtil.PLAT_DEVIATE_LINE_ALARM);
			 * System.out.println("--------------"+list2.size());
			 */

		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	public void addPacket(VehicleMessage vehicleMessage)
			throws InterruptedException {

		// logger.info(name+"LineAlarmAddIn"+vehicleMessage.getVid()+"size"+getPacketsSize());
		if (vehicleMessage != null) {
			// vPacket.put(vehicleMessage);
			vPacket.put(vehicleMessage);
			// System.out.println(getPacketsSize());
		}
	}

	public int getPacketsSize() {
		return vPacket.size();
	}

	public VehicleMessage getPacket() throws InterruptedException {
		// return vPacket.poll();
		return vPacket.take();
	}

}
