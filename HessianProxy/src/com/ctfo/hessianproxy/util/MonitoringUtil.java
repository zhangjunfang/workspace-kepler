package com.ctfo.hessianproxy.util;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.dom4j.Document;
import org.dom4j.Element;

import com.ctfo.hessianproxy.pool.MonitoringPool;
import com.ctfo.monitoring.beans.MonitoringData;

public class MonitoringUtil {

	/**
	 * 
	 * @return
	 */
	public static Map<String, MonitoringData> getMonitoringDataMap(String filePath) {
		Map<String, MonitoringData> monitoringDataMap = MonitoringPool.getInstance();
		List<Document> list = getMeteDataDocument(filePath);
		for (Document document : list) {
			Element root = DocumentUtil.getRootElement(document);
			List<Element> serviceElementList = DocumentUtil.getElementList(root, "Service");
			for (Element serviceElement : serviceElementList) {
				String serviceName = DocumentUtil.getAttribute(serviceElement, "Name");
				List<Element> serviceActionElementList = DocumentUtil.getElementList(serviceElement, "ServiceAction");
				for (Element serviceActionElement : serviceActionElementList) {
					MonitoringData monitoringData = setMonitoringData(serviceName, serviceActionElement);
					if (null != monitoringData && null != monitoringData.getKey() && !"".equals(monitoringData.getKey())) {
						monitoringDataMap.put(monitoringData.getKey(), monitoringData);
					}
				}
			}
		}
		return monitoringDataMap;
	}

	/**
	 * 
	 * @param serviceName
	 * @param serviceActionElement
	 * @return
	 */
	private static MonitoringData setMonitoringData(String serviceName, Element serviceActionElement) {
		MonitoringData monitoringData = new MonitoringData();
		Map<String, String> inputMap = new HashMap<String, String>();
		Map<String, String> outputMap = new HashMap<String, String>();
		String methodName = DocumentUtil.getAttribute(serviceActionElement, "Name");
		StringBuilder key = new StringBuilder();
		key.append(serviceName).append(methodName);

		monitoringData.setKey(key.toString());
		monitoringData.setServiceName(serviceName);
		monitoringData.setMethodName(methodName);
			
		List<Element> serviceRequestModelElementList = DocumentUtil.getElementList(serviceActionElement, "ServiceRequestModel");
		for (Element serviceRequestModelElement : serviceRequestModelElementList) {
			List<Element> inputParameterElementList = DocumentUtil.getElementList(serviceRequestModelElement, "InputParameter");
			for (Element inputParameterElement : inputParameterElementList) {
				String name = DocumentUtil.getAttribute(inputParameterElement, "Name");
				String mapapp = DocumentUtil.getAttribute(inputParameterElement, "Mapapp");
				inputMap.put(name, (mapapp != null && !"".equals(mapapp)) ? mapapp : name);
			}
			monitoringData.setInputMap(inputMap);
		}
		List<Element> serviceResponseModelElementList = DocumentUtil.getElementList(serviceActionElement, "ServiceResponseModel");
		for (Element serviceResponseModel : serviceResponseModelElementList) {
			List<Element> outputParameterElementList = DocumentUtil.getElementList(serviceResponseModel, "OutputParameter");
			for (Element outputParameterElement : outputParameterElementList) {
				List<Element> attributeElementList = DocumentUtil.getElementList(outputParameterElement, "Attribute");
				for (Element attributeElement : attributeElementList) {
					String name = DocumentUtil.getAttribute(attributeElement, "Name");
					String mapapp = DocumentUtil.getAttribute(attributeElement, "Mapapp");
					outputMap.put((mapapp != null && !"".equals(mapapp)) ? mapapp : name, name);
				}
			}
			monitoringData.setOutputMap(outputMap);
		}
		return monitoringData;
	}

	/**
	 * 读取
	 * 
	 * @return
	 */
	private static List<Document> getMeteDataDocument(String filePath) {
		List<Document> list = new ArrayList<Document>();
		File dir = new File(filePath);
		if (dir.exists()) {
			// String files[] = dir.list();
			StringBuffer sb = new StringBuffer();
			// for (String file : files) {
			sb.setLength(0);
			try {
				FileInputStream in = new FileInputStream(new File(filePath));
				InputStreamReader isr = new InputStreamReader(in);
				BufferedReader br = new BufferedReader(isr);
				String line;
				while ((line = br.readLine()) != null) {
					if (null != line && !"".equals(line)) {
						sb.append(line);
					}
				}
				br.close();
				isr.close();
				Document document = DocumentUtil.toDocument(sb.toString());
				Monitoring.data.append(document.asXML());
				list.add(document);
			} catch (Exception e) {
				e.printStackTrace();
			}
			// }
		}
		return list;
	}

	public static void main(String[] args) {
		Map<String, MonitoringData> map = getMonitoringDataMap("F:/workspace/.metadata/.plugins/org.eclipse.wst.server.core/tmp0/wtpwebapps/HessianProxy/WEB-INF/classes/monitoring.xml");
		for (Map.Entry<String, MonitoringData> monitoringDataMap : map.entrySet()) {
			MonitoringData monitoringData = monitoringDataMap.getValue();
//			System.out.println(monitoringData.getKey());
//			System.out.println(monitoringData.getServiceName());
//			System.out.println(monitoringData.getMethodName());
//			for (Map.Entry<String, String> entry : monitoringData.getInputMap().entrySet()) {
//				System.out.println(entry.getKey() + " -|- " + entry.getValue());
//			}
			for (Map.Entry<String, String> entry : monitoringData.getOutputMap().entrySet()) {
				System.out.println(entry.getKey() + " -|- " + entry.getValue());
			}
		}
	}
}
