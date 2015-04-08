package com.ctfo.storage.dispatch.service;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.alibaba.fastjson.JSON;
import com.ctfo.storage.dispatch.util.Tools;


/**
 * ProtocolAnalyService
 * 
 * 
 * @author huangjincheng
 * 2014-5-19下午05:10:54
 * 
 */
public class ProtocolAnalyService {
	
	private static final Logger logger = LoggerFactory.getLogger(ProtocolAnalyService.class);
	/**
	 * 
	 * 实现BasicAnaly接口
	 * 输入消息体message和需要得到的className，获取赋值后的实体bean
	 * @param message 消息体全部
	 * @param className bean类名
	 * @return Object
	 * @throws Exception
	 */
	public Object getTableFromControl(String message,String className){
			
		String msgContent = message.substring(2,message.length()-4);//消息头+消息体
		String msgBody = msgContent.substring(36,msgContent.length());//消息体
		String slaveType = msgBody.substring(0,4);
		String masterType = msgContent.substring(16,20);
		String centerSourceIp = Integer.parseInt(msgContent.substring(0,8),16)+"";
		String centerGoalIp = Integer.parseInt(msgContent.substring(8,16),16)+"";
		String messageRuuningNumber = Integer.parseInt(msgContent.substring(20,28),16)+"";
		int messagelength = Integer.parseInt(msgContent.substring(28,36),16);
		int operateType  = Integer.parseInt(msgBody.substring(4,6));
		//得到对象
		Class<?> c = null;
		try {
			c = Class.forName("com.ctfo.storage.dispatch.model."+className);
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		}
		Object obj;
		try {
			obj = c.newInstance();
		} catch (InstantiationException e) {
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			e.printStackTrace();
		}
		//json字符串赋值对象
		String jsonStr = Tools.getChinese(msgBody.substring(6),"gbk");
//		JSONObject jsonobject = JSONObject.fromObject(jsonStr);
		obj = JSON.parseObject(jsonStr,c);
//		obj = JSONObject.toBean(jsonobject, c);
		//得到方法
		/*		Method methlist[] = c.getDeclaredMethods();
		for (int i = 0; i < methlist.length; i++) {
			Method m = methlist[i];
		}*/
		//获取到方法对象
		Method setMasterType = null;
		Method setSlaveType = null;
		Method setCenterSourceIp = null;
		Method setCenterGoalIp = null;
		Method setMessageRuuningNumber = null;
		Method setMessagelength = null;
		Method setOperateType = null;
		try {
			setMasterType = c.getMethod("setMasterType", new Class[] {String.class});
			setSlaveType = c.getMethod("setSlaveType", new Class[] {String.class});
			setCenterSourceIp = c.getMethod("setCenterSourceIp", new Class[] {String.class});
			setCenterGoalIp = c.getMethod("setCenterGoalIp", new Class[] {String.class});
			setMessageRuuningNumber = c.getMethod("setMessageRuuningNumber", new Class[] {String.class});
			setMessagelength = c.getMethod("setMessagelength", new Class[] {int.class});
			setOperateType = c.getMethod("setOperateType", new Class[] {int.class});
		} catch (NoSuchMethodException e) {
			e.printStackTrace();
		} catch (SecurityException e) {
			e.printStackTrace();
		}
		//获得参数Object
		Object[] arguments = new Object[] {masterType,slaveType,centerSourceIp,centerGoalIp,messageRuuningNumber,messagelength,operateType};
		//执行方法
		try {
			setMasterType.invoke(obj , arguments[0]);		
			setSlaveType.invoke(obj , arguments[1]);
			setCenterSourceIp.invoke(obj , arguments[2]);
			setCenterGoalIp.invoke(obj , arguments[3]);
			setMessageRuuningNumber.invoke(obj , arguments[4]);
			setMessagelength.invoke(obj , arguments[5]);
			setOperateType.invoke(obj , arguments[6]);
		} catch (IllegalAccessException e) {
			logger.error(e.toString());
		} catch (IllegalArgumentException e) {
			logger.error(e.toString());
		} catch (InvocationTargetException e) {
			logger.error(e.toString());
		}
		return obj;
	}

	public static void main(String[] args) {
		ProtocolAnalyService p = new ProtocolAnalyService();
		p.getTableFromControl("5B00BC614E05397FB11600003D1A3A000001F51611017B2263656E746572476F616C4970223A22222C2263656E746572536F757263654970223A22222C226368616E6E656C4E756D223A342C2263726561744E616D65223A22222C226372656174654279223A3136343433342C2263726561746554696D65223A313339343037343830323030302C226476724964223A223131303335222C226476724D616B6572436F6465223A22222C226476724E6F223A22E58E9F32333435E7A7BBE69CBA222C226476725365724970223A22222C22647672536572506F7274223A22222C226476727365724964223A2232343731222C226476727365724E616D65223A22222C22656E61626C65466C6167223A2231222C22656E744964223A22313634333833222C22656E744E616D65223A22222C226D616B6572223A22222C226D617374657254797065223A22222C226D6573736167655275756E696E674E756D626572223A22222C226D6573736167656C656E677468223A302C226F70657261746554797065223A302C2270686F6E654E756D223A22222C227175656E654E616D65223A2254425F445652222C22726567537461747573223A2D312C22736C61766554797065223A22222C227570646174654279223A3136343433342C227570646174654E616D65223A22222C2275706461746554696D65223A2231333937303034323839303030227D3D5D", "TbDvr3G");
	}


}
