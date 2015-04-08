package com.ctfo.informationser.memcache.service;

import java.util.List;
import java.util.Map;

import com.ctfo.combusiness.beans.TbFeedback;
import com.ctfo.combusiness.beans.TbPublishInfo;
import com.ctfo.informationser.annotations.AnnotationName;
import com.ctfo.informationser.basic.service.RemoteJavaServiceRmi;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.memcache.beans.GradeMonthstat;
import com.ctfo.memcache.beans.VehicleNum;
import com.ctfo.portalmng.beans.RoadCondition;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
 * 描述： <br>
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
 * <td>2011-11-15</td>
 * <td>zhangming</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author zhangming
 * @since JDK1.6
 */
@AnnotationName(name = "缓存管理")
public interface MemcacheGetServiceRmi extends RemoteJavaServiceRmi {

	/**
	 * 路况信息
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "路况信息")
	public Map<String, List<RoadCondition>> getRoadCondition() throws CtfoAppException;

	/**
	 * 公告信息
	 * 
	 * @param infoType
	 *            001/系统公告， 002/企业公告，003/政策法规，004/行业快讯
	 * @param entId
	 *            组织id
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "公告信息")
	public List<TbPublishInfo> getTbPublishInfo(String infoType, String entId) throws CtfoAppException;

	/**
	 * 信息反馈
	 * 
	 * @param entId
	 *            组织id
	 * @return
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "信息反馈")
	public List<TbFeedback> getTbFeedback(String entId) throws CtfoAppException;

	/**
	 * 入网车辆数、在线车辆数、接入企业数
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "入网车辆数、在线车辆数、接入企业数")
	public VehicleNum getVehicleNum() throws CtfoAppException;

	/**
	 * 企业接入车辆数、企业在线车辆数、企业在线行驶车辆数
	 * 
	 * @param entId
	 *            组织id
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "企业接入车辆数、企业在线车辆数、企业在线行驶车辆数")
	public VehicleNum getVehicleCoprNum(String entId) throws CtfoAppException;

	/**
	 * 车辆排行榜
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "车辆排行榜")
	public List<GradeMonthstat> getVehicleTop(String entId) throws CtfoAppException;

	/**
	 * 车队排行榜
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "车队排行榜")
	public List<GradeMonthstat> getVehicleTeamTop(String entId) throws CtfoAppException;
	
	/**
	 * 企业公告、企业资讯
	 * 
	 * @param infoType
	 *            0002/企业公告，005/企业咨询
	 * @param entId
	 *            组织id
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "公告信息")
	public List<TbPublishInfo> getTbComPublishInfo(String infoType, String entId) throws CtfoAppException;
	
	/**
	 * 系统公告
	 * @param type
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "系统公告")
	public List<TbPublishInfo> getSystemAnnouncement(String type)throws CtfoAppException; 
}
