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
    /// Interaction logic for UrediKolegij.xaml
    /// </summary>
    public partial class UrediKolegij : Window
    {
        public List<Kolegij> kolegiji = new List<Kolegij>();
        public Kolegij trenutniKolegij;
        public List<string> lines = new List<string>();
        public UrediKolegij(Kolegij ovajKolegij)
        {
            trenutniKolegij = ovajKolegij;
            InitializeComponent();
            SetInfo(trenutniKolegij);
        }

        private void SetInfo(Kolegij ovajKolegij)
        {
            tbNazivKolegija.Text = ovajKolegij.Name;
            tbPredavačKolegija.Text = ovajKolegij.UsersName;
        }

        private void btnUredi_Click(object sender, RoutedEventArgs e)
        {
            LoadKolegijiData();
            foreach (var kolegij in kolegiji)
            {
                if (kolegij.ID != trenutniKolegij.ID)
                    lines.Add($"{kolegij.Name}|{kolegij.UsersName}|{kolegij.ID}");
                else
                    lines.Add($"{tbNazivKolegija.Text}|{tbPredavačKolegija.Text}|{kolegij.ID}");
            }
            File.Delete(konstante.Kolegiji_Path);
            File.AppendAllLines(konstante.Kolegiji_Path, lines);

            this.Close();
        }

        private void LoadKolegijiData()
        {
            try
            {
                kolegiji = Utilities.FileUtilities.LoadFileDataforKolegiji(konstante.Kolegiji_Path);
            }
            catch (Exception)
            {
                MessageBox.Show("Greška pri učitavanju obavijesti!");
            }
        }
    }
}
