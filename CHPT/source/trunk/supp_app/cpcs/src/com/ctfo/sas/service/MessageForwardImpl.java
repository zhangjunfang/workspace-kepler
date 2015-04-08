package com.ctfo.sas.service;

import java.util.Date;

import javax.jws.WebService;

import org.codehaus.jackson.map.ObjectMapper;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.context.CustomizedPropertyPlaceholderConfigurer;
import com.ctfo.sas.service.bean.Message;
import com.ctfo.sas.service.bean.MessageResponse;
import com.ctfo.storage.util.Base64_URl;
import com.ctfo.storage.util.DateUtil;
import com.ctfo.storage.util.SerialUtil;
import com.ctfo.storage.util.Tools;
import com.ctfo.threedes.ThreeDES;

@WebService(endpointInterface = "com.ctfo.sas.service.MessageForward")
public class MessageForwardImpl implements MessageForward {
	
	private static final Logger logger = LoggerFactory.getLogger(MessageForwardImpl.class);
	
	private RedisService redisService;
	
	public MessageResponse messageForwardYt(Message message) throws Exception {
		
		String returnStatus = "S";
		String returnValue = "";
		String billNumber = message.getBillNumber();
		String billType = message.getBillType();
		String opType = message.getOpType();
		String requestTime = message.getRequestTime();
		String serviceStationSap = message.getServiceStationSap();
		
		//校验字段是否为空
		if(billNumber.equals("")){
			returnStatus = "F";
			returnValue = "billNumber is null";
		}
		if(billType.equals("")){
			returnStatus = "F";
			returnValue = "billType is null";
		}
		if(opType.equals("")){
			returnStatus = "F";
			returnValue = "opType is null";
		}
		if(requestTime.equals("")){
			returnStatus = "F";
			returnValue = "requestTime is null";
		}
		if(serviceStationSap.equals("")){
			returnStatus = "F";
			returnValue = "serviceStationSap is null";
		}
		
		String transferMsgKey = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("transferMsgKey");
		String transferMsgEncoded = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("transferMsgEncoded");
		
		//解密
		Message msg = new Message();
		msg.setBillNumber(ThreeDES.Decrypt3DES(billNumber, transferMsgKey, transferMsgEncoded) );
		msg.setBillType(ThreeDES.Decrypt3DES(billType, transferMsgKey, transferMsgEncoded));
		msg.setOpType(ThreeDES.Decrypt3DES(opType, transferMsgKey, transferMsgEncoded));
		msg.setRequestTime(ThreeDES.Decrypt3DES(requestTime, transferMsgKey, transferMsgEncoded));
		msg.setServiceStationSap(ThreeDES.Decrypt3DES(serviceStationSap, transferMsgKey, transferMsgEncoded));
		
		//转换协议  在存储redis
		transformProtocol(msg);
		
		//宇通接口的响应报文
		MessageResponse messageResponse = new MessageResponse();
		messageResponse.setReturnStatus(ThreeDES.Encrypt3DES(transferMsgKey, returnStatus, transferMsgEncoded));
		messageResponse.setReturnValue(ThreeDES.Encrypt3DES(transferMsgKey, returnValue, transferMsgEncoded) );
		
		return messageResponse;
	}
	
	public void transformProtocol(Message message){
		String serviceStationId = (String)CustomizedPropertyPlaceholderConfigurer.getContextProperty("serviceStationId");
		
		//转json
		String json=null;
		ObjectMapper mapper=null;
		try
		{
		   mapper = new ObjectMapper();
		   json=mapper.writeValueAsString(message);//把map或者是list,object转换成 json
		}catch(Exception e)
		{
		   e.printStackTrace();
		}
		
		logger.info("下发的宇通数据为："+json);
		 
		//从redis 里取服务站ID
		String stationId = redisService.getStationId(message.getServiceStationSap());
		
		StringBuffer msg = new StringBuffer();
		msg.append(serviceStationId).append("$"); //虚拟服务站id
		msg.append(SerialUtil.getInt()).append("$");	//消息流水
		msg.append("Y").append("$");	//消息主类型
		msg.append("$");				//消息子类型
		msg.append(DateUtil.dateToUtcTime(new Date())).append("$");	//时间戳
		msg.append(stationId).append("$"); //服务站id。宇通消息会转发到这个服务站
		msg.append(Base64_URl.base64Encode(json)); //操作对象
		
		//校验码
		String msgString = msg.toString();
		String checkCode = Tools.getCheckCode(msgString);
		
		StringBuffer msg_ = new StringBuffer();
		msg_.append("["); 
		msg_.append(msgString).append("$");
		msg_.append(checkCode);
		msg_.append("]");
		
		//存到redis 里
		redisService.storageRedis("CF_"+stationId, msg_.toString());
		
	}

	public RedisService getRedisService() {
		return redisService;
	}

	public void setRedisService(RedisService redisService) {
		this.redisService = redisService;
	}

}
