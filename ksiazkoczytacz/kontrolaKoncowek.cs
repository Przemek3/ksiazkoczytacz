using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ksiazkoczytacz
{
    static public class kontrolaKoncowek
    {
        static paraPorownywanych obecnaPara;
        static public bool czyMaKoncowke(string slowo)
        {
            char ostatnia = slowo[slowo.Length - 1];
            if ('s' == ostatnia)
            {
                return true;
            }
            if ("dr".Contains(ostatnia))
            {
                if (slowo[slowo.Length - 2] == 'e')
                    return true;
            }
            if (slowo.Length > 4)
                if ((ostatnia == 'g' && slowo[slowo.Length - 2] == 'n' && slowo[slowo.Length - 3] == 'i') || (ostatnia == 't' && slowo[slowo.Length - 2] == 's' && slowo[slowo.Length - 3] == 'e'))
                {
                    return true;
                }
            return false;
        }
        static public bool inneFormy(string slowo, string linijka)
        {
            if (slowo.Contains(linijka))
            {
                if (slowo == linijka) //|| slowo== linijka + "s"  || slowo == linijka + "d" || slowo == linijka + "ed" || slowo == linijka + "er" || slowo == linijka + "est" || slowo == linijka + "ing")
                    return true;
                int dluzszy = slowo.Length, krotszy = linijka.Length;
                int roznica = dluzszy - krotszy;
                if (roznica < 5)
                {
                    if (roznica < 4)
                    {
                        if (roznica < 3)
                        {
                            if (roznica < 2)
                            {
                                if ("srd".Contains(slowo[krotszy])) return true;
                            }
                            else if (slowo[krotszy] == 'e' && ("sdr".Contains(slowo[krotszy + 1]))) return true;
                        }
                        else if (('i' == slowo[krotszy] && 'n' == slowo[krotszy + 1] && 'g' == slowo[krotszy + 2]) || ('e' == slowo[krotszy] && 's' == slowo[krotszy + 1] && 't' == slowo[krotszy + 2])) return true;
                    }
                    else if (slowo[krotszy] == slowo[krotszy - 1] && 'i' == slowo[krotszy + 1] && 'n' == slowo[krotszy + 2] && 'g' == slowo[krotszy + 3]) return true;
                }
            }
            return false;
        }
        static public bool czyWyrazyPasuja(string wejzKoncowka, string wejbezKoncowki)
        {
            if (wejbezKoncowki.Length == 1)
                return false;
            if(wejzKoncowka[1]!= wejbezKoncowki[1] && wejzKoncowka[1]!='y' && wejbezKoncowki[1]=='i')   //  sprawdzenie drugiej litery
            {
                return false;
            }
            int roznica = wejzKoncowka.Length - wejbezKoncowki.Length;
            if (roznica > 4 || roznica<0)
                return false;
            obecnaPara = new paraPorownywanych(wejzKoncowka, wejbezKoncowki);


            return obecnaPara.czyPasuja();
        }
    }
}
