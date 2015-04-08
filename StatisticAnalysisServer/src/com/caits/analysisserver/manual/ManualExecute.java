package com.caits.analysisserver.manual;

import java.util.Date;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.caits.analysisserver.addin.kcpt.addin.task.FileTaskThread;
import com.caits.analysisserver.bean.StaPool;
import com.caits.analysisserver.database.AnalysisDBAdapter;
import com.caits.analysisserver.quartz.alarm.jobs.impl.StatAlarmDaysJobdetail;
import com.caits.analysisserver.quartz.alarm.jobs.impl.StatAlarmMonthJobdetail;
import com.caits.analysisserver.quartz.alarm.jobs.impl.StatAlarmWeekstatJobdetail;
import com.caits.analysisserver.quartz.alarm.jobs.impl.SynAlarmDaysJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.OilConsumeMonthstatJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.OilConsumeYearstatJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.OrgAlarmDaystatJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.OrgAlarmMonthstatJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.OrgOperationDaystatJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.OrgOperationMonthstatJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.OrgOperationWeekstatJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.VehicleItineraryDaystatJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.VehicleOilWearAnalyseDaystatJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.VehicleOilWearAnalyseMonthstatJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.VehicleOilWearAnalyseWeekstatJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.VehicleOperationPropertyDaysJobdetail;
import com.caits.analysisserver.quartz.jobs.impl.VehicleOverspeedAnalyseDaystatJobdetail;
import com.caits.analysisserver.quartz.service.jobs.impl.FillStopStartDataDaysJobdetail;
import com.caits.analysisserver.quartz.service.jobs.impl.StatFactoryMonthsJobdetail;
import com.caits.analysisserver.quartz.service.jobs.impl.StatFactoryWeekstatJobdetail;
import com.caits.analysisserver.quartz.service.jobs.impl.StatMobileClientMonthsJobdetail;
import com.caits.analysisserver.quartz.service.jobs.impl.StatServiceDaysJobdetail;
import com.caits.analysisserver.quartz.service.jobs.impl.StatServiceMonthsJobdetail;
import com.caits.analysisserver.quartz.state.jobs.impl.StatStateDaysJobdetail;
import com.caits.analysisserver.quartz.visitlog.jobs.impl.AnalyserVisitlogDaysJobdetail;
import com.caits.analysisserver.quartz.visitlog.jobs.impl.BackBasedataDaysJobdetail;
import com.caits.analysisserver.quartz.visitlog.jobs.impl.StatBasedataDaysJobdetail;
import com.caits.analysisserver.quartz.visitlog.jobs.impl.StatVisitlogDaysJobdetail;
import com.caits.analysisserver.repair.BasedataRepair;
import com.caits.analysisserver.repair.OilMonitorDataRepair;
import com.caits.analysisserver.utils.CDate;

public class ManualExecute {
	
	private static final Logger logger = LoggerFactory.getLogger(ManualCommand.class);
	
	public ManualExecute(){
		
	}
	
