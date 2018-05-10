using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPaw
{
    public partial class Form2 : Form
    {
        imobile cas;
        public Form2(imobile cas=null)
        {
            InitializeComponent();
            if (cas != null)
            {
                this.cas = cas;
                textBox1.Text = cas.Nume;
                textBox2.Text = cas.Pret.ToString();
                textBox3.Text = cas.Camere.ToString();
                textBox4.Text = cas.Suprafata.ToString();
                textBox5.Text = cas.Etaj.ToString();
                textBox6.Text = cas.Adresa;

               
            }
        }
        public imobile Rezultat { get { return cas; } }

       

        private void button1_Click(object sender, EventArgs e)
        {
            if (cas == null)
            {
                cas = new imobile(textBox1.Text, int.Parse(textBox2.Text), int.Parse(textBox3.Text), int.Parse(textBox4.Text),int.Parse(textBox5.Text),textBox6.Text);
            }
            else
            {
                cas.Nume = textBox1.Text;
                cas.Pret = int.Parse(textBox2.Text);
                cas.Camere = int.Parse(textBox3.Text);
                cas.Suprafata = int.Parse(textBox4.Text);
                cas.Etaj = int.Parse(textBox5.Text);
                cas.Adresa = textBox6.Text;
            }
            
            this.DialogResult = DialogResult.OK;
            Close();
        }

      
    }
}
