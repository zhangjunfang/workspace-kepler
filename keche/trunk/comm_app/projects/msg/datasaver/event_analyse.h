/******************************************************
 *  CopyRight: 北京中交兴路科技有限公司(2012-2015)
 *   FileName: alertanalyse.h
 *     Author: liubo  2012-10-31 
 *Description: 事件分析处理，包括报警，驾驶行为事件等的开始时间结束时间。
 *******************************************************/

#ifndef EVENTANALYSE_H_
#define EVENTANALYSE_H_

#include <list>
#include <map>

#define ALERT808   0
#define ALERT808b  1

using namespace std;

class AlertInfo
{
public:
	time_t begin_time;
	time_t end_time;

	AlertInfo():begin_time(0), end_time(0){}

	void reset()
	{
		begin_time = end_time = 0;
	}
};

class AlertEvent
{
public:
	int event;
	unsigned int single_alert_id; /* 单一报警的ID */
	AlertInfo alert_info;
};

class AlertValue
{
public:
	AlertValue(): bit_map808(0), bit_map808b(0){}
	~AlertValue(){}
	list<AlertEvent> check(unsigned int alert_id, int type, time_t gps_time);

private:
	/*****************************
	 * 与现在的报警标识位异或操作，得到change_map在map808中的索引。
	 * map808中，0表示最高位的报警，31表示最低位的报警。
	 *****************************/
	bool bit_value(unsigned int bit_map, unsigned int index)
	{
		return (bit_map & (0x80000000 >> index)) != 0;
	}
	//取得map中index对应的变量值。
	unsigned int alert_value(unsigned int index)
	{
		return 0x80000000 >> index;
	}

private:
    //现在的报警位图, 存储的上一次的报警值。
	unsigned  int bit_map808;
	AlertInfo map808[32];

	//808b协议为扩展，现在的表中暂且不实现。
	unsigned  int bit_map808b;
	AlertInfo map808b[32];
};

class AlertAnalyse
{
public:
	AlertAnalyse(){}
	~AlertAnalyse(){}
    list<AlertEvent> check_alert(long long car_id,
    		int type, unsigned int alert_id, time_t gps_time);
private:
    map<long long , AlertValue> _map_car_alert;
};

#endif /* ALERTANALYSE_H_ */
