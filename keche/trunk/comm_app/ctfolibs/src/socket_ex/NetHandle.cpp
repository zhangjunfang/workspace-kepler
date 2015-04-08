#include "NetHandle.h"

CNetHandle::CNetHandle()
{
	_tcp_handle.set_owner(this);
	_udp_handle.set_owner(this);
}

CNetHandle::~CNetHandle()
{
//	cout<<"CNetHandle::~CNetHandle()"<<endl;
}

void CNetHandle::setpackspliter( IPackSpliter *pack )
{
	// 设置数据分包对象
	_tcp_handle.setpackspliter( pack ) ;
	_udp_handle.setpackspliter( pack ) ;
}
