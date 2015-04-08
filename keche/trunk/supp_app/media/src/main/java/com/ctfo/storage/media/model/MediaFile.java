/**
 * 
 */
package com.ctfo.storage.media.model;

/**
 * 多媒体文件
 *
 */
public class MediaFile {
	/**	多媒体文件名称	*/
	private String name; 
	/**	多媒体文件内容	*/
	private byte[] content;
	/**
	 * @return 获取 多媒体文件名称
	 */
	public String getName() {
		return name;
	}
	/**
	 * 设置多媒体文件名称
	 * @param name 多媒体文件名称 
	 */
	public void setName(String name) {
		this.name = name;
	}
	/**
	 * @return 获取 多媒体文件内容
	 */
	public byte[] getContent() {
		return content;
	}
	/**
	 * 设置多媒体文件内容
	 * @param content 多媒体文件内容 
	 */
	public void setContent(byte[] content) {
		this.content = content;
	}
	
}
