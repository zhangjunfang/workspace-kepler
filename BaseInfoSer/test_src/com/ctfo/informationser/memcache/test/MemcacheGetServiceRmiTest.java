package com.ctfo.informationser.memcache.test;

import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

import org.springframework.context.support.ClassPathXmlApplicationContext;

import com.ctfo.combusiness.beans.TbFeedback;
import com.ctfo.combusiness.beans.TbPublishInfo;
import com.ctfo.informationser.memcache.service.MemcacheGetServiceRmi;
import com.ctfo.informationser.test.util.GeneralTestBase;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.memcache.beans.GradeMonthstat;
import com.ctfo.memcache.beans.StaticMemcache;
import com.ctfo.memcache.beans.VehicleNum;
import com.ctfo.portalmng.beans.RoadCondition;

public class MemcacheGetServiceRmiTest extends GeneralTestBase {

	private ClassPathXmlApplicationContext classPath = GeneralTestBase.getClassXmlContext();

	private MemcacheGetServiceRmi memcacheGetServiceRmi = (MemcacheGetServiceRmi) classPath.getBean("memcacheGetServiceRmi");

	/**
	 * 路况信息
	 * 
	 * @throws CtfoAppException
	 */
	public void testGetRoadCondition() {
		Map<String, List<RoadCondition>> map = memcacheGetServiceRmi.getRoadCondition();
		for (Entry<String, List<RoadCondition>> entry : map.entrySet()) {
			System.out.println(entry.getKey());
			for (RoadCondition roadCondition : entry.getValue()) {
				System.out.println(roadCondition.getRoadConditionTime());
				System.out.println(roadCondition.getDescriptions());
			}
			System.out.println("=========================================");
		}
	}

	/**
	 * 公告信息
	 * 
	 * @param infoType
	 *            001/系统公告， 002/企业公告，003/政策法规，004/行业快讯
	 * @param entId
	 *            组织id
	 * @throws CtfoAppException
	 */
	public void testGetTbPublishInfo() {
		String infoType = StaticMemcache.TBPUBLISHINFO_INFOTYPE_001;
		String entId = StaticMemcache.TBPUBLISHINFO_SYSTEM;
		List<TbPublishInfo> list = memcacheGetServiceRmi.getTbPublishInfo(infoType, entId);
		for (TbPublishInfo tbPublishInfo : list) {
			System.out.println(tbPublishInfo.getEntId());
		}
	}

	/**
	 * 信息反馈
	 * 
	 * @param entId
	 *            组织id
	 * @return
	 * @throws CtfoAppException
	 */
	public void testGetTbFeedback() {
		String entId = "1";
		List<TbFeedback> list = memcacheGetServiceRmi.getTbFeedback(entId);
		for (TbFeedback tbFeedback : list) {
			System.out.println(tbFeedback.getEntId());
		}
	}

	/**
	 * 入网车辆数、在线车辆数、接入企业数
	 * 
	 * @throws CtfoAppException
	 */
	public void testGetVehicleNum() {
		VehicleNum vehicleNum = memcacheGetServiceRmi.getVehicleNum();
		System.out.println(vehicleNum.getKey());
		System.out.println(vehicleNum.getVehicleNetworkNum());
		System.out.println(vehicleNum.getVehicleOnlineNum());
		System.out.println(vehicleNum.getCorpNetworkNum());
	}

	/**
	 * 企业接入车辆数、企业在线车辆数、企业在线行驶车辆数
	 * 
	 * @param entId
	 *            组织id
	 * 
	 * @throws CtfoAppException
	 */
	public void testGetVehicleCoprNum() {
		String entId = "1";
		VehicleNum vehicleNum = memcacheGetServiceRmi.getVehicleCoprNum(entId);
		System.out.println(vehicleNum.getKey());
		System.out.println(vehicleNum.getCorpVehicleNetworkNum());
		System.out.println(vehicleNum.getCorpVehicleOnlineNum());
		System.out.println(vehicleNum.getCorpVehicleOnlineDrivingNum());
	}

	/**
	 * 车辆排行榜
	 * 
	 * @throws CtfoAppException
	 */
	public void testGetVehicleTop() {
		String entId = "1";
		List<GradeMonthstat> list = memcacheGetServiceRmi.getVehicleTop(entId);
		for (GradeMonthstat gradeMonthstat : list) {
			System.out.println(gradeMonthstat.getSeqId());
			System.out.println(gradeMonthstat.getVid());
			System.out.println(gradeMonthstat.getVehicleNo());
			System.out.println(gradeMonthstat.getAllScoreSum());
		}
	}

	/**
	 * 车队排行榜
	 * 
	 * @throws CtfoAppException
	 */
	public void testGetVehicleTeamTop() {
		String entId = "1";
		List<GradeMonthstat> list = memcacheGetServiceRmi.getVehicleTeamTop(entId);
		for (GradeMonthstat gradeMonthstat : list) {
			System.out.println(gradeMonthstat.getSeqId());
			System.out.println(gradeMonthstat.getTeamId());
			System.out.println(gradeMonthstat.getTeamName());
			System.out.println(gradeMonthstat.getAllScoreSum());
		}
	}
}
