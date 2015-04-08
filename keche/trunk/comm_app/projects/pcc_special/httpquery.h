/*
 * httpquery.h
 *
 *  Created on: 2013-4-3
 *      Author: Administrator
 */

#ifndef _HTTPQUERY_H_
#define _HTTPQUERY_H_ 1

#include <vector>
#include <string>

using std::vector;
using std::string;

#include <curl/curl.h>

class HttpQuery {
    CURL *_curl;

    vector<unsigned char> _data;

    static const char *user_agent;
    static size_t writeData(unsigned char *ptr, size_t count, size_t block, vector<unsigned char> *data);

    bool init();
public:
    HttpQuery();
    ~HttpQuery();

    bool get(const string &url);
	bool post(const string &url, const string &dat);

    unsigned char * data();
    size_t size();
};


#endif /* _HTTPQUERY_H_ */
