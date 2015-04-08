/**
 * Copyright (c) 2011, CTFO Group, Ltd. All rights reserved.
 */
package com.ctfo.informationser.monitoring.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import com.ctfo.informationser.basic.service.RemoteJavaServiceRmiAbstract;
import com.ctfo.informationser.monitoring.beans.VehicleInfo;
import com.ctfo.informationser.monitoring.dao.VehicleInfoDao;
import com.ctfo.informationser.monitoring.service.VehicleInfoService;
import com.ctfo.informationser.util.XMLParse;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.local.obj.DynamicSqlParameter;
import com.ctfo.redis.core.RedisDaoSupport;
import com.ctfo.redis.util.RedisJsonUtil;
import com.google.gson.reflect.TypeToken;

/**
 * 
 * <p>
 * ----------------------------------------------------------------------------- <br>
 * 工程名 ： InformationSer <br>
 * 功能： <br>
 * 描述： <br>
 * 授权 : (C) Copyright (c) 2011 <br>
 * 公司 : 北京中交兴路信息科技有限公司 <br>
 * ----------------------------------------------------------------------------- <br>
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
 * <td>Dec 22, 2011</td>
 * <td>DEVELOPER</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author DEVELOPER
 * @since JDK1.6
 */
public class VehicleInfoServiceImpl extends RemoteJavaServiceRmiAbstract implements VehicleInfoService {

