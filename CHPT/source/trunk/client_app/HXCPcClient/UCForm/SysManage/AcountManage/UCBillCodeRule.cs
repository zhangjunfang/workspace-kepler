using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;

namespace HXCPcClient.UCForm.SysManage.AcountManage
{
    /// <summary>
    /// 编码生成规则
    /// 修改人：杨天帅
    /// </summary>
    public partial class UCBillCodeRule : UCBase
    {
        public UCBillCodeRule()
        {
            InitializeComponent();
            DataGridViewEx.SetDataGridViewStyle(this.dgvBillCodeRule);
        }
        /// <summary> 操作行索引
        /// </summary>
        private int editRowIndex = -1;
        private void UCBillCodeRule_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);//角色按钮权限-是否隐藏   
            this.btnDelete.Visible = false;
            this.btnStatus.Visible = false;
            this.btnSave.Visible = true;
            this.btnCancel.Visible = true;

            this.dgvBillCodeRule.ReadOnly = false;
            base.SetBtnStatus(WindowStatus.View);
            base.EditEvent += new ClickHandler(UCDictionaryAddOrEdit_EditEvent);
            base.SaveEvent += new ClickHandler(UCDictionaryAddOrEdit_SaveEvent);
            base.CancelEvent += new ClickHandler(UCDictionaryAddOrEdit_CancelEvent);

            DataSources.BindComDataGridViewBoxColumnDataEnum(this.code_method, typeof(DataSources.EnumBillCodeMethod));
            DataSources.BindComDataGridViewBoxColumnDataEnum(this.delimiter, typeof(DataSources.EnumDelimiter));

