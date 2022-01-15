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
using static KomunikatorAI.szablony;
using System.Threading;

namespace KomunikatorAI
{
    public partial class Menu : Form
    {
        public string IDKonta;

        User użytkownik;

        List<string> IDZaproszeń = new List<string>();
        List<string> IDZnajomych = new List<string>();

        public Menu(string identyfikator)
        {
            InitializeComponent();
            IDKonta = identyfikator;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            PobieranieUżytkownikaAsync();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            WyślijZaproszenieAsync();
        }

        private async Task PobieranieUżytkownikaAsync()
        {
            DocumentSnapshot dane = await Google.PobierzRekord("Konta", IDKonta);
            if (dane.Exists)
            {
                użytkownik = dane.ConvertTo<User>();
                przywitanie.Text = "Witaj "+użytkownik.Login;
                Odświeżenie();
            }
            else
            {
                MessageBox.Show("Nie ma użytkownika o takim ID "+IDKonta);
            }
        }


        private async Task WyślijZaproszenieAsync()
        {
            string komunikat = await Google.WyślijZaproszenieAsync(użytkownik.Login, nowyznajomy.Text);
            MessageBox.Show(komunikat);
        }

        private async Task WstępneZaproszeniaAsync()
        {
            zaproszeniaznajomych.Items.Clear();
            IDZaproszeń.Clear();

            QuerySnapshot dane = await Google.Znajomi(użytkownik.Login, false);

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
            Dictionary<string, object> zaproszenie = new Dictionary<string, object>() {
                {"Zaakceptowane", true}
            };

            await Google.AktualizacjaRekordu("Relacje", IDZaproszeń[zaproszeniaznajomych.SelectedIndex], zaproszenie);
            Odświeżenie();
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

                Thread.Sleep(1000);

                Odświeżenie();
            }
            else
            {
                MessageBox.Show("Nie wybrałeś żadnego zaproszenia");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Odświeżenie();
        }

        private void Odświeżenie()
        {
            WstępneZaproszeniaAsync();
            ListaZnajomychAsync();
        }

        private async Task ListaZnajomychAsync()
        {
            listaznajomych.Items.Clear();
            IDZnajomych.Clear();

            QuerySnapshot dane = await Google.Znajomi(użytkownik.Login, true);

            foreach (DocumentSnapshot documentSnapshot in dane.Documents)
            {
                Dictionary<string, object> zaproszenia = documentSnapshot.ToDictionary();

                IDZnajomych.Add(documentSnapshot.Id);
                listaznajomych.Items.Add(zaproszenia["Nadawca"]);
            }

            dane = await Google.Znajomi(użytkownik.Login, true, false);

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

                Thread.Sleep(1000);

                Odświeżenie();
            }
            else
            {
                MessageBox.Show("Nie wybrałeś żadnego znajomego");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Rozmowa(IDZnajomych[listaznajomych.SelectedIndex]).Show();
        }
    }
}
