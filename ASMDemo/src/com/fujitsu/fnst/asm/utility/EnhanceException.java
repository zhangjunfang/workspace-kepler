package com.fujitsu.fnst.asm.utility;

/**
 * Exception for enhancement
 * @author paul
 *
 */
public class EnhanceException extends Exception {
	
	private static final long serialVersionUID = -7342496729458561516L;
	
	private Class<?> enhanceClass;
	private Class<?> [] implementClasses;
	
	public EnhanceException(Exception ex,Class<?> ec,Class<?>... imClazz){
		super(ex);
		this.enhanceClass = ec;
		this.implementClasses = imClazz;
	}

	public Class<?> getEnhanceClass() {
		return enhanceClass;
	}

	public Class<?>[] getImplementClasses() {
		return implementClasses;
	}
	
	
}
