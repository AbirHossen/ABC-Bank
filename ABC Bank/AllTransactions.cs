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
    public partial class AllTransactions : Form
    {
        public AllTransactions()
        {
            InitializeComponent();
        }

        private void AllTransactions_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connecntion cn = new Connecntion();

            cn.connOpen();


            SqlDataAdapter d = new SqlDataAdapter("select * from [transaction]", cn.con);
            DataTable t = new DataTable();

            d.Fill(t);
            dataGridView1.DataSource = t;

            //DataSet ds = new DataSet();
            //d.Fill(ds,"transaction,transfer");
            //dataGridView1.DataSource = ds;
            //dataGridView1.DataMember = "transaction,transfer";


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
