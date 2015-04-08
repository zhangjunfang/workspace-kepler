package com.ctfo.sectionspeed.thrift.server;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

import org.apache.thrift.TException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.sectionspeed.gis.SectionSpeedIndexManager;
import com.ctfo.sectionspeed.thrift.GPSInfo;
import com.ctfo.sectionspeed.thrift.Point;
import com.ctfo.sectionspeed.thrift.RoadSpeedInfo;
import com.ctfo.sectionspeed.thrift.SectionSpeed;
import com.ctfo.sectionspeed.thrift.SectionSpeedServiceException;
import com.ctfo.sectionspeed.util.ResourcesUtil;
import com.vividsolutions.jts.geom.Coordinate;
import com.vividsolutions.jts.geom.GeometryFactory;
import com.zjxl.ssl.CarType;
import com.zjxl.ssl.RNetInfo;
import com.zjxl.ssl.SectionSLException;
import com.zjxl.ssl.SectionSpeedLimit;

public class SectionSpeedHandler implements SectionSpeed.Iface {

	private static final int EXIT_SUCCESS = 0;
	private static SectionSpeedIndexManager manager = SectionSpeedIndexManager.INSTANCE;
	private static final Logger logger = LoggerFactory.getLogger(SectionSpeedHandler.class);
	private static String idxPath;
	private static final String DEF_IDX_PATH = "d:/idx";
	private static final GeometryFactory factory = new GeometryFactory();

	public SectionSpeedHandler() {
		Properties p = null;
		try {
			p = ResourcesUtil.getResourceAsProperties("system.properties");
		} catch (IOException e) {
			e.printStackTrace();
		}

		idxPath = (p == null) ? DEF_IDX_PATH : p.getProperty("indexpath");
	}

	@Override
	public boolean isOverspeed(GPSInfo info) throws TException, SectionSpeedServiceException {
		CarType ct;

		switch (info.cf.getValue()) {
		case 1:
			ct = CarType.TRUCK;
			break;
		case 0:
		case 2:
		default:
			ct = CarType.COACH;
		}

		Point p = info.getPoint();
		Coordinate coord = new Coordinate(p.getLongitude(), p.getLatitude());
		if(logger.isInfoEnabled()) {
			logger.info(p.toString());
		}
		try {
			return getSectionSpeedLimit().isOverspeed(coord, (float) info.getAngle(), (float) info.getSpeed(), ct);
		} catch (SectionSLException e) {
			e.printStackTrace();
			throw new SectionSpeedServiceException("gis interface error");
		}
	}

	@Override
	public List<RoadSpeedInfo> searchSectionLInfo(List<Point> lps) throws TException, SectionSpeedServiceException {
		Coordinate[] coordinates = new Coordinate[lps.size()];
		for (int i = 0, count = lps.size(); i < count; i++) {
			Point p = lps.get(i);
			coordinates[i] = new Coordinate(p.longitude, p.latitude);
		}
		List<RNetInfo> rnf = null;
		try {
			rnf = getSectionSpeedLimit().searchSectionLInfo(factory.createLineString(coordinates));
		} catch (SectionSLException e) {
			e.printStackTrace();
			throw new SectionSpeedServiceException("gis interface error");
		}
		List<RoadSpeedInfo> list = new ArrayList<RoadSpeedInfo>();
		for (RNetInfo item : rnf) {
			list.add(getRoadSpeedInfo(item));
		}
		return list;
	}

	@Override
	public List<RoadSpeedInfo> searchSectionRNet(Point point) throws TException, SectionSpeedServiceException {
		List<RoadSpeedInfo> list = new ArrayList<RoadSpeedInfo>();
		try {
			List<RNetInfo> rnf = getSectionSpeedLimit().searchSectionRNet(
					new Coordinate(point.getLatitude(), point.getLongitude()));

			for (RNetInfo item : rnf) {
				list.add(getRoadSpeedInfo(item));
			}
		} catch (SectionSLException e) {
			e.printStackTrace();
			throw new SectionSpeedServiceException("gis interface error");
		}
		return list;
	}

	@Override
	public int ping() throws TException {
		System.out.println("ping is successful~~!");
		return EXIT_SUCCESS;
	}

	private RoadSpeedInfo getRoadSpeedInfo(RNetInfo item) {
		RoadSpeedInfo info = new RoadSpeedInfo();
		info.setRid(item.getRid());
		info.setPostcode(item.getOwnercode());
		info.setFc(item.getFc());
		info.setStname(item.getStname());
		info.setByname(item.getByname());
		info.setDir(item.getDir());
		info.setNr(item.getNr());
		info.setRdnum(item.getRdnum());
		info.setRstruct(item.getRstruct());
		info.setStandardv(item.getStandardv());
		info.setTruckv(item.getTruckv());
		info.setCoachv(item.getCoachv());
		info.setToll(item.getToll());
		List<Point> l = new ArrayList<Point>();
		for (Coordinate gd : item.getGeo().getCoordinates()) {
			Point p = new Point();
			p.setLongitude(gd.x);
			p.setLatitude(gd.y);
			p.setElevation(gd.z);
			l.add(p);
		}
		info.setPointsList(l);
		return info;
	}

	private static SectionSpeedLimit getSectionSpeedLimit() {
		return manager.getSectionSpeedLimit(idxPath);
	}
}
