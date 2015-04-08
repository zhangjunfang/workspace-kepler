package com.ctfo.statistics.alarm.job;

import java.text.SimpleDateFormat;
import java.util.Date;

import org.quartz.Job;
import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ctfo.statistics.alarm.common.Utils;

public class HeartbeatJob implements Job {
	private static Logger log = LoggerFactory.getLogger(HeartbeatJob.class);
	
	public void execute(JobExecutionContext context) throws JobExecutionException {
		try {
			log.info("-------------HeartbeatTask starting ...");
		} catch (Exception e) {
			log.error("报警统计异常:" + e.getMessage(), e);
		} 
	}
public static void main(String[] args) {
	System.out.println(""+(1*100/10) + "%" + " time:" + 
new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date(Long.parseLong("1321934400062"))));
String str = "16127583:65833485:65836261:16125508:0:0:1417597897000:,1,:0:0:0:0:0:0:0:0:0:0:0:0:0:0:0:::1::289::225:2:湘N21321:15286844796:3179773480497812569720:11C1279:申满华:怀化集团会同分公司:10229:213:E028:1417597898800:1:0:0:未分队车辆";
String[] array = Utils.splitPreserveAllTokens(str, ":");
int i = 0;
for(String s : array){
	
	System.out.println(i+"="+s);
	i++;
}
}
}
