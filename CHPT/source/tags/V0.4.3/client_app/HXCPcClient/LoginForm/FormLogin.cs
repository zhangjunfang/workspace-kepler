using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using System.Net;
using SYSModel;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;

namespace HXCPcClient.LoginForms
{
    public partial class FormLogin : ShadowForm
    {
        #region --UI交互
        public delegate void UiHandler(object para);
        public UiHandler uiHandler;
        #endregion

        #region --成员变量
        private bool lockFlag = true;
        
        /// <summary> 登录用户名
        /// </summary>
        private string userName = string.Empty;
        /// <summary> 登录密码
        /// </summary>
        private string pwd = string.Empty;
        /// <summary> 默认账套信息 
        /// </summary>
        private string defaultAccount = string.Empty;
        /// <summary> 登录可用标志
        /// </summary>
        private bool enableFlag = false;
        /// <summary> 帐套表
        /// </summary>
        private DataTable dtDb;

        private FormCmb formCmb = null;

        private FormSet formSet = null;

        /// <summary> 用户DataSet
        /// </summary>
        private DataSet dsUser;
        ///// <summary> 当前页码
        ///// </summary>
        //private int currentPage = 0;
        ///// <summary> 页面个数
        ///// </summary>
        //private int pageCount = 4;
        /// <summary> 用户表
        /// </summary>
        private DataTable dtUser;
        #endregion

        #region --构造函数
        public FormLogin()
        {
            InitializeComponent();

            this.pbChecked.Tag = "false";
            this.tbDb.InnerTextBox.BackColor = Color.White;
            this.tbUser.InnerTextBox.BackColor = Color.White;
            this.tbPwd.InnerTextBox.BackColor = Color.White;
            this.tbPwd.InnerTextBox.TextChanged += new EventHandler(this._PwdTextChanged);
        }
        #endregion

        #region --登录按钮
        private void panelLogin_Click(object sender, EventArgs e)
        {
            if (this.enableFlag)
            {                
                if (this.dtDb.Rows.Count == 0)
                {
                    MessageBoxEx.Show("帐套信息加载中…");
                    this.RefreshAccData();
                    return;
                }
                this.errorProvider.Clear();
                if (string.IsNullOrEmpty(this.tbUser.Caption.Trim())
                    || this.tbUser.Caption.Trim() == "用户名")
                {
                    Utility.Common.Validator.SetError(this.errorProvider, this.tbUser, "用户名不能为空!");
                    return;
                }
                if (string.IsNullOrEmpty(this.tbPwd.Caption.Trim())
                    || this.tbPwd.Caption.Trim() == "密码")
                {
                    Utility.Common.Validator.SetError(this.errorProvider, this.tbPwd, "密码不能为空!");
                    return;
                }
                FormLoading.StartLoading(this,new Size(110,120));
                this.enableFlag = false;
                this.userName = this.tbUser.Caption.Trim();
                this.pwd = this.tbPwd.Caption.Trim();
                this.uiHandler += new UiHandler(this.LoginResult);

                //保存帐套信息
                string dbId = this.tbDb.Tag.ToString();
                DataRow dr = this.dtDb.Select("Value='" + dbId + "'")[0];

                GlobalStaticObj.CurrAccID = dbId;
                GlobalStaticObj.CurrAccCode = dr["Code"].ToString();
                GlobalStaticObj.CurrAccName = dr["Name"].ToString();
                GlobalStaticObj.IsDefaultAcc = dr["Status"].ToString() == DataSources.EnumYesNo.Yes.ToString("d");

                this.timer.Enabled = true;

                //登录验证                             
                ThreadPool.QueueUserWorkItem(new WaitCallback(this._Login));
            }
            else
            {
                MessageBoxEx.Show("服务连接异常！");
            }
        }
        private void panelLogin_MouseEnter(object sender, EventArgs e)
        {
            this.panelLogin.BackColor = Color.FromArgb(69, 186, 255);
        }

        private void panelLogin_MouseLeave(object sender, EventArgs e)
        {
            this.panelLogin.BackColor = Color.FromArgb(4, 154, 243);
        }
        #endregion

        #region --关闭按钮
        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(-1);  //add by kord
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

        #region --服务器设置
        private void labelSet_Click(object sender, EventArgs e)
        {
            if (this.formSet == null
                || this.formSet.IsDisposed)
            {
                this.formSet = new FormSet();
                this.formSet.RefreshDataStart -= new FormSet.RefreshData(this.RefreshAccData);
                this.formSet.RefreshDataStart += new FormSet.RefreshData(this.RefreshAccData);
            }
            this.formSet.Show();
            this.formSet.Location = this.Location;
            this.Opacity = 0;
        }
        #endregion

