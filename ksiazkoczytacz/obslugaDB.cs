using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ksiazkoczytacz
{
    class obslugaDB
    {
        naukaSlowekEntities context = new naukaSlowekEntities();
        public doNauczenia pytajace = new doNauczenia();
        
      
        public void pobierzLosowyRekord()
        {
             pytajace = context.doNauczenia.OrderBy(r => Guid.NewGuid()).Skip(3).Take(1).First();
        }
        public bool sprawdzPoprawnosc(string odp)
        {
            doNauczenia don = context.doNauczenia.FirstOrDefault(x => x.Id == pytajace.Id);
            if(odp==pytajace.polski)
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
            var model = context.doNauczenia.Where(m => m.czyNauczone == false).Select(m => new { m.Id, m.angielski, m.polski }).ToList();
            return model;
        }

        public void wrzucDoZnanych(int Id)
        {
            doNauczenia slowko = context.doNauczenia.FirstOrDefault(x => x.Id == Id);
            slowko.czyNauczone = true;
            context.SaveChanges();
        }
    }
}
