using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    public class imobile
    {
        string nume;
        int pret;
        int camere;
        int suprafata;
        int etaj;
        string adresa;
        public imobile(string nume, int pret,int camere, int suprafata, int etaj, string adresa) {
            Nume = nume;
            Pret = pret;
            Camere = camere;
            Suprafata = suprafata;
            Etaj = etaj;
            Adresa = adresa;
        }

        public string Nume {
            get { return nume; }
            set { nume = value; }
        }

        public int Pret
        {
            get { return pret; }
            set { pret = value; }
        }

        public int Camere {
            get { return camere; }
            set { camere = value; }
        }
        
        public int Suprafata
        {
            get { return suprafata; }
            set { suprafata = value; }
        }

        public int Etaj
        {
            get { return etaj; }
            set { etaj = value; }
        }
        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }


     
    }
}
