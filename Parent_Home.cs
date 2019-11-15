using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ChatBot
{
    public partial class Parent_Home : Form
    {
        public Parent_Home()
        {
            InitializeComponent();
            toolStripButton8.Image = (Image)Image.FromFile(Program.picpath);
            toolStripButton8.Text = "Welcome " + Program.username;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Chatinbox obj = new Chatinbox();
            obj.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

            Startup obj = new Startup();
            obj.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            botcheck obj = new botcheck();
            obj.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Chatuser obj = new Chatuser();
            obj.Show();
        }
    }
}
