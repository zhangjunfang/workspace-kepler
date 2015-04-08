package com.caits.analysisserver.utils;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.dom4j.Document;
import org.dom4j.DocumentHelper;

/**
 * 获取地理位置信息
 * @author LiangJian
 * 2012年10月24日15:39:41
 */
public class GetAddressUtil {

	private static final Logger logger = LoggerFactory.getLogger(GetAddressUtil.class);
	
	/**
	 * 获取地理位置信息
	 * @param lon 经度
	 * @param lat 纬度
	 * @return 位置描述
	 * @throws Exception
	 */
	public static String getAddress(String lon,String lat){
		try{
			Document doc01 = getRGCService(lon, lat, "3");//获取详细街道位置信息
			String code = doc01.selectSingleNode("//result/@code").getStringValue();
			if(!"0".equals(code)){
				String pname = doc01.selectSingleNode("//result/pinfo/pname").getStringValue();//具体位置名称
				String paddress = doc01.selectSingleNode("//result/pinfo/paddress").getStringValue();//省市县
				String angle = doc01.selectSingleNode("//result/pinfo/angle").getStringValue();//方向
				String distance = doc01.selectSingleNode("//result/pinfo/distance").getStringValue();//查询点与该poi的距离，单位米
				String address = paddress+pname+"，向"+angle+"方向，"+distance+"米";
				return address; 
			}else{
				Document doc02 = getRGCService(lon, lat, "1");//获取省市县
				String code2 = doc02.selectSingleNode("//result/@code").getStringValue();
				if(!"0".equals(code2)){
					String province = doc02.selectSingleNode("//result/ainfob/province").getStringValue();//省
					String city = doc02.selectSingleNode("//result/ainfob/city").getStringValue();//市
					String county = doc02.selectSingleNode("//result/ainfob/county").getStringValue();//县
					String address =  province + city + county;
					return address;
				}else{
					return "未知位置";
				}
			}
		}catch(Exception e){
			logger.error("获取地理位置信息-接口异常", e);
			return "未知位置";
		}
	}
	
	/**
	 * 获取地理位置道路信息
	 * @param lon 经度
	 * @param lat 纬度
	 * @param angle 方向
	 * @return 管理等级 1：高速公路、2：国道、3：快速路、4：省道、5：主要道路、6：次要道路、7：一般道路、8：出入目的地道路、9：系道路、10：步行道路
	 * @throws Exception
	 */
	public static String getAddressNo(double lon,double lat,int angle){
		Document doc01 = null;
		try{
		doc01 = getRGCServiceRoadQuery(lon+"", lat+"", angle+"");//获取详细街道位置信息
		if(doc01.selectSingleNode("//result/@code")!=null){
			String code = doc01.selectSingleNode("//result/@code").getStringValue();
			if(!"0".equals(code)){
				if(doc01.selectSingleNode("//result/road/nr")!=null){
					String nr = doc01.selectSingleNode("//result/road/nr").getStringValue();//1：高速公路、2：国道、3：快速路、4：省道、5：主要道路、6：次要道路、7：一般道路、8：出入目的地道路、9：系道路、10：步行道路 
					return nr; 
				}else{
					logger.error("获取地理位置道路信息-接口返回非约定的XML格式:"+doc01.asXML());
					return null;
				}
			}else{
				return null;
			}
		}else{
			logger.error("获取地理位置道路信息-接口返回非约定的XML格式:"+doc01.asXML());
			return null;
		}
		}catch(Exception e){
			logger.error(" 获取地理位置道路信息-接口异常，lon="+lon+";lat="+lat+";angle="+angle+";return xml:"+doc01.asXML()+";Exception:"+e.getMessage(), e);
			return null;
		}
	}
	
	/**
	 * 请求远程服务，获取位置信息
	 * @param lon 经度
	 * @param lat 纬度
	 * @param serviceNo {1-6} 服务编号 1、逆地理编码行政区划搜索接口;3、逆地理编码POI搜索接口
	 * @return
	 * #获取位置信息HTTP地址,部分参数在程序中拼接
RGCService_IP=http://srv.transmap.com.cn/RGCService/rgc
#获取国道、省道、县道信息。例子：http://srv.transmap.com.cn/RPService/roadquery.xml?coord=116.384963840197%2039.9059135528446&angle=120
RGCService_IP_RoadQuery=http://srv.transmap.com.cn/RPService
	 */
	public static Document getRGCService(String lon,String lat,String serviceNo)throws Exception{
		String rgcservice_ip = "http://srv.transmap.com.cn/RGCService/rgc";
		String url = rgcservice_ip+"?pt="+lon+"%20"+lat+"&service="+serviceNo;
		String xml = HttplUtil.doPost(url);
		Document doc = DocumentHelper.parseText(xml);
		return doc;
	}
	
	/**
	 * 请求远程服务，获取道路信息
	 * @param lon 经度
	 * @param lat 纬度
	 * @param angle 方向
	 * @return
	 */
	public static Document getRGCServiceRoadQuery(String lon,String lat,String angle)throws Exception{
		String rgcservice_ip = "http://srv.transmap.com.cn/RGCService/rgc";
		String url = rgcservice_ip+"/roadquery.xml?coord="+lon+"%20"+lat+"&angle="+angle;
		String xml = HttplUtil.doPost(url);
		Document doc = DocumentHelper.parseText(xml);
		return doc;
	}
	
	public static void main(String[] args)throws Exception{
		String address = GetAddressUtil.getAddress((double)(106.4493916666667) + "", (17733154/600000.0) + "");
		//String addressNo = GetAddressUtil.getAddressNo((double)(106.4493916666667), (17733154/600000.0) + "",3);
		System.out.println(address);
		//System.out.println(addressNo);
		System.out.println("==========" + GetAddressUtil.getRGCServiceRoadQuery("106.4493916666667", "29.68474666666667", "3").asXML());
	}
}
