/*
 * main.cpp
 *
 *  Created on: 2012-10-22
 *      Author: humingqing
 */

#include <stdio.h>
#include "queuemgr.h"

struct _msgdata
{
	int   _fd  ;
	int   _seq ;
	char *_ptr ;
	int   _len ;
};

// 数据回调对象
class CQCaller : public IQCaller
{
public:
	CQCaller(){

	}
	virtual ~CQCaller() {}

	// 调用超时重发数据
	bool OnReSend( void *data ) {
		if ( data == NULL )
			return false ;

		_msgdata *p = ( _msgdata *) data ;
		printf( "fd %d, resend message time %ld, seqid %d, data %s\n", p->_fd, time(NULL), p->_seq , p->_ptr) ;
		return true ;
	}

	// 调用超时后重发次数据删除数据
	void Destroy( void *data ) {
		if ( data == NULL )
			return ;
		_msgdata *p = ( _msgdata *) data;
		if ( p->_ptr != NULL ) {
			delete [] p->_ptr ;
		}
		delete p ;
	}
};

void AddMsg( CQueueMgr &mgr, const char *msg, int len, const char *id, int seq , int fd, bool send )
{
	_msgdata *p = new _msgdata ;
	p->_fd  = fd ;
	p->_seq = seq ;
	p->_ptr = new char[len+1] ;
	memcpy( p->_ptr , msg , len ) ;
	p->_len = len ;

	mgr.Add( id , seq , p, send ) ;
}

int main( int argc, char **argv )
{
	CQCaller caller ;
	CQueueMgr mgr( &caller ) ;

	const char *msg = "hello world" ;
	int len = strlen(msg) ;

	const char *id = "15001088478" ;

	AddMsg( mgr, msg, len, id, 1, 1,  true ) ;
	AddMsg( mgr, msg, len, id, 2, 2, false ) ;
	mgr.Remove( id, 2, true ) ;
	AddMsg( mgr, msg, len, id, 1, 3, true ) ;
	AddMsg( mgr, msg, len, id, 2, 4, true ) ;
	AddMsg( mgr, msg, len, id, 3, 5, true ) ;

	while(true) {
		sleep(1) ;
	}

	return 0 ;
}




