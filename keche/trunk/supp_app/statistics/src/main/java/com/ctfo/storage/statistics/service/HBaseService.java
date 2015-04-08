package com.ctfo.storage.statistics.service;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.apache.hadoop.hbase.client.HTable;
import org.apache.hadoop.hbase.client.Put;
import org.apache.hadoop.hbase.util.Bytes;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.storage.statistics.dao.HBaseDataSource;
import com.ctfo.storage.statistics.model.Vehicle;

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
	private HTable table = null;
	/**	表名	*/
	private String tableName;
	/**
	 * @param tableName
	 * @throws IOException 
	 */
	public HBaseService(String tableName) throws Exception { 
		try {
			this.tableName = tableName;
			table = new HTable(HBaseDataSource.getConf(), tableName);
		} catch (Exception e) {
			throw new Exception("启动HBase服务异常:" + e.getMessage(), e);
		}
	}
	/**
	 * 存储报警结束列表
	 * @param list
	 */
	public void saveAlarmEndList(List<Vehicle> list){
		try {
			List<Put> putList = new ArrayList<Put>();
			for(Vehicle vehicle : list){
				String rowKey = vehicle.getVid();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes("alarmEnd"), Bytes.toBytes(JSON.toJSONString(vehicle)));
				putList.add(put);
			}
			if(putList.size() > 0){
				table.put(putList);
			}
			putList.clear();
		} catch (Exception e) {
			log.error("存储报警信息列表异常:" + e.getMessage(), e);
			try {
				table = new HTable(HBaseDataSource.getConf(), tableName);
			} catch (IOException e1) {
				log.error("存储报警信息列表--重置HTable对象异常:" + e.getMessage(), e);
			}
		}
	}
}
