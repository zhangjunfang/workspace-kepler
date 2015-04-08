namespace HXC.UI.Library.Controls
{
    partial class ExtTextBox
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
            this.txt_textbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txt_textbox
            // 
            this.txt_textbox.AccessibleName = "0";
            this.txt_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_textbox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_textbox.Location = new System.Drawing.Point(3, 3);
            this.txt_textbox.Name = "txt_textbox";
            this.txt_textbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_textbox.Size = new System.Drawing.Size(200, 16);
            this.txt_textbox.TabIndex = 2;
            // 
            // ExtTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.txt_textbox);
            this.Name = "ExtTextBox";
            this.Size = new System.Drawing.Size(206, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_textbox;

    }
}
