using Google.Cloud.Firestore;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KomunikatorAI.szablony;

namespace KomunikatorAI
{
    public partial class Logowanie : Form
    {
        public static User użytkownik;
        public static bool wyłącz = true;

        public Logowanie()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Logowanie.zamykanie);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoryzacjaAsync();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Rejestracja().Show();
            this.Hide();
        }

        private void Logowanie_Load(object sender, EventArgs e)
        {
            Google.Connect();
        }


        private async Task AutoryzacjaAsync()
        {
            QuerySnapshot dokumenty = await Google.Logowanie(wpisanylogin.Text, wpisanehaslo.Text);
            
            if (dokumenty != null)
            {
                użytkownik = dokumenty.First().ConvertTo<User>();
                new Menu().Show();
                this.Hide();
            }
            else
            {
                użytkownik = null;
                MessageBox.Show("Niepoprawne dane");
            }
        }

        public static void zamykanie(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (wyłącz)
                {
                    Application.Exit();
                }
                else
                {
                    wyłącz = true;
                }
            }
        }
    }
}
