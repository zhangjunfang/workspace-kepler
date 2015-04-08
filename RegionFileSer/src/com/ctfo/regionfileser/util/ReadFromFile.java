/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.regionfileser.util;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.RandomAccessFile;
import java.io.Reader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.UUID;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

/**
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： MonitorSer
 * <br>
 * 功能： 读取文件
 * <br>
 * 描述：
 * <br>
 * 授权 : (C) Copyright (c) 2011
 * <br>
 * 公司 : 北京中交兴路信息科技有限公司
 * <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史
 * <br>
 * <table width="432" border="1">
 * <tr><td>版本</td><td>时间</td><td>作者</td><td>改变</td></tr>
 * <tr><td>1.0</td><td>2011-10-8</td><td>yangjian</td><td>创建</td></tr>
 * </table>
 * <br>
 * <font color="FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font>
 * <br>
 * 
 * @version 1.0
 * 
 * @author yangjian
 * @since JDK1.6
 */
public class ReadFromFile {
	
	
	/**
	 * 日志
	 */
	private static Log log = LogFactory.getLog(ReadFromFile.class);
	
	
	 /**
     * 以字节为单位读取文件，常用于读二进制文件，如图片、声音、影像等文件。
     */
    public static void readFileByBytes(String fileName) {
        File file = new File(fileName);
        InputStream in = null;
        try {
            log.debug("以字节为单位读取文件内容，一次读一个字节：");
            // 一次读一个字节
            in = new FileInputStream(file);
            int tempbyte;
            while ((tempbyte = in.read()) != -1) {
                System.out.write(tempbyte);
            }
            in.close();
        } catch (IOException e) {
        	log.error(e.fillInStackTrace());
            return;
        }
        try {
            log.debug("以字节为单位读取文件内容，一次读多个字节：");
            // 一次读多个字节
            byte[] tempbytes = new byte[100];
            int byteread = 0;
            in = new FileInputStream(fileName);
            ReadFromFile.showAvailableBytes(in);
            // 读入多个字节到字节数组中，byteread为一次读入的字节数
            while ((byteread = in.read(tempbytes)) != -1) {
                System.out.write(tempbytes, 0, byteread);
            }
        } catch (Exception e) {
        	log.error(e.fillInStackTrace());
        } finally {
            if (in != null) {
                try {
                    in.close();
                } catch (IOException e) {
                	log.error(e.fillInStackTrace());
                }
            }
        }
    }

    /**
     * 以字符为单位读取文件，常用于读文本，数字等类型的文件
     */
    public static void readFileByChars(String fileName) {
        File file = new File(fileName);
        Reader reader = null;
        try {
            log.debug("以字符为单位读取文件内容，一次读一个字节：");
            // 一次读一个字符
            reader = new InputStreamReader(new FileInputStream(file));
            int tempchar;
            while ((tempchar = reader.read()) != -1) {
                // 对于windows下，\r\n这两个字符在一起时，表示一个换行。
                // 但如果这两个字符分开显示时，会换两次行。
                // 因此，屏蔽掉\r，或者屏蔽\n。否则，将会多出很多空行。
                if (((char) tempchar) != '\r') {
                    System.out.print((char) tempchar);
                }
            }
            reader.close();
        } catch (Exception e) {
        	log.error(e.fillInStackTrace());
        }
        try {
            log.debug("以字符为单位读取文件内容，一次读多个字节：");
            // 一次读多个字符
            char[] tempchars = new char[30];
            int charread = 0;
            reader = new InputStreamReader(new FileInputStream(fileName));
            // 读入多个字符到字符数组中，charread为一次读取字符数
            while ((charread = reader.read(tempchars)) != -1) {
                // 同样屏蔽掉\r不显示
                if ((charread == tempchars.length)
                        && (tempchars[tempchars.length - 1] != '\r')) {
                    System.out.print(tempchars);
                } else {
                    for (int i = 0; i < charread; i++) {
                        if (tempchars[i] == '\r') {
                            continue;
                        } else {
                            System.out.print(tempchars[i]);
                        }
                    }
                }
            }

        } catch (Exception e) {
        	log.error(e.fillInStackTrace());
        } finally {
            if (reader != null) {
                try {
                    reader.close();
                } catch (IOException e) {
                	log.error(e.fillInStackTrace());
                }
            }
        }
    }

    /**
     * 以行为单位读取文件，常用于读面向行的格式化文件 
     */
    @SuppressWarnings("unchecked")
	public static List<String> readFileByLines(String fileName) throws Exception{
        File file = new File(fileName);
        BufferedReader reader = null;
        List dataList=null;
        
        try {
             log.debug("以行为单位读取文件内容，一次读一整行：");
            reader = new BufferedReader(new FileReader(file));
            String tempString = null;
            int line = 1;
            dataList=new ArrayList();
            // 一次读入一行，直到读入null为文件结束
            while ((tempString = reader.readLine()) != null) {
                // 显示行号
                log.debug("line " + line + ": " + tempString);
                dataList.add(tempString);
                line++;
            }
            reader.close();
            return dataList; 
        } catch (IOException e) {
        	throw e;
        } finally {
        	   if (reader != null) {
                   reader.close(); 
               } 
        }
    }
    /**
     * 根据开始、结束时间 读取格式化文件 
     */
    @SuppressWarnings("unchecked")
	public static List<String> readFileByDate(String fileName,Date startTime,Date endTime,Integer gpsDateSite) throws Exception{
        File file = new File(fileName);
        BufferedReader reader = null;
        List dataList=null;
        // gps时间
        Date gpsDate=null;  
        // 分隔后的数据
		String[] splitArray = null;
        try {
             log.debug("以行为单位读取文件内容，一次读一整行：");
            reader = new BufferedReader(new FileReader(file));
            String tempString = null;
            int line = 1;
            dataList=new ArrayList();
            // 一次读入一行，直到读入null为文件结束
            while ((tempString = reader.readLine()) != null) {
                // 显示行号
             if(tempString!=null && tempString.length()>0){
            	 
                log.debug("line " + line + ": " + tempString);                
                splitArray = tempString.split(":");
				// "20111008/tempString"
                if(splitArray!=null && splitArray.length>0){
				gpsDate = getGpsDate(splitArray[gpsDateSite]);
				// 判断该列数据是不是在startTime和endTime之间
				if (gpsDate != null && gpsDate.after(startTime)
						&& gpsDate.before(endTime)) {                
                    dataList.add(tempString);
				} 
                line++;
                }
             }
             
            }
            reader.close();
            return dataList; 
        } catch (IOException e) {
        	throw e;
        } catch (Exception e) {
        	throw e;
        }finally {
        	   if (reader != null) {
                   reader.close(); 
               } 
        }
    }
    
