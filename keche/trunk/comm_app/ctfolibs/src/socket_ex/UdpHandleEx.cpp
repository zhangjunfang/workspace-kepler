#include "UdpHandleEx.h"
#include "NetHandle.h"

CUdpHandleEx::CUdpHandleEx()
{
	_owner = NULL;
}

CUdpHandleEx::~CUdpHandleEx()
{

}

void CUdpHandleEx::set_owner(void* owner)
{
	_owner = owner;
}

void CUdpHandleEx::on_data_arrived( socket_t *sock , void* data, int len)
{
	if ( _owner != NULL )
		((CNetHandle*)_owner)->on_data_arrived( sock, data, len);
}

void CUdpHandleEx::on_dis_connection( socket_t *sock )
{
	if ( _owner != NULL )
		((CNetHandle*)_owner)->on_dis_connection( sock );
}

void CUdpHandleEx::on_new_connection( socket_t *sock , const char* ip, int port)
{
	if ( _owner != NULL )
		((CNetHandle*)_owner)->on_new_connection( sock, ip, port);
}


