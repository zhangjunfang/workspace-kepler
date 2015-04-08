package com.ctfo.init;


import java.io.InputStream;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.log4j.Logger;
import org.dom4j.Document;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;


public class CovertTable
{

	public CovertTable()
	{
	}

	private String fileName = null;

	private Map<String, String> keyValue = null;

	private Logger logger = Logger.getLogger(CovertTable.class);

	public String getFileName()
	{
		return fileName;
	}

	public void setFileName(String fileName)
	{
		this.fileName = fileName;
	}

	public Map<String, String> getKeyValue()
	{
		return keyValue;
	}

	public void setKeyValue(Map<String, String> keyValue)
	{
		this.keyValue = keyValue;
	}

	public void init()
	{
		keyValue = new HashMap<String, String>();

		try
		{
			logger.debug("开始读取映射数据：" + this.getFileName());
			InputStream is = this.getClass().getResourceAsStream(this.getFileName());
			SAXReader reader = new SAXReader();
			Document doc = reader.read(is);
			Element first_level = doc.getRootElement();
			List<?> second_level_list = first_level.elements();
			
			for (int j = 0; j < second_level_list.size(); j++)
			{
				Element second_node = (Element) second_level_list.get(j);
				String  second_nodeName=second_node.getName();//2012-04-18  获取类名！！！
				List<Element> third_level = second_node.elements();
				for (int i = 0; i < third_level.size(); i++)
				{
					Element third_node = third_level.get(i);
					String leafupper = third_node.getName();
					List<?> fouth_level = third_node.elements();
					for (int k = 0; k < fouth_level.size(); k++)
					{
						Element leaf_node = (Element) fouth_level.get(k);
						String value = leaf_node.getText();
						if(value.equalsIgnoreCase("SYS_AREA_INFO")){
							keyValue.put(second_nodeName+"_"+leafupper, "SYS_AREA_INFO"); //2012-04-18 key改为类名+属性名 加于区分，取的时候，也要加上类名再取
						}else if (value.equalsIgnoreCase("UTC_TIME"))
						{
							keyValue.put(second_nodeName+"_"+leafupper, "UTC_TIME");
						}else if (value.equalsIgnoreCase("UTC_DATE"))
						{
							keyValue.put(second_nodeName+"_"+leafupper, "UTC_DATE");
						}else if (value.equalsIgnoreCase("REMAIN_SPACE"))
						{
							keyValue.put(second_nodeName+"_"+leafupper, "REMAIN_SPACE");
						}else if (value.equalsIgnoreCase("WORKING_HOURS"))
						{
							keyValue.put(second_nodeName+"_"+leafupper, "WORKING_HOURS");
						}else if (value.equalsIgnoreCase("ACCESSORIES_SUM"))
						{
							keyValue.put(second_nodeName+"_"+leafupper, "ACCESSORIES_SUM");
						}else if (value.equalsIgnoreCase("ONLINE_TIME"))
						{
							keyValue.put(second_nodeName+"_"+leafupper, "ONLINE_TIME");
						}else if (value.equalsIgnoreCase("DIRECTION"))
						{
							keyValue.put(second_nodeName+"_"+leafupper, "DIRECTION");
						}else if (value.equalsIgnoreCase("SERVER_TOTEL"))
						{
							keyValue.put(second_nodeName+"_"+leafupper, "SERVER_TOTEL");
						}
						else
						{
							try
							{
								keyValue.put(second_nodeName+"_"+leafupper + "_" + value.split("==")[0], value.split("==")[1]);
							}
							catch (Exception e)
							{
								logger.debug("系统错误, convertTable.xml 损坏或不完整" + e);
							}
						}
					}
				}
			}
			logger.debug("读取映射数据完成");
		}
		catch (Exception e)
		{
			logger.error("读取映射数据错误" + e.getMessage());
			e.printStackTrace();
		}
	}	
	public static void main(String[] args)
	{
		CovertTable config = new CovertTable();
		config.setFileName("convertTable.xml");
		config.init();
		System.out.println(config.getKeyValue());
	}
}