	@SuppressWarnings("unused")
	public String[] execute(String s){
		logger.info("数据重跑任务开始；参数："+s);
		String result[] = new String[2];
		try{
		String msg[] = s.split(":");
		if (msg.length == 3) {
			if ("1".equals(msg[1])) {
				result[1] = "执行日统计（"+msg[2]+"）";
				
				String re0[] = runjob(1,msg[2]);
				if ("1".equals(re0[0])){
					String re1[] = runjob(2,msg[2]);
					String re2[] = runjob(5,msg[2]);
					if ("1".equals(re1[0])&&"1".equals(re2[0])){
						String re3[] = runjob(6,msg[2]);
						if ("1".equals(re3[0])){
							String re4[] = runjob(10,msg[2]);
							String re5[] = runjob(11,msg[2]);
							String re6[] = runjob(13,msg[2]);
							String re7[] = runjob(16,msg[2]);
							String re8[] = runjob(19,msg[2]);
							String re9[] = runjob(20,msg[2]);
							String re10[] = runjob(21,msg[2]);
							String re11[] = runjob(22,msg[2]);

							String addtip="";
							if ("0".equals(re4[0])||"0".equals(re5[0])||"0".equals(re6[0])||"0".equals(re7[0])||"0".equals(re8[0])||"0".equals(re9[0])){
								addtip += ("0".equals(re4[0])?re4[1]+"\r\n":"")+("0".equals(re5[0])?re5[1]+"\r\n":"");
								addtip += ("0".equals(re6[0])?re6[1]+"\r\n":"")+("0".equals(re7[0])?re7[1]+"\r\n":"");
								addtip += ("0".equals(re8[0])?re8[1]+"\r\n":"")+("0".equals(re9[0])?re9[1]+"\r\n":"");
							}
							
							if ("1".equals(re10[0])&&"1".equals(re11[0])){
								String re12[] = runjob(23,msg[2]);
								String re13[] = runjob(24,msg[2]);
								if ("1".equals(re12[0])&&"1".equals(re13[0])){
									result[0] = "1";
									
									if (!"".equals(addtip)){
										result[1] = result[1]+"成功！\r\n以下任务未执行成功，请单独执行：\r\n"+addtip;
									}else{
										result[1] = result[1]+"成功！";
									}
								}else{
									result[0] = "0";
									result[1] = result[1]+"失败！"+re0[1]+"\r\n"+re1[1]+"\r\n"+re2[1]+"\r\n"+re3[1]+"\r\n"+re10[1]+"\r\n"+re11[1]+"\r\n"+addtip;
								}
							}else{
								result[0] = "0";
								result[1] = result[1]+"失败！"+re0[1]+"\r\n"+re1[1]+"\r\n"+re2[1]+"\r\n"+re3[1]+"\r\n"+re10[1]+"\r\n"+re11[1]+"\r\n其他日任务未执行";
							}
						}else{
							result[0] = "0";
							result[1] = result[1]+"失败！"+re0[1]+"\r\n"+re1[1]+"\r\n"+re2[1]+"\r\n"+re3[1]+"\r\n其他日任务未执行";
						}
					}else{
						result[0] = "0";
						result[1] = result[1]+"失败！"+re0[1]+"\r\n"+re1[1]+"\r\n"+re2[1]+"\r\n其他日任务未执行";
					}
				}else{
					result[0] = "0";
					result[1] = result[1]+"失败！"+re0[1]+"\r\n其他日任务未执行";
				}
			} else if ("2".equals(msg[1])) {
				result[1] = "执行周统计（"+msg[2]+"所在周）";
				
				String re0[] = runjob(3,msg[2]);
				String re1[] = runjob(14,msg[2]);
				String re2[] = runjob(17,msg[2]);
				String re3[] = runjob(26,msg[2]);
				
				if ("1".equals(re0[0])&&"1".equals(re1[0])&&"1".equals(re2[0])&&"1".equals(re3[0])){
					result[0] = "1";
					result[1] = result[1]+"成功！";
				}else{
					result[0] = "0";
					result[1] = result[1]+"失败！";
				}
			} else if ("3".equals(msg[1])) {
				result[1] = "执行月统计（"+msg[2]+"）";

				String re0[] = runjob(4,msg[2]);
				String re1[] = runjob(7,msg[2]);
				String re2[] = runjob(8,msg[2]);
				String re3[] = runjob(12,msg[2]);
				String re4[] = runjob(15,msg[2]);
				String re5[] = runjob(18,msg[2]);
				String re6[] = runjob(25,msg[2]);
				String re7[] = runjob(27,msg[2]);
				if ("1".equals(re0[0])&&"1".equals(re1[0])&&"1".equals(re2[0])&&"1".equals(re3[0])&&"1".equals(re4[0])&&"1".equals(re5[0])&&"1".equals(re6[0])&&"1".equals(re7[0])){
					result[0] = "1";
					result[1] = result[1]+"成功！";
				}else{
					result[0] = "0";
					result[1] = result[1]+"失败！";
				}

			}else if ("4".equals(msg[1])) {
				result[1] = "执行年统计（"+msg[2]+"）";
				int year = Integer.parseInt(msg[2]);
				
				String re[] = runjob(9,msg[2]);
				
				if ("1".equals(re[0])){
					result[0] = "1";
					result[1] = result[1]+"成功！";
				}else{
					result[0] = "0";
					result[1] = result[1]+"失败！";
				}
			}else if ("5".equals(msg[1])) {
				result[1] = "重新同步首页告警数据";
				SynAlarmDaysJobdetail syn = new SynAlarmDaysJobdetail();
				syn.executeStatRecorder();
				result[0] = "1";
				result[1] = result[1]+"成功！";
			} else if ("6".equals(msg[1])) {// job独立调用
				String tmpStr[] = msg[2].split("\\|");
				result = runjob(Integer.parseInt(tmpStr[0]),tmpStr[1]);
			} else if ("99".equals(msg[1])) {
				//变量重置
				result[1] = "变量重置";
				StaPool.isAnalyserFile = false;
				StaPool.clearCountList();
				AnalysisDBAdapter.clearCollections();
				result[0] = "1";
				result[1] = result[1]+"成功！";
			}
		} else {
			result[0] = "0";
			result[1] = "缺少参数！";
		}
		return result;
		}catch(Exception ex){
			result[0] = "0";
			result[1] = result[1]+"过程中出错，执行失败！";
			return result;
		}finally{
			
		}
	}
	
