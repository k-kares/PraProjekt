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
        private const string User_path = "users.txt";
        private List<User> users = new List<User>();

        public LoginWindow(MainWindow mw)
        {
            MainW = mw;
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            List<string> loaded = Utilities.FileUtilities.LoadFileData(User_path);
            MakeIntoUsers(loaded);
        }

        private void MakeIntoUsers(List<string> loaded)
        {
            for (int i = 0; i <= loaded.Count -2;)
            {
                string ime = loaded[i++];
                string email = loaded[i++];
                string password = loaded[i++];
                bool admin = bool.Parse(loaded[i++]);
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
            lblResult.Content = "Krivi mail ili password";
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
    }
}
