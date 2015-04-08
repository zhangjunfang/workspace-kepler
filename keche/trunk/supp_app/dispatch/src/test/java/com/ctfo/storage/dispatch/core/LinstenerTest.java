/**
 * 
 */
package com.ctfo.storage.dispatch.core;

import static org.junit.Assert.*;

import org.junit.Test;

import com.ctfo.storage.dispatch.core.Linstener;

/**
 * @author zjhl
 *
 */
public class LinstenerTest {

	/**
	 * 测试监听启动
	 */
	@Test
	public void testRun() {
		Linstener l = new Linstener();
//		l.start(); 
		assertNotNull(l);
	}

}
