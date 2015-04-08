package com.ctfo.trackservice.common;

import com.vividsolutions.jts.geom.Coordinate;
import com.vividsolutions.jts.geom.Geometry;
import com.vividsolutions.jts.geom.GeometryCollection;
import com.vividsolutions.jts.geom.GeometryFactory;
import com.vividsolutions.jts.geom.LineString;
import com.vividsolutions.jts.geom.LinearRing;
import com.vividsolutions.jts.geom.MultiLineString;
import com.vividsolutions.jts.geom.MultiPoint;
import com.vividsolutions.jts.geom.MultiPolygon;
import com.vividsolutions.jts.geom.Point;
import com.vividsolutions.jts.geom.Polygon;
import com.vividsolutions.jts.io.ParseException;
import com.vividsolutions.jts.io.WKTReader;

/**
 * 
 * Class GeometryDemo.java
 * 
 * Description Geometry 几何实体的创建，读取操作
 * 
 * Company mapbar
 * 
 * author Chenll E-mail: Chenll@mapbar.com
 * 
 * Version 1.0
 * 
 * Date 2012-2-17 上午11:08:50
 */
public class GeometryUtil {

	private GeometryFactory geometryFactory = new GeometryFactory();

	/**
	 * create a point
	 * 
	 * @return
	 */
	public Point createPoint() {
		Coordinate coord = new Coordinate(109.013388, 32.715519);
		Point point = geometryFactory.createPoint(coord);
		return point;
	}

	/**
	 * create a point by WKT
	 * 
	 * @return
	 * @throws ParseException
	 */
	public Point createPointByWKT() throws ParseException {
		WKTReader reader = new WKTReader(geometryFactory);
		Point point = (Point) reader.read("POINT (109.013388 32.715519)");
		return point;
	}

	/**
	 * create multiPoint by wkt
	 * 
	 * @return
	 */
	public MultiPoint createMulPointByWKT() throws ParseException {
		WKTReader reader = new WKTReader(geometryFactory);
		MultiPoint mpoint = (MultiPoint) reader.read("MULTIPOINT(109.013388 32.715519,119.32488 31.435678)");
		return mpoint;
	}

	/**
	 * 
	 * create a line
	 * 
	 * @return
	 */
	public LineString createLine() {
		Coordinate[] coords = new Coordinate[] { new Coordinate(2, 2), new Coordinate(2, 2) };
		LineString line = geometryFactory.createLineString(coords);
		return line;
	}

	/**
	 * create a line by WKT
	 * 
	 * @return
	 * @throws ParseException
	 */
	public LineString createLineByWKT() throws ParseException {
		WKTReader reader = new WKTReader(geometryFactory);
		LineString line = (LineString) reader.read("LINESTRING(0 0, 2 0)");
		return line;
	}

	/**
	 * create multiLine
	 * 
	 * @return
	 */
	public MultiLineString createMLine() {
		Coordinate[] coords1 = new Coordinate[] { new Coordinate(2, 2), new Coordinate(2, 2) };
		LineString line1 = geometryFactory.createLineString(coords1);
		Coordinate[] coords2 = new Coordinate[] { new Coordinate(2, 2), new Coordinate(2, 2) };
		LineString line2 = geometryFactory.createLineString(coords2);
		LineString[] lineStrings = new LineString[2];
		lineStrings[0] = line1;
		lineStrings[1] = line2;
		MultiLineString ms = geometryFactory.createMultiLineString(lineStrings);
		return ms;
	}

	/**
	 * create multiLine by WKT
	 * 
	 * @return
	 * @throws ParseException
	 */
	public MultiLineString createMLineByWKT() throws ParseException {
		WKTReader reader = new WKTReader(geometryFactory);
		MultiLineString line = (MultiLineString) reader.read("MULTILINESTRING((0 0, 2 0),(1 1,2 2))");
		return line;
	}

	/**
	 * create a polygon(多边形) by WKT
	 * 
	 * @return
	 * @throws ParseException
	 */
	public Polygon createPolygonByWKT() throws ParseException {
		WKTReader reader = new WKTReader(geometryFactory);
		Polygon polygon = (Polygon) reader.read("POLYGON((20 10, 30 0, 40 10, 30 20, 20 10))");
		return polygon;
	}

	/**
	 * create multi polygon by wkt
	 * 
	 * @return
	 * @throws ParseException
	 */
	public MultiPolygon createMulPolygonByWKT() throws ParseException {
		WKTReader reader = new WKTReader(geometryFactory);
		MultiPolygon mpolygon = (MultiPolygon) reader.read("MULTIPOLYGON(((40 10, 30 0, 40 10, 30 20, 40 10),(30 10, 30 0, 40 10, 30 20, 30 10)))");
		return mpolygon;
	}

	/**
	 * create GeometryCollection contain point or multiPoint or line or
	 * multiLine or polygon or multiPolygon
	 * 
	 * @return
	 * @throws ParseException
	 */
	public GeometryCollection createGeoCollect() throws ParseException {
		LineString line = createLine();
		Polygon poly = createPolygonByWKT();
		Geometry g1 = geometryFactory.createGeometry(line);
		Geometry g2 = geometryFactory.createGeometry(poly);
		Geometry[] garray = new Geometry[] { g1, g2 };
		GeometryCollection gc = geometryFactory.createGeometryCollection(garray);
		return gc;
	}

	/**
	 * create a Circle 创建一个圆，圆心(x,y) 半径RADIUS
	 * 
	 * @param x
	 * @param y
	 * @param RADIUS
	 * @return
	 */
	public Polygon createCircle(double x, double y, final double RADIUS) {
		final int SIDES = 32;// 圆上面的点个数
		Coordinate coords[] = new Coordinate[SIDES + 1];
		for (int i = 0; i < SIDES; i++) {
			double angle = ((double) i / (double) SIDES) * Math.PI * 2.0;
			double dx = Math.cos(angle) * RADIUS;
			double dy = Math.sin(angle) * RADIUS;
			coords[i] = new Coordinate((double) x + dx, (double) y + dy);
		}
		coords[SIDES] = coords[0];
		LinearRing ring = geometryFactory.createLinearRing(coords);
		Polygon polygon = geometryFactory.createPolygon(ring, null);
		return polygon;
	}

	private final static double R = 6371229; // 地球的半径 unit m

	/**
	 * 
	 * @param longt1
	 * @param lat1
	 * @param longt2
	 * @param lat2
	 * @return unit:m 如：double len1 = getDistance(113.67202, 34.70384,113.67202, 34.70249);
	 */
	public static double getDistance(double longt1, double lat1, double longt2, double lat2) {
		double x, y, distance;
		x = (longt2 - longt1) * Math.PI * R * Math.cos(((lat1 + lat2) / 2) * Math.PI / 180) / 180;
		y = (lat2 - lat1) * Math.PI * R / 180;
		distance = Math.hypot(x, y);
		return distance;
	}

	/**
	 * @param args
	 * @throws ParseException
	 */
	public static void main(String[] args) throws ParseException {
		GeometryUtil gt = new GeometryUtil();
		Polygon p = gt.createCircle(0, 1, 2);
		// 圆上所有的坐标(32个)
		Coordinate coords[] = p.getCoordinates();
		for (Coordinate coord : coords) {
			System.out.println(coord.x + "," + coord.y);
		}

		double len1 = getDistance(113.67202, 34.70384, 113.67202, 34.70249);
		System.out.println("len1:" + len1);
		double len2 = getDistance(113.67202, 34.70384, 113.67352, 34.70384);
		System.out.println("len2:" + len2);
	}
}
