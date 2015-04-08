using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using SYSModel;
using System.Data;

namespace yuTongWebService
{
    public class WebServHelper
    {
        /// <summary> 根据EnumWebServFunName枚举，调用WebService方法名称
        /// </summary>
        /// <param name="funcName">WebService方法名称</param>
        /// <param name="funcObject">WebService参数对象</param>
        /// <returns>返回约定内容</returns>
        public static SYSModel.RespFunStruct WebServHandler(SYSModel.EnumWebServFunName enumFunName, object funcObject)
        {
            SYSModel.RespFunStruct resp = new SYSModel.RespFunStruct();
            string msg = string.Empty;//错误消息
            switch (enumFunName)
            {
                #region 基础数据
                case SYSModel.EnumWebServFunName.UpLoadCcontact:
                    {
                        tb_contacts_ex model = Newtonsoft.Json.JsonConvert.DeserializeObject<tb_contacts_ex>(funcObject.ToString());
                        string result = WebServ_YT_BasicData.UpLoadCcontact(model);
                        resp = HandleErr(string.IsNullOrEmpty(result) ? "1" : "0", result);
                        break;
                    }
                case SYSModel.EnumWebServFunName.UpLoadCustomer:
                    {
                        tb_customer model = Newtonsoft.Json.JsonConvert.DeserializeObject<tb_customer>(funcObject.ToString());
                        string result = WebServ_YT_BasicData.UpLoadCustomer(model);
                        resp = HandleErr(string.IsNullOrEmpty(result) ? "1" : "0", result);
                        break;
                    }
                case SYSModel.EnumWebServFunName.UpLoadSercicePartStock:
                    {
                        Dictionary<string, int> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, int>>(funcObject.ToString());
                        bool flag = WebServ_YT_BasicData.UpLoadSercicePartStock(dic);
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.SearchContact:
                    {
                        DataTable dt = WebServ_YT_BasicData.SearchContact();
                        if (dt == null || dt.Rows.Count == 0)
                        {
                            resp.IsSuccess = "0";
                        }
                        else
                        {
                            resp.IsSuccess = "1";
                        }
                        resp.Msg = "";
                        resp.ReturnObject = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                        break;
                    }
                #endregion
                #region 业务数据
                case SYSModel.EnumWebServFunName.UpLoadPartSale:
                    {
                        bool flag = WebServ_YT_BusiData.UpLoadPartSale(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.UpLoadServiceOrder:
                    {
                        bool flag = WebServ_YT_BusiData.UpLoadServiceOrder(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.UpLoadRepairBill:
                    {
                        bool flag = WebServ_YT_BusiData.UpLoadRepairBill(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.UpLoadPartPurchase:
                    {
                        bool flag = WebServ_YT_BusiData.UpLoadPartPurchase(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.UpLoadPartPutStore:
                    {
                        bool flag = WebServ_YT_BusiData.UpLoadPartPutStore(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.LoadOrderStatus:
                    {
                        bool flag = WebServ_YT_BusiData.LoadOrderStatus(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.LoadPartInStore:
                    {
                        bool flag = WebServ_YT_BusiData.LoadPartInStore(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.LoadPartPurchaseStauts:
                    {
                        bool flag = WebServ_YT_BusiData.LoadPartPurchaseStauts(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.LoadStoreCenter:
                    {
                        bool flag = WebServ_YT_BusiData.LoadStoreCenter(funcObject.ToString());
                        resp = HandleErr(flag ?"1":"0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.QuerySettleAccounts:
                    {
                        bool flag = WebServ_YT_BusiData.QuerySettleAccounts(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.LoadServiceSettleStatus:
                    {
                        bool flag = WebServ_YT_BusiData.LoadServiceSettleStatus(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.UpPartReturnCreate:
                    {
                        bool flag = WebServ_YT_BusiData.UpPartReturnCreate(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.LoadPartRetureStatus:
                    {
                        bool flag = WebServ_YT_BusiData.LoadPartRetureStatus(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case SYSModel.EnumWebServFunName.UpPartRetureUpdate:
                    {
                        bool flag = WebServ_YT_BusiData.UpPartRetureUpdate(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                case EnumWebServFunName.SearchOrderStatus:
                    {
                        string status = WebServ_YT_BusiData.SearchOrderStatus(funcObject.ToString());
                        resp = HandleErr(string.IsNullOrEmpty(status) ? "0" : "1", status);
                        break;
                    }
                case EnumWebServFunName.UpLoadServiceSettleStatus:
                    {
                        bool flag = WebServ_YT_BusiData.UpLoadServiceSettleStatus(funcObject.ToString());
                        resp = HandleErr(flag ? "1" : "0", flag.ToString());
                        break;
                    }
                #endregion
                default:
                    {
                        resp.IsSuccess = "0";
                        resp.Msg = "找不到对应的Wcf方法";
                        break;
                    }
            }

            return resp;
        }

        /// <summary> 处理错误信息
        /// </summary>
        /// <param name="isSuccess">0:失败 1：成功</param>
        /// <param name="result">返回参数</param>
        /// <returns></returns>
        private static SYSModel.RespFunStruct HandleErr(string isSuccess, string result)
        {
            SYSModel.RespFunStruct resp = new SYSModel.RespFunStruct();
            resp.IsSuccess = isSuccess;
            resp.ReturnObject = result;
            resp.Msg = isSuccess == "1" ? "" : "调用WebService方法出现异常";
            return resp;
        }
    }
}
