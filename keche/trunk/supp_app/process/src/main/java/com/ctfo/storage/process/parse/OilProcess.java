/**
 * 
 */
package com.ctfo.storage.process.parse;

import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.storage.process.model.Oil;
import com.ctfo.storage.process.model.ThOil;
import com.ctfo.storage.process.util.Converser;
import com.ctfo.storage.process.util.Tools;

/**
 * 油量数据处理线程
 *
 */
public class OilProcess extends Thread {
	private static Logger log = LoggerFactory.getLogger(OilProcess.class);
	/**	多媒体文件队列	*/
	private static ArrayBlockingQueue<Oil> queue = new ArrayBlockingQueue<Oil>(100000);
	/**	计数器	*/
	private int index = 0;
	/**	最后提交时间	*/
	private long lastTime = System.currentTimeMillis();
	/**	油量存储线程	*/
	private OilStorage oilStorage;
	
	public OilProcess() throws Exception{ 
		setName("OilProcess");
		oilStorage = new OilStorage();
		oilStorage.start();
	}
	
	public void run(){
		while(true){
			try {
				Oil oil = queue.take();//获取并移除此队列的头部，在元素变得可用之前一直等待（如果有必要）。						
				index++;
				process(oil);
				long current = System.currentTimeMillis();
				if ((current - lastTime) > 10000) {
					int queueSize = getQueueSize();
					log.info("OilProcess-10秒处理[{}]条, 排队[{}]条", index, queueSize);
					index = 0;
					lastTime = System.currentTimeMillis();
				}
			} catch (Exception e) {
				log.error("油量数据处理队列数据异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 获取队列大小
	 * @return
	 */
	private int getQueueSize() {
		return queue.size();
	}
	/**
	 * 处理油量数据
	 * @param Oil
	 * @throws  InterruptedException 
	 */
	private void process(Oil oil)  {
		ThOil thOil = parsePassThrough(oil.getPassThroughStr());
		oilStorage.put(thOil);
	}
	
	/**
	 * 处理透传数据
	 * @param passThroughStr
	 */
	private ThOil parsePassThrough(String passThroughStr) {
		try {
			if (passThroughStr == null || passThroughStr.length() == 0) {
				return null;
			}
			byte[] b;
			byte[] a;
			ThOil oil = new ThOil();
			a = Tools.base64DecodeToArray(passThroughStr);
			b = new byte[a.length];
			for (int i = 0; i < a.length; i++) {
				b[i] = (byte) (a[i] & 0xff);
			}
			int locZspt = -1;
			// 跳过消息透传类型 0x82
			locZspt += 1;
			// 跳过协议版本号
			locZspt += 1;
			String baseInfo = getBasicInfo(b, locZspt);
			oil.setBaseStatus(baseInfo);
			locZspt += 20;
			// ----防偷油数据
			byte ftlyData[] = new byte[b.length - locZspt];
			System.arraycopy(b, locZspt, ftlyData, 0, b.length - locZspt);
			int ftyLoc = 0;
			byte oilboxStateData[] = new byte[1];
			System.arraycopy(ftlyData, ftyLoc, oilboxStateData, 0, 1);
			String oilboxStateStr = Converser.hexTo2BCD(Converser.bytesToHexString(oilboxStateData));
			String oilboxState = oilboxStateStr.substring(5, oilboxStateStr.length());
			// log.info(NAME+"【防偷漏油】油位异常标志-->: "+oilboxState);
			// 000正常 001偷油 010加油
			if ("000".equals(oilboxState) || "001".equals(oilboxState) || "010".equals(oilboxState)) {

				String state = oilboxState.substring(1);
				oil.setOilBoxState(state);
				// oil.append(state);[1]
				// oil.append(":");

				ftyLoc += 1;
				// ----燃油液位
				byte oilboxLevelData[] = new byte[4];
				System.arraycopy(ftlyData, ftyLoc, oilboxLevelData, 3, 1);
				int oilboxLevelTemp = Converser.bytes2int(oilboxLevelData);
				// String oilboxLevel=df.format(oilboxLevelTemp);
				// log.info(NAME+"【防偷漏油】燃油液位-->: "+oilboxLevel);
				oil.setOilBoxLevelTemp(oilboxLevelTemp);
				// oil.append(oilboxLevelTemp);
				// oil.append(":");

				ftyLoc += 3;
				// ----本次加油量
				byte addOilData[] = new byte[4];
				System.arraycopy(ftlyData, ftyLoc, addOilData, 2, 2);
				// 定义新数组，调整后两个数组的位置
				byte addOilNewData[] = new byte[4];
				addOilNewData[0] = addOilData[0];
				addOilNewData[1] = addOilData[1];
				addOilNewData[2] = addOilData[3];
				addOilNewData[3] = addOilData[2];

				int addOilTemp = Converser.bytes2int(addOilNewData);
				// String addOil=df.format(addOilTemp);
				// log.info(NAME+"【防偷漏油】本次加油量-->: "+addOil);
				oil.setAddOilTemp(addOilTemp);
				// oil.append(addOilTemp);
				// oil.append(":");

				ftyLoc += 2;
				// ----油箱燃油油量
				byte oilboxMassData[] = new byte[4];
				System.arraycopy(ftlyData, ftyLoc, oilboxMassData, 2, 2);
				// 定义新数组，调整后两个数组的位置
				byte oilboxMassNewData[] = new byte[4];
				oilboxMassNewData[0] = oilboxMassData[0];
				oilboxMassNewData[1] = oilboxMassData[1];
				oilboxMassNewData[2] = oilboxMassData[3];
				oilboxMassNewData[3] = oilboxMassData[2];

				int oilboxMassTemp = Converser.bytes2int(oilboxMassNewData);
				// String oilboxMass=df.format(oilboxMassTemp);
				// log.info(NAME+"【防偷漏油】油箱燃油油量-->: "+oilboxMass);
				// oil.append(oilboxMassTemp);
				oil.setOilBoxMassTemp(oilboxMassTemp);
				// if ("01".equals(state) || "10".equals(state)) { // 遇到B1B0=01
				// saveOilChanged(oil.toString(), vid);
				// if ("01".equals(state)) { // 如果是偷油则在报警表存储偷油告警
				// saveStealingOilAlarm(oil.toString(), vid,
				// GeneratorPK.instance().getPKString());
				// }
				// }

			}
			return oil;
		} catch (Exception e) {
			log.error("存储油量监控信息异常:" + e.getMessage(), e);
			return null;
		}

	}

	/**
	 * 解析防偷油基本信息
	 * @param buf
	 * @param locZspt
	 * @return
	 */
	public static String getBasicInfo(byte[] buf,int locZspt){	
		//纬度		
		byte latBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, latBytes, 0, 4);		
		int lattmp = Converser.bytes2int(latBytes);
		int lat = lattmp;
		
		double tmpLat = (lat*6)/10;
		
		locZspt += 4;
		//经度		
		byte lonBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, lonBytes, 0, 4);		
		int lontmp = Converser.bytes2int(lonBytes);		
		int lon = lontmp;
		
		double tmpLon = (lon*6)/10;
		
		locZspt += 4;
		//海拔高度		
		byte elevBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, elevBytes, 2, 2);		
		int elevtmp = Converser.bytes2int(elevBytes);		
		int elev = elevtmp;
		
		locZspt += 2;
		//速度      WORD格式为什么还要new byte[4],本来可以new byte[2],
		//因为INT 类型是4个字节，所以为了避免两个字节强转出现异常,创建4个字节
		byte speedBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, speedBytes, 2, 2);		
		int speedtmp = Converser.bytes2int(speedBytes);		
		//double speed = elevtmp/10;
		
		locZspt += 2;
		//方向				
		byte directionBytes[] = new byte[4];		
		System.arraycopy(buf, locZspt, directionBytes, 2, 2);		
		int direction = Converser.bytes2int(directionBytes);
		
		locZspt += 2;
		//时间		
		byte timeBytes[] = new byte[6];		
		System.arraycopy(buf, locZspt, timeBytes, 0, 6);		
		String time = Converser.bcdToStr(timeBytes, 0, 6);	
	   //log.info(NAME+"【"+moduleName+"】纬度-->:"+lat+" 经度-->:"+lon+" 海拔-->:"+elev+"速度-->:"+speed+" 方向-->:"+direction+" 时间-->:"+time);		
		
		//赋值对象
		StringBuffer bsBuf = new StringBuffer();
		//bsBuf.append(lat);//纬度
		bsBuf.append(Math.round(tmpLat));
		bsBuf.append(":");
		//bsBuf.append(lon);//经度
		bsBuf.append(Math.round(tmpLon));
		bsBuf.append(":");
		bsBuf.append(elev);//海拔
		bsBuf.append(":");
		bsBuf.append(direction);//方向
		bsBuf.append(":");
		bsBuf.append(speedtmp);//GPS车速
		bsBuf.append(":");
		bsBuf.append(time);//终端上报时间	
		
//		uhc.setLatitude(""+lat);   
//		uhc.setLongesttalk(""+lon);
//		uhc.setElevation(""+elev); 
//		uhc.setDirection(""+direction);
//		uhc.setGps_speeding(""+speed);
//		uhc.setSpeed(""+speed);    		
//		uhc.setTerminal_time(time);		
		return bsBuf.toString();
}

	/**
	 * 将data插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则返回 false。
	 * @param data
	 */
	public static boolean offer(Oil oil){
		return queue.offer(oil);
	}
	/**
	 * 将指定的元素插入此队列的尾部，如果该队列已满，则等待可用的空间。
	 * @param data
	 * @return
	 */
	public void put(Oil oil){
		try {
			queue.put(oil);
		} catch (InterruptedException e) {
			log.error("插入数据到队列异常!"); 
		}
	}
	/**
	 * 将指定的元素插入到此队列的尾部（如果立即可行且不会超过该队列的容量），在成功时返回 true，如果此队列已满，则抛出 IllegalStateException。
	 * @param data
	 * @return
	 */
	public static boolean add(Oil oil){
		return queue.add(oil);
	}
}
