/**
 * 2014-5-22OracleService.java
 */
package com.ctfo.advice.service;

import org.apache.commons.lang3.StringUtils;

import com.ctfo.advice.model.MqEntity;



/**
 * 文件名：数据库业务处理逻辑类（通过MQ通知类型|信息id获取数据）
 * 功能：
 *
 * @author hjc
 * 2014-8-12下午3:54:25
 * 
 */
public class OracleService {
	private MqEntity parseBean = null;
	
	public OracleService(){
		
	}

	
	/**
	 * 根据MQ通知判断那种信息类型
	 * "ADD:VID:" + tbVehicle.getVid();
	 * "UPDATA:VID:" + tbVehicle.getVid();
	 * "DELETE:VID:" + vid;
	 * "BULKADD:VID: ";
	 * 
	 * "ADD:TB_ID:" + alarmNotice.getTbId();
	 * 
	 * "DELETE:LINE_ID:" + tbClassLine.getLineId();
	 */
	private void infoMQType(String message){
		String msg[] = StringUtils.splitPreserveAllTokens(message, ":");
		parseBean = new MqEntity(msg[0],msg[1],msg[2]);
	}
	
	private String select(String operateType) throws Exception{
		PackageService packageService = null;
		if(MqEntity.OPERATER_TYPE_ADD.equalsIgnoreCase(operateType) || MqEntity.OPERATER_TYPE_UPDATA.equalsIgnoreCase(operateType)){
			packageService = new UpdatePackageData(parseBean);
			return packageService.packageData();
		}else if(MqEntity.OPERATER_TYPE_DELETE.equalsIgnoreCase(operateType)){
			packageService = new DeletePackageData(parseBean);
			return packageService.packageData();
		}else{
			throw new Exception("输入了不支持的操作类型：" + operateType);
		}
	}
	
	/**
	 * 处理来自mq的消息，并将消息按指定的格式输出
	 * @param message
	 * @return
	 * @throws Exception
	 */
	public String processMessage(String message) throws Exception{
		infoMQType(message);
		return select(parseBean.getOperateType());
	}
}
