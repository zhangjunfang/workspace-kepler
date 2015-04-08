package com.caits.analysisserver.manual;

import java.io.BufferedReader;
import java.io.InputStreamReader;

public class ManualCommand {
	
	public ManualCommand(){
		
	}
	
	public String generateCommand(){
		String command ="";
		StringBuffer msg = new StringBuffer("");
		try{
		String analType = "";
		String date = "";
		System.out.println("1.执行日统计,2.执行周统计,3.执行月统计,4.执行年统计,5.同步首页报警,6.数据补录,0.退出");

		BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
		boolean flag = false;

		do {
			if (flag) {
				System.out.println("1.执行日统计,2.执行周统计,3.执行月统计,4.执行年统计,5.同步首页报警,6.数据补录,0.退出");
			}

			flag = true;
			System.out.print("请选择分析类型：");
			analType = br.readLine();
			if (!"1".equals(analType) && !"2".equals(analType)	&& !"3".equals(analType)&& !"4".equals(analType)&& !"5".equals(analType)&& !"6".equals(analType) &&!"99".equals(analType) && !"0".equals(analType)) {
				analType = "";
				continue;
			} else if ("1".equals(analType)) {
				msg.append(analType);
				System.out.println("请输入年月日 格式:YYYY/MM/DD");
				date = br.readLine();

				if (!date.matches("\\d{4}/\\d{2}/\\d{2}")) {
					System.out.println("输入错误,请从新选择输入");
					msg.delete(0, msg.length());
					continue;
				}

				msg.append(":");
				msg.append(date);
				command = msg.toString();
				msg.delete(0, msg.length());
				break;
			} else if ("2".equals(analType)) {
				msg.append(analType);
				System.out.println("请输入年月日 格式:YYYY/MM/DD");
				date = br.readLine();

				if (!date.matches("\\d{4}/\\d{2}/\\d{2}")) {
					System.out.println("输入错误,请从新选择输入");
					msg.delete(0, msg.length());
					continue;
				}

				msg.append(":");
				msg.append(date);
				command = msg.toString();
				msg.delete(0, msg.length());
				break;
			} else if ("3".equals(analType)) {
				msg.append(analType);
				System.out.println("请输入年月 格式:YYYY/MM");
				date = br.readLine();

				if (!date.matches("\\d{4}/\\d{2}")) {
					System.out.println("输入错误,请从新选择输入");
					msg.delete(0, msg.length());
					continue;
				}

				msg.append(":");
				msg.append(date);
				command = msg.toString();
				msg.delete(0, msg.length());
				break;
			} else if ("4".equals(analType)) {
				msg.append(analType);
				System.out.println("请输入年份格式:YYYY");
				date = br.readLine();

				if (!date.matches("\\d{4}")) {
					System.out.println("输入错误,请从新选择输入");
					msg.delete(0, msg.length());
					continue;
				}

				msg.append(":");
				msg.append(date);
				command = msg.toString();
				msg.delete(0, msg.length());
				break;
			}else if ("5".equals(analType)) {
				msg.append(analType);
				msg.append(":");
				msg.append("0");
				command = msg.toString();
				msg.delete(0, msg.length());
				break;
			}else if ("6".equals(analType)) {
				msg.append(analType);
				msg.append(":");
				System.out.println("序号		名称									说明						参数格式		依赖项序号");
				System.out.println("1		TrackFileAnalyse					轨迹文件分析任务			YYYY/MM/DD	无");
				System.out.println("2		StatAlarmDaysJob					企业告警分级别按日统计	YYYY/MM/DD	1");
				System.out.println("3		StatAlarmWeeksJob					企业告警分级别按周统计	YYYY/MM/DD	2");
				System.out.println("4		StatAlarmMonthJob					企业告警分级别按月统计	YYYY/MM		2");
				System.out.println("5		StatStateDaysJob					车辆状态信息日统计		YYYY/MM/DD	1");
				System.out.println("6		StatServiceDaysJob					车辆日统计				YYYY/MM/DD	2,5");
				System.out.println("7		StatServiceMonthsJob				车辆月统计				YYYY/MM		6");
				System.out.println("8		OilConsumeMonthstatJob				车辆燃油消耗月汇总		YYYY/MM		6");
				System.out.println("9		OilConsumeYearstatJob				车辆燃油消耗年汇总		YYYY		6");
				System.out.println("10		VehicleOperationPropertyDaysJob		生成车辆每日运营属性		YYYY/MM/DD	无");
				System.out.println("11		OrgAlarmDaystatJob					企业车辆告警情况日汇总	YYYY/MM/DD	1,10");
				System.out.println("12		OrgAlarmMonthstatJob				企业车辆告警情况月汇总	YYYY/MM		1");
				System.out.println("13		OrgOperationDaystatJob				车辆运营情况日汇总		YYYY/MM/DD	6");
				System.out.println("14		OrgOperationWeekstatJob				车辆运营情况周汇总		YYYY/MM/DD	12,13");
				System.out.println("15		OrgOperationMonthstatJob			车辆运营情况月汇总		YYYY/MM		12,13");
				System.out.println("16		VehicleOilWearAnalyseDaystatJob		企业车辆油耗分析日汇总	YYYY/MM/DD	6");
				System.out.println("17		VehicleOilWearAnalyseWeekstatJob	企业车辆油耗分析周汇总	YYYY/MM/DD	16");
				System.out.println("18		VehicleOilWearAnalyseMonthstatJob	企业车辆油耗分析月汇总	YYYY/MM		16");
				System.out.println("19		VehicleOverspeedAnalyseDaystatJob	车辆超速情况日统计		YYYY/MM/DD	6");
				System.out.println("20		VehicleItineraryDaystatJob			车辆运行趟次日统计		YYYY/MM/DD	无");
				System.out.println("21		AnalyserVisitlogDaysJob				访问日志按小时按日分析	YYYY/MM/DD	无");
				System.out.println("22		BackBasedataDaysJob					基础数据按日备份			YYYY/MM/DD	无");
				System.out.println("23		StatBasedataDaysJob					基础数据按日统计			YYYY/MM/DD	21,22");
				System.out.println("24		StatVisitlogDaysJob					访问记录按日统计			YYYY/MM/DD	21,22,23");
				System.out.println("25		StatMobileClientMonthsJob			手机端引用数据月统计		YYYY/MM		2,22");
				System.out.println("26		StatFactoryWeeksJob					车厂系统指标周统计		YYYY/MM/DD	6");
				System.out.println("27		StatFactoryMonthsJob				车厂系统指标月统计		YYYY/MM		6");
				//System.out.println("28   FillStopStartDataDaysJobdetail    起步停车状态统计数据补录YYYY/MM/DD    1");
				System.out.println("-------------------基础数据重新分析类---------------------------------------------------------------");
				System.out.println("29		BasedataRepair						基础数据恢复				YYYY/MM/DD	");
				System.out.println("30		OilChangeRepair						油量监控日统计恢复		YYYY/MM/DD	");
				//System.out.println("31   IllegalOperationsRepair           非法运营告警数据恢复        YYYY/MM/DD     ");
				
				
				System.out.println("注意：执行之前确保任务所用表历史数据已经清理，否则会出现重复数据！");
				System.out.println("请选择补录数据类型(为空或输入0表示退出)：");
				date = br.readLine();
				
				if (!date.matches("\\d+")) {
					System.out.println("你输入的["+date+"]不是有效编号,输入错误!");
					msg.delete(0, msg.length());
					continue;
				}
				
				int idx = Integer.parseInt(date);
				
				if (idx<=0||idx>31){
					System.out.println("你输入的["+date+"]不是有效编号,输入错误!");
					msg.delete(0, msg.length());
					continue;
				}else{
					msg.append(date);
					String data2="";
					if (idx==9){
						System.out.println("请输入补录数据参数 格式:YYYY");
						data2 = br.readLine();
						
						if (!data2.matches("\\d{4}")) {
							System.out.println("输入错误,请从新输入");
							msg.delete(0, msg.length());
							continue;
						}
					}else if (idx==4||idx==7||idx==8||idx==12||idx==15||idx==18||idx==25||idx==27){
						System.out.println("请输入补录数据参数 格式:YYYY/MM");
						data2 = br.readLine();
						
						if (!data2.matches("\\d{4}/\\d{2}")) {
							System.out.println("输入错误,请从新输入");
							msg.delete(0, msg.length());
							continue;
						}
					}else{
						System.out.println("请输入补录数据参数 格式:YYYY/MM/DD");
						data2 = br.readLine();
						
						if (!data2.matches("\\d{4}/\\d{2}/\\d{2}")) {
							System.out.println("输入错误,请从新输入");
							msg.delete(0, msg.length());
							continue;
						}
						
						if (idx==29){
							System.out.println("请输入欲单独分析的数据文件名称(多个文件用半角逗号(,)分隔):ddd.txt");
							String data3 = br.readLine();
							
							if (data3==null){
								data3="";
							}
							data2 = data2+"@"+data3;
							
							System.out.println("请选择欲单独分析的数据类别(多选用半角逗号(,)分隔)");
							System.out.println("------------------------------------");
							System.out.println("1、恢复告警明细数据");
							System.out.println("2、恢复状态明细数据");
							System.out.println("3、恢复起步停车数据");
							System.out.println("4、恢复开门明细数据");
							System.out.println("5、恢复日统计数据");
							System.out.println("6、恢复中间表数据(超速、超转等值区间分布中间表)");
							System.out.println("7、恢复速度异常记录数据");
							System.out.println("8、恢复夜间非法运营数据");
							System.out.println("9、恢复驾驶明细数据");
							System.out.println("------------------------------------");
	
							String data4 = br.readLine();
							
							if (data4==null||"".equals(data4)){
								System.out.println("输入错误,请从新输入");
								msg.delete(0, msg.length());
								continue;
							}
							
							data2 = data2+"@"+data4;
						}
						if (idx==30){
							System.out.println("请输入欲单独分析的数据文件名称(多个文件用半角逗号(,)分隔):ddd.txt");
							String data3 = br.readLine();
							
							if (data3==null){
								data3="";
							}
							data2 = data2+"@"+data3;
						}
					}
					
					if (!"".equals(data2)){
						msg.append("|");
						msg.append(data2);
					}
				}			
				
				command = msg.toString();
				msg.delete(0, msg.length());
				break;
			}else if ("99".equals(analType)) {
				//变量重置
				msg.append(analType);
				msg.append(":");
				msg.append("0");
				command = msg.toString();
				msg.delete(0, msg.length());
				break;
			}
			if ("0".equals(analType)) {
				break;
			}
		} while (true); // End while
		msg.delete(0, msg.length());
		msg = null;
		return command;
		}catch(Exception ex){
			ex.printStackTrace();
			command = "";
			return command;
		}finally{
			msg = null;
		}
	}
}
