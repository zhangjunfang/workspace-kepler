package com.ocean.calculateAbort;

import com.ocean.MigrantWorker;
import com.ocean.WareHouse;

import java.util.Random;

public class CancelWorker extends MigrantWorker {
	/**
	 * 
	 */
	private static final long serialVersionUID = 5086493167421582884L;

	@Override
	public WareHouse doTask(WareHouse inhouse) {
		int n = 0;
		Random rd = new Random();
		while (!isInterrupted()) {
			n = rd.nextInt(100000);
			System.out.println(n);
			if (n == 888)
				break;
		}
		return new WareHouse("result", n);
	}

	public static void main(String[] args) {
		CancelWorker mw = new CancelWorker();
		mw.waitWorking("localhost", Integer.parseInt(args[0]), "cancelworker");
	}
}