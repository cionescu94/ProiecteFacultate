using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicentaPedagogie
{
    public class Absenta
    {
        string clasa;
        string nume;
        string disciplina;
        int zi;
        int luna;

        public string Disciplina
        {
            get { return disciplina; }
            set { disciplina = value; }
        }

        public string Clasa
        {
            get { return clasa; }
            set { clasa = value; }
        }

        public string Nume
        {
            get { return nume; }
            set { nume = value; }
        }

        public int Zi
        {
            get { return zi; }
            set { zi = value; }
        }

        public int Luna
        {
            get { return luna; }
            set { luna = value; }
        }
       
   
        public Absenta(string clasa,string nume,string disciplina, int zi, int luna) {
            this.Clasa = clasa;
            this.Nume = nume;
            this.Disciplina = disciplina;
            this.Zi = zi;
            this.Luna = luna;
        }
    }
}
