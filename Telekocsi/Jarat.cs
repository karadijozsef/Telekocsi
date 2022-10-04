using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telekocsi
{
    class Jarat
    {
        public string indulas;
        public string cel;
        public string rendszam;
        public string telefonszam;
        public int ferohely;

        public Jarat (string AdatSor)
        {
            string[] AdatSorElemek = AdatSor.Split(';');
            this.indulas = AdatSorElemek[0];
            this.cel = AdatSorElemek[1];
            this.rendszam = AdatSorElemek[2];
            this.telefonszam = AdatSorElemek[3];
            this.ferohely = Convert.ToInt32(AdatSorElemek[4]);
        }
    }
}
