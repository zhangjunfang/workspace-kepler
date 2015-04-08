/*
 * datapool.h
 *
 *  Created on: 2012-5-25
 *      Author: humingqing
 */

#ifndef __DB__POOL__H___
#define __DB__POOL__H___

#include "idatapool.h"
#include <time.h>
#include <Thread.h>
#include <Monitor.h>
#include <TQueue.h>

class CDbPool: public IDataPool, public share::Runnable
{
	// 管理数据库连接对象链
	class ObjList
	{
		struct _DataObj
		{
			time_t _time;
			IDBFace *_pFace;
			_DataObj *_next;
			_DataObj *_pre;
		};
	public:
		// 后一个数据对象指针
		ObjList *_next;
		// 前一个数据对象指针
		ObjList *_pre ;

	public:
		ObjList(){};
		~ObjList(){ Clear() ; };
		// 添加数据库对象
		void AddObj(IDBFace *pface);
		// 取得数据库对象
		IDBFace *GetObj(void);
		// 检测超时的对象
		void Check(int timeout);
		// 取得当前队列
		int Size(void ){ return _queue.size(); }
	private:
		// 移除结点
		void Remove(_DataObj *p, bool release);
		// 清除全部对象
		void Clear(void);

	private:
		// 数据库对象队列
		TQueue<_DataObj> _queue;
	};

	typedef map<unsigned int, ObjList*> CMapObj;
public:
	CDbPool();
	~CDbPool();
	// 初始化数据库连接对象
	bool Init(void);
	// 启动数据库连接对象
	bool Start(void);
	// 停止数据连接对象
	bool Stop(void);
	// 签出数据操作对象,根据连接串来创建军对象
	IDBFace * CheckOut(const char *connstr);
	// 签入数据操作对象
	void CheckIn(IDBFace *obj);
	// 实现释放连接数据的接口
	void Remove(IDBFace *obj);

public:
	// 线程执行体
	void run(void *param);

private:
	// 清除所有对象
	void Clear(void);

private:
	// 线程等待信号
	share::Monitor 		 _monitor;
	// 数据操作对象锁
	share::Mutex 		 _mutex;
	// 线程对象
	share::ThreadManager _thread;
	// 数据类型对象
	CMapObj 			 _index;
	// 对象链表
	TQueue<ObjList> 	 _queue ;
	// 是否初始化
	bool 				 _inited;
};

#endif /* DATAPOOL_H_ */
