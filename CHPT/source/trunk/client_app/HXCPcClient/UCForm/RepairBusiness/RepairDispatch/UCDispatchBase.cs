using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.RepairBusiness.RepairDispatch
{
    public delegate void addUserControls(UserControl userContol, string memuName, string menuId, string thisUcTag, string PUCName);//定义委托 
    public delegate void deleteMenu(string strTag, string PUCName);
    /// <summary>
    /// 维修管理-维修调度专用顶部按钮设置
    /// Author：JC
    /// AddTime：2014.10.17
    public partial class UCDispatchBase : UserControl
    {
        public WindowStatus windowStatus = WindowStatus.View;
        public static addUserControls addUserControls;//定义委托实例 
        public static deleteMenu deleteMenu;
        public UCDispatchBase()
        {
            InitializeComponent();

            Load += delegate
            {
                foreach (var cmdButton in tableLayoutPanel1.Controls.OfType<ButtonEx_sms>().Select(control => control))
                {
                    cmdButton.Icon = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(cmdButton.Name.Replace("btn", "") + "_E", true);
                    cmdButton.EnabledChanged += delegate(object sender, EventArgs args)
                    {
                        var cSender = sender as ButtonEx_sms;
                        if (cSender == null) return;
                        if (cSender.Enabled)
                        {
                            var iconE = ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                                cSender.Name.Replace("btn", "") + "_E", true);
                            var iconL = (windowStatus == WindowStatus.Add || windowStatus == WindowStatus.Edit) ? ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                                cSender.Name.Replace("btn", "") + "_L", true) : null;
                            cSender.Icon = iconL ?? iconE;
                        }
                        else
                        {
                            cSender.Icon =
                                ServiceStationClient.Skin.SkinAssistant.GetResourcesImage(
                                    cSender.Name.Replace("btn", "") + "_D", true);
                        }
                        if ((cSender.Tag as ToolStripItem) != null)
                        {
                            (cSender.Tag as ToolStripItem).Enabled = cSender.Enabled;
                        }
                    };
                }
            };
        }
        /// <summary>
        /// 添加标签 添加内容窗体
        /// </summary>
        /// <param name="userContol">新窗体</param>
        /// <param name="memuName">标签名 例： 车型档案-预览  或 车型档案</param>
        /// <param name="menuId">三级菜单id(新窗体+操作动词   例:新增 "UCBaseAdd"  如果是编辑或复制或预览 新窗体+操作动词+操作数据ID 例：UCBaseEdit123)</param>
        /// <param name="thisUcTag">当前窗体Tag tag包括（三级 |一级| 二级 菜单的id）</param>
        /// <param name="PUCName">当前窗体Name   this.Name）</param>
        public void addUserControl(UserControl userContol, string memuName, string menuId, string thisUcTag, string PUCName)
        {
            if (addUserControls != null)
            {
                addUserControls(userContol, memuName, menuId, thisUcTag, PUCName);
            }
        }
        /// <summary>
        /// 删除标签 及其 内容 
        /// </summary>
        /// <param name="strTag">窗体的Tag  或者按钮的 Tag</param>
        /// <param name="PUCName">父窗体name</param>
        public void deleteMenuByTag(string strTag, string PUCName)
        {
            if (deleteMenu != null)
            {
                deleteMenu(strTag, PUCName);
            }
        }

        /// <summary>
        /// 关闭前操作
        /// </summary>
        /// <returns>true关闭当前标签，false阻止关闭当前标签</returns>
        public virtual bool CloseMenu()
        {
            return true;
        }
        public delegate void ClickHandler(object sender, EventArgs e);

        #region 事件定义
        /// <summary> 保存事件
        /// </summary>
        public event ClickHandler AddSaveEvent;

        /// <summary> 逐项派工事件
        /// </summary>
        public event ClickHandler DtaloEvent;


        /// <summary> 整体派工事件
        /// </summary>
        public event ClickHandler OverallEvent;

        /// <summary> 确认派工事件
        /// </summary>
        public event ClickHandler AffirmEvent;

        /// <summary> 开工事件
        /// </summary>
        public event ClickHandler StartEvent;

        /// <summary> 停工事件
        /// </summary>
        public event ClickHandler StopEvent;

        /// <summary> 质检事件
        /// </summary>
        public event ClickHandler QCEvent;

        /// <summary> 试结算事件
        /// </summary>
        public event ClickHandler BalanceEvent;

        /// <summary> 取消事件
        /// </summary>
        public event ClickHandler CancelEvent;

        /// <summary> 预览事件
        /// </summary>
        public event ClickHandler ViewEvent;

        /// <summary> 打印事件
        /// </summary>
        public event ClickHandler PrintEvent;

        /// <summary> 设置事件
        /// </summary>
        public event ClickHandler SetEvent;

        /// <summary> 操作记录事件
        /// </summary>
        public event ClickHandler OperationEvent;

        /// <summary> 完工事件
        /// </summary>
        public event ClickHandler CompleteEvent;
        /// <summary>
        /// 删除事件
        /// </summary>
        public event ClickHandler Delete;
        #endregion

        #region 按钮事件

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (AddSaveEvent != null)
            {
                AddSaveEvent(sender, e);
            }            
        }
        /// <summary>
        /// 逐项派工事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDtalo_Click(object sender, EventArgs e)
        {
            if (DtaloEvent != null)
            {
                DtaloEvent(sender, e);
            }      
        }
        /// <summary>
        /// 整体派工事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOverall_Click(object sender, EventArgs e)
        {

            if (OverallEvent != null)
            {
                OverallEvent(sender, e);
            }    

        }
        /// <summary>
        /// 确认派工事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAffirm_Click(object sender, EventArgs e)
        {
            if (AffirmEvent != null)
            {
                AffirmEvent(sender, e);
            }    
        }
        /// <summary>
        /// 开工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSatart_Click(object sender, EventArgs e)
        {
            if (StartEvent != null)
            {
                StartEvent(sender, e);
            }    
        }
        /// <summary>
        /// 停工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (StopEvent != null)
            {
                StopEvent(sender, e);
            }    
        }
        /// <summary>
        /// 完工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (CompleteEvent != null)
            {
                CompleteEvent(sender, e);
            }    
        }
        /// <summary>
        /// 质检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQC_Click(object sender, EventArgs e)
        {
            if (QCEvent != null)
            {
                QCEvent(sender, e);
            }    
        }
        /// <summary>
        /// 试结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBalance_Click(object sender, EventArgs e)
        {
            if (BalanceEvent != null)
            {
                BalanceEvent(sender, e);
            }    
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CancelEvent != null)
            {
                CancelEvent(sender, e);
            }    
        }
        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (ViewEvent != null)
            {
                ViewEvent(sender, e);
            }    

        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (PrintEvent != null)
            {
                PrintEvent(sender, e);
            }    
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeting_Click(object sender, EventArgs e)
        {
            if (SetEvent != null)
            {
                SetEvent(sender, e);
            }    
        }
        /// <summary>
        /// 操作记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperation_Click(object sender, EventArgs e)
        {
            if (OperationEvent != null)
            {
                OperationEvent(sender, e);
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Delete != null)
            {
                Delete(sender, e);
            }
        }
        #endregion

    }
}
