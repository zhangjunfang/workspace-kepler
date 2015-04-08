package com.demo.main;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.demo.bean.BuildUpCommand;
import com.demo.bean.Up_InfoContent;
import com.kypt.c2pp.buffer.UpCommandBuffer;
import com.kypt.c2pp.inside.msg.resp.DownTextResp;
import com.kypt.c2pp.inside.msg.resp.LocationReportResp;
import com.kypt.c2pp.util.ValidationUtil.GENERAL_STATUS;

public class UpLocThread extends Thread {

	private static Logger log = LoggerFactory.getLogger(UpLocThread.class);

	int i = 0;

	int j = 0;

	String ym = "201111";

	int day = 14;

	int hours = 0;

	int minutes = 0;

	int second = 0;
	
	int datesecond = 24*60*60*1000;

	java.text.DateFormat df = new java.text.SimpleDateFormat("yyyyMMddHHmmss");

	public UpLocThread() {

	}

	
	@Override
	public void run() {

		while (true) {
//			System.out.println("---------------------------------------------");
			Up_InfoContent udp;
			if (i % 2 == 0) {
				udp = get01(i);
			} else {
				udp = get02(i);
			}
			try {
				LocationReportResp str = BuildUpCommand.getInstance().buildUpCommandString(
						udp);
//				log.info("UP COMMAND--" + udp.getTerminal_time() + ":" + str);
				UpCommandBuffer.getInstance().add(str);

				/*
				 * if (i%6==0){
				 * UpCommandBuffer.getInstance().add(buildDownTextRespString()); }
				 */
				this.sleep(2000);
			} catch (ParseException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			if (i == 200) {
				i = 0;
			}
			i++;

			String hours = "00";
			if (j < 10) {
				hours = "0" + j;
			} else if (j < 24) {
				hours = "" + j;
			} else {
				hours = "00";
				j = 0;
			}

		}
	}

	private Up_InfoContent get01(int alarmType) {
		Up_InfoContent udp = new Up_InfoContent();
		udp.setCellphone("13673561234");
		udp.setLatitude("34.87964532678");
		udp.setLongitude("113.68796543234");
		udp.setGps_valid("1");
		udp.setElevation("34");
		udp.setDirection("50");
		// udp.setTime("20110911231546");
		udp.setMileage("6756");
		udp.setOil_instant("0");
		udp.setOil_total("80");
		udp.setFire_up_state("1");

		if (alarmType > 50) {
			udp.setOverspeed_alert("2");
			udp.setSpeeding("" + (Integer.parseInt("34") + alarmType));
		} else {
			udp.setSos("1");
			udp.setSpeeding("34");
		}
		udp.setE_torque("20");
		udp.setRegion_id("345");
		udp.setGps_speeding("50");
		DateFormat format1 = new SimpleDateFormat("yyMMddHHmmss");
		String dt = format1.format(new Date());
//		System.out.println(dt);
		udp.setTerminal_time(dt);
		return udp;
	}

	private Up_InfoContent get02(int alarmType) {
		Up_InfoContent udp = new Up_InfoContent();
		udp.setCellphone("13709809009");
		udp.setLatitude("35.87964532678");
		udp.setLongitude("114.68796543234");
		udp.setGps_valid("1");
		udp.setElevation("34");
		udp.setDirection("50");
		// udp.setTime("20110913231546");
		udp.setMileage("6756");
		udp.setOil_instant("0");
		udp.setOil_total("80");
		udp.setFire_up_state("1");

		if (alarmType > 50) {
			udp.setOverspeed_alert("2");
			udp.setSpeeding("" + (Integer.parseInt("67") + alarmType));
		} else {
			udp.setSos("1");
			udp.setSpeeding("67");
		}
		udp.setE_torque("20");
		udp.setRegion_id("221");
		udp.setGps_speeding("53");
		DateFormat format1 = new SimpleDateFormat("yyMMddHHmmss");
		String dt = format1.format(new Date());
//		System.out.println(dt);
		udp.setTerminal_time(dt);
		return udp;
	}

	private String buildDownTextRespString() throws ParseException {
		DownTextResp resp = new DownTextResp();
		resp.setSeq("9367_1318423369_128");
		resp.setDeviceNo("13673561234");
		resp.setCommType("0");
		resp.setStatus(GENERAL_STATUS.success);
		return resp.toString();
	}


}
