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
    public partial class Withdraw : Form
    {
        public Withdraw()
        {
            InitializeComponent();
            label6.Text = DateTime.Now.ToShortDateString();
            textBox1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Withdraw_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Connecntion cn = new Connecntion();
            cn.connOpen();
            SqlDataReader rd;
            using (SqlCommand cmd = new SqlCommand("Select Name,Balance from customer where Account_no='" + textBox1.Text + "'", cn.con))
            {
                using (rd = cmd.ExecuteReader())
                {
                    if (rd.HasRows == false)
                    {
                        MessageBox.Show("There is no Account with whis Account Number", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                    }
                    else if (rd.Read())
                    {
                        textBox5.Text = rd.GetString(0);
                        textBox6.Text = rd.GetInt32(1).ToString();

                    }
                }
            }
            cn.con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (textBox7.Text != string.Empty)
            {
                int anInteger;
                anInteger = Convert.ToInt32(textBox7.Text);
                anInteger = int.Parse(textBox7.Text);
                Connecntion cn = new Connecntion();
                cn.connOpen();
                int crrBalance;
                crrBalance = Convert.ToInt32(textBox6.Text);
                crrBalance = int.Parse(textBox6.Text);
                if (anInteger <= crrBalance)
                {
                    {
                        using (SqlCommand cmd = new SqlCommand("insert into withdraw(Account_No,Withdraw_Amount,Date) values (@acc,@wa,@date)", cn.con))
                        {
                            cmd.Parameters.AddWithValue("@acc", textBox1.Text);
                            cmd.Parameters.AddWithValue("@wa", textBox7.Text);
                            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                            try
                            {
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Withdraw Completed!", "Operation Complete");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("insert into [transaction] (Account_No,Debit,Date) values (@acc,@ca,@date)", cn.con))
                    {
                        cmd.Parameters.AddWithValue("@acc", textBox1.Text);
                        cmd.Parameters.AddWithValue("@ca", textBox7.Text);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Transaction Table Update!!!", "Operation Complete");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    
                    
                    int finalbalance = crrBalance - anInteger;
                    using (SqlCommand cmd = new SqlCommand("update customer set balance='" + finalbalance + "' where Account_No='" + textBox1.Text + "'", cn.con))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Customer Table Updated", "Operation Complete");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        cn.con.Close();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Withdrawal amount is more than Current Balance\nInput Again","Error Result");
                    textBox7.Clear();
                    textBox7.Select();
                }


                cn.con.Close();
            }
            
            else {
                MessageBox.Show("Input Withdrawal Amount", "No Value");
                textBox7.Clear();
                textBox7.Select();
            }
            
           

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox7.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers And Bigger than Zero[0].", "Invalid Input");
                textBox7.Text = textBox7.Text.Remove(textBox7.Text.Length - 1);
            }
            
                var box = (TextBox)sender;
                if (box.Text.StartsWith("0")) box.Text = "";
            
        }
    }
}
