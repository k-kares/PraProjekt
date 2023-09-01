using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public UrediObavijest(bool isAdmin, Obavijest ovaObavijest)
        {
            trenutnaObavijest=ovaObavijest;
            InitializeComponent();
            SetInfo(trenutnaObavijest);
        }

        private void SetInfo(Obavijest ovaObavijest)
        {
            tbNazivObavijesti.Text = ovaObavijest.Title;
            tbObavijest.Text = ovaObavijest.Message;
            lbKolegij.Content = ovaObavijest.ImeKolegija;
        }

        private void btnUredi_Click(object sender, RoutedEventArgs e)
        {
            LoadObavijestiData();
            foreach (var obavijest in obavijesti)
            {
                if(obavijest.ID != trenutnaObavijest.ID)
                    lines.Add($"{obavijest.ImeKolegija}|{obavijest.Title}|{obavijest.Message}|{obavijest.ID}");
                else
                    lines.Add($"{lbKolegij.Content}|{tbNazivObavijesti.Text}|{tbObavijest.Text}|{obavijest.ID}");
            }
            File.Delete(konstante.Obavijesti_Path);
            File.AppendAllLines(konstante.Obavijesti_Path, lines);

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

    }
}
