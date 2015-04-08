using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using Model;
using Utility.Common;
using System.Reflection;
using BLL;
using HXC_FuncUtility;
using SYSModel;
using Utility.Net;
using System.Collections;
using Utility.Security;
using Utility.CommonForm;


namespace HXCServerWinForm.UCForm
{
    public partial class frmSoftReg : FormEx
    {
        /// <summary>当前步骤
        /// </summary>
        private int currStep = 0;
        private bool isNeedReg = false;
        public frmSoftReg()
        {
            InitializeComponent();
        }

        private void frmSoftReg_Load(object sender, EventArgs e)
        {
            DataSources.BindComBoxDataEnum(cmbrepair_qualification, typeof(DataSources.EunumRepairQualification), false, true);
            DataSources.BindComBoxDataEnum(cmbunit_properties, typeof(DataSources.EunumUnitNature), false, true);
            CommonClass.CommonFuncCall.BindProviceComBox(ddlprovince, "请选择");
            BindData();
            if (!isNeedReg)
            {
                txtservice_station_sap.ReadOnly = true;
                txtaccess_code.ReadOnly = true;
                txtgrant_authorization.ReadOnly = true;
                txtauthentication.ReadOnly = true;
                txts_user.ReadOnly = true;
                txts_pwd.ReadOnly = true;
                txtsign_id.ReadOnly = true;
            }
            ToStep(true);
        }

