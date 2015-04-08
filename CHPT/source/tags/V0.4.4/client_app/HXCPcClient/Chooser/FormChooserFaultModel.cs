using System;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.Chooser
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     FormFaultModel         
    /// Author:       Kord
    /// Date:         2014/11/26 9:41:56
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	FormFaultModel
    ///***************************************************************************//
    public partial class FormChooserFaultModel : FormChooser
    {
        #region Constructor -- 构造函数
        public FormChooserFaultModel()
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
        public String FmeaCode { get; set; }
        public String FmeaName { get; set; }
        public String FmeaIndexCode { get; set; }
        #endregion

        #region Method -- 方法
        private void Init() //初始化数据
        {
            #region 按钮事件
            btnSubmit.Click += (sender, args) => BindPageData();
            btnClear.Click += delegate
            {
                txt_fmea__name.Caption = String.Empty;
                txt_fmea_code.Caption = String.Empty;
            };
            dgvCompany.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs args)
            {
                if (args.RowIndex >= 0)
                {
                    FmeaCode = CommonCtrl.IsNullToString(dgvCompany.Rows[args.RowIndex].Cells[drtxt_fmea_code.Name].Value);
                    FmeaName = CommonCtrl.IsNullToString(dgvCompany.Rows[args.RowIndex].Cells[drtxt_fmea_name.Name].Value);
                    FmeaIndexCode = CommonCtrl.IsNullToString(dgvCompany.Rows[args.RowIndex].Cells[drtxt_fmea_index_code.Name].Value);
                    DialogResult = DialogResult.OK;
                }
            };
            #endregion
        }
        private String BuildString()
        {
            var where = String.Format(" '1'='1' ");

            if (!String.IsNullOrEmpty(txt_fmea__name.Caption.Trim()))    //故障模式名称
            {
                where += String.Format(" and fmea_name like '%{0}%'", txt_fmea__name.Caption.Trim());
            }
            if (!String.IsNullOrEmpty(txt_fmea_code.Caption.Trim()))   //故障模式代码
            {
                where += String.Format(" and  fmea_code like '%{0}%'", txt_fmea_code.Caption.Trim());
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
                var dt = DBHelper.GetTableByPage("分页查询故障模式信息", "tb_fault_model", "*", where, "", " order by fmea_code ", winFormPager1.PageIndex, winFormPager1.PageSize, out recordCount);
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
