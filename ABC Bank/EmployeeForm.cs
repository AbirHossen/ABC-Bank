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
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
            label2.Text = DateTime.Today.ToLongDateString();
        }

        private void newAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerForm cf = new CustomerForm();
            cf.Show();
        }

        private void searchUpdateAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerInfoUpdate cu = new CustomerInfoUpdate();
            cu.Show();
        }

        private void closeAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close_Account ca = new Close_Account();
            ca.Show();
        }

        private void depositToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deposit d = new Deposit();
            d.Show();
        }

        private void withdrawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Withdraw wd = new Withdraw();
            wd.Show();
        }

        private void transferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoneyTransfer mf = new MoneyTransfer();
            mf.Show();
        }

        private void fDFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FdForm ff = new FdForm();
            ff.Show();
        }

        private void loanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoanForm l = new LoanForm();
            l.Show();
        }

        private void loanPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoanPayment lp = new LoanPayment();
            lp.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void logOytToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "Form1")
                    Application.OpenForms[i].Close();
            }
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void f2(object sender, FormClosingEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
    
}
