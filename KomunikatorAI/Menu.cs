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
    }
}
