package com.ctfo.storage.media.dao;


import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.hbase.client.HTableInterface;
import org.apache.hadoop.hbase.client.HTablePool;
import org.apache.hadoop.hbase.util.PoolMap.PoolType;

/**
 * HBaseResource
 * 
 * @author huangjincheng
 * 2014-5-13下午03:04:27
 * 
 */
@SuppressWarnings("deprecation")
public class HBaseDataSource {
	
	private static HBaseDataSource hbaseDataSource = new HBaseDataSource();
	/**	主键		*/
    public static String HBASE_ROW = "row"; 
    /**	列镞	*/
    public static String HBASE_FAMILY = "family"; 
    /**	列名	*/
    public static String HBASE_QUALIFIER = "qualifier";
    /**	列值	*/
    public static String HBASE_QUALIFIERVALUE = "qualifiervalue";
    /**	时间戳	*/
    public static String HBASE_TIMESTAMP = "timestamp"; 
    /** 访问HBase线程池大小 */
    public static int poolSize = 10;
    
    public static Configuration conf;
    
    public static String quorum;
    
    public static String port;
    
	private static HTablePool tablePool = null;
	
	private static int maxSize = 5;
	
	private HBaseDataSource(){
	}
	public static HBaseDataSource getInstance(){
		if(hbaseDataSource == null){
			hbaseDataSource = new HBaseDataSource();
		}
		
		return hbaseDataSource;
	}
	public void init() throws Exception {    
        try {
        	conf = new Configuration();
        	conf.set("hbase.zookeeper.quorum",quorum);
            conf.set("hbase.zookeeper.property.clientPort",port);
            tablePool = new HTablePool(conf, maxSize, PoolType.ThreadLocal);
        }catch (Exception e) {
        	throw new Exception(e);
        }
    }
  

    /**
     * HTablePool可以解决HTable存在的线程不安全问题
     * 同时通过维护固定数量的HTable对象
     * 能够在程序运行期间复用这些HTable资源对象。
     * 
     * @return
     */
	public synchronized static HTableInterface getTable(String tableName) {
		if (tablePool == null) {
			tablePool = new HTablePool(conf, maxSize, PoolType.ThreadLocal);
			return tablePool.getTable(tableName);
		}
		return tablePool.getTable(tableName);
	}
	
	
	
	
	/**
	 * 获取hbaseDataSource的值
	 * @return hbaseDataSource  
	 */
	public static HBaseDataSource getHbaseDataSource() {
		return hbaseDataSource;
	}
	/**
	 * 设置hbaseDataSource的值
	 * @param hbaseDataSource
	 */
	public static void setHbaseDataSource(HBaseDataSource hbaseDataSource) {
		HBaseDataSource.hbaseDataSource = hbaseDataSource;
	}
	/**
	 * 获取hBASE_ROW的值
	 * @return hBASE_ROW  
	 */
	public static String getHBASE_ROW() {
		return HBASE_ROW;
	}
	/**
	 * 设置hBASE_ROW的值
	 * @param hBASE_ROW
	 */
	public static void setHBASE_ROW(String hBASE_ROW) {
		HBASE_ROW = hBASE_ROW;
	}
	/**
	 * 获取hBASE_FAMILY的值
	 * @return hBASE_FAMILY  
	 */
	public static String getHBASE_FAMILY() {
		return HBASE_FAMILY;
	}
	/**
	 * 设置hBASE_FAMILY的值
	 * @param hBASE_FAMILY
	 */
	public static void setHBASE_FAMILY(String hBASE_FAMILY) {
		HBASE_FAMILY = hBASE_FAMILY;
	}
	/**
	 * 获取hBASE_QUALIFIER的值
	 * @return hBASE_QUALIFIER  
	 */
	public static String getHBASE_QUALIFIER() {
		return HBASE_QUALIFIER;
	}
	/**
	 * 设置hBASE_QUALIFIER的值
	 * @param hBASE_QUALIFIER
	 */
	public static void setHBASE_QUALIFIER(String hBASE_QUALIFIER) {
		HBASE_QUALIFIER = hBASE_QUALIFIER;
	}
	/**
	 * 获取hBASE_QUALIFIERVALUE的值
	 * @return hBASE_QUALIFIERVALUE  
	 */
	public static String getHBASE_QUALIFIERVALUE() {
		return HBASE_QUALIFIERVALUE;
	}
	/**
	 * 设置hBASE_QUALIFIERVALUE的值
	 * @param hBASE_QUALIFIERVALUE
	 */
	public static void setHBASE_QUALIFIERVALUE(String hBASE_QUALIFIERVALUE) {
		HBASE_QUALIFIERVALUE = hBASE_QUALIFIERVALUE;
	}
	/**
	 * 获取hBASE_TIMESTAMP的值
	 * @return hBASE_TIMESTAMP  
	 */
	public static String getHBASE_TIMESTAMP() {
		return HBASE_TIMESTAMP;
	}
	/**
	 * 设置hBASE_TIMESTAMP的值
	 * @param hBASE_TIMESTAMP
	 */
	public static void setHBASE_TIMESTAMP(String hBASE_TIMESTAMP) {
		HBASE_TIMESTAMP = hBASE_TIMESTAMP;
	}
	/**
	 * 获取访问HBase线程池大小的值
	 * @return poolSize  
	 */
	public static int getPoolSize() {
		return poolSize;
	}
	/**
	 * 设置访问HBase线程池大小的值
	 * @param poolSize
	 */
	public static void setPoolSize(int poolSize) {
		HBaseDataSource.poolSize = poolSize;
	}
	/**
	 * 获取conf的值
	 * @return conf  
	 */
	public static Configuration getConf() {
		return conf;
	}
	/**
	 * 设置conf的值
	 * @param conf
	 */
	public static void setConf(Configuration conf) {
		HBaseDataSource.conf = conf;
	}
	/**
	 * 获取ip的值
	 * @return ip  
	 */
	public static String getQuorum() {
		return quorum;
	}
	/**
	 * 设置ip的值
	 * @param ip
	 */
	public void setQuorum(String quorum) {
		HBaseDataSource.quorum = quorum;
	}
	/**
	 * 获取port的值
	 * @return port  
	 */
	public static String getPort() {
		return port;
	}
	/**
	 * 设置port的值
	 * @param port
	 */
	public void setPort(String port) {
		HBaseDataSource.port = port;
	}
	
}
