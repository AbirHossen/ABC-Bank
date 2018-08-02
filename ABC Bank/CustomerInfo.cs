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
using System.IO;

namespace ABC_Bank
{
    public partial class CustomerForm : Form
    {      
        public CustomerForm()
        {
            this.BackgroundImage = Properties.Resources.background;
            InitializeComponent();
            label16.Text = "Date:" + DateTime.Now.ToLongDateString();//setting up the date
            textBox2.Select();
            
            
        }
        public void acc()
        {
            Connecntion cn = new Connecntion();
            cn.connOpen();
            SqlCommand cmd = new SqlCommand("select max(Account_no+1) from customer", cn.con);

            //int count = (int)cmd.ExecuteScalar();
            textBox1.Text = cmd.ExecuteScalar().ToString();
            cn.con.Close();
        }

        
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            acc();
        }

        private void button3_Click(object sender, EventArgs e)//cancel button
        {
            if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }




        
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty && textBox5.Text != string.Empty && textBox8.Text != string.Empty && textBox9.Text != string.Empty && textBox10.Text != string.Empty && richTextBox1.Text != string.Empty)
            {//if textbox r not empty
                if(radioButton1.Checked || radioButton2.Checked && radioButton3.Checked || radioButton4.Checked)
                {//if radio buttons are selected correctly
                    if (dateTimePicker1.Value < DateTime.Today.AddYears(-18)
                        && dateTimePicker1.Value > DateTime.Today.AddYears(-100))
                    {   //after checking the customer is >18

                        if(imgLoc == null)
                        {
                            if (MessageBox.Show("No Customer Image\nInput image now?", "No image Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                button1.PerformClick();
                                if (MessageBox.Show("Are you sure you want to submit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes){
                                    inQuery();
                                }
                                
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
                    else {

                        MessageBox.Show("Wrong Date of Birth");
                    }
                }
                else { MessageBox.Show("Check All Radio buttons"); }


            }
            else {MessageBox.Show("Input all the informations");}
        }

        public string gen, mstat;
        public void inQuery()
        {
            Connecntion cn = new Connecntion();
            
                cn.connOpen();
            
            using (SqlCommand cmd = new SqlCommand("insert into customer(Account_No,Name,DOB,Phone_No,Email,Address,District,Gender,Marital_Status,Father_Name,Mother_Name,Balance,Photo) values (@ac,@na,@dob,@pno,@em,@add,@dis,@gen,@msta,@fn,@mn,@bl,@ph)", cn.con))
            {
                byte[] image = getPic();
                cmd.Parameters.AddWithValue("@ac", textBox1.Text);
                cmd.Parameters.AddWithValue("@na", textBox2.Text);
                cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@pno", textBox3.Text);
                cmd.Parameters.AddWithValue("@em", textBox4.Text);
                cmd.Parameters.AddWithValue("@add", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@dis", textBox5.Text);
                cmd.Parameters.AddWithValue("@gen", gen);
                cmd.Parameters.AddWithValue("@msta", mstat);
                cmd.Parameters.AddWithValue("@fn", textBox8.Text);
                cmd.Parameters.AddWithValue("@mn", textBox9.Text);
                cmd.Parameters.AddWithValue("@bl", textBox10.Text);
                cmd.Parameters.AddWithValue("@ph", image);


                try { cmd.ExecuteNonQuery();
                    MessageBox.Show("Information Saved!", "Operation Completed");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                
            }
            cn.con.Close();
            this.Close();


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
        
        private void button1_Click(object sender, EventArgs e)
        {
            
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
                button1.PerformClick();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.", "Invalid Input");
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox10.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.", "Invalid Input");
                textBox10.Text = textBox10.Text.Remove(textBox10.Text.Length - 1);
            }
            var box = (TextBox)sender;
            if (box.Text.StartsWith("0")) box.Text = "";

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gen = "Male";

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gen = "Female";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            mstat = "Unmarried";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            mstat = "Married";
        }
    }
}
