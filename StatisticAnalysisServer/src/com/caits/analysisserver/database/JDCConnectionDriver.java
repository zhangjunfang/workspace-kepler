package com.caits.analysisserver.database;

import java.sql.Connection;
import java.sql.Driver;
import java.sql.DriverManager;
import java.sql.DriverPropertyInfo;
import java.sql.SQLException;
import java.util.Properties;

public class JDCConnectionDriver implements Driver {

	public static final String URL_PREFIX = "jdbc:jdc:";
	private static final int MAJOR_VERSION = 1;
	private static final int MINOR_VERSION = 0;
	private JDCConnectionPool pool;

	public JDCConnectionDriver(String driver,String url, String user, String password,int poolsize,long timeout,long delay) throws ClassNotFoundException,
			InstantiationException, IllegalAccessException, SQLException {

		DriverManager.registerDriver(this);
		Class.forName(driver).newInstance();
		pool = new JDCConnectionPool(url, user, password,poolsize,timeout,delay);
	}

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
