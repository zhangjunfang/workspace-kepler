/*
 * httpquery.cpp
 *
 *  Created on: 2013-4-3
 *      Author: ycq
 */

#include "httpquery.h"

const char* HttpQuery::user_agent = "User-Agent: Mozilla/5.0 (Windows NT 5.1; rv:19.0) Gecko/20100101 Firefox/19.0";

size_t HttpQuery::writeData(unsigned char *ptr, size_t count, size_t block, vector<unsigned char> *data)
{
	size_t len = count * block;

	data->insert(data->end(), ptr, ptr + len);

	return len;
}

bool HttpQuery::init()
{
	if (curl_easy_setopt(_curl, CURLOPT_FORBID_REUSE, 1) != CURLE_OK) {
		return false;
	}

	/*
	if (curl_easy_setopt(_curl, CURLOPT_CONNECTTIMEOUT, 3) != CURLE_OK) {
		return false;
	}

	if (curl_easy_setopt(_curl, CURLOPT_TIMEOUT, 3) != CURLE_OK) {
		return false;
	}
	*/

	if (curl_easy_setopt(_curl, CURLOPT_NOSIGNAL, 1) != CURLE_OK) {
		return false;
	}

	if (curl_easy_setopt(_curl, CURLOPT_USERAGENT, user_agent) != CURLE_OK) {
		return false;
	}

	if (curl_easy_setopt(_curl, CURLOPT_WRITEFUNCTION, writeData) != CURLE_OK) {
		return false;
	}

	return true;
}

HttpQuery::HttpQuery() {
	_curl = curl_easy_init();
	_data.reserve(1024 * 1024);
}

HttpQuery::~HttpQuery() {
	curl_easy_cleanup(_curl);
}

bool HttpQuery::get(const string &url) {
	if (_curl == NULL) {
		return false;
	}

	_data.clear();
	curl_easy_reset(_curl);

	if (init() == false) {
		return false;
	}

	if (curl_easy_setopt(_curl, CURLOPT_URL, url.c_str()) != CURLE_OK) {
		return false;
	}

	if (curl_easy_setopt(_curl, CURLOPT_WRITEDATA, &_data) != CURLE_OK) {
		return false;
	}

	if (curl_easy_perform(_curl) != CURLE_OK) {
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
