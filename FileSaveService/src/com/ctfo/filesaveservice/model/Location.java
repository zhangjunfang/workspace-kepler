package com.ctfo.filesaveservice.model;
/**
 *	本地轨迹文件对象
 */
public class Location {
	/**	车辆编号	*/
	private String vid;
	/**	文件内容	*/
	private String content;
	/**	目录（格式:/path/file/track/2014/01/01/1.txt）	*/
	private String path;
	
	/**
	 * 获得车辆编号的值
	 * @return the vid 车辆编号  
	 */
	public String getVid() {
		return vid;
	}
	/**
	 * 设置车辆编号的值
	 * @param vid 车辆编号  
	 */
	public void setVid(String vid) {
		this.vid = vid;
	}
	/**
	 * 获得文件内容的值
	 * @return the content 文件内容  
	 */
	public String getContent() {
		return content;
	}
	/**
	 * 设置文件内容的值
	 * @param content 文件内容  
	 */
	public void setContent(String content) {
		this.content = content;
	}
	/**
	 * 获得目录（格式:pathfiletrack201401011.txt）的值
	 * @return the path 目录（格式:pathfiletrack201401011.txt）  
	 */
	public String getPath() {
		return path;
	}
	/**
	 * 设置目录（格式:pathfiletrack201401011.txt）的值
	 * @param path 目录（格式:pathfiletrack201401011.txt）  
	 */
	public void setPath(String path) {
		this.path = path;
	}
	
}
