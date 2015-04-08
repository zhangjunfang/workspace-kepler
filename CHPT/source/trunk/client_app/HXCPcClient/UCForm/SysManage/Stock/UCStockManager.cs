using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using System.Threading;
using SYSModel;
using ServiceStationClient.ComponentUI;
using Utility.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace HXCPcClient.UCForm.SysManage.Stock
{
    /// <summary>
    /// 库存设置
    /// 创建人：杨天帅
    /// </summary>
    public partial class UCStockManager : UCBase
    {
        #region --成员变量
        private DataTable dtHourse = null;
        private bool myLock = true;
        private string wh_id = string.Empty;
        private string wh_code = string.Empty;
        private string wh_name = string.Empty;
        private bool isMore = false;
        private List<int> rowList = new List<int>();
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

            this.btnEdit.Visible = true;
            this.btnEdit.Enabled = true;
            this.btnAdd.Visible = false;
            this.btnDelete.Visible = false;
            this.btnStatus.Visible = false;
            this.btnSave.Visible = true;
            this.btnSave.Enabled = false;
            this.btnCancel.Visible = true;
            this.btnCancel.Enabled = true;

            this.uiHandler -= new UiHandler(this.BindTree);
            this.uiHandler += new UiHandler(this.BindTree);

            base.EditEvent += new ClickHandler(UC_EditEvent);
            base.SaveEvent += new ClickHandler(UC_SaveEvent);
            base.CancelEvent += new ClickHandler(UC_CancelEvent);

            ThreadPool.QueueUserWorkItem(new WaitCallback(this._LoadWareHouse));
        }
        #endregion

        #region --加载仓库树
        /// <summary>
        /// 加载仓库
        /// </summary>
        private void _LoadWareHouse(object obj)
        {
            this.dtHourse = DBHelper.GetTable("查询仓库信息", "tb_warehouse", "*",
                string.Format(" enable_flag='{0}' ", DataSources.EnumEnableFlag.USING.ToString("d")), "", "");
            this.Invoke(this.uiHandler, "");
        }

        private void BindTree(object obj)
        {
            this.uiHandler -= new UiHandler(this.BindTree);

            this.uiHandler -= new UiHandler(this.ShowBindData);
            this.uiHandler += new UiHandler(this.ShowBindData);

            this.tvStock.Nodes.Clear();
            TreeNode root = new TreeNode("全部仓库");
            if (this.dtHourse != null)
            {
                TreeNode node;
                foreach (DataRow dr in this.dtHourse.Rows)
                {
                    node = new TreeNode(dr["wh_name"].ToString());
                    node.Tag = dr["wh_id"].ToString();
                    root.Nodes.Add(node);
                }
            }
            tvStock.Nodes.Add(root);
            root.Expand();

            this.BindPageData();
        }
        #endregion

        #region --数据查询
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindPageData();
        }
        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            if (this.myLock)
            {
                this.myLock = false;

                string where = string.Empty;

                if (!string.IsNullOrEmpty(this.tbCode.Caption.Trim()))//编码
                {
                    where += string.Format("a.ser_parts_code like '%{0}%'", this.tbCode.Caption.Trim());
                }

                if (!string.IsNullOrEmpty(this.tbName.Caption.Trim()))//全称
                {
                    if (where.Length > 0)
                    {
                        where += " and ";
                    }
                    where += string.Format("a.parts_name like '%{0}%'", this.tbName.Caption.Trim());
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(this._BindPageData), where);
            }
        }
        /// <summary> 异步数据查询 
        /// </summary>
        /// <param name="obj"></param>
        private void _BindPageData(object obj)
        {
            SQLObj sql = new SQLObj();
            sql.Param = new Dictionary<string, SYSModel.ParamObj>();
            sql.cmdType = CommandType.Text;
            sql.sqlString = " select top 500 a.parts_id,a.ser_parts_code,a.parts_name,a.default_unit,";
            sql.sqlString += " b.id,b.wh_id,b.max_num,b.min_num,b.is_default,b.create_time ";
            sql.sqlString += " from tb_parts a left join tr_hourse_parts b on a.parts_id=b.parts_id ";
            if (obj.ToString().Length > 0)
            {
                sql.sqlString += " where ";
                sql.sqlString += obj.ToString();
            }
            sql.sqlString += " order by a.create_time desc";

            DataTable _dt = null;
            try
            {
                DataSet ds = DBHelper.GetDataSet("获取仓库配件关系", sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    _dt = ds.Tables[0];
                }
            }
            catch
            {

            }

            if (dgvRecord.InvokeRequired)
            {
                dgvRecord.Invoke(uiHandler, _dt);
            }
            //this.Invoke(this.uiHandler, _dt);
        }
        /// <summary> 异步绑定数据 
        /// </summary>
        /// <param name="obj"></param>
        private void ShowBindData(object obj)
        {
            this.btnSave.Enabled = false;
            this.rowList.Clear();
            if (!this.isMore)
            {
                this.dgvRecord.Rows.Clear();
            }

            if (obj == null)
            {
                this.myLock = true;
                return;
            }
            DataTable _dt = obj as DataTable;
            string code = string.Empty;
            string name = string.Empty;

            if (this.dtHourse != null)
            {
                if (this.wh_code.Length > 0)
                {
                    code = this.wh_code;
                    name = this.wh_name;
                    this.AddData(_dt, code, name, wh_id);
                }
                else
                {
                    foreach (DataRow dr in this.dtHourse.Rows)
                    {
                        code = dr["wh_code"].ToString();
                        name = dr["wh_name"].ToString();
                        this.AddData(_dt, code, name, dr["wh_id"].ToString());
                    }
                }
            }

            this.myLock = true;
        }

        private void AddData(DataTable _dt, string code, string name, string id)
        {
            DataGridViewRow dgvr;
            foreach (DataRow dr in _dt.Rows)
            {
                if (dr["wh_id"].ToString().Length > 0)
                {
                    if (dr["wh_id"].ToString() != id)
                    {
                        continue;
                    }
                }

                dgvr = new DataGridViewRow();
                dgvr.ReadOnly = true;

                DataGridViewTextBoxCell wh_id = new DataGridViewTextBoxCell();
                wh_id.Value = id;
                dgvr.Cells.Add(wh_id);

                DataGridViewTextBoxCell parts_id = new DataGridViewTextBoxCell();
                parts_id.Value = dr["parts_id"];
                dgvr.Cells.Add(parts_id);

                DataGridViewTextBoxCell wh_code = new DataGridViewTextBoxCell();
                wh_code.Value = code;
                dgvr.Cells.Add(wh_code);

                DataGridViewTextBoxCell wh_name = new DataGridViewTextBoxCell();
                wh_name.Value = name;
                dgvr.Cells.Add(wh_name);

                DataGridViewTextBoxCell ser_parts_code = new DataGridViewTextBoxCell();
                ser_parts_code.Value = dr["ser_parts_code"];
                dgvr.Cells.Add(ser_parts_code);

                DataGridViewTextBoxCell parts_name = new DataGridViewTextBoxCell();
                parts_name.Value = dr["parts_name"];
                dgvr.Cells.Add(parts_name);

                DataGridViewTextBoxCell parts_num = new DataGridViewTextBoxCell();
                parts_num.Value = string.Empty;
                dgvr.Cells.Add(parts_num);

                DataGridViewTextBoxCell parts_unit = new DataGridViewTextBoxCell();
                parts_unit.Value = dr["default_unit"];
                dgvr.Cells.Add(parts_unit);

                DataGridViewTextBoxCell parts_min = new DataGridViewTextBoxCell();
                parts_min.Value = dr["min_num"];

                dgvr.Cells.Add(parts_min);

                DataGridViewTextBoxCell parts_max = new DataGridViewTextBoxCell();
                parts_max.Value = dr["max_num"];
                dgvr.Cells.Add(parts_max);

                DataGridViewCheckBoxCell parts_default = new DataGridViewCheckBoxCell();
                parts_default.Value = dr["is_default"];
                dgvr.Cells.Add(parts_default);

                DataGridViewTextBoxCell tb_id = new DataGridViewTextBoxCell();
                tb_id.Value = dr["id"];
                dgvr.Cells.Add(tb_id);

                DataGridViewTextBoxCell tb_fill = new DataGridViewTextBoxCell();
                dgvr.Cells.Add(tb_fill);

                this.dgvRecord.Rows.Add(dgvr);
            }
        }
        /// <summary>
        /// 页码改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            this.BindPageData();
        }
        #endregion

        #region --按钮事件
        //编辑
        void UC_EditEvent(object sender, EventArgs e)
        {
            if (dgvRecord.CurrentRow == null)
            {
                MessageBoxEx.Show("请选择编辑记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.dgvRecord.CurrentRow.ReadOnly = false;

            foreach (DataGridViewRow item in dgvRecord.Rows)
            {
                item.Cells["columnMin"].ReadOnly = false;
                item.Cells["columnMax"].ReadOnly = false;
            }

            if (!this.rowList.Contains(this.dgvRecord.CurrentRow.Index))
            {
                if (!this.btnSave.Enabled)
                {
                    this.btnSave.Enabled = true;
                }

                this.rowList.Add(this.dgvRecord.CurrentRow.Index);
                SetDgvEditCellBgColor(this.dgvRecord.CurrentRow, new string[] { this.columnMax.Name, this.columnMin.Name }, true);
            }
        }
        //保存
        void UC_SaveEvent(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (!this.ValidatorDgv(ref msg))
            {
                MessageBoxEx.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.dgvRecord.EndEdit();

            List<SysSQLString> sqlList = new List<SysSQLString>();

            string max = string.Empty;
            string min = string.Empty;
            string id = string.Empty;
            string wh_id = string.Empty;
            string parts_id = string.Empty;
            foreach (int index in this.rowList)
            {
                id = this.dgvRecord[this.columnId.Name, index].Value.ToString();
                max = this.dgvRecord[this.columnMax.Name, index].Value.ToString();
                min = this.dgvRecord[this.columnMin.Name, index].Value.ToString();
                wh_id = this.dgvRecord[this.column_wh_id.Name, index].Value.ToString();
                parts_id = this.dgvRecord[this.column_parts_id.Name, index].Value.ToString();
                SysSQLString sysSql = new SysSQLString();
                sysSql.cmdType = CommandType.Text;
                sysSql.Param = new Dictionary<string, string>();
                if (id.Length > 0)
                {
                    //修改
                    sysSql.sqlString += string.Format("update tr_hourse_parts set max_num={0},min_num={1} where id='{2}'", max, min, id);
                }
                else
                {
                    //新增
                    sysSql.sqlString += string.Format("insert into tr_hourse_parts(id,wh_id,parts_id,max_num,min_num,create_by,create_time) values ('{0}','{1}','{2}',{3},{4},'{5}',{6})"
                        , Guid.NewGuid().ToString(), wh_id, parts_id, max, min, HXCPcClient.GlobalStaticObj.UserID, Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime));
                }
                sqlList.Add(sysSql);
            }

            if (DBHelper.BatchExeSQLStringMultiByTrans("编辑仓库配件关系", sqlList))
            {
                MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                btnQuery.PerformClick();
                foreach (int index in this.rowList)
                {
                    this.dgvRecord.Rows[index].ReadOnly = true;
                    SetDgvEditCellBgColor(this.dgvRecord.Rows[index], new string[] { this.columnMax.Name, this.columnMin.Name }, false);
                }
            }
            else
            {
                MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        

        private bool ValidatorDgv(ref string msg)
        {
            decimal max = 0;
            decimal min = 0;
            foreach (int index in this.rowList)
            {
                string MaxEditAfterString = this.dgvRecord[this.columnMax.Name, index].EditedFormattedValue.ToString();
                string MinEditAfterString = this.dgvRecord[this.columnMin.Name, index].EditedFormattedValue.ToString();
                if (!ValidateUtil.isnumeric(MaxEditAfterString) || !ValidateUtil.isnumeric(MinEditAfterString))
                {
                    msg = "第" + (index + 1) + "行应输入数字！";
                    return false;
                }

                if (!decimal.TryParse(MaxEditAfterString, out max))
                {
                    msg = "第" + (index + 1) + "行库存上限应输入数字！";
                    return false;
                }
                if (!decimal.TryParse(MinEditAfterString, out min))
                {
                    msg = "第" + (index + 1) + "行库存下限应输入数字！";
                    return false;
                }
                if (max < min || max == min)
                {
                    msg = "第" + (index + 1) + "行库存下限应大于上限值！";
                    return false;
                }
                if (max.ToString().Length > 20)
                {
                    msg = "第" + (index + 1) + "行库存下限数据过大，请仔细核对你填写的数据";
                    return false;
                }
                if (min.ToString().Length > 20)
                {
                    msg = "第" + (index + 1) + "行库存上限数据过大，请仔细核对你填写的数据";
                    return false;
                }
            }
            return true;
        }
        //取消
        void UC_CancelEvent(object sender, EventArgs e)
        {
            this.BindPageData();
        }
        #endregion

        #region --清除查询
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.tbCode.Caption = string.Empty;
            this.tbName.Caption = string.Empty;
        }
        #endregion

        #region --选中仓库
        private void tvStock_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                this.wh_id = e.Node.Tag.ToString();
                DataRow[] drs = this.dtHourse.Select("wh_id='" + this.wh_id + "'");
                if (drs.Length > 0)
                {
                    this.wh_code = drs[0]["wh_code"].ToString();
                    this.wh_name = e.Node.Text;
                    this.BindPageData();
                }
            }
            else
            {
                this.wh_id = string.Empty;
                this.wh_code = string.Empty;
                this.wh_name = string.Empty;
                this.BindPageData();
            }
        }
        #endregion

        #region --编辑颜色变化
        /// <summary> 设置编辑行编辑数据的背景色
        /// </summary>
        /// <param name="row">编辑行</param>
        /// <param name="fields">编辑字段</param>
        private void SetDgvEditCellBgColor(DataGridViewRow row, string[] fields, bool isEdit)
        {
            DataGridViewCellStyle cell = new DataGridViewCellStyle();
            if (isEdit)
            {
                cell.ForeColor = Color.White;
                cell.BackColor = Color.FromArgb(20, 129, 194);
                cell.SelectionBackColor = Color.FromArgb(20, 129, 194);
            }
            else
            {
                cell.ForeColor = Color.Black;
                cell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
                cell.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            }

            foreach (string str in fields)
            {
                row.Cells[str].Style = cell;
            }
            row.DataGridView.CurrentCell = row.Cells[fields[0]];
        }

        private void dgvRecord_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string name = this.dgvRecord.Columns[e.ColumnIndex].Name;
            if (name != this.columnMax.Name
                && name != this.columnMin.Name)//非编辑字段
            {
                // 取消编辑 
                e.Cancel = true;
            }
        }
        #endregion

    }
}
