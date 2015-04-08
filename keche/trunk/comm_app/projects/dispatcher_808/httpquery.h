/*
 * httpquery.h
 *
 *  Created on: 2013-4-3
 *      Author: Administrator
 */

#ifndef _HTTPQUERY_H_
#define _HTTPQUERY_H_ 1

#include <list>
#include <vector>
#include <string>

using std::list;
using std::vector;
using std::string;

#include <curl/curl.h>

class HttpQuery {
    CURL *_curl;
	struct curl_slist *_headers;

	int _timeout;
    vector<unsigned char> _data;

    static const char *user_agent;
    static size_t writeData(unsigned char *ptr, size_t count, size_t block, vector<unsigned char> *data);

    bool init(const string &url);
	bool perform();
public:
    HttpQuery();
    ~HttpQuery();

    bool get(const string &url);
	bool post(const string &url, const string &dat);

	void header(const list<string> &headers);
	void timeout(int tv);

    unsigned char * data();
    size_t size();
};


#endif /* _HTTPQUERY_H_ */
