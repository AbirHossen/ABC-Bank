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
    public partial class UserDetails : Form
    {
        public UserDetails()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserDetails_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the '_UserDetails.user' table. You can move, or remove it, as needed.


            this.userTableAdapter.Fill(this._UserDetails.user);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.userTableAdapter.FillBy(this._UserDetails.user, richTextBox1.Text);
            }
            catch
            {
                if (richTextBox1.Text == string.Empty)
                {
                    MessageBox.Show("Enter Usename", "Enter Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No Record Found", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        
    }
}
