package com.ctfo.datatransferserver.util;

import java.io.ByteArrayOutputStream;
import java.util.zip.GZIPOutputStream;

public class ZipUtil {

/**
 * GZIP压缩
 * @param data
 * @return
 */
	public static byte[] GZIP(byte[] data) {
		ByteArrayOutputStream bos = null;
		GZIPOutputStream gos = null;
		try {
			bos = new ByteArrayOutputStream();
			gos = new GZIPOutputStream(bos);
			gos.write(data);
			gos.finish();

			byte[] bytes = bos.toByteArray();
			return bytes;
		} catch (Exception e) {
			e.printStackTrace();
			return null;
		} finally {
			try {
				bos.close();
				gos.close();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}

	}
}
