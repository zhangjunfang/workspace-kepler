package com.ctfo.trackservice.io;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileFilter;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.List;
import java.util.Vector;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.util.Tools;


public class LoadFile {
	private static final Logger logger = LoggerFactory.getLogger(LoadFile.class);
	
	/***
	 * 如果没有指定时间，则统计查询前一天车辆列表，否则统计指定时间的车辆
	 * @param fileUrl 加载文件URL
	 * @param utc 指定时间
	 * @return
	 */
	public static Vector<File> findFile(String fileUrl,long utc) {
		// 统计文件集合
		Vector<File> fileList = new Vector<File>();
		
		Calendar cal = Calendar.getInstance();
		
		cal.setTimeInMillis(utc);
		int year = cal.get(Calendar.YEAR);
		int month = cal.get(Calendar.MONTH) + 1;
		int day = cal.get(Calendar.DATE);

		String path = fileUrl;

		File file = new File(path);
		if (file.exists()) {
			path = fileUrl + "/" + year;
			File yearDir = new File(path);
			if (yearDir.exists()) {
				path = path + "/" + Tools.addSuffixZero(month);
				File monthDir = new File(path);
				if (monthDir.exists()) {
					path = path + "/" + Tools.addSuffixZero(day);
					File dayDir = new File(path);
					if (dayDir.exists()) {
						//logger.info("统计文件夹路径:" + dayDir.getAbsolutePath());
						File[] files = dayDir.listFiles(new FileFilter(){
					        public boolean accept(File pathname) {
					            String tmp=pathname.getName().toLowerCase();
					            if(tmp.matches("^(-)?\\d*.txt")){
					                return true;
					            }
					            return false;
					        }
					    });
						fileList.addAll(Arrays.asList(files));
					} else {
						logger.debug(path + " 文件夹不存在");
					}
				} else {
					logger.debug(path + " 文件夹不存在");
				}
			} else {
				logger.debug(path + " 文件夹不存在");
			}
		} else {
			logger.debug(path + " 文件夹不存在");
		}
		return fileList;
	}
	
	/**
	 * 读取文件行到列表中
	 * @param file
	 * @param encoding
	 * @return
	 * @throws IOException
	 */
	public static List<String> readLines(File file, String encoding) throws IOException {
        InputStream in = null;
        try {
            in = openInputStream(file);
            return readLines(in, encoding == null ? "utf-8" : encoding);
        } finally {
        	 try {
                 if (in != null) {
                	 in.close();
                 }
             } catch (IOException ioe) {
                 // ignore
             }
        }
    }
	/**
	 * 按行读取数据
	 * @param input
	 * @param encoding
	 * @return
	 * @throws IOException
	 */
    public static List<String> readLines(InputStream input, String encoding) throws IOException {
        InputStreamReader reader = new InputStreamReader(input, encoding);
        return readLines(reader);
    }
    
    /**
     * 打开文件输入流
     * @param file
     * @return
     * @throws IOException
     */
    public static FileInputStream openInputStream(File file) throws IOException {
        if (file.exists()) {
            if (file.isDirectory()) {
                throw new IOException("File '" + file + "' exists but is a directory");
            }
            if (file.canRead() == false) {
                throw new IOException("File '" + file + "' cannot be read");
            }
        } else {
            throw new FileNotFoundException("File '" + file + "' does not exist");
        }
        return new FileInputStream(file);
    }
    /**
     * 根据reader读取文件到列表
     * @param input
     * @return
     * @throws IOException
     */
    public static List<String> readLines(Reader input) throws IOException {
        BufferedReader reader = toBufferedReader(input);
        List<String> list = new ArrayList<String>();
        String line = reader.readLine();
        while (line != null) {
            list.add(line);
            line = reader.readLine();
        }
        return list;
    }
    /**
     * 获得输入流
     * @param reader
     * @return
     */
    public static BufferedReader toBufferedReader(Reader reader) {
        return reader instanceof BufferedReader ? (BufferedReader) reader : new BufferedReader(reader);
    }
}
