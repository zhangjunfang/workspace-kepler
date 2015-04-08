package com.ctfo.storage.command.service;



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
import com.ctfo.storage.command.dao.HBaseDataSource;
import com.ctfo.storage.command.model.AuthModel;
import com.ctfo.storage.command.model.LogoutModel;
import com.ctfo.storage.command.model.OnOffLineModel;
import com.ctfo.storage.command.model.RegisterModel;


/**
 * HBaseDao
 * 
 * 
 * @author huangjincheng 2014-5-13下午02:52:09
 * 
 */
public class HBaseService {

	private static final Logger log = LoggerFactory.getLogger(HBaseService.class);
	
	/**
	 * 插入一行记录
	 */
	
	private static HTableInterface table = null;


	/**
	 * 注销
	 * @param list
	 */
	public void saveLogoutModelList(List<LogoutModel> list){

		try {
			List<Put> putList = new ArrayList<Put>();
			for(LogoutModel o : list){
				String rowKey = System.currentTimeMillis() +":"+ UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes(o.getVid()), Bytes.toBytes(JSON.toJSONString(o)));
				putList.add(put);
			}
			table = HBaseDataSource.getTable("TH_VEHICLE_LOGOFF");
			if(table != null){
				table.put(putList);
			} else {	
				table.put(putList);
			}
		} catch (IOException e) {
			log.error("存储异常:" + e.getMessage(), e);
		} 
	}
	
	
	public void saveRegisterModelList(List<RegisterModel> list){

		try {
			List<Put> putList = new ArrayList<Put>();
			for(RegisterModel o : list){
				String rowKey = System.currentTimeMillis()+":"+o.getProvinceId()+":"+o.getCityId()+":"+UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes(o.getTid()), Bytes.toBytes(JSON.toJSONString(o)));
				putList.add(put);
			}
			
			table = HBaseDataSource.getTable("TH_TERMINAL_REGISTER");
			if(table != null){
				table.put(putList);
			} else {	
				table.put(putList);
			}
		} catch (IOException e) {
			log.error("存储异常:" + e.getMessage(), e);
		} 
	}
	
	
	public void saveAuthModelList(List<AuthModel> list){

		try {
			List<Put> putList = new ArrayList<Put>();
			for(AuthModel o : list){
				String rowKey = System.currentTimeMillis() + ":" +o.getResult() +":"+o.getCommaddr()+":"+UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes(o.getAkey()), Bytes.toBytes(JSON.toJSONString(o)));
				putList.add(put);
			}
			table = HBaseDataSource.getTable("TH_VEHICLE_CHECKED");
			if(table != null){				
				table.put(putList);
			} else {
				
				table.put(putList);
			}
		} catch (IOException e) {
			log.error("存储异常:" + e.getMessage(), e);
		} 
	}
	
	
	public void saveOnOffLineModelList(List<OnOffLineModel> list){

		try {
			List<Put> putList = new ArrayList<Put>();
			for(OnOffLineModel o : list){
				String rowKey = System.currentTimeMillis() + ":" +o.getVid()+":"+UUID.randomUUID().toString();
				Put put = new Put(Bytes.toBytes(rowKey));
				put.add(Bytes.toBytes("info"), Bytes.toBytes(o.getVid()), Bytes.toBytes(JSON.toJSONString(o)));
				putList.add(put);
			}
			table = HBaseDataSource.getTable("TH_VEHICLE_ONOFFLINE");
			if(table != null){			
				table.put(putList);
			} else {				
				table.put(putList);
			}
		} catch (IOException e) {
			log.error("存储异常:" + e.getMessage(), e);
		} 
	}


	/**
	 * 获取插入一行记录的值
	 * @return table  
	 */
	public static HTableInterface getTable() {
		return table;
	}


	/**
	 * 设置插入一行记录的值
	 * @param table
	 */
	public void setTable(HTableInterface table) {
		HBaseService.table = table;
	}
	
	
	
	
}
