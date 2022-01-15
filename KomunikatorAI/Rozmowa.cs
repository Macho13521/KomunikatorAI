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
    public partial class Rozmowa : Form
    {
        string IDRozmowy;
        public Rozmowa(string ID)
        {
            InitializeComponent();
            IDRozmowy = ID;
        }

        private void Rozmowa_Load(object sender, EventArgs e)
        {
            info_rozmowa.Text = "ID Rozmowy: "+IDRozmowy;
        }
    }
}
