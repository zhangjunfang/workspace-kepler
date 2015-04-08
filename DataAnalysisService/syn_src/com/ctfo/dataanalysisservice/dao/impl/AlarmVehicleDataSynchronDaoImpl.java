package com.ctfo.dataanalysisservice.dao.impl;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import com.ctfo.dataanalysisservice.beans.AlarmStationBean;
import com.ctfo.dataanalysisservice.beans.AlarmVehicleBean;
import com.ctfo.dataanalysisservice.beans.AreaDataObject;
import com.ctfo.dataanalysisservice.beans.KeyPointDataObject;
import com.ctfo.dataanalysisservice.beans.SectionsDataObject;
import com.ctfo.dataanalysisservice.dao.AlarmVehicleDataSynchronDao;
import com.ctfo.dataanalysisservice.database.DBConnectionManager;

/**
 * 
 * 
 * <p>
 * -----------------------------------------------------------------------------
 * <br>
 * 工程名 ： DataAnalysisService <br>
 * 功能：获取报警车辆列表 <br>
 * 描述：获取报警车辆列表 <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * -----------------------------------------------------------------------------
 * <br>
 * 修改历史 <br>
 * <table width="432" border="1">
 * <tr>
 * <td>版本</td>
 * <td>时间</td>
 * <td>作者</td>
 * <td>改变</td>
 * </tr>
 * <tr>
 * <td>1.0</td>
 * <td>Feb 17, 2012</td>
 * <td>张高</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author 张高
 * @since JDK1.6
 */
