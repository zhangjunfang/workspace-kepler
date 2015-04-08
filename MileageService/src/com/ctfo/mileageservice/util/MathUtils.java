package com.ctfo.mileageservice.util;

import java.text.DecimalFormat;
import java.text.NumberFormat;


public class MathUtils {

	public static void main(String[] args) {
		double d = (77777777 / 600000.00);
		float f = (float) (77777777 / 600000.00);
		precisionDotMethod(d, 5);
		precisionDotMethod(f, 5);
	}

	public static String precisionDotMethod(double lon, int n) {
		// logger.info("输入的小数" + lon);
		// logger.info("想要输出" + n + "位小数");
		String num = "#.";
		for (int i = 0; i < n; i++) {
			num += "0";
		}
		DecimalFormat f = new DecimalFormat(num); // 创建一个格式化类f
		String rtnString = f.format(lon); // 格式化数据a,将a格式化为f
		// logger.info("返回的小数：" + rtnString); // 输出f
		return rtnString;
	}

	public static String precisionDotMethod(float lon, int n) {
		// logger.info("输入的小数" + lon);
		// logger.info("想要输出" + n + "位小数");
		NumberFormat f = NumberFormat.getInstance(); // 创建一个格式化类f
		f.setMaximumFractionDigits(n); // 设置小数位的格式
		String rtnString = f.format(lon); // 格式化数据a,将a格式化为f
		// logger.info("返回的小数：" + rtnString); // 输出f
		return rtnString;
	}
	
	/**
	 * 判断数字是奇偶数，如果是偶数返回true，奇数返回false
	 * @param num
	 * @return
	 */
	public static boolean checkOddEvenNum(int num){
		if(num % 2 ==0){
			return true;
		}
		return false;
	}
	
	
	public static String addSuffixZero(int num){
		if(num < 10){
			return  "0"+ num; 
		}
		return "" + num;
	}
}
