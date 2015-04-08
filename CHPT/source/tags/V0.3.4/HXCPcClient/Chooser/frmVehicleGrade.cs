using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStationClient.ComponentUI;
using HXCPcClient.CommonClass;
using SYSModel;
using HXCPcClient.UCForm;
using HXCPcClient.UCForm.DataManage.VehicleFiles;

namespace HXCPcClient.Chooser
{
    /// <summary>
    /// 车辆档案选择器
    /// Author：JC
    /// AddTime：2014.9.29
    /// </summary>
    public partial class frmVehicleGrade : FormEx
    {
        #region 外部属性设置
        /// <summary>
        /// 车牌号
        /// </summary>
        public string strLicensePlate = string.Empty;
        /// <summary>
        /// 车辆VIN
        /// </summary>
        public string strVIN = string.Empty;
        /// <summary>
        /// 发动机号
        /// </summary>
        public string strEngineNum = string.Empty;
        /// <summary>
        /// 车辆品牌
        /// </summary>
        public string strBrand = string.Empty;
        /// <summary>
        /// 车辆颜色
        /// </summary>
        public string strColor = string.Empty;
        /// <summary>
        /// 车型
        /// </summary>
        public string strModel = string.Empty;
        /// <summary>
        /// 车工号
        /// </summary>
        public String Turner { get; set; }
        /// <summary>
        /// 出厂日期
        /// </summary>
        public String ProductionTime { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public String ProducingArea { get; set; }
        /// <summary>
        /// 协议保养里程
        /// </summary>
        public String MaintainMileage { get; set; }
        /// <summary>
        /// 协议保养日期
        /// </summary>
        public String MaintainTime { get; set; }
        /// <summary>
        /// 所属客户ID
        /// </summary>
        public string strCustId = string.Empty;
        /// <summary>
        /// 所属客户名称
        /// </summary>
        public string strCustName = string.Empty;
        /// <summary>
        /// 所属客户编码
        /// </summary>
        public string strCustCode = string.Empty;
        #endregion
        public frmVehicleGrade()
        {
            InitializeComponent();
            SetDgvAnchor();
            UIAssistants.SetButtonStyle4QueryAndClear(btnSearch, btnClear);  //设置查询按钮和清除按钮样式

            pageQ.PageIndexChanged += delegate
            {
                string where = "a.enable_flag='1'";
                if (!string.IsNullOrEmpty(txtLicensePlate.Caption.Trim()))
                {
                    where += string.Format(" and  license_plate like '%{0}%'", txtLicensePlate.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtVIN.Caption.Trim()))
                {
                    where += string.Format(" and  vin like '%{0}%'", txtVIN.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtEngineNum.Caption.Trim()))
                {
                    where += string.Format(" and  engine_num like '%{0}%'", txtEngineNum.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtCCode.Caption.Trim()))
                {
                    where += string.Format(" and  b.cust_code like '%{0}%'", txtCCode.Caption.Trim());
                }
                if (!string.IsNullOrEmpty(txtCName.Caption.Trim()))
                {
                    where += string.Format(" and b.cust_namelike '%{0}%'", txtCName.Caption.Trim());
                }
                BindPageData(String.IsNullOrEmpty(where) ? "a.enable_flag = '1'" : where);
            };
        }

        #region datagridview属性设置
        /// <summary>
        /// datagridview属性设置
        /// </summary>
        private void SetDgvAnchor()
        {
            dgvVehicle.ReadOnly = false;
            dgvVehicle.Columns["license_plate"].ReadOnly = true;
            dgvVehicle.Columns["vin"].ReadOnly = true;
            dgvVehicle.Columns["cust_code"].ReadOnly = true;
            dgvVehicle.Columns["cust_name"].ReadOnly = true;
            dgvVehicle.Columns["engine_num"].ReadOnly = true;
            dgvVehicle.Columns["v_model"].ReadOnly = true;
            dgvVehicle.Columns["v_color"].ReadOnly = true;
            dgvVehicle.Columns["v_brand"].ReadOnly = true;
            dgvVehicle.Columns["turner"].ReadOnly = true;
            dgvVehicle.Columns["remark"].ReadOnly = true;
        }
        #endregion

        #region 清空事件
        /// <summary>
        /// 清空事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEngineNum.Caption = string.Empty;
            txtLicensePlate.Caption = string.Empty;
            txtVIN.Caption = string.Empty;
            txtEngineNum.Focus();
        }
        #endregion 

