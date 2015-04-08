package com.ctfo.sectionspeed.thrift.server;

import org.apache.thrift.protocol.TCompactProtocol;
import org.apache.thrift.protocol.TProtocol;
import org.apache.thrift.transport.TSocket;
import org.apache.thrift.transport.TTransport;

import com.ctfo.sectionspeed.thrift.CarForm;
import com.ctfo.sectionspeed.thrift.GPSInfo;
import com.ctfo.sectionspeed.thrift.Point;
import com.ctfo.sectionspeed.thrift.SectionSpeed;

public class Client {
	public static void main(String[] args) {
		TTransport transport = new TSocket("localhost", 3344, 10000);
	//	TFramedTransport tf = new TFramedTransport(transport);
		SectionSpeed.Client client = null;
		try {
			TProtocol protocol = new TCompactProtocol(transport);
			client = new SectionSpeed.Client(protocol);
			transport.open();
			long t = System.currentTimeMillis();
			client.ping();
			Point p = new Point();
			p.setLatitude(0);
			p.setLongitude(1);
			p.setElevation(3);
			client.searchSectionRNet(p);
			GPSInfo ginfo = new GPSInfo();
			ginfo.setAngle(23.3);
			ginfo.setSpeed(123);
			ginfo.setCf(CarForm.COACH);
			client.send_isOverspeed(ginfo);
			transport.close();
		//	client.searchSectionLInfo(null);
			System.out.println((System.currentTimeMillis() - t));
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
