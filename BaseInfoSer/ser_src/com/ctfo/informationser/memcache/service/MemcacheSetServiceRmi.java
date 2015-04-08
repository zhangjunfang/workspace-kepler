package com.ctfo.informationser.memcache.service;

import com.ctfo.informationser.annotations.AnnotationName;
import com.ctfo.informationser.basic.service.RemoteJavaServiceRmi;
import com.ctfo.local.exception.CtfoAppException;

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
public interface MemcacheSetServiceRmi extends RemoteJavaServiceRmi {

	/**
	 * 路况信息
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "路况信息")
	public void setRoadCondition() throws CtfoAppException;

	/**
	 * 公告信息
	 * 
	 * @param infoType
	 *            001/系统公告， 002/企业公告，003/政策法规，004/行业快讯，005/企业资讯
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "公告信息")
	public void setTbPublishInfo(String infoType) throws CtfoAppException;

	/**
	 * 信息反馈
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "信息反馈")
	public void setTbFeedback() throws CtfoAppException;

	/**
	 * 入网车辆数、在线车辆数、接入企业数
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "入网车辆数、在线车辆数、接入企业数")
	public void setVehicleNum() throws CtfoAppException;

	/**
	 * 企业接入车辆数、企业在线车辆数、企业在线行驶车辆数
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "企业接入车辆数、企业在线车辆数、企业在线行驶车辆数")
	public void setVehicleCoprNum() throws CtfoAppException;

	/**
	 * 车辆排行榜
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "车辆排行榜")
	public void setVehicleTop() throws CtfoAppException;

	/**
	 * 车队排行榜
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "车队排行榜")
	public void setVehicleTeamTop() throws CtfoAppException;
	
	/**
	 * 企业咨询和企业公告
	 * 
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "企业咨询和企业公告")
	public void setTbComPublishInfo(String type, String infoTypeStr) throws CtfoAppException; 
	
	/**
	 * 系统公告
	 * @param type
	 * @throws CtfoAppException
	 */
	@AnnotationName(name = "系统公告")
	public void setSystemAnnouncement(String type)throws CtfoAppException; 
}
