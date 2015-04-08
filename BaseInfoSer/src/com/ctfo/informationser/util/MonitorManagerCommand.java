package com.ctfo.informationser.util;

import java.util.Iterator;
import java.util.Map;
import java.util.Set;

public class MonitorManagerCommand {
	
	public static int sendCommand(MonitorManagerBean bean) {
		String command = getCommand(bean);
		System.out.println("==============================================");
		System.out.println(command);
		System.out.println("==============================================");
		ProxyClient.sendMeaage(command);
		return 0;
	}

	private static String getCommand(MonitorManagerBean bean) {
		StringBuffer sb = new StringBuffer();
		sb.append(MonitorStaticData.COMMAND_KEY).append(MonitorStaticData.KEY_SEPARATED_MARK);
		sb.append(bean.getSeq()).append(MonitorStaticData.KEY_SEPARATED_MARK);
		sb.append(bean.getMacId()).append(MonitorStaticData.KEY_SEPARATED_MARK);
		sb.append(bean.getConnType()).append(MonitorStaticData.KEY_SEPARATED_MARK);
		sb.append(MonitorStaticData.KEY).append(MonitorStaticData.KEY_SEPARATED_MARK);
		sb.append("{");
		sb.append("AREA_CODE:"+bean.getAreaID()).append(MonitorStaticData.PARAM_SEPARATED_MARK);
//		sb.append(bean.getVehicle()).append(MonitorStaticData.PARAM_SEPARATED_MARK);
//		sb.append(bean.getAccessCode()).append(MonitorStaticData.PARAM_SEPARATED_MARK);
		sb.append("TYPE:" + bean.getType()).append(MonitorStaticData.PARAM_SEPARATED_MARK);
		StringBuffer valueSb = new StringBuffer();
		Map<String, String> typeValue = bean.getTypeValue();
		// 计数器
		int count = 1;
		if (typeValue != null) {
			Set<String> set = typeValue.keySet();
			Iterator<String> iterator = set.iterator();
			for (; iterator.hasNext();) {
				String key = iterator.next();
				String value = typeValue.get(key);
				valueSb.append(key).append(MonitorStaticData.PARAM_VALUE_SEPARATED_MARK);
				if (count == typeValue.size()) {
					valueSb.append(value);
				}else{
					valueSb.append(value).append(MonitorStaticData.PARAM_SEPARATED_MARK);
				}
				count++;
			}
		} else {
			return null;
		}
		sb.append(valueSb.toString());
		sb.append("}\r\n");
		return sb.toString();
	}

}
