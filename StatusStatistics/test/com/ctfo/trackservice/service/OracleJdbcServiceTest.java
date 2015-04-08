///**
// * 
// */
//package com.ctfo.trackservice.service;
//
//import java.util.HashMap;
//import java.util.Map;
//import java.util.UUID;
//
//import org.junit.Test;
//import org.springframework.context.ApplicationContext;
//import org.springframework.context.support.ClassPathXmlApplicationContext;
//
//import com.ctfo.trackservice.model.EquipmentStatus;
//import com.ctfo.trackservice.model.StatusBean;
//import com.ctfo.trackservice.model.StatusCode;
//import com.ctfo.trackservice.util.OracleProperties;
//
///**
// * @author ruky
// *
// */
//public class OracleJdbcServiceTest {
//
//	ApplicationContext ac = null;
//	OracleProperties oracleProperties = null;
//	OracleJdbcService service = null;
//	
//	public OracleJdbcServiceTest(){
//		ac = new ClassPathXmlApplicationContext("spring-dataaccess.xml");
//		oracleProperties = (OracleProperties) ac.getBean("oracleProperties");
//		service = new OracleJdbcService(oracleProperties);
//		service.createOracleJdbcService(); 
//		service.createOnlineOracleJdbcService();
//		service.createEquipmentOracleJdbcService();
//	}
//	
//	/**
//	 * Test method for {@link com.ctfo.trackservice.service.OracleJdbcService#queryStatusCode(java.lang.String)}.
//	 */
//	@Test
//	public void testQueryStatusCode() {
//		String vid = "241561";
//		EquipmentStatus eqStatus = new EquipmentStatus();
//		System.out.println("VID:"+vid+"\r\n查询前:"+service.getResult("select * from  TH_VEHICLE_STATUS where VID='"+vid+"'"));
//		eqStatus.setVid(vid);
//		eqStatus.setTerminalStatus(2); 
//		StatusCode statusCode = service.queryStatusCode(vid);
//		String result = getMethodsResult(statusCode);
//		System.out.println("查询结果:"+result);
//	}
//
//
//
//
//	/**
//	 * Test method for {@link com.ctfo.trackservice.service.OracleJdbcService#updateVehicleLineStatus(com.ctfo.trackservice.model.EquipmentStatus)}.
//	 * @throws Exception 
//	 */
//	@Test
//	public void testUpdateVehicleLineStatus() throws Exception { 
//		String vid = "241561";
//		EquipmentStatus eqStatus = new EquipmentStatus();
//		System.out.println("VID:"+vid+"\r\n更新前:"+service.getResult("select * from  TH_VEHICLE_STATUS where VID='"+vid+"'"));
//		eqStatus.setVid(vid);
//		eqStatus.setTerminalStatus(2); 
//		service.updateVehicleLineStatus(eqStatus);
//		service.commit();
//		System.out.println("更新结果:"+service.getResult("select * from  TH_VEHICLE_STATUS where VID='"+vid+"'"));
//	}
//
//	/**
//	 * Test method for {@link com.ctfo.trackservice.service.OracleJdbcService#updateLastTrackISonLine(java.lang.String, java.lang.String)}.
//	 */
//	@Test
//	public void testUpdateLastTrackISonLine() {
//		String vid = "241561";
//		System.out.println("VID:"+vid+"\r\n更新前:"+service.getResult("select * from  TR_VEHICLE_LASTTRACK where VID='"+vid+"'"));
//		service.updateLastTrackISonLine(vid, "7");
//		service.commit();
//		System.out.println("更新结果:"+service.getResult("select * from  TR_VEHICLE_LASTTRACK where VID='"+vid+"'"));
//	}
//
//	/**
//	 * Test method for {@link com.ctfo.trackservice.service.OracleJdbcService#updateLastTrack(java.util.Map)}.
//	 */
//	@Test
//	public void testUpdateLastTrack() {
//		String vid = "241561";
//		System.out.println("VID:"+vid+"\r\n更新前:"+service.getResult("select * from  TR_VEHICLE_LASTTRACK where VID='"+vid+"'"));
//		Map<String, String> eqStatus = new HashMap<String, String>();
//		eqStatus.put("1", "0");
//		eqStatus.put("2", "0");
//		eqStatus.put("3", "100");
//		eqStatus.put("maplon", "0");
//		eqStatus.put("maplat", "0");
//		eqStatus.put("utc", "1382341107715");
//		eqStatus.put("5", "0");
//		eqStatus.put("vid", "241561");
//		eqStatus.put("20", ",1,");
//		eqStatus.put("ratio", "-100");
//		eqStatus.put("gears", "-100");
//		eqStatus.put("570", "2");
//		service.updateLastTrack(eqStatus);
//		service.commit();
//		System.out.println("更新结果:"+service.getResult("select * from  TR_VEHICLE_LASTTRACK where VID='"+vid+"'"));
//	}
//
//	/**
//	 * Test method for {@link com.ctfo.trackservice.service.OracleJdbcService#updateLastTrackLine(java.util.Map)}.
//	 */
//	@Test
//	public void testUpdateLastTrackLine() {
//		String vid = "241561";
//		System.out.println("VID:"+vid+"\r\n更新前:"+service.getResult("select * from  TR_VEHICLE_LASTTRACK where VID='"+vid+"'"));
//		Map<String, String> eqStatus = new HashMap<String, String>();
//		eqStatus.put("1", "0");
//		eqStatus.put("2", "0");
//		eqStatus.put("3", "100");
//		eqStatus.put("maplon", "0");
//		eqStatus.put("maplat", "0");
//		eqStatus.put("utc", "1382341107715");
//		eqStatus.put("5", "0");
//		eqStatus.put("vid", "241561");
//		eqStatus.put("20", ",1,");
//		eqStatus.put("ratio", "-100");
//		eqStatus.put("gears", "-100");
//		eqStatus.put("570", "2");
//		service.updateLastTrackLine(eqStatus);
//		service.commit();
//		System.out.println("更新结果:"+service.getResult("select * from  TR_VEHICLE_LASTTRACK where VID='"+vid+"'"));
//	}
//
//	/**
//	 * Test method for {@link com.ctfo.trackservice.service.OracleJdbcService#saveOnline(java.lang.String, java.lang.String, java.lang.String)}.
//	 */
//	@Test
//	public void testSaveOnline() {
//		String uuid = UUID.randomUUID().toString().replace("-", "");
//		System.out.println("UUID:"+uuid+"\r\n插入前:"+service.getResult("select * from  TH_VEHICLE_ONLINE where ONOFFLINEID='"+uuid+"'"));
//		service.saveOnline("12345", "测A12345", uuid); 
//		service.commit();
//		System.out.println("插入结果:"+service.getResult("select * from  TH_VEHICLE_ONLINE where ONOFFLINEID='"+uuid+"'"));
//	}
//
//	/**
//	 * Test method for {@link com.ctfo.trackservice.service.OracleJdbcService#saveOffline(java.lang.String)}.
//	 */
//	@Test
//	public void testSaveOffline() {
//		String uuid = UUID.randomUUID().toString().replace("-", "");
//		System.out.println("UUID:"+uuid+"\r\n插入前:"+service.getResult("select * from  TH_VEHICLE_OFFLINE where ONOFFLINEID='"+uuid+"'"));
//		service.saveOffline(uuid); 
//		service.commit();
//		System.out.println("插入结果:"+service.getResult("select * from  TH_VEHICLE_OFFLINE where ONOFFLINEID='"+uuid+"'"));
//	}
//
//	/**
//	 * Test method for {@link com.ctfo.trackservice.service.OracleJdbcService#updateOnOfflineStatus(java.lang.Integer, java.lang.String, java.lang.String)}.
//	 */
//	@Test
//	public void testUpdateOnOfflineStatus() {
//		service.updateOnOfflineStatus(1, "20001", "12345"); 
//	}
//
//	
//	private String getMethodsResult(Object statusCode) {
//		java.lang.reflect.Method[] methods = statusCode.getClass().getMethods();
//		StringBuffer sb = new StringBuffer();
//		Object[] str = null;
//		for (int i = 0; i < methods.length; i++) {
//			String method = methods[i].getName();
//			if (method.startsWith("get")) {
//				try {
//					Object o = methods[i].invoke(statusCode, str);
//					if(o instanceof StatusBean){
//						StatusBean s = (StatusBean) o;
//						sb.append(method).append(":Max=").append(s.getMax()).append(", Min=").append(s.getMin()).append("; ");
//					} else {
//						sb.append(method).append(":").append(o).append("; ");
//					}
//				} catch (Exception e) {
//					e.printStackTrace();
//				}
//			}
//		}
//		return sb.toString();
//	}
//}
