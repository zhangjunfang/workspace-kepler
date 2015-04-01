package com.ocean.distributedcalcuteall;

import com.ocean.MigrantWorker;
import com.ocean.WareHouse;

public class WorkerDemo extends MigrantWorker {
	/**
	 * 
	 */
	private static final long serialVersionUID = -7808353654462831258L;
	private String workname;

	public WorkerDemo(String workname) {
		this.workname = workname;
	}

	@Override
	public WareHouse doTask(WareHouse inhouse) {
		String v = inhouse.getString("id");
		System.out.println(workname + " inhouse:" + v);
		return new WareHouse("id", v + "-" + workname + "-");
	}

	public static void main(String[] args) {
		WorkerDemo wd = new WorkerDemo(args[0]);
		wd.waitWorking("localhost", Integer.parseInt(args[1]), "workdemo");
	}
}