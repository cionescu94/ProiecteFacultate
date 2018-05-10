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

namespace LicentaPedagogie
{
    public partial class Catalog : Form
    {
        string Utilizator;
        List<Nota> note;
        public Catalog(string Utilizator=null)
        {
            InitializeComponent();
            this.Utilizator = Utilizator;
            note=new List<Nota>();
            String line;

            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
            _filePath += @"\note.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(_filePath);
            int j;
            j = 0;

            while ((line = file.ReadLine()) != null)
            {
                note.Add(new Nota("","", "", 0, 0, 0));
                note[j].Clasa = file.ReadLine();
                note[j].Nume = file.ReadLine();
                note[j].Disciplina =file.ReadLine();
                note[j].Not = int.Parse(file.ReadLine());
                note[j].Zi = int.Parse(file.ReadLine());
                note[j].Luna =int.Parse(file.ReadLine());
                j++;
            }
            file.Close();
            Console.WriteLine(this.Utilizator);

            Afisare();

        }

        void Afisare()
        {
            listView1.Items.Clear();
            foreach (Nota cas in note)
            {
                if (cas.Nume ==this.Utilizator)
                {
                    ListViewItem item = new ListViewItem(new string[] { cas.Clasa, cas.Nume, cas.Disciplina, cas.Not.ToString(), cas.Zi.ToString(), cas.Luna.ToString() });
                    item.Tag = cas;
                    listView1.Items.Add(item);
                }
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Utilizator != null)
                using (CatalogAbsente f = new CatalogAbsente(Utilizator))
                {
                    if (f.ShowDialog(this) == DialogResult.OK) { }

                }

            else {
                using (CatalogAbsente f = new CatalogAbsente())
                {
                    if (f.ShowDialog(this) == DialogResult.OK) { }

                }
            }
        }


        public Catalog() {

            InitializeComponent();
            note = new List<Nota>();
            String line;

            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
            _filePath += @"\note.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(_filePath);
            int j;
            j = 0;

            while ((line = file.ReadLine()) != null)
            {
                note.Add(new Nota("", "", "", 0, 0, 0));
                note[j].Clasa = file.ReadLine();
                note[j].Nume = file.ReadLine();
                note[j].Disciplina = file.ReadLine();
                note[j].Not = int.Parse(file.ReadLine());
                note[j].Zi = int.Parse(file.ReadLine());
                note[j].Luna = int.Parse(file.ReadLine());
                j++;
            }
            file.Close();

            Afisare2();
        }
        void Afisare2()
        {
            listView1.Items.Clear();
            foreach (Nota cas in note)
            {
                
                    ListViewItem item = new ListViewItem(new string[] { cas.Clasa, cas.Nume, cas.Disciplina, cas.Not.ToString(), cas.Zi.ToString(), cas.Luna.ToString() });
                    item.Tag = cas;
                    listView1.Items.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Utilizator==null)
            using (Profesor f = new Profesor())
            {
                if (f.ShowDialog(this) == DialogResult.OK) { note.Add(f.Rezultat); Afisare2(); }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Utilizator == null)
            {
                string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
                _filePath += @"\note.txt";
                System.IO.StreamWriter file = new System.IO.StreamWriter(_filePath);
                //file.WriteLine(" ");
                foreach (Nota cas in note)
                {
                    file.WriteLine(" ");
                    file.WriteLine(cas.Clasa);
                    file.WriteLine(cas.Nume);
                    file.WriteLine(cas.Disciplina);
                    file.WriteLine(cas.Not.ToString());
                    file.WriteLine(cas.Zi.ToString());
                    file.WriteLine(cas.Luna.ToString());

                }
                file.Close();
            }
        }
    }
}
