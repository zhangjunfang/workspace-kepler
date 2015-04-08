/**********************************************
 * LoginCheck.h
 *
 *  Created on: 2010-12-9
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/

#ifndef LOGINCHECK_H_
#define LOGINCHECK_H_

#include "std.h"
#include <time.h>
#include <Mutex.h>

typedef struct _CheckInfo
{
	int access_code;
	string user_id;
	string user_password;
	string user_ip;
	unsigned int valid_flag;
	unsigned char encrypt ;  // 是否要加密
	unsigned int M1  ;  	 // 加密密钥1  M1_IA1_IC1
	unsigned int IA1 ;  	 // 加密密钥2
	unsigned int IC1 ;  	 // 加密密钥3

	_CheckInfo() {
		encrypt = 0 ;
		M1      = 0 ;
		IA1     = 0 ;
		IC1     = 0 ;
	}
}CheckInfo;

class LoginCheck
{
	typedef map<int,CheckInfo>  CMapCheckInfo;
public:
	LoginCheck(const char *config_file);
	virtual ~LoginCheck();
	// 检测用户是否为合法用户
	unsigned char CheckUser( int accesscode , const string &user_name, const string &user_password, const string &ip , unsigned int &flag );
	bool Reload(int timeval = 0);
	string get_config_file()
	{
		return _config_file;
	}
	// 取得用户的加密密钥
	bool GetEncryptKey( int access_code, unsigned int &M1, unsigned int &IA1, unsigned int &IC1 ) ;
private:

	string 				    _config_file;
	CMapCheckInfo      		_login_check_map;
	time_t 					_last_reload_time;
	share::Mutex 		    _mutex;
};

#endif /* LOGINCHECK_H_ */
