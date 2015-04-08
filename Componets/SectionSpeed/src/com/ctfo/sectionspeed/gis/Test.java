package com.ctfo.sectionspeed.gis;

import java.util.concurrent.locks.ReentrantLock;

public class Test {
	private final static ReentrantLock lock = new ReentrantLock(); // 是否正在加载内存

	public synchronized void run() throws InterruptedException {
		lock.lock();
		System.out.println("OOOOO");
		Thread.sleep(1000);
		lock.unlock();
	}

	public static void main(String[] args) throws InterruptedException {
		Runnable t = new Runnable() {
			@Override
			public void run() {
				try {
					new Test().run();
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
			}
		};
		for (int i = 0; i < 10; i++) {
			new Thread(t).start();
		}
	}
}
