package com.ocean.unifiedConfigurationManagement;

import com.ocean.BeanContext;
import com.ocean.ParkLocal;

public class SetConfig {
	public static void main(String[] args) {
		ParkLocal pl = BeanContext.getPark();
		pl.create("zhejiang", "hangzhou", "xihu");
		try {
			Thread.sleep(8000);
		} catch (Exception e) {
		}
		pl.update("zhejiang", "hangzhou", "yuhang");
	}
}