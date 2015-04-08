package com.zjxl.transmap.core;

import java.io.BufferedReader;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;
import java.util.zip.GZIPInputStream;

import org.apache.log4j.Logger;

import com.zjxl.transmap.rt.dcar.Car;
import com.zjxl.transmap.rt.dcar.IndexManager;
import com.zjxl.transmap.rt.server.IReceiver;

/**
 * 运营应用,动态数据接收器
 * 
 * @author dux(duxionggis@126.com)
 */
public class CarReceiver implements IReceiver {
	private static Logger logger = Logger.getLogger(CarReceiver.class);
	private int port;
	private boolean gzip;

	public static void main(String[] args) throws IOException {
		new CarReceiver(4000, true).run();
		logger.info("程序启动.......");
	}

	public CarReceiver() {
	}

	public CarReceiver(int port, boolean gzip) {
		this.port = port;
		this.gzip = gzip;
	}

	public void setProperty(Properties ppt) {
		if (ppt != null) {
			String carPort = ppt.getProperty("carPort");
			String zip = ppt.getProperty("gzip");

			this.gzip = (zip == null ? false : zip.equalsIgnoreCase("true"));
			if ((carPort != null) && (!carPort.isEmpty()))
				try {
					this.port = Integer.valueOf(carPort.trim()).intValue();
				} catch (Exception e) {
					this.port = 4000;
				}
		}
	}

	public void run() {
		try {
			run(this.port);
		} catch (IOException e) {
			logger.error("gps run error: ", e);
		}
	}

	public void run(int port) throws IOException {
		ServerSocket server = new ServerSocket(port);
		logger.info("GPS聚合服务启动, 图例说明:0:离线-灰色 、 1:红色、 2:黄色 3:绿色 4:蓝色; 监听端口:" + port);
		while (true) {
			Socket client = server.accept();
			try {
				receive(client.getInputStream());
			} catch (Exception e) {
				logger.error("gps receive error: ", e);
			}
			client.close();
		}
	}

	private void receive(InputStream is) throws IOException {
		long t1 = System.currentTimeMillis();
		DataInputStream dis = new DataInputStream(is);
		if (this.gzip) {
			GZIPInputStream gis = new GZIPInputStream(is);
			int size = 0;
			byte[] tempBuffer = new byte[8192];
			ByteArrayOutputStream baos = new ByteArrayOutputStream();
			while ((size = gis.read(tempBuffer)) != -1) {
				baos.write(tempBuffer, 0, size);
			}
			dis = new DataInputStream(new ByteArrayInputStream(baos.toByteArray()));
			baos.close();
			gis.close();
		}

		StringBuilder result = new StringBuilder();
		try {
			BufferedReader d = new BufferedReader(new InputStreamReader(dis));
			while (d.ready())
				result.append(d.readLine());
		} catch (IOException ex) {
			ex.printStackTrace();
		}
//		System.out.println(result.toString());
		long t2 = System.currentTimeMillis();

		String[] vehicleArray = result.toString().split(";");
		byte isnative = -1;
		for (String vehicleStr : vehicleArray) {
			try {
				String[] vehicle = splitWorker(vehicleStr, ":", -1 , true);
				Car car = new Car(vehicle[0], vehicle[1], vehicle[2], vehicle[3], Integer.valueOf(vehicle[4]).intValue(), Integer.valueOf(vehicle[5]).intValue(), Integer.valueOf(vehicle[6]).intValue(), Byte.parseByte(vehicle[7]), Integer.valueOf(vehicle[8]).intValue(), Short.valueOf(vehicle[9]).shortValue(), Byte.parseByte(vehicle[10]), isnative);
				IndexManager.getInstance().putCar(car);
				logger.debug(":data======> vid:" + vehicle[0] + " vehicleno:" + vehicle[1] + " transtype:" + vehicle[2] + " nativecode:" + vehicle[3] + " lon:" + vehicle[4]
						+ " lat:" + vehicle[5] + " utc:" + vehicle[6] + " colorid:" + vehicle[7] + " alarmcode:-1 head:" + vehicle[8] + " state:" + vehicle[9] + " isnative:-1");
			} catch (Exception e) {
				logger.error("构建树信息异常:" + e.getMessage(), e);
			}
		}
		dis.close();
		long t3 = System.currentTimeMillis();
		IndexManager.getInstance().buildSpatialTree();
		long t4 = System.currentTimeMillis();
		logger.info("记录总数:" + vehicleArray.length + "条, 接收处理耗时: " + (t2 - t1) + "ms, 存储节点耗时:" + (t3 - t2) + "ms, 构建树耗时:" + (t4 - t3) + "ms, 处理数据总耗时:"
				+ (System.currentTimeMillis() - t1) + "ms");
		logger.info("free memory：" + Runtime.getRuntime().freeMemory() / 1024L / 1024L + "M");
	}
	/**
	 * 解析字符串
	 * @param str
	 * @param separatorChars
	 * @param max
	 * @param preserveAllTokens
	 * @return
	 */
    private static String[] splitWorker(String str, String separatorChars, int max, boolean preserveAllTokens) {
        // Performance tuned for 2.0 (JDK1.4)
        // Direct code is quicker than StringTokenizer.
        // Also, StringTokenizer uses isSpace() not isWhitespace()

        if (str == null) {
            return null;
        }
        int len = str.length();
        if (len == 0) {
            return null;
        }
        List<String> list = new ArrayList<String>();
        int sizePlus1 = 1;
        int i = 0, start = 0;
        boolean match = false;
        boolean lastMatch = false;
        if (separatorChars == null) {
            // Null separator means use whitespace
            while (i < len) {
                if (Character.isWhitespace(str.charAt(i))) {
                    if (match || preserveAllTokens) {
                        lastMatch = true;
                        if (sizePlus1++ == max) {
                            i = len;
                            lastMatch = false;
                        }
                        list.add(str.substring(start, i));
                        match = false;
                    }
                    start = ++i;
                    continue;
                }
                lastMatch = false;
                match = true;
                i++;
            }
        } else if (separatorChars.length() == 1) {
            // Optimise 1 character case
            char sep = separatorChars.charAt(0);
            while (i < len) {
                if (str.charAt(i) == sep) {
                    if (match || preserveAllTokens) {
                        lastMatch = true;
                        if (sizePlus1++ == max) {
                            i = len;
                            lastMatch = false;
                        }
                        list.add(str.substring(start, i));
                        match = false;
                    }
                    start = ++i;
                    continue;
                }
                lastMatch = false;
                match = true;
                i++;
            }
        } else {
            // standard case
            while (i < len) {
                if (separatorChars.indexOf(str.charAt(i)) >= 0) {
                    if (match || preserveAllTokens) {
                        lastMatch = true;
                        if (sizePlus1++ == max) {
                            i = len;
                            lastMatch = false;
                        }
                        list.add(str.substring(start, i));
                        match = false;
                    }
                    start = ++i;
                    continue;
                }
                lastMatch = false;
                match = true;
                i++;
            }
        }
        if (match || preserveAllTokens && lastMatch) {
            list.add(str.substring(start, i));
        }
        return list.toArray(new String[list.size()]);
    }
	
}