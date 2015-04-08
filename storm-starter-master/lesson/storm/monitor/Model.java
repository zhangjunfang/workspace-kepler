package storm.monitor;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

public class Model {

	public Model(String path) {
		this.readConfigXML(path);
	}

	private int supervisorNum;
	private String zkConnectString;
	private int zkTimeout;

	private String super_num_alarm;
	private String topo_name_alarm;
	private String nimbus_alarm;
	private List<String> telList;

	public List<String> getTelList() {
		return telList;
	}

	public void setTelList(List<String> telList) {
		this.telList = telList;
	}

	public String getNimbus_alarm() {
		return nimbus_alarm;
	}

	public void setNimbus_alarm(String nimbusAlarm) {
		nimbus_alarm = nimbusAlarm;
	}

	public String getSuper_num_alarm() {
		return super_num_alarm;
	}

	public void setSuper_num_alarm(String superNumAlarm) {
		super_num_alarm = superNumAlarm;
	}

	public String getTopo_name_alarm() {
		return topo_name_alarm;
	}

	public void setTopo_name_alarm(String topoNameAlarm) {
		topo_name_alarm = topoNameAlarm;
	}

	public String getZkConnectString() {
		return zkConnectString;
	}

	public void setZkConnectString(String zkConnectString) {
		this.zkConnectString = zkConnectString;
	}

	public int getZkTimeout() {
		return zkTimeout;
	}

	public void setZkTimeout(int zkTimeout) {
		this.zkTimeout = zkTimeout;
	}

	private List<String> topologys;

	public int getSupervisorNum() {
		return supervisorNum;
	}

	public void setSupervisorNum(int supervisorNum) {
		this.supervisorNum = supervisorNum;
	}

	public List<String> getTopologys() {
		return topologys;
	}

	public void setTopologys(List<String> topologys) {
		this.topologys = topologys;
	}

	public void readConfigXML(String path) {
		NodeList nlist = null;
		Element e = null;
		try {
			File file = new File(path);
			DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
			DocumentBuilder db = dbf.newDocumentBuilder();
			Document doc = db.parse(file);

			nlist = doc.getElementsByTagName("person");
			e = (Element) nlist.item(0);
			nlist = e.getElementsByTagName("tel");
			telList = new ArrayList<String>();
			for (int i = 0; i < nlist.getLength(); i++) {
				// System.out.println(nlist.item(i).getFirstChild().getNodeValue());
				this.telList.add(nlist.item(i).getFirstChild().getNodeValue());
			}
			nlist = doc.getElementsByTagName("nimbus");
			e = (Element) nlist.item(0);
			this.setNimbus_alarm(e.getElementsByTagName("alarm").item(0)
					.getFirstChild().getNodeValue());
			// System.out.println(this.getNimbus_alarm());

			nlist = doc.getElementsByTagName("Supervisor");
			e = (Element) nlist.item(0);
			String Supervisor_sum = e.getElementsByTagName("Supervisor-sum")
					.item(0).getFirstChild().getNodeValue();
			if (Supervisor_sum != null) {
				setSupervisorNum(Integer.parseInt(Supervisor_sum));
			}
			// System.out.println(this.getSupervisorNum());
			this.setSuper_num_alarm(e.getElementsByTagName("alarm").item(0)
					.getFirstChild().getNodeValue());
			// System.out.println(this.getSuper_num_alarm());

			nlist = doc.getElementsByTagName("Topology");
			e = (Element) nlist.item(0);

			int topo_num = e.getElementsByTagName("topology-name").getLength();
			String topo_name = null;
			// MatchLogic =
			topologys = new ArrayList<String>();
			for (int i = 0; i < topo_num; i++) {
				topo_name = e.getElementsByTagName("topology-name").item(i)
						.getFirstChild().getNodeValue();
				if (topo_name != null) {
					topologys.add(topo_name);
				}
			}

			this.setTopo_name_alarm(e.getElementsByTagName("alarm").item(0)
					.getFirstChild().getNodeValue());
			// System.out.println(this.getTopo_name_alarm());

			nlist = doc.getElementsByTagName("zookeeper");
			e = (Element) nlist.item(0);
			String host = e.getElementsByTagName("host").item(0)
					.getFirstChild().getNodeValue();
			if (host != null) {
				setZkConnectString(host);
			}
			// System.out.println(getZkConnectString());
			String timeout = e.getElementsByTagName("timeout").item(0)
					.getFirstChild().getNodeValue();
			if (timeout != null) {
				setZkTimeout(Integer.parseInt(timeout));
			}
			// System.out.println(this.getZkTimeout());

		} catch (Exception ep) {
			ep.printStackTrace();
		}
	}

	public static void main(String[] args) {
		new Model("storm-monitor-config.xml");
		// monitor.readConfigXML("storm-monitor-config.xml");
	}

}
