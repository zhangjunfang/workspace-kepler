package com.ctfo.local.dao;

import org.junit.Test;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;


public class GenericIbatisDaoTest 
{
	@SuppressWarnings("unused")
	@Test
	public void  singleVehicleTest(){
		ApplicationContext ctx = new ClassPathXmlApplicationContext("com/ctfo/resource/applicationContext*.xml"); 
		
	}
}
