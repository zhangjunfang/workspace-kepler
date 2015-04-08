package storm.monitor;

import java.sql.Connection;
import java.sql.Statement;
import java.util.List;

public class SMS_oracle {

	/**
	 * @param args
	 */
	public static Connection con = null;
	static {
		try {
			con = DBConnection.getInstance();
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	public static void closeCon() throws Exception {
		if (con != null) {
			con.close();
		}
	}

	public static void SentAlarm(Model model, String alarmString) {
		try {
			Statement st = con.createStatement();

			if (con == null) {
				con = DBConnection.getInstance();
			}
			if (st == null) {
				st = con.createStatement();
			}
			String sql = null;

			for (String mobile : model.getTelList()) {
				sql = "insert into tb_queue_alarm(id,phone,msg,pwd,inserttime,sendlevel,svrtype,smstotal,jobid) ";
				sql += " values (EDM_USER.SEQ_QUE.NEXTVAL@edm_link,'" + mobile
						+ "','" + alarmString + "',0,sysdate,2,14,0,0)";
				st.execute(sql);
				// System.out.println(sql);
			}

			st.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void SentAlarm(List<String> telList, String alarmString) {
		try {
			Statement st = con.createStatement();

			if (con == null) {
				con = DBConnection.getInstance();
			}
			if (st == null) {
				st = con.createStatement();
			}
			String sql = null;

			for (String mobile : telList) {
				sql = "insert into tb_queue_alarm(id,phone,msg,pwd,inserttime,sendlevel,svrtype,smstotal,jobid) ";
				sql += " values (EDM_USER.SEQ_QUE.NEXTVAL@edm_link,'" + mobile
						+ "','" + alarmString + "',0,sysdate,2,14,0,0)";
				st.execute(sql);
				// System.out.println(sql);
			}

			st.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}
