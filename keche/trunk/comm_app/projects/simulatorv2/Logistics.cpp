/**
 * Author: xifengming
 * date:   2012-11-17
 * memo:   物流模拟数据
 */
#include <tools.h>
#include <vector>
#include <comlog.h>
#include <BaseTools.h>
#include <GBProtoParse.h>
#include "Logistics.h"
//--------------------------------------------------------------------------
static void get_hex_from_str( unsigned int &ntValue, string &s_str )
{
	sscanf( s_str.c_str(), "%d", & ntValue );
}
//--------------------------------------------------------------------------
CLogistics::CLogistics( )
{
	seq = 0;
	memset( & _auto_data_schedule, 0, sizeof ( _auto_data_schedule ) );
}
//--------------------------------------------------------------------------
CLogistics::~CLogistics( )
{

}
//--------------------------------------------------------------------------
bool CLogistics::LoadInitFile( const char *szName )
{
	int len = 0;
	char *buf = ReadFile( szName, len );

	if ( buf == NULL || len == 0 ) {
		printf( "read file %s failed\n", szName );
		return false;
	}

	char *ptr = buf;
	char *p   = strstr( ptr, "$\n" );

	string line;
	while ( p != NULL ) {
		* p = 0;
		if ( ptr != NULL ) {
			line = ptr;
			vector < string > vec_temp;
			splitvector( line, vec_temp, ":", 0 );
			ParseParam (vec_temp);
		}

		ptr = p + 2;
		p = strstr( ptr, "$\n" );
	}

	return true;
}
//--------------------------------------------------------------------------
void CLogistics::ParseParam( vector< string > &vec_param )
{
	unsigned short wtype = 0x00;

	string type = vec_param[0];
	sscanf( type.c_str(), "%4x", &wtype );

	switch ( wtype )
	{
	case UPLOAD_DATAINFO_REQ: //0x1022 自动配货上报
	{
		Init_Auto_Data_Schedule( vec_param );
		break;
	}
	case UP_CARDATA_INFO_REQ: //0x1023 上传配货信息
	{
		Init_Car_Data_Info( vec_param );
		break;
	}
	case UP_ORDER_FORM_INFO_REQ:/*0x1027 初始化上传订单状态*/
	{
		Init_Order_Form_Info( vec_param );
		break;
	}
	case UP_TRANSPORT_FORM_INFO_REQ: /*0x1028 初始化上传运单状态*/
	{
		Init_Transport_Form_Info( vec_param );
		break;
	}
	default:
		break;
	}
}
//--------------------------------------------------------------------------
/*0x1022 初始化配货上报*/
void CLogistics::Init_Auto_Data_Schedule( vector< string > &vec_param )
{
	unsigned int dwTemp = 0;

	get_hex_from_str( dwTemp, vec_param[1] );
	_auto_data_schedule.byState = dwTemp;
	get_hex_from_str( dwTemp, vec_param[2] );
	_auto_data_schedule.bySpace = dwTemp;
	get_hex_from_str( dwTemp, vec_param[3] );
	_auto_data_schedule.w_weight = htons( dwTemp );
	strtoBCD( vec_param[4].c_str(), ( char* ) _auto_data_schedule.byTime );
	get_hex_from_str( dwTemp, vec_param[5] );
	_auto_data_schedule.dwSarea = htonl( dwTemp );
	get_hex_from_str( dwTemp, vec_param[6] );
	_auto_data_schedule.dwDarea = htonl( dwTemp );
}
//--------------------------------------------------------------------------
/*0x1023 初始化上传配货信息*/
void CLogistics::Init_Car_Data_Info( vector< string > &vec_param )
{
	unsigned int dwTemp = 0;
	memcpy( _car_data_info._sid, vec_param[1].c_str(), vec_param[1].length() );

	get_hex_from_str( dwTemp, vec_param[2] );
	_car_data_info._status = dwTemp;
	get_hex_from_str( dwTemp, vec_param[3] );
	_car_data_info._price = htonl( dwTemp );
}
//--------------------------------------------------------------------------
/*0x1027 初始化上传订单状态*/
void CLogistics::Init_Order_Form_Info( vector< string > &vec_param )
{
	unsigned int dwTemp = 0;

	memcpy( _order_form_info._sid, vec_param[1].c_str(), vec_param[1].length() );
	memcpy( _order_form_info._order_form, vec_param[2].c_str(), vec_param[2].length() );

	get_hex_from_str( dwTemp, vec_param[3] );
	_order_form_info._action = dwTemp;
	get_hex_from_str( dwTemp, vec_param[4] );
	_order_form_info._status = dwTemp;
	get_hex_from_str( dwTemp, vec_param[5] );
	_order_form_info._lon = htonl( dwTemp );
	get_hex_from_str( dwTemp, vec_param[6] );
	_order_form_info._lat = htonl( dwTemp );
}
//--------------------------------------------------------------------------
/*0x1028 初始化上传运单状态*/
void CLogistics::Init_Transport_Form_Info( vector< string > &vec_param )
{
	unsigned int dwTemp = 0;
	memcpy( _transport_form_info._sid, vec_param[1].c_str(), vec_param[1].length() );
	get_hex_from_str( dwTemp, vec_param[2] );
	_transport_form_info._action = dwTemp;
	get_hex_from_str( dwTemp, vec_param[3] );
	_transport_form_info._status = dwTemp;
	get_hex_from_str( dwTemp, vec_param[4] );
	_transport_form_info._lon = htonl( dwTemp );
	get_hex_from_str( dwTemp, vec_param[5] );
	_transport_form_info._lat = htonl( dwTemp );
}
//--------------------------------------------------------------------------
/*解析平台通用应答回复*/
void CLogistics::Parse0x8000Frame( unsigned char *lpData )
{
	RSP_PLATFORM_COMMON *rep = ( RSP_PLATFORM_COMMON * ) lpData;

	OUT_INFO( NULL, 0, NULL, "0x8000 RSP Type: %d", ntohs(rep->wType) );
	OUT_INFO( NULL, 0, NULL, "0x8000 RSP Result: %d", rep->byResult );
}
//--------------------------------------------------------------------------
/*下发配货信息*/
int CLogistics::Parse0x1021Frame( unsigned char *lpSrcData, int msglen, unsigned int nSeq, DataBuffer &pRetuBuf )
{
	CreateTerminalCommRspFrame( SEND_CARDAT_INFO_REQ, nSeq, pRetuBuf );
	return pRetuBuf.getLength();
}
//--------------------------------------------------------------------------
//下发运单信息
int CLogistics::Parse0x1025Frame( unsigned char *lpSrcData, int msglen, unsigned int nSeq, DataBuffer &pRetuBuf )
{
	CreateTerminalCommRspFrame( SEND_TRANSPORT_ORDER_FORM_INFO_REQ, nSeq, pRetuBuf );
	return pRetuBuf.getLength();
}
//--------------------------------------------------------------------------
//配货成交状态确认
int CLogistics::Parse0x1024Frame( unsigned char *lpSrcData, int msglen, unsigned int nSeq, DataBuffer &pRetuBuf )
{
	CreateTerminalCommRspFrame( SEND_CARDATA_INFO_CONFIRM_REQ, nSeq, pRetuBuf );
	return pRetuBuf.getLength();
}
//--------------------------------------------------------------------------
/*终端通用回复*/
void CLogistics::CreateTerminalCommRspFrame( unsigned short wMsgType, unsigned int nSeq, DataBuffer &pBuf )
{
	RSP_PLATFORM_COMMON pTerminalCommRsp = { 0 };

	pTerminalCommRsp.wType = htons( wMsgType );
	pTerminalCommRsp.byResult = 0x00;

	CreateRequestFrame( TERMINAL_COMMON_RSP, ( unsigned char * ) & pTerminalCommRsp, sizeof ( pTerminalCommRsp ), nSeq,
			pBuf );
}
//--------------------------------------------------------------------------
int CLogistics::CreateRequestFrame( unsigned short wDataType, unsigned char *lpData, unsigned short nLen,
		unsigned int nSeq, DataBuffer &pBuf )
{
	pBuf.writeInt8( 0x01 ); //透传类型

	MSG_HEADER pMsgHeader;
	memset( & pMsgHeader, 0, sizeof(MSG_HEADER) );

	pMsgHeader.wMsgVer = htons( 0x0001 );
	pMsgHeader.wMsgType = htons( wDataType );
	pMsgHeader.dwMsgSeq = htonl( nSeq );
	pMsgHeader.dwDataLen = htonl( nLen );

	int nLens = sizeof(MSG_HEADER) + nLen + 1; //+1 透传数据类型

	pBuf.writeBlock( & pMsgHeader, sizeof(MSG_HEADER) );
	pBuf.writeBlock( lpData, nLen );

	return nLens;
}
//--------------------------------------------------------------------------
int CLogistics::BuildTransportData( unsigned short wType, DataBuffer &pBuf )
{
	int nLen = 0;
	//unsigned short wType = UP_TRANSPORT_FORM_INFO_REQ;

	switch ( wType )
	{
	case UP_CARDATA_INFO_REQ: //0x1023 上报配货信息
	{
		nLen = CreateRequestFrame( UP_CARDATA_INFO_REQ, ( unsigned char * ) & _car_data_info, sizeof ( _car_data_info ),
				seq ++, pBuf );
		break;
	}
	case UPLOAD_DATAINFO_REQ: //0x1022 配货状态上报
	{
		nLen = CreateRequestFrame( UPLOAD_DATAINFO_REQ, ( unsigned char * ) & _car_data_info, sizeof ( _car_data_info ),
				seq ++, pBuf );
		break;
	}
	case UP_ORDER_FORM_INFO_REQ:/*0x1027 初始化上传订单状态*/
	{
		nLen = CreateRequestFrame( UP_ORDER_FORM_INFO_REQ, ( unsigned char * ) & _order_form_info,
				sizeof ( _order_form_info ), seq ++, pBuf );
		break;
	}
	case UP_TRANSPORT_FORM_INFO_REQ:/*0x1028 初始化上传运单状态*/
	{
		nLen = CreateRequestFrame( UP_TRANSPORT_FORM_INFO_REQ, ( unsigned char * ) & _transport_form_info,
				sizeof ( _transport_form_info ), seq ++, pBuf );
		break;
	}
	default:
		break;
	}

	if ( nLen > 2048 ) return - 1;

	return nLen;
}

