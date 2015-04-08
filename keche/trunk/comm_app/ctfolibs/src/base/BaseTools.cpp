/**********************************************
 * BaseTools.cpp
 *
 *  Created on: 2010-8-2
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/
#include <stdio.h>
#include <string.h>
#include "BaseTools.h"

const string uitodecstr(unsigned int intger)
{
	char buf[16] = {0};
	sprintf(buf,"%u",(unsigned int)intger);
	return buf ;
}

const string ustodecstr(unsigned short us)
{
	char buf[16] = {0};
	sprintf(buf,"%u",us);
	return buf ;
}

const string chartodecstr(unsigned char uc)
{
	char buf[16] = {0};
	sprintf(buf,"%d",uc);
	return buf ;
}

const string charto2decstr(unsigned char uc)
{
	char buf[16] = {0};
	sprintf(buf,"%02d",uc);
	return buf ;
}

void print_hex(const char * buf ,int buf_len)
{
	if(buf == NULL || buf_len <= 0)
		return;

	for(int i = 0; i < buf_len; i++)
	{
		printf("%02x ",(unsigned char)buf[i]);
	}
	printf("\n");
}

void print_hex(char *dest,const char * buf ,int buf_len)
{
	if(buf == NULL || buf_len <= 0)
		return;

	for(int i = 0; i < buf_len; i++)
	{
		sprintf(dest,"%02x ",(unsigned char)buf[i]);
		dest += 3;
	}
	sprintf(dest,"\n");
}

static unsigned int power(int base, int times)
{
    int i;
    unsigned int rslt = 1;
    for(i=0; i<times; i++)
        rslt *= base;
    return rslt;
}

unsigned int BCDtouint( char *bcd, int len )
{
    int i, tmp;
    unsigned int dec = 0;
    for(i=0; i<len; i++)
    {
       tmp = ((bcd[i]>>4)&0x0F)*10 + (bcd[i]&0x0F);
       dec += tmp * power(100, len-1-i);
    }
    return dec;
}

// 将字符串转成BCD码
unsigned int strtoBCD( const char *str, char *bcd, int len )
{
	int n = strlen(str) ;
	if( n < len*2 )
		 return -1;

	for( int i=0; i< len; ++i )
	{
		bcd[i]  = ( ( (str[2*i] - 48 ) << 4 ) & 0xf0 ) ;
		bcd[i] |= ( ( str[2*i+1]- 48 ) & 0x0f ) ;
	}

	return 0;
}

// 将BCD码转成字符串
const string BCDtostr( char *bcd, int len )
{
	if ( len > 64 ) return "" ;

	char buf[129] = {0};
	for( int i = 0; i < len; ++ i )
	{
		buf[2*i]   = (( bcd[i] & 0xf0 ) >> 4 ) + 48 ;
		buf[2*i+1] = ( bcd[i] & 0x0f ) + 48 ;
	}
	return buf;
}

int UinttoBCD(unsigned int macid,char * bcd,int length)
{
	int i;
	int temp;
	for(i = length-1;i>=0;i--)
	{
		temp = macid%100;
		bcd[i] = ((temp/10)<<4) + ((temp%10) & 0x0F);
		macid = macid/100;
	}
	return 0;
}
