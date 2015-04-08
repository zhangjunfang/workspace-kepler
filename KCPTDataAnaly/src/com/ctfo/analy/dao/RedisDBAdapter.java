package com.ctfo.analy.dao;

import java.util.HashMap;
import java.util.Map;

import org.apache.log4j.Logger;

import redis.clients.jedis.Jedis;

import com.ctfo.analy.Constant;
import com.ctfo.analy.TempMemory;
import com.ctfo.analy.beans.AlarmBaseBean;
import com.ctfo.analy.beans.AlarmCacheBean;
import com.ctfo.analy.beans.OrgParentInfo;
import com.ctfo.analy.beans.VehicleMessageBean;
import com.ctfo.analy.connpool.RedisConnectionPool;
import com.ctfo.redis.core.RedisAdapter;
import com.ctfo.redis.pool.JedisConnectionPool;

public class RedisDBAdapter {
	private static final Logger logger = Logger.getLogger(RedisDBAdapter.class);
	/*private Jedis jdTrack = null;
	private Jedis jdValid = null;
	private Jedis jdOffLine = null;*/
	
	public RedisDBAdapter(){
	/*	jdTrack = RedisConnectionPool.getJedisConnection();
		jdTrack.select(1); // 选择数据库
		
		jdValid = RedisConnectionPool.getJedisConnection();
		jdValid.select(1);
		
		jdOffLine = RedisConnectionPool.getJedisConnection();
		jdOffLine.select(1);*/
	}

	
	/*****
	 * 启动存储服务，从REDIS获取最近一次报警列表
	 * @param vid
	 * @return
	 */
	public Map<String, String> getLastAlarmCode(String vid){
		Jedis jedis =null;
		try{
			jedis = RedisConnectionPool.getJedisConnection();
			if(jedis.exists(vid)){
				
				String value = jedis.get(vid);
				String[] arr = value.split(":");
				if(null != arr[7]){
					Map<String, String> map = new HashMap<String, String>();
					map.put(Constant.VID, vid);
					map.put(Constant.ALARMCODE, arr[7]);
					return map;
				}
			}
		}catch(Exception ex){
			logger.error("Connection redis server time out:"+ ex.getMessage());
		}finally{
			RedisConnectionPool.returnJedisConnection(jedis);
		}
		return null;
	}
	
	/**
	 * 添加及更新软报警缓存   RedisDao.saveOrUpdateAnalysisAlarm
	 * @param messageBean
	 */
	public void setAnalysisAlarmInfo(VehicleMessageBean messageBean){
		String vid = messageBean.getVid();
		long utc = messageBean.getUtc();
		String alarmCode = messageBean.getAlarmcode();
		try{
			RedisAdapter.saveOrUpdateAnalysisAlarm(vid,utc,alarmCode);
			
			//组装实时告警信息key  key: ALARM0##1#200#201#13897#15432#17653#PLATE湘A12345:8a46c870b2374eb7980a7b69d429cc95,
			StringBuffer sb = new StringBuffer();
			sb.append("ALARM");
			sb.append(alarmCode);
			sb.append("#");
			
			AlarmBaseBean alarmVehicleBean = TempMemory
			.getAlarmVehicleMap(messageBean.getCommanddr());
			if (alarmVehicleBean!=null){
				OrgParentInfo parent  = TempMemory.getOrgParentMap(alarmVehicleBean.getTeamId());
				if (parent!=null&&parent.getParent()!=null){
					String value = parent.getParent().replaceAll(",", "#");
					sb.append(value);
				}
			}
			
			sb.append("PLATE").append(messageBean.getVehicleno());
		    sb.append(":").append(messageBean.getAlarmid()).append(",");
		    
		    String cacheAlarmId = sb.toString();
		    
		    //组装实时告警缓存数据
		    //报警开始时间，车辆编号VID,车牌号,车牌颜色,告警类型,速度,经度,纬度,企业名称,报警id，报警结束UTC时间
		    StringBuffer sbval = new StringBuffer();
		    sbval.append(messageBean.getUtc()).append(":");
		    sbval.append(vid).append(":");
		    sbval.append(messageBean.getVehicleno()==null?"":messageBean.getVehicleno()).append(":");
		    if (alarmVehicleBean!=null){
		    	sbval.append(alarmVehicleBean.getPlateColor()==null?"":alarmVehicleBean.getPlateColor()).append(":");
		    }else{
		    	sbval.append(alarmCode).append(":");
		    }
		    sbval.append(messageBean.getAlarmcode()).append(":");
		    sbval.append(messageBean.getSpeed()).append(":");
		    sbval.append(messageBean.getMaplon()).append(":");
		    sbval.append(messageBean.getMaplat()).append(":");
		    if (alarmVehicleBean!=null){
		    	sbval.append(alarmVehicleBean.getCorpName()==null?"":alarmVehicleBean.getCorpName()).append(":");
		    }else{
		    	sbval.append("").append(":");
		    }
		    
		    sbval.append(messageBean.getAlarmid()).append(":");
		    
		    saveAlarmStartInfo(vid,cacheAlarmId,sbval.toString(),Constant.expiredSeconds);
			
		}catch(Exception ex){
			logger.error("Connection redis server time out:"+ ex.getMessage());
		}
	}
	
