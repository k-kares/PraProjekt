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

namespace PraProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //prati podatke u kojem je tabu
        private Button? lastPressedButton;
        public User OvajUser { get; set; }

        private const string Kolegiji_Path = "kolegiji.txt";
        private const string Obavijesti_Path = "obavijesti.txt";

        List<Obavijest> obavijesti = new List<Obavijest>();
        List<Kolegij> kolegiji = new List<Kolegij>();
        public MainWindow()
        {
            LoginUsera();
            InitializeComponent();
            LoadObavijestiData();
            LoadKolegijiData();
            CheckIfAdmin();
            DrawUser();
        }

        private void LoadKolegijiData()
        {
            //loadaj kolegiji iz datoteke
        }

        private void LoadObavijestiData()
        {
            //loadaj obavijesti iz datoteke
        }

        private void DrawUser()
        {
            uvUser.SetUser(OvajUser);
        }

        private void CheckIfAdmin()
        {
            if (OvajUser.IsAdmin)
            {
                btnDodaj.Visibility = Visibility.Visible;
            }
        }

        private void LoginUsera()
        {
            LoginWindow login = new LoginWindow(this);
            login.ShowDialog();
            if (OvajUser is null)
            {
                this.Close();
            }
        }

        private void BtnObavijest_click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (IsActiveTab(but)) 
            {
                return;
            }
            lastPressedButton = but;

            ClearSpace();
            //treba metoda koja stavlja button koji ce onClick stavit novu obavijest
            PutContentOnScreen(obavijesti);
        }

        private void BtnPredmeti_click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (IsActiveTab(but))
            {
                return;
            }
            lastPressedButton = but;

            ClearSpace();
            PutContentOnScreen(kolegiji);
        }


        private void PutContentOnScreen(List<Obavijest> obavijesti)
        {
            foreach (var obavijest in obavijesti)
            {
                MakeObavijest(obavijest);
            }
        }

        private void PutContentOnScreen(List<Kolegij> kolegiji)
        {
            foreach (var kolegij in kolegiji)
            {
                MakeKolegij(kolegij);
            }
        }

        private void MakeObavijest(Obavijest obavijest)
        {
            ObavijestView ov = new ObavijestView(obavijest);
            StackPanelContent.Children.Add(ov);
        }

        private void MakeKolegij(Kolegij kolegij)
        {
            KolegijView kv = new KolegijView(kolegij);
            StackPanelContent.Children.Add(kv);
        }

        private void BtnDodaj_click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (IsActiveTab(but))
            {
                return;
            }
            lastPressedButton = but;
            ClearSpace();
        }
        private void ClearSpace()
        {
            StackPanelContent.Children.Clear();
        }
        private bool IsActiveTab(Button? but)
        {
            if (lastPressedButton == but)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //save data iz liste kolegija u kolegiji.txt
            //save data iz liste obavijesti u obavijesti.txt
        }
    }
}
