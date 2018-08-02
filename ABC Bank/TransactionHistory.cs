using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Bank
{
    public partial class TransactionHistory : Form
    {
        public TransactionHistory()
        {
            InitializeComponent();
            label2.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TransactionHistory_Load(object sender, EventArgs e)
        {
           

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                loan_paymentTableAdapter.FillBy(transactionDataSet.loan_payment, ((int)(Convert.ChangeType(richTextBox1.Text, typeof(int)))));
            }
            catch{
                if (richTextBox1.Text == string.Empty)
                {
                    MessageBox.Show("Enter Customer Account Number", "Enter Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No Record Found", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loan_paymentTableAdapter.Fill(this.transactionDataSet.loan_payment);
        }
    }
}
