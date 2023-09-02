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
        public List<Obavijest> obavijesti = new List<Obavijest>();
        public Kolegij trenutniKolegij;
        public List<string> lines = new List<string>();
        public User trenutniUser = new User();

        public UrediKolegij(Kolegij ovajKolegij, User ovajUser)
        {
            trenutniKolegij = ovajKolegij;
            trenutniUser = ovajUser;
            InitializeComponent();
            SetInfo(trenutniKolegij);
            DisabletbPredavacKolegija();
            HidebtnUredi();
            HidebtnObrisi();
        }

        private void DisabletbPredavacKolegija()
        {
            if(!trenutniUser.IsAdmin)
            {
                tbPredavačKolegija.Focusable = false;
                tbPredavačKolegija.IsReadOnly = true;
                tbPredavačKolegija.Background = Brushes.Gray;
            }
        }

        private void HidebtnObrisi()
        {
            if (!trenutniUser.IsAdmin && trenutniUser.Name != trenutniKolegij.UsersName)
            {
                btnObrisi.Visibility = Visibility.Hidden;
                return;
            }
        }

        private void HidebtnUredi()
        {
            if (!trenutniUser.IsAdmin && trenutniUser.Name != trenutniKolegij.UsersName)
            {
                btnObrisi.Visibility = Visibility.Hidden;
                return;
            }
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

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            //obrise kolegij
            LoadKolegijiData();
            foreach (var kolegij in kolegiji)
            {
                if (kolegij.ID != trenutniKolegij.ID)
                    lines.Add($"{kolegij.Name}|{kolegij.UsersName}|{kolegij.ID}");
            }
            File.Delete(konstante.Kolegiji_Path);
            File.AppendAllLines(konstante.Kolegiji_Path, lines);


            //obrise obavijesti kolegija koji vise ne postoji
            lines.Clear();
            LoadObavijestiData();
            foreach (var obavijest in obavijesti)
            {
                if (obavijest.ImeKolegija != tbNazivKolegija.Text)
                    lines.Add($"{obavijest.ImeKolegija}|{obavijest.Title}|{obavijest.Message}|{obavijest.ImePredavaca}|{obavijest.DatumObjave}|{obavijest.DatumIsteka}|{obavijest.ID}");
            }
            File.Delete(konstante.Obavijesti_Path);
            File.AppendAllLines(konstante.Obavijesti_Path, lines);

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
                MessageBox.Show("Greška pri učitavanju kolegija!");
            }
        }

        private void LoadObavijestiData()
        {
            try
            {
                obavijesti = Utilities.FileUtilities.LoadFileDataforObavijest(konstante.Obavijesti_Path);
            }
            catch (Exception)
            {
                MessageBox.Show("Greška pri učitavanju obavijesti!");
            }
        }
    }
}
