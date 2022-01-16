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
            PobierzRozmowęAsync();
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
            oknorozmowy.Items.Clear();

            QuerySnapshot dane = await Google.PobieranieRozmowyAsync(IDRozmowy);


            foreach (DocumentSnapshot dokument in dane.Documents.Reverse())
            {
                ListViewItem linijka = new ListViewItem("Czat");
                Dictionary<string, object> wiadomość = dokument.ToDictionary();

                if (wiadomość["Nadawca"].ToString() == UserLogin)
                {
                    linijka.Text = "";
                    linijka.SubItems.Add(wiadomość["Treść"].ToString());
                }
                else
                {
                    linijka.SubItems.Add("");
                    linijka.Text = wiadomość["Treść"].ToString();
                }
                oknorozmowy.Items.Add(linijka);
            }
            
        }
    }
}
