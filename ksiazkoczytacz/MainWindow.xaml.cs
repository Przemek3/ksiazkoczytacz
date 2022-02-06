using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Net.Http;
using HtmlAgilityPack;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace ksiazkoczytacz
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        obslugaPdf pdfowiec = new obslugaPdf();
        tlumacz myTranslator = new tlumacz();
        
        public MainWindow()
        {
            InitializeComponent();
            //txtBX.AppendText(App.)
        }

        private void przyciskParsuj(object sender, RoutedEventArgs e)
        {
            Parser.Parsuj();

            //wyswietlacz.Text = "";
            
            using(naukaSlowekEntities context = new naukaSlowekEntities())
            {
                doNauczenia slowo = context.doNauczenia.FirstOrDefault(x => x.polski == "by");
                foreach (doNauczenia item in Parser.tablica)
                {
                    //wyswietlacz.Text += item.polski + '\n';
                    slowo = context.doNauczenia.FirstOrDefault(x => x.angielski == item.angielski);
                    if (slowo == null)
                    {
                        slowo = new doNauczenia { angielski = item.angielski, polski = item.polski, liczbaDobrych = 0 };
                        context.doNauczenia.Add(slowo);
                    }
                    //else wyswietlacz.Text = "Takie słowo już jest";
                }
                context.SaveChanges();
            }
        }
        private void bPobierzZPDF(object sender, RoutedEventArgs e)
        {
            KtoreStrony oknoWyboruStron = new KtoreStrony();
            oknoWyboruStron.Show();
            
        }

        private void btnUstawienia(object sender, RoutedEventArgs e)
        {
            Ustawienia ust = new Ustawienia();
            ust.Show();

        }
        private async Task tl()
        {
            await myTranslator.Translate();
            //wyswietlacz.Text = myTranslator.odpowiedz;
        }

        private void bOtworzSlowka(object sender, RoutedEventArgs e)
        {
            oknoNieznanych otwarciePytan = new oknoNieznanych();
            otwarciePytan.Show();
        }

    }
}
