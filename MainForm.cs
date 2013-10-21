using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Coding4Fun_1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExampleForm1 frm = new ExampleForm1();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AuthenticationForm frm = new AuthenticationForm();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UploadForm frm = new UploadForm();
            frm.Show();
        }

        private void UpdateFormButton_Click(object sender, EventArgs e)
        {
            UpdateForm frm = new UpdateForm();
            frm.Show();
        }
    }
}