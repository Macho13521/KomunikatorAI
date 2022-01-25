using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KomunikatorAI.Menu;

namespace KomunikatorAI
{
    public partial class Rozmowa : Form
    {
        string IDRelacji;
        string UserLogin;
        string odbiorca;

        public static FirestoreChangeListener nasłuchiwacz;

        public Rozmowa(string ID, string user, string rozmówca)
        {
            InitializeComponent();
            IDRelacji = ID;
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
            await Google.WyślijWiadomośćAsync(IDRelacji, UserLogin, nowawiadomosc.Text);
            if (użytkownik.Nauczyciel)
            {
                Google.AnalizaJęzykaAsync(nowawiadomosc.Text);
            }
            nowawiadomosc.Text = "";
        }

        private async Task PobierzRozmowęAsync()
        {
            if (oknorozmowy.InvokeRequired)
            {
                oknorozmowy.Invoke(new Action(() => { oknorozmowy.Items.Clear(); }));
            }

            QuerySnapshot dane = await Google.PobieranieRozmowyAsync(IDRelacji);

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
            Query warunki = Google.db.Collection("Relacje").Document(IDRelacji).Collection("Rozmowa");
            nasłuchiwacz = warunki.Listen(snapshot =>
            {
                PobierzRozmowęAsync();
            });
        }

        private void nowawiadomosc_TextChanged(object sender, EventArgs e)
        {
            PodpowiedziAsync();
        }

        private async Task PodpowiedziAsync()
        {
            sugestie.Items.Clear(); 

            QuerySnapshot dane = await Google.SugestiaAsync(nowawiadomosc.Text);

            if (dane!=null)
            {
                foreach (DocumentSnapshot documentSnapshot in dane.Documents)
                {
                    Dictionary<string, object> sugestia = documentSnapshot.ToDictionary();

                    sugestie.Items.Add(sugestia["Wyraz"].ToString());
                }
            }
            else
            {
                Console.WriteLine("Nie ma podpowiedzi do tego słowa");
            }

        }

        private void sugestie_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] słowa = nowawiadomosc.Text.Split(' ');
            string nowezdanie="";

            for (int x=0; x<słowa.Length; x++)
            {
                if (x==słowa.Length-1)
                {
                    nowezdanie += sugestie.SelectedItem.ToString()+" ";
                }
                else
                {
                    nowezdanie+=słowa[x].ToString()+" ";    
                }
            }
            nowawiadomosc.Text = nowezdanie;
        }
    }
}
