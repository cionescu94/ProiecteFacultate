using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicentaPedagogie
{
    class Utilizatori
    {
        string nume;
        string parola;

        public string Nume
        {
            get { return nume; }
            set { nume = value; }
        }
        public string Parola
        {
            get { return parola; }
            set { parola = value; }
        }

        public Utilizatori(string nume, string parola) {
            this.Nume = nume;
            this.Parola = parola;
        }
    }
}
