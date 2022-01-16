using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KomunikatorAI
{
    public partial class Rozmowa : Form
    {
        string IDRozmowy;
        string UserLogin;

        public Rozmowa(string ID, string user)
        {
            InitializeComponent();
            IDRozmowy = ID;
            UserLogin = user;   
        }

        private void Rozmowa_Load(object sender, EventArgs e)
        {
            info_rozmowa.Text = "ID Rozmowy: "+IDRozmowy+" Użytkownik: "+UserLogin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WysyłanieWiadomości();
        }

        private async void WysyłanieWiadomości()
        {
            await Google.WyślijWiadomośćAsync(IDRozmowy, UserLogin, nowawiadomosc.Text);
            nowawiadomosc.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PobierzRozmowęAsync();
        }

        private async Task PobierzRozmowęAsync()
        {
            oknorozmowy.Clear();

            QuerySnapshot dane = await Google.PobieranieRozmowyAsync(IDRozmowy);

            ListViewItem linijka = new ListViewItem("Okno Rozmowy");

            foreach (DocumentSnapshot dokument in dane.Documents.Reverse())
            {
                Dictionary<string, object> wiadomość = dokument.ToDictionary();

                if (wiadomość["Nadawca"].ToString() == UserLogin)
                {
                    MessageBox.Show("Prawda - "+wiadomość["Nadawca"].ToString()+" : "+wiadomość["Treść"].ToString());
                    linijka.SubItems.Add("Nic");
                    linijka.SubItems.Add(wiadomość["Treść"].ToString());
                }
                else
                {
                    MessageBox.Show("Fałsz - " + wiadomość["Nadawca"].ToString() + " : " + wiadomość["Treść"].ToString());
                    linijka.SubItems.Add(wiadomość["Treść"].ToString());
                    linijka.SubItems.Add("Nic");
                }
            }
            oknorozmowy.Items.Add(linijka);
        }
    }
}
