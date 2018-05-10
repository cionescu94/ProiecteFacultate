using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace ProiectPaw
{
    public partial class Form1 : Form
    {
        public List<imobile> case1;
        int[] valori;
        
        public Form1()
        {
            InitializeComponent();
            case1= new List<imobile>();          
            String line;
         
            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
            _filePath += @"\imobile.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(_filePath);
            int j;
            j=0;
            
            while ((line = file.ReadLine()) != null)
            {
                case1.Add(new imobile("", 0, 0, 0, 0, ""));            
                case1[j].Nume=file.ReadLine();
                case1[j].Pret = int.Parse(file.ReadLine());
                case1[j].Camere = int.Parse(file.ReadLine());
                case1[j].Suprafata = int.Parse(file.ReadLine());
                case1[j].Etaj = int.Parse(file.ReadLine());
                case1[j].Adresa=file.ReadLine();
                j++; 
            }
            file.Close();
      
            Afisare();
            int a=case1.Count;
            valori= new int[a];
            int i=0;
            foreach (imobile im in case1) { valori[i++] = im.Pret; }
            int aux = -1;
            for (int z = 0; z < a - 1; z++) for (int t = z + 1; t < a; t++) if (valori[z] > valori[t]) {
                aux = valori[z];
                valori[z] = valori[t];
                valori[t] = aux;
            }
            
        }

        void Afisare() {
            listView1.Items.Clear();
            foreach (imobile cas in case1) { 
            ListViewItem item=new ListViewItem(new string[]{cas.Nume, cas.Pret.ToString(), cas.Camere.ToString(),cas.Suprafata.ToString(),cas.Etaj.ToString(),cas.Adresa});
                item.Tag=cas;
                listView1.Items.Add(item);
            }
        }

        

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            using (Form2 f = new Form2())
            {
                if (f.ShowDialog(this) == DialogResult.OK) { case1.Add(f.Rezultat); Afisare(); }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                imobile casa = (imobile)listView1.SelectedItems[0].Tag;
                using (Form2 f = new Form2(casa))
                {
                    if (f.ShowDialog(this) == DialogResult.OK) { Afisare(); }

                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                imobile casa = (imobile)listView1.SelectedItems[0].Tag;
                if (MessageBox.Show("Siguri doriti sa stergeti?", "Confirmare", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    case1.Remove(casa);
                    Afisare();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
           // string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
           // _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
          //  _filePath += @"\imobile.txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Users\\Cristi\\Documents\\Visual Studio 2013\\Projects\\ProiectPaw\\ProiectPaw\\imobile.txt");
            //file.WriteLine(" ");
            foreach (imobile cas in case1) {
                file.WriteLine(" ");
                file.WriteLine(cas.Nume);
                file.WriteLine(cas.Pret.ToString());
                file.WriteLine(cas.Camere.ToString());
                file.WriteLine(cas.Suprafata.ToString());
                file.WriteLine(cas.Etaj.ToString());
                file.WriteLine(cas.Adresa);
            }
            file.Close(); 
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (Form4 f = new Form4(valori))
            {
                if (f.ShowDialog(this) == DialogResult.OK) {  }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            using (Form5 f = new Form5())
            {
                if (f.ShowDialog(this) == DialogResult.OK) { }
            }
        }

        
       
        
    }
}
