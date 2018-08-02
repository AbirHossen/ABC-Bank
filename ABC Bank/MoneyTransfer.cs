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
    public partial class MoneyTransfer : Form
    {
        public MoneyTransfer()
        {
            InitializeComponent();
            label7.Text = DateTime.Now.ToShortDateString();
        }

        private void MoneyTransfer_Load(object sender, EventArgs e)
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
            Connecntion cn = new Connecntion();
            cn.connOpen();
            SqlDataReader rd;
            using (SqlCommand cmd = new SqlCommand("Select Name,Balance from customer where Account_No='"+textBox2.Text+"'", cn.con))
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
                        
                        textBox3.Text =rd.GetString(0);
                        //int crrBalance;
                        //crrBalance = Convert.ToInt32(textBox1.Text);
                        //crrBalance = int.Parse(textBox1.Text);
                        //int i;
                        //string str = rd.GetString(1);
                        //i = Convert.ToInt32(str);
                        textBox1.Text = rd.GetInt32(1).ToString();

                    }
                }
            }
            cn.con.Close();

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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers And Bigger than Zero[0].", "Invalid Input");
                textBox4.Text = textBox4.Text.Remove(textBox4.Text.Length - 1);
            }
            var box = (TextBox)sender;
            if (box.Text.StartsWith("0")) box.Text = "";

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

            if (textBox5.Text != string.Empty) {
                Connecntion cn = new Connecntion();
                cn.connOpen();
                SqlDataReader rd;
                using (SqlCommand cmd = new SqlCommand("select * from customer where Account_No='" + textBox4.Text + "'", cn.con))
                {

                    using (rd = cmd.ExecuteReader())
                    {
                        if (rd.HasRows == false)
                        {
                            MessageBox.Show("No Account Found\ninput again", "Result");
                            textBox4.Clear();
                            textBox4.Select();
                        }


                        else if (rd.Read())
                        {


                            rd.Close();
                            using (SqlCommand cmd2 = new SqlCommand("update customer set Balance=Balance+'" + textBox5.Text + "'where Account_no='" + textBox4.Text + "'", cn.con))
                            {

                                try
                                {
                                    cmd2.ExecuteNonQuery();

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            using (SqlCommand cmd2 = new SqlCommand("update customer set Balance=Balance-'" + textBox5.Text + "' where Account_No='" + textBox2.Text + "'", cn.con))
                            {

                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Customer Table Updated", "Operation Complete");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                            using (SqlCommand cmd2 = new SqlCommand("insert into [transfer] (Payee_Account,Recv_Account,Transfer_Amount,Date) values(@pa,@ra,@amount,@date) ", cn.con))
                            {
                                cmd2.Parameters.AddWithValue("@pa", textBox2.Text);
                                cmd2.Parameters.AddWithValue("@ra", textBox4.Text);
                                cmd2.Parameters.AddWithValue("@amount", textBox5.Text);
                                cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));

                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Transfer Table Updated!", "Operation Complete");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                            }
                            using (SqlCommand cmd2 = new SqlCommand("insert into [transaction] (Account_No,TransferFrom,TransferTo,Amount,Date) values (@acc,@tf,@tt,@amount,@date)", cn.con))
                            {
                                cmd2.Parameters.AddWithValue("@acc", textBox2.Text.ToString());
                                cmd2.Parameters.AddWithValue("@tf", textBox2.Text);
                                cmd2.Parameters.AddWithValue("@tt", textBox4.Text);
                                cmd2.Parameters.AddWithValue("@amount", textBox5.Text);
                                cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Transaction Complete!", "OPeration Succedd!!");
                                    cn.con.Close();
                                    this.Close();

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
            else
            {
                MessageBox.Show("Enter Amount!","Error");
            }
            
        }
    }
}
