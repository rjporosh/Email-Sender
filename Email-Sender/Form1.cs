using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace Email_Sender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtEmail.Text.Contains("@gmail.com"))
                {
                    MessageBox.Show("You need to Provide a Gmail Credentials.");
                    return;
                }

                btnSend.Enabled = false;

                var message = new MailMessage
                {
                    From = new MailAddress(txtEmail.Text.Trim()), Subject = txtSubject.Text, Body = txtBody.Text
                };
               
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential()
                    {
                        UserName = txtEmail.Text.Trim(), Password = txtPassword.Text.Trim()
                    },
                    EnableSsl = true,
                    //Timeout = 10000,
                    //DeliveryMethod = SmtpDeliveryMethod.Network,
                    //UseDefaultCredentials = false
            };
                //message.BodyEncoding = UTF8Encoding.UTF8;
                //message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                foreach (var s in txtRecipient.Text.Split(';'))
                {
                    message.To.Add(s);
                }
                client.Send(message);
                MessageBox.Show("E-mail Sent Successfully.");
            }
            catch
            {
                MessageBox.Show(
                    "There was an error to sending the mail. Make sure you have typed your credential perfectly and you have an active Internet Connection.And plz Enable the Less Secure App Access to Your Gmail. go to https://myaccount.google.com/lesssecureapps?pli=1 and enable it.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { btnSend.Enabled = true; }
        }
    }
}
