package com.ctfo.informationser.portalmng.daoImpl;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.URL;
import java.net.URLConnection;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.ctfo.informationser.annotations.AnnotationName;
import com.ctfo.informationser.portalmng.dao.RoadConditionsURL;
import com.ctfo.informationser.util.StaticSession;
import com.ctfo.local.exception.CtfoAppException;
import com.ctfo.portalmng.beans.RoadConditions;

/**
 * 
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
 * <td>2011-10-20</td>
 * <td>zhangming</td>
 * <td>创建</td>
 * </tr>
 * </table>
 * <br>
 * <font color="#FF0000">注意: 本内容仅限于[北京中交兴路信息科技有限公司]内部使用，禁止转发</font> <br>
 * 
 * @version 1.0
 * 
 * @author zhangming
 * @since JDK1.6
 */
@SuppressWarnings("unused")
@AnnotationName(name = StaticSession.SYSOPERATE + ":路况管理")
public class RoadConditionsURLImpl implements RoadConditionsURL {

	private final static String CODE = "GBK";
	private final static String[] eventTypes1 = { "事件", "交通管制" };
	private final static String[] eventTypes2_1 = { "无原因现象", "交通事故", "火灾", "路上障碍物", "道路施工", "道路作业", "活动", "气象", "灾害" };
	private final static String[] eventTypes2_2 = { "无限制", "通行限制", "转弯限制", "速度限制", "入口匝道限制", "出口匝道限制", "双向道路单侧通行限制", "车辆类型限制", "车道通行限制", "其他" };
	private final static String[] eventTypes3 = {};
	private final static String[] relations = { "事件发生路段", "事件影响路段", "事件预警路段" };

	private String url;

	private String directory;

