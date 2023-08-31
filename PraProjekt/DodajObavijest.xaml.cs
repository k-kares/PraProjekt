using System;
using System.Collections.Generic;
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
using Utilities;

namespace PraProjekt
{
    /// <summary>
    /// Interaction logic for DodajObavijest.xaml
    /// </summary>
    public partial class DodajObavijest : Window
    {
        MainWindow MainW;
        private List<Kolegij> kolegiji { get; set; }
        public DodajObavijest(MainWindow mw)
        {
            MainW = mw;
            InitializeComponent();
            kolegiji = mw.kolegiji;
            LoadKolegij();
        }

        private void LoadKolegij()
        {
            List<string> listaKolegija = new List<string>();
            foreach(var kolegij in kolegiji)
            {
                listaKolegija.Add(kolegij.Name);
            }
            cbKolegij.ItemsSource = listaKolegija;
            cbKolegij.SelectedIndex = 0;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            String obavijest = $"{cbKolegij.SelectedItem.ToString()}|{tbNazivObavijesti.Text}|{tbObavijest.Text}" + Environment.NewLine;

            /*ne znam kako pristupiti pathu bez da pretvorim mw.kolegiji_path u public property umjesto const
            string obavijestPath = mw.Obavijesti_Path;*/
            File.AppendAllText("Podaci/obavijesti.txt", obavijest);
            this.Close();
        }

        private void cbKolegij_Loaded(object sender, RoutedEventArgs e)
        {
            LoadKolegij();
        }
    }
}
