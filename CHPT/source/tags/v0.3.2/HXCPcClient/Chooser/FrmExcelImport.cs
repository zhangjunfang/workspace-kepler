using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HXCPcClient.Chooser;
using HXCPcClient.CommonClass;
using Utility.Common;
using SYSModel;
using ServiceStationClient.ComponentUI;
using System.Collections;
namespace HXCPcClient.Chooser
{
    public partial class FrmExcelImport : FormChooser
    {
        #region
        public DataTable ExcelTable = null;//存储Excel表格数据
        public Hashtable MatchFieldHTable = new Hashtable();//存储单据与Excel表格匹配字段
        private string ExcelFieldDefVal = "请选择";
        Dictionary<string,string> BillFieldDic=null;//存储单据字段列名称和标题文本
        #endregion
        public FrmExcelImport(Dictionary<string, string> FieldDic, string Head)
        {
            InitializeComponent();
            this.BillFieldDic = FieldDic;
            this.Text = Head;//标题文本
        }
        /// <summary>
        /// 循环获取Excel表格中所有列名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportExcelFields_Click(object sender, EventArgs e)
        {
            try
            {
                               //打开指定文件类型
               OpenFileDialog openFileDialog = new OpenFileDialog();
               openFileDialog.Filter = "EXCEL文件(*.xls;*.xlsx)|*.xls;*.xlsx";
               openFileDialog.FilterIndex = 1;
               openFileDialog.RestoreDirectory = true;
               openFileDialog.Title = "导入Excel配件信息文件";
               if (openFileDialog.ShowDialog() == DialogResult.OK)
               {
                   ExcelTable = CommonFuncCall.ImportExcelFile(openFileDialog.FileName);//获取Excel表格数据
               }
               else
               {
                   return;
               }
                //获取所有Excel表格列名
                foreach (DataGridViewRow dgRow in dgMatchList.Rows)
                {

                   CommonFuncCall.BindExcelHeadText((DataGridViewComboBoxCell)dgRow.Cells["ExcelField"], ExcelTable, ExcelFieldDefVal);


                }
            }catch(Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 加载导入单据列名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmExcelImport_Load(object sender, EventArgs e)
        {
            try
            {
                dgMatchList.ReadOnly = false;
                HeardText.ReadOnly = true;
                foreach (KeyValuePair<string ,string> KVDic in BillFieldDic)
                { 
                  DataGridViewRow dgRow=dgMatchList.Rows[dgMatchList.Rows.Add()];//创建行项
                  dgRow.Cells["BillField"].Value = KVDic.Key;
                  dgRow.Cells["HeardText"].Value = KVDic.Value;
                  dgRow.Cells["ExcelField"].Value=string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }
        /// <summary>
        /// 导入Excel与单据匹配字段的所有数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImportExcelData_Click(object sender, EventArgs e)
        {
            try
            {
                for(int i=0;i<dgMatchList.Rows.Count;i++)
                {
                    if (string.IsNullOrEmpty( dgMatchList.Rows[0].Cells["ExcelField"].Value.ToString()))
                    {
                        MessageBoxEx.Show("请您先导入Excel文件配件信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (dgMatchList.Rows[i].Cells["ExcelField"].Value.ToString() == ExcelFieldDefVal)
                    {
                        MessageBoxEx.Show("请您选择与单据对应的Excel列名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        lblImportStatus.Text = "正在导入中......";
                        //获取Excel匹配的字段名
                        MatchFieldHTable.Add(dgMatchList.Rows[i].Cells["BillField"].ToString(), dgMatchList.Rows[i].Cells["ExcelField"].ToString());
                        DialogResult = DialogResult.OK;
                        if (ExcelTable.Rows.Count == 0)
                        {
                            lblImportStatus.Text = string.Empty;
                            MessageBoxEx.Show("导入成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }



    }
}
