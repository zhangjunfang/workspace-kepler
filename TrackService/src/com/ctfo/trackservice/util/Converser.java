package com.ctfo.trackservice.util;



import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Date;


public class Converser {

//	private static final Logger log = LoggerFactory.getLogger(Converser.class);
	
    public static final int DIRECTION_LEFT = 0;

    public static final int DIRECTION_RIGHT = 1;

    /**
     * 把单个字节转换成16进制字符串
     * @param bArray
     * @return
     */
    public static String byteToHexString(byte bArray) {
        StringBuffer sb = new StringBuffer(1);
        String sTemp;
        for (int i = 0; i < 1; i++) {
            sTemp = Integer.toHexString(0xFF & bArray);
            if (sTemp.length() < 2) sb.append(0);
            sb.append(sTemp.toUpperCase());
        }
        return sb.toString();
    }

    /**
     * @函数功能: BCD码转为10进制串(阿拉伯数据)
     * @输入参数: BCD码
     * @输出结果: 10进制串
     */
    public static String bcdToStr(byte[] src, int pos, int len) {
        byte[] bytes = new byte[len];
        System.arraycopy(src, pos, bytes, 0, len);
        StringBuffer temp = new StringBuffer(bytes.length * 2);

        for (int i = 0; i < bytes.length; i++) {
            temp.append((byte) ((bytes[i] & 0xf0) >>> 4));
            temp.append((byte) (bytes[i] & 0x0f));
        }
        return temp.toString().substring(0, 1).equalsIgnoreCase("0") ? temp.toString().substring(1)
                : temp.toString();
    }

    /**
     * @函数功能: 10进制串转为BCD码
     * @输入参数: 10进制串
     * @输出结果: BCD码
     */
    public static byte[] strToBcd(String asc) {
        int len = asc.length();
        int mod = len % 2;

        if (mod != 0) {
            asc = "0" + asc;
            len = asc.length();
        }

        byte abt[] = new byte[len];
        if (len >= 2) {
            len = len / 2;
        }

        byte bbt[] = new byte[len];
        abt = asc.getBytes();
        int j, k;

        for (int p = 0; p < asc.length() / 2; p++) {
            if ((abt[2 * p] >= '0') && (abt[2 * p] <= '9')) {
                j = abt[2 * p] - '0';
            } else if ((abt[2 * p] >= 'a') && (abt[2 * p] <= 'z')) {
                j = abt[2 * p] - 'a' + 0x0a;
            } else {
                j = abt[2 * p] - 'A' + 0x0a;
            }

            if ((abt[2 * p + 1] >= '0') && (abt[2 * p + 1] <= '9')) {
                k = abt[2 * p + 1] - '0';
            } else if ((abt[2 * p + 1] >= 'a') && (abt[2 * p + 1] <= 'z')) {
                k = abt[2 * p + 1] - 'a' + 0x0a;
            } else {
                k = abt[2 * p + 1] - 'A' + 0x0a;
            }

            int a = (j << 4) + k;
            byte b = (byte) a;
            bbt[p] = b;
        }
        return bbt;
    }

    private final static byte[] hex = "0123456789ABCDEF".getBytes();

    private static int parse(char c) {
        if (c >= 'a') return (c - 'a' + 10) & 0x0f;
        if (c >= 'A') return (c - 'A' + 10) & 0x0f;
        return (c - '0') & 0x0f;
    }

    /**
     * 从字节数组到十六进制字符串转换。
     * @param b
     * @return
     */
    public static String bytesToHexString(byte[] b) {
        byte[] buff = new byte[2 * b.length];
        for (int i = 0; i < b.length; i++) {
            buff[2 * i] = hex[(b[i] >> 4) & 0x0f];
            buff[2 * i + 1] = hex[b[i] & 0x0f];
        }
        return new String(buff);
    }

    /**
     * 从十六进制字符串到字节数组转换。
     * @param hexstr
     * @return
     */
    public static byte[] hexStringToBytes(String hexstr) {
    	if (null == hexstr) {
            return null;
        }
        if (hexstr.length() % 2 == 1) {

              hexstr = "0" + hexstr;
        }
        
        byte[] b = new byte[hexstr.length() / 2];
        int j = 0;
        for (int i = 0; i < b.length; i++) {
            char c0 = hexstr.charAt(j++);
            char c1 = hexstr.charAt(j++);
            b[i] = (byte) ((parse(c0) << 4) | parse(c1));
        }
        return b;
    }

