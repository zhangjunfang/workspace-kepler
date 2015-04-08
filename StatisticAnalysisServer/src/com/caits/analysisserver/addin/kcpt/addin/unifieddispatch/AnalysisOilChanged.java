package com.caits.analysisserver.addin.kcpt.addin.unifieddispatch;

import java.io.File;
import java.sql.SQLException;
import java.util.concurrent.ArrayBlockingQueue;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import oracle.jdbc.OracleConnection;
import com.caits.analysisserver.addin.kcpt.addin.UnifiedFileDispatch;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.services.OilService;
import com.caits.analysisserver.utils.FileUtils;

public class AnalysisOilChanged extends UnifiedFileDispatch {

	private static final Logger logger = LoggerFactory.getLogger(AnalysisOilChanged.class);
	
	private ArrayBlockingQueue<File> vPacket = new ArrayBlockingQueue<File>(100000);
	
	private final String keyWord = "track";
	
	private int threadId = 0;
	
	// 油箱油量分析文件目录
	//private String oilFileUrl = null;
	
	private long utc = 0; // 统计时间
	
	private String vid ;

	@Override
	public void addPacket(File file) {
		try {
			vPacket.put(file);
		} catch (InterruptedException e) {
			logger.error("统计主线程添加file文件到队列出错",e);
		}
	}
	
	public void run() {	
		logger.info("油量监测统计分析线程" + this.threadId + "启动");
		while(true){
			File oilFile = null;
			OracleConnection dbCon = null;
			try {
				File file = vPacket.take();
				// 替换轨迹文件路径转换为油量路径
				logger.info( "Thread Id : "+ this.threadId + ", AnalysisOilChanged : " + file.getName());
				String filePath = FileUtils.replaceRoot(file.getPath(), FilePool.getinstance().getFile(this.utc,"oilUrl"),keyWord); 
				vid = file.getName().replaceAll("\\.txt", "");
				oilFile = new File(filePath);
				if(null != AnalysisDBAdapter.oilMonitorMap.get(vid) && "000100060002".equals(AnalysisDBAdapter.oilMonitorMap.get(vid))){
					if(oilFile.exists()){ // 根据车辆配置方案计算油量
						dbCon = (OracleConnection) OracleConnectionPool.getConnection();
						OilService oilService = new OilService(dbCon,utc,vid);
						oilService.analysisOilRecords(oilFile);
					}else{
						logger.info( "Thread Id : "+ this.threadId + ",VID="+vid+" 油量监控文件不存在！");
					}
				}else{
					if (null == AnalysisDBAdapter.oilMonitorMap.get(vid)){
						logger.info( "Thread Id : "+ this.threadId + ",VID="+vid+" 油量监控缓存没有此车信息！");
					}else{
						logger.info( "Thread Id : "+ this.threadId + ",VID="+vid+" 此车没有配置油量监控设置！code:"+AnalysisDBAdapter.oilMonitorMap.get(vid));
					}
					
				}
			}catch(Exception ex){
				logger.error("油量监测统计分析线程" + this.threadId + "出错.");
			}finally{
				try {
					if (dbCon!=null){
							dbCon.close();
					}
				} catch (SQLException e) {
					// TODO Auto-generated catch block
					logger.error("将连接放回连接池出错：",e);
				}
			}
		}// End while
	}

	@Override
	public void costTime() {
	}

	@Override
	public void initAnalyser() {
		//oilFileUrl = FilePool.getinstance().getFile(this.utc,"oilUrl");
	}

	@Override
	public void setThreadId(int threadId) {
		this.threadId = threadId;
	}

	@Override
	public void setTime(long utc) {
		this.utc = utc;
	}

}
