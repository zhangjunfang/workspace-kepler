/**
 * 2014-6-4ProtocolAnaly.java
 */
package com.ctfo.storage.command.service;

import com.ctfo.storage.command.model.AuthModel;
import com.ctfo.storage.command.model.LogoutModel;
import com.ctfo.storage.command.model.OnOffLineModel;
import com.ctfo.storage.command.model.RegisterModel;
import com.ctfo.storage.command.util.Tools;


/**
 * ProtocolAnaly
 * 
 * 
 * @author huangjincheng
 * 2014-6-4下午02:27:03
 * 
 */
public class ProtocolAnaly {
	
	
	private MySqlService mySqlService = new MySqlService();
	/**
	 * 上下线解析
	 * @param message
	 * @return
	 */
	public OnOffLineModel getOnOffLineModel(String message){
		OnOffLineModel olm = new OnOffLineModel();
		String msgBody = message.substring(38,message.length()-4);
		String phoneNo = Tools.getASCIIByHex(msgBody.substring(4,28));
		String vehicleNo = mySqlService.getVehicleNoByPhone(phoneNo);
		String vid = mySqlService.getVidByPhone(phoneNo);
		olm.setVehicleNo(vehicleNo);
		olm.setVid(vid);
		olm.setCode(Integer.parseInt(msgBody.substring(28,30)));
		olm.setUtc(System.currentTimeMillis());
		
		
		return olm;
	}
	
	
	/**
	 * 
	 * 鉴权解析
	 * @param message
	 * @return
	 */
	public AuthModel getAuthModel(String message){
		AuthModel am = new AuthModel();
		String msgBody = message.substring(38,message.length()-4);
		String phoneNo = Tools.getASCIIByHex(msgBody.substring(4,28));		
		String oemCode = mySqlService.getOemCodeByPhone(phoneNo);
		
		am.setCommaddr(phoneNo);
		am.setResult(Integer.parseInt(msgBody.substring(28,30))+"");
		am.setAkey(Tools.getASCIIByHex(msgBody.substring(30)));
		am.setUtc(System.currentTimeMillis());
		am.setOemCode(oemCode);
		
		
		return am;
	}
	/**
	 * 注销解析
	 * @param message
	 * @return
	 */
	public LogoutModel getLogoutModel(String message){
		LogoutModel lm = new LogoutModel();
		String msgBody = message.substring(38,message.length()-4);
		String phoneNo = Tools.getASCIIByHex(msgBody.substring(4,28));
		String vid =  mySqlService.getVidByPhone(phoneNo);
		String oemCode = mySqlService.getOemCodeByPhone(phoneNo);
		lm.setVid(vid);
		lm.setUtc(System.currentTimeMillis());
		lm.setOemCode(oemCode);
		
		return lm;
	}
	
	/**
	 * 
	 * 注册解析
	 * @param message
	 * @return
	 */
	public RegisterModel getRegisterModel(String message){
		RegisterModel rm = new RegisterModel();
		String msgBody = message.substring(38,message.length()-4);
		
		rm.setCommaddr(Tools.getASCIIByHex(msgBody.substring(4,28)));
		rm.setResult(Integer.parseInt(msgBody.substring(28,30))+"");
		rm.setProvinceId(msgBody.substring(30,34));
		rm.setCityId(msgBody.substring(34,38));
		rm.setManufacturerId(msgBody.substring(38,48));
		rm.setOemCode(Tools.getASCIIByHex(msgBody.substring(48,88)));
		rm.setTid(Tools.getASCIIByHex(msgBody.substring(88,102)));
		rm.setVehicleColor(Integer.parseInt(msgBody.substring(102,104))+"");
		
		
		
		
		return rm;
	}

	
}
