package com.ctfo.hessianproxy.util;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.dom4j.Attribute;
import org.dom4j.Document;
import org.dom4j.Element;

import com.ctfo.hessianproxy.pool.MonitoringPool;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.monitoring.beans.MonitoringData;
import com.ctfo.monitoring.beans.MonitoringDataParameter;

public class MonitoringParameterUtil {

	private final static String INPUT = "input";

	private final static String OUTPUT = "output";

	@SuppressWarnings("unchecked")
	public static List<MonitoringDataParameter> getMonitoringDataParameter(String xml) {
		Map<String, MonitoringData> map = MonitoringPool.getInstance();
		List<MonitoringDataParameter> monitoringDataParameterList = new ArrayList<MonitoringDataParameter>();

		Document document = DocumentUtil.toDocument(xml);
		Element root = DocumentUtil.getRootElement(document);

		String id = DocumentUtil.getAttribute(root, "id");
		String service = DocumentUtil.getAttribute(root, "service");
		String method = DocumentUtil.getAttribute(root, "method");
		StringBuilder key = new StringBuilder();
		key.append(service).append(method);
		MonitoringData monitoringData = map.get(key.toString());

		List<Element> paramElementList = DocumentUtil.getElementList(root, "Param");
		for (Element paramElement : paramElementList) {
			List<Element> itemElementList = DocumentUtil.getElementList(paramElement, "Item");
			for (Element itemElement : itemElementList) {
				MonitoringDataParameter monitoringDataParameter = new MonitoringDataParameter();
				DynamicSqlParameter parameter = new DynamicSqlParameter();
				parameter.setOutputValue(monitoringData.getOutputMap());
				Map<String, String> equal = new HashMap<String, String>();

				monitoringDataParameter.setId(id);
				monitoringDataParameter.setServiceName(service);
				monitoringDataParameter.setMethodName(method);

				List<Attribute> attributeList = itemElement.attributes();
				for (Attribute attribute : attributeList) {
					String equalKey = attributeToAttribute(monitoringData, INPUT, key.toString(), attribute.getName());
					String equalValue = attribute.getValue();
					equal.put(equalKey, equalValue);
				}
				parameter.setEqual(equal);
				monitoringDataParameter.setParameter(parameter);

				monitoringDataParameterList.add(monitoringDataParameter);
			}
		}
		return monitoringDataParameterList;
	}

	private static String attributeToAttribute(MonitoringData monitoringData, String type, String key, String attribute) {

		String equalAttribute = new String();
		if (type.equals(INPUT) && null != monitoringData && null != monitoringData.getInputMap() && 0 < monitoringData.getInputMap().size()) {
			Map<String, String> inputMap = monitoringData.getInputMap();
			equalAttribute = inputMap.get(attribute);
		}
		if (type.equals(OUTPUT) && null != monitoringData && null != monitoringData.getOutputMap() && 0 < monitoringData.getOutputMap().size()) {
			Map<String, String> outputMap = monitoringData.getOutputMap();
			equalAttribute = outputMap.get(attribute);
		}
		return equalAttribute;
	}

	public static void main(String[] args) {
		StringBuilder xml = new StringBuilder();
		xml.append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
		xml.append("<Request service=\"vehicleInforService\" method=\"isRegVehicle\" key=\"1\" id=\"id\">");
		xml.append("<Param>");
		xml.append("<Item vehicleColor=\"1-1\" vehicleno=\"2-1\"/>");
		// xml.append("<Item vehicleno=\"1-2\" startUtc=\"2-2\"/>");
		xml.append("</Param>");
		xml.append("</Request>");

		List<MonitoringDataParameter> monitoringDataParameterList = getMonitoringDataParameter(xml.toString());
		for (MonitoringDataParameter monitoringDataParameter : monitoringDataParameterList) {
			System.out.println(monitoringDataParameter.getId());
			System.out.println(monitoringDataParameter.getServiceName());
			System.out.println(monitoringDataParameter.getMethodName());
			DynamicSqlParameter parameter = monitoringDataParameter.getParameter();
			System.out.println(parameter);
			// for (Map.Entry<String, String> entry : parameter.getEqual().entrySet()) {
			// System.out.println(entry.getKey() + "----" + entry.getValue());
			// }
		}
	}
}