	/*System.out.println("序号  名称                                                                                  说明                                                参数格式                   依赖项序号");
	System.out.println("1    TrackFileAnalyse                  轨迹文件分析任务                  无                                  无");
	System.out.println("2    StatAlarmDaysJob                  企业告警分级别按日统计   YYYY/MM/DD     1");
	System.out.println("3    StatAlarmWeeksJob                 企业告警分级别按周统计   YYYY/MM/DD     2");
	System.out.println("4    StatAlarmMonthJob                 企业告警分级别按月统计   YYYY/MM        2");
	System.out.println("5    StatStateDaysJob                  车辆状态信息日统计             YYYY/MM/DD     1");
	System.out.println("6    StatServiceDaysJob                车辆日统计                                 YYYY/MM/DD     2,5");
	System.out.println("7    StatServiceMonthsJob              车辆月统计                                 YYYY/MM        6");
	System.out.println("8    OilConsumeMonthstatJob            车辆燃油消耗月汇总             YYYY/MM        6");
	System.out.println("9    OilConsumeYearstatJob             车辆燃油消耗年汇总             YYYY           6");
	System.out.println("10   VehicleOperationPropertyDaysJob   生成车辆每日运营属性        YYYY/MM/DD     无");
	System.out.println("11   OrgAlarmDaystatJob                企业车辆告警情况日汇总   YYYY/MM/DD     1,10");
	System.out.println("12   OrgAlarmMonthstatJob              企业车辆告警情况月汇总   YYYY/MM        1");
	System.out.println("13   OrgOperationDaystatJob            车辆运营情况日汇总             YYYY/MM/DD     6");
	System.out.println("14   OrgOperationWeekstatJob           车辆运营情况周汇总             YYYY/MM/DD     12,13");
	System.out.println("15   OrgOperationMonthstatJob          车辆运营情况月汇总             YYYY/MM        12,13");
	System.out.println("16   VehicleOilWearAnalyseDaystatJob   企业车辆油耗分析日汇总   YYYY/MM/DD     6");
	System.out.println("17   VehicleOilWearAnalyseWeekstatJob  企业车辆油耗分析周汇总   YYYY/MM/DD     16");
	System.out.println("18   VehicleOilWearAnalyseMonthstatJob 企业车辆油耗分析月汇总   YYYY/MM        16");
	System.out.println("19   VehicleOverspeedAnalyseDaystatJob 车辆超速情况日统计             YYYY/MM/DD     6");
	System.out.println("20   VehicleItineraryDaystatJob        车辆运行趟次日统计             YYYY/MM/DD     无");
	System.out.println("21   AnalyserVisitlogDaysJob           访问日志按小时按日分析   YYYY/MM/DD     无");
	System.out.println("22   BackBasedataDaysJob               基础数据按日备份                  YYYY/MM/DD     无");
	System.out.println("23   StatBasedataDaysJob               基础数据按日统计                  YYYY/MM/DD     21,22");
	System.out.println("24   StatVisitlogDaysJob               访问记录按日统计                  YYYY/MM/DD     21,22,23");
	System.out.println("25   StatMobileClientMonthsJob         手机端引用数据月统计        YYYY/MM        2,22");
	System.out.println("26   FillStopStartDataDaysJobdetail    起步停车状态统计数据补录YYYY/MM/DD    1");
	System.out.println("-------------------基础数据重新分析类---------------------------------------------------------------");
	System.out.println("27   BasedataRepair                    基础数据恢复                            YYYY/MM/DD     ");
	System.out.println("28   OilChangeRepair                   油量监控日统计恢复              YYYY/MM/DD     ");
	*/
	@SuppressWarnings("unused")
	private String[] runjob(int jobId,String param){
		String result[] = new String[2];
		result[0]="0";
		int flag = 0;
		try{
			int year = CDate.getCurrentYear();
			int month = CDate.getPreviousMonth();
			int week = CDate.getPreviousWeek();
			Date dt = CDate.convertUtc2Date(CDate.getMiddyUtc(CDate.getCurrentDayUTC()));
			if (jobId==1){
				//等于1时不做任何处理
			}else if (jobId==9){
				year = Integer.parseInt(param);
			}else if (jobId==4||jobId==7||jobId==8||jobId==12||jobId==15||jobId==18||jobId==25||jobId==27){
				year = Integer.parseInt(param.substring(0, 4));
				month = Integer.parseInt(param.substring(5, 7))-1;
			}else if (jobId==3||jobId==14||jobId==17||jobId==26){
				Date dt3 = CDate.strToDateByFormat(param,"yyyy/MM/dd");// 转换成日期
				week = CDate.getDaysWeek(dt3);
				year = Integer.parseInt(param.substring(0, 4));
			}else if (jobId==29||jobId==30||jobId==31){
				//基础数据恢复 需各任务自行解析参数
			}else{
				dt = CDate.strToDateByFormat(param+"/12","yyyy/MM/dd/HH");
			}
			
			switch(jobId){
			case 1:
				if (!StaPool.isAnalyserFile){
					result[1] = "执行轨迹文件分析任务("+param+")";
					FileTaskThread fileTaskThread = new FileTaskThread();
					fileTaskThread.setDate(CDate.yearMonthDayConvertUtc(param));
					fileTaskThread.isUsingSettingTime(true);
					fileTaskThread.run();
					flag = 1;
				}else{
					flag = 0;
					result[1] = "文件分析服务正在运行，不能开始新的分析！("+param+")";
				}
				break;
			case 2:
				result[1] = "执行企业告警分级别按日统计任务("+param+")";
				StatAlarmDaysJobdetail statAlarmDaysJobdetail= new StatAlarmDaysJobdetail(dt);
				flag = statAlarmDaysJobdetail.executeStatRecorder();
				break;
			case 3:
				result[1] = "执行企业告警分级别按周统计任务("+param+")";
				StatAlarmWeekstatJobdetail statAlarmWeekstatJobdetail = new StatAlarmWeekstatJobdetail(year,week);
				flag = statAlarmWeekstatJobdetail.executeStatRecorder();
				break;
			case 4:
				result[1] = "执行企业告警分级别按月统计任务("+param+")";
				StatAlarmMonthJobdetail statAlarmMonthJobdetail = new StatAlarmMonthJobdetail(year,month);
				flag = statAlarmMonthJobdetail.executeStatRecorder();
				break;
			case 5:
				result[1] = "执行车辆状态信息日统计任务("+param+")";
				StatStateDaysJobdetail statStateDaysJobdetail = new StatStateDaysJobdetail(dt);
				flag = statStateDaysJobdetail.executeStatRecorder();
				break;
			case 6:
				result[1] = "执行车辆日统计任务("+param+")";
				StatServiceDaysJobdetail statServiceDaysJobdetail = new StatServiceDaysJobdetail(dt);
				flag = statServiceDaysJobdetail.executeStatRecorder();
				break;
			case 7:
				result[1] = "执行车辆月统计任务("+param+")";
				StatServiceMonthsJobdetail statServiceMonthsJobdetail = new StatServiceMonthsJobdetail(year,month);
				flag = statServiceMonthsJobdetail.executeStatRecorder();
				break;
			case 8:
				result[1] = "执行车辆燃油消耗月汇总任务("+param+")";
				OilConsumeMonthstatJobdetail oilConsumeMonthstatJobdetail = new OilConsumeMonthstatJobdetail(year,month);
				flag = oilConsumeMonthstatJobdetail.executeStatRecorder();
				break;
			case 9:
				result[1] = "执行车辆燃油消耗年汇总任务("+param+")";
				OilConsumeYearstatJobdetail oilConsumeYearstatJobdetail = new OilConsumeYearstatJobdetail(year);
				flag = oilConsumeYearstatJobdetail.executeStatRecorder();
				break;
			case 10:
				result[1] = "执行生成车辆每日运营属性任务("+param+")";
				VehicleOperationPropertyDaysJobdetail vehicleOperationPropertyDaysJobdetail = new VehicleOperationPropertyDaysJobdetail(dt);
				flag = vehicleOperationPropertyDaysJobdetail.executeStatRecorder();
				break;
			case 11:
				result[1] = "执行企业车辆告警情况日汇总任务("+param+")";
				OrgAlarmDaystatJobdetail orgAlarmDaystatJobdetail = new OrgAlarmDaystatJobdetail(dt);
				flag = orgAlarmDaystatJobdetail.executeStatRecorder();
				break;
			case 12:
				result[1] = "执行企业车辆告警情况月汇总任务("+param+")";
				OrgAlarmMonthstatJobdetail orgAlarmMonthstatJobdetail = new OrgAlarmMonthstatJobdetail(year,month);
				flag = orgAlarmMonthstatJobdetail.executeStatRecorder();
				break;
			
			case 13:
				result[1] = "执行车辆运营情况日汇总任务("+param+")";
				OrgOperationDaystatJobdetail orgOperationDaystatJobdetail = new OrgOperationDaystatJobdetail(dt);
				flag = orgOperationDaystatJobdetail.executeStatRecorder();
				break;
			case 14:
				result[1] = "执行车辆运营情况周汇总任务("+param+")";
				OrgOperationWeekstatJobdetail orgOperationWeekstatJobdetail = new OrgOperationWeekstatJobdetail(year,week);
				flag = orgOperationWeekstatJobdetail.executeStatRecorder();
				break;
			case 15:
				result[1] = "执行车辆运营情况月汇总任务("+param+")";
				OrgOperationMonthstatJobdetail orgOperationMonthstatJobdetail = new OrgOperationMonthstatJobdetail(year,month);
				flag = orgOperationMonthstatJobdetail.executeStatRecorder();
				break;
			case 16:
				result[1] = "执行企业车辆油耗分析日汇总任务("+param+")";
				VehicleOilWearAnalyseDaystatJobdetail vehicleOilWearAnalyseDaystatJobdetail = new VehicleOilWearAnalyseDaystatJobdetail(dt);
				flag = vehicleOilWearAnalyseDaystatJobdetail.executeStatRecorder();
				break;
			case 17:
				result[1] = "执行企业车辆油耗分析周汇总任务("+param+")";
				VehicleOilWearAnalyseWeekstatJobdetail vehicleOilWearAnalyseWeekstatJobdetail= new VehicleOilWearAnalyseWeekstatJobdetail(year,week);
				flag = vehicleOilWearAnalyseWeekstatJobdetail.executeStatRecorder();
				break;
			case 18:
				result[1] = "执行企业车辆油耗分析月汇总任务("+param+")";
				VehicleOilWearAnalyseMonthstatJobdetail vehicleOilWearAnalyseMonthstatJobdetail = new VehicleOilWearAnalyseMonthstatJobdetail(year,month);
				flag = vehicleOilWearAnalyseMonthstatJobdetail.executeStatRecorder();
				break;
			case 19:
				result[1] = "执行车辆超速情况日统计任务("+param+")";
				VehicleOverspeedAnalyseDaystatJobdetail vehicleOverspeedAnalyseDaystatJobdetail = new VehicleOverspeedAnalyseDaystatJobdetail(dt);
				flag = vehicleOverspeedAnalyseDaystatJobdetail.executeStatRecorder();
				break;
			case 20:
				result[1] = "执行车辆运行趟次日统计任务("+param+")";
				VehicleItineraryDaystatJobdetail vehicleItineraryDaystatJobdetail = new VehicleItineraryDaystatJobdetail(dt);
				flag = vehicleItineraryDaystatJobdetail.executeStatRecorder();
				break;
			case 21:
				result[1] = "执行访问日志按小时按日分析任务("+param+")";
				AnalyserVisitlogDaysJobdetail analyserVisitlogDaysJobdetail = new AnalyserVisitlogDaysJobdetail(dt);
				flag = analyserVisitlogDaysJobdetail.executeStatRecorder();
				break;
			case 22:
				result[1] = "执行基础数据按日备份任务("+param+")";
				BackBasedataDaysJobdetail backBasedataDaysJobdetail = new BackBasedataDaysJobdetail(dt);
				flag = backBasedataDaysJobdetail.executeStatRecorder();
				break;
			case 23:
				result[1] = "执行基础数据按日统计任务("+param+")";
				StatBasedataDaysJobdetail statBasedataDaysJobdetail = new StatBasedataDaysJobdetail(dt);
				flag = statBasedataDaysJobdetail.executeStatRecorder();
				break;
			case 24:
				result[1] = "执行访问记录按日统计任务("+param+")";
				StatVisitlogDaysJobdetail statVisitlogDaysJobdetail = new StatVisitlogDaysJobdetail(dt);
				flag = statVisitlogDaysJobdetail.executeStatRecorder();
				break;
			case 25:
				result[1] = "执行手机客户端引用数据月统计 任务("+param+")";
				StatMobileClientMonthsJobdetail mcJobdetail = new StatMobileClientMonthsJobdetail(year,month);
				flag = mcJobdetail.executeStatRecorder();
				break;
			case 26:
				result[1] = "车厂指标周统计 任务("+param+")";
				StatFactoryWeekstatJobdetail statFactoryWeekstatJobdetail = new StatFactoryWeekstatJobdetail(year,week);
				flag = statFactoryWeekstatJobdetail.executeStatRecorder();
				break;
			case 27:
				result[1] = "车厂指标月统计 任务("+param+")";
				StatFactoryMonthsJobdetail statFactoryMonthsJobdetail = new StatFactoryMonthsJobdetail(year,month);
				flag = statFactoryMonthsJobdetail.executeStatRecorder();
				break;
			case 28:
				result[1] = "起步停车状态统计数据补录 任务("+param+")";
				FillStopStartDataDaysJobdetail fsyy = new FillStopStartDataDaysJobdetail(dt);
				flag = fsyy.executeStatRecorder();
				break;
			case 29:
				if (!StaPool.isAnalyserFile){
					result[1] = "执行基础数据补录分析("+param+")";
					String[] params = param.split("@");
					BasedataRepair basedataRepair = new BasedataRepair();
					basedataRepair.setDate(CDate.yearMonthDayConvertUtc(params[0]));
					basedataRepair.setFileNameStr(params[1]);
					basedataRepair.setRepairDataType(params[2]);
					basedataRepair.run();
					flag = 1;
				}else{
					flag = 0;
					result[1] = "文件分析服务正在运行，不能进行基础数据补录分析！("+param+")";
				}
				break;
			case 30:
				result[1] = "执行油量监控日统计分析("+param+")";
				String[] params = param.split("@");
				OilMonitorDataRepair oilChangeRepair = new OilMonitorDataRepair();
				oilChangeRepair.setDate(CDate.yearMonthDayConvertUtc(params[0]));
				oilChangeRepair.setFileNameStr(params[1]);
				oilChangeRepair.run();
				flag = 1;
				break;
			case 31:
				result[1] = "执行夜间非法营运告警数据补录("+param+")";
				String[] param30s = param.split("@");
				Date dt0 = CDate.strToDateByFormat(param30s[0]+"/12","yyyy/MM/dd/HH");
				int entId = 0;
				if (param30s[1]!=null&&!"".equals(param30s[1])){
					entId = Integer.parseInt(param30s[1]);
				}
				/*IllegalOperationsRepair llegalOperationsRepair = new IllegalOperationsRepair(dt0,entId);
				flag =llegalOperationsRepair.executeStatRecorder();*/

				break;
			}
			
			result[0] = ""+flag;
			if (flag == 1){
				result[1] = result[1]+"成功！";
			}else{
				result[1] = result[1]+"失败！";
			}
			return result;
		}catch(Exception ex){
			logger.error("手动执行任务过程中出错，任务编号"+jobId,ex);
			result[0] = "1";
			result[1] = result[1]+"失败！";
			return result;
		}
	}
}
