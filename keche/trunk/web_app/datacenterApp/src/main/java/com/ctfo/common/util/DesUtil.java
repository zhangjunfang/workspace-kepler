package com.ctfo.common.util;

import javax.crypto.Cipher;
import javax.crypto.spec.SecretKeySpec;

import sun.misc.BASE64Decoder;
import sun.misc.BASE64Encoder;

@SuppressWarnings("restriction")
public class DesUtil {

	private static DesUtil instance = new DesUtil();

	public static DesUtil getInstance() {
		return instance;
	}

	private final static String rawKey = "CTFOZJHL";

	/**
	 * 加密 String 明文输入 ,String 密文输出
	 */
	public String encryptStr(String strMing) {
		byte[] byteMi = null;
		byte[] byteMing = null;
		String strMi = "";
		BASE64Encoder base64en = new BASE64Encoder();
		try {
			byteMing = strMing.getBytes("UTF8");
			byteMi = this.encryptByte(byteMing);
			strMi = base64en.encode(byteMi);
		} catch (Exception e) {
			throw new RuntimeException("Error initializing SqlMap class. Cause: " + e);
		} finally {
			base64en = null;
			byteMing = null;
			byteMi = null;
		}
		return strMi;
	}

	/**
	 * 解密 以 String 密文输入 ,String 明文输出
	 * 
	 * @param strMi
	 * @return
	 */
	public String decryptStr(String strMi) {
		BASE64Decoder base64De = new BASE64Decoder();
		byte[] byteMing = null;
		byte[] byteMi = null;
		String strMing = "";
		try {
			byteMi = base64De.decodeBuffer(strMi);
			byteMing = this.decryptByte(byteMi);
			strMing = new String(byteMing, "UTF8");
		} catch (Exception e) {
			throw new RuntimeException("Error initializing SqlMap class. Cause: " + e);
		} finally {
			base64De = null;
			byteMing = null;
			byteMi = null;
		}
		return strMing;
	}

	/**
	 * 加密以 byte[] 明文输入 ,byte[] 密文输出
	 * 
	 * @param byteS
	 * @return
	 */
	private byte[] encryptByte(byte[] byteS) {
		byte[] byteFina = null;
		Cipher cipher;
		try {
			byte[] key = rawKey.getBytes();
			// Instantiate the cipher
			SecretKeySpec skeySpec = new SecretKeySpec(key, "DES");
			cipher = Cipher.getInstance("DES");
			cipher.init(Cipher.ENCRYPT_MODE, skeySpec);
			byteFina = cipher.doFinal(byteS);
		} catch (Exception e) {
			throw new RuntimeException("Error initializing SqlMap class. Cause: " + e);
		} finally {
			cipher = null;
		}
		return byteFina;
	}

	/**
	 * 解密以 byte[] 密文输入 , 以 byte[] 明文输出
	 * 
	 * @param byteD
	 * @return
	 */
	private byte[] decryptByte(byte[] byteD) {
		Cipher cipher;
		byte[] byteFina = null;
		try {
			byte[] key = rawKey.getBytes();
			SecretKeySpec skeySpec = new SecretKeySpec(key, "DES");

			cipher = Cipher.getInstance("DES");
			cipher.init(Cipher.DECRYPT_MODE, skeySpec);
			byteFina = cipher.doFinal(byteD);
		} catch (Exception e) {
			throw new RuntimeException("Error initializing SqlMap class. Cause: " + e);
		} finally {
			cipher = null;
		}
		return byteFina;
	}

	public static void main(String[] str) {
		// 加密前：test
		// 加密后：73C58BAFE578C59366D8C995CD0B9D6D
		// 解密后：test
		System.out.println("333:" + DesUtil.getInstance().encryptStr("1")); // 5LPdAornzr0Ou/buv9s0Lg==
		System.out.println("rrra;" + DesUtil.getInstance().decryptStr("JH/PYDtapy3g7BtIxSEqgg=="));
	}

}
