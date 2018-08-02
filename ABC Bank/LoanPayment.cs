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
    public partial class LoanPayment : Form
    {
        public LoanPayment()
        {
            InitializeComponent();
            label2.Text = DateTime.Now.ToLongDateString();
            textBox2.Select();
        }

        private void LoanPayment_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                Connecntion cn = new Connecntion();
                cn.connOpen();
                SqlDataReader rd;
                using (SqlCommand cmd = new SqlCommand("select Name from customer where Account_no=(select Account_No from loan where Account_No='" + textBox2.Text + "' and Loan_Status='Approved')", cn.con))
                {
                    using (rd = cmd.ExecuteReader())
                    {
                        if (rd.HasRows == false)
                        {
                            MessageBox.Show("Account doesn't have any Loan", "Result");
                            textBox2.Clear();
                            textBox2.Select();

                        }
                        else if(rd.Read())
                        {
                            textBox3.Text = rd.GetString(0);
                        }
                    }
                }
            }
                
            
            
            else
            {
                MessageBox.Show("Enter Account Number","Missing Data");
            }
        }
        public void inQuery()
        {
            int main_loan=0;
            int up_loan = 0;

            Connecntion cn = new Connecntion();
            cn.connOpen();
            SqlDataReader rd;
            using (SqlCommand cmd=new SqlCommand("select Loan_Amount from loan where account_no='"+ textBox2.Text.ToString() + "'", cn.con))
            {
                

                using (rd = cmd.ExecuteReader())
                {
                    if (rd.HasRows == false)
                    {
                        MessageBox.Show("Account doesn't have any Loan", "Result");
                        textBox2.Clear();
                        textBox2.Select();

                    }
                    else if (rd.Read())
                    {
                        main_loan = rd.GetInt32(0);
                    }
                }

            }
            if (main_loan >= Convert.ToInt32(textBox5.Text.ToString()))
            {
                //procedure call
                up_loan = main_loan - Convert.ToInt32(textBox5.Text);

                using (SqlCommand cmd = new SqlCommand("loan_pay", cn.con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ano", Convert.ToInt32(textBox2.Text.ToString()));
                    cmd.Parameters.AddWithValue("@n", textBox3.Text);
                    cmd.Parameters.AddWithValue("@pm", textBox4.Text);
                    cmd.Parameters.AddWithValue("@pa", Convert.ToInt32(textBox5.Text));
                    cmd.Parameters.AddWithValue("@dt", DateTime.Today.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@ul", up_loan);


                    try
                    {
                        //cn.con.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Loan Installment Successfull");
                        this.Close();
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("incorrect payment");
            }

            



        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers And Bigger than Zero[0].", "Invalid Input");
                textBox5.Text = textBox5.Text.Remove(textBox5.Text.Length - 1);
            }
            var box = (TextBox)sender;
            if (box.Text.StartsWith("0")) box.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty && textBox5.Text != string.Empty)
            {

                inQuery();

            }
            else
            {
                MessageBox.Show("Fill all the boxes","Result");
            }
        }

        
    }
}
