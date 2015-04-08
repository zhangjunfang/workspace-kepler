package com.ruishenh.dubbo.example;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;


public class LuncherProvider  {
	public static void main(String[] args) throws InterruptedException{
		LuncherProvider luncher=new LuncherProvider();
		luncher.start();
		Thread.sleep(1000*60*10);
	}
	
	void start(){
		String configLocation="spring/dubbo-provider.xml";
		ApplicationContext context =new  ClassPathXmlApplicationContext(configLocation);
		String [] names=context.getBeanDefinitionNames();
		System.out.print("Beans:");
		for (String string : names)
			System.out.print(string+",");
		System.out.println();
	}
}
