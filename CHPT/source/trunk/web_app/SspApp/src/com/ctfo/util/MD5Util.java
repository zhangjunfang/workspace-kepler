package com.ctfo.util;

public class MD5Util {
	  /**
     * Description :对字符串进行加密处理.用到的算法是MD5算法，MD5是一个不可逆的算法．
     * @param  :String
     * @return :String
     * @throws :Exception
     */
    public static String getMd5(String str) {
    	String encryptString = null;
		try {
			//生成一个MessageDigest类,确定计算方法【参数:算法名,如 SHA-1 或MD5】
			java.security.MessageDigest alg = java.security.MessageDigest.getInstance("MD5");
			//添加要进行计算摘要的信息
			alg.update(str.getBytes());
			//完成计算,返回计算得到的摘要(对于MD5是16位,SHA是20位)
			byte[] digesta = alg.digest();
			encryptString = byte2hex(digesta);
		}catch (java.security.NoSuchAlgorithmException ex) {
			ex.printStackTrace();
		}
		return encryptString;
    }
    
    /**
	 * Description :把二进制数组转换成十六进制
	 * @param :byte[]
	 * @return :String
	 */
	private static String byte2hex(byte[] b){
		String hs = "";
		String stmp = "";
		for (int n=0; n<b.length; n++){
			stmp = (java.lang.Integer.toHexString(b[n] & 0XFF));
			if (stmp.length() == 1) 
				hs = hs + "0" + stmp;
			else 
				hs = hs + stmp;
		 	}
		return hs.toUpperCase();
   }
}
