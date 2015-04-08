package com.ctfo.dataanalysisservice.gis;

import java.util.Map;
import java.util.TreeMap;

/**
 * <p>
 * PoiToLine
 * </p>
 * <p>
 * 计算点到线段的最近点： 如 果该线段平行于X轴（Y轴），则过点point作该线段所在直线的垂线，垂足很容易求得，
 * 然后计算出垂足，如果垂足在线段上则返回垂足，否则返回离垂足近 的端点；
 * 如果该线段不平行于X轴也不平行于Y轴，则斜率存在且不为0。设线段的两端点为pt1和pt2，斜率为：k = ( pt2.y - pt1. y ) /
 * (pt2.x - pt1.x ); 该直线方程为：y = k* ( x - pt1.x) + pt1.y。其垂线的斜率为 - 1 / k，垂线方程为：y
 * = (-1/k) * (x - point.x) + point.y 。 联立两直线方程解得：x = ( k^2 * pt1.x + k *
 * (point.y - pt1.y ) + point.x ) / ( k^2 + 1) ，y = k * ( x - pt1.x) + pt1.y;
 * 然后再判断垂足是否在线段上，如果在线段上则返回垂足；如果不在则计算两端点到垂足的距离，选择距离垂足较近的端点返回。
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
 *          <td>2011-7-18 下午04:19:25</td>
 *          </tr>
 *          <tr>
 *          <td>XXX</td>
 *          <td>XXX</td>
 *          <td>XXX</td>
 *          <td>XXX</td>
 *          </tr>
 *          </table>
 */
public class PoiToLine {
	static double EARTH_RADIUS = 6378137.0; // 单位M
	static double PI = Math.PI;
	static Map<Double, String> map = new TreeMap<Double, String>();

	static double getRad(double d) {
		return d * PI / 180.0;
	}

	/**
	 * caculate the great circle distance
	 * 
	 * @param x1
	 * @param y1
	 * @param x2
	 * @param y2
	 * @return
	 */
	public static double getGreatCircleDistance(double x1, double y1,
			double x2, double y2) {

		double radLat1 = getRad(y1);
		double radLat2 = getRad(y2);

		double a = radLat1 - radLat2;
		double b = getRad(x1) - getRad(x2);

		double s = 2 * Math.asin(Math.sqrt(Math.pow(Math.sin(a / 2), 2)
				+ Math.cos(radLat1) * Math.cos(radLat2)
				* Math.pow(Math.sin(b / 2), 2)));
		s = s * EARTH_RADIUS;
		s = Math.round(s * 10000) / 10000.0;
		return s;
	}

	/**
	 * caculate the great circle distance
	 * 
	 * @param x1
	 * @param y1
	 * @param x2
	 * @param y2
	 * @return
	 */
	public static String getAzimuth(double x1, double y1, double x2, double y2) {
		String az = "";
		double dy = y2 - y1;
		double dx = x2 - x1;
		double jd = (Math.atan(dy / dx)) * 180.0 / PI;
		if (dy > 0 && dx > 0) {
			jd = jd;
		}
		if (dy > 0 && dx < 0) {
			jd = jd + 180.0;
		}
		if (dy < 0 && dx < 0) {
			jd = jd + 270.0;
		}
		if (dy < 0 && dx > 0) {
			jd = jd + 360.0;
		}
		if (jd < 15.0)
			az = "正东";
		if (jd >= 15.0 && jd < 75.0)
			az = "东北";
		if (jd >= 75.0 && jd < 105.0)
			az = "正北";
		if (jd >= 105.0 && jd < 165.0)
			az = "西北";
		if (jd >= 165.0 && jd < 195.0)
			az = "正西";
		if (jd >= 195.0 && jd < 255.0)
			az = "西南";
		if (jd >= 255.0 && jd < 285.0)
			az = "正南";
		if (jd >= 285.0 && jd < 345.0)
			az = "东南";
		if (jd >= 345.0 && jd < 360.0)
			az = "正东";
		return az;
	}

	public static Map<Double, String> PTLine(double x, double y, double x1,
			double y1, double x2, double y2) {
		double x0, y0, d;// 垂足坐标或最近点坐标和距该坐标距离
		double k;// 斜率
		if (x1 == x2) {// 平行于Y轴
			if (y >= Math.min(y1, y2) && y <= Math.max(y1, y2)) {
				x0 = x1;
				y0 = y;
			} else {
				if (y > Math.max(y1, y2)) {
					x0 = x1;
					y0 = Math.max(y1, y2);
				} else {
					x0 = x1;
					y0 = Math.min(y1, y2);
				}
			}
		} else if (y1 == y2) {// 平行于X轴
			if (x > Math.min(x1, x2) && x < Math.max(x1, x2)) {
				x0 = x;
				y0 = y1;
			} else {
				if (x > Math.max(x1, x2)) {
					x0 = Math.max(x1, x2);
					y0 = y1;
				} else {
					x0 = Math.min(x1, x2);
					y0 = y1;
				}
			}
		} else {
			k = (y2 - y1) / (x2 - x1);
			x0 = (k * k * x1 + k * (y - y1) + x) / (k * k + 1);
			y0 = k * (x0 - x1) + y1;
			if (x0 >= Math.min(x1, x2) && x0 <= Math.max(x1, x2)
					&& y0 >= Math.min(y1, y2) && y0 <= Math.max(y1, y2)) {
				x0 = x0;
				y0 = y0;
			}
			if (x0 < Math.min(x1, x2) && y0 > Math.max(y1, y2)) {
				x0 = Math.min(x1, x2);
				y0 = Math.max(y1, y2);
			}
			if (x0 < Math.min(x1, x2) && y0 < Math.min(y1, y2)) {
				x0 = Math.min(x1, x2);
				y0 = Math.min(y1, y2);
			}
			if (x0 > Math.max(x1, x2) && y0 < Math.min(y1, y2)) {
				x0 = Math.max(x1, x2);
				y0 = Math.min(y1, y2);
			}
			if (x0 > Math.max(x1, x2) && y0 > Math.max(y1, y2)) {
				x0 = Math.max(x1, x2);
				y0 = Math.max(y1, y2);
			}
		}
		d = getGreatCircleDistance(x, y, x0, y0);
		map.put(d, x0 + " " + y0);
		return map;
	}

	public static synchronized void clearMap() {
		if (null != map)
			map.clear();
	}
}
