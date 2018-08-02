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
    public partial class FdForm : Form
    {
        public FdForm()
        {
            InitializeComponent();
            label7.Text = DateTime.Now.ToShortDateString();
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
            if(textBox1.Text != string.Empty || textBox3.Text != string.Empty || textBox4.Text != string.Empty || textBox5.Text != string.Empty)
            {
                if (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true )
                { inQuery(); }
                else
                {
                    MessageBox.Show("Check type of the FD Plan","Type not found");
                }
            }
            else
            {
                MessageBox.Show("Please supply all the information","Datamissing");
            }

        }
        public void inQuery()
        {
            Connecntion cn = new Connecntion();
            cn.connOpen();
            SqlDataReader rd;
            using (SqlCommand cmd2 = new SqlCommand("Select * from customer where Account_No='"+textBox1.Text+"'",cn.con))
            {
                using (rd = cmd2.ExecuteReader()) {
                    if (rd.HasRows==false)
                    {
                        MessageBox.Show("Account does not exist","Resuult");
                        textBox1.Clear();
                        textBox1.Select();
                    }
                    else if (rd.Read())
                    {
                        rd.Close();
                        using (SqlCommand cmd = new SqlCommand("insert into fd (Account_No,Type,Amount,Period,Interest,Date) values(@acc,@type,@amount,@per,@inte,@date)", cn.con))
                        {
                            cmd.Parameters.AddWithValue("@acc", textBox1.Text);
                            cmd.Parameters.AddWithValue("@type", rb);
                            cmd.Parameters.AddWithValue("@amount", textBox3.Text);
                            cmd.Parameters.AddWithValue("@per", textBox4.Text);
                            cmd.Parameters.AddWithValue("@inte", textBox5.Text.ToString());
                            cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                            try
                            {
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Fd Form Created", "Operation Complete");
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
        public string rb;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                rb = "Flexi";
                textBox5.Text = "3";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                rb = "Regular";
                textBox5.Text = "4";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                rb = "5 Year Plan";
                textBox5.Text = "5";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, "60"))
            {
                textBox5.Text = "5";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers And Bigger than Zero[0].", "Invalid Input");
                textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);


            }
            var box = (TextBox)sender;
            if (box.Text.StartsWith("0")) box.Text = "";
        }
    }
}