	private final String BINSOURCE = "binSource";

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.ctfo.informationser.portalmng.dao.RoadConditionsURL#findRoadConditions(java.lang.Long[])
	 */
	@Override
	public Map<Long, List<RoadConditions>> findRoadConditions(Long[] provinceCodes) throws CtfoAppException {
		Map<Long, List<RoadConditions>> map = null;
		InputStream ins = null;
		FileOutputStream fout = null;
		FileInputStream in = null;
		if (null == provinceCodes || 0 >= provinceCodes.length) {
			return map;
		}
		File fileDirectory = new File(getDirectory());
		if (!fileDirectory.exists()) {
			fileDirectory.mkdirs();
		}
		map = new HashMap<Long, List<RoadConditions>>();
		try {
			synchronized (this) {
				for (Long provinceCode : provinceCodes) {
					StringBuilder fileName = new StringBuilder();
					fileName.append(getDirectory());
					fileName.append(BINSOURCE);
					fileName.append(System.currentTimeMillis());
					File file = new File(fileName.toString());
					// 发送请求
					URL url = new URL(getUrl().replace("{0}", String.valueOf(provinceCode)));
					URLConnection connect = url.openConnection();
					ins = connect.getInputStream();
					// 写文件
					fout = new FileOutputStream(fileName.toString());
					byte[] data = new byte[1024];
					int count = 0;
					while ((count = ins.read(data, 0, 1024)) > 0) {
						fout.write(data, 0, count);
					}
					fout.flush();
					// 读文件
					in = new FileInputStream(fileName.toString());
					// 组装对象
					List<RoadConditions> roadConditionsList = new ArrayList<RoadConditions>();
					byte[] byteData = new byte[19];
					in.read(byteData, 0, 6);
					int dticodeCount = byteToInt(byteData[2]) * 256 + byteToInt(byteData[3]);
					SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm");
					Date baseDate = null;
					long baseTime = 0;
					baseDate = dateFormat.parse("2011-01-01 00:00");
					baseTime = baseDate.getTime();

					for (int i = 0; i < dticodeCount; i++) {
						RoadConditions roadConditions = new RoadConditions();
						in.read(byteData, 0, 19);
						// 省、直辖市编码
						roadConditions.setProvinceCode(provinceCode);
						// 图幅号
						int symbols = ((((byteToInt(byteData[0]) << 8) | byteToInt(byteData[1])) << 4) | (byteToInt(byteData[2]) >> 4));
						roadConditions.setSymbols(symbols);
						// 路段等级
						int roadRank = (byteToInt(byteData[2]) & 15) >> 1;
						roadConditions.setRoadRank(roadRank);
						// LocId
						int locid = (((((byteToInt(byteData[2]) & 1) << 8) | (byteToInt(byteData[3]))) << 4) | ((byteToInt(byteData[4])) >> 4));
						roadConditions.setLocid(locid);
						// 事件编号
						int eventId = ((((((byteToInt(byteData[4]) & 15) << 8) | (byteToInt(byteData[5]) << 8)) | byteToInt(byteData[6])) << 6) | (byteToInt(byteData[7]) >> 2));
						roadConditions.setEventId(eventId);
						// 事件类型1
						int eventType1 = ((byteToInt(byteData[7]) & 3) << 6) | (byteToInt(byteData[8]) >> 2);
						roadConditions.setEventType1(eventType1ToString(eventType1));
						// 事件类型2
						int eventType2 = ((byteToInt(byteData[8]) & 3) << 6) | (byteToInt(byteData[9]) >> 2);
						roadConditions.setEventType2(eventType2ToString(eventType1, eventType2));
						// 事件类型3
						int eventType3 = ((byteToInt(byteData[9]) & 3) << 6) | (byteToInt(byteData[10]) >> 2);
						roadConditions.setEventType3(eventType3ToString(eventType3));
						// 起始时间，自2011年1月1日零时零分开始至事件发生时的秒数差
						int startTime = (((((((((byteToInt(byteData[10]) & 3) << 8) | byteToInt(byteData[11])) << 8) | byteToInt(byteData[12])) << 8) | byteToInt(byteData[13])) << 6) | (byteToInt(byteData[14]) >> 2));
						String startTimeStr = dateFormat.format(new Date(baseTime + (long) startTime * 1000));
						roadConditions.setStartTime(startTimeStr);
						// 终止时间，自2011年1月1日零时零分开始至事件结束时的秒数差
						int endTime = (((((((((byteToInt(byteData[14]) & 3) << 8) | byteToInt(byteData[15])) << 8) | byteToInt(byteData[16])) << 8) | byteToInt(byteData[17])) << 6) | (byteToInt(byteData[18]) >> 2));
						String endTimeStr = dateFormat.format(new Date(baseTime + (long) endTime * 1000));
						roadConditions.setEndTime(endTimeStr);
						// 事件与道路的关系
						int relation = byteToInt(byteData[18]) & 3;
						roadConditions.setRelation(relationToString(relation));
						// 信息描述长度
						in.read(byteData, 0, 2);
						int descriptionLength = byteToInt(byteData[0]) * 256 + byteToInt(byteData[1]);
						roadConditions.setDescriptionLength(descriptionLength);
						// 读信息描述
						byte[] discByteArray = new byte[descriptionLength];
						in.read(discByteArray, 0, descriptionLength);
						String description = new String(discByteArray, CODE);
						roadConditions.setDescription(description);
						roadConditionsList.add(roadConditions);
					}
					in.close();
					map.put(provinceCode, roadConditionsList);
					// 用完扔了，哈哈哈哈！
					file.delete();
				}
			}
		} catch (Exception e) {
			throw new CtfoAppException(e.fillInStackTrace());
		} finally {
			// 用完扔了，哈哈哈哈！
			fileDirectory.delete();
			try {
				ins.close();
				fout.close();
				in.close();
			} catch (IOException e) {
				throw new CtfoAppException(e.fillInStackTrace());
			}
		}
		return map;
	}

	/**
	 * 字节型转换成无符号整型
	 * 
	 * @param bt
	 * @return
	 */
	private static int byteToInt(byte bt) {
		return bt >= 0 ? bt : 256 + bt;
	}

	/**
	 * 事件类型1
	 */
	private static String eventType1ToString(int eventType1Key) {
		for (int i = 0; i < eventTypes1.length; i++) {
			if ((i + 1) == eventType1Key) {
				return eventTypes1[i];
			}
		}
		return null;
	}

	/**
	 * 事件类型2
	 */
	private static String eventType2ToString(int eventType1Key, int eventType2Key) {
		if (1 == eventType1Key) {
			for (int i = 0; i < eventTypes2_1.length; i++) {
				if (i == eventType2Key) {
					return eventTypes2_1[i];
				}
			}
		}
		if (2 == eventType1Key) {
			for (int i = 0; i < eventTypes2_2.length; i++) {
				if (i == eventType2Key) {
					System.out.println(eventType2Key);
					return eventTypes2_2[i];
				}
			}
		}
		return null;
	}

	/**
	 * 事件类型3
	 */
	private static String eventType3ToString(int eventType3Key) {
		return null;
	}

	/**
	 * 事件与道路的关系
	 */
	private static String relationToString(int relationKey) {
		for (int i = 0; i < relations.length; i++) {
			if (i == relationKey) {
				return relations[i];
			}
		}
		return null;
	}

	public void setUrl(String url) {
		this.url = url;
	}

	public String getUrl() {
		return url;
	}

	public String getDirectory() {
		return directory;
	}

	public void setDirectory(String directory) {
		this.directory = directory;
	}
}
