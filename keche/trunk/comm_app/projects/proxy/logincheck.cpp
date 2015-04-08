/**********************************************
 * LoginCheck.cpp
 *
 *  Created on: 2010-12-9
 *      Author: LiuBo
 *       Email:liubo060807@gmail.com
 *    Comments:
 *********************************************/

#include "logincheck.h"
#include "tools.h"

LoginCheck::LoginCheck(const char * config_file)
{
	// TODO Auto-generated constructor stub
	_config_file 	  = config_file;
	_last_reload_time = 0;
}

LoginCheck::~LoginCheck()
{
	// TODO Auto-generated destructor stub
}

unsigned char LoginCheck::CheckUser( int access_code, const string &user_name, const string &user_password, const string &ip , unsigned int &flag )
{
	share::Guard g( _mutex ) ;
	{
		CMapCheckInfo::iterator iter = _login_check_map.find(access_code);
		if(iter == _login_check_map.end())
		{
			return 2;
		}
		if(user_name !=  iter->second.user_id)
		{
			return 3;
		}
		if(user_password != iter->second.user_password)
		{
			return 4;
		}
		/**
		if(ip != iter->second.user_ip)
		{
			unlock();
			return 1;
		}*/

		flag = iter->second.valid_flag ;

		return 0;
	}
}

// 取得用户的加密密钥
bool LoginCheck::GetEncryptKey( int access_code, unsigned int &M1, unsigned int &IA1, unsigned int &IC1 )
{
	share::Guard g( _mutex ) ;
	{
		CMapCheckInfo::iterator it = _login_check_map.find(access_code);
		if ( it == _login_check_map.end() ) {
			return false ;
		}

		CheckInfo &info = it->second ;
		if ( info.encrypt == 0 ) {
			return false ;
		}

		// 如果三个值都为零则不需要处理加密
		if ( info.M1 == 0 && info.IA1 == 0 && info.IC1 == 0 )
			return false ;

		// 取得对应密钥
		M1  = info.M1 ;
		IA1 = info.IA1 ;
		IC1 = info.IC1 ;

		return true ;
	}
}

bool LoginCheck::Reload(int timeval)
{
	if(time(0) - _last_reload_time < timeval)
		return false;

	if (_config_file.empty())
	{
		return false;
	}

	char buf[1024] = {0};
	FILE *fp = NULL;
	fp = fopen(_config_file.c_str(), "r");
	if (fp == NULL)
	{
		string config_file_back = _config_file + ".bak";
		fp = fopen(config_file_back.c_str(),"r");
		if(fp == NULL)
			return false;
	}

	CMapCheckInfo check_map;
	while (fgets(buf, sizeof(buf), fp))
	{
		unsigned int i = 0;
		while (i < sizeof(buf))
		{
			if (!isspace(buf[i]))
				break;
			i++;
		}
		if (buf[i] == '#')
			continue;

		char temp[1024] = {0};
		for (int i = 0, j = 0; i < (int)strlen(buf); ++ i )
		{
			if (buf[i] != ' ' && buf[i] != '\r' && buf[i] != '\n')
			{
				temp[j++] = buf[i];
			}
		}

		string line = temp;
		vector<string> vec_param ;
		splitvector(line, vec_param, ":", 0 );
		if (vec_param.size() < 5)
			continue;
		string &scode = vec_param[0] ;
		if ( scode.empty() )
			continue ;
		int ncode = atoi( scode.c_str() ) ;
		if ( ncode <= 0 )
			continue ;

		CheckInfo check_info;
		check_info.access_code   = ncode ;
		check_info.user_id 		 = vec_param[1];
		check_info.user_password = vec_param[2];
		check_info.user_ip 		 = vec_param[3];
		check_info.valid_flag 	 = atoi(vec_param[4].c_str());

		// 是否要加密处理
		if ( vec_param.size() == 6 ) {
			// 解析密钥 M1_IA1_IC1
			vector<string> vec2 ;
			splitvector( vec_param[5], vec2, "_" , 0 ) ;
			if ( vec2.size() == 3 ) { // 主要针对同步程序的导致的数据错误
				if ( ! vec2[0].empty() && ! vec2[1].empty() && ! vec2[2].empty() ) {
					check_info.encrypt = 1 ;
					check_info.M1      = atoi( vec2[0].c_str() ) ;
					check_info.IA1     = atoi( vec2[1].c_str() ) ;
					check_info.IC1     = atoi( vec2[2].c_str() ) ;
				}
			}
		}

		check_map.insert(make_pair(check_info.access_code, check_info));
	}
	fclose(fp);
	fp = NULL;

	_mutex.lock();
	_login_check_map.clear();
	_login_check_map = check_map;
	_mutex.unlock();

	_last_reload_time = time(0);

	return true;

}
