using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using FlickrNet;

namespace Coding4Fun_1
{
    public partial class AuthenticationForm : Coding4Fun_1.TemplateForm
    {
        public AuthenticationForm()
        {
            InitializeComponent();
        }

        private string Frob;

        private void Step1Button_Click(object sender, EventArgs e)
        {
            Flickr flickr = new Flickr(ApiKey.Text, SharedSecret.Text);

            Frob = flickr.AuthGetFrob();

            OutputTextbox.Text += "Frob = " + Frob + "\r\n";

            string url = flickr.AuthCalcUrl(Frob, AuthLevel.Write);

            OutputTextbox.Text += "Url = " + url + "\r\n";

            System.Diagnostics.Process.Start(url);

            Step1Button.Enabled = false;
            Step2Button.Enabled = true;
            Step3Button.Enabled = true;
        }

        private void Step2Button_Click(object sender, EventArgs e)
        {
            Flickr flickr = new Flickr(ApiKey.Text, SharedSecret.Text);

            try
            {
                Auth auth = flickr.AuthGetToken(Frob);
                OutputTextbox.Text += "User Authenticated = " + auth.User.UserName + "\r\n";
                OutputTextbox.Text += "Auth Token = " + auth.Token + "\r\n";

                AuthToken.Text = auth.Token;
            }
            catch (FlickrException ex)
            {
                OutputTextbox.Text += "Authentication failed : " + ex.Message + "\r\n";
            }

            Step1Button.Enabled = true;
            Step2Button.Enabled = false;
            Step3Button.Enabled = false;
        }

        private void Step3Button_Click(object sender, EventArgs e)
        {
            Step1Button.Enabled = true;
            Step2Button.Enabled = false;
            Step3Button.Enabled = false;
        }
    }
}

