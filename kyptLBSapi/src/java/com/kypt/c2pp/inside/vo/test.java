package com.kypt.c2pp.inside.vo;

import com.kypt.c2pp.inside.msg.utils.InsideMsgUtils;

public class test {
		
		/**
		 * 数字验证
		 * @param data
		 * @return
		 * @throws MyWorngNumberException
		 */
		public static int checkDigit(String data) {
			
				return Integer.parseInt(data);
			
		}
		
		
		
		/**
		 * 二进制转换
		 * @param n
		 * @return 二进制
		 */
		public static String printBits (int n) {
			return Integer.toBinaryString(n);
		}
		
		public static byte[] int2Bytes(int b) {
	        byte[] intBuf = new byte[4];
	        for (int i = 0; i < 4; i++) {
	            intBuf[i] = (byte) (b >> 8 * (3 - i) & 0xFF);
	        }
	        return intBuf;
	    }
		
		 public static String int2Binary(String num){
		    	int n=Integer.parseInt(num);
		    	
		    	return Integer.toBinaryString(n);
		    }
	

	
	public static void main(String args[]){
		
		boolean nihao = false;
		
		System.out.println(nihao);
		String s = "3";
		//System.out.println(int2Binary(s));
		
		byte b[]=int2Bytes(3);
		for (int i=0;i<b.length;i++){
			System.out.println(i+"="+b[i]);
		}
		
		
	}

}
