using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace ABC_Bank
{
    public partial class CustomerInfoUpdate : Form
    {
        public CustomerInfoUpdate()
        {
            InitializeComponent();
            label15.Text = DateTime.Now.ToShortDateString();
        }
        

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connecntion cn = new Connecntion();
            cn.connOpen();
            SqlDataReader rd;
            
            using (SqlCommand cmd = new SqlCommand("Select * from customer where Account_no=@ac", cn.con))
            {
                cmd.Parameters.AddWithValue("@ac", textBox1.Text);
                rd = cmd.ExecuteReader();
                if (rd.HasRows == false)
                {
                    MessageBox.Show("Account No Unavailable");
                }
                else if (rd.Read())
                {

                    textBox2.Text = rd.GetString(1);
                    textBox4.Text = rd.GetString(9);
                    textBox5.Text = rd.GetString(10);
                    textBox8.Text = rd.GetString(3);
                    textBox9.Text = rd.GetString(4);
                    richTextBox1.Text = rd.GetString(5);
                    textBox10.Text = rd.GetString(6);
                    textBox3.Text = rd.GetInt32(11).ToString();
                    dateTimePicker1.Value = rd.GetDateTime(2);

                    if (rd.GetString(7)=="Male")
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                    if(rd.GetString(8)=="Married")
                    {
                        radioButton3.Checked = true;
                    }
                    else
                    {
                        radioButton4.Checked = true;
                    }
                    byte[] img = (byte[])(rd[12]);
                    if (img == null)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                    

                }



            }
            cn.con.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == string.Empty)
                {
                    MessageBox.Show("Enter a Customer Name to search Details", "Emplty Box", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    customerTableAdapter.searchName(aBCDataSet.customer, textBox2.Text);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
       

        private void CustomerInfoUpdate_Load(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {


            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty && textBox5.Text != string.Empty && textBox8.Text != string.Empty && textBox9.Text != string.Empty && textBox10.Text != string.Empty && richTextBox1.Text != string.Empty)
            {//if textbox r not empty
                if (radioButton1.Checked || radioButton2.Checked && radioButton3.Checked || radioButton4.Checked)
                {//if radio buttons are selected correctly
                    if (dateTimePicker1.Value < DateTime.Today.AddYears(-18)
                        && dateTimePicker1.Value > DateTime.Today.AddYears(-100))
                    {   //after checking the customer is >18

                        if (ucount>=1)
                        {
                            
                                if (MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    inQueryP();
                                }

                            

                        }
                        else
                        {
                            if (MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                inQuery();
                            }
                        }

                    }
                    else
                    {

                        MessageBox.Show("Wrong Date of Birth");
                    }
                }
                else { MessageBox.Show("Check All Radio buttons"); }


            }
            else { MessageBox.Show("Input all the informations","Information Missing"); }


        }
        public string gen, mstat;
        public void inQueryP()
        {
            Connecntion cn = new Connecntion();
            cn.connOpen();
            using (SqlCommand cmd = new SqlCommand("Update customer set Name='" + textBox2.Text + "',DOB='" + dateTimePicker1.Text + "',Phone_No='" + textBox8.Text + "',Email='" + textBox9.Text + "',Address='" + richTextBox1.Text + "',District='" + textBox5.Text + "',Gender=@gen,Marital_Status=@mstat,Father_Name='" + textBox4.Text + "',Mother_Name='" + textBox5.Text + "',Balance='" + textBox3.Text + "',Photo=@img where Account_No='" + textBox1.Text + "'", cn.con))
            {
                byte[] image = getPic();
                cmd.Parameters.AddWithValue("@gen", gen);
                cmd.Parameters.AddWithValue("@mstat", mstat);
                cmd.Parameters.AddWithValue("@img", image);
                try
                {
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (MessageBox.Show("Info Updated!", "Operation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    { Clear_Boxes(); }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            cn.con.Close();

        }
        public void inQuery()
        {
            
            Connecntion cn = new Connecntion();
            cn.connOpen();
            using (SqlCommand cmd = new SqlCommand("Update customer set Name='"+textBox2.Text+"',DOB='"+ dateTimePicker1.Text + "',Phone_No='"+textBox8.Text+"',Email='"+ textBox9.Text + "',Address='"+richTextBox1.Text +"',District='"+ textBox10.Text + "',Gender=@gen,Marital_Status=@mstat,Father_Name='"+ textBox4.Text + "',Mother_Name='"+ textBox5.Text + "',Balance='"+ textBox3.Text + "' where Account_No='"+textBox1.Text+"'", cn.con))
            {
                cmd.Parameters.AddWithValue("@gen", gen);
                cmd.Parameters.AddWithValue("@mstat", mstat);
                try {
                    SqlDataReader rd = cmd.ExecuteReader();
                    MessageBox.Show("Info Updated!", "Operation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Boxes(); 
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
             }
            cn.con.Close();
            
        }
        public void Clear_Boxes()
        {
            richTextBox1.Clear();
            
            pictureBox1.Image = null;

            foreach (Control Cleartext in Controls)
            {
                if (Cleartext is TextBox)
                {
                    ((TextBox)Cleartext).Text = string.Empty;
                }
            }
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;



        }

        public byte[] getPic()
        {

            byte[] img = null;
            FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            img = br.ReadBytes((int)fs.Length);
            return img;


        }
        public string imgLoc;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.", "Invalid Input");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.", "Invalid Input");
                textBox3.Text = textBox3.Text.Remove(textBox3.Text.Length - 1);
            }
        }
        public int ucount = 0;
        private void button3_Click_1(object sender, EventArgs e)
        {
            ucount = +1;
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "JPG(*.JPG)|*.jpg";
            f.Title = "Select Picture";
            if (f.ShowDialog() == DialogResult.OK)
            {
                imgLoc = f.FileName.ToString();
                pictureBox1.ImageLocation = imgLoc;
            }
            else
            {
                button3.PerformClick();
            }
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            gen = "Male";

        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            gen = "Female";
        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
            mstat = "Unmarried";
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            mstat = "Married";
        }

        
    }
}
