/******************************************************
 *  CopyRight: 北京中交兴路科技有限公司(2012-2015)
 *   FileName: alertanalyse.cpp
 *     Author: liubo  2012-10-31 
 *Description:
 *******************************************************/

#include "event_analyse.h"
#include "idatapool.h"

list< AlertEvent > AlertAnalyse::check_alert( long long car_id, int type, unsigned int alert_id,
		time_t gps_time )
{
	map< long long, AlertValue >::iterator iter = _map_car_alert.find( car_id );

	//之前没有上过任何数据。
	if ( iter == _map_car_alert.end() ) {
		AlertValue value;
		_map_car_alert.insert( make_pair( car_id, value ) );
	}

	return _map_car_alert[car_id].check( alert_id, type, gps_time );
}

list< AlertEvent > AlertValue::check( unsigned int alert_id, int type, time_t gps_time )
{
	list< AlertEvent > list_event;
	AlertInfo *pmap = NULL;
	unsigned int *pbit_map = NULL;

	if ( type == ALERT808 ) {
		pmap = map808;
		pbit_map = & bit_map808;
	} else {
		pmap = map808b;
		pbit_map = & bit_map808b;
	}

	unsigned int change_map = ( alert_id ) ^ ( * pbit_map );
	for ( int i = 0 ; i < 32 ; i ++ ) {
		if ( bit_value( change_map, i ) ) {
			//报警从无到有
			if ( bit_value( alert_id, i ) ) {
				pmap[i].begin_time = gps_time;
				pmap[i].end_time = 0;

				AlertEvent alert_event;
				alert_event.event = IDataPool::insert;
				alert_event.alert_info = pmap[i];
				alert_event.single_alert_id = alert_value( i );
				list_event.push_back( alert_event );
			} else { //从有到无
				AlertEvent alert_event;
				alert_event.event = IDataPool::update;
				alert_event.single_alert_id = alert_value( i );
				alert_event.alert_info.begin_time = pmap[i].begin_time;
				alert_event.alert_info.end_time = gps_time;
				list_event.push_back( alert_event );
				pmap[i].reset();
			}
		}
	}
	* pbit_map = alert_id;
	return list_event;
}
