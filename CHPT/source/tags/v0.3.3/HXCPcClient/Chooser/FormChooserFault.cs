using System;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using HXCPcClient.UCForm;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.Chooser
{
    ///*************************************************************************//
    /// System:       HuiXuCheSYS
    /// FileName:     FormChooserFault         
    /// Author:       Kord
    /// Date:         2014/11/7 10:57:52
    /// Machine Name: KORD
    ///***************************************************************************//
    /// Function: 
    /// 	FormChooserFault
    ///***************************************************************************//
    public partial class FormChooserFault : FormChooser
    {

        #region Constructor -- 构造函数
        public FormChooserFault()
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
        public String SystemCode { get; set; }
        public String SystemName { get; set; }
        public String AssemblyCode { get; set; }
        public String AssemblyName { get; set; }
        public String PartCode { get; set; }
        public String PartmName { get; set; }
        public String SchemaCode { get; set; }
        public String SchemaName { get; set; }
        #endregion

        #region Method -- 方法
        private void Init() //初始化数据
        {
            #region 按钮事件
            btnSubmit.Click += (sender, args) => BindPageData();
            btnClear.Click += delegate
            {
                txt_system_name.Caption = String.Empty;
                txt_assembly_name.Caption = String.Empty;
            };
            dgvCompany.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs args)
            {
                if (args.RowIndex >= 0)
                {
                    SystemName = CommonCtrl.IsNullToString(dgvCompany.Rows[args.RowIndex].Cells[drtxt_system_name.Name].Value);
                    SystemCode = CommonCtrl.IsNullToString(dgvCompany.Rows[args.RowIndex].Cells[drtxt_system_code.Name].Value);
                    AssemblyName = CommonCtrl.IsNullToString(dgvCompany.Rows[args.RowIndex].Cells[drtxt_assembly_name.Name].Value);
                    AssemblyCode = CommonCtrl.IsNullToString(dgvCompany.Rows[args.RowIndex].Cells[drtxt_assembly_code.Name].Value);
                    PartCode = CommonCtrl.IsNullToString(dgvCompany.Rows[args.RowIndex].Cells[drtxt_part_code.Name].Value);
                    PartmName = CommonCtrl.IsNullToString(dgvCompany.Rows[args.RowIndex].Cells[drtxt_part_name.Name].Value);
                    DialogResult = DialogResult.OK;
                }
            };
            #endregion
        }
        private String BuildString()
        {
            var where = String.Format(" '1'='1' ");//enable_flag 1未删除

            if (!String.IsNullOrEmpty(txt_system_name.Caption.Trim()))    //故障系统名称
            {
                where += String.Format(" and system_name like '%{0}%'", txt_system_name.Caption.Trim());
            }
            if (!String.IsNullOrEmpty(txt_assembly_name.Caption.Trim()))   //故障总成名称
            {
                where += String.Format(" and  assembly_name like '%{0}%'", txt_assembly_name.Caption.Trim());
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
                var dt = DBHelper.GetTableByPage("分页查询故障信息", "v_fault", "*", where, "", " order by system_code ", winFormPager1.PageIndex, winFormPager1.PageSize, out recordCount);
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


