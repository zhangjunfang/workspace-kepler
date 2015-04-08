/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： SyncService		</li><br>
 * <li>文件名称：com.ctfo.syncservice.dao OracleDataSource.java	</li><br>
 * <li>时        间：2013-12-16  上午11:07:41	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.dao;

import java.sql.Connection;
import java.sql.SQLException;
import java.util.List;

import com.alibaba.druid.pool.DruidDataSource;

/*****************************************
 * <li>描        述：oracle数据接口		
 * 
 *****************************************/
public class OracleDataSource {
	/**	连接池	*/
	private static DruidDataSource druidDataSource = null;
	
	private static OracleDataSource oracleDataSource = new OracleDataSource();
	/**	连接类	*/
	private String driver;
	/**	连接地址	*/
	private String url;
	/**	用户名	*/
	private String username;
	/**	密码	*/
	private String password;
	/**	最大连接数	*/
	private int maxActive;
	/**	最小空闲数	*/
	private int minIdle;
	/**	连接初始大小	*/
	private int initialSize ;
	/**	连接等待超时	*/
	private long maxWait;
	/**	删除未使用的连接	*/
	private boolean removeAbandoned;
	
	public static OracleDataSource getInstance(){
		if(oracleDataSource == null){
			oracleDataSource = new OracleDataSource();
		}
		return oracleDataSource;
	}
	
	public void init() throws SQLException{ 
		druidDataSource = new DruidDataSource();
//		druidDataSource.setDriverClassName(driver);
		druidDataSource.setOracle(true);
		druidDataSource.setUrl(url);
		druidDataSource.setUsername(username);
		druidDataSource.setPassword(password);
//		<!-- 配置初始化大小、最小、最大 -->
		druidDataSource.setInitialSize(initialSize);
		druidDataSource.setMaxActive(maxActive);
		druidDataSource.setMinIdle(minIdle);
//		 配置获取连接等待超时的时间。 单位是毫秒
		druidDataSource.setMaxWait(maxWait);
//		配置间隔多久才进行一次检测，检测需要关闭的空闲连接，单位是毫秒 
		druidDataSource.setTimeBetweenEvictionRunsMillis(60000);
//		 配置一个连接在池中最小生存的时间，单位是毫秒 
		druidDataSource.setMinEvictableIdleTimeMillis(300000);
		druidDataSource.setValidationQuery("SELECT 'x' FROM DUAL");
		druidDataSource.setTestWhileIdle(true);
		druidDataSource.setTestOnBorrow(true);
		druidDataSource.setTestOnReturn(false);
//		打开PSCache，并且指定每个连接上PSCache的大小
		druidDataSource.setPoolPreparedStatements(true);
		druidDataSource.setMaxPoolPreparedStatementPerConnectionSize(20);
		
		druidDataSource.setRemoveAbandoned(removeAbandoned);
		
		druidDataSource.init();
	}
	/*****************************************
	 * <li>描        述：关闭 		</li><br>
	 * <li>时        间：2013-12-16  上午11:10:22	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	public void destroy() {
		druidDataSource.close();
	}

	public Connection getConnection() throws SQLException{
		return druidDataSource.getConnection();
	}
	
	public String getDriver() {
		return driver;
	}
	public void setDriver(String driver) {
		this.driver = driver;
	}
	public String getUrl() {
		return url;
	}
	public void setUrl(String url) {
		this.url = url;
	}
	public String getUsername() {
		return username;
	}
	public void setUsername(String username) {
		this.username = username;
	}
	public String getPassword() {
		return password;
	}
	public void setPassword(String password) {
		this.password = password;
	}
	public int getMaxActive() {
		return maxActive;
	}
	public void setMaxActive(int maxActive) {
		this.maxActive = maxActive;
	}
	public int getMinIdle() {
		return minIdle;
	}
	public void setMinIdle(int minIdle) {
		this.minIdle = minIdle;
	}
	public int getInitialSize() {
		return initialSize;
	}
	public void setInitialSize(int initialSize) {
		this.initialSize = initialSize;
	}
	/**
	 * @return the 连接初始大小
	 */
	public long getMaxWait() {
		return maxWait;
	}
	/**
	 * 设置连接初始大小的值
	 * @param maxWait 连接初始大小  
	 */
	public void setMaxWait(long maxWait) {
		this.maxWait = maxWait;
	}

	/**
	 * @return the 连接初始大小
	 */
	public boolean isRemoveAbandoned() {
		return removeAbandoned;
	}
	/**
	 * 设置连接初始大小的值
	 * @param removeAbandoned 连接初始大小  
	 */
	public void setRemoveAbandoned(boolean removeAbandoned) {
		this.removeAbandoned = removeAbandoned;
	}
	/**
	 * 打印连接池当前状态
	 * @return
	 */
	public static String getPoolStackTrace (){
		StringBuffer sb = new StringBuffer();
		sb.append("当前连接池状态:").append("\r\n");
		sb.append(druidDataSource.dump()).append("\r\n");
		sb.append("活动连接状态:").append("\r\n");
		List<String> stack = druidDataSource.getActiveConnectionStackTrace();
		for(String str : stack){
			sb.append(str).append("\r\n");
		}
		return sb.toString();
	}
	
}
