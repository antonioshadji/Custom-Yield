using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Custom_Yield_Config_Builder
{
    public partial class frmModelSpecs : Form
    {
        public bond currentBond;
        public bool closeOK = false;
        public ListViewItem lvItem;
        Dictionary<string, string> contracts;

        public frmModelSpecs(bond b, Dictionary<string, string> c, ListViewItem lv)
        {
            lvItem = lv;
            currentBond = b;
            contracts = c;
            InitializeComponent();
            initializeLists(contracts);
            txtName.Text = currentBond.coupon + " " + currentBond.stringMonth(DateTime.Parse(currentBond.maturityDate).Month) + " " + DateTime.Parse(currentBond.maturityDate).Year;
        }

        void initializeLists(Dictionary<string, string> contracts)
        {
            cmbSettleDate.Items.Add("Today");
            cmbSettleDate.Items.Add("Tomorrow");
            cmbSettleDate.Items.Add("Today + 2");

            foreach (KeyValuePair<string,string> contract in contracts)
            {
                cmbFuturesContract.Items.Add(contract.Key);
            }
        }

        private void radFutures_CheckedChanged(object sender, EventArgs e)
        {
            if (radFutures.Checked)
            {
                cmbFuturesContract.Enabled = true;
                cmbSettleDate.Enabled = false;
                cmbSettleDate.Visible = false;
                calFuturesSettle.Visible = true;
            }
        }

        private void radCash_CheckedChanged(object sender, EventArgs e)
        {
            if (radCash.Checked)
            {
                cmbFuturesContract.Enabled = false;
                cmbSettleDate.Enabled = true;
                cmbSettleDate.Visible = true;
                calFuturesSettle.Visible = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (radFutures.Checked)
            {
                currentBond.settlementDate = calFuturesSettle.SelectionRange.Start.ToShortDateString();                
            }
            else if (radCash.Checked)
            {
                switch (cmbSettleDate.SelectedText)
                {
                    case "Today":
                        currentBond.settlementDate = "Today";
                        break;
                    case "Tomorrow":
                        currentBond.settlementDate = "Today+1";
                        break;
                    case "Today + 2":
                        currentBond.settlementDate = "Today+2";
                        break;
                    default:
                        currentBond.settlementDate = "Today";
                        break;
                }
            }
            else
            {
                MessageBox.Show("Select futures contract or cash bond to continue!");
                return;
            }

            currentBond.name = txtName.Text;

            if (chkExtras.Checked)
            {
                currentBond.firstCouponDate = calFirstCouponDate.SelectionEnd.Date.ToShortDateString();
            }

            string tempConversionFactor;
            if (contracts.TryGetValue(cmbFuturesContract.Text, out tempConversionFactor) && radFutures.Checked)
            {
                currentBond.conversionFactor = double.Parse(tempConversionFactor);
            }
            else
            {
                currentBond.conversionFactor = 1;
            }
            currentBond.couponsPerYear = 2;
            currentBond.DayCountType = bond.dayCountType.ActualActual;
            this.closeOK = true;
            this.Close();
        }

        private void chkExtras_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExtras.Checked)
            {
                calFirstCouponDate.Visible = true;
            }
            else
            {
                calFirstCouponDate.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
