/**********************************************
 * BaseTools.h
 *
 *  Created on: 2010-8-2
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/

#ifndef BASETOOLS_H_
#define BASETOOLS_H_

#include <string>
#include <iostream>
using namespace std;

const string uitodecstr(unsigned int intger);
const string ustodecstr(unsigned short us);
const string chartodecstr(unsigned char uc);
const string charto2decstr(unsigned char uc);

void print_hex(const char * buf ,int buf_len);
void print_hex(char *dest,const char * buf ,int buf_len);

unsigned int BCDtouint(char *bcd, int len = 6 );
const string BCDtostr(char *bcd, int len=6 ) ;
int UinttoBCD(unsigned int macid,char * bcd,int length);
unsigned int strtoBCD(const char *str,char *bcd, int len=6);

#endif /* BASETOOLS_H_ */
