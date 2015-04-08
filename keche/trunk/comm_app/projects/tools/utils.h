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
	static size_t splitStr(const string &str, T &res, char tag) {
		string::size_type prev = 0;
		string::size_type next = 0;
		string::size_type size = str.size();
		while (next < size) {
			if ((next = str.find(tag, prev)) == string::npos) {
				next = size;
			}

			res.push_back(str.substr(prev, next - prev));
			prev = next + 1;
		}

		return res.size();
	}

	template<class T>
	static size_t splitStr(const string &str, T &res, char tagb, char tage) {
		string::size_type i;

		string::size_type len = str.length();
		string::size_type posb = string::npos;
		string::size_type pose = string::npos;
		for (i = 0; i < len; ++i) {
			if (str[i] == tagb) {
				posb = i;
			} else if (str[i] == tage) {
				pose = i;
			}

			if (posb != string::npos && pose != string::npos && posb < pose) {
				++posb;
				res.push_back(str.substr(posb, pose - posb));
				posb = string::npos;
				pose = string::npos;
			}
		}

		return res.size();
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

	static size_t enCode808(const unsigned char *src, size_t len, unsigned char *dst);
	static size_t deCode808(const unsigned char *src, size_t len, unsigned char *dst);
};

#endif//_UTILS_
