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
using System.Windows.Shapes;
using Utilities;

namespace PraProjekt
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        MainWindow MainW;

        //gleda runtime direktorij, znaci praprojekt/bin/debug itd. tijekom rada u visual studiu
        public const string User_path = "../../../Podaci/korisnici.txt";
        private List<User> users = new List<User>();

        public LoginWindow(MainWindow mw)
        {
            MainW = mw;
            InitializeComponent();
            LoadUsers();
            tbMail.Focus();
        }

        private void LoadUsers()
        {
            try
            {
                List<string> loaded = Utilities.FileUtilities.LoadFileData(User_path);
                MakeIntoUsers(loaded);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MakeIntoUsers(List<string> loaded)
        {
            for (int i = 0; i <= loaded.Count -2;)
            {
                string ime = loaded[i++];
                string email = loaded[i++];
                string password = loaded[i++];
                bool admin = bool.Parse(loaded[i++]);
                //i++; skipa id
                User newUser = new User(ime, email, password, admin);
                users.Add(newUser);
            }
        }

        private void btnOkClick(object sender, RoutedEventArgs e)
        {
            if (!Check())
            {
                WrongValues();
            }
            else
            {
                this.Close();
            }
        }

        private void WrongValues()
        {
            lblResult.Content = "Korisnik ne postoji ili greška pri upisu!";
        }

        private bool Check()
        {
            foreach (var user in users) 
            {
                if (tbMail.Text == user.Email && pbPass.Password == user.Password)
                {
                    MainW.OvajUser = user;
                    return true;
                }
            }
            return false;
        }

        private void OnEnterLogin(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                btnOkClick(sender, e);
            }
        }
    }
}
