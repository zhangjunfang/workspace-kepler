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
            //extTextBox1.DisplayValue = "1111111111111";
            //Process.Start("mstsc.exe");
        }

        private void InitializeComponent()
        {
            this.extSearchPanel1 = new HXC.UI.Library.ExtendControl.ExtSearchPanel();
            this.extFieldControl1 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl2 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extFieldControl3 = new HXC.UI.Library.Controls.ExtFieldControl();
            this.extTextBox1 = new HXC.UI.Library.Controls.ExtTextBox();
            this.extSearchPanel1.ContentPanel.SuspendLayout();
            this.extSearchPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // extSearchPanel1
            // 
            this.extSearchPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.extSearchPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extSearchPanel1.BorderWidth = 1;
            // 
            // extSearchPanel1.ContentPanel
            // 
            this.extSearchPanel1.ContentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extSearchPanel1.ContentPanel.AutoPlacement = true;
            this.extSearchPanel1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.extSearchPanel1.ContentPanel.Controls.Add(this.extFieldControl1);
            this.extSearchPanel1.ContentPanel.Controls.Add(this.extFieldControl2);
            this.extSearchPanel1.ContentPanel.Controls.Add(this.extFieldControl3);
            this.extSearchPanel1.ContentPanel.ItemHeight = 23;
            this.extSearchPanel1.ContentPanel.ItemValueWidth = 150;
            this.extSearchPanel1.ContentPanel.Location = new System.Drawing.Point(0, 19);
            this.extSearchPanel1.ContentPanel.Name = "ContentPanel";
            this.extSearchPanel1.ContentPanel.Padding = new System.Windows.Forms.Padding(5);
            this.extSearchPanel1.ContentPanel.Size = new System.Drawing.Size(732, 41);
            this.extSearchPanel1.ContentPanel.TabIndex = 2;
            this.extSearchPanel1.CornerRadiu = 5;
            this.extSearchPanel1.DisplayValue = "";
            this.extSearchPanel1.InputtingVerifyCondition = null;
            this.extSearchPanel1.Location = new System.Drawing.Point(21, 109);
            this.extSearchPanel1.Name = "extSearchPanel1";
            this.extSearchPanel1.ShowError = false;
            this.extSearchPanel1.Size = new System.Drawing.Size(817, 78);
            this.extSearchPanel1.TabIndex = 0;
            this.extSearchPanel1.Value = null;
            this.extSearchPanel1.VerifyCondition = null;
            this.extSearchPanel1.VerifyType = null;
            this.extSearchPanel1.VerifyTypeName = null;
            // 
            // extFieldControl1
            // 
            this.extFieldControl1.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl1.BorderWidth = 0;
            this.extFieldControl1.CornerRadiu = 5;
            this.extFieldControl1.DisplayName = "字段名称";
            this.extFieldControl1.DisplayValue = "";
            this.extFieldControl1.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl1.FieldName = null;
            this.extFieldControl1.InputtingVerifyCondition = null;
            this.extFieldControl1.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl1.Location = new System.Drawing.Point(8, 8);
            this.extFieldControl1.Name = "extFieldControl1";
            this.extFieldControl1.NameCtrlWidth = 63;
            this.extFieldControl1.ShowError = false;
            this.extFieldControl1.Size = new System.Drawing.Size(213, 23);
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
            this.extFieldControl2.CornerRadiu = 5;
            this.extFieldControl2.DisplayName = "字段名称";
            this.extFieldControl2.DisplayValue = "";
            this.extFieldControl2.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl2.FieldName = null;
            this.extFieldControl2.InputtingVerifyCondition = null;
            this.extFieldControl2.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl2.Location = new System.Drawing.Point(227, 8);
            this.extFieldControl2.Name = "extFieldControl2";
            this.extFieldControl2.NameCtrlWidth = 63;
            this.extFieldControl2.ShowError = false;
            this.extFieldControl2.Size = new System.Drawing.Size(213, 23);
            this.extFieldControl2.TabIndex = 1;
            this.extFieldControl2.Value = null;
            this.extFieldControl2.ValueCtrlWidth = 150;
            this.extFieldControl2.VerifyCondition = null;
            this.extFieldControl2.VerifyType = null;
            this.extFieldControl2.VerifyTypeName = null;
            // 
            // extFieldControl3
            // 
            this.extFieldControl3.BackColor = System.Drawing.Color.Transparent;
            this.extFieldControl3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extFieldControl3.BorderWidth = 0;
            this.extFieldControl3.CornerRadiu = 5;
            this.extFieldControl3.DisplayName = "字段名称";
            this.extFieldControl3.DisplayValue = "";
            this.extFieldControl3.FieldCtrlType = HXC.UI.Library.Controls.FieldCtrlType.TextBox;
            this.extFieldControl3.FieldName = null;
            this.extFieldControl3.InputtingVerifyCondition = null;
            this.extFieldControl3.JudgeSymbols = "like \'%{0}%\'";
            this.extFieldControl3.Location = new System.Drawing.Point(446, 8);
            this.extFieldControl3.Name = "extFieldControl3";
            this.extFieldControl3.NameCtrlWidth = 63;
            this.extFieldControl3.ShowError = false;
            this.extFieldControl3.Size = new System.Drawing.Size(213, 23);
            this.extFieldControl3.TabIndex = 2;
            this.extFieldControl3.Value = null;
            this.extFieldControl3.ValueCtrlWidth = 150;
            this.extFieldControl3.VerifyCondition = null;
            this.extFieldControl3.VerifyType = null;
            this.extFieldControl3.VerifyTypeName = null;
            // 
            // extTextBox1
            // 
            this.extTextBox1.BackColor = System.Drawing.Color.White;
            this.extTextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(211)))), ((int)(((byte)(254)))));
            this.extTextBox1.BorderWidth = 1;
            this.extTextBox1.CornerRadiu = 5;
            this.extTextBox1.DisplayValue = "";
            this.extTextBox1.InputtingVerifyCondition = null;
            this.extTextBox1.Location = new System.Drawing.Point(445, 48);
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
            this.ClientSize = new System.Drawing.Size(912, 482);
            this.Controls.Add(this.extSearchPanel1);
            this.Name = "Testbed4UILibrary";
            this.Load += new System.EventHandler(this.Testbed4UILibrary_Load);
            this.extSearchPanel1.ContentPanel.ResumeLayout(false);
            this.extSearchPanel1.ResumeLayout(false);
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
}


