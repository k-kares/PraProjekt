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
    /// Interaction logic for UrediKorisnika.xaml
    /// </summary>
    public partial class UrediKorisnika : Window
    {
        public User trenutniKorisnik = new User();
        public List<User> korisnici = new List<User>();
        public List<string> lines = new List<string>();
        public UrediKorisnika(User ovajUser)
        {
            trenutniKorisnik = ovajUser;
            InitializeComponent();
            SetInfo(trenutniKorisnik);
        }

        private void SetInfo(User trenutniKorisnik)
        {
            tbNazivKorisnika.Text = trenutniKorisnik.Name;
            tbEmailKorisnika.Text = trenutniKorisnik.Email;
            cbAdminStatus.IsChecked = trenutniKorisnik.IsAdmin;
            //stari password se ne vidi namjerno
        }

        private void btnUredi_Click(object sender, RoutedEventArgs e)
        {
            LoadKorisniciData();
            foreach (var korisnik in korisnici)
            {
                if (korisnik.ID != trenutniKorisnik.ID)
                    lines.Add($"{korisnik.Name}|{korisnik.Email}|{korisnik.Password}|{korisnik.IsAdmin}|{korisnik.ID}");
                else {

                //ako se password ne mjenja, zadrzi stari. inace uzmi novi
                if (tbPasswordKorisnika.Text.Length < 1)
                {
                    lines.Add($"{tbNazivKorisnika.Text}|{tbEmailKorisnika.Text}|{korisnik.Password}|{cbAdminStatus.IsChecked}|{korisnik.ID}");
                }
                else
                {
                    lines.Add($"{tbNazivKorisnika.Text}|{tbEmailKorisnika.Text}|{tbPasswordKorisnika.Text}|{cbAdminStatus.IsChecked}|{korisnik.ID}");
                }
            }

            }
            File.Delete(konstante.Korisnici_Path);
            File.AppendAllLines(konstante.Korisnici_Path, lines);

            this.Close();
        }
        private void LoadKorisniciData()
        {
            try
            {
                korisnici = Utilities.FileUtilities.LoadFileDataforKorisnici(konstante.Korisnici_Path);
            }
            catch (Exception)
            {
                MessageBox.Show("Greška pri učitavanju korisnika!");
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            LoadKorisniciData();
            foreach (var korisnik in korisnici)
            {
                if (korisnik.ID != trenutniKorisnik.ID)
                    lines.Add($"{korisnik.Name}|{korisnik.Email}|{korisnik.Password}|{korisnik.IsAdmin}|{korisnik.ID}");
            }

            File.Delete(konstante.Korisnici_Path);
            File.AppendAllLines(konstante.Korisnici_Path, lines);

            this.Close();
        }
    }
}
