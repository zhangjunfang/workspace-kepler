using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using SYSModel;
using HuiXiuCheWcfContract;
using System.IO;
using Utility.Log;
using Model;
using yuTongWebService;
using HXC_FuncUtility;
using System.ServiceModel.Channels;

namespace HuiXiuCheWcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class HXCWCFService : IHXCWCFService
    {
        /// <summary> 测试连接是否正常
        /// </summary>
        /// <returns></returns>
        public string TestConnect()
        {
            return "1";
        }

        /// <summary> 获取所使用中的有帐套列表
        /// </summary>
        /// <returns>返回resp对象</returns>
        public string GetAccList()
        {
            RespFunStruct resp = new RespFunStruct();
            DataTable dt = new DataTable();
            try
            {
                string where = string.Format("enable_flag='{0}' ", ((int)DataSources.EnumEnableFlag.USING));
                dt = BLL.DBHelper.GetTable("获取帐套列表", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "sys_setbook", "*", where, "", "order by setbook_code");
                resp.IsSuccess = "1";
                resp.RecordCount = dt.Rows.Count;
                resp.ReturnObject = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                resp.IsSuccess = "0";
                resp.RecordCount = dt.Rows.Count;
                resp.Msg = "获取账套信息失败:" + ex.Message;
                resp.ReturnObject = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                Utility.Log.Log.writeLineToLog("【获取账套】" + ex.Message, "wcf服务");
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(resp);
        }

        /// <summary> 登陆验证
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string LoginIn(string str)
        {
            RespFunStruct resp = new RespFunStruct();
            try
            {
                string loginStr = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(str);
                LoginInput loginObj = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginInput>(loginStr);
                if (string.IsNullOrEmpty(loginObj.acccode))
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "帐套不能为空";
                }
                else
                {
                    SetDbName(loginObj.acccode, false);

                    OperationContext context = OperationContext.Current;
                    //获取传进的消息属性  
                    MessageProperties properties = context.IncomingMessageProperties;
                    //获取消息发送的远程终结点IP和端口  
                    RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    string IPStr = endpoint.Address + ":" + endpoint.Port.ToString();
                    DataSet ds = BLL.ClientUser.UserLogin(loginObj, IPStr, GlobalStaticObj_Server.Instance.CurrAccDbName);
                    if (ds == null || ds.Tables.Count == 0 || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0))
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "登录失败，用户名、密码问题";
                    }
                    else
                    {
                        string userID = ds.Tables[0].Rows[0][0].ToString();
                        //将登陆id及cookie信息加入到缓存中
                        string cookieStr = LoginSessionInfo.Instance.LoginIn(userID); ;
                        DataTable dt = new DataTable("cookieStr");
                        DataColumn dc = new DataColumn("cookieStr", typeof(string));
                        dt.Columns.Add(dc);
                        DataRow dr = dt.NewRow();
                        dr["cookieStr"] = cookieStr;
                        dt.Rows.Add(dr);
                        ds.Tables.Add(dt);
                        resp.IsSuccess = "1";
                        resp.ReturnObject = JsonConvert.SerializeObject(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccess = "0";
                resp.Msg = "登陆失败:" + ex.Message;
                Utility.Log.Log.writeLineToLog("【登陆验证】" + ex.Message, "wcf服务");
            }
            return HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(resp));
        }

        /// <summary> 登出
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string LoginOut(string strJson)
        {
            RespFunStruct resp = new RespFunStruct();
            try
            {
                string objStr = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(strJson);
                ReqeFunStruct reqeObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ReqeFunStruct>(objStr);

                if (string.IsNullOrEmpty(reqeObj.AccCode))
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "帐套不能为空";
                }
                else
                {
                    SetDbName(reqeObj.AccCode, false);
                    string userID = reqeObj.userIDOP.UserID;
                    BLL.ClientUser.UserLoginOut(userID, GlobalStaticObj_Server.Instance.CurrAccDbName);
                    //将登陆id及cookie信息从缓存中移除
                    LoginSessionInfo.Instance.LoginOut(userID);
                    resp.IsSuccess = "1";
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccess = "0";
                resp.Msg = "登出失败:" + ex.Message;
                Utility.Log.Log.writeLineToLog("【用户登出】" + ex.Message, "wcf服务");
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(resp);
        }

        /// <summary> 数据操作
        /// </summary>
        /// <param name="strJson">json字串</param>
        /// <returns></returns>
        public string JsonOperate(string strJson)
        {
            RespFunStruct resp = new RespFunStruct();
            string returnStr = string.Empty;
            try
            {
                string objStr = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(strJson);
                ReqeFunStruct reqeObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ReqeFunStruct>(objStr);

                if (string.IsNullOrEmpty(reqeObj.AccCode))
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "帐套不能为空";
                }
                else
                {
                    SetDbName(reqeObj.AccCode, false);
                    if (!LoginSessionInfo.Instance.ValidaiteUser(reqeObj.userIDOP.UserID, reqeObj.PCClientCookieStr))
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "用户验证失败";
                    }
                    else
                    {
                        switch (reqeObj.SubSysName)
                        {
                            case SubSysName.CommonBaseFuncCall:
                                resp = CommonBaseFuncCall.ComFunCall(reqeObj);
                                break;
                            case SubSysName.ExtendBaseFuncCall:
                                resp = ExtendBaseFuncCall.ExtBaseFuncCall(reqeObj);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccess = "0";
                resp.Msg = "执行失败:"+ex.Message;
                Utility.Log.Log.writeLineToLog("【数据操作】" + ex.Message, "wcf服务");
            }
            returnStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(resp));
            return returnStr;
        }

        /// <summary> webservice方法调用
        /// </summary>
        /// <param name="strJson">json串</param>
        /// <returns>返回RespFunStruct</returns>
        public string HandleWebServ(string strJson)
        {
            RespFunStruct resp = new RespFunStruct();
            string returnStr = string.Empty;
            try
            {
                string objStr = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(strJson);
                ReqeFunStruct_WebServ reqeObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ReqeFunStruct_WebServ>(objStr);
                if (string.IsNullOrEmpty(reqeObj.AccCode))
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "帐套不能为空";
                }
                else
                {
                    SetDbName(reqeObj.AccCode, true);
                    if (!LoginSessionInfo.Instance.ValidaiteUser(reqeObj.userIDOP.UserID, reqeObj.PCClientCookieStr))
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "用户验证失败";
                    }
                    else
                    {
                        EnumWebServFunName enumFun = reqeObj.FunName;
                        resp = WebServHelper.WebServHandler(reqeObj.FunName, reqeObj.FunObject);

                    }
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccess = "0";
                resp.Msg = "接口调用:" + ex.Message;    //modify by kord
                Utility.Log.Log.writeLineToLog("【接口调用】" + ex.Message, "wcf服务");
            }
            returnStr = HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(resp));
            return returnStr;
        }

        /// <summary> 设定当前帐套数据库
        /// </summary>
        /// <param name="accCode">帐套代码</param>
        public void SetDbName(string accCode, bool isWebServ)
        {
            if (isWebServ)
            {
                GlobalStaticObj_Server.Instance.CurrAccDbName = GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.Instance.MainAccCode;//宇通接口单独调用主库
            }
            else
            {
                GlobalStaticObj_Server.Instance.CurrAccDbName = GlobalStaticObj_Server.DbPrefix + accCode;
            }
        }
    }
}
