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
        private Button? lastPressedButton; //prati podatke u kojem je tabu
        public User OvajUser { get; set; }

        //gleda runtime direktorij, znaci praprojekt/bin/debug itd. tijekom rada u visual studiu
        //treba nacin da posaljem path u DodajKolegij i DodajObavijest 
        public const string Kolegiji_Path = "../../../Podaci/kolegiji.txt";
        public const string Obavijesti_Path = "../../../Podaci/obavijesti.txt";

        public List<Obavijest> obavijesti = new List<Obavijest>();
        public List<Kolegij> kolegiji = new List<Kolegij>();

        public int lastKolegijId;
        public int lastObavijestId;

        //provjerava da li je nesto removano, ako je window.closing treba sve iz lista stavit na txt file-ove
        public bool wasSomethingRemoved = false;
        public MainWindow()
        {
            LoginUsera();
            InitializeComponent();
            LoadKolegijiData();
            LoadObavijestiData();
            ClearSpace();
            CheckIfAdmin();
            DrawUser();
        }

        private void LoadKolegijiData()
        {
            try
            {
                kolegiji = Utilities.FileUtilities.LoadFileDataforKolegiji(Kolegiji_Path);
                if (kolegiji.Any())
                {
                    lastKolegijId = int.Parse(kolegiji.LastOrDefault().ID); 
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Greška pri učitavanju kolegija!");
            }

            PutContentOnScreen(kolegiji);
        }

        private void LoadObavijestiData()
        {
            try
            {
                obavijesti = Utilities.FileUtilities.LoadFileDataforObavijest(Obavijesti_Path);
                if (obavijesti.Any())
                {
                    lastObavijestId = int.Parse(obavijesti.LastOrDefault().ID); 
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Greška pri učitavanju obavijesti!");
            }
            
            PutContentOnScreen(obavijesti);
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

            LoadObavijestiData();

            ClearSpace();
            MakeButtonAddObavijest();
            
            PutContentOnScreen(obavijesti);
        }

        private void MakeButtonAddObavijest()
        {
            Button but = new Button() {
                Name = "AddObavijest",
                Content = "Napravi novu obavijest",
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Color.FromRgb(77, 73, 98)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(77,73, 98)),
            };
            but.Click += But_Click;
            StackPanelContent.Children.Add(but);
        }

        private void MakeButtonAddKolegij()
        {
            Button but = new Button()
            {
                Name = "AddKolegij",
                Content = "Napravi novi kolegij",
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Color.FromRgb(77, 73, 98)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(77, 73, 98)),
            };
            but.Click += But_Click;
            StackPanelContent.Children.Add(but);
            if (!OvajUser.IsAdmin)
            {
                but.Visibility = Visibility.Hidden;
            }
        }

        private async void But_Click(object sender, RoutedEventArgs e)
        {
            //napravi novu obavijest ili kolegij

            Button senderButton = (Button)sender;
            if(senderButton.Name == "AddKolegij")
            {
                Window window = new DodajKolegij(this);
                window.ShowDialog();
                ClearSpace();
                MakeButtonAddKolegij();
                LoadKolegijiData();
            }

            if (senderButton.Name == "AddObavijest")
            {
                Window window = new DodajObavijest(this);
                window.ShowDialog();
                ClearSpace();
                MakeButtonAddObavijest();
                LoadObavijestiData();
            }
        }

        private void BtnPredmeti_click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (IsActiveTab(but))
            {
                return;
            }
            lastPressedButton = but;

            LoadKolegijiData();

            ClearSpace();
            MakeButtonAddKolegij();

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
                if (kolegij.UsersName == OvajUser.Name || OvajUser.IsAdmin)
                {
                    MakeKolegij(kolegij); 
                }
            }
        }

        private void MakeObavijest(Obavijest obavijest)
        {
            ObavijestView ov = new ObavijestView(obavijest, OvajUser.IsAdmin);
            StackPanelContent.Children.Add(ov);
        }

        private void MakeKolegij(Kolegij kolegij)
        {
            KolegijView kv = new KolegijView(kolegij, OvajUser.IsAdmin);
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

    }
}
