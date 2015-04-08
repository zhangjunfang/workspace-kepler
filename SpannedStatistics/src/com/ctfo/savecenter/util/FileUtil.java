package com.ctfo.savecenter.util;

import java.io.File;
import java.io.FileInputStream;
import java.util.Calendar;
import java.util.StringTokenizer;

public class FileUtil {
	// 生成文件目录
	public static void createFolder(String fileurl) {
		Calendar cal = Calendar.getInstance();
		int year = cal.get(Calendar.YEAR);
		int month = cal.get(Calendar.MONTH) + 1;
		int count =0;
		while(count < 2){ //连续生成3年的文件夹
			File yearFilePath = new File(fileurl + "/" + year);
			if (!yearFilePath.exists()) {
				yearFilePath.mkdir();
				for (int i = month; i <= 12; i++) {
					String monthString = String.valueOf(i);
					if (monthString.length() == 1)
						monthString = "0" + monthString;
					File monthFilePath = new File(fileurl + "/" + year + "/"
							+ monthString);
					if (!monthFilePath.exists()) {
						monthFilePath.mkdir();
						cal.set(Calendar.MONTH, i - 1);
						for (int j = 1; j <= cal.getActualMaximum(Calendar.DATE); j++) {
							String dayString = String.valueOf(j);
							if (dayString.length() == 1)
								dayString = "0" + dayString;
							File dayFilePath = new File(fileurl + "/" + year + "/"
									+ monthString + "/" + dayString);
							if (!dayFilePath.exists()) {
								dayFilePath.mkdir();
							}
						}//End for
					}
				}//End for
				month = 1;
			}
			year = year + 1;
			count++;
		}// End while
	}
	
	// 生成文件目录
	public static void coverFolder(String fileurl) {
		Calendar cal = Calendar.getInstance();
		int year = cal.get(Calendar.YEAR);
		//int month = cal.get(Calendar.MONTH) + 1;
//		int comp=year+10;
		//while(comp>year){
		
		int month=1;
		File yearFilePath = new File(fileurl + "/" + year);
		 
			yearFilePath.mkdir();
			for (int i = month; i <= 12; i++) {
				String monthString = String.valueOf(i);
				if (monthString.length() == 1)
					monthString = "0" + monthString;
				File monthFilePath = new File(fileurl + "/" + year + "/"
						+ monthString);
			 
					monthFilePath.mkdir();
					cal.set(Calendar.MONTH, i - 1);
					for (int j = 1; j <= cal.getActualMaximum(Calendar.DATE); j++) {
						String dayString = String.valueOf(j);
						if (dayString.length() == 1)
							dayString = "0" + dayString;
						File dayFilePath = new File(fileurl + "/" + year + "/"
								+ monthString + "/" + dayString);
						if (!dayFilePath.exists()) {
							dayFilePath.mkdir();
						}
					}
				 
			}
		 
		year++;
		//}
	}
	

	/**
	 * 从当前路径和classpath中搜索指定文件名的文件
	 * 
	 * @param filename
	 *            要搜索的文件名,不能包含路径
	 * @return 包含完整路径的文件名,如果没有搜索到则返回的是null
	 */
	public static String getFileInClassPath(String filename) {
		FileInputStream fin = null;
		// 搜索当前路径
		try {
			fin = new FileInputStream(filename);
			fin.close();
			return filename;
		} catch (Exception e) {
		}
		// 获得系统变量classpath值
		String classpath = System.getProperty("java.class.path");
		StringTokenizer stk = new StringTokenizer(classpath, File.pathSeparator);
		String path = null;
		// 依次搜索classpath
		while (stk.hasMoreTokens()) {
			path = stk.nextToken();
			if (!path.endsWith(File.separator))
				path += File.separator;
			try {
				fin = new FileInputStream(path + filename);
				fin.close();
				// 找到文件则返回
				return path + filename;
			} catch (Exception e) {
			}
		}
		return null;
	}

}
