package com.caits.analysisserver.addin.kcpt.addin.unifieddispatch;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.concurrent.ArrayBlockingQueue;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.addin.UnifiedFileDispatch;
import com.caits.analysisserver.database.FilePool;
import com.caits.analysisserver.database.OracleConnectionPool;
import com.caits.analysisserver.database.SQLPool;
import com.caits.analysisserver.utils.FileUtils;
import com.ctfo.generator.pk.GeneratorPK;

public class AnalysisEloadThread extends UnifiedFileDispatch {
	
	private static final Logger logger = LoggerFactory.getLogger(AnalysisEloadThread.class);
	
	private ArrayBlockingQueue<File> vPacket = new ArrayBlockingQueue<File>(100000);
	
	private final String keyWord = "track";
	
	private String sql_saveEloaddDayStat = null;
	
	private PreparedStatement stSaveEloaddDayStat = null;
	
	private Connection dbCon = null;

	private long[][] cols = null;
	
	private int threadId = 0;
	
	// 统计时间
	private long utc = 0;
	
	public void run() {		
		logger.info("发动机负荷率统计线程" + this.threadId + "启动");
		while(true){
			File eloaddistFile = null;
			BufferedReader buf = null;
			try {
				File file = vPacket.take();
				logger.info( "Thread Id : "+ this.threadId + ", AnalysisEloadThread : " + file.getName());
				String alarmFile = FileUtils.replaceRoot(file.getAbsolutePath(), FilePool.getinstance().getFile(this.utc,"eloaddistFileUrl"), keyWord);
				eloaddistFile = new File(alarmFile);
				if(eloaddistFile.exists()){
					String vid = file.getName().replaceAll("\\.txt", "");
					try{
						//从连接池获取连接
						dbCon = OracleConnectionPool.getConnection();
						stSaveEloaddDayStat = dbCon.prepareStatement(sql_saveEloaddDayStat);
						buf = new BufferedReader(new FileReader(eloaddistFile));
						String bufLine = null;	
						initArray();
						
						while((bufLine = buf.readLine()) != null){
							String[] arr = bufLine.split(" ");
							countEloaddStDay(arr);
						}// End while
						saveEloaddStDay(vid);
					}finally{
						try {
							if(stSaveEloaddDayStat != null){
								stSaveEloaddDayStat.close();
							}
							if(dbCon != null){
								dbCon.close();
							}
						} catch (SQLException e) {
							logger.error("连接放回连接池出错.",e);
						}
						if(buf != null){
							try {
								buf.close();
							} catch (IOException e) {
								logger.error("关闭文件出错.",e);
							}
						}
					}
				}									
			} catch (Exception e) {
				logger.error(" 发动机负荷率统计操作出错,文件：" + eloaddistFile.getAbsolutePath() ,e);
			}
		}// End while	
		
	}
	
	private void initArray(){
		cols = new long[12][14];
	}
	
	public void addPacket(File file) {
		try {
			vPacket.put(file);
		} catch (InterruptedException e) {
			logger.error("发动机负荷率统计出错",e);
		}
	}
	
	/**
	 * 初始化发动机负荷率统计线程
	 * @param config
	 * @param nodeName
	 * @throws Exception
	 */
	public void initAnalyser() {
		//eloaddistFileUrl = FilePool.getinstance().getFile(this.utc,"eloaddistFileUrl");
		
		// 存储发动机负荷率
		sql_saveEloaddDayStat = SQLPool.getinstance().getSql("sql_saveEloaddDayStat");
	}
	
	/**
	 * 计算发动机负荷率
	 * @param vid
	 * @throws SQLException 
	 */
	private void countEloaddStDay(String[] arr) throws SQLException{
		if(arr.length == 168){
			for(int row = 0; row <= 11;row++){
				cols[row][0] = cols[row][0] + Integer.parseInt(arr[0 + row * 14]);
				cols[row][1] = cols[row][1] + Integer.parseInt(arr[1 + row * 14]);
				cols[row][2] = cols[row][2] + Integer.parseInt(arr[2 + row * 14]);
				cols[row][3] = cols[row][3] + Integer.parseInt(arr[3 + row * 14]);
				cols[row][4] = cols[row][4] + Integer.parseInt(arr[4 + row * 14]);
				cols[row][5] = cols[row][5] + Integer.parseInt(arr[5 + row * 14]);
				cols[row][6] = cols[row][6] + Integer.parseInt(arr[6 + row * 14]);
				cols[row][7] = cols[row][7] + Integer.parseInt(arr[7 + row * 14]);
				cols[row][8] = cols[row][8] + Integer.parseInt(arr[8 + row * 14]);
				cols[row][9] = cols[row][9] + Integer.parseInt(arr[9 + row * 14]);
				cols[row][10] = cols[row][10] + Integer.parseInt(arr[10 + row * 14]);
				cols[row][11] = cols[row][11] + Integer.parseInt(arr[11 + row * 14]);
				cols[row][12] = cols[row][12] + Integer.parseInt(arr[12 + row * 14]);
				cols[row][13] = cols[row][13] + Integer.parseInt(arr[13 + row * 14]);
			}// End for
		}
	}
	
	/**
	 * 存储发动机负荷率
	 * @param vid
	 * @throws SQLException 
	 */
	private void saveEloaddStDay(String vid) throws SQLException{		
		String linkId = GeneratorPK.instance().getPKString();
		try{
			for(int row = 0; row <= 11;row++){
				stSaveEloaddDayStat.setString(1, GeneratorPK.instance().getPKString());
				stSaveEloaddDayStat.setString(2, linkId);
				stSaveEloaddDayStat.setString(3, vid);
				stSaveEloaddDayStat.setLong(4, this.utc + 12 * 60 * 60 * 1000);
				stSaveEloaddDayStat.setLong(5, cols[row][0]);
				stSaveEloaddDayStat.setLong(6, cols[row][1]);
				stSaveEloaddDayStat.setLong(7, cols[row][2]);
				stSaveEloaddDayStat.setLong(8, cols[row][3]);
				stSaveEloaddDayStat.setLong(9, cols[row][4]);
				stSaveEloaddDayStat.setLong(10, cols[row][5]);
				stSaveEloaddDayStat.setLong(11, cols[row][6]);
				stSaveEloaddDayStat.setLong(12, cols[row][7]);
				stSaveEloaddDayStat.setLong(13, cols[row][8]);
				stSaveEloaddDayStat.setLong(14, cols[row][9]);
				stSaveEloaddDayStat.setLong(15, cols[row][10]);
				stSaveEloaddDayStat.setLong(16, cols[row][11]);
				stSaveEloaddDayStat.setLong(17, cols[row][12]);
				stSaveEloaddDayStat.setLong(18, cols[row][13]);
				stSaveEloaddDayStat.setInt(19, row+1);//序列从1开始到12.
				stSaveEloaddDayStat.addBatch();
			}
			stSaveEloaddDayStat.executeBatch();
		}catch(SQLException e){
			logger.error("统计 " + vid + " 发动机负荷率出错。",e);
		}
	}

	@Override
	public void costTime() {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void setThreadId(int threadId) {
		this.threadId = threadId;
	}

	/****
	 * 设置统计时间
	 */
	@Override
	public void setTime(long utc) {
		this.utc = utc;
		
	}
}
