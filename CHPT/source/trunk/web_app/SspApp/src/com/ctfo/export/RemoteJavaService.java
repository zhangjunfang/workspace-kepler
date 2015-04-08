package com.ctfo.export;

import com.ctfo.local.exception.CtfoAppException;

public interface RemoteJavaService {
	/**
	 * 导出公用方法
	 * 
	 * @param exportDataHeader
	 *            Action传给后台的头文件
	 * @param url
	 *            url请求地址
      * @param beanName
	 *            convertTable.xml 文件中类节点名称
	 * @param result
	 *            结果数据
	 * @return 下载的url地址
	 */
	public String exportData(String exportDataHeader, String result, String url,String beanName,String sheetName,String funName) throws CtfoAppException;
	public String exportData(String exportDataHeader, String result, String url,String beanName,String funName) throws CtfoAppException;
}
