package com.kypt.c2pp.util;
import java.io.IOException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import sun.misc.BASE64Decoder;
import sun.misc.BASE64Encoder;

public class SUNBASE64 {
	private static Logger log = LoggerFactory.getLogger(SUNBASE64.class);

    /**
     * 将字符串编码为Base64, 可以使用 decodeString进行返转?
     * @param str
     * @return
     */
    public static String encodeString(byte[] str) {
        BASE64Encoder encoder = new BASE64Encoder();
        return encoder.encodeBuffer(str).trim();
    }

    /**
     * 将编码为Base64的字符串转换为普通的字符串
     * @param str
     * @return
     * @throws IOException
     */
    public static String decodeString(String str) {
        BASE64Decoder dec = new BASE64Decoder();
        String temp = null;
        try {
            temp = new String(dec.decodeBuffer(str));
        } catch (IOException e) {
            log.error("SUNBASE64 decode error!");
        }
        return temp;
    }
    
    public static void main(String[] args){
    	String s= SUNBASE64.decodeString("6KGM5Lia5pyN5Yqh5Zmo55m76ZmG6L+H56iL5Lit5bmz5Y+w5Y+R55Sf5YaF6YOo6ZSZ6K+v5omn6KGMYXV0aEFnZW505p+l6K+i6ZSZ6K+vaWQ6");
    	System.out.print(s);
    }
}