    public static byte hexStringToByte(String hexstr) {
        if (hexstr.length() != 2) {
            return 0;
        }

        byte b = 0;
        char c0 = hexstr.charAt(0);
        char c1 = hexstr.charAt(1);
        b = (byte) ((parse(c0) << 4) | parse(c1));

        return b;
    }

    public static String trimChar(String src, char c, int direction) {

        String dest = "";
        if (src == null || src.length() == 0) {
            return dest;
        }

        int pos = 0;
        if (direction == DIRECTION_RIGHT) {
            pos = src.length() - 1;
            for (int i = src.length(); i > 0; i--) {
                if (src.charAt(i - 1) != c) {
                    pos = i;
                    break;
                }
            }

            dest = src.substring(0, pos);

        } else {
            for (int i = 0; i < src.length(); i++) {
                if (src.charAt(i) != c) {
                    pos = i;
                    break;
                }
            }

            dest = src.substring(pos, src.length());
        }

        return dest;
    }

    public static String converToString(String src, int length, char c) {
        for (int j = src.length(); j < length; j++) {
            src = c + src;
        }
        return src;
    }

    public static long getUTC() {        // 从2000年1月1日（UTC/GMT00:00:00Z）开始所经过的秒数，不考虑闰秒。
        return (new Date().getTime() / 1000 - 946656000);
    }

    

    public static short bytes2Short(byte[] b, int pos) {
        return (short) (((b[pos] << 8) | b[pos + 1] & 0xff));
    }
    
    public static int bytes2int(byte b[]) {
        int s = 0;
        s = ((((b[0] & 0xff) << 8 | (b[1] & 0xff)) << 8) | (b[2] & 0xff)) << 8 | (b[3] & 0xff);
        return s;
        }

    public static byte[] short2Bytes(short b) {
        byte[] shortBuf = new byte[2];
        shortBuf[1] = (byte) (b & 0xff);
        shortBuf[0] = (byte) ((b >>> 8) & 0xff);
        return shortBuf;
    }

    public static byte[] int2Bytes(int b) {
        byte[] intBuf = new byte[4];
        for (int i = 0; i < 4; i++) {
            intBuf[i] = (byte) (b >> 8 * (3 - i) & 0xFF);
        }
        return intBuf;
    }

    public static short getCRC16(byte[] src) {

        short crc = (short) 0xFFFF;
        int i, j;
        boolean c15, bit;

        for (i = 0; i < src.length; i++) {
            for (j = 0; j < 8; j++) {
                c15 = ((crc >> 15 & 1) == 1);
                bit = ((src[i] >> (7 - j) & 1) == 1);
                crc <<= 1;
                if (c15 ^ bit) crc ^= 0x1021;
            }
        }

        return crc;
    }
    
    /**
     * 轮换字节数组为十六进制字符串
     * @param b 字节数组
     * @return 十六进制字符串
     */
    @SuppressWarnings("unused")
	private static String byteArrayToHexString(byte[] b) {
        StringBuffer resultSb = new StringBuffer();
        for (int i = 0; i < b.length; i++) {
            resultSb.append(byteToHexString(b[i]));
        }
        return resultSb.toString();
    }

