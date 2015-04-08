package com.caits.analysisserver.utils;

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Iterator;
import java.util.TreeMap;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;

import com.caits.analysisserver.database.Envelope;
import com.caits.analysisserver.database.SystemBaseInfoPool;


public class GisAccountMeg {
	
	private static final Logger logger = LoggerFactory.getLogger(GisAccountMeg.class);
	
	/******
	 * 剔除不合理数据
	 * @param map
	 * @param vid
	 * @return
	 */
	public static long accountMilege(TreeMap<Long,String> map,String vid) throws Exception{
		StringBuffer latLons = new StringBuffer("latons=");
		Iterator<Long> ky = map.keySet().iterator();
		
		while(ky.hasNext()){
			long key = ky.next();
			String[] cols = map.get(key).split(":");

			double lon =  Double.parseDouble(cols[0]==null ||"".equals(cols[0])?"0":cols[0])/600000;
			double lat = Double.parseDouble(cols[1]==null ||"".equals(cols[1])?"0":cols[1])/600000;
			String spdFrom = cols[24];	
			int gpsSpeed = 0; // 转换后速度
			gpsSpeed = Utils.getSpeed(spdFrom,cols); // 根据来源获取车速
			
			if(lon == 0 && lat == 0){ // 剔除掉经纬度为0
				continue;
			}else if(!Envelope.checkEnvelope(lat, lon)){
				continue;
			}
			
			latLons.append(lon);
			latLons.append(",");
			latLons.append(lat);
			latLons.append(",");
			latLons.append(gpsSpeed);
			latLons.append(",");
			latLons.append(cols[2]);
			latLons.append(";");
		}// End while
		long mileage = 0;
		mileage = postConnectGisServer(latLons.toString(), vid);
		latLons.delete(0, latLons.length());
		return mileage;
	}
	
	public static long accountMilege(String latLons,String vid) throws Exception{
		long mileage = 0;
		mileage = postConnectGisServer(latLons, vid);
		return mileage;
	}
	
	/*****
	 * 连接GIS server
	 * @param latons
	 * @param vid
	 * @return
	 */
	private static InputStream getConnectGisInputStream(String latons,String vid){
		URL url;
		try {
			url = new URL(SystemBaseInfoPool.getinstance().getBaseInfoMap("gis_url").getValue());
			HttpURLConnection url_con = (HttpURLConnection) url.openConnection();
			url_con.setRequestMethod("POST");
			url_con.setConnectTimeout(10000);// （单位：毫秒）
			url_con.setReadTimeout(20000);// （单位：毫秒）
			url_con.setDoOutput(true);
			byte[] b = latons.toString().getBytes();
			url_con.getOutputStream().write(b, 0, b.length);
			url_con.getOutputStream().flush();
			url_con.getOutputStream().close();
			return url_con.getInputStream();
		} catch (Exception e) {
			logger.error("Connect to gis server failed " + SystemBaseInfoPool.getinstance().getBaseInfoMap("gis_url").getValue() + ", when Account vid = " + vid, e);
			return null;
		}
	}
	
	/****
	 * post 请求连接 GIS server
	 * @param latons
	 * @param vid
	 * @return
	 */
	private static long postConnectGisServer(String latons,String vid){
		InputStream l_urlStream = null;
		try {
			
			int count = 0;
			while(count <3 ){ // 连接失败，试图连接最多3次。
				l_urlStream = getConnectGisInputStream(latons,vid);
				count++;
				if(null != l_urlStream){
					break;
				}
			}// End while
			
			String[] res = parseXml(l_urlStream,vid);
			if(!"".equals(res[0]) ){
				logger.info( "GIS计算里程==>VID:" + vid +  ";time : "+ res[1] + "; mileage : " + res[0]);
				return Long.parseLong(res[0]);
			}
			
		} catch (Exception e) {
			logger.error("VID="+vid+",Connected gis server for accounting milege to fail." , e);
		}finally{
			if(l_urlStream != null){
				try {
					l_urlStream.close();
				} catch (IOException e) {
					logger.error("Connected gis server Link to fail." , e);
				}
			}
		}
		return 0;
	}
	
	/*****
	 * 解析计算结果
	 * @param l_urlStream
	 * @return
	 */
	@SuppressWarnings("unused")
	private static String[] parseXml(InputStream l_urlStream,String vid ){
		SAXReader reader = new SAXReader();
		Document doc;
		String[] res = {"",""};
		try {
			doc = reader.read(l_urlStream);
			//code为1，正常输出
			if("1".equals(doc.getRootElement().attribute("code").getValue())){
				Element e = doc.getRootElement().element("mileage").element("length");
				long mileage = 0;
				
				if(e!=null){
					// 获取里程
					String len = e.getText();
					if(len!=null&&!"".equalsIgnoreCase(len)){
						mileage = Math.round(Double.parseDouble(len) * 10); // 公里转换成终端上报标准格式
						res[0] = String.valueOf(mileage);
					}
					//获取计算时间
					String time = doc.getRootElement().attribute("time").getValue();
					if(null != time && !"".equals(time)){
						res[1] = time;
					}
				}
				
				// 获取GIS匹配后轨迹
				Element ec = doc.getRootElement().element("mileage").element("coordinates");
				if(null != ec){
					String gisLatLon = ec.getTextTrim();
				}
			}
		} catch (DocumentException e) {
			logger.error("VID = " + vid + ";Parse xml to error.",e);
		}
		return res;
	}
}
