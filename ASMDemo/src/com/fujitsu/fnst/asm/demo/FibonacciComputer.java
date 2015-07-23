package com.fujitsu.fnst.asm.demo;

public class FibonacciComputer implements IFibonacciComputer {

	public long compute(int n) {
		long first=1;
		long second=1;
		long third=0;
		for(int i=3;i<=n;i++){
			third=first+second;
			first=second;
			second=third;
		}
		return third;
	}

}
