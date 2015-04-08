package storm.monitor;

import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;
import java.sql.Connection;
import java.sql.DriverManager;
import java.util.Properties;

public class DBConnection {
	private static Connection con = null;

	private DBConnection() {
	}

	public static Connection getInstance() throws Exception {
		Properties prop = new Properties();
		InputStream stream = new FileInputStream(new File("db.properties"));
		prop.load(stream);
		if (con == null) {
			Class.forName(prop.getProperty("db.driver"));
			con = DriverManager.getConnection(prop.getProperty("db.url"),
					prop.getProperty("db.user"),
					prop.getProperty("db.password"));
		}
		return con;
	}

	/**
	 * @param args
	 * @throws Exception
	 */
	public static void main(String[] args) throws Exception {

		System.out.println(DBConnection.getInstance().toString());
	}

}
