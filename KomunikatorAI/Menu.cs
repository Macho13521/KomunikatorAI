using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static KomunikatorAI.Logowanie;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Linq;

namespace KomunikatorAI
{
    public partial class Menu : Form
    {
        List<string> IDZaproszeń = new List<string>();
        List<string> IDZnajomych = new List<string>();

        public FirestoreChangeListener nasłuchiwacz;

        private bool OknoAktywne=true;

        private bool OczekująceOdświeżenie = false;

        public Menu()
        {
            InitializeComponent();

            this.Activated += ZyskanieUwagi;
            this.Deactivate += StracenieUwagi;
            this.FormClosed += new FormClosedEventHandler(Logowanie.zamykanie);
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            PobieranieUżytkownikaAsync();
        }

        private void StracenieUwagi(object sender, EventArgs e)
        {
            OknoAktywne = false;

        }

        private void ZyskanieUwagi(object sender, EventArgs e)
        {
            OknoAktywne = true;
            if (OczekująceOdświeżenie)
            {
                WstępneZaproszeniaAsync();
                ListaZnajomychAsync();
                OczekująceOdświeżenie = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WyślijZaproszenieAsync();
        }

        private async Task PobieranieUżytkownikaAsync()
        {
            przywitanie.Text = "Witaj "+użytkownik.Login;
            OdświeżanieZaproszeń();
            OdświeżanieZnajomych();

            WstępneZaproszeniaAsync();
            ListaZnajomychAsync();
        }


        private async Task WyślijZaproszenieAsync()
        {
            if (użytkownik.Login!= nowyznajomy.Text)
            {
                string komunikat = await Google.WyślijZaproszenieAsync(nowyznajomy.Text);
                MessageBox.Show(komunikat);
            }
            else
            {
                MessageBox.Show("Nie możesz dodać sam siebie");
            }
            
        }

        private void OdświeżanieZaproszeń()
        {
            Query warunki = Google.db.Collection("Relacje")
                    .WhereEqualTo("Odbiorca", użytkownik.Login)
                    .WhereEqualTo("Zaakceptowane", false);
            nasłuchiwacz = warunki.Listen(snapshot =>
            {
                QuerySnapshot otrzymanezaproszenie = snapshot.Query.Limit(1).GetSnapshotAsync().Result;
                if (!OknoAktywne)
                {
                    new ToastContentBuilder().AddText("Otrzymane zaproszenie do znajomych od: " + otrzymanezaproszenie.First().GetValue<string>("Nadawca").ToString()).Show();
                    OczekująceOdświeżenie = true;
                }
                else
                {
                    WstępneZaproszeniaAsync();
                }
            });
        }

        private void OdświeżanieZnajomych()
        {
            Query warunki = Google.db.Collection("Relacje")
                    .WhereEqualTo("Nadawca", użytkownik.Login)
                    .WhereEqualTo("Zaakceptowane", true);
            nasłuchiwacz = warunki.Listen(snapshot =>
            {
                QuerySnapshot nowyznajomy = snapshot.Query.Limit(1).GetSnapshotAsync().Result;
                if (!OknoAktywne)
                {
                    new ToastContentBuilder().AddText(nowyznajomy.First().GetValue<string>("Nadawca").ToString()+" jest twoim znajomym").Show();
                    OczekująceOdświeżenie = true;
                }
                else
                {
                    ListaZnajomychAsync();
                }
            });
        }

        private async Task WstępneZaproszeniaAsync()
        {
            zaproszeniaznajomych.Items.Clear();
            IDZaproszeń.Clear();

            QuerySnapshot dane = await Google.Znajomi(false);

            foreach (DocumentSnapshot documentSnapshot in dane.Documents)
            {
                Dictionary<string, object> zaproszenia = documentSnapshot.ToDictionary();

                IDZaproszeń.Add(documentSnapshot.Id);
                zaproszeniaznajomych.Items.Add(zaproszenia["Nadawca"]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AkceptowanieZaproszeniaAsync();
        }

        private async Task AkceptowanieZaproszeniaAsync()
        {
            if (zaproszeniaznajomych.SelectedIndex >= 0)
            {
                Dictionary<string, object> zaproszenie = new Dictionary<string, object>() {
                    {"Zaakceptowane", true}
                };

                await Google.AktualizacjaRekordu("Relacje", IDZaproszeń[zaproszeniaznajomych.SelectedIndex], zaproszenie);
                WstępneZaproszeniaAsync();
                ListaZnajomychAsync();
            }
            else
            {
                MessageBox.Show("Nie wybrałeś żadnego zaproszenia");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OdrzucanieZaproszeniaAsync();
        }

        private void OdrzucanieZaproszeniaAsync()
        {
            if (zaproszeniaznajomych.SelectedIndex >= 0)
            {
                Google.UsuwanieRekordu("Relacje", IDZaproszeń[zaproszeniaznajomych.SelectedIndex]);
                Thread.Sleep(500);
                WstępneZaproszeniaAsync();
            }
            else
            {
                MessageBox.Show("Nie wybrałeś żadnego zaproszenia");
            }
        }

        private async Task ListaZnajomychAsync()
        {
            listaznajomych.Items.Clear();
            IDZnajomych.Clear();

            QuerySnapshot dane = await Google.Znajomi(true);

            foreach (DocumentSnapshot documentSnapshot in dane.Documents)
            {
                Dictionary<string, object> zaproszenia = documentSnapshot.ToDictionary();

                IDZnajomych.Add(documentSnapshot.Id);
                listaznajomych.Items.Add(zaproszenia["Nadawca"]);
            }

            dane = await Google.Znajomi(true, false);

            foreach (DocumentSnapshot documentSnapshot in dane.Documents)
            {
                Dictionary<string, object> zaproszenia = documentSnapshot.ToDictionary();

                IDZnajomych.Add(documentSnapshot.Id);
                listaznajomych.Items.Add(zaproszenia["Odbiorca"]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listaznajomych.SelectedIndex >=0)
            {
                Google.UsuwanieRekordu("Relacje", IDZnajomych[listaznajomych.SelectedIndex]);
                Thread.Sleep(500);
                ListaZnajomychAsync();
            }
            else
            {
                MessageBox.Show("Nie wybrałeś żadnego znajomego");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RozmowaAsync();
        }

        private async Task RozmowaAsync()
        {
            if (listaznajomych.SelectedIndex >= 0)
            {
                string IDRelacji = IDZnajomych[listaznajomych.SelectedIndex];

                new Rozmowa(IDRelacji, listaznajomych.SelectedItem.ToString()).Show();
                wyłącz = false;
                nasłuchiwacz.StopAsync();

                this.Close();
            }
            else
            {
                MessageBox.Show("Nie wybrałeś żadnego znajomego");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            użytkownik = null;
            new Logowanie().Show();
            wyłącz = false;
            this.Close();
        }
    }
}
