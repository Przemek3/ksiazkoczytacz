using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ksiazkoczytacz
{
    class paraPorownywanych
    {
        string zKoncowka;
        string bezKoncowki;
        int dluzszy, krotszy;
        private List<string> koncowkowe;
        private string item;

        public string Koncowka { get; set; }
        public int Roznica { get; }
        private char OdKoncaBez(int liczba)
        {
            return bezKoncowki[bezKoncowki.Length - liczba];
        }
        private char OdKoncaZ(int liczba)
        {
            return zKoncowka[zKoncowka.Length - liczba];
        }
        public paraPorownywanych(string zKonc, string bezKonc)
        {
            zKoncowka = zKonc.ToLower();
            bezKoncowki = bezKonc.ToLower();
            dluzszy = zKonc.Length;
            krotszy = bezKonc.Length;
            Roznica = dluzszy - krotszy;

        }

        public paraPorownywanych(List<string> koncowkowe, string item)
        {
            this.koncowkowe = koncowkowe;
            this.item = item;
        }

        public bool czyRownePoZamianie(char litera, string koncowka)  // wpisujemy numery od konca
        {
            if(OdKoncaBez(1) == litera)
                return zKoncowka == bezKoncowki.Remove(bezKoncowki.Length - 1) + koncowka;
            return false;
        }
        public bool czyRownePoDodaniu(string koncowka)
        {
            return zKoncowka == bezKoncowki + koncowka;
        }

        public bool czyPasuja()
        {
            switch (zKoncowka[zKoncowka.Length - 1])
            {
                case 'g':
                    Koncowka = "g";
                    return koncowkaIng();
                case 't':
                    Koncowka = "t";
                    return koncowkaEst();
                case 'r':
                    Koncowka = "r";
                    return koncowkaEr();
                case 's':
                    Koncowka = "s";
                    return koncowkaS();
                case 'd':
                    Koncowka = "d";
                    return koncowkaEd();
            }
            return false;
        }
        private bool koncowkaS()
        {
            if (Roznica == 2)
            {
                if (czyRownePoDodaniu("es"))
                {
                    return true;
                }
                else if(czyRownePoZamianie('f', "ves"))
                   return true;
                
            }
            else if (Roznica == 1)
            {
                if (czyRownePoDodaniu("s"))
                {
                    return true;
                }
                else if (OdKoncaBez(1) == 'e' && OdKoncaBez(2) == 'f')
                {
                    if (OdKoncaBez(1) == 'e') bezKoncowki= bezKoncowki.Remove(krotszy - 1);
                    if (czyRownePoZamianie('f', "ves"))
                        return true;
                }
                else if (OdKoncaBez(2) == 'i')
                {
                    //string temp = "";
                    if (bezKoncowki.Insert(bezKoncowki.Length - 2, "es").Contains(zKoncowka))
                        return true;

                }
            }
            return false;
        }

        private bool koncowkaIng()
        {
            if (Roznica == 4)
            {
                if (czyRownePoDodaniu(bezKoncowki[krotszy - 1] + "ing")) // sprawdza czy dodajemy taką samą plus ing
                    return true;
            }
            else if (Roznica == 3)
            {
                if (czyRownePoDodaniu("ing"))
                    return true;
            }
            else if (Roznica == 2)        //moze pozniej
            {
                if (OdKoncaBez(1) == 'e')
                {
                    if (bezKoncowki.Remove(krotszy-1) + "ing" == zKoncowka)
                        return true;
                    if (OdKoncaBez(2) == 'i')
                    {
                        if (bezKoncowki.Remove(krotszy - 2) + "ying" == zKoncowka)
                            return true;
                    }
                }
                
            }
            return false;
        }
        private bool koncowkaEd()
        {
            if (Roznica == 2 && !(OdKoncaBez(1)=='e'))      //  zeby wykluczyc weed
            {
                if (czyRownePoDodaniu("ed"))
                    return true;

                if (OdKoncaBez(1) == 'y')
                {
                    return czyRownePoZamianie('y', "ied");
                }
            }
            else if (Roznica == 1)
            {
                if (czyRownePoDodaniu("d"))
                {
                    return true;
                }
            }
            else if (Roznica == 3)
            {
                return czyRownePoDodaniu(OdKoncaBez(1) + "ed");
            }
            return false;
        }
        private bool koncowkaEr()
        {
            if (Roznica == 2 && !(OdKoncaBez(1) == 'e'))
            {
                if (czyRownePoDodaniu("er"))
                    return true;

                if (OdKoncaBez(1) == 'y' && OdKoncaZ(3) == 'i')
                {
                    return czyRownePoZamianie('y',"ier");
                }
            }
            else if (Roznica == 1)
            {
                return czyRownePoDodaniu("r");
            }
            else if (Roznica == 3)
            {
                return czyRownePoDodaniu(OdKoncaBez(1) + "er");
            }
            return false;
        }
        private bool koncowkaEst()
        {
            if (Roznica == 3)
            {
                if (czyRownePoDodaniu("est"))
                    return true;
                if (OdKoncaBez(1) == 'y' && OdKoncaZ(4) == 'i')
                {
                    return czyRownePoZamianie('y',"iest");
                }
            }
            else if (Roznica == 2)
                return czyRownePoDodaniu("st");
            else if (Roznica == 4)
            {
                return (czyRownePoDodaniu(OdKoncaBez(1) + "est"));
            }
            return false;
        }
    }
}
