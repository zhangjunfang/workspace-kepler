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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.extFieldPanel3 = new HXC.UI.Library.Controls.ExtFieldPanel();
            this.extFieldControl19 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl20 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl21 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl22 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl23 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl24 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl25 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl26 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl27 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldPanel2 = new HXC.UI.Library.Controls.ExtFieldPanel();
            this.extFieldControl10 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl11 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl12 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl13 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl14 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl15 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl16 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl17 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl18 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extButton2 = new HXC.UI.Library.Controls.ExtButton();
            this.extButton3 = new HXC.UI.Library.Controls.ExtButton();
            this.extFieldPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.extFieldPanel3.SuspendLayout();
            this.extFieldPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // extFieldPanel1
            // 
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
            this.extFieldPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extFieldPanel1.ItemHeight = 23;
            this.extFieldPanel1.ItemMargin = new System.Windows.Forms.Padding(5);
            this.extFieldPanel1.ItemValueWidth = 150;
            this.extFieldPanel1.Location = new System.Drawing.Point(3, 3);
            this.extFieldPanel1.Name = "extFieldPanel1";
            this.extFieldPanel1.Size = new System.Drawing.Size(803, 99);
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
            this.extFieldControl1.NameCtrlWidth = 23;
            this.extFieldControl1.RequiredField = false;
            this.extFieldControl1.ShowError = false;
            this.extFieldControl1.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl1.TabIndex = 0;
            this.extFieldControl1.Value = null;
            this.extFieldControl1.ValueCtrlWidth = 150;
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
            this.extFieldControl2.Location = new System.Drawing.Point(188, 5);
            this.extFieldControl2.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl2.Name = "extFieldControl2";
            this.extFieldControl2.NameCtrlWidth = 23;
            this.extFieldControl2.RequiredField = false;
            this.extFieldControl2.ShowError = false;
            this.extFieldControl2.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl2.TabIndex = 0;
            this.extFieldControl2.Value = null;
            this.extFieldControl2.ValueCtrlWidth = 150;
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
            this.extFieldControl5.Location = new System.Drawing.Point(371, 5);
            this.extFieldControl5.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl5.Name = "extFieldControl5";
            this.extFieldControl5.NameCtrlWidth = 23;
            this.extFieldControl5.RequiredField = false;
            this.extFieldControl5.ShowError = false;
            this.extFieldControl5.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl5.TabIndex = 0;
            this.extFieldControl5.Value = null;
            this.extFieldControl5.ValueCtrlWidth = 150;
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
            this.extFieldControl6.Location = new System.Drawing.Point(554, 5);
            this.extFieldControl6.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl6.Name = "extFieldControl6";
            this.extFieldControl6.NameCtrlWidth = 23;
            this.extFieldControl6.RequiredField = false;
            this.extFieldControl6.ShowError = false;
            this.extFieldControl6.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl6.TabIndex = 0;
            this.extFieldControl6.Value = null;
            this.extFieldControl6.ValueCtrlWidth = 150;
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
            this.extFieldControl4.Location = new System.Drawing.Point(5, 38);
            this.extFieldControl4.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl4.Name = "extFieldControl4";
            this.extFieldControl4.NameCtrlWidth = 23;
            this.extFieldControl4.RequiredField = false;
            this.extFieldControl4.ShowError = false;
            this.extFieldControl4.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl4.TabIndex = 0;
            this.extFieldControl4.Value = null;
            this.extFieldControl4.ValueCtrlWidth = 150;
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
            this.extFieldControl3.Location = new System.Drawing.Point(188, 38);
            this.extFieldControl3.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl3.Name = "extFieldControl3";
            this.extFieldControl3.NameCtrlWidth = 23;
            this.extFieldControl3.RequiredField = false;
            this.extFieldControl3.ShowError = false;
            this.extFieldControl3.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl3.TabIndex = 0;
            this.extFieldControl3.Value = null;
            this.extFieldControl3.ValueCtrlWidth = 150;
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
            this.extFieldControl7.Location = new System.Drawing.Point(371, 38);
            this.extFieldControl7.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl7.Name = "extFieldControl7";
            this.extFieldControl7.NameCtrlWidth = 23;
            this.extFieldControl7.RequiredField = false;
            this.extFieldControl7.ShowError = false;
            this.extFieldControl7.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl7.TabIndex = 0;
            this.extFieldControl7.Value = null;
            this.extFieldControl7.ValueCtrlWidth = 150;
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
            this.extFieldControl9.Location = new System.Drawing.Point(554, 38);
            this.extFieldControl9.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl9.Name = "extFieldControl9";
            this.extFieldControl9.NameCtrlWidth = 23;
            this.extFieldControl9.RequiredField = false;
            this.extFieldControl9.ShowError = false;
            this.extFieldControl9.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl9.TabIndex = 0;
            this.extFieldControl9.Value = null;
            this.extFieldControl9.ValueCtrlWidth = 150;
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
            this.extFieldControl8.Location = new System.Drawing.Point(5, 71);
            this.extFieldControl8.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl8.Name = "extFieldControl8";
            this.extFieldControl8.NameCtrlWidth = 23;
            this.extFieldControl8.RequiredField = false;
            this.extFieldControl8.ShowError = false;
            this.extFieldControl8.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl8.TabIndex = 0;
            this.extFieldControl8.Value = null;
            this.extFieldControl8.ValueCtrlWidth = 150;
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Maroon;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.extFieldPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.extFieldPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.extFieldPanel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 41);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(809, 315);
            this.tableLayoutPanel1.TabIndex = 4;
            this.tableLayoutPanel1.Resize += new System.EventHandler(this.tableLayoutPanel1_Resize);
            // 
            // extFieldPanel3
            // 
            this.extFieldPanel3.AutoPlacement = true;
            this.extFieldPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.extFieldPanel3.Controls.Add(this.extFieldControl19);
            this.extFieldPanel3.Controls.Add(this.extFieldControl20);
            this.extFieldPanel3.Controls.Add(this.extFieldControl21);
            this.extFieldPanel3.Controls.Add(this.extFieldControl22);
            this.extFieldPanel3.Controls.Add(this.extFieldControl23);
            this.extFieldPanel3.Controls.Add(this.extFieldControl24);
            this.extFieldPanel3.Controls.Add(this.extFieldControl25);
            this.extFieldPanel3.Controls.Add(this.extFieldControl26);
            this.extFieldPanel3.Controls.Add(this.extFieldControl27);
            this.extFieldPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extFieldPanel3.ItemHeight = 23;
            this.extFieldPanel3.ItemMargin = new System.Windows.Forms.Padding(5);
            this.extFieldPanel3.ItemValueWidth = 150;
            this.extFieldPanel3.Location = new System.Drawing.Point(3, 213);
            this.extFieldPanel3.Name = "extFieldPanel3";
            this.extFieldPanel3.Size = new System.Drawing.Size(803, 99);
            this.extFieldPanel3.TabIndex = 5;
            // 
            // extFieldControl19
            // 
            this.extFieldControl19.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl19.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl19.BorderWidth = 0;
            this.extFieldControl19.Content = null;
            this.extFieldControl19.ContentTypeName = null;
            this.extFieldControl19.ContentTypeParameter = null;
            this.extFieldControl19.CornerRadiu = 5;
            this.extFieldControl19.DisplayName = "A";
            this.extFieldControl19.DisplayValue = "";
            this.extFieldControl19.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl19.FieldName = null;
            this.extFieldControl19.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl19.InputtingVerifyCondition = null;
            this.extFieldControl19.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl19.Location = new System.Drawing.Point(5, 5);
            this.extFieldControl19.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl19.Name = "extFieldControl19";
            this.extFieldControl19.NameCtrlWidth = 23;
            this.extFieldControl19.RequiredField = false;
            this.extFieldControl19.ShowError = false;
            this.extFieldControl19.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl19.TabIndex = 0;
            this.extFieldControl19.Value = null;
            this.extFieldControl19.ValueCtrlWidth = 150;
            this.extFieldControl19.VerifyCondition = null;
            this.extFieldControl19.VerifyType = null;
            this.extFieldControl19.VerifyTypeName = null;
            // 
            // extFieldControl20
            // 
            this.extFieldControl20.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl20.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl20.BorderWidth = 0;
            this.extFieldControl20.Content = null;
            this.extFieldControl20.ContentTypeName = null;
            this.extFieldControl20.ContentTypeParameter = null;
            this.extFieldControl20.CornerRadiu = 5;
            this.extFieldControl20.DisplayName = "B";
            this.extFieldControl20.DisplayValue = "";
            this.extFieldControl20.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl20.FieldName = null;
            this.extFieldControl20.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl20.InputtingVerifyCondition = null;
            this.extFieldControl20.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl20.Location = new System.Drawing.Point(188, 5);
            this.extFieldControl20.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl20.Name = "extFieldControl20";
            this.extFieldControl20.NameCtrlWidth = 23;
            this.extFieldControl20.RequiredField = false;
            this.extFieldControl20.ShowError = false;
            this.extFieldControl20.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl20.TabIndex = 0;
            this.extFieldControl20.Value = null;
            this.extFieldControl20.ValueCtrlWidth = 150;
            this.extFieldControl20.VerifyCondition = null;
            this.extFieldControl20.VerifyType = null;
            this.extFieldControl20.VerifyTypeName = null;
            // 
            // extFieldControl21
            // 
            this.extFieldControl21.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl21.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl21.BorderWidth = 0;
            this.extFieldControl21.Content = null;
            this.extFieldControl21.ContentTypeName = null;
            this.extFieldControl21.ContentTypeParameter = null;
            this.extFieldControl21.CornerRadiu = 5;
            this.extFieldControl21.DisplayName = "C";
            this.extFieldControl21.DisplayValue = "";
            this.extFieldControl21.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl21.FieldName = null;
            this.extFieldControl21.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl21.InputtingVerifyCondition = null;
            this.extFieldControl21.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl21.Location = new System.Drawing.Point(371, 5);
            this.extFieldControl21.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl21.Name = "extFieldControl21";
            this.extFieldControl21.NameCtrlWidth = 23;
            this.extFieldControl21.RequiredField = false;
            this.extFieldControl21.ShowError = false;
            this.extFieldControl21.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl21.TabIndex = 0;
            this.extFieldControl21.Value = null;
            this.extFieldControl21.ValueCtrlWidth = 150;
            this.extFieldControl21.VerifyCondition = null;
            this.extFieldControl21.VerifyType = null;
            this.extFieldControl21.VerifyTypeName = null;
            // 
            // extFieldControl22
            // 
            this.extFieldControl22.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl22.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl22.BorderWidth = 0;
            this.extFieldControl22.Content = null;
            this.extFieldControl22.ContentTypeName = null;
            this.extFieldControl22.ContentTypeParameter = null;
            this.extFieldControl22.CornerRadiu = 5;
            this.extFieldControl22.DisplayName = "D";
            this.extFieldControl22.DisplayValue = "";
            this.extFieldControl22.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl22.FieldName = null;
            this.extFieldControl22.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl22.InputtingVerifyCondition = null;
            this.extFieldControl22.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl22.Location = new System.Drawing.Point(554, 5);
            this.extFieldControl22.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl22.Name = "extFieldControl22";
            this.extFieldControl22.NameCtrlWidth = 23;
            this.extFieldControl22.RequiredField = false;
            this.extFieldControl22.ShowError = false;
            this.extFieldControl22.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl22.TabIndex = 0;
            this.extFieldControl22.Value = null;
            this.extFieldControl22.ValueCtrlWidth = 150;
            this.extFieldControl22.VerifyCondition = null;
            this.extFieldControl22.VerifyType = null;
            this.extFieldControl22.VerifyTypeName = null;
            // 
            // extFieldControl23
            // 
            this.extFieldControl23.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl23.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl23.BorderWidth = 0;
            this.extFieldControl23.Content = null;
            this.extFieldControl23.ContentTypeName = null;
            this.extFieldControl23.ContentTypeParameter = null;
            this.extFieldControl23.CornerRadiu = 5;
            this.extFieldControl23.DisplayName = "E";
            this.extFieldControl23.DisplayValue = "";
            this.extFieldControl23.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl23.FieldName = null;
            this.extFieldControl23.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl23.InputtingVerifyCondition = null;
            this.extFieldControl23.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl23.Location = new System.Drawing.Point(5, 38);
            this.extFieldControl23.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl23.Name = "extFieldControl23";
            this.extFieldControl23.NameCtrlWidth = 23;
            this.extFieldControl23.RequiredField = false;
            this.extFieldControl23.ShowError = false;
            this.extFieldControl23.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl23.TabIndex = 0;
            this.extFieldControl23.Value = null;
            this.extFieldControl23.ValueCtrlWidth = 150;
            this.extFieldControl23.VerifyCondition = null;
            this.extFieldControl23.VerifyType = null;
            this.extFieldControl23.VerifyTypeName = null;
            // 
            // extFieldControl24
            // 
            this.extFieldControl24.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl24.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl24.BorderWidth = 0;
            this.extFieldControl24.Content = null;
            this.extFieldControl24.ContentTypeName = null;
            this.extFieldControl24.ContentTypeParameter = null;
            this.extFieldControl24.CornerRadiu = 5;
            this.extFieldControl24.DisplayName = "F";
            this.extFieldControl24.DisplayValue = "";
            this.extFieldControl24.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl24.FieldName = null;
            this.extFieldControl24.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl24.InputtingVerifyCondition = null;
            this.extFieldControl24.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl24.Location = new System.Drawing.Point(188, 38);
            this.extFieldControl24.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl24.Name = "extFieldControl24";
            this.extFieldControl24.NameCtrlWidth = 23;
            this.extFieldControl24.RequiredField = false;
            this.extFieldControl24.ShowError = false;
            this.extFieldControl24.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl24.TabIndex = 0;
            this.extFieldControl24.Value = null;
            this.extFieldControl24.ValueCtrlWidth = 150;
            this.extFieldControl24.VerifyCondition = null;
            this.extFieldControl24.VerifyType = null;
            this.extFieldControl24.VerifyTypeName = null;
            // 
            // extFieldControl25
            // 
            this.extFieldControl25.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl25.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl25.BorderWidth = 0;
            this.extFieldControl25.Content = null;
            this.extFieldControl25.ContentTypeName = null;
            this.extFieldControl25.ContentTypeParameter = null;
            this.extFieldControl25.CornerRadiu = 5;
            this.extFieldControl25.DisplayName = "G";
            this.extFieldControl25.DisplayValue = "";
            this.extFieldControl25.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl25.FieldName = null;
            this.extFieldControl25.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl25.InputtingVerifyCondition = null;
            this.extFieldControl25.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl25.Location = new System.Drawing.Point(371, 38);
            this.extFieldControl25.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl25.Name = "extFieldControl25";
            this.extFieldControl25.NameCtrlWidth = 23;
            this.extFieldControl25.RequiredField = false;
            this.extFieldControl25.ShowError = false;
            this.extFieldControl25.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl25.TabIndex = 0;
            this.extFieldControl25.Value = null;
            this.extFieldControl25.ValueCtrlWidth = 150;
            this.extFieldControl25.VerifyCondition = null;
            this.extFieldControl25.VerifyType = null;
            this.extFieldControl25.VerifyTypeName = null;
            // 
            // extFieldControl26
            // 
            this.extFieldControl26.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl26.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl26.BorderWidth = 0;
            this.extFieldControl26.Content = null;
            this.extFieldControl26.ContentTypeName = null;
            this.extFieldControl26.ContentTypeParameter = null;
            this.extFieldControl26.CornerRadiu = 5;
            this.extFieldControl26.DisplayName = "H";
            this.extFieldControl26.DisplayValue = "";
            this.extFieldControl26.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl26.FieldName = null;
            this.extFieldControl26.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl26.InputtingVerifyCondition = null;
            this.extFieldControl26.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl26.Location = new System.Drawing.Point(554, 38);
            this.extFieldControl26.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl26.Name = "extFieldControl26";
            this.extFieldControl26.NameCtrlWidth = 23;
            this.extFieldControl26.RequiredField = false;
            this.extFieldControl26.ShowError = false;
            this.extFieldControl26.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl26.TabIndex = 0;
            this.extFieldControl26.Value = null;
            this.extFieldControl26.ValueCtrlWidth = 150;
            this.extFieldControl26.VerifyCondition = null;
            this.extFieldControl26.VerifyType = null;
            this.extFieldControl26.VerifyTypeName = null;
            // 
            // extFieldControl27
            // 
            this.extFieldControl27.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl27.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl27.BorderWidth = 0;
            this.extFieldControl27.Content = null;
            this.extFieldControl27.ContentTypeName = null;
            this.extFieldControl27.ContentTypeParameter = null;
            this.extFieldControl27.CornerRadiu = 5;
            this.extFieldControl27.DisplayName = "I";
            this.extFieldControl27.DisplayValue = "";
            this.extFieldControl27.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl27.FieldName = null;
            this.extFieldControl27.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl27.InputtingVerifyCondition = null;
            this.extFieldControl27.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl27.Location = new System.Drawing.Point(5, 71);
            this.extFieldControl27.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl27.Name = "extFieldControl27";
            this.extFieldControl27.NameCtrlWidth = 23;
            this.extFieldControl27.RequiredField = false;
            this.extFieldControl27.ShowError = false;
            this.extFieldControl27.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl27.TabIndex = 0;
            this.extFieldControl27.Value = null;
            this.extFieldControl27.ValueCtrlWidth = 150;
            this.extFieldControl27.VerifyCondition = null;
            this.extFieldControl27.VerifyType = null;
            this.extFieldControl27.VerifyTypeName = null;
            // 
            // extFieldPanel2
            // 
            this.extFieldPanel2.AutoPlacement = true;
            this.extFieldPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.extFieldPanel2.Controls.Add(this.extFieldControl10);
            this.extFieldPanel2.Controls.Add(this.extFieldControl11);
            this.extFieldPanel2.Controls.Add(this.extFieldControl12);
            this.extFieldPanel2.Controls.Add(this.extFieldControl13);
            this.extFieldPanel2.Controls.Add(this.extFieldControl14);
            this.extFieldPanel2.Controls.Add(this.extFieldControl15);
            this.extFieldPanel2.Controls.Add(this.extFieldControl16);
            this.extFieldPanel2.Controls.Add(this.extFieldControl17);
            this.extFieldPanel2.Controls.Add(this.extFieldControl18);
            this.extFieldPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extFieldPanel2.ItemHeight = 23;
            this.extFieldPanel2.ItemMargin = new System.Windows.Forms.Padding(5);
            this.extFieldPanel2.ItemValueWidth = 150;
            this.extFieldPanel2.Location = new System.Drawing.Point(3, 108);
            this.extFieldPanel2.Name = "extFieldPanel2";
            this.extFieldPanel2.Size = new System.Drawing.Size(803, 99);
            this.extFieldPanel2.TabIndex = 4;
            // 
            // extFieldControl10
            // 
            this.extFieldControl10.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl10.BorderWidth = 0;
            this.extFieldControl10.Content = null;
            this.extFieldControl10.ContentTypeName = null;
            this.extFieldControl10.ContentTypeParameter = null;
            this.extFieldControl10.CornerRadiu = 5;
            this.extFieldControl10.DisplayName = "A";
            this.extFieldControl10.DisplayValue = "";
            this.extFieldControl10.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl10.FieldName = null;
            this.extFieldControl10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl10.InputtingVerifyCondition = null;
            this.extFieldControl10.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl10.Location = new System.Drawing.Point(5, 5);
            this.extFieldControl10.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl10.Name = "extFieldControl10";
            this.extFieldControl10.NameCtrlWidth = 23;
            this.extFieldControl10.RequiredField = false;
            this.extFieldControl10.ShowError = false;
            this.extFieldControl10.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl10.TabIndex = 0;
            this.extFieldControl10.Value = null;
            this.extFieldControl10.ValueCtrlWidth = 150;
            this.extFieldControl10.VerifyCondition = null;
            this.extFieldControl10.VerifyType = null;
            this.extFieldControl10.VerifyTypeName = null;
            // 
            // extFieldControl11
            // 
            this.extFieldControl11.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl11.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl11.BorderWidth = 0;
            this.extFieldControl11.Content = null;
            this.extFieldControl11.ContentTypeName = null;
            this.extFieldControl11.ContentTypeParameter = null;
            this.extFieldControl11.CornerRadiu = 5;
            this.extFieldControl11.DisplayName = "B";
            this.extFieldControl11.DisplayValue = "";
            this.extFieldControl11.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl11.FieldName = null;
            this.extFieldControl11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl11.InputtingVerifyCondition = null;
            this.extFieldControl11.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl11.Location = new System.Drawing.Point(188, 5);
            this.extFieldControl11.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl11.Name = "extFieldControl11";
            this.extFieldControl11.NameCtrlWidth = 23;
            this.extFieldControl11.RequiredField = false;
            this.extFieldControl11.ShowError = false;
            this.extFieldControl11.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl11.TabIndex = 0;
            this.extFieldControl11.Value = null;
            this.extFieldControl11.ValueCtrlWidth = 150;
            this.extFieldControl11.VerifyCondition = null;
            this.extFieldControl11.VerifyType = null;
            this.extFieldControl11.VerifyTypeName = null;
            // 
            // extFieldControl12
            // 
            this.extFieldControl12.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl12.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl12.BorderWidth = 0;
            this.extFieldControl12.Content = null;
            this.extFieldControl12.ContentTypeName = null;
            this.extFieldControl12.ContentTypeParameter = null;
            this.extFieldControl12.CornerRadiu = 5;
            this.extFieldControl12.DisplayName = "C";
            this.extFieldControl12.DisplayValue = "";
            this.extFieldControl12.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl12.FieldName = null;
            this.extFieldControl12.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl12.InputtingVerifyCondition = null;
            this.extFieldControl12.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl12.Location = new System.Drawing.Point(371, 5);
            this.extFieldControl12.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl12.Name = "extFieldControl12";
            this.extFieldControl12.NameCtrlWidth = 23;
            this.extFieldControl12.RequiredField = false;
            this.extFieldControl12.ShowError = false;
            this.extFieldControl12.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl12.TabIndex = 0;
            this.extFieldControl12.Value = null;
            this.extFieldControl12.ValueCtrlWidth = 150;
            this.extFieldControl12.VerifyCondition = null;
            this.extFieldControl12.VerifyType = null;
            this.extFieldControl12.VerifyTypeName = null;
            // 
            // extFieldControl13
            // 
            this.extFieldControl13.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl13.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl13.BorderWidth = 0;
            this.extFieldControl13.Content = null;
            this.extFieldControl13.ContentTypeName = null;
            this.extFieldControl13.ContentTypeParameter = null;
            this.extFieldControl13.CornerRadiu = 5;
            this.extFieldControl13.DisplayName = "D";
            this.extFieldControl13.DisplayValue = "";
            this.extFieldControl13.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl13.FieldName = null;
            this.extFieldControl13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl13.InputtingVerifyCondition = null;
            this.extFieldControl13.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl13.Location = new System.Drawing.Point(554, 5);
            this.extFieldControl13.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl13.Name = "extFieldControl13";
            this.extFieldControl13.NameCtrlWidth = 23;
            this.extFieldControl13.RequiredField = false;
            this.extFieldControl13.ShowError = false;
            this.extFieldControl13.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl13.TabIndex = 0;
            this.extFieldControl13.Value = null;
            this.extFieldControl13.ValueCtrlWidth = 150;
            this.extFieldControl13.VerifyCondition = null;
            this.extFieldControl13.VerifyType = null;
            this.extFieldControl13.VerifyTypeName = null;
            // 
            // extFieldControl14
            // 
            this.extFieldControl14.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl14.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl14.BorderWidth = 0;
            this.extFieldControl14.Content = null;
            this.extFieldControl14.ContentTypeName = null;
            this.extFieldControl14.ContentTypeParameter = null;
            this.extFieldControl14.CornerRadiu = 5;
            this.extFieldControl14.DisplayName = "E";
            this.extFieldControl14.DisplayValue = "";
            this.extFieldControl14.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl14.FieldName = null;
            this.extFieldControl14.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl14.InputtingVerifyCondition = null;
            this.extFieldControl14.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl14.Location = new System.Drawing.Point(5, 38);
            this.extFieldControl14.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl14.Name = "extFieldControl14";
            this.extFieldControl14.NameCtrlWidth = 23;
            this.extFieldControl14.RequiredField = false;
            this.extFieldControl14.ShowError = false;
            this.extFieldControl14.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl14.TabIndex = 0;
            this.extFieldControl14.Value = null;
            this.extFieldControl14.ValueCtrlWidth = 150;
            this.extFieldControl14.VerifyCondition = null;
            this.extFieldControl14.VerifyType = null;
            this.extFieldControl14.VerifyTypeName = null;
            // 
            // extFieldControl15
            // 
            this.extFieldControl15.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl15.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl15.BorderWidth = 0;
            this.extFieldControl15.Content = null;
            this.extFieldControl15.ContentTypeName = null;
            this.extFieldControl15.ContentTypeParameter = null;
            this.extFieldControl15.CornerRadiu = 5;
            this.extFieldControl15.DisplayName = "F";
            this.extFieldControl15.DisplayValue = "";
            this.extFieldControl15.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl15.FieldName = null;
            this.extFieldControl15.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl15.InputtingVerifyCondition = null;
            this.extFieldControl15.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl15.Location = new System.Drawing.Point(188, 38);
            this.extFieldControl15.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl15.Name = "extFieldControl15";
            this.extFieldControl15.NameCtrlWidth = 23;
            this.extFieldControl15.RequiredField = false;
            this.extFieldControl15.ShowError = false;
            this.extFieldControl15.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl15.TabIndex = 0;
            this.extFieldControl15.Value = null;
            this.extFieldControl15.ValueCtrlWidth = 150;
            this.extFieldControl15.VerifyCondition = null;
            this.extFieldControl15.VerifyType = null;
            this.extFieldControl15.VerifyTypeName = null;
            // 
            // extFieldControl16
            // 
            this.extFieldControl16.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl16.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl16.BorderWidth = 0;
            this.extFieldControl16.Content = null;
            this.extFieldControl16.ContentTypeName = null;
            this.extFieldControl16.ContentTypeParameter = null;
            this.extFieldControl16.CornerRadiu = 5;
            this.extFieldControl16.DisplayName = "G";
            this.extFieldControl16.DisplayValue = "";
            this.extFieldControl16.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl16.FieldName = null;
            this.extFieldControl16.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl16.InputtingVerifyCondition = null;
            this.extFieldControl16.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl16.Location = new System.Drawing.Point(371, 38);
            this.extFieldControl16.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl16.Name = "extFieldControl16";
            this.extFieldControl16.NameCtrlWidth = 23;
            this.extFieldControl16.RequiredField = false;
            this.extFieldControl16.ShowError = false;
            this.extFieldControl16.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl16.TabIndex = 0;
            this.extFieldControl16.Value = null;
            this.extFieldControl16.ValueCtrlWidth = 150;
            this.extFieldControl16.VerifyCondition = null;
            this.extFieldControl16.VerifyType = null;
            this.extFieldControl16.VerifyTypeName = null;
            // 
            // extFieldControl17
            // 
            this.extFieldControl17.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl17.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl17.BorderWidth = 0;
            this.extFieldControl17.Content = null;
            this.extFieldControl17.ContentTypeName = null;
            this.extFieldControl17.ContentTypeParameter = null;
            this.extFieldControl17.CornerRadiu = 5;
            this.extFieldControl17.DisplayName = "H";
            this.extFieldControl17.DisplayValue = "";
            this.extFieldControl17.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl17.FieldName = null;
            this.extFieldControl17.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl17.InputtingVerifyCondition = null;
            this.extFieldControl17.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl17.Location = new System.Drawing.Point(554, 38);
            this.extFieldControl17.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl17.Name = "extFieldControl17";
            this.extFieldControl17.NameCtrlWidth = 23;
            this.extFieldControl17.RequiredField = false;
            this.extFieldControl17.ShowError = false;
            this.extFieldControl17.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl17.TabIndex = 0;
            this.extFieldControl17.Value = null;
            this.extFieldControl17.ValueCtrlWidth = 150;
            this.extFieldControl17.VerifyCondition = null;
            this.extFieldControl17.VerifyType = null;
            this.extFieldControl17.VerifyTypeName = null;
            // 
            // extFieldControl18
            // 
            this.extFieldControl18.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl18.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl18.BorderWidth = 0;
            this.extFieldControl18.Content = null;
            this.extFieldControl18.ContentTypeName = null;
            this.extFieldControl18.ContentTypeParameter = null;
            this.extFieldControl18.CornerRadiu = 5;
            this.extFieldControl18.DisplayName = "I";
            this.extFieldControl18.DisplayValue = "";
            this.extFieldControl18.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl18.FieldName = null;
            this.extFieldControl18.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extFieldControl18.InputtingVerifyCondition = null;
            this.extFieldControl18.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl18.Location = new System.Drawing.Point(5, 71);
            this.extFieldControl18.Margin = new System.Windows.Forms.Padding(5);
            this.extFieldControl18.Name = "extFieldControl18";
            this.extFieldControl18.NameCtrlWidth = 23;
            this.extFieldControl18.RequiredField = false;
            this.extFieldControl18.ShowError = false;
            this.extFieldControl18.Size = new System.Drawing.Size(173, 23);
            this.extFieldControl18.TabIndex = 0;
            this.extFieldControl18.Value = null;
            this.extFieldControl18.ValueCtrlWidth = 150;
            this.extFieldControl18.VerifyCondition = null;
            this.extFieldControl18.VerifyType = null;
            this.extFieldControl18.VerifyTypeName = null;
            // 
            // extButton2
            // 
            this.extButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.extButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("extButton2.BackgroundImage")));
            this.extButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.extButton2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extButton2.BorderWidth = 0;
            this.extButton2.Content = null;
            this.extButton2.ContentTypeName = null;
            this.extButton2.ContentTypeParameter = null;
            this.extButton2.CornerRadiu = 0;
            this.extButton2.DisplayValue = "+";
            this.extButton2.InputtingVerifyCondition = null;
            this.extButton2.LightBackgroudImage = ((System.Drawing.Image)(resources.GetObject("extButton2.LightBackgroudImage")));
            this.extButton2.Location = new System.Drawing.Point(491, 12);
            this.extButton2.Name = "extButton2";
            this.extButton2.NormalBackgroudImage = ((System.Drawing.Image)(resources.GetObject("extButton2.NormalBackgroudImage")));
            this.extButton2.ShowError = false;
            this.extButton2.Size = new System.Drawing.Size(75, 23);
            this.extButton2.TabIndex = 5;
            this.extButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.extButton2.Value = "+";
            this.extButton2.VerifyCondition = null;
            this.extButton2.VerifyType = null;
            this.extButton2.VerifyTypeName = null;
            this.extButton2.Click += new System.EventHandler(this.extButton2_Click);
            // 
            // extButton3
            // 
            this.extButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.extButton3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("extButton3.BackgroundImage")));
            this.extButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.extButton3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extButton3.BorderWidth = 0;
            this.extButton3.Content = null;
            this.extButton3.ContentTypeName = null;
            this.extButton3.ContentTypeParameter = null;
            this.extButton3.CornerRadiu = 0;
            this.extButton3.DisplayValue = "-";
            this.extButton3.InputtingVerifyCondition = null;
            this.extButton3.LightBackgroudImage = ((System.Drawing.Image)(resources.GetObject("extButton3.LightBackgroudImage")));
            this.extButton3.Location = new System.Drawing.Point(572, 12);
            this.extButton3.Name = "extButton3";
            this.extButton3.NormalBackgroudImage = ((System.Drawing.Image)(resources.GetObject("extButton3.NormalBackgroudImage")));
            this.extButton3.ShowError = false;
            this.extButton3.Size = new System.Drawing.Size(75, 23);
            this.extButton3.TabIndex = 6;
            this.extButton3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.extButton3.Value = "-";
            this.extButton3.VerifyCondition = null;
            this.extButton3.VerifyType = null;
            this.extButton3.VerifyTypeName = null;
            this.extButton3.Click += new System.EventHandler(this.extButton3_Click);
            // 
            // Testbed4UILibrary
            // 
            this.ClientSize = new System.Drawing.Size(834, 366);
            this.Controls.Add(this.extButton3);
            this.Controls.Add(this.extButton2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.extButton1);
            this.Controls.Add(this.extComboBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Testbed4UILibrary";
            this.Load += new System.EventHandler(this.Testbed4UILibrary_Load);
            this.extFieldPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.extFieldPanel3.ResumeLayout(false);
            this.extFieldPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void extButton1_Click(object sender, EventArgs e)
        {
            extFieldControl7.Visible = false;
            //extFieldPanel1.Controls.Add(new ExtFieldControl());
            //MessageBox.Show(extComboBox1.Value.ToString() + "%%%%%%" + extComboBox1.DisplayValue);
        }

        private void extButton2_Click(object sender, EventArgs e)
        {
            Width += 2;
        }

        private void extButton3_Click(object sender, EventArgs e)
        {
            Width -= 2;
        }

        private void tableLayoutPanel1_Resize(object sender, EventArgs e)
        {
            Invalidate();
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


