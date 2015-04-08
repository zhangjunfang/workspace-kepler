package com.ctfo.storage.service;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.dao.MongoDataSource;
import com.ctfo.storage.model.file.MessageFile;
import com.mongodb.gridfs.GridFS;
import com.mongodb.gridfs.GridFSInputFile;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SspDispatchService <br>
 * 功能： MongoDB接口<br>
 * 描述： MongoDB接口<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-11-7</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class MongoService {

	private static final Logger logger = LoggerFactory.getLogger(MongoService.class);

	/** 数据库名称 */
	private String dbName;

	/** 文件库名称 */
	private String fileDir;

	public MongoService() {

	}

	/**
	 * 存储文件
	 * 
	 * @param messageFile
	 *            文件对象
	 * @param dbName
	 *            数据库名称
	 * @param mediaDir
	 *            文件库名称
	 */
	public void save(MessageFile messageFile) {
		try {
			GridFS gfsPhoto = new GridFS(MongoDataSource.getDb(dbName), fileDir);
			GridFSInputFile gfsFile = gfsPhoto.createFile(messageFile.getContent());
			gfsFile.setFilename(messageFile.getFileName());
			gfsFile.setContentType(messageFile.getContentType());
			gfsFile.save();
			logger.debug("存储附件文件[{}]完成!", messageFile.getFileName());
		} catch (Exception e) {
			logger.error("存储附件文件异常:" + e.getMessage(), e);
		}
	}

	/**
	 * 删除文件
	 * 
	 * @param fileName
	 *            文件名称
	 */
	public void delete(String fileName) {
		try {
			GridFS gfsPhoto = new GridFS(MongoDataSource.getDb(dbName), fileDir);
			gfsPhoto.remove(fileName);
			logger.debug("删除成功!文件名：" + fileName);
		} catch (Exception e) {
			logger.error("删除异常:" + e.getMessage(), e);
		}
	}

	public String getDbName() {
		return dbName;
	}

	public void setDbName(String dbName) {
		this.dbName = dbName;
	}

	public String getFileDir() {
		return fileDir;
	}

	public void setFileDir(String fileDir) {
		this.fileDir = fileDir;
	}

}