        #region --下拉按钮
        private void pbCmb_Click(object sender, EventArgs e)
        {
            if (formCmb != null)
            {
                formCmb.Visible = !formCmb.Visible;
                if (formCmb.Visible)
                {
                    formCmb.AdjustLocation();
                }
            }
        }

        private void pbCmb_MouseEnter(object sender, EventArgs e)
        {
            this.pbCmb.BackgroundImage = Properties.Resources.down_d;
        }

        private void pbCmb_MouseLeave(object sender, EventArgs e)
        {
            this.pbCmb.BackgroundImage = Properties.Resources.down_n;
        }
        #endregion

        #region --最小化
        private void pbMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        #region --记住用户名密码
        private void pbChecked_Click(object sender, EventArgs e)
        {
            if (this.pbChecked.Tag.ToString() == "false")
            {
                this.pbChecked.Tag = "true";
                this.pbChecked.BackgroundImage = Properties.Resources.select_d;
            }
            else
            {
                this.pbChecked.Tag = "false";
                this.pbChecked.BackgroundImage = Properties.Resources.select_n;
            }
        }
        #endregion

        #region --窗体初始化
        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.dtDb = new DataTable();
            this.dtDb.Columns.Add("DisplayName");
            this.dtDb.Columns.Add("Name");
            this.dtDb.Columns.Add("Value");
            this.dtDb.Columns.Add("Code");
            this.dtDb.Columns.Add("Status");
            this.uiHandler -= new UiHandler(this.ShowAccData);
            this.uiHandler += new UiHandler(this.ShowAccData);

            //初始化Wcf
            WCFClientProxy.CreatePCClientProxy();

            //加载帐套信息
            ThreadPool.QueueUserWorkItem(new WaitCallback(this._LoadAccData));

            //初始化配置
            LocalVariable.Init();

            //加载登录记忆
            this.LoadMemery();
        }
        #endregion

