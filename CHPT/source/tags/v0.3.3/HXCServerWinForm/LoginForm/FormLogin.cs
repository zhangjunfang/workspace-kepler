using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using ServiceStationClient.ComponentUI;
using SYSModel;
using System.Runtime.InteropServices;
using HXC_FuncUtility;

namespace HXCServerWinForm.LoginForms
{
    public partial class FormLogin : Form
    {
        #region --UI交互
        public delegate void UiHandler(object para);
        public UiHandler uiHandler;
        #endregion

        #region --成员变量
        /// <summary> 登录用户名
        /// </summary>
        private string userName = string.Empty;
        /// <summary> 登录密码
        /// </summary>
        private string pwd = string.Empty;
        /// <summary> 登录用户Id
        /// </summary>
        private string login_Id = string.Empty;     
        /// <summary> 登录可用标志
        /// </summary>
        private bool enableFlag = false;           

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
            this.tbUser.InnerTextBox.BackColor = Color.White;
            this.tbPwd.InnerTextBox.BackColor = Color.White;
            this.tbPwd.InnerTextBox.TextChanged += new EventHandler(this._PwdTextChanged);
            this.enableFlag = true;
            this.tbUser.Tag = string.Empty;

            ThreadPool.SetMinThreads(3, 3);       
        }
        #endregion

        #region --登录按钮
        private void panelLogin_Click(object sender, EventArgs e)
        {
            if (this.enableFlag)
            {                
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
                FormLoading.StartLoading(this);
                this.enableFlag = false;            
                this.userName = this.tbUser.Caption.Trim();
                this.pwd = this.tbPwd.Caption.Trim();                
                this.uiHandler += new UiHandler(this.LoginResult);
                //登录验证                             
                ThreadPool.QueueUserWorkItem(new WaitCallback(this._Login));
            }
            else
            {
                FormMessgeBox.ShowMsg(this, "数据库连接异常！", this.panelTop.BackColor);                 
            }
        }
        private void panelLogin_MouseEnter(object sender, EventArgs e)
        {
            this.panelLogin.BackColor = Color.FromArgb(69, 255, 200);
        }

        private void panelLogin_MouseLeave(object sender, EventArgs e)
        {
            this.panelLogin.BackColor = Color.FromArgb(20, 194, 159);
        }
        #endregion

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
            //初始化配置
            LocalVariable.Init();
           
