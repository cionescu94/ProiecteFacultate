using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPaw
{
    public class Cerere
    {
        public int Cod { get; set; }
        public String Nume { get; set; }
        public int Tel { get; set; }

        public Cerere() {
            this.Cod = 0;
            this.Nume = "";
            this.Tel = 1234;
        }


        public Cerere(int cod, String nume, int tel) {
            this.Cod = cod;
            this.Nume = nume;
            this.Tel = tel;
        }

    }
}
