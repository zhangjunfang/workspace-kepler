package com.caits.analysisserver.quartz.alarm.jobs.impl;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import com.ctfo.memcache.beans.AlarmNum;

public class OrgValueMap {

	private String dates[];

	private HashMap<String, HashMap<String, AlarmNum>> hm = new HashMap<String, HashMap<String, AlarmNum>>();

	public OrgValueMap(String dates[]) {
		this.dates = dates;
	}

	public void putHm(String entId, String date, AlarmNum alarmNum) {
		if (hm.containsKey(entId)) {
			hm.get(entId).put(date, alarmNum);
		} else {
			HashMap<String, AlarmNum> tmpHm = new HashMap<String, AlarmNum>();

			for (String d : dates) {
				AlarmNum an = new AlarmNum();
				an.setAlarmDate(d);
				tmpHm.put(d, an);
			}
			tmpHm.put(date, alarmNum);

			hm.put(entId, tmpHm);
		}
	}

	@SuppressWarnings({ "unchecked", "rawtypes" })
	public Map<String, List<AlarmNum>> getMap() {
		HashMap<String, List<AlarmNum>> tmpHm = new HashMap<String, List<AlarmNum>>();
		Iterator it = hm.keySet().iterator();
		while (it.hasNext()) {
			String key = it.next().toString();
			List ls = new ArrayList(hm.get(key).values());
			Collections.sort(ls);
			tmpHm.put(key, ls);
		}

		return tmpHm;

	}

}
