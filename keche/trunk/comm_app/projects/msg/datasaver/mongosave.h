/******************************************************
 *  CopyRight: 北京中交兴路科技有限公司(2012-2015)
 *   FileName: mongosave.h
 *     Author: liubo  2012-10-30 
 *Description:
 *******************************************************/

#ifndef MONGOSAVE_H_
#define MONGOSAVE_H_

#include "event_analyse.h"

class CSqlObj;
class Inter2SaveConvert;
class InterProto;

class MongoSave
{
public:
	MongoSave();
	virtual ~MongoSave();

	void init(Inter2SaveConvert *inter2save);

	bool mongo_gps_info(InterProto *inter_proto);
    bool mongo_off_online(InterProto *inter_proto);
    bool mongo_alert(InterProto *inter_proto);
    bool mongo_acc_rate(InterProto *inter_proto);
    bool mongo_drive_act(InterProto *inter_proto);
    bool mongo_alert_handle(InterProto *inter_proto);

private:
    static int check_gps_valid(void *gps_info);
    void mongo_alert(void *g, int type,
    		unsigned int alert_id, time_t gps_time);
private:
    Inter2SaveConvert *_inter2save;
    AlertAnalyse _alert_analyse;
};

#endif /* MONGOSAVE_H_ */
