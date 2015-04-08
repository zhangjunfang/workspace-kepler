using System;
using System.Data;
using System.Collections.Generic;
using SYSModel;
using DALFactory;
using IDAL;
using HXC_FuncUtility;
namespace BLL
{
    public class ClientUser
    {
        public static DataSet UserLogin(LoginInput loginO, string IPStr, string currAccDbName)
        {
            Dictionary<string, ParamObj> dic = new Dictionary<string, ParamObj>();
            ParamObj paraO1 = new ParamObj();
            paraO1.name = "land_name";
            paraO1.size = 40;
            paraO1.type = SysDbType.VarChar;
            paraO1.value = loginO.username;
            ParamObj paraO2 = new ParamObj();
            paraO2.name = "password";
            paraO2.size = 40;
            paraO2.type = SysDbType.VarChar;
            paraO2.value = loginO.pwd;
            ParamObj paraO3 = new ParamObj();
            paraO3.name = "login_time";
            paraO3.type = SysDbType.BigInt;
            paraO3.value = System.DateTime.Now.ToUniversalTime().Ticks;
            ParamObj paraO4 = new ParamObj();
            paraO4.name = "computer_ip";
            paraO4.size = 40;
            paraO4.type = SysDbType.VarChar;
            paraO4.value = IPStr;
            ParamObj paraO5 = new ParamObj();
            paraO5.name = "computer_name";
            paraO5.size = 40;
            paraO5.type = SysDbType.NVarChar;
            paraO5.value = loginO.ComputerName;
            ParamObj paraO6 = new ParamObj();
            paraO6.name = "computer_mac";
            paraO6.size = 40;
            paraO6.type = SysDbType.VarChar;
            paraO6.value = loginO.MAC;
            ParamObj paraO7 = new ParamObj();
            paraO7.name = "login_Id";
            paraO7.size = 40;
            paraO7.type = SysDbType.VarChar;
            paraO7.value = loginO.Login_Id;
            dic.Add("land_name", paraO1);
            dic.Add("password", paraO2);
            dic.Add("login_time", paraO3);
            dic.Add("computer_ip", paraO4);
            dic.Add("computer_name", paraO5);
            dic.Add("computer_mac", paraO6);
            dic.Add("login_Id", paraO7);
            SQLObj sqlObj = new SQLObj();
            sqlObj.cmdType = System.Data.CommandType.StoredProcedure;
            sqlObj.sqlString = "ClientUserLogin";
            sqlObj.Param = dic;
            return DBHelper.GetDataSet("客户端用户登录", currAccDbName, sqlObj);
        }

        public static void UserLoginOut(string UserID, string currAccCode)
        {
            DateTime dt = DBHelper.GetCurrentTime(GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode);
            long exitTime = Utility.Common.Common.LocalDateTimeToUtcLong(dt);
            SysSQLString sysSqlString;
            List<SysSQLString> list = new List<SysSQLString>();
            sysSqlString = new SysSQLString();
            sysSqlString.cmdType = CommandType.Text;
            sysSqlString.sqlString = "UPDATE [sys_user] set [is_online] = '0' where [user_id]  = '" + UserID + "'";
            list.Add(sysSqlString);
            sysSqlString = new SysSQLString();
            sysSqlString.cmdType = CommandType.Text;
            sysSqlString.sqlString = "UPDATE [sys_log_log] set exit_time=" + exitTime.ToString() + " where exit_time is null and  [user_id] = '" + UserID + "'";
            list.Add(sysSqlString);
            DBHelper.BatchExeSQLStrMultiByTransNoLogNoBackup("系统登出", currAccCode, list);
        }
    }
}
