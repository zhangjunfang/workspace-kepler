package com.ctfo.baseinfo.service.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ctfo.baseinfo.dao.SysGeneralCodeDao;
import com.ctfo.baseinfo.service.SysGeneralCodeService;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.local.obj.PaginationResult;
import com.ctfo.sys.beans.SysAreaInfo;

@Service
public class SysGeneralCodeServiceImpl implements SysGeneralCodeService{

	@Autowired
	SysGeneralCodeDao sysGeneralCodeDao;
	
    /**
     * 区域编码Map
     * sysAreaInfoExpMap 导出
     * sysAreaInfoImpMap 导入
     * author liweijie
     */
    public final static Map<String,String>  sysAreaInfoExpMap = new HashMap<String, String>();
    public final static Map<String,String>  sysAreaInfoImpMap = new HashMap<String, String>();
	
	@Override
	public String findSysGeneralCodeByCode(DynamicSqlParameter params) throws CtfoAppException {
		// TODO Auto-generated method stub
		String jsonResult = "{";
		try{
		params = new DynamicSqlParameter();
		List<Integer> list = new ArrayList<Integer>();
		list.add(0);
		list.add(1);
		list.add(2);
		Map<String,List> areaMap = new HashMap<String, List>();
		areaMap.put("areaLevel", list);
		params.setInMap(areaMap);
		PaginationResult areaInfoList = sysGeneralCodeDao.selectPagination(params);
		if(areaInfoList != null  && areaInfoList.getData() !=null){
			List<SysAreaInfo> listArea = (List<SysAreaInfo>)areaInfoList.getData();
			jsonResult += this.getAreaInfoJson(listArea);
			this.getInitAreaInfoToMap(listArea);//初始化区域信息到Map
		}
		}catch (Exception e) {
			System.out.println(e);
		}
		jsonResult +="}";
		return jsonResult;
	}

	/**
	 * 
	 * 得到区域编码json数据
	 * 
	 */
	private String getAreaInfoJson(List<SysAreaInfo> list) {
		if (list.size() == 0)
			return "";
		List<SysAreaInfo> rootList = new ArrayList<SysAreaInfo>();
		List<SysAreaInfo> chileList = new ArrayList<SysAreaInfo>();
		List<SysAreaInfo> chileChileList = new ArrayList<SysAreaInfo>();
		for (int i=0;i<list.size();i++) {
			SysAreaInfo info = list.get(i);
			if (info.getAreaLevel() == 0) {
				rootList.add(info);
			} else if(info.getAreaLevel() == 1) {
				chileList.add(info);
			} else{
				chileChileList.add(info);
			}
		}
		return this.creatAreaInfoJson(rootList, chileList, chileChileList);
	}
	/**
	 * 初始化区域编码到Map
	 * 导入导出时区域转码
	 * @param infoList
	 * @return
	 */
	private void getInitAreaInfoToMap(List<SysAreaInfo> infoList) {
		for(SysAreaInfo info :infoList){
			this.sysAreaInfoExpMap.put(info.getAreaCode(), info.getAreaName());
			this.sysAreaInfoImpMap.put(info.getAreaName(), info.getAreaCode());
		}
	}
	/**
	 * 创建区域编码json
	 * @param rootList
	 * @param chileList
	 * @return
	 */
	private String creatAreaInfoJson(List<SysAreaInfo> rootList, List<SysAreaInfo> chileList, List<SysAreaInfo> chileChileList) {
		StringBuilder sb = new StringBuilder();
		sb.append("'SYS_AREA_INFO':[");
		for (SysAreaInfo info : rootList) {
			sb.append("{'code':'").append(info.getAreaCode()).append("',");
			sb.append("'name':'").append(info.getAreaName()).append("',");
			sb.append("children:[");
			String cJson = this.getChileAreaInfoJson(chileList,chileChileList, info.getAreaCode());
			sb.append(cJson).append("]},");
		}
		return sb.substring(0, sb.length() - 1)+"]";
	}
	
	private String getChileAreaInfoJson(List<SysAreaInfo> chileList,List<SysAreaInfo> chileChileList, String code) {
		StringBuilder sb = new StringBuilder();
		for (int i=0;i<chileList.size();i++) {
			SysAreaInfo info = chileList.get(i);
			String rtCode = code.substring(0, 2);
			String ctCode = info.getAreaCode().substring(0, 2);
			if (ctCode.equals(rtCode)){
				sb.append("{'code':'").append(info.getAreaCode()).append("',");;
				sb.append("'name':'").append(info.getAreaName()).append("',");
				sb.append("children:[");
				String cJson = this.getChileChileAreaInfoJson(chileChileList, info.getAreaCode());
				sb.append(cJson).append("]},");
			}
		}
		if(sb != null && !"".equals(sb)&& sb.length()>0){
			return sb.substring(0, sb.length() - 1);
			}else{
				return "";
		}
	}
	/**
	 * 
	 * 递归得到市下县
	 * @param chileList
	 * @param code
	 * @return
	 */
	private String getChileChileAreaInfoJson(List<SysAreaInfo> chileChileList, String code) {
		StringBuilder sb = new StringBuilder();
		for (int i=0;i<chileChileList.size();i++) {
			SysAreaInfo info = chileChileList.get(i);
			String rtCode = code.substring(0, 4);
			String ctCode = info.getAreaCode().substring(0, 4);
			if (ctCode.equals(rtCode)){
				sb.append("{'code':'").append(info.getAreaCode()).append("',");;
				sb.append("'name':'").append(info.getAreaName()).append("'},");
			}
		}
		if(sb != null && !"".equals(sb)&& sb.length()>0){
			return sb.substring(0, sb.length() - 1);
			}else{
				return "";
		}
	}
}
