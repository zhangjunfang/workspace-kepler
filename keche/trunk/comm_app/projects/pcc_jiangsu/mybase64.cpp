/*
 * mybase64.cpp
 *
 *  Created on: 2012-3-2
 *      Author: humingqing
 *
 *  特殊的base64码表处理方法
 */

#include "mybase64.h"
#include <stdio.h>
#include <stdlib.h>

/* {{{ */
static const char *base64_table = "0123456789:;ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
static const char base64_pad = '=';

// 根据上面base64码表反推出对应表
static const short base64_reverse_table[256] = {
	-2,-2,-2,-2,-2,-2,-2,-2,-2,-1,-1,-2,-2,-1,-2,-2,
	-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,
	-1,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,
	0,1,2,3,4,5,6,7,8,9,10,11,-2,-2,-2,-2,
	-2,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,
	27,28,29,30,31,32,33,34,35,36,37,-2,-2,-2,-2,-2,
	-2,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,
	53,54,55,56,57,58,59,60,61,62,63,-2,-2,-2,-2,-2,
	-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,
	-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,
	-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,
	-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,
	-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,
	-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,
	-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,
	-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2,-2
};
/* }}} */

/* {{{ base64_encode */
static unsigned char *base64_encode_ex(const unsigned char *str, int length, int *ret_length)
{
	const unsigned char *current = str;
	unsigned char *p;
	unsigned char *result;

	if ((length + 2) < 0 || ((length + 2) / 3) >= (1 << (sizeof(int) * 8 - 2))) {
		if (ret_length != NULL) {
			*ret_length = 0;
		}
		return NULL;
	}

	result = (unsigned char *)malloc( ( ((length + 2) / 3) * 4 + 4 ) * sizeof(char) );
	p = result;

	while (length > 2) { /* keep going until we have less than 24 bits */
		*p++ = base64_table[current[0] >> 2];
		*p++ = base64_table[((current[0] & 0x03) << 4) + (current[1] >> 4)];
		*p++ = base64_table[((current[1] & 0x0f) << 2) + (current[2] >> 6)];
		*p++ = base64_table[current[2] & 0x3f];

		current += 3;
		length -= 3; /* we just handle 3 octets of data */
	}

	/* now deal with the tail end of things */
	if (length != 0) {
		*p++ = base64_table[current[0] >> 2];
		if (length > 1) {
			*p++ = base64_table[((current[0] & 0x03) << 4) + (current[1] >> 4)];
			*p++ = base64_table[(current[1] & 0x0f) << 2];
			//*p++ = base64_pad;
		} else {
			*p++ = base64_table[(current[0] & 0x03) << 4];
			//*p++ = base64_pad;
			//*p++ = base64_pad;
		}
	}
	if (ret_length != NULL) {
		*ret_length = (int)(p - result);
	}
	*p = '\0';
	return result;
}
/* }}} */

/* {{{ base64_decode */
/* as above, but backwards. :) */
static unsigned char *base64_decode_ex(const unsigned char *str, int length, int *ret_length )
{
	const unsigned char *current = str;
	int ch, i = 0, j = 0, k;
	/* this sucks for threaded environments */
	unsigned char *result;

	result = (unsigned char *)malloc(length + 1);

	/* run through the whole string, converting as we go */
	while ((ch = *current++) != '\0' && length-- > 0) {
		if (ch == base64_pad) {
			if (*current != '=' && (i % 4) == 1) {
				free(result);
				return NULL;
			}
			continue;
		}

		ch = base64_reverse_table[ch];
		if ( ch < 0 || ch == -1 ) { /* a space or some other separator character, we simply skip over */
			continue;
		} else if (ch == -2) {
			free(result);
			return NULL;
		}

		switch(i % 4) {
		case 0:
			result[j] = ch << 2;
			break;
		case 1:
			result[j++] |= ch >> 4;
			result[j] = (ch & 0x0f) << 4;
			break;
		case 2:
			result[j++] |= ch >>2;
			result[j] = (ch & 0x03) << 6;
			break;
		case 3:
			result[j++] |= ch;
			break;
		}
		i++;
	}

	k = j;
	/* mop things up if we ended on a boundary */
	if (ch == base64_pad) {
		switch(i % 4) {
		case 1:
			free(result);
			return NULL;
		case 2:
			k++;
		case 3:
			result[k++] = 0;
		}
	}
	if(ret_length) {
		*ret_length = j;
	}
	result[j] = '\0';
	return result;
}
/* }}} */

static void base64_free_ex( void *ptr )
{
	if ( ptr == NULL )
	{
		return;
	}
	free( ptr ) ;
}


CBase64Ex::CBase64Ex(): _pData(NULL), _size(0)
{
}

CBase64Ex::~CBase64Ex()
{
	if ( _pData != NULL ){
		base64_free_ex( _pData ) ;
		_size = 0 ;
	}
}

// 加密处理
bool CBase64Ex::Encode( const char *data, int len )
{
	int nsize = 0 ;
	char *ptr = (char *) base64_encode_ex( (unsigned char *)data, len , &nsize ) ;
	if ( ptr == NULL )
		return false ;

	if ( _pData != NULL )
		base64_free_ex( _pData ) ;

	_pData = ptr ;
	_size  = nsize ;

	return true ;
}

// 解密处理
bool CBase64Ex::Decode( const char *data, int len )
{
	int nsize = 0 ;
	char *ptr = (char *)base64_decode_ex((unsigned char *) data, len , &nsize ) ;
	if ( ptr == NULL )
		return false ;

	if ( _pData != NULL )
		base64_free_ex( _pData ) ;

	_pData = ptr ;
	_size  = nsize ;

	return true ;
}

// 加密数据
const std::string  CBase64Ex::EncodeEx( const char *data, int len )
{
	std::string s ;
	if ( ! Encode( data, len ) ) {
		return s ;
	}
	s.append( _pData, _size ) ;
	return s ;
}

// 解密数据
const std::string  CBase64Ex::DecodeEx( const char *data, int len )
{
	std::string s;
	if ( ! Decode( data, len ) ) {
		return s ;
	}
	s.append( _pData, _size ) ;
	return s ;
}



