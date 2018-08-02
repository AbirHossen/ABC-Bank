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
    public partial class AdminHome : Form
    {
        public AdminHome()
        {
            InitializeComponent();
            label2.Text = DateTime.Today.ToLongDateString();
        }

        private void newAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerForm c = new CustomerForm();
            c.Show();
        }

        private void searchUpdateCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerInfoUpdate cu = new CustomerInfoUpdate();
            cu.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close_Account ca = new Close_Account();
            ca.Show();
        }

        private void allCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerDetails cd = new CustomerDetails();
            cd.Show();
        }

        private void newUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserInfo ui = new UserInfo();
            ui.Show();
        }

        private void searchUpdateUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateUser uu = new UpdateUser();
            uu.Show();
        }

        private void removeUSerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveUser ru = new RemoveUser();
            ru.Show();
        }

        private void allUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDetails ud = new UserDetails();
            ud.Show();
        }

        private void depositToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Deposit d = new Deposit();
            d.Show();
        }

        private void withdrawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Withdraw w = new Withdraw();
            w.Show();
        }

        private void transferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoneyTransfer t = new MoneyTransfer();
            t.Show();
        }

        private void fDFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FdForm fd = new FdForm();
            fd.Show();
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

        private void fDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllFixedDeposit afd = new AllFixedDeposit();
            afd.Show();
        }

        private void loansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loans l = new Loans();
            l.Show();
        }

        private void loanStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoanStatus ls = new LoanStatus();
            ls.Show();
        }

        private void showLoanPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransactionHistory th = new TransactionHistory();
            th.Show();
        }

        private void bankDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BankDetails bd = new BankDetails();
            bd.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePass cp = new ChangePass();
            cp.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "Form1")
                    Application.OpenForms[i].Close();
            }
           
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void f2(object sender, FormClosingEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void allTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllTransactions at = new AllTransactions();
            at.Show();
        }
    }

}