    private static int BUFFER_SIZE = 8096;
    /**
     * 以行为单位读取文件，常用于读面向行的格式化文件 
     */
    public static List<String> readFileByLinesFromUrl(String path,String destUrl) throws Exception{  
    	
    	
    	//sun.net.ftp.FtpClient ftp=new 
    FileOutputStream fos = null;
         BufferedInputStream bis = null;
       HttpURLConnection httpUrl = null;
       URL url = null;
        try
        {
        	
          byte[] buf = new byte[BUFFER_SIZE];
          int size = 0;
            destUrl="http://localhost:8080/trackFileSer/txt/15.txt";
           url = new URL(destUrl);
          httpUrl = (HttpURLConnection)url.openConnection();
    
         System.out.println(httpUrl);
          httpUrl.connect();
    
          bis = new BufferedInputStream(httpUrl.getInputStream());
        
          String filename=UUID.randomUUID().toString();
          String filePath=path+File.separator+filename+".txt";
          fos = new FileOutputStream(filePath);
  
          while ((size = bis.read(buf)) != -1) {
           fos.write(buf, 0, size);
          }
         fos.close();
          bis.close();
          httpUrl.disconnect();
          
          List<String> list=readFileByLines(filePath);
          
          File file = new File(filePath);file.delete();
          
          return list;
        } catch (Throwable t) {
	            throw new RuntimeException(t);
        }
        }
    /**
     * 随机读取文件内容
     */
    public static void readFileByRandomAccess(String fileName) {
        RandomAccessFile randomFile = null;
        try {
            log.debug("随机读取一段文件内容：");
            // 打开一个随机访问文件流，按只读方式
            randomFile = new RandomAccessFile(fileName, "r");
            // 文件长度，字节数
            long fileLength = randomFile.length();
            // 读文件的起始位置
            int beginIndex = (fileLength > 4) ? 4 : 0;
            // 将读文件的开始位置移到beginIndex位置。
            randomFile.seek(beginIndex);
            byte[] bytes = new byte[10];
            int byteread = 0;
            // 一次读10个字节，如果文件内容不足10个字节，则读剩下的字节。
            // 将一次读取的字节数赋给byteread
            while ((byteread = randomFile.read(bytes)) != -1) {
                System.out.write(bytes, 0, byteread);
            }
        } catch (IOException e) {
        	log.error(e.fillInStackTrace());
        } finally {
            if (randomFile != null) {
                try {
                    randomFile.close();
                } catch (IOException e) {
                	log.error(e.fillInStackTrace());
                }
            }
        }
    }

    /**
     * 显示输入流中还剩的字节数
     */
    private static void showAvailableBytes(InputStream in) {
        try {
            log.debug("当前字节输入流中的字节数为:" + in.available());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    /**
	 * 将上报的gps时间字符串转换为date类型
	 * 
	 * @param dateStr
	 * @return
	 */
	private static Date getGpsDate(String dateStr) {

		// DEFAULT_FORMATSTR="yyyy-MM-dd hh:mm:ss";
		// 20111008/193923
		StringBuffer sbu = new StringBuffer("");
		String dateSeparate = "-";
		String timeSeparate = ":";

		if (dateStr != null && dateStr.length() == 15) {

			// 年
			sbu.append(dateStr.substring(0, 4));
			sbu.append(dateSeparate);
			// 月
			sbu.append(dateStr.substring(4, 6));
			sbu.append(dateSeparate);
			// 日
			sbu.append(dateStr.substring(6, 8));
			sbu.append(" ");
			// 小时
			sbu.append(dateStr.substring(9, 11));
			sbu.append(timeSeparate);
			// 分钟
			sbu.append(dateStr.substring(11, 13));
			sbu.append(timeSeparate);
			// 秒
			sbu.append(dateStr.substring(13, 15));
			try {

				return DateUtil.getDate(DateUtil.DEFAULT_FORMATSTR,
						sbu.toString());
			} catch (Exception e) {
				log.debug(e.fillInStackTrace());
				return null;
			}
		} else {
			return null;
		}

	}

    public static void main(String[] args) throws Exception {
    	//readFileByLinesFromUrl("");
       /* String fileName = "D:/newTemp.txt";
        ReadFromFile.readFileByBytes(fileName);
        ReadFromFile.readFileByChars(fileName);
        ReadFromFile.readFileByLines(fileName);
        ReadFromFile.readFileByRandomAccess(fileName);*/
    }

}
