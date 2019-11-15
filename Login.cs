using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ChatBot
{
    public partial class Login : Form
    {
        BaseConnection con=new BaseConnection();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are You a Student?", "Please Select a Type of User",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Student_Registration obj = new Student_Registration();
                ActiveForm.Hide();
                obj.Show();
            }
            else if (result == DialogResult.No)
            {
                Parent_Registration obj = new Parent_Registration();
                ActiveForm.Hide();
                obj.Show();
            }
            else if (result == DialogResult.Cancel)
            {
                //code for Cancel
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usertype = "";
            if (textBox1.Text == "admin" && textBox2.Text == "pwd")
            {
                Admin_Home obj = new Admin_Home();
                obj.Show();
            }
            else
            {
                string query = "select * from login where username='" + textBox1.Text + "' and password='" + textBox2.Text + "'";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    Program.userid = dr[0].ToString();
                    usertype = dr[3].ToString();

                    if (usertype == "Teacher")
                    {
                        Program.username = dr[1].ToString();
                        Program.picpath = Application.StartupPath + dr[5].ToString();
                        Teacher_Home obj = new Teacher_Home();
                        obj.Show();
                    }
                    else if (usertype == "Student")
                    {
                        Program.username = dr[1].ToString();
                        Program.picpath = Application.StartupPath + dr[5].ToString();
                        Student_Home obj = new Student_Home();
                        obj.Show();
                    }
                    else if (usertype == "Parent")
                    {
                        Program.username = dr[1].ToString();
                        Program.picpath = Application.StartupPath + dr[5].ToString();
                        Parent_Home obj = new Parent_Home();
                        obj.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid user.....");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
