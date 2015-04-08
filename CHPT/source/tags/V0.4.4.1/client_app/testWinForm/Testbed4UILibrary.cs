using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using HXC.UI.Library.Controls;

namespace testWinForm
{
    /// <summary>
    /// Testbed4UILibrary
    /// </summary>
    /// <versioning>
    ///     <version number="1.0.0.0">
    ///         <author>Kord Kuo</author> 
    ///         <datetime>2015/1/14 14:23:06</datetime>
    ///         <comment>create</comment>
    ///     </version>
    /// </versioning>
    public partial class Testbed4UILibrary : Form
    {
        #region Constructor -- 构造函数
        public Testbed4UILibrary()
        {
            InitializeComponent();
        }
        #endregion

        private void Testbed4UILibrary_Load(object sender, System.EventArgs e)
        {
            //var dic = new Dictionary<String, String>();
            //dic.Add("1", "A");
            //dic.Add("2", "B");
            //dic.Add("3", "C");
            //dic.Add("4", "D");
            //extComboBox1.DisplayMember = "Value";
            //extComboBox1.DisplayValue = "Key";
            extComboBox1.ContentTypeParameter = typeof (EnumAutoBackupType);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Testbed4UILibrary));
            this.extFieldPanel1 = new HXC.UI.Library.Controls.ExtFieldPanel();
            this.extFieldControl1 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl2 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl5 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl6 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl4 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl3 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl7 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl9 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl8 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extButton1 = new HXC.UI.Library.Controls.ExtButton();
            this.extComboBox1 = new HXC.UI.Library.Controls.ExtComboBox();
            this.extTextBox1 = new HXC.UI.Library.Controls.ExtTextBox();
            this.extFieldPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // extFieldPanel1
            // 
            this.extFieldPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extFieldPanel1.AutoPlacement = true;
            this.extFieldPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.extFieldPanel1.Controls.Add(this.extFieldControl1);
            this.extFieldPanel1.Controls.Add(this.extFieldControl2);
            this.extFieldPanel1.Controls.Add(this.extFieldControl5);
            this.extFieldPanel1.Controls.Add(this.extFieldControl6);
            this.extFieldPanel1.Controls.Add(this.extFieldControl4);
            this.extFieldPanel1.Controls.Add(this.extFieldControl3);
            this.extFieldPanel1.Controls.Add(this.extFieldControl7);
            this.extFieldPanel1.Controls.Add(this.extFieldControl9);
            this.extFieldPanel1.Controls.Add(this.extFieldControl8);
            this.extFieldPanel1.ItemHeight = 23;
            this.extFieldPanel1.ItemMargin = new System.Windows.Forms.Padding(5);
            this.extFieldPanel1.ItemValueWidth = 150;
            this.extFieldPanel1.Location = new System.Drawing.Point(12, 41);
            this.extFieldPanel1.Name = "extFieldPanel1";
            this.extFieldPanel1.Size = new System.Drawing.Size(878, 143);
            this.extFieldPanel1.TabIndex = 3;
            // 
            // extFieldControl1
            // 
            this.extFieldControl1.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl1.BorderWidth = 0;
            this.extFieldControl1.Content = null;
            this.extFieldControl1.ContentTypeName = null;
            this.extFieldControl1.ContentTypeParameter = null;
            this.extFieldControl1.CornerRadiu = 5;
            this.extFieldControl1.DisplayName = "A";
            this.extFieldControl1.DisplayValue = "";
            this.extFieldControl1.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl1.FieldName = null;
            this.extFieldControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl1.InputtingVerifyCondition = null;
            this.extFieldControl1.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl1.Location = new System.Drawing.Point(5, 5);
            this.extFieldControl1.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl1.Name = "extFieldControl1";
            this.extFieldControl1.NameCtrlWidth = 22;
            this.extFieldControl1.RequiredField = false;
            this.extFieldControl1.ShowError = false;
            this.extFieldControl1.Size = new System.Drawing.Size(213, 23);
            this.extFieldControl1.TabIndex = 0;
            this.extFieldControl1.Value = null;
            this.extFieldControl1.ValueCtrlWidth = 191;
            this.extFieldControl1.VerifyCondition = null;
            this.extFieldControl1.VerifyType = null;
            this.extFieldControl1.VerifyTypeName = null;
            // 
            // extFieldControl2
            // 
            this.extFieldControl2.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl2.BorderWidth = 0;
            this.extFieldControl2.Content = null;
            this.extFieldControl2.ContentTypeName = null;
            this.extFieldControl2.ContentTypeParameter = null;
            this.extFieldControl2.CornerRadiu = 5;
            this.extFieldControl2.DisplayName = "B";
            this.extFieldControl2.DisplayValue = "";
            this.extFieldControl2.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl2.FieldName = null;
            this.extFieldControl2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl2.InputtingVerifyCondition = null;
            this.extFieldControl2.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl2.Location = new System.Drawing.Point(228, 5);
            this.extFieldControl2.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl2.Name = "extFieldControl2";
            this.extFieldControl2.NameCtrlWidth = 21;
            this.extFieldControl2.RequiredField = false;
            this.extFieldControl2.ShowError = false;
            this.extFieldControl2.Size = new System.Drawing.Size(213, 23);
            this.extFieldControl2.TabIndex = 0;
            this.extFieldControl2.Value = null;
            this.extFieldControl2.ValueCtrlWidth = 192;
            this.extFieldControl2.VerifyCondition = null;
            this.extFieldControl2.VerifyType = null;
            this.extFieldControl2.VerifyTypeName = null;
            // 
            // extFieldControl5
            // 
            this.extFieldControl5.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl5.BorderWidth = 0;
            this.extFieldControl5.Content = null;
            this.extFieldControl5.ContentTypeName = null;
            this.extFieldControl5.ContentTypeParameter = null;
            this.extFieldControl5.CornerRadiu = 5;
            this.extFieldControl5.DisplayName = "C";
            this.extFieldControl5.DisplayValue = "";
            this.extFieldControl5.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl5.FieldName = null;
            this.extFieldControl5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl5.InputtingVerifyCondition = null;
            this.extFieldControl5.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl5.Location = new System.Drawing.Point(451, 5);
            this.extFieldControl5.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl5.Name = "extFieldControl5";
            this.extFieldControl5.NameCtrlWidth = 22;
            this.extFieldControl5.RequiredField = false;
            this.extFieldControl5.ShowError = false;
            this.extFieldControl5.Size = new System.Drawing.Size(213, 23);
            this.extFieldControl5.TabIndex = 0;
            this.extFieldControl5.Value = null;
            this.extFieldControl5.ValueCtrlWidth = 191;
            this.extFieldControl5.VerifyCondition = null;
            this.extFieldControl5.VerifyType = null;
            this.extFieldControl5.VerifyTypeName = null;
            // 
            // extFieldControl6
            // 
            this.extFieldControl6.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl6.BorderWidth = 0;
            this.extFieldControl6.Content = null;
            this.extFieldControl6.ContentTypeName = null;
            this.extFieldControl6.ContentTypeParameter = null;
            this.extFieldControl6.CornerRadiu = 5;
            this.extFieldControl6.DisplayName = "D";
            this.extFieldControl6.DisplayValue = "";
            this.extFieldControl6.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl6.FieldName = null;
            this.extFieldControl6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl6.InputtingVerifyCondition = null;
            this.extFieldControl6.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl6.Location = new System.Drawing.Point(5, 38);
            this.extFieldControl6.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl6.Name = "extFieldControl6";
            this.extFieldControl6.NameCtrlWidth = 23;
            this.extFieldControl6.RequiredField = false;
            this.extFieldControl6.ShowError = false;
            this.extFieldControl6.Size = new System.Drawing.Size(213, 23);
            this.extFieldControl6.TabIndex = 0;
            this.extFieldControl6.Value = null;
            this.extFieldControl6.ValueCtrlWidth = 190;
            this.extFieldControl6.VerifyCondition = null;
            this.extFieldControl6.VerifyType = null;
            this.extFieldControl6.VerifyTypeName = null;
            // 
            // extFieldControl4
            // 
            this.extFieldControl4.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl4.BorderWidth = 0;
            this.extFieldControl4.Content = null;
            this.extFieldControl4.ContentTypeName = null;
            this.extFieldControl4.ContentTypeParameter = null;
            this.extFieldControl4.CornerRadiu = 5;
            this.extFieldControl4.DisplayName = "E";
            this.extFieldControl4.DisplayValue = "";
            this.extFieldControl4.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl4.FieldName = null;
            this.extFieldControl4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl4.InputtingVerifyCondition = null;
            this.extFieldControl4.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl4.Location = new System.Drawing.Point(228, 38);
            this.extFieldControl4.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl4.Name = "extFieldControl4";
            this.extFieldControl4.NameCtrlWidth = 20;
            this.extFieldControl4.RequiredField = false;
            this.extFieldControl4.ShowError = false;
            this.extFieldControl4.Size = new System.Drawing.Size(213, 23);
            this.extFieldControl4.TabIndex = 0;
            this.extFieldControl4.Value = null;
            this.extFieldControl4.ValueCtrlWidth = 193;
            this.extFieldControl4.VerifyCondition = null;
            this.extFieldControl4.VerifyType = null;
            this.extFieldControl4.VerifyTypeName = null;
            // 
            // extFieldControl3
            // 
            this.extFieldControl3.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl3.BorderWidth = 0;
            this.extFieldControl3.Content = null;
            this.extFieldControl3.ContentTypeName = null;
            this.extFieldControl3.ContentTypeParameter = null;
            this.extFieldControl3.CornerRadiu = 5;
            this.extFieldControl3.DisplayName = "F";
            this.extFieldControl3.DisplayValue = "";
            this.extFieldControl3.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl3.FieldName = null;
            this.extFieldControl3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl3.InputtingVerifyCondition = null;
            this.extFieldControl3.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl3.Location = new System.Drawing.Point(451, 38);
            this.extFieldControl3.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl3.Name = "extFieldControl3";
            this.extFieldControl3.NameCtrlWidth = 20;
            this.extFieldControl3.RequiredField = false;
            this.extFieldControl3.ShowError = false;
            this.extFieldControl3.Size = new System.Drawing.Size(213, 23);
            this.extFieldControl3.TabIndex = 0;
            this.extFieldControl3.Value = null;
            this.extFieldControl3.ValueCtrlWidth = 193;
            this.extFieldControl3.VerifyCondition = null;
            this.extFieldControl3.VerifyType = null;
            this.extFieldControl3.VerifyTypeName = null;
            // 
            // extFieldControl7
            // 
            this.extFieldControl7.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl7.BorderWidth = 0;
            this.extFieldControl7.Content = null;
            this.extFieldControl7.ContentTypeName = null;
            this.extFieldControl7.ContentTypeParameter = null;
            this.extFieldControl7.CornerRadiu = 5;
            this.extFieldControl7.DisplayName = "G";
            this.extFieldControl7.DisplayValue = "";
            this.extFieldControl7.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl7.FieldName = null;
            this.extFieldControl7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl7.InputtingVerifyCondition = null;
            this.extFieldControl7.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl7.Location = new System.Drawing.Point(5, 71);
            this.extFieldControl7.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl7.Name = "extFieldControl7";
            this.extFieldControl7.NameCtrlWidth = 23;
            this.extFieldControl7.RequiredField = false;
            this.extFieldControl7.ShowError = false;
            this.extFieldControl7.Size = new System.Drawing.Size(213, 23);
            this.extFieldControl7.TabIndex = 0;
            this.extFieldControl7.Value = null;
            this.extFieldControl7.ValueCtrlWidth = 190;
            this.extFieldControl7.VerifyCondition = null;
            this.extFieldControl7.VerifyType = null;
            this.extFieldControl7.VerifyTypeName = null;
            // 
            // extFieldControl9
            // 
            this.extFieldControl9.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl9.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl9.BorderWidth = 0;
            this.extFieldControl9.Content = null;
            this.extFieldControl9.ContentTypeName = null;
            this.extFieldControl9.ContentTypeParameter = null;
            this.extFieldControl9.CornerRadiu = 5;
            this.extFieldControl9.DisplayName = "H";
            this.extFieldControl9.DisplayValue = "";
            this.extFieldControl9.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl9.FieldName = null;
            this.extFieldControl9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl9.InputtingVerifyCondition = null;
            this.extFieldControl9.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl9.Location = new System.Drawing.Point(228, 71);
            this.extFieldControl9.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl9.Name = "extFieldControl9";
            this.extFieldControl9.NameCtrlWidth = 23;
            this.extFieldControl9.RequiredField = false;
            this.extFieldControl9.ShowError = false;
            this.extFieldControl9.Size = new System.Drawing.Size(213, 23);
            this.extFieldControl9.TabIndex = 0;
            this.extFieldControl9.Value = null;
            this.extFieldControl9.ValueCtrlWidth = 190;
            this.extFieldControl9.VerifyCondition = null;
            this.extFieldControl9.VerifyType = null;
            this.extFieldControl9.VerifyTypeName = null;
            // 
            // extFieldControl8
            // 
            this.extFieldControl8.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl8.BorderWidth = 0;
            this.extFieldControl8.Content = null;
            this.extFieldControl8.ContentTypeName = null;
            this.extFieldControl8.ContentTypeParameter = null;
            this.extFieldControl8.CornerRadiu = 5;
            this.extFieldControl8.DisplayName = "I";
            this.extFieldControl8.DisplayValue = "";
            this.extFieldControl8.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl8.FieldName = null;
            this.extFieldControl8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl8.InputtingVerifyCondition = null;
            this.extFieldControl8.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl8.Location = new System.Drawing.Point(451, 71);
            this.extFieldControl8.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl8.Name = "extFieldControl8";
            this.extFieldControl8.NameCtrlWidth = 17;
            this.extFieldControl8.RequiredField = false;
            this.extFieldControl8.ShowError = false;
            this.extFieldControl8.Size = new System.Drawing.Size(213, 23);
            this.extFieldControl8.TabIndex = 0;
            this.extFieldControl8.Value = null;
            this.extFieldControl8.ValueCtrlWidth = 196;
            this.extFieldControl8.VerifyCondition = null;
            this.extFieldControl8.VerifyType = null;
            this.extFieldControl8.VerifyTypeName = null;
            // 
            // extButton1
            // 
            this.extButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.extButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("extButton1.BackgroundImage")));
            this.extButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.extButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extButton1.BorderWidth = 0;
            this.extButton1.Content = null;
            this.extButton1.ContentTypeName = null;
            this.extButton1.ContentTypeParameter = null;
            this.extButton1.CornerRadiu = 0;
            this.extButton1.DisplayValue = "Button";
            this.extButton1.InputtingVerifyCondition = null;
            this.extButton1.LightBackgroudImage = ((System.Drawing.Image)(resources.GetObject("extButton1.LightBackgroudImage")));
            this.extButton1.Location = new System.Drawing.Point(410, 12);
            this.extButton1.Name = "extButton1";
            this.extButton1.NormalBackgroudImage = ((System.Drawing.Image)(resources.GetObject("extButton1.NormalBackgroudImage")));
            this.extButton1.ShowError = false;
            this.extButton1.Size = new System.Drawing.Size(75, 23);
            this.extButton1.TabIndex = 2;
            this.extButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.extButton1.Value = "Button";
            this.extButton1.VerifyCondition = null;
            this.extButton1.VerifyType = null;
            this.extButton1.VerifyTypeName = null;
            this.extButton1.Click += new System.EventHandler(this.extButton1_Click);
            // 
            // extComboBox1
            // 
            this.extComboBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extComboBox1.BorderWidth = 1;
            this.extComboBox1.Content = null;
            this.extComboBox1.ContentTypeName = "Enum";
            this.extComboBox1.ContentTypeParameter = null;
            this.extComboBox1.CornerRadiu = 5;
            this.extComboBox1.DisplayMember = "Text";
            this.extComboBox1.DisplayValue = "";
            this.extComboBox1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extComboBox1.FormattingEnabled = true;
            this.extComboBox1.InputtingVerifyCondition = null;
            this.extComboBox1.Location = new System.Drawing.Point(12, 12);
            this.extComboBox1.Name = "extComboBox1";
            this.extComboBox1.ShowError = false;
            this.extComboBox1.Size = new System.Drawing.Size(392, 23);
            this.extComboBox1.TabIndex = 1;
            this.extComboBox1.Value = null;
            this.extComboBox1.ValueMember = "Value";
            this.extComboBox1.VerifyCondition = null;
            this.extComboBox1.VerifyType = null;
            this.extComboBox1.VerifyTypeName = null;
            // 
            // extTextBox1
            // 
            this.extTextBox1.BackColor = System.Drawing.Color.White;
            this.extTextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extTextBox1.BorderWidth = 1;
            this.extTextBox1.Content = null;
            this.extTextBox1.ContentTypeName = null;
            this.extTextBox1.ContentTypeParameter = null;
            this.extTextBox1.CornerRadiu = 5;
            this.extTextBox1.DisplayValue = "";
            this.extTextBox1.InputtingVerifyCondition = null;
            this.extTextBox1.Location = new System.Drawing.Point(445, 48);
            this.extTextBox1.MaxLength = 32767;
            this.extTextBox1.Name = "extTextBox1";
            this.extTextBox1.ReadOnly = false;
            this.extTextBox1.ShowError = false;
            this.extTextBox1.Size = new System.Drawing.Size(206, 22);
            this.extTextBox1.TabIndex = 0;
            this.extTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.extTextBox1.Value = null;
            this.extTextBox1.VerifyCondition = null;
            this.extTextBox1.VerifyType = null;
            this.extTextBox1.VerifyTypeName = null;
            // 
            // Testbed4UILibrary
            // 
            this.ClientSize = new System.Drawing.Size(912, 371);
            this.Controls.Add(this.extFieldPanel1);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.extComboBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Testbed4UILibrary";
            this.Load += new System.EventHandler(this.Testbed4UILibrary_Load);
            this.extFieldPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void extSearchPanel1_Load(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {

        }

        private void extUserControl1_Paint(object sender, PaintEventArgs e)
        {
            Margin = new Padding();
        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            extFieldControl7.Visible = false;
            //extFieldPanel1.Controls.Add(new ExtFieldControl());
            //MessageBox.Show(extComboBox1.Value.ToString() + "%%%%%%" + extComboBox1.DisplayValue);
        }

        #region Field -- 字段

        #endregion

        #region Property -- 属性

        #endregion

        #region Method -- 方法

        #endregion

        #region Event -- 事件

        #endregion
    }

    /// <summary> 
    /// 自动备份类型
    /// </summary>
    public enum EnumAutoBackupType
    {
        [Description("每天")]
        EveryDay = 0,
        [Description("每周")]
        EveryWeek,
        [Description("每月")]
        EveryMonth
    }

}


