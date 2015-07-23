package com.fujitsu.fnst.asm.utility;


/**
 * This class just extends ClassLoader to use defineClass method.
 * We will use it to load dynamic created classes.
 * @author paul
 *
 */
public class SimpleClassLoader extends ClassLoader {
	public Class<?> defineClass(String className, byte[] byteCodes) {
		return super.defineClass(className, byteCodes, 0, byteCodes.length);
	}
}
