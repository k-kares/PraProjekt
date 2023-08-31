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
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            String kolegij = Environment.NewLine + $"{tbNazivKolegija.Text}|{tbPredavačKolegija.Text}";

            /*ne znam kako pristupiti pathu bez da pretvorim mw.kolegiji_path u public property umjesto const
            string kolegijPath = mw.Kolegiji_Path;*/
            File.AppendAllText("Podaci/kolegiji.txt", kolegij);
            this.Close();
        }
    }
}
