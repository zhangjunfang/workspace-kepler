package com.ocean.localparallel;

import com.ocean.Contractor;
import com.ocean.WareHouse;
import com.ocean.WorkerLocal;

public class CtorLocalDemo extends Contractor {
	@Override
	public WareHouse giveTask(WareHouse inhouse) {
		WorkerLocal[] wks = getLocalWorkers(3);// ����3�����ع����߳�
		for (int j = 0; j < wks.length; j++)
			wks[j].setWorker(new WorkerLocalDemo("worker" + j));// ���ñ��ع���ʵ����

		WareHouse[] tasks = new WareHouse[20];// ����20������
		for (int i = 0; i < tasks.length; i++) {
			tasks[i] = new WareHouse("task", i + "");
		}

		WareHouse[] result = doTaskCompete(wks, tasks);// ����20�������3�����ع��˲��м������
		System.out.println("result:");
		for (WareHouse r : result)
			System.out.println(r);

		return inhouse;
	}

	public static void main(String[] args) {
		CtorLocalDemo cd = new CtorLocalDemo();
		cd.giveTask(null);
		cd.exit();
	}
}