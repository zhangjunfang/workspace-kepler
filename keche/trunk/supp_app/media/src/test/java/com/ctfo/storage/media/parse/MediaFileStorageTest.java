/**
 * 
 */
package com.ctfo.storage.media.parse;

import java.awt.Color;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.IOException;

import javax.imageio.ImageIO;

import org.junit.Test;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.media.dao.MongoDataSource;

/**
 * @author zjhl
 *
 */
public class MediaFileStorageTest {
	private static final Logger log = LoggerFactory.getLogger(MediaFileStorageTest.class);
	
	MongoDataSource mds = null;
	public MediaFileStorageTest() throws Exception{ 
//		mds = MongoDataSource.getInstance();
//		mds.setHost("192.168.100.52");
//		mds.setPort(27017);
//		mds.setConnections(10);
//		mds.setThreads(10); 
//		mds.init();
	}
	/**
	 * 测试存储图片
	 * @throws IOException 
	 */
	@Test 
	public void testRun() throws IOException {
		long s = System.currentTimeMillis();
		int index = 0;
		
//		MediaFileStorage mfs = new MediaFileStorage();
//		mfs.start();
//		for(int i = 0; i < 10; i++){
//			index++;
//			MediaFile mediaFile = new MediaFile();
//			mediaFile.setName("testFileName" + System.currentTimeMillis() + ".jpeg");
//			byte[] image = createImage(1400, 902, "1400 X 902   " + mediaFile.getName());
//			mediaFile.setContent(image);
//			mfs.put(mediaFile);
//		}
		
		log.info("创建图片[{}]张完成，耗时[{}]ms", index,  System.currentTimeMillis() - s);
	}
	
	public byte[] createImage(int width, int height, String text) throws IOException { 
        BufferedImage image = new BufferedImage(width, height, BufferedImage.TYPE_BYTE_INDEXED);
        Graphics graphics = image.createGraphics();
        graphics.setColor(Color.YELLOW);
        graphics.fillRect(0, 0, image.getWidth(), image.getHeight());
        Font serif = new Font("serif", Font.PLAIN, 30);
        graphics.setFont(serif);
        graphics.setColor(Color.BLUE);
        graphics.drawString(text, 10, 50);
        ByteArrayOutputStream out = new ByteArrayOutputStream();
        ImageIO.write(image, "jpeg", out);
        return out.toByteArray();
    }
}
