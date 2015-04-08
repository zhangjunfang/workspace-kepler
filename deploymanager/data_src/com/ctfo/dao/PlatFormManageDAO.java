package com.ctfo.dao;

import java.util.Map;

import org.springframework.stereotype.Service;

import com.ctfo.beans.PlatForm;
import com.ctfo.service.DynamicSqlParameter;
@Service
public interface PlatFormManageDAO extends GenericIbatisDao<PlatForm, Long> {
	
	public Map<String, Object> getPlatFormList(DynamicSqlParameter param) throws Exception;

	public Map<String, Object> getEditPlatForm(DynamicSqlParameter requestParam);

	public int addPlatForm(DynamicSqlParameter requestParam);

	public int delPlatForm(DynamicSqlParameter requestParam);

	public int editPlatForm(DynamicSqlParameter requestParam);

	public int checkPlatExist(DynamicSqlParameter requestParam);
}