	private static Log log = LogFactory.getLog(VehicleInfoServiceImpl.class);
	private VehicleInfoDao vehicleBaseInfoDao;
	protected RedisDaoSupport writeJedisDao; // redis服务-主
	protected RedisDaoSupport readJedisDao; // redis服务-从
	private String isCheckVehicleAndVin; // 是否校验车牌号码以及VIN号码 true：校验 false：不校验
	private String isAllowRepeatRegister; // 是否车机允许重复注册 true：允许 false：不允许
	/* 注册鉴权方式 （1：正常鉴权 2：只通过手机号鉴权） */
	private String registerType;

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.VehicleInfoServiceRmi# getRegVehicleInfo(com.ctfo.local.obj.DynamicSqlParameter)
	 */
	@Override
	public String getRegVehicleInfo(DynamicSqlParameter param, String bKey) {
		log.info("获取车辆信息请求开始执行。");
		try {
			VehicleInfo vehicleInfo = null;
			if (param != null && param.getEqual() != null) {
				log.info("请求查询参数：" + param.getEqual().toString());
				vehicleInfo = vehicleBaseInfoDao.getRegVehicleInfo(param);
				String response = XMLParse.getResponse(param.getOutputValue(), vehicleInfo, bKey).asXML();
				response = response.replace("<Item/>", "<Item result=\"Interface is disabled\"/>");
				log.info("获取车辆信息请求响应结果为：\n" + response);
				log.info("获取车辆信息请求执行结束。");
				return response;
			} else {
				vehicleInfo = new VehicleInfo();
				return XMLParse.getResponse(param.getOutputValue(), new VehicleInfo(), bKey).asXML();
			}
		} catch (Exception e) {
			log.error("获取车辆信息异常，异常信息" + e.getMessage(), e);
			return XMLParse.getResponse(param.getOutputValue(), -1, bKey).asXML();
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.VehicleInfoService# getBaseVehicleInfo(com.ctfo.local.obj.DynamicSqlParameter, java.lang.String)
	 */
	@Override
	public String getBaseVehicleInfo(DynamicSqlParameter param, String bKey) {
		log.info("获取车辆基本信息请求开始执行。");
		try {
			VehicleInfo vehicleInfo = null;
			if (param != null && param.getEqual() != null) {
				vehicleInfo = vehicleBaseInfoDao.getBaseVehicleInfo(param);
				String response = XMLParse.getResponse(param.getOutputValue(), vehicleInfo, bKey).asXML();
				
				log.info("获取车辆基本信息请求响应结果为：\n" + response);
				log.info("获取车辆基本信息请求执行结束。");
				return response;
			} else {
				vehicleInfo = new VehicleInfo();
				return XMLParse.getResponse(param.getOutputValue(), new VehicleInfo(), bKey).asXML();
			}
		} catch (Exception e) {
			log.error("获取车辆基本信息请求异常，异常信息" + e.getMessage());
			return XMLParse.getResponse(param.getOutputValue(), -1, bKey).asXML();
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.VehicleInfoService# getDriverOfVehicleByType(com.ctfo.local.obj.DynamicSqlParameter, java.lang.String)
	 */
	@Override
	public String getDriverOfVehicleByType(DynamicSqlParameter param, String bKey) {
		log.info("获取车辆驾驶员信息请求开始执行。");
		try {
			VehicleInfo vehicleInfo = null;
			if (param != null && param.getEqual() != null) {
				vehicleInfo = vehicleBaseInfoDao.getDriverOfVehicleByType(param);
				String response = XMLParse.getResponse(param.getOutputValue(), vehicleInfo, bKey).asXML();
				log.info("获取车辆驾驶员信息请求响应结果为：\n" + response);
				log.info("获取车辆驾驶员信息请求执行结束。");
				return response;
			} else {
				vehicleInfo = new VehicleInfo();
				return XMLParse.getResponse(param.getOutputValue(), new VehicleInfo(), bKey).asXML();
			}
		} catch (Exception e) {
			log.error("获取车辆驾驶员信息请求异常，异常信息" + e.getMessage());
			return XMLParse.getResponse(param.getOutputValue(), -1, bKey).asXML();
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.VehicleInfoService# getEticketByVehicle(com.ctfo.local.obj.DynamicSqlParameter, java.lang.String)
	 */
	@Override
	public String getEticketByVehicle(DynamicSqlParameter param, String bKey) {
		log.info("获取车辆电子运单信息请求开始执行。");
		try {
			VehicleInfo vehicleInfo = null;
			if (param != null && param.getEqual() != null) {
				vehicleInfo = vehicleBaseInfoDao.getEticketByVehicle(param);
				String response = XMLParse.getResponse(param.getOutputValue(), vehicleInfo, bKey).asXML();
				log.info("获取车辆电子运单信息请求响应结果为：\n" + response);
				log.info("获取车辆电子运单信息请求执行结束。");
				return response;
			} else {
				vehicleInfo = new VehicleInfo();
				return XMLParse.getResponse(param.getOutputValue(), new VehicleInfo(), bKey).asXML();
			}
		} catch (Exception e) {
			log.error("获取车辆电子运单信息请求异常，异常信息" + e.getMessage());
			return XMLParse.getResponse(param.getOutputValue(), -1, bKey).asXML();
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.VehicleInfoService# getTernimalByVehicleByType(com.ctfo.local.obj.DynamicSqlParameter, java.lang.String)
	 */
	@Override
	public String getTernimalByVehicleByType(DynamicSqlParameter param, String bKey) {
		log.info("获取车辆终端信息请求开始执行。");
		try {
			VehicleInfo vehicleInfo = null;
			if (param != null && param.getEqual() != null) {
				vehicleInfo = vehicleBaseInfoDao.getTernimalByVehicleByType(param);
				String response = XMLParse.getResponse(param.getOutputValue(), vehicleInfo, bKey).asXML();
				log.info("获取车辆终端信息请求响应结果为：\n" + response);
				log.info("获取车辆终端信息请求执行结束。");
				return response;
			} else {
				vehicleInfo = new VehicleInfo();
				return XMLParse.getResponse(param.getOutputValue(), new VehicleInfo(), bKey).asXML();
			}
		} catch (Exception e) {
			log.error("获取车辆终端信息请求异常，异常信息" + e.getMessage());
			return XMLParse.getResponse(param.getOutputValue(), -1, bKey).asXML();
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.VehicleInfoService# isDriverOfVehicle(com.ctfo.local.obj.DynamicSqlParameter, java.lang.String)
	 */
	@Override
	public String isDriverOfVehicle(DynamicSqlParameter param, String bKey) {
		log.info("驾驶员身份验证请求开始执行。");
		try {
			VehicleInfo vehicleInfo = new VehicleInfo();
			vehicleInfo.setResult(-1);
			vehicleInfo.setMessage("驾驶员身份识别失败");
			Long count = vehicleBaseInfoDao.isDriverOfVehicle(param);
			if (count != 0) {
				vehicleInfo.setResult(0);
				vehicleInfo.setMessage("驾驶员身份识别成功");
			}
			String response = XMLParse.getResponse(param.getOutputValue(), vehicleInfo, bKey).asXML();
			log.info("获取车辆终端信息请求响应结果为：\n" + response);
			log.info("驾驶员身份验证请求执行结束。");
			return response;
		} catch (Exception e) {
			log.error("驾驶员身份验证请求异常，异常信息：" + e.getMessage());
			return XMLParse.getResponse(param.getOutputValue(), -1, bKey).asXML();
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.VehicleInfoService#isRegVehicle (com.ctfo.local.obj.DynamicSqlParameter, java.lang.String)
	 */
	@Override
	public String isRegVehicle(DynamicSqlParameter parameter, String bKey) throws Exception {
		log.info("获取车辆基本信息请求开始执行。");
		try {
			VehicleInfo vehicleInfo = null;
			String color = null;
			if (parameter != null && parameter.getEqual() != null) {
				// 正常注册
				if (registerType.equals("1")) {
					Map<String, String> paramEqual = parameter.getEqual();
					color = paramEqual.get("plateColor");
					if ("true".equals(isCheckVehicleAndVin)) {
						paramEqual.put("isCheckVehicleAndVin", "true");
					} else {
						paramEqual.remove("isCheckVehicleAndVin");
					}
					parameter.setEqual(paramEqual);
					if ("0".equals(color)) {
						// 对于车牌颜色为0的车辆，传过来的车牌号码其实是VIN号，查询SQL时用VIN比对
						vehicleInfo = vehicleBaseInfoDao.getAllBaseInfoByVIN(parameter);
					} else {
						// 正常流程
						vehicleInfo = vehicleBaseInfoDao.getAllBaseInfo(parameter);
					}
					// 通过手机号鉴权
				} else if (registerType.equals("2")) {
					vehicleInfo = vehicleBaseInfoDao.getAllBaseInfoByPhoneNumber(parameter);
				} else {
					// 提示：鉴权类型未设置
					vehicleInfo = new VehicleInfo();
					vehicleInfo.setResult(5);
					return this.returnForReg(parameter, vehicleInfo, bKey);
				}
				if (vehicleInfo == null) {
					// 数据库无该车辆
					vehicleInfo = new VehicleInfo();
					vehicleInfo.setResult(2);
					return this.returnForReg(parameter, vehicleInfo, bKey);
				} else {
					if (vehicleInfo.getVid() == null || !"2".equals(vehicleInfo.getVehicleState())) {
						// 数据库无该车辆
						vehicleInfo.setResult(2);
						return this.returnForReg(parameter, vehicleInfo, bKey);
					} else {
						if ("0".equals(vehicleInfo.getVehicleRegStatus())) {
							// 车辆已注册
							vehicleInfo.setResult(1);

							/*
							 * 为测试注册鉴权流程将重复注册去掉，2012.5.31 杨晓光修改
							 */
							if ("false".equals(isAllowRepeatRegister))// false不允许重复注册
							{
								return this.returnForReg(parameter, vehicleInfo, bKey);
							}

						}
					}
					if (vehicleInfo.getTid() == null || !"2".equals(vehicleInfo.getTerState())) {
						// 数据库无该终端
						vehicleInfo.setResult(4);
						return this.returnForReg(parameter, vehicleInfo, bKey);
					} else {
						if ("0".equals(vehicleInfo.getTerRegStatus())) {
							// 终端已注册
							vehicleInfo.setResult(3);

							/*
							 * 为测试注册鉴权流程将重复注册去掉，2012.5.31 杨晓光修改
							 */
							if ("false".equals(isAllowRepeatRegister)) // false不允许重复注册
							{
								return this.returnForReg(parameter, vehicleInfo, bKey);
							}
						}
					}
					/*
					 * 为测试注册鉴权流程将重复注册去掉，2012.5.31 杨晓光修改
					 */
					if ("true".equals(isAllowRepeatRegister)) // isAllowRepeatRegister为true时，允许重复注册
					{
						if ("0".equals(vehicleInfo.getTerRegStatus()) && "0".equals(vehicleInfo.getVehicleRegStatus())) {
							log.fatal("业务号：" + bKey + "-国标协议-车辆[" + parameter.getEqual().get("vehicleNo") + "]终端[" + parameter.getEqual().get("tmac") + "]卡号[" + parameter.getEqual().get("commaddr") + "]重复注册，状态为：[" + vehicleInfo.getResult() + "]");
							vehicleInfo.setResult(0);
						}
					}

					if (vehicleInfo.getSid() == null) {
						// 数据库无该终端
						vehicleInfo.setResult(4);
						return this.returnForReg(parameter, vehicleInfo, bKey);
					}

					/*
					 * 为测试注册鉴权流程将重复注册去掉，2012.5.31 杨晓光修改
					 */
					if ("true".equals(isAllowRepeatRegister)) // isAllowRepeatRegister为true时，允许重复注册
					{
						// 如果车辆注册结果不为0（成功）时则直接Return
						if (vehicleInfo.getResult() != null && vehicleInfo.getResult() != 0) {
							vehicleInfo.setOemcode("");
							vehicleInfo.setAkey("");
							String response = XMLParse.getResponse(parameter.getOutputValue(), vehicleInfo, bKey).asXML();
							log.info("获取车辆注册信息请求响应结果为：\n" + response);
							log.info("获取车辆注册信息请求执行结束。");
							return response;
						}
					}

					DynamicSqlParameter countParam = new DynamicSqlParameter();
					Map<String, String> map = new HashMap<String, String>();
					map.put("tid", vehicleInfo.getTid());
					map.put("vid", vehicleInfo.getVid());
					map.put("sid", vehicleInfo.getSid());
					countParam.setEqual(map);
					Long count = vehicleBaseInfoDao.getCountForServiceunit(countParam);
					if (count != 0) {
						vehicleInfo.setResult(0);
						DynamicSqlParameter update = new DynamicSqlParameter();
						Map<String, Object> updateValue = new HashMap<String, Object>();
						updateValue.put("regStatus", "0");// 注册成功
						updateValue.put("updateTime", System.currentTimeMillis());
						Map<String, String> equal = new HashMap<String, String>();
						equal.put("tid", vehicleInfo.getTid());
						equal.put("vid", vehicleInfo.getVid());
						update.setEqual(equal);
						update.setUpdateValue(updateValue);
						vehicleBaseInfoDao.modifyByRegStatus(update);
						// 将注册数据存入Memcache
						vehicleInfo.setCommaddr(parameter.getEqual().get("commaddr"));
						addToMemcache(vehicleInfo);
					} else {
						// 数据库无该终端
						vehicleInfo.setOemcode("");
						vehicleInfo.setAkey("");
						vehicleInfo.setResult(4);
					}
				}
				String response = XMLParse.getResponse(parameter.getOutputValue(), vehicleInfo, bKey).asXML();
				log.info("获取车辆注册信息请求响应结果为：\n" + response);
				log.info("获取车辆注册信息请求执行结束。");

				return response;
			} else {
				vehicleInfo = new VehicleInfo();
				return XMLParse.getResponse(parameter.getOutputValue(), new VehicleInfo(), bKey).asXML();
			}
		} catch (Exception e) {
			// e.printStackTrace();
			// log.error("获取车辆注册信息请求异常，异常信息：" + e.getMessage());
			// // 事物手动回滚
			// SpringBUtils.rollBack();
			// return XMLParse.getResponse(parameter.getOutputValue(), -1,
			// bKey).asXML();
			throw e;
		}
	}

	// 非国标注册流程，当车机，车，卡，终端注册状态为已注册时返回鉴权码和OEMCODE
	@Override
	public String isRegVehicleNOGB(DynamicSqlParameter parameter, String bKey) throws Exception {
		log.info("获取非国标车辆基本信息请求开始执行。");
		try {

			Map<String, String> paramEqual = parameter.getEqual();

			String color = paramEqual.get("plateColor");

			if ("true".equals(isCheckVehicleAndVin)) {
				paramEqual.put("isCheckVehicleAndVin", "true");
			} else {
				paramEqual.remove("isCheckVehicleAndVin");
			}
			parameter.setEqual(paramEqual);

			VehicleInfo vehicleInfo = null;
			if (parameter != null && parameter.getEqual() != null) {

				if ("0".equals(color)) {
					// 对于车牌颜色为0的车辆，传过来的车牌号码其实是VIN号，查询SQL时用VIN比对
					vehicleInfo = vehicleBaseInfoDao.getAllBaseInfoByVIN(parameter);

				} else {
					// 正常流程
					vehicleInfo = vehicleBaseInfoDao.getAllBaseInfo(parameter);
				}

				if (vehicleInfo == null) {
					// 数据库无该车辆
					vehicleInfo = new VehicleInfo();
					vehicleInfo.setResult(2);
				} else {
					if (vehicleInfo.getVid() == null || !"2".equals(vehicleInfo.getVehicleState())) {
						// 数据库无该车辆
						vehicleInfo.setResult(2);
					} else {
						if ("0".equals(vehicleInfo.getVehicleRegStatus())) {
							// 车辆已注册
							vehicleInfo.setResult(1);
						}
					}
					if (vehicleInfo.getTid() == null || !"2".equals(vehicleInfo.getTerState())) {
						// 数据库无该终端
						vehicleInfo.setResult(4);
					} else {
						if ("0".equals(vehicleInfo.getTerRegStatus())) {
							// 终端已注册
							vehicleInfo.setResult(3);
						}
					}
					if (vehicleInfo.getSid() == null) {
						// 数据库无该终端
						vehicleInfo.setResult(4);
					}
					// 如果车辆注册结果不为0（成功）时则直接Return
					if (vehicleInfo.getResult() != null && vehicleInfo.getResult() != 0) {
						String response = XMLParse.getResponse(parameter.getOutputValue(), vehicleInfo, bKey).asXML();
						log.info("获取非国标车辆注册信息请求响应结果为：\n" + response);
						log.info("获取非国标车辆注册信息请求执行结束。");
						return response;
					}
					DynamicSqlParameter countParam = new DynamicSqlParameter();
					Map<String, String> map = new HashMap<String, String>();
					map.put("tid", vehicleInfo.getTid());
					map.put("vid", vehicleInfo.getVid());
					map.put("sid", vehicleInfo.getSid());
					countParam.setEqual(map);
					Long count = vehicleBaseInfoDao.getCountForServiceunit(countParam);
					if (count != 0) {
						vehicleInfo.setResult(0);
						DynamicSqlParameter update = new DynamicSqlParameter();
						Map<String, Object> updateValue = new HashMap<String, Object>();
						updateValue.put("regStatus", "0");// 注册成功
						updateValue.put("updateTime", System.currentTimeMillis());
						Map<String, String> equal = new HashMap<String, String>();
						equal.put("tid", vehicleInfo.getTid());
						equal.put("vid", vehicleInfo.getVid());
						update.setEqual(equal);
						update.setUpdateValue(updateValue);
						vehicleBaseInfoDao.modifyByRegStatus(update);
						// 将注册数据存入Memcache
						vehicleInfo.setCommaddr(parameter.getEqual().get("commaddr"));
						addToMemcache(vehicleInfo);
					} else {
						// 数据库无该终端
						vehicleInfo.setOemcode("");
						vehicleInfo.setAkey("");
						vehicleInfo.setResult(4);
					}
				}
				String response = XMLParse.getResponse(parameter.getOutputValue(), vehicleInfo, bKey).asXML();
				log.info("获取非国标车辆注册信息请求响应结果为：\n" + response);
				log.info("获取非国标车辆注册信息请求执行结束。");

				return response;
			} else {
				vehicleInfo = new VehicleInfo();
				return XMLParse.getResponse(parameter.getOutputValue(), new VehicleInfo(), bKey).asXML();
			}
		} catch (Exception e) {
			// e.printStackTrace();
			// log.error("获取车辆注册信息请求异常，异常信息：" + e.getMessage());
			// // 事物手动回滚
			// SpringBUtils.rollBack();
			// return XMLParse.getResponse(parameter.getOutputValue(), -1,
			// bKey).asXML();
			throw e;
		}
	}

	@Override
	@Deprecated
	public String isCheckVehicle(DynamicSqlParameter parameter, String bKey) {
		log.info("车辆鉴权请求开始执行。");
		VehicleInfo vehicleInfo = null;
		// 调用Memcatch
//		Map<String, String> map = new HashMap<String, String>();
//		map = parameter.getEqual();
		try {
//			vehicleInfo = memcachedClient.get("PCC_" + map.get("commaddr"));
			// String vehicleStr = jedisService.get(0, "PCC_" + map.get("commaddr"));
			// vehicleInfo = (VehicleInfo) RedisJsonUtil.jsonToObject(vehicleStr, new TypeToken<VehicleInfo>(){});
			// 如果在mem中存在记录
//			if (vehicleInfo != null) {
//				vehicleInfo.setResult(1);
//				// 判断parameter中的akey是否跟memcache中的一致
//				if (vehicleInfo.getAkey().equals(map.get("akey"))) {
//					log.info("获取车辆鉴权 终端SIM[" + map.get("commaddr") + "]业务序号为[" + bKey + "]鉴权成功");
//					vehicleInfo.setResult(0);
//				} else {
//					log.info("获取车辆鉴权 终端SIM[" + map.get("commaddr") + "]业务序号为[" + bKey + "]鉴权码不匹配!原始鉴权码[" + map.get("akey") + "] 请求鉴权码[" + vehicleInfo.getAkey() + "]");
//					vehicleInfo.setAkey("");
//				}
//			} else {// mem中不存在时不能返回鉴权码
//				log.info("获取车辆鉴权 终端SIM[" + map.get("commaddr") + "]业务序号为[" + bKey + "]缓存服务器中数据不存在!");
//				vehicleInfo = new VehicleInfo();
//				vehicleInfo.setAkey("");
//				vehicleInfo.setResult(1);
//			}
			log.error("车辆鉴权请求执行结束。接口已停用!");
			String response = XMLParse.getResponse(parameter.getOutputValue(), vehicleInfo, bKey).asXML();
			response = response.replace("<Item/>", "<Item result=\"Interface is disabled\"/>");
			log.error("车辆鉴权请求响应结果为：\n" + response);
			return response;
		} catch (CtfoAppException e) {
			vehicleInfo = isCheckVehicleInfoByDb(vehicleInfo, parameter);
			log.info("车辆鉴权请求执行结束。");
			String response = XMLParse.getResponse(parameter.getOutputValue(), vehicleInfo, bKey).asXML();
			log.info("车辆鉴权请求响应结果为：\n" + response);
			return response;
		} catch (Exception e) {
			vehicleInfo = isCheckVehicleInfoByDb(vehicleInfo, parameter);
			log.info("车辆鉴权请求执行结束。");
			String response = XMLParse.getResponse(parameter.getOutputValue(), vehicleInfo, bKey).asXML();
			log.info("车辆鉴权请求响应结果为：\n" + response);
			return response;
		}

	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.VehicleInfoService#isLogoffVehicle (com.ctfo.local.obj.DynamicSqlParameter, java.lang.String) 车辆注销
	 */
	@Override
	// public String isLogoffVehicle(DynamicSqlParameter parameter, String bKey) {
	// log.info("车辆注销请求开始执行。");
	// VehicleInfo vehicle = null;
	// // 调用Memcatch，在memcathc中删除注册车辆
	// Map<String, String> map = new HashMap<String, String>();
	// try {
	// map = parameter.getEqual();
	// // 获取Memcached中的对象数据
	// vehicle = memcachedClient.get("PCC_" + map.get("commaddr"));
	// // String vehicleStr = jedisService.get(0, "PCC_" + map.get("commaddr"));
	// // vehicle = (VehicleInfo) RedisJsonUtil.jsonToObject(vehicleStr, new TypeToken<VehicleInfo>() {});
	// if (vehicle != null) {
	// map.put("vid", vehicle.getVid());
	// map.put("tid", vehicle.getTid());
	// Map<String, Object> updateValue = new HashMap<String, Object>();
	// // 设置表注册状态为未注册
	// updateValue.put("regStatus", "-1");
	// updateValue.put("updateTime", System.currentTimeMillis() / 1000);
	// parameter.setUpdateValue(updateValue);
	// memcachedClient.delete("PCC_" + map.get("commaddr"));
	// // jedisService.del(0, "PCC_" + map.get("commaddr"));
	// // 更新车辆表及终端表的注册状态为未注册
	// vehicleBaseInfoDao.modifyByRegStatus(parameter);
	// vehicle.setResult(0);
	// } else {
	// vehicle = isLogoffVehicleInfoByDb(vehicle, parameter);
	// }
	// log.info("车辆注销请求执行结束。");
	// String response = XMLParse.getResponse(parameter.getOutputValue(), vehicle, bKey).asXML();
	// log.info("车辆鉴权请求响应结果为：\n" + response);
	// return response;
	// } catch (CtfoAppException e) {
	// log.info("车辆注销请求执行结束。");
	// vehicle = isLogoffVehicleInfoByDb(vehicle, parameter);
	// String response = XMLParse.getResponse(parameter.getOutputValue(), vehicle, bKey).asXML();
	// log.info("车辆鉴权请求响应结果为：\n" + response);
	// return response;
	// } catch (Exception e) {
	// log.info("车辆注销请求执行结束。");
	// vehicle = isLogoffVehicleInfoByDb(vehicle, parameter);
	// String response = XMLParse.getResponse(parameter.getOutputValue(), vehicle, bKey).asXML();
	// log.info("车辆鉴权请求响应结果为：\n" + response);
	// return response;
	// }
	// }
	@Deprecated
	public String isLogoffVehicle(DynamicSqlParameter parameter, String bKey) {
		log.info("车辆注销请求开始执行。");
		VehicleInfo vehicle = null;
		// 调用Memcatch，在memcathc中删除注册车辆
//		Map<String, String> map = new HashMap<String, String>();
		try {
//			map = parameter.getEqual();
			// 获取Memcached中的对象数据
//			vehicle = memcachedClient.get("PCC_" + map.get("commaddr"));
			// String vehicleStr = jedisService.get(0, "PCC_" + map.get("commaddr"));
			// vehicle = (VehicleInfo) RedisJsonUtil.jsonToObject(vehicleStr, new TypeToken<VehicleInfo>(){});
//			if (vehicle != null) {
//				map.put("vid", vehicle.getVid());
//				map.put("tid", vehicle.getTid());
//				Map<String, Object> updateValue = new HashMap<String, Object>();
//				// 设置表注册状态为未注册
//				updateValue.put("regStatus", "-1");
//				updateValue.put("updateTime", System.currentTimeMillis() / 1000);
//				parameter.setUpdateValue(updateValue);
////				memcachedClient.delete("PCC_" + map.get("commaddr"));
//				// jedisService.del(0, "PCC_" + map.get("commaddr"));
//				// 更新车辆表及终端表的注册状态为未注册
//				vehicleBaseInfoDao.modifyByRegStatus(parameter);
//				vehicle.setResult(0);
//			} else {
				vehicle = isLogoffVehicleInfoByDb(vehicle, parameter);
//			}
			log.info("车辆注销请求执行结束。接口已停用");
			String response = XMLParse.getResponse(parameter.getOutputValue(), vehicle, bKey).asXML();
			response = response.replace("<Item/>", "<Item result=\"Interface is disabled\"/>");
			log.info("车辆鉴权请求响应结果为：\n" + response);
			return response;
		} catch (CtfoAppException e) {
			log.info("车辆注销请求执行结束。");
			vehicle = isLogoffVehicleInfoByDb(vehicle, parameter);
			String response = XMLParse.getResponse(parameter.getOutputValue(), vehicle, bKey).asXML();
			response = response.replace("<Item/>", "<Item result=\"Interface is disabled\"/>");
			log.info("车辆鉴权请求响应结果为：\n" + response);
			return response;
		} catch (Exception e) {
			log.info("车辆注销请求执行结束。");
			vehicle = isLogoffVehicleInfoByDb(vehicle, parameter);
			String response = XMLParse.getResponse(parameter.getOutputValue(), vehicle, bKey).asXML();
			response = response.replace("<Item/>", "<Item result=\"Interface is disabled\"/>");
			log.info("车辆鉴权请求响应结果为：\n" + response);
			return response;
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.VehicleInfoService# getDetailOfVehicleInfo(com.ctfo.local.obj.DynamicSqlParameter, java.lang.String)
	 */
	@Override
	public String getDetailOfVehicleInfo(DynamicSqlParameter param, String bKey) {
		log.info("获取车辆详细信息请求开始执行。");
		try {
			VehicleInfo vehicleInfo = null;
			if (param != null && param.getEqual() != null) {
				vehicleInfo = vehicleBaseInfoDao.getDetailOfVehicleInfo(param);
				String response = XMLParse.getResponse(param.getOutputValue(), vehicleInfo, bKey).asXML();
				log.info("获取车辆详细信息请求响应结果为：\n" + response);
				log.info("获取车辆详细信息请求执行结束。");
				return response;
			} else {
				vehicleInfo = new VehicleInfo();
				return XMLParse.getResponse(param.getOutputValue(), new VehicleInfo(), bKey).asXML();
			}
		} catch (Exception e) {
			log.error("获取车辆详细信息请求异常，异常信息：" + e.getMessage());
			return XMLParse.getResponse(param.getOutputValue(), -1, bKey).asXML();
		}
	}

	/**
	 * 车辆鉴权从库表中查询数据比对
	 * 
	 * @param vehicleInfo
	 * @param parameter
	 * @return
	 */
	public VehicleInfo isCheckVehicleInfoByDb(VehicleInfo vehicleInfo, DynamicSqlParameter parameter) {
		try {
			vehicleInfo.setResult(1);
			vehicleInfo = vehicleBaseInfoDao.getAkeyVehicleInfo(parameter);
			Map<String, String> map = new HashMap<String, String>();
			map = parameter.getEqual();
			// 判断parameter中的akey是否跟DB中的一致
			if (vehicleInfo.getAkey().equals(map.get("akey"))) {
				vehicleInfo.setResult(0);
			} else {
				vehicleInfo.setAkey("");
			}
			return vehicleInfo;
		} catch (Exception e) {
			log.error("车辆鉴权请求Db未能获取数据" + e.getMessage());
			VehicleInfo vehicleInfos = new VehicleInfo();
			vehicleInfos.setResult(-1);
			vehicleInfos.setAkey("");
			return vehicleInfos;
		}
	}

	/**
	 * 车辆注销，更新数据库表状态
	 * 
	 * @param vehicleInfo
	 * @param parameter
	 * @return
	 */
	@Deprecated
	public VehicleInfo isLogoffVehicleInfoByDb(VehicleInfo vehicleInfo, DynamicSqlParameter parameter) {
		Map<String, String> map = new HashMap<String, String>();
		try {
			vehicleInfo = vehicleBaseInfoDao.getAllIdForLogoff(parameter);
			if (vehicleInfo != null) {
				map = parameter.getEqual();
				map.put("vid", vehicleInfo.getVid());
				map.put("tid", vehicleInfo.getTid());
				Map<String, Object> updateValue = new HashMap<String, Object>();
				// 设置表注册状态为未注册
				updateValue.put("regStatus", "-1");
				updateValue.put("updateTime", System.currentTimeMillis() / 1000);
				parameter.setUpdateValue(updateValue);
				// 更新车辆表及终端表的注册状态为未注册
				vehicleBaseInfoDao.modifyByRegStatus(parameter);
			} else {
				vehicleInfo = new VehicleInfo();
			}
			vehicleInfo.setResult(0);
			return vehicleInfo;
		} catch (Exception e) {
			log.error("车辆注销请求Db未能获取数据" + e.getMessage());
			VehicleInfo vehicleInfos = new VehicleInfo();
			vehicleInfos.setResult(-1);
			return vehicleInfos;
		}
	}

	/**
	 * 车辆注册结果添加到缓存中
	 * 
	 * @param vehicleInfo
	 * 
	 */
	@Deprecated
	public void addToMemcache(VehicleInfo vehicleInfo) throws Exception {
//		StringBuffer key = new StringBuffer().append("PCC_").append(vehicleInfo.getCommaddr());
		try {
//			this.memcachedClient.set(key.toString(), 0, vehicleInfo);
			// jedisService.set(0, key.toString(),RedisJsonUtil.objectToJson(vehicleInfo));
		} catch (Exception e) {
			throw e;
		}
	}

	/**
	 * 返回注册结果
	 * 
	 * @param parameter
	 *            动态参数
	 * @param vehicleInfo
	 *            车辆信息
	 * @param bKey
	 *            业务号
	 * @return
	 */
	private String returnForReg(DynamicSqlParameter parameter, VehicleInfo vehicleInfo, String bKey) {
		try {
			vehicleInfo.setOemcode("");
			vehicleInfo.setAkey("");
			String response = XMLParse.getResponse(parameter.getOutputValue(), vehicleInfo, bKey).asXML();
			log.info("获取车辆注册信息请求响应结果为：\n" + response);
			log.info("获取车辆注册信息请求执行结束。");
			return response;
		} catch (Exception e) {
			log.info("获取车辆注册信息请求响应异常：\n" + e.getMessage());
			XMLParse.getResponse(parameter.getOutputValue(), -1, bKey).asXML();
		}
		return "";
	}

	/**
	 * 根据3G卡号获取2G卡号
	 * 
	 * @param parameter
	 * @param vehicleInfo
	 * @param bKey
	 * @return
	 */
	@Override
	public String get2gBy3g(DynamicSqlParameter param, String bKey) throws CtfoAppException {
		log.info("获取车辆基本信息请求开始执行。");
		try {
			VehicleInfo vehicleInfo = null;
			if (param != null && param.getEqual() != null) {
				vehicleInfo = vehicleBaseInfoDao.get2gBy3g(param);
				String response = XMLParse.getResponse(param.getOutputValue(), vehicleInfo, bKey).asXML();
				log.info("获取车辆基本信息请求响应结果为：\n" + response + "\n获取车辆基本信息请求执行结束。");
				return response;
			} else {
				log.warn("获取车辆基本信息请求参数异常！");
				return XMLParse.getResponse(param.getOutputValue(), -1, bKey).asXML();
			}
		} catch (Exception e) {
			log.error("获取车辆基本信息请求异常，异常信息" + e.getMessage());
			return XMLParse.getResponse(param.getOutputValue(), -1, bKey).asXML();
		}
	}

	/**
	 * 根据2G卡号获取3G卡号
	 * 
	 * @param parameter
	 * @param vehicleInfo
	 * @param bKey
	 * @return
	 */
	@Override
	public String get3gBy2g(DynamicSqlParameter param, String bKey) throws CtfoAppException {
		log.info("获取车辆基本信息请求开始执行。");
		try {
			VehicleInfo vehicleInfo = null;
			if (param != null && param.getEqual() != null) {
				vehicleInfo = vehicleBaseInfoDao.get3gBy2g(param);
				String response = XMLParse.getResponse(param.getOutputValue(), vehicleInfo, bKey).asXML();
				log.info("获取车辆基本信息请求响应结果为：\n" + response + "\n获取车辆基本信息请求执行结束。");
				return response;
			} else {
				log.warn("获取车辆基本信息请求参数异常！");
				return XMLParse.getResponse(param.getOutputValue(), -1, bKey).asXML();
			}
		} catch (Exception e) {
			log.error("获取车辆基本信息请求异常，异常信息" + e.getMessage());
			return XMLParse.getResponse(param.getOutputValue(), -1, bKey).asXML();
		}
	}

	@Override
	/**
	 * 获得2g3gSim卡号映射表
	 * @param parameter
	 * @param vehicleInfo
	 * @param bKey
	 * @return
	 */
	public String get2g3gSimMapping(DynamicSqlParameter parameter, String key) {
		log.info("获取车辆基本信息请求开始执行。");
		try {
			List<VehicleInfo> vehicleInfo = null;
			if (parameter != null && parameter.getEqual() != null) {
				vehicleInfo = vehicleBaseInfoDao.get2g3gSimMapping();
				String response = XMLParse.getElementString(vehicleInfo, key);
				log.info("获取车辆基本信息请求响应结果为：\n" + response + "\n获取车辆基本信息请求执行结束。");
				return response;
			} else {
				log.warn("获取车辆基本信息请求参数异常！");
				return XMLParse.getResponse(parameter.getOutputValue(), -1, key).asXML();
			}
		} catch (Exception e) {
			log.error("获取车辆基本信息请求异常，异常信息" + e.getMessage());
			return XMLParse.getResponse(parameter.getOutputValue(), -1, key).asXML();
		}
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.monitoring.service.VehicleInfoService#isRegVehicleNOGBNew(com.ctfo.local.obj.DynamicSqlParameter, java.lang.String)
	 */
	@Override
	public String isRegVehicleNOGBNew(DynamicSqlParameter param, String bKey) throws Exception {
		log.info("获取车辆基本信息请求开始执行。");
		try {
			VehicleInfo vehicleInfo = null;
			if (param != null && param.getEqual() != null) {
				// 先从缓存取车辆注册信息
				vehicleInfo = this.getVehicleInfo(param);
				if (vehicleInfo == null) {
					vehicleInfo = vehicleBaseInfoDao.getAllBaseInfoByTmac(param);
					if (vehicleInfo == null) {
						// 数据库无该车辆
						vehicleInfo = new VehicleInfo();
						vehicleInfo.setResult(2);
					} else {
						if (vehicleInfo.getVid() == null || !"2".equals(vehicleInfo.getVehicleState())) {
							// 数据库无该车辆
							vehicleInfo.setResult(2);
						} else {
							if ("0".equals(vehicleInfo.getVehicleRegStatus())) {
								// 车辆已注册
								vehicleInfo.setResult(1);
							}
						}
						if (vehicleInfo.getTid() == null || !"2".equals(vehicleInfo.getTerState())) {
							// 数据库无该终端
							vehicleInfo.setResult(4);
						} else {
							if ("0".equals(vehicleInfo.getTerRegStatus())) {
								// 终端已注册
								vehicleInfo.setResult(3);
							}
						}
						if (vehicleInfo.getSid() == null) {
							// 数据库无该终端
							vehicleInfo.setResult(4);
						}

						DynamicSqlParameter countParam = new DynamicSqlParameter();
						Map<String, String> map = new HashMap<String, String>();
						map.put("tid", vehicleInfo.getTid());
						map.put("vid", vehicleInfo.getVid());
						map.put("sid", vehicleInfo.getSid());
						countParam.setEqual(map);
						Long count = vehicleBaseInfoDao.getCountForServiceunit(countParam);
						if (count != 0) {
							vehicleInfo.setResult(0);
							DynamicSqlParameter update = new DynamicSqlParameter();
							Map<String, Object> updateValue = new HashMap<String, Object>();
							updateValue.put("regStatus", "0");// 注册成功
							updateValue.put("updateTime", System.currentTimeMillis());
							Map<String, String> equal = new HashMap<String, String>();
							equal.put("tid", vehicleInfo.getTid());
							equal.put("vid", vehicleInfo.getVid());
							update.setEqual(equal);
							update.setUpdateValue(updateValue);
							vehicleBaseInfoDao.modifyByRegStatus(update);
							// 将注册数据存入redis
							if (param.getEqual().get("commaddr") != null && !param.getEqual().get("commaddr").equals("")) {
								vehicleInfo.setCommaddr(param.getEqual().get("commaddr"));
							}
							this.addToRedis(vehicleInfo);
						} else {
							// 数据库无该终端
							vehicleInfo.setOemcode("");
							vehicleInfo.setAkey("");
							vehicleInfo.setResult(4);
						}
					}
				}
				String response = XMLParse.getResponse(param.getOutputValue(), vehicleInfo, bKey).asXML();
				log.info("获取车辆注册信息请求响应结果为：\n" + response);
				log.info("获取车辆注册信息请求执行结束。");

				return response;
			} else {
				vehicleInfo = new VehicleInfo();
				return XMLParse.getResponse(param.getOutputValue(), new VehicleInfo(), bKey).asXML();
			}
		} catch (Exception e) {
			throw e;
		}
	}

	/**
	 * 车辆注册结果添加到redis缓存中
	 * 
	 * @param vehicleInfo
	 * @throws Exception
	 */
	private void addToRedis(VehicleInfo vehicleInfo) throws Exception {
		StringBuffer key = new StringBuffer().append("PCC_").append(vehicleInfo.getTmac());
		try {
			String json = RedisJsonUtil.objectToJson(vehicleInfo);
			this.writeJedisDao.setAuthenticationInfo(key.toString(), json);
		} catch (Exception e) {
			throw e;
		}
	}

	/**
	 * 从缓存中获取车辆注册结果
	 * 
	 * @param param
	 * @return
	 * @throws Exception
	 */
	private VehicleInfo getVehicleInfo(DynamicSqlParameter param) throws Exception {
		String tmac = param.getEqual().get("tmac");
		StringBuffer key = new StringBuffer().append("PCC_").append(tmac);
		VehicleInfo vehicleInfo = null;
		try {
			String json = readJedisDao.getAuthenticationInfo(key.toString());
			if (null != json) {
				vehicleInfo = (VehicleInfo) RedisJsonUtil.jsonToObject(json, new TypeToken<VehicleInfo>() {
				});
			}
			return vehicleInfo;
		} catch (Exception e) {
			throw e;
		}
	}

	/*********************************************** GET AND SET ***********************************************/

	public VehicleInfoDao getVehicleBaseInfoDao() {
		return vehicleBaseInfoDao;
	}

	public void setVehicleBaseInfoDao(VehicleInfoDao vehicleBaseInfoDao) {
		this.vehicleBaseInfoDao = vehicleBaseInfoDao;
	}

	public String getIsCheckVehicleAndVin() {
		return isCheckVehicleAndVin;
	}

	public void setIsCheckVehicleAndVin(String isCheckVehicleAndVin) {
		this.isCheckVehicleAndVin = isCheckVehicleAndVin;
	}

	public String getIsAllowRepeatRegister() {
		return isAllowRepeatRegister;
	}

	public void setIsAllowRepeatRegister(String isAllowRepeatRegister) {
		this.isAllowRepeatRegister = isAllowRepeatRegister;
	}

	public String getRegisterType() {
		return registerType;
	}

	public void setRegisterType(String registerType) {
		this.registerType = registerType;
	}

	public void setWriteJedisDao(RedisDaoSupport writeJedisDao) {
		this.writeJedisDao = writeJedisDao;
	}

	public void setReadJedisDao(RedisDaoSupport readJedisDao) {
		this.readJedisDao = readJedisDao;
	}

}
