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
    /// Interaction logic for KolegijView.xaml
    /// </summary>
    public partial class KolegijView : UserControl
    {
        Kolegij ovajKolegij = new Kolegij();
        bool isAdmin = false;
        public KolegijView(Kolegij kolegij, bool isAdmin_)
        {
            isAdmin = isAdmin_;
            ovajKolegij = kolegij;
            InitializeComponent();
            SetInfo();
        }

        private void SetInfo()
        {
            lblImeKolegija.Content = ovajKolegij.Name;
            lblImeProfesora.Content = ovajKolegij.UsersName;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (isAdmin)
            {
                UrediKolegij window = new UrediKolegij(ovajKolegij);
                window.ShowDialog(); 
            }
        }
    }
}
