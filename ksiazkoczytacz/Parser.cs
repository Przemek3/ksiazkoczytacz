using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ksiazkoczytacz
{
    
    static class Parser
    {
        //static public String wyraz;
        static public doNauczenia[] tablica;
        
        static public void Parsuj()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlNode[] nodes;
            HtmlDocument document = web.Load("https://www.e-ang.pl/slowka,52,Inzynieria-Typy-produkcji.html");

            nodes = document.DocumentNode.SelectNodes("//table//td").ToArray();
            HtmlNode[] ekcept = nodes.Where(x => x.InnerHtml.Contains("<b>")).ToArray();
            nodes = nodes.Except(ekcept).ToArray();
            int i = 0, j= 0;
            doNauczenia element = new doNauczenia { polski = "", angielski = "", liczbaDobrych = 0 };
            tablica = new doNauczenia[nodes.Length/2];
            foreach(HtmlNode item in nodes)
            {
                if(i==0)
                {
                    element.angielski = item.InnerHtml;
                    i++;
                }
                else
                {
                    element.polski = item.InnerHtml;
                    i = 0;
                    tablica[j++]=new doNauczenia { polski = element.polski, angielski = element.angielski, liczbaDobrych = 0 };
                } 
            }
          
            /*foreach (HtmlNode item in nodes)
            {
                //Console.WriteLine(item.InnerHtml);
                napis += item.InnerHtml;
            }
            wyraz = napis;*/

            //Console.WriteLine(napis);
        }
    }
}
