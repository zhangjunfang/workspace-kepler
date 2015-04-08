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
using Utility.Common;

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

        /// <summary> 获取账套在线用户数
        /// </summary>
        /// <param name="accCode">加密账套编码</param>
        /// <returns>在线用户数</returns>
        public int GetOnLineUserCount(string str)
        {
            string accCode = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(str);
            return LoginSessionInfo.Instance.GetClientAccUserCount(accCode);
        }

        /// <summary> 获取所有未删除的帐套列表
        /// </summary>
        /// <returns>返回resp对象</returns>
        public string GetAccList()
        {
            RespFunStruct resp = new RespFunStruct();
            DataTable dt = new DataTable();
            try
            {
                string where = string.Format("setbook_code!='000' and enable_flag='{0}' ", ((int)DataSources.EnumEnableFlag.USING));
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
                GlobalStaticObj_Server.WCFLogService.WriteLog("获取账套", ex);
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

                #region 账套信息
                if (string.IsNullOrEmpty(loginObj.acccode))
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "帐套不能为空";
                    return HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(resp));
                }
                #endregion

                SetDbName(loginObj.acccode, false);

                #region 软件注册信息
                DataTable dt = BLL.DBHelper.GetTable("获取注册信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_signing_info", "authentication_status,protocol_expires_time", "", "", "");
                if (dt.Rows.Count == 0)
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "软件未注册";
                    return HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(resp));
                }
                DataSources.EnumAuthenticationStatus enumAuthenticationStatus = (DataSources.EnumAuthenticationStatus)Convert.ToInt32(dt.Rows[0]["authentication_status"].ToString());
                if (enumAuthenticationStatus != DataSources.EnumAuthenticationStatus.AUTHORIZED)
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "软件" + DataSources.GetDescription(enumAuthenticationStatus, true);
                    return HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(resp));
                }

                DateTime applyTime = Common.UtcLongToLocalDateTime(Convert.ToInt64(dt.Rows[0]["protocol_expires_time"].ToString()));
                if (applyTime < GlobalStaticObj_Server.Instance.CurrentDateTime)
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "软件过期";
                    return HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(resp));
                }
                #endregion

                #region 验证用户
                string tempUserID = BLL.DBHelper.GetSingleValue("验证用户是否存在", GlobalStaticObj_Server.Instance.CurrAccDbName, "sys_user", "user_id", " land_name='" + loginObj.username + "'", "");
                if (string.IsNullOrEmpty(tempUserID))
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "登录用户不存在";
                    return HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(resp));
                }
                //if (LoginSessionInfo.Instance.dicLoginInfos.ContainsKey(tempUserID))
                //{
                //    resp.IsSuccess = "0";
                //    resp.Msg = "该用户已在其他电脑登录";
                //     return HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(resp));
                //}

                OperationContext context = OperationContext.Current;
                //获取传进的消息属性  
                MessageProperties properties = context.IncomingMessageProperties;
                //获取消息发送的远程终结点IP和端口  
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                string IPStr = endpoint.Address + ":" + endpoint.Port.ToString();

                DataSet ds = new DataSet();
                string errMsg = BLL.ClientUser.UserLogin(loginObj, IPStr, GlobalStaticObj_Server.Instance.CurrAccDbName, out ds);
                if (!string.IsNullOrEmpty(errMsg))
                {
                    resp.IsSuccess = "0";
                    //resp.Msg = "登录密码错误";
                    resp.Msg = errMsg;
                    return HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(resp));
                }
                if (ds == null || ds.Tables.Count == 0 || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0))
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "登录异常";
                    return HXCCommon.DotNetEncrypt.DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(resp));
                }
                #endregion

                string userID = ds.Tables[0].Rows[0][0].ToString();
                //将登陆id及cookie信息加入到缓存中
                string cookieStr = LoginSessionInfo.Instance.LoginIn(loginObj.acccode, userID); ;
                DataTable dtReturn = new DataTable("cookieStr");
                DataColumn dc = new DataColumn("cookieStr", typeof(string));
                dtReturn.Columns.Add(dc);
                DataRow dr = dtReturn.NewRow();
                dr["cookieStr"] = cookieStr;
                dtReturn.Rows.Add(dr);
                ds.Tables.Add(dtReturn);
                resp.IsSuccess = "1";
                resp.ReturnObject = JsonConvert.SerializeObject(ds);
            }
            catch (Exception ex)
            {
                resp.IsSuccess = "0";
                resp.Msg = "登陆失败:" + ex.Message;
                GlobalStaticObj_Server.WCFLogService.WriteLog("登陆验证", ex);
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
                    BLL.ClientUser.UserLoginOut(userID, reqeObj.AccCode);
                    //将登陆id及cookie信息从缓存中移除
                    LoginSessionInfo.Instance.LoginOut(reqeObj.AccCode, userID);
                    resp.IsSuccess = "1";
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccess = "0";
                resp.Msg = "登出失败:" + ex.Message;
                GlobalStaticObj_Server.WCFLogService.WriteLog("用户登出", ex);
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
            string opName = string.Empty;
            try
            {
                string objStr = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(strJson);
                ReqeFunStruct reqeObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ReqeFunStruct>(objStr);
                opName = reqeObj.userIDOP.OPName;
                if (string.IsNullOrEmpty(reqeObj.AccCode))
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "帐套不能为空";
                }
                else
                {
                    SetDbName(reqeObj.AccCode, false);
                    string errMsg = LoginSessionInfo.Instance.ValidaiteUser(reqeObj.AccCode, reqeObj.userIDOP.UserID, reqeObj.PCClientCookieStr);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = errMsg;
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
                resp.Msg = "【接口调用】-" + opName + ":" + ex.Message;
                GlobalStaticObj_Server.WCFLogService.WriteLog("数据操作-" + opName, ex);
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
            string opName = string.Empty;
            try
            {
                string objStr = HXCCommon.DotNetEncrypt.DESEncrypt.Decrypt(strJson);
                ReqeFunStruct_WebServ reqeObj = Newtonsoft.Json.JsonConvert.DeserializeObject<ReqeFunStruct_WebServ>(objStr);
                opName = reqeObj.userIDOP.OPName;
                if (string.IsNullOrEmpty(reqeObj.AccCode))
                {
                    resp.IsSuccess = "0";
                    resp.Msg = "帐套不能为空";
                }
                else
                {
                    SetDbName(reqeObj.AccCode, true);
                    string errMsg = LoginSessionInfo.Instance.ValidaiteUser(reqeObj.AccCode, reqeObj.userIDOP.UserID, reqeObj.PCClientCookieStr);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = errMsg;
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
                resp.Msg = "【接口调用】-" + opName + ":" + ex.Message;
                Utility.Log.Log.writeLineToLog("【接口调用】-" + opName + ":" + ex.Message, "wcf服务");
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
