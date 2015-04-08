package com.ctfo.synmodelser.http;

import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.TimeoutException;

import net.rubyeye.xmemcached.MemcachedClient;
import net.rubyeye.xmemcached.exception.MemcachedException;

import com.ctfo.informationser.monitoring.beans.VehicleInfo;
import com.ctfo.synmodelser.jdbc.JdbcManager;
import com.ctfo.synmodelser.mem.MemManager;
import com.ctfo.synmodelser.server.AbstractModel;
import com.ctfo.synmodelser.server.ModelBulider;
import com.ctfo.synmodelser.util.Config;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SynModelSer <br>
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
 * <td>Feb 14, 2012</td>
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
public class HttpSynServiceImpl extends AbstractModel implements ModelBulider {

	private Connection conn = null;
	MemcachedClient client = null;
	private Config config;

	public HttpSynServiceImpl(Config config) {
		this.config = config;
	}

	public HttpSynServiceImpl() {
	}

	public HttpSynServiceImpl(String filePath) {
		this.config = new Config(filePath);
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.synmodelser.server.ModelBulider#bulider()
	 */
	@Override
	public ModelBulider bulider() {
		if (config != null) {
			conn = JdbcManager.getConnection(config);
			client = MemManager.getMemcachedClient(config);
			client.setOpTimeout(100000);
		} else {
			conn = JdbcManager.getConnection();
			client = MemManager.getMemcachedClient();
		}
		return this;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.synmodelser.server.ModelBulider#getSynData()
	 */
	@Override
	public int getSynData() {
		String sql = "SELECT T.AUTH_CODE, T.OEM_CODE,SS.COMMADDR FROM TB_TERMINAL T, TB_SIM SS, TR_SERVICEUNIT S WHERE T.TID = S.TID AND S.SID = SS.SID  AND SS.ENABLE_FLAG != 0 AND T.REG_STATUS != -1 and t.reg_status=0";
		try {
			PreparedStatement ps = conn.prepareStatement(sql);
			ResultSet rs = ps.executeQuery();
			List<VehicleInfo> list = new ArrayList<VehicleInfo>();
			while (rs.next()) {
				VehicleInfo info = new VehicleInfo();
				info.setAkey(rs.getObject("AUTH_CODE") == null ? "" : String.valueOf(rs.getObject("AUTH_CODE")));
				info.setOemcode(rs.getObject("OEM_CODE") == null ? "" : String.valueOf(rs.getObject("OEM_CODE")));
				info.setCommaddr(rs.getObject("COMMADDR") == null ? "" : String.valueOf(rs.getObject("COMMADDR")));
				list.add(info);
			}
			addMemCached(list);
			return list.size();
		} catch (SQLException e) {
			System.out.println("Syn data is error.");
			System.out.println(e.getMessage());
		} finally {
			if (conn != null)
				JdbcManager.disConn(conn);
		}
		return 0;
	}

	/**
	 * 将数据放入Mem中
	 * 
	 * @param info
	 *            数据对象
	 */
	private void addMemCached(List<VehicleInfo> info) {
		try {
			for (VehicleInfo v : info) {
				System.out.println(v.getCommaddr());
				client.delete("PCC_" + v.getCommaddr());
				client.set("PCC_" + v.getCommaddr(), 0, v);
			}
			System.out.println("此次共同步数据[" + info.size() + "]条.");
		} catch (TimeoutException e) {
			e.printStackTrace();
		} catch (InterruptedException e) {
			e.printStackTrace();
		} catch (MemcachedException e) {
			e.printStackTrace();
		} finally {
			try {
				client.shutdown();
			} catch (IOException e) {
				e.printStackTrace();
			}
		}

	}
}
