package com.ctfo.analy.dao;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;

import org.apache.log4j.Logger;

import com.ctfo.analy.DataAnalyMain;

public class CustTipConfig {
	
	private static final Logger logger = Logger.getLogger(CustTipConfig.class);

	private static CustTipConfig ctc = null;
	
	private boolean isSendToTerminal = false;
	
	private String sendMsg = "";
	
	private CustTipConfig(){
		loadConfig();
	}

	public synchronized static CustTipConfig getInstance() {
		if (ctc == null) {
			ctc = new CustTipConfig();
		}
		return ctc;
	}
	
	/*****
	 * 加载道路等级限速下发消息开关及消息内容
	 */
	private void loadConfig(){
		BufferedReader buf = null;
		File f = null;
		String str = null;
		try {
			logger.info("filePath:"+DataAnalyMain.gpsInspectionConfigFile);
			//此文件保存地址和GPS巡检配置文件相同
			String filePath = DataAnalyMain.gpsInspectionConfigFile.substring(0, DataAnalyMain.gpsInspectionConfigFile.lastIndexOf("/")) +"/downmsg.txt";
			f = new File(filePath);
			if(f.exists()){
				FileInputStream fr =new FileInputStream(f);
				buf = new BufferedReader(new InputStreamReader(fr,"utf-8"));
				
				while(null != (str = buf.readLine())){
					if(!str.startsWith("#")){
						logger.info( "downMsg Config : "+ str);
						String[] arr = str.split("\\$");
						if (arr!=null&&arr.length==2){
							if (arr[0]!=null&&!"".equals(arr[0])){
								String tmpStr = arr[0];
								String aa = "1";
								if ("1".equals(tmpStr)){
									this.setSendToTerminal(true);
								}else{
									this.setSendToTerminal(false);
								}
							}
							/*logger.info(new String(arr[1].getBytes(),"unicode"));
							logger.info(new String(arr[1].getBytes("utf-8"),"gbk"));
							logger.info(new String(arr[1].getBytes(),"utf-8"));
							logger.info(new String(arr[1].getBytes(),"gbk"));*/
							if (arr[1]!=null&&!"".equals(arr[1])){
								this.setSendMsg(arr[1]);
								//this.setSendMsg("你已超速，请谨慎驾驶！");
							}
						}
					}else{
						logger.info( "失效配置 : "+ str);
					}
				}// End while
			}else{
				logger.error("No found downmsg file");
			}
				
		} catch (Exception e) {
			logger.error("加载:downmsg 文件失败。",e);
		}finally{
			if(null != buf){
				try {
					buf.close();
				} catch (IOException e) {
					logger.equals(e);
				}
			}
		}
	}

	public boolean isSendToTerminal() {
		return isSendToTerminal;
	}

	public void setSendToTerminal(boolean isSendToTerminal) {
		this.isSendToTerminal = isSendToTerminal;
	}

	public String getSendMsg() {
		return sendMsg;
	}

	public void setSendMsg(String sendMsg) {
		this.sendMsg = sendMsg;
	}
	
	public void modDownMsgFile(){
		BufferedWriter bw = null;
		File f = null;
		try {
			logger.info("filePath:"+DataAnalyMain.gpsInspectionConfigFile);
			//此文件保存地址和GPS巡检配置文件相同
			String filePath = DataAnalyMain.gpsInspectionConfigFile.substring(0, DataAnalyMain.gpsInspectionConfigFile.lastIndexOf("/")) +"/downmsg.txt";
			f = new File(filePath);
			if(f.exists()){
				 bw = new BufferedWriter(new OutputStreamWriter(new FileOutputStream(f),"utf-8"));
				 //bw = new BufferedWriter(new FileWriter(filePath));
				 String switchflag = this.isSendToTerminal()?"1":"0";
				 bw.write(switchflag+"$"+this.getSendMsg());
			     bw.flush();
			     bw.close();
			}else{
				logger.error("No found downmSg file");
			}
				
		} catch (Exception e) {
			logger.error("写入:downmsg 文件失败。",e);
		}finally{
			if(null != bw){
				try {
					bw.close();
				} catch (IOException e) {
					logger.equals(e);
				}
			}
		}
	}

}