        #region 查询事件
        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string where = "a.enable_flag='" + Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString() + "'";
            if (!string.IsNullOrEmpty(txtLicensePlate.Caption.Trim()))
            {
                where += string.Format(" and  a.license_plate like '%{0}%'", txtLicensePlate.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtVIN.Caption.Trim()))
            {
                where += string.Format(" and  a.vin like '%{0}%'", txtVIN.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtEngineNum.Caption.Trim()))
            {
                where += string.Format(" and  a.engine_num like '%{0}%'", txtEngineNum.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtCCode.Caption.Trim()))
            {
                where += string.Format(" and  b.cust_code like '%{0}%'", txtCCode.Caption.Trim());
            }
            if (!string.IsNullOrEmpty(txtCName.Caption.Trim()))
            {
                where += string.Format(" and b.cust_namelike '%{0}%'", txtCName.Caption.Trim());
            }
            BindPageData(where);
        }
        #endregion

        #region 绑定车辆档案数据方法
        /// <summary> 绑定车辆档案数据
        /// </summary>
        private void BindPageData(string where)
        {
            //select a.license_plate,a.vin,a.engine_num,a.v_model,a.v_color,a.v_brand,a.turner,b.cust_name,b.cust_code,a.remark,a.v_id,a.cust_id,d.cont_name,d.cont_phone
            //from tb_vehicle a left join tb_customer b on a.cust_id=b.cust_id left join tr_driver_vehicle c on a.v_id=c.v_id left join tb_contacts d on c.driver_id=d.cont_id
            //where c.isDefault='1'
            int recordCount;
            string strFileds = @"a.license_plate,a.vin,a.engine_num,a.v_model,a.v_color,a.v_brand,a.turner,b.cust_name,b.cust_code,a.remark,a.v_id,a.cust_id,d.cont_name,d.cont_phone";
            var dt = DBHelper.GetTableByPage("选择器车辆档案列表", "tb_vehicle a left join tb_customer b on a.cust_id=b.cust_id left join tr_driver_vehicle c on a.v_id=c.v_id and isDefault='1' left join tb_contacts d on c.driver_id=d.cont_id", strFileds, where, "", "order by a.create_time desc", pageQ.PageIndex, pageQ.PageSize, out recordCount);
            dgvVehicle.DataSource = dt;
            pageQ.RecordCount = recordCount;
        }
        #endregion

