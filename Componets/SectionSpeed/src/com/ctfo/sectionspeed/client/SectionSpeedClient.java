package com.ctfo.sectionspeed.client;

import java.util.List;

import org.apache.thrift.TException;

import com.ctfo.sectionspeed.thrift.GPSInfo;
import com.ctfo.sectionspeed.thrift.Point;
import com.ctfo.sectionspeed.thrift.RoadSpeedInfo;
import com.ctfo.sectionspeed.thrift.SectionSpeed;
import com.ctfo.sectionspeed.thrift.SectionSpeedServiceException;

public enum SectionSpeedClient implements SectionSpeed.Iface {
	INSTANCE;
	
	private static final ClientManager manager = ClientManager.INSTANCE;

	@Override
	public boolean isOverspeed(GPSInfo info) throws SectionSpeedServiceException, TException {
		return manager.getClient().isOverspeed(info);
	}

	@Override
	public int ping() throws TException {
		return manager.getClient().ping();
	}

	@Override
	public List<RoadSpeedInfo> searchSectionLInfo(List<Point> lps) throws SectionSpeedServiceException, TException {
		return manager.getClient().searchSectionLInfo(lps);
	}

	@Override
	public List<RoadSpeedInfo> searchSectionRNet(Point point) throws SectionSpeedServiceException, TException {
		return manager.getClient().searchSectionRNet(point);
	}
	
	public static void main(String[] args) throws TException, SectionSpeedServiceException {
		SectionSpeedClient.INSTANCE.ping();
		Point p = new Point();
		p.setLongitude(234);
		p.setLatitude(321);
		p.setElevation(4321);
		List<RoadSpeedInfo> l = SectionSpeedClient.INSTANCE.searchSectionRNet(p);
		for(RoadSpeedInfo info : l) {
			System.out.println(info);
		}
	}
	
}
