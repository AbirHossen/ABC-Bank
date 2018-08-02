using System;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace ABC_Bank
{
    class Connecntion
    {
        public static string connString = "Data Source=DESKTOP-0IIJOK5\\SQLEXPRESS;Initial Catalog=ABC;Integrated Security=True";
        public SqlConnection con = new SqlConnection(connString);
        public void connOpen()
        {
            try { con.Open(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void connClose()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            
        }
    }

         
}
