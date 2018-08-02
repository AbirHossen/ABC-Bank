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
    public partial class LoanForm : Form
    {
        public LoanForm()
        {
            InitializeComponent();
            label8.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != string.Empty || textBox2.Text != string.Empty || textBox3.Text != string.Empty || textBox4.Text != string.Empty || textBox5.Text != string.Empty)
            {
                inQuery();
            }
            else
            {
                MessageBox.Show("Please supply all the information", "Data Missing");
            }
        }
        public void inQuery()
        {   
            Connecntion cn = new Connecntion();
            cn.connOpen();
            
            using (SqlCommand cmd = new SqlCommand("select * from customer where Account_No='" + textBox1.Text + "'", cn.con))
            {

                SqlDataReader rd;
                using (rd = cmd.ExecuteReader())
                {
                    if (rd.HasRows == false)
                    {
                        MessageBox.Show("Accoung does not exist", "Error");
                        textBox1.Clear();
                        textBox1.Select();
                    }
                    else if (rd.Read())
                    {

                        rd.Close();

                        
                        using (SqlCommand cmd3 = new SqlCommand("select * from loan where Account_No='"+textBox1.Text+"'",cn.con))
                        {
                            using (rd=cmd3.ExecuteReader())
                            {
                                if (rd.HasRows == false)
                                {
                                    rd.Close();
                                    using (SqlCommand cmd2 = new SqlCommand("insert into loan (Account_No,Loan_Amount,Monthly_Pay,Period,Interest,Loan_Status,Date) values(@acc,@loan,@mpay,@per,@inte,@loanstat,@date)", cn.con))
                                    {
                                        string str = "Pending";
                                        cmd2.Parameters.AddWithValue("@acc", textBox1.Text);
                                        cmd2.Parameters.AddWithValue("@loan", textBox2.Text);
                                        cmd2.Parameters.AddWithValue("@loanstat", str.ToString());
                                        cmd2.Parameters.AddWithValue("@mpay", textBox3.Text);

                                        cmd2.Parameters.AddWithValue("@per", textBox4.Text);
                                        cmd2.Parameters.AddWithValue("@inte", textBox5.Text.ToString());
                                        cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                                        try
                                        {
                                            cmd2.ExecuteNonQuery();
                                            MessageBox.Show("Loan Form Created", "Operation Complete");
                                            cn.con.Close();
                                            this.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("There is already a Loan for this Account","Request Denied");
                                    textBox1.Select();

                                }
                            }
                            
                        }

                        
                    }
                }
            }




        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers And Bigger than Zero[0].", "Invalid Input");
                textBox5.Text = textBox5.Text.Remove(textBox5.Text.Length - 1);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers And Bigger than Zero[0].", "Invalid Input");
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
            }
            var box = (TextBox)sender;
            if (box.Text.StartsWith("0")) box.Text = "";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers And Bigger than Zero[0].", "Invalid Input");
                textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers And Bigger than Zero[0].", "Invalid Input");
                textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);
            }
        }
    }
}
