using System;
using System.Data;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.Chooser
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     Form         
    /// Author:       Kord
    /// Date:         2014/10/30 10:55:34
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	Form
    ///***************************************************************************//
    public partial class FormSignInfoChooser : FormChooser
    {
        #region Constructor -- 构造函数
        public FormSignInfoChooser()
        {
            InitializeComponent();

            Init();

            BindPageData();

            Load += (sender, args) => UIAssistants.SetDataGridViewCheckColumn(dgvCompany, drchk_check);
        }
        #endregion

        #region Field -- 字段

        #endregion

        #region Property -- 属性
        /// <summary>
        /// 服务站Id
        /// </summary>
        public String SignId { get; set; }
        /// <summary>
        /// 服务站编码
        /// </summary>
        public String SignCode { get; set; }
        /// <summary>
        /// 服务站名称
        /// </summary>
        public String SignName { get; set; }
        /// <summary>
        /// 是否二级服务站
        /// </summary>
        public Boolean SecondStation { get; set; }
        #endregion

        #region Method -- 方法
        private void Init() //初始化数据
        {
            #region 按钮事件
            btnSubmit.Click += (sender, args) => BindPageData();
            btnClear.Click += delegate
            {
                txt_sign_code.Caption = String.Empty;
                txt_com_short_name.Caption = String.Empty;
                txt_com_name.Caption = String.Empty;
            };
            dgvCompany.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs args)
            {
                if (args.RowIndex >= 0)
                {
                    SignCode = dgvCompany.Rows[args.RowIndex].Cells["drtxt_sign_code"].Value.ToString();
                    SignId = dgvCompany.Rows[args.RowIndex].Cells["drtxt_sign_id"].Value.ToString();
                    SignName = dgvCompany.Rows[args.RowIndex].Cells["drtxt_com_name"].Value.ToString();
                    //SecondStation = dgvCompany.Rows[args.RowIndex].Cells["drtxt_com_name"].Value.ToString() == "1"; //二级服务站
                    DialogResult = DialogResult.OK;
                }
            };
            #endregion
        }
        private String BuildString()
        {
            var where = String.Format(" enable_flag='1' ");//enable_flag 1未删除

            if (!String.IsNullOrEmpty(txt_sign_code.Caption.Trim()))    //服务站编码
            {
                where += String.Format(" and sign_code like '%{0}%'", txt_sign_code.Caption.Trim());
            }
            if (!String.IsNullOrEmpty(txt_com_short_name.Caption.Trim()))   //服务站简称
            {
                where += String.Format(" and  com_short_name like '%{0}%'", txt_com_short_name.Caption.Trim());
            }
            if (!String.IsNullOrEmpty(txt_com_name.Caption.Trim())) //服务站全称
            {
                where += String.Format(" and  com_name like '%{0}%'", txt_com_name.Caption.Trim());
            }
            return where;
        }
        /// <summary>
        /// 查询绑定数据
        /// </summary>
        public void BindPageData()
        {
            try
            {
                Int32 recordCount;
                var where = BuildString();
                var dt = DBHelper.GetTableByPage("分页查询服务站", "tb_signing_info", "*", where, "", " order by sign_id ", winFormPager1.PageIndex, winFormPager1.PageSize, out recordCount);
                dgvCompany.DataSource = dt;
                winFormPager1.RecordCount = recordCount;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Event -- 事件

        #endregion
    }
}


