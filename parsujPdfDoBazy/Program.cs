using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parsujPdfDoBazy
{
    class Program
    {
        private static string name = @"C:\Users\pkolo\Downloads\largedictionary.pdf";
        static void Main(string[] args)
        {
            try
            {
                PdfReader pdfReader = new PdfReader(name);
                PdfDocument pdfDocument = new PdfDocument(pdfReader);
                ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                for (int licznikStron = 0; licznikStron < 443; licznikStron++)
                {
                    string[] tab = (string[])PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(38 + licznikStron), strategy).Split('\n').ToArray();
                    foreach (string x in tab)
                    {
                        Console.WriteLine(x);
                        Console.ReadKey();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Blad");
                Console.ReadKey();
            }

        }
    }
}
