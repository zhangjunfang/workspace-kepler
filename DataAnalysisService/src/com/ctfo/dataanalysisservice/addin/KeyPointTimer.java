package com.ctfo.dataanalysisservice.addin;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Timer;

import com.ctfo.dataanalysisservice.PermeterInit;
import com.ctfo.dataanalysisservice.beans.PlatAlarmTypeUtil;
import com.ctfo.dataanalysisservice.mem.MemManager;

public class KeyPointTimer extends Thread {

	/**
	 * 时间TImer定时执行计算关键点的任务
	 */
	public void run() {
		// 
		Timer timer = new Timer();
		while(true){
			String key = PlatAlarmTypeUtil.KEY_WORD + "_" + PlatAlarmTypeUtil.KEY_POINT_WORD;
			try {
				List<Map.Entry<String, List<String>>> keys = MemManager.getStationMap(key);
				SimpleDateFormat sf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
				SimpleDateFormat da = new SimpleDateFormat("yyyy-MM-dd");
				Calendar cc = Calendar.getInstance();
				StringBuffer sb = new StringBuffer();
				sb.append(da.format(cc.getTime()));
				if(keys != null){
					for (Entry<String, List<String>> en : keys) {
						String tempKey = en.getKey();
						sb.append(" ");
						sb.append(tempKey);
						long pointTime;
						try {
							pointTime = sf.parse(sb.toString()).getTime();
							pointTime = pointTime + PermeterInit.getKeyPointTimeTolerance() * 1000; //增加时间容差
							long cuTime = cc.getTimeInMillis();
							if (pointTime < cuTime) {
							} else {
								System.out.println("开始执行TImer=================");
								List<String> vids = en.getValue();
								cc.setTimeInMillis(pointTime);
								KeyPointTimerTask task = new KeyPointTimerTask(vids);
							
								timer.schedule(task, cc.getTime());
								while (!KeyPointTimerTask.isFinished) {
									Thread.sleep(1000);
								}// End while
							}
						} catch (ParseException e) {
							e.printStackTrace();
						}
					} // End for
				}else{
					Thread.sleep(10 * 1000);
				}
			} catch (Exception e) {
				e.printStackTrace();
			}
		} // End while
	}
}