        #region 事件
        /// <summary> 保存
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //bool flag = HandleStep();
                bool flag = SaveCorpInfo();
                if (flag)
                {
                    MessageBoxEx.ShowInformation("操作成功");
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("注册鉴权", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 上一步
        /// </summary>
        private void btnPrevStep_Click(object sender, EventArgs e)
        {
            try
            {
                ToStep(false);
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("注册鉴权", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 下一步
        /// </summary>
        private void btnNextStep_Click(object sender, EventArgs e)
        {
            try
            {
                errProvider.Clear();
                bool flag = false;
                if (currStep == 1)
                {
                    #region 信息验证
                    if (string.IsNullOrEmpty(txtcom_name.Caption))
                    {
                        Validator.SetError(errProvider, txtcom_name, "请录入公司名称");
                        return;
                    }
                    if (string.IsNullOrEmpty(txtcom_address.Caption))
                    {
                        Validator.SetError(errProvider, txtcom_address, "请录入联系地址");
                        return;
                    }
                    if (!string.IsNullOrEmpty(txtzip_code.Caption))
                    {
                        if (Common.ValidateStr(txtzip_code.Caption.Trim(), @"[1-9]\d{5}(?!\d)") != 1)
                        {
                            Validator.SetError(errProvider, txtzip_code, "请录入正确的邮政编码");
                            return;
                        }
                    }
                    if (string.IsNullOrEmpty(txtcom_contact.Caption))
                    {
                        Validator.SetError(errProvider, txtcom_contact, "请录入联系人");
                        return;
                    }
                    if (string.IsNullOrEmpty(txtcom_tel.Caption))
                    {
                        Validator.SetError(errProvider, txtcom_tel, "请录入联系人电话号码或手机号");
                        return;
                    }
                    else
                    {
                        if (Common.ValidateStr(txtcom_tel.Caption.Trim(), @"[\d-]+") != 1)
                        {
                            Validator.SetError(errProvider, txtcom_tel, "请录入正确的电话号码或手机号");
                            return;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtcom_email.Caption))
                    {
                        if (Common.ValidateStr(txtcom_email.Caption.Trim(), @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*") != 1)
                        {
                            Validator.SetError(errProvider, txtcom_email, "请录入正确的电子邮箱");
                            return;
                        }
                    }
                    if (!string.IsNullOrEmpty(txthotline.Caption))
                    {
                        if (Common.ValidateStr(txthotline.Caption.Trim(), @"[\d-]+") != 1)
                        {
                            Validator.SetError(errProvider, txthotline, "请录入正确的电话号码或手机号");
                            return;
                        }
                    }
                    #endregion

                    #region 宇通验证
                    if (string.IsNullOrEmpty(txtservice_station_sap.Caption))
                    {
                        if (MessageBoxEx.ShowQuestion("如果本公司与宇通签约，请填写宇通SAP代码和接入码，该信息填写后，才能与宇通关联，\r\n注意：如本次不填写，将无法成为签约服务站，后续也不能补充修改，是否返回填写？"))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtaccess_code.Caption.Trim()))
                        {
                            Validator.SetError(errProvider, txtaccess_code, "请录入宇通接入码");
                            return;
                        }
                    }
                    #endregion

                    #region 执行第一步
                    ProcessOperator process = new ProcessOperator();
                    #region 匿名方法，后台线程执行调用
                    process.BackgroundWork = delegate()
                    {
                        flag = FirstStep();
                    };
                    #endregion
                    process.MessageInfo = "请稍候...";
                    #region 匿名方法，后台线程执行完调用
                    process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(
                            delegate(object osender, BackgroundWorkerEventArgs be)
                            {
                                if (be.BackGroundException != null)
                                {
                                    flag = false;
                                    GlobalStaticObj_Server.GlobalLogService.WriteLog("账套设置", be.BackGroundException);
                                    MessageBoxEx.ShowWarning("操作失败！");
                                }
                            }
                        );
                    #endregion
                    process.Start();
                    #endregion
                }
                else if (currStep == 2)
                {
                    flag = SecondStep();
                }
                if (flag)
                {
                    ToStep(true);
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("注册鉴权", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 完成注册
        /// </summary>
        private void btnReg_Click(object sender, EventArgs e)
        {
            try
            {
                errProvider.Clear();
                if (string.IsNullOrEmpty(txtauthentication.Caption))
                {
                    Validator.SetError(errProvider, txtauthentication, "请录入通讯鉴权码");
                    return;
                }
                if (string.IsNullOrEmpty(txts_user.Caption))
                {
                    Validator.SetError(errProvider, txts_user, "请录入通讯账号");
                    return;
                }
                if (string.IsNullOrEmpty(txts_pwd.Caption))
                {
                    Validator.SetError(errProvider, txts_pwd, "请录入通讯密码");
                    return;
                }
                ProcessOperator process = new ProcessOperator();
                #region 匿名方法，后台线程执行调用
                process.BackgroundWork = delegate()
                {
                    FinishReg();
                };
                #endregion
                process.MessageInfo = "正在注册...";
                #region 匿名方法，后台线程执行完调用
                process.BackgroundWorkerCompleted += new EventHandler<BackgroundWorkerEventArgs>(
                        delegate(object osender, BackgroundWorkerEventArgs be)
                        {
                            if (be.BackGroundException != null)
                            {
                                GlobalStaticObj_Server.GlobalLogService.WriteLog("账套设置", be.BackGroundException);
                                MessageBoxEx.ShowWarning("注册失败！");
                            }
                            else
                            {
                                this.DialogResult = DialogResult.OK;
                                MessageBoxEx.ShowInformation("注册成功");
                            }
                        }
                    );
                #endregion
                process.Start();
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("注册鉴权", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 关闭事件
        /// </summary>
        private void frmSoftReg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                return;
            }
            string errMsg = "";
            if (isNeedReg)
            {
                errMsg = "该操作将会放弃注册，退出系统，是否继续？";
            }
            else if (currStep == 1)
            {
                errMsg = "该操作将会放弃注册信息，是否继续？";
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                if (!MessageBoxEx.ShowQuestion(errMsg))
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        /// <summary> 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary> 选择省份
        /// </summary>
        private void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlprovince.SelectedValue.ToString()))
                {
                    CommonClass.CommonFuncCall.BindCityComBox(ddlcity, ddlprovince.SelectedValue.ToString(), "请选择");
                    CommonClass.CommonFuncCall.BindCountryComBox(ddlcounty, ddlcity.SelectedValue.ToString(), "请选择");
                }
                else
                {
                    CommonClass.CommonFuncCall.BindCityComBox(ddlcity, "", "请选择");
                    CommonClass.CommonFuncCall.BindCountryComBox(ddlcounty, "", "请选择");
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("注册鉴权", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }

        /// <summary> 选择城市
        /// </summary>
        private void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlcity.SelectedValue.ToString()))
                {
                    CommonClass.CommonFuncCall.BindCountryComBox(ddlcounty, ddlcity.SelectedValue.ToString(), "请选择");
                }
                else
                {
                    CommonClass.CommonFuncCall.BindCountryComBox(ddlcounty, "", "请选择");
                }
            }
            catch (Exception ex)
            {
                GlobalStaticObj_Server.GlobalLogService.WriteLog("注册鉴权", ex);
                MessageBoxEx.ShowWarning("程序异常");
            }
        }
        #endregion

        #region 方法
        /// <summary> 绑定数据
        /// </summary>
        private void BindData()
        {
            DataTable dt = DBHelper.GetTable("获取公司信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_signing_info", "*", "", "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                txtmachine_code_sequence.Caption = Utility.Tools.HardwareSerialNumber.GetMachineCode();
                isNeedReg = true;
                return;
            }
            DataRow dr = dt.Rows[0];
            txtsign_id.Caption = CommonCtrl.IsNullToString(dr["sign_id"]);
            txtcom_name.Caption = CommonCtrl.IsNullToString(dr["com_name"]);
            txtcom_name2.Caption = txtcom_name.Caption;
            ddlprovince.SelectedValue = CommonCtrl.IsNullToString(dr["province"]);
            ddlcity.SelectedValue = CommonCtrl.IsNullToString(dr["city"]);
            ddlcounty.SelectedValue = CommonCtrl.IsNullToString(dr["county"]);
            txtcom_address.Caption = CommonCtrl.IsNullToString(dr["contact_address"]);
            txtzip_code.Caption = CommonCtrl.IsNullToString(dr["zip_code"]);
            txtcom_contact.Caption = CommonCtrl.IsNullToString(dr["contact"]);
            txtcom_tel.Caption = CommonCtrl.IsNullToString(dr["contact_tel"]);
            txtlegal_person.Caption = CommonCtrl.IsNullToString(dr["legal_person"]);
            cmbrepair_qualification.SelectedValue = CommonCtrl.IsNullToString(dr["repair_qualification"]);
            cmbunit_properties.SelectedValue = CommonCtrl.IsNullToString(dr["nature_unit"]);
            txtcom_email.Caption = CommonCtrl.IsNullToString(dr["email"]);
            txthotline.Caption = CommonCtrl.IsNullToString(dr["hotline"]);
            txtcom_fax.Caption = CommonCtrl.IsNullToString(dr["fax"]);
            txtservice_station_sap.Caption = CommonCtrl.IsNullToString(dr["service_station_sap"]);
            txtaccess_code.Caption = CommonCtrl.IsNullToString(dr["access_code"]);
            txtmachine_code_sequence.Caption = CommonCtrl.IsNullToString(dr["machine_code_sequence"]);
            txtgrant_authorization.Caption = CommonCtrl.IsNullToString(dr["grant_authorization"]);
            txtauthentication.Caption = CommonCtrl.IsNullToString(dr["authentication"]);
            txts_user.Caption = CommonCtrl.IsNullToString(dr["s_user"]);
            txts_pwd.Caption = CommonCtrl.IsNullToString(dr["s_pwd"]);
            isNeedReg = CommonCtrl.IsNullToString(dr["authentication_status"]) != DataSources.EnumAuthenticationStatus.AUTHORIZED.ToString("d");
            //string dtStr = dr["protocol_expires_time"].ToString();
            //if (!string.IsNullOrEmpty(dtStr))
            //{
            //    long dateInt = Convert.ToInt64(dtStr);
            //    DateTime date = Common.UtcLongToLocalDateTime(dateInt);
            //    if (GlobalStaticObj_Server.Instance.CurrentDateTime > date)
            //    {
            //        isNeedReg = true;
            //    }
            //}
        }

        /// <summary> 获取企业信息并转换成json返回
        /// </summary>
        /// <returns>返回json</returns>
        private string GetModelFromFrm()
        {
            tb_signing_info model = new tb_signing_info();
            model.comName = txtcom_name.Caption.Trim();
            model.province = ddlprovince.SelectedValue.ToString();
            model.city = ddlcounty.SelectedValue.ToString();
            model.county = ddlcounty.SelectedValue.ToString();
            model.comAddress = txtcom_address.Caption.Trim();
            model.zipCode = txtzip_code.Caption.Trim();
            model.comContact = txtcom_contact.Caption.Trim();
            model.comTel = txtcom_tel.Caption.Trim();
            model.legalPerson = txtlegal_person.Caption.Trim();
            model.repairQualification = cmbrepair_qualification.SelectedValue.ToString();
            model.unitProperties = cmbunit_properties.SelectedValue.ToString();
            model.comEmail = txtcom_email.Caption.Trim();
            model.hotLtel = txthotline.Caption.Trim();
            model.comFax = txtcom_fax.Caption.Trim();
            model.serviceStationSap = txtservice_station_sap.Caption.Trim();
            model.accessCode = txtaccess_code.Caption.Trim();
            model.machineSerial = txtmachine_code_sequence.Caption.Trim();
            string version = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            model.serviceVersion = version.Substring(0, version.Length - 2);
            model.macAddress = GetIP.GetMacAddress();
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return jsonStr;
        }

        /// <summary> 跳转到步骤
        /// </summary>
        /// <param name="toDirection">正向反向</param>
        public void ToStep(bool toDirection)
        {
            if (toDirection)
            {
                currStep += 1;
            }
            else
            {
                currStep -= 1;
            }

            this.Text = string.Format((isNeedReg ? "软件注册" : "编辑注册信息") + "引导（{0}/3）", currStep);
            this.Invalidate(TextRect);
            btnPrevStep.Visible = currStep != 1;
            btnNextStep.Visible = currStep != 3;
            if (currStep == 1)
            {
                btnSave.Visible = !isNeedReg;
                btnCancel.Caption = "取消";
                btnReg.Visible = false;
            }
            if (currStep == 2)
            {
                btnSave.Visible = false;
                btnCancel.Caption = isNeedReg ? "取消" : "关闭";
                btnReg.Visible = false;
            }
            else if (currStep == 3)
            {
                btnSave.Visible = false;
                btnCancel.Caption = isNeedReg ? "取消" : "关闭";
                btnReg.Visible = isNeedReg;

            }

            #region 跳转
            if (currStep == 1)
            {
                pnlStep1.BringToFront();
            }
            if (currStep == 2)
            {
                pnlStep2.BringToFront();
            }
            if (currStep == 3)
            {
                pnlStep3.BringToFront();
            }
            #endregion
        }

        /// <summary> 每步处理
        /// </summary>
        public bool HandleStep()
        {

            return true;
        }

        /// <summary> 第一步
        /// </summary>
        /// <returns></returns>
        public bool FirstStep()
        {
            #region 软件注册
            if (isNeedReg)
            {
                #region 如果sap代码可以编辑且填写sap代码，就验证是否能够同步服务站信息
                if (!string.IsNullOrEmpty(txtservice_station_sap.Caption))
                {
                    string errMsg = yuTongWebService.WebServ_YT_BasicData.LoadServiceStation(txtaccess_code.Caption.Trim(), txtservice_station_sap.Caption.Trim(), false);
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBoxEx.ShowWarning("宇通sap代码及接入码验证失败," + errMsg);
                        }));
                        return false;
                    }
                }
                #endregion

                #region 在线注册
                Hashtable ht = new Hashtable();
                string jsonStr = GetModelFromFrm();
                ht.Add("jsonStr", Secret.StringToBase64String(jsonStr));
                //GlobalStaticObj_Server.Instance.SoftRegUrl = "http://192.168.35.111:8080/sspapp/operation/auth/";
                string resultStr = HttpRequest.DoGet(GlobalStaticObj_Server.Instance.SoftRegUrl + "remoteRegisterAuth.do", ht);
                if (!string.IsNullOrEmpty(resultStr))
                {
                    jsonStr = Secret.Base64StringToString(resultStr);
                    ResultInfo resultInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultInfo>(jsonStr);
                    if (resultInfo.isSuccess == 1)
                    {
                        txtsign_id.Caption = resultInfo.sign_id;
                        txtsign_id.ReadOnly = true;
                        if (!string.IsNullOrEmpty(resultInfo.grantAuthorization))
                        {
                            txtgrant_authorization.Caption = resultInfo.grantAuthorization;
                            txtgrant_authorization.ReadOnly = true;
                        }
                        else
                        {
                            txtgrant_authorization.ReadOnly = false;
                        }
                        txtgrant_authorization.Tag = resultInfo.validate;
                        txtauthentication.Caption = resultInfo.authentication;
                        txtauthentication.ReadOnly = true;
                        txts_user.Caption = resultInfo.sUser;
                        txts_user.ReadOnly = true;
                        txts_pwd.Caption = resultInfo.sPwd;
                        txts_pwd.ReadOnly = true;
                    }
                    else
                    {
                        txtsign_id.ReadOnly = false;
                    }
                }
                #endregion

                txtcom_name2.Caption = txtcom_name.Caption;
            }


            bool flag = SaveCorpInfo();
            if (!flag)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBoxEx.ShowWarning("保存失败");
                }));
                return false;
            }
            #endregion
            return true;
        }

