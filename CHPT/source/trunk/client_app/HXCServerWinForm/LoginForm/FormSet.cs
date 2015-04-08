using System;
using System.Drawing;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXC_FuncUtility;
using DBUtility;
using System.Collections;
using System.Threading;
using System.Net.NetworkInformation;
using Utility.Security;

namespace HXCServerWinForm.LoginForms
{
    public partial class FormSet : Form
    {
        #region --回调更新事件
        public delegate void TestDelegate(bool flag);
        public TestDelegate TestDelegated;

        public delegate void RefreshData();
        public RefreshData RefreshDataStart;
        #endregion

        #region --成员变量
        /// <summary>
        /// 数据库连接拼接数组
        /// </summary>
        private string[] arrays;

        private string ip = string.Empty;
        private string db = string.Empty;
        private string user = string.Empty;
        private string dbPwd = string.Empty;
        Hashtable ht;
        private string connString = string.Empty;
        private bool myLock = true;

        #endregion

        public FormSet()
        {
            InitializeComponent();
        }

        #region --关闭按钮
        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            this.pbClose.BackgroundImage = Properties.Resources.close_d;
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            this.pbClose.BackgroundImage = Properties.Resources.close_n;
        }
        #endregion

        #region --最小化
        private void pbMin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbMin_MouseEnter(object sender, EventArgs e)
        {
            this.pbMin.BackgroundImage = Properties.Resources.small_d;
        }

        private void pbMin_MouseLeave(object sender, EventArgs e)
        {
            this.pbMin.BackgroundImage = Properties.Resources.small_n;
        }
        #endregion

        #region --确认按钮
        private void panelYes_Click(object sender, EventArgs e)
        {
            if (!myLock)
            {
                return;
            }
            this.myLock = false;

            this.errorProvider.Clear();

            if (string.IsNullOrEmpty(this.tbDBIp.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(this.errorProvider, this.tbDBIp, "服务器Ip不能为空!");
                return;
            }
            if (!InetetTest(this.tbDBIp.Caption.Trim(), 100))
            {
                FormMessgeBox.ShowMsg(this, "IP无法连接！", this.panelTop.BackColor);
                this.myLock = true;
                return;
            }
            if (string.IsNullOrEmpty(this.tbDBUser.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(this.errorProvider, this.tbDBUser, "用户名不能为空!");
                return;
            }

            if (string.IsNullOrEmpty(this.tbDBPwd.Caption.Trim()))
            {
                Utility.Common.Validator.SetError(this.errorProvider, this.tbDBPwd, "密码不能为空!");
                return;
            }

            this.arrays[1] = this.tbDBIp.Caption.Trim();

            this.arrays[5] = this.tbDBUser.Caption.Trim();

            this.arrays[7] = this.tbDBPwd.Caption.Trim();

            connString = ConfigHelper.PackageConnString(this.arrays);

            FormLoading.StartLoading(this);

            ThreadPool.QueueUserWorkItem(new WaitCallback(this._DbTest), connString);
        }
        private void _DbTest(object obj)
        {
            bool flag = SqlHelper.IsConnected(
                obj.ToString().Replace("@@@", GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode));

            this.Invoke(this.TestDelegated, flag);
        }
        private void ShowTestResult(bool flag)
        {
            FormLoading.EndLoading();
            if (flag)
            {
                string _connString = Secret.Encrypt3DES(connString, LocalVariable.Key);

                if (ConfigManager.SaveConnString(ConfigConst.ConnectionManageString, _connString, ConfigConst.ConfigPath)
                    && ConfigManager.SaveConnString(ConfigConst.ConnectionStringWrite, _connString, ConfigConst.ConfigPath)
                    && ConfigManager.SaveConnString(ConfigConst.ConnectionStringReadonly, _connString, ConfigConst.ConfigPath)
                    && ConfigManager.SaveConnString(ConfigConst.ConStrManageSql, _connString, ConfigConst.ConfigPath))
                {
                    LocalVariable.SetConnStringValue(ConfigConst.ConnectionManageString, connString);
                    LocalVariable.SetConnStringValue(ConfigConst.ConnectionStringWrite, connString);
                    LocalVariable.SetConnStringValue(ConfigConst.ConnectionStringReadonly, connString);
                    LocalVariable.SetConnStringValue(ConfigConst.ConStrManageSql, connString);

                    FormMessgeBox.ShowMsg(this, "测试成功，并保存！", this.panelTop.BackColor);
                    this.myLock = true;
                    this.Close();
                }
                else
                {
                    FormMessgeBox.ShowMsg(this, "测试成功，但保存失败！", this.panelTop.BackColor);
                }
            }
            else
            {
                FormMessgeBox.ShowMsg(this, "数据库连接失败，请确认用户名密码是否正确！", this.panelTop.BackColor);
            }
            this.myLock = true;
        }

        private void panelYes_MouseEnter(object sender, EventArgs e)
        {
            this.panelYes.BackColor = Color.FromArgb(9, 222, 160);
        }

        private void panelYes_MouseLeave(object sender, EventArgs e)
        {
            this.panelYes.BackColor = Color.FromArgb(4, 243, 172);
        }
        #endregion

        #region --取消按钮
        private void panelCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


        private void FormSet_Load(object sender, EventArgs e)
        {
            this.TestDelegated -= new TestDelegate(this.ShowTestResult);
            this.TestDelegated += new TestDelegate(this.ShowTestResult);

            this.tbDBName.ReadOnly = true;

            this.tbDBPwd.InnerTextBox.PasswordChar = '●';
            this.SetValues();
        }

        #region --设置数据库连接值
        /// <summary>
        /// 设置参数显示值
        /// </summary>
        private void SetValues()
        {
            //设置数据库连接解析内容
            this.arrays = ConfigHelper.SplitConnString(GlobalStaticObj_Server.Instance.ManagerConnString);
            //数据库机器IP
            if (this.arrays.Length >= 2)
            {
                ip = this.arrays[1];
            }
            //数据库名称
            if (this.arrays.Length >= 4)
            {
                db = GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode;
            }
            //数据库登陆用户名
            if (this.arrays.Length >= 6)
            {
                user = this.arrays[5];
            }
            //登录口令
            if (this.arrays.Length >= 8)
            {
                dbPwd = this.arrays[7];
            }
            this.tbDBIp.Caption = ip;
            this.tbDBName.Caption = db;
            this.tbDBUser.Caption = user;
            this.tbDBPwd.Caption = dbPwd;
        }
        #endregion


        /// <summary>
        /// 网络测试
        /// </summary>
        /// <param name="remoteIP">远程服务器IP</param>
        /// <param name="time">超时时间</param>
        /// <returns></returns>
        public static bool InetetTest(string remoteIP, int time)
        {
            Ping ping = new Ping();
            try
            {
                PingReply pr = ping.Send(remoteIP, time);
                if (pr.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        private void FormSet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.RefreshDataStart != null)
            {
                this.RefreshDataStart();
            }
        }

        #region --快捷键
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.panelYes_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}
