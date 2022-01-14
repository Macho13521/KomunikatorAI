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
    public partial class Logowanie : Form
    {
        public Logowanie()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoryzacjaAsync();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Rejestracja().Show();
            this.Hide();
        }

        private void Logowanie_Load(object sender, EventArgs e)
        {
            Google.Connect();
        }

        private async Task AutoryzacjaAsync()
        {
            string IDKonta = await Google.Logowanie(wpisanylogin.Text, wpisanehaslo.Text);
            if (IDKonta.Length>10)
            {
                MessageBox.Show("Zalogowano");
                new Menu().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Niepoprawne dane");
            }
        }
    }
}
