package com.ctfo.kypt;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.Reader;
import java.io.Writer;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

import org.apache.tools.ant.BuildException;
import org.apache.tools.ant.Task;

public class AddJsAndCssVersion extends Task {

	private String path;

	public void execute() throws BuildException {
		Date date = new Date();
		SimpleDateFormat df = new SimpleDateFormat("yyyyMMddhhmmss");
		String version = df.format(date);
		addVersion(path, version);
	}

	/**
	 * 遍历web目录中的vm文件，给js和css的引用加上版本号
	 * 
	 * @param path
	 */
	private void addVersion(String path, String version) {
		File dir = new File(path);
		File[] files = dir.listFiles();
		if (files == null)
			return;
		for (int i = 0; i < files.length; i++) {
			if (files[i].isDirectory()) {
				addVersion(files[i].getAbsolutePath(), version);
			} else {
				String strFileName = files[i].getAbsolutePath();
				// 如果是符合条件的文件，则添加版本信息
				if (strFileName.endsWith("version.js")
						|| strFileName.endsWith("index.html")) {
					//System.out.println(strFileName);
					InputStream is = null;
					OutputStream os = null;
					List<String> contentList = new ArrayList<String>();

					// 读文件
					try {
						is = new FileInputStream(files[i]);
						Reader r = new InputStreamReader(is);
						BufferedReader br = new BufferedReader(r);
						String line = null;
						while ((line = br.readLine()) != null) {
							String modLine = getModLine(line, version);
							if (modLine != null) {
								line = modLine;
							}
							line = line + "\r\n";
							contentList.add(line);
						}
						// 关闭流
						br.close();
						r.close();
					} catch (Exception e) {
						System.out.println("读文件失败");
						e.printStackTrace();
					} finally {
						if (null != is) {
							try {
								is.close();
							} catch (Exception e) {
								e.printStackTrace();
							}
						}

					}

					// 写文件
					try {
						os = new FileOutputStream(files[i]);
						Writer w = new OutputStreamWriter(os, "UTF-8");
						BufferedWriter bw = new BufferedWriter(w);
						for (Iterator<String> it = contentList.iterator(); it
								.hasNext();) {
							String line = it.next();
							bw.write(line);
						}
						// 更新到文件
						bw.flush();
						// 关闭流
						bw.close();
						w.close();
					} catch (Exception e) {
						System.out.println("写文件失败");
						e.printStackTrace();
					} finally {
						if (null != os) {
							try {
								os.close();
							} catch (Exception e) {
								e.printStackTrace();
							}
						}
					}
				}
			}
		}
	}

	/**
	 * 查找行中是否有js或css的引用，如果有，则加上版本号
	 * 
	 * @param line
	 */
	private String getModLine(String line, String version) {
		// 增加js版本
		line.trim();
		if (line.indexOf("<script") > -1 && line.indexOf("script>") > -1
				&& line.indexOf("data-main") > -1) {
			// System.out.println(line);
			String modLine = "";
			modLine = line.replace("script/main", "dist/main");
			return modLine;
		} else if (line.indexOf("buildVersion") > -1) {
			String modLine = "var buildVersion = " + "\"" + version + "\"";
			// System.out.println(line);
			return modLine;
		} else {
			return null;
		}
	}

	public void setPath(String path) {
		this.path = path;
	}
}
