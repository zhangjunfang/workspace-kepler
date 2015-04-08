/**********************************************
 * CorpInfoHandle.h
 *
 *  Created on: 2010-10-12
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/

#ifndef CORPINFOHANDLE_H_
#define CORPINFOHANDLE_H_

#include <map>
#include <list>
#include <iostream>
#include <vector>
#include "tools.h"
#include "BaseTools.h"
#include "ProtoHeader.h"
#include "ProtoParse.h"
#include <Mutex.h>

using namespace std;

#define ONLINE 1
#define OFFLINE 0

class CorpInfoHandle
{
public:
#pragma pack(1)
typedef struct _CorpInfo
{
	time_t send_time;
	unsigned int   send_num;
	_CorpInfo()
	{
		send_num = 0;//上次发送数据包的时间
		send_time = 0;
	}
}CorpInfo;

typedef struct _CarInfo
{
	char vehicle_no[21];
	unsigned char vehicle_color;
	unsigned int access_code;

	_CarInfo()
	{
		memset(vehicle_no,0,sizeof(vehicle_no));
		vehicle_color = 0;
		access_code = 0;
	}

}CarInfo;

typedef struct _CommandInfo
{
	time_t send_time;
	unsigned short num;
	_CommandInfo()
	{
		send_time = time(0);
		num = 1;
	}
}CommandInfo;

#pragma pack()
public:
	CorpInfoHandle(){}
	virtual ~CorpInfoHandle(){}

	CorpInfo update_corp_map(unsigned int access_code,unsigned int num,unsigned int max_num)
	{
		CorpInfo info;
		_mutex.lock() ;
		map<unsigned int, CorpInfo>::iterator iter = _corp_map.find(access_code);
		if(iter != _corp_map.end())
		{			
			iter->second.send_num += num;
			if(iter->second.send_num >= max_num)
			{
				iter->second.send_num = (iter->second.send_num+max_num)%max_num;
				info = iter->second;
				iter->second.send_time = time(0);
			}
			else info = iter->second;
		}
		else
		{
			info.send_num = num;
			info.send_time = time(0);
			_corp_map.insert(make_pair(access_code,info));
		}
		_mutex.unlock() ;
		return info;
	}

	//更新mac_id和access_code的对应关系。
	string get_mac_id(	char vehicle_no[21],unsigned char vehicle_color)
	{
		char buffer[32] = {0};
		sprintf(buffer,"%d_",vehicle_color);
		strncpy(buffer+strlen(buffer),vehicle_no,21);
		string str(buffer);
		return str;
	}

	void update_car_info(const string &mac_id,unsigned int access_code)
	{
		_car_mutex.lock();

		map<string,unsigned int>::iterator iter = _car_map.find(mac_id);
		if (iter != _car_map.end()){
			iter->second = access_code;
			// printf( "update mac id %s access code %d\n" , mac_id.c_str(), access_code ) ;
		} else {
			_car_map.insert(make_pair(mac_id,access_code));
			// printf( "insert mac id %s access code %d\n" , mac_id.c_str(), access_code ) ;
		}

		_car_mutex.unlock();
	}

	unsigned int get_access_code(const string &mac_id)
	{
		unsigned int access_code = 0;

		_car_mutex.lock();

		map<string, unsigned int>::iterator iter = _car_map.find(mac_id);
		if (iter != _car_map.end()){
			access_code = iter->second;
			// printf( "find mac id %s access code %d\n" , mac_id.c_str(), access_code ) ;
		}

		_car_mutex.unlock();

		return access_code;
	}

	void add_command_info(const string &command_id)
	{
		_command_mutex.lock();
		//先把时间超时的给删除掉》
		time_t cur_time = time(0);
		map<string,CommandInfo>::iterator iter = _command_map.begin();
		for(; iter != _command_map.end();)
		{
			if(cur_time - iter->second.send_time > 3*60)
			{
				_command_map.erase(iter++);
			}
			else
				++iter;
		}

		iter = _command_map.find(command_id);
		if(iter == _command_map.end())
		{
			CommandInfo info;
			_command_map.insert(make_pair(command_id,info));
		}
		else
		{
			iter->second.num++;
			iter->second.send_time = time(0);
		}

		_command_mutex.unlock();
	}

	bool get_command_info(const string& command_id)
	{
		bool ret = false;
		_command_mutex.lock();
		map<string,CommandInfo>::iterator iter = _command_map.find(command_id);
		if(iter != _command_map.end())
		{
			ret = true;
			if(--(iter->second.num) == 0)
				_command_map.erase(iter++);
		}
		_command_mutex.unlock();
		return ret;
	}
private:
	share::Mutex _mutex;
	share::Mutex _car_mutex;
	share::Mutex _command_mutex;
	map<unsigned int ,CorpInfo>  _corp_map;
	map<string ,unsigned int> _car_map;

	//map->first 为seq的组成：color_macid_datatype;
	map<string,CommandInfo> _command_map;

};

#endif /* CORPINFOHANDLE_H_ */
