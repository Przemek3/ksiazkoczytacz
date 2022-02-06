using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ksiazkoczytacz
{
    /// <summary>
    /// Logika interakcji dla klasy oknoNieznanych.xaml
    /// </summary>
    public partial class oknoNieznanych : Window
    {
        obslugaDB pracaZBaza = new obslugaDB();
        private string sciezkaPlikuZNumerkamiDoNauki = @"C:\Users\pkolo\Documents\sciezkaZNumerkami.txt";


        public oknoNieznanych()
        { 
            
            List<string> items = new List<string>();
            foreach(var i in pracaZBaza.pobierzNieznane())
            {
                items.Add(i.Id + "   " + i.angielski + '\t' + '\t' + i.polski);
            }

            //Populate the ListBox from the item list
            
            InitializeComponent();
            Lista.ItemsSource = items;
        }

        private void btnWrzucDoZnanych(object sender, RoutedEventArgs e)
        {
            
            foreach(string s in pozyskajNumerki())
            {
                pracaZBaza.wrzucDoZnanych(int.Parse(s));
            }
        }

        private void btnUczSie(object sender, RoutedEventArgs e)
        {
            string text ="";
            foreach(string s in pozyskajNumerki())
            {
                text += s + ',';
            }
            using (StreamWriter outputFile = new StreamWriter(sciezkaPlikuZNumerkamiDoNauki))
            {
                outputFile.WriteLine(text);
            }
            Process.Start(@"C:\Users\pkolo\source\repos\ksiazkoczytacz\konsolka\bin\Debug\konsolka.exe");
        }

        private string[] pozyskajNumerki()
        {
            string wybrany = "";
            string[] listaStringow = new string[Lista.SelectedItems.Count];
            testowyText.Text = "";
            int j = 0;

            foreach (var item in Lista.SelectedItems)
            {
                wybrany = item.ToString();
                int i = 0;
                while (Char.IsDigit(wybrany[i]))
                {
                    listaStringow[j] += wybrany[i];
                    i += 1;
                }
                j += 1;
            }
            return listaStringow;
        }
    }
}
