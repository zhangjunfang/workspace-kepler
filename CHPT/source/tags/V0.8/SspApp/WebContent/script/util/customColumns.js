  //<!--安全驾驶 -->
  var customColumns = {
    "safeDriver":[    
{
"name":"sumOverspeedAlarm","text":"超速"},
    {"name":"sumOverrpmAlarm","text":"超转"},
    {"name":"sumUrgentSpeedNum","text":"急加速"},
    {"name":"sumUrgentLowdownNum","text":"急减速"},
    {"name":"sumFatigueAlarm","text":"疲劳驾驶"},
    {"name":"sumGearGlideNum","text":"空挡滑行"},
    {"name":"sumMileage","text":"总里程"}
//<!--    {"name":"sumOilWear","text":"总油耗"}, -->
//<!--      {"name":"sumOilwearMileage","text":"百公里油耗"}, -->
  ],
  "deviceStatus":[    
{
"name":"corpName","text":"所属企业"},
    {"name":"teamName","text":"车队名称"},
    {"name":"vehicleNo","text":"车牌号"},
    {"name":"vinCode","text":"车辆VIN"},
    {"name":"gatherTimeStr","text":"采集时间"},
    {"name":"terminalStatus","text":"终端状态"},
    {"name":"gpsStatus","text":"定位状态"},
    {"name":"EWaterTempStatus","text":"冷却液温度"},
    {"name":"extVoltageStatus","text":"蓄电池电压"},
    {"name":"seriousStatus","text":"严重警告"},
    {"name":"blockStatus","text":"阻塞警告"},
    {"name":"hightempStatus","text":"高温警告"},
    {"name":"otherStatus","text":"其它警告"}
  ],
  "alarmTrack":[    
{
"name":"vehicleNo","text":"车牌号码"},
    {"name":"alarmTypeName","text":"报警类型"},
    {"name":"alarmLevel","text":"报警级别"},
    {"name":"alarmSource","text":"报警来源"},
    {"name":"alarmSpeed","text":"报警车速(Km/h)"},
    {"name":"alarmPlace","text":"报警位置"},
    {"name":"alarmHandlerStatusType","text":"处理状态"},
    {"name":"statrTime","text":"开始时间"},
    {"name":"endTime","text":"结束时间"}
  ],
  //<!-- 告警统计 -->
  "alarmStatistic":[    
{
"name":"a001Num","text":"违规驾驶"},
    {"name":"a002Num","text":"电子围栏"},
    {"name":"a003Num","text":"总线告警"},
    {"name":"a004Num","text":"设备故障"},
    {"name":"a005Num","text":"其他告警"},
    {"name":"sumMileage","text":"总里程"}
//<!--    {"name":"sumOilWear","text":"总油耗"}, -->
//<!--    {"name":"sumOilwearMileage","text":"百公里油耗"}, -->
  ],
  "vehicleAlarm2":[    
{
"name":"pentName","text":"所属企业"},
    {"name":"teamName","text":"车队"},
    {"name":"vehicleNo","text":"车牌号"},
    {"name":"vinCode","text":"车辆VIN"},
    {"name":"A001CSBJ","text":"超速行驶(次)"},
    {"name":"A001PLBJ","text":"疲劳驾驶(次)"},
    {"name":"A001DTJSCS","text":"当天驾驶超时(次)"},
    {"name":"A001KDHXGJ","text":"空挡滑行(次)"},
    {"name":"A001CCDSGJ","text":"超长怠速(次)"},
    {"name":"A001DSKTGJ","text":"怠速空调(次)"},
    {"name":"A001FDJCZGJ","text":"发动机超转(次)"},
    {"name":"A001JJIASBJ","text":"急加速(次)"},
    {"name":"A001JJIANSBJ","text":"急减速(次)"},
    {"name":"alarmTimeNoUtc","text":"告警时间"}
  ],
  "vehicleAlarm3":[    
{
"name":"pentName","text":"所属企业"},
    {"name":"teamName","text":"车队"},
    {"name":"vehicleNo","text":"车牌号"},
    {"name":"vinCode","text":"车辆VIN"},
    {"name":"A002CSTC","text":"超时停车(次)"},
    {"name":"A002JCQY","text":"进出区域(次)"},
    {"name":"A002LXPY","text":"路线偏移(次)"},
    {"name":"alarmTimeNoUtc","text":"告警时间"}
  ],
  "vehicleAlarm4":[    
{
"name":"pentName","text":"所属企业"},
    {"name":"teamName","text":"车队"},
    {"name":"vehicleNo","text":"车牌号"},
    {"name":"vinCode","text":"车辆VIN"},
    {"name":"A003SWDBJ","text":"水位低告警(次)"},
    {"name":"A003JLDSXH","text":"机滤堵塞告警(次)"},
    {"name":"A003CWBJXH","text":"发动机仓温告警(次)"},
    {"name":"A003ZDTPMSBJ","text":"制动蹄片磨损(次)"},
    {"name":"A003RYGJ","text":"燃油不足告警(次)"},
    {"name":"A003ZDQYBJ","text":"制动气压告警(次)"},
    {"name":"A003YYBJ","text":"机油压力告警(次)"},
    {"name":"A003RYDSXH","text":"燃油堵塞告警(次)"},
    {"name":"A003HSQGWBJXH","text":"缓速器高温告警(次)"},
    {"name":"A003JYWDBJXH","text":"机油温度告警(次)"},
    {"name":"A003KLDSBJ","text":"空滤堵塞告警(次)"},
    {"name":"A003LQYWDBJ","text":"冷却液温度告警(次)"},
    {"name":"A003XDCDYBJ","text":"蓄电池电压告警(次)"},
    {"name":"A003ABSGZBJ","text":"ABS故障告警(次)"},
    {"name":"alarmTimeNoUtc","text":"告警时间"}
  ],
  "vehicleAlarm5":[    
{
"name":"pentName","text":"所属企业"},
    {"name":"teamName","text":"车队"},
    {"name":"vehicleNo","text":"车牌号"},
    {"name":"vinCode","text":"车辆VIN"},
    {"name":"A004GNSSMKGZBJ","text":"导航模块故障(次)"},
    {"name":"A004GNSSTXWJHBJDBJ","text":"导航天线未接(次)"},
    {"name":"A004DHTXDL","text":"导航天线短路(次)"},
    {"name":"A004ZDZDYQYBJ","text":"终端主电源欠压(次)"},
    {"name":"A004ZDZDYDDBJ","text":"终端主电源掉电(次)"},
    {"name":"A004ZDXSPGZ","text":"终端显示屏故障(次)"},
    {"name":"A004YYMKGZ","text":"语音模块故障(次)"},
    {"name":"A004SXTGZBJ","text":"摄像头故障(次)"},
    {"name":"A004GNSSTXDLBJ","text":"速度传感器故障(次)"},
    {"name":"A004YZGZ","text":"车辆严重故障(次)"},
    {"name":"alarmTimeNoUtc","text":"告警时间"}
  ],
  "vehicleAlarm6":[    
{
"name":"pentName","text":"所属企业"},
    {"name":"teamName","text":"车队"},
    {"name":"vehicleNo","text":"车牌号"},
    {"name":"vinCode","text":"车辆VIN"},
    {"name":"A005JJBJ","text":"SOS告警(次)"},
    {"name":"A005JCLX","text":"进出路线(次)"},
    {"name":"A005LXXSSJBZGC","text":"路线行驶时间不足/过长(次)"},
    {"name":"A005FFDHBJ","text":"车辆非法点火(次)"},
    {"name":"A005KMBJ","text":"开门告警(次)"}
  ],
  //<!-- 行驶记录 -->
  "drivingRecord":[    
{
"name":"pointId","text":"序号"},
    {"name":"vehicleNo","text":"车牌号"},
    {"name":"vinCode","text":"VIN码"},
    {"name":"vehicleType","text":"车辆类型"},
    {"name":"driverName","text":"司机姓名"},
    {"name":"drivingNumber","text":"驾驶证号"},
    {"name":"startSpeed","text":"制动起始速度"},
    {"name":"brakingTime","text":"制动过程时间"},
    {"name":"stopTime","text":"停车时间"}
  ],
  //<!-- 油耗统计分析 -->
  "oilWearAnalysis":[    
{
"name":"totalOilwear","text":"全程"},
    {"name":"drivingOilwear","text":"行车"},
    {"name":"idlespeedOilwear","text":"怠速"}
//<!--    {"name":"bl100kml","text":"百公里油耗(L/100km)"},
//    {"name":"checkOilwearStr","text":"考核油耗(L/100km)"},
//    {"name":"addupOilweargapStr","text":"燃油差距(L)"},
//    {"name":"addupOiling","text":"累计加油量(L)"},
//    {"name":"oilwearGapStr","text":"油耗量差距(L)"},
//    {"name":"factOilwear","text":"实际油耗(L)"},
//    {"name":"factMileage","text":"实际里程(km)"},
//-->
  ],
  //<!-- 节油驾驶分析 -->
  "econDriver":[    
{
"name":"totalOilWear","text":"总油耗(L)"},
    {"name":"totalMileage","text":"总里程(KM)"},
    {"name":"totalBl100kmoil","text":"百公里油耗(L/100KM)"},
    {"name":"sumOverspeedAlarm","text":"超速"},
    {"name":"sumOverrpmAlarm","text":"超转"},
    {"name":"sumUrgentSpeedNum","text":"急加速"},
    {"name":"sumUrgentLowdownNum","text":"急减速"},
    {"name":"sumLongIdleNum","text":"超长怠速"},
    {"name":"sumAirConditionNum","text":"怠速空调"},
    {"name":"sumGearGlideNum","text":"空挡滑行"},
    {"name":"sumAirconditionTimestr","text":"空调工作时长"},
    {"name":"sumHeatupTimestr","text":"暖风工作时长"},
    {"name":"economicRunRate","text":"超经济区运行比例"},
    {"name":"driverConditionNum","text":"行车空调"}
    //<!--
    //{"name":"corpName","text":"所属企业"},
    //{"name":"teamName","text":"车队名称"},
    //{"name":"vehicleNo","text":"车牌号"},
    //{"name":"CVin","text":"车辆VIN"},
    //{"name":"overspeedAlarm","text":"超速(次)"},
    //{"name":"overspeedTime","text":"超速时间"},
    //{"name":"overrpmAlarm","text":"超转(次)"},
    //{"name":"overrpmTime","text":"超转时间"},
    //{"name":"longIdleNum","text":"超长怠速(次)"},
    //{"name":"longIdleTime","text":"超长怠速时间"},
    //{"name":"urgentSpeedNum","text":"急加速(次)"},
    //{"name":"urgentLowdownNum","text":"急减速(次)"},
    //{"name":"airConditionTime","text":"怠速空调时间"},
    //{"name":"economicRunRate","text":"超经济区运行比例(%)"},
    //{"name":"gearGlideNum","text":"空挡滑行(次)"},
    //{"name":"gearGlideTime","text":"空挡滑行时间"},
    //{"name":"airconditionTime","text":"空调开启时间"},
    //{"name":"heatupTime","text":"加热器工作时间"},-->
  ],
  //<!-- 加油记录管理 -->
  "vehicleOliManage":[    
{
"name":"vehicleNo","text":"车牌号"},
    {"name":"DNum","text":"加油量(L)"},
    {"name":"DSum","text":"加油费用"},
    {"name":"DUtc","text":"加油时间"},
    {"name":"refuelPerson","text":"加油人"},
    {"name":"DMiles","text":"运行里程(km)"},
    {"name":"DStation","text":"加油站"}
  ],

  //<!--                        机务管理                                                               -->
  //<!-- 维保类别管理 -->
  "mainTainClassManage":[    
{
"name":"maintainId","text":"序号"},
    {"name":"maintainName","text":"维保类别"},
    {"name":"maintainContent","text":"维保内容"},
    {"name":"createByName","text":"创建人"},
    {"name":"createTime","text":"创建时间"},
    {"name":"modifyByName","text":"最后修改人"},
    {"name":"modifyTime","text":"最后修改时间"}
  ],
  //<!-- 维保计划管理 -->
  "mainTainPlanManager":[    
{
"name":"planId","text":"序号"},
    {"name":"maintainName","text":"维保项目"},
    {"name":"exeFrequency","text":"执行频率"},
    {"name":"intervalMileage","text":"维保间隔里程"},
    {"name":"warnMileage","text":"提前提醒里程"},
    {"name":"intervalDays","text":"维保间隔天数"},
    {"name":"warnDays","text":"提前提醒天数"},
    {"name":"exeTime","text":"计划执行时间"},
    {"name":"createTime","text":"创建时间"}
  ],
  //<!-- 维保记录维护 -->
  "mainTainRecord":[    
{
"name":"planCode","text":"维保计划号"},
    {"name":"vehicleNo","text":"车牌号"},
    {"name":"prodName","text":"车型"},
    {"name":"maintainName","text":"维保项目"},
    {"name":"exeFrequency","text":"执行频率"},
    {"name":"exeTime","text":"计划维保时间"},
    {"name":"intervalMileage","text":"计划维保里程"},
    {"name":"maintainDateString","text":"实际维保时间"},
    {"name":"maintainMileage","text":"实际维保里程"},
    {"name":"maintainStat","text":"维保状态"}
  ],
  //<!-- 车辆运行统计 -->
  "vehiclepageTotal":[    
{
"name":"pentName","text":"所属企业"},
    {"name":"teamName","text":"车队名称"},
    {"name":"vehicleNo","text":"车辆号牌"},
    {"name":"CVin","text":"VIN码"},
    {"name":"engineRotateTimeString","text":"发动机工作时间"},
    {"name":"travelTime","text":"行车时间"},
    {"name":"idlingTimeString","text":"怠速时间"},
    {"name":"thePercentageOfIdleTime","text":"怠速时间百分比"},
    {"name":"airconditionTimeString","text":"空调时间"},
    {"name":"airConditioninOn","text":"空调时间百分比"},
    {"name":"mileageString","text":"运行里程"},
    {"name":"oilWearString","text":"燃油消耗"},
    {"name":"hundredKilometers","text":"百公里油耗"}
  ],
  //<!--- 单车分析报告 -->
  "vehicleReport":[    
{
"name":"autoId","text":"序号"},
    {"name":"vehicleNo","text":"车牌号"},
    {"name":"vinCode","text":"VIN码"},
    {"name":"repBeginTime","text":"开始时间"},
    {"name":"repEndTime","text":"结束时间"},
    {"name":"creatorName","text":"创建人"},
    {"name":"buildTime","text":"创建时间"}
  ],
  //<!--                            运营统计                                             -->
  //<!-- 运营统计->运营违规统计 -->
  "operatingIlleaglTotal":[    
{
"name":"vehicleNo","text":"车牌号"},
    {"name":"driverName","text":"驾驶员"},
    {"name":"alarmClass","text":"违规类型"},
    {"name":"alarmCodeName","text":"违规事件说明"},
    {"name":"beginTime","text":"开始时间"},
    {"name":"endTime","text":"结束时间"},
    {"name":"beginGpsSpeed","text":"违规开始速度"},
    {"name":"keypointGpsSpeed","text":"违规最高车速"}
  ],
  //<!--  运营统计->上线率统计 -->
  "vehicleLinerateTotal":[    
{
"name":"vehicleNo","text":"车牌号"},
    {"name":"vehicleVin","text":"VIN码"},
    {"name":"corpName","text":"所属企业"},
    {"name":"teamName","text":"所属车队"},
    {"name":"startTime","text":"开始时间"},
    {"name":"endTime","text":"结束时间"},
    {"name":"onlineRate","text":"时间段内在线率"}
  ],
  //<!-- 查看日志文件：轨迹日志 -->
  "viewFileLogTrack":[   
  //<!--     {"name":"offsetLongitude","text":"偏移经度"},
  //      {"name":"offsetLatitude","text":"偏移纬度"},-->
        {"name":"address","text":"地址"},
        {"name":"gpsTime","text":"GPS时间"},
        {"name":"gpsSpeed","text":"GPS速度"},
        {"name":"angleWithNorth","text":"正北方向夹角"},
        {"name":"vehicleStatus","text":"车辆状态"},
        {"name":"alarmTypeNameComStr","text":"报警类型"},
  //<!--      {"name":"longitude","text":"经度"},
  //      {"name":"latitude","text":"纬度"},-->
        {"name":"elevation","text":"海拔"},
        {"name":"mileage","text":"里程"},
        {"name":"cumulativeOilConsumption","text":"累计油耗"},
        {"name":"accurateOil","text":"累计精准油耗"},
        {"name":"engineRunningLong","text":"发动机运行总时长"},
        {"name":"engineSpeed","text":"引擎转速"},
        {"name":"overSpeedInformation","text":"超速报警附加信息"},
        {"name":"routeRunInformation","text":"路线行驶时间附加信息"},
        {"name":"localBasicInfoStr","text":"基本信息状态位"},
        {"name":"accStatusName","text":"ACC状态"},
        {"name":"gpsStatusName","text":"GPS状态"},
        {"name":"regionalOrLineAlarm","text":"报区域/线路报警"},
        {"name":"coolantTemperature","text":"冷却液温度"},
        {"name":"batteryVoltage","text":"蓄电池电压"},
        {"name":"instantaneousOilConsumption","text":"瞬时油耗"},
        {"name":"drivingRecordSpeed","text":"行驶记录仪速度"},
        {"name":"oilPressure","text":"机油压力"},
        {"name":"atmosphericPressure","text":"大气压力"},
        {"name":"engineTorque","text":"发动机扭矩百分比"},
        {"name":"vehicleSignalState","text":"车辆信号状态"},
        {"name":"speedSourceName","text":"车速来源"},
        {"name":"oilOfFuelGauge","text":"油量表油量"},
        {"name":"acceleratorPedalPosition","text":"油门踏板位置"},
        {"name":"terminalBatteryVoltage","text":"终端内置电池电压"},
        {"name":"engineWaterTemperature","text":"发动机水温"},
        {"name":"oilTemperature","text":"机油温度"},
        {"name":"intakeAirTemperature","text":"进气温度"},
        {"name":"doorStatus","text":"开门状态"},
        {"name":"alarmEventId","text":"报警事件ID"},
        {"name":"systemTime","text":"系统时间"}
    ],
    //<!-- 查看日志文件：驾驶行为事件日志 -->
    "viewFileLogEvent":[        {"name":"eventName","text":"事件项编码"},
  //<!--      {"name":"beginLongitude","text":"开始点经度"},
  //      {"name":"beginLatitude","text":"开始点纬度"},-->
        {"name":"beginElevation","text":"开始点海拔"},
        {"name":"beginSpeed","text":"开始点速度"},
        {"name":"beginAngleWithNorth","text":"开始点方向"},
        {"name":"beginTime","text":"开始点时间"},
   //<!--     {"name":"endLongitude","text":"结束点经度"},
    //    {"name":"endLatitude","text":"结束点纬度"},-->
        {"name":"endElevation","text":"结束点高程"},
        {"name":"endSpeed","text":"结束点速度"},
        {"name":"endAngleWithNorth","text":"结束点方向"},
        {"name":"endTime","text":"结束点时间"}
    ],
    //<!-- 运营违规 -->
  "illOperating":[    
{
"name":"vid" ,"show":"hidden","text":"自编号"},
	{"name":"vinCode" ,"show":"hidden","text":"VIN码"},
	{"name":"vehicleType" ,"show":"hidden","text":"车型"},
	{"name":"illegalRunSum","text":"夜间非法运营"},
	{"name":"routeRunSum","text":"区域内开门"},
	{"name":"routeRunOutSum","text":"区域外开门"},
	{"name":"routeRunStopSum","text":"区域内停车"},
	{"name":"routeRunOutStopSum","text":"区域外停车"},
	{"name":"offLineSum","text":"偏航"},
	{"name":"routeRunSpeedSum","text":"带速开门"},
	{"name":"overspeedAlarmSum","text":"超速"},
	{"name":"fatigueAlarmSum","text":"疲劳驾驶"},
	{"name":"overmanSum","text":"超员"}
  ],
    //<!-- 查看日志文件：告警日志 -->
    "viewFileLogAlarm":[        {"name":"alarmTypeNameComStr","text":"报警编码"},
  //<!--      {"name":"offsetLongitude","text":"偏移经度"},
  //      {"name":"offsetLatitude","text":"偏移纬度"},
   //    {"name":"longitude","text":"经度"},
   //     {"name":"latitude","text":"纬度"},-->
        {"name":"gpsTime","text":"GPS时间"},
        {"name":"gpsSpeed","text":"GPS速度"},
        {"name":"angleWithNorth","text":"正北方向夹角"},
        {"name":"cumulativeOilConsumption","text":"累计油耗"},
        {"name":"mileage","text":"里程"},
        {"name":"regionalOrLineAlarm","text":"报区域/线路报警"},
        {"name":"elevation","text":"海拔"},
        {"name":"speedSourceName","text":"车速来源"},
        {"name":"systemTime","text":"系统时间"}
    ],
    //<!-- 运营考核：综合评分 -->
    "integrationScore_corp":[       {"name":"innerCode","text":"自编号"},
       {"name":"corpName","text":"组织"},
        {"name":"vehicleSum","text":"车辆数"},
        {"name":"oilScoreSum","text":"节油操作得分"},
        {"name":"safeScoreSum","text":"安全操作得分"},
        {"name":"oilwearScore","text":"燃油消耗得分"},
        {"name":"allScoreSum","text":"综合得分"},
        {"name":"allRanking","text":"排名"}
    ],
    "integrationScore_team":[        {"name":"corpName","text":"组织"},
        {"name":"teamName","text":"车队"},
        {"name":"vehicleSum","text":"车辆数"},
        {"name":"oilScoreSum","text":"节油操作得分"},
        {"name":"safeScoreSum","text":"安全操作得分"},
        {"name":"oilwearScore","text":"燃油消耗得分"},
        {"name":"allScoreSum","text":"综合得分"},
        {"name":"allRanking","text":"排名"}
    ],
    "integrationScore_vehicle":[       {"name":"corpName","text":"组织"},
        {"name":"teamName","text":"车队"},
        {"name":"vehicleNo","text":"车牌号"},
        {"name":"innerCode" ,"show":"hidden","text":"自编号"},
        {"name":"vinCode" ,"show":"hidden","text":"VIN码"},
        {"name":"prodName" ,"show":"hidden","text":"车型"},
        {"name":"oilScoreSum","text":"节油操作得分"},
        {"name":"safeScoreSum","text":"安全操作得分"},
        {"name":"oilwearScore","text":"燃油消耗得分"},
        {"name":"allScoreSum","text":"综合得分"},
        {"name":"allRanking","text":"排名"}
    ],
    //<!-- 运营考核：节油操作得分 -->
    "oilScore_corp":[        {"name":"corpName","text":"组织"},
        {"name":"vehicleSum","text":"车辆数"},
        {"name":"travelMileage","text":"总里程"},
        {"name":"overspeedSum","text":"超速"},
        {"name":"overrpmSum","text":"超转"},
        {"name":"longIdleSum","text":"超长怠速"},
        {"name":"gearGlideSum","text":"空挡滑行"},
        {"name":"urgentSpeedNum","text":"急加速"},
        {"name":"urgentLowdownNum","text":"急减速"},
        {"name":"airConditionSum","text":"怠速空调"},
        {"name":"economicRunSum","text":"超经济区运行"},
        {"name":"oilScoreSum","text":"节油操作得分"},
        {"name":"allRanking","text":"排名"}
    ],
    "oilScore_team":[        {"name":"corpName","text":"组织"},
        {"name":"teamName","text":"车队"},
        {"name":"vehicleSum","text":"车辆数"},
        {"name":"travelMileage","text":"总里程"},
        {"name":"overspeedSum","text":"超速"},
        {"name":"overrpmSum","text":"超转"},
        {"name":"longIdleSum","text":"超长怠速"},
        {"name":"gearGlideSum","text":"空挡滑行"},
        {"name":"urgentSpeedNum","text":"急加速"},
        {"name":"urgentLowdownNum","text":"急减速"},
        {"name":"airConditionSum","text":"怠速空调"},
        {"name":"economicRunSum","text":"超经济区运行"},
        {"name":"oilScoreSum","text":"节油操作得分"},
        {"name":"allRanking","text":"排名"}
    ],
    "oilScore_vehicle":[        {"name":"corpName","text":"组织"},
        {"name":"teamName","text":"车队"},
        {"name":"vehicleNo","text":"车牌号"},
        {"name":"innerCode" ,"show":"hidden","text":"自编号"},
        {"name":"vinCode" ,"show":"hidden","text":"VIN码"},
        {"name":"prodName" ,"show":"hidden","text":"车型"},
        {"name":"travelMileage","text":"总里程"},
        {"name":"overspeedSum","text":"超速"},
        {"name":"overrpmSum","text":"超转"},
        {"name":"longIdleSum","text":"超长怠速"},
        {"name":"gearGlideSum","text":"空挡滑行"},
        {"name":"urgentSpeedNum","text":"急加速"},
        {"name":"urgentLowdownNum","text":"急减速"},
        {"name":"airConditionSum","text":"怠速空调"},
        {"name":"economicRunSum","text":"超经济区运行"},
        {"name":"oilScoreSum","text":"节油操作得分"},
        {"name":"allRanking","text":"排名"}
    ],
    //<!-- 运营考核：安全操作得分 -->
    "safeScore_corp":[        {"name":"corpName","text":"组织"},
        {"name":"vehicleSum","text":"车辆数"},
        {"name":"travelMileage","text":"总里程"},
        {"name":"overspeedSum","text":"超速"},
        {"name":"gearGlideSum","text":"空挡滑行"},
        {"name":"urgentSpeedNum","text":"急加速"},
        {"name":"urgentLowdownNum","text":"急减速"},
        {"name":"fatigueSum","text":"疲劳驾驶"},
        {"name":"safeScoreSum","text":"安全操作得分"},
        {"name":"allRanking","text":"排名"}
    ],
    "safeScore_team":[        {"name":"corpName","text":"组织"},
        {"name":"teamName","text":"车队"},
        {"name":"vehicleSum","text":"车辆数"},
        {"name":"travelMileage","text":"总里程"},
        {"name":"overspeedSum","text":"超速"},
        {"name":"gearGlideSum","text":"空挡滑行"},
        {"name":"urgentSpeedNum","text":"急加速"},
        {"name":"urgentLowdownNum","text":"急减速"},
        {"name":"fatigueSum","text":"疲劳驾驶"},
        {"name":"safeScoreSum","text":"安全操作得分"},
        {"name":"allRanking","text":"排名"}
    ],
    "safeScore_vehicle":[        {"name":"corpName","text":"组织"},
        {"name":"teamName","text":"车队"},
        {"name":"vehicleNo","text":"车牌号"},
        {"name":"innerCode" ,"show":"hidden","text":"自编号"},
        {"name":"vinCode" ,"show":"hidden","text":"VIN码"},
        {"name":"prodName" ,"show":"hidden","text":"车型"},
        {"name":"travelMileage","text":"总里程"},
        {"name":"overspeedSum","text":"超速"},
        {"name":"gearGlideSum","text":"空挡滑行"},
        {"name":"urgentSpeedNum","text":"急加速"},
        {"name":"urgentLowdownNum","text":"急减速"},
        {"name":"fatigueSum","text":"疲劳驾驶"},
        {"name":"safeScoreSum","text":"安全操作得分"},
        {"name":"allRanking","text":"排名"}
    ],
        //<!-- 运营考核：燃油消耗得分 -->
    "oilWearScore_corp":[         {"name":"corpName","text":"组织"},
        {"name":"vehicleSum","text":"车辆数"},
        {"name":"oilWearSum","text":"实际耗油量"},
        {"name":"travelMileage","text":"实际行驶里程"},
        {"name":"factOilwear","text":"百公里油耗"},
        {"name":"checkOilwear","text":"考核油耗"},
        {"name":"oilGap","text":"燃油差距"},
        {"name":"oilwearScore","text":"燃油消耗得分"},
        {"name":"allRanking","text":"排名"}
    ],
    "oilWearScore_team":[         {"name":"corpName","text":"组织"},
        {"name":"teamName","text":"车队"},
        {"name":"vehicleSum","text":"车辆数"},
        {"name":"oilWearSum","text":"实际耗油量"},
        {"name":"travelMileage","text":"实际行驶里程"},
        {"name":"factOilwear","text":"百公里油耗"},
        {"name":"checkOilwear","text":"考核油耗"},
        {"name":"oilGap","text":"燃油差距"},
        {"name":"oilwearScore","text":"燃油消耗得分"},
        {"name":"allRanking","text":"排名"}
    ],
    "oilWearScore_vehicle":[         {"name":"corpName","text":"组织"},
        {"name":"teamName","text":"车队"},
        {"name":"vehicleNo","text":"车牌号"},
        {"name":"innerCode" ,"show":"hidden","text":"自编号"},
        {"name":"vinCode" ,"show":"hidden","text":"VIN码"},
        {"name":"prodName" ,"show":"hidden","text":"车型"},
        {"name":"oilWearSum","text":"实际耗油量"},
        {"name":"travelMileage","text":"实际行驶里程"},
        {"name":"factOilwear","text":"百公里油耗"},
        {"name":"checkOilwear","text":"考核油耗"},
        {"name":"oilGap","text":"燃油差距"},
        {"name":"oilwearScore","text":"燃油消耗得分"},
        {"name":"allRanking","text":"排名"}
    ],
    //<!-- 车辆运行统计 -->
    "powerVehicleReport":[      {"name":"innerCode" ,"show":"hidden","text":"自编号"},
    {"name":"vinCode" ,"show":"hidden","text":"VIN编号"},
    {"name":"vehicleType" ,"show":"hidden","text":"车型"},
    {"name":"accCloseTimeStr","text":"ACC开启时长"},
    {"name":"engineRotateTimeStr","text":"发动机工作时长"},
    {"name":"drivingTimeStr","text":"行车时长"},
    {"name":"idlingTimeStr","text":"怠速时长"},
    {"name":"heatupTimeStr","text":"加热器工作时长"},
    {"name":"airconditionTimeStr","text":"空调时长"},
    {"name":"door1OpenNum","text":"前门开启次数"},
    {"name":"door2OpenNum","text":"中门开启次数"},
    {"name":"brakeNum","text":"制动次数"},
    {"name":"trumpetNum","text":"喇叭工作次数"},
    {"name":"absWorkNum","text":"ABS工作次数"},
    {"name":"mileage","text":"总里程"},
    {"name":"oilWear","text":"总油耗"},
    {"name":"oilWearPerHundredKm","text":"百公里油耗"}
  ],
  "powerDetailVehicleReport":[    
{
"name":"innerCode" ,"show":"hidden","text":"自编号"},
    {"name":"vinCode" ,"show":"hidden","text":"VIN编号"},
    {"name":"vehicleType" ,"show":"hidden","text":"车型"},
    {"name":"engineRotateTimeStr","text":"发动机工作时长"},
    {"name":"drivingTimeStr","text":"行车时长"},
    {"name":"idlingTimeStr","text":"怠速时长"},
    {"name":"heatupTimeStr","text":"加热器工作时长"},
    {"name":"airconditionTimeStr","text":"空调时长"},
    {"name":"door1OpenNum","text":"前门开启次数"},
    {"name":"door2OpenNum","text":"中门开启次数"},
    {"name":"brakeNum","text":"制动次数"},
    {"name":"trumpetNum","text":"喇叭工作次数"},
    {"name":"absWorkNum","text":"ABS工作次数"},
    {"name":"mileage","text":"总里程"},
    {"name":"oilWear","text":"总油耗"},
    {"name":"oilWearPerHundredKm","text":"百公里油耗"}
  ],
  //<!-- 油箱油量监控 -->
  "oilMassMon":[    
{
"name":"vid" ,"show":"hidden","text":"自编号"},
    {"name":"vehicleType" ,"show":"hidden","text":"车型"},
    {"name":"brandName" ,"show":"hidden","text":"车辆品牌"},
    {"name":"changeType","text":"油量变化状态"},
    {"name":"addoilVolume","text":"加油量(L)"},
    {"name":"useoilVolume","text":"正常消耗量(L)"},
    {"name":"decreaseoilVolume","text":"异常减少量(L)"}
  ],
  //<!-- 油箱油量分析 -->
  "oilMassAnalysis":[	{"name":"vehicleType" ,"show":"hidden","text":"车型"},
	{"name":"brandName" ,"show":"hidden","text":"车辆品牌"},
	{"name":"changeType","text":"油量变化状态"},
	{"name":"addoilVolume","text":"加油量(L)"},
	{"name":"useoilVolume","text":"正常消耗量(L)"},
	{"name":"decreaseoilVolume","text":"异常减少量(L)"}
  ],
  "icCardReport":[      {"name":"driverIccard","text":"驾驶员IC卡号"},
      {"name":"onLineTime","text":"刷卡登录时间"},
      {"name":"offLineTime","text":"刷卡退出时间"},
      {"name":"driveTime","text":"驾驶时长"},
      {"name":"sex" ,"show":"hidden","text":"性别"},
      {"name":"mobilephone" ,"show":"hidden","text":"联系方式"},
      {"name":"archiveno" ,"show":"hidden","text":"驾驶证档案号"},
      {"name":"driverIdno","text":"驾驶员身份证号"},
      {"name":"qualificationNo" ,"show":"hidden","text":"从业资格证号"},
      {"name":"address" ,"show":"hidden","text":"联系地址"},
      {"name":"icIssueName" ,"show":"hidden","text":"发卡机构"},
      {"name":"icMakerName","text":"卡制造商"}
    ],
    //<!-- 驾驶员IC卡管理 -->
    "icCard":[      {"name":"staffEntName","text":"所属组织"},
    {"name":"cardNo","text":"驾驶员IC卡号"},
    {"name":"issueName","text":"发卡机构"},
    {"name":"makerName","text":"卡制造商"},
    {"name":"cardState","text":"状态"},
    {"name":"staffName","text":"驾驶员姓名"},
    {"name":"cardId","text":"驾驶员身份证号"},
    {"name":"bussinessId","text":"从业资格证号"},
    {"name":"staffMobile","text":"联系方式"},
    {"name":"createByName" ,"show":"hidden","text":"创建人"},
    {"name":"createTime" ,"show":"hidden","text":"创建时间"},
    {"name":"updateByName" ,"show":"hidden","text":"最后编辑人"},
    {"name":"updateTime" ,"show":"hidden","text":"最后编辑时间"}
  ],
  //<!-- 驾驶员 -->
  "bsEmployeeId":[    
{
"name":"staffName","text":"驾驶员姓名"},
    {"name":"staffSex","text":"性别"},
    {"name":"staffMobile","text":"联系手机号"},
    {"name":"staffAddress","text":"联系地址"},
    {"name":"staffDriverNo","text":"驾驶证档案号"},
    {"name":"parentEntName","text":"所属企业"},
    {"name":"staffVehicleNo","text":"驾驶车牌号"},
    {"name":"driverIccard","text":"驾驶员IC卡号"}
  ],
  //<!-- 设备异常统计 -->
  "devicestatusId":[    
{
"name":"devicesStatus","text":"终端状态"},
//<!--    {"name":"gpsStatus","text":"定位状态"},   -->
    {"name":"ewaterTempStatus","text":"冷却液温度"},
    {"name":"extVoltageStatus","text":"蓄电池电压"},
    {"name":"seriousWarn","text":"严重警告"},
    {"name":"blockWarn","text":"阻塞警告"},
    {"name":"highTemWarn","text":"高温警告"},
    {"name":"otherWarn","text":"其它警告"}

  ],

  //<!-- 终端参数设置 -->
  "terminalParamQuery":[    
{
"name":"sqlid","text":"序号"},
    {"name":"vehicleNo","text":"车牌号"},
    {"name":"entName","text":"所属公司"},
    {"name":"terminalState","text":"获取状态"},
    {"name":"lastFetchTime","text":"最后获取时间"},
    {"name":"source","text":"速度来源"},
    {"name":"reportProject","text":"位置汇报方案"},
    {"name":"emergencyReportTime","text":"紧急报警时汇报时间间隔"},
    {"name":"saveTimeReportTime","text":"缺省时间汇报间隔"},
    {"name":"maxSpeed","text":"超速报警速度阀值"},
    {"name":"overSpeedTime","text":"超速时间阀值"},
    {"name":"overSpeedEmergency","text":"超速报警预警差值"},
    {"name":"sleepSpeedEmergency","text":"疲劳驾驶预警差值"},
    {"name":"continuDriverTime","text":"疲劳驾驶时间阀值"},
    {"name":"minSleepTime","text":"疲劳驾驶最小休息时间"},
    {"name":"dayDriverTime","text":"当天累计驾驶时间阀值"},
    {"name":"maxSleepTime","text":"超时停车最长停车时间"},
    {"name":"slideSpeedValue","text":"空挡滑行速度阀值"},
    {"name":"slideTimeValue","text":"空挡滑行时间阀值"},
    {"name":"slideRevolveTimeValue","text":"空挡滑行转速阀值"},
    {"name":"overRevolutionsValue","text":"发动机超转阀值"},
    {"name":"overRevolutionsAlarmValue","text":"超转报警阀值"},
    {"name":"idleTimeValue","text":"超长怠速的时间阀值"},
    {"name":"idleDefineValue","text":"怠速的定义阀值"},
    {"name":"idleAirValue","text":"怠速空调时间阀值"}
  ],

  //<!-- 历史告警明细 -->
  "alarmDetailHis":[    
{
"name":"parentEntName","text":"组织"},
    {"name":"entName","text":"车队"},
    {"name":"vehicleNo","text":"车牌号码"},
    {"name":"alarmType","text":"报警类型"},
    {"name":"alarmLevel","text":"报警级别"},
    {"name":"alarmSource","text":"报警来源"},
    {"name":"maxSpeed","text":"最高车速(Km/h)"},
	{"name":"speedThreshold","text":"超速报警阀值(Km/h)"},
    {"name":"beginUtc","text":"开始时间"},
    {"name":"endUtc","text":"结束时间"},
    {"name":"duration","text":"持续时间"},
    {"name":"alarmPosition","text":"报警位置"}
  ],

  //<!-- 燃料消耗汇总表 -->
  "busconsumeSum":[    
{
"name":"sqlid","text":"序 号"},
    {"name":"city","text":"地市"},
    {"name":"county","text":"县、市、区"},
    {"name":"operatorName","text":"组织"},
    {"name":"vehicleNum","text":"车辆数量"},
    {"name":"averageConsumeGas","text":"汽油平均油料单耗"},
    {"name":"averageConsumeDiesel","text":"柴油平均油料单耗"},
    {"name":"averageConsumeLpg","text":"天然气平均油料单耗"},
    {"name":"averageConsumeCng","text":"液化气平均油料单耗"},
    {"name":"yearTravelKmGas","text":"汽油全年行驶里程"},
    {"name":"yearTravelKmDiesel","text":"柴油全年行驶里程"},
	{"name":"yearTravelKmLpg","text":"天然气全年行驶里程"},
	{"name":"yearTravelKmCng","text":"液化气全年行驶里程"},
	{"name":"yearConsumeGas","text":"汽油全年油耗总量"},
	{"name":"yearConsumeDiesel","text":"柴油全年油耗总量"},
	{"name":"yearConsumeLpg","text":"天然气全年油耗总量"},
	{"name":"yearConsumeCng","text":"液化气全年油耗总量"},
	{"name":"oilSubcoeAmount","text":"补贴金额"}
  ],

  //<!-- 燃料消耗明细表 -->
  "busconsumeDetail":[    
{
"name":"sqlid","text":"序 号"},
    {"name":"vehicleNo","text":"车牌号码"},
    {"name":"serviceNo","text":"道路运输证号"},
    {"name":"codeName","text":"品牌"},
    {"name":"vbrandCode","text":"车辆型号"},
    {"name":"outnumber","text":"排放标准"},
    {"name":"ebrandpower","text":"发动机功率"},
    {"name":"oiltypeId","text":"燃料类型"},
    {"name":"maximalPeople","text":"核定在客人数"},
    {"name":"vehicleAge","text":"车龄"},
    {"name":"servicedeadline","text":"年运营期限"},
	{"name":"servicedays","text":"实际运营天数"},
	{"name":"servicetype","text":"运营方式"},
	{"name":"linescope","text":"线路起讫点"},
	{"name":"linelength","text":"线路营运里程"},
	{"name":"yearbeginkm","text":"年初公里"},
	{"name":"yearendkm","text":"年末公里"},
	{"name":"yeartravelkm","text":"全年行驶里程"},
	{"name":"averageconsumeGas","text":"汽油平均燃料单耗"},
	{"name":"averageconsumeDiesel","text":"柴油平均燃料单耗"},
	{"name":"averageconsumeLpg","text":"天然气平均燃料单耗"},
	{"name":"averageconsumeCng","text":"液化气平均燃料单耗"},
	{"name":"yearconsumeGas","text":"汽油全年燃料消耗总量"},
	{"name":"yearconsumeDiesel","text":"柴油全年燃料消耗总量"},
	{"name":"yearconsumeLpg","text":"天然气全年燃料消耗总量"},
	{"name":"yearconsumeCng","text":"液化气全年燃料消耗总量"},
	{"name":"busLevelOilSubcoe","text":"客车等级油补系数"},
	{"name":"roadLevelOilSubcoe","text":"道路等级油补系数"},
	{"name":"oilSubcoe","text":"综合油补系数"},
	{"name":"oilSubcoeAmount","text":"补贴金额"}

  ],

  //<!-- 远程锁车表 -->
  "remoteLock":[    
{
"name":"vehicleNo","text":"车牌号"},
    {"name":"corpName","text":"所属公司"},
    {"name":"terminalState","text":"终端参数获取状态"},
    {"name":"updateTime","text":"最后获取时间"},
    {"name":"maplon","text":"当前位置"},
    {"name":"vehicleStatus","text":"锁车状态"},
    {"name":"lockType","text":"锁车详情"},
    {"name":"unlockCode","text":"解锁码"},
    {"name":"preLockTime","text":"预置锁车时间"},
    {"name":"opName","text":"最后操作人"},
    {"name":"createTime","text":"最后操作时间"}
  ],

  //<!-- 车辆管理 add by fanxuean in 2013.9.27 -->
  "vehicleManage":[    
{
"name":"vinCode","text":"车辆VIN"},
    {"name":"vehicleNo","text":"车牌号"},
    {"name":"plateColor","text":"车牌颜色"},
    {"name":"voltage","text":"电压"},
    {"name":"staffName","text":"驾驶员"},
    {"name":"vehicleType","text":"车辆类型"},
    {"name":"innerCode","text":"内部编号"},
    {"name":"pentName","text":"所属企业"},
    {"name":"entName","text":"所属车队"},
    {"name":"vehicleOperationState","text":"车辆运营状态"},
    {"name":"vehiclRegisterNoTime","text":"车辆上牌时间"},
    {"name":"annualAuditStatus","text":"车辆年审状态"},
  ]
}