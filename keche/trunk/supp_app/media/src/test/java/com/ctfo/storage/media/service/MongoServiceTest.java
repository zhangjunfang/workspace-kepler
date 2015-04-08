/**
 * 
 */
package com.ctfo.storage.media.service;

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
public class MongoServiceTest {
	private static final Logger log = LoggerFactory.getLogger(MongoServiceTest.class);
	
	MongoDataSource mds = null;
	MongoService ms = null;
	public MongoServiceTest() throws Exception{ 
//		mds = MongoDataSource.getInstance();
//		mds.setHost("192.168.100.52");
//		mds.setPort(27017);
//		mds.setConnections(10);
//		mds.setThreads(10); 
//		mds.init();
//		ms = new MongoService();
	}
	/**
	 * Test method for {@link com.ctfo.storage.media.service.MongoService#save(com.ctfo.storage.media.model.Media)}.
	 * @throws Exception 
	 */
	@Test
	public void testSave() throws Exception { 
		long s = System.currentTimeMillis();
		
//		MediaFile media = new MediaFile();
//		media.setName("test202.jpg");
//		byte[] image = createImage(1400, 902, "1400 X 902   " + media.getName() + System.currentTimeMillis());
//		media.setContent(image);
//		ms.save(media, "pics", "fs");
		
		log.info("图片存储完成，耗时[{}]ms",  System.currentTimeMillis() - s);
	}

	/**
	 * 
	 */
	@Test
	public void testDelete() {
//		ms.delete("test101.jpg", "pics", "fs");
	}

	/**
	 * 
	 */
	@Test
	public void testQuery() {
//		ms.query("test100.jpg", "pics", "fs");
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
