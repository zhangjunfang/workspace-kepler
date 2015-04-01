package com.ocean.unifiedConfigurationManagement;

import com.ocean.BeanContext;
import com.ocean.LastestEvent;
import com.ocean.LastestListener;
import com.ocean.ObjectBean;
import com.ocean.ParkLocal;

public class GetConfigB implements LastestListener {
	@Override
	public boolean happenLastest(LastestEvent le) {
		ObjectBean ob = (ObjectBean) le.getSource();
		System.out.println(ob);
		return false;
	}

	public static void main(String[] args) {
		ParkLocal pl = BeanContext.getPark();
		pl.addLastestListener("zhejiang", "hangzhou", null, new GetConfigB());
	}
}