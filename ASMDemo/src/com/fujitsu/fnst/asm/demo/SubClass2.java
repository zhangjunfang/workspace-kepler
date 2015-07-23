package com.fujitsu.fnst.asm.demo;

public abstract class SubClass2 extends SuperClass  implements IFibonacciComputer,ITimeRetriever{
	
	public void methodDefinedInSubClass2(){
		System.out.println("Method defined in SubClass2");
	}
}
