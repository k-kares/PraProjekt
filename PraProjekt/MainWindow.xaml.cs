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

        public List<Obavijest> obavijesti = new List<Obavijest>();
        public List<Kolegij> kolegiji = new List<Kolegij>();
        public List<User> korisnici = new List<User>();

        public int lastKolegijId;
        public int lastObavijestId;
        public int lastKorisnikId;

        public MainWindow()
        {
            LoginUsera();
            InitializeComponent();
            LoadKolegijiData();
            LoadObavijestiData();
            ClearSpace();
            HidebtnKorisnici();


            DrawUser();
            lastPressedButton = btnObavijesti;
            MakeButtonAddObavijest();
            PutContentOnScreen(obavijesti);
        }

        private void LoadKolegijiData()
        {
            try
            {
                kolegiji = Utilities.FileUtilities.LoadFileDataforKolegiji(konstante.Kolegiji_Path);
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
                obavijesti = Utilities.FileUtilities.LoadFileDataforObavijest(konstante.Obavijesti_Path);
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
        private void LoadKorisniciData()
        {
            try
            {
                korisnici = Utilities.FileUtilities.LoadFileDataforKorisnici(konstante.Korisnici_Path);
                if (korisnici.Any())
                {
                    lastKorisnikId = int.Parse(korisnici.LastOrDefault().ID);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Greška pri učitavanju korisnika!");
            }

            PutContentOnScreen(korisnici);
        }

        private void DrawUser()
        {
            uvUser.SetUser(OvajUser);
        }

        private void HidebtnKorisnici()
        {
            if (!OvajUser.IsAdmin)
            {
                btnKorisnici.Visibility = Visibility.Hidden;
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
        private void MakeButtonAddUser()
        {
            Button but = new Button()
            {
                Name = "AddUser",
                Content = "Napravi novog korisnika",
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),
                Background = new SolidColorBrush(Color.FromRgb(77, 73, 98)),
                BorderBrush = new SolidColorBrush(Color.FromRgb(77, 73, 98)),
            };
            but.Click += But_Click;
            StackPanelContent.Children.Add(but);
            //adda usera
        }

        private async void But_Click(object sender, RoutedEventArgs e)
        {
            //napravi novu obavijest, kolegij ili korisnika

            Button senderButton = (Button)sender;
            if (senderButton.Name == "AddKolegij")
            {
                Window window = new DodajKolegij(this);
                window.ShowDialog();
                ClearSpace();
                MakeButtonAddKolegij();
                LoadKolegijiData();
            }

            else if (senderButton.Name == "AddObavijest")
            {
                Window window = new DodajObavijest(this);
                window.ShowDialog();
                ClearSpace();
                MakeButtonAddObavijest();
                LoadObavijestiData();
            }

            else if (senderButton.Name == "AddUser")
            {
                Window window = new DodajKorisnika(this);
                window.ShowDialog();
                ClearSpace();
                MakeButtonAddUser();
                LoadKorisniciData();
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

        private void btnKorisnici_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (IsActiveTab(but))
            {
                return;
            }
            lastPressedButton = but;

            LoadKorisniciData();

            ClearSpace();
            MakeButtonAddUser();

            PutContentOnScreen(korisnici);
        }

        public void PutContentOnScreen(List<Obavijest> obavijesti)
        {
            foreach (var obavijest in obavijesti)
            {
                if(DateTime.Parse(obavijest.DatumIsteka) > DateTime.Today)
                MakeObavijest(obavijest);
            }
        }

        public void PutContentOnScreen(List<Kolegij> kolegiji)
        {
            foreach (var kolegij in kolegiji)
            {
                if (kolegij.UsersName == OvajUser.Name || OvajUser.IsAdmin)
                {
                    MakeKolegij(kolegij); 
                }
            }
        }
        public void PutContentOnScreen(List<User> korisnici)
        {
            foreach (var korisnik in korisnici)
            {
                MakeKorisnik(korisnik);
            }
        }

        private void MakeKorisnik(User korisnik)
        {
            KorisnikView uv = new KorisnikView(korisnik);
            StackPanelContent.Children.Add(uv);
        }

        private void MakeObavijest(Obavijest obavijest)
        {
            ObavijestView ov = new ObavijestView(obavijest, OvajUser);
            StackPanelContent.Children.Add(ov);
        }

        private void MakeKolegij(Kolegij kolegij)
        {
            KolegijView kv = new KolegijView(kolegij, OvajUser);
            StackPanelContent.Children.Add(kv);
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

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //da refresha prikazane podatke nakon zatvaranja UrediProzora
            System.Threading.Thread.Sleep(1);

            switch(lastPressedButton.Name)
            {
                case "btnObavijesti":
                    {
                        LoadObavijestiData();

                        ClearSpace();
                        MakeButtonAddObavijest();

                        PutContentOnScreen(obavijesti);
                        break;
                    }
                case "btnPredmeti":
                    {
                        LoadKolegijiData();

                        ClearSpace();
                        MakeButtonAddKolegij();

                        PutContentOnScreen(kolegiji);  
                        break;
                    }
                case "btnKorisnici":
                    {
                        LoadKorisniciData();

                        ClearSpace();
                        MakeButtonAddUser();

                        PutContentOnScreen(korisnici);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
