/**
 * 2014-5-12MongoManager.java
 */
package com.ctfo.storage.media.dao;

import java.util.List;
import java.util.Set;

import com.mongodb.DB;
import com.mongodb.DBCollection;
import com.mongodb.Mongo;
import com.mongodb.MongoOptions;

/**
 * MongoDataSource
 * 
 */
public class MongoDataSource {
	private static MongoDataSource mongoDataSource = new MongoDataSource();
	/**	MongoDB连接池	*/
	private static Mongo mongo = null;
	/**	主机名		*/
	private String host;
	/**	端口		*/
	private int port;
	/**	每个主机的连接数量		*/
	private int connections = 50;
	/**	线程队列数，它以上面connectionsPerHost值相乘的结果就是线程队列最大值。如果连接线程排满了队列就会抛出“Out of semaphores to get db”错误。		*/
	private int threads = 50;
	/**	最大等待连接的线程阻塞时间		*/
	private int maxWaitTime = 3000;
	/**	连接超时的毫秒。0是默认和无限	*/
	private int connectTimeout = 3000;
	/**	socket超时。0是默认和无限	*/
	private int socketTimeout = 10000;
	/**	这个控制是否在一个连接时，系统会自动重试	*/
	private boolean autoConnectRetry = true;
	
	private MongoDataSource(){
	}
	public static MongoDataSource getInstance(){
		if(mongoDataSource == null){
			mongoDataSource = new MongoDataSource();
		}
		return mongoDataSource;
	}
	/**
	 * 初始化mongodb连接池
	 * @throws Exception 
	 */
	public void init() throws Exception {    
        try {
            mongo = new Mongo(host, port);
            MongoOptions opt = mongo.getMongoOptions();
            opt.setConnectionsPerHost(connections);
            opt.setThreadsAllowedToBlockForConnectionMultiplier(threads);
            opt.setMaxWaitTime(maxWaitTime);
            opt.setConnectTimeout(connectTimeout);
            opt.setSocketTimeout(socketTimeout);
            opt.setAutoConnectRetry(autoConnectRetry);
        }catch (Exception e) {
        	throw new Exception(e);
        }
    }
    
	/**
	 * 获取mongo库
	 * @param dbName
	 * @return
	 */
	public static DB getDb(String dbName){
        return mongo.getDB(dbName);
	}
    /**
	 * 得到一个Mongo实体下所有的Database 名称列表
	 * @param dbName
	 * @return
	 */
	public List<String> getDBs(){
		return mongo.getDatabaseNames();
	}
	
	/**
	 * 获取集合
	 * @param dbName
	 * @param collectionName
	 * @return
	 */
	public static DBCollection getCollection(String dbName,String collectionName){
		return getDb(dbName).getCollection(collectionName);
	}
	
	/**
	 * 获得某个Db下的所有集合（表）
	 * @param dbName
	 * @return collections 集合（表）
	 */
	public Set<String> getCollectionsByDB(String dbName){
		return getDb(dbName).getCollectionNames();
	}
	
	
	/**
	 * @return 获取 主机名
	 */
	public String getHost() {
		return host;
	}
	/**
	 * 设置主机名
	 * @param host 主机名 
	 */
	public void setHost(String host) {
		this.host = host;
	}
	/**
	 * @return 获取 端口
	 */
	public int getPort() {
		return port;
	}
	/**
	 * 设置端口
	 * @param port 端口 
	 */
	public void setPort(int port) {
		this.port = port;
	}
	/**
	 * @return 获取 每个主机的连接数量
	 */
	public int getConnections() {
		return connections;
	}
	/**
	 * 设置每个主机的连接数量
	 * @param connections 每个主机的连接数量 
	 */
	public void setConnections(int connections) {
		this.connections = connections;
	}
	/**
	 * @return 获取 线程队列数，它以上面connectionsPerHost值相乘的结果就是线程队列最大值。如果连接线程排满了队列就会抛出“Outofsemaphorestogetdb”错误。
	 */
	public int getThreads() {
		return threads;
	}
	/**
	 * 设置线程队列数，它以上面connectionsPerHost值相乘的结果就是线程队列最大值。如果连接线程排满了队列就会抛出“Outofsemaphorestogetdb”错误。
	 * @param threads 线程队列数，它以上面connectionsPerHost值相乘的结果就是线程队列最大值。如果连接线程排满了队列就会抛出“Outofsemaphorestogetdb”错误。 
	 */
	public void setThreads(int threads) {
		this.threads = threads;
	}
	/**
	 * @return 获取 最大等待连接的线程阻塞时间
	 */
	public int getMaxWaitTime() {
		return maxWaitTime;
	}
	/**
	 * 设置最大等待连接的线程阻塞时间
	 * @param maxWaitTime 最大等待连接的线程阻塞时间 
	 */
	public void setMaxWaitTime(int maxWaitTime) {
		this.maxWaitTime = maxWaitTime;
	}
	/**
	 * @return 获取 连接超时的毫秒。0是默认和无限
	 */
	public int getConnectTimeout() {
		return connectTimeout;
	}
	/**
	 * 设置连接超时的毫秒。0是默认和无限
	 * @param connectTimeout 连接超时的毫秒。0是默认和无限 
	 */
	public void setConnectTimeout(int connectTimeout) {
		this.connectTimeout = connectTimeout;
	}
	/**
	 * @return 获取 socket超时。0是默认和无限
	 */
	public int getSocketTimeout() {
		return socketTimeout;
	}
	/**
	 * 设置socket超时。0是默认和无限
	 * @param socketTimeout socket超时。0是默认和无限 
	 */
	public void setSocketTimeout(int socketTimeout) {
		this.socketTimeout = socketTimeout;
	}
	/**
	 * @return 获取 这个控制是否在一个连接时，系统会自动重试
	 */
	public boolean isAutoConnectRetry() {
		return autoConnectRetry;
	}
	/**
	 * 设置这个控制是否在一个连接时，系统会自动重试
	 * @param autoConnectRetry 这个控制是否在一个连接时，系统会自动重试 
	 */
	public void setAutoConnectRetry(boolean autoConnectRetry) {
		this.autoConnectRetry = autoConnectRetry;
	}
	
}
