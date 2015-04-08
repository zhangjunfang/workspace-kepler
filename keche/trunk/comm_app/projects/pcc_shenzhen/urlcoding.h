/*
 * urlcoding.h
 *
 *  Created on: 2014-8-14
 *      Author: ycq
 */

#ifndef _URLCODING_H_
#define _URLCODING_H_

#include <string>
using std::string;

class UrlCoding {
	static const char _EnTab[0x11];

	const char _tag;
public:
	UrlCoding(char tag);
	virtual ~UrlCoding();

	string encode(const string &arg);
};

#endif//_URLCODING_H_
