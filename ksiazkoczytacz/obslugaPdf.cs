using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace ksiazkoczytacz
{
    class obslugaPdf
    {
        //static private string sciezkaPlikuA1 = @"C:\Users\pkolo\Documents\sciezkab1zTlumaczeniem.txt";
        private string sciezkaPlikow = @"C:\Users\pkolo\Documents\posortowane\sciezka";
        private string[] poziomyJezyka = { "A1.txt", "A2.txt", "B1.txt", "B2.txt" };
        private string sciezkaPlikuZ500Naj = @"C:\Users\pkolo\Documents\posortowane\sciezkaZ500Najpopularniejszych.txt";// sciezkaZ500Najpopularniejszych.txt";
        private string name = @"C:\Users\pkolo\Downloads\IT.pdf";
        private string textowy = @"C:\Users\pkolo\Documents\text1.txt";
        private string wszystkieSlowaKtorychNieMa = @"C:\Users\pkolo\Documents\wszystkieSlowaKtorychNieMa.txt";
        private string plikZnanychSlowZKoncowkami = @"C:\Users\pkolo\Documents\textZnaneKoncowki.txt";
        private string plikBezKoncowek = @"C:\Users\pkolo\Documents\textMinusKoncowkowe.txt";


        int jakiPoziom = 2;
        //private string name2 = @"C:\Users\pkolo\Downloads\Frankenstein.pdf";
        public List<string> slowkaEng = new List<string>();

        void chooseName()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            Nullable<bool> result = openFile.ShowDialog();
            if (result == true)
            {
                name = result.ToString();
            }
        }
        public async Task fromPdfToTableAsync(int pierwsza, int ileStron)
        {
            try
            {
                PdfReader pdfReader = new PdfReader(name);
                PdfDocument pdfDocument = new PdfDocument(pdfReader);
                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                string temp;
                string[] stringSeparators = new string[] { ". ", " ", ", " };
                slowkaEng.Clear();
                List<string> listaDoPrzeszukiwan = new List<string>();
                List<string> listaPomocnicza = new List<string>();

                for (int licznikStron = 0; licznikStron < ileStron; licznikStron++)
                {
                    listaPomocnicza.Clear();
                    listaDoPrzeszukiwan.Clear();
                    string[] tab = (string[])PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(pierwsza + licznikStron), strategy).
    Replace("\n", " ").Replace("-", " ").Replace(".", " ").Replace(",", " ").Replace("/", " ").Replace("(", " ").
    Replace(")", " ").Replace("[", " ").Replace("]", " ").Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries).Where(x => x.Length > 1).ToArray();
                    //Array.Sort(tab,0,tab.Length);
                    for (int i = 0; i < tab.Length; i++)
                    {
                        temp = new string(tab[i].Where(c => Char.IsLetter(c)).ToArray());

                        if (!listaDoPrzeszukiwan.Contains(temp) && !slowkaEng.Contains(temp) && !listaDoPrzeszukiwan.Contains(temp.ToLower()) && !slowkaEng.Contains(temp.ToLower()))//!czySiePokrywa(temp.ToLower())
                        {
                            listaDoPrzeszukiwan.Add(temp);
                            //slowkaEng.Add(temp);
                        }
                    }
                    listaDoPrzeszukiwan.Sort();

                    for (int it = 0; it < listaDoPrzeszukiwan.Count; it++)
                    {
                        if (it > 0 && listaDoPrzeszukiwan[it - 1].ToLower() != listaDoPrzeszukiwan[it].ToLower())
                        {
                            listaPomocnicza.Add(listaDoPrzeszukiwan[it]);
                        }

                    }
                    foreach (int nieznane in ktorychNieZnamy(listaPomocnicza))
                    {
                        slowkaEng.Add(listaPomocnicza[nieznane]);

                    }

                }
                using (StreamWriter str = new StreamWriter(wszystkieSlowaKtorychNieMa))
                {
                    foreach (string st in slowkaEng)
                    {
                        str.WriteLine(st);
                    }
                }

                pdfDocument.Close();
                pdfReader.Close();
                MessageBox.Show("Wiadomosc", "Cos poszlo", MessageBoxButton.OK);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cos nie poszlo", MessageBoxButton.OK);
            }
        }

        public void toTextfile()
        {
            slowkaEng.Sort();
            using (StreamWriter outputFile = new StreamWriter(textowy))
            {
                for (int i = 0; i < slowkaEng.Count; i++)
                {
                    outputFile.WriteLine(slowkaEng[i]);
                }
            }
        }

        private List<int> ktorychNieZnamy(List<string> slowa)           //  sprawdza cala strone znakow ktore slowa sa zapisane w plikach
        {
            String line;
            string miejsceBledu = "-";
            List<int> numery = new List<int>();
            List<int> nieznane = new List<int>();
            int iteratorSlow = 0;
            numery.Clear();

            for (int i = -1; i < jakiPoziom; i++)
            {
                iteratorSlow = 0;
                using (StreamReader inputFile = new StreamReader(i != -1 ? sciezkaPlikow + poziomyJezyka[i] : sciezkaPlikuZ500Naj))
                {
                    while ((line = inputFile.ReadLine()) != null)
                    {
                        switch (string.Compare(slowa[iteratorSlow].ToLower(), line, true))
                        {
                            case 1:
                                if (kontrolaKoncowek.inneFormy(slowa[iteratorSlow].ToLower(), line.ToLower()))
                                    numery.Add(iteratorSlow);
                                break;
                            case 0:
                                if (!numery.Contains(iteratorSlow))
                                    numery.Add(iteratorSlow++);
                                break;
                            case -1:
                                if (kontrolaKoncowek.inneFormy(slowa[iteratorSlow].ToLower(), line.ToLower()) && !numery.Contains(iteratorSlow))
                                    numery.Add(iteratorSlow);
                                do
                                {
                                    if (++iteratorSlow >= slowa.Count)
                                    {
                                        break;
                                    }
                                    else if (kontrolaKoncowek.inneFormy(slowa[iteratorSlow].ToLower(), line.ToLower()))
                                        numery.Add(iteratorSlow);

                                } while (string.Compare(slowa[iteratorSlow], line, true) < 0);

                                break;
                        }
                        if (iteratorSlow == slowa.Count)
                            break;
                    }
                }
            }
            for (int iter = 0; iter < slowa.Count; iter++)
            {
                if (!numery.Contains(iter))
                    nieznane.Add(iter);
            }
            return nieznane;
        }

        public void stworzListeKoncowkowych()
        {
            List<string> koncowkoweZTekstu = new List<string>();
            List<string> znaneZkoncowkami = new List<string>();
            string slowkoZBazyPosiadanych;
            foreach (string item in slowkaEng)      //  wyszczegolnienie wszystkich slowek z koncowkami
            {
                if (kontrolaKoncowek.czyMaKoncowke(item))
                    koncowkoweZTekstu.Add(item);
            }
            koncowkoweZTekstu.Sort();

            /*using (StreamWriter outputFile = new StreamWriter(plikSlowekZKoncowkami))   //  zapis do pliku
            {
                for (int i = 0; i < slowkaEng.Count; i++)
                {
                    outputFile.WriteLine(koncowkoweZTekstu[i]);
                }
            }*/

            using (StreamWriter outputFile = new StreamWriter(plikZnanychSlowZKoncowkami, false))
            {
                for (int i = -1; i < jakiPoziom; i++)
                {
                    char litera = (char)97;
                    int iteratorKoncowkowych = 0;
                    int lKoncowkowych = koncowkoweZTekstu.Count;
                    int pierwszaRowna;
                    List<string> zTaSamaPierwszaZBazy = new List<string>();


                    slowkoZBazyPosiadanych = litera.ToString() + litera.ToString() + litera.ToString();

                    using (StreamReader inputFile = new StreamReader(i != -1 ? sciezkaPlikow + poziomyJezyka[i] : sciezkaPlikuZ500Naj))
                    //  sprawdzanie, czy slowka naleza do ktoregos z
                    //  kolejnych poziomow. sprawdzanie wszyskich na raz
                    {
                        slowkoZBazyPosiadanych = inputFile.ReadLine().ToLower();
                        iteratorKoncowkowych = 0;

                        Func<bool> wZakresie = () => iteratorKoncowkowych < koncowkoweZTekstu.Count;
                        Func<int, bool> rowne = (numerLitery) => koncowkoweZTekstu[iteratorKoncowkowych].ToLower()[numerLitery] == slowkoZBazyPosiadanych[numerLitery];
                        Func<int, bool> zBazyWieksze = (numerLitery) => koncowkoweZTekstu[iteratorKoncowkowych].ToLower()[numerLitery] < slowkoZBazyPosiadanych[numerLitery];
                        Func<int, bool> zBazyMniejsze = (numerLitery) => koncowkoweZTekstu[iteratorKoncowkowych].ToLower()[numerLitery] > slowkoZBazyPosiadanych[numerLitery];
                        Func<bool> CzyPasuja = () => kontrolaKoncowek.czyWyrazyPasuja(koncowkoweZTekstu[iteratorKoncowkowych].ToLower(), slowkoZBazyPosiadanych);
                        do
                        {
                            while (wZakresie() && !zBazyWieksze(0))        //  dopóki słówka ze sprawdzanego poziomu mają niższą lub rowna litere alfabetu
                            {
                                while (wZakresie() && zBazyMniejsze(0))            //  dopoki słówka ze sprawdzanego poziomu mają niższą litere alfabetu
                                {
                                    slowkoZBazyPosiadanych = inputFile.ReadLine().ToLower();
                                    //outputFile.WriteLine("zle"+slowkoZBazyPosiadanych);
                                }
                                while (wZakresie() && rowne(0) && !inputFile.EndOfStream)            //  tworzymy liste slowek z bazy poziomu z rowna litera alfabetu
                                {
                                    if (zTaSamaPierwszaZBazy.Any() && zTaSamaPierwszaZBazy[0][0] != slowkoZBazyPosiadanych[0])
                                        zTaSamaPierwszaZBazy = new List<string>();
                                    zTaSamaPierwszaZBazy.Add(slowkoZBazyPosiadanych);
                                    slowkoZBazyPosiadanych = inputFile.ReadLine();
                                }

                                while (iteratorKoncowkowych < koncowkoweZTekstu.Count() && koncowkoweZTekstu[iteratorKoncowkowych].ToLower()[0] == zTaSamaPierwszaZBazy[0][0])  //  dopoki pierwsza litera ta sama
                                {
                                    foreach (string x in zTaSamaPierwszaZBazy)   //  sprawdzaj wszystkie slowka o danym poziomie i literze
                                    {
                                        if (kontrolaKoncowek.czyWyrazyPasuja(koncowkoweZTekstu[iteratorKoncowkowych], x))
                                        {
                                            outputFile.WriteLine(koncowkoweZTekstu[iteratorKoncowkowych] + " " + x);
                                            znaneZkoncowkami.Add(koncowkoweZTekstu[iteratorKoncowkowych]);
                                        }
                                    }
                                    iteratorKoncowkowych++;                     //  dla kolejnych slowek z ksiazki
                                }
                            }
                            iteratorKoncowkowych++;
                        } while (wZakresie() && zBazyWieksze(0));  //  dopoki slowka z naszego poziomu maja wyzsza litere alfabetu

                    }

                }
            }
            znaneZkoncowkami.Sort();
            List<string> lKonBezPowtarz = new List<string>();
            lKonBezPowtarz.Add(znaneZkoncowkami[0]);
            foreach (var x in znaneZkoncowkami)
                if (lKonBezPowtarz[lKonBezPowtarz.Count - 1] != x)
                    lKonBezPowtarz.Add(x);
            using (StreamWriter pisacz = new StreamWriter(plikZnanychSlowZKoncowkami))
            {
                foreach (var x in lKonBezPowtarz)
                {
                    pisacz.WriteLine(x);
                }
            }
            odejmijListeKoncowkowyc(lKonBezPowtarz);
        }

        public void odejmijListeKoncowkowyc(List<String> lKonc)
        {
            tlumacz tlumaczenia = new tlumacz();
            List<int> lista = new List<int>();
            List<string> listaWyj = new List<string>();
            for (int x = 0, y = 0; x < slowkaEng.Count && y < lKonc.Count;)
            {
                switch (String.Compare(slowkaEng[x].ToLower(), lKonc[y].ToLower()))
                {
                    case -1:
                        x = x + 1;
                        break;
                    case 0:
                        lista.Add(x);
                        x = x + 1;
                        break;
                    case 1:
                        y = y + 1;
                        break;
                }
            }
            using (StreamWriter outputLine = new StreamWriter(plikBezKoncowek))
            {
                int k = 0;
                for (int j = 0; j < slowkaEng.Count; j++)
                {
                    if (j != lista[k])
                    {
                        if (slowkaEng[j].Length > 1)
                        {
                            listaWyj.Add(slowkaEng[j]);
                            outputLine.WriteLine(slowkaEng[j]);
                        }
                    }
                    else
                    {
                        k++;
                        if (k == lista.Count())
                        {
                            while (++j < slowkaEng.Count)
                            {
                                if (slowkaEng[j].Length > 1)
                                {
                                    listaWyj.Add(slowkaEng[j]);
                                    outputLine.WriteLine(slowkaEng[j]);
                                }
                            }
                        }
                    }
                }
            }

            MessageBox.Show("Wiadomosc", "Udalo sie 2", MessageBoxButton.OK); 
            tlumaczenia.TlumaczZPliku(listaWyj);
        }
    }
}
