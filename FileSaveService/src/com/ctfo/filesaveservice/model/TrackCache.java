package com.ctfo.filesaveservice.model;


public class TrackCache {
	/**	文件内容	*/
	private String content = ""; //轨迹文件
	/**	文件路径	*/
	private String path;
	/**	文件内容数量	*/
	private int count; // 文件数据量
	/**
	 * 重置内容
	 */
	public void resetContent(){
		this.count = 0;
		this.content = "";
	}
	/**
	 * 重置所以参数
	 */
	public void resetAll(){
		this.count = 0;
		this.content = "";
		this.path = "";
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
		this.count++;
	}
	/**
	 * 获得文件路径的值
	 * @return the path 文件路径  
	 */
	public String getPath() {
		return path;
	}
	/**
	 * 设置文件路径的值
	 * @param path 文件路径  
	 */
	public void setPath(String path) {
		this.path = path;
	}
	/**
	 * 获得文件内容数量的值
	 * @return the count 文件内容数量  
	 */
	public int getCount() {
		return count;
	}
	/**
	 * 设置文件内容数量的值
	 * @param count 文件内容数量  
	 */
	public void setCount(int count) {
		this.count = count;
	}
	
}