            this.BindData();
        }

        #region 按钮事件
        /// <summary> 编辑
        /// </summary>
        void UCDictionaryAddOrEdit_EditEvent(object sender, EventArgs e)
        {
            if (dgvBillCodeRule.CurrentRow == null)
            {
                MessageBox.Show("请选择编辑记录！");
                return;
            }
            object obj = dgvBillCodeRule.CurrentRow.Cells["last_bill_no"].Value;
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                MessageBoxEx.ShowWarning("该编码规则正在使用，不能修改!");
                return;
            }
            editRowIndex = dgvBillCodeRule.CurrentRow.Index;
            SetDgvEditCellBgColor(dgvBillCodeRule.Rows[editRowIndex], new string[] { "bill_code", "code_method", "delimiter", "start_num", "example" }, true);
            base.SetBtnStatus(WindowStatus.Edit);
        }

        /// <summary> 编辑
        /// </summary>
        private void dgvBillCodeRule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                UCDictionaryAddOrEdit_EditEvent(null, null);
            }
        }

        /// <summary> 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UCDictionaryAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            if (editRowIndex < 0)
            {
                return;
            }
            if (!ValidateValue())
            {
                return;
            }
            bool flag = SaveRecord();
            if (flag)
            {
                SetDgvEditCellBgColor(dgvBillCodeRule.Rows[editRowIndex], new string[] { "bill_code", "code_method", "delimiter", "start_num", "example" }, false);
                dgvBillCodeRule.CurrentCell = dgvBillCodeRule.Rows[editRowIndex].Cells[0];
                base.SetBtnStatus(WindowStatus.Save);
                editRowIndex = -1;
                MessageBoxEx.Show("操作成功！");
            }
            else
            {
                MessageBoxEx.Show("操作失败！");
            }
        }

        /// <summary> 取消
        /// </summary>
        void UCDictionaryAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            if (editRowIndex < 0 || dgvBillCodeRule.Rows.Count == 0)
            {
                return;
            }
            this.BindData();
            base.SetBtnStatus(WindowStatus.View);
        }

        /// <summary> 控制批号单元格编辑
        /// </summary>
        private void dgvBillCodeRule_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //判断是否编辑列
            if (e.RowIndex != editRowIndex)
            {
                e.Cancel = true;
            }
            string field = dgvBillCodeRule.Columns[e.ColumnIndex].DataPropertyName;
            if (field != "bill_code" && field != "code_method" && field != "delimiter" && field != "start_num" && field != "example")//非编辑字段
            {
                // 取消编辑 
                e.Cancel = true;
            }
        }


        /// <summary> cell值变化
        /// </summary> 
        private void dgvBillCodeRule_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex == editRowIndex)
            {
                string colName = dgvBillCodeRule.Columns[e.ColumnIndex].Name;
                if (colName.Equals("bill_code") || colName.Equals("code_method")
                    || colName.Equals("delimiter") || colName.Equals("start_num"))
                {
                    DataGridViewRow row = dgvBillCodeRule.Rows[e.RowIndex];
                    object objBillCode = row.Cells["bill_code"].Value;
                    string billCode = objBillCode == null ? "" : objBillCode.ToString().ToUpper();
                    object objCodeMethod = row.Cells["code_method"].Value;
                    string codeMethod = objCodeMethod == null ? "" : objCodeMethod.ToString();
                    object objDelimiter = row.Cells["delimiter"].Value;
                    string delimiter = objDelimiter == null ? "" : objDelimiter.ToString();

                    object objStartNum = row.Cells["start_num"].Value;
                    string startNum = objStartNum == null ? "" : objStartNum.ToString();
                    if (!string.IsNullOrEmpty(billCode) && !string.IsNullOrEmpty(startNum))
                    {
                        string prefix = CommonUtility.GetPrefix(billCode, codeMethod, delimiter);
                        row.Cells["example"].Value = prefix + startNum;
                    }
                }
            }
        }

        #endregion

        #region 成员方法

        /// <summary> 绑定数据
        /// </summary>
        private void BindData()
        {
            DataTable dt = DBHelper.GetTable("查询编码生成规则", "v_bill_code_rule", "*", "", "", "bill_code");
            this.dgvBillCodeRule.DataSource = dt;
        }

        /// <summary> 保存验证
        /// </summary>
        /// <returns></returns>
        public bool ValidateValue()
        {
            DataGridViewRow row = dgvBillCodeRule.Rows[editRowIndex];
            string billCode = row.Cells["bill_code"].EditedFormattedValue.ToString();
            if (string.IsNullOrEmpty(billCode))
            {
                MessageBoxEx.Show("项目编码不能为空！");
                dgvBillCodeRule.CurrentCell = row.Cells["bill_code"];
                return false;
            }
            else
            {
                if (billCode.Length > 7 || Common.ValidateStr(billCode, "^[A-Za-z]+$") != 1)
                {
                    MessageBoxEx.Show("请录入1-7个字母的项目编码！");
                    return false;
                }
                string ruleID = row.Cells["bill_code_rule_id"].EditedFormattedValue.ToString();
                string where = string.Format("bill_code='{0}' and bill_code_rule_id !='{1}'", billCode, ruleID);
                string tmpStr = DBHelper.GetSingleValue("获取项目编码", "sys_bill_code_rule", "bill_code", where, "");
                if (!string.IsNullOrEmpty(tmpStr))
                {
                    MessageBoxEx.Show("项目编码已存在！");
                    dgvBillCodeRule.CurrentCell = row.Cells["bill_code"];
                    return false;
                }
            }
            string startNum = row.Cells["start_num"].EditedFormattedValue.ToString();
            if (string.IsNullOrEmpty(startNum))
            {
                MessageBoxEx.Show("起始编号不能为空！");
                dgvBillCodeRule.CurrentCell = row.Cells["start_num"];
                return false;
            }
            else
            {
                if (startNum.Length < 3 || startNum.Length > 6 || Common.ValidateStr(startNum, @"^[0-9]\d*$") != 1)
                {
                    MessageBoxEx.Show("请录入3-6位数值的起始编号！");
                    return false;
                }
            }
            return true;
        }

        /// <summary> 编辑项目编码生成规则
        /// </summary>
        private bool SaveRecord()
        {
            dgvBillCodeRule.EndEdit();
            string keyName = string.Empty;
            string keyValue = string.Empty;
            string opName = "更新项目编码生成规则";
            DataGridViewRow row = dgvBillCodeRule.Rows[editRowIndex];
            Dictionary<string, string> dicFileds = new Dictionary<string, string>();
            string nowTicks = Common.LocalDateTimeToUtcLong(GlobalStaticObj.CurrentDateTime).ToString();
            dicFileds.Add("bill_code", row.Cells["bill_code"].Value.ToString().ToUpper());
            object objCodeMethod = row.Cells["code_method"].Value;
            string strCodeMethod = objCodeMethod == null ? "" : objCodeMethod.ToString();
            dicFileds.Add("code_method", strCodeMethod);
            object objDelimiter = row.Cells["delimiter"].Value;
            string strDelimiter = objDelimiter == null ? "" : objDelimiter.ToString();
            dicFileds.Add("delimiter", strDelimiter);
            dicFileds.Add("start_num", row.Cells["start_num"].Value.ToString());
            dicFileds.Add("example", row.Cells["example"].Value.ToString());
            dicFileds.Add("last_bill_no", "");
            dicFileds.Add("update_time", nowTicks);
            dicFileds.Add("update_by", GlobalStaticObj.UserID);
            keyName = "bill_code_rule_id";
            keyValue = row.Cells["bill_code_rule_id"].Value.ToString();
            bool flag = DBHelper.Submit_AddOrEdit(opName, "sys_bill_code_rule", keyName, keyValue, dicFileds);
            //string str = CommonUtility.GetNewNo (DataSources.EnumProjectType.Supplier);
            return flag;
        }

        /// <summary> 设置编辑行编辑数据的背景色
        /// </summary>
        /// <param name="row">编辑行</param>
        /// <param name="fields">编辑字段</param>
        private void SetDgvEditCellBgColor(DataGridViewRow row, string[] fields, bool isEdit)
        {
            DataGridViewCellStyle cell = new DataGridViewCellStyle();
            if (isEdit)
            {
                cell.BackColor = Color.FromArgb(20, 129, 194);
                cell.SelectionBackColor = Color.FromArgb(20, 129, 194);
            }
            else
            {
                cell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
                cell.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(138)))));
            }

            foreach (string str in fields)
            {
                row.Cells[str].Style = cell;
            }
            row.DataGridView.CurrentCell = row.Cells[fields[0]];
        }
        #endregion
    }
}