        #region 窗体Load事件
        private void frmVehicleGrade_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnSearch;
            btnSearch_Click(null, null);
        }
        #endregion

        #region 双击单元赋值
        /// <summary>
        /// 双击单元格给外部属性赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.strLicensePlate = CommonCtrl.IsNullToString(dgvVehicle.Rows[e.RowIndex].Cells["license_plate"].Value);
                this.strVIN = CommonCtrl.IsNullToString(dgvVehicle.Rows[e.RowIndex].Cells["vin"].Value);
                this.strEngineNum = CommonCtrl.IsNullToString(dgvVehicle.Rows[e.RowIndex].Cells["engine_num"].Value);
                this.strBrand = CommonCtrl.IsNullToString(dgvVehicle.Rows[e.RowIndex].Cells["v_Brand"].Value);
                this.strColor = CommonCtrl.IsNullToString(dgvVehicle.Rows[e.RowIndex].Cells["v_color"].Value);
                this.strModel = CommonCtrl.IsNullToString(dgvVehicle.Rows[e.RowIndex].Cells["v_model"].Value);
                Turner = CommonCtrl.IsNullToString(dgvVehicle.Rows[e.RowIndex].Cells["turner"].Value);
                strCustId = CommonCtrl.IsNullToString(dgvVehicle.Rows[e.RowIndex].Cells["cust_id"].Value);
                strCustCode = CommonCtrl.IsNullToString(dgvVehicle.Rows[e.RowIndex].Cells["cust_code"].Value);
                strCustName = CommonCtrl.IsNullToString(dgvVehicle.Rows[e.RowIndex].Cells["cust_name"].Value);
                //MaintainMileage = dgvVehicle.Rows[e.RowIndex].Cells["warranty_mileage"].Value.ToString();
                //MaintainTime = dgvVehicle.Rows[e.RowIndex].Cells["warranty_period"].Value.ToString();
                //ProducingArea = dgvVehicle.Rows[e.RowIndex].Cells["warranty_mileage"].Value.ToString();
                //ProductionTime = dgvVehicle.Rows[e.RowIndex].Cells["warranty_period"].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
        }
        #endregion

        #region 重写车牌
        private void dgvVehicle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }
            string fieldNmae = dgvVehicle.Columns[e.ColumnIndex].DataPropertyName;
            if (fieldNmae.Equals("v_brand") || fieldNmae.Equals("v_color"))
            {
                e.Value = GetDicName(e.Value.ToString());
            }
            if (fieldNmae.Equals("v_model"))
            {
                e.Value = GetVmodel(e.Value.ToString());
            }  
        }
        #endregion

        #region 获取车型名称
        private string  GetVmodel(string strVId)
        {
            return DBHelper.GetSingleValue("获得车型名称", "tb_vehicle_models", "vm_name", "vm_id='" + strVId + "'", ""); 
        }
        #endregion

        #region 根据码表ID获取其对应的名称
        /// <summary>
        /// 根据码表ID获取其对应的名称
        /// </summary>
        /// <param name="strId">码表Id值</param>
        private string GetDicName(string strId)
        {
            return LocalCache.GetDictNameById(strId);
        }
        #endregion

        #region 关闭事件
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 确定事件
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!IsCheck())
            {
                return;
            }
            this.strLicensePlate = CommonCtrl.IsNullToString(dgvVehicle.CurrentRow.Cells["license_plate"].Value);
            this.strVIN = CommonCtrl.IsNullToString(dgvVehicle.CurrentRow.Cells["vin"].Value);
            this.strEngineNum = CommonCtrl.IsNullToString(dgvVehicle.CurrentRow.Cells["engine_num"].Value);
            this.strBrand = CommonCtrl.IsNullToString(dgvVehicle.CurrentRow.Cells["v_Brand"].Value);
            this.strColor = CommonCtrl.IsNullToString(dgvVehicle.CurrentRow.Cells["v_color"].Value);
            this.strModel = CommonCtrl.IsNullToString(dgvVehicle.CurrentRow.Cells["v_model"].Value);
            Turner = CommonCtrl.IsNullToString(dgvVehicle.CurrentRow.Cells["turner"].Value);
            strCustId = CommonCtrl.IsNullToString(dgvVehicle.CurrentRow.Cells["cust_id"].Value);
            strCustCode = CommonCtrl.IsNullToString(dgvVehicle.CurrentRow.Cells["cust_code"].Value);
            strCustName = CommonCtrl.IsNullToString(dgvVehicle.CurrentRow.Cells["cust_name"].Value);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region 添加新车辆
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!MessageBoxEx.ShowQuestion("新增车辆档案会关闭当前页面，确认要新增吗？"))
            {
                return;
            }
            if (GlobalStaticObj.AppMainForm is HXCMainForm)
            {
                UCVehicleAddOrEdit Add = new UCVehicleAddOrEdit();
                UCVehicleManage manage = new UCVehicleManage();
                Add.uc = manage;
                Add.wStatus = WindowStatus.Add;
                string tag = "CL_DataManagement_BasicData_VehicleFiles|CL_DataManagement|CL_DataManagement_BasicData";
                ((HXCMainForm)GlobalStaticObj.AppMainForm).addUserControls(Add, "车辆档案-新增", "Add", tag, "");
            }
            this.Close();
        }
        #endregion

        #region 判断是否选择了一条数据
        /// <summary>
        /// 判断是否选择了一条数据
        /// </summary>
        /// <returns></returns>
        private bool IsCheck()
        {
            bool isOk = false;
            List<string> listField = new List<string>();
            foreach (DataGridViewRow dr in dgvVehicle.Rows)
            {
                object isCheck = dr.Cells["colCheck"].EditedFormattedValue;
                if (isCheck != null && (bool)isCheck)
                {
                    listField.Add(dr.Cells["cust_id"].Value.ToString());
                }
            }
            if (listField.Count == 0)
            {
                MessageBoxEx.Show("请选择车辆档案记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count > 1)
            {
                MessageBoxEx.Show(" 一次仅能选择1条记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (listField.Count == 1)
            {
                isOk = true;
            }
            return isOk;
        }
        #endregion

        #region 选择复选框
        private void dgvVehicle_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == colCheck.Index)
            {
                return;
            }
            //清空已选择框
            foreach (DataGridViewRow dgvr in dgvVehicle.Rows)
            {
                object check = dgvr.Cells[colCheck.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[colCheck.Name].Value = false;
                }
            }
            //选择当前行
            dgvVehicle.Rows[e.RowIndex].Cells[colCheck.Name].Value = true;
        }
        #endregion         
    }
}
