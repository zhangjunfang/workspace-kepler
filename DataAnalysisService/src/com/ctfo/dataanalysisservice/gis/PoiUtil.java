package com.ctfo.dataanalysisservice.gis;

import java.util.ArrayList;
import java.util.List;

import com.vividsolutions.jts.geom.Coordinate;
import com.vividsolutions.jts.geom.Geometry;
import com.vividsolutions.jts.geom.GeometryFactory;

/**
 * <p>
 * TestPoiInPolygon
 * </p>
 * <p>
 * 点与多边形的判断
 * </p>
 * 
 * @author 袁秀君(yuanxiujun@ctfo.com)
 * @version 0.0.0
 *          <table style="border:1px solid gray;">
 *          <tr>
 *          <th width="100px">版本号</th>
 *          <th width="100px">动作</th>
 *          <th width="100px">修改人</th>
 *          <th width="100px">修改时间</th>
 *          </tr>
 *          <!-- 以 Table 方式书写修改历史 -->
 *          <tr>
 *          <td>0.0.0</td>
 *          <td>创建类</td>
 *          <td>Administrator</td>
 *          <td>2012-2-20 下午04:31:21</td>
 *          </tr>
 *          <tr>
 *          <td>XXX</td>
 *          <td>XXX</td>
 *          <td>XXX</td>
 *          <td>XXX</td>
 *          </tr>
 *          </table>
 */
public class PoiUtil {

	/** 经纬度换算值 */
	private static double coord = 100000;

	/**
	 * 多边形围栏区域判定:判定一个坐标点是否在一多边形区域(闭合多边形)内（电子围栏）
	 * 
	 * @param xys
	 *            （【n1 经度，纬度】 【n2 经度，纬度】 【n.. 经度，纬度】【n1 经度，纬度】 ）
	 * @param point
	 *            （经度，纬度）
	 * @return
	 */
	public static boolean PoiInPoly(List<String> pointList, String point) {
		double x = MathExtend.divide(Double.parseDouble(point.split(",")[0]),
				coord, 8);
		double y = MathExtend.divide(Double.parseDouble(point.split(",")[1]),
				coord, 8);
		PoiInPolygon poiInPolygon = new PoiInPolygon();
		String[] xys = null;
		// String[] xys = points.split(" ");
		xys = new String[pointList.size()];
		pointList.toArray(xys);
		List<Point> poly = new ArrayList<Point>(xys.length);
		for (String xy : xys) {
			double px = MathExtend.divide(Double.parseDouble(xy.split(",")[0]),
					coord, 8);
			double py = MathExtend.divide(Double.parseDouble(xy.split(",")[1]),
					coord, 8);
			poly.add(new Point(px, py));
		}
		int pip = poiInPolygon.InPolygon(poly, new Point(x, y));
		if (pip == 0 || pip == 1)
			return true;
		return false;
	}

	/**
	 * 路线偏移判定 :判定一个坐标点是否在一条线路扩展成的多边形区域内
	 * 
	 * @param line
	 *            (【线路起点 经度，纬度】【线路终点 经度，纬度】)
	 * @param distance
	 *            （单位 米）
	 * @param point
	 *            （经度，纬度）
	 * @return
	 */
	public static boolean PoiInLineBuffer(String line, String distance,
			String point) {
		String line1 = line.split(" ")[0];
		String line2 = line.split(" ")[1];
		double buff = Double.parseDouble(distance);
		buff = MathExtend.divide(buff, 108000, 8);
		Coordinate points[] = new Coordinate[2];
		double l1x = MathExtend.divide(Double.parseDouble(line1.split(",")[0]),
				coord, 8);
		double l1y = MathExtend.divide(Double.parseDouble(line1.split(",")[1]),
				coord, 8);
		double l2x = MathExtend.divide(Double.parseDouble(line2.split(",")[0]),
				coord, 8);
		double l2y = MathExtend.divide(Double.parseDouble(line2.split(",")[1]),
				coord, 8);
		points[0] = new Coordinate(l1x, l1y);
		points[1] = new Coordinate(l2x, l2y);
		GeometryFactory factory = new GeometryFactory();
		Geometry lineString = factory.createLineString(points);
		// quadrantSegments = 8,endCapStyle = 3默认为1为弧形3为方形;
		Geometry polygon = lineString.buffer(buff, 8, 3);

		PoiInPolygon poiInPolygon = new PoiInPolygon();
		Coordinate coordinates[] = polygon.getCoordinates();
		List<Point> poly = new ArrayList<Point>(coordinates.length);
		for (Coordinate coord : coordinates) {
			poly.add(new Point(coord.x, coord.y));
		}
		double x = MathExtend.divide(Double.parseDouble(point.split(",")[0]),
				coord, 8);
		double y = MathExtend.divide(Double.parseDouble(point.split(",")[1]),
				coord, 8);
		int pip = poiInPolygon.InPolygon(poly, new Point(x, y));
		if (pip == 0 || pip == 1)
			return true;
		return false;
	}

	/**
	 * 关键点区域判定:判定一个坐标点是否在一个以关键点坐标为圆心， 半径为M（单位 米）的 圆形区域内
	 * 
	 * @param point1
	 *            （经度，纬度）
	 * @param radius
	 *            （单位 米）
	 * @param point2
	 *            （经度，纬度） 待判定的坐标点
	 * @return
	 */
	public static boolean PoiInPoiBuffer(String point1, String radius,
			String point2) {
		double p1x = MathExtend.divide(
				Double.parseDouble(point1.split(",")[0]), coord, 8);
		double p1y = MathExtend.divide(
				Double.parseDouble(point1.split(",")[1]), coord, 8);
		double p2x = MathExtend.divide(
				Double.parseDouble(point2.split(",")[0]), coord, 8);
		double p2y = MathExtend.divide(
				Double.parseDouble(point2.split(",")[1]), coord, 8);
		double r = Double.parseDouble(radius);
		double difference = PoiToLine
				.getGreatCircleDistance(p2x, p2y, p1x, p1y);
		if (difference <= r)
			return true;
		return false;
	}

	public static void main(String[] args) {
		PoiUtil tp = new PoiUtil();
		boolean b1 = tp.PoiInLineBuffer("11481329,4097365 11520468,4090207",
				"500", "11481329,4097365");
		System.out.println(b1);
		boolean b2 = tp.PoiInPoiBuffer("11560468,4090207", "50000",
				"11581329,4097365");
		System.out.println(b2);
		List list = new ArrayList();
		list.add("11483116,4089021");
		list.add("11508247,4089436");
		list.add("11505638,4078006");
		list.add("11483116,4089021");
		boolean b3 = tp.PoiInPoly(list, "11486116,4089021");
		System.out.println(b3);
	}

}