//--------------------------------------------------------------------------
int CLogistics::ParseTransparentMsgData( unsigned char *lpData, int nLen, DataBuffer &pRetBuf )
{
	int nLens = 0;

	MSG_HEADER *lpMsgHeader = ( MSG_HEADER * ) lpData;

	unsigned int nSeq = ntohl( lpMsgHeader->dwMsgSeq );

	switch ( ntohs( lpMsgHeader->wMsgType ) )
	{
	case PLATFORM_COMMON_RSP: //平台通用应答
	{
		Parse0x8000Frame( & lpData[sizeof(MSG_HEADER)] );
		break;
	}
	case SEND_CARDAT_INFO_REQ: //下发配货信息
	{
		nLens = Parse0x1021Frame( & lpData[sizeof(MSG_HEADER)], nLen, nSeq, pRetBuf );
		break;
	}
	case SEND_TRANSPORT_ORDER_FORM_INFO_REQ: //下发运单
	{
		nLens = Parse0x1025Frame( & lpData[sizeof(MSG_HEADER)], nLen, nSeq, pRetBuf );
		break;
	}
	case SEND_CARDATA_INFO_CONFIRM_REQ: //下发成交配货状态
	{
		nLens = Parse0x1024Frame( & lpData[sizeof(MSG_HEADER)], nLen, nSeq, pRetBuf );
		break;
	}
	default:
		break;
	}
	return nLens;
}
//--------------------------------------------------------------------------
