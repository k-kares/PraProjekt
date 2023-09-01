using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class KorisnikView : UserControl
    {
        User trenutniUser = new User();
        public KorisnikView(User ovajUser)
        {
            trenutniUser=ovajUser;
            InitializeComponent();
            SetInfo();
        }

        public void SetInfo()
        {
            lblName.Content = trenutniUser.Name;
            if (trenutniUser.IsAdmin)
            {
                lblAdminStatus.Content = "Administrator";
            }
            else
            {
                lblAdminStatus.Content = "Predavač";
            }
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UrediKorisnika window = new UrediKorisnika(trenutniUser);
            window.ShowDialog();
        }
    }
}
