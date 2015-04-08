package com.ctfo.dataanalysisservice.service;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.HashMap;
import java.util.Map;

import org.apache.log4j.Logger;

import com.ctfo.dataanalysisservice.Constant;
import com.ctfo.dataanalysisservice.beans.AlarmVehicleBean;
import com.ctfo.dataanalysisservice.beans.Message;
import com.ctfo.dataanalysisservice.beans.PlatAlarmTypeUtil;
import com.ctfo.dataanalysisservice.beans.VehicleMessage;
import com.ctfo.dataanalysisservice.mem.MemManager;
import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;

/**
 * 通用报文解析类
 * 
 * 
 */
public class AlarmAnalyseService implements IAnalyseService {

	private static final Logger logger = Logger
			.getLogger(AlarmAnalyseService.class);

	// private static Map<String,Long> map=new HashMap<String,Long>();
	/**
	 * 监控处理报文
	 * 
	 */
	public VehicleMessage dealPacket(Message messagecommand) {

		try {
			// 原始指令字符串
			String command = null;
			// msgid 标明是那个msg发送的指令
			String msgid = null;
			// 原始命令 拆封指令字符串
			String[] messageArray = null;
			// 命令解析对象
			VehicleMessage vehicle = null;
			// 解析包头
			String head = null;
			// 业务序列号
			String seq = null;
			// 通讯码
			String macId = null;
			// 通道
			String channel = null;
			// 指令字
			String mtype = null;
			// 指令参数
			String content = null;
			String usecontent = null;

			String[] macIdArray = null;
			// 车机类型码
			String oemCode = null;
			// 终端标识码(手机号)
			String commanddr = null;
			// 手机号 关联 vid
			AlarmVehicleBean alarmBean = null;

			if (messagecommand != null) {

				command = messagecommand.getCommand();
				msgid = messagecommand.getMsgid();

				//logger.info("command" + messagecommand.getCommand());

				// 原始命令 拆封指令字符串 "\\s+"
				messageArray = command.split(Constant.COMMAND_SPLIT);

				// 非业务包 屏蔽
				if (messageArray.length < 6) {
					return null;
				}

				// 取得命令头
				head = messageArray[0];
				seq = messageArray[1];// 业务序列号
				macId = messageArray[2];// 通讯码
				channel = messageArray[3];// 通道
				mtype = messageArray[4];// 指令字
				content = messageArray[5];// 指令参数

				// CAITS 0_0 4C54_15000026503 0 U_REPT
				// {TYPE:0,RET:0,1:69818319,2:23974476,20:0,215:68,24:100,3:60,4:20120214/214324,5:0,509:68,510:18,511:118,6:100,7:800,8:0,9:2000}

				// 命令头为CAITS的数据 才为平台软报警所需数据 （上下线信息 永锋确认忽略）
				if ((head == null || !head
						.equals(Constant.COMMAND_HEAD_ISALARM))) {
					return null;
				}

				if (content != null) {
					usecontent = content.substring(1, content.length() - 1);
				}

				// 只处理位置上报数据
				if (!Constant.COMMAND_MTYPE_POSITION.equals(mtype)) {
					return null;
				}
				String[] parm = usecontent.split(",");
				String[] tempKV = null;
				Map<String, String> stateKV = new HashMap<String, String>();// 状态键值
				for (int i = 0; i < parm.length; i++) {
					tempKV = parm[i].split(":", 2);
					if (tempKV.length == 2)
						stateKV.put(tempKV[0], tempKV[1]);
				}
				if ("0".equals(stateKV.get("TYPE"))
						|| "1".equals(stateKV.get("TYPE"))){
				}else{
					return null;
				}
				macIdArray = macId.split("_");

				if (macIdArray != null && macIdArray.length == 2) {

					oemCode = macIdArray[0];// 车机类型码
					commanddr = macIdArray[1]; // 终端标识码
					// 模拟数据 手机号 从 15000000001 15000050000
				}
				// {TYPE:0-100,k1:v1,k2:v2...}
				//logger.info("commanddr" + commanddr);

				// 缓存 根据手机号取得vid
				// 不能取得数据 说明本数据所属的车辆不存在平台软报警

				String key = PlatAlarmTypeUtil.KEY_WORD + "_" + commanddr;
				alarmBean = MemManager.getAlarmVehicleMap(key);

				// System.out.println("---------" + );
				// if(map.containsKey(commanddr)){ 测试暂时注释
				if (alarmBean != null) {
					logger.info("vid" + alarmBean.getVid());
					// 测试只接受1000个车机的数据
					// if(Long.valueOf(commanddr)<15000002001L){
					vehicle = new VehicleMessage();

					// Object o=map.get(commanddr);
					// ------------测试数据-------------- 0:围栏（进出报警和超速） 1：关键点（到达和离开
					// 没有超速） 2：线路（偏移和分段限速）
					vehicle.setAlarmType(alarmBean.getAlarmType());
					// vehicle.setAlarmType("com.ctfo.dataanalysisservice.addin.AreaAlarmAddIn,com.ctfo.dataanalysisservice.addin.LineAlarmAddIn,com.ctfo.dataanalysisservice.addin.KeyPointAlarmAddIn");
					// String ss=System.currentTimeMillis()+"";
					// String vid=ss.substring(ss.length()-3);
					// String vid=ss.substring(ss.length()-3);
					// vehicle.setVid(new Long(vid));
					vehicle.setVid(Long.valueOf(alarmBean.getVid()));
					// vehicle.setVid(Long.valueOf(commanddr)); //测试 vid使用手机号
					// --------------------------
					vehicle.setOemCode(oemCode);
					vehicle.setCommanddr(commanddr);
					// 1 lon 经度(度的600000倍)
					// 2 lat 纬度(度的600000倍)
					// 3 速度(km/h)
					// 4 时间(yyyymmdd/hhmmss)
					// 5 方向(度)
					// 6 海拔(米)
					// 7 行驶记录仪速度(km/h)

					// 上报的为轨迹包/位置状态包 才解析数据
					/*
					 * if(stateKV!=null && stateKV.get("TYPE")!=null &&
					 * stateKV.get("TYPE").equals("0")){
					 * vehicle.setLon(Long.valueOf(stateKV.get("1")));
					 * vehicle.setLat(Long.valueOf(stateKV.get("2")));
					 * vehicle.setSpeed(Long.valueOf(stateKV.get("3")));
					 * vehicle.setUtc(formatStrDate2Utc(stateKV.get("4")));
					 * BussinesDistributeThreadPool.addCount(); }else{ return
					 * null; }
					 */
					long lon=Long.valueOf(stateKV.get("1") == null ? "0"
							: stateKV.get("1"));
					long lat=Long.valueOf(stateKV.get("2") == null ? "0"
							: stateKV.get("2"));
					vehicle.setLon(lon);
					vehicle.setLat(lat);
					vehicle.setSpeed(Long.valueOf(stateKV.get("3") == null ? "0"
							: stateKV.get("3")));
					vehicle.setUtc(formatStrDate2Utc(stateKV.get("4") == null ? "0"
							: stateKV.get("4")));
					
					// 偏移
					Converter conver = new Converter();
					Point point = conver.getEncryPoint(lon / 600000.0, lat / 600000.0);
					if (point != null) {
						long maplon = Math.round(point.getX() * 600000);
						long maplat = Math.round(point.getY() * 600000);
						vehicle.setMaplon(maplon);
						vehicle.setMaplat(maplat);
					}
					
					return vehicle;
				} else {
					return null;
				}

				// logger.info("Utc"+vehicle.getUtc());
				// logger.info("Lon"+vehicle.getLon());
				// logger.info("Lat"+vehicle.getLat());
				// logger.info("Speed"+vehicle.getSpeed());
				// logger.info("累计轨迹上报数据 "+BussinesDistributeThreadPool.getCount());
				// 测试数据
				/*
				 * vehicle=new VehicleMessage(); String
				 * ss=System.currentTimeMillis()+""; String
				 * vid=ss.substring(ss.length()-3); vehicle.setVid(new
				 * Long(vid));
				 */

			} else {

				return null;
			}
		} catch (Exception e) {
			logger.error("协议解析错误：", e);
			return null;
		}

	}

	/**
	 * 格式化时间2UTC
	 * 
	 * @param dateStr
	 * @return
	 * @throws ParseException
	 */
	private Long formatStrDate2Utc(String dateStr) throws ParseException {
		// logger.info("dateStr"+dateStr);
		Long utc = null;
		SimpleDateFormat ssxf = null;
		Calendar calendarf = null;

		if (dateStr != null) {
			 ssxf = new SimpleDateFormat("yyyyMMdd/HHmmss");
			 calendarf = Calendar.getInstance();
			 calendarf.setTime(ssxf.parse(dateStr));
			//utc = System.currentTimeMillis();
			 utc =calendarf.getTimeInMillis();
		}
		return utc;
	}

	public static void main(String arg[]) {

	}
}
