package com.ctfo.savecenter.util;

import java.math.BigDecimal;
import java.sql.SQLException;
@SuppressWarnings("unused")
public class AccountUtils {
	private static final String GEARS0 = "0";
	private static final String GEARS1 = "1";
	private static final String GEARS2 = "2";
	private static final String GEARS3 = "3";
	private static final String GEARS4 = "4";
	private static final String GEARS5 = "5";
	private static final String GEARS6 = "6";

	private static final String GEARS7 = "7";
	private static final String GEARS8 = "8";
	private static final String GEARSR = "12";

	/**
	 * 计算变速箱比
	 * 
	 * @param vin
	 * @param speeding
	 * @param engine_rotate_speed
	 * @return
	 * @throws SQLException 
	 */
	public static String accountRatio(Long vid, String speeding,String engine_rotate_speed,String tyre_r,String rear_axle_rate){
		
		String ratio = "";
		double d;
		if (tyre_r != null && !tyre_r.trim().equals("") && !tyre_r.trim().equals("0.0") && !tyre_r.trim().equals("0")
				&& rear_axle_rate != null
				&& !rear_axle_rate.trim().equals("") && !rear_axle_rate.trim().equals("0.0") && !rear_axle_rate.trim().equals("0") && speeding != null
				&& !speeding.equals("") && engine_rotate_speed != null
				&& !engine_rotate_speed.equals("")
				&& !speeding.equals("0.0") && !speeding.equals("0")) {
			// 轮胎滚动半径
			double rk = Double.parseDouble(tyre_r) / 1000;
			// 后桥速比
			double rar = Double.parseDouble(rear_axle_rate);
			d = (double) 0.377 * rk
					* Double.parseDouble(engine_rotate_speed)
					/ (rar * Double.parseDouble(speeding));
			BigDecimal bg = new BigDecimal(d);
			ratio = Utils.checkLength(bg.setScale(3,
					BigDecimal.ROUND_HALF_UP).toString(),
					Utils.RATIO_BASE);
			return ratio;
		} else {
			return "-";
		}
	}

	/**
	 * 计算档位
	 * 
	 * @param vin
	 * @param speeding
	 * @param engine_rotate_speed
	 * @return
	 * @throws SQLException 
	 * @throws NumberFormatException 
	 */
	public static String accountGears(Long vid, String speeding,
			String engine_rotate_speed,String tyre_r,String rear_axle_rate) throws NumberFormatException, SQLException {
		if (vid != null
				&& speeding != null
				&& !speeding.equals("")
				&& engine_rotate_speed != null
				&& !engine_rotate_speed.equals("")
				&& accountRatio(vid, speeding, engine_rotate_speed,tyre_r,rear_axle_rate) != null
				&& !accountRatio(vid, speeding, engine_rotate_speed,tyre_r,rear_axle_rate).equals("")
				&& !accountRatio(vid, speeding, engine_rotate_speed,tyre_r,rear_axle_rate).equals("-")) {
			double ratio = Double.valueOf(accountRatio(vid, speeding,engine_rotate_speed,tyre_r,rear_axle_rate));
			if (ratio < 10 && ratio >= 5) {
				return GEARS1;
			} else if (ratio < 5 && ratio >= 3) {
				return GEARS2;
			} else if (ratio < 3 && ratio >= 2) {
				return GEARS3;
			} else if (ratio < 2 && ratio >= 1) {
				return GEARS4;
			} else if (ratio < 1.1 && ratio >= 0.9) {
				return GEARS5;
			} else if (ratio < 0.9 && ratio >= 0.7) {
				return GEARS6;
			} else if(ratio >= 10){
				return GEARS0;
			}else{
				return "-";
			}
		} else {
			return "-";
		}
	}

}
