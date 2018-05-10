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
    public partial class Absente : Form
    {
        Absenta ab;
        public Absente(Absente ab=null)
        {
            InitializeComponent();
        }
        public Absenta Rezultat { get { return ab; } }

        private void label1_Click(object sender, EventArgs e) {}

        private void button1_Click(object sender, EventArgs e)
        {
            if (ab == null)
            {
                ab = new Absenta(textBox1.Text,textBox2.Text, textBox3.Text, int.Parse(textBox4.Text), int.Parse(textBox5.Text));
            }
            else
            {
                ab.Clasa = textBox1.Text;
                ab.Nume = textBox2.Text;
                ab.Disciplina = textBox3.Text;
                ab.Zi = int.Parse(textBox4.Text);
                ab.Luna = int.Parse(textBox5.Text);
                
            }
            this.DialogResult = DialogResult.OK;
            Close();  
        } 
    }
}
