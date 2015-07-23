package com.fujitsu.fnst.asm.demo;

public abstract class SubClass1 extends SuperClass implements IFibonacciComputer,ITimeRetriever{
	
	public void methodDefinedInSubClass1(){
		System.out.println("Method defined in SubClass1");
	}
}
