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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            
            
        }
        Connecntion cn = new Connecntion();
        //public string conString = "Data Source=DESKTOP-0IIJOK5\\SQLEXPRESS;Initial Catalog=ABC;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection(conString);
            //con.Open();
            
            
            using (SqlCommand StrQuer = new SqlCommand("SELECT User_Type FROM [user] WHERE Username=@userid AND Password=@password", cn.con))
            {
                cn.connOpen();
                StrQuer.Parameters.AddWithValue("@userid", textBox1.Text);
                StrQuer.Parameters.AddWithValue("@password", textBox2.Text);
                using (SqlDataReader dr = StrQuer.ExecuteReader())
                {
                    
                    if (dr.HasRows)
                    {
                        dr.Close();
                        object result = StrQuer.ExecuteScalar();
                        string str = Convert.ToString(result);



                        if (str == "Admin")
                        {
                            MessageBox.Show("Admin  Login Successful!!", "Login");
                            AdminHome ah = new AdminHome();
                            ah.Show();
                            this.Hide();
                        }
                        else if (str == "Employee")
                        {
                            MessageBox.Show("Employee Login Successful!!", "Login");
                            EmployeeForm em = new EmployeeForm();
                            em.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Manager Login Successful!!", "Login");
                            ManagerForm m = new ManagerForm();
                            m.Show();
                            this.Hide();
                        }

                    }
                    else
                    {

                        MessageBox.Show("Username or Password incorrect");
                        textBox2.Clear();
                        textBox1.Select();
                    }
                }
                    
               

            }
            cn.con.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            cn.connClose();
        }
    }
}
    
