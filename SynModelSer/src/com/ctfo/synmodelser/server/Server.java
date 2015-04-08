package com.ctfo.synmodelser.server;

import com.ctfo.synmodelser.http.HttpSyn;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SynModelSer <br>
 * 功能：同步DB数据到Memcached中，根据默认配置信息加载 <br>
 * 描述： 同步DB数据到Memcached中，根据默认配置信息加载<br>
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
 * <td>2012-2-19</td>
 * <td>DEVELOP</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author DEVELOP
 * @since JDK1.6
 */
public class Server {
	public static void main(String[] args) {
		HttpSyn syn = new HttpSyn();
		syn.getSynData();
	}
}
