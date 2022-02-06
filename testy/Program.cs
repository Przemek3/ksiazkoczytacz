using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testy
{
    class Program
    {
        static private string textowy = @"C:\Users\pkolo\Documents\";
        static private string koncowka = "text3.txt";
        static private string[] koncowki = {"sciezkaA1.txt", "sciezkaA2.txt", "sciezkaB1.txt", "sciezkaB2.txt",
            "sciezkaA1zTlumaczeniem.txt", "sciezkaA2zTlumaczeniem.txt", "sciezkab1zTlumaczeniem.txt", "sciezkab2zTlumaczeniem.txt",
        "sciezkaZ500Najpopularniejszych.txt"};

        static void Main(string[] args)
        {
            string line= "", line2 = "chuje";
            List<string> lista = new List<string>();
            Console.WriteLine("Zaczynamy");
            //for (int i = 0; i < koncowki.Length; i++)
            //{
                lista.Clear();
                using (StreamReader inputFile = new StreamReader(textowy + koncowka))
                {

                    while ((line = inputFile.ReadLine()) != null)
                    {
                        lista.Add(line);
                        //Console.WriteLine(line);
                    }

                }

                using (StreamWriter outputFile = new StreamWriter(textowy + "posortowane\\" + koncowka))
                {
                    lista.Sort();
                    foreach (string l in lista)
                    {
                        outputFile.WriteLine(l);
                    }
                }
           // }*/
            Console.WriteLine(string.Compare(line,line2));
            Console.ReadKey();
        }
    }
}
