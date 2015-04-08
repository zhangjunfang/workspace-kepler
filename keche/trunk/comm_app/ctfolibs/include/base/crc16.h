#ifndef _CRC16_H_
#define _CRC16_H_

unsigned short crc16_ccitt_tab(const char *buf, int len) ;

unsigned short crc16_ccitt_xor(const char * buf, int len) ;

unsigned short GetCrcCode(const char *buf,int len);

#endif /* _CRC16_H_ */
