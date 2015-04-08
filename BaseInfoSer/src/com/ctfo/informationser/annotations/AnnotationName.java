package com.ctfo.informationser.annotations;

import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;

/**
 * 
 * 自定义注释. 实现对类和方法的注释。在AOP日志记录中使用。
 * <p>
 * 
 * @version 1.0
 * 
 * @author wangpeng
 * @since JDK1.6
 */
@Retention(RetentionPolicy.RUNTIME)
public @interface AnnotationName {
	/**
	 * 定义注释的属性
	 * 
	 * @return 属性
	 */
	String name();
}
