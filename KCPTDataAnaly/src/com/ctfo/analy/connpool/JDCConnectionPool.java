package com.ctfo.analy.connpool;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Enumeration;
import java.util.Vector;

public class JDCConnectionPool {
	private Vector<JDCConnection> connections;
	private String url, user, password;
	private long timeout = 60000;
	private ConnectionReaper reaper;
	private int poolsize = 10;

	public JDCConnectionPool(String url, String user, String password,int poolsize,long timeout,long delay) {
		this.url = url;
		this.user = user;
		this.password = password;
		this.poolsize = poolsize;
		this.timeout = timeout;
		connections = new Vector<JDCConnection>(this.poolsize);
		reaper = new ConnectionReaper(this,delay);
		reaper.start(); //启动检测线程
	}

	/***
	 * 检测连接是否超时或者已经断开
	 */
	public synchronized void reapConnections() {

		long stale = System.currentTimeMillis() - timeout;
		Enumeration<JDCConnection> connlist = connections.elements();

		while ((connlist != null) && (connlist.hasMoreElements())) {
			JDCConnection conn = connlist.nextElement();

			if ((conn.inUse()) && (stale > conn.getLastUse())
					&& (!conn.validate())) {
				removeConnection(conn);
			}
		}// End while
	}

	/**
	 * 断开连接
	 */
	public synchronized void closeConnections() {

		Enumeration<JDCConnection> connlist = connections.elements();

		while ((connlist != null) && (connlist.hasMoreElements())) {
			JDCConnection conn = connlist.nextElement();
			removeConnection(conn);
		}// End while
	}
	
	private synchronized void removeConnection(JDCConnection conn) {
		connections.removeElement(conn);
	}

	/***
	 * 获得连接
	 * @return
	 * @throws SQLException
	 */
	public synchronized Connection getConnection() throws SQLException {

		JDCConnection c;
		for (int i = 0; i < connections.size(); i++) {
			c = connections.elementAt(i);
			if (c.lease()  &&  c.validate()) {
				return c;
			}else{
				if(c  != null){
					c.close();
				}
				connections.removeElementAt(i);
			}
		}// End for

		Connection conn = DriverManager.getConnection(url, user, password);
		c = new JDCConnection(conn, this);
		c.lease();
		connections.addElement(c);
		return c;
	}

	public synchronized void returnConnection(JDCConnection conn) {
		conn.expireLease();
	}
}

class ConnectionReaper extends Thread {

	private JDCConnectionPool pool;
	private long delay = 300000;

	ConnectionReaper(JDCConnectionPool pool,long delay) {
		this.pool = pool;
		this.delay = delay;
	}

	public void run() {
		while (true) {
			try {
				sleep(delay);
			} catch (InterruptedException e) {
			}
			pool.reapConnections();
		}
	}
}
