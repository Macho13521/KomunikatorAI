using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KomunikatorAI
{
    public partial class Rozmowa : Form
    {
        string IDRozmowy;
        string UserLogin;

        string odbiorca;

        public static FirestoreChangeListener nasłuchiwacz;

        public Rozmowa(string ID, string user, string rozmówca)
        {
            InitializeComponent();
            IDRozmowy = ID;
            UserLogin = user;
            odbiorca = rozmówca;
        }

        private void Rozmowa_Load(object sender, EventArgs e)
        {
            info_rozmowa.Text = "Rozmowa z użytkownikiem: "+odbiorca;
            PobierzRozmowęAsync();
            //powiadomienia(); Nie działa
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WysyłanieWiadomości();
        }

        private async void WysyłanieWiadomości()
        {
            await Google.WyślijWiadomośćAsync(IDRozmowy, UserLogin, nowawiadomosc.Text);
            Google.AnalizaJęzykaAsync(nowawiadomosc.Text);
            nowawiadomosc.Text = "";
            PobierzRozmowęAsync();
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

        private void powiadomienia()
        {
            Query warunki = Google.db.Collection("Rozmowy").Document(IDRozmowy).Collection("Rozmowa");
            nasłuchiwacz = warunki.Listen(snapshot =>
            {
                
            });
        }

    
    }
}
