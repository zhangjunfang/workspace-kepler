/******************************************************
 *  CopyRight: 北京中交兴路科技有限公司(2012-2015)
 *   FileName: oraclesave.h
 *     Author: liubo  2012-10-24 
 *Description:
 *******************************************************/

#ifndef ORACLESAVE_H_
#define ORACLESAVE_H_

#include <string>

using namespace std;
class CSqlObj;
class CSqlWhere;
class Inter2SaveConvert;
class InterProto;

#define TH_VEHICLE_COMMAND             "TH_VEHICLE_COMMAND"
#define TH_VEHICLE_MULTIMEDIA_EVENT    "TH_VEHICLE_MULTIMEDIA_EVENT"
#define TH_VEHICLE_MEDIA               "TH_VEHICLE_MEDIA"
#define TH_VEHICLE_EVENTS              "TH_VEHICLE_EVENTS"
#define TH_VEHICLE_INFOPLAY            "TH_VEHICLE_INFOPLAY"
#define TH_VEHICLE_ETICKET             "TH_VEHICLE_ETICKET"
#define TH_VEHICLE_DRIVER              "TH_VEHICLE_DRIVER"
#define TH_VEHICLE_RECORDER            "TH_VEHICLE_RECORDER"
#define TH_VEHICLE_DISPATCH_MSG        "TH_VEHICLE_DISPATCH_MSG"
#define TH_VEHICLE_MEDIA               "TH_VEHICLE_MEDIA"
#define TB_TERMINAL_HISTORY_PARAM      "TB_TERMINAL_HISTORY_PARAM"
#define TR_LINE_VEHICLE                "TR_LINE_VEHICLE"
#define TR_VEHICLE_AREA                "TR_VEHICLE_AREA"
#define TB_TERMINAL_PARAM              "TB_TERMINAL_PARAM"
#define TB_TERMINAL_UPDATEINFO         "TB_TERMINAL_UPDATEINFO"
#define TH_VEHICLE_MEDIA_IDX           "TH_VEHICLE_MEDIA_IDX"
#define TH_VEHICLE_BRIDGE              "_map_mongo_fun.insert(make_pair(MSG_SNDM_DOWN | 4,  &DataBaseSave::oracle_vehicle_dispatch_msg));"
#define TH_VEHICLE_COMPRESS            "TH_VEHICLE_COMPRESS"
#define TH_QUESTION_ANSWER             "TH_QUESTION_ANSWER"

class OracleSave
{
public:
	OracleSave();
	virtual ~OracleSave();

	void init(Inter2SaveConvert *inter2save) {_inter2save = inter2save;}

    bool oracle_down_command(InterProto *inter_proto);
    bool oracle_vehicle_multimedia_event(InterProto *inter_proto);
    bool oracle_vehicle_media(InterProto *inter_proto);
    bool oracle_vehicle_infoplay(InterProto *inter_proto);
    bool oracle_vehicle_eticket(InterProto *inter_proto);
    bool oracle_vehicle_driver(InterProto *inter_proto);
    bool oracle_vehicle_recorder(InterProto *inter_proto);
    bool oracle_vehicle_dispatch_msg(InterProto *inter_proto);
    bool oracle_terminal_history_param(InterProto *inter_proto);
    bool oracle_line_vehicle(InterProto *inter_proto);
    bool oracle_vehicle_area(InterProto *inter_proto);
    bool oracle_terminal_param(InterProto *inter_proto);
    bool oracle_terminal_updateinfo(InterProto *inter_proto);
    bool oracle_vehicle_media_idx(InterProto *inter_proto);
    bool oracle_vehicle_bridge(InterProto *inter_proto);
    bool oracle_vehicle_compress(InterProto *inter_proto);
    bool oracle_question_answer(InterProto *inter_proto);
private:

    bool insert(CSqlObj *sql_obj, const char *table);
    bool update(CSqlObj *sql_obj, CSqlWhere *sql_where, const char *table);


    Inter2SaveConvert *_inter2save;
};

#endif /* ORACLESAVE_H_ */
