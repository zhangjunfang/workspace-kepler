package com.caits.analysisserver.utils;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.util.ArrayList;
import java.util.List;


public class FileUtils {
	
	public static String replaceRoot(String pathFile, String oldRoot,String root){
		pathFile = pathFile.replaceAll("\\\\", "/");
		return oldRoot + pathFile.substring(pathFile.indexOf(root)+ root.length());
	}

	
	public static void main(String[] args){
		System.out.println(replaceRoot("D:/alarm/2011/10/1.txt","D:/event","alarm"));
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
}
