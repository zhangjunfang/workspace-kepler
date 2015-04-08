using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace ServiceStationClient.ComponentUI
{
    public partial class MessageBoxEx : FormEx
    {

        public MessageBoxEx()
        {
            InitializeComponent();
            btnLeft.Init();
            btnMidlle.Init();
            btnRight.Init();
            base.BackColor = pnlContainer.BackColor = Color.FromArgb(225, 225, 225);
        }


        /// <summary>
        /// Message
        /// </summary>
        public string Message 
        {
            get 
            {
                return lblMessage.Text;
            }
            set 
            {
                lblMessage.Text = value;
            }
        }

        private static readonly string STR_BUTTON_TEXT_OK="确定";
        private static readonly string STR_BUTTON_TEXT_CANCEL = "取消";
        private static readonly string STR_BUTTON_TEXT_ABORT = "中止";
        private static readonly string STR_BUTTON_TEXT_RETRY = "重试";
        private static readonly string STR_BUTTON_TEXT_IGNORE = "忽略";
        private static readonly string STR_BUTTON_TEXT_YES = "是";
        private static readonly string STR_BUTTON_TEXT_NO = "否";
        public void InitButton(MessageBoxButtons messageBoxButtons,MessageBoxDefaultButton messageBoxDefaultButton= MessageBoxDefaultButton.Button3) 
        {
            switch (messageBoxButtons)
            {
                case MessageBoxButtons.OK:
                    btnLeft.Visible = false;
                    btnMidlle.Visible = false;
                    btnRight.Caption = STR_BUTTON_TEXT_OK;
                    break;
                case MessageBoxButtons.OKCancel:
                    btnLeft.Visible = false;
                    btnMidlle.Caption = STR_BUTTON_TEXT_OK;
                    btnRight.Caption = STR_BUTTON_TEXT_CANCEL;
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    btnLeft.Caption = STR_BUTTON_TEXT_ABORT;
                    btnMidlle.Caption = STR_BUTTON_TEXT_RETRY;
                    btnRight.Caption = STR_BUTTON_TEXT_IGNORE;

                    break;
                case MessageBoxButtons.YesNoCancel:
                    btnLeft.Caption = STR_BUTTON_TEXT_YES;
                    btnMidlle.Caption = STR_BUTTON_TEXT_NO;
                    btnRight.Caption = STR_BUTTON_TEXT_CANCEL;
                    break;
                case MessageBoxButtons.YesNo:
                    btnLeft.Visible = false;
                    btnMidlle.Caption = STR_BUTTON_TEXT_YES;
                    btnRight.Caption = STR_BUTTON_TEXT_NO;
                    break;
                case MessageBoxButtons.RetryCancel:
                    btnLeft.Visible = false;
                    btnMidlle.Caption = STR_BUTTON_TEXT_RETRY;
                    btnRight.Caption = STR_BUTTON_TEXT_CANCEL;
                    break;
            }

            
            switch (messageBoxDefaultButton) 
            {
                case MessageBoxDefaultButton.Button1:
                    if (btnLeft.Visible) 
                    {
                        this.AcceptButton = this.btnLeft;
                    }
                    else if (btnMidlle.Visible)
                    {
                        this.AcceptButton = this.btnMidlle;
                    }
                    else 
                    {
                        this.AcceptButton = this.btnRight;
                    }
                    break;
                case MessageBoxDefaultButton.Button2:
                    if (btnMidlle.Visible)
                    {
                        this.AcceptButton = this.btnMidlle;
                    }
                    else
                    {
                        this.AcceptButton = this.btnRight;
                    }
                    break;
                case MessageBoxDefaultButton.Button3:
                    this.AcceptButton = this.btnRight;
                    break;
            }
        }

        public void InitIcon(MessageBoxIcon messageBoxIcon)
        {
            switch (messageBoxIcon) 
            {
                case MessageBoxIcon.None:
                    picIcon.Image = ServiceStationClient.Skin.Properties.Resources._messageBoxIco02;
                    break;
                case MessageBoxIcon.Error:
                    picIcon.Image = ServiceStationClient.Skin.Properties.Resources._messageBoxIco03;
                    break;
                case MessageBoxIcon.Question:
                    picIcon.Image = ServiceStationClient.Skin.Properties.Resources._messageBoxIco04;
                    break;
                case MessageBoxIcon.Warning:
                    picIcon.Image  = ServiceStationClient.Skin.Properties.Resources._messageBoxIco05;
                    break;
                case MessageBoxIcon.Information:
                    picIcon.Image = ServiceStationClient.Skin.Properties.Resources._messageBoxIco01;
                    break;
            }
        }
        
        /// <summary>
        /// Message: Text to display in the message box.
        /// </summary>
        static public DialogResult Show(string text)
        {
            MessageBoxEx Msg = new MessageBoxEx();
            Msg.Message = text;
            Msg.InitButton(MessageBoxButtons.OK);
            Msg.InitIcon(MessageBoxIcon.None);
            var dialog = Msg.ShowDialog();
            return dialog;
        }

        /// <summary>
        /// Title: Text to display in the title bar of the messagebox.
        /// </summary>
        static public DialogResult Show(string text, string caption)
        {
            MessageBoxEx Msg = new MessageBoxEx();
            Msg.Message = text;
            if (!string.IsNullOrEmpty(caption)) 
            {
                Msg.Text = caption;
            }
            
            Msg.InitButton(MessageBoxButtons.OK);
            Msg.InitIcon(MessageBoxIcon.None);
            return Msg.ShowDialog();
        }

        /// <summary>
        /// MButtonExs: Display MyButtonExs on the message box.
        /// </summary>
        static public DialogResult Show(string text, string caption, MessageBoxButtons messageBoxButtons)
        {
            MessageBoxEx Msg = new MessageBoxEx();
            Msg.Message = text;
            if (!string.IsNullOrEmpty(caption))
            {
                Msg.Text = caption;
            }
            Msg.InitButton(messageBoxButtons);
            Msg.InitIcon(MessageBoxIcon.None);
            return Msg.ShowDialog();
        }

        /// <summary>
        /// MIcon: Display MyIcon on the message box.
        /// </summary>
        static public DialogResult Show(string text, string caption, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            MessageBoxEx Msg = new MessageBoxEx();
            Msg.Message = text;
            if (!string.IsNullOrEmpty(caption))
            {
                Msg.Text = caption;
            }
            Msg.InitButton(messageBoxButtons);
            Msg.InitIcon(messageBoxIcon);
            return Msg.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtionClick(object sender, EventArgs e) 
        {
            ButtonEx btn = sender as ButtonEx;
            string buttonText = string.Empty;
            if (btn != null)
            {
                buttonText=btn.Caption;
            }

            if (buttonText.Equals(STR_BUTTON_TEXT_OK)) 
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (buttonText.Equals(STR_BUTTON_TEXT_CANCEL)) 
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else if (buttonText.Equals(STR_BUTTON_TEXT_YES))
            {
                this.DialogResult = DialogResult.Yes;
            }
            else if (buttonText.Equals(STR_BUTTON_TEXT_NO))
            {
                this.DialogResult = DialogResult.No;
            }
            else if (buttonText.Equals(STR_BUTTON_TEXT_ABORT))
            {
                this.DialogResult = DialogResult.Abort;
            }
            else if (buttonText.Equals(STR_BUTTON_TEXT_IGNORE))
            {
                this.DialogResult = DialogResult.Ignore;
            }
            else if (buttonText.Equals(STR_BUTTON_TEXT_RETRY))
            {
                this.DialogResult = DialogResult.Retry;
            }
            else 
            {
                this.DialogResult = DialogResult.None;
            }
        }

        #region 提示框
        /// <summary> 提示框：显示一般信息
        /// </summary>
        /// <param name="information">一般提示信息</param>
        public static void ShowInformation(string information)
        {
            if (information.Length > 300)
            {
                information = information.Substring(0, 300);
                information += " ... ...";
            }
            MessageBoxEx.Show(information, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary> 提示框：显示警告信息
        /// </summary>
        /// <param name="warning">警告信息</param>
        public static void ShowWarning(string warning)
        {
            if (warning.Length > 300)
            {
                warning = warning.Substring(0, 300);
                warning += " ... ...";
            }
            MessageBoxEx.Show(warning, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary> 提示框：显示错误信息
        /// </summary>
        /// <param name="error">错误信息</param>
        public static void ShowError(string error)
        {
            if (error.Length > 300)
            {
                error = error.Substring(0, 300);
                error += " ... ...";
            }
            MessageBoxEx.Show(error, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary> 提示框：显示确认框
        /// </summary>
        /// <param name="question">确认信息</param>
        /// <returns>确认结果</returns>
        public static bool ShowQuestion(string question)
        {
            if (question.Length > 300)
            {
                question = question.Substring(0, 300);
                question += " ... ...";
            }
            if (MessageBoxEx.Show(question, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                return true;
            else
                return false;
        }
        #endregion

    }
}
