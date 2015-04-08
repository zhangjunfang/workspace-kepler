package com.ctfo.filesaveservice.task;

import org.junit.Test;

import com.ctfo.filesaveservice.dao.OracleConnectionPool;
import com.ctfo.filesaveservice.model.OracleProperties;

public class VehicleCacheTaskTest {
	OracleConnectionPool pool = null;
	public VehicleCacheTaskTest() throws Exception{  
		OracleProperties oracleProperties = new OracleProperties();
		oracleProperties.setUrl("jdbc:oracle:thin:@192.168.200.115:1521:ORCl");
		oracleProperties.setUsername("kcpt");
		oracleProperties.setPassword("kcpt");
		oracleProperties.setMaxActive(10);
		oracleProperties.setMinIdle(1);
		oracleProperties.setInitialSize(1);
		pool = new OracleConnectionPool(oracleProperties);
	}
	@Test
	public void testInit() {
		VehicleCacheTask dct = new VehicleCacheTask();
		dct.setConfig("clearTime", "01");
		dct.setConfig("sql_syncAll", "SELECT V.VEHICLE_NO, S.SUID, V.VID AS VID, V.PLATE_COLOR AS PLATE_COLOR_ID, M.COMMADDR AS T_IDENTIFYNO, T.OEM_CODE AS OEMCODE, T.TMODEL_CODE, T.TID, V.VIN_CODE FROM TB_SIM M	 INNER JOIN TR_SERVICEUNIT S	  ON S.SID = M.SID    INNER JOIN TB_VEHICLE V ON V.VID = S.VID INNER JOIN TB_TERMINAL T ON T.TID = S.TID WHERE COMMADDR IS NOT NULL AND V.ENABLE_FLAG = '1' AND T.ENABLE_FLAG = '1'");
		dct.setConfig("sql_syncIncrements", "SELECT V.VEHICLE_NO, S.SUID, V.VID AS VID, V.PLATE_COLOR AS PLATE_COLOR_ID, M.COMMADDR AS T_IDENTIFYNO, T.OEM_CODE AS OEMCODE, T.TMODEL_CODE, T.TID, V.VIN_CODE FROM TB_SIM M	 INNER JOIN TR_SERVICEUNIT S	  ON S.SID = M.SID    INNER JOIN TB_VEHICLE V ON V.VID = S.VID INNER JOIN TB_TERMINAL T ON T.TID = S.TID WHERE COMMADDR IS NOT NULL AND V.ENABLE_FLAG = '1' AND T.ENABLE_FLAG = '1' AND (M.UPDATE_TIME >= ? OR M.CREATE_TIME >= ? OR S.UPDATE_TIME >= ? OR S.CREATE_TIME >= ? OR V.UPDATE_TIME >= ? OR V.CREATE_TIME >= ? OR T.UPDATE_TIME >= ? OR T.CREATE_TIME >= ?)");
		dct.setConfig("sql_syncAll_3g", "SELECT S.SUID, V.VID AS VID, V.PLATE_COLOR AS PLATE_COLOR_ID, M.COMMADDR  AS T_IDENTIFYNO, T.OEM_CODE  AS OEMCODE, T.TMODEL_CODE, T.TID, VEHICLE_NO, TD.DVR_SIMNUM, V.VIN_CODE FROM TB_SIM M INNER JOIN TR_SERVICEUNIT S ON S.SID = M.SID INNER JOIN TB_VEHICLE V ON V.VID = S.VID INNER JOIN TB_TERMINAL T ON T.TID = S.TID INNER JOIN KCPT.TB_DVR TD ON TD.DVR_ID = S.DVR_ID WHERE TD.DVR_SIMNUM IS NOT NULL AND V.ENABLE_FLAG = '1' AND T.ENABLE_FLAG = '1' AND T.TER_STATE = 2");
		dct.setConfig("sql_syncIncrements_3g", "SELECT S.SUID, V.VID AS VID, V.PLATE_COLOR AS PLATE_COLOR_ID, M.COMMADDR  AS T_IDENTIFYNO, T.OEM_CODE  AS OEMCODE, T.TMODEL_CODE, T.TID, VEHICLE_NO, TD.DVR_SIMNUM, V.VIN_CODE FROM TB_SIM M INNER JOIN TR_SERVICEUNIT S ON S.SID = M.SID INNER JOIN TB_VEHICLE V ON V.VID = S.VID INNER JOIN TB_TERMINAL T ON T.TID = S.TID INNER JOIN KCPT.TB_DVR TD ON TD.DVR_ID = S.DVR_ID WHERE TD.DVR_SIMNUM IS NOT NULL AND V.ENABLE_FLAG = '1' AND T.ENABLE_FLAG = '1' AND T.TER_STATE = 2 AND (M.UPDATE_TIME >= ? OR M.CREATE_TIME >= ? OR S.UPDATE_TIME >= ? OR S.CREATE_TIME >= ? OR V.UPDATE_TIME >= ? OR V.CREATE_TIME >= ? OR T.UPDATE_TIME >= ? OR T.CREATE_TIME >= ? OR TD.UPDATE_TIME >=? OR TD.CREATE_TIME >=?)");
		dct.init();
		dct.init();
	}
}
