using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ABC_Bank
{
    public partial class Close_Account : Form
    {
        public Close_Account()
        {
            InitializeComponent();
        }

        private void Close_Account_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aBCDataSet1.customer' table. You can move, or remove it, as needed.
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                this.Close();
            }

        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                
                customerTableAdapter.searchAccount(aBCDataSet1.customer, ((int)(System.Convert.ChangeType(richTextBox2.Text, typeof(int)))));
            }
            catch
            {
                if (richTextBox2.Text == string.Empty)
                {
                    MessageBox.Show("Enter Customer Account Number", "Account_No missing");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            customerTableAdapter.Fill(this.aBCDataSet1.customer);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connecntion cn = new Connecntion();
            cn.connOpen();
            SqlDataReader rd;
            SqlCommand cmd;
            using (cmd = new SqlCommand("Select * from customer where Account_No='" + richTextBox1.Text + "'", cn.con))
            {
                try
                {
                   rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        rd.Close();
                       cmd =new SqlCommand("Delete from customer where Account_No='" + richTextBox1.Text + "'", cn.con);
                        try { rd = cmd.ExecuteReader();
                            MessageBox.Show("Customer Info Deleted!","Operation Complete");
                            button3.PerformClick();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No record found","Result");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            cn.con.Close();

        }
    }
}
