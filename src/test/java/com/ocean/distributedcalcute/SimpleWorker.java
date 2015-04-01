package com.ocean.distributedcalcute;

import com.ocean.MigrantWorker;
import com.ocean.WareHouse;

public class SimpleWorker extends MigrantWorker {
	/**
	 * 
	 */
	private static final long serialVersionUID = 4344865233785017567L;

	@Override
	public WareHouse doTask(WareHouse inhouse) {
		String word = inhouse.getString("word");
		System.out.println(word + " from Contractor.");
		return new WareHouse("word", word + " world!");
	}

	public static void main(String[] args) {
		SimpleWorker mw = new SimpleWorker();
		mw.waitWorking("simpleworker");
	}
}