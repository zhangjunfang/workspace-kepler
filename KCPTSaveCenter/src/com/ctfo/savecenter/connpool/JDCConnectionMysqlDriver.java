package com.ctfo.savecenter.connpool;

import java.sql.Connection;
import java.sql.Driver;
import java.sql.DriverManager;
import java.sql.DriverPropertyInfo;
import java.sql.SQLException;
import java.util.Properties;

public class JDCConnectionMysqlDriver implements Driver {

	public static final String URL_PREFIX = "jdbc:jdc:mysql:";
	private static final int MAJOR_VERSION = 1;
	private static final int MINOR_VERSION = 0;
	private JDCConnectionPool pool;
	public JDCConnectionMysqlDriver(String driver,String url, String user, String password,int poolsize,long timeout,long delay) throws ClassNotFoundException,
			InstantiationException, IllegalAccessException, SQLException {

		DriverManager.registerDriver(this);
		Class.forName(driver).newInstance();
		pool = new JDCConnectionPool(url, user, password,poolsize,timeout,delay);
	}

//	/*****
//	 * 
//	 * @return
//	 */
//	public static String listMysqlInfo(){
//		StringBuffer buf = new StringBuffer();
//		Enumeration<JDCConnection> connlist = pool.getConnectionVector().elements();
//		int available = 0;
//		int count = 0;
//		while ((connlist != null) && (connlist.hasMoreElements())) {
//			JDCConnection conn = connlist.nextElement();
//			
//			if (conn.inUse() && !conn.validate()) {
//				available++;
//			}
//			count++;
//		}// End while
//		buf.append( pool.connections.size() + " connections are available;");
//		buf.append( available + " connections are active.");
//		return buf.toString();
//	}
	
	public Connection connect(String url, Properties props) 
	                                       throws SQLException {
	        if(!url.startsWith(URL_PREFIX)){
	             return null;
	        }
	        
	        return pool.getConnection();
	    }

	public boolean acceptsURL(String url) {
		return url.startsWith(URL_PREFIX);
	}

	public int getMajorVersion() {
		return MAJOR_VERSION;
	}

	public int getMinorVersion() {
		return MINOR_VERSION;
	}

	public DriverPropertyInfo[] getPropertyInfo(String str, Properties props) {
		return new DriverPropertyInfo[0];
	}

	public boolean jdbcCompliant() {
		return false;
	}

}
