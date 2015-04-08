/*
 * nodeparse.h
 *
 *  Created on: 2011-11-11
 *      Author: humingqing
 */

#ifndef __NODEPARSE_H__
#define __NODEPARSE_H__

#include <stdio.h>
#include <nodeheader.h>
#include <arpa/inet.h>

class CNodeParser
{
public:
	CNodeParser() {};
	~CNodeParser() {};

	static const char * Decode( const char *data, int len )
	{
		static char buf[1024] = {0} ;

		if ( len < (int) sizeof(NodeHeader) )
			return NULL ;

		NodeHeader *header = ( NodeHeader *) data ;
		unsigned short cmd = ntohs( header->cmd ) ;
		unsigned int   seq = ntohl( header->seq ) ;
		if ( cmd < NODE_CONNECT_REQ || cmd > NODE_MSGCHG_RSP )
			return NULL ;

		switch( cmd ) {
		case NODE_CONNECT_REQ:
			sprintf( buf, "NODE_CONNECT_REQ , command %04x, seq %x" , cmd , seq ) ;
			break ;
		case NODE_CONNECT_RSP:
			sprintf( buf, "NODE_CONNECT_RSP , command %04x, seq %x" , cmd , seq) ;
			break ;
		case NODE_DISCONN_REQ:
			sprintf( buf, "NODE_DISCONN_REQ , command %04x, seq %x" , cmd , seq ) ;
			break;
		case NODE_DISCONN_RSP:
			sprintf( buf, "NODE_DISCONN_RSP , command %04x, seq %x" , cmd , seq ) ;
			break;
		case NODE_LINKTEST_REQ:
			sprintf( buf, "NODE_LINKTEST_REQ , command %04x, seq %x" , cmd , seq ) ;
			break;
		case NODE_LINKTEST_RSP:
			sprintf( buf, "NODE_LINKTEST_RSP , command %04x, seq %x" , cmd , seq ) ;
			break;
		case NODE_USERNAME_REQ:
			sprintf( buf, "NODE_USERNAME_REQ , command %04x, seq %x" , cmd , seq ) ;
			break ;
		case NODE_USERNAME_RSP:
			sprintf( buf, "NODE_USERNAME_RSP , command %04x, seq %x" , cmd , seq ) ;
			break ;
		case NODE_USERNOTIFY_REQ:
			sprintf( buf, "NODE_USERNOTIFY_REQ , command %04x, seq %x" , cmd , seq ) ;
			break ;
		case NODE_USERNOTIFY_RSP:
			sprintf( buf, "NODE_USERNOTIFY_RSP , command %04x, seq %x" , cmd , seq ) ;
			break ;
		case NODE_GETMSG_REQ:
			sprintf( buf, "NODE_GETMSG_REQ , command %04x, seq %x" , cmd , seq ) ;
			break ;
		case NODE_GETMSG_RSP:
			sprintf( buf, "NODE_GETMSG_RSP , command %04x, seq %x" , cmd , seq ) ;
			break ;
		case NODE_MSGERROR_REQ:
			sprintf( buf, "NODE_MSGERROR_REQ , command %04x, seq %x" , cmd , seq ) ;
			break ;
		case NODE_MSGERROR_RSP:
			sprintf( buf, "NODE_MSGERROR_RSP , command %04x, seq %x" , cmd , seq ) ;
			break ;
		case NODE_MSGCHG_REQ:
			sprintf( buf, "NODE_MSGCHG_REQ , command %04x, seq %x" , cmd , seq ) ;
			break ;
		case NODE_MSGCHG_RSP:
			sprintf( buf, "NODE_MSGCHG_RSP , command %04x, seq %x" , cmd , seq ) ;
			break ;
		}
		return buf ;
	}
};

#endif /* NODEPARSE_H_ */
