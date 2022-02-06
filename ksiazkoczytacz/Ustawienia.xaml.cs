using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logika interakcji dla klasy Ustawienia.xaml
    /// </summary>
    public partial class Ustawienia : Window
    {
        int numerPoziomu;
        private string sciezkaUstawienia = @"C:\Users\pkolo\Documents\Ustawienia";
        public Ustawienia()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            exp.Header = rb.Content;
            numerPoziomu = Int16.Parse(rb.Uid);
        }
        private void zwiniecie(object sender, RoutedEventArgs e)
        {
            exp.Height = 40;
        }
        private void rozwiniecie(object sender, RoutedEventArgs e)
        {
            exp.Height = 200;
        }

        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
