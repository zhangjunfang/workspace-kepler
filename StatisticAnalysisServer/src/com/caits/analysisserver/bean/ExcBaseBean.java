package com.caits.analysisserver.bean;

public class ExcBaseBean {
	private long max = 0;
	
	private long min = 0;

	public long getMax() {
		return max;
	}

	public void setMax(long max) {
		if(this.max == 0){
			this.max = max;
		}
		
		if(max > this.max){
			this.max = max;
		}
	}

	public long getMin() {
		return min;
	}

	public void setMin(long min) {
		if(this.min == 0){
			this.min = min;
		}
		
		if(min < this.min && min > 0){
			this.min = min;
		}
	}
}
