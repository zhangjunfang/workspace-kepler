using System;
using System.Collections.Generic;
using System.Data;
namespace SYSModel
{
   public class SQLObj
    {
       
       /// <summary>
       /// CommandType Command类型
       /// </summary>
        public CommandType cmdType{ get; set; }
       /// <summary>
       /// SQL语句内容或者名称
       /// </summary>
        public String sqlString { get; set; }        
       /// <summary>
       /// 参数内容字典
       /// </summary>
        public Dictionary<string, ParamObj> Param { get; set; }
    }

   public class SysSQLString
   {

       /// <summary>
       /// CommandType Command类型
       /// </summary>
       public CommandType cmdType { get; set; }
       /// <summary>
       /// SQL语句内容或者名称
       /// </summary>
       public String sqlString { get; set; }
       /// <summary>
       /// 参数内容字典
       /// </summary>
       public Dictionary<string, string> Param { get; set; }
   }
}
