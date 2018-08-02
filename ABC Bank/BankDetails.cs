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
    public partial class BankDetails : Form
    {
        public BankDetails()
        {
            InitializeComponent();
            label3.Text = DateTime.Now.ToLongDateString();
            setLabel();
        }
        public void setLabel()
        {
            Connecntion cn = new Connecntion();
            cn.connOpen();
            using (SqlCommand cmd=new SqlCommand("Select COALESCE(SUM(Balance), 0) from customer", cn.con))
            {
                
                object result = cmd.ExecuteScalar();
                label10.Text = Convert.ToString(result);

            }
            
            using(SqlCommand cmd= new SqlCommand("Select COALESCE(SUM(Loan_Amount), 0) from loan where Loan_Status='approved'", cn.con))
            {
                
                    object result = cmd.ExecuteScalar();
                    label11.Text = Convert.ToString(result);
                
                
            }
            using (SqlCommand cmd = new SqlCommand("Select Count(Username) from [user] ", cn.con))
            {
               
                    object result = cmd.ExecuteScalar();
                    label12.Text = Convert.ToString(result);
                
            }
            using(SqlCommand cmd= new SqlCommand("Select Count(Account_No) from customer ",cn.con))
            {
                object result=cmd.ExecuteScalar();
                label13.Text = Convert.ToString(result);
            }
            using (SqlCommand cmd = new SqlCommand("Select Count(Account_No) from loan where Loan_Status='approved'", cn.con))
            {
                
                    object result = cmd.ExecuteScalar();
                    label14.Text = Convert.ToString(result);
                
            }
            using (SqlCommand cmd = new SqlCommand("Select Count(Account_No) from fd",cn.con))
            {
                object result =cmd.ExecuteScalar();
                label15.Text = Convert.ToString(result);
            }
        }
        
        private void BankDetails_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
