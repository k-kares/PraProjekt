﻿using System;
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
    public partial class UserView : UserControl
    {
        User OvajUser = new User();
        public UserView()
        {
            InitializeComponent();
        }

        public void SetUser(User user)
        {
            OvajUser = user;
            SetName();
        }

        public void SetName()
        {
            lblName.Content = OvajUser.Name;
            if(OvajUser.IsAdmin)
            {
                lblAdminStatus.Content = "Administrator";
            }
            else
            {
                lblAdminStatus.Content = "Predavač";
            }
        }
    }
}
