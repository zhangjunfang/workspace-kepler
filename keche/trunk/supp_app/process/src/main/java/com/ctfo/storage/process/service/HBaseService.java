package com.ctfo.storage.process.service;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import org.apache.hadoop.hbase.client.HTable;
import org.apache.hadoop.hbase.client.HTableInterface;
import org.apache.hadoop.hbase.client.Put;
import org.apache.hadoop.hbase.util.Bytes;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.storage.process.dao.HBaseDataSource;
import com.ctfo.storage.process.model.AlarmEnd;
import com.ctfo.storage.process.model.AlarmStart;
import com.ctfo.storage.process.model.ThAlarm;
import com.ctfo.storage.process.model.ThEvent;
import com.ctfo.storage.process.model.ThOil;
import com.ctfo.storage.process.model.ThVehicleStatus;
import com.ctfo.storage.process.model.TrackFile;

/**
 * HBaseDao
 * 
 * 
 * @author huangjincheng 2014-5-13下午02:52:09
 * 
 */
public class HBaseService {
	private static final Logger log = LoggerFactory.getLogger(HBaseService.class);
	/**	HBase表实例	*/
	//private HTable table = null;
	private HTableInterface table = null;
	/**	表名	*/
	private String tableName;
	
	/**	缓冲区put数目	*/
	private int count;
	
