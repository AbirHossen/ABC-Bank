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
    public partial class AllFixedDeposit : Form
    {
        public AllFixedDeposit()
        {
            InitializeComponent();
        }

        private void AllFixedDeposit_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void searchAccToolStripButton_Click(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {   
                fdTableAdapter.searchAcc(allFDs.fd, ((int)(Convert.ChangeType(richTextBox1.Text, typeof(int)))));
            }
            catch 
            {
                if (richTextBox1.Text == string.Empty)
                {
                    MessageBox.Show(" Please Enter Account Number","Parameter Missing",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
            // TODO: This line of code loads data into the 'allFDs.fd' table. You can move, or remove it, as needed.
            this.fdTableAdapter.Fill(this.allFDs.fd);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(richTextBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers And Bigger than Zero[0].", "Invalid Input");
                richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.Text.Length - 1);
            }
            var box = (RichTextBox)sender;
            if (box.Text.StartsWith("0")) box.Text = "";
        }
    }
}
