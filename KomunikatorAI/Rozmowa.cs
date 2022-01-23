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
            OdświeżanieCzatu();
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
        }

        private async Task PobierzRozmowęAsync()
        {
            if (oknorozmowy.InvokeRequired)
            {
                oknorozmowy.Invoke(new Action(() => { oknorozmowy.Items.Clear(); }));
            }

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
                if (oknorozmowy.InvokeRequired)
                {
                    oknorozmowy.Invoke(new Action(() => { oknorozmowy.Items.Add(linijka); }));
                }
            }
        }

        private void OdświeżanieCzatu()
        {
            Query warunki = Google.db.Collection("Rozmowy").Document(IDRozmowy).Collection("Rozmowa");
            nasłuchiwacz = warunki.Listen(snapshot =>
            {
                PobierzRozmowęAsync();
            });
        }

        private void nowawiadomosc_TextChanged(object sender, EventArgs e)
        {
            Podpowiedzi();
        }

        private void Podpowiedzi()
        {
            List<string> Sugestie = new List<string>();
            Sugestie = Google.SugestiaAsync(nowawiadomosc.Text).Result;

            for (int x=0; x<Sugestie.Count; x++)
            {
                sugestie.Items.Add(Sugestie[x]);
            }
        }
    }
}
