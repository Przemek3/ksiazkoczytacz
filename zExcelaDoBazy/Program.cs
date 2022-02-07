using System;
using System.IO;

namespace zExcelaDoBazy
{
    class Program
    {
        static string pobranaBaza = @"C:\Users\pkolo\Downloads\englishDictionary.csv";
        static string baza2 = @"C:\Users\pkolo\Downloads\englishDictionaryObrobiony.csv";
        static void Main(string[] args)
        {
            zExcelaDoExcela();
            Console.WriteLine("Hello World!");
        }
        static void zExcelaDoExcela()
        {
            string line;
            int i;
            using (StreamWriter pisana = new StreamWriter(baza2)) 
            using (StreamReader czytana = new StreamReader(pobranaBaza))
            {
                while ((line = czytana.ReadLine()) != null)
                {
                    i = 0;
                    for(; i < line.Length && line[i]!=',' && line[i] != ' '; i++)
                    {
                        pisana.Write(line[i]);
                    }
                    pisana.Write(';');
                    while(i<line.Length)
                    {
                        pisana.Write(line[i++]);
                    }
                    pisana.WriteLine();
                }

            }

        }
    }
}
