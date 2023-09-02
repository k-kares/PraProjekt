using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ObavijestWindow.xaml
    /// </summary>
    public partial class UrediObavijest : Window
    {
        public List<Obavijest> obavijesti = new List<Obavijest>();
        public Obavijest trenutnaObavijest;
        public List<string> lines = new List<string>();
        public User trenutniUser = new User();


        public UrediObavijest(Obavijest ovaObavijest, User ovajUser)
        {
            trenutnaObavijest=ovaObavijest;
            trenutniUser=ovajUser;
            InitializeComponent();
            SetInfo(trenutnaObavijest);
            HidebtnUredi();
            HidebtnObrisi();
        }

        private void HidebtnObrisi()
        {
            if (!trenutniUser.IsAdmin && trenutniUser.Name!=trenutnaObavijest.ImePredavaca)
            {
                btnObrisi.Visibility = Visibility.Hidden; 
                return;
            }
        }

        private void HidebtnUredi()
        {
            if (!trenutniUser.IsAdmin && trenutniUser.Name != trenutnaObavijest.ImePredavaca)
            {
                btnObrisi.Visibility = Visibility.Hidden;
                return;
            }
        }

        private void SetInfo(Obavijest ovaObavijest)
        {
            tbNazivObavijesti.Text = ovaObavijest.Title;
            tbObavijest.Text = ovaObavijest.Message;
            lbKolegij.Content = ovaObavijest.ImeKolegija;
            dpDatumIsteka.SelectedDate = DateTime.ParseExact(ovaObavijest.DatumIsteka, "dd/MM/yyyy", null);
        }

        private void btnUredi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadObavijestiData();
                foreach (var obavijest in obavijesti)
                {
                    //DateTime dt = DateTime.ParseExact(dpDatumIsteka.SelectedDate.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                    DateTime dt = dpDatumIsteka.SelectedDate.Value;
                    string noviDatum = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (obavijest.ID != trenutnaObavijest.ID)
                        lines.Add($"{obavijest.ImeKolegija}|{obavijest.Title}|{obavijest.Message}|{obavijest.ImePredavaca}|{obavijest.DatumObjave}|{obavijest.DatumIsteka}|{obavijest.ID}");
                    else
                        lines.Add($"{lbKolegij.Content}|{tbNazivObavijesti.Text}|{tbObavijest.Text}|{obavijest.ImePredavaca}|{obavijest.DatumObjave}|{noviDatum}|{obavijest.ID}");
                }
                File.Delete(konstante.Obavijesti_Path);
                File.AppendAllLines(konstante.Obavijesti_Path, lines);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
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

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            LoadObavijestiData();
            foreach (var obavijest in obavijesti)
            {
                if (obavijest.ID != trenutnaObavijest.ID)
                    lines.Add($"{obavijest.ImeKolegija}|{obavijest.Title}|{obavijest.Message}|{obavijest.ImePredavaca}|{obavijest.DatumObjave}|{obavijest.DatumIsteka}|{obavijest.ID}");
            }

            File.Delete(konstante.Obavijesti_Path);
            File.AppendAllLines(konstante.Obavijesti_Path, lines);

            this.Close();
        }
    }
}
