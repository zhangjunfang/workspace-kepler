package com.ctfo.mgdb.conn;



import org.apache.log4j.Logger;


import com.ctfo.mgdb.beans.Mongodb;
import com.ctfo.mgdb.beans.Record;
import com.mongodb.BasicDBObject;
import com.mongodb.DB;
import com.mongodb.DBCollection;
import com.mongodb.Mongo;

/**
 * 
 * mongodb存储
 * @author huangjincheng
 *
 */
public class MongodbConn implements DbConn{
	
	private static final Logger log = Logger.getLogger(MongodbConn.class);
	public Mongodb mongodb ;
	public Mongo m;
	public DB db;
	public DBCollection collection;
	public MongodbConn(){
		try {
			this.mongodb = new Mongodb();
			this.m = new Mongo(mongodb.ip,mongodb.port);
			this.db = this.m.getDB(mongodb.dbname);
			this.collection = this.db.getCollection(mongodb.collection);
		} catch (Exception e) {
			log.debug("mongoDB数据库连接失败！请检查配置文件！"+e.getMessage());
		}	
		
	}
	
	/* (non-Javadoc)
	 * @see com.ctfo.mgdb.conn.DbConn#save(com.ctfo.mgdb.beans.Record)
	 */
	public void save(Record record){
		BasicDBObject doc = new BasicDBObject();
		doc.put("S", record.getIp());
		doc.put("A", record.getAppType());
		doc.put("T", record.getUtcTime()/1000);
		doc.put("P", record.getPhoneNum());
		doc.put("B", record.getDataType());
		doc.put("E", record.getEnable_flag());
		doc.put("M", record.getContent());
		try {
			this.collection.insert(doc);
		} catch (Exception e) {
			log.debug("mongoDB数据库连接失败！请检查配置文件！"+e.getMessage());
		}
	}


	
	
}
