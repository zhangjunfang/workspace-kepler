package com.ctfo.context;



import javax.servlet.ServletContext;
import org.springframework.context.ApplicationContext;
import org.springframework.web.context.support.WebApplicationContextUtils;

import com.ctfo.local.exception.CtfoAppException;

public class FrameworkContext {

	static ServletContext context;
	
	static ApplicationContext appContext;
	//系统路径
	static String appPath;
	
	static String systemTitle;
	


	/**
	 * 根据BEAN的ID，从SPRING中得到注册的BEAN对象。
	 * @param beanId 在SPRING配置文件中注册的BEANID。
	 * @return
	 */
	public static Object getBean(String beanId)
		throws CtfoAppException{
		if(appContext==null){
			appContext = WebApplicationContextUtils.getRequiredWebApplicationContext(context);
		}
		return appContext.getBean(beanId);
	}
	
	
	public static String getAppPath() {
		return appPath;
	}

	public static void setAppPath(String path) {
		appPath = path;
	}
	public static void setAppContext(ApplicationContext appContext){
		FrameworkContext.appContext = appContext;
	}
	
}
