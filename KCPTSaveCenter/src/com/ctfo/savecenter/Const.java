/*****************************************
 * <ul>
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.savecenter Const.java	</li><br>
 * <li>时        间：2013-7-10  下午2:10:28	</li><br>		
 * </ul>
 *****************************************/
package com.ctfo.savecenter;

/*****************************************
 * <ul>
 * <li>说        明：常量工具类  <br> 程序中尽量使用常量包进行字符串拼接，减少创建内存空间以及降低垃圾回收的频率
 * <li>创  建  者：hushaung 		</li><br>
 * <li>工程名称： storage		</li><br>
 * <li>文件名称：com.ctfo.savecenter Const.java	</li><br>
 * <li>时        间：2013-7-10  下午2:10:28	</li><br>
 * </ul>
 *****************************************/
public class Const {
	/** ----------------数字-----------------------  */
	public static final Long L_ZERO = 0l;
	
	/** ----------------字符数字-----------------------  */
	/** 轨迹包/位置状态包   &  '0'  */
	public static final String P0 = "0";
	/** 经度   & 报警包   & '1'  */
	public static final String P1 = "1";
	/** 纬度  & 记录仪数据  & '2'  */
	public static final String P2 = "2";
	/** GPS速度(km/h)  & 多媒体数据上传  & '3'  */
	public static final String P3 = "3";
	/** 普通短信  & 时间(yyyymmdd/hhmmss)   & '4'  */
	public static final String P4 = "4";
	/** 方向(度)  & 上下线通知  &  '5'  */
	public static final String P5 = "5";
	/** 计价器数据   & 海拔(米) &  '6'  */
	public static final String P6 = "6";
	/** 盲区补传   &  行驶记录仪速度(km/h) &  '7'  */
	public static final String P7 = "7";
	/** 司机身份数据   & 基本信息状态位  &  '8'  */
	public static final String P8 = "8";
	/** 透明传输   & 里程，1/10km & '9'  */
	public static final String P9 = "9";
	/** 求助    &  '10'  */
	public static final String P10 = "10";
	/** 上线补传   &  '11'  */
	public static final String P11 = "11";
	/** 告警确认包   &  '12'  */
	public static final String P12 = "12";
	/** 第三方数据包   &  '13'  */
	public static final String P13 = "13";
	/** 数据压缩上传   &  '14'  */
	public static final String P14 = "14";
	/** 发动机故障告警   & GPS是否有效  &  '15'  */
	public static final String P15 = "15";
	/** 故障诊断处理   & 触发抓拍事件  &  '16'  */
	public static final String P16 = "16";
	/** 普通上行短信   &  '17'  */
	public static final String P17 = "17";
	/** 设备上下线   &  '18'  */
	public static final String P18 = "18";
	/** 车机状态   &  '19'  */
	public static final String P19 = "19";
	/** 基础报警位   &  '20'  */
	public static final String P20 = "20";
	/** 扩展报警位  &  '21'  */
	public static final String P21 = "21";
	/** 通讯流量(KB)   &  '22'  */
	public static final String P22 = "22";
	/** 温度(.C)   &  '23'  */
	public static final String P23 = "23";
	/** 油箱存油量(升)   &  '24'  */
	public static final String P24 = "24";
	/** 水泥罐车状态   &  '25'  */
	public static final String P25 = "25";
	/** 车辆外设状态   &  '26'  */
	public static final String P26 = "26";
	/** 求助   &  '27'  */
	public static final String P27 = "27";
	/** 身份识别（0成功 1失败）   &  '28'  */
	public static final String P28 = "28";
	/** 认证类型（0中心 1本地）  &  '29'  */
	public static final String P29 = "29";
	/** 黑匣子数据   &  '30'  */
	public static final String P30 = "30";
	/** 事件报告(预警)   & 超速报警  &  '31'  */
	public static final String P31 = "31";	
	/** 提问应答  & 进出区域/路段报警附加信息类型 & '32'  */
	public static final String P32 = "32";	
	/** 信息点播/取消  &  短消息内容 & '33'  */
	public static final String P33 = "33";	
	/** 周边信息查询   & 告警确认包  & '34'  */
	public static final String P34 = "34";	
	/** 电子路单   &  路线行驶时间不足/过长  & '35'  */
	public static final String P35 = "35";	
	/** 终端注册  & 第三方数据包 &  '36'  */
	public static final String P36 = "36";	
	/** 终端注销   &  '37'  */
	public static final String P37 = "37";	
	/** 终端鉴权   &  '38'  */
	public static final String P38 = "38";	
	/** 多媒体事件上传   &  '39'  */
	public static final String P39 = "39";	
	/** 多媒体进度通知   & 省域ID & '40'  */
	public static final String P40 = "40";
	/**  发动机超转告警  & '47'  */
	public static final String P47 = "47";
	
	/** 车辆分析数据上传   &  空重车  & '50'  */
	public static final String P50 = "50";	
	/**历史数据上传   &  起点时间  & '51'  */
	public static final String P51 = "51";	
	/** 驾驶行为事件上传   & 终点时间 & '52'  */
	public static final String P52 = "52";	
	/** 终端版本信息上传   &  乘车时间  & '53'  */
	public static final String P53 = "53";	
	/** 怠速信息上传   & 上车时间  &  '54'  */
	public static final String P54 = "54";	
	/** 下车时间  &  '55'  */
	public static final String P55 = "55";	
	/** CAN 总线数据上传   &  '30'  */
	public static final String P60 = "60";	

	
	/** 发动机最大转速   &  '210'  */
	public static final String P210 = "210";
	
	/** -----------------告警编号--------以A开头---------------  */
	/** 发动机超转告警   &  '47'  */
	public static final String A47 = ",47,";
	

	/** -----------------符号-----------------------  */
	
	/**  空格      */
	public static final String SPACES = "";
	/**  逗号      */
	public static final String COMMA = ",";
	/**  句号      */
	public static final String PERIOD = ",";
	/** 下划线      */
	public static final String UNDERLINE = "_";
	/** 冒号      */
	public static final String COLON = ":";
	
	/** -----------------业务符号-----------------------  */
	/** 接收      */
	public static final String RECEIVED = "received";
	/** 解析队列      */
	public static final String PARSE = "parse";
	/** 指令队列      */
	public static final String INSTRUCTION = "instruction";
	/** 多媒体队列      */
	public static final String COMMAND = "command";
	/** 插件队列      */
	public static final String PLUG = "plug";
	/** 轨迹主队列      */
	public static final String TRACKMAIN = "trackmain";
	/** 轨迹队列      */
	public static final String TRACK = "track";
	/** 处理队列      */
	public static final String ANALYSIS = "analysis";
	/** 报警队列      */
	public static final String ALARM = "alarm";
	/** 线路队列      */
	public static final String LINE = "line";
	/** 文件队列      */
	public static final String FILE = "file";
	/** 上下线队列      */
	public static final String ONOFFLINE = "onoffline";
	/** 最后位置队列      */
	public static final String LASTTRACK = "lasttrack";
	/** redis队列      */
	public static final String REDIS = "redis";
	
	/** 设备状态队列      */
	public static final String EQUIPMENT = "equipment";
	/** -----------------文件-----------------------  */
	
	public static final String WRAP= "\r\n";
	
	/** -----------------时间-----------------------  */
	/** 企业告警更新时间 30分钟      */
	public static final Long ORG_ALARM_UPDATE_TIME = 1800000l;
	
	
}
