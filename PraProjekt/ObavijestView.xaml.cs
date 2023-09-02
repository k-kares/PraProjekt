﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ObavijestView.xaml
    /// </summary>
    public partial class ObavijestView : UserControl
    {
        Obavijest ovaObavijest = new Obavijest();
        private bool admin;
        private User trenutniUser;
        public ObavijestView(Obavijest obavijest, User ovajUser)
        {
            ovaObavijest = obavijest;
            trenutniUser = ovajUser;
            InitializeComponent();
            SetInfo();
        }

        private void SetInfo()
        {
            lblImeKolegija.Content = ovaObavijest.ImeKolegija;
            lblTitle.Content = ovaObavijest.Title;
            tbContent.Text = ovaObavijest.Message;
            lblDatumObjave.Content = ovaObavijest.DatumObjave;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UrediObavijest window = new UrediObavijest(ovaObavijest, trenutniUser);
            window.ShowDialog(); 
        }
    }
}
