package com.ctfo.mgdbser.analy;


import java.util.HashMap;




import com.ctfo.mgdb.beans.Message;
import com.ctfo.mgdb.beans.Record;
import com.ctfo.mgdb.util.AnalyUtil;

/**
 * 
 * 
 * 解析实现类
 * @author huangjincheng
 *
 *就只有一个集合，LogServer，各字段如下：
字段名称
 类型
 说明
 
S
 字符串型
 来源应用程序的IP
 
A
 整型数据
 应用程序类型，0x01 前置机，0x02 透传服务，0x03 鉴权服务
 
T
 长整型UTC时间
 日志记录时间
 
P
 字符串
 来源车机的手机号
 
B
 整型数据
 类型,1 注册，2鉴权，3 上线，4 下线(TYPE:5,1上线，0下线)
 
E
 整型数据
 是否为错误日志，0 正常，1错误
 
M
 字符串型数据
 日志记录内容，UTF-8的文本
 

 *
 */
public class CommonAnalyseService implements IAnalyseService {
	
	//private static final Logger logger = Logger.getLogger(CommonAnalyseService.class);
	
	public Record dealPacket(Message message) {
		// CAITS 0_1384824304628_6674 E013_13617395958 0 U_REPT {TYPE:0,1:66868924,2:16333838,3:0,4:20131119/092549,5:196,6:2,8:2,20:0}
		Record record  = null;String type = null;HashMap<String, String> p = null;
		String command = message.getCommand();
		record = new Record();
		String[] command_arr = command.split(" ");
		if(message.getEnable_flag() == 0){
			p= AnalyUtil.getJson(command);
			type = getValue("TYPE", p);
		}else {
			type = command_arr[5].substring(6,8);
		}
/*				if("0".equals(type)){
					//基本定位
					record.setIp(message.getIp());
					record.setAppType(01);
					record.setUtcTime(dateToUtc(getValue("4",p)));
					record.setPhoneNum(command_arr[2].substring(5));
					record.setDataType(3);
					record.setEnable_flag(0);
					record.setContent(command);
				}else */
			if("36".equals(type)){
				//注册
				record.setIp(message.getMsgid());
				record.setAppType(1);
				record.setUtcTime(System.currentTimeMillis());
				record.setPhoneNum(command_arr[2].substring(5));
				record.setDataType(1);
				record.setEnable_flag(message.getEnable_flag());
				record.setContent(command);
			}else if("38".equals(type)){
				//鉴权
				record.setIp(message.getMsgid());
				record.setAppType(3);
				record.setUtcTime(System.currentTimeMillis());
				record.setPhoneNum(command_arr[2].substring(5));
				record.setDataType(2);
				record.setEnable_flag(message.getEnable_flag());
				record.setContent(command);
			
			
			}else if("5".equals(type)){
				//上线下线
				String dp_flag = getValue("18", p).substring(0,1);
				if(dp_flag.equals("0")){
					record.setDataType(4);
				}else if(dp_flag.equals("1")){
					record.setDataType(3);
				}
				record.setIp(message.getMsgid());
				record.setAppType(1);
				record.setUtcTime(System.currentTimeMillis());
				record.setPhoneNum(command_arr[2].substring(5));
				record.setEnable_flag(message.getEnable_flag());
				record.setContent(command);
			
			}
		
		return record;
	}
	
	/*private long dateToUtc(String date){
		String str = date.substring(0,4)+"-"+date.substring(4,6)+"-"+date.substring(6,8)+"T"+date.substring(9,11)+":"+date.substring(11,13)+":"+date.substring(13,15)+".000";
		Calendar cal=getStringToCal(str);   
		return cal.getTimeInMillis();
	}
	private  Calendar getStringToCal(String date) {
          final String year = date.substring(0, 4);
          final String month = date.substring(5, 7);
          final String day = date.substring(8, 10);
          final String hour = date.substring(11, 13);
          final String minute = date.substring(14, 16);
          final String second = date.substring(17, 19);
          final int millisecond = Integer.valueOf(date.substring(20, 23));
          Calendar result =
              new GregorianCalendar(Integer.valueOf(year),
                  Integer.valueOf(month) - 1, Integer.valueOf(day),
                  Integer.valueOf(hour), Integer.valueOf(minute),
                  Integer.valueOf(second));
          result.set(Calendar.MILLISECOND, millisecond);
          result.setTimeZone(TimeZone.getTimeZone("Etc/UTC"));
          return result;

    }*/
	 private String getValue(String key, HashMap<String, String> json)
	  {
	    String value = (String)json.get(key);
	    if (value != null) {
	      if (value.indexOf("!") > -1) {
	        value = value.replace("!", ":");
	      }
	      if (value.indexOf("?") > -1) {
	        value = value.replace("?", ",");
	      }
	    }
	    return value != null ? value : "";
	  }

}
