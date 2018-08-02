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
    public partial class Loans : Form
    {
        public Loans()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Loans_Load(object sender, EventArgs e)
        {
            

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                loanTableAdapter.FillBy(loanSet.loan, ((int)(Convert.ChangeType(richTextBox1.Text, typeof(int)))));
            }
            catch 
            {
                if (richTextBox1.Text == string.Empty)
                {
                    MessageBox.Show(" Please Enter Account Number", "Parameter Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    richTextBox1.Select();
                }
                else
                {
                    MessageBox.Show("No Record Found", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.loanTableAdapter.Fill(this.loanSet.loan);
        }
    }
}
