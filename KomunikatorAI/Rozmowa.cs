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

using static KomunikatorAI.Menu;

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
            AnalizaJęzykaAsync(nowawiadomosc.Text);
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

        private async Task AnalizaJęzykaAsync(string wiadomość)
        {
            string[] słowa = wiadomość.Split(' ', '.', ',', '?', '!', '"', '*');

            foreach (var słowo in słowa)
            {
                if(słowo!="")
                {
                    Query zapytanie = Google.db.Collection("Słownik").WhereEqualTo("Wyraz", słowo);
                    QuerySnapshot dane = await zapytanie.GetSnapshotAsync();

                    if (dane.Documents.Count==1)
                    {
                        DocumentReference dokument = Google.db.Collection("Słownik").Document(dane.Documents.First().Id);
                        await dokument.UpdateAsync("Popularność", FieldValue.Increment(1));
                    }
                    else
                    {
                        CollectionReference kolekcja = Google.db.Collection("Słownik");
                        Dictionary<string, object> nowesłowo = new Dictionary<string, object>
                        {
                            {"Wyraz", słowo},
                            {"Popularność", 1}
                        };
                        await kolekcja.AddAsync(nowesłowo);
                    }
                }
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
