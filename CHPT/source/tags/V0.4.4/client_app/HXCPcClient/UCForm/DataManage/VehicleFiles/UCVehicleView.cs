using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;
using ServiceStationClient.ComponentUI;

namespace HXCPcClient.UCForm.DataManage.VehicleFiles
{
    /// <summary>
    /// 数据管理-车辆档案详情
    /// Author：JC
    /// AddTime：2014.12.04
    /// </summary>
    public partial class UCVehicleView : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 单据Id值
        /// </summary>
        string strVId = string.Empty;
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCVehicleManage uc;
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        #endregion

        #region 初始化窗体
        public UCVehicleView(string strId)
        {
            InitializeComponent();
            strVId = strId;
            SetTopbuttonShow();
            base.AddEvent += new ClickHandler(UCVehicleView_AddEvent);
            base.EditEvent += new ClickHandler(UCVehicleView_EditEvent);
            base.CopyEvent += new ClickHandler(UCVehicleView_CopyEvent);
            base.DeleteEvent += new ClickHandler(UCVehicleView_DeleteEvent);
            base.StatusEvent += new ClickHandler(UCVehicleView_StatusEvent);
        }
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnSave.Visible = false;
            base.btnSubmit.Visible = false;
            base.btnImport.Visible = false;
            base.btnVerify.Visible = false;
            base.btnCancel.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
        }
        #endregion

        #region 启停用事件
        void UCVehicleView_StatusEvent(object sender, EventArgs e)
        {
            List<SQLObj> listSql = new List<SQLObj>();
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            string strMsg = string.Empty;
            if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Start).ToString())
            {
                strMsg = "停用";
                dicParam.Add("status", new ParamObj("status", DataSources.EnumStatus.Stop, SysDbType.VarChar, 40));//状态
            }
            else if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Stop).ToString())
            {
                strMsg = "启用";
                dicParam.Add("status", new ParamObj("status", DataSources.EnumStatus.Start, SysDbType.VarChar, 40));//状态
            }
            if (MessageBoxEx.Show("确认要" + strMsg + "吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            dicParam.Add("v_id", new ParamObj("v_id", strVId, SysDbType.VarChar, 40));//车辆档案Id 
            dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id          
            dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
            obj.sqlString = "update tb_vehicle set status=@status,update_by=@update_by,update_time=@update_time where v_id=@v_id";
            obj.Param = dicParam;
            listSql.Add(obj);
            if (DBHelper.BatchExeSQLMultiByTrans("更新单据状态为启停用", listSql))
            {
                MessageBoxEx.Show("" + strMsg + "成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
            }
        }
        #endregion

        #region 删除事件
        void UCVehicleView_DeleteEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要删除吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            List<SQLObj> listSql = new List<SQLObj>();
            List<string> listField = new List<string>();
            listField.Add(strVId);
            Dictionary<string, string> comField = new Dictionary<string, string>();
            comField.Add("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.DELETED).ToString());
            comField.Add("update_by", HXCPcClient.GlobalStaticObj.UserID);//修改人Id
            comField.Add("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString());//修改时间  
            bool flag = DBHelper.BatchUpdateDataByIn("删除车辆档案", "tb_vehicle", comField, "v_id", listField.ToArray());
            if (flag)
            {
                MessageBoxEx.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uc.BindPageData();
                deleteMenuByTag(this.Tag.ToString(), "UCVehicleView");
            }
            else
            {
                MessageBoxEx.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 复制事件
        void UCVehicleView_CopyEvent(object sender, EventArgs e)
        {
            UCVehicleAddOrEdit Copy = new UCVehicleAddOrEdit();
            UCVehicleManage manage = new UCVehicleManage();
            Copy.uc = uc;
            Copy.wStatus = WindowStatus.Copy;
            Copy.strId = strVId;
            base.addUserControl(Copy, "领料单-复制", "UCVehicleAddOrEdit" + Copy.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), "UCVehicleView");
        }
        #endregion

        #region 编辑事件
        void UCVehicleView_EditEvent(object sender, EventArgs e)
        {
            UCVehicleAddOrEdit Edit = new UCVehicleAddOrEdit();
            Edit.uc = uc;
            Edit.wStatus = WindowStatus.Edit;
            Edit.strId = strVId;
            base.addUserControl(Edit, "车辆档案-编辑", "UCVehicleAddOrEdit" + Edit.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), "UCVehicleView");
        }
        #endregion

        #region 新增事件
        void UCVehicleView_AddEvent(object sender, EventArgs e)
        {
            UCVehicleAddOrEdit Add = new UCVehicleAddOrEdit();
            Add.wStatus = WindowStatus.Add;
            base.addUserControl(Add, "车辆档案-新增", "UCVehicleAddOrEdit" + Add.strId, this.Tag.ToString(), this.Name);
            deleteMenuByTag(this.Tag.ToString(), "UCVehicleView");
        }
        #endregion

        #region 窗体Load事件
        private void UCVehicleView_Load(object sender, EventArgs e)
        {
            BindData();
            SetDgvAnchor();
            ucAttr.ReadOnly = true;
        }
        #endregion

        #region 设置底部DataGridView的Anchor属性
        /// <summary>
        /// 设置底部DataGridView的Anchor属性
        /// </summary>
        private void SetDgvAnchor()
        {
            dgvUser.Dock = DockStyle.Fill;
        }
        #endregion

        #region 根据车辆档案ID获取信息，复制和编辑用
        /// <summary>
        /// 根据车辆档案ID获取信息，复制和编辑用
        /// </summary>
        private void BindData()
        {
            #region 基础信息
            string strWhere = string.Format("a.v_id='{0}'", strVId);
            string strTable = "tb_vehicle a left join attachment_info b on b.relation_object='tb_vehicle' and is_main='1' and a.v_id = b.relation_object_id left join tb_vehicle_models c on a.v_model=c.vm_id";
            DataTable dt = DBHelper.GetTable("查询车辆档案", strTable, "a.*,b.att_path,c.vm_name", strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            labCarNOS.Text = CommonCtrl.IsNullToString(dr["license_plate"]);//车牌号
            labCarBrandS.Text = GetDicName(CommonCtrl.IsNullToString(dr["v_brand"]));//车辆品牌;
            labFactoryS.Text = GetDicName(CommonCtrl.IsNullToString(dr["v_factory"]));//生产厂家
            labVINS.Text = CommonCtrl.IsNullToString(dr["vin"]);//VIN
            labYardCodeS.Text = CommonCtrl.IsNullToString(dr["turner"]);//车场编号
            labColorS.Text = GetDicName(CommonCtrl.IsNullToString(dr["v_color"]));//颜色 
            labCarTypeS.Text = CommonCtrl.IsNullToString(dr["vm_name"]);//车型 
            labRatingS.Text = GetDicName(CommonCtrl.IsNullToString(dr["v_class"]));//车辆等级           
            labCustomNOS.Text = GetCustName(CommonCtrl.IsNullToString(dr["cust_id"]));//所属客户
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["carbuy_date"])))
            {
                long Rticks = Convert.ToInt64(CommonCtrl.IsNullToString(dr["carbuy_date"]));
                labTimeS.Text = Common.UtcLongToLocalDateTime(Rticks).ToString();//购车日期     
            }
            labRemarkS.Text = CommonCtrl.IsNullToString(dr["remark"]);//备注  

            strStatus = CommonCtrl.IsNullToString(dr["status"]);//状态
            if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Start).ToString())
            {
                base.btnStatus.Caption = "停用";
            }
            else if (strStatus == Convert.ToInt32(DataSources.EnumStatus.Stop).ToString())
            {
                base.btnStatus.Caption = "启用";
            }
            #endregion

            #region 车辆信息
            txtv_long.Caption = CommonCtrl.IsNullToString(dr["v_long"]);//车长
            txtv_wide.Caption = CommonCtrl.IsNullToString(dr["v_wide"]);//车宽
            txtv_high.Caption = CommonCtrl.IsNullToString(dr["v_high"]);//车高
            txtaxle_count.Caption = CommonCtrl.IsNullToString(dr["axle_count"]);//轴数
            txtaxle_distance.Caption = CommonCtrl.IsNullToString(dr["axle_distance"]);//轴距
            txtwheel_distance.Caption = CommonCtrl.IsNullToString(dr["wheel_distance"]);//轮距
            txttotal_quality.Caption = CommonCtrl.IsNullToString(dr["total_quality"]);//总质量
            txtapproved_quality.Caption = CommonCtrl.IsNullToString(dr["approved_quality"]);//核定质量
            txtengine_factory.Caption = CommonCtrl.IsNullToString(dr["engine_factory"]);//发动机厂家
            txtengine_model.Caption = CommonCtrl.IsNullToString(dr["engine_model"]);//发动机型号
            txtengine_num.Caption = CommonCtrl.IsNullToString(dr["engine_num"]);//发动机号
            txtdischarge.Caption = CommonCtrl.IsNullToString(dr["discharge"]);//排量
            txtfuel_type.Caption = CommonCtrl.IsNullToString(dr["fuel_type"]);//燃油类型
            txtair_factory.Caption = CommonCtrl.IsNullToString(dr["air_factory"]);//空调厂家
            txtair_model.Caption = CommonCtrl.IsNullToString(dr["air_model"]);//空调型号
            txtair_num.Caption = CommonCtrl.IsNullToString(dr["air_num"]);//空调编码
            txtaxle_factory.Caption = CommonCtrl.IsNullToString(dr["axle_factory"]);//车桥厂家
            txtaxle_model.Caption = CommonCtrl.IsNullToString(dr["axle_model"]);//车桥型号
            txtaxle_num.Caption = CommonCtrl.IsNullToString(dr["axle_num"]);//车桥编号
            txtlowdisc_factory.Caption = CommonCtrl.IsNullToString(dr["lowdisc_factory"]);//底盘厂家
            txtwheel_count.Caption = CommonCtrl.IsNullToString(dr["wheel_count"]);//轮胎数
            txtwheel_factory.Caption = CommonCtrl.IsNullToString(dr["wheel_factory"]);//轮胎厂家
            txtwheel_model.Caption = CommonCtrl.IsNullToString(dr["wheel_model"]);//轮胎型号
            txtshift_factory.Caption = CommonCtrl.IsNullToString(dr["shift_factory"]);//变速箱厂家
            txtshift_model.Caption = CommonCtrl.IsNullToString(dr["shift_model"]);//变速箱型号
            txtshift_num.Caption = CommonCtrl.IsNullToString(dr["shift_num"]);//变速箱号
            txtshift_type.Caption = CommonCtrl.IsNullToString(dr["shift_type"]);//变速箱类型
            txtgps_factory.Caption = CommonCtrl.IsNullToString(dr["gps_factory"]);//GPS厂家
            txtgps_model.Caption = CommonCtrl.IsNullToString(dr["gps_model"]);//GPS型号
            txtgps_num.Caption = CommonCtrl.IsNullToString(dr["gps_num"]);//GPS编号
            txtdvr_factory.Caption = CommonCtrl.IsNullToString(dr["dvr_factory"]);//DVR厂家
            txtdvr_model.Caption = CommonCtrl.IsNullToString(dr["dvr_model"]);//DVR型号
            txtdvr_num.Caption = CommonCtrl.IsNullToString(dr["dvr_num"]);//DVR编号
            string photoPath = CommonCtrl.IsNullToString(dr["att_path"]);
            picPhoto.BackgroundImage = FileOperation.DownLoadImage(photoPath);//图片信息
            picPhoto.ImageLocation = photoPath;

            string strSource = CommonCtrl.IsNullToString(dr["data_source"]);
            if (strSource== Convert.ToInt32(DataSources.EnumDataSources.YUTONG).ToString())
            {
                btnEdit.Enabled = false;
                tsmiEdit.Enabled = false;
                btnDelete.Enabled = false;
                tsmiDelete.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
                tsmiEdit.Enabled = true;
                btnDelete.Enabled = true;
                tsmiDelete.Enabled = true;
            }
            #endregion

            #region 底部datagridview数据

            #region 驾驶员信息
            //驾驶员信息                
            DataTable dpt = DBHelper.GetTable("驾驶员信息", "tr_driver_vehicle", "*", string.Format(" v_id='{0}'", strVId), "", "");
            if (dpt.Rows.Count > 0)
            {
                string strConId = string.Empty;
                for (int i = 0; i < dpt.Rows.Count; i++)
                {
                    DataRow dur = dpt.Rows[i];
                    strConId += "'" + CommonCtrl.IsNullToString(dur["driver_id"]) + "'" + ",";

                }
                strConId = strConId.Substring(0, strConId.Length - 1);
                if (!string.IsNullOrEmpty(strConId))
                {
                    DataTable dut = DBHelper.GetTable("联系人信息", "tb_contacts", string.Format("*,{0} phone, {1} tel", EncryptByDB.GetDesFieldValue("cont_phone"), EncryptByDB.GetDesFieldValue("cont_tel")), "cont_id in (" + strConId + ")", "", "order by cont_id desc");
                    if (dut.Rows.Count > dgvUser.Rows.Count)
                    {
                        dgvUser.Rows.Add(dut.Rows.Count - dgvUser.Rows.Count + 1);
                    }
                    for (int j = 0; j < dut.Rows.Count; j++)
                    {
                        DataRow dpr = dut.Rows[j];
                        dgvUser.Rows[j].Cells["cont_name"].Value = CommonCtrl.IsNullToString(dpr["cont_name"]);
                        dgvUser.Rows[j].Cells["cont_post"].Value = GetDicName(CommonCtrl.IsNullToString(dpr["cont_post"]));
                        dgvUser.Rows[j].Cells["cont_phone"].Value = CommonCtrl.IsNullToString(dpr["phone"]);
                        dgvUser.Rows[j].Cells["cont_tel"].Value = CommonCtrl.IsNullToString(dpr["tel"]);
                        dgvUser.Rows[j].Cells["cont_email"].Value = CommonCtrl.IsNullToString(dpr["cont_email"]);
                        dgvUser.Rows[j].Cells["cont_birthday"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["cont_birthday"])) ? Common.UtcLongToLocalDateTime(Convert.ToInt64(CommonCtrl.IsNullToString(dpr["cont_birthday"]))).ToString() : "";
                        dgvUser.Rows[j].Cells["remark"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                        dgvUser.Rows[j].Cells["cont_id"].Value = CommonCtrl.IsNullToString(dpr["cont_id"]);
                        DataRow dur = dpt.Rows[j];
                        dgvUser.Rows[j].Cells["dir_v_id"].Value = CommonCtrl.IsNullToString(dur["dir_v_id"]);
                        string strDefault = CommonCtrl.IsNullToString(dur["isDefault"]);
                        if (!string.IsNullOrEmpty(strDefault))
                        {
                            dgvUser.Rows[j].Cells[isDefault.Name].Value = strDefault == "1" ? true : false;
                        }
                    }
                }
            }
            #endregion
            //附件信息数据
            ucAttr.TableName = "tb_vehicle";
            ucAttr.TableNameKeyValue = strVId;
            ucAttr.BindAttachment();

            #endregion
        }
        #endregion

        #region 获取客户名称
        private string GetCustName(string strCId)
        {
            return DBHelper.GetSingleValue("获取联系人名称", "tb_customer", "cust_name", "cust_id='" + strCId + "'", " order by cust_id desc");
        }
        #endregion

        #region 根据码表ID获取其对应的名称
        /// <summary>
        /// 根据码表ID获取其对应的名称
        /// </summary>
        /// <param name="strId">码表Id值</param>
        private string GetDicName(string strId)
        {
            return DBHelper.GetSingleValue("获取码表值", "sys_dictionaries", "dic_name", "dic_id='" + strId + "'", "");
        }
        #endregion

        #region 单击图片放大
        private void picPhoto_Click(object sender, EventArgs e)
        {
            string strIUrl = CommonCtrl.IsNullToString(picPhoto.ImageLocation);
            if (!string.IsNullOrEmpty(strIUrl))
            {
                Image img = FileOperation.DownLoadImage(strIUrl);//图片信息               
                if (img != null)
                {
                    BigImage bigimage = new BigImage(img);
                    bigimage.ShowDialog();
                }
            }
        }
        #endregion

      
    }
}
