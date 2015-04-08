using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm;
using HXCPcClient.UCForm.RepairBusiness.ReserveQuery;
using HXCPcClient.UCForm.RepairBusiness.RepairQuery;
using SYSModel;

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

            ds.Tables[0].TableName = DataSources.EnumReminderType.YYDZ.ToString();
            //预约到站提醒
            this.ShowItem("预约到站提醒",ds.Tables[0]);

            ds.Tables[1].TableName = DataSources.EnumReminderType.DPG.ToString();
            //待派工提醒
            this.ShowItem("待派工提醒", ds.Tables[1]);

            ds.Tables[2].TableName = DataSources.EnumReminderType.SBFWDBH.ToString();
            //三包服务驳回提醒
            this.ShowItem("三包服务驳回提醒", ds.Tables[2]);   

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
                //预约到站提醒             
                UCReserveOrderQuery ucForm = new UCReserveOrderQuery(dt);
                string tag = "UCReserveOrderQuery_self|UCReserveOrderQuery_001|UCReserveOrderQuery_02";
                UCBase.AddUserControl(ucForm, "预约到站提醒", "UCReserveOrderQuery", tag, "");
                return;
            }
            if (dt.TableName == DataSources.EnumReminderType.DPG.ToString())
            {
                //待派工提醒           
                RepairQueryManager ucForm = new RepairQueryManager();
                string tag = "RepairQueryManager_self|RepairQueryManager_001|RepairQueryManager_002";
                UCBase.AddUserControl(ucForm, "待派工提醒", "RepairQueryManager", tag, "");
                return;
            }
            if (dt.TableName == DataSources.EnumReminderType.SBFWDBH.ToString())
            {
                //三包服务驳回提醒          
                RepairQueryManager ucForm = new RepairQueryManager();
                string tag = "UCReserveOrderQuery_self";
                UCBase.AddUserControl(ucForm, "三包服务驳回提醒", "UCReserveOrderQuery", tag, this.Name);
                return;
            }
        }
    }
}
