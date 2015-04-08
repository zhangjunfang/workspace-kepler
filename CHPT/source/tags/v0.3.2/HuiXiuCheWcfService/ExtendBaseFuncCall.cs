using System.Collections.Generic;
using SYSModel;
using Newtonsoft.Json;
using System;
using System.Data;
using HXC_FuncUtility;
namespace HuiXiuCheWcfService
{
    public class ExtendBaseFuncCall
    {
        public static RespFunStruct ExtBaseFuncCall(ReqeFunStruct reqeObj)
        {
            RespFunStruct resp = new RespFunStruct();
            bool result = false;
            UserIDOP userID = reqeObj.userIDOP;     
            switch (reqeObj.FunName)
            {
                case ComFunCallEnum.LogFunctionCall:
                    UserFunctionOPLog add = JsonConvert.DeserializeObject<UserFunctionOPLog>(reqeObj.FunObject.ToString());
                    try
                    {
                        result = BLL.OPLog.Add(add,GlobalStaticObj_Server.DbPrefix+GlobalStaticObj_Server.CommAccCode);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "ComFunCallEnum.LogFunctionCall error";
                        ex.Data.Add("UserCustomDefinitionMsg", "准确点错误描述：ComFunCallEnum.LogFunctionCall;Add对象JSON字串描述：" + reqeObj.FunObject.ToString());
                        throw ex;
                    }
                    break;
            }
            return resp;
        }
    }
}