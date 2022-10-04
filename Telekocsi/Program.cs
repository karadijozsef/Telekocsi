using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Telekocsi
{
    class Program
    {
        static List<Jarat> autok = new List<Jarat>();
        static List<Jarat> igenyek = new List<Jarat>();

        static void Main(string[] args)
        {
            //1.feladat
            StreamReader Olvas = new StreamReader("autok.csv", Encoding.Default);
            string Fejlec = Olvas.ReadLine();
            while (!Olvas.EndOfStream)
            {
                autok.Add(new Jarat(Olvas.ReadLine()));
            }
            Olvas.Close();
            StreamReader Olvasas = new StreamReader("igenyek.csv", Encoding.Default);
            Fejlec = Olvasas.ReadLine();
            while (!Olvasas.EndOfStream)
            {
                igenyek.Add(new Jarat(Olvasas.ReadLine()));
            }
            Olvasas.Close();

            //2.feladat
            Console.WriteLine("2.feladat");
            Console.WriteLine($"\t{autok.Count} autós hirdet fuvart.");

            //3.feladat
            int ferohely = igenyek.Where(x => x.indulas == "Budapest" && x.cel == "Miskolc").Select(x => x.ferohely).Sum();
            Console.WriteLine($"3. feladat:\n\tÖsszesen {ferohely} férőhelyet hirdettek az autósok Budapestről Miskolcra");

            //4.feladat
            var max = autok.OrderByDescending(a => a.ferohely).First();
            Console.WriteLine("4. feladat: \n \t A legtöbb férőhelyet " + max.indulas + " - " + max.cel + " útvonalon kínálták " + max.ferohely + " hellyel");

            Dictionary<Jarat,Jarat> matches = new Dictionary<Jarat, Jarat>();
            foreach (var igeny in igenyek)
            {
                foreach (var jarat in autok)
                {
                    if (!(matches.ContainsKey(igeny)) &&
                        (igeny.cel == jarat.cel && igeny.indulas == jarat.indulas && igeny.ferohely <= jarat.ferohely))
                    {
                        matches.Add(igeny, jarat);
                    }
                }
            }

            //5.feladat
            Console.WriteLine("5. feladat: \n \t");
            foreach (var item in matches)
            {
                Console.WriteLine("\t " + item.Key.rendszam+ " ---> " + item.Value.rendszam);
            }

            //6.feladat
            Console.WriteLine("6. feladat: utasuzenetek.txt");
            using (StreamWriter sw = new StreamWriter("utasuzenetek.txt"))
            {
                foreach (var item in igenyek)
                {
                    if (matches.ContainsKey(item))
                    {
                        sw.WriteLine(item.rendszam + ": Rendszám: " + matches[item].rendszam + ", Telefonszám: " + matches[item].telefonszam);
                    }
                    else
                    {
                        sw.WriteLine(item.rendszam + ": Sajnos nem sikerült autót találni");
                    }
                }
            }

            Console.WriteLine("\nProgram vége");
            Console.ReadKey();
        }
    }
}
