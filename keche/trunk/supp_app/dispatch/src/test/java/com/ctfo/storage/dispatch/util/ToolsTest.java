/**
 * 
 */
package com.ctfo.storage.dispatch.util;

import org.junit.Test;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.dispatch.util.Tools;

/**
 * @author zjhl
 *
 */
public class ToolsTest {
	private static Logger log = LoggerFactory.getLogger(ToolsTest.class);
	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#checkIP(java.lang.String)}.
	 */
	@Test
	public void testCheckIP() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getSeqId()}.
	 */
	@Test
	public void testGetSeqId() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#charToInt(byte)}.
	 */
	@Test
	public void testCharToInt() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getCheckCode(java.lang.String)}.
	 */
	@Test
	public void testGetCheckCode() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getTransferContent(java.lang.String)}.
	 */
	@Test
	public void testGetTransferContent() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getNowTime()}.
	 */
	@Test
	public void testGetNowTime() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getNowTimeByFomat(java.lang.String)}.
	 */
	@Test
	public void testGetNowTimeByFomat() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getTimeByFormat(java.util.Date, java.lang.String)}.
	 */
	@Test
	public void testGetTimeByFormat() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getBinaryStrByHexStr(java.lang.String)}.
	 */
	@Test
	public void testGetBinaryStrByHexStr() {
	}

	/**
	 * 
	 */
	@Test
	public void testFillNBitBefore() {
		
//		beijing -> 6265696a696e67000000000000000000
		String str = "beijing";
		String userHex = Tools.getHzHexStr(str);
		String hex = Tools.fillNBitAfter(userHex, 32, "0");
			
		System.out.println(str+ " getHzHex=" + userHex + " fillNBitAfter=" +hex);
		
		
		String pass = "0420A206AE8B77B60F314A33B38C875A";
		String passHex = Tools.getHzHexStr(pass);
		String passhex = Tools.fillNBitAfter(userHex, 32, "0");
			
		log.info(pass + " getHzHex=" + passHex + " fillNBitAfter=" +passhex);
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#fillNBitAfter(java.lang.String, int, java.lang.String)}.
	 */
	@Test
	public void testFillNBitAfter() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getHzHexStr(java.lang.String)}.
	 */
	@Test
	public void testGetHzHexStrString() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getHzHexStr(java.lang.String, java.lang.String)}.
	 */
	@Test
	public void testGetHzHexStrStringString() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#bytesToHexStr(byte[])}.
	 */
	@Test
	public void testBytesToHexStr() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getChinese(java.lang.String)}.
	 */
	@Test
	public void testGetChineseString() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getChinese(java.lang.String, java.lang.String)}.
	 */
	@Test
	public void testGetChineseStringString() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#hexStrToBytes(java.lang.String)}.
	 */
	@Test
	public void testHexStrToBytes() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getHexByBinary(java.lang.String)}.
	 */
	@Test
	public void testGetHexByBinary() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#asciiToHex(java.lang.String)}.
	 */
	@Test
	public void testAsciiToHex() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getASCIIByHex(java.lang.String)}.
	 */
	@Test
	public void testGetASCIIByHex() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getCheckSum(java.lang.String)}.
	 */
	@Test
	public void testGetCheckSum() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getUTC()}.
	 */
	@Test
	public void testGetUTC() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getBigEndtion(java.lang.String)}.
	 */
	@Test
	public void testGetBigEndtion() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#hex2chars(java.lang.String)}.
	 */
	@Test
	public void testHex2chars() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#getValiCode2(java.lang.String)}.
	 */
	@Test
	public void testGetValiCode2() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#bytesToUint(byte[])}.
	 */
	@Test
	public void testBytesToUint() {
	}

	/**
	 * Test method for {@link com.ctfo.storage.dispatch.util.Tools#binary2SpecifiedLengthInt(java.lang.String)}.
	 */
	@Test
	public void testBinary2SpecifiedLengthInt() {
	}

}
