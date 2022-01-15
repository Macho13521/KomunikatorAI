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

namespace KomunikatorAI
{
    public partial class Menu : Form
    {
        public string IDKonta;

        User użytkownik;

        List<string> IDZaproszeń = new List<string>();

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
                WstępneZaproszeniaAsync();
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

            QuerySnapshot dane = await Google.OczekująceZaproszenia(użytkownik.Login);

            foreach (DocumentSnapshot documentSnapshot in dane.Documents)
            {
                Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
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
            WstępneZaproszeniaAsync();
        }
    }
}
