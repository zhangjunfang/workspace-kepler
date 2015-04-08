package com.ctfo.stat.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.ctfo.common.local.exception.CtfoAppException;
import com.ctfo.common.local.obj.DynamicSqlParameter;
import com.ctfo.common.util.DateUtil;
import com.ctfo.stat.dao.TlRepFacActDetailMonthDAO;
import com.ctfo.stat.service.TlRepFacActDetailMonthService;

/**
 * 
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： datacenterApp <br>
 * 功能： 考核指标按月统计<br>
 * 描述： 考核指标按月统计<br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交慧联信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>2014-6-18</td>
 * <td>xuehui</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交慧联信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author xuehui
 * @since JDK1.6
 */
public class TlRepFacActDetailMonthServiceImpl implements TlRepFacActDetailMonthService {

	private static Log log = LogFactory.getLog(TlRepFacActDetailMonthServiceImpl.class);

	/** SIM卡DAO */
	@Autowired
	private TlRepFacActDetailMonthDAO tlRepFacActDetailMonthDAO;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.stat.service.TlRepFacActDetailMonthService#querySumActivityDegreeMonth(com.ctfo.common.local.obj.DynamicSqlParameter)
	 */
	@Override
	public List<Map<String, Object>> querySumActivityDegreeMonth(DynamicSqlParameter param) throws CtfoAppException {
		try {
			Long endDate = DateUtil.stringTimeToUtcTime(DateUtil.getPreMonthLastDay());
			String statisticsMonth = DateUtil.getPreYearMonth();
			if (null == param.getEqual()) { // 参数为空则获取前一月的时间
				Map<String, Object> equal = new HashMap<String, Object>();
				equal.put("endDate", endDate);
				equal.put("statisticsMonth", statisticsMonth);
				param.setEqual(equal);
			} else {
				param.getEqual().put("endDate", endDate);
			}
			Object netVhNum = null;
			Map<String, Object> map = null;
			List<Map<String, Object>> list = tlRepFacActDetailMonthDAO.querySumJoinNetVhMonth(param);
			if (list.size() > 0) {
				map = list.get(0);
				netVhNum = map.get("net_vh_num");
			} else {
				netVhNum = "";
			}
			list = tlRepFacActDetailMonthDAO.querySumActivityDegreeMonth(param);
			if (list.size() > 0) {
				list.get(0).put("net_vh_num", netVhNum);
			} else {
				if (null != map) {
					list.add(map);
				}
			}
			return list;
		} catch (CtfoAppException e) {
			log.error(e.fillInStackTrace());
			throw e;
		}
	}

	public void setTlRepFacActDetailMonthDAO(TlRepFacActDetailMonthDAO tlRepFacActDetailMonthDAO) {
		this.tlRepFacActDetailMonthDAO = tlRepFacActDetailMonthDAO;
	}

}
