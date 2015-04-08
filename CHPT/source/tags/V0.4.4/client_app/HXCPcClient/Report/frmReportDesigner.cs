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
using System.IO;
using SYSModel;
using System.Drawing.Printing;

namespace HXCPcClient.Report
{
    /// <summary>
    /// 报表设计
    /// </summary>
    public partial class frmReportDesigner : FormEx
    {
        #region 属性
        string styleObject = string.Empty;//报表对象
        string styleTitle = string.Empty;//报表标题
        DataTable dt = new DataTable();//报表数据内容
        Dictionary<string, List<string>> dicSpanRows = new Dictionary<string, List<string>>();//合并列
        string styleName = string.Empty;//样式名称
        string fileName = string.Empty;//样式文件
        FastReportEx reportEx = new FastReportEx();
        bool isDefault = false;//是否默认
        PaperSize paperSize = null;//
        /// <summary>
        /// 报表大小
        /// </summary>
        public PaperSize ReportPaperSize
        {
            get
            {
                return paperSize;
            }
            set
            {
                paperSize = value;
                reportEx.ReportPaperSize = value;
            }
        }
        #endregion
        /// <summary>
        /// 报表设计器
        /// </summary>
        /// <param name="styleObject">报表对象</param>
        /// <param name="styleTitle">报表标题</param>
        /// <param name="dt">报表数据内容</param>
        /// <param name="dicSpanRows">合并列</param>
        public frmReportDesigner(string styleObject, string styleTitle, DataTable dt, Dictionary<string, List<string>> dicSpanRows)
        {
            InitializeComponent();
            this.styleObject = styleObject;
            this.styleTitle = styleTitle;
            this.dt = dt;
            this.dicSpanRows = dicSpanRows;
            designerReport.UIStyle = FastReport.Utils.UIStyle.Office2003;
            pnlContainer.BackColor = ColorTranslator.FromHtml("#eff8ff");

            reportEx.styleObject = styleObject;
            reportEx.dt = dt;
            reportEx.dicSpanRows = dicSpanRows;
            reportEx.styleTitle = styleTitle;
        }

        public void Dispose()
        {
            base.Dispose(true);
            if (reportEx != null)
            {
                reportEx.Dispose();
            }
            if (designerReport != null)
            {
                designerReport.Dispose();
            }
            styleObject = null;
            styleTitle = null;
            if (dt != null)
            {
                dt.Dispose();
            }
            dt = null;
            dicSpanRows = null;
            styleName = null;
            fileName = null;
            paperSize = null;
        }

