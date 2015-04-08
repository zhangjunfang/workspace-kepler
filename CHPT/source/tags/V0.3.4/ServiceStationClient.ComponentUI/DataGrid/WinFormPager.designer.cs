using System.Windows.Forms;
namespace ServiceStationClient.ComponentUI
{
    partial class WinFormPager
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinFormPager));
            this.imglstPager = new System.Windows.Forms.ImageList(this.components);
            this.cbPageSize = new System.Windows.Forms.ComboBox();
            this.btnToPageIndex = new System.Windows.Forms.Button();
            this.lbEnd = new System.Windows.Forms.Label();
            this.txtToPageIndex = new System.Windows.Forms.NumericUpDown();
            this.lbPre = new System.Windows.Forms.Label();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.lblPager = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtToPageIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // imglstPager
            // 
            this.imglstPager.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstPager.ImageStream")));
            this.imglstPager.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstPager.Images.SetKeyName(0, "first.gif");
            this.imglstPager.Images.SetKeyName(1, "prev.gif");
            this.imglstPager.Images.SetKeyName(2, "next.gif");
            this.imglstPager.Images.SetKeyName(3, "last.gif");
            this.imglstPager.Images.SetKeyName(4, "firstdisable.gif");
            this.imglstPager.Images.SetKeyName(5, "prevdisable.gif");
            this.imglstPager.Images.SetKeyName(6, "nextdisable.gif");
            this.imglstPager.Images.SetKeyName(7, "lastdisable.gif");
            // 
            // cbPageSize
            // 
            this.cbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPageSize.FormattingEnabled = true;
            this.cbPageSize.Items.AddRange(new object[] {
            "10",
            "15",
            "20",
            "35",
            "50",
            "100"});
            this.cbPageSize.Location = new System.Drawing.Point(142, 6);
            this.cbPageSize.Name = "cbPageSize";
            this.cbPageSize.Size = new System.Drawing.Size(46, 20);
            this.cbPageSize.TabIndex = 29;
            this.cbPageSize.SelectedIndexChanged += new System.EventHandler(this.cbPageSize_SelectedIndexChanged);
            // 
            // btnToPageIndex
            // 
            this.btnToPageIndex.Location = new System.Drawing.Point(414, 4);
            this.btnToPageIndex.Name = "btnToPageIndex";
            this.btnToPageIndex.Size = new System.Drawing.Size(44, 23);
            this.btnToPageIndex.TabIndex = 28;
            this.btnToPageIndex.Text = "跳转";
            this.btnToPageIndex.UseVisualStyleBackColor = true;
            this.btnToPageIndex.Click += new System.EventHandler(this.btnToPageIndex_Click);
            // 
            // lbEnd
            // 
            this.lbEnd.AutoSize = true;
            this.lbEnd.Location = new System.Drawing.Point(400, 9);
            this.lbEnd.Name = "lbEnd";
            this.lbEnd.Size = new System.Drawing.Size(17, 12);
            this.lbEnd.TabIndex = 27;
            this.lbEnd.Text = "页";
            // 
            // txtToPageIndex
            // 
            this.txtToPageIndex.Location = new System.Drawing.Point(360, 5);
            this.txtToPageIndex.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.txtToPageIndex.Name = "txtToPageIndex";
            this.txtToPageIndex.Size = new System.Drawing.Size(40, 21);
            this.txtToPageIndex.TabIndex = 26;
            this.txtToPageIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtToPageIndex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbPre
            // 
            this.lbPre.AutoSize = true;
            this.lbPre.Location = new System.Drawing.Point(344, 9);
            this.lbPre.Name = "lbPre";
            this.lbPre.Size = new System.Drawing.Size(17, 12);
            this.lbPre.TabIndex = 25;
            this.lbPre.Text = "第";
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(310, 4);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(35, 23);
            this.btnLast.TabIndex = 24;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(272, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(35, 23);
            this.btnNext.TabIndex = 23;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(234, 4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(35, 23);
            this.btnPrevious.TabIndex = 22;
            this.btnPrevious.Text = "<";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(196, 4);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(35, 23);
            this.btnFirst.TabIndex = 21;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // lblPager
            // 
            this.lblPager.AutoSize = true;
            this.lblPager.Location = new System.Drawing.Point(0, 10);
            this.lblPager.Name = "lblPager";
            this.lblPager.Size = new System.Drawing.Size(137, 12);
            this.lblPager.TabIndex = 20;
            this.lblPager.Text = "当前{1}/{2}页 总数:{0}";
            // 
            // WinFormPager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.cbPageSize);
            this.Controls.Add(this.btnToPageIndex);
            this.Controls.Add(this.lbEnd);
            this.Controls.Add(this.txtToPageIndex);
            this.Controls.Add(this.lbPre);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.lblPager);
            this.Name = "WinFormPager";
            this.Size = new System.Drawing.Size(480, 31);
            this.Load += new System.EventHandler(this.WinFormPager_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WinFormPager_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WinFormPager_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.txtToPageIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imglstPager;
        private ComboBox cbPageSize;
        private Button btnToPageIndex;
        private Label lbEnd;
        private NumericUpDown txtToPageIndex;
        private Label lbPre;
        private Button btnLast;
        private Button btnNext;
        private Button btnPrevious;
        private Button btnFirst;
        private Label lblPager;
    }
}
