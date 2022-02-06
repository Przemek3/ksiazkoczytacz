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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Text.RegularExpressions;

namespace ksiazkoczytacz
{
    /// <summary>
    /// Logika interakcji dla klasy KtoreStrony.xaml
    /// </summary>
    public partial class KtoreStrony : Window
    {
        obslugaPdf pdfActions = new obslugaPdf();
        public KtoreStrony()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, TXTzacznijOd);
        }
        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            TextBox tx = sender as TextBox;
            if (tx.Text=="0" && !e.Handled)
            {
                tx.Text = "";
            }
            
        }

        private void bAnuluj(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bZatwierdz(object sender, RoutedEventArgs e)
        {
            toTekstAsync();
            this.Close();
        }
        public async Task toTekstAsync()
        { 
            await pdfActions.fromPdfToTableAsync(Int32.Parse(TXTzacznijOd.Text), Int32.Parse(TXTileStron.Text));
            pdfActions.toTextfile();
            pdfActions.stworzListeKoncowkowych();
        }
        private void FunkcjeKlawiaturowe(object sender, KeyEventArgs e)
        {
            // ... Test for F5 key.
            if (e.Key == Key.Enter)
            {
                IInputElement element = FocusManager.GetFocusedElement(this);
                if (element == TXTzacznijOd)
                {
                    FocusManager.SetFocusedElement(this, TXTileStron);
                }
                else if(element == TXTileStron)
                {
                    bZatwierdz(sender, e);
                }
            }
            if(e.Key == Key.Down)
            {
                FocusManager.SetFocusedElement(this, TXTileStron);
            }
            if (e.Key == Key.Up)
            {
                FocusManager.SetFocusedElement(this, TXTzacznijOd);
            }
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
