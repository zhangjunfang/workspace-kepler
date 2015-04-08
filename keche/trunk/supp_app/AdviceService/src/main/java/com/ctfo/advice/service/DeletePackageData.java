package com.ctfo.advice.service;

import com.ctfo.advice.model.MqEntity;
import com.ctfo.advice.util.Constant;
import com.ctfo.advice.util.Tools;

/**
 * 提供对删除数据的统一格式
 * @author yinshuaiwei
 *
 */
public class DeletePackageData implements PackageService {
	private MqEntity mqEntity;
	
	public DeletePackageData(MqEntity parseGetMqBean){
		this.mqEntity = parseGetMqBean;
	}

	@Override
	public String packageData() {
		return mqEntity.getOperateType() + Constant.CONSTANT_SPLIT_ATTRIBUTE + 
				Tools.strToBase64("{" + mqEntity.getBusinessKey() + Constant.CONSTANT_SPLIT_ATTRIBUTE 
						+ mqEntity.getValue() + Constant.CONSTANT_SPLIT_OVER + "}");
	}
}
