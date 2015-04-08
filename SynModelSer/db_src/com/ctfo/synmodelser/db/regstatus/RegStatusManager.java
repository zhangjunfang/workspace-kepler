package com.ctfo.synmodelser.db.regstatus;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;

import com.ctfo.synmodelser.jdbc.JdbcManager;
import com.ctfo.synmodelser.server.ModelBulider;
import com.ctfo.synmodelser.util.Config;

public class RegStatusManager implements ModelBulider {
	private Config config;
	private Connection conn = null;

	private String param;

	public RegStatusManager(String filePath, String param) {
		this.config = new Config(filePath);
		this.param = param;
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
		} else {
			conn = JdbcManager.getConnection();
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
		try {
			conn.setAutoCommit(false);
		} catch (SQLException e1) {
			e1.printStackTrace();
		}
		String sql = "update tb_vehicle t set reg_status=? where t.VEHICLE_NO=? and t.PLATE_COLOR=?";

		String sql1 = "update TB_TERMINAL t set reg_status=? where t.TMAC=?";
		PreparedStatement s = null;
		PreparedStatement ss = null;
		try {
			s = conn.prepareStatement(sql);
			String values[] = this.getParam().split(",");
			if (values.length < 4) {
				System.out.println("参数不正确，请输入：更新状态,车牌号,车牌颜色,终端ID");
				return -1;
			}
			s.setString(1, values[0]);
			s.setString(2, values[1]);
			s.setString(3, values[2]);
			s.executeUpdate();
			ss = conn.prepareStatement(sql1);
			ss.setString(1, values[0]);
			ss.setString(2, values[3]);
			int result = ss.executeUpdate();
			conn.commit();
			s.close();
			ss.close();
			System.out.println("共同步了[" + result + "]条数据 .");
		} catch (SQLException e) {
			try {
				System.out.println("执行sql异常，进行回滚。。。。");
				s.execute("ROLLBACK;");
			} catch (SQLException e1) {
				e1.printStackTrace();
			}
		} finally {
			if (conn != null)
				JdbcManager.disConn(conn);
		}
		return 0;
	}

	/**
	 * @return the config
	 */
	public Config getConfig() {
		return config;
	}

	/**
	 * @param config
	 *            the config to set
	 */
	public void setConfig(Config config) {
		this.config = config;
	}

	/**
	 * @return the conn
	 */
	public Connection getConn() {
		return conn;
	}

	/**
	 * @param conn
	 *            the conn to set
	 */
	public void setConn(Connection conn) {
		this.conn = conn;
	}

	/**
	 * @return the param
	 */
	public String getParam() {
		return param;
	}

	/**
	 * @param param
	 *            the param to set
	 */
	public void setParam(String param) {
		this.param = param;
	}

}