        /// <summary> 第二步
        /// </summary>
        /// <returns></returns>
        public bool SecondStep()
        {
            #region 软件授权  注册需要验证授权信息,非注册不处理
            if (isNeedReg)
            {
                if (string.IsNullOrEmpty(txtsign_id.Caption))
                {
                    Validator.SetError(errProvider, txtsign_id, "请录入服务站ID");
                    return false;
                }
                if (string.IsNullOrEmpty(txtgrant_authorization.Caption))
                {
                    Validator.SetError(errProvider, txtgrant_authorization, "请录入授权码");
                    return false;
                }
                if (!ValidateGrantAuthorizationInfo())
                {
                    Validator.SetError(errProvider, txtgrant_authorization, "授权验证失败,请录入正确的授权码");
                    txtgrant_authorization.Focus();
                    return false;
                }
                if (txtgrant_authorization.Tag == null)
                {
                    //如是离线注册默认一年期限
                    txtgrant_authorization.Tag = Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime.AddYears(1));
                }
            }
            #endregion
            return true;
        }

        /// <summary> 验证授权信息
        /// </summary>
        /// <returns></returns>
        private bool ValidateGrantAuthorizationInfo()
        {
            string secretStr = Secret.MD5(txtmachine_code_sequence.Caption.Trim() + GlobalStaticObj_Server.RegSecret).ToUpper();
            if (txtgrant_authorization.Caption.Trim().ToUpper() != secretStr)
            {
                return false;
            }
            return true;
        }

        /// <summary> 通讯鉴权验证
        /// </summary>
        /// <returns></returns>
        private string ValidateSocket()
        {
            string errMsg = CloudPlatSocket.AutoTask.LoginTest(txtsign_id.Caption.Trim(), txts_user.Caption.Trim(), txts_pwd.Caption.Trim(), txtauthentication.Caption.Trim());
            return errMsg;
        }

        /// <summary> 完成注册
        /// </summary>
        public void FinishReg()
        {
            #region 在线注册
            #region 通讯鉴权  注册需要验证鉴权信息，非注册不处理
            string errMsg = ValidateSocket();
            if (!string.IsNullOrEmpty(errMsg))
            {
                this.Invoke(new Action(() =>
               {
                   Validator.SetError(errProvider, txts_pwd, "鉴权失败:" + errMsg);
               }));
                return;
            }
            #endregion

            #region 提交支撑注册结果
            Hashtable ht = new Hashtable();
            string jsonStr = GetModelFromFrm();
            ht.Add("machineCodeSequence", Secret.StringToBase64String(txtmachine_code_sequence.Caption.Trim()));
            string resultStr = HttpRequest.DoGet(GlobalStaticObj_Server.Instance.SoftRegUrl + "remoteSendAuthResult.do", ht);
            if (!string.IsNullOrEmpty(resultStr))
            {
                jsonStr = Secret.Base64StringToString(resultStr);
                ResultInfoBase resultInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultInfoBase>(jsonStr);
                if (resultInfo.isSuccess != 1)
                {
                    this.Invoke(new Action(() =>
                    {
                        MessageBoxEx.ShowWarning("软件注册失败");
                    }));
                    return;
                }
            }
            #endregion
            #endregion

            bool flag = SaveCorpInfo();
            if (!flag)
            {
                throw new Exception("注册失败");
            }
        }

        /// <summary> 保存注册公司信息
        /// </summary>
        /// <returns>是否成功</returns>
        public bool SaveCorpInfo()
        {
            Dictionary<string, string> dicFields = GetCorpParas();
            string pkName = "";
            string pkValue = "";
            string sign_id = DBHelper.GetSingleValue("查询注册信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_signing_info", "sign_id", "", "");
            if (!string.IsNullOrEmpty(sign_id))
            {
                pkName = "sign_id";
                pkValue = sign_id;
            }
            bool flag = DBHelper.Submit_AddOrEdit("保存帐套信息", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, "tb_signing_info", pkName, pkValue, dicFields);
            return flag;
        }

        /// <summary> 获取注册公司参数集合
        /// </summary>
        public Dictionary<string, string> GetCorpParas()
        {
            Dictionary<string, string> dicFields = new Dictionary<string, string>();
            dicFields.Add("com_name", txtcom_name.Caption);//服务站全称
            dicFields.Add("province", ddlprovince.SelectedValue.ToString());//省份
            dicFields.Add("city", ddlcity.SelectedValue.ToString());//城市
            dicFields.Add("county", ddlcounty.SelectedValue.ToString());//县市区
            dicFields.Add("zip_code", txtzip_code.Caption);//邮编
            dicFields.Add("email", txtcom_email.Caption);//email           
            dicFields.Add("contact_address", txtcom_address.Caption);//联系地址
            dicFields.Add("contact", txtcom_contact.Caption);//联系人
            dicFields.Add("contact_tel", txtcom_tel.Caption);//联系电话
            dicFields.Add("legal_person", txtlegal_person.Caption);//法人
            dicFields.Add("repair_qualification", cmbrepair_qualification.SelectedValue.ToString());//维修资质
            dicFields.Add("nature_unit", cmbunit_properties.SelectedValue.ToString());//单位性质
            dicFields.Add("hotline", txthotline.Caption);//热线电话
            dicFields.Add("fax", txtcom_fax.Caption);//传真 
            dicFields.Add("service_station_sap", txtservice_station_sap.Caption.Trim());
            dicFields.Add("access_code", txtaccess_code.Caption.Trim());
            if (isNeedReg)
            {
                if (txtgrant_authorization.Tag != null)
                {
                    dicFields.Add("protocol_expires_time", txtgrant_authorization.Tag.ToString());
                }
                dicFields.Add("machine_code_sequence", txtmachine_code_sequence.Caption.Trim());//机器码
                dicFields.Add("grant_authorization", txtgrant_authorization.Caption.Trim());//授权码
                if (currStep == 3)
                {
                    dicFields.Add("authentication_status", ((int)DataSources.EnumYesNo.Yes).ToString());//授权状态
                }
                else
                {
                    dicFields.Add("authentication_status", ((int)DataSources.EnumYesNo.NO).ToString());//授权状态
                }
                dicFields.Add("authentication", txtauthentication.Caption.Trim());//鉴权码
                dicFields.Add("s_user", txts_user.Caption.Trim());
                dicFields.Add("s_pwd", txts_pwd.Caption.Trim());
                dicFields.Add("sign_id", txtsign_id.Caption.Trim());
                dicFields.Add("apply_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
                dicFields.Add("create_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            else
            {
                dicFields.Add("update_time", Common.LocalDateTimeToUtcLong(GlobalStaticObj_Server.Instance.CurrentDateTime).ToString());
            }
            return dicFields;
        }


        /// <summary> 同步宇通服务站信息
        /// </summary>
        /// <returns>是否成功</returns>
        public void SyncServStationInfo()
        {
            yuTongWebService.WebServ_YT_BasicData.LoadServiceStation(txtservice_station_sap.Caption.Trim(), txtaccess_code.Caption.Trim(), true);
        }
        #endregion
    }
}

#region 用户注册信息
/// <summary> 用户注册信息
/// </summary>
public class tb_signing_info
{
    /// <summary>
    /// 公司名称
    /// </summary>		
    private string _com_name;
    public string comName
    {
        get { return _com_name; }
        set { _com_name = value; }
    }

    /// <summary> 软件版本
    /// </summary>
    private string _serviceVersion;

    public string serviceVersion
    {
        get { return _serviceVersion; }
        set { _serviceVersion = value; }
    }

    /// <summary> mac地址
    /// </summary>
    private string _macAddress;

    public string macAddress
    {
        get { return _macAddress; }
        set { _macAddress = value; }
    }


    /// <summary>
    /// 申请时间
    /// </summary>		
    private long _registTime;
    public long registTime
    {
        get { return _registTime; }
        set { _registTime = value; }
    }
    /// <summary>
    /// 法人
    /// </summary>		
    private string _legalPerson;
    public string legalPerson
    {
        get { return _legalPerson; }
        set { _legalPerson = value; }
    }
    /// <summary>
    /// 省
    /// </summary>		
    private string _province;
    public string province
    {
        get { return _province; }
        set { _province = value; }
    }
    /// <summary>
    /// 市
    /// </summary>		
    private string _city;
    public string city
    {
        get { return _city; }
        set { _city = value; }
    }
    /// <summary>
    /// 区
    /// </summary>		
    private string _county;
    public string county
    {
        get { return _county; }
        set { _county = value; }
    }
    /// <summary>
    /// 联系地址
    /// </summary>		
    private string _comAddress;
    public string comAddress
    {
        get { return _comAddress; }
        set { _comAddress = value; }
    }
    /// <summary>
    /// 邮编
    /// </summary>		
    private string _zip_code;
    public string zipCode
    {
        get { return _zip_code; }
        set { _zip_code = value; }
    }
    /// <summary>
    /// 联系人
    /// </summary>		
    private string _comContact;
    public string comContact
    {
        get { return _comContact; }
        set { _comContact = value; }
    }
    /// <summary>
    /// 热线电话
    /// </summary>		
    private string _hotLtel;
    public string hotLtel
    {
        get { return _hotLtel; }
        set { _hotLtel = value; }
    }
    /// <summary>
    /// 联系电话
    /// </summary>		
    private string _comTel;
    public string comTel
    {
        get { return _comTel; }
        set { _comTel = value; }
    }
    /// <summary>
    /// 维修资质
    /// </summary>		
    private string _repairQualification;
    public string repairQualification
    {
        get { return _repairQualification; }
        set { _repairQualification = value; }
    }
    /// <summary>
    /// 单位性质
    /// </summary>		
    private string _unitProperties;
    public string unitProperties
    {
        get { return _unitProperties; }
        set { _unitProperties = value; }
    }
    /// <summary>
    /// 电子邮件
    /// </summary>		
    private string _comEmail;
    public string comEmail
    {
        get { return _comEmail; }
        set { _comEmail = value; }
    }
    /// <summary>
    /// 传真
    /// </summary>		
    private string _comFax;
    public string comFax
    {
        get { return _comFax; }
        set { _comFax = value; }
    }

    /// <summary>
    /// 宇通sap
    /// </summary>		
    private string _service_station_sap;
    public string serviceStationSap
    {
        get { return _service_station_sap; }
        set { _service_station_sap = value; }
    }

    /// <summary>
    /// 宇通接入码
    /// </summary>		
    private string _access_code;
    public string accessCode
    {
        get { return _access_code; }
        set { _access_code = value; }
    }
    /// <summary>
    /// 机器序列码
    /// </summary>		
    private string _machineSerial;
    public string machineSerial
    {
        get { return _machineSerial; }
        set { _machineSerial = value; }
    }
}


#region 返回结果
public class ResultInfoBase
{
    /// <summary>0或1(是否成功)
    /// </summary>
    private int _isSuccess;

    public int isSuccess
    {
        get { return _isSuccess; }
        set { _isSuccess = value; }
    }

    /// <summary>错误原因
    /// </summary>
    private string _errMsg;
    public string errMsg
    {
        get { return _errMsg; }
        set { _errMsg = value; }
    }

}

public class ResultInfo : ResultInfoBase
{
    private string _sign_id;
    /// <summary> 服务站id（支撑下发）
    /// </summary>
    public string sign_id
    {
        get { return _sign_id; }
        set { _sign_id = value; }
    }

    /// <summary>软件有效期
    /// </summary>
    public string _validate;
    public string validate
    {
        get { return _validate; }
        set { _validate = value; }
    }

    /// <summary>授权码
    /// </summary>
    public string _grantAuthorization;
    public string grantAuthorization
    {
        get { return _grantAuthorization; }
        set { _grantAuthorization = value; }
    }

    /// <summary>鉴权码
    /// </summary>
    public string _authentication;
    public string authentication
    {
        get { return _authentication; }
        set { _authentication = value; }
    }

    /// <summary>Socket用户名
    /// </summary>
    public string _sUser;
    public string sUser
    {
        get { return _sUser; }
        set { _sUser = value; }
    }

    /// <summary>Socket密码
    /// </summary>
    public string _sPwd;
    public string sPwd
    {
        get { return _sPwd; }
        set { _sPwd = value; }
    }
    /// <summary>机器序列码
    /// </summary>
    public string _machineCodeSequence;
    public string machineCodeSequence
    {
        get { return _machineCodeSequence; }
        set { _machineCodeSequence = value; }
    }

    //        isSuccess
    //errMsg:
    //validate:软件有效期
    //machineCodeSequence:机器序列码

    //grantAuthorizatio:授权码
    //sUser: Socket用户名
    //sPwd:Socket密码
    //authentication:鉴权码

}
#endregion
#endregion
