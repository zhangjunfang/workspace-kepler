package com.ctfo.statistics.alarm.common;

import java.io.File;
import java.io.FilenameFilter;
/**
 * 轨迹文件过滤器
 *
 */
public class FileNameFilter implements FilenameFilter {

	@Override
	public boolean accept(File dir, String name) {
		return name.endsWith(".txt");
	}
}
