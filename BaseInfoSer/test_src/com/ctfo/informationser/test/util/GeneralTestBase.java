package com.ctfo.informationser.test.util;

import java.lang.reflect.Field;
import java.util.HashMap;
import java.util.Map;

import junit.framework.TestCase;

import org.springframework.context.support.ClassPathXmlApplicationContext;
import org.springframework.transaction.PlatformTransactionManager;
import org.springframework.transaction.TransactionStatus;
import org.springframework.transaction.jta.JtaTransactionManager;
import org.springframework.transaction.support.DefaultTransactionDefinition;

import com.ctfo.local.obj.DynamicSqlParameter;


/**
 * 
 * @author wang
 * 
 */
public abstract class GeneralTestBase extends TestCase {
	public static ClassPathXmlApplicationContext classPathXmlContext = null;
	protected PlatformTransactionManager transactionManager;
	protected TransactionStatus transactionStatus;

	/**
	 * 方法是在测试方法调用前调用，加载相应配置文件
	 */
	@Override
	protected void setUp() throws Exception {
		init();
		System.out.println("Base up..............");
	}

	/**
	 * 方法是在测试方法返回时调用，能够对测试数据进行回滚。
	 */
	@Override
	protected void tearDown() throws Exception {
		try {
//			transactionManager.rollback(this.transactionStatus);
			transactionManager.commit(this.transactionStatus);
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
		}
		System.out.println("Base down............");
	}

	/**
	 * Init spring config.
	 */
	protected void init() {
		classPathXmlContext = GeneralTestBase.getClassXmlContext();
		transactionManager = (JtaTransactionManager) classPathXmlContext.getBean("springTransactionManager");
		transactionStatus = transactionManager.getTransaction(new DefaultTransactionDefinition());
	}

	/**
	 * Get class path XML context
	 * 
	 * @return ClassPathXmlApplicationContext
	 */
	public final static ClassPathXmlApplicationContext getClassXmlContext() {
		if (classPathXmlContext == null) {
			try {
				String[] classXmlContexts = 
				{ "com/ctfo/informationser/resource/basic/spring-dataaccess.xml", 
				  "com/ctfo/informationser/resource/basic/spring-hessian.xml", 	
				  "com/ctfo/informationser/resource/monitoring/spring-monitoring.xml",
				  "com/ctfo/informationser/resource/memcache/spring-memcache.xml",
				  "com/ctfo/informationser/resource/basic/spring-redis.xml"};
				classPathXmlContext = new ClassPathXmlApplicationContext(classXmlContexts);
			} catch (Exception e) {
				System.out.println("-----------------------------------------------------------");
				e.printStackTrace();
			}
		}
		return classPathXmlContext;
	}

	/**
	 * 生产测试数据
	 * 
	 * @return DynamicSqlParameter
	 */
	@SuppressWarnings({ "rawtypes", "unchecked" })
	public static DynamicSqlParameter getParam(Object object) {
		Class ob = object.getClass();
		Field fields[] = ob.getDeclaredFields();
		DynamicSqlParameter parm = new DynamicSqlParameter();
		// 需要等于的参数
		Map equal = new HashMap();
		// 需要修改的值
		Map updateValue = new HashMap();
		// 模糊查询参数
		Map like = new HashMap();
		equal.put(fields[0].getName(), Long.valueOf("12"));
		like.put(fields[1].getName(), "test");
		for (Field field : fields) {

			if (field.getName().equalsIgnoreCase("comboId"))
				updateValue.put(field.getName(), "321");
			else
				updateValue.put(field.getName(), "1");
		}
		parm.setEqual(equal);
		parm.setLike(like);
		parm.setUpdateValue(updateValue);
		parm.setPage(1);
		parm.setRows(30);
		return parm;
	}

	public static void main(String[] args) {
	}

}
