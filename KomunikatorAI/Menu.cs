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
    public partial class Menu : Form
    {
        public string IDKonta;
        public Menu(string identyfikator)
        {
            InitializeComponent();
            IDKonta = identyfikator;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            przywitanie.Text = IDKonta;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
