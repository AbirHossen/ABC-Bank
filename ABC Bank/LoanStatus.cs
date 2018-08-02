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
    public partial class LoanStatus : Form
    {
        public LoanStatus()
        {
            InitializeComponent();
            label2.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void LoanStatus_Load(object sender, EventArgs e)
        {
            

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                loanTableAdapter.FillBy(loanStatDataSet.loan, ((int)(Convert.ChangeType(richTextBox1.Text, typeof(int)))));
            }
            catch 
            {
                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            loanTableAdapter.Fill(loanStatDataSet.loan);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text == string.Empty)
            {
                MessageBox.Show("Enter Customer Account Number", "Enter Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (richTextBox2.Text != string.Empty)
            {
                Connecntion cn = new Connecntion();
                cn.connOpen();
                SqlDataReader rd;
                using (SqlCommand cmd = new SqlCommand("select * from loan where Account_No='" + richTextBox2.Text + "'", cn.con))
                {
                    using (rd = cmd.ExecuteReader())
                    {
                        if (rd.HasRows == false)
                        {
                            MessageBox.Show("No loan record found","Not Found");
                        }
                        else if(rd.Read())
                        {
                            rd.Close();
                            using(SqlCommand cmd2 = new SqlCommand("update loan set Loan_Status='Approved' where Account_No='" + richTextBox2.Text + "'", cn.con))
                            {
                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Loan Accepted!","Updated!");
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }

                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text == string.Empty)
            {
                MessageBox.Show("Enter Customer Account Number", "Enter Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (richTextBox2.Text != string.Empty)
            {
                Connecntion cn = new Connecntion();
                cn.connOpen();
                SqlDataReader rd;
                using (SqlCommand cmd = new SqlCommand("select * from loan where Account_No='" + richTextBox2.Text + "'", cn.con))
                {
                    using (rd = cmd.ExecuteReader())
                    {
                        if (rd.HasRows == false)
                        {
                            MessageBox.Show("No loan record found", "Not Found");
                        }
                        else if (rd.Read())
                        {
                            rd.Close();
                            using (SqlCommand cm3 = new SqlCommand("Select Loan_Status from loan where Account_No='"+richTextBox2.Text+"'",cn.con))
                            {
                                object result = cm3.ExecuteScalar();
                                string res = Convert.ToString(result);
                                if (res == "Approved")
                                {
                                    MessageBox.Show("Loan is approved!\n Can not decline", "Declined");
                                }
                                else
                                {
                                    using (SqlCommand cmd2 = new SqlCommand("Delete loan where Account_No='" + richTextBox2.Text + "'", cn.con))
                                    {
                                        try
                                        {
                                            cmd2.ExecuteNonQuery();
                                            MessageBox.Show("Loan Declined", "Updated!");
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                }
                            }

                            

                        }
                    }
                }
            }
        }

       
    }
}
