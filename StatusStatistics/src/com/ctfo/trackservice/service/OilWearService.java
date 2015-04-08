package com.ctfo.trackservice.service;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Iterator;
import java.util.TreeMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.trackservice.model.OilWearBean;



/**
 * 文件名：OilWearService.java
 * 功能：
 *
 * @author huangjincheng
 * 2014-9-25上午9:45:04
 * 
 */
public class OilWearService {
	private final static Logger logger = LoggerFactory.getLogger(OilWearService.class);
	private OilWearBean oilWear = new OilWearBean();
	//private OracleService oracleService = new OracleService();
	private long utc;
	private String vid;
	public OilWearService(long utc,String vid){
		this.utc = utc;
		this.vid = vid;
		oilWear.setStatDate(this.utc+12*60*60*1000);
		oilWear.setVid(this.vid);
	}
	
	/*******
	 * 解析油量监控数据
	 * @param file
	 * @throws IOException 
	 */
	public OilWearBean analysisOilRecords(File file){
		BufferedReader buf = null;
		//long analysisOilRecordsStartTime=System.currentTimeMillis();
		try {
			buf = new BufferedReader(new FileReader(file));
			TreeMap<Long, String>  map = getOrderMap(buf);
			if(map.size() > 0){
				boolean isOil = false;
				int currentOil = 0;
				int lastOil = -1;
				Iterator<Long> oilIt = map.keySet().iterator();
				while(oilIt.hasNext()){
					long utc = oilIt.next();				
					String line = map.get(utc);
					String[] arr = line.split(":");
					long speed = Long.parseLong("".equals(arr[4])?"0":arr[4]);
					/*****
					 * 判断是否偷油，油位异常标志  B1B0=00  油位正常; B1B0=01 偷油告警;B1B0=10 加油告警;B1B0=11 保留;
					 * 每日只要有一次偷油，则标记为偷油
					 */			
					if("11".equals(arr[6])){ // 跳过保留值
						continue;
					}
					
					if(("01".equals(arr[6])||"10".equals(arr[6])) && !isOil){
						isOil = true;
						oilWear.setChanged_type(arr[6]);
					}
					
					int oil = Integer.parseInt(arr[8]);
					if("10".equals(arr[6]) || "01".equals(arr[6])){
/*						//油箱油量监控变化详细，只记录加油和偷漏油
						OilmassChangedDetail oilmassChangedDetail = new OilmassChangedDetail();
						oilmassChangedDetail.setLat(Long.parseLong(notNull(arr[0])));
						oilmassChangedDetail.setLon(Long.parseLong(notNull(arr[1])));
						oilmassChangedDetail.setElevation(Integer.parseInt(notNull(arr[2])));
						oilmassChangedDetail.setDirection(Integer.parseInt(notNull(arr[3])));
						oilmassChangedDetail.setGps_speed(Integer.parseInt(notNull(arr[4])));
						oilmassChangedDetail.setGpsTime(notNull(arr[5]));
						oilmassChangedDetail.setChangeType(notNull(arr[6]));					
						oilmassChangedDetail.setCurr_oilmass(Double.parseDouble(notNull(arr[9])));
						oilmassChangedDetail.setChange_oilmass(oil);
						oilChangeList.add(oilmassChangedDetail);*/
						//当日加油量=当日加油量之和
						if("10".equals(arr[6])){
							logger.info("加油源数据："+line+",文件名："+file.getName());	
							oilWear.addAddOil(oil);
						}else{
							//当日减少量=当日偷油量之和
							logger.info("偷漏油源数据："+line+",文件名："+file.getName());							
							oilWear.addDecreaseOil(oil);
						}
					}
					
					// 当前油量
					currentOil = Integer.parseInt(arr[9]);
					
					/******
					 * 当日正常消耗量=（连续两条数据的当前油量之差的绝对值-本次加油量（若有加油则计算）-本次偷油量（若有偷油则计算））的绝对值 全天累计之和
					 * 如：
					 * 数据1：当前油量10    
					 * 数据2：加油5L,当前油量12L 
					 * 则正常消耗量=||（10-12）|-5|=3  
					 * 
					 * 如：
					 * 数据1：当前油量10     
					 * 数据2：偷油油5L,当前油量3L 
					 * 则正常消耗量=||（10-3）|-5|=2
					 * 
					 * 正常消耗量 = 开始时间油量值 + 期间加油量 - 期间异常减少量 - 结束时间油量值
					 */
					if(-1 != lastOil){
						if("10".equals(arr[6])){ //判断当前是否为加油
							if(speed>50){
								oilWear.addRunningOil(Math.abs(lastOil + oil - currentOil));
							}
							oilWear.addUsedOil(Math.abs(lastOil + oil - currentOil));
						}else if("01".equals(arr[6])){ //判断当前是否为偷油
							if(speed>50){
								oilWear.addRunningOil(Math.abs(lastOil + oil - currentOil));
							}
							oilWear.addUsedOil(Math.abs(lastOil - oil - currentOil));
						}else{
							if(speed>50){
								oilWear.addRunningOil(Math.abs(lastOil + oil - currentOil));
							}
							oilWear.addUsedOil(Math.abs(lastOil - currentOil)<=50?Math.abs(lastOil - currentOil):0);
						}
					}
					lastOil = currentOil;
				}// End while
	/*			if(oilChangeList.size()>0){
					OracleService.saveOilChangedMass(OracleConnectionPool.getConnection(),file, oilChangeList);
				}
			*/
			}
			//long analysisOilRecordsEndTime =System.currentTimeMillis();
			//logger.info("----"+file.getName()+"----分析油量监控时长："+(analysisOilRecordsEndTime-analysisOilRecordsStartTime)/1000+"s");
		} catch (FileNotFoundException e) {
			logger.error("解析油量监控文件出错" + file.getAbsolutePath(),e);
		} catch (Exception e) {
			logger.error("解析油量监控文件出错",e);
		}finally{
			if(null != buf){
				try {
					buf.close();
				} catch (IOException e) {
					logger.error(e.getMessage(), e);
				}
			}
		}
		return oilWear;
	}
	
	/**
	 * 使用终端上报时间进行排序
	 */
	private TreeMap<Long, String> getOrderMap(BufferedReader buf) {
		TreeMap<Long, String> returnOilMap = new TreeMap<Long, String>();
		String readLine = null;
		String gpsdate = null;
		String[] oil = null;
		try {
			while ((readLine = buf.readLine()) != null) {
				// 报警文件每行的数据分割
				oil = readLine.split(":");
				if(oil.length >=10) {
					gpsdate = oil[5];
					long utc = getTime(gpsdate);
					returnOilMap.put(utc,readLine); // 按GPS时间排序滤重
					
				}	
			}// End while
		} catch (Exception e) {
			logger.error("读取油量监控文件信息出错",e);
		}
		
		return returnOilMap;
	}
	
	
	public static long getTime(String gpsdate){
		long ret = 0;
		try {
			SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss");
			Date d = sdf.parse("20"+gpsdate);
			ret = d.getTime();
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return ret;
	}
	
	
/*	private String notNull(String s){
		if("".equals(s) || null == s){
			return "0";
		}
		else return s;
	}
	*/
	
	
}
