using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Custom_Yield_Config_Builder
{
    public partial class frmWebDownload : Form
    {
        static readonly string assembleDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(frmWebDownload)).Location);
        //private string _iniPath = null;
        public string tempfile = assembleDirectory + "\\temp.xls";
        public bool downloadOK = false;

        public frmWebDownload()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                WebClient client = new WebClient();                
                client.DownloadFile(textBox1.Text, tempfile);
                downloadOK = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = "http://www.cmegroup.com/trading/interest-rates/treasury-conversion-factors.html";

            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch
                (
                 System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    }
}