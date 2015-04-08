/******************************************************
*  CopyRight: 北京中交兴路科技有限公司(2012-2015)
*   FileName: dbsave.h
*     Author: liubo  2012-10-31 
*Description: 为什么没有采用虚函数继承的方式，而采用Bridge模式，是因为在具体的实在中mongo和oracle是
*Description: 两个不同的类,而且这样更具灵活性，可以对不同的接口进行平滑。
*******************************************************/

#ifndef DBSAVE_H____
#define DBSAVE_H____

#include "mongosave.h"
#include "oraclesave.h"

class DataBaseSave
{
public:
	DataBaseSave(){}
	~DataBaseSave()
	{
		if(_mongo_save != NULL)
		{
			delete _mongo_save;
			_mongo_save = NULL;
		}

		if(_oracle_save != NULL)
		{
			delete _oracle_save;
			_oracle_save = NULL;
		}
	}
    void init(Inter2SaveConvert *inter2save)
    {
    	_mongo_save = new MongoSave;
    	_mongo_save->init(inter2save);
    	_oracle_save = new OracleSave;
    	_oracle_save->init(inter2save);
    }

	bool mongo_gps_info(InterProto *inter_proto)
	{
        return _mongo_save->mongo_gps_info(inter_proto);
	}
	bool mongo_off_online(InterProto *inter_proto)
	{
		return _mongo_save->mongo_off_online(inter_proto);
	}
	bool mongo_acc_rate(InterProto *inter_proto)
	{
		return _mongo_save->mongo_acc_rate(inter_proto);
	}
	bool mongo_drive_act(InterProto *inter_proto)
	{
		return _mongo_save->mongo_drive_act(inter_proto);
	}
	bool mongo_alert_handle(InterProto *inter_proto)
	{
		return _mongo_save->mongo_alert_handle(inter_proto);
	}

	bool oracle_down_command(InterProto *inter_proto)
	{
        return _oracle_save->oracle_down_command(inter_proto);
	}
	bool oracle_vehicle_multimedia_event(InterProto *inter_proto)
	{
		return _oracle_save->oracle_vehicle_multimedia_event(inter_proto);
	}
	bool oracle_vehicle_media(InterProto *inter_proto)
	{
		return _oracle_save->oracle_vehicle_media(inter_proto);
	}
	bool oracle_vehicle_infoplay(InterProto *inter_proto)
	{
		return _oracle_save->oracle_vehicle_infoplay(inter_proto);
	}
	bool oracle_vehicle_eticket(InterProto *inter_proto)
	{
		return _oracle_save->oracle_vehicle_eticket(inter_proto);
	}
	bool oracle_vehicle_driver(InterProto *inter_proto)
	{
		return _oracle_save->oracle_vehicle_driver(inter_proto);
	}
	bool oracle_vehicle_recorder(InterProto *inter_proto)
	{
		return _oracle_save->oracle_vehicle_recorder(inter_proto);
	}
	bool oracle_vehicle_dispatch_msg(InterProto *inter_proto)
	{
		return _oracle_save->oracle_vehicle_dispatch_msg(inter_proto);
	}
	bool oracle_terminal_history_param(InterProto *inter_proto)
	{
		return _oracle_save->oracle_terminal_history_param(inter_proto);
	}
	bool oracle_line_vehicle(InterProto *inter_proto)
	{
		return _oracle_save->oracle_line_vehicle(inter_proto);
	}
	bool oracle_vehicle_area(InterProto *inter_proto)
	{
		return _oracle_save->oracle_vehicle_area(inter_proto);
	}
	bool oracle_terminal_param(InterProto *inter_proto)
	{
		return _oracle_save->oracle_terminal_param(inter_proto);
	}
	bool oracle_terminal_updateinfo(InterProto *inter_proto)
	{
		return _oracle_save->oracle_terminal_updateinfo(inter_proto);
	}
	bool oracle_vehicle_media_idx(InterProto *inter_proto)
	{
		return _oracle_save->oracle_vehicle_media_idx(inter_proto);
	}
	bool oracle_vehicle_bridge(InterProto *inter_proto)
	{
		return _oracle_save->oracle_vehicle_bridge(inter_proto);
	}
	bool oracle_vehicle_compress(InterProto *inter_proto)
	{
		return _oracle_save->oracle_vehicle_compress(inter_proto);
	}
	bool oracle_question_answer(InterProto *inter_proto)
	{
		return _oracle_save->oracle_question_answer(inter_proto);
	}
private:
	MongoSave *_mongo_save;
	OracleSave *_oracle_save;
};




#endif /* DBSAVE_H_ */
