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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Logowanie().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Google.NoweKontoAsync("Konta", nowylogin.Text, nowehaslo.Text);
        }
    }
}
