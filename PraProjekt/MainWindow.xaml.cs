﻿using System;
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
        public MainWindow()
        {
            LoginUsera();
            InitializeComponent();
            CheckIfAdmin();
            DrawUser();
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
    }
}
