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
    public partial class UpdateUser : Form
    {
        public UpdateUser()
        {
            InitializeComponent();
            label8.Text = DateTime.Today.ToShortDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
                        textBox1.ReadOnly = true;
                        textBox1.BackColor = SystemColors.Window;
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
            }
            cn.con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && richTextBox1.Text != string.Empty)
            {//if textbox r not empty
                if (radioButton1.Checked || radioButton2.Checked)
                {//if radio buttons are selected correctly
                    if (dateTimePicker1.Value < DateTime.Today.AddYears(-18)
                        && dateTimePicker1.Value > DateTime.Today.AddYears(-100))
                    {   //after checking the customer is >18


                        inQuery();

                    }
                    else
                    {

                        MessageBox.Show("Wrong Date of Birth");
                    }
                }
                else { MessageBox.Show("Choose a gender"); }


            }
            else { MessageBox.Show("Input all the informations"); }






        }
        public string gen;
        public void inQuery()
        {
            
            Connecntion cn = new Connecntion();
            cn.connOpen();
            using (SqlCommand cmd = new SqlCommand("update  [user] set Name=@name,Username=@uname,Gender=@gen,DOB=@dob,Phone=@pno,Address=@add where Username='"+textBox1.Text+"'", cn.con))
            {
                cmd.Parameters.AddWithValue("@name", textBox2.Text);
                cmd.Parameters.AddWithValue("@uname", textBox1.Text);

                cmd.Parameters.AddWithValue("@add", richTextBox1.Text);

                cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@pno", textBox3.Text);
                cmd.Parameters.AddWithValue("@gen", gen);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User updated to database", "Operation Sucess");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cn.con.Close();
                this.Close();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                gen = "Male";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                gen = "Female";
            }
        }
    }
}
