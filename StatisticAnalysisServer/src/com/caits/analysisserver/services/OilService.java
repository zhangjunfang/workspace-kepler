package com.caits.analysisserver.services;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.Iterator;
import java.util.TreeMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.bean.OilChangedBean;
import com.caits.analysisserver.bean.VehicleInfo;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.CDate;

public class OilService {
	private static final Logger logger = LoggerFactory.getLogger(OilService.class);
	private OilChangedBean oilChangedBean = null;
	private long utc = 0;
	private String vid;
	Connection dbCon = null;
	
	public OilService(Connection dbCon,long utc, String vid){
		this.utc = utc;
		this.vid = vid;
		this.dbCon = dbCon;
	}
	
	/*******
	 * 解析油量监控数据
	 * @param file
	 * @throws IOException 
	 */
	public void analysisOilRecords(File file){
		BufferedReader buf = null;
		long analysisOilRecordsStartTime=System.currentTimeMillis();
		try {
			buf = new BufferedReader(new FileReader(file));
			TreeMap<Long, String>  map = getOrderMap(buf);
			if(map.size() > 0){
				oilChangedBean = new OilChangedBean();
				boolean isOil = false;
				int currentOil = 0;
				int lastOil = -1;
				Iterator<Long> oilIt = map.keySet().iterator();
				
				while(oilIt.hasNext()){
					long utc = oilIt.next();
					String line = map.get(utc);
					String[] arr = line.split(":");
					/*****
					 * 判断是否偷油，油位异常标志  B1B0=00  油位正常; B1B0=01 偷油告警;B1B0=10 加油告警;B1B0=11 保留;
					 * 每日只要有一次偷油，则标记为偷油
					 */
					
					if("11".equals(arr[6])){ // 跳过保留值
						continue;
					}
					
					if("01".equals(arr[6]) && !isOil){
						isOil = true;
						oilChangedBean.setChanged_type(arr[6]);
					}
					
					int oil = Integer.parseInt(arr[8]);
					
					//当日加油量=当日加油量之和
					if("10".equals(arr[6])){
						oilChangedBean.addAddOil(oil);
					}
					
					//当日减少量=当日偷油量之和
					if("01".equals(arr[6])){
						oilChangedBean.addDecreaseOil(oil);
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
							oilChangedBean.addUsedOil(Math.abs(lastOil + oil - currentOil));
						}else if("01".equals(arr[6])){ //判断当前是否为偷油
							oilChangedBean.addUsedOil(Math.abs(lastOil - oil - currentOil));
						}else{
							oilChangedBean.addUsedOil(Math.abs(lastOil - currentOil));
						}
					}
					
					lastOil = currentOil;
				}// End while
				
				//存储数据库
				saveOilDayStat();
			}
			long analysisOilRecordsEndTime =System.currentTimeMillis();
			logger.info("----"+file.getPath()+"----分析油量监控时长："+(analysisOilRecordsEndTime-analysisOilRecordsStartTime)/1000+"s");
		} catch (FileNotFoundException e) {
			logger.error("解析油量监控文件出错" + file.getAbsolutePath(),e);
		} catch (SQLException e) {
			logger.error("保存油量监控日统计结果出错",e);
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
					long utc = CDate.strToLong(gpsdate);
					returnOilMap.put(utc,readLine); // 按GPS时间排序滤重
					
				}	
			}// End while
		} catch (Exception e) {
			logger.error("读取油量监控文件信息出错",e);
		}
		
		return returnOilMap;
	}	
	
	/******
	 * 存储油量日统计
	 * @throws SQLException
	 */
	private void saveOilDayStat() throws Exception{
		PreparedStatement stSaveOilDayStat = null;
		try{
			//从连接池获取连接
			VehicleInfo info = AnalysisDBAdapter.queryVechileInfo(vid);
			stSaveOilDayStat = dbCon.prepareStatement(SQLPool.getinstance().getSql("sql_saveOilDayStat"));
			stSaveOilDayStat.setLong(1, this.utc + 12 * 60 * 60 * 1000);
			stSaveOilDayStat.setString(2, vid);
			stSaveOilDayStat.setString(3, info.getVehicleNo()); // 车牌号码
			stSaveOilDayStat.setString(4, info.getInnerCode()); // 内部编号
			if(null != info.getVehicleType()){
				stSaveOilDayStat.setString(5, info.getVehicleType()); // 车型
			}else{
				stSaveOilDayStat.setString(5, null); // 车型
			}
			stSaveOilDayStat.setString(6, info.getEntId()); // 企业ID
			stSaveOilDayStat.setString(7, info.getEntName()); // 企业名称
			stSaveOilDayStat.setString(8, info.getTeamId()); // 车队ID
			stSaveOilDayStat.setString(9, info.getTeamName()); // 车队名称
			stSaveOilDayStat.setString(10, null); // 驾驶员ID  注：以后考虑
			stSaveOilDayStat.setString(11, info.getDriverName()); // 驾驶员名称
			stSaveOilDayStat.setString(12, oilChangedBean.getChanged_type());
			stSaveOilDayStat.setInt(13, oilChangedBean.getAddOil());
			stSaveOilDayStat.setInt(14, oilChangedBean.getDecreaseOil());
			stSaveOilDayStat.setInt(15, oilChangedBean.getUsedOil());
			stSaveOilDayStat.executeUpdate();
			logger.info("[VID="+vid+"] 油量监控日统计保存成功！");
		}catch(Exception ex){
			logger.error("存储日油量监控出错." + vid,ex);
		}finally{
			if(null != stSaveOilDayStat){
				stSaveOilDayStat.close();
			}
		}
	}
}