	@SuppressWarnings("deprecation")
	public HBaseService(String tableName) throws Exception { 
		try {
			this.tableName = tableName;
			//table = new HTable(HBaseDataSource.getConf(), tableName);
			table = HBaseDataSource.getTablePool().getTable(tableName);
			table.setAutoFlush(false);//hbase缓冲区自动刷写，提高大量数据写入性能
			table.setWriteBufferSize(10485760);//20M:20971520
		} catch (Exception e) {
			throw new Exception("启动HBase服务异常:" + e.getMessage(), e);
		}
	}
	/**
	 * 存储报警结束列表
	 * @param list
	 */
	public void saveAlarmEndList(List<AlarmEnd> list){
		try {
			//List<Put> putList = new ArrayList<Put>();
			for(AlarmEnd alarmEnd : list){
				String rowKey = alarmEnd.getAlarmId();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes("alarmEnd"), Bytes.toBytes(JSON.toJSONString(alarmEnd)));
				table.put(put);
			}
			//log.info("Auto flush:"+table.isAutoFlush());
			//table.flushCommits();//将所有put一次性写入hbase
		} catch (Exception e) {	
			log.error("存储报警信息列表异常:" + e.getMessage(), e);
			try {
				table = new HTable(HBaseDataSource.getConf(), tableName);
			} catch (IOException e1) {
				log.error("存储报警信息列表--重置HTable对象异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 存储报警开始列表
	 * @param list
	 */
	public void saveAlarmStartList(List<AlarmStart> list){
		try {
			//List<Put> putList = new ArrayList<Put>();
			for(AlarmStart alarm : list){
				String rowKey = alarm.getAlarmId()+ ":" + UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes("alarmStart"), Bytes.toBytes(JSON.toJSONString(alarm)));
				table.put(put);
			}
			//table.flushCommits();//将所有put一次性写入hbase
		} catch (Exception e) {
			log.error("存储报警开始列表异常:" + e.getMessage(), e);
			try {
				table = new HTable(HBaseDataSource.getConf(), tableName);
			} catch (IOException e1) {
				log.error("存储报警开始列表--重置HTable对象异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 存储车辆状态信息列表
	 * @param list
	 */
	public void saveVehicleStatusList(List<ThVehicleStatus> list) {
		try {
			//List<Put> putList = new ArrayList<Put>();
			for (ThVehicleStatus status : list) {
				String rowKey = System.currentTimeMillis() + status.getVid()+ ":" + UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes(status.getEntId()), Bytes.toBytes(JSON.toJSONString(status)));
				table.put(put);
			}
			//table.flushCommits();//将所有put一次性写入hbase
		} catch (Exception e) {
			log.error("存储车辆状态信息列表异常:" + e.getMessage(), e);
			try {
				table = new HTable(HBaseDataSource.getConf(), tableName);
			} catch (IOException e1) {
				log.error("存储车辆状态信息列表--重置HTable对象异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 存储车辆轨迹信息列表
	 * @param list
	 */
	public void saveTrackFileList(List<TrackFile> list) {
		try {
			//List<Put> putList = new ArrayList<Put>();
			for (TrackFile track : list) {
				String rowKey = track.getGpsTime() + ":" + UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes(track.getVid()), Bytes.toBytes(JSON.toJSONString(track)));
				table.put(put);
			}
			//table.flushCommits();//将所有put一次性写入hbase
		} catch (Exception e) {
			log.error("存储车辆轨迹信息列表异常:" + e.getMessage(), e);
			try {
				table = new HTable(HBaseDataSource.getConf(), tableName);
			} catch (IOException e1) {
				log.error("存储车辆轨迹信息列表--重置HTable对象异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 存储报警文件列表
	 * @param list
	 */
	public void saveAlarmFileList(List<ThAlarm> alarmFileList) {
		try {
			//List<Put> putList = new ArrayList<Put>();
			for(ThAlarm alarmFile : alarmFileList){
				String rowKey = alarmFile.getGpsTime() + ":" + UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes(alarmFile.getVid()), Bytes.toBytes(JSON.toJSONString(alarmFile)));
				table.put(put);
			}
			//table.flushCommits();//将所有put一次性写入hbase
		} catch (Exception e) {
			log.error("存储报警文件列表异常:" + e.getMessage(), e);
			try {
				table = new HTable(HBaseDataSource.getConf(), tableName);
			} catch (IOException e1) {
				log.error("存储报警文件列表--重置HTable对象异常:" + e.getMessage(), e);
			}
		} 
	}
	/**
	 * 存储驾驶事件列表
	 * @param list
	 */
	public void saveEventList(List<ThEvent> eventList) {
		try {
			//List<Put> putList = new ArrayList<Put>();
			for(ThEvent event : eventList){
				String rowKey = event.getStartTime() + UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes(event.getVid()), Bytes.toBytes(JSON.toJSONString(event)));
				table.put(put);
			}
			//log.info("Auto flush:"+table.isAutoFlush()+":"+table.getWriteBufferSize());
			//table.flushCommits();//将所有put一次性写入hbase
		} catch (Exception e) {
			log.error("存储驾驶事件异常:" + e.getMessage(), e);
			try {
				table = new HTable(HBaseDataSource.getConf(), tableName);
			} catch (IOException e1) {
				log.error("存储驾驶事件列表--重置HTable对象异常:" + e.getMessage(), e);
			}
		}   
	}
	/**
	 * 存储油量文件列表
	 * @param list
	 */
	public void saveOilList(List<ThOil> oilList) {
		try {
			//List<Put> putList = new ArrayList<Put>();
			for (ThOil oil : oilList) {
				String rowKey = oil.getUpTime() + UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes(oil.getVid()), Bytes.toBytes(JSON.toJSONString(oil)));
				table.put(put);
			}
			//table.flushCommits();//将所有put一次性写入hbase
		} catch (Exception e) {
			log.error("存储油量文件列表异常:" + e.getMessage(), e);
			try {
				table = new HTable(HBaseDataSource.getConf(), tableName);
			} catch (IOException e1) {
				log.error("存储油量文件列表--重置HTable对象异常:" + e.getMessage(), e);
			}
		}
	}
	/**
	 * 获取table的值
	 * @return table  
	 */
	public HTableInterface getTable() {
		return table;
	}
	/**
	 * 设置table的值
	 * @param table
	 */
	public void setTable(HTableInterface table) {
		this.table = table;
	}
	/**
	 * 获取count的值
	 * @return count  
	 */
	public int getCount() {
		return count;
	}
	/**
	 * 设置count的值
	 * @param count
	 */
	public void setCount(int count) {
		this.count = count;
	}
	
	
	
}