    public static byte[] md5(byte[] src) throws NoSuchAlgorithmException {
        MessageDigest md = MessageDigest.getInstance("MD5");
        return md.digest(src);
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
    
    public static byte[] Encryptbyte(String str) {		
		@SuppressWarnings("unused")
		String encryptString = null;
		byte[] digesta =null;
		try {
			//生成一个MessageDigest类,确定计算方法【参数:算法名,如 SHA-1 或MD5】
			java.security.MessageDigest alg = java.security.MessageDigest.getInstance("MD5");
			//添加要进行计算摘要的信息
			alg.update(str.getBytes());
			//完成计算,返回计算得到的摘要(对于MD5是16位,SHA是20位)
			  digesta = alg.digest();
			 
		}catch (java.security.NoSuchAlgorithmException ex) {
			ex.printStackTrace();
		}
		return digesta;	
	}
    
    public static byte[] gbk2utf8(String chenese){   
    	  char c[] = chenese.toCharArray();   
    	        byte [] fullByte =new byte[3*c.length];   
    	        for(int i=0; i<c.length; i++){   
    	         int m = (int)c[i];   
    	         String word = Integer.toBinaryString(m);   
//    	         System.out.println(word);   
    	            
    	         StringBuffer sb = new StringBuffer();   
    	         int len = 16 - word.length();   
    	         //补零   
    	         for(int j=0; j<len; j++){   
    	          sb.append("0");   
    	         }   
    	         sb.append(word);   
    	         sb.insert(0, "1110");   
    	         sb.insert(8, "10");   
    	         sb.insert(16, "10");   
    	            
//    	         System.out.println(sb.toString());   
    	            
    	         String s1 = sb.substring(0,8);             
    	         String s2 = sb.substring(8,16);             
    	         String s3 = sb.substring(16);   
    	            
    	         byte b0 = Integer.valueOf(s1, 2).byteValue();   
    	         byte b1 = Integer.valueOf(s2, 2).byteValue();   
    	         byte b2 = Integer.valueOf(s3, 2).byteValue();   
    	         byte[] bf = new byte[3];   
    	         bf[0] = b0;   
    	         fullByte[i*3] = bf[0];   
    	         bf[1] = b1;   
    	         fullByte[i*3+1] = bf[1];   
    	         bf[2] = b2;   
    	         fullByte[i*3+2] = bf[2];   
    	            
    	        }   
    	        return fullByte;   
    	 }  
    /**
     * 16进制字符串转成10进制字符串
     * @param hex
     * @return
     */
    public static String hexToString(String hex){
    	return String.valueOf(Long.valueOf(hex, 16));
    }
    
    public static int hexToInt(String hex){
    	return Integer.valueOf(hex, 16);
    }
    
    public static long hexTolong(String hex){
    	return Long.valueOf(hex, 16);
    }
    
    public static String hexToString(long l,int times){
    	return String.valueOf((double)l/times);
    }
    
    public static String hexToString(String hex,int times,double d){
    	return String.valueOf(hexTo2String(hex, times)*d);
    }
    
    public static String hexToString(String hex,int times){
    	return String.valueOf(hexTo2String(hex, times));
    }
    
    public static double hexTo2String(String hex,int times){
    	return (double)Long.valueOf(hex,16)/times;
    }
    /**
     * 16进制字符串转成2进制字符串
     * @param str
     * @return
     */
    public static String hexTo2BCD(String str){
//    		String str = new String(bytevalue);
    		StringBuilder sb = new StringBuilder();
    		byte[] bytevalue = str.getBytes();
    		for(int i=0;i<bytevalue.length;i++){
    			switch((char)bytevalue[i]) {
    				case '0': sb.append("0000");continue; 
    				case '1': sb.append("0001");continue;
    				case '2': sb.append("0010");continue;
    				case '3': sb.append("0011");continue;
    				case '4': sb.append("0100");continue;
    				case '5': sb.append("0101");continue;
    				case '6': sb.append("0110");continue;
    				case '7': sb.append("0111");continue;
    				case '8': sb.append("1000");continue;
    				case '9': sb.append("1001");continue;
    				case 'a': sb.append("1010");continue;
    				case 'b': sb.append("1011");continue;
    				case 'c': sb.append("1100");continue;
    				case 'd': sb.append("1101");continue;
    				case 'e': sb.append("1110");continue;
    				case 'f': sb.append("1111");continue;
    				case 'A': sb.append("1010");continue;
    				case 'B': sb.append("1011");continue;
    				case 'C': sb.append("1100");continue;
    				case 'D': sb.append("1101");continue;
    				case 'E': sb.append("1110");continue;
    				case 'F': sb.append("1111");continue;
    				default:
    					break;
    			}
//    			sb.append(InsideMsgUtils.formatSignal(Integer.toBinaryString(Integer.parseInt(str.substring(i,i+1)))));
    		}
//    		System.out.println(sb.toString());
    		return sb.toString();
	}
    
    public static String bytestoHexstring(byte[] b){
    	String aa = "";
    	for (int i=0;i<b.length;i++){
    		System.out.print(byteToHexString(b[i]));
    	}
    	return aa;
    }
    
    /**
     * 将编码为Base64的字符串转换为普通的字符串
     * @param str
     * @return
     */
    public static String decodeString(String str) {
//        sun.misc.BASE64Decoder dec = new sun.misc.BASE64Decoder();
//        try {
            return new String(Base64_URl.base64DecodeToArray(str));
//            return new String(dec.decodeBuffer(str));
//        } catch (IOException ex) {
//            log.error(ex.getMessage());
//            throw new RuntimeException(ex.getMessage(), ex.getCause());
//        }
    }
    
    public static  void main(String args[]){
//    	String s1 = "youhaocs";
//    	String s = Base64.encode(Base64.encode(new String(Converser.Encryptbyte("123456"))));
//    	System.out.println(new String("50".getBytes()));
//    	System.out.println(hexTo2String("50", 16));
//    	System.out.println(Base64.encode(s1));
    	
    	byte aa=(byte)0x81;
    	System.out.println(byteToHexString(aa));
    	System.out.println(hexTo2BCD(byteToHexString(aa)));
    } 
}
