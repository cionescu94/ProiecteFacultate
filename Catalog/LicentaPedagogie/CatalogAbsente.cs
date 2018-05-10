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
    public partial class CatalogAbsente : Form
    {
        string Utilizator;
        List<Absenta> abse;
        public CatalogAbsente(string Utilizator = null)
        {
            InitializeComponent();
            abse=new List<Absenta>();
            this.Utilizator = Utilizator;
            String line;

            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
            _filePath += @"\absente.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(_filePath);
            int j;
            j = 0;

            while ((line = file.ReadLine()) != null)
            {
                abse.Add(new Absenta("", "", "",0, 0));
                abse[j].Clasa = file.ReadLine();
                abse[j].Nume = file.ReadLine();
                abse[j].Disciplina = file.ReadLine();
                abse[j].Zi = int.Parse(file.ReadLine());
                abse[j].Luna = int.Parse(file.ReadLine());
                j++;
            }
            file.Close();
            Console.WriteLine(this.Utilizator);


            Afisare();

        }


        void Afisare()
        {
            listView1.Items.Clear();
            foreach (Absenta cas in abse)
            {
                if (cas.Nume == this.Utilizator)
                {
                    ListViewItem item = new ListViewItem(new string[] { cas.Clasa, cas.Nume, cas.Disciplina, cas.Zi.ToString(), cas.Luna.ToString() });
                    item.Tag = cas;
                    listView1.Items.Add(item);
                }

            }
        }


        public CatalogAbsente() {
            InitializeComponent();
            abse = new List<Absenta>();
            String line;

            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
            _filePath += @"\absente.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(_filePath);
            int j;
            j = 0;

            while ((line = file.ReadLine()) != null)
            {
                abse.Add(new Absenta("", "", "", 0, 0));
                abse[j].Clasa = file.ReadLine();
                abse[j].Nume = file.ReadLine();
                abse[j].Disciplina = file.ReadLine();
                abse[j].Zi = int.Parse(file.ReadLine());
                abse[j].Luna = int.Parse(file.ReadLine());
                j++;
            }
            file.Close();
            Afisare2();
        }


        void Afisare2()
        {
            listView1.Items.Clear();
            foreach (Absenta cas in abse)
            {
                
                    ListViewItem item = new ListViewItem(new string[] { cas.Clasa, cas.Nume, cas.Disciplina, cas.Zi.ToString(), cas.Luna.ToString() });
                    item.Tag = cas;
                    listView1.Items.Add(item);
                

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Utilizator == null)
            using (Absente f = new Absente())
            {
                if (f.ShowDialog(this) == DialogResult.OK) { abse.Add(f.Rezultat); Afisare2();}

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Utilizator == null)
            {
                string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                _filePath = Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName;
                _filePath += @"\absente.txt";
                System.IO.StreamWriter file = new System.IO.StreamWriter(_filePath);
                //file.WriteLine(" ");
                foreach (Absenta cas in abse)
                {
                    file.WriteLine(" ");
                    file.WriteLine(cas.Clasa);
                    file.WriteLine(cas.Nume);
                    file.WriteLine(cas.Disciplina);
                    file.WriteLine(cas.Zi.ToString());
                    file.WriteLine(cas.Luna.ToString());

                }
                file.Close();
            }
        }

    }
}
