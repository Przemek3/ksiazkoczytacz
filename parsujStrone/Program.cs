using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;

namespace parsujStrone
{
    class Program
    {
        static private string sciezkaPlikuZ500Naj = @"C:\Users\pkolo\Documents\sciezkaZ500Najpopularniejszych.txt";
        static private string adres500 = "https://www.smart-words.org/500-most-commonly-used-english-words.html";
        static private string sciezkaPlikuA1 = @"C:\Users\pkolo\Documents\sciezkaB2.txt";
        static private string adresA1 = "https://www.ang.pl/slownictwo/slownictwo-angielskie-poziom-b2/page/";
        static private string sciezkaPlikuNieregularnych = @"C:\Users\pkolo\Documents\sciezkaC2.txt";//nieregularne.txt";
        static private string adresNieregularnych = "https://www.ang.pl/gramatyka/czasowniki-verbs/czasowniki-nieregularne";

        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlNode[] nodes, pl;
            HtmlDocument document;
            HtmlNode[] ekcept;
            string wyjscie="h";
            using (StreamWriter outputFile = new StreamWriter(sciezkaPlikuNieregularnych, append: false))
            {
                document = web.Load(adresNieregularnych);
                //nodes = ;
                ekcept = document.DocumentNode.SelectNodes("//tr//td").Where(x => x.Attributes[0].Value== "ang c2 nowrap").ToArray();// document.DocumentNode.SelectNodes("//tr//td").Where(x => x.InnerText.Contains("<")).ToArray();

                //nodes = nodes.ToArray();
                foreach (HtmlNode item in ekcept )
                {
                    foreach(HtmlNode i2 in item.ParentNode.SelectNodes(".//td[@class='ang']").ToArray())
                    {
                        if (wyjscie != i2.InnerHtml)
                        {
                            if (i2.InnerHtml.Contains(","))
                            {
                                Console.WriteLine(i2.InnerHtml.Split(',')[0]);
                                Console.WriteLine(i2.InnerHtml.Split(',')[1].Trim());
                                outputFile.WriteLine(i2.InnerHtml.Split(',')[0]);
                                outputFile.WriteLine(i2.InnerHtml.Split(',')[1].Trim());
                            }
                            else
                            {
                                Console.WriteLine(i2.InnerHtml);
                                outputFile.WriteLine(i2.InnerHtml);
                            }
                            wyjscie = i2.InnerHtml;
                        }
                    }


                }
                    /*for (int i = 1; i < 53; i++)
                    {
                        document = web.Load(adresA1 + i.ToString());
                        nodes = document.DocumentNode.SelectNodes("//div[@class='ditem']").Where(x => x.InnerHtml.Contains("p class")
                        && x.InnerHtml.Contains("<a")).ToArray();//p[@class='word']").Where(x => x.InnerHtml.Contains("<a")).First();//.ToArray();//"//p//a" || 
                                                                 //nodes = cos.SelectNodes("//a").ToArray();
                                                                 //pl = document.DocumentNode.SelectNodes("//p[@class='tr']").ToArray();
                                                                 //ekcept = //nodes.Where(x => x.InnerHtml.Contains("img")).ToArray();
                                                                 //nodes = nodes.Except(ekcept).ToArray();
                        for (int j = 0; j < nodes.Length; j++)
                        {
                            wyjscie = nodes[j].SelectNodes(".//p//a").FirstOrDefault().InnerHtml;// + " - " + nodes[j].SelectNodes(".//p[@class='tr']").First().InnerHtml;
                            Console.WriteLine(wyjscie);
                            outputFile.WriteLine(wyjscie);
                        }

                    }*/


            }
            Console.ReadLine();
        }

        static void pobierz500()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlNode[] nodes;
            HtmlDocument document = web.Load(adres500);

            nodes = document.DocumentNode.SelectNodes("//ol//li").ToArray();

            //HtmlNode[] ekcept = nodes.Where(x => x.InnerHtml.Contains("<li>")).ToArray();
            //nodes = nodes.Except(ekcept).ToArray();
            using (StreamWriter outputFile = new StreamWriter(sciezkaPlikuZ500Naj))
            {
                foreach (HtmlNode item in nodes)
                {
                    Console.WriteLine(item.InnerHtml);

                    //outputFile.WriteLine(item.InnerHtml);

                }
            }
        }
    }
}
