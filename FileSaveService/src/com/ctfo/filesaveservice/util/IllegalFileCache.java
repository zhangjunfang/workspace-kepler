package com.ctfo.filesaveservice.util;

/*****************************************
 * <li>描        述：非法文件缓存		
 * 
 *****************************************/
public class IllegalFileCache {
	/**	文件名称(车辆编号+_+车牌号)	*/
	private String fileName;
	/**	文件内容	*/
	private String filecontent ; //轨迹文件
	/**	文件存储时间格式	*/
	private String date ;
	/**	文件数据量	*/
	private int recordCount = 0; // 

	public void addRecordCount(int num){
		recordCount = recordCount + num;
	}
	public void resetUnit(){
		this.recordCount = 0;
		this.filecontent = "";
		this.date = "";
	}
	public void resetRecordCount(){
		this.recordCount = 0;
		this.filecontent = "";
	}
	public int getRecordCount(){
		return this.recordCount;
	}
	public String getFileName() {
		return fileName;
	}
	public void setFileName(String fileName) {
		this.fileName = fileName;
	}
	public String getDate() {
		return date;
	}
	public void setDate(String date) {
		this.date = date;
	}
	public void setRecordCount(int recordCount) {
		this.recordCount = recordCount;
	}
	public String getFilecontent() {
		return filecontent;
	}
	public void setFilecontent(String filecontent) {
		this.filecontent = filecontent;
	}
}
