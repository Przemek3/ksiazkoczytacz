using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace konsolka
{
    class Program
    {
        //int odTejLiczbyUznajemyZaNauczone=3;
        static obslugaDB pracaZBaza = new obslugaDB();
        static List<doNauczenia> doNau = new List<doNauczenia>();
        static void Main(string[] args)
        {
            pobierzRekordy();
            czyDobre();
            //wypiszRekordy();

        }

        static void pobierzRekordy()
        {
            var rekordyDB = pracaZBaza.pobierzNieznane();
            foreach(var r in rekordyDB)
            {
                doNau.Add(new doNauczenia(r.Id, r.angielski, r.polski, r.liczbaDobrych));
            }
        }

        static void czyDobre()
        {
            
            int index;
            string czytana;
            

            do
            {
                index = losujNumer();
                if (index == -1)
                {
                    return;
                }
                


                Console.WriteLine(doNau[index].polski);
                czytana = Console.ReadLine();
                if (czytana == doNau[index].angielski)
                {
                    if (doNau[index].liczbaDobrych++ > 3)// odTejLiczbyUznajemyZaNauczone)
                    {
                        doNau[index].czyNauczone = true;
                        Console.WriteLine("Dodane do nauczonych");
                    }
                    Console.WriteLine("Dobrze!");
                    pracaZBaza.dodajPunkt(doNau[index].Id);
                }
                else
                {
                    Console.WriteLine("zle " + doNau[index].angielski);
                    pracaZBaza.zeruj(doNau[index].Id);
                    doNau[index].liczbaDobrych = 0;
                }
                Thread.Sleep(2000);
                
                Console.Clear();
            } while (czytana != "zapisz");

        }

        static int losujNumer()
        {
            Random random = new Random();
            int index, oczekiwanie, liczbaPytanychSlowek;
            oczekiwanie = 0;
            liczbaPytanychSlowek = pracaZBaza.czyWybrane || doNau.Count < 10 ? doNau.Count : 10;
           
            do
            {
                index = random.Next(liczbaPytanychSlowek);
                oczekiwanie += 1;
            } while (doNau[index].czyNauczone == true && oczekiwanie < 10);

            if (oczekiwanie == 10)
            {
                List<int> l= czySa();
                oczekiwanie = random.Next(l.Count);
                index = l.Count>0 ? l[oczekiwanie] : -1;
            }
            return index;
        }
        
        static List<int> czySa()
        {
            List<int> liczby = new List<int>();
            for(int i=0; i<doNau.Count; i++)
            {
                if(doNau[i].czyNauczone==false)
                {
                    liczby.Add(i);
                }
            }
            return liczby;
        }

        static void wypiszRekordy()
        {
            foreach(doNauczenia slowa in doNau)
            {
                Console.WriteLine(slowa.angielski);
            }
        }
    }
}
