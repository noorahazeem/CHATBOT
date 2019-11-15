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
    public partial class Admin_AddTeacher : Form
    {
        BaseConnection con = new BaseConnection();

        public static string uid = "";
        public static string tid = "";
        public static string coverpath = "";
        public static string  thumbpath= "";


        public Admin_AddTeacher()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = ofd.Filter = "Jpeg Images(*.jpg)|*.jpg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    profilepic.ImageLocation = ofd.FileName;
                    profilepic.BackgroundImageLayout = ImageLayout.Stretch;
                    pic.Text = ofd.FileName;
                    coverpath = ofd.FileName;



                   

                      }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while choosing cover pic.......");
            }
        }


        public static Image RoundCorners(Image StartImage, int CornerRadius, Color BackgroundColor)
        {
            CornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);
            Graphics g = Graphics.FromImage(RoundedImage);
            g.Clear(BackgroundColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new TextureBrush(StartImage);
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
            gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
            gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            gp.AddArc(0, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            g.FillPath(brush, gp);
            return RoundedImage;
        }




        public void user()
        {
            try
            {
                string query = "select isnull(max(Userid),100)+1 from login";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    uid = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating user Id........");
            }
        }

        public void teacherid()
        {
            try
            {
                string query = "select isnull(max(teacherid),100)+1 from Teacher_details";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    tid = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating user Id........");
            }
        }


       

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                teacherid();
                    user();
                   
                        coverpath = Application.StartupPath + "\\ProfilePictures\\" +uid + ".jpg";
                        System.IO.File.Copy(profilepic.ImageLocation.ToString(), coverpath);
                        coverpath = "\\ProfilePictures\\" + uid + ".jpg";

                        Image image = Image.FromFile(profilepic.ImageLocation);
                        Image thumbnail = image.GetThumbnailImage(60, 60, () => false, IntPtr.Zero);
                        Image RoundedImage = RoundCorners(thumbnail, 20, Color.Transparent);

                        thumbpath = Application.StartupPath + "\\ProfilePictures\\Thumbnails\\" + uid + ".jpg";
                        RoundedImage.Save(thumbpath);
                        thumbpath = "\\ProfilePictures\\Thumbnails\\" + uid + ".jpg";

                  
                    string utype="Teacher";
                    string query = "insert into login values(" + uid + ",'" + deuserid.Text + "','" + depassword.Text + "','" + utype + "','" + coverpath + "','" + thumbpath + "')";
                    if (con.exec1(query) > 0)
                    {
                        string query1 = "insert into Teacher_details values(" + tid + "," + uid + ",'" + tname.Text + "','" + dept.Text + "','" + mob.Text + "','" + mail.Text + "')";
                        if (con.exec1(query1) > 0)
                        {
                            MessageBox.Show("Teacher details Inserted Successfully...");
                            this.Close();
                        }
                        
                    }
                
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured....");
               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar == '.') || (e.KeyChar == '-') || (e.KeyChar == ' '))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void mob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == false)
            {
                if (e.KeyChar.Equals(Convert.ToChar(Keys.Back)))
                    e.Handled = false;
                else
                    e.Handled = true;
            }

        }




    }
}
