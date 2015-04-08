package com.caits.analysisserver.database;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class TestConnectionPool {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		try {
			new JDCConnectionDriver("oracle.jdbc.driver.OracleDriver","jdbc:oracle:thin:@192.168.5.120:1521:orcl", "kcpt", "kcpt", 1, 30000, 5000);
		} catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InstantiationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		while(true){
			try {
				Connection dbCon = DriverManager.getConnection("jdbc:jdc:jdcpool");
				
				
				if(dbCon  != null){
					System.out.println("连接成功。");
					dbCon.close();
				}else{
					System.out.println("连接失败，从新获得连接。");
				}
				
				Connection dbConSec = DriverManager.getConnection("jdbc:jdc:jdcpool");
				if(dbConSec  != null){
					System.out.println("第二次连接成功。");
					
					dbConSec.close();
				}else{
					System.out.println("连接失败，从新获得连接。");
				}
			} catch (SQLException e) {
				e.printStackTrace();
			}
			try {
				Thread.sleep(1000);
			} catch (InterruptedException e1) {
				// TODO Auto-generated catch block
				e1.printStackTrace();
			}
		}
	}

}
