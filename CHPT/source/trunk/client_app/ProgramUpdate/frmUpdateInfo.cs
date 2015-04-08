using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace ProgramUpdate
{
    public partial class frmUpDateInfo : Form
    {


        public frmUpDateInfo()
        {
            InitializeComponent();
        }

        public string UpdateFileUrl { get; set; }
        private void FormUpDateInfo_Load(object sender, EventArgs e)
        {
            try
            {
                string updateTxtFile = UpdateFileUrl.Replace(".zip", ".txt");
                this.rtxtContext.Text = GetFileData(updateTxtFile);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(0, "Load" + ex.Message, 0);
            }
        }

        private void rtxtContext_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);// 在IE浏览器中浏览单击的超链接
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected string GetFileData(string TheURL)
        {
            Uri uri = new Uri(TheURL);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.AllowAutoRedirect = false;
            request.Timeout = 5000;
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.UTF8);
            string retext = readStream.ReadToEnd().ToString();
            readStream.Close();
            return retext;
        }
    }
}
