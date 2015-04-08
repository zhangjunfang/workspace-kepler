package com.ctfo.util;

import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.Date;
import java.util.UUID;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class StringUtil {
	
	public static String genUUID(){
		return UUID.randomUUID().toString().replace("-","");
	}

	/**
	 * 开发人： 张波
	 * 开发时间： 2013-07-09 下午02:04:42
	 * 功能描述：判断是否为空
	 * 方法的参数和返回值
	 * @param arg
	 * @return
	 * boolean 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static boolean isBlank(String arg){
		return arg==null || "".equals(arg.trim());
	}
	
	/**
	 * 开发人： 张波
	 * 开发时间： 2013-07-09 下午02:04:44
	 * 功能描述：判断是否为非空
	 * 方法的参数和返回值
	 * @param arg
	 * @return
	 * boolean 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static boolean isNotBlank(String arg){
		return !isBlank(arg);
	}
	/**
	 * 开发人： 陈园
	 * 开发时间： 2013-07-09 下午02:04:44
	 * 功能描述：判断是否为非空 为空返回空字符串
	 * 方法的参数和返回值
	 * @param arg
	 * @return
	 * boolean 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static String isBlankReturn(String str){
		return isBlank(str) ? "":str;
	}
	/**
	 * 开发人： 张波
	 * 开发时间： 2012-3-10 下午3:15:29
	 * 功能描述：判断是否为整形数字
	 * 方法的参数和返回值
	 * @param arg
	 * @return
	 * boolean 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static boolean isInteger(String arg){
		try {
			Integer.parseInt(arg);
		} catch (NumberFormatException e) {
			return false;
		}
		return true;
	}

	/**
	 * 开发人： 张波
	 * 开发时间： 2012-3-10 下午3:15:43
	 * 功能描述：判断是否为浮点数
	 * 方法的参数和返回值
	 * @param arg
	 * @return
	 * boolean 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static boolean isFloat(String arg){
		try {
			Float.parseFloat(arg);
		} catch (NumberFormatException e) {
			return false;
		}
		return true;
	}

	public static String[] split(String str, char split) {
		if (str == null) {
			return null;
		}
		int i = str.length();
		if (i == 0) {
			return new String[0];
		}
		return str.split(""+split);
	}

	public static String substringBeforeLast(String str,
			String flag) {
		if ((str == null) || (flag == null)
				|| (str.length() == 0) || (flag.length() == 0)) {
			return str;
		}
		int i = str.lastIndexOf(flag);
		if (i == -1) {
			return str;
		}
		return str.substring(0, i);
	}
	/**
	 * 开发人： 张波
	 * 开发时间： 2013-07-09 下午02:31:06
	 * 功能描述：将字符串数组抓换成以“,”相隔的字符串,并根据isWrap参数确定是否每项都以“‘”包围
	 * 方法的参数和返回值
	 * @param sourceStr   
	 * @param isWrap  解析的字符串是否以”’“进行包围显示
	 * @return
	 * String 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static String fromArrayToStr(String[] sourceStr,boolean isWrap){
		String str = "";
		if(sourceStr!=null&&sourceStr.length>0){
			for(int i = 0;i<sourceStr.length-1;i++){
				if(isWrap)
					str = str + "'" + sourceStr[i] +"',";
				else
					str = str + sourceStr[i] +",";
			}
			if(isWrap)
				str = str + "'" +sourceStr[sourceStr.length-1] + "'";
			else
			    str = str + sourceStr[sourceStr.length-1];
		}
		return str;
	}
	
	/**
	 * 开发人： 张波
	 * 开发时间：  2013-07-09 上午10:14:04
	 * 功能描述：移除以某一种分隔符组成的字符串中的重复字符
	 * 方法的参数和返回值
	 * @param sourceStr   11,22,11,33,
	 * @param separator   ,
	 * @return            11,22,33
	 * String 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static String removeRepStr(String sourceStr,String separator){
		String[] sepStrs = sourceStr.split(separator);
		String reStr = ",";   //要返回的字符串
		for(int i=0;i<sepStrs.length;i++){
			if(StringUtil.isNotBlank(sepStrs[i])&&reStr.indexOf(","+sepStrs[i]+",")<0){
				reStr = reStr +  sepStrs[i] + ",";
			}
		}
		if(StringUtil.isNotBlank(reStr))
			reStr = reStr.substring(0,reStr.length()-1);
		return reStr;
	}

    /**
     * 开发人： 张波
     * 开发时间： 2011-6-23 上午11:29:46
     * 功能描述：按字节数截取字符串，汉字按2字节
     * 方法的参数和返回值
     * @param str
     * @param len
     * @return
     * String 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
    public static String cutString(String str, int len) {
    	len = len -3;
    	if(StringUtil.isBlank(str))
    		return "";
        if (len+3 > str.length()*2)
            return str;
        int cnCount = 0;
        for (int i = 0; i < str.length(); i++){
            if (str.charAt(i)>Byte.MAX_VALUE)
                cnCount+=2;
            else
            	cnCount++;
            if(cnCount>=len){
            	if(str.length()-i<=2){
            		return str;
            	}
            	else{
            		return str.substring(0,i)+"...";
            	}
            }
        }
        return str;
    }

	/**
	 * 开发人： 张波
	 * 开发时间：  2013-07-09 上午11:30:18
	 * 功能描述：将文件大小格式化成易读的字符串
	 * 方法的参数和返回值
	 * @param fileSize
	 * @return
	 * String 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static String formatFileSize(long fileSize){
        DecimalFormat formater = new DecimalFormat();
        formater.applyPattern("###.##");
        if(fileSize < 1024){
            return fileSize + " B";
        }
        else if(fileSize < 1024*1024){
            return formater.format(fileSize/1024f) + " KB";
        }
        else if(fileSize < 1024*1024*1024){
            return formater.format(fileSize/(1024*1024f)) + " MB";
        }
        else{
            return formater.format(fileSize/(1024*1024*1024f)) + " GB";
        }
	}
	
	/**
	 * 开发人： 张波
	 * 开发时间：  2013-07-09 上午08:52:42
	 * 功能描述：比较两个字符串内容是否相等，设定null=="" = true
	 * 方法的参数和返回值
	 * @param arg1
	 * @param arg2
	 * @return
	 * boolean 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static boolean equals(String arg1,String arg2){
		if("".equals(arg1))arg1=null;
		if("".equals(arg2))arg2=null;
		return equals((Object)arg1,(Object)arg2);
	}
	/**
	 * 开发人： 张波
	 * 开发时间：  2013-07-09 上午08:55:32
	 * 功能描述：比较两个对象是否相等
	 * 方法的参数和返回值
	 * @param arg1
	 * @param arg2
	 * @return
	 * boolean 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static boolean equals(Object arg1,Object arg2){
		if("".equals(arg1))arg1=null;
		if("".equals(arg2))arg2=null;
		
		if(arg1==null && arg2==null){
			return true;
		}
		else if(arg1==null || arg2==null){
			return false;
		}
		else{
			return arg1.equals(arg2);
		}
	}
	/**
	 * 开发人： 张波
	 * 开发时间：  2013-07-09 上午08:58:29
	 * 功能描述：将一个字符串数组以,分隔组成一个字符串
	 * 方法的参数和返回值
	 * @param ary
	 * @return
	 * String 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static String join(String[] ary){
		return join(ary,",");
	}
	/**
	 * 开发人： 张波
	 * 开发时间： 2013-07-09 上午08:58:57
	 * 功能描述：将一个字符串数组以指定字符分隔组成一个字符串
	 * 方法的参数和返回值
	 * @param ary
	 * @param split
	 * @return
	 * String 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static String join(String[] ary,String split){
		if(ary==null||ary.length==0){
			return "";
		}
		if(split==null){
			split = ",";
		}
		StringBuffer buff = new StringBuffer();
		for(String str:ary){
			buff.append(split).append(str);
		}
		return buff.substring(split.length());
	}
	/**
	 * 开发人： 张波
	 * 开发时间：  2013-07-09 下午07:17:38
	 * 功能描述：按指定正则表达式对给定字符串进行解析，返回第一个匹配的捕获组，如正则表达式中未定义捕获组，则返回所有匹配字符
	 * 方法的参数和返回值
	 * @param src		要解析的字符串
	 * @param regex		正则表达式
	 * @return
	 * String 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static String getFirstMatch(String src,String regex){
		if(src==null){
			return null;
		}
		Pattern p = Pattern.compile(regex);
		Matcher m = p.matcher(src);
		if(m.find()){
			return m.groupCount()>0?m.group(1):m.group();
		}
		return null;
	}

	/**
	 * 开发人： 张波
	 * 开发时间：  2013-07-09 上午11:30:57
	 * 功能描述：转换字符串编码
	 * 方法的参数和返回值
	 * @param src
	 * @param fromEncode
	 * @param toEncode
	 * @return
	 * String 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static String transEncode(String src,String fromEncode,String toEncode) {
		String des = null;
		if (src == null)
			return "";
		try {
			byte[] temp = src.getBytes(fromEncode);
			des = new String(temp, toEncode);
		} catch (Exception e) {
			//LogUtil.logInfo("-----------Error trans from "+fromEncode+" to "+toEncode+"!");
		}
		return des == null ? "" : des;
	}
	public static String IsoToGB2312(String src) {
		return transEncode(src,"ISO8859-1","GB2312");
	}

	public static String IsoToUTF_16(String src) {
		return transEncode(src,"ISO8859-1","UTF-16");
	}

	public static String GB2312ToIso(String src) {
		return transEncode(src,"GB2312","ISO8859-1");
	}

	public final static String GBKToISO(String src) {
		return transEncode(src,"GBK","ISO8859-1");
	}

	public final static String[] GBKToISO(String[] src) {
		if (src.length != 0) {
			String[] tmp = new String[src.length];
			for (int i = 0; i < src.length; i++) {
				tmp[i] = GBKToISO(src[i]);
			}
			return tmp;
		}
		return null;
	}

	/**
	 * 开发人： 张波
	 * 开发时间：  2013-07-09 上午11:31:10
	 * 功能描述：去掉字符串两端的空白，如果参数为null仍返回null
	 * 方法的参数和返回值
	 * @param str
	 * @return
	 * String 
	 * ==================================
	 * 修改历史
	 * 修改人        修改时间      修改原因及内容
	 *
	 * ==================================
	 */
	public static String trim(String str) {
		if (str == null) {
			return str;
		} else {
			return str.trim();
		}
	}

    /**
     * 开发人： 张波
     * 开发时间： 2011-6-23 上午11:31:31
     * 功能描述：将第一个字符大写
     * 方法的参数和返回值
     * @param arg
     * @return
     * String 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
    public static String caseStartChar(String arg) {
        if (isBlank(arg)){
            return arg;
        }
        String strTmp = arg.substring(0, 1);
        strTmp = strTmp.toUpperCase();
        strTmp += arg.substring(1);
        return strTmp;
    }
    
    /**
     * 开发人： 张波
     * 开发时间：  2013-07-09 下午01:40:00
     * 功能描述：转换html字符
     * 方法的参数和返回值
     * @param inputs
     * @return
     * String 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
    public static String escapeHTMLTags(String inputs){
    	if(StringUtil.isNotBlank(inputs))
    	    return inputs.replace("<", "&lt;").replace(">", "&gt;").replace("&", "&amp;");
    	else
    		return null;
    }
    
    /**
     * 开发人： 张波
     * 开发时间：  2013-07-09 下午01:40:21
     * 功能描述： 过滤sql中的特殊字符
     * 方法的参数和返回值
     * @param content
     * @return
     * String 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
    public static String filterSqlContent(String content){
//    	String flt = "'，and，exec，insert，select，delete，update，count，*，%，chr，mid，master，truncate，char，declare，;，or，-，+，,";
    	String flt = "'，*，%，;，--，+，,";
    	String filter[] = flt.split("，"); 
    	for(int i=0;i<filter.length;i++)  {
    	   content = content.replace(filter[i], "#"); 
    	}
    	  return content; 
    	}
    /**
     * 
     * @author ： 陈园
     * @since： 2013-8-15 下午2:40:45
     * 功能描述：传过来两个数转化为百分比的形式  p1所占p2 的百分比
     * 方法的参数和返回值
     * @param p1 除数
     * @param p2 被除数 不能为0
     * @return
     * String 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
    public static String percentFormat(double p1,double p2){
    	 String str;
    	 double p3=0;
    	 NumberFormat nf=NumberFormat.getPercentInstance(); 
    	 if(p2!=0){
    		 p3=p1/p2;
    	 }
    	 nf.setMinimumFractionDigits(0); 
		 str=nf.format(p3); 
		 return str;
    }
    /**
     * 
     * @author ： 陈园
     * @since： 2013-8-17 上午11:30:03
     * 功能描述：生成流水号 如20130810002   年月后的以为代表1 代表预约单 2代表自建订单 3 代表结算单
     * 方法的参数和返回值
     * @param sum
     * @param flag
     * @return
     * String 
     * ==================================
     * 修改历史
     * 修改人        修改时间      修改原因及内容
     *
     * ==================================
     */
    public  static String serialCodeMax(int sum,int flag){
		 
		  String orderNumber = DateUtil.format(new Date(), "yyyyMM") ;  
		  String strCode = String.valueOf(sum);
		  strCode.length();
		  for(int i = 4-strCode.length(); i>0 ;i--){
			  strCode="0"+strCode;
		  }
		  return orderNumber+String.valueOf(flag)+strCode;
	  }
    public static  boolean checkDoubleStr(String str1,String str2){
    	boolean flag = false;
    	if(StringUtil.isBlank(str1)&&StringUtil.isBlank(str2)){
    		flag=true;
    	}
    	if(StringUtil.isNotBlank(str1)&&StringUtil.isNotBlank(str2)){
    		flag=true;
    	}
    	return flag;
    }
}
