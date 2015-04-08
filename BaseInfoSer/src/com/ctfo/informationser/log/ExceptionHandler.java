package com.ctfo.informationser.log;

import java.lang.annotation.Annotation;
import java.lang.reflect.Method;
import java.util.logging.Level;
import java.util.logging.Logger;

import org.springframework.aop.ThrowsAdvice;

/**
 * 
 * 异常日志记录用户操作异常日志信息.
 * 
 * @version 1.0
 * 
 * @author wangpeng
 * @since JDK1.6
 */
public class ExceptionHandler implements ThrowsAdvice {
	private Logger logger = Logger.getLogger(this.getClass().getName());
	private String classA = "缺少注释";
	private String methodA = "缺少注释";

	/**
	 * 处理捕获异常，并记录日志
	 * 
	 * @param method
	 *            执行的方法名
	 * @param args
	 *            方法的参数
	 * @param target
	 *            方法所在的类
	 * @param subclass
	 *            异常类
	 * @throws Throwable
	 *             抛出处理异常
	 */
	public void afterThrowing(Method method, Object[] args, Object target,
			Throwable subclass) throws Throwable {

		Annotation[] ac = target.getClass().getAnnotations();
		if (ac.length != 0) {
			classA = ac[0].toString();
			classA = classA.substring(classA.indexOf("(name=") + 6,
					classA.length() - 1);
		}

		ac = method.getAnnotations();
		if (ac.length != 0) {
			methodA = ac[0].toString();
			methodA = methodA.substring(methodA.indexOf("(name=") + 6,
					methodA.length() - 1);
		}

		// 获取类型和方法名
		String actionname = target.getClass().getName();
		String actionclass = actionname
				.substring(actionname.lastIndexOf('.') + 1);
		String methodname = method.getName();

		// 组装日志记录
		logger.log(Level.INFO, actionclass + " 执行 " + methodname
				+ " 时有异常抛出...." + subclass.getMessage());
	}
}