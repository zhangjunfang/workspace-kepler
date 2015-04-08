#include "TcpHandleEx.h"
#include "NetHandle.h"

CTcpHandleEx::CTcpHandleEx()
{
	_owner = NULL;
}

CTcpHandleEx::~CTcpHandleEx()
{

}

void CTcpHandleEx::set_owner(void* owner)
{
	_owner = owner;
}

void CTcpHandleEx::on_data_arrived( socket_t *sock, void* data, int len)
{
	if ( _owner != NULL )
		((CNetHandle*)_owner)->on_data_arrived( sock, data, len);
}

void CTcpHandleEx::on_dis_connection( socket_t *sock )
{
	if ( _owner != NULL )
		((CNetHandle*)_owner)->on_dis_connection( sock );
}

void CTcpHandleEx::on_new_connection( socket_t *sock , const char* ip, int port)
{
	if ( _owner != NULL )
		((CNetHandle*)_owner)->on_new_connection( sock, ip, port);
}

void CTcpHandleEx::on_send_failed( socket_t *sock, void* data, int len)
{
	if ( _owner != NULL )
		((CNetHandle*)_owner)->on_send_failed( sock, data , len );
}

// ¼ì²âIPÊÇ·ñÓĞĞ§
bool CTcpHandleEx::invalidate_ip( const char *ip )
{
	if ( _owner != NULL ){
		return ((CNetHandle*)_owner)->check_ip( ip ) ;
	}
	return true ;
}



