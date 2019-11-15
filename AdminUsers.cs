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
    public partial class AdminUsers : Form
    {
        BaseConnection con = new BaseConnection();
        public AdminUsers()
        {
            InitializeComponent();
            fillgrid();
        }
        public void fillgrid()
        {
            try
            {
                string query = "select * from Teacher_details";
                DataSet ds = con.ret_ds(query);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

                string query1 = "select * from Student_details";
                DataSet ds1 = con.ret_ds(query1);
                dataGridView2.DataSource = ds1.Tables[0].DefaultView;

                string query2 = "select * from Parent_details";
                DataSet ds2 = con.ret_ds(query2);
                dataGridView3.DataSource = ds2.Tables[0].DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
    }
}
