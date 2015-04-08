#include "utils.h"

unsigned char Utils::hex2bin(char ch)
{
	switch(ch) {
	case '0' ... '9':
		ch = ch - '0';
		break;
	case 'A' ... 'F':
		ch = ch - 'A' + 10;
		break;
	case 'a' ... 'f':
		ch = ch - 'a' + 10;
		break;
	default:
		ch = 0;
		break;
	}

	return ch;
}

size_t Utils::hex2array(const string &hex, uint8_t *arr)
{
	size_t pos = 0;
	size_t idx = 0;

	if ((hex.length() % 2) == 1) {
		arr[pos] = (arr[pos] << 4) | 0;
		++idx;
	}

	string::const_iterator it;
	for (it = hex.begin(); it != hex.end(); ++it) {
		arr[pos] = (arr[pos] << 4) | hex2bin(*it);

		if ((++idx % 2) == 0) ++pos;
	}

	return pos;
}


string Utils::array2hex(const uint8_t *arr, size_t len)
{
    size_t i;
    string res;
    char tmp[3];
    const char *tab = "0123456789abcdef";

    res.reserve(len * 2 + 1);
    for(i = 0; i < len; ++i) {
        tmp[0] = tab[arr[i] >> 4];
        tmp[1] = tab[arr[i] & 0xf];
        tmp[2] = '\0';
        res.append(tmp);
    }

    return res;
}

string Utils::filter(const string &str, const string &keys)
{
    size_t pos = 0;
    string tmp = str;

    while(pos < tmp.length()) {
        if((pos = tmp.find_first_of(keys, pos)) == string::npos) {
            break;
        }

        tmp.erase(pos, 1);
    }

    return tmp;
}

size_t Utils::enCode808(const unsigned char *src, size_t len, unsigned char *dst)
{
	size_t i, j;

	dst[0] = src[0];
	for (i = j = 1; i < len - 1; ++i) {
		switch (src[i]) {
		case 0x7d:
			dst[j++] = 0x7d;
			dst[j++] = 0x01;
			break;
		case 0x7e:
			dst[j++] = 0x7d;
			dst[j++] = 0x02;
			break;
		default:
			dst[j++] = src[i];
			break;
		}
	}
	dst[j++] = src[i];

	return j;
}

size_t Utils::deCode808(const unsigned char *src, size_t len, unsigned char *dst)
{
	size_t i, j;
	int flag;

	flag = 0;
	dst[0] = src[0];
	for (i = j = 1; i < len - 1; ++i) {
		if(src[i] == 0x7d) {
			flag = 1;
			continue;
		}

		if(flag == 0) {
			dst[j++] = src[i];
		} else if(flag == 1 && src[i] == 0x01) {
			dst[j++] = 0x7d;
		} else if(flag == 1 && src[i] == 0x02) {
			dst[j++] = 0x7e;
		}
		flag = 0;
	}
	dst[j++] = src[i];

	return j;
}
