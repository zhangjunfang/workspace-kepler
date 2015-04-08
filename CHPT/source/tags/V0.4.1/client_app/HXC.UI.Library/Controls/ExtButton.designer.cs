namespace HXC.UI.Library.Controls
{
    partial class ExtButton
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_label = new HXC.UI.Library.Controls.ExtLabel();
            this.SuspendLayout();
            // 
            // lbl_label
            // 
            this.lbl_label.BackColor = System.Drawing.Color.Transparent;
            this.lbl_label.DisplayValue = "Button";
            this.lbl_label.InputtingVerifyCondition = null;
            this.lbl_label.Location = new System.Drawing.Point(0, 0);
            this.lbl_label.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_label.Name = "lbl_label";
            this.lbl_label.ShowError = false;
            this.lbl_label.Size = new System.Drawing.Size(75, 23);
            this.lbl_label.TabIndex = 1;
            this.lbl_label.Text = "Button";
            this.lbl_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_label.Value = "Button";
            this.lbl_label.VerifyCondition = null;
            this.lbl_label.VerifyType = null;
            this.lbl_label.VerifyTypeName = null;
            // 
            // ExtButton
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.lbl_label);
            this.Name = "ExtButton";
            this.Size = new System.Drawing.Size(75, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtLabel lbl_label;



    }
}
