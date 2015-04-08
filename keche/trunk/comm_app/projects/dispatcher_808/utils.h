#ifndef _UTILS_
#define _UTILS_ 1

#include <list>
#include <vector>
#include <string>
#include <sstream>

using std::list;
using std::vector;
using std::string;
using std::ios;
using std::istringstream;
using std::ostringstream;

class Utils {
	static unsigned char hex2bin(char ch);
public:
	template<class T>
	static size_t splitStr(const string &str, T &res, char tag)
	{
		string::size_type prev;
		string::size_type next;
		string::size_type size;

		size_t cnt = 0;

		prev = 0;
		next = 0;
		size = str.size();
		while(next < size) {
			if((next = str.find(tag, prev)) == string::npos) {
				next = size;
			}

			res.push_back(str.substr(prev, next - prev));
			++cnt;

			prev = next + 1;
		}

		return cnt;
	}

	template<class T>
	static T& str2int(const string &str, T &val, const ios::fmtflags &fmt = ios::dec)
	{
	    istringstream iss(str);

	    iss.setf(fmt, ios::basefield);
	    iss>>val;

	    return val;
	}

	template<class T>
	static string& int2str(const T &val, string &str, const ios::fmtflags &fmt = ios::dec)
	{
	    ostringstream oss;

	    oss.setf(fmt, ios::basefield);
	    oss<<(val + 0);

	    return str = oss.str();
	}

	static string array2hex(const uint8_t *arr, size_t len);
	static size_t hex2array(const string &hex, uint8_t *arr);

	static string filter(const string &str, const string &keys);
};

#endif//_UTILS_
