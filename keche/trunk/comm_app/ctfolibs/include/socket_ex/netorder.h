/**
 * Author: humingqing
 * Date:   2011-10-14
 * Memo:   网络字节序转换处理，增加对int64_t数据类型进行处理
 */
#ifndef __NETORDER_H__
#define __NETORDER_H__

#include <arpa/inet.h>

#ifndef __BYTE_ORDER
# if defined(BYTE_ORDER) && defined(LITTLE_ENDIAN) && defined(BIG_ENDIAN)
#  define __BYTE_ORDER BYTE_ORDER
#  define __LITTLE_ENDIAN LITTLE_ENDIAN
#  define __BIG_ENDIAN BIG_ENDIAN
# else
#  include <boost/config.hpp>
#  define __BYTE_ORDER BOOST_BYTE_ORDER
#  define __LITTLE_ENDIAN BOOST_LITTLE_ENDIAN
#  define __BIG_ENDIAN BOOST_BIG_ENDIAN
# endif
#endif

#if __BYTE_ORDER == __BIG_ENDIAN
#  define ntohll(n) (n)
#  define htonll(n) (n)
# if defined(__GNUC__) && defined(__GLIBC__)
#  include <byteswap.h>
#  define htolell(n) bswap_64(n)
#  define letohll(n) bswap_64(n)
# else /* GNUC & GLIBC */
#  define bswap_64(n) \
      ( (((n) & 0xff00000000000000ull) >> 56) \
      | (((n) & 0x00ff000000000000ull) >> 40) \
      | (((n) & 0x0000ff0000000000ull) >> 24) \
      | (((n) & 0x000000ff00000000ull) >> 8)  \
      | (((n) & 0x00000000ff000000ull) << 8)  \
      | (((n) & 0x0000000000ff0000ull) << 24) \
      | (((n) & 0x000000000000ff00ull) << 40) \
      | (((n) & 0x00000000000000ffull) << 56) )
#  define htolell(n) bswap_64(n)
#  define letohll(n) bswap_64(n)
# endif /* GNUC & GLIBC */
#elif __BYTE_ORDER == __LITTLE_ENDIAN
#  define htolell(n) (n)
#  define letohll(n) (n)
# if defined(__GNUC__) && defined(__GLIBC__)
#  include <byteswap.h>
#  define ntohll(n) bswap_64(n)
#  define htonll(n) bswap_64(n)
# else /* GNUC & GLIBC */
#  define ntohll(n) ( (((unsigned long long)ntohl(n)) << 32) + ntohl(n >> 32) )
#  define htonll(n) ( (((unsigned long long)htonl(n)) << 32) + htonl(n >> 32) )
# endif /* GNUC & GLIBC */
#else /* __BYTE_ORDER */
# error "Can't define htonll or ntohll!"
#endif

#endif
