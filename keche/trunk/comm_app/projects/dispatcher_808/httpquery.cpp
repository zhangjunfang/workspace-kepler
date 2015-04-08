/*
 * httpquery.cpp
 *
 *  Created on: 2013-4-3
 *      Author: ycq
 */

#include <poll.h>
#include "httpquery.h"

const char* HttpQuery::user_agent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0";

static struct AutoInit {
	AutoInit() {
		curl_global_init(CURL_GLOBAL_ALL);
	}

	~AutoInit() {
		curl_global_cleanup();
	}
} _autoInit;

size_t HttpQuery::writeData(unsigned char *ptr, size_t count, size_t block, vector<unsigned char> *data)
{
	size_t len = count * block;

	data->insert(data->end(), ptr, ptr + len);

	return len;
}

void HttpQuery::header(const list<string> &headers)
{
	if(_headers != NULL) {
		curl_slist_free_all(_headers);
		_headers = NULL;
	}

	list<string>::const_iterator it;
	for(it = headers.begin(); it != headers.end(); ++it) {
		_headers = curl_slist_append(_headers, it->c_str());
	}
}

void HttpQuery::timeout(int timeout)
{
	_timeout = timeout;
}

bool HttpQuery::init(const string &url)
{
	_data.clear();
	curl_easy_reset(_curl);

	if (curl_easy_setopt(_curl, CURLOPT_URL, url.c_str()) != CURLE_OK) {
		return false;
	}

	if (curl_easy_setopt(_curl, CURLOPT_FORBID_REUSE, 1) != CURLE_OK) {
		return false;
	}
	
	if (curl_easy_setopt(_curl, CURLOPT_NOSIGNAL, 1) != CURLE_OK) {
		return false;
	}
	
	if (curl_easy_setopt(_curl, CURLOPT_USERAGENT, user_agent) != CURLE_OK) {
		return false;
	}

	if (curl_easy_setopt(_curl, CURLOPT_WRITEDATA, &_data) != CURLE_OK) {
		return false;
	}

	if (curl_easy_setopt(_curl, CURLOPT_WRITEFUNCTION, writeData) != CURLE_OK) {
		return false;
	}

	if(_headers != NULL) {
		if(curl_easy_setopt(_curl, CURLOPT_HTTPHEADER, _headers) != CURLE_OK) {
			return false;
		}
	}

	return true;
}

HttpQuery::HttpQuery() {
	_curl = curl_easy_init();
	_data.reserve(1024);
	_headers = NULL;
	_timeout = 3;
}

HttpQuery::~HttpQuery() {
	curl_easy_cleanup(_curl);

	if(_headers != NULL) {
		curl_slist_free_all(_headers);
		_headers = NULL;
	}

}

bool HttpQuery::perform()
{
	int running;
	CURLM *multi;

	if((multi = curl_multi_init()) == NULL) {
		return false;
	}
	curl_multi_add_handle(multi, _curl);

	time_t timeout = time(NULL) + _timeout;
	while(timeout > time(NULL)) {
		if(curl_multi_perform(multi, &running) == CURLM_CALL_MULTI_PERFORM) {
			continue;
		}

		if(running == 0) {
			break;
		}

		usleep(1000);
	}
	curl_multi_remove_handle(multi, _curl);
	curl_multi_cleanup(multi);

	if(running != 0) {
		return false;
	}

	return true;
}

bool HttpQuery::get(const string &url) {
	if (_curl == NULL || init(url) == false) {
		return false;
	}

	if (curl_easy_setopt(_curl, CURLOPT_HTTPGET, 1) != CURLE_OK) {
		return false;
	}
	if( ! perform()) {
		return false;
	}
	/*
	if (curl_easy_perform(_curl) != CURLE_OK) {
		return false;
	}
	*/

	long httpCode = 0;
	if (curl_easy_getinfo(_curl, CURLINFO_HTTP_CODE, &httpCode) != CURLE_OK) {
		return false;
	}

	if (httpCode != 200) {
		return false;
	}

	return true;
}
bool HttpQuery::post(const string &url, const string &dat) {
	if (_curl == NULL || init(url) == false) {
		return false;
	}

	if (curl_easy_setopt(_curl, CURLOPT_POSTFIELDS, dat.c_str()) != CURLE_OK) {
		return false;
	}
	if( ! perform()) {
		return false;
	}
	/*
	if (curl_easy_perform(_curl) != CURLE_OK) {
		return false;
	}
	*/

	long httpCode = 0;
	if (curl_easy_getinfo(_curl, CURLINFO_HTTP_CODE, &httpCode) != CURLE_OK) {
		return false;
	}

	if (httpCode != 200) {
		return false;
	}

	return true;
}

unsigned char* HttpQuery::data() {
	if(_data.empty()) {
		return NULL;
	}

	return &_data[0];
}

size_t HttpQuery::size() {
	return _data.size();
}
