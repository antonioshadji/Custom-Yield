using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Windows;

namespace Custom_Yield_Config_Builder
{
    
    public partial class frmMain : Form
    {
        static readonly string assembleDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(frmWebDownload)).Location);
        private string tempXLS;
        private string tempCSV = assembleDirectory + "\\temp.csv";
        frmWebDownload download = new frmWebDownload();
        frmModelSpecs modelSpecs;

        public frmMain()
        {
            InitializeComponent();
            InitializeLists(3);
            lstPriceModels.ItemChecked += new ItemCheckedEventHandler(lstPriceModels_ItemChecked);
            lstDeliverableBonds.ItemChecked += new ItemCheckedEventHandler(lstDeliverableBonds_ItemChecked);
            openFileDialog1.FileOk += new CancelEventHandler(openFileDialog1_FileOk);
        }

        void lstPriceModels_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                e.Item.Remove();
            }
        }

        void InitializeLists(int listCount)
        {
            
            if (listCount == 1 || listCount == 3)
            {
                lstDeliverableBonds.Clear();
                lstDeliverableBonds.Columns.Add("Duration", 100, HorizontalAlignment.Left);
                lstDeliverableBonds.Columns.Add("Coupon Rate", 100, HorizontalAlignment.Left);
                lstDeliverableBonds.Columns.Add("Issue Date", 100, HorizontalAlignment.Center);
                lstDeliverableBonds.Columns.Add("Maturity Date", 100, HorizontalAlignment.Center);
                lstDeliverableBonds.Columns.Add("Cusip Number", 100, HorizontalAlignment.Center);
            }
            if (listCount == 2 || listCount == 3)
            {
                lstPriceModels.Clear();
                lstPriceModels.Columns.Add("Model Name", 140, HorizontalAlignment.Left);
                lstPriceModels.Columns.Add("Coupon Rate", 80, HorizontalAlignment.Center);
                lstPriceModels.Columns.Add("Conversion Factor", 100, HorizontalAlignment.Center);
                lstPriceModels.Columns.Add("Coupons Per Year", 100, HorizontalAlignment.Center);
                lstPriceModels.Columns.Add("Day Count Type", 90, HorizontalAlignment.Center);
                lstPriceModels.Columns.Add("Settlement Date", 90, HorizontalAlignment.Center);
                lstPriceModels.Columns.Add("Maturity Date", 80, HorizontalAlignment.Center);
                lstPriceModels.Columns.Add("First Coupon Date", 100, HorizontalAlignment.Center);
                lstPriceModels.Columns.Add("Issue Date", 80, HorizontalAlignment.Center);
            }
        }

        void lstDeliverableBonds_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                int columnCount = e.Item.SubItems.Count;
                Dictionary<string, string> contracts = new Dictionary<string, string>();
                for (int z = 5; z < columnCount; z++)
                {
                    if (!e.Item.SubItems[z].Text.Equals(string.Empty))
                    {
                        contracts.Add(lstDeliverableBonds.Columns[z].Text, e.Item.SubItems[z].Text);
                    }
                }

                bond currentBond = new bond();
                if (currentBond.convertFromListViewItem(e.Item))
                {
                    modelSpecs = new frmModelSpecs(currentBond, contracts, e.Item);
                    modelSpecs.Show();
                    modelSpecs.FormClosed += new FormClosedEventHandler(modelSpecs_FormClosed);
                }
            }
            else
            {
                //find the price model cooresponding to the unchecked model and remove it
            }
        }

        void modelSpecs_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (modelSpecs.closeOK)
            {
                bond currentBond = modelSpecs.currentBond;
                ListViewItem tmpItem = new ListViewItem();
                tmpItem.Text = currentBond.name;
                tmpItem.SubItems.Add(currentBond.coupon.ToString());
                tmpItem.SubItems.Add(currentBond.conversionFactor.ToString());
                tmpItem.SubItems.Add(currentBond.couponsPerYear.ToString());
                tmpItem.SubItems.Add(currentBond.DayCountType.ToString());
                tmpItem.SubItems.Add(currentBond.settlementDate);
                tmpItem.SubItems.Add(currentBond.maturityDate);
                tmpItem.SubItems.Add(currentBond.firstCouponDate);
                tmpItem.SubItems.Add(currentBond.issueDate);

                lstPriceModels.Items.Add(tmpItem);
            }
            else
            {
                modelSpecs.lvItem.Checked = false;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fromLocalCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            InitializeLists(1);
            readCSV(openFileDialog1.FileName);
        }

        private void fromWebAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            download = new frmWebDownload();
            download.Show();
            tempXLS = download.tempfile;
            download.FormClosed += new FormClosedEventHandler(download_FormClosed);
        }

        void download_FormClosed(object sender, FormClosedEventArgs e)
        {            
            if (download.downloadOK)
            {
                InitializeLists(1);
                if (convertExcelToCSV(tempXLS, "Conversion Factors", tempCSV))
                {
                    readCSV(tempCSV);
                }
            }
        }

        bool convertExcelToCSV(string sourceFile, string worksheetName, string targetFile)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sourceFile + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\""; 
            OleDbConnection conn = null;
            StreamWriter wrtr = null;
            OleDbCommand cmd = null;
            OleDbDataAdapter da = null;
            bool finished = false;
            try
            {
                conn = new OleDbConnection(strConn);
                conn.Open();
 
                cmd = new OleDbCommand("SELECT * FROM [" + worksheetName + "$]", conn);
                cmd.CommandType = CommandType.Text;
                wrtr = new StreamWriter(targetFile);
 
                da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
 
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string rowString = "";
                    for (int y = 0; y < dt.Columns.Count; y++)
                    {
                        rowString += dt.Rows[x][y].ToString() + ",";
                    }
                    wrtr.WriteLine(rowString);
                }
                finished = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);                
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                cmd.Dispose();
                da.Dispose();
                wrtr.Close();
                wrtr.Dispose();
            }
            if (finished)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void readCSV(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            string currentLine = sr.ReadLine();
            string[] lineArray;
            int lineIndex = 0;
            bool onTheRun = false;
            string couponString = "";
            DateTime issueDate = new DateTime();
            DateTime maturityDate = new DateTime();
            string duration ="2-YEAR";
            string cusipNumber;
            int deliverIndex = 7;
            int row = 0;
            bool isEven = true;

            while (currentLine != null)
            {
                if (currentLine.Equals(string.Empty))
                {
                    break;
                }
                lineArray = currentLine.Split(",".ToCharArray());

                if (lineArray[0].Contains("2-YEAR"))
                {
                    duration = "2-YEAR";
                }
                else if (lineArray[0].Contains("3-YEAR"))
                {
                    duration = "3-YEAR";
                }
                else if (lineArray[0].Contains("5-YEAR"))
                {
                    duration = "5-YEAR";
                }
                else if (lineArray[0].Contains("10-YEAR"))
                {
                    duration = "10-YEAR";
                }
                else if (lineArray[0].Contains("U.S. TREASURY BOND FUTURES CONTRACT"))
                {
                    duration = "30-YEAR";
                }

                if (lineArray[7].Contains("Jun") || lineArray[7].Contains("Sep") || lineArray[7].Contains("Dec") || lineArray[7].Contains("Mar"))
                {
                    while (!lineArray[deliverIndex].Equals(string.Empty))
                    {
                        if (!lstDeliverableBonds.Columns.ToString().Equals(lineArray[deliverIndex]))
                        {
                            DateTime tmp;
                            if (!DateTime.TryParse(lineArray[deliverIndex], out tmp))
                            {
                                tmp = DateTime.Parse("Jan 1900");
                                tmp = tmp.AddDays(double.Parse(lineArray[deliverIndex]));                                
                            }
                            // DateTime.Parse(lineArray[deliverIndex]);
                            string tempDate = convertToStringMonth(tmp.Month) + " " + tmp.Year.ToString();
                            lstDeliverableBonds.Columns.Add(tempDate, 100, HorizontalAlignment.Center);
                        }
                        deliverIndex++;
                    }
                }
                if (lineArray[0].Contains(".)"))
                {
                    if (lineArray[1].Equals("@"))
                    {
                        onTheRun = true;
                    }
                    else
                    {
                        onTheRun = false;
                    }
                    couponString = lineArray[2];
                    issueDate = DateTime.Parse(lineArray[3]);
                    maturityDate = DateTime.Parse(lineArray[4]);                    
                    cusipNumber = lineArray[5];
                    ListViewItem item = new ListViewItem();
                    
                    item.Text = duration;
                    item.SubItems.Add(couponString);
                    //item.SubItems.Add(couponString);
                    item.SubItems.Add(issueDate.ToShortDateString());
                    item.SubItems.Add(maturityDate.ToShortDateString());
                    item.SubItems.Add(cusipNumber);

                    int a = 7;
                    while (a <lineArray.Length)
                    {
                        if (lineArray[a].Equals("-----"))
                        {
                            item.SubItems.Add("");
                        }
                        else
                        {
                            item.SubItems.Add(lineArray[a].ToString());
                        }
                        a++;
                    }
                    if (onTheRun)
                    {
                        item.BackColor = Color.Black;
                        item.ForeColor = Color.White;
                    }
                    else
                    {
                        item.ForeColor = Color.Black;
                        item.BackColor = Color.White;
                    }
                    lstDeliverableBonds.Items.Add(item);
                    row++;
                }
                lineIndex++;
                currentLine = sr.ReadLine();
            }
        }

        private void saveConfigAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstPriceModels.Items.Count > 0)
            {
                string fileName = saveFile(assembleDirectory, "Save Config File");
                if (fileName != null)
                {
                    /*                     
                        [1]
                        Name=ZB Sep09
                        MaturityDate=6/30/2007
                        Coupon=5
                        SettlementDate=8/20/2004
                        CouponsPerYear=2
                        DayCountMethod=1
                        EndOfMonthFlag=TRUE
                        DatedDate=5/14/2004
                        FirstCouponDate=12/31/2004
                        ConversionFactor=1
                     */

                    StreamWriter sw = new StreamWriter(fileName);
                    int i = 0;
                    sw.WriteLine("[Settings]\nInitSource=1\nInstruments="+ lstPriceModels.Items.Count.ToString() + "\nDebugMode=false");

                    foreach (ListViewItem tmpItem in lstPriceModels.Items)
                    {
                        bond tmpBond = new bond();
                        tmpBond.name = tmpItem.Text;
                        tmpBond.coupon = double.Parse(tmpItem.SubItems[1].Text);
                        tmpBond.conversionFactor = double.Parse(tmpItem.SubItems[2].Text);
                        tmpBond.couponsPerYear = int.Parse(tmpItem.SubItems[3].Text);
                        tmpBond.setDayCountType(tmpItem.SubItems[4].Text);
                        tmpBond.settlementDate =tmpItem.SubItems[5].Text;
                        tmpBond.maturityDate =tmpItem.SubItems[6].Text;

                        sw.WriteLine("\n[" + i.ToString() + "]");
                        sw.WriteLine("Name=" + tmpBond.name);
                        sw.WriteLine("Coupon=" + tmpBond.coupon.ToString());
                        sw.WriteLine("ConversionFactor=" + tmpBond.conversionFactor.ToString());
                        sw.WriteLine("CouponsPerYear=" + tmpBond.couponsPerYear.ToString());
                        sw.WriteLine("DayCountMetohd=" + tmpBond.DayCountType.GetHashCode().ToString());
                        sw.WriteLine("SettlementDate=" + tmpBond.settlementDate);
                        sw.WriteLine("MaturityDate=" + tmpBond.maturityDate);

                        if (!tmpItem.SubItems[7].Text.Equals(string.Empty))
                        {
                            tmpBond.firstCouponDate = tmpItem.SubItems[7].Text;
                            tmpBond.issueDate = tmpItem.SubItems[8].Text;
                            sw.WriteLine("FirstCouponDate=" + tmpBond.firstCouponDate);
                            sw.WriteLine("DatedDate=" + tmpBond.issueDate);
                        }
                        i++;
                    }
                    sw.Close();
                }
                else
                {
                    MessageBox.Show("No file selected");
                }
            }
            else
            {
                MessageBox.Show("No Price Models to save!");
            }
        }

        string saveFile(string initialDirectory, string title)
        {
            saveFileDialog1.InitialDirectory = initialDirectory;
            saveFileDialog1.Title = title;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog1.FileName;
            }
            else
            {
                return null;
            }             
        }

        string convertToStringMonth(int intMonth)
        {
            switch (intMonth)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "May";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sep";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
                default:
                    return "Jan";
            }
        }
    }

    public class bond
    {
        public double coupon;
        public double conversionFactor;
        public int couponsPerYear;
        public int duration;
        public string name;
        public string settlementDate;
        public string cusip; 
        public string issueDate;
        public string maturityDate;
        public string firstCouponDate;
        public dayCountType DayCountType;

        public enum dayCountType : int
        {
            Thirty360 = 1,
            Thirty365 = 2,
            ThirtyE360 = 3,
            Actual360 = 4,
            Actual365 = 5,
            ActualActual = 6
        };
        
        public int getDayCountTypeInt()
        {
            switch (this.DayCountType)
            {
                case dayCountType.Actual360:
                    return dayCountType.Actual360.GetHashCode();
                case dayCountType.Actual365:
                    return dayCountType.Actual365.GetHashCode();
                case dayCountType.ActualActual:
                    return dayCountType.ActualActual.GetHashCode();
                case dayCountType.Thirty360:
                    return dayCountType.Thirty360.GetHashCode();
                case dayCountType.Thirty365:
                    return dayCountType.Thirty365.GetHashCode();
                case dayCountType.ThirtyE360:
                    return dayCountType.ThirtyE360.GetHashCode();
                default:
                    return dayCountType.ActualActual.GetHashCode();
            }           
        }

        public void setDayCountType(string tmpDayCountType)
        {
            switch (tmpDayCountType)
            {
                case "Actual360":
                    this.DayCountType = dayCountType.Actual360;
                    break;
                case "Actual365":
                    this.DayCountType = dayCountType.Actual365;
                    break;
                case "ActualActual":
                    this.DayCountType = dayCountType.ActualActual;
                    break;
                case "Thirty360":
                    this.DayCountType = dayCountType.Thirty360;
                    break;
                case "Thirty365":
                    this.DayCountType = dayCountType.Thirty365;
                    break;
                case "ThirtyE360":
                    this.DayCountType = dayCountType.ThirtyE360;
                    break;
                default:
                    this.DayCountType = dayCountType.ActualActual;
                    break;
            }
        }

        public string stringMonth(int intMonth)
        {
            switch (intMonth)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "May";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sep";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
                default:
                    return "Jan";
            }
        }

        void setDuration()
        {
            duration = DateTime.Parse(maturityDate).Year - DateTime.Parse(settlementDate).Year;            
        }

        public bool convertFromListViewItem(ListViewItem lvItem)
        {
            try
            {
                string[] tmp = lvItem.SubItems[1].Text.Split(' ');

                if (tmp.Length>1)
                {
                    if (!tmp[1].Equals(string.Empty))
                    {
                        string[] tmpFraction = tmp[1].Split('/');

                        if (!tmp[0].Equals(string.Empty))
                        {
                            this.coupon = double.Parse(tmp[0]) + double.Parse(tmpFraction[0]) / double.Parse(tmpFraction[1]);
                        }
                        else
                        {
                            this.coupon = double.Parse(tmpFraction[0]) / double.Parse(tmpFraction[1]);
                        }
                    }
                    else
                    {
                        this.coupon = double.Parse(tmp[0]);
                    }
                }
                else
                {
                    this.coupon = double.Parse(tmp[0]);
                }
                this.issueDate = DateTime.Parse(lvItem.SubItems[2].Text).ToShortDateString();
                this.maturityDate = DateTime.Parse(lvItem.SubItems[3].Text).ToShortDateString();
                this.cusip = lvItem.SubItems[4].Text;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}