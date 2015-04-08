package storm.monitor;

import java.util.ArrayList;
import java.util.List;

import org.apache.zookeeper.WatchedEvent;
import org.apache.zookeeper.Watcher;
import org.apache.zookeeper.ZooKeeper;

public class Moniter {

	public static final String JSTORM_ROOT_PATH = "/storm/";
	public static final String SUPERVISOR_PATH = JSTORM_ROOT_PATH
			+ "supervisors";
	public static final String TOPO_PATH = JSTORM_ROOT_PATH + "storms";
	public static final String NIMBUS_PATH = JSTORM_ROOT_PATH + "nimbus_host";

	/**
	 * @param args
	 */
	public static void main(String[] args) {

		String configFile = null;
		if (args.length == 0) {
			configFile = "storm-monitor-config.xml";
		} else {
			configFile = args[0];
		}
		String alarmString = null;

		Model model = new Model(configFile);
		ZooKeeper zkKeeper = null;
		List<String> supervisorList = null;
		int supervisor_num = 0;
		try {
			zkKeeper = new ZooKeeper(model.getZkConnectString(),
					model.getZkTimeout(), new Watcher() {
						@Override
						public void process(WatchedEvent event) {

							// System.out.println(event.getType());
						}

					});
			/** 检查nimbus 是否正常 */
			if (zkKeeper.getChildren(NIMBUS_PATH, false).size() < 1) {
				// nimbus 挂掉告警
				System.out.println(model.getNimbus_alarm());
				alarmString = model.getNimbus_alarm();
				SMS_oracle.SentAlarm(model, alarmString);
				SMS_oracle.closeCon();
				zkKeeper.close();
				System.exit(1);
			}
			/** 检查supervisor个数是否正常 */
			supervisorList = zkKeeper.getChildren(SUPERVISOR_PATH, false);
			supervisor_num = supervisorList.size();
			// supervisor_num=4;
			if (supervisor_num != model.getSupervisorNum()) {
				// supervisor个数不足，告警出来
				System.out.println(model.getSuper_num_alarm().replaceAll(
						"\\$\\{num}", supervisor_num + ""));
				alarmString = model.getSuper_num_alarm().replaceAll(
						"\\$\\{num}", supervisor_num + "");
				SMS_oracle.SentAlarm(model, alarmString);
				SMS_oracle.closeCon();
				zkKeeper.close();
				System.exit(2);
			}

			/** 检查topo是否正常 */
			List<String> topoList = zkKeeper.getChildren(TOPO_PATH, false);
			List<String> topoNameList = new ArrayList<String>();
			if (topoList.size() > 0) {
				for (String topoNameALL : topoList) {
					topoNameList.add(topoNameALL.split("-")[0]);
				}
			}
			for (String topo : model.getTopologys()) {
				System.out.println(topo);
				if (!topoNameList.contains(topo)) {
					// topo 已经停止，告警出来
					alarmString = model.getTopo_name_alarm().replaceAll(
							"\\$\\{name}", topo);
					System.out.println(alarmString);
					SMS_oracle.SentAlarm(model, alarmString);
				}
				Thread.sleep(2000);
			}

			SMS_oracle.closeCon();
			zkKeeper.close();
		} catch (Exception e) {
			
			e.printStackTrace();
		}
	}

}
