package com.ctfo.analy.util;

import org.apache.log4j.Logger;
import org.dom4j.Document;
import org.dom4j.DocumentHelper;
import org.dom4j.Element;

import com.encryptionalgorithm.Converter;
import com.encryptionalgorithm.Point;

/**
 * 获取地理位置信息
 * @author LiangJian
 * 2012年10月24日15:39:41
 */
public class GetAddressUtil {

	private static final Logger logger = Logger.getLogger(GetAddressUtil.class);
	
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
	public static String getAddressNo2(double lon,double lat,int angle){
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
	 * 根据经纬度、角度信息获取相近道路的最高道路等级
	 * @param lon 经度
	 * @param lat 纬度
	 * @param angle 方向
	 * @return 管理等级 1：高速公路、2：国道、3：快速路、4：省道、5：主要道路、6：次要道路、7：一般道路、8：出入目的地道路、9：系道路、10：步行道路
	 * @throws Exception
	 */
	public static String getAddressNo(double lon,double lat,int angle){
		logger.info("进入方法getAddressNo()");
		Document doc01 = null;
		try{
		doc01 = getRGCServiceRoadQuery(lon+"", lat+"", angle+"");//获取详细街道位置信息
		logger.info("doc01 = " + doc01);
		int lvl = 99;
		
		if(doc01.selectSingleNode("//result/@code")!=null){
			logger.info("doc01.selectSingleNode(\"//result/@code\")!=null");
			String code = doc01.selectSingleNode("//result/@code").getStringValue();
			logger.info("code = " + code);
			if(!"0".equals(code)){
				logger.info("equals(code)");
				java.util.List ls = doc01.selectNodes("//result/roads/road/nr");
				logger.info("ls");
				if (ls!=null&&ls.size()>0){
					logger.info("ls!=null");
					for (int i=0;i<ls.size();i++){
						Element element = (Element)ls.get(i);
						String nr = element.getStringValue();
						int tmpLvl = Integer.parseInt((nr==null||"".equals(nr))?"99":nr.trim());
						if (tmpLvl<lvl){
							lvl = tmpLvl;
						}
					}
				}
				if (lvl!=99){
					logger.info("lvl!=99");
					return ""+lvl;
				}else{
					logger.debug("获取地理位置道路信息-接口没有返回道路等级:"+doc01.asXML());
					return null;
				}
			}else{
				logger.debug("获取地理位置道路信息-接口返回结果状态代码为"+code+":"+doc01.asXML());
				return null;
			}
		}else{
			logger.debug("获取地理位置道路信息-接口返回非约定的XML格式:"+doc01.asXML());
			return null;
		}
		}catch(Exception e){
			logger.info("------------------------------" + e.getMessage());
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
	 */
	public static Document getRGCService(String lon,String lat,String serviceNo)throws Exception{
		String rgcservice_ip = PropertiesUtil.PROPERTIES.read("system.properties", "RGCService_IP");
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
		logger.info("进入方法getRGCServiceRoadQuery()");
		String rgcservice_ip = PropertiesUtil.PROPERTIES.read("system.properties", "RGCService_IP_RoadQuery");
		logger.info("rgcservice_ip = " + rgcservice_ip);
		String url = rgcservice_ip+"/roadquery2.xml?coord="+lon+"%20"+lat+"&angle="+angle;
		logger.info("url = " + url);
		String xml = HttplUtil.doPost(url);
		logger.info("xml = " + xml);
		Document doc = DocumentHelper.parseText(xml);
		logger.info("退出方法getRGCServiceRoadQuery()");
		return doc;
	}
	
	public static void main(String[] args)throws Exception{
		String address = GetAddressUtil.getAddress((double)(106.4493916666667) + "", (17733154/600000.0) + "");
		
		/*Converter conver = new Converter();
		Point point = conver.getEncryPoint(66121125/600000.0, 16705254/ 600000.0);
		if (point != null) {
			long maplon = Math.round(point.getX() * 600000);
			long maplat = Math.round(point.getY() * 600000);
			System.out.println(66121125/600000.0+","+16705254/ 600000.0);
			System.out.println(maplon/600000.0+","+maplat/ 600000.0);
			String addressNo = GetAddressUtil.getAddressNo2((maplon/600000.0), (maplat/600000.0),229);
			System.out.println(addressNo);
		}
		*/
		
		System.out.println(66121125/600000.0+","+16705254/600000.0);
		
		String addressNo = GetAddressUtil.getAddressNo((65142654/600000.0), (16692983/600000.0),161);
		System.out.println("1 :"+addressNo);

		String addressNo2 = GetAddressUtil.getAddressNo((64552249/600000.0), (17640524/600000.0),278);
		System.out.println("2 :"+addressNo2);

		String addressNo3 = GetAddressUtil.getAddressNo((64548582/600000.0), (17640616/600000.0),270);
		System.out.println("3 :"+addressNo3);
		
		String addressNo4 = GetAddressUtil.getAddressNo((64545282/600000.0), (17640681/600000.0),270);
		System.out.println("4 :"+addressNo4);
		
		String addressNo5 = GetAddressUtil.getAddressNo((64541985/600000.0), (17640993/600000.0),286);
		System.out.println("5 :"+addressNo4);
		
		String addressNo6 = GetAddressUtil.getAddressNo((64538707/600000.0), (17641828/600000.0),274);
		System.out.println("6 :"+addressNo4);
		/*String addressNo4 = GetAddressUtil.getAddressNo((64548582/600000.0), (17640616/600000.0),270);
		System.out.println(addressNo4);*/
		
		//System.out.println(addressNo);
		//System.out.println(Base64_URl.base64Encode("你已超速，请谨慎驾驶！"));
		//System.out.println("==========" + GetAddressUtil.getRGCServiceRoadQuery("106.4493916666667", "29.68474666666667", "3").asXML());
	}
}
