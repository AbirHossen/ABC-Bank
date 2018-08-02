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
    public partial class ChangePass : Form
    {
        public ChangePass()
        {
            InitializeComponent();
            button2.Text = "Validate";
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox1.Select();
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
            if(button2.Text== "Validate")
            {
                Connecntion cn = new Connecntion();
                using (SqlCommand StrQuer = new SqlCommand("SELECT * FROM [user] WHERE Username=@userid AND Password=@password", cn.con))
                {
                    cn.connOpen();
                    StrQuer.Parameters.AddWithValue("@userid", textBox1.Text.ToString());
                    StrQuer.Parameters.AddWithValue("@password", textBox2.Text.ToString());
                    using (SqlDataReader dr = StrQuer.ExecuteReader())
                    {
                        if (dr.HasRows == false)
                        {
                            MessageBox.Show("User ID Incorrect", "Error");


                        }
                        else
                        {

                            button2.Text = "Confirm";
                            textBox3.ReadOnly = false;
                            textBox4.ReadOnly = false;
                            textBox1.ReadOnly = true;
                            textBox2.ReadOnly = true;
                            passChange();

                        }
                    }
                }
            }
            else
            {
                passChange();
            }
            
        }
        public void passChange()
        {
            if (textBox3.Text==string.Empty || textBox4.Text==string.Empty)
            {
                MessageBox.Show("Please type new password and confirm password");
                textBox3.Select();
            }
            else if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Password didn't match","Error");
            }
            else
            {
                Connecntion cn = new Connecntion();
                using (SqlCommand cmd = new SqlCommand("update [user] set Password='"+textBox3.Text+"'where Username='"+textBox1.Text+"'",cn.con))
                {
                    cn.connOpen();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Password Updated","Operation Succeed");
                        this.Close();
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
    