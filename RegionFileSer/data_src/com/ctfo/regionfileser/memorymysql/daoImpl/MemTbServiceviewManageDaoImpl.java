package com.ctfo.regionfileser.memorymysql.daoImpl;

import java.math.BigDecimal;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import com.ctfo.memorymysql.beans.MemTbServiceviewManage;
import com.ctfo.regionfileser.dbc.DataBaseConnection;
import com.ctfo.regionfileser.memorymysql.dao.MemTbServiceviewManageDao;

public class MemTbServiceviewManageDaoImpl implements MemTbServiceviewManageDao {
	
	
	private String dbdriver;
	
	private String dburl;
	
	private String username;
	
	private String password;
	
	
	public void setDbdriver(String dbdriver) {
		this.dbdriver = dbdriver;
	}

	
	public void setDburl(String dburl) {
		this.dburl = dburl;
	}

	public void setUsername(String username) {
		this.username = username;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	private String selectsql;

	/**
	 * 查询所有轨迹信息
	 */
	public List<MemTbServiceviewManage> queryAll() {
		
		DataBaseConnection dbc = DataBaseConnection.getInstance(dbdriver,dburl,username,password);
		
		List<MemTbServiceviewManage> list = new ArrayList<MemTbServiceviewManage>();
		
		String sql = selectsql; 
		
		PreparedStatement pstmt = null;
		
		ResultSet rs = null;
			
		try {
			
			pstmt = dbc.getConnection().prepareStatement(sql);
			
			rs = pstmt.executeQuery();
			
			while(rs.next()) {
				
				// 查询出内容将内容赋值给对象
				MemTbServiceviewManage tbService = new MemTbServiceviewManage();
				tbService.setVid(rs.getInt(1));
				tbService.setVehicleno(rs.getString(2));
				tbService.setCname(rs.getString(3));			
				tbService.setMaplon(rs.getLong(4));
				tbService.setMaplat(rs.getLong(5));
				tbService.setSpeed(rs.getInt(6));
				tbService.setCorpId(rs.getInt(7));
				tbService.setCorpName(rs.getString(8));
				tbService.setTeamId(rs.getInt(9));
				tbService.setTeamName(rs.getString(10));
				tbService.setUtc(rs.getLong(11));
				tbService.setAlarmcode(rs.getString(12));
				tbService.setPlateColorId(rs.getInt(13));
				
				list.add(tbService);
			}
			
		
			
		} catch (Exception e) {
			
			e.printStackTrace();
			
		} finally {
			
	        try {
	        	//if(pstmt!=null)
	        	//pstmt.close();
				//rs.close();
				// 关闭数据库连接
				dbc.close(pstmt, rs, dbc.getConnection());
				
			} catch (SQLException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			
		}
		
		return list;
	}
	
	/**
	 * 插入测试数据
	 */
	public void insertTbService() {
		
		DataBaseConnection dbc = DataBaseConnection.getInstance(dbdriver,dburl,username,password);
		
		PreparedStatement pstmt = null;
		
		try {
			
			Long beginTime = System.currentTimeMillis();
			BigDecimal lon = new BigDecimal(68216140.000000);
			BigDecimal lat = new BigDecimal(20810506.000000);
			BigDecimal add = new BigDecimal(5.000000);
			Long timer = System.currentTimeMillis();
			String no = "京";
			
			String sql = "insert into MEM_TB_SERVICEVIEW(vid,vehicleno,cname,maplon,maplat,speed,corp_id,corp_name,team_id,team_name,utc,alarmcode,cid,PLATE_COLOR_ID,VEHICLETYPE_ID) values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
			
			pstmt = dbc.getConnection().prepareStatement(sql);
			
			for (int i = 1; i <= 50000; i++) {
							
				pstmt.setInt(1, i);
				pstmt.setString(2, no+i);
				pstmt.setString(3, "abc");
				lon = lon.add(add);
				lat = lat.add(add);
				pstmt.setBigDecimal(4, lon);
				pstmt.setBigDecimal(5, lat);
				pstmt.setInt(6, 20);
				pstmt.setInt(7, 15);
				pstmt.setString(8, "中交兴路");
				pstmt.setInt(9, 14);
				pstmt.setString(10, "默认车队");
				timer = timer + 25;
				pstmt.setLong(11, timer);
				pstmt.setString(12, "12");
				pstmt.setInt(13, 15);
				pstmt.setInt(14, 1);
				pstmt.setInt(15, 35);
				
				pstmt.addBatch();
				
				// 批量保存
				if(i%1000 ==0){
					pstmt.executeBatch();
										
					pstmt.clearBatch();
					
					Long endTime = System.currentTimeMillis();					
					System.out.println("pstmt+batch："+(endTime-beginTime)/1000+"秒");				
				}
			}
					
			Long endTimes = System.currentTimeMillis();
			System.out.println("pstmt+batch："+(endTimes-beginTime)/1000+"秒");
			
			//pstmt.close();

		} catch (Exception e) {

			e.printStackTrace();
			
		} finally {
			
			// 关闭数据库连接
			try {
				dbc.close(pstmt, null, dbc.getConnection());
				
			} catch (Exception e) {
				e.printStackTrace();
			}
			
		}
			
	}
	
	public void setSelectsql(String selectsql) {
		this.selectsql = selectsql;
	}
}
