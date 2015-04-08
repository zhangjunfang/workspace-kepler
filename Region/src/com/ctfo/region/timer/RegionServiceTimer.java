package com.ctfo.region.timer;

import java.text.SimpleDateFormat;
import java.util.Date;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.region.service.RegionServiceRmi;

/*****************************************
 * <li>@描        述：定时器			</li><br>
 * <li>@创  建  者：hushaung 		</li><br>
 * <li>@时        间：2013-6-15  下午3:14:47	</li><br>
 * 
 *****************************************/
public class RegionServiceTimer {
	private static final Logger logger = LoggerFactory.getLogger(RegionServiceTimer.class);
	
	private RegionServiceRmi regionServiceRmi;


	public void timerTbService() {
		long start = System.currentTimeMillis();
		logger.info("开始执行定时器");
		// 将轨迹数据写入文件
		try{
			regionServiceRmi.saveRegionFile();
//			for(int i = 0; i < 1000; i ++){
//				logger.info("该发言人说，朝美在高层会谈中可就缓和紧张的军事气氛、由半岛停战体制转变为和平体制、美国提出的构建无核世界等问题进行深入探讨。会谈的具体时间与地点可由美国决定"+i);
//			}
		}catch(Exception e){
			String data = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date());
			logger.error(data+"存储区域文件异常:",e);
		}
		long end = System.currentTimeMillis();
		logger.info("定时器执行结束,耗时(ms):"+(end - start));
	}

	public RegionServiceRmi getRegionServiceRmi() {
		return regionServiceRmi;
	}

	public void setRegionServiceRmi(RegionServiceRmi regionServiceRmi) {
		this.regionServiceRmi = regionServiceRmi;
	}

}
