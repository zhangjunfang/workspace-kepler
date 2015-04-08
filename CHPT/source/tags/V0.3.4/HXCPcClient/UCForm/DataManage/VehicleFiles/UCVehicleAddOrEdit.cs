using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYSModel;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using ServiceStationClient.ComponentUI;
using Utility.Common;


namespace HXCPcClient.UCForm.DataManage.VehicleFiles
{
    /// <summary>
    /// 数据管理-车辆档案新增编辑
    /// Author：JC
    /// AddTime：2014.12.03
    /// </summary>
    public partial class UCVehicleAddOrEdit : UCBase
    {
        #region 属性设置
        /// <summary>
        /// 父窗体
        /// </summary>
        public UCVehicleManage uc;
        /// <summary>
        /// 窗体状态
        /// </summary>
        public WindowStatus wStatus;
        /// <summary>
        /// 车辆档案ID
        /// </summary>
        public string strId = string.Empty;
        /// <summary>
        /// 单据状态
        /// </summary>
        string strStatus = string.Empty;
        /// <summary>
        /// 操作名称
        /// </summary>
        string opName = string.Empty;
        /// <summary>
        /// 图片附件ID
        /// </summary>
        private string photoID = string.Empty;
        /// <summary>
        /// 图片地址
        /// </summary>
        string photo = string.Empty;
        #endregion

        #region 初始化窗体
        public UCVehicleAddOrEdit()
        {
            InitializeComponent();
            dtpSTime.Value = DateTime.Now;
            CommonCtrl.BindComboBoxByDictionarr(cobCarBrand, "sys_vehicle_brand", true);//绑定车型品牌
            CommonCtrl.BindComboBoxByDictionarr(cobRating, "sys_vehicle_grade", true);//绑定车辆等级
            CommonCtrl.BindComboBoxByDictionarr(cobColor, "sys_vehicle_color", true);//绑定车辆颜色
            CommonCtrl.BindComboBoxByDictionarr(cobFactory, "sys_vehicle_factory", true);//绑定车辆生产厂家  
            SetTexDecimal();
            SetDgvAnchor();
            SetTopbuttonShow();
            base.SaveEvent += new ClickHandler(UCVehicleAddOrEdit_SaveEvent);
            base.CancelEvent += new ClickHandler(UCVehicleAddOrEdit_CancelEvent);
        }
        #endregion

        #region 设定某些输入框仅能数据数字和小数点
        /// <summary>
        /// 设定某些输入框仅能数据数字和小数点
        /// </summary>
        private void SetTexDecimal()
        {
            ControlsConfig.TextToDecimal(txtv_long);
            ControlsConfig.TextToDecimal(txtv_wide);
            ControlsConfig.TextToDecimal(txtv_high);
            ControlsConfig.TextToDecimal(txtaxle_count);
            ControlsConfig.TextToDecimal(txtaxle_distance);
            ControlsConfig.TextToDecimal(txtwheel_distance);
            ControlsConfig.TextToDecimal(txttotal_quality);
            ControlsConfig.TextToDecimal(txtapproved_quality);
            ControlsConfig.TextToDecimal(txtdischarge);
            ControlsConfig.TextToDecimal(txtwheel_count);
        }
        #endregion

        #region 取消事件
        void UCVehicleAddOrEdit_CancelEvent(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确认要取消吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }
            deleteMenuByTag(this.Tag.ToString(), uc.Name);
        }
        #endregion

