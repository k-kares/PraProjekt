﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities;

namespace PraProjekt
{
    /// <summary>
    /// Interaction logic for DodajKorisnika.xaml
    /// </summary>
    public partial class DodajKorisnika : Window
    {
        private User trenutniKorisnik = new User();
        MainWindow MainW;
        public DodajKorisnika(MainWindow mw)
        {
            MainW = mw;
            InitializeComponent();
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmailKorisnika.Text.Contains("|") || tbNazivKorisnika.Text.Contains("|") || tbPasswordKorisnika.Text.Contains("|"))
            {
                MessageBox.Show("Nesmije bit znaka '|' u tekstu");
                return;
            }

            if (tbNazivKorisnika.Text == "" || tbPasswordKorisnika.Text == "")
            {
                MessageBox.Show("Popunite sve vrijednosti");
                return;
            }

            if (!tbEmailKorisnika.Text.Contains("@"))
            {
                MessageBox.Show("Neispravna email adresa!");
                return;
            }
            String korisnik = $"{tbNazivKorisnika.Text}|{tbEmailKorisnika.Text}|{tbPasswordKorisnika.Text}|{cbAdminStatus.IsChecked}|{++MainW.lastKorisnikId}";
            File.AppendAllText(konstante.Korisnici_Path, Environment.NewLine);
            File.AppendAllText(konstante.Korisnici_Path, korisnik);
            this.Close();
        }
    }
}
