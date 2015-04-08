package com.caits.analysisserver.bean;

public class CountTimeBean {
	private int num = 0;
	private int time = 0;

	public int getNum() {
		return num;
	}

	public void addNum(int num) {
		this.num = num + this.num;
	}

	public int getTime() {
		return time;
	}

	public void addTime(int time) {
		this.time = time + this.time;
	}

}
