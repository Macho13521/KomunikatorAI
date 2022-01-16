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
    public partial class Rejestracja : Form
    {
        public Rejestracja()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Logowanie.zamykanie);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Logowanie().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UtwórzKontoAsync();
        }

        private async Task UtwórzKontoAsync()
        {
            bool stan = await Google.NoweKontoAsync(nowylogin.Text, nowehaslo.Text);
            if (stan)
            {
                MessageBox.Show("Zarejestrowano");
            }
            else
            {
                MessageBox.Show("Takie konto już istnieje");
            }
        }
    }
}