	/**
	 * 移除软报警缓存  RedisDao.removeAnalysisAlarm
	 * @param messageBean
	 */
	public void removeAnalysisAlarmInfo(VehicleMessageBean messageBean){

		String vid = messageBean.getVid();

		String alarmCode = messageBean.getAlarmcode();
		
		String key = "analysisalarm_"+vid;

		try{
			RedisAdapter.removeAnalysisAlarm(vid,alarmCode);
			
			//组装告警信息字符串
			StringBuffer sb = new StringBuffer();
			sb.append("ALARM");
			sb.append(alarmCode);
			sb.append("#");
			
			AlarmBaseBean alarmVehicleBean = TempMemory
			.getAlarmVehicleMap(messageBean.getCommanddr());
			if (alarmVehicleBean!=null){
				OrgParentInfo parent  = TempMemory.getOrgParentMap(alarmVehicleBean.getTeamId());
				if (parent!=null&&parent.getParent()!=null){
					String value = parent.getParent().replaceAll(",", "#");
					sb.append(value);
				}
			}
			
			sb.append("PLATE").append(messageBean.getVehicleno());
		    sb.append(":").append(messageBean.getAlarmid()).append(",");
			
			saveAlarmEndInfo(vid,sb.toString(),""+messageBean.getUtc());

		}catch(Exception ex){
			logger.error("Connection redis server time out:"+ ex.getMessage());
		}
	}
	
	public void setAnalysisAlarmInfo(VehicleMessageBean messageBean,AlarmCacheBean alarmCache){
		String vid = messageBean.getVid();
		long utc = messageBean.getUtc();
		String alarmCode = alarmCache.getAlarmcode();
		
		try{
			RedisAdapter.saveOrUpdateAnalysisAlarm(vid,utc,alarmCode);
			
			//组装实时告警信息key  key: ALARM0##1#200#201#13897#15432#17653#PLATE湘A12345:8a46c870b2374eb7980a7b69d429cc95,
			StringBuffer sb = new StringBuffer();
			sb.append("ALARM");
			sb.append(alarmCode);
			sb.append("#");
			
			AlarmBaseBean alarmVehicleBean = TempMemory
			.getAlarmVehicleMap(messageBean.getCommanddr());
			if (alarmVehicleBean!=null){
				OrgParentInfo parent  = TempMemory.getOrgParentMap(alarmVehicleBean.getTeamId());
				if (parent!=null&&parent.getParent()!=null){
					String value = parent.getParent().replaceAll(",", "#");
					sb.append(value);
				}
			}
			
			sb.append("PLATE").append(messageBean.getVehicleno());
		    sb.append(":").append(alarmCache.getAlarmId()).append(",");
		    
		    String cacheAlarmId = sb.toString();
		    
		    //组装实时告警缓存数据
		    //报警开始时间，车辆编号VID,车牌号,车牌颜色,告警类型,速度,经度,纬度,企业名称,报警id，报警结束UTC时间
		    StringBuffer sbval = new StringBuffer();
		    sbval.append(alarmCache.getBeginVmb().getUtc()).append(":");
		    sbval.append(vid).append(":");
		    sbval.append(messageBean.getVehicleno()==null?"":messageBean.getVehicleno()).append(":");
		    if (alarmVehicleBean!=null){
		    	sbval.append(alarmVehicleBean.getPlateColor()==null?"":alarmVehicleBean.getPlateColor()).append(":");
		    }else{
		    	sbval.append(alarmCode).append(":");
		    }
		    sbval.append(alarmCache.getAlarmcode()).append(":");
		    sbval.append(alarmCache.getBeginVmb().getSpeed()).append(":");
		    sbval.append(alarmCache.getBeginVmb().getMaplon()).append(":");
		    sbval.append(alarmCache.getBeginVmb().getMaplat()).append(":");
		    if (alarmVehicleBean!=null){
		    	sbval.append(alarmVehicleBean.getCorpName()==null?"":alarmVehicleBean.getCorpName()).append(":");
		    }else{
		    	sbval.append("").append(":");
		    }
		    
		    sbval.append(alarmCache.getAlarmId()).append(":");
		    
		    sbval.append(alarmCache.getBeginVmb().getDriverId()).append(":");
		    sbval.append(alarmCache.getBeginVmb().getDriverSrc()).append(":");
		    
		    logger.info("alarm str:"+sbval.toString());
		    
		    saveAlarmStartInfo(vid,cacheAlarmId,sbval.toString(),Constant.expiredSeconds);
			
		}catch(Exception ex){
			logger.error("Connection redis server time out:"+ ex.getMessage());
		}
	}
	
