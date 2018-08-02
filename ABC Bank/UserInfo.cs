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
    public partial class UserInfo : Form
    {
        public UserInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void UserInfo_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'user_Login_Type.user' table. You can move, or remove it, as needed.
            this.userTableAdapter.Fill(this.user_Login_Type.user);

        }

        private void button2_Click(object sender, EventArgs e)
        {

            
            
            if(textBox2.Text != string.Empty)
            {
                Connecntion cn = new Connecntion();
                cn.connOpen();
                using (SqlCommand cmd = new SqlCommand("select Username from [user] where Username=@uname", cn.con))
                {
                    cmd.Parameters.AddWithValue("@uname", textBox2.Text);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        MessageBox.Show("Username already exists!" +
                            "\nType another one", "Warning!!");
                        textBox2.Clear();
                    }
                }
                cn.con.Close();
            }

            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty && richTextBox1.Text != string.Empty)
                {//if textbox r not empty
                    if (radioButton1.Checked || radioButton2.Checked)
                    {//if radio buttons are selected correctly
                        if (dateTimePicker1.Value < DateTime.Today.AddYears(-18)
                            && dateTimePicker1.Value > DateTime.Today.AddYears(-100))
                        {   //after checking the customer is >18

                            if (comboBox1.Text == string.Empty)
                            {
                                MessageBox.Show("Pick a role", "Combobox unchecked");

                            }
                            else
                            {
                                inQuery();
                            }
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
            using(SqlCommand cmd = new SqlCommand("insert into [user] (Name,Username,Password,Gender,DOB,User_Type,Phone,Address) values (@name,@uname,@pass,@gen,@dob,@utype,@pno,@add)", cn.con))
            {
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@uname", textBox2.Text);
                cmd.Parameters.AddWithValue("@pass",textBox4.Text);
                cmd.Parameters.AddWithValue("@add", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@utype", comboBox1.Text);
                cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@pno", textBox3.Text);
                cmd.Parameters.AddWithValue("@gen", gen);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User added to database","Operation Sucess");
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
            gen = "Male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gen = "Female";
        }

        
    }
}
