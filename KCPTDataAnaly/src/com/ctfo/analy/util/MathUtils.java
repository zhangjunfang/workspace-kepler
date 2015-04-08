package com.ctfo.analy.util;

import java.util.regex.Pattern;

public class MathUtils {

	private static Integer[][] eloArray = new Integer[11][18];

	public static String addSuffixZero(int num) {
		if (num < 10) {
			return "0" + num;
		}
		return "" + num;
	}

	public static Object[] binaryToDecimal(String orign) {
		String a = "";
		System.out.println(Integer.parseInt(orign, 2));
		return null;
	}

	/**
	 * 发动机负荷率分布统计
	 * 
	 * @param line
	 */
	private static void saveEloaddistDay(String line) {

		String elo = line;
		int row = 0;
		int col = 0;
		while (elo.length() > 0) {
			while (col <= 17) {
				String subCol = elo.substring(0, 8);
				int value = Integer.parseInt(subCol, 2);
				eloArray[row][col] = eloArray[row][col] + value;
				elo = elo.substring(8);
				System.out.println(elo);
				col++;
			}// End while
			col = 0;
			row++;
		}// End while
	}

	/**
	 * 将十进制转换为二进制
	 */
	public static String getBinaryStringByInt(String number) {

		int num = Integer.parseInt(number);
		int sum;
		String result = "";

		for (int i = num; i >= 1; i /= 2) {

			if (i % 2 == 0) {
				sum = 0;
			} else {
				sum = 1;
			}
			result = sum + result;

		}

		return result;

	}
	/**
	 * 将Long型数值字符串转换为二进制字符串
	 * @param number
	 * @return
	 */
	public static String getBinaryString(String number) {

		Long num = Long.parseLong(number);

		return Long.toBinaryString(num);

	}

	/**
	 * 判断二进制某位是否是1或0
	 * @param args
	 */
	public static boolean check(String num, String result) {

		boolean bool = false;
		if (result.matches(".*0\\d{"+ num +"}")) { 
			bool = false;
		}
		if (result.matches(".*1\\d{"+ num +"}")) { 
			bool = true;
		}

		return bool;

	}
	
	/**
	 * 判断字符串可否转换为数字
	 * @param str
	 * @return
	 */
	public static boolean isNumeric(String str){ 
	    Pattern pattern = Pattern.compile("[0-9]*"); 
	    return pattern.matcher(str).matches();
	 } 

	// 11111111
	public static void main(String[] args) {
		/*
		 * for (int i = 0; i < 198; i++) { System.out.print("00000011"); } //
		 * System.out.println(binaryToDecimal("00000011")); //saveEloaddistDay(
		 * "000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011000000110000001100000011"
		 * );
		 * 
		 * for (int row = 0; row < eloArray.length; row++) {
		 * System.out.print("第" + row + " :"); for (int col = 0; col < 18;
		 * col++) { System.out.print(eloArray[row][col] + ","); }
		 * System.out.println(); }
		 */
		System.out.println(getBinaryString("2"));
		System.out.println(Long.toBinaryString(Long.parseLong("2")));
		
		System.out.println(check("13","101101000001101"));
		
	}
}