	public void removeAnalysisAlarmInfo(VehicleMessageBean messageBean,AlarmCacheBean alarmCache){

		String vid = messageBean.getVid();

		String alarmCode = alarmCache.getAlarmcode();
		
		String key = "analysisalarm_"+vid;

		try{
			RedisAdapter.removeAnalysisAlarm(vid,alarmCode);
			
			//组装告警信息字符串
			StringBuffer sb = new StringBuffer();
			sb.append("ALARM");
			sb.append(alarmCode);
			sb.append("#");
			
			AlarmBaseBean alarmVehicleBean = TempMemory
			.getAlarmVehicleMap(messageBean.getCommanddr());
			if (alarmVehicleBean!=null){
				OrgParentInfo parent  = TempMemory.getOrgParentMap(alarmVehicleBean.getTeamId());
				if (parent!=null&&parent.getParent()!=null){
					String value = parent.getParent().replaceAll(",", "#");
					sb.append(value);
				}
			}
			
			sb.append("PLATE").append(messageBean.getVehicleno());
		    sb.append(":").append(alarmCache.getAlarmId()).append(",");
			
			saveAlarmEndInfo(vid,sb.toString(),""+messageBean.getUtc());

		}catch(Exception ex){
			logger.error("Connection redis server time out:"+ ex.getMessage());
		}
	}
	
	/**
	 * 向缓存中保存实时告警开始信息
	 * @param vid
	 * @param alarmKey 实时告警缓存key
	 * @param alarmValue 实时告警数据串
	 * @param expiredSeconds 缓存失效时间（unit:s）
	 */
	public void saveAlarmStartInfo(String vid,String alarmKey, String alarmValue,int expiredSeconds) {
		Jedis jedis =null;
		try{
			jedis = JedisConnectionPool.getJedisConnection();
			jedis.select(9);
		    //if(jedis.exists(vid)){
		    jedis.setex(alarmKey, expiredSeconds , alarmValue);
			//}
		}catch(Exception ex){
			logger.error("Connection redis server time out:"+ ex.getMessage());
		}finally{
			JedisConnectionPool.returnJedisConnection(jedis);
		}
	}
	
	/**
	 * 向缓存中保存实时告警结束信息
	 * @param vid
	 * @param alarmKey 缓存告警key
	 * @param endValue 告警结束时对应终端UTC时间
	 */
	public void saveAlarmEndInfo(String vid,String alarmKey,String endValue) {
		Jedis jedis =null;
		try{
			jedis = JedisConnectionPool.getJedisConnection();
			jedis.select(9);
		    if(jedis.exists(alarmKey)){
		    	jedis.append(alarmKey,endValue);
			}
		}catch(Exception ex){
			logger.error("Connection redis server time out:"+ ex.getMessage());
		}finally{
			JedisConnectionPool.returnJedisConnection(jedis);
		}
	}
	
	/**
	 * 查询当前车辆驾驶员信息
	 * @param vid
	 * @return
	 */
	public String getCurrentDriverInfo(String vid){
		try{
			//【车辆编号：驾驶员唯一编号：驾驶员姓名：性别（1=男性； 2=女性）：手机号码：身份证号：资格证号：发证机关：联系地址：来源（0=平台绑定 ；1=驾驶员卡）】
			String driverInfo = RedisAdapter.getDriverInfo(vid);
			return driverInfo;

		}catch(Exception ex){
			logger.error("Connection redis server time out:"+ ex.getMessage());
		}
		return null;
	}
	
}

