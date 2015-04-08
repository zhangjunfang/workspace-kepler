package com.caits.analysisserver.utils;



import java.io.File;
import java.sql.SQLException;
import java.util.Iterator;
import java.util.List;
import java.util.TimerTask;
import java.util.Vector;
import java.util.concurrent.ConcurrentHashMap;

import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;

import com.caits.analysisserver.addin.kcpt.addin.SelfDispatch;
import com.caits.analysisserver.addin.kcpt.addin.UnifiedFileDispatch;
import com.caits.analysisserver.addin.kcpt.addin.task.UnifiedTask;
import com.caits.analysisserver.bean.Task;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.database.TaskPool;

@SuppressWarnings("unchecked")
public class LoadXml { 
	
	public void loadResource(String nodeType,String xmlPath) throws Exception{
		if("sql".equals(nodeType)){ // 加载SQL
			loadSql(xmlPath);
		}else if("database".equals(nodeType)){ // 加载数据库信息
			loadDataBase(xmlPath);
		}else if("addins".equals(nodeType)){ // 加载任务插件
			loadAddin(xmlPath);
		}
	}
	
	/****
	 * 加载SQL文件
	 * @param xmlPath
	 * @throws Exception
	 */
	private void loadSql(String xmlPath) throws Exception{
		SAXReader saxReader = new SAXReader();
		Document document = null;
		
		File  in = new File(xmlPath);
		document = saxReader.read(in);
		Element rootElt = document.getRootElement();
	    List<Element> itemList = (List<Element>)rootElt.elements("item");
	    Iterator<Element> it = itemList.iterator();
	    while(it.hasNext()){
	    	Element itemElt = it.next();
	    	String ky = itemElt.attributeValue("name");
	    	String value = itemElt.elementTextTrim("value");
	    	SQLPool.getinstance().putSql(ky, value);
	    }// End while
	}
	
	/****
	 * 加载数据库XML，并且初始化数据库连接池
	 * @param dbXml
	 * @throws DocumentException
	 * @throws SQLException 
	 */
	private void loadDataBase(String dbXml) throws DocumentException, SQLException{
		SAXReader saxReader = new SAXReader();
		Document document = null;
		
		File  in = new File(dbXml);
		document = saxReader.read(in);
		Element rootElt = document.getRootElement();
		String url = getParamValue(rootElt,"oracle","JDBCUrl"); 
		String username = getParamValue(rootElt,"oracle","JDBCUser");
		String pwd = getParamValue(rootElt,"oracle","JDBCPassword");
		int timeout = Integer.parseInt(getParamValue(rootElt,"oracle","DBReconnectWait"));
		int maxLimit = Integer.parseInt(getParamValue(rootElt,"oracle","maxLimit"));// 连接池中最大连接数
		int minLimit = Integer.parseInt(getParamValue(rootElt,"oracle","minLimit"));// 连接池中最小连接数
		int InitialLimit = Integer.parseInt(getParamValue(rootElt,"oracle","InitialLimit")); // 初始化连接数 
		String propertyCheckInterval = getParamValue(rootElt,"oracle","propertyCheckInterval");
		// 初始ORACLE连接池
		OracleConnectionPool.initConnection(url, username, pwd, minLimit + "", maxLimit + "", InitialLimit + "", timeout + "",propertyCheckInterval);
	}
	
	/****
	 * 解析字节点值
	 * @param rootElt
	 * @param database
	 * @param param
	 * @return
	 */
	private String getParamValue(Element rootElt,String database,String param){
		return rootElt.element(database).element(param).element("value").getTextTrim();
	}
	
	/****
	 * 加载插件线程
	 * @param xmlPath 插件XML
	 * @throws Exception 
	 */
	private void loadAddin(String xmlPath) throws Exception{
		SAXReader saxReader = new SAXReader();
		Document document = null;
		
		File  in = new File(xmlPath);
		document = saxReader.read(in);
		Element rootElt = document.getRootElement();
		List<Element> addinList = rootElt.elements("addin");
		Iterator<Element> it = addinList.iterator();
		while(it.hasNext()){
			Element elt = it.next();
			String className = elt.attributeValue("class");
			String isLoad = elt.attributeValue("isload");
			String type = elt.attributeValue("type");
			
			if("true".equals(isLoad) && "self".equals(type)){ // 启动任务插件
				Class<SelfDispatch> stAna=(Class<SelfDispatch>)Class.forName(className);
				SelfDispatch statiscalAnalysis = (SelfDispatch)stAna.newInstance();
				statiscalAnalysis.initAnalyser();
				statiscalAnalysis.start();
			}else if(type.equals("uinifed")){ // 定期任务插件
				String taskName = elt.attributeValue("name"); // 主任务名称
				String classMainTaskName = elt.attributeValue("class");
				String scheduleTime = elt.attributeValue("scheduletime"); 
				if(!checkTime(scheduleTime)){
					throw new Exception("指定时间不规则:00:30");
				}
				
				Class<UnifiedTask> taskCls=(Class<UnifiedTask>)Class.forName(classMainTaskName);
				UnifiedTask uniTask = (UnifiedTask)taskCls.newInstance();
				uniTask.isUsingSettingTime(false); // 按默认值统计
				Task task = new Task();
				task.setTask((TimerTask)uniTask);
				task.setTime(scheduleTime);
				TaskPool.getinstance().putMainTask(taskName, task); // 添加主任务到集合
				
				ConcurrentHashMap<String,Vector<UnifiedFileDispatch>> cTask = new ConcurrentHashMap<String,Vector<UnifiedFileDispatch>>();				
				List<Element> taskList = elt.elements("task");
				Iterator<Element> taskIt = taskList.iterator();
				while(taskIt.hasNext()){
					Element taskElt = taskIt.next();
					className = taskElt.attributeValue("class");
					isLoad =  taskElt.attributeValue("isload");
					String cTaskName = taskElt.attributeValue("name"); // 子任务名称
					
					int threadNum = 1;
					if(taskElt.attributeValue("threadnum") != null){
						threadNum = Integer.parseInt(taskElt.attributeValue("threadnum")); 
					}
					
					Vector<UnifiedFileDispatch> unified = new Vector<UnifiedFileDispatch>(threadNum);
					if("true".equals(isLoad)){
						for(int  num = 0; num < threadNum; num++ ){
							Class<UnifiedFileDispatch> stAna=(Class<UnifiedFileDispatch>)Class.forName(className);
							UnifiedFileDispatch statiscalAnalysis = (UnifiedFileDispatch)stAna.newInstance();
							statiscalAnalysis.initAnalyser();
							statiscalAnalysis.setThreadId(num + 1);
							statiscalAnalysis.start();
							unified.add(statiscalAnalysis);
						}// End while
						cTask.put(cTaskName, unified); // 添加子任务
					}
				}// End while
				
				TaskPool.getinstance().putChildTask(taskName, cTask); // 添加主任务
			}
		}// End while
	}	
	
	/*****
	 * 核实指定时间为正确格式
	 * @param time
	 * @return
	 */
	private boolean checkTime(String time){
		if(time != null && !"".equals(time) && time.matches("\\d{2}:\\d{2}")){
			return true;
		}
		return false;
	}
	
}
