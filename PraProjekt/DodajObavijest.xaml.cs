using System;
using System.Collections.Generic;
using System.Globalization;
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
        private User OvajUser;
        private List<Kolegij> kolegiji { get; set; }
        public DodajObavijest(MainWindow mw)
        {
            MainW = mw;
            InitializeComponent();
            OvajUser = mw.OvajUser;
            kolegiji = mw.kolegiji;
            LoadKolegij();
        }

        private void LoadKolegij()
        {
            List<string> listaKolegija = new List<string>();
            foreach(var kolegij in kolegiji)
            {
                if (OvajUser.IsAdmin)
                {
                    listaKolegija.Add(kolegij.Name);
                }
                else if(kolegij.UsersName == OvajUser.Name)
                {
                    listaKolegija.Add(kolegij.Name);
                }
            }
            cbKolegij.ItemsSource = listaKolegija;
            cbKolegij.SelectedIndex = 0;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            DateTime datumI = DateTime.ParseExact(dpDatumIsteka.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string datumIsteka = datumI.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime datumO = DateTime.ParseExact(DateTime.Today.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string datumObjave = datumO.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            String obavijest = $"{cbKolegij.SelectedItem.ToString()}|{tbNazivObavijesti.Text}|{tbObavijest.Text}|{datumObjave}|{datumIsteka}|{++MainW.lastObavijestId}";
            File.AppendAllText(konstante.Obavijesti_Path, Environment.NewLine);
            File.AppendAllText(konstante.Obavijesti_Path, obavijest);
            this.Close();
        }

        private void cbKolegij_Loaded(object sender, RoutedEventArgs e)
        {
            LoadKolegij();
        }
    }
}
