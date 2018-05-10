using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicentaPedagogie
{
    public partial class LogInProf : Form
    {
        List<Utilizatori> utilizatori;
        public LogInProf()
        {
            InitializeComponent();
            utilizatori = new List<Utilizatori>();
            utilizatori.Add(new Utilizatori("Ionescu Cristian", "parola"));
            utilizatori.Add(new Utilizatori("Gabriela Constantin", "parola"));
            utilizatori.Add(new Utilizatori("Vasile", "parola"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Utilizatori uti in utilizatori)
            {
                if ((textBox1.Text == uti.Nume) && (textBox2.Text == uti.Parola))
                {
                    using (Catalog f = new Catalog())
                    {
                        if (f.ShowDialog(this) == DialogResult.OK) { }

                    }
                }

            };
        }
    }
}
