using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ServiceStationClient.ComponentUI.DataGrid
{
    /// <summary>
    /// 单元格内的上传控件
    /// </summary>
    public partial class UCDataGridViewFileUpload : UserControl, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        int rowindex;
        bool valueChanged;
        public UCDataGridViewFileUpload()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 更改用户界面，使之与指定单元格样式一样
        /// </summary>
        /// <param name="dataGridViewCellStyle"></param>
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
        }

        /// <summary>
        /// 获取或设置包含单元格的DataGridView
        /// </summary>
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object EditingControlFormattedValue
        {
            get
            {
                if (txtUpload.Caption.Length > 0)
                {
                    return txtUpload.Caption;
                }
                return null;
            }
            set
            {
                txtUpload.Caption = value.ToString();
            }
        }

        public override string Text
        {
            get
            {
                return txtUpload.Caption;
            }
            set
            {
                txtUpload.Caption = value;
            }
        }
        /// <summary>
        /// 获取或设置承载单元格的父行的索引，包含单元格的行索引，如果没有父行则返回-1
        /// </summary>
        public int EditingControlRowIndex
        {
            get
            {
                return rowindex;
            }
            set
            {
                rowindex = value;
            }
        }
        /// <summary>
        /// 获取或者设置一个值，该值指示编辑控件的值是否与承载单元格的值不同
        /// </summary>
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        /// <summary>
        /// 确定指定的键是应由编辑控件处理的常规输入键，还是应由DataGridView处理的特殊键
        /// </summary>
        /// <param name="keyData">表示按下的键</param>
        /// <param name="dataGridViewWantsInputKey"></param>
        /// <returns></returns>
        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 获取当鼠标指针位于DataGridView.EditingPanel上方但不位于编辑控件上方时所使用的光标
        /// </summary>
        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        /// <summary>
        /// 准备当前选中的单元格以进行编辑
        /// </summary>
        /// <param name="selectAll"></param>
        public void PrepareEditingControlForEdit(bool selectAll)
        {

        }

        /// <summary>
        /// 获取一个值，指示每当值更改时，是否需要重新定位单元格的内容
        /// </summary>
        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }
        /// <summary>
        /// 过滤文件类型
        /// </summary>
        public string Filter = "All files(*.*)|*.*;";
        public int FileLength = 4;
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = Filter;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (fileDialog.OpenFile().Length > 4 * 1024 * 1024)
                {
                    MessageBoxEx.Show(string.Format("文件不能大于{0}M", FileLength));
                    return;
                }
                this.txtUpload.Caption = fileDialog.FileName;
                this.valueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            }
        }
    }
}
