package com.fujitsu.fnst.asm.demo;
import com.fujitsu.fnst.asm.utility.EnhanceException;
import com.fujitsu.fnst.asm.utility.EnhanceFactory;

public class Main {

	public static void main(String[] args) throws EnhanceException {
		//Instead of new, we create enhanced class instance by EnhanceFactory.newInstance method.
		SubClass1 obj1 = EnhanceFactory.newInstance(SubClass1.class, TimeRetriever.class,FibonacciComputer.class);
		//Now,we can use it like a common object
		obj1.methodInSuperClass();
		obj1.methodDefinedInSubClass1();
		//Then, we can also can methods that implement the interfaces.
		System.out.println("The Fibonacci number of 10 is "+obj1.compute(10));
		System.out.println("Now is :"+obj1.tellMeTheTime());
		System.out.println("--------------------------------------");
		//With SubClass2 is the same.
		SubClass2 obj2 = EnhanceFactory.newInstance(SubClass2.class, TimeRetriever.class,FibonacciComputer.class);
		//
		obj2.methodInSuperClass();
		obj2.methodDefinedInSubClass2();
		//Then, we can also can methods that implement the interfaces.
		System.out.println("The Fibonacci number of 10 is "+obj1.compute(10));
		System.out.println("Now is :"+obj1.tellMeTheTime());
	}

}
