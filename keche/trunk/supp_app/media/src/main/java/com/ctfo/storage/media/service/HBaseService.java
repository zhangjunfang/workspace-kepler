package com.ctfo.storage.media.service;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import org.apache.hadoop.hbase.client.HTableInterface;
import org.apache.hadoop.hbase.client.Put;
import org.apache.hadoop.hbase.util.Bytes;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.storage.media.dao.HBaseDataSource;
import com.ctfo.storage.media.model.MediaEvent;
import com.ctfo.storage.media.model.MediaInfo;

/**
 * HBaseDao
 * 
 * 
 * @author huangjincheng 2014-5-13下午02:52:09
 * 
 */
//@SuppressWarnings("deprecation")
public class HBaseService {

	private static final Logger log = LoggerFactory.getLogger(HBaseService.class);
	
	/**
	 * 插入一行记录
	 */
//	private static HTablePool tablePool = null;
	
	private static HTableInterface table = null;
/**
	 * @param string
	 */
	public HBaseService(String tableName) {
//		HTableFactory hTableFactory = new HTableFactory();
//		hTableFactory.createHTableInterface(HBaseDataSource.getConf(), Bytes.toBytes(tableName));
//		tablePool = new HTablePool(HBaseDataSource.getConf(), 2, hTableFactory, PoolType.ThreadLocal);
		table = HBaseDataSource.getTable(tableName);
	}
	/**
	 * 存储多媒体事件信息列表
	 * @param list
	 */
	public void saveMediaEventList(List<MediaEvent> list){
//		HTableInterface table = null;
//		MyHTablePool tablePool = null;
		try {
			List<Put> putList = new ArrayList<Put>();
			for(MediaEvent mediaEvent : list){
				String rowKey = System.currentTimeMillis() + UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				JSON.toJSONString(mediaEvent);
				put.add(Bytes.toBytes("info"), Bytes.toBytes(mediaEvent.getPlate()), Bytes.toBytes(JSON.toJSONString(mediaEvent)));
				putList.add(put);
			}
//			tablePool =   HBaseDataSource.getHTablePool("TH_VEHICLE_MULTIMEDIA_EVENT");  
			if(table != null){
				table.put(putList);
			} else {
//				if(tablePool == null){
//					HTableFactory hTableFactory = new HTableFactory();
//					hTableFactory.createHTableInterface(HBaseDataSource.getConf(), Bytes.toBytes("TH_VEHICLE_MULTIMEDIA_EVENT"));
//					tablePool = new HTablePool(HBaseDataSource.getConf(), 2, hTableFactory, PoolType.ThreadLocal);
////					table = tablePool.getTable("TH_VEHICLE_MULTIMEDIA_EVENT");
//				}
				table = HBaseDataSource.getTable("TH_VEHICLE_MULTI_EVENT");
	//			table = HBaseDataSource.getHTablePool("TH_VEHICLE_MULTIMEDIA_EVENT");
				table.put(putList);
			}
			//			HBaseDataSource.getHTablePool().getTable("TH_VEHICLE_MULTIMEDIA_EVENT").put(putList);
		} catch (IOException e) {
			log.error("存储多媒体事件信息列表异常:" + e.getMessage(), e);
		} 
//		finally{
//			try {
//				tablePool.putHTableBack(table);
//				table.close();
//			} catch (IOException e) {
//				log.error("存储多媒体事件信息列表关闭异常:" + e.getMessage(), e);
//			}
//		}
	}
	/**
	 * 存储多媒体信息列表
	 * @param list
	 */
	public void saveMediaInfoList(List<MediaInfo> list) {
//		HTableInterface table = null;
//		MyHTablePool tablePool = null;
		try {
			List<Put> putList = new ArrayList<Put>();
			for(MediaInfo mediaInfo : list){
				String rowKey = mediaInfo.getSysTime() + UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				JSON.toJSONString(mediaInfo);
				put.add(Bytes.toBytes("info"), Bytes.toBytes(mediaInfo.getPlate()), Bytes.toBytes(JSON.toJSONString(mediaInfo)));
				putList.add(put);
			}
			if(table != null){
				table.put(putList);
			} else {
//				if(tablePool == null){
//					HTableFactory hTableFactory = new HTableFactory();
//					hTableFactory.createHTableInterface(HBaseDataSource.getConf(), Bytes.toBytes("TH_VEHICLE_MEDIA"));
//					tablePool = new HTablePool(HBaseDataSource.getConf(), 2, hTableFactory, PoolType.ThreadLocal);
////					table = tablePool.getTable("TH_VEHICLE_MEDIA");
//				}
				table = HBaseDataSource.getTable("TH_MEDIA");
	//			table = HBaseDataSource.getHTablePool("TH_VEHICLE_MULTIMEDIA_EVENT");
				table.put(putList);
			}
//			tablePool =   HBaseDataSource.getHTablePool("TH_VEHICLE_MEDIA");  
//			table = tablePool.getHTable("TH_VEHICLE_MEDIA");
////			table = HBaseDataSource.getHTablePool("TH_VEHICLE_MULTIMEDIA_EVENT");
//			table.put(putList);
			
//			htable = HBaseDataSource.getHTablePool("TH_VEHICLE_MEDIA");
//			htable.put(putList);
			
//			HBaseDataSource.getHTablePool().getTable("TH_VEHICLE_MEDIA").put(putList);
		} catch (IOException e) {
			log.error("存储多媒体事件信息列表异常:" + e.getMessage(), e);
		}  
//		finally{
//			tablePool.putHTableBack(table);
//			try {
//				htable.close();
//			} catch (IOException e) {
//				log.error("存储多媒体事件信息列表关闭异常:" + e.getMessage(), e);
//			}
//		}
	}
	
}
