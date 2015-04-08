/*
 * urlcoding.cpp
 *
 *  Created on: 2014-8-14
 *      Author: ycq
 */

#include "urlcoding.h"
#include <vector>
using std::vector;

UrlCoding::UrlCoding(char tag) : _tag(tag){}

UrlCoding::~UrlCoding() {}

const char UrlCoding::_EnTab[] = "0123456789ABCDEF";

string UrlCoding::encode(const string &arg)
{
	unsigned char ch;
	string::const_iterator it;

	vector<char> buf(arg.length() * 3 + 1);
	char *ptr = &buf[0];
	size_t len = 0;

	for(it = arg.begin(); it != arg.end(); ++it) {
		ch = *it;
		switch(ch) {
		case '.':
		case '-':
		case '*':
		case '_':
		case '0' ... '9':
		case 'a' ... 'z':
		case 'A' ... 'Z':
			ptr[len++] = ch;
			break;
		case ' ':
			ptr[len++] = '+';
			break;
		default:
			ptr[len++] = _tag;
			ptr[len++] = _EnTab[ch >> 4];
			ptr[len++] = _EnTab[ch & 0xf];
			break;
		}
	}

	return string(ptr, len);
}
