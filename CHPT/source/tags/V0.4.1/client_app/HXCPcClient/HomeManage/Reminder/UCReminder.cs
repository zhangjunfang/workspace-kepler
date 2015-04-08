using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm;
using HXCPcClient.UCForm.RepairBusiness.ReserveQuery;
using HXCPcClient.UCForm.RepairBusiness.RepairQuery;
using SYSModel;
using HXCPcClient.UCForm.SysManage.ReminderSet;

namespace HXCPcClient.HomeManage
{
    public partial class UCReminder : UserControl
    {
        #region --UI交互
        public delegate void UiHandler(DataSet dt);
        public UiHandler uiHandler;       
        private bool loadFlag = true;      
        #endregion

        #region --成员变量
        private int curY = 0;
        #endregion

        #region --构造函数
        public UCReminder()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);

            LocalCache.ReminderComplated -= new LocalCache.ReminderComplate(this._LoadReminder);
            LocalCache.ReminderComplated += new LocalCache.ReminderComplate(this._LoadReminder);
            this.uiHandler -= new UiHandler(this.LoadReminder);
            this.uiHandler += new UiHandler(this.LoadReminder);
        }
        #endregion

        #region --设置
        private void pbSet_Click(object sender, EventArgs e)
        {
            //进入提醒设置
            UCReminderSet ucForm = new UCReminderSet();
            string tag = "S_SystemManagement_ReminderSet|CL_SystemManagement|CL_SystemManagement_set";
            UCBase.AddUserControl(ucForm, "提醒设置", "S_SystemManagement_ReminderSet", tag, "");
        }
        #endregion

        #region --窗体初始化
        private void UCReminder_Load(object sender, EventArgs e)
        {
            if (this.loadFlag)
            {
                if (LocalCache.DsReminder != null
                    && LocalCache.DsReminder.Tables.Count > 0)
                {
                    this.Invoke(this.uiHandler, LocalCache.DsReminder);                   
                }
                this.loadFlag = false;
            }
        }
        #endregion

        private void _LoadReminder(DataSet ds)
        {        
            this.loadFlag = false;
            UiHandler _uiHandler = new UiHandler(this.LoadReminder);
            this.Invoke(_uiHandler, ds);
        }
        private void LoadReminder(DataSet ds)
        {
            this.curY = 0;
            this.panelContent.Controls.Clear();

            foreach (DataTable dt in ds.Tables)
            {
                if (dt.TableName == DataSources.EnumReminderType.YYDZ.ToString())
                {
                    //预约到站提醒
                    this.ShowItem("预约到站提醒", dt);
                }
                else if (dt.TableName == DataSources.EnumReminderType.DPG.ToString())
                {
                    //待派工提醒
                    this.ShowItem("待开工提醒", dt);
                }
                else if (dt.TableName == DataSources.EnumReminderType.SBFWDBH.ToString())
                {
                    //三包服务驳回提醒
                    this.ShowItem("三包服务驳回提醒", dt);   
                }
            }           

            ////缺货提醒
            //this.ShowItem("缺货提醒",ds.Tables["3"]);   

            ////库存高提醒
            //this.ShowItem("库存高提醒",ds.Tables["4"]);   

            ////负库存提醒
            //this.ShowItem("负库存提醒",ds.Tables["5"]);   
        }    

        #region --公共方法
        private void ShowItem(string title, DataTable dt)
        {
            UCReminderItem ucItem = new UCReminderItem(title);
            ucItem.SetItems(dt);
            ucItem.ClickedComplated += new UCReminderItem.ClickedComplate(this.OpenUC);
            this.panelContent.Controls.Add(ucItem);
            ucItem.Width = this.panelContent.Width;
            ucItem.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            ucItem.Location = new Point(0, this.curY);
            this.curY += ucItem.Height;
        }
        #endregion

        private void OpenUC(DataTable dt)
        {
            if (dt.TableName == DataSources.EnumReminderType.YYDZ.ToString())
            {
                if (LocalCache.HasFunction("CL_RepairBusiness_Reserve_UCReserveOrderQuery"))
                {
                    //预约到站提醒             
                    UCReserveOrderQuery ucForm = new UCReserveOrderQuery(dt);
                    string tag = "CL_RepairBusiness_Reserve_UCReserveOrderQuery_Reminder|CL_RepairBusiness_Reserve_Reminder|CL_RepairBusiness_Reserve_UCReserveOrderQuery_Reminder";
                    UCBase.AddUserControl(ucForm, "预约到站提醒", "CL_RepairBusiness_Reserve_UCReserveOrderQuery_Reminder", tag, "");
                }
                return;
            }
            if (dt.TableName == DataSources.EnumReminderType.DPG.ToString())
            {
                if (LocalCache.HasFunction("CL_RepairBusiness_RepairQuery"))
                {
                    //待派工提醒           
                    RepairQueryManager ucForm = new RepairQueryManager(dt);
                    string tag = "CL_RepairBusiness_RepairQuery_Reminder|CL_RepairBusiness_Reserve_Reminder|CL_RepairBusiness_RepairQuery_Reminder";
                    UCBase.AddUserControl(ucForm, "待派工提醒", "CL_RepairBusiness_RepairQuery_Reminder", tag, "");
                }
                return;
            }
            if (dt.TableName == DataSources.EnumReminderType.SBFWDBH.ToString())
            {
                if (LocalCache.HasFunction("CL_RepairBusiness__MaintainThreeGuarantyQuery"))
                {
                    //三包服务驳回提醒          
                    RepairQueryManager ucForm = new RepairQueryManager();
                    string tag = "CL_RepairBusiness__MaintainThreeGuarantyQuery_Reminder|CL_RepairBusiness_YuTongThreeGuaranty|CL_RepairBusiness__MaintainThreeGuarantyQuery_Reminder";
                    UCBase.AddUserControl(ucForm, "三包服务驳回提醒", "CL_RepairBusiness__MaintainThreeGuarantyQuery_Reminder", tag, "");
                }
                return;
            }
        }
    }
}
