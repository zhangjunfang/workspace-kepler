package com.ctfo.uitil;

import java.io.IOException;

/**
 * zip工具类
 * 
 * @author 蒋东卿
 * @date 2014年11月6日上午10:53:41
 * @since JDK1.6
 */
public class ZipUtil {

	/**
	 * @param  srcFile  源文件路径       压缩前
	 * @param  destfile 目标文件路径    压缩后
	 * @param  pwd      密码
	 * @throws InterruptedException, IOException 
	 * @author 蒋东卿
	 */
	public static void zipEncrypt(String srcFile, String destfile, String pwd) throws InterruptedException, IOException
	{
		Runtime runtime = Runtime.getRuntime();
		String cmd = "zip -q -r -P "+pwd+" "+destfile+" "+srcFile;
		runtime.exec(cmd).waitFor();
	}
	
	public static void main(String[] args) throws InterruptedException, IOException {
		zipEncrypt("/opt/BusQuery.json","/opt/zipfile.zip","123");
	}

}