        #region --加载帐套信息
        void RefreshAccData()
        {
            this.Opacity = 1;
            this.uiHandler -= new UiHandler(this.LoginResult);
            this.uiHandler -= new UiHandler(this.ShowAccData);
            this.uiHandler += new UiHandler(this.ShowAccData);
            //加载帐套信息
            ThreadPool.QueueUserWorkItem(new WaitCallback(this._LoadAccData));
        }
        private void _LoadAccData(object obj)
        {
            if (!lockFlag)
            {
                return;
            }

            lockFlag = false;

            string msg = string.Empty;
            DataTable dt = DBHelper.GetAccDataTable(out msg);
            if (dt != null)
            {
                this.dtDb.Clear();

                string dbName = string.Empty;
                string comName = string.Empty;
                bool isDefault = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["status"].ToString() == DataSources.EnumStatus.Stop.ToString("d"))
                    {
                        continue;
                    }

                    dbName = string.Empty;
                    comName = string.Empty;
                    isDefault = false;

                    DataRow drDb = this.dtDb.NewRow();
                    if (dt.Columns.Contains("id"))
                    {
                        drDb["Value"] = dr["id"].ToString();
                    }
                    if (dt.Columns.Contains("setbook_code"))
                    {
                        drDb["Code"] = dr["setbook_code"].ToString();
                    }
                    if (dt.Columns.Contains("setbook_name"))
                    {
                        dbName = dr["setbook_name"].ToString();
                        drDb["Name"] = dbName;
                    }
                    if (dt.Columns.Contains("com_name"))
                    {
                        comName = dr["com_name"].ToString();
                    }
                    //帐套组合方法
                    drDb["DisplayName"] = string.Format(dbName + "（" + drDb["Code"] + "）");

                    if (dt.Columns.Contains("is_main_set_book"))
                    {
                        drDb["Status"] = dr["is_main_set_book"].ToString();
                        isDefault = dr["is_main_set_book"].ToString() == "";
                    }                   
                    if (isDefault)
                    {
                        this.dtDb.Rows.InsertAt(drDb, 0);
                    }
                    else
                    {
                        this.dtDb.Rows.Add(drDb);
                    }
                }
            }
            if (this.uiHandler != null)
            {
                this.Invoke(this.uiHandler, msg);
            }
            lockFlag = true;
        }
        private void ShowAccData(object obj)
        {
            this.uiHandler -= new UiHandler(this.ShowAccData);
            if (obj.ToString().Length > 0)
            {
                FormLoading.EndLoading();
                if (this.formSet == null || !this.formSet.Visible)
                {
                    FormMessgeBox.ShowMsg(this, "无法加载帐套数据！", this.panelTop.BackColor);
                }
            }
            this.enableFlag = true;
            //加载帐套
            if (this.dtDb.Rows.Count > 0)
            {
                this.tbDb.Caption = this.dtDb.Rows[0]["DisplayName"].ToString();
                this.tbDb.Tag = this.dtDb.Rows[0]["Value"].ToString();
                if (this.tbUser.Caption.Trim().Length == 0)
                {
                    this.tbUser.Focus();
                }
                else
                {
                    if (this.tbPwd.Caption.Trim().Length == 0)
                    {
                        this.tbPwd.Focus();
                    }
                    else
                    {
                        this.panelLogin.Focus();
                    }
                }
            }
            this.formCmb = new FormCmb();
            this.formCmb.DisplayName = "DisplayName";
            this.formCmb.ValueName = "Value";
            this.formCmb.ItemHeight = 35;
            this.formCmb.BackColor = Color.White;
            this.formCmb.SelectedColor = Color.FromArgb(97, 167, 208);
            this.formCmb.ItemSelected += new FormCmb.ItemSelectedHandler(this.SeletecdAccount);
            this.formCmb.BindCmb(this.tbDb.InnerTextBox, this.dtDb, this.tbDb.Location.X,
                this.panelDb.Height - this.tbDb.Location.Y - this.tbDb.Height, false);
        }
        /// <summary> 选中账套  
        /// </summary>
        /// <param name="display"></param>
        /// <param name="value"></param>
        private void SeletecdAccount(string display, string value)
        {
            this.tbDb.Caption = display;
            this.tbDb.Tag = value;
        }
        #endregion

        #region --用户登录
        private void _Login(object state)
        {
            string computerName = Dns.GetHostName();//本机名
            string mac = LocalVariable.GetComputerMac();//计算机MAC           

            string errMsg = string.Empty;
            try
            {
                errMsg = DBHelper.LoginInput(this.userName, this.pwd, mac, computerName, Guid.NewGuid().ToString());

            }
            catch
            {
                errMsg = "通讯异常！";
            }

            if (this.uiHandler != null)
            {
                this.Invoke(this.uiHandler, errMsg);
            }
        }
        #endregion

        #region --登录结果
        private void LoginResult(object obj)
        {
            this.uiHandler -= new UiHandler(this.LoginResult);

            if (this.enableFlag)
            {
                return;
            }

            this.timer.Enabled = false;

            string errMsg = obj.ToString();
            if (!string.IsNullOrEmpty(errMsg))
            {
                FormLoading.EndLoading();
                FormMessgeBox.ShowMsg(this, errMsg, this.panelTop.BackColor);
                this.enableFlag = true;                
                return;
            }

            if (HXCPcClient.GlobalStaticObj.gLoginDataSet == null)
            {
                FormMessgeBox.ShowMsg(this, "数据加载失败！", this.panelTop.BackColor);
                FormLoading.EndLoading();
                this.enableFlag = true;
                return;
            }

            if (HXCPcClient.GlobalStaticObj.gLoginDataSet.Tables[0].Rows.Count < 1)
            {
                FormLoading.EndLoading();
                this.enableFlag = true;
                return;
            }

            try
            {
                #region 服务站信息   add by kord
                var dr = DBHelper.GetTable("获取当前服务站信息", GlobalStaticObj.CommAccCode, "tb_company", "*", "data_source = '2'", "", "");
                if (dr == null || dr.Rows.Count == 0)
                {
                    GlobalStaticObj.GlobalLogService.WriteLog("无法获取当前服务站信息");
                }
                else
                {
                    GlobalStaticObj.ServerStationCode = CommonCtrl.IsNullToString(dr.Rows[0]["sap_code"]);
                    GlobalStaticObj.ServerStationName = CommonCtrl.IsNullToString(dr.Rows[0]["com_name"]);
                    
                }
                #endregion

                GlobalStaticObj.CookieStr = GlobalStaticObj.gLoginDataSet.Tables["cookieStr"].Rows[0][0].ToString();
                GlobalStaticObj.UserID = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["user_id"].ToString();
                GlobalStaticObj.UserName = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["user_name"].ToString();
                GlobalStaticObj.LandName = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["land_name"].ToString();
                GlobalStaticObj.PassWord = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["password"].ToString();
                GlobalStaticObj.CurrUserCom_Id = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["com_id"].ToString();
                GlobalStaticObj.CurrUserCom_Code = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["com_code"].ToString();
                GlobalStaticObj.CurrUserCom_Name = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["com_name"].ToString();
                GlobalStaticObj.CurrUserOrg_Id = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["org_id"].ToString();
                GlobalStaticObj.CurrUserOrg_Name = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["org_name"].ToString();
                GlobalStaticObj.CurrUserCom_Category = CommonCtrl.IsNullToString(HXCPcClient.GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["category"]);  //modify by kord

            }
            catch (Exception ex)
            {
                //write log
            }

            ThreadPool.SetMinThreads(3, 3);
            ThreadPool.SetMaxThreads(5, 5);

            //预加载信息
            LocalCache.PreLoad();

            //记忆登录保存
            this.SaveMemery();

            FormLoading.EndLoading();
            this.DialogResult = DialogResult.OK;//关键:设置登陆成功状态            
        }
        #endregion

        #region --加载、显示、保存记忆账户
        /// <summary> 加载记忆账户
        /// </summary>
        private void LoadMemery()
        {
            this.dsUser = ConfigManager.User;
            if (this.dsUser == null)
            {
                this.dsUser = new DataSet();
            }
            if (this.dsUser.Tables.Count == 0)
            {
                DataTable dt = new DataTable("User");
                dt.Columns.Add("ID");
                dt.Columns.Add("PWD");
                this.dsUser.Tables.Add(dt);
            }

            this.dtUser = this.dsUser.Tables[0].Copy();

            if (this.dtUser.Rows.Count > 0)
            {
                if (this.dtUser.Columns.Contains("ID"))
                {
                    this.tbUser.Caption = this.dtUser.Rows[0]["ID"].ToString();
                    this.tbUser.Refresh();
                }
                if (this.dtUser.Columns.Contains("PWD"))
                {
                    this.tbPwd.Caption = this.dtUser.Rows[0]["PWD"].ToString();
                    this.tbPwd.Refresh();
                }
                if (this.tbPwd.Caption.Trim().Length > 0)
                {
                    pbChecked_Click(null, null);
                }
                //this.currentPage = 1;
            }
        }
        private void ShowMemery()
        {

        }
        /// <summary> 保存记忆账户
        /// </summary>
        private void SaveMemery()
        {
            if (this.dsUser == null)
            {
                this.dsUser = new DataSet();
            }

            if (this.dtUser != null)
            {
                DataTable dtTemp = this.dsUser.Tables[0].Copy();

                this.dsUser.Tables.Clear();
                this.dtUser.Rows.Clear();

                DataRow dr = this.dtUser.NewRow();
                dr["ID"] = this.userName;
                if (this.pbChecked.Tag.ToString() == "true")
                {
                    if (dr.Table.Columns.Contains("PWD"))
                    {
                        dr["PWD"] = this.pwd;
                    }
                }
                this.dtUser.Rows.Add(dr);

                foreach (DataRow drUser in dtTemp.Rows)
                {
                    if (drUser["ID"].ToString() != this.userName)
                    {
                        this.dtUser.ImportRow(drUser);
                    }
                }
                this.dsUser.Tables.Add(dtUser);

                ConfigManager.WriteUser(this.dsUser);
            }
        }
        #endregion

        #region --快捷键
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (this.formCmb.Visible)
                {
                    this.formCmb.Hide();
                    return true;
                }
            }
            else if (keyData == Keys.Enter)
            {
                panelLogin_Click(null, null);
                return true;
            }
            else if (keyData == Keys.F5)
            {
                this.uiHandler -= new UiHandler(this.LoginResult);
                this.uiHandler -= new UiHandler(this.ShowAccData);
                this.uiHandler += new UiHandler(this.ShowAccData);
                ThreadPool.QueueUserWorkItem(new WaitCallback(this._LoadAccData));
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region --拖动窗体
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.enableFlag)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
        }
        #endregion

        #region --输入密码
        private void _PwdTextChanged(object sender, EventArgs args)
        {
            if (!(sender is TextBox))
            {
                return;
            }

            TextBox tb = sender as TextBox;
            if (tb.Text == "密码")
            {
                this.tbPwd.InnerTextBox.PasswordChar = '\0';
            }
            else
            {
                this.tbPwd.InnerTextBox.PasswordChar = '●';
            }
        }
        #endregion

        #region --超时检测
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!this.enableFlag)
            {
                FormLoading.EndLoading();
                if (!InetetTest(GlobalStaticObj.DataServerIp, 200))
                {
                    FormMessgeBox.ShowMsg(this, "无法连接服务器！", this.panelTop.BackColor);
                }
                else
                {
                    FormMessgeBox.ShowMsg(this, "服务器登录超时！", this.panelTop.BackColor);
                }
                this.enableFlag = true;
            }
            this.timer.Enabled = false;
        }
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
        #endregion
    }
}