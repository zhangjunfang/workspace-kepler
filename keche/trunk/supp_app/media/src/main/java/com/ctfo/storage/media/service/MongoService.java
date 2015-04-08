/**
 * 2014-5-12MongoDao.java
 */
package com.ctfo.storage.media.service;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.media.dao.MongoDataSource;
import com.ctfo.storage.media.model.MediaFile;
import com.mongodb.BasicDBObject;
import com.mongodb.gridfs.GridFS;
import com.mongodb.gridfs.GridFSDBFile;
import com.mongodb.gridfs.GridFSInputFile;

/**
 * MongoService
 * @author huangjincheng
 * 2014-5-12上午11:30:53
 * 
 */
public class MongoService {
	private static final Logger log = LoggerFactory.getLogger(MongoService.class);
	
	public MongoService(){
	}
	/**
	 * 存储多媒体文件
	 * @param media		多媒体文件对象
	 * @param dbName	数据库名称
	 * @param mediaDir	图片库名称
	 */
	public void save(MediaFile mediaFile, String dbName, String mediaDir){
		try{
			GridFS gfsPhoto = new GridFS(MongoDataSource.getDb(dbName), mediaDir);
			GridFSInputFile gfsFile = gfsPhoto.createFile(mediaFile.getContent());
			gfsFile.setFilename(mediaFile.getName());
			gfsFile.setContentType("jpeg");
			gfsFile.save();
			log.debug("存储多媒体文件[{}]完成!", mediaFile.getName());
		} catch (Exception e) {
			log.error("存储多媒体文件异常:"+e.getMessage(), e);
		}
	}
	/**
	 * 删除文件
	 * @param fileName 文件名称
	 * @param dbName	数据库名称
	 * @param bucket	数据桶名称
	 */
	public void delete(String fileName, String dbName, String bucket){
		try{
			GridFS gfsPhoto = new GridFS(MongoDataSource.getDb(dbName), bucket);
			gfsPhoto.remove(gfsPhoto.findOne(fileName));
			log.debug("删除成功!文件名："+fileName);
		} catch (Exception e) {
			log.error("删除异常:"+e.getMessage(), e);
		}
	}
	/**
	 * 查询数据
	 * @param filename 	文件名称
	 * @param dbName    数据库名称
	 * @param bucket	数据桶名称
	 * @return
	 */
	public List<GridFSDBFile> query(String filename, String dbName, String bucket){
		 List<GridFSDBFile> gfsFile ;  
		 BasicDBObject con = new BasicDBObject();
		 con.put("filename", filename);
	     try {        
	       gfsFile = new GridFS(MongoDataSource.getDb(dbName), bucket).find(con);
	     } catch (Exception e) {  
	       log.error(e.getMessage(), e);  
	       return null;  
	     } 
	     log.debug("size:"+gfsFile.size());
	     for(GridFSDBFile gf : gfsFile){
	    	 log.debug("\r\nFilename:"+ gf.getFilename() + "\r\nContentType:"+ gf.getContentType() +"\r\nId:"+ gf.getId() + "\r\nChunkSize:" + gf.getChunkSize());
	     } 
	     return gfsFile;  
	}  
}


