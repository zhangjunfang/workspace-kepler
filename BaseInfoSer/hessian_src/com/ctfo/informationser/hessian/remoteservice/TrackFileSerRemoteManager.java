/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.hessian.remoteservice;

import com.ctfo.informationser.hessian.remote.HessianRemoteManager;



/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ：monitorser <br>
 * 功能：业务支撑服务远程hession接口 <br>
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
 * <td>2011-9-20</td>
 * <td>wangpeng</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author wangpeng
 * @since JDK1.6
 */
public interface TrackFileSerRemoteManager extends HessianRemoteManager {

	/**
	 * 远程hession提供的springbeanid
	 */
	// 读取轨迹文件service
	static String BEAN_ID_READTRACKFILESERVICE = "readTrackFileServiceRmi";

	
	/**
	 * 描述：远程hession提供的 用户关联报警级别 根据用户id查询报警级别list
	 * 
	 */
	
	// 读取轨迹文件方法
	static String METHOD_READTRACKFILESERVICE_READTRACKFILE = "readTrackFile";

	
}
