package com.ocean.mq;

import com.ocean.BeanContext;
import com.ocean.ObjectBean;
import com.ocean.ParkLocal;

import java.util.ArrayList;
import java.util.List;

public class Publisher {
	private static ParkLocal pl = BeanContext.getPark();

	@SuppressWarnings({ "rawtypes", "unchecked" })
	public static Object publish(String topic, Object obj) {
		List<ObjectBean> oblist = pl.get(topic);
		if (oblist != null) {
			for (ObjectBean ob : oblist) {
				ArrayList arr = (ArrayList) ob.toObject();
				arr.add(obj);
				pl.update(ob.getDomain(), ob.getNode(), arr);
			}
		} else
			return null;
		return obj;
	}

	public static void main(String[] args) {
		publish("topic1", "helloworld");
	}
}