        #region 保存事件
        void UCVehicleAddOrEdit_SaveEvent(object sender, EventArgs e)
        {
            try
            {
                #region 必要的判断
                if (string.IsNullOrEmpty(txtCarNO.Caption.Trim()))
                {
                    MessageBoxEx.Show("车牌号不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(cobCarBrand.SelectedText) && cobCarBrand.SelectedText == "全部")
                {
                    MessageBoxEx.Show("车辆品牌不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobFactory.SelectedValue)))
                {
                    MessageBoxEx.Show("生产厂家不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                if (string.IsNullOrEmpty(txtVIN.Caption.Trim()))
                {
                    MessageBoxEx.Show("VIN不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                if (!ucAttr.CheckAttachment())
                {
                    return;
                }
                #endregion
                if (MessageBoxEx.Show("确认要保存吗?", "提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(picPhoto.Tag)))
                {
                    photo = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(picPhoto.Tag.ToString());
                    if (!FileOperation.UploadFile(picPhoto.Tag.ToString(), photo))
                    {
                        return;
                    }
                }
                List<SQLObj> listSql = new List<SQLObj>();
                SaveVehicleData(listSql);
                ucAttr.TableName = "tb_vehicle";
                ucAttr.TableNameKeyValue = strId;
                listSql.AddRange(ucAttr.AttachmentSql);
                AddPhoto(listSql, strId, photo);
                deleteDriInfo(listSql,strId);
                SaveCustomData(listSql, strId);
                if (DBHelper.BatchExeSQLMultiByTrans(opName, listSql))
                {
                    MessageBoxEx.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
                    uc.BindPageData();
                    deleteMenuByTag(this.Tag.ToString(), this.uc.Name);
                }
                else
                {
                    MessageBoxEx.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("保存失败！" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region 保存车辆档案信息
        private void SaveVehicleData(List<SQLObj> listSql)
        {
            SQLObj obj = new SQLObj();
            obj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
            dicParam.Add("data_source", new ParamObj("data_source", Convert.ToInt32(DataSources.EnumDataSources.SELFBUILD).ToString(), SysDbType.VarChar, 40));//数据来源
            dicParam.Add("license_plate", new ParamObj("license_plate", txtCarNO.Caption.Trim(), SysDbType.VarChar, 30));//车牌号
            dicParam.Add("v_brand", new ParamObj("v_brand", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobCarBrand.SelectedValue)) ? cobCarBrand.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//车辆品牌           
            dicParam.Add("v_factory", new ParamObj("v_factory", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobFactory.SelectedValue)) ? cobFactory.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//生产厂家
            dicParam.Add("vin", new ParamObj("vin", txtVIN.Caption.Trim(), SysDbType.VarChar, 20));//车牌号
            dicParam.Add("car_factory_num", new ParamObj("car_factory_num", txtYardCode.Caption.Trim(), SysDbType.VarChar, 20));//车场编号
            dicParam.Add("v_color", new ParamObj("v_color", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobColor.SelectedValue)) ? cobColor.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//车辆颜色
            dicParam.Add("v_model", new ParamObj("v_model", txtCarType.Text.Trim(), SysDbType.VarChar, 40));//车型
            dicParam.Add("v_class", new ParamObj("v_class", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(cobRating.SelectedValue)) ? cobRating.SelectedValue.ToString() : null, SysDbType.VarChar, 40));//车辆等级
            dicParam.Add("cust_id", new ParamObj("cust_id", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(txtCustomNO.Tag)) ? txtCustomNO.Tag.ToString() : null, SysDbType.NVarChar, 40));//所属客户
            dicParam.Add("carbuy_date", new ParamObj("carbuy_date", !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dtpSTime.Value)) ? Common.LocalDateTimeToUtcLong(dtpSTime.Value).ToString() : null, SysDbType.BigInt));//购车日期
            dicParam.Add("remark", new ParamObj("remark", !string.IsNullOrEmpty(txtRemark.Caption.Trim()) ? txtRemark.Caption.Trim() : null, SysDbType.NVarChar, 300));//备注
            dicParam.Add("v_long", new ParamObj("v_long", !string.IsNullOrEmpty(txtv_long.Caption.Trim()) ? txtv_long.Caption.Trim() : null, SysDbType.Float));//车长
            dicParam.Add("v_wide", new ParamObj("v_wide", !string.IsNullOrEmpty(txtv_wide.Caption.Trim()) ? txtv_wide.Caption.Trim() : null, SysDbType.Float));//车宽
            dicParam.Add("v_high", new ParamObj("v_high", !string.IsNullOrEmpty(txtv_high.Caption.Trim()) ? txtv_high.Caption.Trim() : null, SysDbType.Float));//车高
            dicParam.Add("axle_count", new ParamObj("axle_count", !string.IsNullOrEmpty(txtaxle_count.Caption.Trim()) ? txtaxle_count.Caption.Trim() : null, SysDbType.Float));//轴数
            dicParam.Add("axle_distance", new ParamObj("axle_distance", !string.IsNullOrEmpty(txtaxle_distance.Caption.Trim()) ? txtaxle_distance.Caption.Trim() : null, SysDbType.Float));//轴距
            dicParam.Add("wheel_distance", new ParamObj("wheel_distance", !string.IsNullOrEmpty(txtwheel_distance.Caption.Trim()) ? txtwheel_distance.Caption.Trim() : null, SysDbType.Float));//轮距
            dicParam.Add("total_quality", new ParamObj("total_quality", !string.IsNullOrEmpty(txttotal_quality.Caption.Trim()) ? txttotal_quality.Caption.Trim() : null, SysDbType.Float));//总质量
            dicParam.Add("approved_quality", new ParamObj("approved_quality", !string.IsNullOrEmpty(txtapproved_quality.Caption.Trim()) ? txtapproved_quality.Caption.Trim() : null, SysDbType.Float));//核定质量
            dicParam.Add("engine_factory", new ParamObj("engine_factory", txtengine_factory.Caption.Trim(), SysDbType.VarChar, 40));//发动机厂家
            dicParam.Add("engine_model", new ParamObj("engine_model", txtengine_model.Caption.Trim(), SysDbType.VarChar, 40));//发动机型号
            dicParam.Add("engine_num", new ParamObj("engine_num", txtengine_num.Caption.Trim(), SysDbType.NVarChar, 30));//发动机号
            dicParam.Add("discharge", new ParamObj("discharge", !string.IsNullOrEmpty(txtdischarge.Caption.Trim()) ? txtdischarge.Caption.Trim() : null, SysDbType.Float));//排量
            dicParam.Add("fuel_type", new ParamObj("fuel_type", txtfuel_type.Caption.Trim(), SysDbType.VarChar, 40));//燃油类型
            dicParam.Add("air_factory", new ParamObj("air_factory", txtair_factory.Caption.Trim(), SysDbType.VarChar, 40));//空调厂家
            dicParam.Add("air_model", new ParamObj("air_model", txtair_model.Caption.Trim(), SysDbType.VarChar, 40));//空调型号
            dicParam.Add("air_num", new ParamObj("air_num", txtair_num.Caption.Trim(), SysDbType.NVarChar, 30));//空调编号
            dicParam.Add("axle_factory", new ParamObj("axle_factory", txtaxle_factory.Caption.Trim(), SysDbType.VarChar, 40));//车桥厂家
            dicParam.Add("axle_model", new ParamObj("axle_model", txtaxle_model.Caption.Trim(), SysDbType.VarChar, 40));//车桥型号
            dicParam.Add("axle_num", new ParamObj("axle_num", txtaxle_num.Caption.Trim(), SysDbType.NVarChar, 30));//车桥编号
            dicParam.Add("lowdisc_factory", new ParamObj("lowdisc_factory", txtlowdisc_factory.Caption.Trim(), SysDbType.VarChar, 40));//底盘厂家
            dicParam.Add("wheel_count", new ParamObj("wheel_count", !string.IsNullOrEmpty(txtwheel_count.Caption.Trim()) ? txtwheel_count.Caption.Trim() : null, SysDbType.Float));//轮胎数
            dicParam.Add("wheel_factory", new ParamObj("wheel_factory", txtwheel_factory.Caption.Trim(), SysDbType.VarChar, 40));//轮胎厂家
            dicParam.Add("wheel_model", new ParamObj("wheel_model", txtwheel_model.Caption.Trim(), SysDbType.VarChar, 40));//轮胎型号
            dicParam.Add("shift_factory", new ParamObj("shift_factory", txtshift_factory.Caption.Trim(), SysDbType.VarChar, 40));//变速箱厂家
            dicParam.Add("shift_model", new ParamObj("shift_model", txtshift_model.Caption.Trim(), SysDbType.VarChar, 40));//变速箱型号
            dicParam.Add("shift_num", new ParamObj("shift_num", txtshift_num.Caption.Trim(), SysDbType.NVarChar, 30));//变数箱号
            dicParam.Add("shift_type", new ParamObj("shift_type", txtshift_type.Caption.Trim(), SysDbType.VarChar, 40));//变数箱类型
            dicParam.Add("gps_factory", new ParamObj("gps_factory", txtgps_factory.Caption.Trim(), SysDbType.VarChar, 40));//GPS厂家
            dicParam.Add("gps_model", new ParamObj("gps_model", txtgps_model.Caption.Trim(), SysDbType.VarChar, 40));//GPS型号
            dicParam.Add("gps_num", new ParamObj("gps_num", txtgps_num.Caption.Trim(), SysDbType.NVarChar, 30));//GPS编号
            dicParam.Add("dvr_factory", new ParamObj("dvr_factory", txtdvr_factory.Caption.Trim(), SysDbType.VarChar, 40));//DVR厂家
            dicParam.Add("dvr_model", new ParamObj("dvr_model", txtdvr_model.Caption.Trim(), SysDbType.VarChar, 40));//DVR型号
            dicParam.Add("dvr_num", new ParamObj("dvr_num", txtdvr_num.Caption.Trim(), SysDbType.NVarChar, 30));//DVR编号            
            if (wStatus == WindowStatus.Add || wStatus == WindowStatus.Copy)
            {
                dicParam.Add("enable_flag", new ParamObj("enable_flag", Convert.ToInt32(DataSources.EnumEnableFlag.USING).ToString(), SysDbType.VarChar, 1));//信息状态（1|激活；2|作废；0|删除）           
                dicParam.Add("status", new ParamObj("status", (int)DataSources.EnumStatus.Start, SysDbType.VarChar, 40));//启用
                strId = Guid.NewGuid().ToString();
                dicParam.Add("v_id", new ParamObj("v_id", strId, SysDbType.VarChar, 40));//Id
                dicParam.Add("create_by", new ParamObj("create_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//创建人id（制单人）                
                dicParam.Add("create_time", new ParamObj("create_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));
                obj.sqlString = @"insert into tb_vehicle (data_source,license_plate,v_brand,v_factory,vin,car_factory_num,v_color,v_model,v_class,cust_id,carbuy_date
                ,remark,v_long,v_wide,v_high,axle_count,axle_distance,wheel_distance,total_quality,approved_quality,engine_factory,engine_model,engine_num,discharge
                ,fuel_type,air_factory,air_model,air_num,axle_factory,axle_model,axle_num,lowdisc_factory,wheel_count,wheel_factory,wheel_model,shift_factory,shift_model
                ,shift_num,shift_type,gps_factory,gps_model,gps_num,dvr_factory,dvr_model,dvr_num,enable_flag,status,v_id,create_by,create_time)
                 values (@data_source,@license_plate,@v_brand,@v_factory,@vin,@car_factory_num,@v_color,@v_model,@v_class,@cust_id,@carbuy_date
                ,@remark,@v_long,@v_wide,@v_high,@axle_count,@axle_distance,@wheel_distance,@total_quality,@approved_quality,@engine_factory,@engine_model,@engine_num,@discharge
                ,@fuel_type,@air_factory,@air_model,@air_num,@axle_factory,@axle_model,@axle_num,@lowdisc_factory,@wheel_count,@wheel_factory,@wheel_model,@shift_factory,@shift_model
                ,@shift_num,@shift_type,@gps_factory,@gps_model,@gps_num,@dvr_factory,@dvr_model,@dvr_num,@enable_flag,@status,@v_id,@create_by,@create_time);";
            }
            else if (wStatus == WindowStatus.Edit)
            {
                dicParam.Add("v_id", new ParamObj("v_id", strId, SysDbType.VarChar, 40));//Id
                dicParam.Add("update_by", new ParamObj("update_by", HXCPcClient.GlobalStaticObj.UserID, SysDbType.VarChar, 40));//修改人Id                
                dicParam.Add("update_time", new ParamObj("update_time", Common.LocalDateTimeToUtcLong(HXCPcClient.GlobalStaticObj.CurrentDateTime).ToString(), SysDbType.BigInt));//修改时间               
                obj.sqlString = @"update tb_vehicle set data_source=@data_source,license_plate=@license_plate,v_brand=@v_brand,v_factory=@v_factory,vin=@vin,car_factory_num=@car_factory_num
                ,v_color=@v_color,v_model=@v_model,v_class=@v_class,cust_id=@cust_id,carbuy_date=@carbuy_date ,remark=@remark,v_long=@v_long,v_wide=@v_wide,v_high=@v_high
                ,axle_count=@axle_count,axle_distance=@axle_distance,wheel_distance=@wheel_distance,total_quality=@total_quality,approved_quality=@approved_quality
                ,engine_factory=@engine_factory,engine_model=@engine_model,engine_num=@engine_num,discharge=@discharge,fuel_type=@fuel_type,air_factory=@air_factory
                ,air_model=@air_model,air_num=@air_num,axle_factory=@axle_factory,axle_model=@axle_model,axle_num=@axle_num,lowdisc_factory=@lowdisc_factory
                ,wheel_count=@wheel_count,wheel_factory=@wheel_factory,wheel_model=@wheel_model,shift_factory=@shift_factory,shift_model=@shift_model
                ,shift_num=@shift_num,shift_type=@shift_type,gps_factory=@gps_factory,gps_model=@gps_model,gps_num=@gps_num,dvr_factory=@dvr_factory
                ,dvr_model=@dvr_model,dvr_num=@dvr_num,update_by=@update_by,update_time=@update_time
                where v_id=@v_id";
            }
            obj.Param = dicParam;
            listSql.Add(obj);



        }
        #endregion

        #region 删除原来的驾驶员信息
        private void deleteDriInfo(List<SQLObj> listSql,string strDid)
        {
            if (wStatus == WindowStatus.Edit)
            {
                SQLObj obj = new SQLObj();
                obj.cmdType = CommandType.Text;
                Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>();
                dicParam.Add("v_id", new ParamObj("v_id", strDid, SysDbType.VarChar, 40));//司机与车辆关系车辆Id
                obj.sqlString = @"delete from tr_driver_vehicle where v_id=@v_id";
                obj.Param = dicParam;
                listSql.Add(obj);
            }
        }
        #endregion

        #region 添加车辆图片sql语句
        private void AddPhoto(List<SQLObj> listSql, string strId, string path)
        {
            SQLObj sqlObj = new SQLObj();
            sqlObj.cmdType = CommandType.Text;
            Dictionary<string, ParamObj> dic = new Dictionary<string, ParamObj>();
            dic.Add("att_path", new ParamObj("att_path", path, SysDbType.VarChar, 40));
            sqlObj.Param = dic;
            if (photoID.Length == 0)
            {
                dic.Add("att_name", new ParamObj("att_name", "车辆档案图片", SysDbType.NVarChar, 15));
                dic.Add("att_type", new ParamObj("att_type", "图片", SysDbType.NVarChar, 15));
                photoID = Guid.NewGuid().ToString();
                dic.Add("is_main", new ParamObj("is_main", (int)DataSources.EnumYesNo.Yes, SysDbType.VarChar, 5));
                dic.Add("relation_object", new ParamObj("relation_object", "tb_parts", SysDbType.NVarChar, 30));
                dic.Add("relation_object_id", new ParamObj("relation_object_id", strId, SysDbType.VarChar, 40));
                dic.Add("enable_flag", new ParamObj("enable_flag", (int)DataSources.EnumEnableFlag.USING, SysDbType.VarChar, 5));
                dic.Add("create_by", new ParamObj("create_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40));
                dic.Add("create_time", new ParamObj("create_time", DateTime.UtcNow.Ticks, SysDbType.BigInt));
                sqlObj.sqlString = @"insert into [attachment_info] ([att_id],[att_name],[att_type],[att_path],[relation_object],[relation_object_id],[enable_flag],[create_by],[create_time],[is_main])
                            values (@att_id,@att_name,@att_type,@att_path,@relation_object,@relation_object_id,@enable_flag,@create_by,@create_time,@is_main);";
            }
            else
            {
                dic.Add("update_by", new ParamObj("update_by", GlobalStaticObj.UserID, SysDbType.NVarChar, 40));
                dic.Add("update_time", new ParamObj("update_time", DateTime.UtcNow.Ticks, SysDbType.BigInt));
                sqlObj.sqlString = "update [attachment_info] set [att_path]=@att_path,[update_by]=@update_by,[update_time]=@update_time where [att_id]=@att_id;";
            }
            dic.Add("att_id", new ParamObj("att_id", photoID, SysDbType.VarChar, 40));
            listSql.Add(sqlObj);
        }
        #endregion

        #region 保存司机信息
        private void SaveCustomData(List<SQLObj> listSql, string strId)
        {
            foreach (DataGridViewRow dgvr in dgvUser.Rows)
            {
                string strPname = CommonCtrl.IsNullToString(dgvr.Cells["cont_name"].Value);
                if (strPname.Length > 0)
                {
                    SQLObj obj = new SQLObj();
                    obj.cmdType = CommandType.Text;
                    Dictionary<string, ParamObj> dicParam = new Dictionary<string, ParamObj>(); 
                    string strDirVId = CommonCtrl.IsNullToString(dgvr.Cells["dir_v_id"].Value);
                    string strCID = CommonCtrl.IsNullToString(dgvr.Cells["cont_id"].Value);
                    object check = dgvr.Cells[isDefault.Name].EditedFormattedValue;
                    if (check != null && (bool)check)
                    {
                        dicParam.Add("isDefault", new ParamObj("isDefault", "1", SysDbType.VarChar, 1));//是否默认联系人
                    }
                    else
                    {
                        dicParam.Add("isDefault", new ParamObj("isDefault", "0", SysDbType.VarChar, 1));//是否默认联系人
                    }
                    if (strDirVId.Length == 0)
                    {
                        opName = "添加司机信息";
                       strDirVId = Guid.NewGuid().ToString();
                       dicParam.Add("dir_v_id", new ParamObj("dir_v_id", strDirVId, SysDbType.VarChar, 40));//司机与车辆关系Id
                       dicParam.Add("driver_id", new ParamObj("driver_id", strCID, SysDbType.VarChar, 40));//司机与车辆关系司机Id
                       dicParam.Add("v_id", new ParamObj("v_id", strId, SysDbType.VarChar, 40));//司机与车辆关系车辆Id
                       obj.sqlString = @"insert into tr_driver_vehicle (dir_v_id,driver_id,v_id,isDefault) values(@dir_v_id,@driver_id,@v_id,@isDefault);";
                    }
                    else
                    {
                        opName = "添加司机信息";                       
                        dicParam.Add("dir_v_id", new ParamObj("dir_v_id", strDirVId, SysDbType.VarChar, 40));//司机与车辆关系Id
                        dicParam.Add("driver_id", new ParamObj("driver_id", strCID, SysDbType.VarChar, 40));//司机与车辆关系司机Id
                        dicParam.Add("v_id", new ParamObj("v_id", strId, SysDbType.VarChar, 40));//司机与车辆关系车辆Id                       
                        obj.sqlString = @"insert into tr_driver_vehicle (dir_v_id,driver_id,v_id,isDefault) values(@dir_v_id,@driver_id,@v_id,@isDefault);";
                       
                    }
                    obj.Param = dicParam;
                    listSql.Add(obj);
                }
            }
        }
        #endregion

        #region 顶部button显示设置
        /// <summary>
        ///  顶部button显示设置
        /// </summary>
        private void SetTopbuttonShow()
        {
            base.btnAdd.Visible = false;
            base.btnCopy.Visible = false;
            base.btnEdit.Visible = false;
            base.btnDelete.Visible = false;
            base.btnStatus.Visible = false;
            base.btnVerify.Visible = false;
            base.btnSync.Visible = false;
            base.btnConfirm.Visible = false;
            base.btnActivation.Visible = false;
            base.btnSubmit.Visible = false;
        }
        #endregion

        #region 窗体Load事件
        private void UCVehicleAddOrEdit_Load(object sender, EventArgs e)
        {           
            //base.SetBtnStatus(wStatus);
            if (wStatus == WindowStatus.Edit || wStatus == WindowStatus.Copy)
            {
                BindData();
            }
        }
        #endregion

        #region 车型选择器
        private void txtCarType_ChooserClick(object sender, EventArgs e)
        {
            frmVehicleModels frmModels = new frmVehicleModels();
            DialogResult result = frmModels.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCarType.Text = frmModels.VMName;
                txtCarType.Tag = frmModels.VMID;
            }
        }
        #endregion

        #region 所属客户选择器
        private void txtCustomNO_ChooserClick(object sender, EventArgs e)
        {
            frmCustomerInfo frmCInfo = new frmCustomerInfo();
            DialogResult result = frmCInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtCustomNO.Text = frmCInfo.strCustomerName;
                txtCustomNO.Tag = frmCInfo.strCustomerId;

            }
        }
        #endregion

        #region 上传图片
        private void llbUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "图片|*.jpg;*.png;*.gif;*.bmp";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (fileDialog.FileName.Length == 0)
                {
                    return;
                }
                Image image = new Bitmap(fileDialog.FileName);
                //picPhoto.Image = image;
                picPhoto.BackgroundImage = image;
                picPhoto.Tag = fileDialog.FileName;
                picPhoto.ImageLocation = fileDialog.FileName;  
            }
        }
        #endregion

        #region 删除图片
        private void llbDetele_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (photoID.Length == 0)
            {
                picPhoto.Image = null;              
            }
            else
            {
                if (DBHelper.BatchDeleteDataByWhere("删除车辆档案图片", "attachment_info", string.Format("att_id='{0}'", photoID)))
                {
                    picPhoto.BackgroundImage = null;
                }
                else
                {
                    MessageBoxEx.Show("删除失败！");
                }
            }
        }
        #endregion

        #region 设置底部DataGridView的Anchor属性
        /// <summary>
        /// 设置底部DataGridView的Anchor属性
        /// </summary>
        private void SetDgvAnchor()
        {
            dgvUser.Dock = DockStyle.Fill;
            dgvUser.ReadOnly = false;
            dgvUser.Rows.Add(3);
            dgvUser.AllowUserToAddRows = true;
            dgvUser.Columns["cont_name"].ReadOnly = true;
            dgvUser.Columns["cont_post"].ReadOnly = true;
            dgvUser.Columns["cont_phone"].ReadOnly = true;
            dgvUser.Columns["cont_tel"].ReadOnly = true;
            dgvUser.Columns["cont_email"].ReadOnly = true;
            dgvUser.Columns["cont_birthday"].ReadOnly = true;
            dgvUser.Columns["remark"].ReadOnly = true;
            dgvUser.Columns["cont_id"].ReadOnly = true;
        }
        #endregion

        #region 根据车辆档案ID获取信息，复制和编辑用
        /// <summary>
        /// 根据车辆档案ID获取信息，复制和编辑用
        /// </summary>
        private void BindData()
        {
            #region 基础信息
            string strWhere = string.Format("a.v_id='{0}'", strId);
            DataTable dt = DBHelper.GetTable("查询车辆档案", "tb_vehicle a left join attachment_info m on a.v_id = m.relation_object_id", "*", strWhere, "", "");
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            txtCarNO.Caption = CommonCtrl.IsNullToString(dr["license_plate"]);//车牌号
            cobCarBrand.SelectedValue = CommonCtrl.IsNullToString(dr["v_brand"]);//车辆品牌;
            cobFactory.SelectedValue = CommonCtrl.IsNullToString(dr["v_factory"]);//生产厂家
            txtVIN.Caption = CommonCtrl.IsNullToString(dr["vin"]);//VIN
            txtYardCode.Caption = CommonCtrl.IsNullToString(dr["car_factory_num"]);//车场编号
            cobColor.SelectedValue = CommonCtrl.IsNullToString(dr["v_color"]);//颜色 
            txtCarType.Text = CommonCtrl.IsNullToString(dr["v_model"]);//车型 
            cobRating.SelectedValue = CommonCtrl.IsNullToString(dr["v_class"]);//车辆等级
            txtCustomNO.Tag = CommonCtrl.IsNullToString(dr["cust_id"]);//所属客户id
            txtCustomNO.Text = GetCustName(CommonCtrl.IsNullToString(dr["cust_id"]));//所属客户
            if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dr["carbuy_date"])))
            {
                long Rticks = Convert.ToInt64(CommonCtrl.IsNullToString(dr["carbuy_date"]));
                dtpSTime.Value = Common.UtcLongToLocalDateTime(Rticks);//购车日期     
            }
            txtRemark.Caption = CommonCtrl.IsNullToString(dr["remark"]);//备注           
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
            photo = photoPath;
            photoID = CommonCtrl.IsNullToString(dr["att_id"]);
            picPhoto.BackgroundImage = FileOperation.DownLoadImage(photoPath);//图片信息     
            picPhoto.ImageLocation = photoPath;
            #endregion

            #region 底部datagridview数据

            #region 驾驶员信息
            //驾驶员信息                
            DataTable dpt = DBHelper.GetTable("驾驶员信息", "tr_driver_vehicle", "*", string.Format(" v_id='{0}'", strId), "", "");
            if (dpt.Rows.Count > 0)
            {
                
                string strConId = string.Empty;//联系人Id              
                for (int i = 0; i < dpt.Rows.Count; i++)
                {
                    DataRow dur = dpt.Rows[i];
                    strConId += "'" + CommonCtrl.IsNullToString(dur["driver_id"]) + "'" + ",";
                }

                #region 从联系人信息中读取驾驶员信息
                strConId = strConId.Substring(0, strConId.Length - 1);
                if (!string.IsNullOrEmpty(strConId))
                {
                    DataTable dut = DBHelper.GetTable("联系人信息", "tb_contacts", "*", "cont_id in (" + strConId + ")", "", "order by cont_id desc");
                    if (dut.Rows.Count > dgvUser.Rows.Count)
                    {
                        dgvUser.Rows.Add(dut.Rows.Count - dgvUser.Rows.Count + 1);
                    }
                    for (int j = 0; j < dut.Rows.Count; j++)
                    {
                        DataRow dpr = dut.Rows[j];
                        dgvUser.Rows[j].Cells["cont_name"].Value = CommonCtrl.IsNullToString(dpr["cont_name"]);
                        dgvUser.Rows[j].Cells["cont_post"].Value = GetDicName(CommonCtrl.IsNullToString(dpr["cont_post"]));
                        dgvUser.Rows[j].Cells["cont_phone"].Value = CommonCtrl.IsNullToString(dpr["cont_phone"]);
                        dgvUser.Rows[j].Cells["cont_tel"].Value = CommonCtrl.IsNullToString(dpr["cont_tel"]);
                        dgvUser.Rows[j].Cells["cont_email"].Value = CommonCtrl.IsNullToString(dpr["cont_email"]);
                        dgvUser.Rows[j].Cells["cont_birthday"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["cont_birthday"])) ? Common.UtcLongToLocalDateTime(Convert.ToInt64(CommonCtrl.IsNullToString(dpr["cont_birthday"]))).ToString() : "";
                        dgvUser.Rows[j].Cells["remark"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                        dgvUser.Rows[j].Cells["cont_id"].Value = CommonCtrl.IsNullToString(dpr["cont_id"]);
                        DataRow dur = dpt.Rows[j];
                        dgvUser.Rows[j].Cells["dir_v_id"].Value = CommonCtrl.IsNullToString(dur["dir_v_id"]);
                        string strDefault = CommonCtrl.IsNullToString(dur["isDefault"]);
                        if (!string.IsNullOrEmpty(strDefault))
                        {
                            dgvUser.Rows[j].Cells[isDefault.Name].Value = strDefault=="1"? true:false;
                        }
                    }
                }
                #endregion              
            }
            #endregion
            //附件信息数据
            ucAttr.TableName = "tb_vehicle";
            ucAttr.TableNameKeyValue = strId;
            ucAttr.BindAttachment();

            #endregion
        }
        #endregion

        #region 获取客户名称
        private string GetCustName(string strCId)
        {
            return DBHelper.GetSingleValue("获取联系人名称", "tb_customer", "cust_name", "cust_id='" + strCId + "'", " cust_id desc");
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

        #region 读取联系人（司机）信息
        private void dgvUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmContacts contact = new frmContacts();
            DialogResult result = contact.ShowDialog();
            if (result == DialogResult.OK)
            {
                DataTable dut = DBHelper.GetTable("获取联系人(司机)", "tb_contacts", "*", "cont_id='" + contact.contID + "'", "", " order by cont_id desc");
                if (dut.Rows.Count > 0)
                {
                    DataRow dpr = dut.Rows[0];
                    dgvUser.Rows[e.RowIndex].Cells["cont_name"].Value = CommonCtrl.IsNullToString(dpr["cont_name"]);
                    dgvUser.Rows[e.RowIndex].Cells["cont_post"].Value = GetDicName(CommonCtrl.IsNullToString(dpr["cont_post"]));
                    dgvUser.Rows[e.RowIndex].Cells["cont_phone"].Value = CommonCtrl.IsNullToString(dpr["cont_phone"]);
                    dgvUser.Rows[e.RowIndex].Cells["cont_tel"].Value = CommonCtrl.IsNullToString(dpr["cont_tel"]);
                    dgvUser.Rows[e.RowIndex].Cells["cont_email"].Value = CommonCtrl.IsNullToString(dpr["cont_email"]);
                    dgvUser.Rows[e.RowIndex].Cells["cont_birthday"].Value = !string.IsNullOrEmpty(CommonCtrl.IsNullToString(dpr["cont_birthday"])) ? Common.UtcLongToLocalDateTime(Convert.ToInt64(CommonCtrl.IsNullToString(dpr["cont_birthday"]))).ToString() : "";
                    dgvUser.Rows[e.RowIndex].Cells["remark"].Value = CommonCtrl.IsNullToString(dpr["remark"]);
                    dgvUser.Rows[e.RowIndex].Cells["cont_id"].Value = CommonCtrl.IsNullToString(dpr["cont_id"]);
                    dgvUser.Rows.Add(1);
                }
            }
        }
        #endregion

        #region 验证输入的手机号、固话、生日格式是否正确
        private void dgvUser_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0)
            //{
            //    return;
            //}
            //if (dgvUser.Rows[e.RowIndex].ReadOnly)
            //{
            //    return;
            //}
            //if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvUser.Rows[e.RowIndex].Cells["cont_phone"].Value)))
            //{
            //    if (!Validator.IsMobile(dgvUser.Rows[e.RowIndex].Cells["cont_phone"].Value.ToString()))
            //    {
            //        MessageBoxEx.Show("联系人手机号码格式错误,请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            //if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvUser.Rows[e.RowIndex].Cells["cont_tel"].Value)))
            //{
            //    if (!Validator.IsTel(dgvUser.Rows[e.RowIndex].Cells["cont_tel"].Value.ToString()))
            //    {
            //        MessageBoxEx.Show("联系人固话号码格式错误,请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            //if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvUser.Rows[e.RowIndex].Cells["cont_email"].Value)))
            //{
            //    if (!Validator.IsEmail(dgvUser.Rows[e.RowIndex].Cells["cont_email"].Value.ToString()))
            //    {
            //        MessageBoxEx.Show("联系人邮箱格式错误,请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            //if (!string.IsNullOrEmpty(CommonCtrl.IsNullToString(dgvUser.Rows[e.RowIndex].Cells["cont_birthday"].Value)))
            //{
            //    if (!Validator.IsDateTime(dgvUser.Rows[e.RowIndex].Cells["cont_birthday"].Value.ToString()))
            //    {
            //        MessageBoxEx.Show("联系人生日格式错误,请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
        }
        #endregion

        #region 单击图片放大
        private void picPhoto_Click(object sender, EventArgs e)
        {
            string strIUrl = CommonCtrl.IsNullToString(picPhoto.ImageLocation);
            if (!string.IsNullOrEmpty(strIUrl))
            {
                Image img=null;
                if (wStatus == WindowStatus.Edit || wStatus == WindowStatus.Copy)
                {
                    img = FileOperation.DownLoadImage(strIUrl);//图片信息
                }
                else
                {
                    img = new Bitmap(strIUrl);
                }
                if (img != null)
                {
                    BigImage bigimage = new BigImage(img);
                    bigimage.ShowDialog();
                }
            }
        }
        #endregion

        #region 选择默认联系人
        private void dgvUser_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
            foreach (DataGridViewRow dgvr in dgvUser.Rows)
            {
                object check = dgvr.Cells[isDefault.Name].EditedFormattedValue;
                if (check != null && (bool)check)
                {
                    dgvr.Cells[isDefault.Name].Value = false;
                }
            }
            //选择当前行
            dgvUser.Rows[e.RowIndex].Cells[isDefault.Name].Value = true;
        }
        #endregion
    }
}
