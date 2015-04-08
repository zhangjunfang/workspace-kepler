using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using Newtonsoft.Json;
using HXCPcClient.CommonClass;
using Utility.Common;
using HXCPcClient.Chooser;
using SYSModel;
using Model;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.DataManage.Dic
{
    public partial class UCDictionaryManager : UCBase
    {
        public UCDictionaryManager()
        {
            InitializeComponent();
        }

        private void UCDictionaryManager_Load(object sender, EventArgs e)
        {
            base.RoleButtonStstus(this.Name);
            DataGridViewEx.SetDataGridViewStyle(dgvDicList);
            base.btnCopy.Visible = false;
            base.btnStatus.Visible = false;
            base.btnAdd.Visible = false;
            base.btnEdit.Visible = false;
            base.btnSave.Visible = false;
            base.btnCancel.Visible = false;
            base.btnDelete.Visible = false;
            base.SetBtnStatus(WindowStatus.View);
            dtpStart.Value = "";
            dtpEnd.Value = "";
            btnSearch_Click(null, null);
        }

        private string where = string.Empty;

        #region 事件
        /// <summary> 选择码表
        /// </summary>
        private void txtparent_id_ChooserClick(object sender, EventArgs e)
        {
            frmDictionaries frm = new frmDictionaries();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtparent_id.Text = frm.Dic_Name;
                txtparent_id.Tag = frm.Dic_ID;
            }
        }

        private void txtcreate_by_ChooserClick(object sender, EventArgs e)
        {
            frmUsers frm = new frmUsers();
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtcreate_by.Text = frm.User_Name;
                txtcreate_by.Tag = frm.User_ID;
            }
        }

        /// <summary> 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            where = string.Format("is_class='1' and enable_flag='1'");
            if (!string.IsNullOrEmpty(txtdic_code.Caption.Trim()))
            {
                where += string.Format(" and  dic_code like '%{0}%'", txtdic_code.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtdic_name.Caption.Trim()))
            {
                where += string.Format(" and  dic_name like '%{0}%'", txtdic_name.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtparent_id.Text))
            {
                where += string.Format(" and  (dic_id = '{0}' or parent_id='{0}')", txtparent_id.Tag.ToString());
            }
            if (!string.IsNullOrEmpty(txtcreate_by.Text))
            {
                where += string.Format(" and  create_by = '{0}'", txtcreate_by.Tag.ToString());
            }
            if (!string.IsNullOrEmpty(dtpStart.Value))
            {
                long startTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpStart.Value));
                where += " and create_time>=" + startTicks.ToString();
            }
            if (!string.IsNullOrEmpty(dtpEnd.Value))
            {
                long endTicks = Common.LocalDateTimeToUtcLong(Convert.ToDateTime(dtpEnd.Value).AddDays(1));
                where += " and create_time<" + endTicks.ToString();
            }
            page.PageIndex = 1;
            BindPageData(where);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //partsale pSale = new partsale();
            //pSale.amount = "120";
            //pSale.cust_name = "add";
            //pSale.cust_phone = "123123";
            //pSale.license_plate = "豫G99300";
            //pSale.remark = "dfgdfsdf";
            //pSale.sale_date = "2014-10-10  14:46:44";
            //pSale.turner = "14H197P-0013";
            //partDetail[] pArry = new partDetail[1];
            //partDetail p0 = new partDetail();
            //p0.amount = "12";
            //p0.business_count = "1";
            //p0.business_price = "12";
            //p0.car_parts_code = "2100-00233";
            //p0.parts_remark = "asdfasdf";
            //p0.remark = "asdfasf";
            //p0.wh_code = "CK00000202";
            //pArry[0] = p0;
            //pSale.partDetails = pArry;
            //DBHelper.WebServHandler("配件销售开单", EnumWebServFunName.UpLoadPartSale, pSale);
            //return;
            txtdic_code.Caption = string.Empty;
            txtdic_name.Caption = string.Empty;
            txtparent_id.Text = string.Empty;
            txtparent_id.Tag = null;
            txtcreate_by.Text = string.Empty;
            txtcreate_by.Tag = null;
            dtpStart.Value = "";
            dtpEnd.Value = "";
        }

        /// <summary> 页码改变事件
        /// </summary>
        private void page_PageIndexChanged(object sender, EventArgs e)
        {
            BindPageData(where);
        }

        /// <summary> 双击新增/复制/编辑字典内容
        /// </summary>
        private void dgvDicList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string dic_id = dgvDicList.Rows[e.RowIndex].Cells["dic_id"].Value.ToString();
                string dic_code = dgvDicList.Rows[e.RowIndex].Cells["dic_code"].Value.ToString();
                string dic_name = dgvDicList.Rows[e.RowIndex].Cells["dic_name"].Value.ToString();
                UCDictionaryAddOrEdit uc = new UCDictionaryAddOrEdit();
                uc.ParentId = dic_id;
                uc.ParentCode = dic_code;
                uc.ParentName = dic_name;                
                base.addUserControl(uc, "字典码表-"+dic_name, "UCDictionaryManagerOperator" + dic_code, this.Tag.ToString(), this.Name);

            }
        }

        /// <summary> 格式化
        /// </summary>
        private void dgvDicList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                return;
            }
            string fieldNmae = dgvDicList.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("create_time") || fieldNmae.Equals("update_time"))
            {
                long ticks = (long)e.Value;
                e.Value = Common.UtcLongToLocalDateTime(ticks);
            }
            if (fieldNmae.Equals("enable_flag"))
            {
                DataSources.EnumEnableFlag enumEnableFlag = (DataSources.EnumEnableFlag)Convert.ToInt16(e.Value.ToString());
                e.Value = DataSources.GetDescription(enumEnableFlag, true);
            }
            if (fieldNmae.Equals("data_source"))
            {
                DataSources.EnumDataSources enumDataSources = (DataSources.EnumDataSources)Convert.ToInt16(e.Value.ToString());
                e.Value = DataSources.GetDescription(enumDataSources, true);
            }
        }
        #endregion

        #region 方法
        /// <summary> 绑定数据
        /// </summary>
        private void BindPageData(string where)
        {
            int recordCount;
            DataTable dt = DBHelper.GetTableByPage("分页查询码表", "v_dictionaries", "*", where, "", "order by dic_code", page.PageIndex, page.PageSize, out recordCount);
            dgvDicList.DataSource = dt;
            page.RecordCount = recordCount;
            page.SetBtnState();
        }
        #endregion
    }
}