public class AlarmVehicleDataSynchronDaoImpl implements
		AlarmVehicleDataSynchronDao {

	/*
	 * 获取时间关键点报警车辆的列表 (non-Javadoc)
	 * 
	 * @see com.ctfo.dataanalysisservice.dao.AlarmVehicleDataSynchronDao#
	 * getAlarmStationVehicleList()
	 */
	public List<AlarmStationBean> getAlarmStationVehicleList() {
		List<AlarmStationBean> list = null;
		Connection con = null;
		PreparedStatement st = null;
		ResultSet rs = null;
		try {
			con = DBConnectionManager.getConnection();
			String sql = "select l.vid,s.to_utc,s.leave_utc from tr_line_vehicle l "
					+ " left join tb_station s on s.line_id = l.class_line_id and s.station_type=1 and s.enable_flag = 1 "
					+ " where s.to_utc is not null or s.leave_utc is not null";
			st = con.prepareStatement(sql);
			st.execute();
			rs = st.getResultSet();
			list = new ArrayList<AlarmStationBean>();
			while (rs.next()) {
				AlarmStationBean vo = new AlarmStationBean();
				vo.setVid(String.valueOf(rs.getInt("VID")));
				vo.setToUtc(rs.getString("TO_UTC"));
				vo.setLeaveUtc(rs.getString("LEAVE_UTC"));
				list.add(vo);
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			DBConnectionManager.freeDBResource(rs, st, con);
		}
		return list;
	}

	/*
	 * 获取报警车辆列表 (non-Javadoc)
	 * 
	 * @see com.ctfo.dataanalysisservice.dao.AlarmVehicleDataSynchronDao#
	 * getAlarmVehicleList()
	 */
	public List<AlarmVehicleBean> getAlarmVehicleList() {
		List<AlarmVehicleBean> list = null;
		Connection con = null;
		PreparedStatement st = null;
		ResultSet rs = null;
		try {
			con = DBConnectionManager.getConnection();

			// 0:围栏（进出报警和超速） 1：关键点（到达和离开 没有超速） 2：线路（偏移和分段限速）
			String sql = "select vid,commaddr ,wmsys.wm_concat(keyword) keyword from ("
					+ " select A.VID, '0' KEYWORD, SIM1.COMMADDR"
					+ " from TB_VEHICLE A"
					+ " left join TR_VEHICLE_AREA B on A.VID = B.VID"
					+ " left join TR_SERVICEUNIT TR1 on TR1.VID = B.VID"
					+ " left join TB_SIM SIM1 on TR1.SID = SIM1.Sid"
					+ " where B.Id is not null and B.SEND_STATUS=0 and A.ENABLE_FLAG=1"
					+ " union"
					+ " select A1.VID, '1' KEYWORD, SIM1.COMMADDR"
					+ " from TB_VEHICLE A1"
					+ " left join TR_LINE_VEHICLE C1 on A1.VID = C1.VID"
					+ " left join TB_STATION D1 on D1.LINE_ID = C1.CLASS_LINE_ID"
					+ " left join TR_SERVICEUNIT TR1 on TR1.VID = C1.VID"
					+ " left join TB_SIM SIM1 on TR1.SID = SIM1.Sid"
					+ " where D1.STATION_ID is not null and D1.STATION_TYPE=1 and C1.SEND_COMMAND_STATUS=-1 and A1.ENABLE_FLAG=1"
					+ " union"
					+ " select A2.VID, '2' KEYWORD, SIM2.COMMADDR"
					+ " from TB_VEHICLE A2"
					+ " left join TR_LINE_VEHICLE C2 on A2.VID = C2.VID"
					+ " left join TB_STATION D2 on D2.LINE_ID = C2.CLASS_LINE_ID"
					+ " left join TR_SERVICEUNIT TR2 on TR2.VID = C2.VID"
					+ " left join TB_SIM SIM2 on TR2.SID = SIM2.Sid"
					+ " where C2.PID is not null and D2.STATION_ID is not null and D2.STATION_TYPE=2 and C2.SEND_COMMAND_STATUS=-1 and A2.ENABLE_FLAG=1"
					+ ") group by vid,commaddr";

			st = con.prepareStatement(sql);
			st.execute();
			rs = st.getResultSet();
			list = new ArrayList<AlarmVehicleBean>();
			while (rs.next()) {
				AlarmVehicleBean vo = new AlarmVehicleBean();
				vo.setVid(String.valueOf(rs.getInt("VID")));
				vo.setAlarmType(rs.getString("KEYWORD"));
				vo.setMobileNo(rs.getString("COMMADDR"));
				list.add(vo);
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			DBConnectionManager.freeDBResource(rs, st, con);
		}
		return list;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.dataanalysisservice.dao.AlarmVehicleDataSynchronDao#
	 * getLineDataObject()
	 */
	@Override
	public List<SectionsDataObject> getLineDataObject() {
		// 获取线段的SQL
		String sql = getLineSectionSql();
		Connection con = null;
		PreparedStatement st = null;
		ResultSet rs = null;
		con = DBConnectionManager.getConnection();
		List<SectionsDataObject> data = new ArrayList<SectionsDataObject>();
		try {
			st = con.prepareStatement(sql);
			rs = st.executeQuery();
			while (rs.next()) {
				SectionsDataObject bo = new SectionsDataObject();
				bo.setLineId(String.valueOf(rs.getObject("classLineId")));
				bo.setLat(Long.parseLong(String.valueOf(rs.getObject("lat"))));
				bo.setLon(Long.parseLong(String.valueOf(rs.getObject("lon"))));
				bo.setMaxSpeed(String.valueOf(rs.getObject("speedThreshold")));
				bo.setMaxSpeedTime(String.valueOf(rs
						.getObject("speedTimeThreshold")));
				// bo.setSectionsId(String.valueOf(rs.getObject("")));
				bo.setVid(String.valueOf(rs.getObject("vid")));
				bo.setWight(String.valueOf(rs.getObject("roadWight")));
				bo.setStartLat(Long.parseLong(String.valueOf(rs
						.getObject("startLat"))));
				bo.setStartLon(Long.parseLong(String.valueOf(rs
						.getObject("startLon"))));
				bo.setEndLat(Long.parseLong(String.valueOf(rs
						.getObject("endLat"))));
				bo.setEndLon(Long.parseLong(String.valueOf(rs
						.getObject("endLon"))));
				
				bo.setValidStartTime(Long.parseLong(String.valueOf(rs
						.getObject("startTime"))));//线路的有效开始时间和结束时间
				bo.setValidEndTime(Long.parseLong(String.valueOf(rs
						.getObject("endTime"))));
				
				bo.setSectionsId(String.valueOf(rs.getObject("stationNo")));
				
				data.add(bo);
			}
		} catch (SQLException e) {
			e.printStackTrace();
		} finally {
			DBConnectionManager.freeDBResource(rs, st, con);
		}
		return data;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.dataanalysisservice.dao.AlarmVehicleDataSynchronDao#
	 * getKeyPointDataObject()
	 */
	@Override
	public List<KeyPointDataObject> getKeyPointDataObject() {
		Connection con = null;
		PreparedStatement st = null;
		ResultSet rs = null;
		con = DBConnectionManager.getConnection();
		List<KeyPointDataObject> data = new ArrayList<KeyPointDataObject>();

		// 查询关键点数据
		String sql = "select s.station_id stationId," + "s.lon lon,"
				+ "tr.vid vid," + "s.lat lat," + "s.station_area stationArea,"
				+ "s.speed_threshold speedThresHold,"
				+ "s.speed_time_threshold speedTimeThreshold,"
				+ "s.to_utc toUtc," + "s.leave_utc leaveUtc"
				+ " from tr_line_vehicle tr"
				+ " left join tb_class_line c on tr.class_line_id = c.line_id"
				+ " left join tb_station s on s.line_id = tr.class_line_id"
				+ " where c.enable_flag = 1" + " and s.enable_flag = 1"
				+ " and s.station_type = 1";

		try {

			st = con.prepareStatement(sql);
			rs = st.executeQuery();
			while (rs.next()) {
				KeyPointDataObject bo = new KeyPointDataObject();
				bo.setVid(String.valueOf(rs.getObject("vid")));
				bo.setKeyPointArea(String.valueOf(rs.getObject("stationArea")));
				bo.setKeyPointId(String.valueOf(rs.getObject("stationId")));
				bo.setLat(Long.parseLong(String.valueOf(rs.getObject("lat"))));
				bo.setLeaveTime(String.valueOf(rs.getObject("leaveUtc")));
				bo.setLon(Long.parseLong(String.valueOf(String.valueOf(rs
						.getObject("lon")))));
				bo.setMaxSpeed(String.valueOf(rs.getObject("speedThresHold")));
				bo.setMaxSpeedTime(String.valueOf(rs
						.getObject("speedTimeThreshold")));
				bo.setReachTime(String.valueOf(rs.getObject("toUtc")));
				bo.setVid(String.valueOf(rs.getObject("vid")));
				data.add(bo);
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			DBConnectionManager.freeDBResource(rs, st, con);
		}
		return data;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.dataanalysisservice.dao.AlarmVehicleDataSynchronDao#
	 * getAreaDataObject()
	 */
	@Override
	public List<AreaDataObject> getAreaDataObject() {
		List<AreaDataObject> data = new ArrayList<AreaDataObject>();
		// 查询围栏的sql
		String sql = "select tr.vid,"
				+ "tr.area_id areaId,tr.area_begintime begintime,tr.area_endtime endtime,"
				+ "tra.lon  ||','|| tra.lat  lonlat,"
				+ "tra.area_maxspeed areaMaxSpeed,"
				+ "tra.superspeed_times superSpeedTimes,"
				+ "tr.area_usetype areaUseType"
				+ " from tr_vehicle_area tr"
				+ " left join tb_area a on a.area_id = tr.area_id"
				+ " left join tr_area tra on tra.area_id = tr.area_id"
				+ " where "
				+ " tr.send_status = 0  order by tr.vid,tr.area_id";

		Connection con = null;
		PreparedStatement st = null;
		ResultSet rs = null;
		con = DBConnectionManager.getConnection();

		try {
			st = con.prepareStatement(sql);
			rs = st.executeQuery();
			while (rs.next()) {
				AreaDataObject bo = new AreaDataObject();

				bo.setLonlat(rs.getString("lonlat"));
				bo.setMaxSpeed(rs.getString("areaMaxSpeed"));
				bo.setMaxSpeedTime(rs.getString("superSpeedTimes"));
				bo.setVid(rs.getString("vid"));
				bo.setAreaID(rs.getLong("areaId"));
				bo.setBeginTime(rs.getLong("begintime"));
				bo.setEndTime(rs.getLong("endtime"));
				data.add(bo);
			}
		} catch (SQLException e) {
			e.printStackTrace();
		} finally {
			DBConnectionManager.freeDBResource(rs, st, con);
		}
		return data;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.dataanalysisservice.dao.AlarmVehicleDataSynchronDao#
	 * getSectionDataObject()
	 */
	@Override
	public List<SectionsDataObject> getSectionDataObject() {
		// 获取线段的SQL
		String sql = getLineSectionSql();
		Connection con = null;
		PreparedStatement st = null;
		ResultSet rs = null;
		con = DBConnectionManager.getConnection();
		List<SectionsDataObject> data = new ArrayList<SectionsDataObject>();
		try {
			st = con.prepareStatement(sql);
			rs = st.executeQuery();
			while (rs.next()) {
				SectionsDataObject bo = new SectionsDataObject();
				bo.setId(String.valueOf(rs.getObject("classLineId")));
				bo.setLat(Long.parseLong(String.valueOf(rs.getObject("lat"))));
				bo.setLon(Long.parseLong(String.valueOf(rs.getObject("lon"))));
				bo.setMaxSpeed(String.valueOf(rs.getObject("speedThreshold")));
				bo.setMaxSpeedTime(String.valueOf(rs
						.getObject("speedTimeThreshold")));
				// bo.setSectionsId(String.valueOf(rs.getObject("")));
				bo.setVid(String.valueOf(rs.getObject("vid")));
				bo.setWight(String.valueOf(rs.getObject("roadWight")));
				bo.setStartLat(Long.parseLong(String.valueOf(rs
						.getObject("startLat"))));
				bo.setStartLon(Long.parseLong(String.valueOf(rs
						.getObject("startLon"))));
				bo.setEndLat(Long.parseLong(String.valueOf(rs
						.getObject("endLat"))));
				bo.setEndLon(Long.parseLong(String.valueOf(rs
						.getObject("endLon"))));
				data.add(bo);
			}

		} catch (SQLException e) {
			e.printStackTrace();
		} finally {
			DBConnectionManager.freeDBResource(rs, st, con);
		}
		return data;
	}

	/**
	 * 获取线路，线段SQL
	 * 
	 * @return sql
	 */
	private String getLineSectionSql() {
		// 获取线路的SQL

		// (select lat from tb_station ss where ss.station_id =
		// p.end_station_id) endLat,
		// (select lon from tb_station ss where ss.station_id =
		// p.end_station_id) endLaon,

		String sql = "select tr.class_line_id classLineId,"
				+ "tr.vid vid,"
				+ "s.lat,s.lon," 
				+ "c.start_time startTime ,"
				+ "c.end_time endTime,"
				+ "nvl((select lat from tb_station ss where ss.station_id = p.start_station_id),0) startLat,"
				+ "nvl((select lon from tb_station ss where ss.station_id = p.start_station_id),0)  startLon,"
				+ "nvl((select lat from tb_station ss where ss.station_id = p.end_station_id),0)  endLat,"
				+ "nvl((select lon from tb_station ss where ss.station_id = p.end_station_id),0)  endLon,"
				+ "s.station_no stationNo," + "p.road_wight roadWight,"
				+ "p.speed_threshold speedThreshold,"
				+ "p.speed_time_threshold speedTimeThreshold"
				+ " from tr_line_vehicle tr"
				+ " left join tb_class_line c on tr.class_line_id = c.line_id"
				+ " left join tb_station s on s.line_id = tr.class_line_id"
				+ " left join tb_line_prop p on p.line_id = tr.class_line_id"
				+ " where s.enable_flag = 1"
				+ " and s.station_type = 2";
		return sql;
	}
}
