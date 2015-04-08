package com.caits.analysisserver.database;

import java.io.File;
import java.io.FileFilter;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Vector;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.StaPool;
import com.caits.analysisserver.utils.MathUtils;

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
				path = path + "/" + MathUtils.addSuffixZero(month);
				File monthDir = new File(path);
				if (monthDir.exists()) {
					path = path + "/" + MathUtils.addSuffixZero(day);
					File dayDir = new File(path);
					if (dayDir.exists()) {
						logger.info("统计文件夹路径:" + dayDir.getAbsolutePath());
						//清空缓存
						StaPool.clearCountList();
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
	
	public static Vector<File> loadAssignFile(String fileUrl,long utc,String[] fileNames) {
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
				path = path + "/" + MathUtils.addSuffixZero(month);
				File monthDir = new File(path);
				if (monthDir.exists()) {
					path = path + "/" + MathUtils.addSuffixZero(day);
					File dayDir = new File(path);
					if (dayDir.exists()) {
						logger.info("统计文件夹路径:" + dayDir.getAbsolutePath());
						
						for (int i=0;i<fileNames.length;i++){
							String fileName = fileNames[i].trim();
							File f = new File(path+"/"+fileName);
							if (f.exists()&&f.isFile()){
								fileList.add(f);
							}else{
								logger.debug("不存在文件："+path+"/"+fileName);
							}
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
		} else {
			logger.debug(path + " 文件夹不存在");
		}
		return fileList;
	}
}
