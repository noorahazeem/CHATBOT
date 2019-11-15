using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace ChatBot
{
    public partial class Chat : Form
    {
        BaseConnection con = new BaseConnection();
        public static string senderid = "";
        public static string receiver = "";

        public static string senderimg = "";
        public static string receiverimg = "";

        public static string cid = "";
        public static string mid = "";
        public Chat()
        {
            InitializeComponent();
        }

        public Chat(string sid,string rid)
        {
            InitializeComponent();
            loadimagedetails(sid, rid);
            senderid = sid;
            receiver = rid;
            chatid();
           
        }

        public Chat(string sid, string rid,string chatid)
        {
            InitializeComponent();
            loadimagedetails(sid, rid);
            senderid = sid;
            receiver = rid;
            cid = chatid;
            populatelist();
        }

        public void populatelist()
        {
            listView1.Items.Clear();
            int count = 0;
            string query = "select * from chat where chatid=" + cid + " order by msgid asc";
            string simage = "";
            SqlDataReader dr1 = con.ret_dr(query);
            while (dr1.Read())
            {
                string query1 = "select Thumbnail from login where userid='" + dr1[5].ToString() + "'";
                SqlDataReader dr2 = con.ret_dr(query1);
                if (dr2.Read())
                {
                    simage = dr2[0].ToString();
                   
                }
                
                imageList1.Images.Add(Image.FromFile(Application.StartupPath +simage));
                ListViewItem lst = new ListViewItem(dr1[2].ToString(), count);
                
                listView1.Items.Add(lst);
                count++;

                listView1.Refresh();

            }
        }

        public void populatelist(string mid)
        {
            imageList1.Images.Clear();
            listView1.Items.Clear();
            int count = 0;
            string query = "select * from chat where chatid=" + cid + " order by msgid asc";
            string simage = "";
            SqlDataReader dr1 = con.ret_dr(query);
            while (dr1.Read())
            {
                string query1 = "select Thumbnail from login where userid='" + dr1[5].ToString() + "'";
                SqlDataReader dr2 = con.ret_dr(query1);
                simage = "";
                if (dr2.Read())
                {
                    simage = dr2[0].ToString();

                }

                imageList1.Images.Add(Image.FromFile(Application.StartupPath + simage));
                ListViewItem lst = new ListViewItem(dr1[2].ToString(), count);

                listView1.Items.Add(lst);
                count++;

                listView1.Refresh();

            }
        }
        public void chatid()
        {
            try
            {
                string query = "select isnull(max(chatid),100)+1 from chat";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    cid = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating chat Id........");
            }
        }

        public void msgid(string id)
        {
            try
            {
                string query = "select isnull(max(msgid),100)+1 from chat where chatid='"+id+"'";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    mid = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating message Id........");
            }
        }

        public void loadimagedetails(string sid,string rid)
        {
            try
            {
                string query = "select Thumbnail from login where userid='"+sid+"'";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    senderimg = dr[0].ToString();
                    simage.Image = Image.FromFile(Application.StartupPath + senderimg);

                }

                string query1 = "select Thumbnail,username from login where userid='" + rid + "'";
                SqlDataReader dr1 = con.ret_dr(query1);
                if (dr1.Read())
                {
                    receiverimg = dr1[0].ToString();
                    rimage.Image = Image.FromFile(Application.StartupPath + receiverimg);
                    rname.Text = dr1[1].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating user Id........");
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                string date=System.DateTime.Now.ToShortDateString();
                string time=System.DateTime.Now.ToShortTimeString();
                string datetime= date +"  "+time;
                string status= "unseen";
                msgid(cid);
                string query = "insert into chat values(" + cid + "," + mid + ",'" + msgbox.Text + "','" + datetime + "','" + status + "','" + senderid + "','" + receiver + "')";
                 if (con.exec1(query) > 0)
                 {
                   //  MessageBox.Show("Message Sent........");
                     msgbox.Text = "";
                 }
                 populatelist(mid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while chatting........");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Chatinbox obj = new Chatinbox();
            obj.Show();
        }

         //try
         //   {
         //       string date = System.DateTime.Now.ToShortDateString();
         //       string time = System.DateTime.Now.ToShortTimeString();
         //       string datetime = date + "  " + time;
         //       string status = "unseen";
         //       msgid(cid);
         //       string query = "insert into chat values(" + cid + "," + mid + ",'" + msgbox.Text + "','" + datetime + "','" + status + "','" + receiver + "')";
         //       if (con.exec1(query) > 0)
         //       {
         //           MessageBox.Show("Message Sent........");
         //       }
         //       populatelist();
         //   }
         //   catch (Exception ex)
         //   {
         //       MessageBox.Show("Error while chatting........");
         //   }
    }
}
