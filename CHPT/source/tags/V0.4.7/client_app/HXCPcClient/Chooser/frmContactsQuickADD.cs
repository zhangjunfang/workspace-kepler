using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using SYSModel;
using Utility.Common;
using HXCPcClient.CommonClass;
using System.Text.RegularExpressions;

namespace HXCPcClient.Chooser
{
    /// <summary>
    /// 为保存事件传递用户信息
    /// </summary>
    /// <param name="contactsInfo">联系人信息</param>
    public delegate void SaveDelegate(ContactsInfo contactsInfo);

    public partial class frmContactsQuickADD : FormEx
    {
        //保存事件
        public event SaveDelegate SaveEvent;

        //构造函数
        public frmContactsQuickADD()
        {
            InitializeComponent();
        }

        //保存按钮
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                if (Save())
                {
                    this.Close();
                }

            }
        }

        //保存
        private bool Save()
        {
            Dictionary<string, ParamObj> dic = new Dictionary<string, ParamObj>();
            List<SysSQLString> listSql = new List<SysSQLString>();
            //List<SQLObj> listSql = new List<SQLObj>();
            SysSQLString sysSql = new SysSQLString();
            //SQLObj obj = new SQLObj();
            sysSql.cmdType = CommandType.Text;
            sysSql.Param = new Dictionary<string, string>();
            //obj.cmdType = CommandType.Text;
            //obj.Param = new Dictionary<string, ParamObj>();
            string cont_id = Guid.NewGuid().ToString();

            //obj.Param.Add("cont_id", new ParamObj("cont_id", cont_id.ToString(), SysDbType.VarChar));//cont_id
            //obj.Param.Add("cont_name", new ParamObj("cont_name", txtContName.Caption, SysDbType.VarChar));//cont_name
            //obj.Param.Add("enable_flag", new ParamObj("enable_flag", (int)DataSources.EnumEnableFlag.USING, SysDbType.Int));//enable_flag
            //obj.Param.Add("data_source", new ParamObj("data_source", (int)DataSources.EnumDataSources.SELFBUILD, SysDbType.Int));//data_source
            //obj.Param.Add("status", new ParamObj("status", (int)DataSources.EnumStatus.Start, SysDbType.Int, 5));//status
            //obj.Param.Add("create_by", new ParamObj("create_by", GlobalStaticObj.UserID, SysDbType.VarChar));//create_by
            //obj.Param.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(DateTime.Now), SysDbType.DateTime));//create_time

            sysSql.sqlString = string.Format(@"INSERT INTO [tb_contacts]
                        ([cont_id]
                        ,[cont_name]       
                        ,[cont_tel]
                        ,[enable_flag]
                        ,[data_source]
                        ,[status]
                        ,[create_by]
                        ,[create_time]
                        ,[cont_phone])
                        VALUES
                        ('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}',{8})",
            cont_id.ToString(),//cont_id
            txtContName.Caption,//cont_name
            EncryptByDB.GetEncFieldValue(txtContTel.Caption),//cont_tel
            (int)DataSources.EnumEnableFlag.USING,//enable_flag
            (int)DataSources.EnumDataSources.SELFBUILD,//data_source
            (int)DataSources.EnumStatus.Start,//status
            GlobalStaticObj.UserID,//create_by
            Common.LocalDateTimeToUtcLong(DateTime.Now),//create_time
            EncryptByDB.GetEncFieldValue(txtContPhone.Caption));//cont_phone
            //            obj.sqlString = string.Format(@"INSERT INTO [tb_contacts]
            //            ([cont_id]
            //            ,[cont_name]       
            //            ,[cont_tel]
            //            ,[enable_flag]
            //            ,[data_source]
            //            ,[status]
            //            ,[create_by]
            //            ,[create_time]
            //            ,[cont_phone])
            //            VALUES
            //            (@cont_id,@cont_name,{0},@enable_flag,@data_source,@status,@create_by,@create_time,{1})",
            //            EncryptByDB.GetEncFieldValue(txtContTel.Caption),//cont_tel
            //            EncryptByDB.GetEncFieldValue(txtContPhone.Caption));//cont_phone

            //listSql.Add(obj);
            listSql.Add(sysSql);
            if (DBHelper.BatchExeSQLStringMultiByTrans("新增联系人", listSql))
            {
                ContactsInfo continfo = new ContactsInfo();
                continfo.contDuty = string.Empty;
                continfo.contID = cont_id;
                continfo.contName = txtContName.Caption;
                continfo.contPhone = txtContTel.Caption;
                continfo.crmId = string.Empty;
                //保存成功后触发保存事件
                if (SaveEvent != null)
                {
                    SaveEvent.BeginInvoke(continfo, null, null);
                }
                MessageBoxEx.Show("保存成功!");
                return true;
            }
            else
            {
                MessageBoxEx.Show("保存失败!");
                return false;
            }
        }

        //检测数据正确性
        private bool Check()
        {
            if (CheckName() && CheckPhone())
                return true;
            else
                return false;
        }

        //检测名称
        private bool CheckName()
        {
            //验证姓名
            string name = txtContName.Caption.Trim();
            if (name.Length == 0)
            {
                MessageBoxEx.Show("请填写联系人姓名！");
                txtContName.Focus();
                return false;
            }
            return true;
        }

        //检测手机和电话
        private bool CheckPhone()
        {
            bool rightphone = false;
            //验证手机号
            string phone = txtContPhone.Caption.Trim();
            if (phone.Length > 0)
            {
                if (!Utility.Common.Validator.IsMobile(phone))
                {
                    rightphone = false;
                }
                else
                    rightphone = true;
            }
            else
                rightphone = false;

            bool righttel = false;
            //验证固话
            string tel = txtContTel.Caption.Trim();
            if (tel.Length > 0 && tel.Length <= 12)
            {
                //if (!IsTel(tel))
                //{

                //    righttel = false;
                //}
                //else
                //    righttel = true;
                righttel = true;
            }
            else
                righttel = false;

            if (righttel || rightphone)
            {
                return true;
            }
            else
            {
                if (!righttel)
                {
                    txtContPhone.Focus();
                }
                else
                {
                    txtContTel.Focus();
                }
                MessageBoxEx.Show("请输入正确的手机号码或电话号码!");
                return false;
            }
        }


        //控制手机只能数字输入
        private void txtContPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNum(e);
        }

        //控制电话只能数字输入
        private void txtContTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNum(e);
        }

        //只允许输入数字的控制
        private static void OnlyNum(KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                return;
            }
        }

        private void btnColse_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
