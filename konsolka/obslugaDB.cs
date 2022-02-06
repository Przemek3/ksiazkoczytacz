using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konsolka
{
    class obslugaDB
    {
        naukaSlowekEntities context = new naukaSlowekEntities();
        public doNauczenia pytajace = new doNauczenia();
        private string sciezkaPlikuZNumerkamiDoNauki = @"C:\Users\pkolo\Documents\sciezkaZNumerkami.txt";
        public bool czyWybrane = false;


        public void pobierzLosowyRekord()
        {
            pytajace = context.doNauczenia.OrderBy(r => Guid.NewGuid()).Skip(3).Take(1).First();
        }
        public bool sprawdzPoprawnosc(string odp)
        {
            doNauczenia don = context.doNauczenia.FirstOrDefault(x => x.Id == pytajace.Id);
            if (odp == pytajace.polski)
            {
                don.liczbaDobrych += 1;
                context.SaveChanges();
                return true;
            }
            don.liczbaDobrych = 0;
            context.SaveChanges();
            return false;
        }

        public dynamic pobierzNieznane()
        {
            string numeryString, jedenNr="";
            List<int> numerySlowek = new List<int>();
            using (StreamReader inputFile = new StreamReader(sciezkaPlikuZNumerkamiDoNauki))
            {
                numeryString = inputFile.ReadLine();
            }
            foreach(Char c in numeryString)
            {
                if(Char.IsDigit(c))
                {
                    jedenNr += c;
                }
                else
                {
                    numerySlowek.Add(int.Parse(jedenNr));
                    jedenNr = "";
                }
            }
            if(numerySlowek.Count>0)
            {
                var model = context.doNauczenia.Where(m => numerySlowek.Contains(m.Id)).Select(m => new { m.Id, m.angielski, m.polski, m.liczbaDobrych }).ToList();
                czyWybrane = true;
                return model;
            }
            else
            {
                var model = context.doNauczenia.Where(m => m.czyNauczone == false).Select(m => new { m.Id, m.angielski, m.polski, m.liczbaDobrych }).ToList();
                return model;
            }
             
        }

        public void wrzucDoZnanych(int Id)
        {
            doNauczenia slowko = context.doNauczenia.FirstOrDefault(x => x.Id == Id);
            slowko.czyNauczone = true;
            context.SaveChanges();
        }
        public async void dodajPunkt(int Id)
        {
            doNauczenia slowko = context.doNauczenia.FirstOrDefault(x => x.Id == Id);
            if(slowko.liczbaDobrych++>3)
            {
                slowko.czyNauczone = true;
            }
            context.SaveChanges();
        }
        public async void zeruj(int Id)
        {
            doNauczenia slowko = context.doNauczenia.FirstOrDefault(x => x.Id == Id);
            slowko.liczbaDobrych = 0;
            slowko.czyNauczone = false;
            context.SaveChanges();
        }
    }
}
