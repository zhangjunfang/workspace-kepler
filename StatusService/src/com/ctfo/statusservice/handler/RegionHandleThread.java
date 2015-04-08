/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： TrackService		</li><br>
 * <li>文件名称：com.ctfo.trackservice.service TrackHandleThread.java	</li><br>
 * <li>时        间：2013-9-16  下午1:40:48	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.statusservice.handler;

import java.util.UUID;
import java.util.concurrent.ArrayBlockingQueue;

import org.apache.commons.lang3.StringUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statusservice.model.Pack;
import com.ctfo.statusservice.model.Region;
import com.ctfo.statusservice.util.Cache;

/*****************************************
 * <li>描        述：轨迹位置处理线程		
 * 
 *****************************************/
public class RegionHandleThread extends Thread {
	private static final Logger logger = LoggerFactory.getLogger(RegionHandleThread.class);
	/** 数据缓冲队列	  */
	private ArrayBlockingQueue<Pack> dataQueue = new ArrayBlockingQueue<Pack>(100000);
	/** 线程编号	  */
	private int threadId;
	/** 计数器	  */
	private int index;
	/** 计数器2	  */
	private int index2;
	/** 上次时间	  */
	private long lastTime = System.currentTimeMillis();
	
	/** 跨域存储线程 */
	private RegionStorageThread regionStorageThread;
	
	public RegionHandleThread(int threadId, RegionStorageThread regionStorageThread) {
		super("RegionHandleThread" + threadId);
		this.threadId = threadId;
		this.regionStorageThread = regionStorageThread;
	}
	/*****************************************
	 * <li>描        述：将数据插入队列顶部 		</li><br>
	 * <li>时        间：2013-9-16  下午4:42:17	</li><br>
	 * <li>参数： @param dataMap			</li><br>
	 * 
	 *****************************************/
	public void putDataMap(Pack pack) {
		try {
			dataQueue.put(pack);
		} catch (InterruptedException e) {
			logger.error(e.getMessage());
		}
	}
	/*****************************************
	 * <li>描        述：获得队列大小 		</li><br>
	 * <li>时        间：2013-9-16  下午4:42:47	</li><br>
	 * <li>参数： @return			</li><br>
	 * 
	 *****************************************/
	public int getQueueSize() {
		return dataQueue.size();
	}
	
	/**
	 * 	跨域统计线程运行业务处理 <br>
	 * <pre>
	 *	出入境  
	 *  市级：出本市算一次，从其他市到另外市也算一次。从其他市回本市不算。<br>
	 *  省级：出本省算一次，在外省内任何市之间不算，从其他省到另外省算一次，在另外省内任何市不算。<br>
	 * </pre>
	 */
	public void run(){
		String lastCityCodeStr = null;
		String lastCityCode = null;
		String cityCode = null;
		long areaCode = 0;
		while(true){
			try{
				Pack pack = dataQueue.take();
				//1. 根据经纬度获取区域编码
				areaCode = Cache.getAreaAnalyzer(Double.parseDouble(pack.getLonStr())/600000, Double.parseDouble(pack.getLatStr())/600000);
				if(areaCode <= 0){
//					logger.debug("===跨域统计存储服务===地理位置解析异常：经度:"+app.get("1")+" , 纬度:"+app.get("2")+" 车辆VID:"+app.get(Constant.VID)+" , 当前区域码:"+areaCode);
					continue;
				}
				String currentCode = String.valueOf(areaCode);
				//2. 根据VID获取上一次车辆所在地区域编码（前4位）
				lastCityCodeStr =Cache.getAreaLastMapValue(pack.getVid());
				if(lastCityCodeStr == null || lastCityCodeStr.length() < 4){
					Cache.setAreaLastMap(pack.getVid(), currentCode);
					continue;
				}
				lastCityCode = lastCityCodeStr.substring(0, 4);
				cityCode = Cache.getVehicleMapValue(pack.getMacid()).getAreacode();
				if(!StringUtils.isNumeric(cityCode)){
					continue;
				}
				//3. 如果区域编码改变，存储当前变化信息，更新缓存中当前区域编码
//				logger.debug("---跨域统计存储服务---处理当前区域信息：经度:"+app.get("1")+" , 纬度:"+app.get("2")+" 车辆VID:"+app.get(Constant.VID)+" , 属地码:"+cityCode+" , 上一区域码:"+lastCityCode+" , 当前区域码:"+areaCode);
				if(immigrationLogical(currentCode, lastCityCode)){
					//判断回境
					if(currentCode.startsWith(cityCode.substring(0, 4))){
						Cache.setAreaLastMap(pack.getVid(), currentCode);
					}else{
						Region region = new Region();
						region.setRegionId(UUID.randomUUID().toString());
						region.setLocalCode(cityCode);
						region.setCurrentCode(currentCode);
						region.setLocalCityCode(cityCode.substring(0, 4));
						region.setCurrentCityCode(currentCode.substring(0, 4));
						region.setLocalProvinceCode(cityCode.substring(0, 2));
						region.setCurrentProvinceCode(currentCode.substring(0, 2));
						region.setCurrentTime(System.currentTimeMillis());
						regionStorageThread.putDataMap(region);
						Cache.setAreaLastMap(pack.getVid(), currentCode);
						index2++;
					}
				}
				long currentTime = System.currentTimeMillis();
				if((currentTime - lastTime ) > 60000){
					int size = getQueueSize();
					logger.info("region:" + threadId + ",排队:(" + size + "),10秒处理:[" + index + "],有效数据[" + index2 +"]");
					index = 0;
					index2 = 0;
					lastTime = System.currentTimeMillis();
				}
				index ++;
			}catch(Exception ex){
				logger.error("跨域统计存储异常: 上一区域代码:"+lastCityCode +", 属地区域代码："+cityCode+", 解析经纬度区域代码:"+areaCode+",异常内容:\n"+ex.getMessage(),ex);
			}
		}
	}
	/**
	 * 判断出境
	 * 出入境    <br>
	 *  市级：出本市算一次，从其他市到另外市也算一次。从其他市回本市不算。<br>
	 *  省级：出本省算一次，在外省内任何市之间不算，从其他省到另外省算一次，在另外省内任何市不算。<br>
	 * @param currentCode 		当前区域编码
	 * @param lastCode			上一次区域编码
	 * @return
	 * boolean
	 *
	 */
	private boolean immigrationLogical(String currentCode, String lastCode){
//		判断省级
		if(currentCode.startsWith(lastCode.substring(0, 2))){ 
			//判断市级
			if(currentCode.startsWith(lastCode.substring(0, 4))){
				return false;
			}else{
				return true;
			}
		}else{
			return true;
		}
	}
}
