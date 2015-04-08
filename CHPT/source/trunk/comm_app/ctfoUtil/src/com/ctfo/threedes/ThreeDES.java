package com.ctfo.threedes;

import javax.crypto.Cipher;
import javax.crypto.SecretKey;
import javax.crypto.spec.SecretKeySpec;
import com.sun.org.apache.xerces.internal.impl.dv.util.Base64;
/**
 * 
 * @author 马驰
 * 2014-09-28
 */
public class ThreeDES {
	
	/***
	 * 3des解码
	 * @param value 密文
	 * @param key 密钥
	 * @param encoded 编码方式
	 * @return 加密字符串
	 * @throws Exception
	 * 
	 * @modifyInformation 添加了编码规则参数
	 * @author 蒋东卿
	 * 
	 */
	public static String Decrypt3DES(String value, String key, String encoded) throws Exception {
       
    	byte[] b = decryptMode(GetKeyBytes(key), Base64.decode(value));
    	if( b != null){
    		return new String(b,encoded);
    	}
    	else{
    		return "密文、密钥错误";
    	}
    }

     /***
      * 3des加密
      * @param key 密钥
      * @param value 加密字符串  
      * @param encoded 编码方式
      * @return 密文
      * @throws Exception
      * 
      * @modifyInformation 添加了编码规则参数
	  * @author 蒋东卿
      */
    public static String Encrypt3DES(String key, String value, String encoded) throws Exception {
    	if(key.equals("")||null==key){
    		return "密钥为空";
    	}else{
    		String str = byte2Base64(encryptMode(GetKeyBytes(key), value.getBytes(encoded)));
    		return str;
    	}
    }
  //计算24位长的密码byte值,首先对原始密钥做MD5算hash值，再用前8位数据对应补全后8位

    private static byte[] GetKeyBytes(String strKey) throws Exception {

        if (null == strKey || strKey.length() < 1){
        	return null;
        }else{
            java.security.MessageDigest alg = java.security.MessageDigest.getInstance("MD5");

            alg.update(strKey.getBytes());

            byte[] bkey = alg.digest();

            int start = bkey.length;

            byte[] bkey24 = new byte[24];

            for (int i = 0; i < start; i++) {

                bkey24[i] = bkey[i];

            }

            for (int i = start; i < 24; i++) {//为了与.net16位key兼容

                bkey24[i] = bkey[i - start];

            }

            return bkey24;
        }
    }

 

    private static final String Algorithm = "DESede"; //定义 加密算法,可用 DES,DESede,Blowfish       

 

    //keybyte为加密密钥，长度为24字节

    //src为被加密的数据缓冲区（源）  

    private static byte[] encryptMode(byte[] keybyte, byte[] src) {

        try {

            //生成密钥

            SecretKey deskey = new SecretKeySpec(keybyte, Algorithm); //加密 

            Cipher c1 = Cipher.getInstance(Algorithm);

            c1.init(Cipher.ENCRYPT_MODE, deskey);

            return c1.doFinal(src);

       } catch (java.security.NoSuchAlgorithmException e1) {

            e1.printStackTrace();

        } catch (javax.crypto.NoSuchPaddingException e2) {

            e2.printStackTrace();

        } catch (java.lang.Exception e3) {

            e3.printStackTrace();

        }

        return null;

    }

 

    //keybyte为加密密钥，长度为24字节  

    //src为加密后的缓冲区

    private static byte[] decryptMode(byte[] keybyte, byte[] src) {

        try { //生成密钥   

            SecretKey deskey = new SecretKeySpec(keybyte, Algorithm);

            //解密     

            Cipher c1 = Cipher.getInstance(Algorithm);

            c1.init(Cipher.DECRYPT_MODE, deskey);

            return c1.doFinal(src);

        } catch (java.security.NoSuchAlgorithmException e1) {

            e1.printStackTrace();

        } catch (javax.crypto.NoSuchPaddingException e2) {

            e2.printStackTrace();

        } catch (java.lang.Exception e3) {
        	
        //	e3.printStackTrace();
        }

        return null;

    }

 

    //转换成base64编码

    private static String byte2Base64(byte[] b) {

        return Base64.encode(b);

    }
}