package com.ctfo.regionfileser.main;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

public class InitProject {
	
	public static void main(String[] args) {
		
		@SuppressWarnings("unused")
		ApplicationContext context = new ClassPathXmlApplicationContext("/com/ctfo/regionfileser/resource/basic/spring-dataaccess.xml");   
	
	}
	
}