            //加载登录记忆
            this.LoadMemery();            
        }
        #endregion      

        #region --用户登录
        private void _Login(object state)
        {
            LoginInput loginO = new LoginInput();
            loginO.username = this.userName;
            loginO.pwd = this.pwd;
            GlobalStaticObj_Server.Instance.LoginIP = Utility.Tools.HardwareSerialNumber.Get_UserIP();
            GlobalStaticObj.gLoginDataSet = BLL.ServerUser.UserLogin(loginO, GlobalStaticObj_Server.DbPrefix + GlobalStaticObj_Server.CommAccCode, GlobalStaticObj_Server.Instance.LoginIP);

            if (this.uiHandler != null)
            {
                this.Invoke(this.uiHandler, "");
            }
        }
        #endregion

        #region --登录结果
        private void LoginResult(object obj)
        {
            this.uiHandler -= new UiHandler(this.LoginResult);
            string errMsg = obj.ToString();

            if (!string.IsNullOrEmpty(errMsg))
            {
                FormLoading.EndLoading();
                FormMessgeBox.ShowMsg(this, errMsg, this.panelTop.BackColor); 
                this.enableFlag = true;
                return;
            }
            

            if (GlobalStaticObj.gLoginDataSet == null
               || GlobalStaticObj.gLoginDataSet.Tables.Count == 0
               || GlobalStaticObj.gLoginDataSet.Tables[0].Rows.Count < 1)
            {
                this.enableFlag = true;
                FormLoading.EndLoading();
                FormMessgeBox.ShowMsg(this, "用户名或密码错误！", this.panelTop.BackColor);         
                return;
            }

            if (HXCServerWinForm.GlobalStaticObj.gLoginDataSet.Tables[0].Rows.Count < 1)
            {
                this.enableFlag = true;
                FormLoading.EndLoading();
                return;
            }

            #region 设置全局变量
            GlobalStaticObj_Server.Instance.UserID = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["user_id"].ToString();
            GlobalStaticObj_Server.Instance.UserName = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["user_name"].ToString();
            GlobalStaticObj_Server.Instance.LoginName = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["land_name"].ToString();
            GlobalStaticObj_Server.Instance.PassWord = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["password"].ToString();

            GlobalStaticObj_Server.Instance.RoleID = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["role_id"].ToString();
            GlobalStaticObj_Server.Instance.RoleName = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["role_name"].ToString();
            GlobalStaticObj_Server.Instance.OrgID = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["org_id"].ToString();
            GlobalStaticObj_Server.Instance.OrgName = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["org_name"].ToString();

            GlobalStaticObj_Server.Instance.ComID = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["com_id"].ToString();
            GlobalStaticObj_Server.Instance.ComName = GlobalStaticObj.gLoginDataSet.Tables[0].Rows[0]["com_name"].ToString();
            GlobalStaticObj_Server.Instance.LoginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            #endregion

            //预加载信息
            //LocalCache.PreLoad();

            //登录记忆保存
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
                this.tbUser.Caption = this.dtUser.Rows[0]["ID"].ToString();
                this.tbPwd.Caption = this.dtUser.Rows[0]["PWD"].ToString();
                if (this.tbPwd.Caption.Trim().Length > 0)
                {
                    pbChecked_Click(null, null);
                }        

                this.formCmb = new FormCmb();
                this.formCmb.DisplayName = "ID";
                this.formCmb.ValueName = "ID";
                this.formCmb.ItemHeight = 35;
                this.formCmb.BackColor = Color.White;
                this.formCmb.SelectedColor = Color.FromArgb(97, 167, 208);
                this.formCmb.ItemSelected += new FormCmb.ItemSelectedHandler(this.SeletecdUser);
                this.formCmb.ItemDeleted += new FormCmb.ItemDeletedHandler(this.DeletedUser);
                this.formCmb.BindCmb(this.tbUser.InnerTextBox, this.dtUser, this.tbUser.Location.X,
                    this.panelUser.Height - this.tbUser.Location.Y - this.tbUser.Height, true);
            }
            if (this.tbUser.Caption.Length > 0)
            {
                this.tbPwd.InnerTextBox.Focus();
            }
            else
            {
                this.tbUser.InnerTextBox.Focus();
            }
        }
        /// <summary> 选中用户
        /// </summary>
        /// <param name="display"></param>
        /// <param name="value"></param>
        private void SeletecdUser(string display, string value)
        {
            this.tbUser.Caption = display;
            this.tbUser.Tag = value;
        }
        /// <summary> 删除用户
        /// </summary>     
        /// <param name="value"></param>
        private void DeletedUser(string value)
        {
            if (this.dtUser == null)
            {
                return;
            }

            DataRow dr = this.dtUser.Select("ID='" + value + "'")[0];
            this.dtUser.Rows.Remove(dr);

            if (this.tbUser.Tag.ToString() == value)
            {
                this.tbUser.Caption = string.Empty;
                this.tbUser.Tag = string.Empty;
                this.tbPwd.Caption = string.Empty;
            }

            this.formCmb.RefreshItems(this.dtUser);
            if (this.dtUser.Rows.Count == 0)
            {
                this.formCmb = null;
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
                if (this.pbChecked.Tag.ToString()=="true")
                {
                    dr["PWD"] = this.pwd;
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
                if (this.formCmb != null && this.formCmb.Visible)
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
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
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

        #region --数据库设置
        private void labelSet_Click(object sender, EventArgs e)
        {
            if (this.formSet == null
                || this.formSet.IsDisposed)
            {
                this.formSet = new FormSet();
                //this.formSet.RefreshDataStart += new FormSet.RefreshData(this.RefreshAccData);
            }
            this.formSet.Show();
            this.formSet.Location = this.Location;
        }
        #endregion
    }
}