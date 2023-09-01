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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities;
using System.IO;

namespace PraProjekt
{
    /// <summary>
    /// Interaction logic for DodajKolegij.xaml
    /// </summary>
    public partial class DodajKolegij : Window
    {
        MainWindow MainW;
        private User OvajUser;
        public DodajKolegij(MainWindow mw)
        {
            MainW = mw;
            InitializeComponent();

            OvajUser = mw.OvajUser;
            tbPredavačKolegija.Text = OvajUser.Name;
            if (!OvajUser.IsAdmin)
            {
                tbPredavačKolegija.IsReadOnly = true;
                tbPredavačKolegija.Foreground = Brushes.Gray;
            }
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            String kolegij = $"{tbNazivKolegija.Text}|{tbPredavačKolegija.Text}|{++MainW.lastKolegijId}";
            File.AppendAllText(konstante.Kolegiji_Path, Environment.NewLine);
            File.AppendAllText(konstante.Kolegiji_Path, kolegij);
            this.Close();
        }
    }
}
