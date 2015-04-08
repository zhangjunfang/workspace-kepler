package com.ctfo.syn.kcpt.httservice;



import com.ctfo.syn.kcpt.utils.SynPool;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： SynModelSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
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
 * <td>Feb 14, 2012</td>
 * <td>wuqj</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author wuqj
 * @since JDK1.6
 */
public class Config {
	/**
	 * URL地址
	 */
	public String url;
	/**
	 * 用户名
	 */
	public String userName;
	/**
	 * 密码
	 */
	public String password;
	/**
	 * Memcached 服务器地址
	 */
	public String memServer;
	/**
	 * 驱动名称
	 */
	public String className;
	/**
	 * 配置
	 */
	public static Config config;
	
	private Long sleepTime = (long) (5*60*1000);

	/**
	 * @return the sleepTime
	 */
	public Long getSleepTime() {
		return sleepTime;
	}


	/**
	 * @param sleepTime the sleepTime to set
	 */
	public void setSleepTime(Long sleepTime) {
		this.sleepTime = sleepTime;
	}


	/**
	 * 根据参数获取配置信息
	 * 
	 * @param filePath
	 */
	public Config() {
		getConfig();
	}


	/**
	 * 根据文件获取配置信息
	 * 
	 * @param filePath
	 *            文件目录
	 */
	private void getConfig() {
		this.memServer = SynPool.getinstance().getSql("httpMemAddr");
		this.sleepTime = SynPool.getinstance().getSql("sleepTime") == null ? 5 * 60 * 1000 : Long.parseLong(SynPool.getinstance().getSql("sleepTime"));
	}

	/**
	 * @return the url
	 */
	public String getUrl() {
		return url;
	}

	/**
	 * @param url
	 *            the url to set
	 */
	public void setUrl(String url) {
		this.url = url;
	}

	/**
	 * @return the userName
	 */
	public String getUserName() {
		return userName;
	}

	/**
	 * @param userName
	 *            the userName to set
	 */
	public void setUserName(String userName) {
		this.userName = userName;
	}

	/**
	 * @return the password
	 */
	public String getPassword() {
		return password;
	}

	/**
	 * @param password
	 *            the password to set
	 */
	public void setPassword(String password) {
		this.password = password;
	}

	/**
	 * @return the memServer
	 */
	public String getMemServer() {
		return memServer;
	}

	/**
	 * @param memServer
	 *            the memServer to set
	 */
	public void setMemServer(String memServer) {
		this.memServer = memServer;
	}

	/**
	 * @return the className
	 */
	public String getClassName() {
		return className;
	}

	/**
	 * @param className
	 *            the className to set
	 */
	public void setClassName(String className) {
		this.className = className;
	}

}
