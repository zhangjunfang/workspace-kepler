/**
 * 
 */
package org.storevm.toolkits.session.config;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.util.Properties;

/**
 * 配置信息实现类，用于读取和设置系统配置属性
 * 
 * @author Administrator
 * @version $Id: Configuration.java, v 0.1 2012-4-3 下午9:28:53 Administrator Exp $
 */
public class Configuration {
    /** 配置属性键值 */
    public static final String   SERVERS            = "servers";
    public static final String   MAX_IDLE           = "maxIdle";
    public static final String   INIT_IDLE_CAPACITY = "initIdleCapacity";
    public static final String   SESSION_TIMEOUT    = "sessionTimeout";
    public static final String   TIMEOUT            = "timeout";
    public static final String   POOLSIZE           = "poolSize";

    /** 配置文件名称 */
    public static final String   CFG_NAME           = ".cfg.properties";

    /** 单例对象 */
    private static Configuration instance;

    /** 配置属性文件 */
    private Properties           config;

    /**
     * 构造方法
     */
    protected Configuration() {
        this.config = new Properties();

        //从用户目录读取配置文件
        String basedir = System.getProperty("user.home");
        File file = new File(basedir, CFG_NAME);
        try {
            //如果文件不存在，则创建新文件
            boolean exist = file.exists();
            if (!exist) {
                file.createNewFile();
            }

            //读取配置属性
            this.config.load(new FileInputStream(file));

            //如果配置不存在，则写入默认值
            if (!exist) {
                this.config.setProperty(SERVERS, "www.storevm.org");
                this.config.setProperty(MAX_IDLE, "8");
                this.config.setProperty(INIT_IDLE_CAPACITY, "4");
                this.config.setProperty(SESSION_TIMEOUT, "5");
                this.config.setProperty(TIMEOUT, "5000");
                this.config.setProperty(POOLSIZE, "5000");
                this.config.store(new FileOutputStream(file), "");
            }
        } catch (Exception ex) {
            //do nothing...
        }
    }

    /**
     * 返回实例的方法
     * 
     * @return
     */
    public static Configuration getInstance() {
        if (instance == null) {
            instance = new Configuration();
        }
        return instance;
    }

    /**
     * 返回指定属性键值对应的配置项值的字符串格式(带有默认值)
     * 
     * @param key
     * @param defaultValue
     * @return
     */
    public String getString(String key, String defaultValue) {
        if (config != null) {
            return config.getProperty(key) != null ? config.getProperty(key) : defaultValue;
        }
        return defaultValue;
    }

    /**
     * 返回指定属性键值对应的配置项值的字符串格式
     * 
     * @param key
     * @return
     */
    public String getString(String key) {
        return getString(key, null);
    }

    /** 
     * @see java.lang.Object#toString()
     */
    @Override
    public String toString() {
        return "Configuration [config=" + config + "]";
    }
}
