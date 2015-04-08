/**
 * 
 */
package com.ctfo.storage.dispatch.util;

import org.junit.Test;

import com.ctfo.storage.dispatch.util.MD5;

/**
 * @author zjhl
 *
 */
public class MD5Test {

	/**
	 * 测试MD5加密
	 */
	@Test
	public void testGetMD5ofStr() {
		MD5 md5 = new MD5();
		System.out.println(md5.getMD5ofStr("beijing"));
	}

}
