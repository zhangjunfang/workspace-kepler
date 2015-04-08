/*
 * pccsession.h
 *
 *  Created on: 2011-03-02
 *      Author: humingqing
 *
 *  对江苏的转网车辆处理
 */

#ifndef __PCCSESSION_H_
#define __PCCSESSION_H_

#include "interface.h"
#include <Mutex.h>
#include <string>
#include <map>
using namespace std ;

// 车辆基本信息
struct _stCarInfo
{
	string macid;       // 终端设置手机号
	string areacode;    // 地区编码	AreaCode
	string color;	    // 车牌颜色	CarColor
	string carmodel;    // 车型代码	CarModel
	string vehicletype; // 车辆类型	VehicleType
	string vehiclenum;  // 车牌号码	VehicleNum
};

// 4C54_15001088478:WZ:1:A1:18:苏E-Y8888
// 对应的车辆信息管理
class CPccSession
{
	class CCarInfoMgr
	{
		struct _stCarList
		{
			_stCarInfo info ; 		// 数据结构体
			_stCarList *next,*pre ; //  头尾指针
		};
		typedef map<string,_stCarList *>  CMapCarInfo ;
	public:
		CCarInfoMgr() ;
		~CCarInfoMgr() ;
		// 添加车辆信息
		bool AddCarInfo( _stCarInfo &info ) ;
		// 取得车辆信息
		bool GetCarInfo( const string &key, _stCarInfo &info , bool byphone ) ;
		// 移除车辆信息
		void RemoveInfo( const string &key, bool byphone ) ;
		// 清空所有数据
		void ClearAll( void ) ;
		// 取得所有MACID
		bool GetAllMacId( string &s ) ;
		// 取得车辆的个数
		int  GetSize( void ) { return _size ; }

	private:
		// 移除掉结点
		void RemoveNode( _stCarList *p ) ;

	private:
		// 数据查找索引
		CMapCarInfo   _phone2car;
		CMapCarInfo   _vehice2car;

		// 链表头尾指针
		_stCarList *  _head ;
		_stCarList *  _tail ;
		int 		  _size ;
		share::Mutex  _mutex ;
	};

public:
	CPccSession() ;
	~CPccSession() ;

	// 加载数据
	bool Load( const char *file ) ;
	// 根据手机MAC取得车牌
	bool GetCarInfo( const char *key, _stCarInfo &info ) ;
	// 根据车牌取得对应的手机MAC
	bool GetCarMacId( const char *key, char *macid ) ;
	// 取得所有MAC数据
	bool GetCarMacData( string &s ) ;
	// 取得当前车辆总数
	int  GetCarTotal( void ) { return _mgr.GetSize(); }

private:
	// 车辆信息管理对象
	CCarInfoMgr  _mgr ;
};


#endif /* PCCSESSION_H_ */