        //加载
        private void frmReportDesigner_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(styleObject))
            {
                return;
            }
            BindStyle(null);
            //如果没有报表，则新建一个
            if (designerReport.Report == null)
            {
                FastReport.Report rep = new FastReport.Report();
                designerReport.Report = rep;
                designerReport.RefreshLayout();
            }
        }
        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            //designerReport.cmdSave.Invoke();
            Save();
        }
        /// <summary>
        /// 保存，只更新模板文件
        /// </summary>
        void Save()
        {
            if (cboPrintStyle.SelectedValue == null)
            {
                return;
            }
            if (cboPrintStyle.SelectedValue.ToString() == styleObject + ".frx")
            {
                isDefault = true;
            }
            styleName = cboPrintStyle.Text;
            fileName = string.Format("Report/{0}", cboPrintStyle.SelectedValue.ToString());
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            designerReport.Report.Save(fileStream);
            //designerReport.cmdSave.Invoke();

            fileStream.Close();
            fileStream.Dispose();
            //上传到服务器端
            //if (!FileOperation.UploadFile(fileName, cboPrintStyle.SelectedValue.ToString()))
            if (!FileOperation.UploadFileByPath("Report", fileName))
            {
                MessageBoxEx.Show("保存失败！");
            }
            else
            {
                btnSave.Enabled = false;
            }
            //fileName = localFileName.Replace("Report/", "");
            //FileTransferOperation fileOperation = new FileTransferOperation();
            //fileOperation.UpReceive += new HXCFileTransferCache_Client.UpLoadFileReceiveHandler<HXCFileTransferCache_Client.UpLoadFileReceiveEventArgs>(fileOperation_UpReceive);
            //fileOperation.UploadFile(localFileName, "//Report");
        }

        //关闭窗体，检测是否有修改
        private void frmReportDesigner_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnSave.Enabled)
            {
                return;
            }
            if (MessageBoxEx.Show("是否保存已修改的模板？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Save();
            }
        }
        //新增保存
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //让用户输入新保存的样式名称
            frmReportStyle frmStyle = new frmReportStyle(styleObject);
            if (frmStyle.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            isDefault = false;
            styleName = frmStyle.styleName;
            string fileName = string.Format("Report/{0}{1}.frx", styleObject, frmStyle.styleName);
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            //获取模板文件流
            //designerReport.Report.Save(fileStream);
            designerReport.Report.Save(fileStream);
            fileStream.Close();
            fileStream.Dispose();
            //FileTransferOperation fileOperation = new FileTransferOperation();
            //fileOperation.UpReceive += new HXCFileTransferCache_Client.UpLoadFileReceiveHandler<HXCFileTransferCache_Client.UpLoadFileReceiveEventArgs>(fileOperation_UpReceive);
            //fileName = localFileName.Replace("Report/", "");
            ////fileName = "./" + fileName;
            ////fileOperation.UploadFile(fileName, null);
            //fileOperation.UploadFile(localFileName, "//Report");
            //上传到服务器
            //if (!FileOperation.UploadFile(fileName, Path.GetFileName(fileName)))
            if (!FileOperation.UploadFileByPath("Report", fileName))
            {
                return;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("style_id", Guid.NewGuid().ToString());
            dic.Add("style_object", styleObject);
            dic.Add("style_name", frmStyle.styleName);
            dic.Add("style_url", Path.GetFileName(fileName));
            dic.Add("create_by", GlobalStaticObj.UserID);
            dic.Add("create_time", DateTime.UtcNow.Ticks.ToString());
            if (DBHelper.Submit_AddOrEdit("新增打印样式", "tb_print_style", null, null, dic))
            {
                MessageBoxEx.Show("新增成功！");
                fileStream.Close();
                fileStream.Dispose();
                BindStyle(styleName);
                cboPrintStyle.SelectedValue = fileName;
            }
            else
            {
                MessageBoxEx.Show("新增失败！");
            }
        }

        //上传接收
        //void fileOperation_UpReceive(object sender, HXCFileTransferCache_Client.UpLoadFileReceiveEventArgs e)
        //{
        //    if (e.UpLoadReceiveFileObj.Status == SYSModel.ReceiveFileStatus.WriteEnd)
        //    {
        //        if (!DBHelper.IsExist("", "tb_print_style", string.Format("style_object='{0}' and style_name='{1}'", styleObject, styleName)))
        //        {
        //            Dictionary<string, string> dic = new Dictionary<string, string>();
        //            dic.Add("style_id", Guid.NewGuid().ToString());
        //            dic.Add("style_object", styleObject);
        //            dic.Add("style_name", styleName);
        //            dic.Add("style_url", fileName);
        //            dic.Add("is_default", isDefault ? "1" : "0");
        //            dic.Add("create_by", GlobalStaticObj.UserID);
        //            dic.Add("create_time", DateTime.UtcNow.Ticks.ToString());
        //            if (DBHelper.Submit_AddOrEdit("新增打印样式", "tb_print_style", null, null, dic))
        //            {
        //                MessageBoxEx.Show("新增成功！");
        //                BindStyle(fileName);
        //            }
        //            else
        //            {
        //                MessageBoxEx.Show("新增失败！");
        //            }
        //        }
        //    }
        //    else if (e.UpLoadReceiveFileObj.Status == SYSModel.ReceiveFileStatus.WriteWaitTimeOut)//接收超时
        //    {
        //        MessageBoxEx.Show("保存失败！");
        //    }
        //}
        /// <summary>
        /// 绑定样式，每个报表必须有一个默认报表
        /// </summary>
        void BindStyle(string fileName)
        {
            DataTable dtStyle = DBHelper.GetTable("", "tb_print_style", "style_url,style_name,style_id,is_default", string.Format("style_object='{0}'", styleObject), "", "");
            //样式内容为空，怎手动创建列
            if (dtStyle == null || dtStyle.Rows.Count == 0)
            {
                if (dtStyle == null)
                {
                    dtStyle = new DataTable();
                }
                dtStyle.Columns.Add("style_url");
                dtStyle.Columns.Add("style_name");
                dtStyle.Columns.Add("style_id");
                dtStyle.Columns.Add("is_default");
            }
            var drs = from t in dtStyle.AsEnumerable()
                      where t.Field<string>("style_name") == "默认样式"
                      select t;
            var drsDefault = from d in dtStyle.AsEnumerable()
                             where d.Field<string>("is_default") == "1"
                             select d;
            if (drs.Count() == 0)
            {
                //添加默认样式
                DataRow dr = dtStyle.NewRow();
                dr["style_url"] = styleObject + ".frx";
                dr["style_name"] = "默认样式";
                string is_default = "1";
                if (drsDefault.Count() > 0)
                {
                    is_default = "0";
                }
                dr["is_default"] = is_default;
                dtStyle.Rows.InsertAt(dr, 0);
            }
            //cboPrintStyle.ValueMember = "style_url";
            //cboPrintStyle.DisplayMember = "style_name";
            //cboPrintStyle.DataSource = dtStyle;
            SetPrintStyle(cboPrintStyle, dtStyle);
            //如果没有要选择的样式，则选择默认样式
            if (string.IsNullOrEmpty(fileName))
            {
                if (drsDefault.Count() <= 0)
                {
                    cboPrintStyle.SelectedValue = styleObject + ".frx";
                }
                else
                {
                    foreach (DataRow dr in drsDefault)
                    {
                        cboPrintStyle.SelectedValue = dr["style_url"];
                    }
                }
            }
            else
            {
                cboPrintStyle.SelectedValue = fileName;
            }
        }

        void SetPrintStyle(ComboBoxEx cbo, DataTable dt)
        {
            Action<DataTable> setPrintStyleAction = dtStyle =>
                {
                    cbo.ValueMember = "style_url";
                    cbo.DisplayMember = "style_name";
                    cbo.DataSource = dtStyle;
                };
            if (cbo.InvokeRequired)
            {
                cbo.Invoke(setPrintStyleAction, dt);
            }
            else
            {
                setPrintStyleAction(dt);
            }
        }
        //导入
        private void btnImport_Click(object sender, EventArgs e)
        {
            designerReport.cmdOpen.Invoke();
        }
        //选择打印样式
        private void cboPrintStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPrintStyle.SelectedItem == null)
            {
                return;
            }
            DataRowView drv = (DataRowView)cboPrintStyle.SelectedItem;
            //样式ID
            string styleID = CommonCtrl.IsNullToString(drv["style_id"]);
            //如果是默认样式，则不用设为默认样式
            btnDefault.Enabled = styleID.Length > 0 && CommonCtrl.IsNullToString(drv["is_default"]) != "1";
            //如果有样式ID,才可以删除
            btnDelete.Enabled = styleID.Length > 0;

            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }
            string printStyle = CommonCtrl.IsNullToString(cboPrintStyle.SelectedValue);
            FastReport.Report rep;
            //FileTransferOperation fileOperation = new FileTransferOperation();
            ////如果下载出问题，用默认样式
            //if (!fileOperation.DownLoadFile(printStyle, "Report"))
            //{
            //    //LoadDefaultStyle();
            //    reportEx.LoadDefaultStyle(designerReport);
            //}
            //fileOperation.DownFile += new HXCFileTransferCache_Client.DownLoadFileHandler<HXCFileTransferCache_Client.DownLoadFileEventArgs>(fileOperation_DownFile);
            //下载打印样式
            string fileName = FileOperation.DownLoadFileByFile(Path.GetFileName(printStyle), "Report");
            //如果没找到，则用默认样式
            if (string.IsNullOrEmpty(fileName))
            {
                FastReportEx reportEx = new FastReportEx();
                reportEx.styleObject = styleObject;
                reportEx.dt = dt;
                reportEx.dicSpanRows = dicSpanRows;
                reportEx.styleTitle = styleTitle;
                rep = reportEx.DefaultReport();
                if (rep == null)
                {
                    return;
                }
            }
            else
            {
                rep = new FastReport.Report();
                rep.Load(fileName);
                if (dt != null && dt.Rows.Count > 0)
                {
                    rep.RegisterData(dt, styleObject);
                }
            }
            designerReport.Report = rep;
            designerReport.RefreshLayout();
        }

        //下载样式
        //void fileOperation_DownFile(object sender, HXCFileTransferCache_Client.DownLoadFileEventArgs e)
        //{
        //    //下载完成
        //    if (e.DownLoadFileObj.Status == SYSModel.GetFileWriteStatus.WriteEnd)
        //    {
        //        string filePath = Path.Combine(e.DownLoadFileObj.path.TrimStart('\\'), e.DownLoadFileObj.FileName);
        //        reportEx.LoadPrintStyle(filePath, designerReport);

        //    }
        //    else if (e.DownLoadFileObj.Status == SYSModel.GetFileWriteStatus.WriteWaitTimeOut)//下载超时
        //    {
        //        reportEx.LoadDefaultStyle(designerReport);
        //    }
        //}
        //删除样式
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cboPrintStyle.SelectedItem == null)
            {
                return;
            }
            DataRowView drv = (DataRowView)cboPrintStyle.SelectedItem;
            //获取样式ID
            string styleID = CommonCtrl.IsNullToString(drv["style_id"]);
            if (styleID.Length == 0)
            {
                return;
            }
            if (MessageBoxEx.Show(string.Format("是否要删除【{0}】样式？", cboPrintStyle.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (DBHelper.DeleteDataByID("", "tb_print_style", "style_id", styleID))
                {
                    BindStyle(null);
                }
                else
                {
                    MessageBoxEx.Show("删除失败！");
                }
            }
        }
        //导出
        private void btnExport_Click(object sender, EventArgs e)
        {
            designerReport.cmdSaveAs.Invoke();
        }

        /// 设为默认
        private void btnDefault_Click(object sender, EventArgs e)
        {
            if (cboPrintStyle.SelectedItem == null)
            {
                return;
            }
            DataRowView drv = (DataRowView)cboPrintStyle.SelectedItem;
            //获取样式ID
            string styleID = CommonCtrl.IsNullToString(drv["style_id"]);
            if (styleID.Length == 0)
            {
                return;
            }
            fileName = cboPrintStyle.SelectedValue.ToString();
            List<SysSQLString> listSql = new List<SysSQLString>();
            //更新去除默认
            SysSQLString sqlDelete = new SysSQLString();
            sqlDelete.cmdType = CommandType.Text;
            sqlDelete.Param = new Dictionary<string, string>();
            sqlDelete.Param.Add("style_object", styleObject);
            sqlDelete.sqlString = "update tb_print_style set is_default='0' where style_object=@style_object ";
            listSql.Add(sqlDelete);
            //更新默认样式
            SysSQLString sqlAdd = new SysSQLString();
            sqlAdd.cmdType = CommandType.Text;
            sqlAdd.Param = new Dictionary<string, string>();
            sqlAdd.Param.Add("style_id", styleID);
            sqlAdd.sqlString = "update tb_print_style set is_default='1' where style_id=@style_id";
            listSql.Add(sqlAdd);
            if (DBHelper.BatchExeSQLStringMultiByTrans("", listSql))
            {
                MessageBoxEx.Show("设置成功！");
                BindStyle(fileName);
            }
            else
            {
                MessageBoxEx.Show("设置失败！");
            }
        }

        //报表设计器状态更改，修改保存按钮状态
        private void designerReport_UIStateChanged(object sender, EventArgs e)
        {
            //如果没有选择打印样式，则不能保存
            if (cboPrintStyle.SelectedValue == null)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = designerReport.cmdSave.Enabled;
            }
        }

    }
}
