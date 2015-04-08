package com.ctfo.analy.activemq;

import org.apache.log4j.Logger;

import com.lingtu.xmlconf.XmlConf;
/**
 * 初始化activemq客户端，每个通道启动一个守护线程
 * @author yujch
 *
 */
public class ActivemqInit {
	private static final Logger logger = Logger.getLogger(ActivemqInit.class);
	private Thread alarmActiveMq = null;
	private XmlConf conf;
	public ActivemqInit(XmlConf config){
		this.conf = config;
	}
	
	public void init(){
		try{
		String mqUrl =conf.getStringValue("activeMQ|mqUrl");
		
		//启动MQ - 车辆频道
		String t_vehicle = conf.getStringValue("activeMQ|t_vehicle");
		alarmActiveMq = new VehicleActiveMQ(mqUrl,t_vehicle,conf);
		alarmActiveMq.start();
		
		//启动MQ - SIM频道
		String t_sim = conf.getStringValue("activeMQ|t_sim");
		alarmActiveMq = new SimActiveMQ(mqUrl,t_sim,conf);
		alarmActiveMq.start();
		
		//启动MQ - 组织频道
		String t_org = conf.getStringValue("activeMQ|t_org");
		alarmActiveMq = new OrgActiveMQ(mqUrl,t_org,conf);
		alarmActiveMq.start();
		
		//启动MQ - 关系频道
		String t_serviceunit = conf.getStringValue("activeMQ|t_serviceunit");
		alarmActiveMq = new ServiceUnitActiveMQ(mqUrl,t_serviceunit,conf);
		alarmActiveMq.start();
		
		//启动MQ - 车辆区间频道
		String t_vehicle_sectioncfg = conf.getStringValue("activeMQ|t_vehicle_sectioncfg");
		alarmActiveMq = new SectioncfgActiveMQ(mqUrl,t_vehicle_sectioncfg,conf);
		alarmActiveMq.start();
		
		//启动MQ - 区间限速频道
		String t_section_speedlimit = conf.getStringValue("activeMQ|t_section_speedlimit");
		alarmActiveMq = new SpeedlimitActiveMQ(mqUrl,t_section_speedlimit,conf);
		alarmActiveMq.start();
		
		//启动MQ - 车辆区域频道
		String t_vehicle_area = conf.getStringValue("activeMQ|t_vehicle_area");
		alarmActiveMq = new VehicleAreaActiveMQ(mqUrl,t_vehicle_area,conf);
		alarmActiveMq.start();
		
		//启动MQ - 区域绑定频道
		String t_bind_area = conf.getStringValue("activeMQ|t_bind_area");
		Thread alarmActiveMq = new BindAreaActiveMQ(mqUrl,t_bind_area,conf);
		alarmActiveMq.start();
		
		//启动MQ - 区域关系频道
		String t_tr_area = conf.getStringValue("activeMQ|t_tr_area");
		alarmActiveMq = new TrAreaActiveMQ(mqUrl,t_tr_area,conf);
		alarmActiveMq.start();
		
		//启动MQ - 区域频道
		String t_area = conf.getStringValue("activeMQ|t_area");
		alarmActiveMq = new AreaActiveMQ(mqUrl,t_area,conf);
		alarmActiveMq.start();
		
		//启动MQ - 线路频道
		String t_line_prop = conf.getStringValue("activeMQ|t_line_prop");
		alarmActiveMq = new LinePropActiveMQ(mqUrl,t_line_prop,conf);
		alarmActiveMq.start();
		
		//启动MQ - 站点频道
		String t_station = conf.getStringValue("activeMQ|t_station");
		alarmActiveMq = new StationActiveMQ(mqUrl,t_station,conf);
		alarmActiveMq.start();
		
		//启动MQ - 车辆线路频道
		String t_line_vehicle = conf.getStringValue("activeMQ|t_line_vehicle");
		alarmActiveMq = new VehicleLineActiveMQ(mqUrl,t_line_vehicle,conf);
		alarmActiveMq.start();
		
		//启动MQ - 线路类型频道
		String t_class_line = conf.getStringValue("activeMQ|t_class_line");
		alarmActiveMq = new ClassLineActiveMQ(mqUrl,t_class_line,conf);
		alarmActiveMq.start();
		
		//启动MQ - 非法运营频道
		String t_illegeal_operation_settime = conf.getStringValue("activeMQ|t_illegeal_operation_settime");
		alarmActiveMq = new IlleOptActiveMQ(mqUrl,t_illegeal_operation_settime,conf);
		alarmActiveMq.start();
		
		//启动MQ - 告警提示频道
		String t_alarm_notice = conf.getStringValue("activeMQ|t_alarm_notice");
		alarmActiveMq = new AlarmNoticeActiveMQ(mqUrl,t_alarm_notice,conf);
		alarmActiveMq.start();
		
		//启动MQ - 终端频道
		String t_terminal = conf.getStringValue("activeMQ|t_terminal");
		alarmActiveMq = new TerminalActiveMQ(mqUrl,t_terminal,conf);
		alarmActiveMq.start();
		
		//启动MQ - 终端厂商频道
		String t_terminal_protocol = conf.getStringValue("activeMQ|t_terminal_protocol");
		alarmActiveMq = new TerminalProtocolActiveMQ(mqUrl,t_terminal_protocol,conf);
		alarmActiveMq.start();
		
		//启动MQ - 终端协议频道
		String t_terminal_oem = conf.getStringValue("activeMQ|t_terminal_oem");
		alarmActiveMq = new TerminalOemActiveMQ(mqUrl,t_terminal_oem,conf);
		alarmActiveMq.start();
		
		//启动MQ - 线路站点信息
		String t_line_station = conf.getStringValue("activeMQ|t_line_station");
		alarmActiveMq = new TrLineStationActiveMQ(mqUrl,t_line_station,conf);
		alarmActiveMq.start();
		
		logger.info("activeMq 客户端守护线程启动完成!");
		}catch(Exception ex){
			logger.error("activeMq 客户端守护线程启动失败!",ex);
		}
	}

}
