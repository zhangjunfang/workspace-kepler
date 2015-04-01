package com.ocean.unifiedConfigurationManagement;

import com.ocean.BeanContext;
import com.ocean.ObjectBean;
import com.ocean.ParkLocal;

public class GetConfigA {
	public static void main(String[] args) {
		ParkLocal pl = BeanContext.getPark();
		ObjectBean oldob = null;
		while (true) {
			ObjectBean newob = pl.getLastest("zhejiang", "hangzhou", oldob);
			if (newob != null) {
				System.out.println(newob);
				oldob = newob;
			}
		}
	}
}