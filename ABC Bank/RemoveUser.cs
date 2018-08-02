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
    public partial class RemoveUser : Form
    {
        public RemoveUser()
        {
            InitializeComponent();
            label8.Text = DateTime.Now.ToLongDateString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connecntion cn = new Connecntion();
            cn.connOpen();
            SqlDataReader rd;
            using (SqlCommand cmd = new SqlCommand("Select Name,Password,Gender,DOB,Phone,Address from [user] where Username='" + textBox1.Text.ToString() + "'", cn.con))
            {
                using (rd = cmd.ExecuteReader())
                {
                    if (rd.HasRows == false)
                    {
                        MessageBox.Show("No Matching details found", "No Data!");
                    }
                    else if (rd.Read())
                    {

                        textBox2.Text = rd.GetString(0);
                        if (rd.GetString(2) == "Male")
                        {
                            radioButton1.Checked = true;
                        }
                        else
                        {
                            radioButton2.Checked = true;
                        }
                        textBox3.Text = rd.GetString(4);
                        dateTimePicker1.Value = rd.GetDateTime(3);
                        richTextBox1.Text = rd.GetString(5);
                    }

                }
                cn.con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Connecntion cn = new Connecntion();
            cn.connOpen();
            SqlDataReader rd;
            SqlCommand cmd;
            using (cmd = new SqlCommand("Select * from [user] where Username='" + textBox1.Text + "'", cn.con))
            {
                try
                {
                    rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        rd.Close();
                        cmd = new SqlCommand("Delete from [user] where Username='" + textBox1.Text + "'", cn.con);
                        try
                        {
                            rd = cmd.ExecuteReader();
                            MessageBox.Show("User Info Deleted!", "Operation Complete");
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No record found", "Result");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            cn.con.Close();
            this.Close();



        }
    }
}

