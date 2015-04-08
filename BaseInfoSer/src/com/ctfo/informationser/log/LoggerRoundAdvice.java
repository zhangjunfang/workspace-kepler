package com.ctfo.informationser.log;

import java.lang.annotation.Annotation;

import org.aopalliance.intercept.MethodInterceptor;
import org.aopalliance.intercept.MethodInvocation;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.apache.log4j.MDC;

import com.ctfo.informationser.hessian.CtfoHessianServiceExporter;

/**
 * 日志记录器通过Spring-Aop方式记录正常日志信息。
 * 
 * @version 1.0
 * 
 * @author 王鹏
 * @since JDK1.6
 */
public class LoggerRoundAdvice implements MethodInterceptor {

	private static Log log = LogFactory.getLog(LoggerRoundAdvice.class);

	@Override
	public Object invoke(MethodInvocation mi) throws Throwable {
		String classA = "缺少注释";
		String methodA = "缺少注释";
		long procTime = System.currentTimeMillis();
		String actionname = mi.getThis().getClass().getName();
		String actionmethod = mi.getMethod().getName();
		Annotation[] ac = mi.getThis().getClass().getAnnotations();
		Object result = mi.proceed();
		String logType = "未知";
		String classDesc = "缺少注释";
		if (ac.length != 0) {
			classA = ac[0].toString();
			classA = classA.substring(classA.indexOf("(name=") + 6, classA.length() - 1);
			if(classA!=null && !"".equals(classA)){
				if(classA.indexOf(":")!=-1){
					logType = classA.split(":")[0];
					classDesc = classA.split(":")[1];
				}
			}
		}
		ac = mi.getMethod().getAnnotations();
		if (ac.length != 0) {
			methodA = ac[0].toString();
			methodA = methodA.substring(methodA.indexOf("(name=") + 6, methodA.length() - 1);
		}
		// log4j 记录日志数据
		MDC.put("opId", 0);
		MDC.put("opName", "admin");
		MDC.put("fromIp", "127.0.0.1");
		MDC.put("entId", 0);
		MDC.put("entName", "中交兴路客车平台");
		MDC.put("funCbs", 0);
		MDC.put("funId", 0);
		MDC.put("logUtc", System.currentTimeMillis()/1000);
		MDC.put("opType", classDesc);
		MDC.put("logDesc", "操作成功");
		MDC.put("logTypeid", logType);
		procTime = System.currentTimeMillis() - procTime;
		String userid = CtfoHessianServiceExporter.mythred.get();
		// 计算执行时间
		log.debug("[InformationSer]在["+userid +"]["+ classA + "-" + actionname + "]中执行[" + methodA + "-" + actionmethod + "]操作成功,执行耗时：" + procTime + "毫秒");
		log.info("[InformationSer]在["+userid +"]["+ classA + "-" + actionname + "]中执行[" + methodA + "-" + actionmethod + "]操作成功,执行耗时：" + procTime + "毫秒");

		return result;
	}

}
