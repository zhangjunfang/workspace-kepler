/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.syn.task DriverAuthenticationTask.java	</li><br>
 * <li>时        间：2013-8-21  下午7:16:43	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.syncservice.task;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import redis.clients.jedis.Jedis;

import com.ctfo.syncservice.model.SysAreaInfoBean;
import com.ctfo.syncservice.model.SysGeneralCodeBean;
import com.ctfo.syncservice.util.TaskAdapter;


/*****************************************
 * <li>描        述：码表信息同步任务	
 * 
 *****************************************/
public class GeneralCodeTask  extends TaskAdapter {
	private final static Logger logger = LoggerFactory.getLogger(GeneralCodeTask.class);

	/*****************************************
	 * <li>描        述：初始化 		</li><br>
	 * <li>时        间：2013-12-16  上午11:35:36	</li><br>
	 * <li>参数： 			</li><br>
	 * 
	 *****************************************/
	@Override
	public void init() {
		setName("GeneralCodeTask");
	}
	/*****************************************
	 * <li>描 述：同步信息 (oracle to redis)</li><br>
	 * <li>时 间：2013-8-21 下午4:38:15</li><br>
	 * <li>参数：</li><br>
	 * 
	 *****************************************/
	public void execute() {
//		logger.info("--task--syncGeneralCode--同步驾驶员身份数据开始");
		long start = System.currentTimeMillis();
		int index = 0;
		int error = 0;
		Jedis jedis = null;
		Connection oraclecon = null;
		Statement oraclestmt = null;
		ResultSet oraclestmtrsetCode = null;
		ResultSet oraclestmtrsetAreal = null;
		String jsonResult = "";
		List<SysGeneralCodeBean> codeList = new ArrayList<SysGeneralCodeBean>();
		List<SysAreaInfoBean> areaList = new ArrayList<SysAreaInfoBean>();
		try {
			jsonResult += "{";
//			String sqlGeneral ="select t.general_code,t.code_name,t.parent_general_code from sys_general_code t where t.enable_flag !=0";
//			String sqlArea = "select t.area_code,t.area_name,t.area_level from sys_area_info t where t.area_level in (0,1) order by area_code";
	
			oraclecon = this.oracle.getConnection();
//			logger.debug("oracle数据库连接成功");
			// 同步车辆数据
			oraclestmt = oraclecon.createStatement();
			oraclestmtrsetCode = oraclestmt.executeQuery(conf.get("syncGeneralSql"));
//			oraclestmtrsetCode = oraclestmt.executeQuery(sqlGeneral);
			
			while (oraclestmtrsetCode.next()) {
				SysGeneralCodeBean generalBean = new SysGeneralCodeBean();
				String generalCode=oraclestmtrsetCode.getString("GENERAL_CODE");
				String codeName =oraclestmtrsetCode.getString("CODE_NAME");
				String parentGeneralCode=oraclestmtrsetCode.getString("PARENT_GENERAL_CODE");
				generalBean.setGeneralCode(generalCode);
				generalBean.setCodeName(codeName);
				generalBean.setParentGeneralCode(parentGeneralCode);
				codeList.add(generalBean);
				index++;
			}// End while
			if(codeList != null && codeList.size()>0){
				jsonResult += getGeneralCodeJson(codeList);
			}
			
			/***获取行政区域编码**/
			oraclestmtrsetAreal = oraclestmt.executeQuery(conf.get("syncAreaSql"));
//			oraclestmtrsetAreal = oraclestmt.executeQuery(sqlArea);
			
			while (oraclestmtrsetAreal.next()) {
				SysAreaInfoBean arealBean = new SysAreaInfoBean();
				String areaCode=oraclestmtrsetAreal.getString("AREA_CODE");
				String areaName =oraclestmtrsetAreal.getString("AREA_NAME");
				int areaLevel =oraclestmtrsetAreal.getInt("AREA_LEVEL");
				arealBean.setAreaName(areaName);
				arealBean.setAreaCode(areaCode);
				arealBean.setAreaLevel(areaLevel);
				areaList.add(arealBean);
			}// End while
			if(areaList != null  && areaList.size()>0){
				if(!jsonResult.trim().equals("{")){
					jsonResult +=",";
				}
				jsonResult += getAreaInfoJson(areaList);
			}
			jsonResult +="}";
			jedis = this.redis.getJedisConnection();
			jedis.select(3);
			jedis.set("MEMCACHE_GENERALCODE",jsonResult);
			long end = System.currentTimeMillis();
			logger.info("--syncGeneralCode--同步码表信息同步结束, 处理数据:({})条, 正常处理:({})条, 异常:({})条, 总耗时:[{}]ms", (index + error), index, error, (end -start));
		}catch (Exception e) {
			if(jedis != null){
				this.redis.returnBrokenResource(jedis);
			}
			logger.error("获取码表数据失败：",e);
		}finally{
			if(jedis != null){
				this.redis.returnJedisConnection(jedis);
			}
			try{
				if(oraclestmtrsetCode != null){
					oraclestmtrsetCode.close();
				}
				if(oraclestmtrsetAreal != null){
					oraclestmtrsetAreal.close();
				}
				if(oraclestmt != null){
					oraclestmt.close();
				}
				if(oraclecon != null){
					oraclecon.close();
				}
			}catch(Exception ex){
				logger.error("同步码表信息同步异常:"+ ex.getMessage(),ex);
			}
			if(codeList !=null && codeList.size() > 0){
				codeList.clear();
				codeList = null;
			}
			
			if(areaList != null && areaList.size() > 0){
				areaList.clear();
				areaList = null;
			}
		}
	}
	/**
	 * 
	 * 得到区域编码json数据
	 * 
	 */
	private String getAreaInfoJson(List<SysAreaInfoBean> list) {
		if (list.size() == 0)
			return "";
		List<SysAreaInfoBean> rootList = new ArrayList<SysAreaInfoBean>();
		List<SysAreaInfoBean> chileList = new ArrayList<SysAreaInfoBean>();
		try{
			for (int i=0;i<list.size();i++) {
				SysAreaInfoBean info = list.get(i);
				if (info.getAreaLevel() == 0) {
					rootList.add(info);
				} else {
					chileList.add(info);
				}
			} // End for
			return creatAreaInfoJson(rootList, chileList);
		}finally{
			if(rootList != null && rootList.size() > 0){
				rootList.clear();
			}
			if(chileList != null && chileList.size() > 0){
				chileList.clear();
			}
		}
	}
	/**
	 * 创建区域编码json
	 * @param rootList
	 * @param chileList
	 * @return
	 */
	private String creatAreaInfoJson(List<SysAreaInfoBean> rootList, List<SysAreaInfoBean> chileList) {
		StringBuilder sb = new StringBuilder();
		sb.append("'SYS_AREA_INFO':[");
		for (SysAreaInfoBean info : rootList) {
			sb.append("{'code':'").append(info.getAreaCode()).append("',");
			sb.append("'name':'").append(info.getAreaName()).append("',");
			sb.append("children:[");
			String cJson = this.getChileAreaInfoJson(chileList, info.getAreaCode());
			sb.append(cJson).append("]},");
		} // End for
		return sb.substring(0, sb.length() - 1)+"]";
	}
	/**
	 * 
	 * 递归得到省下的市
	 * @param chileList
	 * @param code
	 * @return
	 */
	private String getChileAreaInfoJson(List<SysAreaInfoBean> chileList, String code) {
		StringBuilder sb = new StringBuilder();
		for (int i=0;i<chileList.size();i++) {
			SysAreaInfoBean info = chileList.get(i);
			String rtCode = code.substring(0, 2);
			String ctCode = info.getAreaCode().substring(0, 2);
			if (ctCode.equals(rtCode)){
				sb.append("{'code':'").append(info.getAreaCode()).append("',");;
				sb.append("'name':'").append(info.getAreaName()).append("'},");
			}
		} // End for
		
		if(sb != null && !"".equals(sb)&& sb.length()>0){
			return sb.substring(0, sb.length() - 1);
		}else{
			return "";
		}
	}
/********************************************************************************************/
	/**
	 * 得到通用编码json 数据
	 */
	private String getGeneralCodeJson(List<SysGeneralCodeBean> list) {
		if (list.size() == 0)
			return "";
		List<SysGeneralCodeBean> rootList = new ArrayList<SysGeneralCodeBean>();
		List<SysGeneralCodeBean> chileList = new ArrayList<SysGeneralCodeBean>();
		try{
			for (int i=0;i<list.size();i++) {
				SysGeneralCodeBean info =list.get(i);
				if (info.getParentGeneralCode().equals("0")) {
					rootList.add(info);
				} else {
					chileList.add(info);
				}
			}// End for
	
			return creatGeneralCodeJson(rootList, chileList);
		}finally{
			if(rootList != null && rootList.size() > 0){
				rootList.clear();
				rootList = null;
			}
			
			if(chileList != null && chileList.size() > 0){
				chileList.clear();
				chileList = null;
			}
		}
	}
	/**
	 * 创建通用编码 json
	 * @param rootList
	 * @param chileList
	 * @return
	 */
	private String creatGeneralCodeJson(List<SysGeneralCodeBean> rootList, List<SysGeneralCodeBean> chileList) {
		StringBuilder sb = new StringBuilder();
		for (SysGeneralCodeBean info : rootList) {
			sb.append("'").append(info.getGeneralCode()).append("':[");
			String cJson = getChileGeneralCodeJson(chileList, info.getGeneralCode());
			sb.append(cJson).append("],");
		}// End for
		return sb.substring(0, sb.length() - 1);
	}
	
	/**
	 * 递归得到通用编码子类型
	 * @param chileList
	 * @param code
	 * @return
	 */
	private String getChileGeneralCodeJson(List<SysGeneralCodeBean> chileList, String code) {
		StringBuilder sb = new StringBuilder();
		for (int i=0;i<chileList.size();i++) {
			SysGeneralCodeBean info = chileList.get(i);
			String pCode = info.getParentGeneralCode();
			if (pCode.equals(code)){
				sb.append("{'code':'").append(info.getGeneralCode()).append("',");;
				sb.append("'name':'").append(info.getCodeName()).append("'},");
			}
		}// End for
		if(sb != null && !"".equals(sb)&& sb.length()>0){
			return sb.substring(0, sb.length() - 1);
		}else{
			return "";
		}
	}
}
