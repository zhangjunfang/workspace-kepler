package com.ocean.localparallel;

import com.ocean.MigrantWorker;
import com.ocean.WareHouse;

public class WorkerLocalDemo extends MigrantWorker {
	/**
	 * 
	 */
	private static final long serialVersionUID = 191756349946152917L;
	public String name;

	public WorkerLocalDemo(String name) {
		this.name = name;
	}

	@SuppressWarnings("unchecked")
	@Override
	public WareHouse doTask(WareHouse inhouse) {
		System.out.println(name + ":" + inhouse);// ������������ֺͻ�ȡ��������
		inhouse.put("task", inhouse.get("task") + " done.");// ������񷵻�
		return inhouse;
	}
}