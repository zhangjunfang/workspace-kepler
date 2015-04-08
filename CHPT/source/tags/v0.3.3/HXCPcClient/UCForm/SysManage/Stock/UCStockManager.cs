using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using System.Threading;
using SYSModel;

namespace HXCPcClient.UCForm.SysManage.Stock
{
    /// <summary>
    /// 库存设置
    /// 创建人：杨天帅
    /// </summary>
    public partial class UCStockManager : UCBase
    {
        #region --成员变量
        private int recordCount = 0;
        private DataTable dt;
        private bool myLock = true;
        #endregion

        #region --构造函数
        public UCStockManager()
        {
            InitializeComponent();
        }
        #endregion

        #region --窗体初始化
        private void UCStockManager_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏
            this.dgvRecord.ReadOnly = false;

            this.uiHandler -= new UiHandler(this.BindTree);
            this.uiHandler += new UiHandler(this.BindTree);

            ThreadPool.QueueUserWorkItem(new WaitCallback(this._LoadWareHouse));
        }
        #endregion

        /// <summary>
        /// 加载仓库
        /// </summary>
        private void _LoadWareHouse(object obj)
        {
            DataTable dt = DBHelper.GetTable("查询在线用户信息", "tb_warehouse", "*", string.Format(" enable_flag='{0}' ", DataSources.EnumEnableFlag.USING.ToString("d")), "", "");
            this.Invoke(this.uiHandler, dt);
        }

        private void BindTree(object obj)
        {
            this.uiHandler -= new UiHandler(this.BindTree);

            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);

            this.tvStock.Nodes.Clear();
            TreeNode root=new TreeNode("全部仓库");            
            if (obj != null)
            {
                DataTable dt = obj as DataTable;
                TreeNode node;
                foreach (DataRow dr in dt.Rows)
                {
                    node = new TreeNode(dr["wh_name"].ToString());
                    node.Tag = dr["wh_id"].ToString();
                    root.Nodes.Add(node);
                }
            }
            tvStock.Nodes.Add(root);
            root.Expand();
            //tvStock   基本单位 下拉框，库存上限数字 ，库存下线数字，默认仓库 复选框；这四列可编辑
        }       

        #region --数据查询
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindPageData(string.Empty);
        }
        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindPageData(string wh_id)
        {
            if (this.myLock)
            {
                this.myLock = false;

                string where = string.Empty;
                //string where = string.Format(" enable_flag='{0}' ", DataSources.EnumEnableFlag.USING.ToString("d"));

                if (wh_id.Length > 0)
                {
                    if (where.Length > 0)
                    {
                        where += " and ";
                    }
                    where += string.Format("wh_id= '{0}'", wh_id);
                }
                else
                {                   
                    if (!string.IsNullOrEmpty(this.tbCode.Caption.Trim()))//编码
                    {
                        if (where.Length > 0)
                        {
                            where += " and ";
                        }
                        where += string.Format("parts_code like '%{0}%'", this.tbCode.Caption.Trim());
                    }

                    if (!string.IsNullOrEmpty(this.tbName.Caption.Trim()))//全称
                    {
                        if (where.Length > 0)
                        {
                            where += " and ";
                        }
                        where += string.Format("parts_name like '%{0}%'", this.tbName.Caption.Trim());
                    }
                }               

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData), where);
            }
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindPageData(object obj)
        {
            DataTable dt = DBHelper.GetTableByPage("分页配件存储", "tb_parts_stock_p", "*", obj.ToString(), "", " order by stock_id",
                           page.PageIndex, page.PageSize, out this.recordCount);

            this.Invoke(this.uiHandler, dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowBindData(object obj)
        {
            this.dgvRecord.DataSource = obj;
            this.page.RecordCount = this.recordCount;

            this.myLock = true;
        }
        /// <summary>
        /// 页码改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            this.BindPageData(string.Empty);
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.tbCode.Caption = string.Empty;
            this.tbName.Caption = string.Empty;
        }

        private void tvStock_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                string wh_id = e.Node.Tag.ToString();
                this.BindPageData(wh_id);
            }
            else
            {
                this.BindPageData(string.Empty);
            }
        }       
    }
}
