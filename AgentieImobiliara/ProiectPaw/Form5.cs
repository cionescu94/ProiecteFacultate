using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Collections;

namespace ProiectPaw
{
    public  partial class Form5 : Form
    {

         List<Cerere> lista;
        static string Provider = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Cristi\\Documents\\Visual Studio 2013\\Projects\\ProiectPaw\\ProiectPaw\\DatabasePers.accdb";
        // adaug cate un \ in Source
        //Server explorer , Data connections, Add Connection, Access
         private List<Cerere> GetPersoane(String sirCaractere)
        {
            lista = new List<Cerere>();
            OleDbConnection connection = new OleDbConnection(Provider);
            OleDbCommand command = new OleDbCommand();
            try
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT cod, nume, telefon from Persoane WHERE nume LIKE '%" + sirCaractere + "%';";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Cerere p = new Cerere();
                    p.Cod = (int)reader["Cod"];
                    p.Nume = reader["Nume"].ToString();
                    p.Tel=(int)reader["Telefon"];
                    lista.Add(p);
                }
                return lista;
            }
            finally
            {
                connection.Close();
            }

        }




        void Afisare() {
            listView1.Items.Clear();
            foreach (Cerere ang in GetPersoane("a"))
            {
                ListViewItem item = new ListViewItem(new string[] { ang.Cod.ToString(), ang.Nume, ang.Tel.ToString()});
                item.Tag = ang;
                listView1.Items.Add(item);
            }
            }



        public Form5()
        {
            InitializeComponent();

            Afisare();
        }
    }
}
