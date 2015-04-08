package com.ctfo.ypt.client;

import org.apache.mina.core.service.IoService;
import org.apache.mina.core.service.IoServiceListener;
import org.apache.mina.core.session.IdleStatus;
import org.apache.mina.core.session.IoSession;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： cpcs
 * <br>
 * 功能：
 * <br>
 * 描述：云平台连接监听类
 * <br>
 * 授权 : (C) Copyright (c) 2011
 * <br>
 * 公司 : 北京中交慧联信息科技有限公司
 * <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史
 * <br>
 * <table width="432" border="1">
 * <tr><td>版本</td><td>时间</td><td>作者</td><td>改变</td></tr>
 * <tr><td>1.0</td><td>2014年11月19日</td><td>Administrator</td><td>创建</td></tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font>
 * <br>
 * 
 * @version 1.0
 * 
 * @author 蒋东卿
 * @date 2014年11月19日下午4:09:30
 * @since JDK1.6
 */
public class IoListener implements IoServiceListener{

	@Override
	public void serviceActivated(IoService arg0) throws Exception {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void serviceDeactivated(IoService arg0) throws Exception {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void serviceIdle(IoService arg0, IdleStatus arg1) throws Exception {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void sessionCreated(IoSession arg0) throws Exception {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void sessionDestroyed(IoSession arg0) throws Exception {
		// TODO Auto-generated method stub
		
	}

}
