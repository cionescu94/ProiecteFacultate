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
    public partial class Profesor : Form
    {
        Nota ab;
        public Profesor()
        {
            InitializeComponent();
        }

        public Nota Rezultat { get { return ab; } }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ab == null)
            {
                ab = new Nota(textBox1.Text, textBox2.Text, textBox3.Text, int.Parse(textBox4.Text), int.Parse(textBox5.Text), int.Parse(textBox6.Text));
            }
            else
            {
                ab.Clasa = textBox1.Text;
                ab.Nume = textBox2.Text;
                ab.Disciplina = textBox3.Text;
                ab.Not = int.Parse(textBox4.Text);
                ab.Zi = int.Parse(textBox5.Text);
                ab.Luna = int.Parse(textBox6.Text);

            }
            this.DialogResult = DialogResult.OK;
            Close();  
        }

        
    }
}
