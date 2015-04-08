#!/usr/local/bin/thrift --gen java
namespace cpp sectionspeed
namespace java com.ctfo.sectionspeed.thrift

// 车辆类型枚举
enum CarForm {
	COACH,
	TRUCK,
	OTHER;
}

struct Point{
	1: required double longitude; //经度
	2: required double latitude; // 纬度
	3: optional double elevation; // 海拔
}

// 传输位置信息
struct GPSInfo {
	1: optional Point point; //位置信息
	2: optional double speed; // 速度
	3: optional double angle; // 方向夹角
	4: optional CarForm cf; // 车辆类型
	5: optional i64 date_time; // 时间
}

struct RoadSpeedInfo {
	1: optional i64 rid;  // 路段编号
	2: optional i64 rdnum;  // 道路编号
	3: optional i32 postcode; // 行政编号
	4: optional byte fc; //功能等级
	5: optional byte dir;  //交通流方向
	6: optional byte toll; //是否收费
	7: optional byte rstruct;  //结构形态
	8: optional byte nr;	//管理等级
	9: optional string stname;				//标准道路名称
	10: optional string byname;				//地方道路名称
	11: optional string standardv;			//标准车速上下限
	12: optional string truckv;				//货车车速上下限
	13: optional string coachv;	//客车车速上下限
	14: optional list<Point> pointsList;
}

// 服务异常
exception SectionSpeedServiceException {
	1: string message;
}

// 分段限速服务接口
service SectionSpeed {
	i32 ping();
	// 是否超速
	bool isOverspeed(1: GPSInfo info) throws (1:SectionSpeedServiceException ssse);
	// 以中心点查找分段路网信息接口
	list<RoadSpeedInfo> searchSectionRNet(1: Point point) throws (1:SectionSpeedServiceException ssse);
	// 以道路查找分段路网信息接口
	list<RoadSpeedInfo> searchSectionLInfo(1: list<Point> lps) throws (1:SectionSpeedServiceException ssse);
}